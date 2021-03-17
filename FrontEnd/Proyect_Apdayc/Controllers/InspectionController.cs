using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System.Xml;
using System.Text;
using System.Drawing;
using System.IO;
using System.Net;

namespace Proyect_Apdayc.Controllers
{
    public class InspectionController : Base
    {
        //
        // GET: /Inspection/
        
        public const string nomAplicacion = "SRGDA";

        private const string K_SESSION_CARACTERISTICA = "___DTOCaracteristica";
        private const string K_SESSION_CARACTERISTICA_DEL = "___DTOCaracteristicaDEL";
        private const string K_SESSION_CARACTERISTICA_ACT = "___DTOCaracteristicaACT";

        List<DTOCaracteristica> caracteristicas = new List<DTOCaracteristica>();

        private List<DTOCaracteristica> CaracteristicaTmpUPDEstado
        {
            get
            {
                return (List<DTOCaracteristica>)Session[K_SESSION_CARACTERISTICA_ACT];
            }
            set
            {
                Session[K_SESSION_CARACTERISTICA_ACT] = value;
            }
        }
        private List<DTOCaracteristica> CaracteristicaTmpDelBD
        {
            get
            {
                return (List<DTOCaracteristica>)Session[K_SESSION_CARACTERISTICA_DEL];
            }
            set
            {
                Session[K_SESSION_CARACTERISTICA_DEL] = value;
            }
        }
        public List<DTOCaracteristica> CaracteristicaTmp
        {
            get
            {
                return (List<DTOCaracteristica>)Session[K_SESSION_CARACTERISTICA];
            }
            set
            {
                Session[K_SESSION_CARACTERISTICA] = value;
            }
        }

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESSION_CARACTERISTICA);
            Session.Remove(K_SESSION_CARACTERISTICA_ACT);
            Session.Remove(K_SESSION_CARACTERISTICA_DEL);
            return View();
        }

        public JsonResult ListarInpection(int skip, int take, int page, int pageSize, decimal insId, decimal estId, decimal tipoest, decimal? subtipoest, decimal socio, string tipodiv, decimal? division)
        {
            var lista = Listainpection(GlobalVars.Global.OWNER, insId, estId , tipoest, subtipoest, socio, tipodiv, division, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEInspectionEst { ListaInspection = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEInspectionEst { ListaInspection = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEInspectionEst> Listainpection(string owner, decimal insId, decimal estId, decimal tipoest, decimal? subtipoest, decimal socio, string tipodiv, decimal? division, int pagina, int cantRegxPag)
        {
            return new BLInspectionEst().usp_Get_InspectionPage(owner, insId, estId, tipoest, subtipoest, socio, tipodiv, division, pagina, cantRegxPag);
        }

        static DateTime? hour;
        [HttpGet()]
        public JsonResult Obtiene(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLInspectionEst servicio = new BLInspectionEst();
                    BEInspectionEst obj = new BEInspectionEst();
                    obj = servicio.Obtener(GlobalVars.Global.OWNER, id);
                    hour = obj.INSP_HOUR;
                    ObtieneCaracteristica(obj.EST_ID);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "obtener datos inspection", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public void obtenerCaracteristicaestablecimiento(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    ObtieneCaracteristica(id);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "obtener datos caracteristica", ex);
            }
        }

        public BEEstablecimiento ObtieneCabeceraEstablecimiento(decimal id)
        {
            Resultado retorno = new Resultado();
            BEEstablecimiento en = new BEEstablecimiento();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLEstablecimiento servicio = new BLEstablecimiento();

                    var entidad = servicio.ObtenerCabeceraEstablecimiento(id, GlobalVars.Global.OWNER);

                    if (entidad != null)
                    {
                        en = entidad;
                        retorno.data = Json(entidad, JsonRequestBehavior.AllowGet);
                        retorno.message = "Datos de establecimiento encontrados";
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.message = "No se ha podido encontrar los datos de establecimiento";
                        retorno.result = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener datos cabecera establecimiento", ex);
            }
            return en;
        }

        public JsonResult ObtieneCaracteristica(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLEstablecimiento servicio = new BLEstablecimiento();
                    var establecimiento = servicio.ObtieneCaracteristicasInpeccion(id, GlobalVars.Global.OWNER);

                    if (establecimiento.Caracteristicas != null)
                    {
                        caracteristicas = new List<DTOCaracteristica>();
                        if (establecimiento.Caracteristicas != null)
                        {
                            establecimiento.Caracteristicas.ForEach(s =>
                            {
                                caracteristicas.Add(new DTOCaracteristica
                                {
                                    Id = s.CHAR_ID,
                                    caracteristica = s.CHAR_SHORT,
                                    Idcaracteristica = s.CHAR_ID.ToString(),
                                    IdEstablecimiento = Convert.ToString(s.EST_ID),
                                    TipoEstablecimiento = Convert.ToString(s.ESTT_ID),
                                    IdSubTipoEstablecimiento = Convert.ToString(s.SUBE_ID),
                                    Valor = s.VALUE.ToString(),
                                    usercreate = UsuarioActual,
                                    EnBD = true,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    FechaCrea = s.LOG_DATE_CREAT,
                                    UsuarioModifica = s.LOG_USER_UPDAT,
                                    FechaModifica = s.LOG_DATE_UPDATE,
                                    Activo = s.ENDS.HasValue ? false : true
                                });
                            });
                            CaracteristicaTmp = caracteristicas;
                        }
                        retorno.data = Json(establecimiento, JsonRequestBehavior.AllowGet);
                        retorno.message = "Caracteristica encontrada";
                        retorno.result = 1;
                    }

                    else
                    {
                        retorno.message = "No se ha podido encontrar la caracteristica";
                        retorno.result = 0;
                    }
                }
            }
            catch (Exception ex)
            { 
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "obtener caracteristica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insertar(BEInspectionEst Inspection)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEEstablecimiento objEstablecimiento = new BEEstablecimiento();
                    BEEstablecimiento cabeceraEstablecimiento = new BEEstablecimiento();
                    BEInspectionEst obj = new BEInspectionEst();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.INSP_ID = Inspection.INSP_ID;
                    obj.EST_ID = Inspection.EST_ID;
                    obj.INSP_DOC = Inspection.INSP_DOC;
                    obj.INSP_OBS = Inspection.INSP_OBS;
                    obj.BPS_ID = Inspection.BPS_ID;
                    obj.INSP_DATE = Inspection.INSP_DATE;
                    if (Inspection.INSP_HOUR != null)
                        obj.INSP_HOUR = Inspection.INSP_HOUR;
                    else
                        obj.INSP_HOUR = hour;
                    obj.valgraba = Inspection.valgraba;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    objEstablecimiento.Caracteristicas = obtenerCaracteristica();
                    cabeceraEstablecimiento = ObtieneCabeceraEstablecimiento(obj.EST_ID);
                    objEstablecimiento.EST_ID = Inspection.EST_ID;
                    objEstablecimiento.OWNER = GlobalVars.Global.OWNER;
                    objEstablecimiento.ESTT_ID = cabeceraEstablecimiento.ESTT_ID;
                    objEstablecimiento.SUBE_ID = cabeceraEstablecimiento.SUBE_ID;

                    if (obj.valgraba == 0)
                    {
                        var datos = new BLInspectionEst().Insertar(obj, objEstablecimiento);
                    }
                    else
                    {
                        List<BECaracteristicaEst> listaCarDel = null;
                        if (CaracteristicaTmpDelBD != null)
                        {
                            listaCarDel = new List<BECaracteristicaEst>();
                            CaracteristicaTmpDelBD.ForEach(x => { listaCarDel.Add(new BECaracteristicaEst { CHAR_ID = x.Id }); });
                        }
                        List<BECaracteristicaEst> listaCarUpdEst = null;
                        if (CaracteristicaTmpUPDEstado != null)
                        {
                            listaCarUpdEst = new List<BECaracteristicaEst>();
                            CaracteristicaTmpUPDEstado.ForEach(x => { listaCarUpdEst.Add(new BECaracteristicaEst { CHAR_ID = x.Id }); });
                        }

                        obj.INSP_ID = Inspection.INSP_ID;
                        obj.LOG_USER_UPDAT = UsuarioActual;
                        var datos = new BLInspectionEst().Actualizar(obj, objEstablecimiento, listaCarDel, listaCarUpdEst);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "insert inspection", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(decimal idinspection)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLInspectionEst servicio = new BLInspectionEst();
                    var result = servicio.Eliminar(new BEInspectionEst
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        INSP_ID = idinspection,
                        LOG_USER_UPDAT = UsuarioActual
                    });
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar Inspection", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BECaracteristicaEst> obtenerCaracteristica()
        {
            List<BECaracteristicaEst> datos = new List<BECaracteristicaEst>();
            if (CaracteristicaTmp != null)
            {
                CaracteristicaTmp.ForEach(x =>
                {
                    datos.Add(new BECaracteristicaEst
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        CHAR_ID = Convert.ToDecimal(x.Id),
                        ESTT_ID = Convert.ToDecimal(x.TipoEstablecimiento),
                        SUBE_ID = Convert.ToDecimal(x.IdSubTipoEstablecimiento),
                        VALUE = Convert.ToDecimal(x.Valor),
                        LOG_USER_CREAT = UsuarioActual,
                    });
                });
            }
            return datos;
        }

        [HttpPost]
        public JsonResult AddCaracteristica(DTOCaracteristica entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (entidad.GetTipo)
                    {
                        var lista = from a in CaracteristicaTmp
                                    where a.caracteristica == entidad.caracteristica
                                    select a;

                        if (lista.Count() > 0)
                        {
                            retorno.result = 1;
                            return Json(retorno, JsonRequestBehavior.AllowGet);
                        }
                    }

                    caracteristicas = CaracteristicaTmp;
                    if (caracteristicas == null) caracteristicas = new List<DTOCaracteristica>();

                    if (Convert.ToInt32(entidad.Id) <= 0)
                    {
                        decimal nuevoId = 1;
                        if (caracteristicas.Count > 0) nuevoId = caracteristicas.Max(x => x.Id) + 1;
                        entidad.Id = nuevoId;
                        entidad.Activo = true;
                        entidad.EnBD = false;
                        entidad.UsuarioCrea = UsuarioActual;
                        entidad.FechaCrea = DateTime.Now;
                        caracteristicas.Add(entidad);
                    }
                    else
                    {
                        //var item = caracteristicas.Where(x => x.Idcaracteristica == entidad.Idcaracteristica).FirstOrDefault();
                        var item = caracteristicas.Where(x => x.Id == entidad.Id).FirstOrDefault();
                        entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                        entidad.Activo = item.Activo;
                        if (item.GetTipo)
                            entidad.GetTipo = true;

                        entidad.UsuarioCrea = item.UsuarioCrea;
                        entidad.FechaCrea = item.FechaCrea;
                        if (entidad.EnBD)
                        {
                            entidad.UsuarioModifica = UsuarioActual;
                            entidad.FechaModifica = DateTime.Now;
                        }

                        caracteristicas.Remove(item);
                        caracteristicas.Add(entidad);
                    }
                    CaracteristicaTmp = caracteristicas;
                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddCaracteristica(decimal id)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    caracteristicas = CaracteristicaTmp;
                    if (caracteristicas != null)
                    {
                        var objDel = caracteristicas.Where(x => Convert.ToDecimal(x.Idcaracteristica) == id).FirstOrDefault();
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (CaracteristicaTmpUPDEstado == null) CaracteristicaTmpUPDEstado = new List<DTOCaracteristica>();
                                if (CaracteristicaTmpDelBD == null) CaracteristicaTmpDelBD = new List<DTOCaracteristica>();

                                var itemUpd = CaracteristicaTmpUPDEstado.Where(x => Convert.ToDecimal(x.Idcaracteristica) == id).FirstOrDefault();
                                var itemDel = CaracteristicaTmpDelBD.Where(x => Convert.ToDecimal(x.Idcaracteristica) == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) CaracteristicaTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) CaracteristicaTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) CaracteristicaTmpDelBD.Add(objDel);
                                    if (itemUpd != null) CaracteristicaTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                caracteristicas.Remove(objDel);
                                caracteristicas.Add(objDel);
                            }
                            else
                            {
                                caracteristicas.Remove(objDel);
                            }

                            CaracteristicaTmp = caracteristicas;
                            retorno.result = 1;
                            retorno.message = "OK";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarCaracteristica()
        {
            caracteristicas = CaracteristicaTmp;
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tblCaracteristica' border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th>  <th  class='k-header' style='display:none'>IdCaracteristica</th>");
                    shtml.Append("<th class='k-header'>Característica</th><th class='k-header'>Valor</th><th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header' style='display:none'>subtipoId</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (caracteristicas != null)
                    {
                        foreach (var item in caracteristicas.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.Idcaracteristica);
                            shtml.AppendFormat("<td >{0}</td>", item.caracteristica);
                            shtml.AppendFormat("<td >{0}</td>", item.Valor);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.IdSubTipoEstablecimiento);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td> <a href=# onclick='updAddCaracteristica({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Idcaracteristica);
                            shtml.AppendFormat("<a href=# onclick='delAddCaracteristica({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Idcaracteristica, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Característica" : "Activar Característica");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        }
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneCaracteristicaTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var caracteristica = CaracteristicaTmp.Where(x => Convert.ToDecimal(x.Idcaracteristica) == idDir).FirstOrDefault();
                    retorno.data = Json(caracteristica, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneParametroTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

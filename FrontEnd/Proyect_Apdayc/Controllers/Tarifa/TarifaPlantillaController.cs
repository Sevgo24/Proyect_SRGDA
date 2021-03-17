using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;

namespace Proyect_Apdayc.Controllers.Tarifa
{
    public class TarifaPlantillaController : Base
    {
        public const string nomAplicacion = "SRGDA";

        private const string K_SESION_VARIABLE_TARIFA = "___DTOVariableTarifa";
        private const string K_SESION_VARIABLE_TARIFA_DEL = "___DTOariableTarifaDEL";
        private const string K_SESION_VARIABLE_TARIFA_ACT = "___DTOariableTarifaACT";


        private const string K_SESION_VALOR_TARIFA = "___DTOValorTarifa";
        private const string K_SESION_VALOR_TARIFA_DEL = "___DTOValorTarifaDEL";
        private const string K_SESION_VALOR_TARIFA_ACT = "___DTOValorTarifaACT";

        //
        // GET: /TarifaPlantilla/
        List<DTOTarifaCaracteristica> caracteristicas = new List<DTOTarifaCaracteristica>();
        List<DTOTarifaValor> valores = new List<DTOTarifaValor>();

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESION_VARIABLE_TARIFA);
            Session.Remove(K_SESION_VARIABLE_TARIFA_DEL);
            Session.Remove(K_SESION_VARIABLE_TARIFA_ACT);
            Session.Remove(K_SESION_VALOR_TARIFA);
            Session.Remove(K_SESION_VALOR_TARIFA_DEL);
            Session.Remove(K_SESION_VALOR_TARIFA_ACT);
            return View();
        }

        public JsonResult Listar(int skip, int take, int page, int pageSize, string group, string desc, decimal nro, string fini, string ffin, int estado, int confecha)
        {
            Resultado retorno = new Resultado();
            var lista = BLListar(GlobalVars.Global.OWNER, desc, nro, Convert.ToDateTime(fini), Convert.ToDateTime(ffin), estado, confecha, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BETarifaPlantilla { ListarTarifaPlantilla = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BETarifaPlantilla { ListarTarifaPlantilla = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BETarifaPlantilla> BLListar(string owner, string desc, decimal nro, DateTime fini, DateTime ffin, int estado, int confecha, int pagina, int cantRegxPag)
        {
            return new BLTarifaPlantilla().Listar(owner, desc, nro, fini, ffin, estado, confecha, pagina, cantRegxPag);
        }


        #region CARACTERISTICAS

        public List<DTOTarifaCaracteristica> CaracteristicasTmp
        {
            get
            {
                return (List<DTOTarifaCaracteristica>)Session[K_SESION_VARIABLE_TARIFA];
            }
            set
            {
                Session[K_SESION_VARIABLE_TARIFA] = value;
            }
        }

        private List<DTOTarifaCaracteristica> CaracteristicasTmpUPDEstado
        {
            get
            {
                return (List<DTOTarifaCaracteristica>)Session[K_SESION_VARIABLE_TARIFA_ACT];
            }
            set
            {
                Session[K_SESION_VARIABLE_TARIFA_ACT] = value;
            }
        }

        private List<DTOTarifaCaracteristica> CaracteristicasTmpDelBD
        {
            get
            {
                return (List<DTOTarifaCaracteristica>)Session[K_SESION_VARIABLE_TARIFA_DEL];
            }
            set
            {
                Session[K_SESION_VARIABLE_TARIFA_DEL] = value;
            }
        }

        [HttpPost]
        public JsonResult AddCaracteristica(DTOTarifaCaracteristica entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                int registroNuevo = 0;
                int registroModificar = 0;
                caracteristicas = CaracteristicasTmp;
                if (caracteristicas != null)
                {
                    if (entidad.Id == 0) // Nuevo
                    {
                        registroNuevo = caracteristicas.Where(p => p.IdCaracteristica == entidad.IdCaracteristica).Count();
                    }
                    else
                    {
                        registroModificar = caracteristicas.Where(p => p.IdCaracteristica == entidad.IdCaracteristica && p.Id != entidad.Id).Count();
                    }
                }

                if ((entidad.Id == 0 && registroNuevo == 0)
                     || (entidad.Id != 0 && registroModificar == 0)
                   )
                {

                    if (caracteristicas == null) caracteristicas = new List<DTOTarifaCaracteristica>();
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
                        var item = caracteristicas.Where(x => x.Id == entidad.Id).FirstOrDefault();
                        entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                        entidad.Activo = item.Activo;
                        entidad.UsuarioCrea = item.UsuarioCrea;
                        entidad.FechaCrea = item.FechaCrea;
                        caracteristicas.Remove(item);
                        caracteristicas.Add(entidad);
                    }
                    CaracteristicasTmp = caracteristicas;
                    retorno.result = 1;
                    retorno.message = "OK";
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "La caracteristica ya existe.";
                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddCaracteristica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddCaracteristica(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                caracteristicas = CaracteristicasTmp;
                if (caracteristicas != null)
                {
                    var objDel = caracteristicas.Where(x => x.Id == id).FirstOrDefault();
                    if (objDel != null)
                    {
                        if (objDel.EnBD)
                        {
                            bool blActivo = !objDel.Activo;

                            if (CaracteristicasTmpUPDEstado == null) CaracteristicasTmpUPDEstado = new List<DTOTarifaCaracteristica>();
                            if (CaracteristicasTmpDelBD == null) CaracteristicasTmpDelBD = new List<DTOTarifaCaracteristica>();

                            var itemUpd = CaracteristicasTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                            var itemDel = CaracteristicasTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                            if (!(objDel.Activo))
                            {
                                if (itemUpd == null) CaracteristicasTmpUPDEstado.Add(objDel);
                                if (itemDel != null) CaracteristicasTmpDelBD.Remove(itemDel);
                            }
                            else
                            {
                                if (itemDel == null) CaracteristicasTmpDelBD.Add(objDel);
                                if (itemUpd != null) CaracteristicasTmpUPDEstado.Remove(itemUpd);
                            }
                            objDel.Activo = blActivo;
                            caracteristicas.Remove(objDel);
                            caracteristicas.Add(objDel);
                        }
                        else
                        {
                            caracteristicas.Remove(objDel);
                        }
                        CaracteristicasTmp = caracteristicas;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddCaracteristica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BETarifaPlantillaCaracteristica> obtenerCaracteristicas()
        {
            List<BETarifaPlantillaCaracteristica> datos = new List<BETarifaPlantillaCaracteristica>();
            if (CaracteristicasTmp != null)
            {
                CaracteristicasTmp.ForEach(x =>
                {
                    datos.Add(new BETarifaPlantillaCaracteristica
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        TEMPL_ID = Convert.ToInt32(x.Id),
                        CHAR_ID = x.IdCaracteristica,
                        CHAR_LONG = x.DesCaracteristica,
                        IND_TR = Convert.ToString(x.Tramo),
                        LOG_USER_CREAT = UsuarioActual,

                    });
                });
            }
            return datos;
        }

        public JsonResult ObtieneCaracteristicaTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                var variable = CaracteristicasTmp.Where(x => x.Id == idDir).FirstOrDefault();
                retorno.data = Json(variable, JsonRequestBehavior.AllowGet);
                retorno.message = "OK";
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneCaracteristicaTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarCaracteristica(decimal idCaracteristica = 0)
        {
            caracteristicas = CaracteristicasTmp;
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblCaracteristica' border=0 width='100%;' class='k-grid k-widget' id='tblCaracteristica'>");
                shtml.Append("<thead><tr>");
                shtml.Append("<th class='k-header'></th>");
                shtml.Append("<th class='k-header'>Id</th>");
                shtml.Append("<th class='k-header'>Caracteristica</th>");
                shtml.Append("<th class='k-header'>Tramo</th>");
                shtml.Append("<th class='k-header'>Usuario Creación</th>");
                shtml.Append("<th class='k-header'>Fecha Creación</th>");
                shtml.Append("<th  class='k-header' style='width:60px'></th></tr></thead>");

                if (caracteristicas != null)
                {
                    foreach (var item in caracteristicas.OrderBy(x => x.Id))
                    {
                        if (item.Activo)
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td style='width:25px'> ");
                            shtml.AppendFormat("<a href=# onclick='verDeta({0});'><img id='expand" + item.Id + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.Id);
                            shtml.Append("</td>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td nowrap>{0}</td>", item.DesCaracteristica);
                            if (item.Tramo == 1)
                                shtml.AppendFormat("<td style='text-align:center'><input  type='checkbox' DISABLED checked  ></td>");
                            else
                                shtml.AppendFormat("<td style='text-align:center'><input  type='checkbox' DISABLED></td>");

                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);

                            shtml.AppendFormat("<td style='width:80px'>");
                            shtml.AppendFormat("                        <a href=# onclick='nuevoValor({0},{2});'><img src='../Images/botones/nuevo.png' width=20px    border=0 title='{1}'></a>&nbsp;&nbsp;", item.Id, "Agregar valor.", item.Tramo);
                            shtml.AppendFormat("                        <a href=# onclick='updAddCaracteristica({0});'><img src='../Images/iconos/edit.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.Id, "Modificar caracteristica.");
                            shtml.AppendFormat("                        <a href=# onclick='delAddCaracteristica({0});'><img src='../Images/iconos/{1}'      title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar caracteristica." : "Activar caracteristica.");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");

                            shtml.Append("<tr>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td colspan='6'>");
                            //shtml.Append("<div id='" + "div" + item.Id.ToString() + "'  > "); 
                            if (item.Id == idCaracteristica)
                                shtml.Append("<div id='" + "div" + item.Id.ToString() + "'  > ");
                            else
                                shtml.Append("<div style='display:none;' id='" + "div" + item.Id.ToString() + "'  > ");
                            shtml.Append(getHtmlTableDetaCaracteristica(item.Id));

                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");


                        }
                    }
                }
                shtml.Append("</table>");
                retorno.message = shtml.ToString();
                if (caracteristicas != null)
                {
                    int registros = caracteristicas.Where(p => p.Activo == true).Count();
                    //retorno.result = registros;
                    retorno.Code = registros;
                }
                else
                {
                    //retorno.result = 0;
                    retorno.Code = 0;
                }
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                //retorno.result = -1;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarCaracteristica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public StringBuilder getHtmlTableDetaCaracteristica(decimal id)
        {
            var carac = CaracteristicasTmp.Where(p => p.Id == id).FirstOrDefault();
            valores = ValoresTmp;
            if (valores != null && valores.Count > 0)
                valores = ValoresTmp.Where(p => p.IdCaracteristica == id).ToList();
            StringBuilder shtml = new StringBuilder();

            shtml.Append("<table border=0 width='100%;' class='k-grid k-widget' id='FiltroTabla'>");
            shtml.Append("<thead>");

            shtml.Append("<tr>");
            shtml.Append("<td colspan=10 class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Valores de la caracteristica</td>");
            shtml.Append("</tr>");

            shtml.Append("<tr>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Id</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='display:none'>IdCaracteristica</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Valor</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Tramo</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Desde</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Hasta</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Usuario Creación</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Fecha Creación</th>");
            shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='width:60px'></th></tr></thead>");

            if (valores != null && valores.Count > 0)
            {
                foreach (var item in valores.OrderBy(x => x.Id))
                {
                    if (item.Activo)
                    {
                        shtml.Append("<tr class='k-grid-content'>");
                        shtml.AppendFormat("<td >{0}</td>", item.Id);
                        shtml.AppendFormat("<td style='display:none'>{0}</td>", item.IdCaracteristica);
                        shtml.AppendFormat("<td style='width:250px'>{0}</td>", item.Descripcion);
                        shtml.AppendFormat("<td >{0}</td>", carac.Tramo == 0 ? "NO" : "SI");
                        shtml.AppendFormat("<td >{0}</td>", item.Desde.ToString("### ##0.0000"));

                        shtml.AppendFormat("<td >{0}</td>", (carac.Tramo == 0 || item.Hasta == 0) ? "" : item.Hasta.ToString("### ##0.0000"));
                        shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                        shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);

                        shtml.AppendFormat("<td style='width:60px'> <a href=# onclick='updAddValor({0},{2});'><img src='../Images/iconos/edit.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.Id, "Modificar Variable", carac.Tramo);
                        shtml.AppendFormat("                        <a href=# onclick='delAddValor({0});'><img src='../Images/iconos/{1}'      title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Variable" : "Activar Variable");
                        shtml.Append("</td>");
                        shtml.Append("</tr>");
                    }
                }
            }
            shtml.Append("</table>");
            return shtml;
        }

        #endregion


        #region VALORES

        public List<DTOTarifaValor> ValoresTmp
        {
            get
            {
                return (List<DTOTarifaValor>)Session[K_SESION_VALOR_TARIFA];
            }
            set
            {
                Session[K_SESION_VALOR_TARIFA] = value;
            }
        }

        private List<DTOTarifaValor> ValoresTmpUPDEstado
        {
            get
            {
                return (List<DTOTarifaValor>)Session[K_SESION_VALOR_TARIFA_ACT];
            }
            set
            {
                Session[K_SESION_VALOR_TARIFA_ACT] = value;
            }
        }

        private List<DTOTarifaValor> ValoresTmpDelBD
        {
            get
            {
                return (List<DTOTarifaValor>)Session[K_SESION_VALOR_TARIFA_DEL];
            }
            set
            {
                Session[K_SESION_VALOR_TARIFA_DEL] = value;
            }
        }

        [HttpPost]
        public JsonResult AddValor(DTOTarifaValor entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                List<DTOTarifaValor> listaValidacion = null;
                valores = ValoresTmp;
                if (valores != null)
                {
                    var car = CaracteristicasTmp.Where(p => p.Id == entidad.IdCaracteristica).FirstOrDefault();
                    var valoresCaracteristica = valores.Where(p => p.IdCaracteristica == entidad.IdCaracteristica).ToList();

                    listaValidacion = valoresCaracteristica.Where(p => p.IdCaracteristica == entidad.IdCaracteristica &&
                                                        p.Id != entidad.Id &&
                                                        p.Activo == true &&
                        (car.Tramo == 0 && (p.Desde == entidad.Desde)) ||
                        (car.Tramo == 1 && (p.Desde == entidad.Desde || p.Hasta == entidad.Hasta))
                        //(car.Tramo == 1 && (p.Desde >= entidad.Desde && entidad.Desde <= p.Hasta) )

                        ).ToList();

                    //(car.Tramo == 1 && (p.Desde == entidad.Desde || p.Hasta == entidad.Hasta) ) VALIDACION DE RANGO

                }

                if (listaValidacion == null || listaValidacion.Count == 0)
                {
                    if (valores == null) valores = new List<DTOTarifaValor>();
                    if (Convert.ToInt32(entidad.Id) <= 0)
                    {
                        decimal nuevoId = 1;
                        if (valores.Count > 0) nuevoId = valores.Max(x => x.Id) + 1;
                        entidad.Id = nuevoId;
                        entidad.Activo = true;
                        entidad.EnBD = false;
                        entidad.UsuarioCrea = UsuarioActual;
                        entidad.FechaCrea = DateTime.Now;
                        valores.Add(entidad);
                    }
                    else
                    {
                        var item = valores.Where(x => x.Id == entidad.Id).FirstOrDefault();
                        entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                        entidad.Activo = item.Activo;
                        entidad.UsuarioCrea = UsuarioActual;
                        entidad.FechaCrea = DateTime.Now;
                        valores.Remove(item);
                        valores.Add(entidad);
                    }
                    ValoresTmp = valores;
                    retorno.result = 1;
                    retorno.message = "OK";
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "Los datos ingresados ya existen, ingrese otro valor.";
                }

            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddValor", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddValor(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                valores = ValoresTmp;
                if (valores != null)
                {
                    var objDel = valores.Where(x => x.Id == id).FirstOrDefault();

                    if (objDel != null)
                    {
                        if (objDel.EnBD)
                        {
                            bool blActivo = !objDel.Activo;

                            if (ValoresTmpUPDEstado == null) ValoresTmpUPDEstado = new List<DTOTarifaValor>();
                            if (ValoresTmpDelBD == null) ValoresTmpDelBD = new List<DTOTarifaValor>();

                            var itemUpd = ValoresTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                            var itemDel = ValoresTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                            if (!(objDel.Activo))
                            {
                                if (itemUpd == null) ValoresTmpUPDEstado.Add(objDel);
                                if (itemDel != null) ValoresTmpDelBD.Remove(itemDel);
                            }
                            else
                            {
                                if (itemDel == null) ValoresTmpDelBD.Add(objDel);
                                if (itemUpd != null) ValoresTmpUPDEstado.Remove(itemUpd);
                            }
                            objDel.Activo = blActivo;
                            valores.Remove(objDel);
                            valores.Add(objDel);
                        }
                        else
                        {
                            valores.Remove(objDel);
                        }
                        ValoresTmp = valores;
                        retorno.result = 1;
                        retorno.message = "OK";
                        retorno.data = Json(objDel, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddValor", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        private List<BETarifaPlantillaValor> obtenerValores()
        {
            List<BETarifaPlantillaValor> datos = new List<BETarifaPlantillaValor>();
            if (ValoresTmp != null)
            {
                ValoresTmp.ForEach(x =>
                {
                    datos.Add(new BETarifaPlantillaValor
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        TEMPS_ID = Convert.ToInt32(x.Id),
                        TEMPL_ID = Convert.ToInt32(x.IdCaracteristica),
                        SECC_DESC = x.Descripcion,
                        SECC_FROM = x.Desde,
                        SECC_TO = x.Hasta,
                        LOG_USER_CREAT = UsuarioActual,
                        IND_TR = x.Tramo
                    });
                });
            }
            return datos;
        }

        public JsonResult ObtieneValorTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                var variable = ValoresTmp.Where(x => x.Id == idDir).FirstOrDefault();
                retorno.data = Json(variable, JsonRequestBehavior.AllowGet);
                retorno.message = "OK";
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneValorTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion


        //*************************************************************
        [HttpPost]
        public JsonResult Insertar(BETarifaPlantilla plantilla)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    plantilla.OWNER = GlobalVars.Global.OWNER;
                    plantilla.Caracteristicas = obtenerCaracteristicas();
                    plantilla.Valores = obtenerValores();

                    if (plantilla.TEMP_ID == 0)
                    {
                        plantilla.LOG_USER_CREAT = UsuarioActual;
                        var datos = new BLTarifaPlantilla().Insertar(plantilla);
                    }
                    else
                    {
                        plantilla.LOG_USER_UPDATE = UsuarioActual;

                        //.setting caracteristica eliminar
                        List<BETarifaPlantillaCaracteristica> listaCaracteristicaDel = null;
                        if (CaracteristicasTmpDelBD != null)
                        {
                            listaCaracteristicaDel = new List<BETarifaPlantillaCaracteristica>();
                            CaracteristicasTmpDelBD.ForEach(x => { listaCaracteristicaDel.Add(new BETarifaPlantillaCaracteristica { TEMPL_ID = x.Id }); });
                        }
                        //setting caracteristica activar
                        List<BETarifaPlantillaCaracteristica> listaCaracteristicaUpdEst = null;
                        if (CaracteristicasTmpUPDEstado != null)
                        {
                            listaCaracteristicaUpdEst = new List<BETarifaPlantillaCaracteristica>();
                            CaracteristicasTmpUPDEstado.ForEach(x => { listaCaracteristicaUpdEst.Add(new BETarifaPlantillaCaracteristica { TEMPL_ID = x.Id }); });
                        }

                        //.setting valor eliminar
                        List<BETarifaPlantillaValor> listaValorDel = null;
                        if (ValoresTmpDelBD != null)
                        {
                            listaValorDel = new List<BETarifaPlantillaValor>();
                            ValoresTmpDelBD.ForEach(x => { listaValorDel.Add(new BETarifaPlantillaValor { TEMPS_ID = x.Id, TEMPL_ID = x.IdCaracteristica }); });
                        }
                        //setting valor activar
                        List<BETarifaPlantillaValor> listaValorUpdEst = null;
                        if (ValoresTmpUPDEstado != null)
                        {
                            listaValorUpdEst = new List<BETarifaPlantillaValor>();
                            ValoresTmpUPDEstado.ForEach(x => { listaValorUpdEst.Add(new BETarifaPlantillaValor { TEMPS_ID = x.Id, TEMPL_ID = x.IdCaracteristica }); });
                        }


                        var datos = new BLTarifaPlantilla().Actualizar(plantilla,
                                                                 listaCaracteristicaDel, listaCaracteristicaUpdEst,
                                                                 listaValorDel, listaValorUpdEst
                                                                );
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Obtener(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                BETarifaPlantilla tarifa = new BETarifaPlantilla();
                tarifa = new BLTarifaPlantilla().Obtener(GlobalVars.Global.OWNER, id);


                if (tarifa.Caracteristicas != null)
                {
                    caracteristicas = new List<DTOTarifaCaracteristica>();
                    if (tarifa.Caracteristicas != null)
                    {
                        tarifa.Caracteristicas.ForEach(s =>
                        {
                            caracteristicas.Add(new DTOTarifaCaracteristica
                            {
                                Id = s.TEMPL_ID,
                                IdCaracteristica = s.CHAR_ID,
                                DesCaracteristica = s.CHAR_LONG,
                                Tramo = Convert.ToDecimal(s.IND_TR),
                                EnBD = true,
                                Activo = s.ENDS.HasValue ? false : true,
                                UsuarioCrea = s.LOG_USER_CREAT,
                                FechaCrea = s.LOG_DATE_CREAT
                            });
                        });
                        CaracteristicasTmp = caracteristicas;
                    }


                    valores = new List<DTOTarifaValor>();
                    if (tarifa.Valores != null)
                    {
                        tarifa.Valores.ForEach(s =>
                        {
                            valores.Add(new DTOTarifaValor
                            {
                                Id = s.TEMPS_ID,
                                IdCaracteristica = s.TEMPL_ID,
                                Descripcion = s.SECC_DESC,
                                Desde = s.SECC_FROM,
                                Hasta = s.SECC_TO,
                                EnBD = true,
                                Activo = s.ENDS.HasValue ? false : true,
                                UsuarioCrea = s.LOG_USER_CREAT,
                                FechaCrea = s.LOG_DATE_CREAT,
                                Tramo = s.IND_TR
                            });
                        });
                        ValoresTmp = valores;
                    }


                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(tarifa, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "No se ha encontrado la definición de gasto";
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                var servicio = new BLTarifaPlantilla();
                var plantilla = new BETarifaPlantilla();

                plantilla.OWNER = GlobalVars.Global.OWNER;
                plantilla.TEMP_ID = id;
                plantilla.LOG_USER_UPDATE = UsuarioActual;
                servicio.Eliminar(plantilla);

                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult ObtenerXDescripcion(BETarifaPlantilla plantilla)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLTarifaPlantilla servicio = new BLTarifaPlantilla();
                plantilla.OWNER = GlobalVars.Global.OWNER;
                int resultado = servicio.ObtenerXDescripcion(plantilla);
                if (resultado >= 1)
                    retorno.Code = 1;
                else
                    retorno.Code = 0;

                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerXDescripcion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }

}

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

namespace Proyect_Apdayc.Controllers
{
    public class ClaseCreacionController : Base
    {
        private const string K_SESION_DERECHO = "___DTODerecho";
        private const string K_SESION_DERECHO_DEL = "______DTODerechoDEL";
        private const string K_SESION_DERECHO_ACT = "___DTODerechoACT";

        // GET: /ClaseCreacion/
        List<DTODerecho> Derecho = new List<DTODerecho>();


        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESION_DERECHO);
            Session.Remove(K_SESION_DERECHO_DEL);
            Session.Remove(K_SESION_DERECHO_ACT);
            return View();
        }

        public ActionResult Edit()
        {
            Init(false);
            return View();
        }

        public JsonResult Listar_PageJson_Clase_Creacion(int skip, int take, int page, int pageSize, string group, string owner, string clas, int st)
        {
            Resultado retorno = new Resultado();

            var lista = Listar_Page_Clase_Creacion(GlobalVars.Global.OWNER, clas, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEClaseCreacion { ClaseCreacion = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEClaseCreacion { ClaseCreacion = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEClaseCreacion> Listar_Page_Clase_Creacion(string owner, string clas, int st, int pagina, int cantRegxPag)
        {
            return new BLClaseCreacion().Listar_Page_Clase_Creacion(owner, clas, st, pagina, cantRegxPag);
        }

        private static void validacion(out bool exito, out string msg_validacion, BEClaseCreacion entidad)
        {
            exito = true;
            msg_validacion = string.Empty;

            if (exito && string.IsNullOrEmpty(entidad.CLASS_COD))
            {
                msg_validacion = "Ingrese una descripción corta";
                exito = false;
            }

            if (exito && string.IsNullOrEmpty(entidad.CLASS_DESC))
            {
                msg_validacion = "Ingrese una descripción";
                exito = false;
            }
        }

        [HttpPost]
        public JsonResult Insertar(BEClaseCreacion entidad)
        {
            bool exito = true;
            Resultado retorno = new Resultado();
            string msg_validacion = "";
            try
            {
                if (!isLogout(ref retorno))
                {
                    validacion(out exito, out msg_validacion, entidad);
                    entidad.OWNER = GlobalVars.Global.OWNER;
                    entidad.LOG_USER_CREAT = UsuarioActual;
                    entidad.ClaseCreacion = obtenerDetalles();

                    if (exito)
                    {
                        var servicio = new BLClaseCreacion();

                        entidad.LOG_USER_CREAT = UsuarioActual;
                        servicio.Insertar(entidad);

                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = msg_validacion;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "insertar clase creacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Actualizar(BEClaseCreacion entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEClaseCreacion obj = new BEClaseCreacion();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.CLASS_COD = entidad.CLASS_COD;
                    obj.CLASS_DESC = entidad.CLASS_DESC;
                    obj.LOG_USER_UPDATE = UsuarioActual;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    obj.ClaseCreacion = obtenerDetalles();

                    //1.setting formatos de modalidad eliminar
                    List<BEClaseCreacion> listaCarDel = null;
                    if (DerechoTmpDelBD != null)
                    {
                        listaCarDel = new List<BEClaseCreacion>();
                        DerechoTmpDelBD.ForEach(x => { listaCarDel.Add(new BEClaseCreacion { SEQUENCE = x.Id, CLASS_COD = x.IdClase }); });
                    }
                    //setting formatos de modalidad activar
                    List<BEClaseCreacion> listaCarUpdEst = null;
                    if (DerechoTmpUPDEstado != null)
                    {
                        listaCarUpdEst = new List<BEClaseCreacion>();
                        DerechoTmpUPDEstado.ForEach(x => { listaCarUpdEst.Add(new BEClaseCreacion { SEQUENCE = x.Id, CLASS_COD = x.IdClase }); });
                    }

                    var datos = new BLClaseCreacion().Actualizar(obj, listaCarDel, listaCarUpdEst);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "actualiza clase creacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Obtiene(string id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEClaseCreacion tipo = new BEClaseCreacion();
                    tipo = new BLClaseCreacion().Obtener(GlobalVars.Global.OWNER, id);

                    if (tipo != null)
                    {
                        if (tipo.ClaseCreacion != null)
                        {
                            Derecho = new List<DTODerecho>();
                            if (tipo.ClaseCreacion != null)
                            {
                                tipo.ClaseCreacion.ForEach(s =>
                               {
                                   Derecho.Add(new DTODerecho
                                   {
                                       Id = s.SEQUENCE,
                                       IdDerecho = s.RIGHT_COD,
                                       Derecho = s.RIGHT_DESC,
                                       FechaCrea = s.LOG_DATE_CREAT,
                                       UsuarioCrea = s.LOG_USER_CREAT,
                                       auxIdDerecho = s.auxRIGHT_COD,
                                       EnBD = true,
                                       Activo = s.ENDS.HasValue ? false : true
                                   });
                               });

                                DetallesTmp = Derecho;
                            }
                        }
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado la clase de creación";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtiene la clase creacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar(string codigo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLClaseCreacion servicio = new BLClaseCreacion();
                    var result = servicio.Eliminar(new BEClaseCreacion
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        CLASS_COD = codigo,
                        LOG_USER_UPDATE = UsuarioActual,
                    });

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "elimina clase creacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region Agregar y Listar los Derechos

        public List<DTODerecho> DetallesTmp
        {
            get
            {
                return (List<DTODerecho>)Session[K_SESION_DERECHO];
            }
            set
            {
                Session[K_SESION_DERECHO] = value;
            }
        }

        private List<DTODerecho> DerechoTmpUPDEstado
        {
            get
            {
                return (List<DTODerecho>)Session[K_SESION_DERECHO_DEL];
            }
            set
            {
                Session[K_SESION_DERECHO_DEL] = value;
            }
        }

        private List<DTODerecho> DerechoTmpDelBD
        {
            get
            {
                return (List<DTODerecho>)Session[K_SESION_DERECHO_ACT];
            }
            set
            {
                Session[K_SESION_DERECHO_ACT] = value;
            }
        }

        [HttpPost]
        public JsonResult AddDerecho(DTODerecho der)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    Derecho = DetallesTmp;

                    if (Derecho != null)
                    {
                        var query = from item in Derecho
                                    where item.IdDerecho == der.IdDerecho
                                    select item;

                        if (query.ToList().Count >= 1)
                        {
                            retorno.result = 0;
                            retorno.message = "El derecho seleccionado ya ha sido registrado";
                            return Json(retorno, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (Derecho == null) Derecho = new List<DTODerecho>();
                    if (Convert.ToInt32(der.Id) <= 0)
                    {
                        decimal nuevoId = 1;
                        if (Derecho.Count > 0) nuevoId = Derecho.Max(x => x.Id) + 1;
                        der.Id = nuevoId;
                        der.Activo = true;
                        der.EnBD = false;
                        der.UsuarioCrea = UsuarioActual;
                        der.FechaCrea = DateTime.Now;
                        Derecho.Add(der);
                    }
                    else
                    {
                        var item = Derecho.Where(x => x.Id == der.Id).FirstOrDefault();
                        der.EnBD = item.EnBD;//indicador que item viene de la BD
                        der.auxIdDerecho = item.auxIdDerecho;
                        der.Activo = item.Activo;
                        der.UsuarioCrea = UsuarioActual;
                        der.FechaModifica = DateTime.Now;
                        if (der.EnBD)
                        {
                            der.UsuarioModifica = UsuarioActual;
                            der.FechaModifica = DateTime.Now;
                        }
                        Derecho.Remove(item);
                        Derecho.Add(der);
                    }
                    DetallesTmp = Derecho;
                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "AddDerecho", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarDerecho()
        {
            Derecho = DetallesTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tblUsuario' border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th>");
                    shtml.Append("<th class='k-header' >Id Derecho</th>");
                    shtml.Append("<th class='k-header'>Derecho</th>");
                    shtml.Append("<th class='k-header'>Usuario Reg</th>");
                    shtml.Append("<th class='k-header'>Fecha Reg</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'></th>");
                    shtml.Append("</tr></thead>");

                    if (Derecho != null)
                    {
                        foreach (var item in Derecho.OrderBy(x => x.Id))
                        {
                            if (item.Activo == true || item.Activo == false)
                            {
                                shtml.Append("<tr class='k-grid-content'>");
                                shtml.AppendFormat("<td >{0}</td>", item.Id);
                                shtml.AppendFormat("<td >{0}</td>", item.IdDerecho);
                                shtml.AppendFormat("<td >{0}</td>", item.Derecho);
                                shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                                shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                                shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                                shtml.AppendFormat("<td> <a href=# onclick='updAddDerecho({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                              //shtml.AppendFormat("<td> <a href=# onclick='updAddAsociad({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                                shtml.AppendFormat("<a href=# onclick='DellAddDerecho({0});'><img src='../Images/iconos/{1}'      title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Derecho" : "Activar Derecho");
                                shtml.Append("</td>");
                                shtml.Append("</tr>");
                            }
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

        [HttpPost]
        public JsonResult DellAddDerecho(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    Derecho = DetallesTmp;
                    if (Derecho != null)
                    {
                        var objDel = Derecho.Where(x => x.Id == id).FirstOrDefault();
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (DerechoTmpUPDEstado == null) DerechoTmpUPDEstado = new List<DTODerecho>();
                                if (DerechoTmpDelBD == null) DerechoTmpDelBD = new List<DTODerecho>();

                                var itemUpd = DerechoTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                                var itemDel = DerechoTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) DerechoTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) DerechoTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) DerechoTmpDelBD.Add(objDel);
                                    if (itemUpd != null) DerechoTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                Derecho.Remove(objDel);
                                Derecho.Add(objDel);
                            }
                            else
                            {
                                Derecho.Remove(objDel);
                            }
                            DetallesTmp = Derecho;
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "DellAddDerecho", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneDerechoTmp(decimal idDer)
        {                                                                           
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var param = DetallesTmp.Where(x => x.Id == idDer).FirstOrDefault();
                    retorno.data = Json(param, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtieneDerechoTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BEClaseCreacion> obtenerDetalles()
        {
            List<BEClaseCreacion> datos = new List<BEClaseCreacion>();
            if (DetallesTmp != null)
            {
                DetallesTmp.ForEach(x =>
                {
                    datos.Add(new BEClaseCreacion
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        CLASS_COD = x.IdClase,
                        CLASS_DESC = x.Clase,
                        RIGHT_COD = x.IdDerecho,
                        SEQUENCE = x.Id,
                        auxRIGHT_COD = x.auxIdDerecho,
                        LOG_USER_CREAT = x.UsuarioCrea
                    });
                });
            }
            return datos;
        }

        #endregion
    }
}

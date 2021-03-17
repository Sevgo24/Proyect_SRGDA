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
    public class EstadoLicenciaController : Base
    {
        #region varialbles log
        const string nomAplicacion = "SGRDA";
        const string UsuarioActual = "Klescano";
        #endregion

        //
        // GET: /EstadoLicencia/
        #region variables de sesion
        private const string K_SESION_TAB = "___DTOLicenciaEstadoTab";
        private const string K_SESION_TAB_DEL = "___DTOLicenciaEstadoTabDEL";
        private const string K_SESION_TAB_ACT = "___DTOLicenciaEstadoTabACT";
        #endregion

        #region DTO set y get
        List<DTOLicenciaEstadoTab> LicenciaEstadoTab = new List<DTOLicenciaEstadoTab>();

        private List<DTOLicenciaEstadoTab> LicenciaEstadoTabTmpUPDEstado
        {
            get
            {
                return (List<DTOLicenciaEstadoTab>)Session[K_SESION_TAB_ACT];
            }
            set
            {
                Session[K_SESION_TAB_ACT] = value;
            }
        }
        private List<DTOLicenciaEstadoTab> LicenciaEstadoTabTmpDelBD
        {
            get
            {
                return (List<DTOLicenciaEstadoTab>)Session[K_SESION_TAB_DEL];
            }
            set
            {
                Session[K_SESION_TAB_DEL] = value;
            }
        }
        public List<DTOLicenciaEstadoTab> LicenciaEstadoTabTmp
        {
            get
            {
                return (List<DTOLicenciaEstadoTab>)Session[K_SESION_TAB];
            }
            set
            {
                Session[K_SESION_TAB] = value;
            }
        }
        #endregion

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESION_TAB_ACT);
            Session.Remove(K_SESION_TAB_DEL);
            Session.Remove(K_SESION_TAB);
            return View();
        }

        public JsonResult Listar_PageJson_EstadoLicencia(int skip, int take, int page, int pageSize, string group, string parametro, int st)
        {
            Resultado retorno = new Resultado();

            var lista = Listar_Page_EstadoLicencia(parametro, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEEstadoLicencia { EstadoLicencia = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEEstadoLicencia { EstadoLicencia = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEEstadoLicencia> Listar_Page_EstadoLicencia(string parametro, int st, int pagina, int cantRegxPag)
        {
            return new BLEstadoLicencia().Listar_Page_EstadoLicencia(parametro, st, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Obtiene(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEEstadoLicencia tipo = new BEEstadoLicencia();
                    var lista = new BLEstadoLicencia().Obtener(GlobalVars.Global.OWNER, id);

                    if (lista != null)
                    {
                        foreach (var item in lista)
                        {
                            tipo.OWNER = item.OWNER;
                            tipo.LICS_ID = item.LICS_ID;
                            tipo.LIC_TYPE = item.LIC_TYPE;
                            tipo.LICS_NAME = item.LICS_NAME;
                            tipo.LICS_INI = item.LICS_INI;
                            tipo.LICS_END = item.LICS_END;
                        }

                        tipo.ListaTab = new BLLicenciaEstadoTab().ListarTabEstadoLicencia(GlobalVars.Global.OWNER, id);

                        if (tipo.ListaTab != null)
                        {
                            LicenciaEstadoTab = new List<DTOLicenciaEstadoTab>();
                            tipo.ListaTab.ForEach(s =>
                            {
                                LicenciaEstadoTab.Add(new DTOLicenciaEstadoTab
                                {
                                    sequencia = s.SECUENCIA,
                                    IdTab = s.TAB_ID,
                                    antIdTab = s.TAB_ID,
                                    IdLicencia = s.LICS_ID,
                                    Nombre = new BLLicenciaTabs().ObtenerNombre(GlobalVars.Global.OWNER, s.TAB_ID).TAB_NAME,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    UsuarioModifica = s.LOG_USER_UPDATE,
                                    FechaCrea = s.LOG_DATE_CREAT,
                                    FechaModifica = s.LOG_DATE_UPDATE,
                                    EnBD = true,
                                    Activo = s.ENDS.HasValue ? false : true
                                });
                            });
                            LicenciaEstadoTabTmp = LicenciaEstadoTab;
                        }

                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Obtiene el Estado de la Licencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private static void validacion(out bool exito, out string msg_validacion, BEEstadoLicencia entidad)
        {
            exito = true;
            msg_validacion = string.Empty;

            if (exito && string.IsNullOrEmpty(entidad.LICS_NAME))
            {
                msg_validacion = "Ingrese una descripción";
                exito = false;
            }
        }

        [HttpPost]
        public JsonResult Insertar(BEEstadoLicencia entidad)
        {
            bool exito = true;
            Resultado retorno = new Resultado();
            string msg_validacion = "";
            try
            {
                if (!isLogout(ref retorno))
                {
                    validacion(out exito, out msg_validacion, entidad);
                    if (exito)
                    {
                        var servicio = new BLEstadoLicencia();

                        entidad.OWNER = GlobalVars.Global.OWNER;
                        entidad.LOG_USER_CREAT = UsuarioActual;
                        entidad.LOG_USER_UPDATE = UsuarioActual;
                        entidad.ListaTab = obtenerLicenciaEstadoTab();

                        if (entidad.LICS_ID == 0)
                        {
                            servicio.Insertar(entidad);
                        }
                        else
                        {
                            List<BELicenciaEstadoTab> listaTabDel = null;
                            if (LicenciaEstadoTabTmpDelBD != null)
                            {
                                listaTabDel = new List<BELicenciaEstadoTab>();
                                LicenciaEstadoTabTmpDelBD.ForEach(x => { listaTabDel.Add(new BELicenciaEstadoTab { SECUENCIA = x.sequencia }); });
                            }

                            List<BELicenciaEstadoTab> listaTabUpdEst = null;
                            if (LicenciaEstadoTabTmpUPDEstado != null)
                            {
                                listaTabUpdEst = new List<BELicenciaEstadoTab>();
                                LicenciaEstadoTabTmpUPDEstado.ForEach(x => { listaTabUpdEst.Add(new BELicenciaEstadoTab { SECUENCIA = x.sequencia }); });
                            }

                            servicio.Actualizar(entidad, listaTabDel, listaTabUpdEst);
                        }
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Inserta y Actualiza el Estado de la Licencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar(int codigo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var servicio = new BLEstadoLicencia();

                    var tipo = new BEEstadoLicencia();
                    tipo.OWNER = GlobalVars.Global.OWNER;
                    tipo.LICS_ID = codigo;
                    tipo.LOG_USER_UPDATE = UsuarioActual;

                    servicio.Eliminar(tipo);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Cambia el Estado del Estado de la Licencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region Listar
        public JsonResult ListarTab()
        {
            LicenciaEstadoTab = LicenciaEstadoTabTmp;
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th>");
                    //shtml.Append("<th class='k-header' style='display:none'>Nro orden</th>");
                    shtml.Append("<th class='k-header' style='display:none'>IdLic</th>");
                    shtml.Append("<th class='k-header' style='display:none'>Idtab</th>");
                    shtml.Append("<th class='k-header' style='display:none'>Idtabanterior</th>");
                    shtml.Append("<th class='k-header'>Nombre</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (LicenciaEstadoTab != null)
                    {
                        foreach (var item in LicenciaEstadoTab.OrderBy(x => x.sequencia))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.sequencia);
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.IdLicencia);
                            //shtml.AppendFormat("<td style='display:none'>{0}</td>", item.NroordenPeriodo);
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.IdTab);
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.antIdTab);
                            shtml.AppendFormat("<td >{0}</td>", item.Nombre);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td> <a href=# onclick='updAddTab({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.sequencia);
                            shtml.AppendFormat("<a href=# onclick='delAddTab({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.sequencia, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Tab" : "Activar Tab");
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
        #endregion

        private List<BELicenciaEstadoTab> obtenerLicenciaEstadoTab()
        {
            List<BELicenciaEstadoTab> datos = new List<BELicenciaEstadoTab>();
            if (LicenciaEstadoTabTmp != null)
            {
                LicenciaEstadoTabTmp.ForEach(x =>
                {
                    var obj = new BELicenciaEstadoTab();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.SECUENCIA = x.sequencia;
                    obj.TAB_ID = x.IdTab;
                    obj.antTAB_ID = x.antIdTab;
                    obj.LICS_ID = x.IdLicencia;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    obj.LOG_USER_UPDATE = UsuarioActual;
                    datos.Add(obj);
                });
            }
            return datos;
        }

        #region Agregar
        [HttpPost]
        public JsonResult AddTab(DTOLicenciaEstadoTab entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    bool validacion = true; LicenciaEstadoTab = LicenciaEstadoTabTmp;
                    if (LicenciaEstadoTab == null) LicenciaEstadoTab = new List<DTOLicenciaEstadoTab>();

                    string mensaje = string.Empty;
                    var IdTab = from i in LicenciaEstadoTab
                                where i.IdTab == entidad.IdTab
                                select i;

                    if (IdTab.ToList().Count > 0)
                    {
                        mensaje = "La pestaña seleccionada ya ha sido ingresada" + Environment.NewLine;
                        retorno.message = String.Format("{0}", mensaje);
                        retorno.result = 0;
                        validacion = false;
                    }


                    if (validacion)
                    {
                        if (Convert.ToInt32(entidad.sequencia) <= 0)
                        {
                            decimal nuevoId = 1;
                            if (LicenciaEstadoTab.Count > 0) nuevoId = LicenciaEstadoTab.Max(x => x.sequencia) + 1;
                            entidad.sequencia = nuevoId;
                            entidad.Activo = true;
                            entidad.EnBD = false;
                            LicenciaEstadoTab.Add(entidad);
                        }
                        else
                        {
                            var item = LicenciaEstadoTab.Where(x => x.sequencia == entidad.sequencia).FirstOrDefault();
                            entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                            entidad.Activo = item.Activo;
                            LicenciaEstadoTab.Remove(item);
                            LicenciaEstadoTab.Add(entidad);
                        }
                        LicenciaEstadoTabTmp = LicenciaEstadoTab;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "add Tab", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Quitar
        [HttpPost]
        public JsonResult DellAddTab(decimal id)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    LicenciaEstadoTab = LicenciaEstadoTabTmp;
                    if (LicenciaEstadoTab != null)
                    {
                        var objDel = LicenciaEstadoTab.Where(x => x.sequencia == id).FirstOrDefault();
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (LicenciaEstadoTabTmpUPDEstado == null) LicenciaEstadoTabTmpUPDEstado = new List<DTOLicenciaEstadoTab>();
                                if (LicenciaEstadoTabTmpDelBD == null) LicenciaEstadoTabTmpDelBD = new List<DTOLicenciaEstadoTab>();

                                var itemUpd = LicenciaEstadoTabTmpUPDEstado.Where(x => x.sequencia == id).FirstOrDefault();
                                var itemDel = LicenciaEstadoTabTmpDelBD.Where(x => x.sequencia == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) LicenciaEstadoTabTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) LicenciaEstadoTabTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) LicenciaEstadoTabTmpDelBD.Add(objDel);
                                    if (itemUpd != null) LicenciaEstadoTabTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                LicenciaEstadoTab.Remove(objDel);
                                LicenciaEstadoTab.Add(objDel);
                            }
                            else
                            {
                                LicenciaEstadoTab.Remove(objDel);
                            }

                            LicenciaEstadoTabTmp = LicenciaEstadoTab;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "add Tab", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region obtener
        public JsonResult ObtieneLicenciaEstadoTabTmp(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var LicenciaEstadoTab = LicenciaEstadoTabTmp.Where(x => x.sequencia == Id).FirstOrDefault();
                    retorno.data = Json(LicenciaEstadoTab, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneLicenciaEstadoTabTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyect_Apdayc.Clases;
using SGRDA.BL.WorkFlow;
using SGRDA.Entities.WorkFlow;
using SGRDA.Entities;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;

namespace Proyect_Apdayc.Controllers.WorkFlow
{
    public class StateController : Base
    {
        //
        // GET: /State/

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

        public const string nomAplicacion = "SRGDA";

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

        IEnumerable<SelectListItem> itemEstado;
        public JsonResult ListaItemEstado(decimal idCiclo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    itemEstado = new BL_WORKF_STATES().ListarItemEstados(GlobalVars.Global.OWNER, idCiclo)
                    .Select(c => new SelectListItem
                    {
                        Value = c.WRKF_SID.ToString(),
                        Text = c.WRKF_SLABEL
                    });
                    retorno.result = 1;
                    retorno.data = Json(itemEstado, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaItemEstado", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region Index
        public JsonResult Listar(int skip, int take, int page, int pageSize, string group, string nombre, string etiqueta, decimal idTipoEstado, int estado)
        {
            Resultado retorno = new Resultado();
            var lista = BLListar(GlobalVars.Global.OWNER, nombre, etiqueta, idTipoEstado, estado, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new WORKF_STATES { ListarEstados = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new WORKF_STATES { ListarEstados = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<WORKF_STATES> BLListar(string owner, string nombre, string etiqueta, decimal idTipoEstado, int estado, int pagina, int cantRegxPag)
        {
            return new BL_WORKF_STATES().Listar(owner, nombre, etiqueta, idTipoEstado, estado, pagina, cantRegxPag);
        }
        #endregion

        #region Nuevo
        [HttpPost()]
        public ActionResult Eliminar(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                WORKF_STATES estado = new WORKF_STATES();
                estado.OWNER = GlobalVars.Global.OWNER;
                estado.WRKF_SID = id;
                estado.LOG_USER_UPDATE = UsuarioActual;
                var datos = new BL_WORKF_STATES().Eliminar(estado);
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

        private static void validacion(out bool exito, out string msg_validacion, WORKF_STATES entidad)
        {
            exito = true;
            msg_validacion = string.Empty;

            if (exito && string.IsNullOrEmpty(entidad.WRKF_SNAME))
            {
                msg_validacion = "Ingrese un Nombre";
                exito = false;
            }
            if (exito && string.IsNullOrEmpty(entidad.WRKF_SLABEL))
            {
                msg_validacion = "Ingrese una Descripción";
                exito = false;
            }
        }

        [HttpPost()]
        public ActionResult Insertar(WORKF_STATES estado)
        {
            bool exito = true;
            Resultado retorno = new Resultado();
            string msg_validacion = "";
            try
            {
                if (!isLogout(ref retorno))
                {
                    validacion(out exito, out msg_validacion, estado);
                    if (exito)
                    {
                        estado.OWNER = GlobalVars.Global.OWNER;
                        estado.LOG_USER_CREAT = UsuarioActual;
                        estado.LOG_USER_UPDATE = UsuarioActual;
                        estado.ListaTab = obtenerLicenciaEstadoTab();

                        if (estado.WRKF_SID == 0)
                        {
                            var servicio = new BL_WORKF_STATES().Insertar(estado);
                        }
                        else
                        {
                            List<BEREC_LIC_TAB_STAT> listaTabDel = null;
                            if (LicenciaEstadoTabTmpDelBD != null)
                            {
                                listaTabDel = new List<BEREC_LIC_TAB_STAT>();
                                LicenciaEstadoTabTmpDelBD.ForEach(x => { listaTabDel.Add(new BEREC_LIC_TAB_STAT { SECUENCIA = x.sequencia }); });
                            }
                            List<BEREC_LIC_TAB_STAT> listaTabUpdEst = null;
                            if (LicenciaEstadoTabTmpUPDEstado != null)
                            {
                                listaTabUpdEst = new List<BEREC_LIC_TAB_STAT>();
                                LicenciaEstadoTabTmpUPDEstado.ForEach(x => { listaTabUpdEst.Add(new BEREC_LIC_TAB_STAT { SECUENCIA = x.sequencia }); });
                            }

                            var servicio = new BL_WORKF_STATES().Actualizar(estado, listaTabDel, listaTabUpdEst);

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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Obtener(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    WORKF_STATES tipo = new WORKF_STATES();
                    tipo = new BL_WORKF_STATES().Obtener(GlobalVars.Global.OWNER, id);
                    if (tipo != null)
                    {
                        if (tipo.ListaTab != null)
                        {
                            LicenciaEstadoTab = new List<DTOLicenciaEstadoTab>();
                            if (tipo.ListaTab != null)
                            {
                                tipo.ListaTab.ForEach(s =>
                                  {
                                      LicenciaEstadoTab.Add(new DTOLicenciaEstadoTab
                                      {
                                          sequencia = s.SECUENCIA,
                                          IdTab = s.TAB_ID,
                                          IdLicencia = s.WORKF_SID,
                                          Nombre = s.TAB_NAME,
                                          EnBD = true,
                                          UsuarioCrea = s.LOG_USER_CREAT,
                                          FechaCrea = s.LOG_DATE_CREAT,
                                          UsuarioModifica = s.LOG_USER_UPDATE,
                                          FechaModifica = s.LOG_DATE_UPDATE,
                                          antIdTab = s.TAB_ID,
                                          Activo = s.ENDS.HasValue ? false : true
                                      });
                                  });
                                LicenciaEstadoTabTmp = LicenciaEstadoTab;
                                ListarTab();
                            }
                        }
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
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Reporte
        public static WORKF_STATES reporteRep = null;
        public List<WORKF_STATES> ListarReporte(WORKF_STATES estado)
        {
            return new BL_WORKF_STATES().ListarReporte(estado);
        }

        [HttpPost()]
        public JsonResult Reporte(WORKF_STATES estado)
        {
            PasarValores(estado);
            Resultado retorno = new Resultado();
            try
            {
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Reporte", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public void PasarValores(WORKF_STATES estado)
        {
            reporteRep = new WORKF_STATES();
            reporteRep.WRKF_SNAME = string.IsNullOrEmpty(estado.WRKF_SNAME) ? "" : estado.WRKF_SNAME;
            reporteRep.WRKF_SLABEL = string.IsNullOrEmpty(estado.WRKF_SLABEL) ? "" : estado.WRKF_SLABEL;
            reporteRep.ID_ESTADO = estado.ID_ESTADO;
            reporteRep.OWNER = GlobalVars.Global.OWNER;
        }

        public ActionResult DownloadReport(string format)
        {
            try
            {
                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reportes/RptWorkflowState.rdlc");

                WORKF_STATES estado = new WORKF_STATES();
                estado.OWNER = GlobalVars.Global.OWNER;

                List<WORKF_STATES> lista = new List<WORKF_STATES>();
                lista = ListarReporte(reporteRep);

                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet1";
                reportDataSource.Value = lista;

                localReport.DataSources.Add(reportDataSource);
                string reportType = format;
                string mimeType;
                string encoding;
                string fileNameExtension;

                //The DeviceInfo settings should be changed based on the reportType            
                //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
                string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>" + format + "</OutputFormat>" +
                    "  <PageWidth>8.5in</PageWidth>" +
                    "  <PageHeight>11in</PageHeight>" +
                    "  <MarginTop>0.5in</MarginTop>" +
                    "  <MarginLeft>1in</MarginLeft>" +
                    "  <MarginRight>1in</MarginRight>" +
                    "  <MarginBottom>0.5in</MarginBottom>" +
                    "</DeviceInfo>";
                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;
                //Render the report            
                renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension); 
                if (format == null)
                {
                    return File(renderedBytes, "image/jpeg");
                }
                else if (format == "PDF")
                {
                    return File(renderedBytes, mimeType);
                }
                else if (format == "EXCEL")
                {
                    return File(renderedBytes, mimeType);
                }
                else
                {
                    return File(renderedBytes, "image/jpeg");
                }
            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DownloadReport", ex);
                return null;
            }
        }
        #endregion

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
                    shtml.Append("<th class='k-header' style='display:none'>IdEst</th>");
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

        private List<BEREC_LIC_TAB_STAT> obtenerLicenciaEstadoTab()
        {
            List<BEREC_LIC_TAB_STAT> datos = new List<BEREC_LIC_TAB_STAT>();
            if (LicenciaEstadoTabTmp != null)
            {
                LicenciaEstadoTabTmp.ForEach(x =>
                {
                    var obj = new BEREC_LIC_TAB_STAT();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.SECUENCIA = x.sequencia;
                    obj.TAB_ID = x.IdTab;
                    obj.antTAB_ID = x.antIdTab;
                    obj.WORKF_SID = x.IdLicencia;
                    obj.TAB_NAME = x.Nombre;
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
                            entidad.FechaCrea = DateTime.Now;
                            LicenciaEstadoTab.Add(entidad);
                        }
                        else
                        {
                            var item = LicenciaEstadoTab.Where(x => x.sequencia == entidad.sequencia).FirstOrDefault();
                            entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                            entidad.antIdTab = item.antIdTab;
                            entidad.Activo = item.Activo;
                            entidad.UsuarioCrea = item.UsuarioCrea;
                            entidad.FechaCrea = item.FechaCrea;
                            if (entidad.EnBD)
                            {
                                entidad.UsuarioModifica = UsuarioActual;
                                entidad.FechaModifica = DateTime.Now;
                            }
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

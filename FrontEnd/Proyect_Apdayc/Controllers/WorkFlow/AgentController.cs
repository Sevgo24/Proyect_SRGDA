using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyect_Apdayc.Clases;
using SGRDA.BL.WorkFlow;
using SGRDA.Entities.WorkFlow;
using Microsoft.Reporting.WebForms;

namespace Proyect_Apdayc.Controllers.WorkFlow
{
    public class AgentController : Base
    {
        //
        // GET: /Agent/

        public const string nomAplicacion = "SRGDA";        

        public ActionResult Index()
        {
            Init(false);
            return View();
        }
        public ActionResult Nuevo()
        {
            Init(false);
            return View();
        }

        #region Index
        public JsonResult Listar(int skip, int take, int page, int pageSize, string group, string nombre, string etiqueta, int estado)
        {
            Resultado retorno = new Resultado();
            var lista = BLListar(GlobalVars.Global.OWNER, nombre, etiqueta, estado, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new WORKF_AGENTS { ListarAgentes = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new WORKF_AGENTS { ListarAgentes = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<WORKF_AGENTS> BLListar(string owner, string nombre, string etiqueta, int estado, int pagina, int cantRegxPag)
        {
            return new BL_WORKF_AGENTS().Listar(owner, nombre, etiqueta, estado, pagina, cantRegxPag);
        }

        #endregion

        #region Reporte
        public static WORKF_AGENTS agenteRep = null;
        public List<WORKF_AGENTS> ListarReporte(WORKF_AGENTS agente)
        {
            return new BL_WORKF_AGENTS().ListarReporte(agente);
        }

        [HttpPost()]
        public JsonResult Reporte(WORKF_AGENTS agente)
        {
            PasarValores(agente);
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
        public void PasarValores(WORKF_AGENTS agente)
        {
            agenteRep = new WORKF_AGENTS();
            agenteRep.WRKF_AGNAME = string.IsNullOrEmpty(agente.WRKF_AGNAME) ? "" : agente.WRKF_AGNAME;
            agenteRep.WRKF_AGLABEL = string.IsNullOrEmpty(agente.WRKF_AGLABEL) ? "" : agente.WRKF_AGLABEL;
            agenteRep.ID_ESTADO = agente.ID_ESTADO;
            agenteRep.OWNER = GlobalVars.Global.OWNER;
        }

        public ActionResult DownloadReport(string format)
        {
            try
            {
                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reportes/RptWorkflowAgent.rdlc");

                WORKF_AGENTS agente = new WORKF_AGENTS();
                agente.OWNER = GlobalVars.Global.OWNER;

                List<WORKF_AGENTS> lista = new List<WORKF_AGENTS>();
                lista = ListarReporte(agenteRep);

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

        #region Nuevo
        [HttpPost()]
        public ActionResult Eliminar(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                WORKF_AGENTS estado = new WORKF_AGENTS();
                estado.OWNER = GlobalVars.Global.OWNER;
                estado.WRKF_AGID = id;
                estado.LOG_USER_UPDATE = UsuarioActual;
                var datos = new BL_WORKF_AGENTS().Eliminar(estado);
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
        public ActionResult Insertar(WORKF_AGENTS agente)
        {
            Resultado retorno = new Resultado();
            try
            {
                agente.OWNER = GlobalVars.Global.OWNER;
                if (agente.WRKF_AGID == 0)
                {
                    agente.LOG_USER_CREAT = UsuarioActual;
                    var datos = new BL_WORKF_AGENTS().Insertar(agente);
                }
                else
                {
                    agente.LOG_USER_UPDATE = UsuarioActual;
                    var datos = new BL_WORKF_AGENTS().Actualizar(agente);
                }
                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Obtener(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var cliente = new BL_WORKF_AGENTS().Obtener(GlobalVars.Global.OWNER, id);
                    retorno.data = Json(cliente, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
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
    }
}

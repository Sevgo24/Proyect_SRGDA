using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases;
using SGRDA.BL.Reporte;
using SGRDA.Entities.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Controllers.Reportes
{
    public class Reporte_BEC_InactivasController : Base
    {
        // GET: Reporte_BEC_Inactivas
        private const string K_SESSION_LISTA_REPORTE_DE_BECS_INACTIVAS = "___K_SESSION_LISTA_REPORTE_DE_BECS_INACTIVAS";
       
        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_DE_BECS_INACTIVAS);
            Init(false);
            return View();
        }
        private List<Be_Bec_Inactivas> ListaBecInactivasTMP
        {
            get
            {
                return (List<Be_Bec_Inactivas>)Session[K_SESSION_LISTA_REPORTE_DE_BECS_INACTIVAS];
            }
            set
            {
                Session[K_SESSION_LISTA_REPORTE_DE_BECS_INACTIVAS] = value;
            }
        }
        private static string Fini;
        private static string Ffin;
        public ActionResult ReporteBecInactivas(string formato)
        {
            string format = formato;
            int oficina_id = 0;
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            Resultado retorno = new Resultado();

            try
            {
                ///         DS_REC_BECS_ESPECIALES
                LocalReport localReport = new LocalReport();
           
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_BECS_INACTIVAS.rdlc");
                
                List<Be_Bec_Inactivas> lstReporte = new List<Be_Bec_Inactivas>();
                lstReporte = ListaBecInactivasTMP;

                if (lstReporte.Count > 0)
                {
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = lstReporte;
                    localReport.DataSources.Add(reportDataSource);

                    ReportParameter parametroFecha1 = new ReportParameter();
                    parametroFecha1 = new ReportParameter("FechaInicio", Fini);
                    localReport.SetParameters(parametroFecha1);

                    ReportParameter parametroFecha2 = new ReportParameter();
                    parametroFecha2 = new ReportParameter("FechaFin", Ffin);
                    localReport.SetParameters(parametroFecha2);

                    ReportParameter parametroNomusu = new ReportParameter();
                    parametroNomusu = new ReportParameter("NombreUsuario", oficina);
                    localReport.SetParameters(parametroNomusu);

                    ReportParameter parametroNomoficina = new ReportParameter();
                    parametroNomoficina = new ReportParameter("NombreOficina", usuario);
                    localReport.SetParameters(parametroNomoficina);

                    ReportParameter fecha = new ReportParameter();
                    fecha = new ReportParameter("FechaImpresion", DateTime.Now.ToShortDateString());
                    localReport.SetParameters(fecha);

                    string reportType = format;
                    string mimeType;
                    string encoding;

                    //aqui le cambie solo dejar string fileNameExtension en caso de error
                    string fileNameExtension;
                    string deviceInfo = "";
                    //CODIGO REPETIBLE
                    //The DeviceInfo settings should be changed based on the reportType            
                    //http://msdn2.microsoft.com/en-us/library/ms155397.aspx       

                    deviceInfo = "<DeviceInfo>" +
                   "  <OutputFormat>" + format + "</OutputFormat>" +
                   //  "  <PageWidth>8.5in</PageWidth>" +
                   "  <PageWidth>13in</PageWidth>" +
                   //"  <PageHeight>3in</PageHeight>" +
                   "  <PageHeight>8.3in</PageHeight>" +
                   "  <MarginTop>0.2in</MarginTop>" +
                   "  <MarginLeft>0.3in</MarginLeft>" +
                   "  <MarginRight>0.0in</MarginRight>" +
                   "  <MarginBottom>0.1in</MarginBottom>" +
                   "</DeviceInfo>";



                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;

                    renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.result = 1;
                    localReport.DisplayName = "Reporte de Becs Especiales";

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
                else
                {
                    retorno.message = Constantes.MensajeGenerico.MSG_ERROR_REPORTE;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte de Becs Especiales", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ReporteTipo(decimal LIC_ID, decimal BPS_ID, decimal INV_ID, string Serie, decimal nro,
            decimal Bec_id, string Fini_Rechazo, string Ffin_Rechazo, decimal oficina_id, string formato)
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_DE_BECS_INACTIVAS);
            Resultado retorno = new Resultado();
            //int oficina_id = 0;
            string format = formato;
            //decimal off_admin = 0;
            Fini = Fini_Rechazo;
            Ffin = Ffin_Rechazo;
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina_id));
            if (opcAdm == 1)
            {
                oficina_id = 0;
            }

            try
            {
                List<Be_Bec_Inactivas> listar = new List<Be_Bec_Inactivas>();

               
                    listar = new BL_Reporte_Bec_Inactiva().Listar_Becs_Inactivas(LIC_ID, BPS_ID, INV_ID, Serie, nro, Bec_id, Fini_Rechazo, Ffin_Rechazo, oficina_id);
                    ListaBecInactivasTMP = new List<Be_Bec_Inactivas>();
                    ListaBecInactivasTMP = listar;
                

                if (listar.Count > 0)
                {
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = Constantes.MensajeGenerico.MSG_ERROR_REPORTE;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte De Empadronamiento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}
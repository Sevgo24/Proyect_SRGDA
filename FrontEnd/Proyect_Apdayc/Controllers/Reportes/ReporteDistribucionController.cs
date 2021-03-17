using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using SGRDA.BL.Reporte;
using SGRDA.Entities.Reporte;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System.Text;

using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.Common;
using Microsoft.ReportingServices;

using System.Text.RegularExpressions;

//using Microsoft.ReportViewer.Common;
//using Microsoft.ReportViewer.WebForms;
//using Microsoft.ReportViewer.ProcessingObjectModel;

namespace Proyect_Apdayc.Controllers.Reportes
{
    public class ReporteDistribucionController : Base
    {
        private const string K_SESSION_DEPORTE_DISTRIBUCION = "___K_SESSION_REPORTE_DISTRIBUCION";

        private List<BEReporteDistribucion> ListarReporteTmp
        {
            get
            {
                return (List<BEReporteDistribucion>)Session[K_SESSION_DEPORTE_DISTRIBUCION];
            }
            set
            {
                Session[K_SESSION_DEPORTE_DISTRIBUCION] = value;
            }
        }

        // GET: ReporteDistribucion
        public ActionResult Index()
        {
            Session.Remove(K_SESSION_DEPORTE_DISTRIBUCION);
            return View();
        }

        [HttpPost]
        public JsonResult ListarTipoReporte()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLReporteDistribucion().ListaContableDesplegable()
                    .Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.TIPO_REPORTE),
                        Text = c.TIPO_REPORTE
                    });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoReporte", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ConusltarReporte(string fini, string ffin, string tipoReporte)
        {
            //Session.Remove(K_SESSION_CONTABLE_LISTA_REPORTE_SEDES);
            //string oficina = "";
            //string format = formato;
            int cantidad = 0;
            Resultado retorno = new Resultado();
            try
            {
                List<BEReporteDistribucion> listar = new List<BEReporteDistribucion>();
                fini = fini == "" ? "01/01/1990" : fini;
                ffin = ffin == "" ? "01/01/3000" : ffin;

                if (tipoReporte == "NETA")
                    listar = new BLReporteDistribucion().ListadDistribucion_Neta(fini, ffin);
                else if (tipoReporte == "RESUMEN")
                    listar = new BLReporteDistribucion().ListarDistribucion_Resumen(fini, ffin);
                else if (tipoReporte == "RESUMEN - AGRUPADO")
                    listar = new BLReporteDistribucion().ListarDistribucion_Resumen(fini, ffin);
                //listar = new BLReporteDistribucion().ListarDistribucion_Resumen_Agrupado(fini, ffin);
                else if (tipoReporte == "DETALLADO")
                    listar = new BLReporteDistribucion().ListarDistribucion_Detallado(fini, ffin);
                ListarReporteTmp = new List<BEReporteDistribucion>();
                ListarReporteTmp = listar;


                if (listar.Count > 0)
                {
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
                else
                {
                    ListarReporteTmp = new List<BEReporteDistribucion>();
                    retorno.result = 0;
                    retorno.message = Constantes.MensajeGenerico.MSG_ERROR_REPORTE;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ConusltarReporte", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GenerarReporte(string fini, string ffin, string formato, string tipoReporte)
        {
            string oficina = " ";
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            Resultado retorno = new Resultado();
            string format = formato;

            try
            {
                LocalReport localReport = new LocalReport();

                if (tipoReporte == "NETA")
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_DISTRIBUCION_NETO.rdlc");
                else if (tipoReporte == "RESUMEN" && format == "PDF")
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_DISTRIBUCION_RESUMEN.rdlc");
                else if (tipoReporte == "RESUMEN" && format == "EXCEL")
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_DISTRIBUCION_RESUMEN_EXCEL.rdlc");
                else if (tipoReporte == "RESUMEN - AGRUPADO")
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_DISTRIBUCION_RESUMEN_AGRUPADO.rdlc");
                else if (tipoReporte == "DETALLADO" && format == "PDF")
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_DISTRIBUCION_DETALLADO.rdlc");
                else if (tipoReporte == "DETALLADO" && format == "EXCEL")
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_DISTRIBUCION_DETALLADO_EXCEL.rdlc");


                List<BEReporteDistribucion> listar = new List<BEReporteDistribucion>();
                listar = ListarReporteTmp;

                if (listar.Count > 0)
                {
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = listar;
                    localReport.DataSources.Add(reportDataSource);

                    ReportParameter parametroFechaIni = new ReportParameter();
                    parametroFechaIni = new ReportParameter("FechaInicio", fini == "" ? "-" : fini);
                    localReport.SetParameters(parametroFechaIni);

                    ReportParameter parametroFechaFin = new ReportParameter();
                    parametroFechaFin = new ReportParameter("FechaFin", ffin == "" ? "-" : ffin);
                    localReport.SetParameters(parametroFechaFin);


                    ReportParameter parametroFecha = new ReportParameter();
                    parametroFecha = new ReportParameter("FechaImpresion", DateTime.Now.ToShortDateString());
                    localReport.SetParameters(parametroFecha);

                    ReportParameter parametroOficina = new ReportParameter();
                    parametroOficina = new ReportParameter("Oficina", oficina);
                    localReport.SetParameters(parametroOficina);

                    ReportParameter parametroUsuario = new ReportParameter();
                    parametroUsuario = new ReportParameter("Usuario", usuario);
                    localReport.SetParameters(parametroUsuario);



                    string reportType = format;
                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    //CODIGO REPETIBLE
                    //The DeviceInfo settings should be changed based on the reportType            
                    //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
                    string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>" + format + "</OutputFormat>" +
                    //  "  <PageWidth>8.5in</PageWidth>" +
                    "  <PageWidth>11in</PageWidth>" +
                    //"  <PageHeight>11in</PageHeight>" +
                    "  <PageHeight>8.3in</PageHeight>" +
                    "  <MarginTop>0.0in</MarginTop>" +
                    "  <MarginLeft>0.3in</MarginLeft>" +
                    "  <MarginRight>0.0in</MarginRight>" +
                    "  <MarginBottom>0.3in</MarginBottom>" +
                    "</DeviceInfo>";


                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;
                    //Render the report         
                    if (reportType == "EXCEL")
                        renderedBytes = localReport.Render("EXCELOPENXML", deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                    else
                        renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);


                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    //retorno.data = Json(FacturaMasiva, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                    localReport.DisplayName = "Reporte de Consulta de Sociedad";
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
                        //return File(renderedBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    }
                    else
                    {
                        return File(renderedBytes, "image/jpeg");
                    }
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "GenerarReporte", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }



    }
}
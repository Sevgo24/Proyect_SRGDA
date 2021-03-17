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
using System.Text.RegularExpressions;

namespace Proyect_Apdayc.Controllers.Recaudacion
{
    public class ReporteComprobantexAutorController : Base
    {
        //
        // GET: /ReporteComprobantexAutor/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReporteComprobantexAutor(string fini, string ffin, string formato)
        {
            //Init(false);//add sysseg
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            //deberia ser al reves
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            Resultado retorno = new Resultado();
            string format = formato;

            try
            {
                LocalReport localReport = new LocalReport();
                //cambiar ruta del reporte
                localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_COMPROBANTE_X_AUTOR.rdlc");

                List<BEReporteComprobantexAutor> listar = new List<BEReporteComprobantexAutor>();
                listar = new BLReporteComprobantexAutor().ListarFacturaCancelada(fini, ffin, oficina);

                if (listar.Count > 0)
                {
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = listar;
                    localReport.DataSources.Add(reportDataSource);

                    //ReportParameter parametro = new ReportParameter();
                    //parametro = new ReportParameter("Usuario", UsuarioActual.Trim());
                    //localReport.SetParameters(parametro);


                    //parametros para la fecha inicio y fin
                    ReportParameter parametroFechaIni = new ReportParameter();
                    parametroFechaIni = new ReportParameter("FechaInicio", fini);
                    localReport.SetParameters(parametroFechaIni);

                    ReportParameter parametroFechaFin = new ReportParameter();
                    parametroFechaFin = new ReportParameter("FechaFin", ffin);
                    localReport.SetParameters(parametroFechaFin);


                    ReportParameter parametroFecha = new ReportParameter();
                    parametroFecha = new ReportParameter("FechaImpresion", DateTime.Now.ToShortDateString());
                    localReport.SetParameters(parametroFecha);

                    ReportParameter parametroNomusu = new ReportParameter();
                    parametroFecha = new ReportParameter("NombreUsuario", oficina);
                    localReport.SetParameters(parametroFecha);

                    ReportParameter parametroNomoficina = new ReportParameter();
                    parametroNomoficina = new ReportParameter("NombreOficina", usuario);
                    localReport.SetParameters(parametroNomoficina);


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
                        "  <PageWidth>8.5in</PageWidth>" +
                        //"  <PageHeight>11in</PageHeight>" +
                        "  <PageHeight>11in</PageHeight>" +
                        "  <MarginTop>0.3in</MarginTop>" +
                        "  <MarginLeft>0.2in</MarginLeft>" +
                        "  <MarginRight>0.0in</MarginRight>" +
                        "  <MarginBottom>0.0in</MarginBottom>" +
                        "</DeviceInfo>";

                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;
                    //Render the report            
                    renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                    //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension); 

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
                ucLogApp.ucLog.GrabarLogError("SGRDA",UsuarioActual, "Reporte", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        //reporte
        public ActionResult ReporteTipo(string fini, string ffin, string formato)
        {
            //Init(false);//add sysseg
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            Resultado retorno = new Resultado();
            string format = formato;

            try
            {
                List<BEFacturaCancelada> listar = new List<BEFacturaCancelada>();
                //listar = new BLReporteFacturaCancelada().ListarFacturaCancelada(fini, ffin, Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]),0);
                listar = null;
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte DIARIO", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }




    }
}

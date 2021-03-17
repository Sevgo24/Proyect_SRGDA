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

namespace Proyect_Apdayc.Controllers.Reportes.Contable
{
    public class ReporteContableRecaudacionSedesController : Base
    {
        private const string K_SESSION_CONTABLE_LISTA_REPORTE_SEDES = "___K_SESSION_CONTABLE_LISTA_REPORTE_SEDES";

        // GET: ReporteContableRecaudacionSedes
        public ActionResult Index()
        {
            Session.Remove(K_SESSION_CONTABLE_LISTA_REPORTE_SEDES);
            return View();
        }

        private List<BEReporteListarRecaudacionSedes> ListaReporteSedesTmp
        {
            get
            {
                return (List<BEReporteListarRecaudacionSedes>)Session[K_SESSION_CONTABLE_LISTA_REPORTE_SEDES];
            }
            set
            {
                Session[K_SESSION_CONTABLE_LISTA_REPORTE_SEDES] = value;
            }
        }

        //****************************REPORTE*******************************        //
        public ActionResult ReporteContableRecaudacionSedes(string fini, string ffin, string formato, string Contable)
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
                localReport.ReportPath = Server.MapPath("~/Reportes/Contable/R_REC_REPORTE_CONTABLE_RECAUDACION_SEDES.rdlc");

                List<BEReporteListarRecaudacionSedes> listar = new List<BEReporteListarRecaudacionSedes>();
                //listar = new BLReporteRecaudacionSedes().ListarRecaudacionSedes(fini, ffin, oficina);
                listar = ListaReporteSedesTmp;
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
                    parametroFechaIni = new ReportParameter("FechaInicio", fini == "" ? "-" : fini);
                    localReport.SetParameters(parametroFechaIni);

                    ReportParameter parametroFechaFin = new ReportParameter();
                    parametroFechaFin = new ReportParameter("FechaFin", ffin == "" ? "-" : ffin);
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

                    ReportParameter parametroContable = new ReportParameter();
                    parametroContable = new ReportParameter("Contable", Contable);
                    localReport.SetParameters(parametroContable);


                    //ReportParameter parametroFechaIniCon = new ReportParameter();
                    //parametroFechaIniCon = new ReportParameter("FechaIniCon",  "-");
                    //localReport.SetParameters(parametroFechaIniCon);

                    //ReportParameter parametroFechaFinCon = new ReportParameter();
                    //parametroFechaFinCon = new ReportParameter("FechaFinCon", conCon == 1 ? ffinCon : "-");
                    //localReport.SetParameters(parametroFechaFinCon);

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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ReporteContableRecaudacionSedesTodas(string fini, string ffin, string formato, string Contable)
        {
            //Init(false);//add sysseg
            //deberia ser al reves
            string oficina = "General";
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            Resultado retorno = new Resultado();
            string format = formato;

            try
            {
                /////////cambiar ruta del reporte //
                
                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reportes/Contable/R_REC_REPORTE_CONTABLE_RECAUDACION_SEDES.rdlc");
                List<BEReporteListarRecaudacionSedes> listar = new List<BEReporteListarRecaudacionSedes>();
                listar = ListaReporteSedesTmp;
                

                //LocalReport localReport = new LocalReport();
                ////localReport.SetBasePermissionsForSandboxAppDomain(new PermissionSet(PermissionState.Unrestricted));

                //var reportStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("R_REC_REPORTE_CONTABLE_RECAUDACION_SEDES.rdlc");
                //localReport.LoadReportDefinition(reportStream);                
                ////localReport.ReportPath = Server.MapPath("~/Reportes/Contable/R_REC_REPORTE_CONTABLE_RECAUDACION_SEDES.rdlc");
                //List<BEReporteListarRecaudacionSedes> listar = new List<BEReporteListarRecaudacionSedes>();
                //listar = ListaReporteSedesTmp;

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
                    parametroFechaIni = new ReportParameter("FechaInicio", fini == "" ? "-" : fini);
                    localReport.SetParameters(parametroFechaIni);

                    ReportParameter parametroFechaFin = new ReportParameter();
                    parametroFechaFin = new ReportParameter("FechaFin", fini == "" ? "-" : fini);
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

                    ReportParameter parametroContable = new ReportParameter();
                    parametroContable = new ReportParameter("Contable", Contable);
                    localReport.SetParameters(parametroContable);

                    //ReportParameter parametroFechaIniCon = new ReportParameter();
                    //parametroFechaIniCon = new ReportParameter("FechaIniCon", "-");
                    //localReport.SetParameters(parametroFechaIniCon);

                    //ReportParameter parametroFechaFinCon = new ReportParameter();
                    //parametroFechaFinCon = new ReportParameter("FechaFinCon", "-");
                    //localReport.SetParameters(parametroFechaFinCon);

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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        // CONSULTA
        public ActionResult ReporteContableTipo(string fini, string ffin, string formato, decimal idContable)
        //, int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion)
        {
            Session.Remove(K_SESSION_CONTABLE_LISTA_REPORTE_SEDES);
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            Resultado retorno = new Resultado();
            string format = formato;

            try
            {
                List<BEReporteListarRecaudacionSedes> listar = new List<BEReporteListarRecaudacionSedes>();
                fini = fini == "" ? "01/01/1990" : fini;
                ffin = ffin == "" ? "01/01/3000" : ffin;
                listar = new BLReporteRecaudacionSedes().ListarReporteContableRecaudacionSedes(fini, ffin, oficina, idContable);
                //conFechaIngreso, conFechaConfirmacion, finiConfirmacion, ffinConfirmacion);
                ListaReporteSedesTmp = new List<BEReporteListarRecaudacionSedes>();
                ListaReporteSedesTmp = listar;

                if (listar.Count > 0)
                {
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
                else
                {
                    ListaReporteSedesTmp = new List<BEReporteListarRecaudacionSedes>();
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


        public ActionResult ReporteContableTipoTodas(string fini, string ffin, string formato, decimal idContable)
        {
            Session.Remove(K_SESSION_CONTABLE_LISTA_REPORTE_SEDES);
            string oficina = "";
            string format = formato;
            Resultado retorno = new Resultado();
            try
            {
                List<BEReporteListarRecaudacionSedes> listar = new List<BEReporteListarRecaudacionSedes>();
                fini = fini == "" ? "01/01/1990" : fini;
                ffin = ffin == "" ? "01/01/3000" : ffin;
                listar = new BLReporteRecaudacionSedes().ListarReporteContableRecaudacionSedes(fini, ffin, oficina, idContable);
                ListaReporteSedesTmp = new List<BEReporteListarRecaudacionSedes>();
                ListaReporteSedesTmp = listar;

                if (listar.Count > 0)
                {
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
                else
                {
                    ListaReporteSedesTmp = new List<BEReporteListarRecaudacionSedes>();
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
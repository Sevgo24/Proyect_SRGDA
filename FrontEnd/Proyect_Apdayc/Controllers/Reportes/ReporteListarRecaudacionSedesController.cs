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

namespace Proyect_Apdayc.Controllers.Reportes
{
    public class ReporteListarRecaudacionSedesController : Base
    {
        //
        // GET: /ReporteListarRecaudacionSedes/
        private const string K_SESSION_LISTA_REPORTE_SEDES = "___K_SESSION_LISTA_REPORTE_SEDES";
        

        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_SEDES);
            return View();
        }

        private List<BEReporteListarRecaudacionSedes> ListaReporteSedesTmp
        {
            get
            {
                return (List<BEReporteListarRecaudacionSedes>)Session[K_SESSION_LISTA_REPORTE_SEDES];
            }
            set
            {
                Session[K_SESSION_LISTA_REPORTE_SEDES] = value;
            }
        }

        //****************************REPORTE*******************************        //
        public ActionResult ReporteRecaudacionSedes(string fini, string ffin, string formato
                                        , int conIng, int conCon, string finiCon, string ffinCon)
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
                localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_RECAUDACION_SEDES.rdlc");

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
                    parametroFechaIni = new ReportParameter("FechaInicio", conIng == 1 ? fini : "-");
                    localReport.SetParameters(parametroFechaIni);

                    ReportParameter parametroFechaFin = new ReportParameter();
                    parametroFechaFin = new ReportParameter("FechaFin", conIng == 1 ? ffin : "-");
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

                    ReportParameter parametroFechaIniCon = new ReportParameter();
                    parametroFechaIniCon = new ReportParameter("FechaIniCon", conCon == 1 ? finiCon : "-");
                    localReport.SetParameters(parametroFechaIniCon);

                    ReportParameter parametroFechaFinCon = new ReportParameter();
                    parametroFechaFinCon = new ReportParameter("FechaFinCon", conCon == 1 ? ffinCon : "-");
                    localReport.SetParameters(parametroFechaFinCon);

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

        //****************************REPORTE*******************************
        //
        public ActionResult ReporteRecaudacionSedesTodas(string fini, string ffin, string formato
                                        , int conIng, int conCon, string finiCon, string ffinCon)
        {
            //Init(false);//add sysseg
            //deberia ser al reves
            string oficina = "General";
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            Resultado retorno = new Resultado();
            string format = formato;

            try
            {
                LocalReport localReport = new LocalReport();
                //cambiar ruta del reporte
                localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_RECAUDACION_SEDES.rdlc");

                List<BEReporteListarRecaudacionSedes> listar = new List<BEReporteListarRecaudacionSedes>();
                //listar = new BLReporteRecaudacionSedes().ListarRecaudacionSedes(fini, ffin, "");
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
                    parametroFechaIni = new ReportParameter("FechaInicio", conIng == 1 ? fini : "-");
                    localReport.SetParameters(parametroFechaIni);

                    ReportParameter parametroFechaFin = new ReportParameter();
                    parametroFechaFin = new ReportParameter("FechaFin", conIng == 1 ? ffin : "-");
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


                    ReportParameter parametroFechaIniCon = new ReportParameter();
                    parametroFechaIniCon = new ReportParameter("FechaIniCon", conCon == 1 ? finiCon : "-");
                    localReport.SetParameters(parametroFechaIniCon);

                    ReportParameter parametroFechaFinCon = new ReportParameter();
                    parametroFechaFinCon = new ReportParameter("FechaFinCon", conCon == 1 ? ffinCon : "-");
                    localReport.SetParameters(parametroFechaFinCon);

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

        //reporte
        public ActionResult ReporteTipo(string fini, string ffin, string formato
            , int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion)
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_SEDES);
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            Resultado retorno = new Resultado();
            string format = formato;
            int oficina_id = 0;
            int oficina_cod = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);


            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(oficina_cod);
            if (opcAdm == 1)
            {
                oficina_id = 0;
            }
            else
            {
                oficina_id = oficina_cod;
            }
            try
            {
                List<BEReporteListarRecaudacionSedes> listar = new List<BEReporteListarRecaudacionSedes>();
                listar = new BLReporteRecaudacionSedes().ListarRecaudacionSedes(fini, ffin, oficina_id,
                                                        conFechaIngreso, conFechaConfirmacion, finiConfirmacion, ffinConfirmacion);
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

        //reporte
        public ActionResult ReporteTipoTodas(string fini, string ffin, string formato
                                             , int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion)
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_SEDES);
            string oficina = "";
            int oficina_id = 0;
            int oficina_cod = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            string format = formato;

            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(oficina_cod);
            if (opcAdm == 1)
            {
                oficina_id = 0;
            }else
            {
                oficina_id = oficina_cod;
            }
            Resultado retorno = new Resultado();
            try
            {
                List<BEReporteListarRecaudacionSedes> listar = new List<BEReporteListarRecaudacionSedes>();
                listar = new BLReporteRecaudacionSedes().ListarRecaudacionSedes(fini, ffin, oficina_id, conFechaIngreso, conFechaConfirmacion, finiConfirmacion, ffinConfirmacion);
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



        [HttpPost]
        public JsonResult obtenerUltimaActualizacion()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    string UltimaFechaActualizacion = new BLReporteRecaudacionSedes().ObtenerFechaUltActualizacionRepRecaudacionSedes();
                    retorno.result = 1;
                    retorno.message = UltimaFechaActualizacion;

                    //if (ComprobanteDeposito != null)
                    //{
                    //    retorno.result = 1;
                    //    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    //}
                    //else
                    //{
                    //    retorno.result = 0;
                    //    retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                    //}
                    //retorno.data = Json(ComprobanteDeposito, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                //ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "obtenerUltimaActualizacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }



    }
}

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
using SGRDA.BL.BLAlfresco;

namespace Proyect_Apdayc.Controllers.Reportes
{
    public class ReporteEstadoCuentaController : Base
    {
        //
        // GET: /ReporteEstadoCuenta/
        private const string K_SESSION_LISTA_REPORTE_DE_ESTADO_CUENTA = "___K_SESSION_LISTA_REPORTE_DE_ESTADO_CUENTA";

        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_DE_ESTADO_CUENTA);
            Init(false);
            return View();
        }
        private List<BEReporteEstadoCuenta> ListaReporteDeEstadoCuentaTmp
        {
            get
            {
                return (List<BEReporteEstadoCuenta>)Session[K_SESSION_LISTA_REPORTE_DE_ESTADO_CUENTA];
            }
            set
            {
                Session[K_SESSION_LISTA_REPORTE_DE_ESTADO_CUENTA] = value;
            }
        }
        public ActionResult ReporteEstadoCuenta(string fini, string ffin, int BPS_ID, int EST_ID, string formato, string idoficina
            , string nombreoficina, string LIC_ID, int ESTADO, string TipoReporte)
        {
            Resultado retorno = new Resultado();
            string format = formato;
            //int oficina_id = 0;


            if (LIC_ID == "")
            {
                LIC_ID = "0";
            }
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            //oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            //if (oficina_id == 10081 || oficina_id == 10082)
            //    idoficina = idoficina;
            //else
            //    idoficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            try
            {
                LocalReport localReport = new LocalReport();
                if (TipoReporte == "D")
                {
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_ESTADO_CUENTA.rdlc");
                }
                else if (TipoReporte == "R")
                {
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_ESTADO_CUENTA_RESUMEN.rdlc");
                }
                else
                {
                    localReport.ReportPath = Server.MapPath("~/Reportes/EstadoCuentaTransporte.rdlc");

                }


                //ReporteEstadoCuentaResumen

                List<BEReporteEstadoCuenta> listar = new List<BEReporteEstadoCuenta>();
                listar = ListaReporteDeEstadoCuentaTmp;
                string nombreLicencia = listar[0].NOMBRE_LICENCIA.ToString();
                if (nombreLicencia == "")
                {
                    nombreLicencia = "----------";
                }

                if (listar.Count > 0)
                {
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = listar;
                    localReport.DataSources.Add(reportDataSource);

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

                    ReportParameter parametroNomLicencia = new ReportParameter();
                    parametroNomLicencia = new ReportParameter("NombreLicencia", nombreLicencia);
                    localReport.SetParameters(parametroNomLicencia);

                    var result = localReport.GetParameters();
                    var a = result[0].State;
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
                    renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.result = 1;
                    localReport.DisplayName = "Reporte de Usuarios Musicales";

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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte de Usuarios Musicales", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }



        //VALIDACION
        public ActionResult ReporteTipo(string fini, string ffin, int BPS_ID, int EST_ID, string formato, string idoficina
            , string nombreoficina, string LIC_ID, int ESTADO, string TipoReporte)
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_DE_ESTADO_CUENTA);

            Resultado retorno = new Resultado();
            string format = formato;
            int? rubrofiltro = null;
            string off_id = "";
            if (nombreoficina == "--SELECCIONE--")
            {
                nombreoficina = "TODAS LAS OFICINAS";
            }
            if (LIC_ID == "")
            {
                LIC_ID = "0";
            }
            int oficina_id = Convert.ToInt32(Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]));
            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina_id));
            //if (oficina_id == 10081 || oficina_id == 10082)
            if (opcAdm == 1)
            {
                off_id = idoficina;
            }
            else
                off_id = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            //string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            //oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            //if (oficina_id == 10081 || oficina_id == 10082)
            //    idoficina = idoficina;
            //else
            //    idoficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            string oficina_nombre = Convert.ToString(Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]));
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            try
            {
                List<BEReporteEstadoCuenta> listar = new List<BEReporteEstadoCuenta>();

                if (TipoReporte == "D")
                {
                    listar = new BLReporteEstadoCuenta().ReporteEstadoCuenta(fini, ffin, BPS_ID, EST_ID, off_id, LIC_ID, ESTADO, oficina_nombre, usuario);
                    //listar = new BLReporteEstadoCuenta().ReporteEstadoCuentaTransporte(fini, ffin, BPS_ID, EST_ID, off_id, LIC_ID, ESTADO, oficina_nombre, usuario);
                }
                else if (TipoReporte == "R")
                {
                    listar = new BLReporteEstadoCuenta().ReporteEstadoCuentaResumen(fini, ffin, BPS_ID, EST_ID, off_id, LIC_ID, ESTADO, oficina_nombre, usuario);
                }
                else
                {
                    listar = new BLReporteEstadoCuenta().ReporteEstadoCuentaTransporte(fini, ffin, BPS_ID, EST_ID, off_id, LIC_ID, ESTADO, oficina_nombre, usuario, TipoReporte);
                }

                ListaReporteDeEstadoCuentaTmp = listar;
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte de Estado de Cuenta", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValirdarOficinaTransporte()
        {
            Resultado retorno = new Resultado();
            int oficina_id = Convert.ToInt32(Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]));
            if (oficina_id == 10085 || oficina_id == 10081)
            {
                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
            }
            else
            {
                retorno.result = 0;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

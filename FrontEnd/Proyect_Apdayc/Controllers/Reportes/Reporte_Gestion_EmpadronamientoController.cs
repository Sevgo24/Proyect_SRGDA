using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using SGRDA.Entities.Reporte;
using Proyect_Apdayc.Clases;
using SGRDA.BL.Reporte;

namespace Proyect_Apdayc.Controllers.Reportes
{
    public class Reporte_Gestion_EmpadronamientoController : Base
    {
        // GET: Reporte_Gestion_Empadronamiento
        private const string K_SESSION_LISTA_REPORTE_DE_EMPRADRONAMIENTO = "___K_SESSION_LISTA_REPORTE_DE_EMPRADRONAMIENTO ";

        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_DE_EMPRADRONAMIENTO);
            Init(false);
            return View();
        }
        private List<BEREPORTE_DE_EMPRADRONAMIENTO> ListaReporteDeEmpadronamientoTmp
        {
            get
            {
                return (List<BEREPORTE_DE_EMPRADRONAMIENTO>)Session[K_SESSION_LISTA_REPORTE_DE_EMPRADRONAMIENTO];
            }
            set
            {
                Session[K_SESSION_LISTA_REPORTE_DE_EMPRADRONAMIENTO] = value;
            }
        }
        public ActionResult ReporteDeEmpadronamiento(string finicio, string ffin, string formato, int ID_OFICINA, string nombreoficina)
        {
            string format = formato;
            int oficina_id = 0;
            if (nombreoficina.Equals("--SELECCIONE--"))
            {
                nombreoficina = ("TODAS LAS OFICINAS");
            }
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            Resultado retorno = new Resultado();
            if (ID_OFICINA == 1)
            {
                ID_OFICINA = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            }
            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(oficina_id);

            if (opcAdm == 1)
            {
                oficina = nombreoficina;
            }
            //else
            //    ID_OFICINA = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            try
            {

                LocalReport localReport = new LocalReport();
                if (formato == "PDF")
                {
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_GESTION_EMPADRONAMIENTO.rdlc");

                }
                else
                {
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_GESTION_EMPADRONAMIENTO_EXCEL.rdlc");
                }
                List<BEREPORTE_DE_EMPRADRONAMIENTO> lstReporte = new List<BEREPORTE_DE_EMPRADRONAMIENTO>();
                //lstReporte = new BLRegistroVenta().ReporteRegistroVenta(fini, ffin, idoficina, rubrofiltro);
                lstReporte = ListaReporteDeEmpadronamientoTmp;
                if (lstReporte.Count > 0)
                {
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = lstReporte;
                    localReport.DataSources.Add(reportDataSource);

                    ReportParameter parametroFecha1 = new ReportParameter();
                    parametroFecha1 = new ReportParameter("FechaInicio", finicio);
                    localReport.SetParameters(parametroFecha1);

                    ReportParameter parametroFecha2 = new ReportParameter();
                    parametroFecha2 = new ReportParameter("FechaFin", ffin);
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

                    //CODIGO REPETIBLE
                    //The DeviceInfo settings should be changed based on the reportType            
                    //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
                    string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>" + format + "</OutputFormat>" +
                    //  "  <PageWidth>8.5in</PageWidth>" +
                    "  <PageWidth>11in</PageWidth>" +
                    //"  <PageHeight>11in</PageHeight>" +
                    "  <PageHeight>16.3in</PageHeight>" +
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
                    localReport.DisplayName = "Reporte de Empadronamiento";

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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte de Empadronamiento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ReporteTipo(string finicio, string ffin, string formato, int ID_OFICINA, string TIPO_PAGO)
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_DE_EMPRADRONAMIENTO);
            Resultado retorno = new Resultado();
            if (ID_OFICINA == 1)
            {
                ID_OFICINA = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            }
            string format = formato;
            int oficina_id = 0;

            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            //Recupera el codigo de la oficina por la constante

            //OBTIENE DATOS PARA GENERAR EL REPORTE
            try
            {
                List<BEREPORTE_DE_EMPRADRONAMIENTO> listar = new List<BEREPORTE_DE_EMPRADRONAMIENTO>();
                listar = new BLREPORTE_DE_EMPRADRONAMIENTO().ObtenerDatosREPORTE_DE_GESTION_EMPADRONAMIENTO(finicio, ffin, ID_OFICINA, oficina_id, TIPO_PAGO);
                ListaReporteDeEmpadronamientoTmp = new List<BEREPORTE_DE_EMPRADRONAMIENTO>();
                ListaReporteDeEmpadronamientoTmp = listar;
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
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
    public class ReporteEstablecimientoController : Base
    {
        private const string K_SESSION_LISTA_REPORTE_ESTABLECIMIENTO = "___K_SESSION_LISTA_REPORTE_BECESPECIALES";

        // GET: ReporteEstablecimiento
        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_ESTABLECIMIENTO);
            Init(false);
            return View();
        }
        private List<BE_Reporte_Establecimiento> ListaReporteDeEstablecimientoTmp
        {
            get
            {
                return (List<BE_Reporte_Establecimiento>)Session[K_SESSION_LISTA_REPORTE_ESTABLECIMIENTO];
            }
            set
            {
                Session[K_SESSION_LISTA_REPORTE_ESTABLECIMIENTO] = value;
            }
        }

        public ActionResult ReporteEstablecimiento(string mog_id, int id_socio, int id_departamento, int id_provincia
            , int id_distrito, int id_est, string formato, string tipo)
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

                if (format == "PDF")
                {
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_ReporteEstablecimiento.rdlc");
                }
                else
                {
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_ReporteEstablecimiento_Excel.rdlc");
                }
                    

                List<BE_Reporte_Establecimiento> lstReporte = new List<BE_Reporte_Establecimiento>();
                lstReporte = ListaReporteDeEstablecimientoTmp;

                if (lstReporte.Count > 0)
                {
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = lstReporte;
                    localReport.DataSources.Add(reportDataSource);

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

        public ActionResult ReporteTipo(string mog_id,int id_socio, int id_departamento, int id_provincia
            , int id_distrito, int id_est, string formato)
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_ESTABLECIMIENTO);
            Resultado retorno = new Resultado();
            int oficina_id = 0;
            string format = formato;

            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            if (oficina_id == 10081)
            {
                oficina_id = 0;
            }
            try
            {
                List<BE_Reporte_Establecimiento> listar = new List<BE_Reporte_Establecimiento>();

                listar = new BL_ReporteEstablecimiento().ListarDatosEstablecimiento(mog_id,id_socio, id_departamento, id_provincia, id_distrito, id_est);
                ListaReporteDeEstablecimientoTmp = new List<BE_Reporte_Establecimiento>();
                ListaReporteDeEstablecimientoTmp = listar;

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
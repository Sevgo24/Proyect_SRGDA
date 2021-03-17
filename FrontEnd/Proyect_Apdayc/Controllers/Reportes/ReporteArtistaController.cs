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
    public class ReporteArtistaController : Base
    {
        // GET: ReporteArtista
        private const string K_SESSION_LISTA_REPORTE_ARTISTA = "___K_SESSION_LISTA_REPORTE_ARTISTA";
        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_ARTISTA);
            Init(false);
            return View();
        }
        private List<BE_LICENCIA_X_ARTISTA> ListaReporte
        {
            get
            {
                return (List<BE_LICENCIA_X_ARTISTA>)Session[K_SESSION_LISTA_REPORTE_ARTISTA];
            }
            set
            {
                Session[K_SESSION_LISTA_REPORTE_ARTISTA] = value;
            }
        }
        //BL_LICENCIA_X_ARTISTA
        public ActionResult ReporteArtista(string artista, string fini, string ffin, string formato)
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
              
                localReport.ReportPath = Server.MapPath("~/Reportes/Reporte_licencia_x_artista.rdlc");
              
                List<BE_LICENCIA_X_ARTISTA> lstReporte = new List<BE_LICENCIA_X_ARTISTA>();
                lstReporte = ListaReporte;

                if (lstReporte.Count > 0 && artista!="")
                {
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = lstReporte;
                    localReport.DataSources.Add(reportDataSource);

                    ReportParameter parametroFecha1 = new ReportParameter();
                    parametroFecha1 = new ReportParameter("FechaInicio", fini.ToString());
                    localReport.SetParameters(parametroFecha1);

                    ReportParameter parametroFecha2 = new ReportParameter();
                    parametroFecha2 = new ReportParameter("FechaFin", ffin.ToString());
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

                    ReportParameter ARTIS = new ReportParameter();
                    ARTIS = new ReportParameter("ARTIS", artista.ToUpper());
                    localReport.SetParameters(ARTIS);

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
                    "  <PageWidth>9in</PageWidth>" +
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
                    localReport.DisplayName = "Reporte de Artistas";

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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte de Artistas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ReporteTipo(string artista, string fini, string ffin, string formato)
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_ARTISTA);
            Resultado retorno = new Resultado();
            int oficina_id = 0;
            string format = formato;


            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            try
            {
                List<BE_LICENCIA_X_ARTISTA> listar = new List<BE_LICENCIA_X_ARTISTA>();

             
                    listar = new BL_LICENCIA_X_ARTISTA().LISTAR_LICENCIA_X_ARTISTA(artista, fini, ffin);
                    ListaReporte = new List<BE_LICENCIA_X_ARTISTA>();
                    ListaReporte = listar;
             

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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte de Artista", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}
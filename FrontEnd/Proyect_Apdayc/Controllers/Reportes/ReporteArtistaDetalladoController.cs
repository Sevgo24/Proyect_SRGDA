using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases;
using SGRDA.BL.Reporte;
using SGRDA.Entities;
using SGRDA.Entities.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Controllers.Reportes
{
    public class ReporteArtistaDetalladoController : Base
    {
        // GET: ReporteArtistaDetallado
        private const string K_SESSION_LISTA_REPORTE_DE_ARTISTAS_DETALLADO = "___K_SESSION_LISTA_REPORTE_BECESPECIALES";
        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_DE_ARTISTAS_DETALLADO);
            Init(false);
            return View();
        }
        private List<BEReporteArtistaDetallado> ListaReporteArtistaDetalladoTmp
        {
            get
            {
                return (List<BEReporteArtistaDetallado>)Session[K_SESSION_LISTA_REPORTE_DE_ARTISTAS_DETALLADO];
            }
            set
            {
                Session[K_SESSION_LISTA_REPORTE_DE_ARTISTAS_DETALLADO] = value;
            }
        }

        public ActionResult ReporteArtistaDetallado(string femi_ini, string femi_fin, string feve_ini, string feve_fin, string fcan_ini, string fcan_fin
                                                    , string fcon_ini, string fcon_fin, string artista, string formato
                                                    , int con_Emision, int con_Evento, int con_Cancelacion, int con_Contable)
        {
            string format = formato;
            int oficina_id = 0;
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            Resultado retorno = new Resultado();
            string FiltroFecha = "";
            List<BESelectListItem> lista = new List<BESelectListItem>();
            if (con_Emision == 1)
            {
                BESelectListItem be = new BESelectListItem();
                be.Value_Alfresco = "FECHA EMISION :  ";
                be.Text = femi_ini;
                be.Value = femi_fin;
                lista.Add(be);
            }
            if (con_Evento == 1)
            {
                BESelectListItem be = new BESelectListItem();
                be.Value_Alfresco = " FECHA EVENTO :  ";
                be.Text = feve_ini;
                be.Value = feve_fin;
                lista.Add(be);
            }
            if (con_Cancelacion == 1)
            {
                BESelectListItem be = new BESelectListItem();
                be.Value_Alfresco = "FECHA CANCELACION :  ";
                be.Text = fcan_ini;
                be.Value = fcan_fin;
                lista.Add(be);
            }
            if (con_Contable == 1)
            {
                BESelectListItem be = new BESelectListItem();
                be.Value_Alfresco = "FECHA CONTABLE :  ";
                be.Text = fcon_ini;
                be.Value = fcon_fin;
                lista.Add(be);
            }

            foreach (var i in lista)
            {
                FiltroFecha += "\n\r" + i.Value_Alfresco + i.Text + "     al     " + i.Value;
            }
            try
            {
                ///         DS_REC_BECS_ESPECIALES
                LocalReport localReport = new LocalReport();
                if (format == "PDF")
                {
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_ARTISTA_DETALLADO.rdlc");
                }
                else if (format == "EXCEL")
                {
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_ARTISTA_DETALLADO_EXCEL.rdlc");
                }
                else
                {
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_ARTISTA_DETALLADO_EXCEL_FILTRO.rdlc");
                    format = "EXCEL";
                }


                List<BEReporteArtistaDetallado> lstReporte = new List<BEReporteArtistaDetallado>();
                List<BEReporteArtistaDetallado> Lista2 = new List<BEReporteArtistaDetallado>();
                lstReporte = ListaReporteArtistaDetalladoTmp;
                decimal Facturado = 0;
                decimal Cobrado = 0;

                var Lista = (from LISTA in lstReporte
                             select new { LIC_ID = LISTA.LIC_ID, FACTURADO = LISTA.FACTURADO, COBRADO = LISTA.RECAUDADO }).Distinct().ToList();

                foreach (var i in Lista)
                {
                    Facturado = i.FACTURADO + Facturado;
                }
                foreach (var i in Lista)
                {
                    Cobrado = i.COBRADO + Cobrado;
                }

                if (lstReporte.Count > 0)
                {
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = lstReporte;
                    localReport.DataSources.Add(reportDataSource);

                    //ReportParameter parametroFecha1 = new ReportParameter();
                    //parametroFecha1 = new ReportParameter("FechaInicio", fini.ToString());
                    //localReport.SetParameters(parametroFecha1);

                    //ReportParameter parametroFecha2 = new ReportParameter();
                    //parametroFecha2 = new ReportParameter("FechaFin", ffin.ToString());
                    //localReport.SetParameters(parametroFecha2);

                    ReportParameter parametroNomusu = new ReportParameter();
                    parametroNomusu = new ReportParameter("NombreUsuario", oficina);
                    localReport.SetParameters(parametroNomusu);

                    ReportParameter parametroNomoficina = new ReportParameter();
                    parametroNomoficina = new ReportParameter("NombreOficina", usuario);
                    localReport.SetParameters(parametroNomoficina);

                    ReportParameter fecha = new ReportParameter();
                    fecha = new ReportParameter("FechaImpresion", DateTime.Now.ToShortDateString());
                    localReport.SetParameters(fecha);

                    ReportParameter parametroFacturado = new ReportParameter();
                    parametroFacturado = new ReportParameter("Facturado", Facturado.ToString("### ##0.000"));
                    localReport.SetParameters(parametroFacturado);

                    ReportParameter parametroCobrado = new ReportParameter();
                    parametroCobrado = new ReportParameter("Cobrado", Cobrado.ToString("### ##0.000"));
                    localReport.SetParameters(parametroCobrado);

                    ReportParameter parametroFiltroFecha = new ReportParameter();
                    parametroFiltroFecha = new ReportParameter("FiltroFecha", FiltroFecha);
                    localReport.SetParameters(parametroFiltroFecha);

                    ReportParameter parametroArtista = new ReportParameter();
                    parametroArtista = new ReportParameter("Artista", artista.ToUpper());
                    localReport.SetParameters(parametroArtista);

                    //ReportParameter ARTIS = new ReportParameter();
                    //ARTIS = new ReportParameter("ARTIS", artista.ToUpper());
                    //localReport.SetParameters(ARTIS);

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

        public ActionResult ReporteTipo(string femi_ini, string femi_fin, string feve_ini, string feve_fin, string fcan_ini, string fcan_fin, string fcon_ini, string fcon_fin, string artista, string formato
                                        , int con_Emision, int con_Evento, int con_Cancelacion, int con_Contable)
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_DE_ARTISTAS_DETALLADO);
            Resultado retorno = new Resultado();
            int oficina_id = 0;
            string format = formato;
            if (con_Emision != 1)
            {
                femi_ini = "";
                femi_fin = "";
            }
            if (con_Evento != 1)
            {
                feve_ini = "";
                feve_fin = "";
            }
            if (con_Cancelacion != 1)
            {
                fcan_ini = "";
                fcan_fin = "";
            }
            if (con_Contable != 1)
            {
                fcon_ini = "";
                fcon_fin = "";
            }

            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            if (oficina_id == 10081)
            {
                oficina_id = 0;
            }
            try
            {
                List<BEReporteArtistaDetallado> listar = new List<BEReporteArtistaDetallado>();

                listar = new BLReporteArtistaDetallado().ListarArtistaDetallado(femi_ini, femi_fin, feve_ini, feve_fin, fcan_ini, fcan_fin, fcon_ini, fcon_fin, artista);
                ListaReporteArtistaDetalladoTmp = new List<BEReporteArtistaDetallado>();
                ListaReporteArtistaDetalladoTmp = listar;

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

        public JsonResult GetCiudades(string Artista)
        {
            Resultado retorno = new Resultado();
            try
            {

                if (!isLogout(ref retorno))
                {
                    var datos = new BLReporteArtistaDetallado().ListaArtista(Artista)
                     .Select(c => new BESelectListItem
                     {
                         Value = c.Artista,
                         Text = c.Artista

                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarPlaneamientoLicenciaxOpcion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}
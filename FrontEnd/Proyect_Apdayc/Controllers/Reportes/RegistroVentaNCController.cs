using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases;
using SGRDA.BL.Reporte;
using SGRDA.Entities;
using SGRDA.Entities.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Controllers.Reportes   
{
    public class RegistroVentaNCController : Base
    {
        //
        // GET: /RegistroVentaNC/
        private const string ___K_SESSION_LISTA_REPORTE_NOTA_CREDITO = "___K_SESSION_LISTA_REPORTE_NOTA_CREDITO";

        public ActionResult Index()
        {
            Session.Remove(___K_SESSION_LISTA_REPORTE_NOTA_CREDITO);
            Init(false);
            return View();

        }

        private List<BERegistroVentaNC> ListaReporteRegistroVentaNCTmp
        {
            get
            {
                return (List<BERegistroVentaNC>)Session[___K_SESSION_LISTA_REPORTE_NOTA_CREDITO];
            }
            set
            {
                Session[___K_SESSION_LISTA_REPORTE_NOTA_CREDITO] = value;
            }
        }

        public ActionResult ReporteRegistroVenta(string fini, string ffin, string formato, string rubro, string idoficina, string nombreoficina,int ESTADO)
        {
            string format = formato;
            int oficina_id = 0;

            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            Resultado retorno = new Resultado();

            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(oficina_id);

            if (opcAdm == 1)
            {
                idoficina = idoficina;
                oficina = nombreoficina;
            }
            else
                idoficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            try
            {

                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reportes/RptRegistroVentasNC.rdlc");

                List<BERegistroVentaNC> lstReporte = new List<BERegistroVentaNC>();
                //lstReporte = new BLRegistroVenta().ReporteRegistroVenta(fini, ffin, idoficina, rubrofiltro);
                lstReporte = ListaReporteRegistroVentaNCTmp;
                if (lstReporte.Count > 0)
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

                    ReportParameter fecha = new ReportParameter();
                    fecha = new ReportParameter("Fecha", DateTime.Now.ToShortDateString());
                    localReport.SetParameters(fecha);
                    if (idoficina.Equals("0"))
                    {
                        oficina = "TODAS LAS OFICINAS";
                    }
                    ReportParameter parametroNomusu = new ReportParameter();
                    parametroNomusu = new ReportParameter("NombreUsuario", oficina);
                    localReport.SetParameters(parametroNomusu);

                    ReportParameter parametroNomoficina = new ReportParameter();
                    parametroNomoficina = new ReportParameter("NombreOficina", usuario);
                    localReport.SetParameters(parametroNomoficina);

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
                    localReport.DisplayName = "Reporte Nota de Credito";

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

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte Nota de Credito", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ReporteTipo(string fini, string ffin, string formato, string idoficina, string nombreoficina,int ESTADO)
        {
            Session.Remove(___K_SESSION_LISTA_REPORTE_NOTA_CREDITO);
            Resultado retorno = new Resultado();

            string format = formato;
            int oficina_id = 0;

            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(oficina_id);

            //if (oficina_id == 10081 || oficina_id == 10082)
            if (opcAdm == 1)
                idoficina = idoficina;
            else
                idoficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);



            //OBTIENE DATOS PARA GENERAR EL REPORTE
            try
            {
                List<BERegistroVentaNC> listar = new List<BERegistroVentaNC>();
                listar = new BLRegistroVentaNC().ReporteRegistroVentaNC(fini, ffin, idoficina, ESTADO);
                ListaReporteRegistroVentaNCTmp = new List<BERegistroVentaNC>();
                ListaReporteRegistroVentaNCTmp = listar;
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte Diario de Caja", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}


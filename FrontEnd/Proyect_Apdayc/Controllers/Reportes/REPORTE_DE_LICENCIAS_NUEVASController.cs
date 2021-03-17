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
    public class REPORTE_DE_LICENCIAS_NUEVASController : Base
    {
        // GET: REPORTE_DE_LICENCIAS_NUEVAS
        private const string K_SESSION_LISTA_REPORTE_DE_LICENCIAS_NUEVAS = "___K_SESSION_LISTA_REPORTE_DE_LICENCIAS_NUEVAS ";
        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_DE_LICENCIAS_NUEVAS);
            Init(false);
            return View();
        }

        private List<BE_REPORTE_DE_LICENCIAS_NUEVAS> ListaReporteDeEmpadronamientoTmp
        {
            get
            {
                return (List<BE_REPORTE_DE_LICENCIAS_NUEVAS>)Session[K_SESSION_LISTA_REPORTE_DE_LICENCIAS_NUEVAS];
            }
            set
            {
                Session[K_SESSION_LISTA_REPORTE_DE_LICENCIAS_NUEVAS] = value;
            }
        }
        public ActionResult ReporteDeLicenciasNuevas(string fini, string ffin, string formato, int ID_SOCIO, string ID_MODALIDAD, int ID_OFICINA, string nombreoficina)
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
                localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_LICENCIAS_NUEVAS.rdlc");

                List<BE_REPORTE_DE_LICENCIAS_NUEVAS> lstReporte = new List<BE_REPORTE_DE_LICENCIAS_NUEVAS>();
                //lstReporte = new BLRegistroVenta().ReporteRegistroVenta(fini, ffin, idoficina, rubrofiltro);
                lstReporte = ListaReporteDeEmpadronamientoTmp;
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
                    localReport.DisplayName = "Reporte de LICENCIAS NUEVAS";

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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte de LICENCIAS NUEVAS", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ReporteTipo(string finicio, string ffin, string formato, int ID_SOCIO, string ID_MODALIDAD, int ID_OFICINA, int Estado)
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_DE_LICENCIAS_NUEVAS);
            Resultado retorno = new Resultado();

            string format = formato;
            int oficina_id = 0;
            int? rubrofiltro = null;
            if (ID_OFICINA == 1)
            {
                ID_OFICINA = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            }
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(oficina_id);

            //if (oficina_id == 10081 || oficina_id == 10082)
            if (opcAdm != 1)
                ID_OFICINA = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);



            //Recupera el codigo de la oficina por la constante


            //OBTIENE DATOS PARA GENERAR EL REPORTE
            try
            {
                List<BE_REPORTE_DE_LICENCIAS_NUEVAS> listar = new List<BE_REPORTE_DE_LICENCIAS_NUEVAS>();
                listar = new BLREPORTE_DE_LICENCIAS_NUEVAS().ObtenerDatosREPORTE_DE_LICENCIAS_NUEVAS(finicio, ffin, ID_SOCIO, ID_MODALIDAD, ID_OFICINA, Estado);
                ListaReporteDeEmpadronamientoTmp = new List<BE_REPORTE_DE_LICENCIAS_NUEVAS>();
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte De Licencias Nuevas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}
using System;
using SGRDA.BL;
using SGRDA.BL.Reporte;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System.Xml;
using System.Text;
using System.Drawing;
using System.IO;
using System.Net;
using System.Globalization;
namespace Proyect_Apdayc.Controllers.AdministracionMatrizLicencias
{
    public class AdministracionMatrizLicenciaController : Base
    {
        // GET: AdministracionMatrizLicencia

        private const string K_SESSION_LISTA_REPORTE_MATRIZ_LICENCIA = "___K_SESSION_LISTA_REPORTE_MATRIZ_LICENCIA";

        public class VARIABLES
        {
            public const int SI = 1;
            public const int NO = 0;
            public const int CERO = 0;
            public const string MSG_ERROR_AL_LISTAR_MATRIZ = "OCURRIO UN ERROR AL LISTAR LAS LICENCIAS | POR FAVOR DETALLE LOS PARAMETROS AL ADMINISTRADOR RESPONSABLE DEL MODULO PARA SU VERIFICACION";
            public const int ULTIMO_PERIODO_FACTURADO=1;
            public const int HUECOS_PERIODOS = 2;
            public const int VALIDACION_MENSUAL_PASO = 3;
            public const int VALIDACION_MENSUAL_NO_PASO = 4;
            public const string MSG_SELECCIONE_OFICINA = "NO HA SELECCIONADO UNA OFICINA VALIDA";
        }


        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_MATRIZ_LICENCIA);
            return View();
        }

        private List<BEAdministracionMatrizLicencia> ListaReporte
        {
            get
            {
                return (List<BEAdministracionMatrizLicencia>)Session[K_SESSION_LISTA_REPORTE_MATRIZ_LICENCIA];
            }
            set
            {
                Session[K_SESSION_LISTA_REPORTE_MATRIZ_LICENCIA] = value;
            }
        }

        public JsonResult ListarMatrizLicencia(int skip, int take, int page, int pageSize, decimal LIC_ID, decimal BPS_ID, string RAZ_SOC, string NUM_IDE, string NOM_SOC, string APE_SOC, string MAT_SOC, string EST_NAM, string MOG_ID, int CON_FEC, string FEC_INI, string FEC_FIN, decimal DIV_ID, decimal DEP_ID, decimal PROV_ID, decimal DIST_ID, decimal OFF_ID, int ESTADO_LIC, int ESTADO_LIC_FACT,int ESTADO_PL_BLOQ,decimal CODIGO_AGENTE,int OPCIONES_BUSQ,string FEC_INI_BUS, string FEC_FIN_BUS)//
        {
            Resultado retorno = new Resultado();

            var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));

            var ValidaOficinaPadre = new BLAdministracionMatrizLicencia().ObtieneValidacionOficinaPadre(oficina);



            if (opcAdm == VARIABLES.NO  && ValidaOficinaPadre==VARIABLES.NO) // SI NO PASA LA VALIDACION  SELECCIONAR LA OFICINA A LA QUE PERTENECE EL USUARIO
                OFF_ID = oficina;

            List<BEAdministracionMatrizLicencia> lista = null;
            try
            {
                if (OPCIONES_BUSQ == VARIABLES.ULTIMO_PERIODO_FACTURADO && OFF_ID> VARIABLES.CERO)
                    lista = new BLAdministracionMatrizLicencia().lista(skip, take, page, pageSize, LIC_ID, BPS_ID, RAZ_SOC, NUM_IDE, NOM_SOC, APE_SOC, MAT_SOC, EST_NAM, MOG_ID, CON_FEC, FEC_INI, FEC_FIN, DIV_ID, DEP_ID, PROV_ID, DIST_ID, OFF_ID, ESTADO_LIC, ESTADO_LIC_FACT, ESTADO_PL_BLOQ, CODIGO_AGENTE, OPCIONES_BUSQ, FEC_INI_BUS, FEC_FIN_BUS);
                else if (OPCIONES_BUSQ == VARIABLES.HUECOS_PERIODOS && OFF_ID > VARIABLES.CERO)
                    lista = new BLAdministracionMatrizLicencia().listaHuecos(skip, take, page, pageSize, LIC_ID, BPS_ID, RAZ_SOC, NUM_IDE, NOM_SOC, APE_SOC, MAT_SOC, EST_NAM, MOG_ID, CON_FEC, FEC_INI, FEC_FIN, DIV_ID, DEP_ID, PROV_ID, DIST_ID, OFF_ID, ESTADO_LIC, ESTADO_LIC_FACT, ESTADO_PL_BLOQ, CODIGO_AGENTE, OPCIONES_BUSQ, FEC_INI_BUS, FEC_FIN_BUS);
                else if (OPCIONES_BUSQ == VARIABLES.VALIDACION_MENSUAL_NO_PASO && OFF_ID > VARIABLES.CERO)
                    lista = new BLAdministracionMatrizLicencia().listaLicenciasValidacionMensual(skip, take, page, pageSize, LIC_ID, BPS_ID, RAZ_SOC, NUM_IDE, NOM_SOC, APE_SOC, MAT_SOC, EST_NAM, MOG_ID, CON_FEC, FEC_INI, FEC_FIN, DIV_ID, DEP_ID, PROV_ID, DIST_ID, OFF_ID, ESTADO_LIC, ESTADO_LIC_FACT, ESTADO_PL_BLOQ, CODIGO_AGENTE, OPCIONES_BUSQ, FEC_INI_BUS, FEC_FIN_BUS);
                else if (OPCIONES_BUSQ == VARIABLES.VALIDACION_MENSUAL_PASO && OFF_ID > VARIABLES.CERO)
                    lista = new BLAdministracionMatrizLicencia().listaLicenciasValidacionMensualPaso(skip, take, page, pageSize, LIC_ID, BPS_ID, RAZ_SOC, NUM_IDE, NOM_SOC, APE_SOC, MAT_SOC, EST_NAM, MOG_ID, CON_FEC, FEC_INI, FEC_FIN, DIV_ID, DEP_ID, PROV_ID, DIST_ID, OFF_ID, ESTADO_LIC, ESTADO_LIC_FACT, ESTADO_PL_BLOQ, CODIGO_AGENTE, OPCIONES_BUSQ, FEC_INI_BUS, FEC_FIN_BUS);
                else if (OFF_ID == VARIABLES.CERO)
                {
                    retorno.result = VARIABLES.NO;
                    retorno.message = VARIABLES.MSG_SELECCIONE_OFICINA;
                }
                ListaReporte = lista;

            }
            catch (Exception ex)
            {
                retorno.result = VARIABLES.NO;
                retorno.message = VARIABLES.MSG_ERROR_AL_LISTAR_MATRIZ;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "LISTAR Licencia", ex);
            }

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEAdministracionMatrizLicencia { listaMatrizLicencia = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEAdministracionMatrizLicencia { listaMatrizLicencia = lista, TotalVirtual = lista.Count() }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ReporteMatrizLicencia(string formato)
        {
            string format = formato;
            int oficina_id = 0;
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            Resultado retorno = new Resultado();

            try
            {

                List<BEAdministracionMatrizLicencia> lstReporte = new List<BEAdministracionMatrizLicencia>();
                lstReporte = ListaReporte;

                if (lstReporte.Count() > 0 && lstReporte != null)
                {
                    LocalReport localReport = new LocalReport();

                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REPORTE_MATRIZ_LICENCIA.rdlc");

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
                    localReport.DisplayName = "Reporte de consulta de Licencias";

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
                    retorno.message = "REALIZAR LA CONSULTA ANTES DE MOSTRAR EL PDF | EL REPORTE NO TIENE REGISTROS QUE MOSTRAR";
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte de MATRIZ DE LICENCIA", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }


        public ActionResult ReporteTipo()
        {
            Resultado retorno = new Resultado();

            try
            {

                List<BEAdministracionMatrizLicencia> listar = new List<BEAdministracionMatrizLicencia>();
                listar = ListaReporte;


                if (listar != null && listar.Count > 0)
                {
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "REALIZAR BUSQUEDA ANTES DE (VER PDF / REP. EXCEL) |  LA BUSQUEDA NO DEVOLVIO NINGUN RESULTADO";
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


        public ActionResult ReporteMatrizLicenciaValidacion(string formato)
        {
            string format = formato;
            int oficina_id = 0;
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            Resultado retorno = new Resultado();

            try
            {

                List<BEAdministracionMatrizLicencia> lstReporte = new List<BEAdministracionMatrizLicencia>();
                lstReporte = ListaReporte;

                if (lstReporte.Count() > 0 && lstReporte != null)
                {
                    LocalReport localReport = new LocalReport();

                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REPORTE_MATRIZ_LICENCIA_VALIDACION.rdlc");

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
                    localReport.DisplayName = "Reporte de consulta de Licencias";

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
                    retorno.message = "REALIZAR LA CONSULTA ANTES DE MOSTRAR EL PDF | EL REPORTE NO TIENE REGISTROS QUE MOSTRAR";
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte de MATRIZ DE LICENCIA", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
    }
}
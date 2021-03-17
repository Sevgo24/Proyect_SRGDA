using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyect_Apdayc.Clases;
using SGRDA.Entities.Reporte;
using SGRDA.BL.Reporte;
using Microsoft.Reporting.WebForms;

namespace Proyect_Apdayc.Controllers.Reportes
{
    public class BecEspecialesController : Base
    {
        // GET: BecEspeciales
        private const string K_SESSION_LISTA_REPORTE_DE_BECSESPECIALES = "___K_SESSION_LISTA_REPORTE_BECESPECIALES";
        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_DE_BECSESPECIALES);
            Init(false);
            return View();
        }
        private List<Be_BecsEspeciales> ListaReporteDeBecsTmp
        {
            get
            {
                return (List<Be_BecsEspeciales>)Session[K_SESSION_LISTA_REPORTE_DE_BECSESPECIALES];
            }
            set
            {
                Session[K_SESSION_LISTA_REPORTE_DE_BECSESPECIALES] = value;
            }
        }

        public ActionResult ReporteBecEspeciales( string formato,int cant,int mes,int anio, string tipo)
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
                if (tipo == "D")
                {
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_BECS_ESPECIALES.rdlc");
                }
                else if (tipo == "R")
                {
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_BECS_ESPECIALES_RESUMEN.rdlc");
                }
                List<Be_BecsEspeciales> lstReporte = new List<Be_BecsEspeciales>();
                lstReporte = ListaReporteDeBecsTmp;
                var fini = ListaReporteDeBecsTmp[0].FechaInicio;
                var ffin = ListaReporteDeBecsTmp[0].FechaFin;
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
                    string deviceInfo="";
                    //CODIGO REPETIBLE
                    //The DeviceInfo settings should be changed based on the reportType            
                    //http://msdn2.microsoft.com/en-us/library/ms155397.aspx       
                    if (tipo == "D")
                    {
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
                    }   
                    else if (tipo == "R")
                    {
                    deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>" + format + "</OutputFormat>" +
                    //  "  <PageWidth>8.5in</PageWidth>" +
                    "  <PageWidth>8in</PageWidth>" +
                    //"  <PageHeight>11in</PageHeight>" +
                    "  <PageHeight>16.3in</PageHeight>" +
                    "  <MarginTop>0.0in</MarginTop>" +
                    "  <MarginLeft>0.3in</MarginLeft>" +
                    "  <MarginRight>0.0in</MarginRight>" +
                    "  <MarginBottom>0.3in</MarginBottom>" +
                    "</DeviceInfo>";
                    }  
                   

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

        public ActionResult ReporteTipo(int cant,int mes, int anio, string formato,string tipo )
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_DE_BECSESPECIALES);
            Resultado retorno = new Resultado();
            int oficina_id = 0;
            string format = formato;            

          
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            if(oficina_id== 10081)
            {
                oficina_id = 0;
            }
            try
            {
                List<Be_BecsEspeciales> listar = new List<Be_BecsEspeciales>();

                if(tipo=="D")
                {
                    listar = new BL_BecEspeciales().ListarDatosBesEspeciales(cant, mes, anio, oficina_id);
                    ListaReporteDeBecsTmp = new List<Be_BecsEspeciales>();
                    ListaReporteDeBecsTmp = listar;
                }
                else if(tipo == "R")
                {
                    listar = new BL_BecEspeciales().ListarDatosBesEspecialesResumen(cant, mes, anio, oficina_id);
                    ListaReporteDeBecsTmp = new List<Be_BecsEspeciales>();
                    ListaReporteDeBecsTmp = listar;
                }
                
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
        [HttpPost]
        public JsonResult ListarAniosCierre()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BL_BecEspeciales().ListarAniosCierre().Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.ACCOUNTANT_YEAR),
                        Text = Convert.ToString(c.ACCOUNTANT_YEAR)
                    });


                    var idOficina = Convert.ToInt32(Session[Constantes.Sesiones.CodigoOficina]);
                    var idPerfilAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["idPerfilAdminSeg"];

                    if (Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]) != Convert.ToString(idPerfilAdmin))
                    {
                        retorno.valor = "0";
                        retorno.Code = idOficina;
                    }
                    else
                    {
                        retorno.valor = "1";
                        retorno.Code = 0;
                    }


                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarAniosCierre", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarMesesCierre(int anio)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BL_BecEspeciales().ListarMesesCierre(anio).Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.ACCOUNTANT_MONTH),
                        Text = Convert.ToString(c.NOMBRE_MES)
                    });


                    var idOficina = Convert.ToInt32(Session[Constantes.Sesiones.CodigoOficina]);
                    var idPerfilAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["idPerfilAdminSeg"];

                    if (Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]) != Convert.ToString(idPerfilAdmin))
                    {
                        retorno.valor = "0";
                        retorno.Code = idOficina;
                    }
                    else
                    {
                        retorno.valor = "1";
                        retorno.Code = 0;
                    }


                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarAniosCierre", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL.Reporte;
using SGRDA.Entities.Reporte;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;

namespace Proyect_Apdayc.Controllers.Reportes
{
    public class ReporteComprobanteBancarioController : Base
    {
        public const string nomAplicacion = "SRGDA";
        private const string K_SESSION_LISTA_REPORTE_COMPROBANTE_DEPOSITO = "___K_SESSION_LISTA_REPORTE_COMPROBANTE_DEPOSITO ";
        private static string Fecha_Rechazo_Ini = "";
        private static string Fecha_Rechazo_Fin = "";
        private static int conRechazo = 0;
        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_COMPROBANTE_DEPOSITO);
            return View();
        }

        private List<BEComprobanteBancario> ListaReporteComprobanteBancarioTmp
        {
            get
            {
                return (List<BEComprobanteBancario>)Session[K_SESSION_LISTA_REPORTE_COMPROBANTE_DEPOSITO];
            }
            set
            {
                Session[K_SESSION_LISTA_REPORTE_COMPROBANTE_DEPOSITO] = value;
            }
        }


        public ActionResult ReporteDiarioCaja(string fini, string ffin, string formato, string rubro, string idoficina, string nombreoficina
                                                , int conIng, int conCon, string finiCon, string ffinCon, string estado
                                                )
        {
            string format = formato;
            int oficina_id = 0;
            int? rubrofiltro = null;

            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            Resultado retorno = new Resultado();

            //si oficina es igual a && oficina_id != 26
            //if (oficina_id == 10081 || oficina_id == 10082)
            //{
            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(oficina_id);
            if (opcAdm == 1)
            {
                idoficina = idoficina;
                oficina = nombreoficina;
            }
            else
                idoficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            /*
             * 26 OFICINA TV / CABLE
                25 OFICINA TRANSPORTE, DISCOTECA Y RADIO
             *  27 OFICINA SINCRONIZACIÓN Y REDEs 
             */

            if (idoficina == "26")
            {
                if (rubro == "T")
                {
                    rubrofiltro = 50;
                    oficina = "TELEVISIÓN";
                }
                else
                {
                    rubrofiltro = 39;
                    oficina = "CABLE";
                }
            }
            //Buscando Oficina como Admin o Contabilidad
            // if (idoficina==)

            try
            {
                //List<BEREPORTE_CAJA> listar = new List<BEREPORTE_CAJA>();
                //listar = new BLREGISTRO_CAJA().ReporteRegistroCaja(fini, ffin, idoficina, rubrofiltro);
                bool Cobro = false;
                List<BEComprobanteBancario> listar = new List<BEComprobanteBancario>();
                DateTime FechaI = Convert.ToDateTime(fini);
                DateTime FechaF = Convert.ToDateTime(ffin);
                //DateTime FechaCobroCorte = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["FECHA_COBRO_INICIO"]);

                //if ((FechaI.CompareTo(FechaCobroCorte) == 0 || FechaI.CompareTo(FechaCobroCorte) == 1)
                //     && (FechaF.CompareTo(FechaCobroCorte) == 0 || FechaF.CompareTo(FechaCobroCorte) == 1))
                //{
                //    listar = new BLREGISTRO_CAJA().ReporteRegistroCaja_Cobros(fini, ffin, idoficina, rubrofiltro);
                //    Cobro = true;
                //}
                //else
                //listar = new BLReporteComprobantesBancarios().ReporteComprobanteBancario(FechaI, FechaF, idoficina
                //    , conIng, conCon, finiCon, ffinCon , estado).ToList();

                listar = ListaReporteComprobanteBancarioTmp;

                if (listar.Count > 0)
                {
                    LocalReport localReport = new LocalReport();

                    //if (Cobro)
                    //    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_DIARIO_CAJA_COBROS.rdlc");
                    //else

                    if (format == "PDF")
                    {
                        localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_DEPOSITOS_BANCARIOS.rdlc");
                    }
                    else if (format == "EXCEL")
                    {
                        localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_DEPOSITOS_BANCARIOS.rdlc");
                    }
                    else if (format == "EXCEL2")
                    {
                        localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_DEPOSITOS_BANCARIOS_EXCEL.rdlc");
                    }

                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = listar;
                    localReport.DataSources.Add(reportDataSource);
                    /*
                    ReportParameter parametro = new ReportParameter();
                    parametro = new ReportParameter("Usuario", UsuarioActual.Trim());
                    localReport.SetParameters(parametro);
                     * 
                      
                     
                    */
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
                    parametroNomusu = new ReportParameter("NombreUsuario", oficina);
                    localReport.SetParameters(parametroNomusu);

                    ReportParameter parametroNomoficina = new ReportParameter();
                    parametroNomoficina = new ReportParameter("NombreOficina", usuario);
                    localReport.SetParameters(parametroNomoficina);


                    ReportParameter parametroFechaIniCon = new ReportParameter();
                    parametroFechaIniCon = new ReportParameter("FechaIniCon", conCon == 1 ? finiCon : "-");
                    localReport.SetParameters(parametroFechaIniCon);

                    ReportParameter parametroFechaFinCon = new ReportParameter();
                    parametroFechaFinCon = new ReportParameter("FechaFinCon", conCon == 1 ? ffinCon : "-");
                    localReport.SetParameters(parametroFechaFinCon);


                    ReportParameter parametroFechaIniRechazo= new ReportParameter();
                    parametroFechaIniRechazo = new ReportParameter("FechaIniRechazo", conRechazo == 1 ? Fecha_Rechazo_Ini : "-");
                    localReport.SetParameters(parametroFechaIniRechazo);

                    ReportParameter parametroFechaFinRechazo = new ReportParameter();
                    parametroFechaFinRechazo = new ReportParameter("FechaFinRechazo", conRechazo == 1 ? Fecha_Rechazo_Fin : "-");
                    localReport.SetParameters(parametroFechaFinRechazo);

                    if (format == "EXCEL2")
                    {
                        format = "EXCEL";
                    }
                    string reportType = format;
                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    //CODIGO REPETIBLE
                    //The DeviceInfo settings should be changed based on the reportType            
                    //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            

                    //string deviceInfo = "<DeviceInfo>" +
                    //    "  <OutputFormat>" + format + "</OutputFormat>" +
                    //    "  <PageWidth>8.5in</PageWidth>" +
                    //    "  <PageHeight>11in</PageHeight>" +
                    //    "  <MarginTop>0.3in</MarginTop>" +
                    //    "  <MarginLeft>0.2in</MarginLeft>" +
                    //    "  <MarginRight>0.5in</MarginRight>" +
                    //    "  <MarginBottom>0.5in</MarginBottom>" +
                    //    "</DeviceInfo>";

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
                    retorno.result = 1;


                    localReport.DisplayName = "Reporte Diario de Caja";
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
                    retorno.message = Constantes.MensajeGenerico.MSG_ERROR_REPORTE;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Reporte Diario de Caja", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //****************************REPORTE*******************************
        //,string oficina le quite esta variable para no confundir a el JAVASCRIPT
        //PARAMETROS FECHA INICIAL,FECHA FINAL,FORMATO DE EL ARCHIV,EL RUBRO Y EL ID DE LA OFICINA , NOMBRE DE LA OFICINA

        public ActionResult ReporteTipo(string fini, string ffin, string formato, string rubro, string idoficina, string nombreoficina
                                        , int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion, string estado
                                        , string con_Rechazo , string ini_Rech, string fin_Rech ,int idBanco)
        {
            //Init(false);//add sysseg
            Session.Remove(K_SESSION_LISTA_REPORTE_COMPROBANTE_DEPOSITO);
            string format = formato;
            int oficina_id = 0;
            int? rubrofiltro = null;
            Fecha_Rechazo_Ini = ini_Rech;
            Fecha_Rechazo_Fin = fin_Rech;
            conRechazo = Convert.ToInt32(con_Rechazo);
            Resultado retorno = new Resultado();

            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            //if (oficina_id == 10081 || oficina_id == 10082)
            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(oficina_id);
            if (opcAdm == 1)
                idoficina = idoficina;
            else
                idoficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);


            //if (oficina_id == 26)
            //{
            //    if (rubro == "T")
            //        rubrofiltro = 50;
            //    else
            //        rubrofiltro = 39;
            //}

            try
            {
                //listar = new BLREGISTRO_CAJA().ReporteRegistroCaja_Cobros(fini, ffin, idoficina, rubrofiltro);
                List<BEComprobanteBancario> listar = new List<BEComprobanteBancario>();
                DateTime FechaI = Convert.ToDateTime(fini);
                DateTime FechaF = Convert.ToDateTime(ffin);

                if (formato == "PDF")
                {
                    listar = new BLReporteComprobantesBancarios().ReporteComprobanteBancario(FechaI, FechaF, idoficina
                                                            , conFechaIngreso, conFechaConfirmacion, finiConfirmacion, ffinConfirmacion
                                                            , estado, con_Rechazo, ini_Rech, fin_Rech, idBanco).ToList();
                }
                else if (formato == "EXCEL")
                {
                    listar = new BLReporteComprobantesBancarios().ReporteComprobanteBancario(FechaI, FechaF, idoficina
                                                            , conFechaIngreso, conFechaConfirmacion, finiConfirmacion, ffinConfirmacion
                                                            , estado, con_Rechazo, ini_Rech, fin_Rech, idBanco).ToList();
                }
                else if (formato == "EXCEL2")
                {
                    listar = new BLReporteComprobantesBancarios().ReporteComprobanteBancario_Excel(FechaI, FechaF, idoficina
                                                            , conFechaIngreso, conFechaConfirmacion, finiConfirmacion, ffinConfirmacion
                                                            , estado, con_Rechazo, ini_Rech, fin_Rech, idBanco).ToList();
                }



                ListaReporteComprobanteBancarioTmp = new List<BEComprobanteBancario>();
                ListaReporteComprobanteBancarioTmp = listar;

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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ReporteTipo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }


}

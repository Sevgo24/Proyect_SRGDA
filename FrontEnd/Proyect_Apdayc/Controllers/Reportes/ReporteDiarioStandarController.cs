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

namespace Proyect_Apdayc.Controllers.Reportes
{
    public class ReporteDiarioStandarController : Base
    {
        //
        // GET: /ReporteDiarioStandar/
        public const string nomAplicacion = "SRGDA";
        private const string K_SESSION_LISTA_REPORTE_DIARIO_CAJA = "___K_SESSION_LISTA_REPORTE_REGISTRO_VENTA ";

        List<BEFacturaCancelada> ConModalidadXOficina = new List<BEFacturaCancelada>();
        List<BEFacturaCancelada> listarModSeg = new List<BEFacturaCancelada>();
        public static string K_SESION_CONSULTA_MODALIDAD_OFICINA_DESTINO = "__ModalidadXOficinaDestinoTmp";
        public static string K_SESION_CONSULTA_MODALIDAD_OFICINA = "__ConsultaModalidadXOficinaTmp";
        public static string parametrosRubro;
        public static string DESC_Modalidad;

        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_DIARIO_CAJA);
            return View();
        }
        #region LISTA_TIPO_COBRO
        public JsonResult LISTA_TIPO_COBRO()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLREGISTRO_CAJA().ListarTipoCobro().Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.VALUE),
                        Text = Convert.ToString(c.VDESC)
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
        #endregion

        #region Temporales
        private List<BEREPORTE_CAJA> ListaReporteDiarioCajaTmp
        {
            get
            {
                return (List<BEREPORTE_CAJA>)Session[K_SESSION_LISTA_REPORTE_DIARIO_CAJA];
            }
            set
            {
                Session[K_SESSION_LISTA_REPORTE_DIARIO_CAJA] = value;
            }
        }

        public List<BEFacturaCancelada> ModalidadXOficinaDestinoTmp
        {
            get
            {
                return (List<BEFacturaCancelada>)Session[K_SESION_CONSULTA_MODALIDAD_OFICINA_DESTINO];
            }
            set
            {
                Session[K_SESION_CONSULTA_MODALIDAD_OFICINA_DESTINO] = value;
            }
        }

        public List<BEFacturaCancelada> ConsultaModalidadXOficinaTmp
        {
            get
            {
                return (List<BEFacturaCancelada>)Session[K_SESION_CONSULTA_MODALIDAD_OFICINA];
            }
            set
            {
                Session[K_SESION_CONSULTA_MODALIDAD_OFICINA] = value;
            }
        }
        #endregion

        //REPORTE-PDF-EXCEL
        public ActionResult ReporteDiarioCaja(string fini, string ffin, string formato, string rubro, string idoficina, string nombreoficina
                                              , int conIng, int conCon, string finiCon, string ffinCon)
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
                List<BEREPORTE_CAJA> listar = new List<BEREPORTE_CAJA>();
                DateTime FechaI = Convert.ToDateTime(fini);
                DateTime FechaF = Convert.ToDateTime(ffin);
                DateTime FechaCobroCorte = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["FECHA_COBRO_INICIO"]);

                if ((FechaI.CompareTo(FechaCobroCorte) == 0 || FechaI.CompareTo(FechaCobroCorte) == 1)
                     && (FechaF.CompareTo(FechaCobroCorte) == 0 || FechaF.CompareTo(FechaCobroCorte) == 1))
                {
                    //listar = new BLREGISTRO_CAJA().ReporteRegistroCaja_Cobros(fini, ffin, idoficina, rubrofiltro);
                    listar = ListaReporteDiarioCajaTmp;
                    Cobro = true;
                }
                else
                {
                    //listar = new BLREGISTRO_CAJA().ReporteRegistroCaja(fini, ffin, idoficina, rubrofiltro);
                    listar = ListaReporteDiarioCajaTmp;
                }

                listar = ListaReporteDiarioCajaTmp;
                if (listar.Count > 0)
                {
                    LocalReport localReport = new LocalReport();

                    if (Cobro)
                        localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_DIARIO_CAJA_COBROS.rdlc");
                    else
                        localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_CAJA.rdlc");

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

                    //ReportParameter parametroFechaIni = new ReportParameter();
                    //parametroFechaIni = new ReportParameter("FechaInicio", conIng == 1 ? fini : "-");
                    //localReport.SetParameters(parametroFechaIni);

                    //ReportParameter parametroFechaFin = new ReportParameter();
                    //parametroFechaFin = new ReportParameter("FechaFin", conIng == 1 ? ffin : "-");
                    //localReport.SetParameters(parametroFechaFin);


                    ReportParameter parametroFecha = new ReportParameter();
                    parametroFecha = new ReportParameter("FechaImpresion", DateTime.Now.ToShortDateString());
                    localReport.SetParameters(parametroFecha);

                    ReportParameter parametroNomusu = new ReportParameter();
                    parametroNomusu = new ReportParameter("NombreUsuario", oficina.Replace("Y", "&"));
                    localReport.SetParameters(parametroNomusu);

                    ReportParameter parametroNomoficina = new ReportParameter();
                    parametroNomoficina = new ReportParameter("NombreOficina", usuario);
                    localReport.SetParameters(parametroNomoficina);

                    //**************
                    if (Cobro)
                    {
                        ReportParameter parametroFechaIniCon = new ReportParameter();
                        parametroFechaIniCon = new ReportParameter("FechaIniCon", conCon == 1 ? finiCon : "-");
                        localReport.SetParameters(parametroFechaIniCon);

                        ReportParameter parametroFechaFinCon = new ReportParameter();
                        parametroFechaFinCon = new ReportParameter("FechaFinCon", conCon == 1 ? ffinCon : "-");
                        localReport.SetParameters(parametroFechaFinCon);

                        ReportParameter parametroRubros = new ReportParameter();
                        parametroRubros = new ReportParameter("Rubros", DESC_Modalidad == null ? "TODAS LOS RUBROS" : DESC_Modalidad);
                        localReport.SetParameters(parametroRubros);
                    }


                    string reportType = format;
                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    //CODIGO REPETIBLE
                    //The DeviceInfo settings should be changed based on the reportType            
                    //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
                    string deviceInfo = "<DeviceInfo>" +
                        "  <OutputFormat>" + format + "</OutputFormat>" +
                        "  <PageWidth>8.5in</PageWidth>" +
                        "  <PageHeight>11in</PageHeight>" +
                        "  <MarginTop>0.3in</MarginTop>" +
                        "  <MarginLeft>0.2in</MarginLeft>" +
                        //"  <MarginRight>0.5in</MarginRight>" +
                        "  <MarginRight>0.2in</MarginRight>" +
                        "  <MarginBottom>0.5in</MarginBottom>" +
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

        //LISTA
        public ActionResult ReporteTipo(string fini, string ffin, string formato, string rubro, string idoficina, string nombreoficina
                                        , int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion
                                        , string TipoCobro)
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_DIARIO_CAJA);
            //Init(false);//add sysseg
            string format = formato;
            int oficina_id = 0;
            int? rubrofiltro = null;

            Resultado retorno = new Resultado();

            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            //if (oficina_id == 10081 || oficina_id == 10082)
            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(oficina_id);
            if (opcAdm == 1)
                idoficina = idoficina;
            else
                idoficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);


            if (idoficina == "26")
            {
                if (rubro == "T")
                    rubrofiltro = 50;
                else
                    rubrofiltro = 39;
            }

            try
            {


                //listar = new BLREGISTRO_CAJA().ReporteRegistroCaja_Cobros(fini, ffin, idoficina, rubrofiltro);

                List<BEREPORTE_CAJA> listar = new List<BEREPORTE_CAJA>();
                DateTime FechaI = Convert.ToDateTime(fini);
                DateTime FechaF = Convert.ToDateTime(ffin);
                DateTime FechaCobroCorte = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["FECHA_COBRO_INICIO"]);

                if ((FechaI.CompareTo(FechaCobroCorte) == 0 || FechaI.CompareTo(FechaCobroCorte) == 1)
                     && (FechaF.CompareTo(FechaCobroCorte) == 0 || FechaF.CompareTo(FechaCobroCorte) == 1))
                    //listar = new BLREGISTRO_CAJA().ReporteRegistroCaja_Cobros(fini, ffin, idoficina, rubrofiltro);
                    listar = new BLREGISTRO_CAJA().ReporteRegistroCaja_Cobros(fini, ffin, idoficina
                        , conFechaIngreso, conFechaConfirmacion, finiConfirmacion, ffinConfirmacion
                        , rubrofiltro, TipoCobro);
                //,parametrosRubro);
                else
                    listar = new BLREGISTRO_CAJA().ReporteRegistroCaja(fini, ffin, idoficina, rubrofiltro);

                ListaReporteDiarioCajaTmp = new List<BEREPORTE_CAJA>();
                ListaReporteDiarioCajaTmp = listar;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Reporte Diario de Caja", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        #region Listar Rubros por Oficina
        public JsonResult ConsultaModalidadXOficina(int IdOficina)
        {
            parametrosRubro = null;
            if (IdOficina == 1)
            {
                IdOficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            }
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    Session.Remove(K_SESSION_LISTA_REPORTE_DIARIO_CAJA);
                    var ModalidadLicOrigen = new BLReporteFacturaCancelada().ListarGrupoModXOficina(IdOficina);

                    if (ModalidadLicOrigen != null)
                    {
                        ConModalidadXOficina = new List<BEFacturaCancelada>();
                        //ModalidadLicOrigen.ForEach(s =>
                        ModalidadLicOrigen.ForEach(s =>
                        {
                            int valEst = 0;
                            if (ModalidadXOficinaDestinoTmp != null)
                                valEst = ModalidadXOficinaDestinoTmp.Where(x => x.MOG_ID == s.MOG_ID).Count();

                            if (valEst == 0)
                            {
                                ConModalidadXOficina.Add(new BEFacturaCancelada
                                {
                                    MOG_ID = s.MOG_ID,
                                    MOG_DESC = s.MOG_DESC
                                });
                            }
                        });
                        ConsultaModalidadXOficinaTmp = ConModalidadXOficina;
                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(ModalidadLicOrigen, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarConsultaModalidadXOficina", ex);
            }
            try
            {
                StringBuilder shtml = new StringBuilder();

                if (ConsultaModalidadXOficinaTmp.Count() > 0)
                {
                    //shtml.Append("<table class='tblModalidadXOficina' border=1 width='65%;' class='k-grid k-widget' id='tblModalidadXOficina'>");
                    shtml.Append("<table class='tblModalidadXOficina' border=0 class='k-grid k-widget' id='tblModalidadXOficina'>");
                    shtml.Append("<thead><tr>");

                }
                if (ConsultaModalidadXOficinaTmp != null)
                {
                    foreach (var item in ConsultaModalidadXOficinaTmp.OrderBy(x => x.MOG_DESC))
                    {
                        //shtml.AppendFormat("<td align='left-center' style='background-color:white;cursor:pointer;text-align:left-center;width=2.5px ';><input type='checkbox' name='Check' value='" + item.MOG_ID + "' class='Check' checked />&nbsp;&nbsp;" + item.MOG_DESC + "</td>");
                        //shtml.AppendFormat("<td align='left-center' style='background-color:white;cursor:pointer;text-align:left-center; width:1%;white-space:nowrap; ';><input type='checkbox' name='Check' value='" + item.MOG_ID + "' class='Check' checked />&nbsp;&nbsp;" + item.MOG_DESC + "</td>");
                        shtml.AppendFormat("<td align='left-center' style='background-color:white;cursor:pointer;text-align:left-center; width:auto;padding-right:35px; ';><input type='checkbox' name='Check' value='" + item.MOG_ID + "' class='Check' checked />&nbsp;&nbsp;" + item.MOG_DESC + "</td>");

                    }
                }
                shtml.Append("</table>");
                retorno.message = shtml.ToString();
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarFactConsulta", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ModalidadesSeleccionadasTemporalesOriginal(List<BEFacturaCancelada> ReglaValor)
        {
            string para = " ";
            string Mod = "";
            string parametro = " ";
            int count = 0;
            int count2 = 0;
            DESC_Modalidad = null;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ReglaValor != null)
                    {
                        listarModSeg = ReglaValor;
                    }
                    else
                    {

                        listarModSeg = new List<BEFacturaCancelada>();
                    }
                    //Lista que recupera el Mog_ID
                    foreach (var item in listarModSeg.OrderBy(x => x.MOG_ID))
                    {
                        para = item.MOG_ID;
                        count += 1;
                        if (count > 1)
                        {
                            parametro = parametro + "," + para;
                        }
                        else
                        {
                            parametro = para;

                        }
                    };
                    // Recupera la descripcion de las modalidades seleccionadas
                    foreach (var item in listarModSeg.OrderBy(x => x.MOG_ID))
                    {
                        foreach (var item2 in ConsultaModalidadXOficinaTmp.Where(x => x.MOG_ID == item.MOG_ID))
                        {
                            Mod = item2.MOG_DESC;
                            count2 += 1;
                            if (count2 > 1)
                            {
                                DESC_Modalidad = DESC_Modalidad + " - " + Mod;
                            }
                            else
                            {
                                DESC_Modalidad = Mod;
                            }
                        };

                    }
                    parametrosRubro = parametro;
                    if (parametrosRubro == " ")
                    {
                        parametrosRubro = null;
                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(ConsultaModalidadXOficinaTmp, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ConsultaModalidadXOficina", ex);
            }


            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}


using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;
using System.Text.RegularExpressions;
using SGRDA.BL.Reporte;
using System.Globalization;
using SGRDA.Entities.Reporte;

namespace Proyect_Apdayc.Controllers.Reportes
{
    public class RegistroVentaController : Base
    {
        //
        // GET: /RegistroVenta/
        private const string K_SESSION_LISTA_REPORTE_REGISTRO_VENTA = "___K_SESSION_LISTA_REPORTE_REGISTRO_VENTA ";
        public static string formatoReporte = "";


        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_REGISTRO_VENTA);
            Init(false);
            return View();

        }

        private List<BERegistroVenta> ListaReporteRegistroVentaTmp
        {
            get
            {
                return (List<BERegistroVenta>)Session[K_SESSION_LISTA_REPORTE_REGISTRO_VENTA];
            }
            set
            {
                Session[K_SESSION_LISTA_REPORTE_REGISTRO_VENTA] = value;
            }
        }

        public ActionResult ReporteRegistroVenta(string fini, string ffin, string formato, string rubro, string idoficina, string nombreoficina)
        {
            string format = formato;
            int oficina_id = 0;
            int? rubrofiltro = null;

            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            Resultado retorno = new Resultado();

            //si oficina es igual a && oficina_id != 26
            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(oficina_id);
            //if (oficina_id == 10081 || oficina_id == 10082)
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

            //if (idoficina == "26")
            //{
            //    if (rubro == "T")
            //    {
            //        rubrofiltro = 50;
            //        oficina = "TELEVISIÓN";
            //    }
            //    else
            //    {
            //        rubrofiltro = 39;
            //        oficina = "CABLE";
            //    }
            //}
            try
            {

                LocalReport localReport = new LocalReport();
                if (opcAdm == 1)
                {

                    if (formatoReporte == "EXCEL")
                    {
                        localReport.ReportPath = Server.MapPath("~/Reportes/RptRegistroVentasContabilidad_Excel.rdlc");
                    }
                    else
                    {
                        localReport.ReportPath = Server.MapPath("~/Reportes/RptRegistroVentasContabilidad.rdlc");
                    }
                }
                else
                {
                    if (formatoReporte == "EXCEL")
                    {
                        localReport.ReportPath = Server.MapPath("~/Reportes/RptRegistroVentas_EXCEL.rdlc");
                    }
                    else
                    {
                        localReport.ReportPath = Server.MapPath("~/Reportes/RptRegistroVentas.rdlc");
                    }

                }
                List<BERegistroVenta> lstReporte = new List<BERegistroVenta>();
                //lstReporte = new BLRegistroVenta().ReporteRegistroVenta(fini, ffin, idoficina, rubrofiltro);
                lstReporte = ListaReporteRegistroVentaTmp;
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

                    ReportParameter parametroNomusu = new ReportParameter();
                    //parametroNomusu = new ReportParameter("NombreUsuario", oficina.Replace("Y", "&").Replace("--SELECCIONE--",""));
                    parametroNomusu = new ReportParameter("NombreUsuario", oficina.Replace("Y", "&").Replace("--SELECCIONE--", "CONTABILIDAD"));
                    localReport.SetParameters(parametroNomusu);

                    ReportParameter parametroNomoficina = new ReportParameter();
                    parametroNomoficina = new ReportParameter("NombreOficina", usuario);
                    localReport.SetParameters(parametroNomoficina);

                    ReportParameter parametroNomRubro = new ReportParameter();
                    parametroNomRubro = new ReportParameter("Rubro", DESC_Modalidad);
                    localReport.SetParameters(parametroNomRubro);


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

                    if (reportType == "EXCEL")
                        renderedBytes = localReport.Render("EXCELOPENXML", deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                    else
                        renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.result = 1;
                    localReport.DisplayName = "Registro de venta";

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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Registro de Venta", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }


        //****************************REPORTE*******************************
        //PARAMETROS FECHA INICIAL,FECHA FINAL,FORMATO DE EL ARCHIV,EL RUBRO Y EL ID DE LA OFICINA , NOMBRE DE LA OFICINA

        public ActionResult ReporteTipo(string fini, string ffin, string rubro, string idoficina, string nombreoficina, string formato)
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_REGISTRO_VENTA);
            Resultado retorno = new Resultado();
            formatoReporte = formato;
            string format = formato;
            int oficina_id = 0;
            int? rubrofiltro = null;

            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(oficina_id);

            //if (oficina_id == 10081 || oficina_id == 10082)
            if (opcAdm == 1)
                idoficina = idoficina;
            else
                idoficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);


            //VALIDA EL TIPO DE OFICINA
            if (idoficina == "26")
            {
                if (rubro == "T")
                    rubrofiltro = 50;
                else
                    rubrofiltro = 39;
            }

            //OBTIENE DATOS PARA GENERAR EL REPORTE
            try
            {
                List<BERegistroVenta> listar = new List<BERegistroVenta>();
                listar = new BLRegistroVenta().ReporteRegistroVenta(fini, ffin, idoficina, rubrofiltro, parametrosRubro);
                ListaReporteRegistroVentaTmp = new List<BERegistroVenta>();
                ListaReporteRegistroVentaTmp = listar;
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


        #region MODALIDAD - FILTRO

        public static string parametrosRubro;
        public static string DESC_Modalidad;
        List<BEFacturaCancelada> listarModSeg = new List<BEFacturaCancelada>();
        private const string K_SESSION_MODALIDAD = "___K_SESSION_REP_PENDIENTE_MODALIDAD";

        //Temporal
        public List<BEModalidad> ConsultaModalidadXOficinaTmp
        {
            get
            {
                return (List<BEModalidad>)Session[K_SESSION_MODALIDAD];
            }
            set
            {
                Session[K_SESSION_MODALIDAD] = value;
            }
        }

        //Obtiene Grupo  Modalidad y las lista commo checkbox
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
                    Session.Remove(K_SESSION_LISTA_REPORTE_REGISTRO_VENTA);
                    //var ModalidadLicOrigen = new BLReporteFacturaCancelada().ListarGrupoModXOficina(IdOficina);
                    var ModalidadLicOrigen = new BLModalidad().ListarGrupoModXOficina(IdOficina);

                    if (ModalidadLicOrigen != null)
                    {
                        ConsultaModalidadXOficinaTmp = ModalidadLicOrigen;
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
                    string TODOS = "TODOS";
                    shtml.Append("<table class='tblModalidadXOficina' border=0 class='k-grid k-widget' id='tblModalidadXOficina'>");
                    shtml.Append("<thead><tr>");
                    shtml.AppendFormat("<td align='left-center' style='background-color:white;text-align:left-center;width=2.5px';>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='checkbox' id='checkALL' name='Check' value='" + TODOS + "' class='select-all' onclick='check1();' checked /> &nbsp;&nbsp;" + TODOS + " &nbsp;&nbsp;&nbsp;&nbsp;</td>");

                    //shtml.AppendFormat("<td align='left-center' style='background-color:white;text-align:left-center;width=2.5px';><input type='checkbox' name='Check' value='" + TODOS + "' class='Check' checked /> &nbsp;&nbsp;" + TODOS + "</td>");
                   
                    if (ConsultaModalidadXOficinaTmp != null)
                    {
                        int Contador = 0;
                        foreach (var item in ConsultaModalidadXOficinaTmp.OrderBy(x => x.MOG_ID))
                        {
                            Contador = Contador + 1;
                            shtml.AppendFormat("<td align='left-center' style='background-color:white;cursor:pointer;text-align:left-center; width:auto;padding-right:15px; ';><input type='checkbox'  id='checkValue" + Contador + "'name='Check' value='" + item.MOG_ID + "' class='Check' onclick='checkValue();' checked />&nbsp;&nbsp;" + item.MOG_DESC + "</td>");
                        }
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
                else
                {
                    retorno.message = shtml.ToString();
                    retorno.result = 0;

                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarFactConsulta", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        // Se obitiene los check seleccionados
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

                        //if (ConsultaModalidadXOficinaTmp != null)
                        //{
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
                        //}

                    }
                    if (para == "0")
                    {
                        DESC_Modalidad = "TODAS LAS MODALIDADES";
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
            parametrosRubro = parametro;
            if (parametrosRubro == " ")
            {
                parametrosRubro = null;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        #endregion
    }
}

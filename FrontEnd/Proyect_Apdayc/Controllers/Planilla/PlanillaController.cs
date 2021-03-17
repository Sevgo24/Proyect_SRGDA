using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases.Factura_Electronica;
using SGRDA.Entities.FacturaElectronica;
using SGRDA.BL.FacturaElectronica;
using Proyect_Apdayc.Clases;
using System.Text;
using System.Text.RegularExpressions;
using SGRDA.BL.Reporte;
using SGRDA.BL.Consulta;
using System.Globalization;
using SGRDA.Utility;
using System.IO;
using PdfSharp.Pdf.IO;
using SGRDA.BL.WorkFlow;
using SGRDA.Documento;

namespace Proyect_Apdayc.Controllers.Planilla
{
    public class PlanillaController : Base
    {
        private const string K_SESSION_LISTA_PLANILLA = "___K_SESSION_LISTA_PLANILLA";

        public const string nomAplicacion = "SRGDA";

        // GET: Planilla
        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_PLANILLA);
            return View();
        }

        private List<BELicenciaReporte> ListaPlanillaTemporal
        {
            get
            {
                return (List<BELicenciaReporte>)Session[K_SESSION_LISTA_PLANILLA];
            }
            set
            {
                Session[K_SESSION_LISTA_PLANILLA] = value;
            }
        }
        

        public JsonResult ObtenerCabecera(decimal ID_OFICINA, decimal ID_DIVISION, string GRUPO_MODALIDAD,
                                                            decimal LIC_ID, decimal ID_SOCIO, DateTime FEC_INI, DateTime FEC_FIN, int ESTADO)
        {

            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    string idoficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                    decimal oficina_final = 0;
                    var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(ID_OFICINA));
                    //if (oficina_id == 10081 || oficina_id == 10082)
                    if (opcAdm == 1)
                    {
                        if(Convert.ToDecimal(idoficina) == 10081 || Convert.ToDecimal(idoficina) == 10082)
                        {
                            oficina_final = 0;
                        }
                        else
                        {
                            oficina_final = ID_OFICINA;
                        }
                            
                    }
                    else
                    {
                        oficina_final = Convert.ToDecimal(idoficina);
                    }

                    List<BELicenciaReporte> ListaCabecera = new List<BELicenciaReporte>();
                    string fecIni = FEC_INI.Year.ToString() + "-" + FEC_INI.Day.ToString() + "-" + FEC_INI.Month.ToString() + " 00:00:00";
                    string fecFin = FEC_FIN.Year.ToString() + "-" + FEC_FIN.Day.ToString() + "-" + FEC_FIN.Month.ToString() + " 23:59:59";
                    ListaCabecera = new BLLicenciaReporte().ListarBandejaPlanilla(oficina_final, ID_DIVISION, GRUPO_MODALIDAD, LIC_ID, ID_SOCIO, fecIni, fecFin, ESTADO);
                    ListaPlanillaTemporal = ListaCabecera;
                    StringBuilder htmlCabecera = new StringBuilder();
                    htmlCabecera = GenerarGrillaCabecera(ListaCabecera);
                    retorno.message = htmlCabecera.ToString();
                    retorno.valor = ListaCabecera.Count
                                      + " - EMITIDAS: " + ListaCabecera.Where(x => x.REPORT_ID != 0).Count()
                                      + " - PENDIENTES: " + ListaCabecera.Where(x => x.REPORT_ID == 0).Count();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerCabecera", ex);
            }
            //return Json(retorno, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public StringBuilder GenerarGrillaCabecera(List<BELicenciaReporte> ListaCabecera)
        {
            StringBuilder shtml = new StringBuilder();
            try
            {

                shtml.Append("<table class='tblFacturaMasiva' border=0 width='100%;' class='k-grid k-widget' id='tblFacturaMasiva'>");
                shtml.Append("<thead><tr>");


                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ID</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ID Licencia</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Licencia</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Grupo Modalidad</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Modalidad</th>");

                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Planilla</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Nro Reporte</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Estado</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Impresion</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Fec. Crea</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Imprimir</th>");
                shtml.Append("</tr>");
                string ConvertLetras="";
                if (ListaCabecera != null)
                {
                    foreach (var item in ListaCabecera.OrderByDescending(x => x.NMR_SERIAL).OrderByDescending(x => x.INV_NUMBER)) //.OrderByDescending(x => x.id))
                    {

                        shtml.Append("<tr style='background-color:white'>");
                        //shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:45px' onclick='return obtenerId({0},{1});' class='IDCell' >{0}</td>", item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});'>{0}</td>", item.TIPO_EMI_DOC, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); //TIPO EMI M o  A
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", item.REPORT_ID);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", item.LIC_ID);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", item.LICENCIA);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", item.GRUPO_MODALIDAD);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", item.MODALIDAD);

                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", item.PLANILLA);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", item.REPORT_NUMBER);
                        if (item.REPORT_ID == 0)
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:left;color:red;' >{0}</td>", "PENDIENTE");
                        else
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:left;color:green' >{0}</td>", "EMITIDA");
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", item.REPORT_NCOPY);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.LOG_DATE_CREAT));
                        //shtml.AppendFormat("<img src='../Images/botones/print.png' onclick='imprimir({0},{1},{2});' height='20' alt='Imprimir Planilla' title='Imprimir Planilla' border=0>&nbsp;&nbsp;", item.REPORT_ID, item.LIC_ID, item.LIC_PL_ID);
                        //ConvertLetras=ConvertirANumero(item.MOG_ID);
                        shtml.AppendFormat("<td'> ");
                        if (item.REPORT_ID != 0)                          
                        shtml.AppendFormat("<td> <img src='../Images/botones/print.png' onclick='imprimir({0},{1},{2},{3});' height='20' alt='Imprimir Planilla' title='Imprimir Planilla' border=0>&nbsp;&nbsp;", item.REPORT_ID, item.LIC_ID, item.LIC_PL_ID,item.MOD_ID);
                        else
                            shtml.AppendFormat("<td></td> ");
                        shtml.AppendFormat("</td>");
                        shtml.Append("<tr>");


                    }
                }
                shtml.Append("</table>");
                //retorno.message = shtml.ToString();
                //retorno.Code = listar.Count;
                //retorno.result = 1;
            }
            catch (Exception ex)
            {
                //retorno.message = ex.Message;
                //retorno.result = 0;
                shtml = null;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerCabecera", ex);
            }
            return shtml;
        }

        public JsonResult GenerarPlanillas(decimal ID_OFICINA, decimal ID_DIVISION, string GRUPO_MODALIDAD,
                                                            decimal LIC_ID, decimal ID_SOCIO, DateTime FEC_INI, DateTime FEC_FIN, int ESTADO)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    List<BELicenciaReporte> Lista = new List<BELicenciaReporte>();
                    string fecIni = FEC_INI.Year.ToString() + "-" + FEC_INI.Day.ToString() + "-" + FEC_INI.Month.ToString() + " 00:00:00";
                    string fecFin = FEC_FIN.Year.ToString() + "-" + FEC_FIN.Day.ToString() + "-" + FEC_FIN.Month.ToString() + " 23:59:59";
                    Lista = new BLLicenciaReporte().ListarBandejaPlanilla(ID_OFICINA, ID_DIVISION, GRUPO_MODALIDAD, LIC_ID, ID_SOCIO, fecIni, fecFin, ESTADO);
                    var PlanillasPendientes = Lista.Where(x => x.REPORT_ID == 0).ToList();

                    int TotalPlanillas = 0;
                    if (PlanillasPendientes.Count > 1)
                        TotalPlanillas = new BLLicenciaReporte().GenerarPLanillasPendientes(PlanillasPendientes, UsuarioActual);
                    retorno.result = 1;
                    retorno.TotalFacturas = TotalPlanillas;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerCabecera", ex);
            }
            //return Json(retorno, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;


        }

        #region Texto a numero
        public string ConvertirANumero(string texto)
        {
            List<string[]> lista = new List<string[]>();
            List<string[]> lista2 = new List<string[]>();
            #region Lista

            string[] a = { "A", "11"  };
            string[] b = { "B", "12" };
            string[] c = { "C", "13" };
            string[] d = { "D", "14" };
            string[] e = { "E", "15" };
            string[] f = { "F", "16" };
            string[] g = { "G", "17" };
            string[] h = { "H", "18" };
            string[] i = { "I", "19" };
            string[] j = { "J", "20" };
            string[] k = { "K", "21" };
            string[] l = { "L", "22" };
            string[] m = { "M", "23" };
            string[] n = { "N", "24" };
            string[] ñ = { "Ñ", "25" };
            string[] o = { "O", "26" };
            string[] p = { "P", "27" };
            string[] q = { "Q", "28" };
            string[] r = { "R", "29" };
            string[] s = { "S", "30" };
            string[] t = { "T", "31" };
            string[] u = { "U", "32" };
            string[] v = { "V", "33" };
            string[] w = { "W", "34" };
            string[] x = { "Y", "35" };
            string[] y = { "X", "36" };
            string[] z = { "Z", "37" };
            lista.Add(a);
            lista.Add(b);
            lista.Add(c);
            lista.Add(d);
            lista.Add(e);
            lista.Add(f);
            lista.Add(g);
            lista.Add(h);
            lista.Add(i);
            lista.Add(j);
            lista.Add(k);
            lista.Add(l);
            lista.Add(m);
            lista.Add(n);
            lista.Add(ñ);
            lista.Add(o);
            lista.Add(p);
            lista.Add(q);
            lista.Add(r);
            lista.Add(s);
            lista.Add(t);
            lista.Add(u);
            lista.Add(v);
            lista.Add(w);
            lista.Add(x);
            lista.Add(y);
            lista.Add(z);
            #endregion
            string letra = "";
            string Concatenar = "";
            
            for (int ii = 0; ii < texto.Length; ii++)
            {
                letra = texto.Substring(ii, 1);
                for(int aa = 0; aa <= 26; aa++)
                {
                    
                    if (lista[aa].Contains(letra))
                    {
                        string Seleccionado = "";
                        Seleccionado = lista[aa][1].ToString();
                        Concatenar += Seleccionado;
                    }
                   
                }
               
            }
            return Concatenar;
        }

        public JsonResult ConvertiraTexto(string Numero)
        {
            Resultado retorno = new Resultado();
            List<string[]> lista = new List<string[]>();
            List<string[]> lista2 = new List<string[]>();
            #region Lista

            string[] a = { "A", "11" };
            string[] b = { "B", "12" };
            string[] c = { "C", "13" };
            string[] d = { "D", "14" };
            string[] e = { "E", "15" };
            string[] f = { "F", "16" };
            string[] g = { "G", "17" };
            string[] h = { "H", "18" };
            string[] i = { "I", "19" };
            string[] j = { "J", "20" };
            string[] k = { "K", "21" };
            string[] l = { "L", "22" };
            string[] m = { "M", "23" };
            string[] n = { "N", "24" };
            string[] ñ = { "Ñ", "25" };
            string[] o = { "O", "26" };
            string[] p = { "P", "27" };
            string[] q = { "Q", "28" };
            string[] r = { "R", "29" };
            string[] s = { "S", "30" };
            string[] t = { "T", "31" };
            string[] u = { "U", "32" };
            string[] v = { "V", "33" };
            string[] w = { "W", "34" };
            string[] x = { "Y", "35" };
            string[] y = { "X", "36" };
            string[] z = { "Z", "37" };
            lista.Add(a);
            lista.Add(b);
            lista.Add(c);
            lista.Add(d);
            lista.Add(e);
            lista.Add(f);
            lista.Add(g);
            lista.Add(h);
            lista.Add(i);
            lista.Add(j);
            lista.Add(k);
            lista.Add(l);
            lista.Add(m);
            lista.Add(n);
            lista.Add(ñ);
            lista.Add(o);
            lista.Add(p);
            lista.Add(q);
            lista.Add(r);
            lista.Add(s);
            lista.Add(t);
            lista.Add(u);
            lista.Add(v);
            lista.Add(w);
            lista.Add(x);
            lista.Add(y);
            lista.Add(z);
            #endregion
            string letra = "";
            string Concatenar = "";
            try { 
            for (int ii = 0; ii < Numero.Length; ii = ii + 2)
            {
                letra = Numero.Substring(ii, 2);
                for (int aa = 0; aa <= 26; aa++)
                {

                    if (lista[aa].Contains(letra))
                    {
                        string Seleccionado = "";
                        Seleccionado = lista[aa][0].ToString();
                        Concatenar += Seleccionado;
                    }

                }

            }
                retorno.result = 1;
                retorno.valor = Concatenar;
            }
              catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerCabecera", ex);
            }
            //return Json(retorno, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion

        public JsonResult ConcatenarPDF()
        {

            Resultado retorno = new Resultado();
            GeneralController general = new GeneralController();            
            var datos = new BLREC_NUMBERING().ObtenerCorrelativoXtipo(GlobalVars.Global.OWNER, "PL");
            string ruta = GlobalVars.Global.RutaPlantillaLicencia;
            string rutaWeb = GlobalVars.Global.RutaPlantillaLicenciaWeb;
            string Path = DateTime.Now.ToString("yyyyMMddHHmmss") + "_Masivo.pdf";
            try
            {
                PdfSharp.Pdf.PdfDocument outPdf = new PdfSharp.Pdf.PdfDocument();
                foreach (var item in ListaPlanillaTemporal.Where(x=>x.REPORT_ID !=0).OrderBy(x=>x.PLANILLA))
                {
                    var id = new BL_WORKF_OBJECTS().ObtenerPlantilla(item.MOD_ID);
                    
                    var report = new BLLicenciaReporte().ObtenerSeriePlanilla(GlobalVars.Global.OWNER, item.LIC_ID, item.REPORT_ID);
                    var PDF = GenerarFormatoPlanillaJson(id, 1, item.LIC_ID, Convert.ToDecimal(report.NMR_ID), item.REPORT_ID);


                    using (PdfSharp.Pdf.PdfDocument one = PdfReader.Open(PDF, PdfDocumentOpenMode.Import))
                    using (outPdf)
                    {
                        CopyPages(one, outPdf);
                        
                    }

                }
                outPdf.Save(ruta + Path);
                retorno.message = rutaWeb + Path;
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
            }
          

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);

            return jsonResult;
        }

        public void CopyPages(PdfSharp.Pdf.PdfDocument from, PdfSharp.Pdf.PdfDocument to)
        {
            for (int i = 0; i < from.PageCount; i++)
            {
                to.AddPage(from.Pages[i]);
            }
        }

        public string GenerarFormatoPlanillaJson(decimal idObj, decimal idTrace, decimal idRef, decimal idSerie, decimal idReportPlanilla)
        {
            Resultado retorno = new Resultado();
            var id = idObj;
            bool ExitoReporte = true;
            string rutaPDF = string.Empty;
            var rutaWebPDF = "";
            try
            {

                if (!isLogout(ref retorno))
                {

                    ViewBag.Error = "";
                    decimal IdLicencia = idRef;
                    var obj = new BL_WORKF_OBJECTS().ObtenerObjects(GlobalVars.Global.OWNER, idObj);

                    if (obj != null)
                    {
                        var idReport = obj.WRKF_OINTID;
                        WordDocumentReport r = new WordDocumentReport();

                        #region "Configurar nombre de archivos"

                        var nameCopyPDF = string.Empty;
                        var nameCopy = string.Format("{1}_{0}", obj.WRKF_OPATH, DateTime.Now.ToString("yyyyMMddHHmmss"));
                        var rutaFile = string.Format("{0}{1}", GlobalVars.Global.RutaPlantillaLicencia, obj.WRKF_OPATH);
                        // var rutaFile = string.Format("{0}{1}",, obj.WRKF_OPATH);
                        var rutaFileCopy = string.Format("{0}{1}", GlobalVars.Global.RutaPlantillaLicencia, nameCopy);
                        if (rutaFileCopy.IndexOf(".docx") != -1)
                        {
                            rutaPDF = rutaFileCopy.Replace(".docx", ".pdf");
                            nameCopyPDF = nameCopy.Replace(".docx", ".pdf");
                        }
                        else
                        {
                            if (rutaFileCopy.IndexOf(".doc") != -1)
                                rutaPDF = rutaFileCopy.Replace(".doc", ".pdf");
                            nameCopyPDF = nameCopy.Replace(".doc", ".pdf");
                        }
                        rutaWebPDF = string.Format("{0}{1}", GlobalVars.Global.RutaPlantillaLicenciaWeb, nameCopyPDF);

                        #endregion

                        #region "generar documento según su código interno"
                        var existOINT = true;
                        string nombre_oficina = Convert.ToString(Session[Constantes.Sesiones.Oficina]);

                        bool existe = System.IO.File.Exists(rutaFile);
                        switch (idReport)
                        {
                            //Planilla de Ejecución
                            case "1010":
                                ExitoReporte = r.CrearReportePlanillaEjecicionStandard(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idSerie, idReportPlanilla, nombre_oficina);
                                break;
                            //case "944": //PLANILLA RADIO - WEBCASTING
                            //    ExitoReporte = r.CrearReportePlanillaEjecicion(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idSerie, idReportPlanilla);
                            //    break;
                            //case "1000": //PLANILLA MEGACONCIERTO
                            //    ExitoReporte = r.CrearReportePlanillaEjecicionMegaconcierto(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idSerie, idReportPlanilla);
                            //    break;

                            //case "1001": //PLANILLA CADENAS
                            //    ExitoReporte = r.CrearReportePlanillaEjecicionCadenas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idSerie, idReportPlanilla);
                            //    break;
                            default:
                                existOINT = false;
                                break;
                        }
                        #endregion
                        var errorGenPlan = false;
                        if (ExitoReporte == false)
                        {
                            errorGenPlan = true;
                            var listmessage = string.Join(", ", GlobalVars.Global.ListMessageReport);
                            //  ViewBag.Error = "No se pudo generar documento. Faltan datos.\n" + listmessage;
                            //  return View();
                            retorno.result = Constantes.MensajeRetorno.ERROR;
                            retorno.message = "No se pudo generar documento. Faltan datos:<br>" + listmessage;
                        }

                        if (errorGenPlan == false) //existOINT &
                        {
                            //ViewBag.Error = "Documento generado y descargando..";
                            //agregar a tab documenos el doc generado
                            
                            //registraDocumentoGenerado(idRef, nameCopyPDF, rutaPDF, obj.DOC_TYPE);
                            retorno.result = Constantes.MensajeRetorno.OK;
                            retorno.message = rutaWebPDF;
                            // return File(rutaWebPDF, "application/pdf");
                        }
                        //else
                        //{
                        //    //ViewBag.Error = "No se pudo generar documento. Código interno  del objeto no fue encontrado.";
                        //    //return View();
                        //    retorno.result = Constantes.MensajeRetorno.ERROR;
                        //    retorno.message = "No se pudo generar documento. Código interno  del objeto no fue encontrado.";
                        //}
                    }
                }
                else
                {
                    //ViewBag.Error = "Código de Objeto no encontrado";
                    //return View();
                    retorno.result = Constantes.MensajeRetorno.ERROR;
                    retorno.message = "Código de Objeto no encontrado";
                }
                //else
                //{
                //    //ViewBag.Error = Constantes.MensajeGenerico.MSG_LOGOUT;
                //    //return View();

                //    retorno.result = Constantes.MensajeRetorno.LOGOUT;
                //    retorno.message = Constantes.MensajeGenerico.MSG_LOGOUT;
                //}
            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "GenerarFormatoPlanilla", ex);
                //ViewBag.Error = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                //return View();
                retorno.result = Constantes.MensajeRetorno.ERROR;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
            }

            return rutaPDF;
        }


        public void registraDocumentoGenerado(decimal codLic, string nombreGenerado, string rutaActual, decimal idTipoDoc)
        {
            Resultado retorno = new Resultado();

            var docGral = new BEDocumentoGral();

            docGral.DOC_ID = 0;
            docGral.OWNER = GlobalVars.Global.OWNER;
            docGral.DOC_TYPE = Convert.ToInt32(idTipoDoc);
            docGral.DOC_PATH = nombreGenerado;
            docGral.DOC_DATE = DateTime.Now;
            docGral.ENT_ID = Convert.ToInt32(Constantes.ENTIDAD.LICENCIAMIENTO);
            docGral.DOC_USER = UsuarioActual;
            docGral.LOG_USER_CREAT = UsuarioActual;
            docGral.DOC_VERSION = 1;

            var objDocLic = new BEDocumentoLic
            {
                LIC_ID = codLic,
                OWNER = docGral.OWNER,
                LOG_USER_CREAT = docGral.LOG_USER_CREAT,
                DOC_ORG = Constantes.OrigenDocumento.SALIDA
            };
            var codigoGenDoc = new BLDocumentoGral().Insertar(docGral, objDocLic);

            var pathDestino = GlobalVars.Global.RutaDocLicSalida;

            //*****************************************pris
            string oldPath = rutaActual.Replace(".pdf", ".docx");
            string newpath = pathDestino;
            string newFileName = nombreGenerado.Replace(".pdf", ".docx");
            FileInfo f1 = new FileInfo(oldPath);
            f1.CopyTo(string.Format("{0}{1}", newpath, newFileName));
            //****************************************************
            string savePath = String.Format("{0}{1}", pathDestino, nombreGenerado);
            FileStream sourceStream = new FileStream(rutaActual, FileMode.Open);
            FileStream targetStream = new FileStream(savePath, FileMode.CreateNew);
            sourceStream.CopyTo(targetStream);
            targetStream.Close();
            sourceStream.Close();

        }

    }                 
}                     
                      
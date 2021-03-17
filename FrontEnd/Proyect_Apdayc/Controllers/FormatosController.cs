using Microsoft.Reporting.WebForms;
using SGRDA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.IO;
using Proyect_Apdayc.Clases;
using SGRDA.Documento;
using SGRDA.BL.WorkFlow;
using System.Web.Configuration;
using Proyect_Apdayc.Clases.DTO;
using SGRDA.BL;
using SGRDA.Utility;
using SGRDA.BL.BLAlfresco;

namespace Proyect_Apdayc.Controllers
{
    public class FormatosController : Base
    {
        //
        // GET: /Formatos/

        public ActionResult Index()
        {
            return View();
        }
        //decimal idObj

        [HttpGet]
        public ActionResult GenerarFormatoPlanilla(decimal idObj, decimal idTrace, decimal idRef, decimal idSerie, decimal idReportPlanilla)
        {
            var id = idObj;
            bool ExitoReporte = true;
            string rutaPDF = string.Empty;
            try
            {
                Resultado retorno = new Resultado();
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
                        var rutaWebPDF = string.Format("{0}{1}", GlobalVars.Global.RutaPlantillaLicenciaWeb, nameCopyPDF);

                        #endregion

                        #region "generar documento según su código interno"
                        var existOINT = true;
                        switch (idReport)
                        {
                            //Planilla de Ejecución
                            case "857":
                                ExitoReporte = r.CrearReportePlanillaEjecicion(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idSerie, idReportPlanilla);
                                break;
                            default:
                                existOINT = false;
                                break;
                        }
                        #endregion
                        //BLAlfresco bl = new BLAlfresco();
                        //#endregion
                        //if (ExitoReporte)
                        //{

                        //    bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 2);
                        //}
                        if (!ExitoReporte)
                        {
                            var listmessage = string.Join(", ", GlobalVars.Global.ListMessageReport);
                            ViewBag.Error = "No se pudo generar documento. Faltan datos.\n" + listmessage;
                            return View();
                        }

                        if (existOINT)
                        {
                            ViewBag.Error = "Documento generado y descargando..";
                            //agregar a tab documenos el doc generado
                            registraDocumentoGenerado(idRef, nameCopyPDF, rutaPDF, obj.DOC_TYPE);
                            return File(rutaWebPDF, "application/pdf");
                        }
                        else
                        {
                            ViewBag.Error = "No se pudo generar documento. Código interno  del objeto no fue encontrado.";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Código de Objeto no encontrado";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error = Constantes.MensajeGenerico.MSG_LOGOUT;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "GenerarFormatoPlanilla", ex);
                ViewBag.Error = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                return View();
            }
        }


        [HttpGet]
        public ActionResult GenerarFormato(decimal idObj, decimal idTrace, decimal idRef)
        {
            var id = idObj;
            bool ExitoReporte = true;
            string rutaPDF = string.Empty;
            try
            {
                Resultado retorno = new Resultado();
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
                        var rutaWebPDF = string.Format("{0}{1}", GlobalVars.Global.RutaPlantillaLicenciaWeb, nameCopyPDF);

                        #endregion

                        #region "generar documento según su código interno"
                        BLAlfresco bl = new BLAlfresco();
                        var existOINT = true;
                        switch (idReport)
                        {
                            case "824":
                                r.CrearReporteCartaSolidarioResponsableLocal(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "836":
                                r.CrearReporteCartaOrganizacionRequemientoAutorizacion(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "837":
                                r.CrearReporteCartaVerficacionUsoInautorizadoObrasMusicales(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "838":
                                r.CrearReporteCartaResponsableSolidarioRegularizarAutorizacion(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "843":
                                r.CrearReporteCartillaInformativaLeyDerechoAutor(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "780":
                                r.CrearReporteCartaInformativaVerificacionObrasMusicales(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "781":
                                r.CrearReporteCartaReiterativaAutorizacionObrasMusicales(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "782":
                                r.CrearReporteDeclaracionJurada(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "779":
                                r.CrearReporteFichaLevantamientoInformacion(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "790":
                                r.CrearReporteAvisoPrejudicial(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "786":
                                r.ContratoMensualLocalPermanente(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;

                            ///rubros radios
                            case "851":
                                r.CrearReporteActualizacionTarifaModelo(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "852":
                                ExitoReporte = r.CrearReporteCartaNotarilalMoroso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "853":
                                ExitoReporte = r.CrearReporteNotificacion72Horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "854":
                                ExitoReporte = r.CrearReporteNotificacion48Horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;
                            case "855":
                                ExitoReporte = r.CrearReporteNotificacion24Horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;

                            ////Declaración jurada ingresos calculo remuneración derechos autor
                            case "856":
                                r.CrearReporteDeclaracionJuradaCalculoRemuneracionDerechosAutor(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;

                            //Planillas de Omisos
                            case "858":
                                ExitoReporte = r.CrearReporteCartaInicioRecuerdoOmiso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "859":
                                ExitoReporte = r.CrearReporteNotificacion72HorasOmiso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;
                            case "148":
                                ExitoReporte = r.CrearReporteNotificacion48HorasOmiso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;
                            case "124":
                                ExitoReporte = r.CrearReporteNotificacion24HorasOmiso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;
                            case "476":
                                ExitoReporte = r.CrearCartaNotarialOmiso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;
                            case "144":
                                ExitoReporte = r.CrearContratoOmiso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;

                            //Plantillas de Morosos
                            case "729":
                                ExitoReporte = r.CrearReporteNotificacion72HorasMoroso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "489":
                                ExitoReporte = r.CrearReporteNotificacion48HorasMoroso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;
                            case "249":
                                ExitoReporte = r.CrearReporteNotificacion24HorasMoroso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;
                            case "871":
                                ExitoReporte = r.CrearCartaNotarialMoroso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;

                            //Plantillas de Omisos radio
                            case "725":
                                ExitoReporte = r.CrearCartaOmisoRadio72Horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "726":
                                ExitoReporte = r.CrearCartaOmisoRadio48Horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;
                            case "827":
                                ExitoReporte = r.CrearCartaOmisoRadio24Horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;
                            case "860":
                                ExitoReporte = r.CrearCartaNotarialRadioOmiso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;

                            //Contrato radio 
                            case "872":
                                ExitoReporte = r.CrearContratoTipoRadioDifusion(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;

                            default:
                                existOINT = false;
                                break;
                        }
                        if (ExitoReporte)
                        {
                            bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                        }
                        #endregion

                        if (!ExitoReporte)
                        {
                            var listmessage = string.Join(", ", GlobalVars.Global.ListMessageReport);
                            var x = new BL_WORKF_ACTIONS().RollBackStateLic(idTrace);
                            ViewBag.Error = "No se pudo generar documento. Faltan datos.\n" + listmessage;
                            return View();
                        }

                        if (existOINT)
                        {
                            ViewBag.Error = "Documento generado y descargando.. ";
                            //agregar a tab documenos el doc generado
                            registraDocumentoGenerado(idRef, nameCopyPDF, rutaPDF, obj.DOC_TYPE);
                            return File(rutaWebPDF, "application/pdf");
                        }
                        else
                        {
                            //en caso no pudo generar el documento el trace registrado se anula
                            var x = new BL_WORKF_ACTIONS().RollBackStateLic(idTrace);
                            ViewBag.Error = "No se pudo generar documento. Código interno  del objeto no fue encontrado.";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Código de Objeto no encontrado";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error = Constantes.MensajeGenerico.MSG_LOGOUT;
                    return View();
                }
            }
            catch (Exception ex)
            {
                //if (idSerie == null && idReportPlanilla == null)
                //{
                //en caso no pudo generar el documento el trace registrado se anula
                var x = new BL_WORKF_ACTIONS().RollBackStateLic(idTrace);
                //}
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "GenerarFormato", ex);
                ViewBag.Error = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                return View();
            }
        }

        private FileContentResult DownloadFormato(LocalReport localReport, string format)
        {

            string reportType = format;
            string mimeType;
            string encoding;
            string fileNameExtension;

            //The DeviceInfo settings should be changed based on the reportType            
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
            string deviceInfo = "<DeviceInfo>  <OutputFormat>" + format + "</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>  <MarginBottom>0.5in</MarginBottom></DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report            
            renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            return File(renderedBytes, mimeType);
        }
        [HttpGet]
        public ActionResult ReporteContratoBailesEspectaculos(decimal idObj)
        {
            string format = "PDF";
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/Formatos/Rdlc/rptContratoBailesEspectaculos.rdlc");
            var socio = new SGRDA.BL.BLProceso().ObtenerSocio(idObj);

            List<ReportParameter> parametros = new List<ReportParameter>();
            parametros.Add(new ReportParameter("Autorizado", socio.BPS_NAME));
            parametros.Add(new ReportParameter("NumDocAut", socio.TAXN_NAME));
            parametros.Add(new ReportParameter("Representante", "EDITH MICAELA ROMERO DE LOS SANTOS"));
            parametros.Add(new ReportParameter("NumDocRep", "45232048"));
            parametros.Add(new ReportParameter("DirRep", "JR.LAS PERAS 218 URB. NARANJAL - INDEPENDENCIA"));
            parametros.Add(new ReportParameter("DistritoRep", "INDEPENDENCIA"));
            parametros.Add(new ReportParameter("Evento", "FIESTA ROMANA"));
            parametros.Add(new ReportParameter("Artistas", "6 VOLTIOS, AMEN, BANDA NI VOZ NI VOTO, CHABELOS, DANIEL F, DIFONIA, EL TRI DE MEXICO, GRUPO PANDA, GRUPO RIO, INYECTORES, JORGE GONZALEZ, LEUSEMIA, LIBIDO, LOS MOJARRAS, MAR DE COPAS, PSICOS "));
            parametros.Add(new ReportParameter("Fechas", "13/12/2014"));
            parametros.Add(new ReportParameter("Local", "PARQUE DE LA EXPOSICION"));
            parametros.Add(new ReportParameter("DireccionEvento", "AV.28 DE JULIO S/N"));
            parametros.Add(new ReportParameter("DistritoEvento", "LIMA"));
            parametros.Add(new ReportParameter("ImporteLong", "20,720.70 (VEINTE MIL SETECIENTOS VEINTE SOLES 70/100 N.S.)"));
            parametros.Add(new ReportParameter("ImporteShort", "(S/. 20,720.70)"));
            localReport.SetParameters(parametros);

            string reportType = format;
            string mimeType;
            string encoding;
            string fileNameExtension;

            //The DeviceInfo settings should be changed based on the reportType            
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
            string deviceInfo = "<DeviceInfo>  <OutputFormat>" + format + "</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>  <MarginBottom>0.5in</MarginBottom></DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report            
            renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            return File(renderedBytes, mimeType);
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
        /// <summary>
        /// GenerarFormatoJson
        /// </summary>
        /// <param name="idObj"></param>
        /// <param name="idTrace"></param>
        /// <param name="idRef"></param>
        /// <returns></returns>
        public JsonResult GenerarFormatoJson(decimal idObj, decimal idTrace, decimal idRef)
        {
            var id = idObj;
            bool ExitoReporte = true;
            string rutaPDF = string.Empty;
            Resultado retorno = new Resultado();
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
                        string tipoPersona = new BLSocioNegocio().ObtenerTipoPersonaXCodigoLic(idRef, GlobalVars.Global.OWNER).ENT_TYPE.ToString();
                        #region "Configurar nombre de archivos"

                        var nameCopyPDF = string.Empty;
                        var nameCopy = string.Format("{1}_{0}", tipoPersona == Constantes.TipoPersona.NATURAL ? obj.WRKF_OPATH : obj.WRKF_OPATH_JURIDICO, DateTime.Now.ToString("yyyyMMddHHmmss"));
                        var rutaFile = string.Format("{0}{1}", GlobalVars.Global.RutaPlantillaLicencia, tipoPersona == Constantes.TipoPersona.NATURAL ? obj.WRKF_OPATH : obj.WRKF_OPATH_JURIDICO);
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
                        var rutaWebPDF = string.Format("{0}{1}", GlobalVars.Global.RutaPlantillaLicenciaWeb, nameCopyPDF);

                        #endregion

                        #region "generar documento según su código interno"
                        BLAlfresco bl = new BLAlfresco();

                        var existOINT = true;
                        switch (idReport)
                        {
                            case "824":
                                r.CrearReporteCartaSolidarioResponsableLocal(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "836":
                                r.CrearReporteCartaOrganizacionRequemientoAutorizacion(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "837":
                                r.CrearReporteCartaVerficacionUsoInautorizadoObrasMusicales(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "838":
                                r.CrearReporteCartaResponsableSolidarioRegularizarAutorizacion(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "843":
                                r.CrearReporteCartillaInformativaLeyDerechoAutor(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "780":
                                r.CrearReporteCartaInformativaVerificacionObrasMusicales(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "781":
                                r.CrearReporteCartaReiterativaAutorizacionObrasMusicales(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "782":
                                r.CrearReporteDeclaracionJurada(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "779":
                                r.CrearReporteFichaLevantamientoInformacion(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "790":
                                r.CrearReporteAvisoPrejudicial(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            ///rubros radios
                            case "851":
                                r.CrearReporteActualizacionTarifaModelo(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                                break;
                            case "852":
                                ExitoReporte = r.CrearReporteCartaNotarilalMoroso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "853":
                                ExitoReporte = r.CrearReporteNotificacion72Horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "854":
                                ExitoReporte = r.CrearReporteNotificacion48Horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;
                            case "855":
                                ExitoReporte = r.CrearReporteNotificacion24Horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;

                            ////Declaración jurada ingresos calculo remuneración derechos autor
                            case "856":
                                r.CrearReporteDeclaracionJuradaCalculoRemuneracionDerechosAutor(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;

                            //Planillas de Omisos
                            case "858":
                                ExitoReporte = r.CrearReporteCartaInicioRecuerdoOmiso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "859":
                                ExitoReporte = r.CrearReporteNotificacion72HorasOmiso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;
                            case "148":
                                ExitoReporte = r.CrearReporteNotificacion48HorasOmiso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;
                            case "124":
                                ExitoReporte = r.CrearReporteNotificacion24HorasOmiso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;
                            case "476":
                                ExitoReporte = r.CrearCartaNotarialOmiso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;
                            case "144":
                                ExitoReporte = r.CrearContratoOmiso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;

                            //Plantillas de Morosos
                            case "729":
                                ExitoReporte = r.CrearReporteNotificacion72HorasMoroso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "489":
                                ExitoReporte = r.CrearReporteNotificacion48HorasMoroso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;
                            case "249":
                                ExitoReporte = r.CrearReporteNotificacion24HorasMoroso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;
                            case "871":
                                ExitoReporte = r.CrearCartaNotarialMoroso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;

                            //Plantillas de Omisos radio
                            case "725":
                                ExitoReporte = r.CrearCartaOmisoRadio72Horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "726"://48 HORAS
                                ExitoReporte = r.CrearCartaOmisoRadio48Horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;
                            case "827":
                                ExitoReporte = r.CrearCartaOmisoRadio24Horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;
                            case "860":
                                ExitoReporte = r.CrearCartaNotarialRadioOmiso(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace);
                                break;

                            //Contrato radio 
                            case "872":
                                ExitoReporte = r.CrearContratoTipoRadioDifusion(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); // 2
                                break;
                            //TV
                            case "901":
                                ExitoReporte = r.CrearCartaInformativa(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace, tipoPersona); // 2
                                break;
                            case "902":
                                ExitoReporte = r.CrearCartaTv72horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace, tipoPersona);    // 2
                                break;
                            case "903":
                                ExitoReporte = r.CrearCartaTv48horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace, tipoPersona); // 2
                                break;
                            case "904":
                                ExitoReporte = r.CrearCartaTv24horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace, tipoPersona); // 2
                                break;
                            case "905":
                                ExitoReporte = r.CrearCartaMorosoTv72H(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); // 2
                                break;
                            case "906":
                                ExitoReporte = r.CrearCartaMorosoTv48H(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); // 2
                                break;
                            case "907":
                                ExitoReporte = r.CrearCartaMorosoTv24H(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); // 2
                                break;
                            case "908":
                                ExitoReporte = r.CrearCartaNotarialProhibicionTv(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); // 2
                                break;
                            case "927":
                                ExitoReporte = r.CrearContratoLicenciaTv(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);  // 2
                                break;
                            //CABLE
                            case "909":
                                ExitoReporte = r.CrearCartaInformativaCable(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace, tipoPersona);
                                break;
                            case "910":
                                ExitoReporte = r.CrearCartaCable72horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace, tipoPersona);
                                break;
                            case "911":
                                ExitoReporte = r.CrearCartaCable48horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace, tipoPersona);
                                break;
                            case "912":
                                ExitoReporte = r.CrearCartaCable24horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace, tipoPersona);
                                break;
                            case "913":
                                ExitoReporte = r.CrearCartaMorosoCable72H(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "914":
                                ExitoReporte = r.CrearCartaMorosoCable48H(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "915":
                                ExitoReporte = r.CrearCartaMorosoCable24H(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "916":
                                ExitoReporte = r.CrearCartaNotarialProhibicionCable(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "926":
                                ExitoReporte = r.CrearContratoLicenciaCable(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            //TV Internet
                            case "917":
                                ExitoReporte = r.CrearCartaTVxInternet72horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace, tipoPersona);
                                break;
                            case "918":
                                ExitoReporte = r.CrearCartaTVxInternet48horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace, tipoPersona);
                                break;
                            case "919":
                                ExitoReporte = r.CrearCartaTVxInternet24horas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace, tipoPersona);
                                break;
                            case "920":
                                ExitoReporte = r.CrearCartaMorosoTVxInternet72H(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "921":
                                ExitoReporte = r.CrearCartaMorosoTVxInternet48H(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "922":
                                ExitoReporte = r.CrearCartaMorosoTVxInternet24H(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "923":
                                ExitoReporte = r.CrearCartaNotarialProhibicionTVxInternet(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "924":
                                ExitoReporte = r.CrearCartaInformativaTVxInternet(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace, tipoPersona);
                                break;
                            case "925":
                                ExitoReporte = r.CrearContratoLicenciaTvInternet(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;

                            //FONOMECANICOS
                            case "928":
                                ExitoReporte = r.CrearPrimeraCartaReqFono(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace, tipoPersona);
                                break;
                            case "929":
                                ExitoReporte = r.CrearSegundaCartaReqFono(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idTrace, tipoPersona);
                                break;
                            case "930":
                                ExitoReporte = r.CrearCartaTransExtrajudicialFono(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "931":
                                ExitoReporte = r.CrearCartaLicenciaFono(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "932":
                                ExitoReporte = r.CrearCartaAcuerdoExtrajudicialFono(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "933":
                                ExitoReporte = r.CrearCartaConvenioExtrajudicialFono(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;

                            //REDES DIGITALES
                            case "934":
                                ExitoReporte = r.CrearPrimeraCartaReqRedes(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "935":
                                ExitoReporte = r.CrearSegundaCartaReqRedes(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "936":
                                ExitoReporte = r.CrearCartaNotarialRedes(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "937":
                                ExitoReporte = r.CrearContratoLicenciaRedes(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "938":
                                ExitoReporte = r.CrearCartaTransExtrajudicialRedes(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;

                            //SINCRONIZACION
                            case "939":
                                ExitoReporte = r.CrearPrimeraCartaReqSinco(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "940":
                                ExitoReporte = r.CrearSegundaCartaReqSincro(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "941":
                                ExitoReporte = r.CrearCartaNotarialSincro(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "942":
                                ExitoReporte = r.CrearContratoLicenciaSincro(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            case "943":
                                ExitoReporte = r.CrearCartaTransExtrajudicialSincro(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);
                                break;
                            //CADENAS
                            case "945":
                                ExitoReporte = r.CrearCartaContratoCadenas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); //? = DUDA
                                break;
                            case "946":
                                ExitoReporte = r.CrearCartillaInformativaCadenas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); //0 = NO
                                break;
                            case "948": // SEMESTRAL LOCAL
                                ExitoReporte = r.ContratoSemestralLocalPermanente(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); //1 = si
                                break;
                            case "949": //SEMESTRAL LOCALES
                                ExitoReporte = r.ContratoSemestralLocalesPermanente(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); //1
                                break;
                            case "950": //Constancia
                                ExitoReporte = r.CrearConstanciaAutorizacionCadenas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); //1
                                break;
                            case "951": //Contrato 
                                ExitoReporte = r.CrearCartaContratoCadenas(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); // 1 
                                break;
                            case "786": //MENSUAL LOCAL
                                ExitoReporte = r.ContratoMensualLocalPermanente(IdLicencia, rutaFile, rutaFileCopy, rutaPDF);  //1
                                break;
                            case "784": // ANUAL LOCAL
                                ExitoReporte = r.ContratoAnualLocalesPermanente(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); //1
                                break;
                            case "785": // MENSUAL LOCALES
                                ExitoReporte = r.ContratoMensualLocalesPermanente(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); //1
                                break;
                            case "952": // TRANSACCION
                                ExitoReporte = r.CrearCartaTransExtrajudicialCadena(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); // 1
                                break;
                            case "953": // CONCILIACION
                                ExitoReporte = r.CrearCartaCnciliacionCadena(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); // 1
                                break;
                            //MEGAOCNCIERTO
                            case "947": //AUTORIZACION
                                ExitoReporte = r.CrearAutorizacionMegaconcierto(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); // 1
                                break;
                            case "954": //CARTA SOLIDARIO RESPONSABLE 
                                ExitoReporte = r.CrearCartaSolidarioResponsable(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); //1
                                break;
                            case "955": //FORMATO DE BAILES Y ESPECTACULOS
                                r.CrearFormatodeBailesEspectaculos(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); //1
                                break;
                            case "956": //DECLARACION JURADA DE BOLETAJE
                                r.CrearDeclaracionJuradadeBoletaje(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); //1
                                break;
                            case "957": //RETENCION SIMPLE
                                ExitoReporte = r.CrearDocumentoRetencionSimple(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); //1
                                break;
                            case "958": //RETENCION SIMPLE
                                ExitoReporte = r.CrearDocumentoRetencionProntoPago(IdLicencia, rutaFile, rutaFileCopy, rutaPDF); //1
                                break;

                            default:
                                existOINT = false;
                                break;
                        }

                        if (ExitoReporte)
                        {
                            bl.Upload_Files_Path(rutaFileCopy, IdLicencia, 1);
                        }
                        #endregion

                        if (!ExitoReporte)
                        {
                            var listmessage = string.Join(", ", GlobalVars.Global.ListMessageReport);
                            var x = new BL_WORKF_ACTIONS().RollBackStateLic(idTrace);
                            ViewBag.Error = "No se pudo generar documento. Faltan datos.\n" + listmessage;
                            //return View();
                            retorno.result = Constantes.MensajeRetorno.ERROR;
                            retorno.message = "No se pudo generar documento. Faltan datos:<br>" + listmessage;
                        }
                        else
                        {
                            if (ExitoReporte && existOINT)
                            {
                                ViewBag.Error = "Documento generado y descargando.. ";
                                //agregar a tab documenos el doc generado
                                registraDocumentoGenerado(idRef, nameCopyPDF, rutaPDF, obj.DOC_TYPE);
                                retorno.result = Constantes.MensajeRetorno.OK;
                                retorno.message = rutaWebPDF;
                                //return File(rutaWebPDF, "application/pdf");
                            }
                            else
                            {
                                //en caso no pudo generar el documento el trace registrado se anula
                                var x = new BL_WORKF_ACTIONS().RollBackStateLic(idTrace);
                                ViewBag.Error = "No se pudo generar documento. Código interno  del objeto no fue encontrado.";
                                // return View();
                                retorno.result = Constantes.MensajeRetorno.ERROR;
                                retorno.message = "No se pudo generar documento. Código interno  del objeto no fue encontrado.";
                            }
                        }

                    }
                    else
                    {
                        var x = new BL_WORKF_ACTIONS().RollBackStateLic(idTrace);
                        ViewBag.Error = "Código de Objeto no encontrado";
                        //return View();
                        retorno.result = Constantes.MensajeRetorno.ERROR;
                        retorno.message = "Código de Objeto no encontrado";
                    }
                }
                else
                {
                    ViewBag.Error = Constantes.MensajeGenerico.MSG_LOGOUT;
                    var x = new BL_WORKF_ACTIONS().RollBackStateLic(idTrace);

                    retorno.result = Constantes.MensajeRetorno.LOGOUT;
                    retorno.message = Constantes.MensajeGenerico.MSG_LOGOUT;
                    // return View();
                }

            }
            catch (Exception ex)
            {
                //if (idSerie == null && idReportPlanilla == null)
                //{
                //en caso no pudo generar el documento el trace registrado se anula
                var x = new BL_WORKF_ACTIONS().RollBackStateLic(idTrace);
                //}
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "GenerarFormatoJSON", ex);
                ViewBag.Error = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;

                retorno.result = Constantes.MensajeRetorno.ERROR;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                //return View();
            }


            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idObj"></param>
        /// <param name="idTrace"></param>
        /// <param name="idRef"></param>
        /// <param name="idSerie"></param>
        /// <param name="idReportPlanilla"></param>
        /// <returns></returns>
        public JsonResult GenerarFormatoPlanillaJson(decimal idObj, decimal idTrace, decimal idRef, decimal idSerie, decimal idReportPlanilla)
        {
            Resultado retorno = new Resultado();
            var id = idObj;
            bool ExitoReporte = true;
            string rutaPDF = string.Empty;
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
                        var rutaWebPDF = string.Format("{0}{1}", GlobalVars.Global.RutaPlantillaLicenciaWeb, nameCopyPDF);

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
                            registraDocumentoGenerado(idRef, nameCopyPDF, rutaPDF, obj.DOC_TYPE);
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

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GenerarAutorizacionJson(decimal idRef, decimal idSerie, decimal idReportPlanilla)
        {
            Resultado retorno = new Resultado();
            var idObj = 20160; //20160 
            bool ExitoReporte = true;
            string rutaPDF = string.Empty;
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
                        var rutaWebPDF = string.Format("{0}{1}", GlobalVars.Global.RutaPlantillaLicenciaWeb, nameCopyPDF);

                        #endregion

                        #region "generar documento según su código interno"
                        var existOINT = true;
                        switch (idReport)
                        {
                            case "947": //AUTORIZACION
                                ExitoReporte = r.CrearReportePlanillaEjecicion(IdLicencia, rutaFile, rutaFileCopy, rutaPDF, idSerie, idReportPlanilla);
                                break;

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
                            registraDocumentoGenerado(idRef, nameCopyPDF, rutaPDF, obj.DOC_TYPE);
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
                    else
                    {
                        //ViewBag.Error = "Código de Objeto no encontrado";
                        //return View();
                        retorno.result = Constantes.MensajeRetorno.ERROR;
                        retorno.message = "Código de Objeto no encontrado";
                    }
                }
                else
                {
                    //ViewBag.Error = Constantes.MensajeGenerico.MSG_LOGOUT;
                    //return View();

                    retorno.result = Constantes.MensajeRetorno.LOGOUT;
                    retorno.message = Constantes.MensajeGenerico.MSG_LOGOUT;
                }
            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "GenerarFormatoPlanilla", ex);
                //ViewBag.Error = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                //return View();
                retorno.result = Constantes.MensajeRetorno.ERROR;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

using Proyect_Apdayc.Clases;
using SGRDA.BL;
using SGRDA.Entities;
using SGRDA.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Proyect_Apdayc.Controllers.Upload
{
    public class FileUploadTracerController : ApiController
    {
      
        [HttpPost]
        public KeyValuePair<bool, string> UploadTabProcesoLicencia()
        {
            try
            {
                var rutaUpload = GlobalVars.Global.RutaDocLicEntrada;
                return Upload(rutaUpload);
            }
            catch (Exception ex)
            {
                var currentUser = HttpContext.Current.Request.Form["hidUser"] == null ? "Unknow" : HttpContext.Current.Request.Form["hidUser"];
                return new KeyValuePair<bool, string>(false, "An error occurred while uploading the file. Error Message: " + ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", currentUser, "Upload tracer", ex);
            }
        }

        private KeyValuePair<bool, string> Upload(string rutaUpload)
        {
            if (Directory.Exists(rutaUpload))
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var codigoDoc = HttpContext.Current.Request.Form["hidKey"];
                    var idLicencia = HttpContext.Current.Request.Form["hidIdLic"]; //data.append("hidIdLic", documento.codLic);
                    var idDoc = HttpContext.Current.Request.Form["hidIdDoc"]; //data.append("hidIdDoc", documento.id);
                    var idTipoDoc = HttpContext.Current.Request.Form["hidTipoDOc"]; //data.append("hidTipoDOc", documento.TipoDocumento);
                    var Archivo = HttpContext.Current.Request.Form["hidArchivo"]; //data.append("hidArchivo", documento.Archivo);
                    var currentUser = HttpContext.Current.Request.Form["hidUser"];

                 
                    string nombreGenerado = "";
                    //string currentUser = Convert.ToString(HttpContext.Current.Session[Constantes.Sesiones.Usuario]).ToUpper();
                    if (codigoDoc != null && idLicencia != null && idDoc != null && idTipoDoc != null && Archivo != null && currentUser!=null)
                    {
                        // Get the uploaded image from the Files collection
                        var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];

                        if (httpPostedFile != null)
                        {
                            if (httpPostedFile.ContentLength <= GlobalVars.Global.SizeFileUpload)
                            {
                                // Validate the uploaded image(optional)
                                // Get the complete file path

                                var resultado = Convert.ToInt32(codigoDoc);
                                var fec = DateTime.Now.ToString("yyMMddHHmmss");
                                var guid = Guid.NewGuid().ToString();

                                if (codigoDoc != null)
                                {
                                    var name = Util.CleanInputB(httpPostedFile.FileName);
                                    nombreGenerado = String.Format("{0}_{1}_{2}_{3}", fec, resultado, guid, name);
                                    var fileSavePath = rutaUpload + nombreGenerado;
                                    httpPostedFile.SaveAs(fileSavePath);

                                    var exitoUp = true;
                                    decimal codigoDocGenerado = 0;
                                    /// Luego de subir el archivo actualizar el documento en BD
                                    exitoUp = addDocumento(rutaUpload, idLicencia, idDoc, idTipoDoc, nombreGenerado, currentUser, exitoUp, out codigoDocGenerado);
                                    if (exitoUp)
                                    {
                                        return new KeyValuePair<bool, string>(true, Convert.ToString(codigoDocGenerado));
                                    }
                                    else {
                                        return new KeyValuePair<bool, string>(false,"Ocurrió un error mientras actualizaba el documento.");
                                    }
                                }
                            }
                            else
                            {
                                return new KeyValuePair<bool, string>(false, string.Format("El archivo excede el límite permitido. Límite {0} MB", (GlobalVars.Global.SizeFileUpload / 1024)));
                            }
                        }

                        return new KeyValuePair<bool, string>(false, "Could not get the uploaded file.");
                    }

                    return new KeyValuePair<bool, string>(false, "No file found to upload.");
                }
                else {
                    return new KeyValuePair<bool, string>(false, "Parámetros esperados no fueron enviados.");
                }
            }
            else {
                return new KeyValuePair<bool, string>(false, "No directory found to upload.");
            
            }
        }

        private static bool addDocumento(string rutaUpload, string idLicencia, string idDoc, string idTipoDoc, string nombreGenerado, string currentUser, bool exitoUp, out decimal codigoGenDoc)
        {
            codigoGenDoc = 0;
            try
            {
                var docGral = new BEDocumentoGral();
                docGral.DOC_ID = Convert.ToDecimal(idDoc);
                docGral.OWNER = GlobalVars.Global.OWNER;
                docGral.DOC_TYPE = Convert.ToInt32(idTipoDoc);
                docGral.DOC_PATH = nombreGenerado;
                docGral.DOC_DATE = DateTime.Now;
                docGral.ENT_ID = Convert.ToInt32(Constantes.ENTIDAD.LICENCIAMIENTO);
                docGral.DOC_USER = currentUser;
                docGral.LOG_USER_CREAT = currentUser;
                docGral.DOC_VERSION = 1;

                  codigoGenDoc = new BLDocumentoGral().Insertar(docGral, new BEDocumentoLic
                {
                    LIC_ID = Convert.ToDecimal(idLicencia),
                    OWNER = docGral.OWNER,
                    LOG_USER_CREAT = docGral.LOG_USER_CREAT,
                    DOC_ORG = Constantes.OrigenDocumento.ENTRADA
                });
            }
            catch (Exception ex)
            {
                exitoUp = false;
                var path = rutaUpload;
                string savePath = String.Format("{0}{1}", path, nombreGenerado);
                if (System.IO.File.Exists(savePath))
                {
                    System.IO.File.Delete(savePath);
                }
                ucLogApp.ucLog.GrabarLogError("SGRDA", currentUser, "Upload tracer", ex);
            }
            return exitoUp;
        }
        //public string CleanInput(string strIn)
        //{
        //    // Replace invalid characters with empty strings.
        //    try
        //    {
        //        var cadena = System.Text.RegularExpressions.Regex.Replace(strIn, @"[^\w\.@-]", "",
        //                             System.Text.RegularExpressions.RegexOptions.None, TimeSpan.FromSeconds(1.5));

        //        cadena = cadena.Replace("á", "a");
        //        cadena = cadena.Replace("é", "e");
        //        cadena = cadena.Replace("í", "i");
        //        cadena = cadena.Replace("ó", "o");
        //        cadena = cadena.Replace("ú", "u");
        //        cadena = cadena.Replace("Á", "A");
        //        cadena = cadena.Replace("É", "E");
        //        cadena = cadena.Replace("Í", "I");
        //        cadena = cadena.Replace("Ó", "O");
        //        cadena = cadena.Replace("Ú", "U");
        //        cadena = cadena.Replace("ñ", "n");
        //        cadena = cadena.Replace("Ñ", "N");
        //        cadena = cadena.Replace("(", "");
        //        cadena = cadena.Replace(")", "");
        //        cadena = cadena.Replace(" ", "_");
        //        cadena = cadena.ToLower();
        //        return cadena;
        //    }
        //    // If we timeout when replacing invalid characters, 
        //    // we should return Empty.
        //    catch (System.Text.RegularExpressions.RegexMatchTimeoutException)
        //    {
        //        return String.Empty;
        //    }
        //}
    }
}
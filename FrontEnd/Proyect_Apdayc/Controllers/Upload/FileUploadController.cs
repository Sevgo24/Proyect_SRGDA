using SGRDA.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Proyect_Apdayc.Controllers.Upload
{
    public class FileUploadController : ApiController
    {
        //[HttpPost]
        //public KeyValuePair<bool, string> UploadFile()
        //{
        //    try
        //    {
        //        var rutaUpload = System.Web.Configuration.WebConfigurationManager.AppSettings["RutaFisicaImgLicenciaDoc"];
        //        return Upload(rutaUpload);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new KeyValuePair<bool, string>(false, "An error occurred while uploading the file. Error Message: " + ex.Message);
        //    }
        //}
        //[HttpPost]
        //public KeyValuePair<bool, string> UploadTabDocSocio()
        //{
        //    try
        //    {
        //        var rutaUpload = GlobalVars.Global.RutaTabDocumentoSocio;
        //        return Upload(rutaUpload);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new KeyValuePair<bool, string>(false, "An error occurred while uploading the file. Error Message: " + ex.Message);
        //    }
        //}
        //[HttpPost]
        //public KeyValuePair<bool, string> UploadTabDocProveedor()
        //{
        //    try
        //    {
        //        var rutaUpload = GlobalVars.Global.RutaTabDocumentoProveedor;
        //        return Upload(rutaUpload);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new KeyValuePair<bool, string>(false, "An error occurred while uploading the file. Error Message: " + ex.Message);
        //    }
        //}
        //[HttpPost]
        //public KeyValuePair<bool, string> UploadTabDocEmpleado()
        //{
        //    try
        //    {
        //        var rutaUpload = GlobalVars.Global.RutaTabDocumentoEmpleado;
        //        return Upload(rutaUpload);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new KeyValuePair<bool, string>(false, "An error occurred while uploading the file. Error Message: " + ex.Message);
        //    }
        //}
        //[HttpPost]
        //public KeyValuePair<bool, string> UploadTabDocAsociacion()
        //{
        //    try
        //    {
        //        var rutaUpload = GlobalVars.Global.RutaTabDocumentoAsociacion;
        //        return Upload(rutaUpload);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new KeyValuePair<bool, string>(false, "An error occurred while uploading the file. Error Message: " + ex.Message);
        //    }
        //}
        //[HttpPost]
        //public KeyValuePair<bool, string> UploadTabDocRecaudador()
        //{
        //    try
        //    {
        //        var rutaUpload = GlobalVars.Global.RutaTabDocumentoRecaudador;
        //        return Upload(rutaUpload);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new KeyValuePair<bool, string>(false, "An error occurred while uploading the file. Error Message: " + ex.Message);
        //    }
        //}
        //[HttpPost]
        //public KeyValuePair<bool, string> UploadTabDocDerecho()
        //{
        //    try
        //    {
        //        var rutaUpload = GlobalVars.Global.RutaTabDocumentoDerecho;
        //        return Upload(rutaUpload);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new KeyValuePair<bool, string>(false, "An error occurred while uploading the file. Error Message: " + ex.Message);
        //    }
        //}
        [HttpPost]
        public KeyValuePair<bool, string> UploadTabLicencia()
        {
            try
            {
                var rutaUpload = GlobalVars.Global.RutaTabDocumentoLic;
                return Upload(rutaUpload);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, "An error occurred while uploading the file. Error Message: " + ex.Message);
            }
        }

        private KeyValuePair<bool, string> Upload(string rutaUpload)
        {
            //rutaUpload = "\\\\192.168.252.105\\Documentos\\Entrada\\";
            if (Directory.Exists(rutaUpload))
            {

                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {

                    // Get the uploaded image from the Files collection
                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];

                    if (httpPostedFile != null)
                    {
                        if (httpPostedFile.ContentLength <= GlobalVars.Global.SizeFileUpload)
                        {
                            // Validate the uploaded image(optional)
                            // Get the complete file path
                            var codigoDoc = HttpContext.Current.Request.Form["hidKey"];
                            var resultado = Convert.ToInt32(codigoDoc);
                            var fec = DateTime.Now.ToString("yyMMddHHmmss");
                            var guid = Guid.NewGuid().ToString();

                            if (codigoDoc != null)
                            {
                                var name =Util.CleanInputB(httpPostedFile.FileName);
                                var nombreGenerado = String.Format("{0}_{1}_{2}_{3}", fec, resultado, guid, name);
                                var fileSavePath = rutaUpload + nombreGenerado;
                                httpPostedFile.SaveAs(fileSavePath);
                                return new KeyValuePair<bool, string>(true, nombreGenerado);
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
                return new KeyValuePair<bool, string>(false, "No directory found to upload.");
            
            }
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
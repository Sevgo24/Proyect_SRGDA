using Proyect_Apdayc.Clases;
using SGRDA.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Proyect_Apdayc.Controllers.Upload
{
    public class FileUploadProveedorController : ApiController
    {
        [HttpPost]
        public KeyValuePair<bool, string> UploadTabDocProveedor()
        {
            try
            {
                var rutaUpload = GlobalVars.Global.RutaTabDocumentoProveedor;
                return Upload(rutaUpload);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, "An error occurred while uploading the file. Error Message: " + ex.Message);
            }
        }
        private KeyValuePair<bool, string> Upload(string rutaUpload)
        {

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
                                var name = Util.CleanInputB(httpPostedFile.FileName);
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
            else
            {
                return new KeyValuePair<bool, string>(false, "No directory found to upload.");

            }
        }
    }
}

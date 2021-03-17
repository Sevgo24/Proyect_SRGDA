using Proyect_Apdayc.Clases;
using SGRDA.BL;
using SGRDA.BL.WorkFlow;
using SGRDA.Entities;
using SGRDA.Entities.WorkFlow;
using SGRDA.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;



namespace Proyect_Apdayc.Controllers.Upload
{
    public class FileUploadObjetoController : ApiController
    {

        [HttpPost]
        public KeyValuePair<bool, string> UploadDocumentoObjeto()
        {
            try
            {
                var rutaUpload = GlobalVars.Global.RutaTabDocumentoObjeto;
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
                    var idObjeto = HttpContext.Current.Request.Form["hidCodigo"]; //data.append("hidIdLic", documento.codLic);
                    var idCodigoInterno = HttpContext.Current.Request.Form["hidCodigoInterno"]; //data.append("hidIdDoc", documento.id);
                    var Descripcion = HttpContext.Current.Request.Form["hidDescripcion"]; //data.append("hidTipoDOc", documento.TipoDocumento);
                    var Tipo = HttpContext.Current.Request.Form["hidTipo"]; //data.append("hidArchivo", documento.Archivo);
                    var Archivo = HttpContext.Current.Request.Form["hidArchivo"]; //data.append("hidArchivo", documento.Archivo);
                    var Asunto = HttpContext.Current.Request.Form["hidAsunto"]; //data.append("hidArchivo", documento.Archivo);
                    var currentUser = HttpContext.Current.Request.Form["hidUser"];


                    string nombreGenerado = "";
                    //string currentUser = Convert.ToString(HttpContext.Current.Session[Constantes.Sesiones.Usuario]).ToUpper();
                    if (codigoDoc != null && idObjeto != null && idCodigoInterno != null && Descripcion != null &&
                        Tipo != null && Archivo != null && Tipo != null && currentUser != null)
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
                                //var guid = Guid.NewGuid().ToString();

                                if (codigoDoc != null)
                                {
                                    var name = Util.CleanInputB(httpPostedFile.FileName);
                                    nombreGenerado = String.Format("{0}_{1}_{2}", fec, resultado,  name);
                                    var fileSavePath = rutaUpload + nombreGenerado;
                                    httpPostedFile.SaveAs(fileSavePath);

                                    var exitoUp = true;
                                    decimal codigoDocGenerado = 0;
                                    /// Luego de subir el archivo actualizar el documento en BD
                                    exitoUp = addDocumento(rutaUpload, idObjeto, idCodigoInterno, Descripcion, Tipo, Asunto,
                                        nombreGenerado, currentUser, exitoUp, out codigoDocGenerado);
                                    if (exitoUp)
                                    {
                                        return new KeyValuePair<bool, string>(true, Convert.ToString(codigoDocGenerado));
                                    }
                                    else
                                    {
                                        return new KeyValuePair<bool, string>(false, "Ocurrió un error mientras actualizaba el documento.");
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
                else
                {
                    //return new KeyValuePair<bool, string>(false, "Parámetros esperados no fueron enviados.");
                    return new KeyValuePair<bool, string>(false, "Adjunte un documento antes de grabar.");
                }
            }
            else
            {
                return new KeyValuePair<bool, string>(false, "No directory found to upload.");

            }
        }

        private static bool addDocumento(string rutaUpload, string idObjeto, string idCodigoInterno, string Descripcion, string Tipo, string Asunto,
                                        string nombreGenerado, string currentUser, bool exitoUp, out decimal codigoGenDoc)
        {
            codigoGenDoc = 0;
            try
            {
                var entObj = new WORKF_OBJECTS();
                entObj.OWNER = GlobalVars.Global.OWNER;
                entObj.WRKF_OID=Convert.ToDecimal( idObjeto);
                entObj.WRKF_OINTID= idCodigoInterno;
                entObj.WRKF_ODESC= Descripcion;
                entObj.WRKF_OTID =Convert.ToDecimal( Tipo);
                entObj.WRKF_OPATH = nombreGenerado;
                entObj.WRKF_OSUBJECT = Asunto;
                entObj.LOG_USER_CREAT = currentUser;

                if (entObj.WRKF_OID == 0)
                {
                    entObj.LOG_USER_CREAT = currentUser;
                    var datos = new BL_WORKF_OBJECTS().Insertar(entObj);
                }
                else
                {
                    entObj.LOG_USER_UPDATE = currentUser;
                    var datos = new BL_WORKF_OBJECTS().Actualizar(entObj);
                }

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
                ucLogApp.ucLog.GrabarLogError("SGRDA", currentUser, "addDocumento Objeto", ex);
            }
            return exitoUp;
        }

        #region Codigo_Antiguo
        //
        // GET: /FileUploadOficina/

        //[HttpPost]
        //public KeyValuePair<bool, string> UploadTabDocObjeto()
        //{
        //    try
        //    {
        //        var rutaUpload = GlobalVars.Global.RutaTabDocumentoObjeto;
        //        return Upload(rutaUpload);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new KeyValuePair<bool, string>(false, "An error occurred while uploading the file. Error Message: " + ex.Message);
        //    }
        //}

        //private KeyValuePair<bool, string> Upload(string rutaUpload)
        //{

        //    if (Directory.Exists(rutaUpload))
        //    {

        //        if (HttpContext.Current.Request.Files.AllKeys.Any())
        //        {

        //            // Get the uploaded image from the Files collection
        //            var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];

        //            if (httpPostedFile != null)
        //            {
        //                if (httpPostedFile.ContentLength <= GlobalVars.Global.SizeFileUpload)
        //                {
        //                    // Validate the uploaded image(optional)
        //                    // Get the complete file path
        //                    var codigoDoc = HttpContext.Current.Request.Form["hidKey"];
        //                    var resultado = Convert.ToInt32(codigoDoc);
        //                    var fec = DateTime.Now.ToString("yyMMddHHmmss");
        //                    var guid = Guid.NewGuid().ToString();

        //                    if (codigoDoc != null)
        //                    {
        //                        var name = Util.CleanInputB(httpPostedFile.FileName);
        //                        var nombreGenerado = String.Format("{0}_{1}_{2}_{3}", fec, resultado, guid, name);
        //                        var fileSavePath = rutaUpload + nombreGenerado;
        //                        httpPostedFile.SaveAs(fileSavePath);
        //                        return new KeyValuePair<bool, string>(true, nombreGenerado);
        //                    }
        //                }
        //                else
        //                {
        //                    return new KeyValuePair<bool, string>(false, string.Format("El archivo excede el límite permitido. Límite {0} MB", (GlobalVars.Global.SizeFileUpload / 1024)));
        //                }
        //            }

        //            return new KeyValuePair<bool, string>(false, "Could not get the uploaded file.");
        //        }

        //        return new KeyValuePair<bool, string>(false, "No file found to upload.");
        //    }
        //    else
        //    {
        //        return new KeyValuePair<bool, string>(false, "No directory found to upload.");

        //    }
        //}
        #endregion
    }
}

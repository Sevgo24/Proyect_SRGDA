using DotCMIS;
using DotCMIS.Client;
using DotCMIS.Client.Impl;
using DotCMIS.Data.Impl;
using SGRDA.BL.BLAlfresco;
using SGRDA.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Proyect_Apdayc.Controllers.Upload
{
    public class FileUploadAlfrescoController : ApiController
    {
        // GET: FileUploadAlfresco
        [HttpPost]
        public KeyValuePair<bool, string> UploadTabAlfresco()
        {
            try
            {
                var rutaUploadAlfresco = GlobalVars.Global.rutaUploadAlfresco;
                return UploadAlfresco(rutaUploadAlfresco);
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, "An error occurred while uploading the file. Error Message: " + ex.Message);
            }
        }
        private KeyValuePair<bool, string> UploadAlfresco(string rutaUploadAlfresco)
        {

            //if (GlobalVars.Global.ActivarAlfresco_Cobros == "T")
            //{
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                    var CodigoCarpetaAlfresco = HttpContext.Current.Request.Form["CodigoCarpeta"];
                    var CodigoLic = HttpContext.Current.Request.Form["CodigoLic"];
                    var CodigoTipoDoc = HttpContext.Current.Request.Form["CodigoTipoDoc"];
                    var Artist_ID = HttpContext.Current.Request.Form["Artist_ID"];
                    if (HttpContext.Current.Request.Files.AllKeys.Any())
                    {
                        //var lista1 = new BLAlfresco().Listar_PropiedadesAlfresco_x_TipoDocumento(Convert.ToInt32(CodigoTipoDoc));
                        var listaAlfresco = new BLAlfresco().Listar_DatosDocumento_x_Licencia_Y_TipoDocumento(Convert.ToInt32(CodigoTipoDoc), Convert.ToInt32(CodigoLic), Convert.ToInt32(Artist_ID));
                        var Object_id = new BLAlfresco().Object_TypeId(Convert.ToInt32(CodigoTipoDoc));
                        // Get the uploaded image from the Files collection
                        //httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];

                        StreamReader objLeerArchivo = new StreamReader(httpPostedFile.InputStream);
                        byte[] data;
                        using (objLeerArchivo)
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                objLeerArchivo.BaseStream.CopyTo(ms);
                                data = ms.ToArray();
                            }
                        }

                        if (httpPostedFile != null)
                        {
                            //if (httpPostedFile.ContentLength <= GlobalVars.Global.SizeFileUpload)
                            //{
                            // Validate the uploaded image(optional)
                            // Get the complete file path

                            //var url = "http://192.168.252.94:8080/alfresco/api/-default-/cmis/versions/1.0/atom";
                            var name = Util.CleanInputB(httpPostedFile.FileName).Replace(".docx", ".doc");
                            var UsuarioAlfresco = GlobalVars.Global.UsuarioAlfresco;
                            var ContraseñaAlfreso = GlobalVars.Global.ContraseñaAlfreso;

                            //Subir Archivos Alfresco -------------

                            try
                            {
                                //Conexion Alfresco
                                Dictionary<string, string> parameters = new Dictionary<string, string>();
                                parameters[SessionParameter.BindingType] = BindingType.AtomPub;
                                parameters[SessionParameter.AtomPubUrl] = rutaUploadAlfresco;
                                parameters[SessionParameter.User] = UsuarioAlfresco;
                                parameters[SessionParameter.Password] = ContraseñaAlfreso;
                                //parameters[SessionParameter.RepositoryId] = "-default-";
                                SessionFactory factory = SessionFactory.NewInstance();
                                //ISession session = factory.CreateSession(parameters);
                                ISession session = factory.GetRepositories(parameters)[0].CreateSession();
                                var a = session.DefaultContext;

                            #region PruebaMasiva                           
                            //PRUEBA MASIVA
                            //string queryGetFolder = "SELECT * FROM cmis:folder WHERE cmis:name='Documento_Lyrics'";
                            //IItemEnumerable<IQueryResult> docResults = session.Query(queryGetFolder, false);
                            //IQueryResult folderHit = docResults.FirstOrDefault();
                            //string folderId = "";
                            //folderId = folderHit["cmis:objectId"].FirstValue.ToString();
                            //IFolder folder = (IFolder)session.GetObject(folderId);

                            //int numero = 1000;
                            //int numero2 =2000;
                            ////string nombre = "name";
                            //for (int i = numero; i < numero2; i++)
                            //{
                            //    IDictionary<string, object> properties = new Dictionary<string, object>();
                            //    properties[PropertyIds.Name] = "_" + Convert.ToString(i)+ name;
                            //    properties[PropertyIds.ObjectTypeId] = "D:oyd:boletas_obras";
                            //    properties["oyd:codigo_int_socio"] = Convert.ToString(i+1);
                            //    //properties["public:numero_guia"] = "prueba2";

                            //    //byte[] content = UTF8Encoding.UTF8.GetBytes(httpPostedFile.InputStream.ToString());

                            //    //var extension = Path.GetExtension(httpPostedFile.FileName);
                            //    ContentStream contentStream = new ContentStream();
                            //    contentStream.FileName = Convert.ToString(i);
                            //    contentStream.MimeType = "application/pdf";
                            //    contentStream.Length = data.Length;
                            //    contentStream.Stream = new MemoryStream(data);

                            //    IDocument doc = folder.CreateDocument(properties, contentStream, null);
                            //}

                            // Subir Archivo
                            //string queryGetFolder = "SELECT * FROM cmis:folder WHERE cmis:name='Documento_Lyrics'";
                            //IItemEnumerable<IQueryResult> docResults = session.Query(queryGetFolder, false);
                            //IQueryResult folderHit = docResults.FirstOrDefault();
                            //string folderId = "";
                            //folderId = folderHit["cmis:objectId"].FirstValue.ToString();
                            #endregion

                            var BEC = "";

                            if (listaAlfresco[0].PropiedadAlfresco.Contains("rec:codigo_bec"))
                                {
                                    BEC= listaAlfresco[0].DATOS;
                                }
                                   

                                IFolder folder = (IFolder)session.GetObject(CodigoCarpetaAlfresco);

                                IDictionary<string, object> properties = new Dictionary<string, object>();
                                properties[PropertyIds.Name] = BEC+"_"+name;
                                //properties[PropertyIds.ObjectTypeId] = "cmis:document";
                                properties[PropertyIds.ObjectTypeId] = Object_id[0].ToString();

                                foreach (var i in listaAlfresco)
                                {
                                    if (i.PropiedadAlfresco.Contains("fecha"))
                                    {
                                        var propiedad = properties[i.PropiedadAlfresco] = Convert.ToDateTime(i.DATOS);
                                    }
                                    else
                                    {
                                        var propiedad = properties[i.PropiedadAlfresco] = i.DATOS;
                                    }

                                }
                               
                                //byte[] content = UTF8Encoding.UTF8.GetBytes(httpPostedFile.InputStream.ToString());

                                //var extension = Path.GetExtension(httpPostedFile.FileName);
                                ContentStream contentStream = new ContentStream();
                                contentStream.FileName = BEC + "_" + name;
                                contentStream.MimeType = httpPostedFile.ContentType;
                                contentStream.Length = data.Length;
                                contentStream.Stream = new MemoryStream(data);

                                IDocument doc = folder.CreateDocument(properties, contentStream, null);

                                return new KeyValuePair<bool, string>(false, "Guardado Con exito.");

                                //------------------
                            }
                            catch (Exception e)
                            {
                                string Message = "";
                                Message = e.Message;
                                if (Message == "Conflicto")
                                {
                                    Message ="En esta licencia existe un documento con el mismo nombre.";
                                }
                                return new KeyValuePair<bool, string>(false, "Mensaje de Error: " + Message);
                            }
                        }

                        return new KeyValuePair<bool, string>(false, "Could not get the uploaded file.");
                    }
                }
            //}
            //else
            //{
            //    return new KeyValuePair<bool, string>(false, "Servicio Deshabilitado.");
            //}
            return new KeyValuePair<bool, string>(false, "No file found to upload.");
        }


    }
}

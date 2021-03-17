using DotCMIS;
using DotCMIS.Client;
using DotCMIS.Client.Impl;
using DotCMIS.Data;
using DotCMIS.Data.Impl;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SGRDA.Entities;
using SGRDA.Entities.Alfresco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.DA.Alfresco
{
    public class DAAlfreco
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public string Query_Alfresco(int Tipo)
        {
            string Query = "";
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDA_QUERY_ALFFRESCO");
                db.AddInParameter(oDbCommand, "@TIPO", DbType.String, Tipo);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {

                        if (!dr.IsDBNull(dr.GetOrdinal("Query")))
                            Query = dr.GetString(dr.GetOrdinal("Query"));
                    }
                }
            }
            catch (Exception e)
            {
                var message = e.Message;
            }
            return Query;
        }
        public void Upload_Files(string ruta, string newFileName)
        {
            var UsuarioAlfresco = GlobalVars.Global.UsuarioAlfresco;
            var ContraseñaAlfreso = GlobalVars.Global.ContraseñaAlfreso;
            try
            {
                //Conexion Alfresco
                var rutaUploadAlfresco = "http://192.168.252.94:8080/alfresco/api/-default-/cmis/versions/1.0/atom";

                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters[SessionParameter.BindingType] = BindingType.AtomPub;
                parameters[SessionParameter.AtomPubUrl] = rutaUploadAlfresco;
                parameters[SessionParameter.User] = UsuarioAlfresco;
                parameters[SessionParameter.Password] = ContraseñaAlfreso;
                //parameters[SessionParameter.RepositoryId] = "-default-";
                SessionFactory factory = SessionFactory.NewInstance();
                //ISession session = factory.CreateSession(parameters);
                ISession session = factory.GetRepositories(parameters)[0].CreateSession();
                //var a = session.DefaultContext;
                var localhost = GlobalVars.Global.RutaDocumentoLyrics;
                //string file = Directory.GetFiles(ruta);
                //List<BEMigrarContrato> lista = new List<BEMigrarContrato>();
                //lista = ListaContrato();
                string docpath = "";


                docpath = localhost + docpath;

                var existe = System.IO.File.Exists(docpath);
                if (existe == true)
                {
                    FileStream FileStream = new FileStream(docpath, FileMode.Open);
                    if (FileStream != null)
                    {
                        StreamReader objLeerArchivo = new StreamReader(FileStream);
                        byte[] data;

                        using (objLeerArchivo)
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                objLeerArchivo.BaseStream.CopyTo(ms);
                                data = ms.ToArray();
                            }
                        }



                        // Subir Archivo

                        //string queryGetFolder = "SELECT * FROM cmis:folder WHERE cmis:objectId ='daefa1fe-9155-49c7-94eb-7a795b6f1f6e'";
                        //IItemEnumerable<IQueryResult> docResults = session.Query(queryGetFolder, false);
                        //IQueryResult folderHit = docResults.FirstOrDefault();

                        //string folderId = "b881291b-f41f-40f5-9dab-ea1ef4aed38e";
                        string folderId = "17d7eb80-6257-47c9-bf6d-caa3e3e4df95";
                        //folderId = folderHit["cmis:objectId"].FirstValue.ToString();   //Obtener Id de Folder
                        IFolder folder = (IFolder)session.GetObject(folderId);
                        string newname = newFileName.Replace(".pdf", ".doc");

                        IDictionary<string, object> properties = new Dictionary<string, object>();
                        properties[PropertyIds.Name] = newname;
                        properties[PropertyIds.ObjectTypeId] = "D:public:contrato";
                        //REPRESENTANTE LEGAL
                        properties["public:apoderado_cesionario"] = "";
                        properties["public:cesionario"] = "";
                        properties["public:dni_apoderado_ces"] = "";
                        properties["public:domicilio_cesionario"] = "";
                        properties["public:fecha_contrato"] = "";
                        properties["public:modalidad"] = "";
                        properties["public:ruc_cesionario"] = "";
                        properties["public:cod_licencia"] = "";
                        //properties["cmis:name"] = newname;
                        properties["cmis:name"] = newname;

                        //byte[] content = UTF8Encoding.UTF8.GetBytes(httpPostedFile.InputStream.ToString());

                        //var extension = Path.GetExtension(httpPostedFile.FileName);
                        ContentStream contentStream = new ContentStream();
                        contentStream.FileName = newname;
                        //objLeerArchivo.ContentType;
                        contentStream.MimeType = "application/pdf";
                        contentStream.Length = data.Length;
                        contentStream.Stream = new MemoryStream(data);

                        IDocument doc = folder.CreateDocument(properties, contentStream, null);
                    }

                }


                //------------------
            }
            catch (Exception e)
            {
                string Message = "";
                Message = e.Message;
            }


        }

        public List<BEDocumentoGral> ListaDocumento(decimal lic_id, string query)
        {
            //string Query_Alfresco = new DAAlfreco.Query_Alfresco(tipo);
            var UsuarioAlfresco = GlobalVars.Global.UsuarioAlfresco;
            var ContraseñaAlfreso = GlobalVars.Global.ContraseñaAlfreso;

            var rutaUploadAlfresco = GlobalVars.Global.rutaUploadAlfresco;
            var RutaSalidaDocumentosAlfresco = GlobalVars.Global.RutaSalidaDocumentosAlfresco;

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters[SessionParameter.BindingType] = BindingType.AtomPub;
            parameters[SessionParameter.AtomPubUrl] = rutaUploadAlfresco;
            parameters[SessionParameter.User] = UsuarioAlfresco;
            parameters[SessionParameter.Password] = ContraseñaAlfreso;
            //parameters[SessionParameter.RepositoryId] = "-default-";
            SessionFactory factory = SessionFactory.NewInstance();
            //ISession session = factory.CreateSession(parameters);
            ISession session = factory.GetRepositories(parameters)[0].CreateSession();

            string queryGetDocument = query + "'" + lic_id + "'";
            IItemEnumerable<IQueryResult> docResults = session.Query(queryGetDocument, false);
            List<BEDocumentoGral> lista = new List<BEDocumentoGral>();
            foreach (IQueryResult i in docResults)
            {
                BEDocumentoGral obj = new BEDocumentoGral();
                //IQueryResult Result = docResults.FirstOrDefault();
                string ObjectId = i["cmis:objectId"].FirstValue.ToString();

                IDocument docs = session.GetObject(ObjectId) as IDocument;
                // properties
                obj.DOC_USER = ObjectId;
                obj.DOC_PATH = docs.Name;
                obj.LOG_USER_CREAT = docs.CreatedBy;
                obj.LOG_USER_UPDATE = docs.LastModifiedBy;
                obj.LOG_DATE_CREAT = docs.CreationDate;
                obj.LOG_DATE_UPDATE = docs.LastModificationDate;
                obj.OWNER = docs.ContentStreamMimeType;
                obj.DOC_ORG = RutaSalidaDocumentosAlfresco + docs.ContentStreamId.Replace("store://", "").Replace("/", "\\");
                IContentStream contentStream = docs.GetContentStream();
                obj.ArchivoBytes = contentStream.Stream;

                var existe = File.Exists(obj.DOC_ORG);
                //IContentStream contentStreams = docs.GetContentStream();
                //byte[] bytes ;

                //Stream stream = docs.ContentStreamId;

                //try {
                //    using (BinaryReader br = new BinaryReader(stream))
                //    {
                //        bytes = br.ReadBytes((int)stream.Length);
                //        obj.Archivo_byte = bytes;
                //    }
                //}
                //catch(Exception e)
                //{
                //    var message = e.Message;
                //}

                if (obj.ArchivoBytes != null)
                {
                    lista.Add(obj);
                }
            }
            return lista;

            //string folderId = "b881291b-f41f-40f5-9dab-ea1ef4aed38e";
            //string folderId = "17d7eb80-6257-47c9-bf6d-caa3e3e4df95";
            //folderId = folderHit["cmis:objectId"].FirstValue.ToString(); 



            //IObjectId id = session.CreateObjectId("12345678");
            //IDocument doc = session.GetObject(id) as IDocument;

            //// properties
            //Console.WriteLine(doc.Name);
            //Console.WriteLine(doc.GetPropertyValue("my:property"));

            //IProperty myProperty = doc["my:property"];
            //Console.WriteLine("Id:    " + myProperty.Id);
            //Console.WriteLine("Value: " + myProperty.Value);
            //Console.WriteLine("Type:  " + myProperty.PropertyType);

            //// content
            //IContentStream contentStream = doc.GetContentStream();
            //Console.WriteLine("Filename:   " + contentStream.FileName);
            //Console.WriteLine("MIME type:  " + contentStream.MimeType);
            //Console.WriteLine("Has stream: " + (contentStream.Stream != null));
        }
        public List<string> Listar_PropiedadesAlfresco_x_TipoDocumento(int TipoDocumento)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDL_PROPIEDADES_X_TIPO_DOCUMENTO");
            oDataBase.AddInParameter(oDbComand, "@TipoDocumento", DbType.Int32, TipoDocumento);
            var lista = new List<string>();
            string dato = "";
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {

                while (reader.Read())
                {
                    dato = Convert.ToString(reader["PropiedadAlfresco"]);
                    lista.Add(dato);
                }
            }
            return lista;
        }

        public List<BE_Datos> Listar_DatosDocumento_x_Licencia_Y_TipoDocumento(int TipoDocumento, int Cod_Lic,int Artist_ID)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDL_RECUPERAR_DATOS_X_LICENCIA_X_TIPODOCUMENTO");
            oDataBase.AddInParameter(oDbComand, "@TipoDocumento", DbType.Int32, TipoDocumento);
            oDataBase.AddInParameter(oDbComand, "@Cod_lic", DbType.Int32, Cod_Lic);
            oDataBase.AddInParameter(oDbComand, "@ARTIS_ID", DbType.Int32, Artist_ID);
            var lista = new List<BE_Datos>();
            BE_Datos datos = new BE_Datos();
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {

                while (reader.Read())
                {
                    datos = new BE_Datos();
                    datos.PropiedadAlfresco = Convert.ToString(reader["PropiedadAlfresco"]);
                    datos.DATOS = Convert.ToString(reader["DATOS"]);
                    lista.Add(datos);
                }
            }
            return lista;
        }

        public List<string> Object_TypeId(int TipoDocumento)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("OBJECT_TYPEID");
            oDataBase.AddInParameter(oDbComand, "@TIPO", DbType.Int32, TipoDocumento);
            var lista = new List<string>();
            string dato = "";
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {

                while (reader.Read())
                {
                    dato = Convert.ToString(reader["ObjectTypeId"]);
                    lista.Add(dato);
                }
            }
            return lista;
        }
        public List<BESelectListItem> Lista_Artista_X_Licencia(int Lic_Id)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDL_ARTISTA_X_LICENCIA");
            oDataBase.AddInParameter(oDbComand, "@LIC_ID", DbType.Int32, Lic_Id);
            var lista = new List<BESelectListItem>();
            BESelectListItem Dato = new BESelectListItem();
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {

                while (reader.Read())
                {
                    Dato = new BESelectListItem();
                    Dato.Text = Convert.ToString(reader["NAME"]);
                    Dato.Value = Convert.ToString(reader["ARTIST_ID"]);
                    lista.Add(Dato);
                }
            }
            return lista;
        }
        public BESelectListItem ValidarImagen_X_MRECID(string MREC_ID)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDA_Valida_Imagen");
            oDataBase.AddInParameter(oDbComand, "@MREC_ID", DbType.String, MREC_ID);
            //var lista = new List<BESelectListItem>();
            BESelectListItem be = new BESelectListItem();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    be.Value = Convert.ToString(reader["DOC_PATH"]);
                    //lista.Add(be);
                }
            }
            return be;
        }

        public string OBTENER_ID_CARPETA(int IdCarpeta)
        {
            string Id_Caperta_Alfresco = "";
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("OBTENER_ID_CARPETA");
                db.AddInParameter(oDbCommand, "@ID_TIPO", DbType.String, IdCarpeta);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {

                        if (!dr.IsDBNull(dr.GetOrdinal("Id_Caperta_Alfresco")))
                            Id_Caperta_Alfresco = dr.GetString(dr.GetOrdinal("Id_Caperta_Alfresco"));
                    }
                }
            }
            catch (Exception e)
            {
                var message = e.Message;
            }
            return Id_Caperta_Alfresco;
        }
        public void Upload_Files_Path(string ruta, decimal lic_id, int CodigoTipoDoc)
        {
            try
            {
                //Conexion Alfresco
                var UsuarioAlfresco = GlobalVars.Global.UsuarioAlfresco;
                var ContraseñaAlfreso = GlobalVars.Global.ContraseñaAlfreso;
                var rutaUploadAlfresco = GlobalVars.Global.rutaUploadAlfresco;
                var CarpetaDocumentosAutoGenerados = GlobalVars.Global.CarpetaDocumentosAutoGenerados;
                var listaAlfresco = new DAAlfreco().Listar_DatosDocumento_x_Licencia_Y_TipoDocumento(Convert.ToInt32(CodigoTipoDoc), Convert.ToInt32(lic_id), Convert.ToInt32(0));
                var Object_id = new DAAlfreco().Object_TypeId(Convert.ToInt32(CodigoTipoDoc));
                var Name = ruta.Split('\\');
                var count = Name.Count() - 1;
                var name = Name[count];
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters[SessionParameter.BindingType] = BindingType.AtomPub;
                parameters[SessionParameter.AtomPubUrl] = rutaUploadAlfresco;
                parameters[SessionParameter.User] = UsuarioAlfresco;
                parameters[SessionParameter.Password] = ContraseñaAlfreso;
                SessionFactory factory = SessionFactory.NewInstance();
                ISession session = factory.GetRepositories(parameters)[0].CreateSession();
                //var localhost = GlobalVars.Global.RutaDocumentoLyrics;
                var existe = System.IO.File.Exists(ruta);
                if (existe == true)
                {
                    FileStream FileStream = new FileStream(ruta, FileMode.Open);
                    if (FileStream != null)
                    {
                        StreamReader objLeerArchivo = new StreamReader(FileStream);
                        byte[] data;

                        using (objLeerArchivo)
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                objLeerArchivo.BaseStream.CopyTo(ms);
                                data = ms.ToArray();
                            }
                        }
                        // Subir Archivo

                        //var objectId = Object_TypeId(CodigoTipoDoc);
                        
                        string codigo_Carpeta = CarpetaDocumentosAutoGenerados;
                        string folderId = codigo_Carpeta;
                        IFolder folder = (IFolder)session.GetObject(folderId);
                        IDictionary<string, object> properties = new Dictionary<string, object>();
                        properties[PropertyIds.Name] = name;
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
                        contentStream.FileName = name;
                        //objLeerArchivo.ContentType;
                        contentStream.MimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        //contentStream.MimeType = "application/pdf";
                        contentStream.Length = data.Length;
                        contentStream.Stream = new MemoryStream(data);

                        IDocument doc = folder.CreateDocument(properties, contentStream, null);
                    }

                }


                //------------------
            }
            catch (Exception e)
            {
                string Message = "";
                Message = e.Message;
            }


        }
        public int Delete_Files(string cod_alfresco)
        {
            var UsuarioAlfresco = GlobalVars.Global.UsuarioAlfresco;
            var ContraseñaAlfreso = GlobalVars.Global.ContraseñaAlfreso;
            var rutaUploadAlfresco = GlobalVars.Global.rutaUploadAlfresco;

            int Result = 0;
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters[SessionParameter.BindingType] = BindingType.AtomPub;
                parameters[SessionParameter.AtomPubUrl] = rutaUploadAlfresco;
                parameters[SessionParameter.User] = UsuarioAlfresco;
                parameters[SessionParameter.Password] = ContraseñaAlfreso;
                SessionFactory factory = SessionFactory.NewInstance();
                ISession session = factory.GetRepositories(parameters)[0].CreateSession();

                IObjectId newId = session.CreateObjectId(cod_alfresco);
                ICmisObject cmisObject = session.GetObject(newId);

                cmisObject.Delete(true);
                Result = 1;
            }
            catch (Exception e)
            {
                var mensaje = e.Message;
                Result = 0;
            }
            return Result;

        }
        //Procedimiento para migrar todo
        public List<BEMigrarContrato> ListaMigrarDocumentos()
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("LISTA_DOCUMENTOS");
            var lista = new List<BEMigrarContrato>();
            BEMigrarContrato Dato = new BEMigrarContrato();
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {

                while (reader.Read())
                {
                    Dato = new BEMigrarContrato();
                    Dato.COD_LICENCIA = Convert.ToDecimal(reader["LIC_ID"]);
                    Dato.DOC_PATH = Convert.ToString(reader["DOC_PATH"]);
                    lista.Add(Dato);
                }
            }
            return lista;
        }
        public void Upload_Files_Path_Migracion(string ruta, decimal lic_id, int CodigoTipoDoc)
        {
            var UsuarioAlfresco = GlobalVars.Global.UsuarioAlfresco;
            var ContraseñaAlfreso = GlobalVars.Global.ContraseñaAlfreso;
            var rutaUploadAlfresco = GlobalVars.Global.rutaUploadAlfresco;

            try
            {
                //Conexion Alfresco
                var listaAlfresco = new DAAlfreco().Listar_DatosDocumento_x_Licencia_Y_TipoDocumento(Convert.ToInt32(CodigoTipoDoc), Convert.ToInt32(lic_id), Convert.ToInt32(0));
                var Object_id = new DAAlfreco().Object_TypeId(Convert.ToInt32(CodigoTipoDoc));
                var Name = ruta.Split('\\');
                var count = Name.Count() - 1;
                var name = Name[count];
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters[SessionParameter.BindingType] = BindingType.AtomPub;
                parameters[SessionParameter.AtomPubUrl] = rutaUploadAlfresco;
                parameters[SessionParameter.User] = UsuarioAlfresco;
                parameters[SessionParameter.Password] = ContraseñaAlfreso;
                SessionFactory factory = SessionFactory.NewInstance();
                ISession session = factory.GetRepositories(parameters)[0].CreateSession();
                //var localhost = GlobalVars.Global.RutaDocumentoLyrics;
                var existe = System.IO.File.Exists(ruta);
                if (existe == true)
                {
                    FileStream FileStream = new FileStream(ruta, FileMode.Open);
                    if (FileStream != null)
                    {
                        StreamReader objLeerArchivo = new StreamReader(FileStream);
                        byte[] data;

                        using (objLeerArchivo)
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                objLeerArchivo.BaseStream.CopyTo(ms);
                                data = ms.ToArray();
                            }
                        }
                        // Subir Archivo

                        string folderId = "41b92423-016f-4e95-b52a-7c927f69b255";
                        IFolder folder = (IFolder)session.GetObject(folderId);
                        IDictionary<string, object> properties = new Dictionary<string, object>();
                        properties[PropertyIds.Name] = name;
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
                        contentStream.FileName = ruta;
                        //objLeerArchivo.ContentType;
                        //contentStream.MimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        contentStream.MimeType = "application/pdf";
                        contentStream.Length = data.Length;
                        contentStream.Stream = new MemoryStream(data);

                        IDocument doc = folder.CreateDocument(properties, contentStream, null);
                    }

                }


                //------------------
            }
            catch (Exception e)
            {
                string Message = "";
                Message = e.Message;
            }


        }

        public decimal Obtener_INV_ID_X_MREC_ID(int MREC_ID)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("OBTENER_FACTURA_x_MREC_ID");
            oDataBase.AddInParameter(oDbComand, "@MREC_ID", DbType.Int32, MREC_ID);
            decimal dato = 0;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    dato = Convert.ToDecimal(reader["INV_ID"]);
                }
            }
            return dato;
        }

        public void Desactivar_Imagen_Cobro(int MREC_ID)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("INACTIVA_DOCUMENTO");
            oDataBase.AddInParameter(oDbComand, "@MREC_ID", DbType.Int32, MREC_ID);
            oDataBase.ExecuteNonQuery(oDbComand);
        }
      
    }
}

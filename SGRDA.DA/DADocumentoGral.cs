using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using SGRDA.Entities;

namespace SGRDA.DA
{
    public class DADocumentoGral
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BEDocumentoGral doc)
        {
            int TYPE = 0;
            int retorno = 0;
            try
            {
                if (doc.DOC_TYPE == 1)
                {
                    TYPE = 56;
                }
                else if (doc.DOC_TYPE == 2)
                {
                    TYPE = 57;
                }
                else
                {
                    TYPE = doc.DOC_TYPE;
                }
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_DOCUMENTO_GRAL");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, doc.OWNER);
                oDataBase.AddOutParameter(oDbComand, "@DOC_ID", DbType.Decimal, Convert.ToInt32(doc.DOC_ID));
                oDataBase.AddInParameter(oDbComand, "@DOC_TYPE", DbType.Decimal, TYPE);
                oDataBase.AddInParameter(oDbComand, "@ENT_ID", DbType.Decimal, doc.ENT_ID);
                oDataBase.AddInParameter(oDbComand, "@DOC_DATE", DbType.DateTime, doc.DOC_DATE);
                oDataBase.AddInParameter(oDbComand, "@DOC_VERSION", DbType.Decimal, doc.DOC_VERSION);
                oDataBase.AddInParameter(oDbComand, "@DOC_USER", DbType.String, doc.DOC_USER);
                oDataBase.AddInParameter(oDbComand, "@DOC_PATH", DbType.String, doc.DOC_PATH == null ? string.Empty : doc.DOC_PATH);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, doc.LOG_USER_CREAT);

                int n = oDataBase.ExecuteNonQuery(oDbComand);
                int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbComand, "@DOC_ID"));

                retorno = id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="codigoBps"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public List<BEDocumentoGral> DocumentoXSocio(decimal codigoBps, string owner, decimal tipoEntidad)
        {
            List<BEDocumentoGral> documentos = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DOCUMENTO_BPS"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, codigoBps);
                    oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, tipoEntidad);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {

                        BEDocumentoGral objDoc = null;
                        documentos = new List<BEDocumentoGral>();
                        while (dr.Read())
                        {
                            objDoc = new BEDocumentoGral();

                            objDoc.DOC_ID = dr.GetDecimal(dr.GetOrdinal("DOC_ID"));
                            objDoc.DOC_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("DOC_TYPE")));
                            objDoc.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                            objDoc.DOC_DATE = dr.GetDateTime(dr.GetOrdinal("DOC_DATE"));
                            objDoc.DOC_PATH = dr.GetString(dr.GetOrdinal("DOC_PATH"));
                            objDoc.DOC_VERSION = dr.GetDecimal(dr.GetOrdinal("DOC_VERSION"));
                            objDoc.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));



                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                objDoc.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            {
                                objDoc.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            {
                                objDoc.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            {
                                objDoc.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            {
                                objDoc.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                            }

                            documentos.Add(objDoc);

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return documentos;
        }
        public List<BEDocumentoGral> DocumentoXLicencia(decimal codigoLic, string owner, decimal tipoEntidad)
        {

            List<BEDocumentoGral> documentos = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DOCUMENTO_LIC"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, codigoLic);
                    oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, tipoEntidad);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {

                        BEDocumentoGral objDoc = null;
                        documentos = new List<BEDocumentoGral>();
                        while (dr.Read())
                        {
                            objDoc = new BEDocumentoGral();

                            objDoc.DOC_ID = dr.GetDecimal(dr.GetOrdinal("DOC_ID"));
                            objDoc.DOC_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("DOC_TYPE")));
                            objDoc.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                            objDoc.DOC_DATE = dr.GetDateTime(dr.GetOrdinal("DOC_DATE"));
                            objDoc.DOC_PATH = dr.GetString(dr.GetOrdinal("DOC_PATH"));
                            objDoc.DOC_VERSION = dr.GetDecimal(dr.GetOrdinal("DOC_VERSION"));
                            objDoc.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));



                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                objDoc.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            {
                                objDoc.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            {
                                objDoc.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            {
                                objDoc.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            {
                                objDoc.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("DOC_ORG")))
                            {
                                objDoc.DOC_ORG = dr.GetString(dr.GetOrdinal("DOC_ORG"));
                            }




                            documentos.Add(objDoc);

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return documentos;
        }

        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idDoc"></param>
        /// <param name="idBps"></param>
        /// <returns></returns>
        public BEDocumentoGral ObtenerDocBPS(string owner, decimal idDoc, decimal idBps, decimal idEntidad)
        {
            BEDocumentoGral Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DOC_BPS"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@DOC_ID", DbType.Decimal, idDoc);
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idBps);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, idEntidad);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEDocumentoGral();
                        Obj.DOC_ID = dr.GetDecimal(dr.GetOrdinal("DOC_ID"));
                        Obj.DOC_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("DOC_TYPE")));
                        Obj.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                        Obj.DOC_DATE = dr.GetDateTime(dr.GetOrdinal("DOC_DATE"));
                        Obj.DOC_PATH = dr.GetString(dr.GetOrdinal("DOC_PATH"));
                        Obj.DOC_VERSION = dr.GetDecimal(dr.GetOrdinal("DOC_VERSION"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }

            return Obj;
        }
        public BEDocumentoGral ObtenerDocLic(string owner, decimal idDoc, decimal idLic, decimal idEntidad)
        {
            BEDocumentoGral Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DOC_LIC"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@DOC_ID", DbType.Decimal, idDoc);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, idEntidad);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEDocumentoGral();
                        Obj.DOC_ID = dr.GetDecimal(dr.GetOrdinal("DOC_ID"));
                        Obj.DOC_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("DOC_TYPE")));
                        Obj.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                        Obj.DOC_DATE = dr.GetDateTime(dr.GetOrdinal("DOC_DATE"));
                        Obj.DOC_PATH = dr.GetString(dr.GetOrdinal("DOC_PATH"));
                        Obj.DOC_VERSION = dr.GetDecimal(dr.GetOrdinal("DOC_VERSION"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }

            return Obj;
        }
        public BEDocumentoGral ObtenerDocEst(string owner, decimal idDoc, decimal idEstablecimiento)
        {
            BEDocumentoGral Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DOC_EST"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@DOC_ID", DbType.Decimal, idDoc);
                oDataBase.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEstablecimiento);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEDocumentoGral();
                        Obj.DOC_ID = dr.GetDecimal(dr.GetOrdinal("DOC_ID"));
                        Obj.DOC_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("DOC_TYPE")));
                        Obj.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                        Obj.DOC_DATE = dr.GetDateTime(dr.GetOrdinal("DOC_DATE"));
                        Obj.DOC_PATH = dr.GetString(dr.GetOrdinal("DOC_PATH"));
                        Obj.DOC_VERSION = dr.GetDecimal(dr.GetOrdinal("DOC_VERSION"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }

            return Obj;
        }

        public BEDocumentoGral ObtenerDocCamp(string owner, decimal idDoc, decimal idCampnia)
        {
            BEDocumentoGral Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DOC_CAMP"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@DOC_ID", DbType.Decimal, idDoc);
                oDataBase.AddInParameter(cm, "@CONC_CID", DbType.Decimal, idCampnia);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEDocumentoGral();
                        Obj.DOC_ID = dr.GetDecimal(dr.GetOrdinal("DOC_ID"));
                        Obj.DOC_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("DOC_TYPE")));
                        Obj.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                        Obj.DOC_DATE = dr.GetDateTime(dr.GetOrdinal("DOC_DATE"));
                        Obj.DOC_PATH = dr.GetString(dr.GetOrdinal("DOC_PATH"));
                        Obj.DOC_VERSION = dr.GetDecimal(dr.GetOrdinal("DOC_VERSION"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }
            return Obj;
        }

        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="dirId"></param>
        /// <param name="user"></param>
        /// <returns></returns> 
        public int Eliminar(string owner, decimal docId, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_DOC_GRAL");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@DOC_ID", DbType.Decimal, docId);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
        /// <summary> 
        /// addon dbs 20140727
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="docId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Activar(string owner, decimal docId, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_DOC_GRAL");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@DOC_ID", DbType.Decimal, docId);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public int Update(BEDocumentoGral doc)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_DOC_GRAL");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, doc.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@DOC_ID", DbType.Decimal, doc.DOC_ID);
            oDataBase.AddInParameter(oDbCommand, "@DOC_TYPE", DbType.Int32, doc.DOC_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@DOC_DATE", DbType.DateTime, doc.DOC_DATE);
            oDataBase.AddInParameter(oDbCommand, "@DOC_USER", DbType.String, doc.DOC_USER);
            oDataBase.AddInParameter(oDbCommand, "@DOC_PATH", DbType.String, doc.DOC_PATH);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, doc.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }


        public List<BEDocumentoGral> DocumentoXEstablecimiento(decimal idEstablecimiento, string owner, decimal tipoEntidad)
        {
            List<BEDocumentoGral> documentos = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DOCUMENTO_EST"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@EST_ID", DbType.String, idEstablecimiento);
                    oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, tipoEntidad);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        BEDocumentoGral objDoc = null;
                        documentos = new List<BEDocumentoGral>();
                        while (dr.Read())
                        {
                            objDoc = new BEDocumentoGral();
                            objDoc.DOC_ID = dr.GetDecimal(dr.GetOrdinal("DOC_ID"));
                            objDoc.DOC_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("DOC_TYPE")));
                            objDoc.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                            objDoc.DOC_DATE = dr.GetDateTime(dr.GetOrdinal("DOC_DATE"));
                            objDoc.DOC_PATH = dr.GetString(dr.GetOrdinal("DOC_PATH"));
                            objDoc.DOC_VERSION = dr.GetDecimal(dr.GetOrdinal("DOC_VERSION"));
                            objDoc.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));

                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                objDoc.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            {
                                objDoc.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            {
                                objDoc.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            {
                                objDoc.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            {
                                objDoc.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                            }
                            documentos.Add(objDoc);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return documentos;
        }

        public List<BEDocumentoGral> DocumentoXCampania(string owner, decimal idCampania)
        {
            List<BEDocumentoGral> lista = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_LISTAR_DOCUMENTO_CAMP"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@CONC_CID", DbType.Decimal, idCampania);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    BEDocumentoGral item = null;
                    lista = new List<BEDocumentoGral>();
                    while (dr.Read())
                    {
                        item = new BEDocumentoGral();
                        if (!dr.IsDBNull(dr.GetOrdinal("OWNER")))
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DOC_ID")))
                            item.DOC_ID = dr.GetDecimal(dr.GetOrdinal("DOC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DOC_TYPE")))
                            item.DOC_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("DOC_TYPE")));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENT_ID")))
                            item.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                        if (!dr.IsDBNull(dr.GetOrdinal("DOC_DATE")))
                            item.DOC_DATE = dr.GetDateTime(dr.GetOrdinal("DOC_DATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DOC_PATH")))
                            item.DOC_PATH = dr.GetString(dr.GetOrdinal("DOC_PATH"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DOC_VERSION")))
                            item.DOC_VERSION = dr.GetDecimal(dr.GetOrdinal("DOC_VERSION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }


        public List<BEDocumentoGral> DocumentoXOficina(decimal codigoOff, string owner)
        {
            List<BEDocumentoGral> documentos = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_DOCUMENTO_OFF"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@OFF_ID", DbType.Decimal, codigoOff);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {

                        BEDocumentoGral objDoc = null;
                        documentos = new List<BEDocumentoGral>();
                        while (dr.Read())
                        {
                            objDoc = new BEDocumentoGral();

                            objDoc.DOC_ID = dr.GetDecimal(dr.GetOrdinal("DOC_ID"));
                            objDoc.DOC_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("DOC_TYPE")));
                            objDoc.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                            objDoc.DOC_DATE = dr.GetDateTime(dr.GetOrdinal("DOC_DATE"));
                            objDoc.DOC_PATH = dr.GetString(dr.GetOrdinal("DOC_PATH"));
                            objDoc.DOC_VERSION = dr.GetDecimal(dr.GetOrdinal("DOC_VERSION"));

                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                                objDoc.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                                objDoc.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                                objDoc.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                                objDoc.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                objDoc.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }

                            documentos.Add(objDoc);

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return documentos;
        }


        public BETipoDocumento ObtenerTipoDocumento(string Owner, decimal idTipo)
        {


            BETipoDocumento Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTIENE_TIPO_DOC"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                oDataBase.AddInParameter(cm, "@DOC_TYPE", DbType.Decimal, idTipo);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BETipoDocumento();
                        Obj.DOC_TYPE = dr.GetDecimal(dr.GetOrdinal("DOC_TYPE"));
                        Obj.DOC_DESC = dr.GetString(dr.GetOrdinal("DOC_DESC"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }

            return Obj;

        }

        public BEDocumentoGral ObtenerDocOFF(string owner, decimal idDoc, decimal idOff)
        {
            BEDocumentoGral Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DOC_OFF"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@DOC_ID", DbType.Decimal, idDoc);
                oDataBase.AddInParameter(cm, "@OFF_ID", DbType.Decimal, idOff);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEDocumentoGral();
                        Obj.DOC_ID = dr.GetDecimal(dr.GetOrdinal("DOC_ID"));
                        Obj.DOC_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("DOC_TYPE")));
                        Obj.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                        Obj.DOC_DATE = dr.GetDateTime(dr.GetOrdinal("DOC_DATE"));
                        Obj.DOC_PATH = dr.GetString(dr.GetOrdinal("DOC_PATH"));
                        Obj.DOC_VERSION = dr.GetDecimal(dr.GetOrdinal("DOC_VERSION"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }

            return Obj;
        }
        public int UpdatePath(string owner, decimal docId, string fileName)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_DOC_GRAL_FILE");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@DOC_ID", DbType.Decimal, docId);
            oDataBase.AddInParameter(oDbCommand, "@DOC_PATH", DbType.String, fileName);

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BEDocumentoGral Obtener(string owner, decimal idDoc)
        {
            BEDocumentoGral Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DOC"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@DOC_ID", DbType.Decimal, idDoc);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEDocumentoGral();
                        Obj.DOC_ID = dr.GetDecimal(dr.GetOrdinal("DOC_ID"));
                        Obj.DOC_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("DOC_TYPE")));
                        Obj.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                        Obj.DOC_DATE = dr.GetDateTime(dr.GetOrdinal("DOC_DATE"));
                        Obj.DOC_PATH = dr.GetString(dr.GetOrdinal("DOC_PATH"));
                        Obj.DOC_VERSION = dr.GetDecimal(dr.GetOrdinal("DOC_VERSION"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }

            return Obj;
        }

        public int EliminarFisico(string owner, decimal docId)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_DOC_GRAL_FISICO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@DOC_ID", DbType.Decimal, docId);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

    }
}

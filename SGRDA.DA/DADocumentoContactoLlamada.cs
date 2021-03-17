using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SGRDA.Entities;

namespace SGRDA.DA
{
    public class DADocumentoContactoLlamada
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public int Insertar(BEDocumentoContactoLlamada en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAI_CONTAC_DOCS_CALL");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CONC_MID", DbType.Decimal, en.CONC_MID);
            oDataBase.AddInParameter(oDbCommand, "@DOC_ID", DbType.Decimal, en.DOC_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public BEDocumentoContactoLlamada ObtenerDocumento(string owner, decimal Id, decimal IdDoc)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_GET_DOCUMENTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@CONC_MID", DbType.Decimal, Id);
            oDataBase.AddInParameter(oDbCommand, "@DOC_ID", DbType.Decimal, IdDoc);
            BEDocumentoContactoLlamada item = null;

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEDocumentoContactoLlamada();
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_MID")))
                        item.CONC_MID = dr.GetDecimal(dr.GetOrdinal("CONC_MID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DOC_ID")))
                        item.DOC_ID = dr.GetDecimal(dr.GetOrdinal("DOC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DOC_PATH")))
                        item.DOC_PATH = dr.GetString(dr.GetOrdinal("DOC_PATH"));
                }
            }
            return item;
        }

        public List<BEDocumentoGral> DocumentoXContactollamada(decimal Id, string owner, decimal tipoEntidad)
        {
            List<BEDocumentoGral> lista = new List<BEDocumentoGral>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DOCUMENTO_CONTACTO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@CONC_SID", DbType.Decimal, Id);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, tipoEntidad);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    BEDocumentoGral item = null;
                    while (dr.Read())
                    {
                        item = new BEDocumentoGral();
                        if (!dr.IsDBNull(dr.GetOrdinal("CONC_MID")))
                            item.CONC_MID = dr.GetDecimal(dr.GetOrdinal("CONC_MID"));
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
                        if (!dr.IsDBNull(dr.GetOrdinal("OWNER")))
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
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
                return lista;
            }
        }
    }
}

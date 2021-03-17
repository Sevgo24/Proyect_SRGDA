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
    public class DAREC_DOCUMENTS_GRAL
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_DOCUMENTS_GRAL> Get_REC_DOCUMENTS_GRAL()
        {
            List<BEREC_DOCUMENTS_GRAL> lst = new List<BEREC_DOCUMENTS_GRAL>();
            BEREC_DOCUMENTS_GRAL item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_DOCUMENTS_GRAL"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_DOCUMENTS_GRAL();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.DOC_ID = dr.GetDecimal(dr.GetOrdinal("DOC_ID"));
                            item.DOC_TYPE = dr.GetDecimal(dr.GetOrdinal("DOC_TYPE"));
                            item.DOC_DESC = dr.GetString(dr.GetOrdinal("DOC_DESC"));
                            item.DOC_DATE = dr.GetDateTime(dr.GetOrdinal("DOC_DATE"));
                            item.DOC_VERSION = dr.GetDecimal(dr.GetOrdinal("DOC_VERSION"));
                            item.DOC_USER = dr.GetString(dr.GetOrdinal("DOC_USER"));
                            item.DOC_PATH = dr.GetString(dr.GetOrdinal("DOC_PATH"));
                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }

        public List<BEREC_DOCUMENTS_GRAL> REC_DOCUMENTS_GRAL_GET_by_DOC_ID(string OWNER, decimal DOC_ID)
        {
            List<BEREC_DOCUMENTS_GRAL> lst = new List<BEREC_DOCUMENTS_GRAL>();
            BEREC_DOCUMENTS_GRAL item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_DOCUMENTS_GRAL_GET_by_DOC_ID"))
                {
                    db.AddInParameter(cm, "@DOC_ID", DbType.Decimal, DOC_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_DOCUMENTS_GRAL();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.DOC_ID = dr.GetDecimal(dr.GetOrdinal("DOC_ID"));
                            item.DOC_DESC = dr.GetString(dr.GetOrdinal("DOC_DESC"));
                            item.DOC_TYPE = dr.GetDecimal(dr.GetOrdinal("DOC_TYPE"));
                            item.DOC_DATE = dr.GetDateTime(dr.GetOrdinal("DOC_DATE"));
                            item.DOC_VERSION = dr.GetDecimal(dr.GetOrdinal("DOC_VERSION"));
                            item.DOC_USER = dr.GetString(dr.GetOrdinal("DOC_USER"));
                            item.DOC_PATH = dr.GetString(dr.GetOrdinal("DOC_PATH"));
                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }

        public List<BEREC_DOCUMENTS_GRAL> REC_DOCUMENTS_GRAL_Page(string param, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_DOCUMENTS_GRAL_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REC_DOCUMENTS_GRAL_GET_Page", param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREC_DOCUMENTS_GRAL>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEREC_DOCUMENTS_GRAL(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_DOCUMENTS_GRAL_Ins(BEREC_DOCUMENTS_GRAL en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_DOCUMENTS_GRAL_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@DOC_TYPE", DbType.Decimal, en.DOC_TYPE);
                db.AddInParameter(oDbCommand, "@DOC_DATE", DbType.DateTime, en.DOC_DATE);
                db.AddInParameter(oDbCommand, "@DOC_VERSION", DbType.Decimal, en.DOC_VERSION);
                db.AddInParameter(oDbCommand, "@DOC_USER", DbType.String, en.DOC_USER);
                db.AddInParameter(oDbCommand, "@DOC_PATH", DbType.String, en.DOC_PATH);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_DOCUMENTS_GRAL_Upd(BEREC_DOCUMENTS_GRAL en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_DOCUMENTS_GRAL_Upd"); 
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@DOC_ID", DbType.Decimal, en.DOC_ID);
                db.AddInParameter(oDbCommand, "@DOC_TYPE", DbType.Decimal, en.DOC_TYPE);
                db.AddInParameter(oDbCommand, "@DOC_DATE", DbType.DateTime, en.DOC_DATE);
                db.AddInParameter(oDbCommand, "@DOC_VERSION", DbType.Decimal, en.DOC_VERSION);
                db.AddInParameter(oDbCommand, "@DOC_USER", DbType.String, en.DOC_USER);
                db.AddInParameter(oDbCommand, "@DOC_PATH", DbType.String, en.DOC_PATH);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_DOCUMENTS_GRAL_Del(string OWNER, decimal DOC_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_DOCUMENTS_GRAL_Del");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, OWNER);
                db.AddInParameter(oDbCommand, "@DOC_ID", DbType.Decimal, DOC_ID);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

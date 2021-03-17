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
    public class DAREC_QUALIFY_CHAR
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_QUALIFY_CHAR> Get_REC_QUALIFY_CHAR()
        {
            List<BEREC_QUALIFY_CHAR> lst = new List<BEREC_QUALIFY_CHAR>();
            BEREC_QUALIFY_CHAR item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_QUALIFY_CHAR"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_QUALIFY_CHAR();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.QUC_ID = dr.GetDecimal(dr.GetOrdinal("QUC_ID"));
                            item.QUA_ID = dr.GetDecimal(dr.GetOrdinal("QUA_ID"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                            item.DESCTIPO = dr.GetString(dr.GetOrdinal("DESCTIPO"));
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

        public List<BEREC_QUALIFY_CHAR> REC_QUALIFY_CHAR_GET_by_QUA_ID(string OWNER, decimal QUC_ID)
        {
            List<BEREC_QUALIFY_CHAR> lst = new List<BEREC_QUALIFY_CHAR>();
            BEREC_QUALIFY_CHAR item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_QUALIFY_CHAR_GET_by_QUC_ID"))
                {
                    db.AddInParameter(cm, "@QUC_ID", DbType.Decimal, QUC_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_QUALIFY_CHAR();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.QUC_ID = dr.GetDecimal(dr.GetOrdinal("QUC_ID"));
                            item.QUA_ID = dr.GetDecimal(dr.GetOrdinal("QUA_ID"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                            item.DESCTIPO = dr.GetString(dr.GetOrdinal("DESCTIPO"));
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

        public List<BEREC_QUALIFY_CHAR> REC_QUALIFY_CHAR_Page(string param, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_QUALIFY_CHAR_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REC_QUALIFY_CHAR_GET_Page", param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREC_QUALIFY_CHAR>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEREC_QUALIFY_CHAR(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_QUALIFY_CHAR_Ins(BEREC_QUALIFY_CHAR en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_QUALIFY_CHAR_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@QUA_ID", DbType.Decimal, en.QUA_ID);
                db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_QUALIFY_CHAR_Upd(BEREC_QUALIFY_CHAR en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_QUALIFY_CHAR_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@QUC_ID", DbType.Decimal, en.QUC_ID);
                db.AddInParameter(oDbCommand, "@QUA_ID", DbType.Decimal, en.QUA_ID);
                db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_QUALIFY_CHAR_Del(decimal QUC_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_QUALIFY_CHAR_Del");
                db.AddInParameter(oDbCommand, "@QUC_ID", DbType.Decimal, QUC_ID);
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

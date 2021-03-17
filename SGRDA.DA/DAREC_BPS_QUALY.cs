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
    public class DAREC_BPS_QUALY
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_BPS_QUALY> Get_REC_BPS_QUALY()
        {
            List<BEREC_BPS_QUALY> lst = new List<BEREC_BPS_QUALY>();
            BEREC_BPS_QUALY item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_BPS_QUALY"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_BPS_QUALY();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            item.QUC_ID = dr.GetDecimal(dr.GetOrdinal("QUC_ID"));
                            item.CARACTERISTICA = dr.GetString(dr.GetOrdinal("CARACTERISTICA"));

                            DateTime LOG_DATE_CREAT;
                            bool isDateCREAT = DateTime.TryParse(dr["LOG_DATE_CREAT"].ToString(), out LOG_DATE_CREAT);
                            item.LOG_DATE_CREAT = LOG_DATE_CREAT;

                            DateTime LOG_DATE_UPDATE;
                            bool isDateUPD = DateTime.TryParse(dr["LOG_DATE_UPDATE"].ToString(), out LOG_DATE_UPDATE);
                            item.LOG_DATE_UPDATE = LOG_DATE_UPDATE;

                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                            item.LOG_USER_UPDAT = (item.LOG_USER_UPDAT == null) ? string.Empty : item.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
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

        public List<BEREC_BPS_QUALY> REC_BPS_QUALY_GET_by_BPS_ID(string OWNER, decimal BPS_ID)
        {
            List<BEREC_BPS_QUALY> lst = new List<BEREC_BPS_QUALY>();
            BEREC_BPS_QUALY item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_BPS_QUALY_GET_by_BPS_ID"))
                {
                    db.AddInParameter(cm, "@BPS_ID", DbType.Decimal, BPS_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_BPS_QUALY();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            item.QUC_ID = dr.GetDecimal(dr.GetOrdinal("QUC_ID"));
                            item.CARACTERISTICA = dr.GetString(dr.GetOrdinal("CARACTERISTICA"));

                            DateTime LOG_DATE_CREAT;
                            bool isDateCREAT = DateTime.TryParse(dr["LOG_DATE_CREAT"].ToString(), out LOG_DATE_CREAT);
                            item.LOG_DATE_CREAT = LOG_DATE_CREAT;

                            DateTime LOG_DATE_UPDATE;
                            bool isDateUPD = DateTime.TryParse(dr["LOG_DATE_UPDATE"].ToString(), out LOG_DATE_UPDATE);
                            item.LOG_DATE_UPDATE = LOG_DATE_UPDATE;

                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                            item.LOG_USER_UPDAT = (item.LOG_USER_UPDAT == null) ? string.Empty : item.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
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

        public List<BEREC_BPS_QUALY> REC_BPS_QUALY_Page(string param, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_BPS_QUALY_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REC_BPS_QUALY_GET_Page", param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREC_BPS_QUALY>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEREC_BPS_QUALY(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_BPS_QUALY_Ins(BEREC_BPS_QUALY en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_BPS_QUALY_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
                db.AddInParameter(oDbCommand, "@QUC_ID", DbType.Decimal, en.QUC_ID);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_BPS_QUALY_Upd(BEREC_BPS_QUALY en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_BPS_QUALY_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
                db.AddInParameter(oDbCommand, "@QUC_ID", DbType.Decimal, en.QUC_ID);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_BPS_QUALY_Del(string OWNER, decimal BPS_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_BPS_QUALY_Del");
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, BPS_ID);
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, OWNER);
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

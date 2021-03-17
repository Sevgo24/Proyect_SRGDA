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
    public class DAREC_BPS_BANKS_ACC
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_BPS_BANKS_ACC> Get_REC_BPS_BANKS_ACC()
        {
            List<BEREC_BPS_BANKS_ACC> lst = new List<BEREC_BPS_BANKS_ACC>();
            BEREC_BPS_BANKS_ACC item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_BPS_BANKS_ACC"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_BPS_BANKS_ACC();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            item.BNK_ID = dr.GetDecimal(dr.GetOrdinal("BNK_ID"));
                            item.BRCH_ID = dr.GetString(dr.GetOrdinal("BRCH_ID"));
                            item.BACC_NUMBER = dr.GetString(dr.GetOrdinal("BACC_NUMBER"));
                            item.BACC_DC = dr.GetString(dr.GetOrdinal("BACC_DC"));
                            item.BACC_TYPE = dr.GetString(dr.GetOrdinal("BACC_TYPE"));
                            item.BACC_DEF = dr.GetString(dr.GetOrdinal("BACC_DEF"));
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

        public List<BEREC_BPS_BANKS_ACC> REC_BPS_BANKS_ACC_GET_by_BNK_ID(string OWNER, decimal BNK_ID, string BRCH_ID)
        {
            List<BEREC_BPS_BANKS_ACC> lst = new List<BEREC_BPS_BANKS_ACC>();
            BEREC_BPS_BANKS_ACC item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_BPS_BANKS_ACC_GET_by_BNK_ID"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, OWNER);
                    db.AddInParameter(cm, "@BNK_ID", DbType.Decimal, BNK_ID);
                    db.AddInParameter(cm, "@BRCH_ID", DbType.String, BRCH_ID);
                    

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_BPS_BANKS_ACC();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            item.BNK_ID = dr.GetDecimal(dr.GetOrdinal("BNK_ID"));
                            item.BRCH_ID = dr.GetString(dr.GetOrdinal("BRCH_ID"));
                            item.BACC_NUMBER = dr.GetString(dr.GetOrdinal("BACC_NUMBER"));
                            item.BACC_DC = dr.GetString(dr.GetOrdinal("BACC_DC"));
                            item.BACC_TYPE = dr.GetString(dr.GetOrdinal("BACC_TYPE"));
                            item.BACC_DEF = dr.GetString(dr.GetOrdinal("BACC_DEF"));
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

        public List<BEREC_BPS_BANKS_ACC> REC_BPS_BANKS_ACC_Page(string owner, string param, int st, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_BPS_BANKS_ACC_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEREC_BPS_BANKS_ACC>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_BPS_BANKS_ACC(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_BPS_BANKS_ACC_Ins(BEREC_BPS_BANKS_ACC en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_BPS_BANKS_ACC_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
                db.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, en.BNK_ID);
                db.AddInParameter(oDbCommand, "@BRCH_ID", DbType.String, en.BRCH_ID);
                db.AddInParameter(oDbCommand, "@BACC_NUMBER", DbType.String, en.BACC_NUMBER);
                db.AddInParameter(oDbCommand, "@BACC_DC", DbType.String, en.BACC_DC);
                db.AddInParameter(oDbCommand, "@BACC_TYPE", DbType.String, en.BACC_TYPE);
                db.AddInParameter(oDbCommand, "@BACC_DEF", DbType.String, en.BACC_DEF);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_BPS_BANKS_ACC_Upd(BEREC_BPS_BANKS_ACC en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_BPS_BANKS_ACC_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
                db.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, en.BNK_ID);
                db.AddInParameter(oDbCommand, "@BRCH_ID", DbType.String, en.BRCH_ID);
                db.AddInParameter(oDbCommand, "@BACC_NUMBER", DbType.String, en.BACC_NUMBER);
                db.AddInParameter(oDbCommand, "@BACC_DC", DbType.String, en.BACC_DC);
                db.AddInParameter(oDbCommand, "@BACC_TYPE", DbType.String, en.BACC_TYPE);
                db.AddInParameter(oDbCommand, "@BACC_DEF", DbType.String, en.BACC_DEF);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDATE);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_BPS_BANKS_ACC_Del(decimal BNK_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_BPS_BANKS_ACC_Del");
                db.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, BNK_ID);
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
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

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
    public class DAREC_DEBTORS_RANGE
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_DEBTORS_RANGE> Get_REC_DEBTORS_RANGE()
        {
            List<BEREC_DEBTORS_RANGE> lst = new List<BEREC_DEBTORS_RANGE>();
            BEREC_DEBTORS_RANGE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_DEBTORS_RANGE"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_DEBTORS_RANGE();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.RANGE_COD = dr.GetDecimal(dr.GetOrdinal("RANGE_COD"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                            item.RANGE_FROM = dr.GetDecimal(dr.GetOrdinal("RANGE_FROM"));
                            item.RANGE_TO = dr.GetDecimal(dr.GetOrdinal("RANGE_TO"));
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

        public List<BEREC_DEBTORS_RANGE> REC_DEBTORS_RANGE_GET_by_RANGE_COD(string OWNER, decimal RANGE_COD)
        {
            List<BEREC_DEBTORS_RANGE> lst = new List<BEREC_DEBTORS_RANGE>();
            BEREC_DEBTORS_RANGE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_DEBTORS_RANGE_GET_by_RANGE_COD"))
                {
                    db.AddInParameter(cm, "@RANGE_COD", DbType.Decimal, RANGE_COD);
                    db.AddInParameter(cm, "@OWNER", DbType.String, OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_DEBTORS_RANGE();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.RANGE_COD = dr.GetDecimal(dr.GetOrdinal("RANGE_COD"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                            item.RANGE_FROM = dr.GetDecimal(dr.GetOrdinal("RANGE_FROM"));
                            item.RANGE_TO = dr.GetDecimal(dr.GetOrdinal("RANGE_TO"));
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

        public List<BEREC_DEBTORS_RANGE> REC_DEBTORS_RANGE_Page(string param, int st, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_DEBTORS_RANGE_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REC_DEBTORS_RANGE_GET_Page", param, st, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREC_DEBTORS_RANGE>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEREC_DEBTORS_RANGE(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_DEBTORS_RANGE_Ins(BEREC_DEBTORS_RANGE en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_DEBTORS_RANGE_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@RANGE_COD", DbType.Decimal, en.RANGE_COD);
                db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION);
                db.AddInParameter(oDbCommand, "@RANGE_FROM", DbType.Decimal, en.RANGE_FROM);
                db.AddInParameter(oDbCommand, "@RANGE_TO", DbType.Decimal, en.RANGE_TO);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT); 
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_DEBTORS_RANGE_Upd(BEREC_DEBTORS_RANGE en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_DEBTORS_RANGE_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@RANGE_COD", DbType.Decimal, en.RANGE_COD);
                db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION);
                db.AddInParameter(oDbCommand, "@RANGE_FROM", DbType.Decimal, en.RANGE_FROM);
                db.AddInParameter(oDbCommand, "@RANGE_TO", DbType.Decimal, en.RANGE_TO);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_DEBTORS_RANGE_Del(string OWNER, decimal RANGE_COD)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_DEBTORS_RANGE_Del");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, OWNER);
                db.AddInParameter(oDbCommand, "@RANGE_COD", DbType.Decimal, RANGE_COD);
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

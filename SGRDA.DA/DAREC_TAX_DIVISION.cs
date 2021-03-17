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
    public class DAREC_TAX_DIVISION
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREF_TAX_DIVISION> Get_REF_TAX_DIVISION(string owner,decimal territorio)
        {
            List<BEREF_TAX_DIVISION> lst = new List<BEREF_TAX_DIVISION>();
            BEREF_TAX_DIVISION item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REF_TAX_DIVISION"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    db.AddInParameter(cm, "@TIS_N", DbType.Decimal, territorio);
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_TAX_DIVISION();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.TAXD_ID = dr.GetDecimal(dr.GetOrdinal("TAXD_ID"));
                            item.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
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

        public List<BEREF_TAX_DIVISION> REF_TAX_DIVISION_GET_by_TAXD_ID(decimal TAXD_ID)
        {
            List<BEREF_TAX_DIVISION> lst = new List<BEREF_TAX_DIVISION>();
            BEREF_TAX_DIVISION item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_TAX_DIVISION_GET_by_TAXD_ID"))
                {
                    db.AddInParameter(cm, "@TAXD_ID", DbType.Decimal, TAXD_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_TAX_DIVISION();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.TAXD_ID = dr.GetDecimal(dr.GetOrdinal("TAXD_ID"));
                            item.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
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

        public List<BEREF_TAX_DIVISION> REF_TAX_DIVISION_Page(string param, int st, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REF_TAX_DIVISION_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REF_TAX_DIVISION_GET_Page", param, GlobalVars.Global.OWNER, st, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREF_TAX_DIVISION>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEREF_TAX_DIVISION(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REF_TAX_DIVISION_Ins(BEREF_TAX_DIVISION en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_TAX_DIVISION_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, en.TIS_N);
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

        public bool REF_TAX_DIVISION_Upd(BEREF_TAX_DIVISION en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_TAX_DIVISION_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@TAXD_ID", DbType.Decimal, en.TAXD_ID);
                db.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, en.TIS_N);
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

        public bool REF_TAX_DIVISION_Del(decimal TAXD_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_TAX_DIVISION_Del");
                db.AddInParameter(oDbCommand, "@TAXD_ID", DbType.Decimal, TAXD_ID);
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

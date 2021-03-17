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
    public class DAREC_CHARACTERISTICS
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_CHARACTERISTICS> Get_REC_CHARACTERISTICS()
        {
            List<BEREC_CHARACTERISTICS> lst = new List<BEREC_CHARACTERISTICS>();
            BEREC_CHARACTERISTICS item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_CHARACTERISTICS"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_CHARACTERISTICS();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));
                            item.CHAR_SHORT = dr.GetString(dr.GetOrdinal("CHAR_SHORT"));
                            item.CHAR_LONG = dr.GetString(dr.GetOrdinal("CHAR_LONG"));  
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

        public List<BEREC_CHARACTERISTICS> REC_CHARACTERISTICS_GET_by_CHAR_ID(decimal CHAR_ID)
        {
            List<BEREC_CHARACTERISTICS> lst = new List<BEREC_CHARACTERISTICS>();
            BEREC_CHARACTERISTICS item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_CHARACTERISTICS_GET_by_CHAR_ID"))
                {
                    db.AddInParameter(cm, "@CHAR_ID", DbType.Decimal, CHAR_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_CHARACTERISTICS();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));
                            item.CHAR_SHORT = dr.GetString(dr.GetOrdinal("CHAR_SHORT"));
                            item.CHAR_LONG = dr.GetString(dr.GetOrdinal("CHAR_LONG"));
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

        public List<BEREC_CHARACTERISTICS> REC_RATE_FREQUENCY_Page(string param, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_CHARACTERISTICS_GET_Page");
            db.AddInParameter(oDbCommand, "@param", DbType.String, param);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEREC_CHARACTERISTICS>();

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_CHARACTERISTICS(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_CHARACTERISTICS_Ins(BEREC_CHARACTERISTICS en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_CHARACTERISTICS_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@CHAR_SHORT", DbType.String, en.CHAR_SHORT.ToUpper());
                db.AddInParameter(oDbCommand, "@CHAR_LONG", DbType.String, en.CHAR_LONG.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_CHARACTERISTICS_Upd(BEREC_CHARACTERISTICS en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_CHARACTERISTICS_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@CHAR_ID", DbType.Decimal, en.CHAR_ID);
                db.AddInParameter(oDbCommand, "@CHAR_SHORT", DbType.String, en.CHAR_SHORT.ToUpper());
                db.AddInParameter(oDbCommand, "@CHAR_LONG", DbType.String, en.CHAR_LONG.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDATE.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_CHARACTERISTICS_Del(decimal CHAR_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_CHARACTERISTICS_Del");
                db.AddInParameter(oDbCommand, "@CHAR_ID", DbType.Decimal, CHAR_ID);
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

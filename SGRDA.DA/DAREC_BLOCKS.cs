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
    public class DAREC_BLOCKS
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_BLOCKS> Get_REC_BLOCKS()
        {
            List<BEREC_BLOCKS> lst = new List<BEREC_BLOCKS>();
            BEREC_BLOCKS item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_BLOCKS"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_BLOCKS();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.BLOCK_ID = dr.GetDecimal(dr.GetOrdinal("BLOCK_ID"));
                            item.BLOCK_DESC = dr.GetString(dr.GetOrdinal("BLOCK_DESC"));
                            item.BLOCK_PULL = dr.GetString(dr.GetOrdinal("BLOCK_PULL"));
                            item.BLOCK_AUT = dr.GetString(dr.GetOrdinal("BLOCK_AUT"));
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

        public List<BEREC_BLOCKS> REC_BLOCKS_GET_by_BLOCK_ID(decimal BLOCK_ID)
        {
            List<BEREC_BLOCKS> lst = new List<BEREC_BLOCKS>();
            BEREC_BLOCKS item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_BLOCKS_GET_by_BLOCK_ID"))
                {
                    db.AddInParameter(cm, "@BLOCK_ID", DbType.Decimal, BLOCK_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_BLOCKS();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.BLOCK_ID = dr.GetDecimal(dr.GetOrdinal("BLOCK_ID"));
                            item.BLOCK_DESC = dr.GetString(dr.GetOrdinal("BLOCK_DESC"));
                            item.BLOCK_PULL = dr.GetString(dr.GetOrdinal("BLOCK_PULL"));
                            item.BLOCK_AUT = dr.GetString(dr.GetOrdinal("BLOCK_AUT"));
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

        public List<BEREC_BLOCKS> REC_BLOCKS_Page(string param, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_BLOCKS_GET_Page");
            db.AddInParameter(oDbCommand, "@param", DbType.String, param);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REC_BLOCKS_GET_Page", param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREC_BLOCKS>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEREC_BLOCKS(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_BLOCKS_Ins(BEREC_BLOCKS en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_BLOCKS_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@BLOCK_DESC", DbType.String, en.BLOCK_DESC);
                db.AddInParameter(oDbCommand, "@BLOCK_PULL", DbType.String, en.BLOCK_PULL);
                db.AddInParameter(oDbCommand, "@BLOCK_AUT", DbType.String, en.BLOCK_AUT);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_BLOCKS_Upd(BEREC_BLOCKS en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_BLOCKS_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@BLOCK_ID", DbType.Decimal, en.BLOCK_ID);
                db.AddInParameter(oDbCommand, "@BLOCK_DESC", DbType.String, en.BLOCK_DESC);
                db.AddInParameter(oDbCommand, "@BLOCK_PULL", DbType.String, en.BLOCK_PULL);
                db.AddInParameter(oDbCommand, "@BLOCK_AUT", DbType.String, en.BLOCK_AUT);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_BLOCKS_Del(decimal BLOCK_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_BLOCKS_Del");
                db.AddInParameter(oDbCommand, "@BLOCK_ID", DbType.Decimal, BLOCK_ID);
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

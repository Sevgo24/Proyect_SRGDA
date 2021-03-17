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
    public class DAREC_COLL_LEVEL
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_COLL_LEVEL> LISTAR_REC_COLL_LEVEL()
        {
            List <BEREC_COLL_LEVEL > lst = new List<BEREC_COLL_LEVEL>();
            BEREC_COLL_LEVEL item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_OFICINA_CARGO"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_COLL_LEVEL();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.LEVEL_ID = dr.GetDecimal(dr.GetOrdinal("LEVEL_ID"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                            item.LEVEL_DEP = dr.GetDecimal(dr.GetOrdinal("LEVEL_DEP"));
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
        public BEREC_COLL_LEVEL LISTAR_REC_COLL_LEVEL_X_ID(decimal LEVEL_ID)
        {
            //List<BEREC_COLL_LEVEL> lst = new List<BEREC_COLL_LEVEL>();
            BEREC_COLL_LEVEL item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_OFICINA_CARGO_X_ID"))
                {
                    db.AddInParameter(cm, "@LEVEL_ID", DbType.Decimal, LEVEL_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_COLL_LEVEL();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.LEVEL_ID = dr.GetDecimal(dr.GetOrdinal("LEVEL_ID"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                            item.LEVEL_DEP = dr.GetDecimal(dr.GetOrdinal("LEVEL_DEP"));
                            
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return item;
        }
        public List<BEREC_COLL_LEVEL> Get_REC_COLL_LEVEL()
        {
            List<BEREC_COLL_LEVEL> lst = new List<BEREC_COLL_LEVEL>();
            BEREC_COLL_LEVEL item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_COLL_LEVEL"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_COLL_LEVEL();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.LEVEL_ID = dr.GetDecimal(dr.GetOrdinal("LEVEL_ID"));
                            item.NMR_ID = dr.GetDecimal(dr.GetOrdinal("NMR_ID"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                            item.LEVEL_DEP = dr.GetDecimal(dr.GetOrdinal("LEVEL_DEP"));
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

        public List<BEREC_COLL_LEVEL> REC_COLL_LEVEL_GET_by_LEVEL_ID(decimal LEVEL_ID)
        {
            List<BEREC_COLL_LEVEL> lst = new List<BEREC_COLL_LEVEL>();
            BEREC_COLL_LEVEL item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_COLL_LEVEL_GET_by_LEVEL_ID"))
                {
                    db.AddInParameter(cm, "@LEVEL_ID", DbType.Decimal, LEVEL_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_COLL_LEVEL();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.LEVEL_ID = dr.GetDecimal(dr.GetOrdinal("LEVEL_ID"));
                            //item.NMR_ID = dr.GetDecimal(dr.GetOrdinal("NMR_ID"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                            item.LEVEL_DEP = dr.GetDecimal(dr.GetOrdinal("LEVEL_DEP"));
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

        public List<BEREC_COLL_LEVEL> REC_COLL_LEVEL_Page(string param, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_COLL_LEVEL_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REC_COLL_LEVEL_GET_Page", param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREC_COLL_LEVEL>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEREC_COLL_LEVEL(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_COLL_LEVEL_Ins(BEREC_COLL_LEVEL en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_COLL_LEVEL_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@NMR_ID", DbType.Decimal, en.NMR_ID);
                db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION);
                db.AddInParameter(oDbCommand, "@LEVEL_DEP", DbType.Decimal, en.LEVEL_DEP);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_COLL_LEVEL_Upd(BEREC_COLL_LEVEL en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_COLL_LEVEL_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, en.LEVEL_ID);
                db.AddInParameter(oDbCommand, "@NMR_ID", DbType.Decimal, en.NMR_ID);
                db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION);
                db.AddInParameter(oDbCommand, "@LEVEL_DEP", DbType.Decimal, en.LEVEL_DEP);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_COLL_LEVEL_Del(decimal LEVEL_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_COLL_LEVEL_Del");
                db.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, LEVEL_ID);
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

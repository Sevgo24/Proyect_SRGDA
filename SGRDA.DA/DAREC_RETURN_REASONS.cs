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
    public class DAREC_RETURN_REASONS
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_RETURN_REASONS> Get_REC_RETURN_REASONS()
        {
            List<BEREC_RETURN_REASONS> lst = new List<BEREC_RETURN_REASONS>();
            BEREC_RETURN_REASONS item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_RETURN_REASONS"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_RETURN_REASONS();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.RET_ID = dr.GetString(dr.GetOrdinal("RET_ID"));
                            item.RET_DES = dr.GetString(dr.GetOrdinal("RET_DES"));

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

        public List<BEREC_RETURN_REASONS> REC_RETURN_REASONS_GET_by_RET_ID(string OWNER, string RET_ID)
        {
            List<BEREC_RETURN_REASONS> lst = new List<BEREC_RETURN_REASONS>();
            BEREC_RETURN_REASONS item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_RETURN_REASONS_GET_by_RET_ID"))
                {
                    db.AddInParameter(cm, "@RET_ID", DbType.String, RET_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_RETURN_REASONS();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.RET_ID = dr.GetString(dr.GetOrdinal("RET_ID"));
                            item.RET_DES = dr.GetString(dr.GetOrdinal("RET_DES"));

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

        public List<BEREC_RETURN_REASONS> REC_RETURN_REASONS_Page(string param, int st, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_RETURN_REASONS_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            //Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REC_RETURN_REASONS_GET_Page", param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREC_RETURN_REASONS>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_RETURN_REASONS(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_RETURN_REASONS_Ins(BEREC_RETURN_REASONS en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_RETURN_REASONS_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@RET_ID", DbType.String, en.RET_ID.ToUpper());
                db.AddInParameter(oDbCommand, "@RET_DES", DbType.String, en.RET_DES.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_RETURN_REASONS_Upd(BEREC_RETURN_REASONS en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_RETURN_REASONS_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@RET_ID", DbType.String, en.RET_ID.ToUpper());
                db.AddInParameter(oDbCommand, "@RET_DES", DbType.String, en.RET_DES.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_RETURN_REASONS_Del(string OWNER, string RET_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_RETURN_REASONS_Del");
                db.AddInParameter(oDbCommand, "@RET_ID", DbType.String, RET_ID);
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

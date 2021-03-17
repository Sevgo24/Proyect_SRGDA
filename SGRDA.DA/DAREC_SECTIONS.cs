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
    public class DAREC_SECTIONS
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_SECTIONS> Get_REC_SECTIONS()
        {
            List<BEREC_SECTIONS> lst = new List<BEREC_SECTIONS>();
            BEREC_SECTIONS item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_SECTIONS"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_SECTIONS();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.SEC_ID = dr.GetString(dr.GetOrdinal("SEC_ID"));
                            item.SEC_DESC = dr.GetString(dr.GetOrdinal("SEC_DESC"));

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

        public List<BEREC_SECTIONS> REC_SECTIONS_GET_by_SEC_ID(string SEC_ID)
        {
            List<BEREC_SECTIONS> lst = new List<BEREC_SECTIONS>();
            BEREC_SECTIONS item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_SECTIONS_GET_by_SEC_ID"))
                {
                    db.AddInParameter(cm, "@SEC_ID", DbType.String, SEC_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_SECTIONS();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.SEC_ID = dr.GetString(dr.GetOrdinal("SEC_ID"));
                            item.SEC_DESC = dr.GetString(dr.GetOrdinal("SEC_DESC"));
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

        public List<BEREC_SECTIONS> REC_SECTIONS_Page(string param, int st, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_SECTIONS_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            //Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REC_SECTIONS_GET_Page", param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREC_SECTIONS>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_SECTIONS(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_SECTIONS_Ins(BEREC_SECTIONS en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_SECTIONS_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@SEC_ID", DbType.String, en.SEC_ID.ToUpper());
                db.AddInParameter(oDbCommand, "@SEC_DESC", DbType.String, en.SEC_DESC.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_SECTIONS_Upd(BEREC_SECTIONS en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_SECTIONS_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@SEC_ID", DbType.String, en.SEC_ID.ToUpper());
                db.AddInParameter(oDbCommand, "@SEC_DESC", DbType.String, en.SEC_DESC.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_SECTIONS_Del(string OWNER, string SEC_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_SECTIONS_Del");
                db.AddInParameter(oDbCommand, "@SEC_ID", DbType.String, SEC_ID);
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

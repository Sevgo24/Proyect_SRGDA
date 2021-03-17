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
    public class DAREC_MOV_TYPE
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_MOV_TYPE> Get_REC_MOV_TYPE()
        {
            List<BEREC_MOV_TYPE> lst = new List<BEREC_MOV_TYPE>();
            BEREC_MOV_TYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_MOV_TYPE"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_MOV_TYPE();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.MOV_TYPE = dr.GetString(dr.GetOrdinal("MOV_TYPE"));
                            item.MOV_DESC = dr.GetString(dr.GetOrdinal("MOV_DESC"));
                            item.MOV_SIGN = dr.GetString(dr.GetOrdinal("MOV_SIGN"));

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

        public List<BEREC_MOV_TYPE> REC_MOV_TYPE_by_MOV_TYPE(string OWNER, string MOV_TYPE)
        {
            List<BEREC_MOV_TYPE> lst = new List<BEREC_MOV_TYPE>();
            BEREC_MOV_TYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_MOV_TYPE_GET_by_MOV_TYPE"))
                {
                    db.AddInParameter(cm, "@MOV_TYPE", DbType.String, MOV_TYPE);
                    db.AddInParameter(cm, "@OWNER", DbType.String, OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_MOV_TYPE();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.MOV_TYPE = dr.GetString(dr.GetOrdinal("MOV_TYPE"));
                            item.MOV_DESC = dr.GetString(dr.GetOrdinal("MOV_DESC"));
                            item.MOV_SIGN = dr.GetString(dr.GetOrdinal("MOV_SIGN"));

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

        public List<BEREC_MOV_TYPE> REC_MOV_TYPE_Page(string param, int st, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_MOV_TYPE_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            //Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REC_MOV_TYPE_GET_Page", param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREC_MOV_TYPE>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_MOV_TYPE(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_MOV_TYPE_Ins(BEREC_MOV_TYPE en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_MOV_TYPE_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@MOV_TYPE", DbType.String, en.MOV_TYPE.ToUpper());
                db.AddInParameter(oDbCommand, "@MOV_DESC", DbType.String, en.MOV_DESC.ToUpper());
                db.AddInParameter(oDbCommand, "@MOV_SIGN ", DbType.String, en.MOV_SIGN.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_MOV_TYPE_Upd(BEREC_MOV_TYPE en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_MOV_TYPE_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@MOV_TYPE", DbType.String, en.MOV_TYPE.ToUpper());
                db.AddInParameter(oDbCommand, "@MOV_DESC", DbType.String, en.MOV_DESC.ToUpper());
                db.AddInParameter(oDbCommand, "@MOV_SIGN ", DbType.String, en.MOV_SIGN.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_MOV_TYPE_Del(string OWNER, string MOV_TYPE)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_MOV_TYPE_Del");
                db.AddInParameter(oDbCommand, "@MOV_TYPE", DbType.String, MOV_TYPE);
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

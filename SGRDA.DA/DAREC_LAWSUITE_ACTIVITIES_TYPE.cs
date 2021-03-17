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
    public class DAREC_LAWSUITE_ACTIVITIES_TYPE
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_LAWSUITE_ACTIVITIES_TYPE> Get_REC_LAWSUITE_ACTIVITIES_TYPE()
        {
            List<BEREC_LAWSUITE_ACTIVITIES_TYPE> lst = new List<BEREC_LAWSUITE_ACTIVITIES_TYPE>();
            BEREC_LAWSUITE_ACTIVITIES_TYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_LAWSUITE_ACTIVITIES_TYPE"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_LAWSUITE_ACTIVITIES_TYPE();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.LAWS_ATY = dr.GetString(dr.GetOrdinal("LAWS_ATY"));
                            item.LAWS_ATDESC = dr.GetString(dr.GetOrdinal("LAWS_ATDESC"));

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

        public List<BEREC_LAWSUITE_ACTIVITIES_TYPE> REC_LAWSUITE_ACTIVITIES_TYPE_GET_by_LAWS_ATY(string OWNER, string LAWS_ATY)
        {
            List<BEREC_LAWSUITE_ACTIVITIES_TYPE> lst = new List<BEREC_LAWSUITE_ACTIVITIES_TYPE>();
            BEREC_LAWSUITE_ACTIVITIES_TYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_LAWSUITE_ACTIVITIES_TYPE_GET_by_LAWS_ATY"))
                {
                    db.AddInParameter(cm, "@LAWS_ATY", DbType.String, LAWS_ATY);
                    db.AddInParameter(cm, "@OWNER", DbType.String, OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_LAWSUITE_ACTIVITIES_TYPE();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.LAWS_ATY = dr.GetString(dr.GetOrdinal("LAWS_ATY"));
                            item.LAWS_ATDESC = dr.GetString(dr.GetOrdinal("LAWS_ATDESC"));
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

        public List<BEREC_LAWSUITE_ACTIVITIES_TYPE> REC_LAWSUITE_ACTIVITIES_TYPE_Page(string param, int st, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_LAWSUITE_ACTIVITIES_TYPE_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEREC_LAWSUITE_ACTIVITIES_TYPE>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_LAWSUITE_ACTIVITIES_TYPE(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_LAWSUITE_ACTIVITIES_TYPE_Ins(BEREC_LAWSUITE_ACTIVITIES_TYPE en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_LAWSUITE_ACTIVITIES_TYPE_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@LAWS_ATY", DbType.String, en.LAWS_ATY.ToUpper());
                db.AddInParameter(oDbCommand, "@LAWS_ATDESC", DbType.String, en.LAWS_ATDESC.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_LAWSUITE_ACTIVITIES_TYPE_Upd(BEREC_LAWSUITE_ACTIVITIES_TYPE en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_LAWSUITE_ACTIVITIES_TYPE_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@LAWS_ATY", DbType.String, en.LAWS_ATY.ToUpper());
                db.AddInParameter(oDbCommand, "@LAWS_ATDESC", DbType.String, en.LAWS_ATDESC.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_LAWSUITE_ACTIVITIES_TYPE_Del(string OWNER, string LAWS_ATY)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_LAWSUITE_ACTIVITIES_TYPE_Del");
                db.AddInParameter(oDbCommand, "@LAWS_ATY", DbType.String, LAWS_ATY);
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

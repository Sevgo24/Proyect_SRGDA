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
    public class DAREC_CAPACITY_TYPE
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_CAPACITY_TYPE> Get_REC_CAPACITY_TYPE()
        {
            List<BEREC_CAPACITY_TYPE> lst = new List<BEREC_CAPACITY_TYPE>();
            BEREC_CAPACITY_TYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_CAPACITY_TYPE"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_CAPACITY_TYPE();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.CAP_ID = dr.GetString(dr.GetOrdinal("CAP_ID"));
                            item.CAP_DESC = dr.GetString(dr.GetOrdinal("CAP_DESC"));
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
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

        public List<BEREC_CAPACITY_TYPE> REC_CAPACITY_TYPE_GET_by_CAP_ID(string CAP_ID)
        {
            List<BEREC_CAPACITY_TYPE> lst = new List<BEREC_CAPACITY_TYPE>();
            BEREC_CAPACITY_TYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_CAPACITY_TYPE_GET_by_CAP_ID"))
                {
                    db.AddInParameter(cm, "@CAP_ID", DbType.String, CAP_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_CAPACITY_TYPE();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.CAP_ID = dr.GetString(dr.GetOrdinal("CAP_ID"));
                            item.CAP_DESC = dr.GetString(dr.GetOrdinal("CAP_DESC"));
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

        public List<BEREC_CAPACITY_TYPE> REC_CAPACITY_TYPE_Page(string param, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_CAPACITY_TYPE_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REC_CAPACITY_TYPE_GET_Page", param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREC_CAPACITY_TYPE>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEREC_CAPACITY_TYPE(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_CAPACITY_TYPE_Ins(BEREC_CAPACITY_TYPE en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_CAPACITY_TYPE_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@CAP_ID", DbType.String, en.CAP_ID);
                db.AddInParameter(oDbCommand, "@CAP_DESC", DbType.String, en.CAP_DESC);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_CAPACITY_TYPE_Upd(BEREC_CAPACITY_TYPE en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_CAPACITY_TYPE_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@CAP_ID", DbType.String, en.CAP_ID);
                db.AddInParameter(oDbCommand, "@CAP_DESC", DbType.String, en.CAP_DESC);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_CAPACITY_TYPE_Del(string CAP_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_CAPACITY_TYPE_Del");
                db.AddInParameter(oDbCommand, "@CAP_ID", DbType.String, CAP_ID);
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

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
    public class DAREF_SOCIETY_TYPE
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREF_SOCIETY_TYPE> Get_REF_SOCIETY_TYPE()
        {
            List<BEREF_SOCIETY_TYPE> lst = new List<BEREF_SOCIETY_TYPE>();
            BEREF_SOCIETY_TYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REF_SOCIETY_TYPE"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_SOCIETY_TYPE();
                            item.SOC_TYPE = dr.GetString(dr.GetOrdinal("SOC_TYPE"));
                            item.SOC_DESC = dr.GetString(dr.GetOrdinal("SOC_DESC"));                      

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

        public List<BEREF_SOCIETY_TYPE> REF_SOCIETY_TYPE_GET_by_SOC_TYPE(string SOC_TYPE)
        {
            List<BEREF_SOCIETY_TYPE> lst = new List<BEREF_SOCIETY_TYPE>();
            BEREF_SOCIETY_TYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_SOCIETY_TYPE_GET_by_SOC_TYPE"))
                {
                    db.AddInParameter(cm, "@SOC_TYPE", DbType.String, SOC_TYPE);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_SOCIETY_TYPE();
                            item.SOC_TYPE = dr.GetString(dr.GetOrdinal("SOC_TYPE"));
                            item.SOC_DESC = dr.GetString(dr.GetOrdinal("SOC_DESC"));
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

        public List<BEREF_SOCIETY_TYPE> REF_SOCIETY_TYPE_Page(string param, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REF_SOCIETY_TYPE_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REF_SOCIETY_TYPE_GET_Page", param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREF_SOCIETY_TYPE>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEREF_SOCIETY_TYPE(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool RREF_SOCIETY_TYPE_Ins(BEREF_SOCIETY_TYPE en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_SOCIETY_TYPE_Ins");
                db.AddInParameter(oDbCommand, "@SOC_TYPE", DbType.String, en.SOC_TYPE);
                db.AddInParameter(oDbCommand, "@SOC_DESC", DbType.String, en.SOC_DESC);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REF_SOCIETY_TYPE_Upd(BEREF_SOCIETY_TYPE en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_SOCIETY_TYPE_Upd");
                db.AddInParameter(oDbCommand, "@SOC_TYPE", DbType.String, en.SOC_TYPE);
                db.AddInParameter(oDbCommand, "@SOC_DESC", DbType.String, en.SOC_DESC);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REF_SOCIETY_TYPE_Del(string SOC_TYPE)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_SOCIETY_TYPE_Del");
                db.AddInParameter(oDbCommand, "@SOC_TYPE", DbType.String, SOC_TYPE);
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

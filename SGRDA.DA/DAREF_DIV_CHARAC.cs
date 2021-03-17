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
    public class DAREF_DIV_CHARAC
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREF_DIV_CHARAC> usp_Get_REF_DIV_CHARAC()
        {
            List<BEREF_DIV_CHARAC> lst = new List<BEREF_DIV_CHARAC>();
            BEREF_DIV_CHARAC item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_DIV_CHARAC_GET"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_DIV_CHARAC();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.DAC_ID = dr.GetString(dr.GetOrdinal("DAC_ID"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));

                            DateTime LOG_DATE_CREAT;
                            bool isDateCREAT = DateTime.TryParse(dr["LOG_DATE_CREAT"].ToString(), out LOG_DATE_CREAT);
                            item.LOG_DATE_CREAT = LOG_DATE_CREAT;

                            DateTime LOG_DATE_UPDATE;
                            bool isDateUPD = DateTime.TryParse(dr["LOG_DATE_UPDATE"].ToString(), out LOG_DATE_UPDATE);
                            item.LOG_DATE_UPDATE = LOG_DATE_UPDATE;

                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                            item.LOG_USER_UPDATE = (item.LOG_USER_UPDATE == null) ? string.Empty : item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

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

        public List<BEREF_DIV_CHARAC> usp_Get_REF_DIV_TYPE_by_DAC_ID(string DAC_ID)
        {
            List<BEREF_DIV_CHARAC> lst = new List<BEREF_DIV_CHARAC>();
            BEREF_DIV_CHARAC item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_DIV_CHARAC_by_DAC_ID"))
                {
                    db.AddInParameter(cm, "@DAC_ID", DbType.String, DAC_ID);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_DIV_CHARAC();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.DAC_ID = dr.GetString(dr.GetOrdinal("DAC_ID"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));

                            DateTime LOG_DATE_CREAT;
                            bool isDateCREAT = DateTime.TryParse(dr["LOG_DATE_CREAT"].ToString(), out LOG_DATE_CREAT);
                            item.LOG_DATE_CREAT = LOG_DATE_CREAT;

                            DateTime LOG_DATE_UPDATE;
                            bool isDateUPD = DateTime.TryParse(dr["LOG_DATE_UPDATE"].ToString(), out LOG_DATE_UPDATE);
                            item.LOG_DATE_UPDATE = LOG_DATE_UPDATE;

                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                            item.LOG_USER_UPDATE = (item.LOG_USER_UPDATE == null) ? string.Empty : item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

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

        public List<BEREF_DIV_CHARAC> usp_REF_DIV_CHARAC_Page(string param, int st, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REF_DIV_CHARAC_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REF_DIV_CHARAC_GET_Page", param, st, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREF_DIV_CHARAC>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEREF_DIV_CHARAC(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REF_DIV_CHARAC_Ins(BEREF_DIV_CHARAC en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_DIV_CHARAC_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
                db.AddInParameter(oDbCommand, "@DAC_ID", DbType.String, en.DAC_ID);
                db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REF_DIV_CHARAC_Upd(BEREF_DIV_CHARAC en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_DIV_CHARAC_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@DAC_ID", DbType.String, en.DAC_ID);
                db.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REF_DIV_CHARAC_Del(string DAC_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_DIV_CHARAC_Del");
                db.AddInParameter(oDbCommand, "@DAC_ID", DbType.String, DAC_ID);
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BEREF_DIV_CHARAC> ListarTipoCaracteristicas(string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_TIPO_CARACTERISTICAS");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);

            var lista = new List<BEREF_DIV_CHARAC>();
            BEREF_DIV_CHARAC obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BEREF_DIV_CHARAC();
                    obs.DAC_ID = dr.GetString(dr.GetOrdinal("DAC_ID"));
                    obs.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                    lista.Add(obs);
                }
            }
            return lista;
        }
    }
}

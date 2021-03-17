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
    public class DAREC_ECON_ACTIVITIES
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_ECON_ACTIVITIES> Get_REC_ECON_ACTIVITIES()
        {
            List<BEREC_ECON_ACTIVITIES> lst = new List<BEREC_ECON_ACTIVITIES>();
            BEREC_ECON_ACTIVITIES item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_ECON_ACTIVITIES"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        if (dr.FieldCount > 0)
                        {
                            item = new BEREC_ECON_ACTIVITIES();
                            item.ECON_ID = "0";
                            item.ECON_DESC = "--SELECCIONE--";
                            lst.Add(item);

                            while (dr.Read())
                            {
                                item = new BEREC_ECON_ACTIVITIES();
                                item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                                item.ECON_ID = dr.GetString(dr.GetOrdinal("ECON_ID"));
                                item.ECON_DESC = dr.GetString(dr.GetOrdinal("ECON_DESC"));
                                item.ECON_BELONGS = dr.GetString(dr.GetOrdinal("ECON_BELONGS"));
                                lst.Add(item);
                            }
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

        public List<BEREC_ECON_ACTIVITIES> REC_ECON_ACTIVITIES_GET_by_ECON_ID(string ECON_ID)
        {
            List<BEREC_ECON_ACTIVITIES> lst = new List<BEREC_ECON_ACTIVITIES>();
            BEREC_ECON_ACTIVITIES item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_ECON_ACTIVITIES_GET_by_ECON_ID"))
                {
                    db.AddInParameter(cm, "@ECON_ID", DbType.String, ECON_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_ECON_ACTIVITIES();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.ECON_ID = dr.GetString(dr.GetOrdinal("ECON_ID"));
                            item.ECON_DESC = dr.GetString(dr.GetOrdinal("ECON_DESC"));
                            item.ECON_BELONGS = dr.GetString(dr.GetOrdinal("ECON_BELONGS"));
                            item.ECON_ID_Bellong = dr.GetString(dr.GetOrdinal("ECON_ID_Bellong"));
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

        public List<BEREC_ECON_ACTIVITIES> REC_ECON_ACTIVITIES_Page(string owner, string param, int st, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_ECON_ACTIVITIES_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEREC_ECON_ACTIVITIES>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_ECON_ACTIVITIES(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_ECON_ACTIVITIES_Ins(BEREC_ECON_ACTIVITIES en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_ECON_ACTIVITIES_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@ECON_ID", DbType.String, en.ECON_ID);
                db.AddInParameter(oDbCommand, "@ECON_DESC", DbType.String, en.ECON_DESC.ToUpper());
                db.AddInParameter(oDbCommand, "@ECON_BELONGS", DbType.String, en.ECON_BELONGS);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_ECON_ACTIVITIES_Upd(BEREC_ECON_ACTIVITIES en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_ECON_ACTIVITIES_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@ECON_ID", DbType.String, en.ECON_ID);
                db.AddInParameter(oDbCommand, "@ECON_DESC", DbType.String, en.ECON_DESC.ToUpper());
                db.AddInParameter(oDbCommand, "@ECON_BELONGS", DbType.String, en.ECON_BELONGS);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_ECON_ACTIVITIES_Del(string ECON_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_ECON_ACTIVITIES_Del");
                db.AddInParameter(oDbCommand, "@ECON_ID", DbType.String, ECON_ID);
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BEREC_ECON_ACTIVITIES> ListarActividadEcon(string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_ACTIVIDAD_ECON");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);

            var lista = new List<BEREC_ECON_ACTIVITIES>();
            BEREC_ECON_ACTIVITIES obs;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    obs = new BEREC_ECON_ACTIVITIES();
                    obs.ECON_ID = Convert.ToString(reader["ECON_ID"]);
                    obs.ECON_DESC = Convert.ToString(reader["ECON_DESC"]);
                    lista.Add(obs);
                }
            }
            return lista;
        }
    }
}

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
    public class DAREC_MOD_IMPACT
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_MOD_IMPACT> Get_REC_MOD_IMPACT()
        {
            List<BEREC_MOD_IMPACT> lst = new List<BEREC_MOD_IMPACT>();
            BEREC_MOD_IMPACT item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_MOD_IMPACT"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_MOD_IMPACT();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.MOD_INCID = dr.GetString(dr.GetOrdinal("MOD_INCID"));
                            item.MOD_IDESC = dr.GetString(dr.GetOrdinal("MOD_IDESC"));
                            item.MOD_IDET = dr.GetString(dr.GetOrdinal("MOD_IDET"));
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

        public List<BEREC_MOD_IMPACT> REC_MOD_IMPACT_GET_by_MOD_INCID(string MOD_INCID)
        {
            List<BEREC_MOD_IMPACT> lst = new List<BEREC_MOD_IMPACT>();
            BEREC_MOD_IMPACT item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_MOD_IMPACT_GET_by_MOD_INCID"))
                {
                    db.AddInParameter(cm, "@MOD_INCID", DbType.String, MOD_INCID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_MOD_IMPACT();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.MOD_INCID = dr.GetString(dr.GetOrdinal("MOD_INCID"));
                            item.MOD_IDESC = dr.GetString(dr.GetOrdinal("MOD_IDESC"));
                            item.MOD_IDET = dr.GetString(dr.GetOrdinal("MOD_IDET"));
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

        public List<BEREC_MOD_IMPACT> REC_MOD_IMPACT_Page(string param, int st, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_MOD_IMPACT_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));
            var lista = new List<BEREC_MOD_IMPACT>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_MOD_IMPACT(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_MOD_IMPACT_Ins(BEREC_MOD_IMPACT en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_MOD_IMPACT_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@MOD_INCID", DbType.String, en.MOD_INCID.ToUpper());
                db.AddInParameter(oDbCommand, "@MOD_IDESC", DbType.String, en.MOD_IDESC.ToUpper());
                db.AddInParameter(oDbCommand, "@MOD_IDET", DbType.String, en.MOD_IDET.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_MOD_IMPACT_Upd(BEREC_MOD_IMPACT en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_MOD_IMPACT_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@MOD_INCID", DbType.String, en.MOD_INCID.ToUpper());
                db.AddInParameter(oDbCommand, "@MOD_IDESC", DbType.String, en.MOD_IDESC.ToUpper());
                db.AddInParameter(oDbCommand, "@MOD_IDET", DbType.String, en.MOD_IDET.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Eliminar(BEREC_MOD_IMPACT Incidencia)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_MOD_IMPACT_Del");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Incidencia.OWNER);
            db.AddInParameter(oDbCommand, "@MOD_INCID", DbType.String, Incidencia.MOD_INCID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, Incidencia.LOG_USER_UPDAT);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}

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
    public class DAREC_RATE_FREQUENCY
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_RATE_FREQUENCY> Get_REC_RATE_FREQUENCY()
        {
            List<BEREC_RATE_FREQUENCY> lst = new List<BEREC_RATE_FREQUENCY>();
            BEREC_RATE_FREQUENCY item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REC_RATE_FREQUENCY"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_RATE_FREQUENCY();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.RAT_FID = dr.GetDecimal(dr.GetOrdinal("RAT_FID"));
                            item.RAT_FDESC = dr.GetString(dr.GetOrdinal("RAT_FDESC"));
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

        public List<BEREC_RATE_FREQUENCY> REC_RATE_FREQUENCY_GET_by_RAT_FID(decimal RAT_FID)
        {
            List<BEREC_RATE_FREQUENCY> lst = new List<BEREC_RATE_FREQUENCY>();
            BEREC_RATE_FREQUENCY item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_RATE_FREQUENCY_GET_by_RAT_FID"))
                {
                    db.AddInParameter(cm, "@RAT_FID", DbType.Decimal, RAT_FID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_RATE_FREQUENCY();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.RAT_FID = dr.GetDecimal(dr.GetOrdinal("RAT_FID"));
                            item.RAT_FDESC = dr.GetString(dr.GetOrdinal("RAT_FDESC"));
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

        public List<BEREC_RATE_FREQUENCY> REC_RATE_FREQUENCY_Page(string owner, string param, int st, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_RATE_FREQUENCY_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            //Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REC_RATE_FREQUENCY_GET_Page", param,owner, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREC_RATE_FREQUENCY>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_RATE_FREQUENCY(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public int REC_RATE_FREQUENCY_Ins(BEREC_RATE_FREQUENCY en)
        {
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_RATE_FREQUENCY_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddOutParameter(oDbCommand, "@RAT_FID", DbType.Decimal, Convert.ToInt32(en.RAT_FID));
                db.AddInParameter(oDbCommand, "@RAT_FDESC", DbType.String, en.RAT_FDESC.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                int n = db.ExecuteNonQuery(oDbCommand);
                int id = Convert.ToInt32(db.GetParameterValue(oDbCommand, "@RAT_FID"));
                return id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool REC_RATE_FREQUENCY_Upd(BEREC_RATE_FREQUENCY en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_RATE_FREQUENCY_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@RAT_FID", DbType.Decimal, en.RAT_FID);
                db.AddInParameter(oDbCommand, "@RAT_FDESC", DbType.String, en.RAT_FDESC.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Eliminar(BEREC_RATE_FREQUENCY en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_RATE_FREQUENCY_Del");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@RAT_FID", DbType.Decimal, en.RAT_FID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEREC_RATE_FREQUENCY> Listar(string owner)
        {
            List<BEREC_RATE_FREQUENCY> lst = new List<BEREC_RATE_FREQUENCY>();
            BEREC_RATE_FREQUENCY item = null;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_TEMPORALIDAD"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEREC_RATE_FREQUENCY();
                        item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        item.RAT_FID = dr.GetDecimal(dr.GetOrdinal("RAT_FID"));
                        item.RAT_FDESC = dr.GetString(dr.GetOrdinal("RAT_FDESC"));
                        lst.Add(item);
                    }
                }
            }

            return lst;
        }

        public BEREC_RATE_FREQUENCY ObtenerXTarifa(decimal codTarifa)
        {
            BEREC_RATE_FREQUENCY item = null;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_TEMPORALIDAD_TARIFA"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(cm, "@RAT_FID", DbType.Decimal, codTarifa);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEREC_RATE_FREQUENCY();
                        item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        item.RAT_FID = dr.GetDecimal(dr.GetOrdinal("RAT_FID"));
                        item.RAT_FDESC = dr.GetString(dr.GetOrdinal("RAT_FDESC"));

                    }
                }
            }
            return item;
        }

        public BEREC_RATE_FREQUENCY Obtener(decimal codTarifa)
        {
            BEREC_RATE_FREQUENCY item = null;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_TARIFA"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(cm, "@RAT_FID", DbType.Decimal, codTarifa);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    if(dr.Read())
                    {
                        item = new BEREC_RATE_FREQUENCY();
                        item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        item.RAT_FID = dr.GetDecimal(dr.GetOrdinal("RAT_FID"));
                        item.RAT_FDESC = dr.GetString(dr.GetOrdinal("RAT_FDESC"));

                    }
                }
            }
            return item;
        }

        public List<BEREC_RATE_FREQUENCY> ListarPeriodocidad(string owner)
        {
            List<BEREC_RATE_FREQUENCY> lista = null;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_PERIODOCIDAD"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    BEREC_RATE_FREQUENCY item = null;
                    lista = new List<BEREC_RATE_FREQUENCY>();
                    while (dr.Read())
                    {
                        item = new BEREC_RATE_FREQUENCY();
                        item.RAT_FID = dr.GetDecimal(dr.GetOrdinal("RAT_FID"));
                        item.RAT_FDESC = dr.GetString(dr.GetOrdinal("RAT_FDESC"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }
    }
}

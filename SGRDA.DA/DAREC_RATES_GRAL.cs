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
    public class DAREC_RATES_GRAL
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_RATES_GRAL> GET_REC_RATES_GRAL(decimal codTarifa)
        {
            List<BEREC_RATES_GRAL> lst = new List<BEREC_RATES_GRAL>();
            BEREC_RATES_GRAL item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_REC_RATES_GRAL"))
                {
                    db.AddInParameter(cm, "@RATE_ID", DbType.Decimal, codTarifa);
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_RATES_GRAL();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                            item.RATE_DESC = dr.GetString(dr.GetOrdinal("RATE_DESC"));
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

        public List<BEREC_RATES_GRAL> obtenerTarifaAsociada(decimal codModalidad, decimal? codTemp)
        {
            BEREC_RATES_GRAL item = null;
            List<BEREC_RATES_GRAL> lista = null;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_TARIFA_X_MODALIDAD"))
            {
                db.AddInParameter(cm, "@MOD_ID", DbType.Decimal, codModalidad);
                db.AddInParameter(cm, "@RAT_FID", DbType.Decimal, codTemp);
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    lista = new List<BEREC_RATES_GRAL>();
                    while(dr.Read())
                    {
                        item = new BEREC_RATES_GRAL();
                        item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        item.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                        item.RATE_DESC = dr.GetString(dr.GetOrdinal("RATE_DESC"));
                        item.RATE_DESC = dr.GetString(dr.GetOrdinal("NAME"));
                        item.RAT_FID = dr.GetDecimal(dr.GetOrdinal("RAT_FID"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }

        public List<BEREC_RATES_GRAL> ListarTarifasPage(string owner, decimal IdTarifa, string moneda, string moduso,
                                                        string incidencia, string sociedad, string repertorio,
                                                        decimal IdModalidad, int st, string descripcion, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_CONSULTA_TARIFA");
            db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, IdTarifa);
            db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, moneda);
            db.AddInParameter(oDbCommand, "@MOD_USAGE", DbType.String, moduso);
            db.AddInParameter(oDbCommand, "@MOD_INCID", DbType.String, incidencia);
            db.AddInParameter(oDbCommand, "@MOD_SOC", DbType.String, sociedad);
            db.AddInParameter(oDbCommand, "@MOD_REPER", DbType.String, repertorio);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, IdModalidad);
            db.AddInParameter(oDbCommand, "@RATE_DESC", DbType.String, descripcion);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEREC_RATES_GRAL>();
            var item = new BEREC_RATES_GRAL();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEREC_RATES_GRAL();
                    item.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                    item.RATE_LDESC = dr.GetString(dr.GetOrdinal("RATE_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RATE_START")))
                        item.RATE_START = dr.GetDateTime(dr.GetOrdinal("RATE_START"));
                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ESTADO = "ACTIVO";
                    else
                        item.ESTADO = "INACTIVO";
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_DEC")))
                        item.MOD_DEC = dr.GetString(dr.GetOrdinal("MOD_DEC"));
                    item.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(item);
                }
            }
            return lista;
        }

        public int Insertar(BEREC_RATES_GRAL tarifa)
        {
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_MANT_TARIFA");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, tarifa.OWNER);
                db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, tarifa.MOD_ID);
                db.AddInParameter(oDbCommand, "@RATE_DESC", DbType.String, tarifa.RATE_DESC.ToUpper());
                db.AddInParameter(oDbCommand, "@NAME", DbType.String, tarifa.NAME.ToUpper());
                db.AddInParameter(oDbCommand, "@RATE_START", DbType.DateTime, tarifa.RATE_START);
                db.AddInParameter(oDbCommand, "@RATE_END", DbType.DateTime, tarifa.RATE_END);
                db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, tarifa.CUR_ALPHA);
                db.AddInParameter(oDbCommand, "@RATE_OBSERV", DbType.String, tarifa.RATE_OBSERV);
                db.AddInParameter(oDbCommand, "@RAT_FID", DbType.Decimal, tarifa.RAT_FID);
                db.AddInParameter(oDbCommand, "@RATE_ACCOUNT", DbType.String, tarifa.RATE_ACCOUNT);
                db.AddInParameter(oDbCommand, "@RATE_DREPERT", DbType.String, tarifa.RATE_DREPERT);
                db.AddInParameter(oDbCommand, "@RATE_NVAR", DbType.Decimal, tarifa.RATE_NVAR);
                db.AddInParameter(oDbCommand, "@RATE_NCAL", DbType.Decimal, tarifa.RATE_NCAL);
                db.AddInParameter(oDbCommand, "@RATE_FORMULA", DbType.String, tarifa.RATE_FORMULA.ToUpper());
                db.AddInParameter(oDbCommand, "@RATE_MINIMUM", DbType.String, tarifa.RATE_MINIMUM.ToUpper());
                db.AddInParameter(oDbCommand, "@RATE_FTIPO", DbType.String, tarifa.RATE_FTIPO);
                db.AddInParameter(oDbCommand, "@RATE_MTIPO", DbType.String, tarifa.RATE_MTIPO);
                db.AddInParameter(oDbCommand, "@RATE_FDECI", DbType.Decimal, tarifa.RATE_FDECI);
                db.AddInParameter(oDbCommand, "@RATE_MDECI", DbType.Decimal, tarifa.RATE_MDECI);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, tarifa.LOG_USER_CREAT);
                db.AddInParameter(oDbCommand, "@RATE_ID_PREC", DbType.Decimal, tarifa.RATE_ID_PREC);
                db.AddInParameter(oDbCommand, "@RATE_ID_ORIG", DbType.Decimal, tarifa.RATE_ID_ORIG);
                db.AddInParameter(oDbCommand, "@RATE_REDONDEO", DbType.Int32, tarifa.RATE_REDONDEO);
                db.AddOutParameter(oDbCommand, "@RATE_ID", DbType.Decimal, Convert.ToInt32(tarifa.RATE_ID));

                int n = db.ExecuteNonQuery(oDbCommand);
                int id = Convert.ToInt32(db.GetParameterValue(oDbCommand, "@RATE_ID"));
                return id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public BEREC_RATES_GRAL Obtener(string owner, decimal idRate)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_MANT_TARIFA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, idRate);
            BEREC_RATES_GRAL ent = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BEREC_RATES_GRAL();
                    ent.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                    ent.RATE_DESC = dr.GetString(dr.GetOrdinal("RATE_DESC"));
                    ent.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));
                    ent.MOD_DEC = dr.GetString(dr.GetOrdinal("MOD_DEC"));
                    ent.RATE_START = dr.GetDateTime(dr.GetOrdinal("RATE_START"));
                    ent.RATE_END = dr.GetDateTime(dr.GetOrdinal("RATE_END"));
                    ent.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RATE_OBSERV")))
                        ent.RATE_OBSERV = dr.GetString(dr.GetOrdinal("RATE_OBSERV"));
                    ent.RAT_FID = dr.GetDecimal(dr.GetOrdinal("RAT_FID"));
                    ent.RATE_ACCOUNT = dr.GetString(dr.GetOrdinal("RATE_ACCOUNT"));
                    ent.RATE_DREPERT = dr.GetString(dr.GetOrdinal("RATE_DREPERT"));
                    ent.RATE_NVAR = dr.GetDecimal(dr.GetOrdinal("RATE_NVAR"));
                    ent.RATE_NCAL = dr.GetDecimal(dr.GetOrdinal("RATE_NCAL"));
                    ent.RATE_FORMULA = dr.GetString(dr.GetOrdinal("RATE_FORMULA"));
                    ent.RATE_MINIMUM = dr.GetString(dr.GetOrdinal("RATE_MINIMUM"));
                    ent.RATE_FTIPO = dr.GetString(dr.GetOrdinal("RATE_FTIPO"));
                    ent.RATE_MTIPO = dr.GetString(dr.GetOrdinal("RATE_MTIPO"));
                    ent.RATE_FDECI = dr.GetDecimal(dr.GetOrdinal("RATE_FDECI"));
                    ent.RATE_MDECI = dr.GetDecimal(dr.GetOrdinal("RATE_MDECI"));
                    ent.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    ent.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        ent.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RATE_ID_ORIG")))
                        ent.RATE_ID_ORIG = dr.GetDecimal(dr.GetOrdinal("RATE_ID_ORIG"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RATE_ID_PREC")))
                        ent.RATE_ID_PREC = dr.GetDecimal(dr.GetOrdinal("RATE_ID_PREC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RATE_REDONDEO")))
                        ent.RATE_REDONDEO = dr.GetInt32(dr.GetOrdinal("RATE_REDONDEO"));
                }
            }
            return ent;
        }

        public int Actualizar(BEREC_RATES_GRAL tarifa)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_MANT_TARIFA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, tarifa.OWNER);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, tarifa.RATE_ID);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, tarifa.MOD_ID);
            db.AddInParameter(oDbCommand, "@RATE_DESC", DbType.String, tarifa.RATE_DESC.ToUpper());
            db.AddInParameter(oDbCommand, "@NAME", DbType.String, tarifa.NAME.ToUpper());
            db.AddInParameter(oDbCommand, "@RATE_START", DbType.DateTime, tarifa.RATE_START);
            db.AddInParameter(oDbCommand, "@RATE_END", DbType.DateTime, tarifa.RATE_END);
            db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, tarifa.CUR_ALPHA);
            db.AddInParameter(oDbCommand, "@RATE_OBSERV", DbType.String, tarifa.RATE_OBSERV);
            db.AddInParameter(oDbCommand, "@RAT_FID", DbType.Decimal, tarifa.RAT_FID);
            db.AddInParameter(oDbCommand, "@RATE_ACCOUNT", DbType.String, tarifa.RATE_ACCOUNT);
            db.AddInParameter(oDbCommand, "@RATE_DREPERT", DbType.String, tarifa.RATE_DREPERT);
            db.AddInParameter(oDbCommand, "@RATE_NVAR", DbType.Decimal, tarifa.RATE_NVAR);
            db.AddInParameter(oDbCommand, "@RATE_NCAL", DbType.Decimal, tarifa.RATE_NCAL);
            db.AddInParameter(oDbCommand, "@RATE_FORMULA", DbType.String, tarifa.RATE_FORMULA.ToUpper());
            db.AddInParameter(oDbCommand, "@RATE_MINIMUM", DbType.String, tarifa.RATE_MINIMUM.ToUpper());
            db.AddInParameter(oDbCommand, "@RATE_FTIPO", DbType.String, tarifa.RATE_FTIPO);
            db.AddInParameter(oDbCommand, "@RATE_MTIPO", DbType.String, tarifa.RATE_MTIPO);
            db.AddInParameter(oDbCommand, "@RATE_FDECI", DbType.Decimal, tarifa.RATE_FDECI);
            db.AddInParameter(oDbCommand, "@RATE_MDECI", DbType.Decimal, tarifa.RATE_MDECI);
            db.AddInParameter(oDbCommand, "@RATE_ID_PREC", DbType.Decimal, tarifa.RATE_ID_PREC);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, tarifa.LOG_USER_UPDATE);
            db.AddInParameter(oDbCommand, "@RATE_REDONDEO", DbType.Int32, tarifa.RATE_REDONDEO);

            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public List<BEREC_RATES_GRAL> ListarCombo(string owner)
        {
            List<BEREC_RATES_GRAL> lst = new List<BEREC_RATES_GRAL>();
            BEREC_RATES_GRAL item = null;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_LISTA_TARIFA"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEREC_RATES_GRAL();
                        item.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                        item.RATE_DESC = dr.GetString(dr.GetOrdinal("RATE_DESC"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

        public int Eliminar(BEREC_RATES_GRAL tarifa)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_TARIFA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, tarifa.OWNER);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, tarifa.RATE_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, tarifa.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ActualizarModalidad(string owner, decimal rateId, decimal rateId_New, string userUpdate)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_MODALIDAD_TARIFA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, rateId);
            db.AddInParameter(oDbCommand, "@RATE_ID_NEW", DbType.Decimal, rateId_New);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, userUpdate);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public DateTime ObtenerFechaSistema()
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_FECHA_SISTEMA");
            DateTime fecha = Convert.ToDateTime(db.ExecuteScalar(oDbCommand));
            int r = db.ExecuteNonQuery(oDbCommand);
            return fecha;
        }
        public int ObtenerMesesQuiebra()
        {            
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_CANTIDAD_MESES_QUIEBRA");
            int meses = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return meses;
        }

        public List<BEREC_RATES_GRAL> ListarComboModalidad(string owner, decimal idMod)
        {
            List<BEREC_RATES_GRAL> lst = new List<BEREC_RATES_GRAL>();
            BEREC_RATES_GRAL item = null;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_LISTA_TARIFA_MOD"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@MOD_ID", DbType.Decimal, idMod);
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEREC_RATES_GRAL();
                        item.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                        item.RATE_DESC = dr.GetString(dr.GetOrdinal("RATE_DESC"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

        public int ObtenerCantPeriodocidadXProd(BEREC_RATES_GRAL tarifa)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_CANT_PERIODOCIDAD_X_PROD");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, tarifa.OWNER);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, tarifa.RATE_ID);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, tarifa.MOD_ID);
            db.AddInParameter(oDbCommand, "@RAT_FID", DbType.Decimal, tarifa.RAT_FID);
            db.AddInParameter(oDbCommand, "@RATE_START", DbType.DateTime, tarifa.RATE_START);
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        public List<BEREC_RATES_GRAL> TarifasXModalidad(decimal codModalidad, string owner)
        {
            List<BEREC_RATES_GRAL> items = new List<BEREC_RATES_GRAL>();
            BEREC_RATES_GRAL item = null;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_TARIFA_X_MOD"))
            {
                db.AddInParameter(cm, "@MOD_ID", DbType.Decimal, codModalidad);
                db.AddInParameter(cm, "@@OWNER", DbType.Decimal, owner);
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BEREC_RATES_GRAL();
                        item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        item.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                        item.RATE_DESC = dr.GetString(dr.GetOrdinal("RATE_DESC"));
                        item.RAT_FID = dr.GetDecimal(dr.GetOrdinal("RAT_FID"));

                        items.Add(item);
                    }
                }
            }
            return items;
        }

        public BEREC_RATES_GRAL ObtenerNombreTarifa(string owner, decimal IdTarifa)
        {
            BEREC_RATES_GRAL item = null;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_ObtenerNombreTarifa"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@rate_id", DbType.Decimal, IdTarifa);
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BEREC_RATES_GRAL();
                        item.RATE_DESC = dr.GetString(dr.GetOrdinal("RATE_DESC"));
                    }
                }
            }
            return item;
        }

        public BEREC_RATES_GRAL ObtenerTarifaHistorica(string owner, decimal IdTarifa, decimal periodo)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_TARIFA_HISTORICA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, IdTarifa);
            db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, periodo);

            BEREC_RATES_GRAL ent = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BEREC_RATES_GRAL();
                    ent.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                    ent.RATE_DESC = dr.GetString(dr.GetOrdinal("RATE_DESC"));
                }
            }
            return ent;
        }


        public decimal ObtenerRateOrigen(string owner, decimal IdTarifa)
        {

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_RATE_ORIGEN");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, IdTarifa);
            decimal idorigen = Convert.ToDecimal( db.ExecuteScalar(oDbCommand));

            return idorigen;
        }


    }
}


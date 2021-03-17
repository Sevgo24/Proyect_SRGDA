using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using SGRDA.Entities;
using System.Data.Common;

namespace SGRDA.DA
{
    public class DAComisionTotales
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEComisionTotales> ListarPage(string Owner, decimal ProgramaId, DateTime Ultfecha, decimal IdRepresentante, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_COMISION_TOTALES");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "@PRG_ID", DbType.Decimal, ProgramaId);
            db.AddInParameter(oDbCommand, "@PRG_LASTL", DbType.DateTime, Ultfecha);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, IdRepresentante);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEComisionTotales>();
            var item = new BEComisionTotales();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEComisionTotales();
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_ID")))
                        item.PRG_ID = dr.GetDecimal(dr.GetOrdinal("PRG_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_DESC")))
                        item.PRG_DESC = dr.GetString(dr.GetOrdinal("PRG_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("START")))
                        item.START = dr.GetDateTime(dr.GetOrdinal("START"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                        item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RAT_FID")))
                        item.RAT_FID = dr.GetDecimal(dr.GetOrdinal("RAT_FID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RAT_FDESC")))
                        item.RAT_FDESC = dr.GetString(dr.GetOrdinal("RAT_FDESC"));
                    item.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(item);
                }
            }
            return lista;
        }

        public int Insertar(BEComisionTotales en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_COMISION_TOTAL");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddOutParameter(oDbCommand, "@PRG_ID", DbType.Decimal, Convert.ToInt32(en.PRG_ID));
            db.AddInParameter(oDbCommand, "@PRG_DESC", DbType.String, en.PRG_DESC);
            db.AddInParameter(oDbCommand, "@PRG_LASTL", DbType.DateTime, en.PRG_LASTL);
            db.AddInParameter(oDbCommand, "@RAT_FID", DbType.Decimal, en.RAT_FID);
            db.AddInParameter(oDbCommand, "@START", DbType.DateTime, en.START);
            db.AddInParameter(oDbCommand, "@ENDS", DbType.DateTime, en.ENDS);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int n = db.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(db.GetParameterValue(oDbCommand, "@PRG_ID"));
            return id;
        }

        public int InsertarRepresentante(BEComisionRepresentantes en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_COMISION_TOTAL_REP");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@PRG_ID", DbType.Decimal, en.PRG_ID);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@STARTS", DbType.DateTime, en.STARTS);
            db.AddInParameter(oDbCommand, "@ENDS", DbType.DateTime, en.ENDS);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BEComisionTotales en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_COMISION_TOTAL");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@PRG_ID", DbType.Decimal, en.PRG_ID);
            db.AddInParameter(oDbCommand, "@PRG_DESC", DbType.String, en.PRG_DESC);
            db.AddInParameter(oDbCommand, "@PRG_LASTL", DbType.DateTime, en.PRG_LASTL);
            db.AddInParameter(oDbCommand, "@RAT_FID", DbType.Decimal, en.RAT_FID);
            db.AddInParameter(oDbCommand, "@START", DbType.DateTime, en.START);
            db.AddInParameter(oDbCommand, "@ENDS", DbType.DateTime, en.ENDS);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int ActualizarRepresentante(BEComisionRepresentantes en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_COMISION_TOTAL_REP");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@SEQUENCE", DbType.Decimal, en.SEQUENCE);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@STARTS", DbType.DateTime, en.STARTS);
            db.AddInParameter(oDbCommand, "@ENDS", DbType.DateTime, en.ENDS);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int ActivarRepresentante(BEComisionRepresentantes en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTIVAR_COMISION_REP");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@SEQUENCE", DbType.Decimal, en.SEQUENCE);
            db.AddInParameter(oDbCommand, "@PRG_ID", DbType.Decimal, en.PRG_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int InactivarRepresentante(BEComisionRepresentantes en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_INACTIVAR_COMISION_REP");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@SEQUENCE", DbType.Decimal, en.SEQUENCE);
            db.AddInParameter(oDbCommand, "@PRG_ID", DbType.Decimal, en.PRG_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public BEComisionRepresentantes ObtenerRepresentante(string Owner, decimal Sequence, decimal IdPrograma)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_REP");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "@SEQUENCE", DbType.Decimal, Sequence);
            db.AddInParameter(oDbCommand, "@PRG_ID", DbType.Decimal, IdPrograma);
            var item = new BEComisionRepresentantes();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEComisionRepresentantes();
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_ID")))
                        item.PRG_ID = dr.GetDecimal(dr.GetOrdinal("PRG_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("STARTS")))
                        item.STARTS = dr.GetDateTime(dr.GetOrdinal("STARTS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
                return item;
            }
        }

        public BEComisionTotales ObtenerComisionTotales(string Owner, decimal IdPrograma)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_COMISION_TOTAL");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "@PRG_ID", DbType.Decimal, IdPrograma);
            var item = new BEComisionTotales();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEComisionTotales();
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_ID")))
                        item.PRG_ID = dr.GetDecimal(dr.GetOrdinal("PRG_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_DESC")))
                        item.PRG_DESC = dr.GetString(dr.GetOrdinal("PRG_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_LASTL")))
                        item.PRG_LASTL = dr.GetDateTime(dr.GetOrdinal("PRG_LASTL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RAT_FID")))
                        item.RAT_FID = dr.GetDecimal(dr.GetOrdinal("RAT_FID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("START")))
                        item.START = dr.GetDateTime(dr.GetOrdinal("START"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
                return item;
            }
        }

        public List<BEComisionRepresentantes> ListaRepresentante(string Owner, decimal IdPrograma)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTA_REP");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "@PRG_ID", DbType.Decimal, IdPrograma);
            var item = new BEComisionRepresentantes();
            var lista = new List<BEComisionRepresentantes>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEComisionRepresentantes();
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_ID")))
                        item.PRG_ID = dr.GetDecimal(dr.GetOrdinal("PRG_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("STARTS")))
                        item.STARTS = dr.GetDateTime(dr.GetOrdinal("STARTS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        item.LOG_DATE_CREATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("SEQUENCE")))
                        item.SEQUENCE = dr.GetDecimal(dr.GetOrdinal("SEQUENCE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("INACTIVO")))
                        item.Inactivo = dr.GetDateTime(dr.GetOrdinal("INACTIVO"));
                    lista.Add(item);
                }
                return lista;
            }
        }

        public List<BEComisionRecaudadorRango> ListaRangoRepresentante(string Owner, decimal IdPrograma)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_RANGO_REP");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "@PRG_ID", DbType.Decimal, IdPrograma);
            var item = new BEComisionRecaudadorRango();
            var lista = new List<BEComisionRecaudadorRango>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEComisionRecaudadorRango();
                    if (!dr.IsDBNull(dr.GetOrdinal("SEQUENCE")))
                        item.SEQUENCE = dr.GetDecimal(dr.GetOrdinal("SEQUENCE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_ORDER")))
                        item.PRG_ORDER = dr.GetDecimal(dr.GetOrdinal("PRG_ORDER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_ID")))
                        item.PRG_ID = dr.GetDecimal(dr.GetOrdinal("PRG_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_VALUEI")))
                        item.PRG_VALUEI = dr.GetDecimal(dr.GetOrdinal("PRG_VALUEI"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_VALUEF")))
                        item.PRG_VALUEF = dr.GetDecimal(dr.GetOrdinal("PRG_VALUEF"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_PERC")))
                        item.PRG_PERC = dr.GetDecimal(dr.GetOrdinal("PRG_PERC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_VALUEC")))
                    {
                        item.PRG_VALUEC = dr.GetDecimal(dr.GetOrdinal("PRG_VALUEC"));
                        item.Formato = "S/.";         
                    }
                    else
                        item.Formato = "%";
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        item.LOG_DATE_CREATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    lista.Add(item);
                }
                return lista;
            }
        }

        public int InsertarRango(BEComisionRecaudadorRango en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_RANGO_REP");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@PRG_ID", DbType.Decimal, en.PRG_ID);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@PRG_ORDER", DbType.Decimal, en.PRG_ORDER);
            db.AddInParameter(oDbCommand, "@PRG_VALUEI", DbType.Decimal, en.PRG_VALUEI);
            db.AddInParameter(oDbCommand, "@PRG_VALUEF", DbType.Decimal, en.PRG_VALUEF);
            db.AddInParameter(oDbCommand, "@PRG_PERC", DbType.Decimal, en.PRG_PERC == 0 ? null : en.PRG_PERC);
            db.AddInParameter(oDbCommand, "@PRG_VALUEC", DbType.Decimal, en.PRG_VALUEC == 0 ? null : en.PRG_VALUEC);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int ActualizarRango(BEComisionRecaudadorRango en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_RANGO_REP");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@PRG_ID", DbType.Decimal, en.PRG_ID);
            db.AddInParameter(oDbCommand, "@SEQUENCE", DbType.Decimal, en.SEQUENCE);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@PRG_ORDER", DbType.Decimal, en.PRG_ORDER);
            db.AddInParameter(oDbCommand, "@PRG_VALUEI", DbType.Decimal, en.PRG_VALUEI);
            db.AddInParameter(oDbCommand, "@PRG_VALUEF", DbType.Decimal, en.PRG_VALUEF);
            db.AddInParameter(oDbCommand, "@PRG_PERC", DbType.Decimal, en.PRG_PERC == 0 ? null : en.PRG_PERC);
            db.AddInParameter(oDbCommand, "@PRG_VALUEC", DbType.Decimal, en.PRG_VALUEC == 0 ? null : en.PRG_VALUEC);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public BEComisionRecaudadorRango ObtenerRangos(string Owner, decimal Sequence, decimal IdPrograma)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_RANGO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "@SEQUENCE", DbType.Decimal, Sequence);
            db.AddInParameter(oDbCommand, "@PRG_ID", DbType.Decimal, IdPrograma);
            var item = new BEComisionRecaudadorRango();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEComisionRecaudadorRango();
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_ID")))
                        item.PRG_ID = dr.GetDecimal(dr.GetOrdinal("PRG_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_ORDER")))
                        item.PRG_ORDER = dr.GetDecimal(dr.GetOrdinal("PRG_ORDER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_VALUEI")))
                        item.PRG_VALUEI = dr.GetDecimal(dr.GetOrdinal("PRG_VALUEI"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_VALUEF")))
                        item.PRG_VALUEF = dr.GetDecimal(dr.GetOrdinal("PRG_VALUEF"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_PERC")))
                        item.PRG_PERC = dr.GetDecimal(dr.GetOrdinal("PRG_PERC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PRG_VALUEC")))
                        item.PRG_VALUEC = dr.GetDecimal(dr.GetOrdinal("PRG_VALUEC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SEQUENCE")))
                        item.SEQUENCE = dr.GetDecimal(dr.GetOrdinal("SEQUENCE"));
                }
                return item;
            }
        }

        public int ActivarRango(BEComisionRecaudadorRango en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTIVAR_RANGO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@SEQUENCE", DbType.Decimal, en.SEQUENCE);
            db.AddInParameter(oDbCommand, "@PRG_ID", DbType.Decimal, en.PRG_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int InactivarRango(BEComisionRecaudadorRango en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_INACTIVAR_RANGO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@SEQUENCE", DbType.Decimal, en.SEQUENCE);
            db.AddInParameter(oDbCommand, "@PRG_ID", DbType.Decimal, en.PRG_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }
    }
}

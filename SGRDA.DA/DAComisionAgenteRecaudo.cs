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
    public class DAComisionAgenteRecaudo
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BEComisionAgenteRecaudo en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_COMISION_AGENTE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, en.MOD_ID);
            db.AddInParameter(oDbCommand, "@RAT_FID", DbType.Decimal, en.RAT_FID);
            db.AddInParameter(oDbCommand, "@COMT_ID", DbType.Decimal, en.COMT_ID);
            db.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, en.LEVEL_ID);
            db.AddInParameter(oDbCommand, "@COM_ORG", DbType.Decimal, en.COM_ORG);
            db.AddInParameter(oDbCommand, "@COM_START", DbType.DateTime, en.COM_START);
            db.AddInParameter(oDbCommand, "@COM_PER", DbType.Decimal, en.COM_PER);
            db.AddInParameter(oDbCommand, "@COM_VAL", DbType.Decimal, en.COM_VAL);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Actualizar(BEComisionAgenteRecaudo en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_COMISION_AGENTE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@auxBPS_ID", DbType.Decimal, en.auxBPS_ID);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, en.MOD_ID);
            db.AddInParameter(oDbCommand, "@COMT_ID", DbType.Decimal, en.COMT_ID);
            db.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, en.LEVEL_ID);
            db.AddInParameter(oDbCommand, "@COM_ORG", DbType.Decimal, en.COM_ORG);
            db.AddInParameter(oDbCommand, "@COM_START", DbType.DateTime, en.COM_START);
            db.AddInParameter(oDbCommand, "@COM_PER", DbType.Decimal, en.COM_PER);
            db.AddInParameter(oDbCommand, "@COM_VAL", DbType.Decimal, en.COM_VAL);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BEComisionAgenteRecaudo ObtenerNivelAgente(string owner, decimal idAgente)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_COMISION_AGENTE_NIVEL");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, idAgente);

            BEComisionAgenteRecaudo item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEComisionAgenteRecaudo();
                    item.LEVEL_ID = dr.GetDecimal(dr.GetOrdinal("COLL_LEVEL"));
                    item.NIVEL = dr.GetString(dr.GetOrdinal("NIVEL"));
                }
            }
            return item;
        }

        public List<BEComisionAgenteRecaudo> ListarTemporalidadporModalidad(string owner, decimal idModalidad)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_TEMPORALIDAD_MODALIDAD");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, idModalidad);
            List<BEComisionAgenteRecaudo> lista;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                lista = new List<BEComisionAgenteRecaudo>();
                while (dr.Read())
                {
                    var item = new BEComisionAgenteRecaudo
                    {
                        RAT_FID = dr.GetDecimal(dr.GetOrdinal("RAT_FID")),
                        RAT_FDESC = dr.GetString(dr.GetOrdinal("RAT_FDESC"))
                    };
                    lista.Add(item);
                }
            }
            return lista;
        }

        public BEComisionAgenteRecaudo Obtener(string owner, decimal id, decimal idAgente)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_COMISION_AGENTE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, idAgente);

            BEComisionAgenteRecaudo item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEComisionAgenteRecaudo();
                    item.COMT_ID = dr.GetDecimal(dr.GetOrdinal("COMT_ID"));
                    item.COM_ORG = dr.GetDecimal(dr.GetOrdinal("COM_ORG"));
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_VAL")))
                    {
                        item.Formato = "M";
                        item.Valor = dr.GetDecimal(dr.GetOrdinal("COM_VAL"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_PER")))
                    {
                        item.Formato = "P";
                        item.Valor = dr.GetDecimal(dr.GetOrdinal("COM_PER"));
                    }
                    item.LEVEL_ID = dr.GetDecimal(dr.GetOrdinal("LEVEL_ID"));
                    item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    item.auxBPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_START")))
                    {
                        item.COM_START = dr.GetDateTime(dr.GetOrdinal("COM_START"));
                        item.fechaStart = (dr.GetDateTime(dr.GetOrdinal("COM_START"))).ToShortDateString();
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("RAT_FID")))
                    {
                        item.RAT_FID = dr.GetDecimal(dr.GetOrdinal("RAT_FID"));
                    }
                }
            }
            return item;
        }

        public List<BEComisionAgenteRecaudo> ListarPage(string Owner, string Origen, string Sociedad, string Clases, string Grupo, string Derecho, string Incidencia, string Frecuencia, string Repertorio, decimal? Tarifa, decimal TipoComision, decimal OrigenComision, decimal Agente, DateTime FechaIni, DateTime FechaFin, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_COMISION_AGENTE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "@MOD_ORIG", DbType.String, Origen);
            db.AddInParameter(oDbCommand, "@MOD_SOC", DbType.String, Sociedad);
            db.AddInParameter(oDbCommand, "@CLASS_COD", DbType.String, Clases);
            db.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, Grupo);
            db.AddInParameter(oDbCommand, "@RIGHT_COD", DbType.String, Derecho);
            db.AddInParameter(oDbCommand, "@MOD_INCID", DbType.String, Incidencia);
            db.AddInParameter(oDbCommand, "@MOD_USAGE", DbType.String, Frecuencia);
            db.AddInParameter(oDbCommand, "@MOD_REPER", DbType.String, Repertorio);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, Tarifa);
            db.AddInParameter(oDbCommand, "@COMT_ID", DbType.Decimal, TipoComision);
            db.AddInParameter(oDbCommand, "@COM_ORG", DbType.Decimal, OrigenComision);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, Agente);
            db.AddInParameter(oDbCommand, "@Fechaini", DbType.DateTime, FechaIni);
            db.AddInParameter(oDbCommand, "@Fechafin", DbType.DateTime, FechaFin);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEComisionAgenteRecaudo>();
            var item = new BEComisionAgenteRecaudo();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEComisionAgenteRecaudo();
                    item.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));
                    item.COM_DESC = dr.GetString(dr.GetOrdinal("COM_DESC"));
                    item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                    item.MOD_DEC = dr.GetString(dr.GetOrdinal("MOD_DEC"));
                    item.LEVEL_ID = dr.GetDecimal(dr.GetOrdinal("LEVEL_ID"));
                    item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                    item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("COM_VAL")))
                    {
                        item.Formato = "$";
                        item.Valor = dr.GetDecimal(dr.GetOrdinal("COM_VAL"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_PER")))
                    {
                        item.Formato = "%";
                        item.Valor = dr.GetDecimal(dr.GetOrdinal("COM_PER"));
                    }

                    if (!dr.IsDBNull(dr.GetOrdinal("COM_START")))
                    {
                        item.COM_START = dr.GetDateTime(dr.GetOrdinal("COM_START"));
                    }

                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ESTADO = "ACTIVO";
                    else
                        item.ESTADO = "INACTIVO";
                    item.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(item);
                }
            }
            return lista;
        }

        public BEComisionAgenteRecaudo ObtenerTarifaPorTemporalidad(string owner, decimal? idModalidad, decimal idTemporalidad)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_TARIFA_TEMPORALIDAD");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, idModalidad);
            db.AddInParameter(oDbCommand, "@RAT_FID", DbType.Decimal, idTemporalidad);

            BEComisionAgenteRecaudo item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEComisionAgenteRecaudo();
                    if (!dr.IsDBNull(dr.GetOrdinal("RAT_FID")))
                        item.RAT_FID = dr.GetDecimal(dr.GetOrdinal("RAT_FID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RAT_FDESC")))
                        item.RAT_FDESC = dr.GetString(dr.GetOrdinal("RAT_FDESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RATE_ID")))
                        item.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("NAME")))
                        item.NAME = dr.GetString(dr.GetOrdinal("NAME"));
                }
            }
            return item;
        }

        public int Eliminar(BEComisionAgenteRecaudo en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_COMISION_AGENTE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, en.MOD_ID);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ValidacionInsertar(BEComisionAgenteRecaudo en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_VALIDAR_COMISION_AGENT");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, en.MOD_ID);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@RAT_FID", DbType.Decimal, en.RAT_FID);
            db.AddInParameter(oDbCommand, "@COM_START", DbType.DateTime, en.COM_START);
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }
    }
}

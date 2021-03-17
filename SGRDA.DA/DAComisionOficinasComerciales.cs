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
using System.Web.Configuration;


namespace SGRDA.DA
{
    public class DAComisionOficinasComerciales
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BEComisionOficinasComerciales en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_COMISION_OFICINA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, en.OFF_ID);
            db.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, en.LEVEL_ID);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, en.MOD_ID);
            db.AddInParameter(oDbCommand, "@COMT_ID", DbType.Decimal, en.COMT_ID);
            db.AddInParameter(oDbCommand, "@COM_ORG", DbType.Decimal, en.COM_ORG);
            db.AddInParameter(oDbCommand, "@COM_START", DbType.DateTime, en.COM_START);
            db.AddInParameter(oDbCommand, "@COM_PER", DbType.Decimal, en.COM_PER);
            db.AddInParameter(oDbCommand, "@COM_VAL", DbType.Decimal, en.COM_VAL);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Actualizar(BEComisionOficinasComerciales en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_COMISION_OFICINA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, en.OFF_ID);
            db.AddInParameter(oDbCommand, "@auxOFF_ID", DbType.Decimal, en.auxOFF_ID);
            db.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, en.LEVEL_ID);
            db.AddInParameter(oDbCommand, "@auxLEVEL_ID", DbType.Decimal, en.auxLEVEL_ID);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, en.MOD_ID);
            db.AddInParameter(oDbCommand, "@COMT_ID", DbType.Decimal, en.COMT_ID);
            db.AddInParameter(oDbCommand, "@COM_ORG", DbType.Decimal, en.COM_ORG);
            db.AddInParameter(oDbCommand, "@COM_START", DbType.DateTime, en.COM_START);
            db.AddInParameter(oDbCommand, "@COM_PER", DbType.Decimal, en.COM_PER);
            db.AddInParameter(oDbCommand, "@COM_VAL", DbType.Decimal, en.COM_VAL);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(BEComisionOficinasComerciales en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_COMISION_OFICINA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, en.MOD_ID);
            db.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, en.LEVEL_ID);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, en.OFF_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ValidacionInsertar(BEComisionOficinasComerciales en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_VALIDAR_COMISION_OFICINA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, en.MOD_ID);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, en.OFF_ID);
            db.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, en.LEVEL_ID);
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        public BEComisionOficinasComerciales Obtener(string owner, decimal id, decimal idNivAgent, decimal idOficina)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_COMISION_OFICINA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, idNivAgent);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficina);

            BEComisionOficinasComerciales item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEComisionOficinasComerciales();
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
                    item.auxLEVEL_ID = dr.GetDecimal(dr.GetOrdinal("LEVEL_ID"));
                    item.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                    item.auxOFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_START")))
                    {
                        item.COM_START = dr.GetDateTime(dr.GetOrdinal("COM_START"));
                        item.fechaStart = (dr.GetDateTime(dr.GetOrdinal("COM_START"))).ToShortDateString();
                    }
                }
            }
            return item;
        }

        public List<BEComisionOficinasComerciales> ListarOficinas(string owner)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_OFFICES");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            List<BEComisionOficinasComerciales> lista = new List<BEComisionOficinasComerciales>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    //lista.Add(new BEOffices
                    //{
                    //    OFF_ID = Convert.ToInt32((dr.GetOrdinal("OFF_ID"))),
                    //    OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"))
                    //});

                    var item = new BEComisionOficinasComerciales
                    {
                        OFF_ID = dr.GetDecimal((dr.GetOrdinal("OFF_ID"))),
                        OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"))
                    };
                    lista.Add(item);
       
                }
            }
            return lista;   
        }

        public List<BEComisionOficinasComerciales> ListarPage(string Owner, string Origen, string Sociedad, string Clases, string Grupo, string Derecho, string Incidencia, string Frecuencia, string Repertorio, decimal TipoComision, decimal OrigenComision, decimal NivelAgente, decimal Oficina, DateTime FechaIni, DateTime FechaFin, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_COMISION_OFICINA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "@MOD_ORIG", DbType.String, Origen);
            db.AddInParameter(oDbCommand, "@MOD_SOC", DbType.String, Sociedad);
            db.AddInParameter(oDbCommand, "@CLASS_COD", DbType.String, Clases);
            db.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, Grupo);
            db.AddInParameter(oDbCommand, "@RIGHT_COD", DbType.String, Derecho);
            db.AddInParameter(oDbCommand, "@MOD_INCID", DbType.String, Incidencia);
            db.AddInParameter(oDbCommand, "@MOD_USAGE", DbType.String, Frecuencia);
            db.AddInParameter(oDbCommand, "@MOD_REPER", DbType.String, Repertorio);
            db.AddInParameter(oDbCommand, "@COMT_ID", DbType.Decimal, TipoComision);
            db.AddInParameter(oDbCommand, "@COM_ORG", DbType.Decimal, OrigenComision);
            db.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, NivelAgente);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, Oficina);
            db.AddInParameter(oDbCommand, "@Fechaini", DbType.DateTime, FechaIni);
            db.AddInParameter(oDbCommand, "@Fechafin", DbType.DateTime, FechaFin);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEComisionOficinasComerciales>();
            var item = new BEComisionOficinasComerciales();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEComisionOficinasComerciales();
                    item.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));
                    item.COM_DESC = dr.GetString(dr.GetOrdinal("COM_DESC"));
                    item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                    item.MOD_DEC = dr.GetString(dr.GetOrdinal("MOD_DEC"));
                    item.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                    item.OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"));
                    item.LEVEL_ID = dr.GetDecimal(dr.GetOrdinal("LEVEL_ID"));

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
    }
}

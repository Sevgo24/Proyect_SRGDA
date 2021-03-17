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
    public class DAValormusica
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEValormusica> ListaValorMusicaPaginada(string owner, DateTime fechaini, DateTime fechafin, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_VALORMUSICA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            //db.AddInParameter(oDbCommand, "@flag", DbType.Int32, flag);
            db.AddInParameter(oDbCommand, "@Fechaini", DbType.DateTime, fechaini);
            db.AddInParameter(oDbCommand, "@Fechafin", DbType.DateTime, fechafin);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEValormusica>();
            var item = new BEValormusica();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEValormusica();
                    item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                    item.VUM_ID = dr.GetDecimal(dr.GetOrdinal("VUM_ID"));
                    item.START = dr.GetDateTime(dr.GetOrdinal("START"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    item.VUM_VAL = dr.GetDecimal(dr.GetOrdinal("VUM_VAL"));

                    if (dr.IsDBNull(dr.GetOrdinal("DELETE")))
                        item.ESTADO = "ACTIVO";
                    else
                        item.ESTADO = "INACTIVO";

                    item.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(item);
                }
            }
            return lista;
        }

        public BEValormusica Obtener(string owner, string id)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_VALORMUSICA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@VUM_ID", DbType.String, id);

            BEValormusica item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                dr.Read();
                item = new BEValormusica();
                item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                item.VUM_ID = dr.GetDecimal(dr.GetOrdinal("VUM_ID"));
                item.VUM_VAL = dr.GetDecimal(dr.GetOrdinal("VUM_VAL"));
                if (!dr.IsDBNull(dr.GetOrdinal("START")))
                    item.START = dr.GetDateTime(dr.GetOrdinal("START"));
            }
            return item;
        }

        public List<BEValormusica> Listar(string owner)
        {
            List<BEValormusica> lista = null;
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_VALORMUSICA_REPORT");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);

            BEValormusica item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                lista = new List<BEValormusica>();

                while (dr.Read())
                {
                    item = new BEValormusica();
                    item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                    item.VUM_ID = dr.GetDecimal(dr.GetOrdinal("VUM_ID"));
                    item.VUM_VAL = dr.GetDecimal(dr.GetOrdinal("VUM_VAL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("START")))
                        item.START = dr.GetDateTime(dr.GetOrdinal("START"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public int Insertar(BEValormusica en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_VALORMUSICA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@START", DbType.DateTime, en.START);
            db.AddInParameter(oDbCommand, "@VUM_VAL", DbType.Decimal, en.VUM_VAL);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BEValormusica en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_VALORMUSICA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@VUM_ID", DbType.Decimal, en.VUM_ID);
            db.AddInParameter(oDbCommand, "@START", DbType.DateTime, en.START);
            db.AddInParameter(oDbCommand, "@VUM_VAL", DbType.Decimal, en.VUM_VAL);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int ActualizarFechaUltimoRegistro(BEValormusica en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_VALORMUSICA_ENDS");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BEValormusica en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_VALORMUSICA_INACTIVAR");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@VUM_ID", DbType.Decimal, en.VUM_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BEValormusica ObtenerActivo(string owner)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_VUM_ACTIVO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);

            BEValormusica item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                dr.Read();
                item = new BEValormusica();
                item.VUM_ID = dr.GetDecimal(dr.GetOrdinal("VUM_ID"));
                item.START = dr.GetDateTime(dr.GetOrdinal("START"));
                item.VUM_VAL = dr.GetDecimal(dr.GetOrdinal("VUM_VAL"));
            }
            return item;
        }

        public List<BEValormusica> ListarHistorico(string owner)
        {
            List<BEValormusica> lista = null;
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_VUM_HISTORICO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);

            BEValormusica item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                lista = new List<BEValormusica>();

                while (dr.Read())
                {
                    item = new BEValormusica();
                    item.VUM_ID = dr.GetDecimal(dr.GetOrdinal("VUM_ID"));
                    item.VUM_VAL = dr.GetDecimal(dr.GetOrdinal("VUM_VAL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("START")))
                        item.START = dr.GetDateTime(dr.GetOrdinal("START"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    lista.Add(item);
                }
            }
            return lista;
        }

    }
}

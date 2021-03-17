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
    public class DATransicionesEstado
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BETransicionesEstado> ListaTrancionEstadoPaginada(string owner, decimal estadoOri, decimal estadoDes, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_TRANESTADO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LICS_ID", DbType.Decimal, estadoOri);
            db.AddInParameter(oDbCommand, "@LICS_IDT", DbType.Decimal, estadoDes);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BETransicionesEstado>();
            var item = new BETransicionesEstado();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BETransicionesEstado();
                    item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                    item.LICS_ID = dr.GetDecimal(dr.GetOrdinal("LICS_ID"));
                    item.LICS_IDT = dr.GetDecimal(dr.GetOrdinal("LICS_IDT"));
                    item.LICS_NAMEori = dr.GetString(dr.GetOrdinal("LICS_NAMEori"));
                    item.LICS_NAMEdes = dr.GetString(dr.GetOrdinal("LICS_NAMEdes"));

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

        public int Eliminar(BETransicionesEstado en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_INACTIVAR_TRANESTADO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@LICS_ID", DbType.Decimal, en.LICS_ID);
            db.AddInParameter(oDbCommand, "@LICS_IDT", DbType.Decimal, en.LICS_IDT);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Insertar(BETransicionesEstado en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_TRANESTADO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@LICS_ID", DbType.Decimal, en.LICS_ID);
            db.AddInParameter(oDbCommand, "@LICS_IDT", DbType.Decimal, en.LICS_IDT);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int actualizar(BETransicionesEstado en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_TRANESTADO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@auxLICS_ID", DbType.Decimal, en.auxLICS_ID);
            db.AddInParameter(oDbCommand, "@auxLICS_IDT", DbType.Decimal, en.auxLICS_IDT);
            db.AddInParameter(oDbCommand, "@LICS_ID", DbType.Decimal, en.LICS_ID);
            db.AddInParameter(oDbCommand, "@LICS_IDT", DbType.Decimal, en.LICS_IDT);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BETransicionesEstado Obtener(string owner, decimal idori, decimal iddes)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_TRANESTADO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LICS_ID", DbType.Decimal, idori);
            db.AddInParameter(oDbCommand, "@LICS_IDT", DbType.Decimal, iddes);

            BETransicionesEstado item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                dr.Read();
                item = new BETransicionesEstado();
                if (!dr.IsDBNull(dr.GetOrdinal("OWNER")))
                    item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                if (!dr.IsDBNull(dr.GetOrdinal("LICS_ID")))
                    item.LICS_ID = dr.GetDecimal(dr.GetOrdinal("LICS_ID"));
                if (!dr.IsDBNull(dr.GetOrdinal("LICS_IDT")))
                    item.LICS_IDT = dr.GetDecimal(dr.GetOrdinal("LICS_IDT"));
            }
            return item;
        }

        public List<BETransicionesEstado> ObtenerDatosValidad(string owner, decimal idori, decimal iddes)
        {
            List<BETransicionesEstado> lista;
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_TRANESTADO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LICS_ID", DbType.Decimal, idori);
            db.AddInParameter(oDbCommand, "@LICS_IDT", DbType.Decimal, iddes);

            BETransicionesEstado item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                lista = new List<BETransicionesEstado>();

                while (dr.Read())
                {
                    item = new BETransicionesEstado();
                    if (!dr.IsDBNull(dr.GetOrdinal("OWNER")))
                        item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LICS_ID")))
                        item.LICS_ID = dr.GetDecimal(dr.GetOrdinal("LICS_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LICS_IDT")))
                        item.LICS_IDT = dr.GetDecimal(dr.GetOrdinal("LICS_IDT"));

                    lista.Add(item);
                }
            }
            return lista;
        }
    }
}

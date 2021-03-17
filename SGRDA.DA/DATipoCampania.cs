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
    public class DATipoCampania
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BETipoCampania> ListarCampaniaPage(string owner, decimal tipo, string dato, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_CAMPANIAS_PAGE");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OBS_TYPE", DbType.Decimal, tipo);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CTNAME", DbType.String, dato);
            oDataBase.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BETipoCampania>();
            var item = new BETipoCampania();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BETipoCampania();
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CTID")))
                        item.CONC_CTID = dr.GetDecimal(dr.GetOrdinal("CONC_CTID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CTNAME")))
                        item.CONC_CTNAME = dr.GetString(dr.GetOrdinal("CONC_CTNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OBS_DESC")))
                        item.OBS_DESC = dr.GetString(dr.GetOrdinal("OBS_DESC"));
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

        public int Eliminar(BETipoCampania en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_INACTIVAR_TIPOCAMPANIA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CTID", DbType.Decimal, en.CONC_CTID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BETipoCampania Obtener(string owner, decimal Id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_TIPOCAMPANIA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CTID", DbType.Decimal, Id);

            var item = new BETipoCampania();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BETipoCampania();
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CTID")))
                        item.CONC_CTID = dr.GetDecimal(dr.GetOrdinal("CONC_CTID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CTNAME")))
                        item.CONC_CTNAME = dr.GetString(dr.GetOrdinal("CONC_CTNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OBS_TYPE")))
                        item.OBS_TYPE = dr.GetDecimal(dr.GetOrdinal("OBS_TYPE"));
                }
            }
            return item;
        }

        public int Insertar(BETipoCampania en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_TIPOCAMPANIA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@CONC_CTNAME", DbType.String, en.CONC_CTNAME == null ? string.Empty : en.CONC_CTNAME.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@OBS_TYPE", DbType.Decimal, en.OBS_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BETipoCampania en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAU_TIPOCAMPANIA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@CONC_CTID", DbType.Decimal, en.CONC_CTID);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CTNAME", DbType.String, en.CONC_CTNAME == null ? string.Empty : en.CONC_CTNAME.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@OBS_TYPE", DbType.Decimal, en.OBS_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int ObtenerXDescripcion(BETipoCampania en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_TIPOCAMPANIA_DES");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CTNAME", DbType.String, en.CONC_CTNAME);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public List<BETipoCampania> ListaTipoCampania(string owner, decimal Tipo, string Descripcion, int Estado)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TIPOCAMPANIA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OBS_TYPE", DbType.Decimal, Tipo);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CTNAME", DbType.String, Descripcion);
            oDataBase.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, Estado);

            var item = new BETipoCampania();
            List<BETipoCampania> lista = new List<BETipoCampania>();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BETipoCampania();
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CTID")))
                        item.CONC_CTID = dr.GetDecimal(dr.GetOrdinal("CONC_CTID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CTNAME")))
                        item.CONC_CTNAME = dr.GetString(dr.GetOrdinal("CONC_CTNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OBS_DESC")))
                        item.OBS_DESC = dr.GetString(dr.GetOrdinal("OBS_DESC"));
                    item.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public List<BETipoCampania> ListaDropTipoCampania(string owner)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_CAMPANIA_TIPO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            BETipoCampania item = null;
            List<BETipoCampania> lista = new List<BETipoCampania>();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BETipoCampania();
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CTID")))
                        item.CONC_CTID = dr.GetDecimal(dr.GetOrdinal("CONC_CTID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CTNAME")))
                        item.CONC_CTNAME = dr.GetString(dr.GetOrdinal("CONC_CTNAME"));
                    lista.Add(item);
                }
            }
            return lista;
        }
    }
}

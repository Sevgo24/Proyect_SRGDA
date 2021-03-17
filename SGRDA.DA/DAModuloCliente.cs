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
    public class DAModuloCliente
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public BEModuloCliente Obtener(string owner, decimal id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_MODULO_CLIENTE");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@WRKF_CID", DbType.Decimal, id);
            BEModuloCliente obj = new BEModuloCliente();
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    obj.PROC_MOD = dr.GetDecimal(dr.GetOrdinal("PROC_MOD"));
                    obj.MOD_DESC = dr.GetString(dr.GetOrdinal("MOD_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_CLABEL")))
                        obj.MOD_CLABEL = dr.GetString(dr.GetOrdinal("MOD_CLABEL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_CAPIKEY")))
                        obj.MOD_CAPIKEY = dr.GetString(dr.GetOrdinal("MOD_CAPIKEY"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_CSECRETKEY")))
                        obj.MOD_CSECRETKEY = dr.GetString(dr.GetOrdinal("MOD_CSECRETKEY"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
            }
            return obj;
        }

        public int Actualizar(BEModuloCliente cliente)
        {
            DbCommand cmd = oDataBase.GetStoredProcCommand("SGRDASU_MODULO_CLIENTE");
            oDataBase.AddInParameter(cmd, "@OWNER", DbType.String, cliente.OWNER);
            oDataBase.AddInParameter(cmd, "@PROC_MOD", DbType.String, cliente.PROC_MOD);
            oDataBase.AddInParameter(cmd, "@MOD_DESC", DbType.String, cliente.MOD_DESC);
            oDataBase.AddInParameter(cmd, "@MOD_CLABEL", DbType.String, cliente.MOD_CLABEL);
            oDataBase.AddInParameter(cmd, "@MOD_CAPIKEY", DbType.String, cliente.MOD_CAPIKEY);
            oDataBase.AddInParameter(cmd, "@MOD_CSECRETKEY", DbType.String, cliente.MOD_CSECRETKEY);
            oDataBase.AddInParameter(cmd, "@LOG_USER_UPDATE", DbType.String, cliente.LOG_USER_UPDATE);
            int n = oDataBase.ExecuteNonQuery(cmd);
            return n;
        }

        public int Insertar(BEModuloCliente cliente)
        {
            DbCommand cmd = oDataBase.GetStoredProcCommand("SGRDASI_MODULO_CLIENTE");
            oDataBase.AddInParameter(cmd, "@OWNER", DbType.String, cliente.OWNER);
            oDataBase.AddInParameter(cmd, "@MOD_DESC", DbType.String, cliente.MOD_DESC);
            oDataBase.AddInParameter(cmd, "@MOD_CLABEL", DbType.String, cliente.MOD_CLABEL);
            oDataBase.AddInParameter(cmd, "@MOD_CAPIKEY", DbType.String, cliente.MOD_CAPIKEY);
            oDataBase.AddInParameter(cmd, "@MOD_CSECRETKEY", DbType.String, cliente.MOD_CSECRETKEY);
            oDataBase.AddInParameter(cmd, "@LOG_USER_CREAT", DbType.String, cliente.LOG_USER_CREAT);
            int n = oDataBase.ExecuteNonQuery(cmd);
            return n;
        }

        public int Eliminar(BEModuloCliente cliente)
        {
            DbCommand cmd = oDataBase.GetStoredProcCommand("SGRDASD_MODULO_CLIENTE");
            oDataBase.AddInParameter(cmd, "@OWNER", DbType.String, cliente.OWNER);
            oDataBase.AddInParameter(cmd, "@PROC_MOD", DbType.Decimal, cliente.PROC_MOD);
            oDataBase.AddInParameter(cmd, "@LOG_USER_UPDATE", DbType.String, cliente.LOG_USER_UPDATE);
            int n = oDataBase.ExecuteNonQuery(cmd);
            return n;
        }

        public List<BEModuloCliente> Listar(string owner, string desc, string label, int estado, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_MODULO_CLIENTE");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@MOD_DESC", DbType.String, desc);
            oDataBase.AddInParameter(oDbCommand, "@MOD_CLABEL", DbType.String, label);
            oDataBase.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, estado);

            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 0);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));
            var lista = new List<BEModuloCliente>();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                BEModuloCliente modulo = null;
                while (dr.Read())
                {
                    modulo = new BEModuloCliente();
                    modulo.PROC_MOD = dr.GetDecimal(dr.GetOrdinal("PROC_MOD"));
                    modulo.MOD_DESC = dr.GetString(dr.GetOrdinal("MOD_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_CLABEL")))
                        modulo.MOD_CLABEL = dr.GetString(dr.GetOrdinal("MOD_CLABEL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_CAPIKEY")))
                        modulo.MOD_CAPIKEY = dr.GetString(dr.GetOrdinal("MOD_CAPIKEY"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_CSECRETKEY")))
                        modulo.MOD_CSECRETKEY = dr.GetString(dr.GetOrdinal("MOD_CSECRETKEY"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        modulo.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        modulo.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        modulo.ESTADO = "ACTIVO";
                    else
                        modulo.ESTADO = "INACTIVO";
                    modulo.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(modulo);
                }
            }
            return lista;
        }

        public List<BEModuloCliente> ListarNombre(string owner)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_MODULO_CLIENTE_NOMBRE");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            List<BEModuloCliente> Lista = new List<BEModuloCliente>();
            BEModuloCliente obj = new BEModuloCliente();
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    obj = new BEModuloCliente();
                    obj.PROC_MOD = dr.GetDecimal(dr.GetOrdinal("PROC_MOD"));
                    obj.MOD_DESC = dr.GetString(dr.GetOrdinal("MOD_DESC"));
                    Lista.Add(obj);
                }
            }
            return Lista;
        }

    }
}

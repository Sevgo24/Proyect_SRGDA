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
    public class DASociedad
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");
        
        public List<BESociedad> Listar(string owner, string param, int st, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_SOCIEDAD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@MOG_SDESC", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_SOCIEDAD", owner, param, st, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BESociedad>();
            var origen = new BESociedad();
            using (IDataReader dr = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (dr.Read())
                {
                    origen = new BESociedad();
                    origen.MOG_SOC = dr.GetString(dr.GetOrdinal("MOG_SOC"));
                    origen.MOG_SDESC = dr.GetString(dr.GetOrdinal("MOG_SDESC"));
                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        origen.ESTADO = "ACTIVO";
                    else
                        origen.ESTADO = "INACTIVO";

                    origen.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(origen);
                }
            }
            return lista;
        }

        public int Eliminar(BESociedad sociedad)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_SOCIEDAD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, sociedad.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOG_SOC", DbType.String, sociedad.MOG_SOC);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, sociedad.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Insertar(BESociedad sociedad)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_SOCIEDAD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, sociedad.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOG_SOC", DbType.String, sociedad.MOG_SOC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOG_SDESC", DbType.String, sociedad.MOG_SDESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, sociedad.LOG_USER_CREAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Actualizar(BESociedad sociedad)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_SOCIEDAD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, sociedad.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOG_SOC", DbType.String, sociedad.MOG_SOC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOG_SDESC", DbType.String, sociedad.MOG_SDESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, sociedad.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BESociedad Obtener(string owner, string id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_SOCIEDAD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@MOG_SOC", DbType.String, id);
            BESociedad obj = new BESociedad();
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    obj.MOG_SOC = dr.GetString(dr.GetOrdinal("MOG_SOC")).ToUpper();
                    obj.MOG_SDESC = dr.GetString(dr.GetOrdinal("MOG_SDESC")).ToUpper();
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
            }
            return obj;
        }

        public int ObtenerXDescripcion( BESociedad sociedad)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_SOCIEDAD_DESC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, sociedad.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOG_SDESC", DbType.String, sociedad.MOG_SDESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOG_SOC", DbType.String, sociedad.MOG_SOC.ToUpper());
            BESociedad obj = new BESociedad();
            int r =Convert.ToInt32( oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public int ObtenerXCodigo(BESociedad sociedad)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_SOCIEDAD_COD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, sociedad.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOG_SOC", DbType.String, sociedad.MOG_SOC);
            BESociedad obj = new BESociedad();
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public List<BESociedad> ListarTipo(string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_SOCIEDAD_TIPO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);

            var lista = new List<BESociedad>();
            BESociedad obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BESociedad();
                    obs.MOG_SOC = dr.GetString(dr.GetOrdinal("MOG_SOC")).ToUpper();
                    obs.MOG_SDESC = dr.GetString(dr.GetOrdinal("MOG_SDESC")).ToUpper();
                    lista.Add(obs);
                }
            }
            return lista;
        }
    }
}


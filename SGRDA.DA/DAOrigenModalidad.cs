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
    public class DAOrigenModalidad
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEOrigenModalidad> Listar(string param, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_ORIGEN");
            oDataBase.AddInParameter(oDbCommand, "@MOD_ODESC", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));
                        
            var lista = new List<BEOrigenModalidad>();
            var origen = new BEOrigenModalidad();
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    origen = new BEOrigenModalidad();
                    origen.MOD_ORIG = dr.GetString(dr.GetOrdinal("MOD_ORIG")).ToUpper();
                    origen.MOD_ODESC = dr.GetString(dr.GetOrdinal("MOD_ODESC")).ToUpper();
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

        public int Eliminar(BEOrigenModalidad origen)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_ORIGEN");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, origen.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOD_ORIG", DbType.String, origen.MOD_ORIG);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, origen.LOG_DATE_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Insertar(BEOrigenModalidad origen)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_ORIGEN");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, origen.OWNER.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_ORIG", DbType.String, origen.MOD_ORIG.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_ODESC", DbType.String, origen.MOD_ODESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, origen.LOG_USER_CREAT.ToUpper());
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Actualizar(BEOrigenModalidad origen)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ORIGEN");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, origen.OWNER.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_ORIG", DbType.String, origen.MOD_ORIG.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_ODESC", DbType.String, origen.MOD_ODESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, origen.LOG_USER_CREAT.ToUpper());
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BEOrigenModalidad Obtener(string owner, string id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_ORIGEN");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@MOD_ORIG", DbType.String, id);
            BEOrigenModalidad obj = new BEOrigenModalidad();
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    obj.MOD_ORIG = dr.GetString(dr.GetOrdinal("MOD_ORIG"));
                    obj.MOD_ODESC = dr.GetString(dr.GetOrdinal("MOD_ODESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
            }
            return obj;
        }

        public int ObtenerXDescripcion(BEOrigenModalidad origen)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_ORIGEN_DESC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, origen.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOD_ODESC", DbType.String, origen.MOD_ODESC);
            oDataBase.AddInParameter(oDbCommand, "@MOD_ORIG", DbType.String, origen.MOD_ORIG);
            BEOrigenModalidad obj = new BEOrigenModalidad();
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public int ObtenerXCodigo(BEOrigenModalidad origen)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_ORIGEN_COD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, origen.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOD_ORIG", DbType.String, origen.MOD_ORIG);
            BEOrigenModalidad obj = new BEOrigenModalidad();
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public List<BEOrigenModalidad> ListarTipo(string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_MODALIDAD_USO_TIPO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);

            var lista = new List<BEOrigenModalidad>();
            BEOrigenModalidad obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BEOrigenModalidad();
                    obs.MOD_ORIG = dr.GetString(dr.GetOrdinal("MOD_ORIG"));
                    obs.MOD_ODESC = dr.GetString(dr.GetOrdinal("MOD_ODESC"));
                    lista.Add(obs);
                }
            }
            return lista;
        }
    }
}

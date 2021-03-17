using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using SGRDA.Entities;
using System.Data.Common;
using System.Data;

namespace SGRDA.DA
{
    public class DAUsorepertorio
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEUsorepertorio> usp_Get_UsoRepertorioPage(string param, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_USOREPERTORIO_PAGE");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            //Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_USOREPERTORIO_PAGE", owner, param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEUsorepertorio>();
            var usorepertorio = new BEUsorepertorio();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    usorepertorio = new BEUsorepertorio();
                    usorepertorio.MOD_USAGE = dr.GetString(dr.GetOrdinal("MOD_USAGE"));
                    usorepertorio.MOD_DUSAGE = dr.GetString(dr.GetOrdinal("MOD_DUSAGE"));

                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        usorepertorio.ESTADO = "ACTIVO";
                    else
                        usorepertorio.ESTADO = "INACTIVO";
                    usorepertorio.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(usorepertorio);
                }
            }
            return lista;
        }

        public int Eliminar(BEUsorepertorio Usorepertorio)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_INACTIVAR_USOREPERTORIO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, Usorepertorio.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOD_USAGE", DbType.String, Usorepertorio.MOD_USAGE);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, Usorepertorio.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Insertar(BEUsorepertorio Usorepertorio)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_USOREPERTORIO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, Usorepertorio.OWNER.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_USAGE", DbType.String, Usorepertorio.MOD_USAGE.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_DUSAGE", DbType.String, Usorepertorio.MOD_DUSAGE.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, Usorepertorio.LOG_USER_CREAT.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public BEUsorepertorio Obtener(string owner, string id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DATOS_USOREPERTORIO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@MOD_USAGE", DbType.String, id);

            BEUsorepertorio ent = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BEUsorepertorio();

                    ent.MOD_USAGE = dr.GetString(dr.GetOrdinal("MOD_USAGE"));
                    ent.MOD_DUSAGE = dr.GetString(dr.GetOrdinal("MOD_DUSAGE")).ToUpper();
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        ent.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
            }
            return ent;
        }

        public int Actualizar(BEUsorepertorio Usorepertorio)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_USOREPERTORIO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, Usorepertorio.OWNER.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_USAGE", DbType.String, Usorepertorio.MOD_USAGE.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_DUSAGE", DbType.String, Usorepertorio.MOD_DUSAGE.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, Usorepertorio.LOG_USER_UPDAT.ToUpper());
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ObtenerXDescripcion(BEUsorepertorio Usorepertorio)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_USOREPERTORIO_DESC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, Usorepertorio.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOD_DUSAGE", DbType.String, Usorepertorio.MOD_DUSAGE);
            oDataBase.AddInParameter(oDbCommand, "@MOD_USAGE", DbType.String, Usorepertorio.MOD_USAGE);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public int ObtenerXCodigo(BEUsorepertorio Usorepertorio)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_USOREPERTORIO_COD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, Usorepertorio.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOD_USAGE", DbType.String, Usorepertorio.MOD_USAGE);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public List<BEUsorepertorio> ListarTipo(string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_OBRA_TIPO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);

            var lista = new List<BEUsorepertorio>();
            BEUsorepertorio obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BEUsorepertorio();
                    obs.MOD_USAGE = dr.GetString(dr.GetOrdinal("MOD_USAGE"));
                    obs.MOD_DUSAGE = dr.GetString(dr.GetOrdinal("MOD_DUSAGE"));
                    lista.Add(obs);
                }
            }
            return lista;
        }
    }
}

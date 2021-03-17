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
    public class DATipoUsorepertorio
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BETipoUsorepertorio> usp_Get_UsoRepertorioPage(string param, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TIPOUSOREPERTORIO_PAGE");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            //Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_TIPOUSOREPERTORIO_PAGE", owner, param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BETipoUsorepertorio>();
            var TipoUsorepertorio = new BETipoUsorepertorio();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    TipoUsorepertorio = new BETipoUsorepertorio();
                    TipoUsorepertorio.MOD_REPER = dr.GetString(dr.GetOrdinal("MOD_REPER"));
                    TipoUsorepertorio.MOD_DREPER = dr.GetString(dr.GetOrdinal("MOD_DREPER"));

                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        TipoUsorepertorio.ESTADO = "ACTIVO";
                    else
                        TipoUsorepertorio.ESTADO = "INACTIVO";
                    TipoUsorepertorio.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(TipoUsorepertorio);
                }
            }
            return lista;
        }

        public int Eliminar(BETipoUsorepertorio TipoUsorepertorio)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_INACTIVAR_TIPOUSOREPERTORIO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, TipoUsorepertorio.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOD_REPER", DbType.String, TipoUsorepertorio.MOD_REPER);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, TipoUsorepertorio.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Insertar(BETipoUsorepertorio TipoUsorepertorio)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_TIPOUSOREPERTORIO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, TipoUsorepertorio.OWNER.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_REPER", DbType.String, TipoUsorepertorio.MOD_REPER.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_DREPER", DbType.String, TipoUsorepertorio.MOD_DREPER.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, TipoUsorepertorio.LOG_USER_CREAT.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public BETipoUsorepertorio Obtener(string owner, string id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DATOS_TIPOUSOREPERTORIO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@MOD_REPER", DbType.String, id);

            BETipoUsorepertorio ent = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BETipoUsorepertorio();

                    ent.MOD_REPER = dr.GetString(dr.GetOrdinal("MOD_REPER")).ToUpper();
                    ent.MOD_DREPER = dr.GetString(dr.GetOrdinal("MOD_DREPER")).ToUpper();
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        ent.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
            }
            return ent;
        }

        public int Actualizar(BETipoUsorepertorio TipoUsorepertorio)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_TIPOUSOREPERTORIO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, TipoUsorepertorio.OWNER.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_REPER", DbType.String, TipoUsorepertorio.MOD_REPER.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_DREPER", DbType.String, TipoUsorepertorio.MOD_DREPER.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, TipoUsorepertorio.LOG_USER_UPDAT.ToUpper());
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ObtenerXDescripcion(BETipoUsorepertorio TipoUsorepertorio)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TIPOUSOREPERTORIO_DESC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, TipoUsorepertorio.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOD_REPER", DbType.String, TipoUsorepertorio.MOD_REPER);
            oDataBase.AddInParameter(oDbCommand, "@MOD_DREPER", DbType.String, TipoUsorepertorio.MOD_DREPER);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public int ObtenerXCodigo(BETipoUsorepertorio TipoUsorepertorio)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TIPOUSOREPERTORIO_COD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, TipoUsorepertorio.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOD_REPER", DbType.String, TipoUsorepertorio.MOD_REPER);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public List<BETipoUsorepertorio> ListarTipo(string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_REPORTORIO_TIPO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);

            var lista = new List<BETipoUsorepertorio>();
            BETipoUsorepertorio obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BETipoUsorepertorio();
                    obs.MOD_REPER = dr.GetString(dr.GetOrdinal("MOD_REPER"));
                    obs.MOD_DREPER = dr.GetString(dr.GetOrdinal("MOD_DREPER"));
                    lista.Add(obs);
                }
            }
            return lista;
        }

    }
}

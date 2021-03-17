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
    public class DAModalidadIncidencia
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");


        public List<BEModalidadIncidencia> ListarTipo(string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_INCIDENCIA_OBRA_TIPO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);

            var lista = new List<BEModalidadIncidencia>();
            BEModalidadIncidencia obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BEModalidadIncidencia();
                    obs.MOD_INCID = dr.GetString(dr.GetOrdinal("MOD_INCID"));
                    obs.MOD_IDESC = dr.GetString(dr.GetOrdinal("MOD_IDESC"));
                    lista.Add(obs);
                }
            }
            return lista;
        }

    }
}

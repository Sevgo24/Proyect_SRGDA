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
    public class DATipoNumerador
    {

        public List<BETipoNumerador> ListarTipoNum(string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TIPO_NUMERADORES");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);

            var lista = new List<BETipoNumerador>();
            BETipoNumerador obs;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    obs = new BETipoNumerador();
                    obs.NMR_TYPE = Convert.ToString(reader["NMR_TYPE"]);
                    obs.NMR_TDESC = Convert.ToString(reader["NMR_TDESC"]);
                    lista.Add(obs);
                }
            }
            return lista;
        }


        public List<BETipoNumerador> TipoDocumento(string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TIPO_DOCUMENTO_NUM");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);

            var lista = new List<BETipoNumerador>();
            BETipoNumerador obs;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    obs = new BETipoNumerador();
                    obs.NMR_TYPE = Convert.ToString(reader["NMR_TYPE"]);
                    obs.NMR_TDESC = Convert.ToString(reader["NMR_TDESC"]);
                    lista.Add(obs);
                }
            }
            return lista;
        }

    }
}

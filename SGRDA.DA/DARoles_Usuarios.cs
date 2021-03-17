using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common;
using SGRDA.Entities;
using System.Data.Common;

namespace SGRDA.DA
{
    public class DARoles_Usuarios
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<Roles_Usuarios> USP_GET_ROLES_USUARIOS(string dato)
        {
            Roles_Usuarios be = null;
            List<Roles_Usuarios> lista = new List<Roles_Usuarios>();

            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("USP_GET_ROLES_USUARIOS");
            oDataBase.AddInParameter(oDbCommand, "@NOMBRE", DbType.String, dato);
            oDataBase.ExecuteNonQuery(oDbCommand);

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                {
                    be = new Roles_Usuarios();
                    lista.Add(new Roles_Usuarios(reader));
                }
            }
            return lista;
        }

        public List<Roles_Usuarios> usp_Get_RolesUsuariosPage(string param, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_Get_RolesUsuariosPage");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_Get_RolesUsuariosPage", param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<Roles_Usuarios>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new Roles_Usuarios(reader, Convert.ToInt32(results)));


            }
            return lista;
        }
    }
}

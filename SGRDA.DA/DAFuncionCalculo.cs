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
    public class DAFuncionCalculo
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public List<BEFuncionCalculo> ListarDesplegable(string owner)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_FUNCION_MANT_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);

            List<BEFuncionCalculo> listar = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                listar = new List<BEFuncionCalculo>();
                BEFuncionCalculo funcion = null;
                while (dr.Read())
                {
                    funcion = new BEFuncionCalculo();
                    funcion.FUNC_ID = dr.GetDecimal(dr.GetOrdinal("FUNC_ID"));
                    funcion.FUNC_NAME = dr.GetString(dr.GetOrdinal("FUNC_NAME"));
                    listar.Add(funcion);
                }
            }
            return listar;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using SGRDA.Entities;
using System.Configuration;

namespace SGRDA.DA
{
    public class DACABECERA_MODULO
    {
        public List<CABECERA_MODULO> usp_listar_Cabecera_Modulo()
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("USP_GET_CABECERA_MODULO");
            var lista = new List<CABECERA_MODULO>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new CABECERA_MODULO(reader));
            }
            return lista;
        }
    }
}

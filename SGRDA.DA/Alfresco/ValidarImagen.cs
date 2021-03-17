using Microsoft.Practices.EnterpriseLibrary.Data;
using SGRDA.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.DA.Alfresco
{
    public class ValidarImagen
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");
        public List<BESelectListItem> Listar_PropiedadesAlfresco_x_TipoDocumento(string MREC_ID)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDA_Valida_Imagen");
            oDataBase.AddInParameter(oDbComand, "@MREC_ID", DbType.String, MREC_ID);
            var lista = new List<BESelectListItem>();
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {

                while (reader.Read())
                {
                    BESelectListItem be = new BESelectListItem();
                    be.Value= Convert.ToString(reader["RDG.DOC_PATH "]);
                    lista.Add(be);
                }
            }
            return lista;
        }
    }
}

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
    public class DAUit
    {
        private Database oDatabase = DatabaseFactory.CreateDatabase("conexion");

        public BEUit ListaUit(string owner)
        {
            BEUit entidad = new BEUit();
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_OBTENER_VALOR_UIT");
                oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                
                using (IDataReader dr = oDatabase.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {


                        if (!dr.IsDBNull(dr.GetOrdinal("UITV_VALUEP")))
                            entidad.UITV_VALUEP = dr.GetDecimal(dr.GetOrdinal("UITV_VALUEP"));
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return entidad;

        }
    }
}

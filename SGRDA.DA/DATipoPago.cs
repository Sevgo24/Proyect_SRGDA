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
    public class DATipoPago
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BETipoPago> ListarTipoPago(string owner)
        {
            List<BETipoPago> lst = new List<BETipoPago>();
            BETipoPago item = null;

            using (DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TIPOPAGO"))
            {
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        item = new BETipoPago();
                        item.PAY_ID = dr.GetString(dr.GetOrdinal("PAY_ID"));
                        item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }
    }
}

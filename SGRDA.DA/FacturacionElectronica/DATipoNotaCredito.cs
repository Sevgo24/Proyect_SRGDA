using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using SGRDA.Entities.FacturaElectronica;

namespace SGRDA.DA.FacturacionElectronica
{
    public class DATipoNotaCredito
    {

        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public List<BETipoNotaCredito> ListarTipoNotaCredito(string owner)
        {
            List<BETipoNotaCredito> lst = new List<BETipoNotaCredito>();
            BETipoNotaCredito item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_LISTAR_TIPO_NOTACREDITO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BETipoNotaCredito();
                        item.Code_Description = dr.GetString(dr.GetOrdinal("id"));
                        item.Description = dr.GetString(dr.GetOrdinal("descripcion"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }
    }
}

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
    public class DAFormatoFacturacion
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEFormatoFacturacion> GET_REC_INV_FORMAT(string owner)
        {
            List<BEFormatoFacturacion> lst = new List<BEFormatoFacturacion>();
            BEFormatoFacturacion item = null;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_FORMATOFACTURA"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEFormatoFacturacion();
                        item.INVF_ID = dr.GetDecimal(dr.GetOrdinal("INVF_ID"));
                        item.INVF_DESC = dr.GetString(dr.GetOrdinal("INVF_DESC"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }
    }
}
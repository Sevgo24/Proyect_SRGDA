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
    public class DAContable
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");
        public List<BEContable> ListaContableDesplegable()
        {
            List<BEContable> lst = new List<BEContable>();
            BEContable item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_CONTABLE_DESPLEGABLE"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEContable();
                            item.ACCOUNTANT_ID = dr.GetDecimal(dr.GetOrdinal("ACCOUNTANT_ID"));
                            item.ACCOUNTANT_DESC = dr.GetString(dr.GetOrdinal("ACCOUNTANT_DESC"));
                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception  ex)
            {
                throw;
            }
            return lst;
        }


    }
}

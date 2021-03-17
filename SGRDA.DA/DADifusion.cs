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
    public class DADifusion
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEDifusion> ListarMedioDifusion(string owner)
        {
            List<BEDifusion> lst = new List<BEDifusion>();
            BEDifusion item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_DIFUSION"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEDifusion();
                            item.BROAD_ID = dr.GetDecimal(dr.GetOrdinal("BROAD_ID"));
                            item.BROAD_DESC = dr.GetString(dr.GetOrdinal("BROAD_DESC"));
                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }
    }
}

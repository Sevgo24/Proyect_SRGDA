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
using SGRDA.Entities.WorkFlow;
using System.Data.Common;

namespace SGRDA.DA.WorkFlow
{
    public class DA_WORKF_ACTION_TYPES
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");
        
        public List<WORKF_ACTION_TYPES> ListarItem(string owner)
        {
            List<WORKF_ACTION_TYPES> lst = new List<WORKF_ACTION_TYPES>();
            using (DbCommand cm = db.GetStoredProcCommand("SWFSS_ITEMS_ACTION_TYPES"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        WORKF_ACTION_TYPES item = new WORKF_ACTION_TYPES();
                        item.WRKF_ATID = dr.GetDecimal(dr.GetOrdinal("WRKF_ATID"));                        
                        item.WRKF_ATNAME = dr.GetString(dr.GetOrdinal("WRKF_ATNAME")).ToUpper();
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

    }
}

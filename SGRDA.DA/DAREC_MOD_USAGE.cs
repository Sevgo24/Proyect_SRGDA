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
    public class DAREC_MOD_USAGE
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_MOD_USAGE> Get_REC_MOD_USAGE()
        {
            List<BEREC_MOD_USAGE> lst = new List<BEREC_MOD_USAGE>();
            BEREC_MOD_USAGE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_REC_MOD_USAGE"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_MOD_USAGE();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.MOD_USAGE = dr.GetString(dr.GetOrdinal("MOD_USAGE"));
                            item.MOD_DUSAGE = dr.GetString(dr.GetOrdinal("MOD_DUSAGE"));
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

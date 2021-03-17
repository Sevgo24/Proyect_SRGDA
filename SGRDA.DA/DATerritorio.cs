using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common;
using SGRDA.Entities;
using System.Data.Common;

namespace SGRDA.DA
{
    public class DATerritorio
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BETerritorio> Get_TERRITORIO()
        {
            List<BETerritorio> lst = new List<BETerritorio>();
            BETerritorio item = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("usp_REF_TIS_NAME_LISITEM"))
                {
                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BETerritorio();
                            item.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                            item.COD_TIS_ALPHA = dr.GetString(dr.GetOrdinal("COD_TIS_ALPHA"));
                            item.NAME_TER = dr.GetString(dr.GetOrdinal("TEXT"));
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

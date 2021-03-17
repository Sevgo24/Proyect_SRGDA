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
    public class DAREC_LIC_TYPE
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_LIC_TYPE> GET_REC_LIC_TYPE()
        {
            List<BEREC_LIC_TYPE> lst = new List<BEREC_LIC_TYPE>();
            BEREC_LIC_TYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_REC_LIC_TYPE"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_LIC_TYPE();
                            item.LIC_TYPE = dr.GetDecimal(dr.GetOrdinal("LIC_TYPE"));
                            item.LIC_TDESC = dr.GetString(dr.GetOrdinal("LIC_TDESC"));
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
        public BEREC_LIC_TYPE GET_REC_LIC_TYPE_X_COD(decimal LIC_TYPE)
        {
            BEREC_LIC_TYPE item = null;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_REC_LIC_TYPE_X_COD"))
            {
                db.AddInParameter(cm, "@LIC_TYPE", DbType.Decimal, LIC_TYPE);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BEREC_LIC_TYPE();
                        item.LIC_TYPE = dr.GetDecimal(dr.GetOrdinal("LIC_TYPE"));
                        item.LIC_TDESC = dr.GetString(dr.GetOrdinal("LIC_TDESC"));
                    }
                }
            }
            return item;
        }
    }
}

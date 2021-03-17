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
    public class DAREC_ESTABLISHMENT_GRAL
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_ESTABLISHMENT_GRAL> GET_REC_ESTABLISHMENT_GRAL()
        {
            List<BEREC_ESTABLISHMENT_GRAL> lst = new List<BEREC_ESTABLISHMENT_GRAL>();
            BEREC_ESTABLISHMENT_GRAL item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_REC_ESTABLISHMENT_GRAL"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_ESTABLISHMENT_GRAL();
                            item.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                            if(!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                            {
                                item.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                            }
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
        public BEREC_ESTABLISHMENT_GRAL GET_REC_ESTABLISHMENT_GRAL_X_COD(decimal EST_ID)
        {
            BEREC_ESTABLISHMENT_GRAL item = null;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_REC_ESTABLISHMENT_GRAL_X_COD"))
            {
                db.AddInParameter(cm, "@EST_ID", DbType.Decimal, EST_ID);
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BEREC_ESTABLISHMENT_GRAL();
                        item.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                        {
                            item.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                        }
                    }
                }
            }
            return item;
        }
    }
}

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
    public class DAREC_MODALITY
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_MODALITY> GET_REC_MODALITY(string tmp)
        {
            List<BEREC_MODALITY> lst = new List<BEREC_MODALITY>();
            BEREC_MODALITY item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_REC_MODALITY")) 
                {
                    db.AddInParameter(cm, "@MOD_USAGE", DbType.String, tmp);
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_MODALITY();
                            item.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));
                            item.MOD_DEC = dr.GetString(dr.GetOrdinal("MOD_DEC"));
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
        public BEREC_MODALITY GET_REC_MODALITY_X_COD(decimal MOD_ID)
        {
            BEREC_MODALITY item = null;
            using(DbCommand cm = db.GetStoredProcCommand("SGRDASS_REC_MODALITY_X_COD"))
            {
                db.AddInParameter(cm, "@MOD_ID", DbType.Decimal, MOD_ID);
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BEREC_MODALITY();
                        item.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));
                        item.MOD_DEC = dr.GetString(dr.GetOrdinal("MOD_DEC"));
                    }
                }
            }
            return item;
        }
    }
}

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
    public class DA_WORKF_STATE_TYPE
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<WORKF_STATE_TYPE> ListarItemTiposEstados(string owner, decimal IdCicloAprob)
        {
            List<WORKF_STATE_TYPE> lst = new List<WORKF_STATE_TYPE>();
            using (DbCommand cm = db.GetStoredProcCommand("SWFSS_LISTAR_ITEMS_STATE_TYPE"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@WRKF_ID", DbType.String, IdCicloAprob);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        WORKF_STATE_TYPE item = new WORKF_STATE_TYPE();
                        item.WRKF_STID = dr.GetDecimal(dr.GetOrdinal("WRKF_STID"));
                        item.WRKF_STNAME = dr.GetString(dr.GetOrdinal("WRKF_STNAME"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

        public List<WORKF_STATE_TYPE> ListarTiposEstados(string owner)
        {
            List<WORKF_STATE_TYPE> lst = new List<WORKF_STATE_TYPE>();
            using (DbCommand cm = db.GetStoredProcCommand("SWFSS_LISTAR_STATE_TYPE"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        WORKF_STATE_TYPE item = new WORKF_STATE_TYPE();
                        item.WRKF_STID = dr.GetDecimal(dr.GetOrdinal("WRKF_STID"));
                        item.WRKF_STNAME = dr.GetString(dr.GetOrdinal("WRKF_STNAME"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }
    }
}

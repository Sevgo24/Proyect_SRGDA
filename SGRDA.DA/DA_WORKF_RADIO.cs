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
using SGRDA.Entities;
using SGRDA.Entities.WorkFlow;
using System.Data.Common;

namespace SGRDA.DA.WorkFlow
{
    public class DA_WORKF_RADIO
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<WORKF_RADIO> ListaActualizarEstadoLicRadioDif(string owner)
        {
            List<WORKF_RADIO> lst = new List<WORKF_RADIO>();
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_ESTADO_LIC_RADIO_DIFUSION"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        WORKF_RADIO item = new WORKF_RADIO();
                        item.ID_BSP = dr.GetDecimal(dr.GetOrdinal("bps_id"));
                        item.ID_LIC = dr.GetDecimal(dr.GetOrdinal("lic_id"));
                        item.CANT_FACT_DEUDA = dr.GetInt32(dr.GetOrdinal("factdeuda"));
                        item.LICS_ID = dr.GetDecimal(dr.GetOrdinal("LICS_ID"));
                        item.WRFK_ID = dr.GetDecimal(dr.GetOrdinal("WRFK_ID"));
                       
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

    }
}

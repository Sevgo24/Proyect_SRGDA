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
using System.Data.Common;

namespace SGRDA.DA
{
    public class DAObservacionAgenteRecaudo
    {
        public int InsertarObs(BEObservationAgenteRecaudo obs)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_OBS_AGENTE_RECAUDO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, obs.OWNER);
            oDataBase.AddInParameter(oDbComand, "@OBS_ID", DbType.Int32, obs.OBS_ID);
            oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Int32, obs.BPS_ID);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, obs.LOG_USER_CREAT);
            int n = oDataBase.ExecuteNonQuery(oDbComand);
            return n;
        }

        public List<BEObservationGral> ObservacionXOficina(string owner, decimal idBps)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            List<BEObservationGral> observaciones = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBSERVACION_AGENTE_RECAUDO"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idBps);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {

                        BEObservationGral ObjObs = null;
                        observaciones = new List<BEObservationGral>();
                        while (dr.Read())
                        {
                            ObjObs = new BEObservationGral();
                            ObjObs.OBS_ID = dr.GetDecimal(dr.GetOrdinal("OBS_ID"));
                            ObjObs.OBS_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("OBS_TYPE")));
                            ObjObs.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                            ObjObs.OBS_VALUE = dr.GetString(dr.GetOrdinal("OBS_VALUE"));
                            ObjObs.OBS_USER = dr.GetString(dr.GetOrdinal("OBS_USER"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                                ObjObs.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            observaciones.Add(ObjObs);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return observaciones;
        }


    }
}

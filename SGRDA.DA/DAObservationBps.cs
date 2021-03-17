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
    public class DAObservationBps
    {
        public int Insertar(BEObservationBps obs)
        {
            try
            {
                Database oDataBase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_OBSERVACION_BPS");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, obs.OWNER);
                oDataBase.AddInParameter(oDbComand, "@OBS_ID", DbType.Int32, obs.OBS_ID);
                oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Int32, obs.BPS_ID);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, obs.LOG_USER_CREAT);
                return oDataBase.ExecuteNonQuery(oDbComand);
                
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}

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
    public class DARedSocialEst
    {

        public int Insertar(BERedSocialEst obs)
        {
            int retorno = 0;

            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_REDSOCIAL_EST");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, obs.OWNER);
            oDataBase.AddInParameter(oDbComand, "@EST_ID", DbType.Int32, obs.EST_ID);
            oDataBase.AddInParameter(oDbComand, "@CONT_ID", DbType.Int32, obs.CONT_ID);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, obs.LOG_USER_CREAT.ToUpper());

            retorno = oDataBase.ExecuteNonQuery(oDbComand);
            return retorno;
        }
    }
}

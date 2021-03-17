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
    public class DADocumentoLic
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BEDocumentoLic obs)
        {

            int retorno = 0;
          
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_DOCUMENTO_LIC");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, obs.OWNER);
                oDataBase.AddInParameter(oDbComand, "@DOC_ID", DbType.Int32, obs.DOC_ID);
                oDataBase.AddInParameter(oDbComand, "@LIC_ID", DbType.Int32, obs.LIC_ID);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, obs.LOG_USER_CREAT);
                oDataBase.AddInParameter(oDbComand, "@DOC_ORG", DbType.String, obs.DOC_ORG);
                retorno = oDataBase.ExecuteNonQuery(oDbComand);

           

            return retorno;
        }

        public int InsertarEST(BEDocumentoLic obs)
        {

            int retorno = 0;

            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_DOCUMENTO_EST");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, obs.OWNER);
            oDataBase.AddInParameter(oDbComand, "@DOC_ID", DbType.Int32, obs.DOC_ID);
            oDataBase.AddInParameter(oDbComand, "@LIC_ID", DbType.Int32, obs.LIC_ID);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, obs.LOG_USER_CREAT);
            oDataBase.AddInParameter(oDbComand, "@DOC_ORG", DbType.String, obs.DOC_ORG);
            retorno = oDataBase.ExecuteNonQuery(oDbComand);



            return retorno;
        }


        public int InsertarDocBec(BEDocumentoLic obs)
        {

            int retorno = 0;

            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_DOCUMENTO_BEC");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, obs.OWNER);
            oDataBase.AddInParameter(oDbComand, "@DOC_ID", DbType.Int32, obs.DOC_ID);
            oDataBase.AddInParameter(oDbComand, "@INV_ID", DbType.Int32, obs.LIC_ID);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, obs.LOG_USER_CREAT);
            oDataBase.AddInParameter(oDbComand, "@DOC_ORG", DbType.String, obs.DOC_ORG);
            retorno = oDataBase.ExecuteNonQuery(oDbComand);



            return retorno;
        }
    }
}

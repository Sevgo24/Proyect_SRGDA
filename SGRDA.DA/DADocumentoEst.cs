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
    public class DADocumentoEst
    {
        Database db = new DatabaseProviderFactory().Create("conexion");

        public int Insertar(BEDocumentoEst en)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbComand = db.GetStoredProcCommand("SGRDASI_DOCUMENT_EST");
                db.AddInParameter(oDbComand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbComand, "@EST_ID", DbType.Decimal, en.EST_ID);
                db.AddInParameter(oDbComand, "@DOC_ID", DbType.Decimal, en.DOC_ID);
                db.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                retorno = db.ExecuteNonQuery(oDbComand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }        
    }
}

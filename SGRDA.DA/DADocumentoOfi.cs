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
    public class DADocumentoOfi
    {

        public int Insertar(BEDocumentoOfi par)
        {
            try
            {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASI_DOCUMENTO_OFF");
                oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, par.OWNER);
                oDatabase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, par.OFF_ID);
                oDatabase.AddInParameter(oDbCommand, "@DOC_ID", DbType.Decimal, par.DOC_ID);
                oDatabase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, par.LOG_USER_CREAT);

                int n = oDatabase.ExecuteNonQuery(oDbCommand);
                return n;
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}

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
    public class DAParametrosOfi
    {
        public int Insertar(BEParametroOff par)
        {
            try
            {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASI_PARAMETRO_OFF");
                oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, par.OWNER);
                oDatabase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, par.OFF_ID);
                oDatabase.AddInParameter(oDbCommand, "@PAR_ID", DbType.Decimal, par.PAR_ID);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common;
using SGRDA.Entities;
using System.Data.Common;


namespace SGRDA.DA
{
    public class DALegalizacion
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");
        
        public int Insertar(BELegalizacion leg)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_LEGALIZACION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, leg.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MNR_ID", DbType.Decimal, leg.MNR_ID);
            oDataBase.AddInParameter(oDbCommand, "@LEG_ADJ", DbType.String, leg.LEG_ADJ);
            oDataBase.AddInParameter(oDbCommand, "@MNR_VALUE_ADJ", DbType.Decimal, leg.MNR_VALUE_ADJ);
            oDataBase.AddInParameter(oDbCommand, "@MNR_DOC_ADJ", DbType.String, leg.MNR_DOC_ADJ);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, leg.LOG_USER_CREAT);
            oDataBase.AddOutParameter(oDbCommand, "@LEG_ID", DbType.Decimal, Convert.ToInt32(leg.LEG_ID));

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@LEG_ID"));
            return id;
        }

    }
}

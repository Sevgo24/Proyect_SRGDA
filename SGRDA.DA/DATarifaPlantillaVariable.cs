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
    public class DATarifaPlantillaVariable
    {

        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BETarifaPlantillaVariable variable)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_VARIABLE_TARIFA_PLANTILLA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, variable.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID", DbType.Decimal, variable.TEMP_ID);
            oDataBase.AddInParameter(oDbCommand, "@STARTS", DbType.DateTime, variable.STARTS);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_DESC", DbType.String, variable.TEMP_DESC);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_NVAR", DbType.Decimal, variable.TEMP_NVAR);

            oDataBase.AddInParameter(oDbCommand, "@TEMP_VID1", DbType.Decimal, variable.TEMP_VID1);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_VAR_TRA1", DbType.String, variable.TEMP_VAR_TRA1);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_VID2", DbType.Decimal, variable.TEMP_VID2);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_VAR_TRA2", DbType.String, variable.TEMP_VAR_TRA2);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_VID3", DbType.Decimal, variable.TEMP_VID3);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_VAR_TRA3", DbType.String, variable.TEMP_VAR_TRA3);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_VID4", DbType.Decimal, variable.TEMP_VID4);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_VAR_TRA4", DbType.String, variable.TEMP_VAR_TRA4);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_VID5", DbType.Decimal, variable.TEMP_VID5);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_VAR_TRA5", DbType.String, variable.TEMP_VAR_TRA5);

            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, variable.LOG_USER_CREAT);

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

 

    }
}

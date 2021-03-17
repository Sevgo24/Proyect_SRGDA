using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SGRDA.Entities;
using System.Data.Common;
using System.Data;

namespace SGRDA.DA
{
    public class DADocumentoCampania
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public int Insertar(BECampaniaDoc en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAI_CONTAC_DOCS_CAMP");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CID", DbType.Decimal, en.CONC_CID);
            oDataBase.AddInParameter(oDbCommand, "@DOC_ID", DbType.Decimal, en.DOC_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}

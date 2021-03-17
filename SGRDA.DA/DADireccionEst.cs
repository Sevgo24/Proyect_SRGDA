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
    public class DADireccionEst
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BEDireccionEst en)
        {
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_DIRECCIONES_EST");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, en.EST_ID);
                db.AddInParameter(oDbCommand, "@ADD_ID", DbType.String, en.ADD_ID);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {
                return 0; ;
            }
        }
    }
}

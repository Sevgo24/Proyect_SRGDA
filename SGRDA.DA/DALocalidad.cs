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
    public class DALocalidad
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BELocalidad> Listar(string owner)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASS_LOCALIDAD");
            db.AddInParameter(cm, "@OWNER", DbType.String, owner);

            List<BELocalidad> lst = new List<BELocalidad>();

            using (IDataReader dr = db.ExecuteReader(cm))
            {
                while (dr.Read())
                {
                    BELocalidad obj = new BELocalidad();
                    obj.SECID = dr.GetString(dr.GetOrdinal("SEC_ID"));
                    obj.SEC_DESC = dr.GetString(dr.GetOrdinal("SEC_DESC"));
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public BELocalidad ObtenerLocalidadXCod(string owner, decimal idLocalidad)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASS_LOCALIDAD_X_COD");
            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
            db.AddInParameter(cm, "@SEC_ID", DbType.Decimal, idLocalidad);
            BELocalidad obj = null;

            using (IDataReader dr = db.ExecuteReader(cm))
            {
                while (dr.Read())
                {
                    obj = new BELocalidad();
                    obj.SEC_ID = dr.GetDecimal(dr.GetOrdinal("SEC_ID"));
                    obj.SEC_DESC = dr.GetString(dr.GetOrdinal("SEC_DESC"));
                }
            }
            return obj;
        }
    }
}

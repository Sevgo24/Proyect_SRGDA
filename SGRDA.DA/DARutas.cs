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
    public class DARutas
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BERutas> Get_RUTAS(string owner, string rou_tsel)
        {
            List<BERutas> lst = new List<BERutas>();
            BERutas item = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("UPS_BUSCAR_RUTA"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@ROU_TSEL", DbType.String, rou_tsel);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BERutas();
                            item.ROU_ID = dr.GetDecimal(dr.GetOrdinal("VALUE"));
                            item.ROU_COD = dr.GetString(dr.GetOrdinal("TEXT"));
                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }
    }
}

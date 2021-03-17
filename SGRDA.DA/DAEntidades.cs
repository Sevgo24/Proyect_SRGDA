using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using SGRDA.Entities;
using System.Data.Common;

namespace SGRDA.DA
{
    public class DAEntidades
    {
        private Database oDatabase = new DatabaseProviderFactory().Create("conexion");

        public List<BEEntidades> ListaDropEntidades(string owner)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDAS_ENTIDADES");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            BEEntidades item = null;
            List<BEEntidades> lista = new List<BEEntidades>();

            using (IDataReader dr = oDatabase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEEntidades();
                    if (!dr.IsDBNull(dr.GetOrdinal("ENT_ID")))
                        item.ENT_ID = dr.GetDecimal(dr.GetOrdinal("ENT_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENT_DESC")))
                        item.ENT_DESC = dr.GetString(dr.GetOrdinal("ENT_DESC"));
                    lista.Add(item);
                }
            }
            return lista;
        }
    }
}

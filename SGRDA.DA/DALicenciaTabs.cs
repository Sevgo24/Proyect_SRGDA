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
    public class DALicenciaTabs
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BELicenciaTabs> ListarLicenciaTab(string owner)
        {
            List<BELicenciaTabs> lista = null;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_TABS"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    BELicenciaTabs item = null;
                    lista = new List<BELicenciaTabs>();
                    while (dr.Read())
                    {
                        item = new BELicenciaTabs();
                        item.TAB_ID = dr.GetDecimal(dr.GetOrdinal("TAB_ID"));
                        item.TAB_NAME = dr.GetString(dr.GetOrdinal("TAB_NAME"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }

        public BELicenciaTabs ObtenerNombre(string Owner, decimal Id)
        {
            BELicenciaTabs item = null;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBETENER_TAB_NAME"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                db.AddInParameter(cm, "@TAB_ID", DbType.Decimal, Id);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    dr.Read();
                    item = new BELicenciaTabs();
                    item.TAB_ID = dr.GetDecimal(dr.GetOrdinal("TAB_ID"));
                    item.TAB_NAME = dr.GetString(dr.GetOrdinal("TAB_NAME"));
                }
            }
            return item;
        }
    }
}

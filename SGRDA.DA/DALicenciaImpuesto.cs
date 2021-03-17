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
    public class DALicenciaImpuesto
    {
        private Database oDatabase = DatabaseFactory.CreateDatabase("conexion");

        public List<BEImpuestoValor> ListaImpuesto(string Owner, decimal IdEstablecimiento)
        {
            List<BEImpuestoValor> lista = new List<BEImpuestoValor>();
            BEImpuestoValor item = null;

            using (DbCommand cm = oDatabase.GetStoredProcCommand("SGRDASS_LISTAR_IMP"))
            {

                oDatabase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                oDatabase.AddInParameter(cm, "@EST_ID", DbType.Decimal, IdEstablecimiento);

                using (IDataReader dr = oDatabase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEImpuestoValor();
                        item.TAX_ID = dr.GetDecimal(dr.GetOrdinal("TAX_ID"));
                        item.DIVISION = dr.GetString(dr.GetOrdinal("DIVISION"));
                        item.IMPUESTO = dr.GetString(dr.GetOrdinal("IMPUESTO"));
                        item.TAXV_VALUEP = dr.GetDecimal(dr.GetOrdinal("TAXV_VALUEP"));
                        item.TAXV_VALUEM = dr.GetDecimal(dr.GetOrdinal("TAXV_VALUEM"));
                        lista.Add(item);
                    }
                }
                return lista;
            }
        }     
    }
}

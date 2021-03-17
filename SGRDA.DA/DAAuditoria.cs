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
    public class DAAuditoria
    {
        private Database oDatabase = DatabaseFactory.CreateDatabase("conexion");

        public List<BEAuditoria> ListaAuditoria(string Owner, decimal IdLicencia)
        {
            List<BEAuditoria> lista = new List<BEAuditoria>();
            BEAuditoria item = null;

            using (DbCommand cm = oDatabase.GetStoredProcCommand("SGRDASS_LISTAR_AUDIT"))
            {
                oDatabase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                oDatabase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, IdLicencia);

                using (IDataReader dr = oDatabase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEAuditoria();
                        item.AUDIT_ID = dr.GetDecimal(dr.GetOrdinal("AUDIT_ID"));
                        item.AUDIT_DATE = dr.GetDateTime(dr.GetOrdinal("AUDIT_DATE"));
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        item.AUDITOR = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        item.AUDIT_OBSR = dr.GetString(dr.GetOrdinal("AUDIT_OBSR"));
                        lista.Add(item);
                    }
                }
                return lista;
            }
        } 
    }
}

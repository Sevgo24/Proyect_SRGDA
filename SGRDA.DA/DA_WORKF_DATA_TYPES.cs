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
using SGRDA.Entities.WorkFlow;
using System.Data.Common;

namespace SGRDA.DA.WorkFlow
{
    public class DA_WORKF_DATA_TYPES
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<WORKF_DATA_TYPES> ListarDataTypes(string owner, decimal wrkf_dtid)
        {
            List<WORKF_DATA_TYPES> lst = new List<WORKF_DATA_TYPES>();
            WORKF_DATA_TYPES item = null;
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_LISTAR_DATATYPES"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_DTID", DbType.Decimal, wrkf_dtid);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        item = new WORKF_DATA_TYPES();
                        item.WRKF_DTID = dr.GetDecimal(dr.GetOrdinal("WRKF_DTID"));
                        item.WRKF_DTNAME = dr.GetString(dr.GetOrdinal("WRKF_DTNAME"));
                        item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

        public WORKF_DATA_TYPES ObtenerDataTypes(string owner, decimal wrkf_dtid)
        {
            WORKF_DATA_TYPES item = null;
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_OBTENER_DATATYPES"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_DTID", DbType.Decimal, wrkf_dtid);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {
                        item = new WORKF_DATA_TYPES();
                        item.WRKF_DTID = dr.GetDecimal(dr.GetOrdinal("WRKF_DTID"));
		                item.WRKF_DTNAME = dr.GetString(dr.GetOrdinal("WRKF_DTNAME"));
		                item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if(!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
		                    item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
		                item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
		                item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }
            return item;
        }

        public List<WORKF_DATA_TYPES> ListarItem(string owner)
        {
            List<WORKF_DATA_TYPES> lst = new List<WORKF_DATA_TYPES>();
            using (DbCommand cm = db.GetStoredProcCommand("SWFSS_ITEMS_DATA_TYPES"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        WORKF_DATA_TYPES item = new WORKF_DATA_TYPES();
                        item.WRKF_DTID = dr.GetDecimal(dr.GetOrdinal("WRKF_DTID"));
                        item.WRKF_DTNAME = dr.GetString(dr.GetOrdinal("WRKF_DTNAME")).ToUpper();
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }
    }
}

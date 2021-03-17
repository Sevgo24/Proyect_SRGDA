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
    public class DA_WORKF_TRACES
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<WORKF_TRACES> ListarTraces(string owner, decimal wrkf_tid)
        {
            List<WORKF_TRACES> lst = new List<WORKF_TRACES>();
            WORKF_TRACES item = null;
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_LISTAR_TRACES"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_TID", DbType.Decimal, wrkf_tid);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        item = new WORKF_TRACES();
                        item.WRKF_TID = dr.GetDecimal(dr.GetOrdinal("WRKF_TID"));
                        item.WRKF_AID = dr.GetDecimal(dr.GetOrdinal("WRKF_AID"));
                        item.WRKF_ID = dr.GetDecimal(dr.GetOrdinal("WRKF_ID"));
                        item.WRKF_SID = dr.GetDecimal(dr.GetOrdinal("WRKF_SID"));
                        item.WRKF_REF1 = dr.GetDecimal(dr.GetOrdinal("WRKF_REF1"));
                        item.PROC_MOD = dr.GetDecimal(dr.GetOrdinal("PROC_MOD"));
                        item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        {
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        }
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

        public WORKF_TRACES ObtenerTraces(string owner, decimal wrkf_tid)
        {
            WORKF_TRACES item = null;
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_OBTENER_TRACES"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_TID", DbType.Decimal, wrkf_tid);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {
                        item = new WORKF_TRACES();
                        item.WRKF_TID = dr.GetDecimal(dr.GetOrdinal("WRKF_TID"));
		                item.WRKF_AID = dr.GetDecimal(dr.GetOrdinal("WRKF_AID"));
		                item.WRKF_ID = dr.GetDecimal(dr.GetOrdinal("WRKF_ID"));
		                item.WRKF_SID = dr.GetDecimal(dr.GetOrdinal("WRKF_SID"));
		                item.WRKF_REF1 = dr.GetDecimal(dr.GetOrdinal("WRKF_REF1"));
		                item.PROC_MOD = dr.GetDecimal(dr.GetOrdinal("PROC_MOD"));
		                item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if(!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
		                    item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
		                item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
		                if(!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        {
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }
            return item;
        }
        public decimal ActualizarTrace(WORKF_TRACES entidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSU_TRACES"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@WRKF_TID", DbType.Decimal, entidad.@WRKF_TID);
                db.AddInParameter(oDbCommand, "@WRKF_AID", DbType.Decimal, entidad.WRKF_AID);
                db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, entidad.WRKF_ID);
                db.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, entidad.WRKF_SID);
                db.AddInParameter(oDbCommand, "@WRKF_REF1", DbType.Decimal, entidad.WRKF_REF1);
                db.AddInParameter(oDbCommand, "@PROC_MOD", DbType.Decimal, entidad.PROC_MOD);
                db.AddInParameter(oDbCommand, "@PROC_ID", DbType.Decimal, entidad.PROC_ID);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, entidad.LOG_USER_UPDATE);

                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
        }
        public decimal InsertarTrace(WORKF_TRACES entidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSI_WORKF_TRACES"))
            {
                //decimal r = 0;
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@WRKF_AID", DbType.Decimal, entidad.WRKF_AID);
                db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, entidad.WRKF_ID);
                db.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, entidad.WRKF_SID);
                db.AddInParameter(oDbCommand, "@WRKF_REF1", DbType.Decimal, entidad.WRKF_REF1);
                db.AddInParameter(oDbCommand, "@PROC_MOD", DbType.Decimal, entidad.PROC_MOD);
                db.AddInParameter(oDbCommand, "@PROC_ID", DbType.Decimal, entidad.PROC_ID);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, entidad.LOG_USER_CREAT);
                db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, entidad.WRKF_AMID);
                db.AddOutParameter(oDbCommand, "@retorno", DbType.Decimal,10);

                //r = Convert.ToDecimal(db.GetParameterValue(oDbCommand, "@retorno"));
                //int r = db.ExecuteNonQuery(oDbCommand);
                //return r;

                int r = db.ExecuteNonQuery(oDbCommand);
                decimal results = Convert.ToDecimal(db.GetParameterValue(oDbCommand, "@retorno"));
                return results;

            }
        }

        public List<BETracesLog> LogTraces(string owner, decimal wrkf_ref1)
        {
            List<BETracesLog> lst = new List<BETracesLog>();
            BETracesLog item = null;
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_LOG_TRACES"))
            {
                db.AddInParameter(oDbCommand, "@WRKF_REF1", DbType.Decimal, wrkf_ref1);
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        item = new BETracesLog();
                        item.title = dr.GetString(dr.GetOrdinal("title"));
                        item.description = dr.GetString(dr.GetOrdinal("description"));
                     //   item.startDate = dr.GetDateTime(dr.GetOrdinal("startDate"));
                        item.startDate = dr.GetString(dr.GetOrdinal("startDateText"));
                        
                        if (!dr.IsDBNull(dr.GetOrdinal("endDate")))
                        {
                            item.endDate = dr.GetDateTime(dr.GetOrdinal("endDate"));
                        }
                         
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }
    }
}

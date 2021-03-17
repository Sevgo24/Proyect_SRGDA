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
    public class DA_WORKF_PARAMETERS
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<WORKF_PARAMETERS> ListarParameterXActions(string owner, decimal wrkf_aid)
        {
            List<WORKF_PARAMETERS> lst = new List<WORKF_PARAMETERS>();
            WORKF_PARAMETERS item = null;
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_LISTAR_PARAM_ACTIONS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, wrkf_aid);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        item = new WORKF_PARAMETERS();
                        item.WRKF_PID = dr.GetDecimal(dr.GetOrdinal("WRKF_PID"));
                        item.WRKF_PNAME = dr.GetString(dr.GetOrdinal("WRKF_PNAME"));
                        item.WRKF_PVALUE = dr.GetString(dr.GetOrdinal("WRKF_PVALUE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_PORDER")))
                        {
                            item.WRKF_PORDER = dr.GetDecimal(dr.GetOrdinal("WRKF_PORDER"));
                        }
                        item.WRKF_AID = dr.GetDecimal(dr.GetOrdinal("WRKF_AID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_DTID")))
                        {
                            item.WRKF_DTID = dr.GetDecimal(dr.GetOrdinal("WRKF_DTID"));
                        }
                        item.WRKF_PTID = dr.GetString(dr.GetOrdinal("WRKF_PTID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_IS_ARRAY")))
                            item.WRKF_IS_ARRAY = dr.GetDecimal(dr.GetOrdinal("WRKF_IS_ARRAY"));
                        item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
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

        public WORKF_PARAMETERS ObtenerParameterXActions(string owner, decimal wrkf_aid)
        {
            WORKF_PARAMETERS item = null;
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_OBTENER_PARAM_ACTIONS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_AID", DbType.Decimal, wrkf_aid);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {
                        item = new WORKF_PARAMETERS();
                        item.WRKF_PID = dr.GetDecimal(dr.GetOrdinal("WRKF_PID"));
                        item.WRKF_PNAME = dr.GetString(dr.GetOrdinal("WRKF_PNAME"));
                        item.WRKF_PVALUE = dr.GetString(dr.GetOrdinal("WRKF_PVALUE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_PORDER")))
                        {
                            item.WRKF_PORDER = dr.GetDecimal(dr.GetOrdinal("WRKF_PORDER"));
                        }
                        item.WRKF_AID = dr.GetDecimal(dr.GetOrdinal("WRKF_AID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_DTID")))
                        {
                            item.WRKF_DTID = dr.GetDecimal(dr.GetOrdinal("WRKF_DTID"));
                        }
                        item.WRKF_PTID = dr.GetString(dr.GetOrdinal("WRKF_PTID"));
                        item.WRKF_IS_ARRAY = dr.GetDecimal(dr.GetOrdinal("WRKF_IS_ARRAY"));
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
                    }
                }
            }
            return item;
        }

        public List<WORKF_PARAMETERS> ListarXObjetcs(string owner, decimal oid)
        {
            List<WORKF_PARAMETERS> lst = new List<WORKF_PARAMETERS>();
            WORKF_PARAMETERS item = null;
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_LISTAR_PARAM_OBJECTS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_OID", DbType.Decimal, oid);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        item = new WORKF_PARAMETERS();
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_PID")))
                            item.WRKF_PID = dr.GetDecimal(dr.GetOrdinal("WRKF_PID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_PNAME")))
                            item.WRKF_PNAME = dr.GetString(dr.GetOrdinal("WRKF_PNAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_PVALUE")))
                            item.WRKF_PVALUE = dr.GetString(dr.GetOrdinal("WRKF_PVALUE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_PORDER")))
                            item.WRKF_PORDER = dr.GetDecimal(dr.GetOrdinal("WRKF_PORDER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AMID")))
                            item.WRKF_AMID = dr.GetDecimal(dr.GetOrdinal("WRKF_AMID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_DTID")))
                            item.WRKF_DTID = dr.GetDecimal(dr.GetOrdinal("WRKF_DTID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_PTID")))
                            item.WRKF_PTID = dr.GetString(dr.GetOrdinal("WRKF_PTID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_IS_ARRAY")))
                            item.WRKF_IS_ARRAY = dr.GetDecimal(dr.GetOrdinal("WRKF_IS_ARRAY"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

        public WORKF_PARAMETERS ObtenerParametroTransicion(string owner, decimal mid, decimal wrkfdtid)
        {
            WORKF_PARAMETERS item = null;
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_LISTAR_PARAM_TRANS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, mid);
                db.AddInParameter(oDbCommand, "@WRKF_DTID", DbType.Decimal, wrkfdtid);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {
                        item = new WORKF_PARAMETERS();
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_PID")))
                            item.WRKF_PID = dr.GetDecimal(dr.GetOrdinal("WRKF_PID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_PNAME")))
                            item.WRKF_PNAME = dr.GetString(dr.GetOrdinal("WRKF_PNAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_PVALUE")))
                            item.WRKF_PVALUE = dr.GetString(dr.GetOrdinal("WRKF_PVALUE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_PORDER")))
                            item.WRKF_PORDER = dr.GetDecimal(dr.GetOrdinal("WRKF_PORDER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AMID")))
                            item.WRKF_AMID = dr.GetDecimal(dr.GetOrdinal("WRKF_AMID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_DTID")))
                            item.WRKF_DTID = dr.GetDecimal(dr.GetOrdinal("WRKF_DTID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_PTID")))
                            item.WRKF_PTID = dr.GetString(dr.GetOrdinal("WRKF_PTID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_IS_ARRAY")))
                            item.WRKF_IS_ARRAY = dr.GetDecimal(dr.GetOrdinal("WRKF_IS_ARRAY"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PROC_MOD")))
                            item.PROC_MOD = dr.GetDecimal(dr.GetOrdinal("PROC_MOD"));
                    }
                }
            }
            return item;
        }

        public WORKF_PARAMETERS ObtenerParameterXId(string owner, decimal wrkf_pid)
        {
            WORKF_PARAMETERS item = null;
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSSS_OBTENER_PARAMETRO"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_PID", DbType.Decimal, wrkf_pid);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {
                        item = new WORKF_PARAMETERS();
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_PNAME")))
                            item.WRKF_PNAME = dr.GetString(dr.GetOrdinal("WRKF_PNAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_PVALUE")))
                            item.WRKF_PVALUE = dr.GetString(dr.GetOrdinal("WRKF_PVALUE"));
                    }
                }
            }
            return item;
        }

        public int InsertarParametro(WORKF_PARAMETERS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSSI_PARAMETROS");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_PNAME", DbType.String, en.WRKF_PNAME);
            db.AddInParameter(oDbCommand, "@WRKF_PVALUE", DbType.String, en.WRKF_PVALUE);
            db.AddInParameter(oDbCommand, "@WRKF_PORDER", DbType.Decimal, en.WRKF_PORDER);
            db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, en.WRKF_AMID);
            db.AddInParameter(oDbCommand, "@WRKF_DTID", DbType.Decimal, en.WRKF_DTID);
            db.AddInParameter(oDbCommand, "@WRKF_PTID", DbType.String, en.WRKF_PTID);
            db.AddInParameter(oDbCommand, "@WRKF_OID", DbType.Decimal, en.WRKF_OID);
            db.AddInParameter(oDbCommand, "@PROC_MOD", DbType.Decimal, en.PROC_MOD);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ActualizarParametro(WORKF_PARAMETERS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSSU_PARAMETROS");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_PID", DbType.Decimal, en.WRKF_PID);
            db.AddInParameter(oDbCommand, "@WRKF_PVALUE", DbType.String, en.WRKF_PVALUE);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
        /// <summary>
        /// se agrego para agregar nuevo parametro: WRKF_PORDER
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int ActualizarParametroB(WORKF_PARAMETERS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSSU_PARAMETROSB");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_PID", DbType.Decimal, en.WRKF_PID);
            db.AddInParameter(oDbCommand, "@WRKF_PVALUE", DbType.String, en.WRKF_PVALUE);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            db.AddInParameter(oDbCommand, "@WRKF_PORDER", DbType.String, en.WRKF_PORDER);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int EliminarParametro(WORKF_PARAMETERS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSSD_PARAMETROS");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_PID", DbType.Decimal, en.WRKF_PID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int EliminarTransicionParametro(string owner, decimal idmapping, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAD_PARAMETRO_TRANS");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, idmapping);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ParametroXObjeto(string owner, decimal wrkfId, decimal wrkfsId, decimal objId)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SWFSS_VALIDACION_PARAMETROS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, wrkfId);
            oDataBase.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, wrkfsId);
            oDataBase.AddInParameter(oDbCommand, "@WRKF_OID", DbType.Decimal, objId);
            BEOrigenModalidad obj = new BEOrigenModalidad();
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public List<WORKF_PARAMETERS> ListarParametroTransicion(decimal idTipo, string referencia)
        {
            List<WORKF_PARAMETERS> lst = new List<WORKF_PARAMETERS>();
            WORKF_PARAMETERS item = null;
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_PARAMETROS_CAMBIOESTADO"))
            {
                db.AddInParameter(oDbCommand, "@TIPO", DbType.Decimal, idTipo);
                db.AddInParameter(oDbCommand, "@REF", DbType.String, referencia);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        item = new WORKF_PARAMETERS();
                        if (!dr.IsDBNull(dr.GetOrdinal("DESC")))
                            item.WRKF_PNAME = dr.GetString(dr.GetOrdinal("DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("VALUE")))
                            item.WRKF_PVALUE = dr.GetString(dr.GetOrdinal("VALUE"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }
    }
}

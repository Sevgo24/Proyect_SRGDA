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

namespace SGRDA.DA
{
    public class DAREC_LIC_TAB_STAT
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public BEREC_LIC_TAB_STAT ObtenerDetalle(string owner, decimal tabId, decimal staId, decimal wrkid)
        {
            BEREC_LIC_TAB_STAT Obj = null;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_DETALLE_LICENCIA_TAB"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@TAB_ID", DbType.Decimal, tabId);
                db.AddInParameter(cm, "@WORKF_SID", DbType.Decimal, staId);
                db.AddInParameter(cm, "@WRKF_ID", DbType.Decimal, wrkid);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEREC_LIC_TAB_STAT();
                        if (!dr.IsDBNull(dr.GetOrdinal("SECUENCIA")))
                        {
                            Obj.SECUENCIA = dr.GetDecimal(dr.GetOrdinal("SECUENCIA"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("TAB_ID")))
                        {
                            Obj.TAB_ID = dr.GetDecimal(dr.GetOrdinal("TAB_ID"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("TAB_NAME")))
                        {
                            Obj.TAB_NAME = dr.GetString(dr.GetOrdinal("TAB_NAME"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("WORKF_SID")))
                        {
                            Obj.WORKF_SID = dr.GetDecimal(dr.GetOrdinal("WORKF_SID"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_SNAME")))
                        {
                            Obj.WRKF_SNAME = dr.GetString(dr.GetOrdinal("WRKF_SNAME"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        {
                            Obj.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        {
                            Obj.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        {
                            Obj.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            Obj.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                    }
                }
            }

            return Obj;
        }

        public List<BEREC_LIC_TAB_STAT> TabxEstado(string owner, decimal staId, decimal idwf)
        {
            List<BEREC_LIC_TAB_STAT> lst = null;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_ESTADOLICENCIATAB"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@WORKF_SID", DbType.Decimal, staId);
                db.AddInParameter(cm, "@WRKF_ID", DbType.Decimal, idwf);

                using (IDataReader dr = db.ExecuteReader(cm))
                {

                    BEREC_LIC_TAB_STAT Obj = null;
                    lst = new List<BEREC_LIC_TAB_STAT>();
                    while (dr.Read())
                    {
                        Obj = new BEREC_LIC_TAB_STAT();

                        if (!dr.IsDBNull(dr.GetOrdinal("SECUENCIA")))
                        {
                            Obj.SECUENCIA = dr.GetDecimal(dr.GetOrdinal("SECUENCIA"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("TAB_ID")))
                        {
                            Obj.TAB_ID = dr.GetDecimal(dr.GetOrdinal("TAB_ID"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("TAB_NAME")))
                        {
                            Obj.TAB_NAME = dr.GetString(dr.GetOrdinal("TAB_NAME"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("WORKF_SID")))
                        {
                            Obj.WORKF_SID = dr.GetDecimal(dr.GetOrdinal("WORKF_SID"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_SLABEL")))
                        {
                            Obj.WRKF_SLABEL = dr.GetString(dr.GetOrdinal("WRKF_SLABEL"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        {
                            Obj.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            Obj.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        {
                            Obj.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        {
                            Obj.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }

                        lst.Add(Obj);
                    }
                }
            }

            return lst;
        }

        public int InsertarDetalle(BEREC_LIC_TAB_STAT en)
        {
            DbCommand oDbComand = db.GetStoredProcCommand("SGRDASI_ESTADOLICENCIATAB");
            db.AddInParameter(oDbComand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddOutParameter(oDbComand, "@SECUENCIA", DbType.Decimal, Convert.ToInt32(en.SECUENCIA));
            db.AddInParameter(oDbComand, "@TAB_ID", DbType.Decimal, en.TAB_ID);
            db.AddInParameter(oDbComand, "@WORKF_SID", DbType.Decimal, en.WORKF_SID);
            db.AddInParameter(oDbComand, "@WRKF_ID", DbType.Decimal, en.WORKF_ID);
            db.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int n = db.ExecuteNonQuery(oDbComand);
            int id = Convert.ToInt32(db.GetParameterValue(oDbComand, "@SECUENCIA"));
            return n;
        }

        public int ActualizarDetalle(BEREC_LIC_TAB_STAT en)
        {
            DbCommand oDbComand = db.GetStoredProcCommand("SGRDASU_ESTADOLICENCIATAB");
            db.AddInParameter(oDbComand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbComand, "@SECUENCIA", DbType.Decimal, en.SECUENCIA);
            db.AddInParameter(oDbComand, "@TAB_ID", DbType.Decimal, en.TAB_ID);
            db.AddInParameter(oDbComand, "@WRKF_SID", DbType.Decimal, en.WORKF_SID);
            db.AddInParameter(oDbComand, "@WRKF_ID", DbType.Decimal, en.WORKF_ID);
            db.AddInParameter(oDbComand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            return db.ExecuteNonQuery(oDbComand);
        }

        public int Activar(string owner, decimal secuencia, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTIVAR_ESTADOLICENCIATAB");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@SECUENCIA", DbType.Decimal, secuencia);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(string owner, decimal secuencia, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_INACTIVAR_ESTADOLICENCIATAB");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@SECUENCIA", DbType.Decimal, secuencia);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}

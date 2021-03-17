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
    public class DA_WORKF_STATES_WORKFLOW
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<WORKF_STATES_WORKFLOW> ListarItemEstados(string owner, decimal idCiclo)
        {
            List<WORKF_STATES_WORKFLOW> lst = new List<WORKF_STATES_WORKFLOW>();

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_ITEM_ESTADO_WORKF"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@WRKF_ID", DbType.Decimal, idCiclo);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        WORKF_STATES_WORKFLOW item = new WORKF_STATES_WORKFLOW();
                        item.WRKF_ID = dr.GetDecimal(dr.GetOrdinal("WRKF_ID"));
                        item.WRKF_SID = dr.GetDecimal(dr.GetOrdinal("WRKF_SID"));
                        item.WRKF_SLABEL = dr.GetString(dr.GetOrdinal("WRKF_SLABEL"));
                        //item.WRKF_INI = dr.GetBoolean(dr.GetOrdinal("WRKF_INI"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_INI")))
                            item.WRKF_INI = dr.GetBoolean(dr.GetOrdinal("WRKF_INI"));
                        
                        item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

        public int Insertar(WORKF_STATES_WORKFLOW estado)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_STATES_WORKFLOW");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, estado.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, estado.WRKF_ID);
            db.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, estado.WRKF_SID);
            db.AddInParameter(oDbCommand, "@WRKF_INI", DbType.Boolean, estado.WRKF_INI);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, estado.LOG_USER_CREAT);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Actualizar(WORKF_STATES_WORKFLOW estado)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_STATES_WORKFLOW");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, estado.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, estado.WRKF_ID);
            db.AddInParameter(oDbCommand, "@WRKF_SID_ANT", DbType.Decimal, estado.WRKF_SID_ORIGEN);
            db.AddInParameter(oDbCommand, "@WRKF_SID_NUEVO", DbType.Decimal, estado.WRKF_SID);
            db.AddInParameter(oDbCommand, "@WRKF_INI", DbType.Boolean, estado.WRKF_INI);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, estado.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(WORKF_STATES_WORKFLOW estado)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_STATES_WORKFLOW_ELI");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, estado.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, estado.WRKF_ID);
            db.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, estado.WRKF_SID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, estado.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Activar(WORKF_STATES_WORKFLOW estado)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_STATES_WORKFLOW_ACT");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, estado.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, estado.WRKF_ID);
            db.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, estado.WRKF_SID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, estado.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public WORKF_STATES_WORKFLOW Obtener(string owner, decimal wrkf_id, decimal wrkf_sid, Boolean wrkf_ini)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_STATES_WORKFLOW");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, wrkf_id);
            db.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, wrkf_sid);
            db.AddInParameter(oDbCommand, "@WRKF_INI", DbType.Boolean, wrkf_ini);
            WORKF_STATES_WORKFLOW obj = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    obj = new WORKF_STATES_WORKFLOW();
                    obj.WRKF_ID = dr.GetDecimal(dr.GetOrdinal("WRKF_ID"));
                    obj.WRKF_SID = dr.GetDecimal(dr.GetOrdinal("WRKF_SID"));
                    obj.WRKF_INI = dr.GetBoolean(dr.GetOrdinal("WRKF_INI"));
                }
            }
            return obj;
        }
         public decimal ObtenerEstadoInicial(string owner, decimal wrkf_id )
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_OBTENER_ESTADO_INICIAL_WF");
            db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, wrkf_id);
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            decimal estadoInicial = 0;
            WORKF_STATES_WORKFLOW obj = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    
                    estadoInicial = dr.GetDecimal(dr.GetOrdinal("WRKF_SID"));
                }
            }
            return estadoInicial;
        }

 

    }
}

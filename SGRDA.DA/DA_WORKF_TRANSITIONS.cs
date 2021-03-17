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
    public class DA_WORKF_TRANSITIONS
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public WORKF_TRANSITIONS ObtenerCicloTransitions(string owner, decimal tidWrkf)
        {
            WORKF_TRANSITIONS item = null;

            using (DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_OBTENER_CICLO_TRANS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_TID", DbType.Decimal, tidWrkf);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {
                        item = new WORKF_TRANSITIONS();
                        item.WRKF_TID = dr.GetDecimal(dr.GetOrdinal("WRKF_TID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_NAME")))
                            item.WRKF_NAME = dr.GetString(dr.GetOrdinal("WRKF_NAME")).ToUpper();
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_ENAME")))
                            item.WRKF_ENAME = dr.GetString(dr.GetOrdinal("WRKF_ENAME")).ToUpper();
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_INI")))
                            item.ESTADO_INI = dr.GetString(dr.GetOrdinal("ESTADO_INI")).ToUpper();
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_FIN")))
                            item.ESTADO_FIN = dr.GetString(dr.GetOrdinal("ESTADO_FIN")).ToUpper();
                    }
                }
            }
            return item;
        }

        public WORKF_TRANSITIONS ObtenerTransitions(string owner, decimal? tidWrkf)
        {
            WORKF_TRANSITIONS item = null;

            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_OBTENER_TRANSITIONS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_TID", DbType.Decimal, tidWrkf);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {
                        item = new WORKF_TRANSITIONS();
                        item.WRKF_TID = dr.GetDecimal(dr.GetOrdinal("WRKF_TID"));
                        item.WRKF_ID = dr.GetDecimal(dr.GetOrdinal("WRKF_ID"));
                        item.WRKF_CSTATE = dr.GetDecimal(dr.GetOrdinal("WRKF_CSTATE"));
                        item.WRKF_NSTATE = dr.GetDecimal(dr.GetOrdinal("WRKF_NSTATE"));
                        item.WRKF_EID = dr.GetDecimal(dr.GetOrdinal("WRKF_EID"));
                        item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    }
                }
            }
            return item;
        }

        public WORKF_TRANSITIONS ObtenerTransitionsXActionMapping(string owner, decimal amidWrkf)
        {
            WORKF_TRANSITIONS item = null;

            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_OBTENER_TRANSITIONS_X_ACTIONSMAPPINGS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, amidWrkf);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {
                        item = new WORKF_TRANSITIONS();
                        item.WRKF_TID = dr.GetDecimal(dr.GetOrdinal("WRKF_TID"));
                        item.WRKF_ID = dr.GetDecimal(dr.GetOrdinal("WRKF_ID"));
                        item.WRKF_CSTATE = dr.GetDecimal(dr.GetOrdinal("WRKF_CSTATE"));
                        item.WRKF_NSTATE = dr.GetDecimal(dr.GetOrdinal("WRKF_NSTATE"));
                        item.WRKF_EID = dr.GetDecimal(dr.GetOrdinal("WRKF_EID"));
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

        public List<WORKF_TRANSITIONS> Listar(string owner, decimal idCiclo, decimal idEvento, decimal idEstadoIni, decimal idEstadoFin, int estado, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_LISTAR_TRANSITIONS");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, idCiclo);
            db.AddInParameter(oDbCommand, "@WRKF_EID", DbType.Decimal, idEvento);
            db.AddInParameter(oDbCommand, "@WRKF_CSTATE", DbType.Decimal, idEstadoIni);
            db.AddInParameter(oDbCommand, "@WRKF_NSTATE", DbType.Decimal, idEstadoFin);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, estado);

            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 0);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));
            List<WORKF_TRANSITIONS> lista = null;

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                lista = new List<WORKF_TRANSITIONS>();
                WORKF_TRANSITIONS flujo = null;
                while (dr.Read())
                {
                    flujo = new WORKF_TRANSITIONS();
                    flujo.WRKF_TID = dr.GetDecimal(dr.GetOrdinal("WRKF_TID"));
                    flujo.WRKF_NAME = dr.GetString(dr.GetOrdinal("WRKF_NAME"));
                    flujo.WRKF_ENAME = dr.GetString(dr.GetOrdinal("WRKF_ENAME"));
                    flujo.ESTADO_INI = dr.GetString(dr.GetOrdinal("ESTADO_I"));
                    flujo.ESTADO_FIN = dr.GetString(dr.GetOrdinal("ESTADO_F"));
                    flujo.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    flujo.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        flujo.ESTADO = "ACTIVO";
                    else
                        flujo.ESTADO = "INACTIVO";
                    flujo.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(flujo);
                }
            }
            return lista;
        }

        public decimal Insertar(WORKF_TRANSITIONS entidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSI_TRANSITIONS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, entidad.WRKF_ID);
                db.AddInParameter(oDbCommand, "@WRKF_CSTATE", DbType.Decimal, entidad.WRKF_CSTATE);
                db.AddInParameter(oDbCommand, "@WRKF_NSTATE", DbType.Decimal, entidad.WRKF_NSTATE);
                db.AddInParameter(oDbCommand, "@WRKF_EID", DbType.Decimal, entidad.WRKF_EID);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, entidad.LOG_USER_CREAT);

                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
        }

        public decimal Actualizar(WORKF_TRANSITIONS entidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSU_TRANSITIONS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@WRKF_TID", DbType.Decimal, entidad.WRKF_TID);
                db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, entidad.WRKF_ID);
                db.AddInParameter(oDbCommand, "@WRKF_CSTATE", DbType.Decimal, entidad.WRKF_CSTATE);
                db.AddInParameter(oDbCommand, "@WRKF_NSTATE", DbType.Decimal, entidad.WRKF_NSTATE);
                db.AddInParameter(oDbCommand, "@WRKF_EID", DbType.Decimal, entidad.WRKF_EID);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, entidad.LOG_USER_UPDATE);
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
        }
        
        public decimal Eliminar(WORKF_TRANSITIONS entidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSD_TRANSITIONS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@WRKF_TID", DbType.Decimal, entidad.WRKF_TID);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, entidad.LOG_USER_UPDATE);
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
        }

        public decimal Activar(WORKF_TRANSITIONS entidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSU_TRANSITIONS_ACTIVAR"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@WRKF_TID", DbType.Decimal, entidad.WRKF_TID);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, entidad.LOG_USER_UPDATE);
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
        }

        public List<WORKF_TRANSITIONS> ListarTransicionesWorkflow(string owner, decimal idWorkflow)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_TRANSICIONES_WORKFLOW");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, idWorkflow);
            db.ExecuteNonQuery(oDbCommand);

            List<WORKF_TRANSITIONS> lista = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                lista = new List<WORKF_TRANSITIONS>();
                WORKF_TRANSITIONS transicion = null;
                while (dr.Read())
                {
                    transicion = new WORKF_TRANSITIONS();
                    transicion.WRKF_TID = dr.GetDecimal(dr.GetOrdinal("WRKF_TID"));
                    transicion.WRKF_ID = dr.GetDecimal(dr.GetOrdinal("WRKF_ID"));
                    transicion.WRKF_CSTATE = dr.GetDecimal(dr.GetOrdinal("WRKF_CSTATE"));
                    transicion.ESTADO_INI = dr.GetString(dr.GetOrdinal("WRKF_SLABEL"));
                    transicion.WRKF_NSTATE = dr.GetDecimal(dr.GetOrdinal("WRKF_NSTATE"));
                    transicion.ESTADO_FIN = dr.GetString(dr.GetOrdinal("WRKF_SLABEL_FIN"));
                    transicion.WRKF_EID = dr.GetDecimal(dr.GetOrdinal("WRKF_EID"));
                    transicion.WRKF_ENAME = dr.GetString(dr.GetOrdinal("WRKF_ENAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        transicion.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                    transicion.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    transicion.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        transicion.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        transicion.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                    lista.Add(transicion);
                }
            }
            return lista;
        }

        public WORKF_TRANSITIONS ObtenerTransitionsWorkflow(string owner, decimal? idTran, decimal idWF)
        {
            WORKF_TRANSITIONS item = null;

            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_OBTENER_TRANSITIONS_WORKFLOW"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_TID", DbType.Decimal, idTran);
                db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, idWF);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {
                        item = new WORKF_TRANSITIONS();
                        item.WRKF_TID = dr.GetDecimal(dr.GetOrdinal("WRKF_TID"));
                        item.WRKF_ID = dr.GetDecimal(dr.GetOrdinal("WRKF_ID"));
                        item.WRKF_CSTATE = dr.GetDecimal(dr.GetOrdinal("WRKF_CSTATE"));
                        item.WRKF_NSTATE = dr.GetDecimal(dr.GetOrdinal("WRKF_NSTATE"));
                        item.WRKF_EID = dr.GetDecimal(dr.GetOrdinal("WRKF_EID"));
                        item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    }
                }
            }
            return item;
        }

    }
}

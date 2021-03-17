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
using SGRDA.Entities.WorkFlow;
using System.Data.Common;

namespace SGRDA.DA.WorkFlow
{
    public class DA_WORKF_AGENTS
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<WORKF_AGENTS> Listar(string owner, string nombre, string etiqueta, int estado, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_LISTAR_AGENTS");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@WRKF_AGNAME", DbType.String, nombre);
            db.AddInParameter(oDbCommand, "@WRKF_AGLABEL", DbType.String, etiqueta);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, estado);

            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 0);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));
            List<WORKF_AGENTS> lista = null;

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                lista = new List<WORKF_AGENTS>();
                WORKF_AGENTS agente = null;
                while (dr.Read())
                {
                    agente = new WORKF_AGENTS();
                    agente.WRKF_AGID = dr.GetDecimal(dr.GetOrdinal("WRKF_AGID"));
                    agente.WRKF_AGNAME = dr.GetString(dr.GetOrdinal("WRKF_AGNAME")).ToUpper();
                    agente.WRKF_AGLABEL = dr.GetString(dr.GetOrdinal("WRKF_AGLABEL")).ToUpper();
                    agente.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    agente.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        agente.ESTADO = "ACTIVO";
                    else
                        agente.ESTADO = "INACTIVO";
                    agente.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(agente);
                }
            }
            return lista;
        }

        public WORKF_AGENTS Obtener(string owner, decimal id)
        {
            WORKF_AGENTS item = null;
            using (DbCommand cm = db.GetStoredProcCommand("SWFSS_OBTENER_AGENTS"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@WRKF_AGID", DbType.Decimal, id);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new WORKF_AGENTS();
                        item.WRKF_AGID = dr.GetDecimal(dr.GetOrdinal("WRKF_AGID"));
                        item.WRKF_AGNAME = dr.GetString(dr.GetOrdinal("WRKF_AGNAME"));
                        item.WRKF_AGLABEL = dr.GetString(dr.GetOrdinal("WRKF_AGLABEL"));
                    }
                }
                return item;
            }
        }

        public decimal Eliminar(WORKF_AGENTS entidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSD_AGENTS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@WRKF_AGID", DbType.Decimal, entidad.WRKF_AGID);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, entidad.LOG_USER_UPDATE);
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
        }

        public decimal Actualizar(WORKF_AGENTS entidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSU_AGENTS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@WRKF_AGID", DbType.Decimal, entidad.WRKF_AGID);
                db.AddInParameter(oDbCommand, "@WRKF_AGNAME", DbType.String, entidad.WRKF_AGNAME);
                db.AddInParameter(oDbCommand, "@WRKF_AGLABEL", DbType.String, entidad.WRKF_AGLABEL);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, entidad.LOG_USER_UPDATE);
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
        }

        public decimal Insertar(WORKF_AGENTS entidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSI_AGENTS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@WRKF_AGNAME", DbType.String, entidad.WRKF_AGNAME);
                db.AddInParameter(oDbCommand, "@WRKF_AGLABEL", DbType.String, entidad.WRKF_AGLABEL);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, entidad.LOG_USER_CREAT);
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
        }

        public List<WORKF_AGENTS> ListarReporte(WORKF_AGENTS entidad)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_LISTAR_AGENTS_REP");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_AGNAME", DbType.String, entidad.WRKF_AGNAME);
            db.AddInParameter(oDbCommand, "@WRKF_AGLABEL", DbType.String, entidad.WRKF_AGLABEL);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, entidad.ID_ESTADO);
            db.ExecuteNonQuery(oDbCommand);

            List<WORKF_AGENTS> lista = null;

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                lista = new List<WORKF_AGENTS>();
                WORKF_AGENTS agente = null;
                while (dr.Read())
                {
                    agente = new WORKF_AGENTS();
                    agente.WRKF_AGID = dr.GetDecimal(dr.GetOrdinal("WRKF_AGID"));
                    agente.WRKF_AGNAME = dr.GetString(dr.GetOrdinal("WRKF_AGNAME")).ToUpper();
                    agente.WRKF_AGLABEL = dr.GetString(dr.GetOrdinal("WRKF_AGLABEL")).ToUpper();
                    agente.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        agente.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    
                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        agente.ESTADO = "ACTIVO";
                    else
                        agente.ESTADO = "INACTIVO";
                    lista.Add(agente);
                }
            }
            return lista;
        }

        /// <summary>
        /// Obtiene V o T si el  rol de usuario tiene configurado ejecutar la accion
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idRol"></param>
        /// <param name="idAccion"></param>
        /// <returns></returns>
        public bool TieneRol(string owner, decimal idRol,decimal idAccion)
        {
          
            using (DbCommand cm = db.GetStoredProcCommand("SWFSS_EXISTE_AGENTE_ACCION"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@AGID", DbType.Decimal, idRol);
                db.AddInParameter(cm, "@AID", DbType.Decimal, idAccion);
                db.AddOutParameter(cm, "@EXISTE", DbType.Boolean, 1);
                int r = db.ExecuteNonQuery(cm);
                bool results = Convert.ToBoolean(db.GetParameterValue(cm, "@EXISTE"));
                return results; 
            }
        }


    }
}

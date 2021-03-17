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
    public class DA_WORKF_STATES
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public WORKF_STATES ObtenerEstados(string owner, decimal wrkf_sid)
        {
            WORKF_STATES item = null;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_ESTADO_WORKF"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@WRKF_SID", DbType.Decimal, wrkf_sid);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new WORKF_STATES();
                        item.WRKF_SID = dr.GetDecimal(dr.GetOrdinal("WRKF_SID"));
                        item.WRKF_SNAME = dr.GetString(dr.GetOrdinal("WRKF_SNAME"));
                        item.WRKF_SLABEL = dr.GetString(dr.GetOrdinal("WRKF_SLABEL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_SDESC")))
                        {
                            item.WRKF_SDESC = dr.GetString(dr.GetOrdinal("WRKF_SDESC"));
                        }
                        item.WRKF_STID = dr.GetDecimal(dr.GetOrdinal("WRKF_STID"));
                        item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        {
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
                return item;
            }
        }

        public List<WORKF_STATES> ListarItemEstados(string owner, decimal idCiclo)
        {
            List<WORKF_STATES> lst = new List<WORKF_STATES>();

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_ITEM_ESTADO_WORKF"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@WRKF_ID", DbType.Decimal, idCiclo);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        WORKF_STATES item = new WORKF_STATES();
                        item.WRKF_ID = dr.GetDecimal(dr.GetOrdinal("WRKF_ID"));
                        item.WRKF_SID = dr.GetDecimal(dr.GetOrdinal("WRKF_SID"));
                        item.WRKF_SNAME = dr.GetString(dr.GetOrdinal("WRKF_SNAME"));
                        item.WRKF_SLABEL = dr.GetString(dr.GetOrdinal("WRKF_SLABEL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_SDESC")))
                        {
                            item.WRKF_SDESC = dr.GetString(dr.GetOrdinal("WRKF_SDESC"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_STID")))
                        {
                            item.WRKF_STID = dr.GetDecimal(dr.GetOrdinal("WRKF_STID"));
                        }

                        item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            lst.Add(item);
                    }
                }
            }
            return lst;
        }

        public List<WORKF_STATES> Listar(string owner, string nombre, string etiqueta, decimal idTipoEstado, int estado, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_LISTAR_STATE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@WRKF_SNAME", DbType.String, nombre);
            db.AddInParameter(oDbCommand, "@WRKF_SLABEL", DbType.String, etiqueta);
            db.AddInParameter(oDbCommand, "@WRKF_STID", DbType.Decimal, idTipoEstado);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, estado);

            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 0);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));
            List<WORKF_STATES> lista = null;

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                lista = new List<WORKF_STATES>();
                WORKF_STATES flujo = null;
                while (dr.Read())
                {
                    flujo = new WORKF_STATES();
                    flujo.WRKF_SID = dr.GetDecimal(dr.GetOrdinal("WRKF_SID"));
                    flujo.WRKF_SNAME = dr.GetString(dr.GetOrdinal("WRKF_SNAME"));
                    flujo.WRKF_SLABEL = dr.GetString(dr.GetOrdinal("WRKF_SLABEL"));
                    flujo.WRKF_STID = dr.GetDecimal(dr.GetOrdinal("WRKF_STID"));
                    flujo.WRKF_STNAME = dr.GetString(dr.GetOrdinal("WRKF_STNAME"));
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

        public WORKF_STATES Obtener(string owner, decimal id)
        {
            WORKF_STATES item = null;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_ESTADO_WORKF"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@WRKF_SID", DbType.Decimal, id);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new WORKF_STATES();
                        item.WRKF_SID = dr.GetDecimal(dr.GetOrdinal("WRKF_SID"));
                        item.WRKF_SNAME = dr.GetString(dr.GetOrdinal("WRKF_SNAME"));
                        item.WRKF_SLABEL = dr.GetString(dr.GetOrdinal("WRKF_SLABEL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_SDESC")))
                        {
                            item.WRKF_SDESC = dr.GetString(dr.GetOrdinal("WRKF_SDESC"));
                        }
                        item.WRKF_STID = dr.GetDecimal(dr.GetOrdinal("WRKF_STID"));
                        item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
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

        public int Eliminar(WORKF_STATES entidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSD_STATE"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, entidad.WRKF_SID);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, entidad.LOG_USER_UPDATE);
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
        }

        public int Actualizar(WORKF_STATES entidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSU_STATE"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, entidad.WRKF_SID);
                db.AddInParameter(oDbCommand, "@WRKF_SNAME", DbType.String, entidad.WRKF_SNAME.ToUpper());
                db.AddInParameter(oDbCommand, "@WRKF_SLABEL", DbType.String, entidad.WRKF_SLABEL.ToUpper());
                if (!string.IsNullOrEmpty(entidad.WRKF_SDESC))
                    db.AddInParameter(oDbCommand, "@WRKF_SDESC", DbType.String, entidad.WRKF_SDESC.ToUpper());
                else
                    db.AddInParameter(oDbCommand, "@WRKF_SDESC", DbType.String, entidad.WRKF_SDESC);
                db.AddInParameter(oDbCommand, "@WRKF_STID", DbType.Decimal, entidad.WRKF_STID);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, entidad.LOG_USER_UPDATE);

                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
        }

        public int Insertar(WORKF_STATES entidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSI_STATE"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@WRKF_SNAME", DbType.String, entidad.WRKF_SNAME.ToUpper());
                db.AddInParameter(oDbCommand, "@WRKF_SLABEL", DbType.String, entidad.WRKF_SLABEL.ToUpper());
                if (!string.IsNullOrEmpty(entidad.WRKF_SDESC))
                    db.AddInParameter(oDbCommand, "@WRKF_SDESC", DbType.String, entidad.WRKF_SDESC.ToUpper());
                else
                    db.AddInParameter(oDbCommand, "@WRKF_SDESC", DbType.String, entidad.WRKF_SDESC);
                db.AddInParameter(oDbCommand, "@WRKF_STID", DbType.Decimal, entidad.WRKF_STID);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, entidad.LOG_USER_CREAT);
                db.AddOutParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, Convert.ToInt32(entidad.WRKF_SID));

                int r = db.ExecuteNonQuery(oDbCommand);
                int id = Convert.ToInt32(db.GetParameterValue(oDbCommand, "@WRKF_SID"));
                return id;
            }
        }

        public List<WORKF_STATES> ListarReporte(WORKF_STATES entidad)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_LISTAR_STATE_REP");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_SNAME", DbType.String, entidad.WRKF_SNAME);
            db.AddInParameter(oDbCommand, "@WRKF_SLABEL", DbType.String, entidad.WRKF_SLABEL);
            db.AddInParameter(oDbCommand, "@WRKF_STID", DbType.Decimal, entidad.WRKF_STID);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, entidad.ID_ESTADO);
            db.ExecuteNonQuery(oDbCommand);

            List<WORKF_STATES> lista = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                lista = new List<WORKF_STATES>();
                WORKF_STATES agente = null;
                while (dr.Read())
                {
                    agente = new WORKF_STATES();
                    agente.WRKF_SID = dr.GetDecimal(dr.GetOrdinal("WRKF_SID"));
                    agente.WRKF_SNAME = dr.GetString(dr.GetOrdinal("WRKF_SNAME")).ToUpper();
                    agente.WRKF_SLABEL = dr.GetString(dr.GetOrdinal("WRKF_SLABEL")).ToUpper();
                    agente.WRKF_STNAME = dr.GetString(dr.GetOrdinal("WRKF_STNAME")).ToUpper();
                    agente.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        agente.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    lista.Add(agente);
                }
            }
            return lista;
        }

        public List<WORKF_STATES> ListarItemEstadosPorTipo(string owner, decimal Id)
        {
            List<WORKF_STATES> lst = new List<WORKF_STATES>();

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_ESTADO_X_TIPO"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@WRKF_SID", DbType.Decimal, Id);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        WORKF_STATES item = new WORKF_STATES();
                        item.WRKF_SID = dr.GetDecimal(dr.GetOrdinal("WRKF_SID"));
                        item.WRKF_SNAME = dr.GetString(dr.GetOrdinal("WRKF_SNAME"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

        public List<WORKF_EVENTS> ListaTransicionEstados(string Owner, decimal Id, decimal Idestado)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDAS_LISTA_TRANSICIONES");
            db.AddInParameter(cm, "@OWNER", DbType.String, Owner);
            db.AddInParameter(cm, "@WRKF_ID", DbType.Decimal, Id);
            db.AddInParameter(cm, "@WRKF_SID", DbType.Decimal, Idestado);
            WORKF_EVENTS item = null;
            List<WORKF_EVENTS> lista = new List<WORKF_EVENTS>();

            using (IDataReader dr = db.ExecuteReader(cm))
            {
                while (dr.Read())
                {
                    item = new WORKF_EVENTS();
                    item.WRKF_TID = dr.GetDecimal(dr.GetOrdinal("WRKF_TID"));
                    item.WRKF_LABEL = dr.GetString(dr.GetOrdinal("WRKF_LABEL"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public List<WORKF_STATES> ListarEstadosPorWorkFlow(string Owner, decimal Id)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDAS_ESTADO_X_WORKF");
            db.AddInParameter(cm, "@OWNER", DbType.String, Owner);
            db.AddInParameter(cm, "@WRKF_ID", DbType.Decimal, Id);
            WORKF_STATES item = null;
            List<WORKF_STATES> lista = new List<WORKF_STATES>();

            using (IDataReader dr = db.ExecuteReader(cm))
            {
                while (dr.Read())
                {
                    item = new WORKF_STATES();
                    item.WRKF_SID = dr.GetDecimal(dr.GetOrdinal("WRKF_SID"));
                    item.WRKF_SLABEL = dr.GetString(dr.GetOrdinal("WRKF_SLABEL"));
                    lista.Add(item);
                }
                return lista;
            }
        }

        public List<WORKF_STATES> ListaWorkFlowEstado(string Owner)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDAS_WORKF_ESTADO");
            db.AddInParameter(cm, "@OWNER", DbType.String, Owner);
            WORKF_STATES item = null;
            List<WORKF_STATES> lista = new List<WORKF_STATES>();

            using (IDataReader dr = db.ExecuteReader(cm))
            {
                while (dr.Read())
                {
                    item = new WORKF_STATES();
                    item.WRKF_SID = dr.GetDecimal(dr.GetOrdinal("WRKF_SID"));
                    item.WRKF_SLABEL = dr.GetString(dr.GetOrdinal("WRKF_SLABEL"));
                    lista.Add(item);
                }
                return lista;
            }
        }

        public List<WORKF_STATES> ListarItems(string owner)
        {
            List<WORKF_STATES> lst = new List<WORKF_STATES>();
            using (DbCommand cm = db.GetStoredProcCommand("SWFSS_ESTADO_ITEMS"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        WORKF_STATES item = new WORKF_STATES();
                        item.WRKF_SID = dr.GetDecimal(dr.GetOrdinal("WRKF_SID"));
                        item.WRKF_SLABEL = dr.GetString(dr.GetOrdinal("WRKF_SLABEL"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }


    }
}

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
using SGRDA.Entities.WorkFlow;

namespace SGRDA.DA.WorkFlow
{
    public class DA_WORKF_ACTIONS_MAPPINGS
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(WORKF_ACTIONS_MAPPINGS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAI_ACTION_MAPPING");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_TID", DbType.Decimal, en.WRKF_TID);
            db.AddInParameter(oDbCommand, "@WRKF_AORDER", DbType.Decimal, en.WRKF_AORDER);
            db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, en.WRKF_ID);
            db.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, en.WRKF_SID);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ActualizarPrioridad(WORKF_ACTIONS_MAPPINGS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_PRIORIDAD");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, en.WRKF_AMID);
            db.AddInParameter(oDbCommand, "@WRKF_AMPR", DbType.Decimal, en.WRKF_AMPR);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ActualizarObligatorio(WORKF_ACTIONS_MAPPINGS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_OBLIGATORIO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, en.WRKF_AMID);
            db.AddInParameter(oDbCommand, "@WRKF_AMAND", DbType.String, en.WRKF_AMAND);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ActualizarPrerrequisito(WORKF_ACTIONS_MAPPINGS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_PRERREQUISITO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, en.WRKF_AMID);
            db.AddInParameter(oDbCommand, "@WRKF_AIDPRE", DbType.Decimal, en.WRKF_AIDPRE == 0 ? null : en.WRKF_AIDPRE);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ActualizarNext(WORKF_ACTIONS_MAPPINGS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_NEXT");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, en.WRKF_AMID);
            db.AddInParameter(oDbCommand, "@WRKF_AMTRIGGER", DbType.Decimal, en.WRKF_AMTRIGGER);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ActualizarObjeto(WORKF_ACTIONS_MAPPINGS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_OBJETO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, en.WRKF_AMID);
            db.AddInParameter(oDbCommand, "@WRKF_OID", DbType.Decimal, en.WRKF_OID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int EliminarObjeto(WORKF_ACTIONS_MAPPINGS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAD_OBJETO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, en.WRKF_AMID);
            db.AddInParameter(oDbCommand, "@WRKF_OID", DbType.Decimal, en.WRKF_OID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ActualizarAccion(WORKF_ACTIONS_MAPPINGS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_ACCION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, en.WRKF_AMID);
            db.AddInParameter(oDbCommand, "@WRKF_AID", DbType.Decimal, en.WRKF_AID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ActualizarGrabacion(WORKF_ACTIONS_MAPPINGS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_ACCION_GRABACION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, en.WRKF_SID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ActualizarVisible(WORKF_ACTIONS_MAPPINGS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_VISIBLE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, en.WRKF_AMID);
            db.AddInParameter(oDbCommand, "@WRKF_AMVISIBLE", DbType.String, en.WRKF_AMVISIBLE);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ObtenerOrden(WORKF_ACTIONS_MAPPINGS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_ORDEN_ACCTION_MAPPING");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, en.WRKF_ID);
            db.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, en.WRKF_SID);
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        public int ActualizarOrden(string Owner, decimal? IdMappings, int Orden, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_ACTUALIZAR_ORDEN");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, IdMappings);
            db.AddInParameter(oDbCommand, "@Orden", DbType.String, Orden.ToString());
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ActualizarEvento(WORKF_ACTIONS_MAPPINGS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_EVENTO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, en.WRKF_AMID);
            db.AddInParameter(oDbCommand, "@WRKF_ETRIGGER", DbType.Decimal, en.WRKF_ETRIGGER);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ActualizarTransicion(WORKF_ACTIONS_MAPPINGS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_TRANSICION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, en.WRKF_AMID);
            db.AddInParameter(oDbCommand, "@WRKF_TID", DbType.Decimal, en.WRKF_TID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(WORKF_ACTIONS_MAPPINGS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAD_MAPPINGS");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, en.WRKF_AMID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<WORKF_ACTIONS_MAPPINGS> ObtenerOrdenActualizar(WORKF_ACTIONS_MAPPINGS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_OBTENER_ORDEN");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, en.WRKF_ID);
            db.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, en.WRKF_SID);
            db.AddInParameter(oDbCommand, "@OrdenP", DbType.String, en.WRKF_AORDER);
            WORKF_ACTIONS_MAPPINGS item = null;
            List<WORKF_ACTIONS_MAPPINGS> lista = new List<WORKF_ACTIONS_MAPPINGS>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new WORKF_ACTIONS_MAPPINGS();
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AORDER")))
                        item.WRKF_AORDER = dr.GetString(dr.GetOrdinal("WRKF_AORDER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AMID")))
                        item.WRKF_AMID = dr.GetDecimal(dr.GetOrdinal("WRKF_AMID"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public List<WORKF_ACTIONS_MAPPINGS> Listar(string Owner, decimal Idwrk, decimal Idst)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_ACTION_MAPPINGS");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, Idwrk);
            db.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, Idst);
            WORKF_ACTIONS_MAPPINGS item = null;
            List<WORKF_ACTIONS_MAPPINGS> lista = new List<WORKF_ACTIONS_MAPPINGS>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new WORKF_ACTIONS_MAPPINGS();
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AMID")))
                        item.WRKF_AMID = dr.GetDecimal(dr.GetOrdinal("WRKF_AMID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_SID")))
                        item.WRKF_SID = dr.GetDecimal(dr.GetOrdinal("WRKF_SID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_ID")))
                        item.WRKF_ID = dr.GetDecimal(dr.GetOrdinal("WRKF_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_TID")))
                        item.WRKF_TID = dr.GetDecimal(dr.GetOrdinal("WRKF_TID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AID")))
                        item.WRKF_AID = dr.GetDecimal(dr.GetOrdinal("WRKF_AID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AID")))
                        item.WRKF_AIDAUX = dr.GetDecimal(dr.GetOrdinal("WRKF_AID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AMPR")))
                        item.WRKF_AMPR = dr.GetDecimal(dr.GetOrdinal("WRKF_AMPR"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AMAND")))
                        item.WRKF_AMAND = dr.GetString(dr.GetOrdinal("WRKF_AMAND"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AMVISIBLE")))
                        item.WRKF_AMVISIBLE = dr.GetString(dr.GetOrdinal("WRKF_AMVISIBLE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AIDPRE")))
                        item.WRKF_AIDPRE = dr.GetDecimal(dr.GetOrdinal("WRKF_AIDPRE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_OID")))
                        item.WRKF_OID = dr.GetDecimal(dr.GetOrdinal("WRKF_OID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AORDER")))
                        item.WRKF_AORDERNew = dr.GetString(dr.GetOrdinal("WRKF_AORDER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AORDER")))
                        item.WRKF_AORDER = dr.GetString(dr.GetOrdinal("WRKF_AORDER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_ETRIGGER")))
                        item.WRKF_ETRIGGER = dr.GetDecimal(dr.GetOrdinal("WRKF_ETRIGGER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AMTRIGGER")))
                        item.WRKF_AMTRIGGER = dr.GetDecimal(dr.GetOrdinal("WRKF_AMTRIGGER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_ATIMEMAX")))
                        item.WRKF_ATIMEMAX = dr.GetString(dr.GetOrdinal("WRKF_ATIMEMAX"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_ATIMEMAXP")))
                        item.WRKF_ATIMEMAXP = dr.GetString(dr.GetOrdinal("WRKF_ATIMEMAXP"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public WORKF_ACTIONS_MAPPINGS Obtener(string Owner, decimal IdMapaAccion, decimal IdTransicion, decimal IdAccion)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_MAPING_OBTENER");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, IdMapaAccion);
            db.AddInParameter(oDbCommand, "@WRKF_TID", DbType.Decimal, IdTransicion);
            db.AddInParameter(oDbCommand, "@WRKF_AID", DbType.Decimal, IdAccion);
            WORKF_ACTIONS_MAPPINGS item = null;

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new WORKF_ACTIONS_MAPPINGS();
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AMID")))
                        item.WRKF_AMID = dr.GetDecimal(dr.GetOrdinal("WRKF_AMID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_TID")))
                        item.WRKF_TID = dr.GetDecimal(dr.GetOrdinal("WRKF_TID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AID")))
                        item.WRKF_AID = dr.GetDecimal(dr.GetOrdinal("WRKF_AID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AID")))
                        item.WRKF_AIDAUX = dr.GetDecimal(dr.GetOrdinal("WRKF_AID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AMPR")))
                        item.WRKF_AMPR = dr.GetDecimal(dr.GetOrdinal("WRKF_AMPR"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AMAND")))
                        item.WRKF_AMAND = dr.GetString(dr.GetOrdinal("WRKF_AMAND"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AMVISIBLE")))
                        item.WRKF_AMVISIBLE = dr.GetString(dr.GetOrdinal("WRKF_AMVISIBLE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AIDPRE")))
                        item.WRKF_AIDPRE = dr.GetDecimal(dr.GetOrdinal("WRKF_AIDPRE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_OID")))
                        item.WRKF_OID = dr.GetDecimal(dr.GetOrdinal("WRKF_OID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AORDER")))
                        item.WRKF_AORDERNew = dr.GetString(dr.GetOrdinal("WRKF_AORDER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_ETRIGGER")))
                        item.WRKF_ETRIGGER = dr.GetDecimal(dr.GetOrdinal("WRKF_ETRIGGER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AMTRIGGER")))
                        item.WRKF_AMTRIGGER = dr.GetDecimal(dr.GetOrdinal("WRKF_AMTRIGGER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_ATIMEMAX")))
                        item.WRKF_ATIMEMAX = dr.GetString(dr.GetOrdinal("WRKF_ATIMEMAX"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_ATIMEMAXP")))
                        item.WRKF_ATIMEMAXP = dr.GetString(dr.GetOrdinal("WRKF_ATIMEMAXP"));
                }
            }
            return item;
        }

        public List<WORKF_EVENTS> ListarEvento(string Owner)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_LISTAR_EVENTOS");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            WORKF_EVENTS item = null;
            List<WORKF_EVENTS> lista = new List<WORKF_EVENTS>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new WORKF_EVENTS();
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_EID")))
                        item.WRKF_EID = dr.GetDecimal(dr.GetOrdinal("WRKF_EID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_ENAME")))
                        item.WRKF_ENAME = dr.GetString(dr.GetOrdinal("WRKF_ENAME"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        //public List<WORKF_ACTIONS_MAPPINGS> ObtenerOrdenBajar(string Owner, decimal? IdTransicion, int Orden, decimal workflow, decimal estado)
        public List<WORKF_ACTIONS_MAPPINGS> ObtenerOrdenBajar(string Owner, int Orden, decimal workflow, decimal estado)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_OBTENER_ORDEN_BAJAR");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            //db.AddInParameter(oDbCommand, "@WRKF_TID", DbType.Decimal, IdTransicion);
            db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, workflow);
            db.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, estado);
            db.AddInParameter(oDbCommand, "@Order", DbType.Int32, Orden);
            WORKF_ACTIONS_MAPPINGS item = null;
            List<WORKF_ACTIONS_MAPPINGS> lista = new List<WORKF_ACTIONS_MAPPINGS>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new WORKF_ACTIONS_MAPPINGS();
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AORDER")))
                        item.WRKF_AORDER = dr.GetString(dr.GetOrdinal("WRKF_AORDER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AMID")))
                        item.WRKF_AMID = dr.GetDecimal(dr.GetOrdinal("WRKF_AMID"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        //public List<WORKF_ACTIONS_MAPPINGS> ObtenerOrdenSubir(string Owner, decimal? IdTransicion, int Orden, decimal workflow, decimal estado)
        public List<WORKF_ACTIONS_MAPPINGS> ObtenerOrdenSubir(string Owner, int Orden, decimal workflow, decimal estado)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_OBTENER_ORDEN_SUBIR");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            //db.AddInParameter(oDbCommand, "@WRKF_TID", DbType.Decimal, IdTransicion);
            db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, workflow);
            db.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, estado);
            db.AddInParameter(oDbCommand, "@Order", DbType.Int32, Orden);
            WORKF_ACTIONS_MAPPINGS item = null;
            List<WORKF_ACTIONS_MAPPINGS> lista = new List<WORKF_ACTIONS_MAPPINGS>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new WORKF_ACTIONS_MAPPINGS();
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AORDER")))
                        item.WRKF_AORDER = dr.GetString(dr.GetOrdinal("WRKF_AORDER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AMID")))
                        item.WRKF_AMID = dr.GetDecimal(dr.GetOrdinal("WRKF_AMID"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public int ActualizarOrdenEliminar(WORKF_ACTIONS_MAPPINGS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_ORDEN_MAPPING");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, en.WRKF_AMID);
            db.AddInParameter(oDbCommand, "@WRKF_AORDER", DbType.Decimal, en.WRKF_AORDERNew);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<WORKF_ACTIONS_MAPPINGS> ListarPrerrequisito(string Owner, decimal Idwrk, decimal Idst, decimal? wrkfaId)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_LISTAR_PREREQUISITO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, Idwrk);
            db.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, Idst);
            db.AddInParameter(oDbCommand, "@WRKF_AID", DbType.Decimal, wrkfaId);
            WORKF_ACTIONS_MAPPINGS item = null;
            List<WORKF_ACTIONS_MAPPINGS> lista = new List<WORKF_ACTIONS_MAPPINGS>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new WORKF_ACTIONS_MAPPINGS();
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AIDaux")))
                        item.WRKF_AIDAUXId = dr.GetDecimal(dr.GetOrdinal("WRKF_AIDaux"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AID")))
                        item.WRKF_AID = dr.GetDecimal(dr.GetOrdinal("WRKF_AID"));
                    lista.Add(item);
                }
            }
            return lista;
        }
    }
}

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
    public class DA_WORKF_WORKFLOWS
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public decimal InsertarWorkFlow(WORKF_WORKFLOWS entidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSI_WORKFLOW"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@WRKF_NAME", DbType.String, entidad.WRKF_NAME);
                db.AddInParameter(oDbCommand, "@WRKF_LABEL", DbType.String, entidad.WRKF_LABEL);
                db.AddInParameter(oDbCommand, "@PROC_MOD", DbType.Decimal, entidad.PROC_MOD);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, entidad.LOG_USER_CREAT);
                db.AddOutParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, 0);

                int r = db.ExecuteNonQuery(oDbCommand);
                int id = Convert.ToInt32(db.GetParameterValue(oDbCommand, "@WRKF_ID").ToString());
                return id;
            }
        }

        public WORKF_WORKFLOWS ObtenerWorkFlow(string owner, decimal wrkf_id)
        {
            WORKF_WORKFLOWS item = null;
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_OBTENER_WORKFLOW"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, wrkf_id);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {
                        item = new WORKF_WORKFLOWS();
                        item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        item.WRKF_ID = dr.GetDecimal(dr.GetOrdinal("WRKF_ID"));
                        item.WRKF_NAME = dr.GetString(dr.GetOrdinal("WRKF_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_LABEL")))
                            item.WRKF_LABEL = dr.GetString(dr.GetOrdinal("WRKF_LABEL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PROC_MOD")))
                            item.PROC_MOD = dr.GetDecimal(dr.GetOrdinal("PROC_MOD"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                        item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                    }
                }
            }
            return item;
        }

        public int ActualizarWorkFlow(WORKF_WORKFLOWS entidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSU_WORKFLOWS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, entidad.WRKF_ID);
                db.AddInParameter(oDbCommand, "@WRKF_NAME", DbType.String, entidad.WRKF_NAME);
                db.AddInParameter(oDbCommand, "@WRKF_LABEL", DbType.String, entidad.WRKF_LABEL);
                db.AddInParameter(oDbCommand, "@PROC_MOD", DbType.Decimal, entidad.PROC_MOD);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, entidad.LOG_USER_UPDATE);
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
        }

        public List<WORKF_WORKFLOWS> Listar(string owner, string nombre, string etiqueta, decimal idCliente, int estado, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_LISTAR_WORKFLOW");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@WRKF_NAME", DbType.String, nombre);
            db.AddInParameter(oDbCommand, "@WRKF_LABEL", DbType.String, etiqueta);
            db.AddInParameter(oDbCommand, "@PROC_MOD", DbType.Decimal, idCliente);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, estado);

            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 0);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));
            List<WORKF_WORKFLOWS> lista = null;

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                lista = new List<WORKF_WORKFLOWS>();
                WORKF_WORKFLOWS flujo = null;
                while (dr.Read())
                {
                    flujo = new WORKF_WORKFLOWS();
                    flujo.WRKF_ID = dr.GetDecimal(dr.GetOrdinal("WRKF_ID"));
                    flujo.WRKF_NAME = dr.GetString(dr.GetOrdinal("WRKF_NAME"));
                    flujo.WRKF_LABEL = dr.GetString(dr.GetOrdinal("WRKF_LABEL"));
                    flujo.PROC_MOD = dr.GetDecimal(dr.GetOrdinal("PROC_MOD"));
                    flujo.MOD_DESC = dr.GetString(dr.GetOrdinal("MOD_DESC"));
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

        public decimal EliminarWorkFlow(WORKF_WORKFLOWS entidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSD__WORKFLOW"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, entidad.WRKF_ID);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, entidad.LOG_USER_UPDATE);
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
        }

        public List<WORKF_WORKFLOWS> ListarItems(string owner)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_ITEMS_WORKFLOWS");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.ExecuteNonQuery(oDbCommand);

            List<WORKF_WORKFLOWS> lista = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                lista = new List<WORKF_WORKFLOWS>();
                WORKF_WORKFLOWS flujo = null;
                while (dr.Read())
                {
                    flujo = new WORKF_WORKFLOWS();
                    flujo.WRKF_ID = dr.GetDecimal(dr.GetOrdinal("WRKF_ID"));
                    flujo.WRKF_NAME = dr.GetString(dr.GetOrdinal("WRKF_NAME"));
                    lista.Add(flujo);
                }
            }
            return lista;
        }


    }
}

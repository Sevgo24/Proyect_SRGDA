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
    public class DA_WORKF_OBJECTS
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public WORKF_OBJECTS ObtenerObjectsXActions(string owner, decimal wrkfaid)
        {
            WORKF_OBJECTS item = null;

            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_OBTENER_OBJECTS_X_ACTIONS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_AID", DbType.Decimal, wrkfaid);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        item = new WORKF_OBJECTS();
                        item.WRKF_OID = dr.GetDecimal(dr.GetOrdinal("WRKF_OID"));
                        item.WRKF_OINTID = dr.GetString(dr.GetOrdinal("WRKF_OINTID"));
                        item.WRKF_ODESC = dr.GetString(dr.GetOrdinal("WRKF_ODESC"));
                        item.WRKF_OTID = dr.GetDecimal(dr.GetOrdinal("WRKF_OTID"));
                        item.WRKF_OPATH = dr.GetString(dr.GetOrdinal("WRKF_OPATH"));
                        item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        {
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
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
                    }
                }
            }
            return item;
        }

        public WORKF_OBJECTS ObtenerObjects(string owner, decimal? wrkfoid)
        {
            WORKF_OBJECTS item = null;
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_OBTENER_OBJECTS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_OID", DbType.Decimal, wrkfoid);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {
                        item = new WORKF_OBJECTS();
                        item.WRKF_OID = dr.GetDecimal(dr.GetOrdinal("WRKF_OID"));
                        item.WRKF_OINTID = dr.GetString(dr.GetOrdinal("WRKF_OINTID"));
                        item.WRKF_ODESC = dr.GetString(dr.GetOrdinal("WRKF_ODESC"));
                        item.WRKF_OTID = dr.GetDecimal(dr.GetOrdinal("WRKF_OTID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_OPATH")))
                        {
                            item.WRKF_OPATH = dr.GetString(dr.GetOrdinal("WRKF_OPATH"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_OPATH_JURIDICO")))
                        {
                            item.WRKF_OPATH_JURIDICO = dr.GetString(dr.GetOrdinal("WRKF_OPATH_JURIDICO"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_OSUBJECT")))
                        {
                            item.WRKF_OSUBJECT = dr.GetString(dr.GetOrdinal("WRKF_OSUBJECT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_OBODY")))
                        {
                            item.WRKF_OBODY = dr.GetString(dr.GetOrdinal("WRKF_OBODY"));
                        }
                        item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        {
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
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
                        if (!dr.IsDBNull(dr.GetOrdinal("DOC_TYPE")))
                        {
                            item.DOC_TYPE = dr.GetDecimal(dr.GetOrdinal("DOC_TYPE"));
                        }


                    }
                }
            }
            return item;
        }

        public List<WORKF_OBJECTS> Listar(string owner, string nombre, string codInterno, decimal idTipoObjeto, int estado, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_LISTAR_OBJECTS");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@WRKF_ODESC", DbType.String, nombre);
            db.AddInParameter(oDbCommand, "@WRKF_OINTID", DbType.String, codInterno);
            db.AddInParameter(oDbCommand, "@WRKF_OTID", DbType.Decimal, idTipoObjeto);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, estado);

            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 0);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));
            List<WORKF_OBJECTS> lista = null;

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                lista = new List<WORKF_OBJECTS>();
                WORKF_OBJECTS objeto = null;
                while (dr.Read())
                {
                    objeto = new WORKF_OBJECTS();
                    objeto.WRKF_OID = dr.GetDecimal(dr.GetOrdinal("WRKF_OID"));
                    objeto.WRKF_ODESC = dr.GetString(dr.GetOrdinal("WRKF_ODESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_OINTID")))
                    {
                        objeto.WRKF_OINTID = dr.GetString(dr.GetOrdinal("WRKF_OINTID"));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_OPATH")))
                    {
                        objeto.WRKF_OPATH = dr.GetString(dr.GetOrdinal("WRKF_OPATH"));
                    }
                    objeto.WRKF_OTDESC = dr.GetString(dr.GetOrdinal("WRKF_OTDESC"));
                    objeto.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    objeto.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        objeto.ESTADO = "ACTIVO";
                    else
                        objeto.ESTADO = "INACTIVO";
                    objeto.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(objeto);
                }
            }
            return lista;
        }

        public decimal Eliminar(WORKF_OBJECTS entidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSD_OBJECTS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@WRKF_OID", DbType.Decimal, entidad.WRKF_OID);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, entidad.LOG_USER_UPDATE);
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
        }

        public decimal Insertar(WORKF_OBJECTS entidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSI_OBJECTS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@WRKF_OINTID", DbType.String, entidad.WRKF_OINTID);
                db.AddInParameter(oDbCommand, "@WRKF_ODESC", DbType.String, entidad.WRKF_ODESC.ToUpper());
                db.AddInParameter(oDbCommand, "@WRKF_OTID", DbType.Decimal, entidad.WRKF_OTID);
                //if (entidad.WRKF_OPATH == "file"){ entidad.WRKF_OPATH = "null"; }
                //db.AddInParameter(oDbCommand, "@WRKF_OPATH", DbType.String, entidad.WRKF_OPATH.ToUpper());
                db.AddInParameter(oDbCommand, "@WRKF_OPATH", DbType.String, entidad.WRKF_OPATH != null ? entidad.WRKF_OPATH.ToString().ToUpper() : entidad.WRKF_OPATH);
                db.AddInParameter(oDbCommand, "@WRKF_OSUBJECT", DbType.String, entidad.WRKF_OSUBJECT != null ? entidad.WRKF_OSUBJECT.ToString().ToUpper() : entidad.WRKF_OSUBJECT);
                db.AddInParameter(oDbCommand, "@WRKF_OBODY", DbType.String, entidad.WRKF_OBODY != null ? entidad.WRKF_OBODY.ToString().ToUpper() : entidad.WRKF_OBODY);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, entidad.LOG_USER_CREAT);
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
        }

        public decimal Actualizar(WORKF_OBJECTS entidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSU_OBJECTS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@WRKF_OID", DbType.Decimal, entidad.WRKF_OID);
                db.AddInParameter(oDbCommand, "@WRKF_OINTID", DbType.String, entidad.WRKF_OINTID);
                db.AddInParameter(oDbCommand, "@WRKF_ODESC", DbType.String, entidad.WRKF_ODESC.ToUpper());
                db.AddInParameter(oDbCommand, "@WRKF_OTID", DbType.Decimal, entidad.WRKF_OTID);
                //if (entidad.WRKF_OPATH == "file") { entidad.WRKF_OPATH = ""; }
                //db.AddInParameter(oDbCommand, "@WRKF_OPATH", DbType.String, entidad.WRKF_OPATH.ToUpper());
                db.AddInParameter(oDbCommand, "@WRKF_OPATH", DbType.String, entidad.WRKF_OPATH != null ? entidad.WRKF_OPATH.ToString().ToUpper() : entidad.WRKF_OPATH);
                db.AddInParameter(oDbCommand, "@WRKF_OSUBJECT", DbType.String, entidad.WRKF_OSUBJECT != null ? entidad.WRKF_OSUBJECT.ToString().ToUpper() : entidad.WRKF_OSUBJECT);
                db.AddInParameter(oDbCommand, "@WRKF_OBODY", DbType.String, entidad.WRKF_OBODY != null ? entidad.WRKF_OBODY.ToString().ToUpper() : entidad.WRKF_OBODY);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, entidad.LOG_USER_UPDATE);
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
        }

        public List<WORKF_OBJECTS> ListarObjetosParametros(string owner, decimal wrkfId, decimal wrkfsId)
        {
            List<WORKF_OBJECTS> lst = new List<WORKF_OBJECTS>();
            WORKF_OBJECTS item = null;
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_LISTAR_OBJ_PAR"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, wrkfId);
                db.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, wrkfsId);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        item = new WORKF_OBJECTS();
                        item.WRKF_OID = dr.GetDecimal(dr.GetOrdinal("WRKF_OID"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

        public int ObtenerPlantilla(decimal idModalidad)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_PLANILLA_MODALIDAD"))
            {
                db.AddInParameter(oDbCommand, "@MOD_ID", DbType.String, idModalidad);
                int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
                return r;
            }
        }       

    }
}

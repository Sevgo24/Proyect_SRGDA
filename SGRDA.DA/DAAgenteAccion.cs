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
    public class DAAgenteAccion
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEAgenteAccion> AgenteXAccion(string owner, decimal id, string prefijo)
        {
            //BEAgenteAccion ObjObs = null;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_AGENTExAccion"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@WRKF_AID", DbType.Decimal, id);
                db.AddInParameter(cm, "@PREFIJO", DbType.String, prefijo);


                List<BEAgenteAccion> lista;
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    BEAgenteAccion ObjObs;
                    lista = new List<BEAgenteAccion>();
                    while (dr.Read())
                    {
                        ObjObs = new BEAgenteAccion();

                        ObjObs.WRKF_AGAC_ID = dr.GetDecimal(dr.GetOrdinal("WRKF_AGAC_ID"));
                        ObjObs.WRKF_AID = dr.GetDecimal(dr.GetOrdinal("WRKF_AID"));
                        ObjObs.WRKF_AGID = dr.GetDecimal(dr.GetOrdinal("WRKF_AGID"));
                        ObjObs.Nombre = dr.GetString(dr.GetOrdinal("Nombre"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            ObjObs.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        {
                            ObjObs.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        {
                            ObjObs.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        {
                            ObjObs.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            ObjObs.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        lista.Add(ObjObs);
                    }
                }
                return lista;
            }
        }

        public BEAgenteAccion ObtenerDetalle(string owner, decimal id, decimal idAgente)
        {
            BEAgenteAccion Obj = null;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_AGENTExAccion"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@WRKF_AID", DbType.Decimal, id);
                db.AddInParameter(cm, "@WRKF_AGID", DbType.Decimal, idAgente);
                //db.AddInParameter(cm, "@PREFIJO", DbType.String, GlobalVars.Global.PREFIJO);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEAgenteAccion();
                        Obj.WRKF_AGAC_ID = dr.GetDecimal(dr.GetOrdinal("WRKF_AGAC_ID"));
                        Obj.WRKF_AID = dr.GetDecimal(dr.GetOrdinal("WRKF_AID"));
                        Obj.WRKF_AGID = dr.GetDecimal(dr.GetOrdinal("WRKF_AGID"));
                        Obj.Nombre = dr.GetString(dr.GetOrdinal("Nombre"));

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

        public int InsertarDetalle(BEAgenteAccion det)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_ACCION_AGENTE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_AGID", DbType.Decimal, det.WRKF_AGID);
            db.AddInParameter(oDbCommand, "@WRKF_AID", DbType.Decimal, det.WRKF_AID);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, det.LOG_USER_CREAT);

            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int ActualizarDetalle(BEAgenteAccion en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACCION_AGENTE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_AGAC_ID", DbType.String, en.WRKF_AGAC_ID);
            db.AddInParameter(oDbCommand, "@WRKF_AGID", DbType.String, en.WRKF_AGID);
            db.AddInParameter(oDbCommand, "@WRKF_AID", DbType.String, en.WRKF_AID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);

            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Activar(string owner, decimal id, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTIVAR_ACCION_AGENTE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_AGAC_ID", DbType.String, id);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(string owner, decimal id, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_ACCION_AGENTE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@WRKF_AGAC_ID", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}

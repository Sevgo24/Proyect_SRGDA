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

namespace SGRDA.DA
{
    public class DAAutorizacion
    {
        public List<BEAutorizacion> AutorizacionXLicencia(decimal idLic, string owner)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            List<BEAutorizacion> lst = null;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_AUTORIZACION_X_LIC"))
            {
                db.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    lst = new List<BEAutorizacion>();
                    while (dr.Read())
                    {
                        var obj = new BEAutorizacion();
                        obj.LIC_AUT_ID = dr.GetDecimal(dr.GetOrdinal("LIC_AUT_ID"));
                        obj.LIC_ID= dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        obj.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        obj.LIC_AUT_START = dr.GetDateTime(dr.GetOrdinal("LIC_AUT_START"));
                        obj.LIC_AUT_END = dr.GetDateTime(dr.GetOrdinal("LIC_AUT_END"));
                        obj.LIC_AUT_OBS = dr.GetString(dr.GetOrdinal("LIC_AUT_OBS"));
                        obj.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        obj.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            obj.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        {
                            obj.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        lst.Add(obj);
                    }
                }
            }
            return lst;
        }

        public int Insertar(BEAutorizacion entidad)
        {
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASI_AUTORIZACION");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
            oDatabase.AddInParameter(oDbCommand, "@LIC_ID", DbType.String, entidad.LIC_ID);
            oDatabase.AddInParameter(oDbCommand, "@LIC_AUT_START", DbType.DateTime, entidad.LIC_AUT_START);
            oDatabase.AddInParameter(oDbCommand, "@LIC_AUT_END", DbType.DateTime, entidad.LIC_AUT_END);
            oDatabase.AddInParameter(oDbCommand, "@LIC_AUT_OBS", DbType.String, entidad.LIC_AUT_OBS.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, entidad.LOG_USER_CREAT.ToUpper());

            int n = oDatabase.ExecuteNonQuery(oDbCommand);

            return n;

        }
        public BEAutorizacion ObtenerAutorizacionXLic(string owner, decimal idLic, decimal idAut)
        {
            BEAutorizacion obj = null;
            Database db = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_AUT_LIC"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@LIC_AUT_ID", DbType.Decimal, idAut);
                db.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        obj = new BEAutorizacion();
                        obj.LIC_AUT_ID = dr.GetDecimal(dr.GetOrdinal("LIC_AUT_ID"));
                        obj.LIC_ID= dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        obj.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        obj.LIC_AUT_START = dr.GetDateTime(dr.GetOrdinal("LIC_AUT_START"));
                        obj.LIC_AUT_END = dr.GetDateTime(dr.GetOrdinal("LIC_AUT_END"));
                        obj.LIC_AUT_OBS = dr.GetString(dr.GetOrdinal("LIC_AUT_OBS"));
                        obj.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        obj.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            obj.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        {
                            obj.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }
            return obj;
        }
        public int Actualizar(BEAutorizacion aut)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_AUT_GRAL");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, aut.OWNER);
            db.AddInParameter(oDbCommand, "@LIC_AUT_ID", DbType.Decimal, aut.LIC_AUT_ID);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Int32, aut.LIC_ID);
            db.AddInParameter(oDbCommand, "@LIC_AUT_START", DbType.DateTime, aut.LIC_AUT_START);
            db.AddInParameter(oDbCommand, "@LIC_AUT_END", DbType.DateTime, aut.LIC_AUT_END);
            db.AddInParameter(oDbCommand, "@LIC_AUT_OBS", DbType.String, aut.LIC_AUT_OBS);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, aut.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
        public int Eliminar( decimal id, string owner, string usuDel)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_AUT_GRAL");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LIC_AUT_ID", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, usuDel);
 
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
        public int Activar(decimal id, string owner, string usuDel)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_ACTIVAR_AUT_GRAL");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LIC_AUT_ID", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, usuDel);

            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}

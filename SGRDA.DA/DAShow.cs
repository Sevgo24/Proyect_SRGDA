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
    public class DAShow
    {
        public List<BEShow> ShowsXAutorizaciones(decimal idAutorizacion, string owner)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            List<BEShow> lst = null;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_SHOW_X_AUT"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@LIC_AUT_ID", DbType.Decimal, idAutorizacion);
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    lst = new List<BEShow>();
                    while (dr.Read())
                    {
                        var obj = new BEShow();
                        obj.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        obj.SHOW_ID = dr.GetDecimal(dr.GetOrdinal("SHOW_ID"));
                        obj.LIC_AUT_ID = dr.GetDecimal(dr.GetOrdinal("LIC_AUT_ID"));
                        obj.SHOW_NAME = dr.GetString(dr.GetOrdinal("SHOW_NAME"));
                        obj.SHOW_START = dr.GetDateTime(dr.GetOrdinal("SHOW_START"));
                        obj.SHOW_ENDS = dr.GetDateTime(dr.GetOrdinal("SHOW_ENDS"));
                        obj.SHOW_ORDER = dr.GetDecimal(dr.GetOrdinal("SHOW_ORDER"));
                        obj.SHOW_OBSERV = dr.GetString(dr.GetOrdinal("SHOW_OBSERV"));
                        
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


        public BEShow ObtenerShow(decimal idShow, string owner)
        {
            BEShow obj = null;
            Database db = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_SHOW"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@SHOW_ID", DbType.Decimal, idShow);
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                          obj = new BEShow();
                        obj.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        obj.SHOW_ID = dr.GetDecimal(dr.GetOrdinal("SHOW_ID"));
                        obj.LIC_AUT_ID = dr.GetDecimal(dr.GetOrdinal("LIC_AUT_ID"));
                        obj.SHOW_NAME = dr.GetString(dr.GetOrdinal("SHOW_NAME"));
                        obj.SHOW_START = dr.GetDateTime(dr.GetOrdinal("SHOW_START"));
                        obj.SHOW_ENDS = dr.GetDateTime(dr.GetOrdinal("SHOW_ENDS"));
                        obj.SHOW_ORDER = dr.GetDecimal(dr.GetOrdinal("SHOW_ORDER"));
                        obj.SHOW_OBSERV = dr.GetString(dr.GetOrdinal("SHOW_OBSERV"));

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


        public int Insertar(BEShow entidad)
        {
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASI_SHOW");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
            oDatabase.AddInParameter(oDbCommand, "@LIC_AUT_ID", DbType.String, entidad.LIC_AUT_ID);
            oDatabase.AddInParameter(oDbCommand, "@SHOW_START", DbType.DateTime, entidad.SHOW_START);
            oDatabase.AddInParameter(oDbCommand, "@SHOW_ENDS", DbType.DateTime, entidad.SHOW_ENDS);
            oDatabase.AddInParameter(oDbCommand, "@SHOW_OBSERV", DbType.String, entidad.SHOW_OBSERV.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@SHOW_NAME", DbType.String, entidad.SHOW_NAME.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@SHOW_ORDER", DbType.String, entidad.SHOW_ORDER);
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, entidad.LOG_USER_CREAT.ToUpper());

            int n = oDatabase.ExecuteNonQuery(oDbCommand);

            return n;

        }
        public int Actualizar(BEShow entidad)
        {
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASU_SHOW");
            oDatabase.AddInParameter(oDbCommand, "@SHOW_ID", DbType.Decimal, entidad.SHOW_ID);
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
            oDatabase.AddInParameter(oDbCommand, "@LIC_AUT_ID", DbType.Decimal, entidad.LIC_AUT_ID);
            oDatabase.AddInParameter(oDbCommand, "@SHOW_START", DbType.DateTime, entidad.SHOW_START);
            oDatabase.AddInParameter(oDbCommand, "@SHOW_ENDS", DbType.DateTime, entidad.SHOW_ENDS);
            oDatabase.AddInParameter(oDbCommand, "@SHOW_OBSERV", DbType.String, entidad.SHOW_OBSERV.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@SHOW_NAME", DbType.String, entidad.SHOW_NAME.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@SHOW_ORDER", DbType.Decimal, entidad.SHOW_ORDER);
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, entidad.LOG_USER_UPDATE.ToUpper());

            return oDatabase.ExecuteNonQuery(oDbCommand);
            
        }
        public int Eliminar(decimal id, string owner, string usuDel)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_SHOW");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@SHOW_ID", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, usuDel);

            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
        public int Activar(decimal id, string owner, string usuDel)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTIVAR_SHOW");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@SHOW_ID", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, usuDel);

            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEShow> ListarShowxLicencia(string owner,decimal cod_licencia)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_SHOW_X_LIC");
            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
            db.AddInParameter(cm, "@LIC_ID", DbType.Decimal, cod_licencia);

            List<BEShow> lst = new List<BEShow>();

            using (IDataReader dr = db.ExecuteReader(cm))
            {
                while (dr.Read())
                {
                    BEShow obj = new BEShow();
                    obj.SHOW_ID = dr.GetDecimal(dr.GetOrdinal("SHOW_ID"));
                    obj.SHOW_NAME = dr.GetString(dr.GetOrdinal("SHOW_NAME")).ToUpper();
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public int ValidarShowArtistaPlan(decimal ShowId, int Opcion,string ShowStart,decimal LIC_AUT_ID)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_VALIDA_SHOW_ARTISTAS");
            db.AddInParameter(oDbCommand, "@SHOW_ID", DbType.Decimal, ShowId);
            db.AddInParameter(oDbCommand, "@SHOW_START", DbType.String, ShowStart);
            db.AddInParameter(oDbCommand, "@LIC_AUT_ID", DbType.Decimal, LIC_AUT_ID);
            db.AddInParameter(oDbCommand, "@OPCION", DbType.Int32, Opcion);
            


            int r = Convert.ToInt32( db.ExecuteScalar(oDbCommand));
            return r;
        }
    }
}

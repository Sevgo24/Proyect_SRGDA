using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using SGRDA.Entities;

namespace SGRDA.DA
{
    public class DAEntidadLic
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BEEntidadLic en)
        {
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_ENTIDAD_LIC");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
                db.AddInParameter(oDbCommand, "@LIC_BPS", DbType.Decimal, en.LIC_BPS);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {
                return 0; ;
            }
        }

        public List<BEEntidadLic> Listar (string owner, decimal idLicenciamiento)
        {
            List<BEEntidadLic> lista = null;
            BEEntidadLic item = null;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_ENTIDAD_LIC"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@LIC_BPS", DbType.Decimal, idLicenciamiento);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    lista = new List<BEEntidadLic>();
                    while (dr.Read())
                    {
                        item = new BEEntidadLic();
                        item.LIC_BPS_ID = dr.GetDecimal(dr.GetOrdinal("LIC_BPS_ID"));
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        item.LIC_BPS = dr.GetDecimal(dr.GetOrdinal("LIC_BPS"));

                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                        {
                            item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        {
                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        {
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDAT")))
                        {
                            item.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        lista.Add(item);

                    }
                }
            }
            return lista;
        }

        public BEEntidadLic ObtenerEntidad(string owner, decimal id, decimal idLicencia)
        {
            BEEntidadLic item = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_ENTIDAD_LIC"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LIC_BPS_ID", DbType.Decimal, id);
                oDataBase.AddInParameter(cm, "@LIC_BPS", DbType.Decimal, idLicencia);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEEntidadLic();
                        item.LIC_BPS_ID = dr.GetDecimal(dr.GetOrdinal("LIC_BPS_ID"));
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        item.LIC_BPS = dr.GetDecimal(dr.GetOrdinal("LIC_BPS"));

                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                        {
                            item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("TAXT_ID")))
                        {
                            item.TAXT_ID = dr.GetDecimal(dr.GetOrdinal("TAXT_ID"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                        {
                            item.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                        }
                    }
                }
            }
            return item;
        }

        public int Actualizar(BEEntidadLic en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ENTIDAD_LIC");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@LIC_BPS_ID", DbType.Decimal, en.LIC_BPS_ID);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(string owner, decimal id, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_ENTIDAD_LIC");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LIC_BPS_ID", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, user);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Activar(string owner, decimal id, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTIVAR_ENTIDAD_LIC");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LIC_BPS_ID", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, user);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}

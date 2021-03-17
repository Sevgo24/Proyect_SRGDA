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
    public class DADifusionEst
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BEDifusionEst en)
        {
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_DIFUSION_EST");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, en.EST_ID);
                db.AddInParameter(oDbCommand, "@BROAD_ID", DbType.Decimal, en.BROAD_ID);
                db.AddInParameter(oDbCommand, "@BROADE_NUM", DbType.Decimal, en.BROADE_NUM);
                db.AddInParameter(oDbCommand, "@BROADE_STORAGE", DbType.String, en.BROADE_STORAGE.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Actualizar(BEDifusionEst en)
        {
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_DIFUSION_EST");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, en.EST_ID);
                db.AddInParameter(oDbCommand, "@BROAD_ID", DbType.Decimal, en.BROAD_ID);
                db.AddInParameter(oDbCommand, "@BROADE_NUM", DbType.Decimal, en.BROADE_NUM);
                db.AddInParameter(oDbCommand, "@BROADE_STORAGE", DbType.String, en.BROADE_STORAGE.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT.ToUpper());
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public BEDifusionEst ObtenerDifEst(string owner, decimal idEst, decimal idMed)
        {
            BEDifusionEst Obj = null;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_MEDIOSDIFUSION_EST"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEst);
                db.AddInParameter(cm, "@SEQUENCE", DbType.Decimal, idMed);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEDifusionEst();
                        Obj.SEQUENCE = dr.GetDecimal(dr.GetOrdinal("Id"));
                        Obj.BROAD_ID = dr.GetDecimal(dr.GetOrdinal("BROAD_ID"));
                        Obj.BROADE_NUM = dr.GetDecimal(dr.GetOrdinal("BROADE_NUM"));
                        Obj.BROAD_DESC = dr.GetString(dr.GetOrdinal("BROAD_DESC"));
                        Obj.BROADE_STORAGE = dr.GetString(dr.GetOrdinal("BROADE_STORAGE"));
                    }
                }
            }
            return Obj;
        }

        public List<BEDifusionEst> DifusionXEstablecimiento(string owner, decimal idEst)
        {
            List<BEDifusionEst> parametros = null;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_DIFUSION_EST"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEst);

                using (IDataReader dr = db.ExecuteReader(cm))
                {

                    BEDifusionEst ObjObs = null;
                    parametros = new List<BEDifusionEst>();
                    while (dr.Read())
                    {
                        ObjObs = new BEDifusionEst();
                        ObjObs.SEQUENCE = dr.GetDecimal(dr.GetOrdinal("Id"));
                        ObjObs.BROAD_ID = dr.GetDecimal(dr.GetOrdinal("BROAD_ID"));
                        ObjObs.BROAD_DESC = dr.GetString(dr.GetOrdinal("BROAD_DESC"));
                        ObjObs.BROADE_NUM = dr.GetDecimal(dr.GetOrdinal("BROADE_NUM"));
                        ObjObs.BROADE_STORAGE = dr.GetString(dr.GetOrdinal("BROADE_STORAGE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        {
                            ObjObs.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        {
                            ObjObs.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDAT")))
                        {
                            ObjObs.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            ObjObs.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            ObjObs.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        parametros.Add(ObjObs);

                    }
                }
            }
            return parametros;
        }

        public int Activar(string owner, decimal idEstablecimiento, decimal id, string user)
        {
            DbCommand oDbComand = db.GetStoredProcCommand("SGRDASU_ACTIVAR_MEDIOSDIFUSION_EST");
            db.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbComand, "@SEQUENCE", DbType.Decimal, id);
            db.AddInParameter(oDbComand, "@EST_ID", DbType.Decimal, idEstablecimiento);
            db.AddInParameter(oDbComand, "@LOG_USER_UPDATE", DbType.String, user);
            int n = db.ExecuteNonQuery(oDbComand);
            return n;
        }

        public int Eliminar(string owner, decimal idEstablecimiento, decimal id, string user)
        {
            DbCommand oDbComand = db.GetStoredProcCommand("SGRDASU_INACTIVAR_MEDIOSDIFUSION_EST");
            db.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbComand, "@SEQUENCE", DbType.Decimal, id);
            db.AddInParameter(oDbComand, "@EST_ID", DbType.Decimal, idEstablecimiento);
            db.AddInParameter(oDbComand, "@LOG_USER_UPDATE", DbType.String, user);
            int n = db.ExecuteNonQuery(oDbComand);
            return n;
        }
    }
}

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
    public class DAAsociadosEst
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BEAsociadosEst en)
        {
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_ESTABLECIMIENTO_SOCIO");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
                db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, en.EST_ID);
                db.AddInParameter(oDbCommand, "@ROL_ID", DbType.String, en.ROL_ID);
                db.AddInParameter(oDbCommand, "@BPS_MAIN", DbType.Boolean, en.BPS_MAIN);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {
                return 0; ;
            }
        }

        public List<BEAsociadosEst> AsociadoXEstablecimiento(decimal idEst, string owner)
        {
            List<BEAsociadosEst> item = null;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_CONTACTO_EST"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEst);

                using (IDataReader dr = db.ExecuteReader(cm))
                {

                    BEAsociadosEst ObjObs = null;
                    item = new List<BEAsociadosEst>();
                    while (dr.Read())
                    {
                        ObjObs = new BEAsociadosEst();
                        ObjObs.Id = dr.GetDecimal(dr.GetOrdinal("Id"));
                        ObjObs.ROL_ID = dr.GetString(dr.GetOrdinal("ROL_ID"));
                        ObjObs.ROL_DESC = dr.GetString(dr.GetOrdinal("ROL_DESC"));
                        ObjObs.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));

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
                            ObjObs.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            ObjObs.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            ObjObs.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_MAIN")))
                        {
                            ObjObs.BPS_MAIN = dr.GetBoolean(dr.GetOrdinal("BPS_MAIN"));
                        }
                        item.Add(ObjObs);

                    }
                }
            }
            return item;
        }

        public BEAsociadosEst ObtenerAsoEst(string owner, decimal id, decimal idEst)
        {
            BEAsociadosEst Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_ASOCIADO_EST"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@Id", DbType.String, id);
                oDataBase.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEst);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEAsociadosEst();
                        if (!dr.IsDBNull(dr.GetOrdinal("Id")))
                            Obj.Id = dr.GetDecimal(dr.GetOrdinal("Id"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ROL_ID")))
                            Obj.ROL_ID = dr.GetString(dr.GetOrdinal("ROL_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ROL_DESC")))
                            Obj.ROL_DESC = dr.GetString(dr.GetOrdinal("ROL_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            Obj.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_MAIN")))
                            Obj.BPS_MAIN = dr.GetBoolean(dr.GetOrdinal("BPS_MAIN"));
                    }
                }
            }
            return Obj;
        }

        public int Update(BEAsociadosEst par)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ASOCIADO_EST");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, par.OWNER);
            db.AddInParameter(oDbCommand, "@Id", DbType.String, par.Id);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, par.EST_ID);
            db.AddInParameter(oDbCommand, "@ROL_ID", DbType.Decimal, par.ROL_ID);
            db.AddInParameter(oDbCommand, "@BPS_MAIN", DbType.Decimal, par.BPS_MAIN);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, par.LOG_USER_UPDATE);

            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(string owner, decimal id, decimal idEst, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_ASOCIADO_EST");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@Id", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, idEst);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
        public int Activar(string owner, decimal id, decimal idEst, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTIVAR_ASOCIADO_EST");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@Id", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, idEst);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BEAsociadosEst ObtenerDivisionAdministrativa(string Owner, decimal Id)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_OBTENER_DIV_ASOCIADO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, Id);
            BEAsociadosEst item = null;

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEAsociadosEst();
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DAD_ID")))
                        item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                }
                return item;
            }
        }
    }
}

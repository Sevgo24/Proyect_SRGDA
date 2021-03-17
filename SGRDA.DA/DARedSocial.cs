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
    public class DARedSocial
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BERedes_Sociales doc)
        {

            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_REDES_SOCIALES");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, doc.OWNER);
            oDataBase.AddInParameter(oDbComand, "@CONT_TYPE", DbType.Decimal, doc.CONT_TYPE);

            oDataBase.AddInParameter(oDbComand, "@CONT_OBS", DbType.String, doc.CONT_OBS != null ? doc.CONT_OBS.ToUpper() : string.Empty);
            oDataBase.AddInParameter(oDbComand, "@CONT_DESC", DbType.String, doc.CONT_DESC.ToUpper());
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, doc.LOG_USER_CREAT.ToUpper());
            oDataBase.AddOutParameter(oDbComand, "@CONT_ID", DbType.Int32, Convert.ToInt32(doc.CONT_ID));
            oDataBase.AddInParameter(oDbComand, "@ENT_ID", DbType.Decimal, doc.ENT_ID);

            int n = oDataBase.ExecuteNonQuery(oDbComand);
            int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbComand, "@CONT_ID"));

            return id;

        }

        public BERedes_Sociales ObtenerRedSocialBPS(string owner, decimal RsId, decimal idBps, decimal idEntidad)
        {
            BERedes_Sociales Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_REDSOCIAL_BPS"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@CONT_ID", DbType.Decimal, RsId);
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idBps);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, idEntidad);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BERedes_Sociales();

                        Obj.CONT_ID = dr.GetDecimal(dr.GetOrdinal("CONT_ID"));
                        Obj.CONT_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("CONT_TYPE")));
                        Obj.CONT_DESC = dr.GetString(dr.GetOrdinal("CONT_DESC"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("CONT_OBS")))
                        {
                            Obj.CONT_OBS = dr.GetString(dr.GetOrdinal("CONT_OBS"));
                        }
                    }
                }
            }

            return Obj;
        }

        public BERedes_Sociales ObtenerRedSocialEST(string owner, decimal RsId, decimal idEst, decimal idEntidad)
        {
            BERedes_Sociales Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_REDSOCIAL_EST"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@CONT_ID", DbType.Decimal, RsId);
                oDataBase.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEst);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, idEntidad);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BERedes_Sociales();

                        Obj.CONT_ID = dr.GetDecimal(dr.GetOrdinal("CONT_ID"));
                        Obj.CONT_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("CONT_TYPE")));
                        Obj.CONT_DESC = dr.GetString(dr.GetOrdinal("CONT_DESC"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("CONT_OBS")))
                        {
                            Obj.CONT_OBS = dr.GetString(dr.GetOrdinal("CONT_OBS"));
                        }
                    }
                }
            }

            return Obj;
        }

        public int Update(BERedes_Sociales par)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_REDSOCIAL");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, par.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CONT_ID", DbType.Int32, par.CONT_ID);
            oDataBase.AddInParameter(oDbCommand, "@CONT_TYPE", DbType.Int32, par.CONT_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@CONT_OBS", DbType.String, par.CONT_OBS != null ? par.CONT_OBS.ToUpper() : string.Empty);
            oDataBase.AddInParameter(oDbCommand, "@CONT_DESC", DbType.String, par.CONT_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, par.LOG_USER_UPDATE.ToUpper());

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(string owner, decimal RsId, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_REDSOCIAL");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@CONT_ID", DbType.Decimal, RsId);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Activar(string owner, decimal RsId, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_REDSOCIAL");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@CONT_ID", DbType.Decimal, RsId);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BERedes_Sociales Obtener(string owner, decimal idtipo)
        {

            BERedes_Sociales Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_REDSOCIAL_TYPE"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@CONT_ID", DbType.Decimal, idtipo);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BERedes_Sociales();
                        Obj.CONT_TYPE = dr.GetDecimal(dr.GetOrdinal("CONT_TYPE"));
                        Obj.CONT_TDESC = dr.GetString(dr.GetOrdinal("CONT_TDESC"));
                        Obj.CONT_OBS = dr.GetString(dr.GetOrdinal("CONT_OBS"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }

            return Obj;
        }

        public List<BERedes_Sociales> RedSocialXSocio(decimal idBps, string owner, decimal tipoEntidad)
        {
            List<BERedes_Sociales> parametros = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_REDSOCIAL_BPS"))
            {
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idBps);
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, tipoEntidad);


                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {

                    BERedes_Sociales ObjObs = null;
                    parametros = new List<BERedes_Sociales>();
                    while (dr.Read())
                    {
                        ObjObs = new BERedes_Sociales();

                        ObjObs.CONT_ID = dr.GetDecimal(dr.GetOrdinal("CONT_ID"));
                        ObjObs.CONT_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("CONT_TYPE")));
                        //ObjObs.CONT_TDESC = dr.GetString(dr.GetOrdinal("CONT_DESC"));
                        ObjObs.CONT_DESC = dr.GetString(dr.GetOrdinal("CONT_DESC"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            ObjObs.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("CONT_OBS")))
                        {
                            ObjObs.CONT_OBS = dr.GetString(dr.GetOrdinal("CONT_OBS"));
                        }
                        parametros.Add(ObjObs);

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
                    }
                }
            }

            return parametros;
        }

        public List<BERedes_Sociales> RedSocialXEst(decimal idEst, string owner, decimal tipoEntidad)
        {
            List<BERedes_Sociales> parametros = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_REDSOCIAL_EST"))
            {
                oDataBase.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEst);
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, tipoEntidad);


                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {

                    BERedes_Sociales ObjObs = null;
                    parametros = new List<BERedes_Sociales>();
                    while (dr.Read())
                    {
                        ObjObs = new BERedes_Sociales();

                        ObjObs.CONT_ID = dr.GetDecimal(dr.GetOrdinal("CONT_ID"));
                        ObjObs.CONT_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("CONT_TYPE")));
                        //ObjObs.CONT_TDESC = dr.GetString(dr.GetOrdinal("CONT_DESC"));
                        ObjObs.CONT_DESC = dr.GetString(dr.GetOrdinal("CONT_DESC"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            ObjObs.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("CONT_OBS")))
                        {
                            ObjObs.CONT_OBS = dr.GetString(dr.GetOrdinal("CONT_OBS"));
                        }
                        parametros.Add(ObjObs);

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
                    }
                }
            }

            return parametros;
        }
    }
}

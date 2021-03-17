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
    public class DACorreo
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BECorreo doc)
        {
           
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_CORREO");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, doc.OWNER);
                oDataBase.AddInParameter(oDbComand, "@MAIL_TYPE", DbType.Decimal, doc.MAIL_TYPE);

                oDataBase.AddInParameter(oDbComand, "@MAIL_OBS", DbType.String, doc.MAIL_OBS != null ? doc.MAIL_OBS.ToUpper() : string.Empty);
                oDataBase.AddInParameter(oDbComand, "@MAIL_DESC", DbType.String, doc.MAIL_DESC.ToUpper());
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, doc.LOG_USER_CREAT.ToUpper());
                oDataBase.AddOutParameter(oDbComand, "@MAIL_ID", DbType.Int32, Convert.ToInt32(doc.MAIL_ID));
                oDataBase.AddInParameter(oDbComand, "@ENT_ID", DbType.Decimal, doc.ENT_ID);

                int n = oDataBase.ExecuteNonQuery(oDbComand);
                int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbComand, "@MAIL_ID"));

                return id;
            
        }

        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="idFono"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public List<BECorreo> CorreoXSocio(decimal idBps, string owner, decimal tipoEntidad)
        {
            List<BECorreo> parametros = null;
          
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_CORREO_BPS"))
                {
                    oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idBps);
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, tipoEntidad);


                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {

                        BECorreo ObjObs = null;
                        parametros = new List<BECorreo>();
                        while (dr.Read())
                        {
                            ObjObs = new BECorreo();

                            ObjObs.MAIL_ID = dr.GetDecimal(dr.GetOrdinal("MAIL_ID"));
                            ObjObs.MAIL_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("MAIL_TYPE")));
                            ObjObs.MAIL_DESC = dr.GetString(dr.GetOrdinal("MAIL_DESC"));


                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                ObjObs.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("MAIL_OBS")))
                            {
                                ObjObs.MAIL_OBS = dr.GetString(dr.GetOrdinal("MAIL_OBS"));
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

        public List<BECorreo> CorreoXEst(decimal idEst, string owner, decimal tipoEntidad)
        {
            List<BECorreo> parametros = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_CORREO_EST"))
            {
                oDataBase.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEst);
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, tipoEntidad);


                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {

                    BECorreo ObjObs = null;
                    parametros = new List<BECorreo>();
                    while (dr.Read())
                    {
                        ObjObs = new BECorreo();

                        ObjObs.MAIL_ID = dr.GetDecimal(dr.GetOrdinal("MAIL_ID"));
                        ObjObs.MAIL_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("MAIL_TYPE")));
                        ObjObs.MAIL_DESC = dr.GetString(dr.GetOrdinal("MAIL_DESC"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            ObjObs.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("MAIL_OBS")))
                        {
                            ObjObs.MAIL_OBS = dr.GetString(dr.GetOrdinal("MAIL_OBS"));
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


        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idDireccion"></param>
        /// <param name="idFono"></param>
        /// <returns></returns>
        public BECorreo ObtenerMailBPS(string owner, decimal maiId, decimal idBps,decimal idEntidad)
        {
            BECorreo Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_CORREO_BPS"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@MAIL_ID", DbType.Decimal, maiId);
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idBps);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, idEntidad);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BECorreo();

                        Obj.MAIL_ID = dr.GetDecimal(dr.GetOrdinal("MAIL_ID"));
                        Obj.MAIL_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("MAIL_TYPE")));
                        Obj.MAIL_DESC = dr.GetString(dr.GetOrdinal("MAIL_DESC"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("MAIL_OBS")))
                        {
                            Obj.MAIL_OBS = dr.GetString(dr.GetOrdinal("MAIL_OBS"));
                        }


                       
                    }
                }
            }

            return Obj;
        }

        public BECorreo ObtenerMailEst(string owner, decimal maiId, decimal idEst, decimal idEntidad)
        {
            BECorreo Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_CORREO_EST"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@MAIL_ID", DbType.Decimal, maiId);
                oDataBase.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEst);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, idEntidad);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BECorreo();

                        Obj.MAIL_ID = dr.GetDecimal(dr.GetOrdinal("MAIL_ID"));
                        Obj.MAIL_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("MAIL_TYPE")));
                        Obj.MAIL_DESC = dr.GetString(dr.GetOrdinal("MAIL_DESC"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("MAIL_OBS")))
                        {
                            Obj.MAIL_OBS = dr.GetString(dr.GetOrdinal("MAIL_OBS"));
                        }



                    }
                }
            }

            return Obj;
        }

        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="parId"></param>
        /// <param name="user"></param>
        /// <returns></returns> 
        public int Eliminar(string owner, decimal maiId, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_CORREO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@MAIL_ID", DbType.Decimal, maiId); 
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
        /// <summary> 
        /// addon dbs 20140727
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="parId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Activar(string owner, decimal maiId, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_CORREO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@MAIL_ID", DbType.Decimal, maiId);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="obs"></param>
        /// <returns></returns>
        public int Update(BECorreo par)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_CORREO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, par.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MAIL_ID", DbType.Int32, par.MAIL_ID);
            oDataBase.AddInParameter(oDbCommand, "@MAIL_TYPE", DbType.Int32, par.MAIL_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@MAIL_OBS", DbType.String, par.MAIL_OBS != null ? par.MAIL_OBS.ToUpper() : string.Empty);
            oDataBase.AddInParameter(oDbCommand, "@MAIL_DESC", DbType.String, par.MAIL_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, par.LOG_USER_UPDATE.ToUpper());

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }



        public List<string> CorreoNotificarUsuLic(decimal idLic, string owner)
        {
            List<string> correos = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_CORREOS_USU_LIC"))
            {
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    correos = new List<string>();
                    while (dr.Read())
                    {
                        correos.Add(dr.GetString(dr.GetOrdinal("MAIL_DESC")));
                    }
                }
            }
            return correos;
        }

         
         



    }
}

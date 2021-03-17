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
    public class DATelefono
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BETelefono doc)
        {
           
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_TELEFONO");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, doc.OWNER);
                oDataBase.AddInParameter(oDbComand, "@PHONE_TYPE", DbType.Decimal, doc.PHONE_TYPE);
                oDataBase.AddInParameter(oDbComand, "@PHONE_OBS", DbType.String, doc.PHONE_OBS!= null ? doc.PHONE_OBS.ToUpper():string.Empty);
                oDataBase.AddInParameter(oDbComand, "@PHONE_NUMBER", DbType.String, doc.PHONE_NUMBER.ToUpper());
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, doc.LOG_USER_CREAT.ToUpper());
                oDataBase.AddOutParameter(oDbComand, "@PHONE_ID", DbType.Int32, Convert.ToInt32(doc.PHONE_ID));
                oDataBase.AddInParameter(oDbComand, "@ENT_ID", DbType.Decimal, doc.ENT_ID);

                int n = oDataBase.ExecuteNonQuery(oDbComand);
                int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbComand, "@PHONE_ID"));

                return id;
           
        }

        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="idFono"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public List<BETelefono> TelefonoXSocio(decimal idBps, string owner, decimal tipoEntidad)
        {
            List<BETelefono> parametros = null;
           
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TELEFONO_BPS"))
                {
                    oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idBps);
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, tipoEntidad);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {

                        BETelefono ObjObs = null;
                        parametros = new List<BETelefono>();
                        while (dr.Read())
                        {
                            ObjObs = new BETelefono();

                            ObjObs.PHONE_ID = dr.GetDecimal(dr.GetOrdinal("PHONE_ID"));
                            ObjObs.PHONE_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("PHONE_TYPE")));
                            ObjObs.PHONE_NUMBER = dr.GetString(dr.GetOrdinal("PHONE_NUMBER"));

                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                ObjObs.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("PHONE_OBS")))
                            {
                                ObjObs.PHONE_OBS = dr.GetString(dr.GetOrdinal("PHONE_OBS"));
                            }
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
                            parametros.Add(ObjObs);

                        }
                    }
                }
         
            return parametros;
        }

        public List<BETelefono> TelefonoXEst(decimal idEst, string owner, decimal tipoEntidad)
        {
            List<BETelefono> parametros = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TELEFONO_EST"))
            {
                oDataBase.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEst);
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, tipoEntidad);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {

                    BETelefono ObjObs = null;
                    parametros = new List<BETelefono>();
                    while (dr.Read())
                    {
                        ObjObs = new BETelefono();

                        ObjObs.PHONE_ID = dr.GetDecimal(dr.GetOrdinal("PHONE_ID"));
                        ObjObs.PHONE_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("PHONE_TYPE")));
                        ObjObs.PHONE_NUMBER = dr.GetString(dr.GetOrdinal("PHONE_NUMBER"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            ObjObs.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("PHONE_OBS")))
                        {
                            ObjObs.PHONE_OBS = dr.GetString(dr.GetOrdinal("PHONE_OBS"));
                        }
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
                        parametros.Add(ObjObs);

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
        public BETelefono ObtenerTelBPS(string owner, decimal idFono, decimal idBps,decimal idEntidad)
        {
            BETelefono Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TELEFONO_BPS"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@PHONE_ID", DbType.Decimal, idFono);
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idBps);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, idEntidad);

                

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BETelefono();

                        Obj.PHONE_ID = dr.GetDecimal(dr.GetOrdinal("PHONE_ID"));
                        Obj.PHONE_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("PHONE_TYPE")));
                        Obj.PHONE_NUMBER = dr.GetString(dr.GetOrdinal("PHONE_NUMBER"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("PHONE_OBS")))
                        {
                            Obj.PHONE_OBS = dr.GetString(dr.GetOrdinal("PHONE_OBS"));
                        }
                    }
                }
            }

            return Obj;
        }

        public BETelefono ObtenerTelEst(string owner, decimal idFono, decimal idEst, decimal idEntidad)
        {
            BETelefono Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TELEFONO_EST"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@PHONE_ID", DbType.Decimal, idFono);
                oDataBase.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEst);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, idEntidad);



                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BETelefono();

                        Obj.PHONE_ID = dr.GetDecimal(dr.GetOrdinal("PHONE_ID"));
                        Obj.PHONE_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("PHONE_TYPE")));
                        Obj.PHONE_NUMBER = dr.GetString(dr.GetOrdinal("PHONE_NUMBER"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("PHONE_OBS")))
                        {
                            Obj.PHONE_OBS = dr.GetString(dr.GetOrdinal("PHONE_OBS"));
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
        public int Eliminar(string owner, decimal telId, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_TELEFONO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@PHONE_ID", DbType.Decimal, telId); 
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
        public int Activar(string owner, decimal telId, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_TELEFONO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@PHONE_ID", DbType.Decimal, telId);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="obs"></param>
        /// <returns></returns>
        public int Update(BETelefono par)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_TELEFONO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, par.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@PHONE_ID", DbType.Int32, par.PHONE_ID);
            oDataBase.AddInParameter(oDbCommand, "@PHONE_TYPE", DbType.Int32, par.PHONE_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@PHONE_OBS", DbType.String, par.PHONE_OBS!= null ? par.PHONE_OBS.ToUpper():string.Empty);
            oDataBase.AddInParameter(oDbCommand, "@PHONE_NUMBER", DbType.String, par.PHONE_NUMBER.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, par.LOG_USER_UPDATE.ToUpper());

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        


         



    }
}

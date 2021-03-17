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
    public class DAObservationGral
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public int InsertarObsGrl(BEObservationGral obs)
        {
            try
            {
                Database oDataBase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_OBSERVACION_GRAL");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, obs.OWNER);
                oDataBase.AddInParameter(oDbComand, "@OBS_TYPE", DbType.Int32, obs.OBS_TYPE);
                oDataBase.AddInParameter(oDbComand, "@ENT_ID", DbType.Int32, obs.ENT_ID);
                oDataBase.AddInParameter(oDbComand, "@OBS_VALUE", DbType.String, obs.OBS_VALUE);
                oDataBase.AddInParameter(oDbComand, "@OBS_USER", DbType.String, obs.OBS_USER);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, obs.LOG_USER_CREAT);
                oDataBase.AddOutParameter(oDbComand, "@OBS_ID", DbType.Decimal, Convert.ToInt32(obs.OBS_ID));

                int n = oDataBase.ExecuteNonQuery(oDbComand);
                int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbComand, "@OBS_ID"));

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EliminarObsGral(BEObservationGral obs)
        {
            try
            {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASD_OBS_GRAL");
                oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, obs.OWNER);
                oDatabase.AddInParameter(oDbCommand, "@OBS_ID", DbType.Int32, obs.OBS_ID);
                oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, obs.LOG_DATE_UPDATE);
                int r = oDatabase.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {

                return 0;
            }
        }


        public List<BEObservationGral> ObservacionXSocio(decimal codigoBps, string owner, decimal tipoEntidad)
        {
            List<BEObservationGral> observaciones = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_OBSERVACION_BPS"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, codigoBps);
                    oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, tipoEntidad);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
               
                        BEObservationGral ObjObs = null;
                        observaciones = new List<BEObservationGral>();
                        while (dr.Read())
                        {
                            ObjObs = new BEObservationGral();

                            ObjObs.OBS_ID = dr.GetDecimal(dr.GetOrdinal("OBS_ID"));
                            ObjObs.OBS_TYPE =Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("OBS_TYPE")));
                            ObjObs.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                            ObjObs.OBS_VALUE = dr.GetString(dr.GetOrdinal("OBS_VALUE"));
                            ObjObs.OBS_USER = dr.GetString(dr.GetOrdinal("OBS_USER"));
                     

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
                            observaciones.Add(ObjObs);

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return observaciones;
        }

        public List<BEObservationGral> ObservacionXLicencia(decimal codigoLic, string owner, decimal tipoEntidad)
        {
            List<BEObservationGral> observaciones = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_OBSERVACION_LIC"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, codigoLic);
                    oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, tipoEntidad);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {

                        BEObservationGral ObjObs = null;
                        observaciones = new List<BEObservationGral>();
                        while (dr.Read())
                        {
                            ObjObs = new BEObservationGral();

                            ObjObs.OBS_ID = dr.GetDecimal(dr.GetOrdinal("OBS_ID"));
                            ObjObs.OBS_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("OBS_TYPE")));
                            ObjObs.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                            ObjObs.OBS_VALUE = dr.GetString(dr.GetOrdinal("OBS_VALUE"));
                            ObjObs.OBS_USER = dr.GetString(dr.GetOrdinal("OBS_USER"));


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
                            observaciones.Add(ObjObs);

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return observaciones;
        }
        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idDireccion"></param>
        /// <param name="idBps"></param>
        /// <returns></returns>
        public BEObservationGral ObtenerObsBPS(string owner, decimal idObs, decimal idBps,decimal idEntidad)
        {
            BEObservationGral Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_OBS_BPS"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@OBS_ID", DbType.Decimal, idObs);
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idBps);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, idEntidad);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEObservationGral();
                        Obj.OBS_ID = dr.GetDecimal(dr.GetOrdinal("OBS_ID"));
                        Obj.OBS_TYPE =Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("OBS_TYPE")));
                        Obj.ENT_ID =Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                        Obj.OBS_VALUE = dr.GetString(dr.GetOrdinal("OBS_VALUE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }

            return Obj;
        }

        public BEObservationGral ObtenerObsEst(string owner, decimal idObs, decimal idEstablecimiento)
        {
            BEObservationGral Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_OBS_EST"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@OBS_ID", DbType.Decimal, idObs);
                oDataBase.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEstablecimiento);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEObservationGral();
                        Obj.OBS_ID = dr.GetDecimal(dr.GetOrdinal("OBS_ID"));
                        Obj.OBS_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("OBS_TYPE")));
                        Obj.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                        Obj.OBS_VALUE = dr.GetString(dr.GetOrdinal("OBS_VALUE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
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
        /// <param name="dirId"></param>
        /// <param name="user"></param>
        /// <returns></returns> 
        public int Eliminar(string owner, decimal obsId, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_OBS_GRAL");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OBS_ID", DbType.Decimal, obsId);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        } 
        /// <summary> 
        /// addon dbs 20140727
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="dirId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Activar(string owner, decimal obsId, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_OBS_GRAL");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OBS_ID", DbType.Decimal, obsId);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="obs"></param>
        /// <returns></returns>
        public int Update(BEObservationGral obs)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_OBS_GRAL");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, obs.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@OBS_ID", DbType.Int32, obs.OBS_ID);
            oDataBase.AddInParameter(oDbCommand, "@OBS_TYPE", DbType.Int32, obs.OBS_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@OBS_VALUE", DbType.String, obs.OBS_VALUE);
            oDataBase.AddInParameter(oDbCommand, "@OBS_USER", DbType.String, obs.OBS_USER);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, obs.LOG_USER_UPDATE);

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEObservationGral> ObservacionXOficina(string owner, decimal offID)
        {
            List<BEObservationGral> observaciones = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_OBSERVACION_OFF"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@OFF_ID", DbType.Decimal, offID);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {

                        BEObservationGral ObjObs = null;
                        observaciones = new List<BEObservationGral>();
                        while (dr.Read())
                        {
                            ObjObs = new BEObservationGral();

                            ObjObs.OBS_ID = dr.GetDecimal(dr.GetOrdinal("OBS_ID"));
                            ObjObs.OBS_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("OBS_TYPE")));
                            ObjObs.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                            ObjObs.OBS_VALUE = dr.GetString(dr.GetOrdinal("OBS_VALUE"));
                            ObjObs.OBS_USER = dr.GetString(dr.GetOrdinal("OBS_USER"));


                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                ObjObs.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }

                            observaciones.Add(ObjObs);

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return observaciones;
        }





        public List<BEObservationGral> ObservacionXEstablecimiento(decimal idEstablecimiento, string owner, decimal tipoEntidad)
        {
            List<BEObservationGral> observaciones = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_OBSERVACION_EST"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@EST_ID", DbType.String, idEstablecimiento);
                    oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, tipoEntidad);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {

                        BEObservationGral ObjObs = null;
                        observaciones = new List<BEObservationGral>();
                        while (dr.Read())
                        {
                            ObjObs = new BEObservationGral();

                            ObjObs.OBS_ID = dr.GetDecimal(dr.GetOrdinal("OBS_ID"));
                            ObjObs.OBS_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("OBS_TYPE")));
                            ObjObs.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                            ObjObs.OBS_VALUE = dr.GetString(dr.GetOrdinal("OBS_VALUE"));
                            ObjObs.OBS_USER = dr.GetString(dr.GetOrdinal("OBS_USER"));


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
                            observaciones.Add(ObjObs);

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return observaciones;
        }


        public BEObservationGral ObtenerObsOficina(string owner, decimal idObs, decimal idOff)
        {
            BEObservationGral Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_OBS_OFF"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@OFF_ID", DbType.Decimal, idOff);
                oDataBase.AddInParameter(cm, "@OBS_ID", DbType.Decimal, idObs);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEObservationGral();
                        Obj.OBS_ID = dr.GetDecimal(dr.GetOrdinal("OBS_ID"));
                        Obj.OBS_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("OBS_TYPE")));
                        Obj.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                        Obj.OBS_VALUE = dr.GetString(dr.GetOrdinal("OBS_VALUE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }

            return Obj;
        }

        public BEObservationGral ObtenerObsLic(string owner, decimal idObs, decimal idLic, decimal idEntidad)
        {
            BEObservationGral Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_OBS_LIC"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@OBS_ID", DbType.Decimal, idObs);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, idEntidad);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEObservationGral();
                        Obj.OBS_ID = dr.GetDecimal(dr.GetOrdinal("OBS_ID"));
                        Obj.OBS_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("OBS_TYPE")));
                        Obj.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                        Obj.OBS_VALUE = dr.GetString(dr.GetOrdinal("OBS_VALUE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }

            return Obj;
        }

        public List<BEObservationGral> ObservacionXAgenteRecaudo(string owner, decimal idAgente)
        {
            List<BEObservationGral> observaciones = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBSERVACION_AGENTE_RECAUDO"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idAgente);
                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        BEObservationGral ObjObs = null;
                        observaciones = new List<BEObservationGral>();
                        while (dr.Read())
                        {
                            ObjObs = new BEObservationGral();
                            ObjObs.OBS_ID = dr.GetDecimal(dr.GetOrdinal("OBS_ID"));
                            ObjObs.OBS_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("OBS_TYPE")));
                            ObjObs.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                            ObjObs.OBS_VALUE = dr.GetString(dr.GetOrdinal("OBS_VALUE"));
                            ObjObs.OBS_USER = dr.GetString(dr.GetOrdinal("OBS_USER"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                ObjObs.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }
                            observaciones.Add(ObjObs);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return observaciones;
        }

        public BEObservationGral ObtenerObsAgenteRecaudo(string owner, decimal idObs, decimal idBps)
        {
            BEObservationGral Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_OBS_AGENTE_RECAUDO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idBps);
                oDataBase.AddInParameter(cm, "@OBS_ID", DbType.Decimal, idObs);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEObservationGral();
                        Obj.OBS_ID = dr.GetDecimal(dr.GetOrdinal("OBS_ID"));
                        Obj.OBS_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("OBS_TYPE")));
                        Obj.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                        Obj.OBS_VALUE = dr.GetString(dr.GetOrdinal("OBS_VALUE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }

            return Obj;
        }


    }
}

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
    public class DAParametroGral
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BEParametroGral doc)
        {
            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_PARAMETRO_GRAL");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, doc.OWNER);
                oDataBase.AddInParameter(oDbComand, "@PAR_TYPE", DbType.Decimal, doc.PAR_TYPE);
                oDataBase.AddInParameter(oDbComand, "@ENT_ID", DbType.Decimal, doc.ENT_ID);
                oDataBase.AddInParameter(oDbComand, "@PAR_VALUE", DbType.String, doc.PAR_VALUE);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, doc.LOG_USER_CREAT);
                oDataBase.AddInParameter(oDbComand, "@PAR_SUBTYPE", DbType.Decimal, doc.PAR_SUBTYPE);
                oDataBase.AddOutParameter(oDbComand, "@PAR_ID", DbType.Int32, Convert.ToInt32(doc.PAR_ID));

                int n = oDataBase.ExecuteNonQuery(oDbComand);
                int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbComand, "@PAR_ID"));

                return id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="codigoBps"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public List<BEParametroGral> ParametroXSocio(decimal codigoBps, string owner, decimal tipoEntidad)
        {
            List<BEParametroGral> parametros = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PARAMETRO_BPS"))
                {
                    oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, codigoBps);
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, tipoEntidad);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {

                        BEParametroGral ObjObs = null;
                        parametros = new List<BEParametroGral>();
                        while (dr.Read())
                        {
                            ObjObs = new BEParametroGral();

                            ObjObs.PAR_ID = dr.GetDecimal(dr.GetOrdinal("PAR_ID"));
                            ObjObs.PAR_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("PAR_TYPE")));
                            ObjObs.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                            ObjObs.PAR_VALUE = dr.GetString(dr.GetOrdinal("PAR_VALUE"));


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

                            if (!dr.IsDBNull(dr.GetOrdinal("PAR_SUBTYPE")))
                            {
                                ObjObs.PAR_SUBTYPE = dr.GetDecimal(dr.GetOrdinal("PAR_SUBTYPE"));
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("PAR_SUBTYPE_DESC")))
                            {
                                ObjObs.PAR_SUBTYPE_DESC = dr.GetString(dr.GetOrdinal("PAR_SUBTYPE_DESC"));
                            }

                            parametros.Add(ObjObs);

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return parametros;
        }

        public List<BEParametroGral> ParametroXLicencia(decimal codigoLic, string owner, decimal tipoEntidad)
        {
            List<BEParametroGral> parametros = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PARAMETRO_LIC"))
                {
                    oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, codigoLic);
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, tipoEntidad);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {

                        BEParametroGral ObjObs = null;
                        parametros = new List<BEParametroGral>();
                        while (dr.Read())
                        {
                            ObjObs = new BEParametroGral();

                            ObjObs.PAR_ID = dr.GetDecimal(dr.GetOrdinal("PAR_ID"));
                            ObjObs.PAR_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("PAR_TYPE")));
                            ObjObs.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                            ObjObs.PAR_VALUE = dr.GetString(dr.GetOrdinal("PAR_VALUE"));


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
                            parametros.Add(ObjObs);

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return parametros;
        }

        public List<BEParametroGral> ParametroXEstablecimiento(decimal idEstablecimiento, string owner, decimal tipoEntidad)
        {
            List<BEParametroGral> parametros = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PARAMETRO_EST"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@EST_ID", DbType.String, idEstablecimiento);
                    oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, tipoEntidad);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {

                        BEParametroGral ObjObs = null;
                        parametros = new List<BEParametroGral>();
                        while (dr.Read())
                        {
                            ObjObs = new BEParametroGral();

                            ObjObs.PAR_ID = dr.GetDecimal(dr.GetOrdinal("PAR_ID"));
                            ObjObs.PAR_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("PAR_TYPE")));
                            ObjObs.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                            ObjObs.PAR_VALUE = dr.GetString(dr.GetOrdinal("PAR_VALUE"));


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
                            parametros.Add(ObjObs);

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return parametros;
        }


        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idDireccion"></param>
        /// <param name="idBps"></param>
        /// <returns></returns>
        public BEParametroGral ObtenerParBPS(string owner, decimal idPar, decimal idBps, decimal idEntidad)
        {
            BEParametroGral Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_PAR_BPS"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@PAR_ID", DbType.Decimal, idPar);
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idBps);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, idEntidad);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEParametroGral();
                        Obj.PAR_ID = dr.GetDecimal(dr.GetOrdinal("PAR_ID"));
                        Obj.PAR_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("PAR_TYPE")));
                        Obj.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                        Obj.PAR_VALUE = dr.GetString(dr.GetOrdinal("PAR_VALUE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }

            return Obj;
        }
        public BEParametroGral ObtenerParLic(string owner, decimal idPar, decimal idLic, decimal idEntidad)
        {
            BEParametroGral Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_PAR_LIC"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@PAR_ID", DbType.Decimal, idPar);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, idEntidad);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEParametroGral();
                        Obj.PAR_ID = dr.GetDecimal(dr.GetOrdinal("PAR_ID"));
                        Obj.PAR_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("PAR_TYPE")));
                        Obj.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                        Obj.PAR_VALUE = dr.GetString(dr.GetOrdinal("PAR_VALUE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }

            return Obj;
        }
        public BEParametroGral ObtenerParEst(string owner, decimal idPar, decimal idEstablecimiento)
        {
            BEParametroGral Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_PAR_EST"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@PAR_ID", DbType.Decimal, idPar);
                oDataBase.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEstablecimiento);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEParametroGral();
                        Obj.PAR_ID = dr.GetDecimal(dr.GetOrdinal("PAR_ID"));
                        Obj.PAR_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("PAR_TYPE")));
                        Obj.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                        Obj.PAR_VALUE = dr.GetString(dr.GetOrdinal("PAR_VALUE"));
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
        /// <param name="parId"></param>
        /// <param name="user"></param>
        /// <returns></returns> 
        public int Eliminar(string owner, decimal parId, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_PAR_GRAL");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@PAR_ID", DbType.Decimal, parId); 
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
        public int Activar(string owner, decimal parId, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_PAR_GRAL");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@PAR_ID", DbType.Decimal, parId);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="obs"></param>
        /// <returns></returns>
        public int Update(BEParametroGral par)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_PAR_GRAL");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, par.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@PAR_ID", DbType.Int32, par.PAR_ID);
            oDataBase.AddInParameter(oDbCommand, "@PAR_TYPE", DbType.Int32, par.PAR_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@PAR_VALUE", DbType.String, par.PAR_VALUE);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, par.LOG_USER_UPDATE);
            oDataBase.AddInParameter(oDbCommand, "@PAR_SUBTYPE", DbType.Int32, par.PAR_SUBTYPE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEParametroGral> ParametroXOficina(decimal codigoOfi, string owner)
        {
            List<BEParametroGral> parametros = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_PARAMETROS_OFF"))
                {
                    oDataBase.AddInParameter(cm, "@OFF_ID", DbType.Decimal, codigoOfi);
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);


                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {

                        BEParametroGral ObjObs = null;
                        parametros = new List<BEParametroGral>();
                        while (dr.Read())
                        {
                            ObjObs = new BEParametroGral();

                            ObjObs.PAR_ID = dr.GetDecimal(dr.GetOrdinal("PAR_ID"));
                            ObjObs.PAR_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("PAR_TYPE")));
                            ObjObs.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                            ObjObs.PAR_VALUE = dr.GetString(dr.GetOrdinal("PAR_VALUE"));


                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                ObjObs.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }

                            parametros.Add(ObjObs);

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return parametros;
        }


        public BEParametroGral ObtenerParOFF(string owner, decimal idPar, decimal idOff)
        {
            BEParametroGral Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_PAR_OFF"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@PAR_ID", DbType.Decimal, idPar);
                oDataBase.AddInParameter(cm, "@OFF_ID", DbType.Decimal, idOff);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEParametroGral();
                        Obj.PAR_ID = dr.GetDecimal(dr.GetOrdinal("PAR_ID"));
                        Obj.PAR_TYPE = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("PAR_TYPE")));
                        Obj.ENT_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("ENT_ID")));
                        Obj.PAR_VALUE = dr.GetString(dr.GetOrdinal("PAR_VALUE"));
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

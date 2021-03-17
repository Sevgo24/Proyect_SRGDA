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
    public class DACaracteristicaEst
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BECaracteristicaEst en)
        {
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_CARACTERISTICA_EST");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@CHAR_ID", DbType.String, en.CHAR_ID);
                db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, en.EST_ID);
                db.AddInParameter(oDbCommand, "@ESTT_ID", DbType.Decimal, en.ESTT_ID);
                db.AddInParameter(oDbCommand, "@SUBE_ID", DbType.Decimal, en.SUBE_ID);
                db.AddInParameter(oDbCommand, "@VALUE", DbType.Decimal, en.VALUE);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {
                return 0; 
            }
        }

        public int InsertarInspeccionCaracteristica(BECaracteristicaEst en)
        {
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_INSPECCION_CARACTERISTICA_EST");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@CHAR_ID", DbType.String, en.CHAR_ID);
                db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, en.EST_ID);
                db.AddInParameter(oDbCommand, "@ESTT_ID", DbType.Decimal, en.ESTT_ID);
                db.AddInParameter(oDbCommand, "@SUBE_ID", DbType.Decimal, en.SUBE_ID);
                db.AddInParameter(oDbCommand, "@INSP_ID", DbType.Decimal, en.INSP_ID);
                db.AddInParameter(oDbCommand, "@VALUE", DbType.Decimal, en.VALUE);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Actualizar(BECaracteristicaEst en)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_CARACTERISTICA_EST");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@CHAR_ID", DbType.Decimal, en.CHAR_ID);
                db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, en.EST_ID);
                //db.AddInParameter(oDbCommand, "@ESTT_ID", DbType.Decimal, en.ESTT_ID);
                db.AddInParameter(oDbCommand, "@VALUE", DbType.Decimal, en.VALUE);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_CREAT);
                retorno = db.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public int Activar(string owner, decimal carId, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTIVAR_CAR_EST");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@CHAR_ID", DbType.Decimal, carId);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, user);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(string owner, decimal carId, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_CARACTERISTICA_EST");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@CHAR_ID", DbType.Decimal, carId);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, user);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BECaracteristicaEst> CaracteristicaXSubtipoEstablecimiento(decimal idTipoEstablecimiento, decimal idSubtipoEstablecimiento)
        {
            List<BECaracteristicaEst> caracteristicastipo = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_CARACTERISTICA_SUBTIPO_EST"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                    db.AddInParameter(cm, "@ESTT_ID", DbType.Decimal, idTipoEstablecimiento);
                    db.AddInParameter(cm, "@SUBE_ID", DbType.Decimal, idSubtipoEstablecimiento);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        BECaracteristicaEst objCar = null;
                        caracteristicastipo = new List<BECaracteristicaEst>();
                        while (dr.Read())
                        {
                            objCar = new BECaracteristicaEst();
                            objCar.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));
                            objCar.CHAR_LONG = dr.GetString(dr.GetOrdinal("CHAR_LONG"));
                            objCar.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                            //objCar.ESTT_ID = dr.GetDecimal(dr.GetOrdinal("ESTT_ID"));
                            //objCar.VALUE = dr.GetDecimal(dr.GetOrdinal("VALUE"));

                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                objCar.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }

                            objCar.SUBE_ID = dr.GetDecimal(dr.GetOrdinal("SUBE_ID"));

                            caracteristicastipo.Add(objCar);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return caracteristicastipo;
        }

        public List<BECaracteristicaEst> CaracteristicaXEstablecimiento(decimal idEstablecimiento, string owner)
        {
            List<BECaracteristicaEst> caracteristicastipo = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_CARACTERISTICA_EST"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    db.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEstablecimiento);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        BECaracteristicaEst objCar = null;
                        caracteristicastipo = new List<BECaracteristicaEst>();
                        while (dr.Read())
                        {
                            objCar = new BECaracteristicaEst();
                            objCar.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));
                            objCar.CHAR_LONG = dr.GetString(dr.GetOrdinal("CHAR_LONG"));
                            objCar.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                            objCar.ESTT_ID = dr.GetDecimal(dr.GetOrdinal("ESTT_ID"));

                            if (!dr.IsDBNull(dr.GetOrdinal("SUBE_ID")))
                            {
                                objCar.SUBE_ID = dr.GetDecimal(dr.GetOrdinal("SUBE_ID"));
                            }

                            objCar.VALUE = dr.GetDecimal(dr.GetOrdinal("VALUE"));

                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                objCar.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            {
                                objCar.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            {
                                objCar.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDAT")))
                            {
                                objCar.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            {
                                objCar.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                            }
                            caracteristicastipo.Add(objCar);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return caracteristicastipo;
        }

        public BECaracteristicaEst ObtenerCarEst(string owner, decimal idCar, decimal idEstablecimiento)
        {
            BECaracteristicaEst Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_CARACTERISTICA_EST"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@CHAR_ID", DbType.Decimal, idCar);
                oDataBase.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEstablecimiento);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BECaracteristicaEst();
                        Obj.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));
                        Obj.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                        Obj.ESTT_ID = dr.GetDecimal(dr.GetOrdinal("ESTT_ID"));
                        
                        if (!dr.IsDBNull(dr.GetOrdinal("SUBE_ID")))
                        {
                            Obj.SUBE_ID = dr.GetDecimal(dr.GetOrdinal("SUBE_ID"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("VALUE")))
                        {
                            Obj.VALUE = dr.GetDecimal(dr.GetOrdinal("VALUE"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }
            return Obj;
        }


        //Insertando Caracteristicas despues de haber actualizado las caracteristicas de la licencia
        public int ActualizarCaractersiticasEst(BECaracteristicaEst en)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_CARACTERISTICAS_ESTABLECIMIENTO");
                db.AddInParameter(oDbCommand, "@CHARID", DbType.Decimal, en.CHAR_ID);
                db.AddInParameter(oDbCommand, "@LICID", DbType.Decimal, en.LIC_ID);
                //db.AddInParameter(oDbCommand, "@ESTT_ID", DbType.Decimal, en.ESTT_ID);
                db.AddInParameter(oDbCommand, "@VALUE", DbType.Decimal, en.VALUE);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_CREAT);
                retorno = db.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

    }
}

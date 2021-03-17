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
    public class DAPeriodoFrecuencia
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public List<BEPeriodoFrecuencia> Listar(decimal periodoId, string owner)
        {
            List<BEPeriodoFrecuencia> periodo = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_PERIODOFRECUECIA"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@RAT_FID", DbType.Decimal, periodoId);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    BEPeriodoFrecuencia item = null;
                    periodo = new List<BEPeriodoFrecuencia>();
                    while (dr.Read())
                    {
                        item = new BEPeriodoFrecuencia();
                        item.RAT_FID = dr.GetDecimal(dr.GetOrdinal("RAT_FID"));
                        item.FRQ_NPER = dr.GetDecimal(dr.GetOrdinal("FRQ_NPER"));
                        item.FRQ_NPER_ANT = dr.GetDecimal(dr.GetOrdinal("FRQ_NPER"));
                        item.FRQ_DESC = dr.GetString(dr.GetOrdinal("FRQ_DESC"));
                        item.FRQ_DAYS = dr.GetDecimal(dr.GetOrdinal("FRQ_DAYS"));
                        item.FRQ_DATE = dr.GetDateTime(dr.GetOrdinal("FRQ_DATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        {
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        {
                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        {
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        periodo.Add(item);
                    }
                }
                return periodo;
            }
        }

        public BEPeriodoFrecuencia ObtenerPeriodicidadTarifa(string owner, decimal periodoId, decimal orden)
        {
            BEPeriodoFrecuencia periodo = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_PERIODOFRECUECIA_Id_NroOrden"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@RAT_FID", DbType.Decimal, periodoId);
                oDataBase.AddInParameter(cm, "@FRQ_NPER", DbType.Decimal, orden);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {

                        periodo = new BEPeriodoFrecuencia();
                        periodo.RAT_FID = dr.GetDecimal(dr.GetOrdinal("RAT_FID"));
                        periodo.FRQ_NPER = dr.GetDecimal(dr.GetOrdinal("FRQ_NPER"));
                        periodo.FRQ_DESC = dr.GetString(dr.GetOrdinal("FRQ_DESC"));
                        periodo.FRQ_DAYS = dr.GetDecimal(dr.GetOrdinal("FRQ_DAYS"));
                        periodo.FRQ_DATE = dr.GetDateTime(dr.GetOrdinal("FRQ_DATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            periodo.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        {
                            periodo.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        {
                            periodo.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        {
                            periodo.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            periodo.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }

                }
                return periodo;
            }
        }

        public int Insertar(BEPeriodoFrecuencia en)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_PERIODOFRECUECIA");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(oDbComand, "@RAT_FID", DbType.Decimal, en.RAT_FID);
                oDataBase.AddInParameter(oDbComand, "@FRQ_NPER", DbType.Decimal, en.FRQ_NPER);
                oDataBase.AddInParameter(oDbComand, "@FRQ_DESC", DbType.String, en.FRQ_DESC.ToUpper());
                oDataBase.AddInParameter(oDbComand, "@FRQ_DAYS", DbType.Decimal, en.FRQ_DAYS);
                oDataBase.AddInParameter(oDbComand, "@FRQ_DATE", DbType.DateTime, en.FRQ_DATE);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                retorno = oDataBase.ExecuteNonQuery(oDbComand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public int Actualizar(BEPeriodoFrecuencia en)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_PERIODOFRECUECIA");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(oDbComand, "@RAT_FID", DbType.Decimal, en.RAT_FID);
                oDataBase.AddInParameter(oDbComand, "@FRQ_NPER", DbType.Decimal, en.FRQ_NPER);
                oDataBase.AddInParameter(oDbComand, "@FRQ_NPER_ANT", DbType.Decimal, en.FRQ_NPER_ANT);
                oDataBase.AddInParameter(oDbComand, "@FRQ_DESC", DbType.String, en.FRQ_DESC.ToUpper());
                oDataBase.AddInParameter(oDbComand, "@FRQ_DAYS", DbType.Decimal, en.FRQ_DAYS);
                oDataBase.AddInParameter(oDbComand, "@FRQ_DATE", DbType.DateTime, en.FRQ_DATE);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
                retorno = oDataBase.ExecuteNonQuery(oDbComand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public int Activar(string owner, decimal periodoId, string user, decimal nroorden)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_PERIODOFRECUECIA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@RAT_FID", DbType.Decimal, periodoId);
            oDataBase.AddInParameter(oDbCommand, "@FRQ_NPER", DbType.Decimal, nroorden);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(string owner, decimal periodoId, string user, decimal nroorden)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_INACTIVAR_PERIODOFRECUECIA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@RAT_FID", DbType.Decimal, periodoId);
            oDataBase.AddInParameter(oDbCommand, "@FRQ_NPER", DbType.Decimal, nroorden);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}

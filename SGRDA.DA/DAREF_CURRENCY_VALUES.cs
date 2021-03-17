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
    public class DAREF_CURRENCY_VALUES
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<REF_CURRENCY_VALUES> usp_Get_REF_CURRENCY_VALUES(string CUR_ALPHA, int YEAR, int MONTH)
        {
            List<REF_CURRENCY_VALUES> lst = new List<REF_CURRENCY_VALUES>();
            REF_CURRENCY_VALUES item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REF_CURRENCY_VALUES_BY_YEAR_MONTH_CUR_ALPHA"))
                {
                    db.AddInParameter(cm, "CUR_ALPHA", DbType.String, CUR_ALPHA);
                    db.AddInParameter(cm, "YEAR", DbType.Int64, YEAR);
                    db.AddInParameter(cm, "MONTH", DbType.Int64, MONTH);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new REF_CURRENCY_VALUES();
                            item.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                            item.CUR_DATE = dr.GetDateTime(dr.GetOrdinal("CUR_DATE"));
                            item.CUR_VALUE = dr.GetDecimal(dr.GetOrdinal("CUR_VALUE"));
                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }

        public bool usp_Upd_REF_CURRENCY_VALUES_XML(string xml)
        {
            bool exito = false;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Upd_REF_CURRENCY_VALUES_XML"))
                {
                    db.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    exito = db.ExecuteNonQuery(cm) > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return exito;
        }

        public bool usp_Ins_REF_CURRENCY_VALUES_XML(string xml)
        {
            bool exito = false;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Ins_REF_CURRENCY_VALUES_XML"))
                {
                    db.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    exito = db.ExecuteNonQuery(cm) > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return exito;
        }

        public bool usp_Upd_REF_CURRENCY_VALUES(REF_CURRENCY_VALUES en)
        {
            bool exito = false;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_CURRENCY_VALUES_Upd_by_CUR_ALPHA_CUR_DATE"))
                {
                    db.AddInParameter(cm, "@CUR_ALPHA", DbType.String, en.CUR_ALPHA);
                    db.AddInParameter(cm, "@CUR_DATE", DbType.Date, en.CUR_DATE);
                    db.AddInParameter(cm, "@CUR_VALUE", DbType.Decimal, en.CUR_VALUE);
                    db.AddInParameter(cm, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
                    exito = db.ExecuteNonQuery(cm) > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return exito;
        }

        public bool insertar(REF_CURRENCY_VALUES en)
        {
            bool exito = false;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASI_REF_CURRENCY_VALUES"))
                {
                    db.AddInParameter(cm, "@CUR_ALPHA", DbType.String, en.CUR_ALPHA);
                    db.AddInParameter(cm, "@CUR_DATE", DbType.Date, en.CUR_DATE);
                    db.AddInParameter(cm, "@CUR_VALUE", DbType.Decimal, en.CUR_VALUE);
                    db.AddInParameter(cm, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                    exito = db.ExecuteNonQuery(cm) > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return exito;
        }

        public List<REF_CURRENCY_VALUES> usp_REF_CURRENCY_VALUES_GET(DateTime CUR_DATE)
        {
            List<REF_CURRENCY_VALUES> lst = new List<REF_CURRENCY_VALUES>();
            REF_CURRENCY_VALUES item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_CURRENCY_VALUES_GET"))
                {
                    db.AddInParameter(cm, "@CUR_DATE", DbType.DateTime, CUR_DATE);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new REF_CURRENCY_VALUES();
                            item.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                            item.CUR_DATE = dr.GetDateTime(dr.GetOrdinal("CUR_DATE"));
                            item.CUR_VALUE = dr.GetDecimal(dr.GetOrdinal("CUR_VALUE"));
                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }

        public REF_CURRENCY_VALUES ObtenerTipoCambioActual()
        {
            REF_CURRENCY_VALUES tipoCambio = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_TIPO_CAMBIO"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            tipoCambio = new REF_CURRENCY_VALUES();
                            tipoCambio.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                            tipoCambio.CUR_DATE = dr.GetDateTime(dr.GetOrdinal("CUR_DATE"));
                            tipoCambio.CUR_VALUE = dr.GetDecimal(dr.GetOrdinal("CUR_VALUE"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tipoCambio;
        }


        #region MODULO DE ADMINISTRACION -TASA DE CAMBIO

        public List<BEREF_CURRENCY_VALUES> ListaTasaDeCambio(string fecha_ini,string fecha_fin)
        {
            List<BEREF_CURRENCY_VALUES> lst = new List<BEREF_CURRENCY_VALUES>();
            BEREF_CURRENCY_VALUES item = null;
            try
            {
                using (DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_TASA_CAMBIO"))
                {
                    db.AddInParameter(oDbCommand, "@fecha_ini", DbType.String, fecha_ini);
                    db.AddInParameter(oDbCommand, "@fecha_fin", DbType.String, fecha_fin);

                    using (IDataReader dr = db.ExecuteReader(oDbCommand))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_CURRENCY_VALUES();
                            if(!dr.IsDBNull(dr.GetOrdinal("CUR_ALPHA")))
                                item.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));

                            if(!dr.IsDBNull(dr.GetOrdinal("CUR_DATE")))
                                item.CUR_DATE = dr.GetString(dr.GetOrdinal("CUR_DATE"));

                            if(!dr.IsDBNull(dr.GetOrdinal("CUR_VALUE")))
                                item.CUR_VALUE = dr.GetDecimal(dr.GetOrdinal("CUR_VALUE"));

                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                                item.LOG_DATE_CREAT = dr.GetString(dr.GetOrdinal("LOG_DATE_CREAT"));

                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                                item.LOG_DATE_UPDATE = dr.GetString(dr.GetOrdinal("LOG_DATE_UPDATE"));

                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                                item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                                item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                                item.ENDS = dr.GetString(dr.GetOrdinal("ENDS"));

                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }



        public bool GrabarTasaCambio(BEREF_CURRENCY_VALUES en)
        {
            bool exito = false;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASI_TASA_CAMBIO"))
                {
                    //db.AddInParameter(cm, "@CUR_ALPHA", DbType.String, en.CUR_ALPHA);
                    //db.AddInParameter(cm, "@CUR_DATE", DbType.Date, en.CUR_DATE);
                    db.AddInParameter(cm, "@CUR_VALUE", DbType.Decimal, en.CUR_VALUE);
                    db.AddInParameter(cm, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                    exito = db.ExecuteNonQuery(cm) > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return exito;
        }

        public int ConsultaTasaCambio()
        {
            int r = 0;
            try
            {
                using (DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_CONSULTA_TASA_DE_CAMBIO_ACTUAL"))
                {

                     r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));

                }
            }
            catch (Exception ex)
            {
                throw;

            }


           return r;
        }
        #endregion

    }
}


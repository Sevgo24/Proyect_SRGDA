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
    public class DATarifaTest
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public List<BETarifaTest> Listar(string owner, decimal idRegla)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_MATRIZ_TARIFA_TEST");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, idRegla);
            oDataBase.ExecuteNonQuery(oDbCommand);

            List<BETarifaTest> lista = new List<BETarifaTest>();
            BETarifaTest test = null;

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    test = new BETarifaTest();
                    if (!dr.IsDBNull(dr.GetOrdinal("CHAR1_ID")))
                        test.CHAR1_ID = dr.GetDecimal(dr.GetOrdinal("CHAR1_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SECC1_DESC")))
                        test.SECC1_DESC = dr.GetString(dr.GetOrdinal("SECC1_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IND1_TR")))
                        test.IND1_TR = dr.GetString(dr.GetOrdinal("IND1_TR"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CRI1_FROM")))
                        test.CRI1_FROM = dr.GetDecimal(dr.GetOrdinal("CRI1_FROM"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CRI1_TO")))
                        test.CRI1_TO = dr.GetDecimal(dr.GetOrdinal("CRI1_TO"));


                    if (!dr.IsDBNull(dr.GetOrdinal("CHAR2_ID")))
                        test.CHAR2_ID = dr.GetDecimal(dr.GetOrdinal("CHAR2_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SECC2_DESC")))
                        test.SECC2_DESC = dr.GetString(dr.GetOrdinal("SECC2_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IND2_TR")))
                        test.IND2_TR = dr.GetString(dr.GetOrdinal("IND2_TR"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CRI2_FROM")))
                        test.CRI2_FROM = dr.GetDecimal(dr.GetOrdinal("CRI2_FROM"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CRI2_TO")))
                        test.CRI2_TO = dr.GetDecimal(dr.GetOrdinal("CRI2_TO"));


                    if (!dr.IsDBNull(dr.GetOrdinal("CHAR3_ID")))
                        test.CHAR3_ID = dr.GetDecimal(dr.GetOrdinal("CHAR3_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SECC3_DESC")))
                        test.SECC3_DESC = dr.GetString(dr.GetOrdinal("SECC3_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IND3_TR")))
                        test.IND3_TR = dr.GetString(dr.GetOrdinal("IND3_TR"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CRI3_FROM")))
                        test.CRI3_FROM = dr.GetDecimal(dr.GetOrdinal("CRI3_FROM"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CRI3_TO")))
                        test.CRI3_TO = dr.GetDecimal(dr.GetOrdinal("CRI3_TO"));

                    if (!dr.IsDBNull(dr.GetOrdinal("CHAR4_ID")))
                        test.CHAR4_ID = dr.GetDecimal(dr.GetOrdinal("CHAR4_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SECC4_DESC")))
                        test.SECC4_DESC = dr.GetString(dr.GetOrdinal("SECC4_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IND4_TR")))
                        test.IND4_TR = dr.GetString(dr.GetOrdinal("IND4_TR"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CRI4_FROM")))
                        test.CRI4_FROM = dr.GetDecimal(dr.GetOrdinal("CRI4_FROM"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CRI4_TO")))
                        test.CRI4_TO = dr.GetDecimal(dr.GetOrdinal("CRI4_TO"));

                    if (!dr.IsDBNull(dr.GetOrdinal("CHAR5_ID")))
                        test.CHAR5_ID = dr.GetDecimal(dr.GetOrdinal("CHAR5_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SECC5_DESC")))
                        test.SECC5_DESC = dr.GetString(dr.GetOrdinal("SECC5_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IND5_TR")))
                        test.IND5_TR = dr.GetString(dr.GetOrdinal("IND5_TR"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CRI5_FROM")))
                        test.CRI5_FROM = dr.GetDecimal(dr.GetOrdinal("CRI5_FROM"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CRI5_TO")))
                        test.CRI5_TO = dr.GetDecimal(dr.GetOrdinal("CRI5_TO"));


                    if (!dr.IsDBNull(dr.GetOrdinal("VAL_FORMULA")))
                        test.VAL_FORMULA = dr.GetDecimal(dr.GetOrdinal("VAL_FORMULA"));
                    if (!dr.IsDBNull(dr.GetOrdinal("VAL_MINIMUM")))
                        test.VAL_MINIMUM = dr.GetDecimal(dr.GetOrdinal("VAL_MINIMUM"));

                    test.CALRV_ID = dr.GetDecimal(dr.GetOrdinal("CALRV_ID"));
                    test.CALR_ID = dr.GetDecimal(dr.GetOrdinal("CALR_ID"));
                    test.TEMP_ID = dr.GetDecimal(dr.GetOrdinal("TEMP_ID"));
                    lista.Add(test);
                }
            }
            return lista;
        }

        public BEREC_RATES_GRAL Obtener(string owner, decimal idRate)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TARIFA_TEST");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, idRate);
            BEREC_RATES_GRAL ent = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BEREC_RATES_GRAL();
                    ent.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                    ent.RATE_DESC = dr.GetString(dr.GetOrdinal("RATE_DESC"));
                    ent.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));
                    ent.MOD_DEC = dr.GetString(dr.GetOrdinal("MOD_DEC"));
                    ent.RAT_FID = dr.GetDecimal(dr.GetOrdinal("RAT_FID"));
                    ent.RAT_FDESC = dr.GetString(dr.GetOrdinal("RAT_FDESC"));
                    ent.RATE_NVAR = dr.GetDecimal(dr.GetOrdinal("RATE_NVAR"));
                    ent.RATE_NCAL = dr.GetDecimal(dr.GetOrdinal("RATE_NCAL"));
                    ent.RATE_FORMULA = dr.GetString(dr.GetOrdinal("RATE_FORMULA"));
                    ent.RATE_MINIMUM = dr.GetString(dr.GetOrdinal("RATE_MINIMUM"));
                    ent.RATE_FTIPO = dr.GetString(dr.GetOrdinal("RATE_FTIPO"));
                    ent.RATE_MTIPO = dr.GetString(dr.GetOrdinal("RATE_MTIPO"));
                    ent.RATE_FDECI = dr.GetDecimal(dr.GetOrdinal("RATE_FDECI"));
                    ent.RATE_MDECI = dr.GetDecimal(dr.GetOrdinal("RATE_MDECI"));
                    ent.RATE_REDONDEO = dr.GetInt32(dr.GetOrdinal("RATE_REDONDEO"));
                    ent.RATE_START = dr.GetDateTime(dr.GetOrdinal("RATE_START"));
                    ent.RATE_END = dr.GetDateTime(dr.GetOrdinal("RATE_END"));       
                }
            }
            return ent;
        }

        public List<BETarifaCaracteristica> ListarCaracteristica(string owner, decimal idRate)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_MANT_TARIFA_TEST_CARACTERISTICA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, idRate);
            oDataBase.ExecuteNonQuery(oDbCommand);

            List<BETarifaCaracteristica> lista = new List<BETarifaCaracteristica>();
            BETarifaCaracteristica caracteristica = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    caracteristica = new BETarifaCaracteristica();
                    caracteristica.RATE_CHAR_ID = dr.GetDecimal(dr.GetOrdinal("RATE_CHAR_ID"));
                    caracteristica.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                    caracteristica.RATE_CHAR_TVAR = dr.GetString(dr.GetOrdinal("RATE_CHAR_TVAR"));
                    caracteristica.RATE_CHAR_DESCVAR = dr.GetString(dr.GetOrdinal("RATE_CHAR_DESCVAR"));
                    caracteristica.RATE_CHAR_VARUNID = dr.GetString(dr.GetOrdinal("RATE_CHAR_VARUNID"));
                    caracteristica.RATE_CHAR_CARIDSW = dr.GetString(dr.GetOrdinal("RATE_CHAR_CARIDSW"));
                    caracteristica.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    caracteristica.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        caracteristica.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    caracteristica.RATE_CALC_ID = dr.GetDecimal(dr.GetOrdinal("RATE_CALC_ID"));
                    caracteristica.RATE_CALCT = dr.GetString(dr.GetOrdinal("RATE_CALCT"));
                    caracteristica.RATE_CALC = dr.GetDecimal(dr.GetOrdinal("RATE_CALC"));
                    caracteristica.RATE_CALC_AR = dr.GetDecimal(dr.GetOrdinal("RATE_CALC_AR"));
                    caracteristica.CHAR_ORI_REG = dr.GetString(dr.GetOrdinal("CHAR_ORI_REG"));
                    caracteristica.IND_TR = dr.GetString(dr.GetOrdinal("IND_TR"));
                    lista.Add(caracteristica);
                }
            }
            return lista;
        }

        //Facturación Masiva

        public List<BEREC_RATES_GRAL> ObtenerTarifasXML(string xml)
        {
            List<BEREC_RATES_GRAL> Lista = new List<BEREC_RATES_GRAL>();
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TARIFAS_LICENCIA"))
                {
                    oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    BEREC_RATES_GRAL ent = null;

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            ent = new BEREC_RATES_GRAL();
                            ent.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                            ent.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                            ent.RAT_FID = dr.GetDecimal(dr.GetOrdinal("RAT_FID"));
                            ent.RATE_ACCOUNT = dr.GetString(dr.GetOrdinal("RATE_ACCOUNT"));
                            ent.RATE_DREPERT = dr.GetString(dr.GetOrdinal("RATE_DREPERT"));
                            ent.RATE_NVAR = dr.GetDecimal(dr.GetOrdinal("RATE_NVAR"));
                            ent.RATE_NCAL = dr.GetDecimal(dr.GetOrdinal("RATE_NCAL"));
                            ent.RATE_FORMULA = dr.GetString(dr.GetOrdinal("RATE_FORMULA"));
                            ent.RATE_MINIMUM = dr.GetString(dr.GetOrdinal("RATE_MINIMUM"));
                            ent.RATE_FTIPO = dr.GetString(dr.GetOrdinal("RATE_FTIPO"));
                            ent.RATE_MTIPO = dr.GetString(dr.GetOrdinal("RATE_MTIPO"));
                            ent.RATE_FDECI = dr.GetDecimal(dr.GetOrdinal("RATE_FDECI"));
                            ent.RATE_MDECI = dr.GetDecimal(dr.GetOrdinal("RATE_MDECI"));
                            ent.RATE_REDONDEO = dr.GetInt32(dr.GetOrdinal("RATE_REDONDEO"));
                            ent.RATE_REDONDEO_ESP = dr.GetInt32(dr.GetOrdinal("RATE_REDONDEO_ESP"));
                            Lista.Add(ent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Lista;
        }

        public List<BETarifaCaracteristica> ListarCaracteristicaXML(string owner, string xml)
        {
            List<BETarifaCaracteristica> Lista = new List<BETarifaCaracteristica>();
            try
            {
                int carOtro=  new DATarifaTest().IdCaracteristicaOtro(owner);
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_MANT_TARIFA_TEST_CARACTERISTICA_LICENCIA");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "xmlLst", DbType.Xml, xml);
                oDbCommand.CommandTimeout = 1800;
                BETarifaCaracteristica caracteristica = null;

                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_ID")))
                        {
                            caracteristica = new BETarifaCaracteristica();
                            caracteristica.RATE_CHAR_ID = dr.GetDecimal(dr.GetOrdinal("RATE_CHAR_ID"));
                            caracteristica.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                            caracteristica.RATE_CHAR_TVAR = dr.GetString(dr.GetOrdinal("RATE_CHAR_TVAR"));
                            caracteristica.RATE_CHAR_DESCVAR = dr.GetString(dr.GetOrdinal("RATE_CHAR_DESCVAR"));
                            caracteristica.RATE_CHAR_VARUNID = dr.GetString(dr.GetOrdinal("RATE_CHAR_VARUNID"));
                            caracteristica.RATE_CHAR_CARIDSW = dr.GetString(dr.GetOrdinal("RATE_CHAR_CARIDSW"));
                            caracteristica.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                            caracteristica.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                                caracteristica.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            caracteristica.RATE_CALC_ID = dr.GetDecimal(dr.GetOrdinal("RATE_CALC_ID"));
                            caracteristica.RATE_CALCT = dr.GetString(dr.GetOrdinal("RATE_CALCT"));
                            caracteristica.RATE_CALC = dr.GetDecimal(dr.GetOrdinal("RATE_CALC"));
                            caracteristica.RATE_CALC_AR = dr.GetDecimal(dr.GetOrdinal("RATE_CALC_AR"));
                            caracteristica.CHAR_ORI_REG = dr.GetString(dr.GetOrdinal("CHAR_ORI_REG"));
                            caracteristica.IND_TR = dr.GetString(dr.GetOrdinal("IND_TR"));

                            caracteristica.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                            if (caracteristica.RATE_CALC_AR != carOtro)
                                caracteristica.LIC_CHAR_VAL = dr.GetDecimal(dr.GetOrdinal("LIC_CHAR_VAL"));
                            else
                                caracteristica.LIC_CHAR_VAL = 1;
                            caracteristica.VALIDACION_FECHA = dr.GetInt32(dr.GetOrdinal("VALIDACION_FECHA"));
                            caracteristica.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));
                            Lista.Add(caracteristica);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Lista;
        }

        public List<BETarifaTest> ListarXML(string owner, string xml)
        {
            List<BETarifaTest> lista = new List<BETarifaTest>();
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_MATRIZ_TARIFA_TEST_LICENCIA");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "xmlLst", DbType.String, xml);
                //oDataBase.ExecuteNonQuery(oDbCommand);


                BETarifaTest test = null;

                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        test = new BETarifaTest();
                        if (!dr.IsDBNull(dr.GetOrdinal("CHAR1_ID")))
                            test.CHAR1_ID = dr.GetDecimal(dr.GetOrdinal("CHAR1_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SECC1_DESC")))
                            test.SECC1_DESC = dr.GetString(dr.GetOrdinal("SECC1_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("IND1_TR")))
                            test.IND1_TR = dr.GetString(dr.GetOrdinal("IND1_TR"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI1_FROM")))
                            test.CRI1_FROM = dr.GetDecimal(dr.GetOrdinal("CRI1_FROM"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI1_TO")))
                            test.CRI1_TO = dr.GetDecimal(dr.GetOrdinal("CRI1_TO"));


                        if (!dr.IsDBNull(dr.GetOrdinal("CHAR2_ID")))
                            test.CHAR2_ID = dr.GetDecimal(dr.GetOrdinal("CHAR2_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SECC2_DESC")))
                            test.SECC2_DESC = dr.GetString(dr.GetOrdinal("SECC2_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("IND2_TR")))
                            test.IND2_TR = dr.GetString(dr.GetOrdinal("IND2_TR"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI2_FROM")))
                            test.CRI2_FROM = dr.GetDecimal(dr.GetOrdinal("CRI2_FROM"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI2_TO")))
                            test.CRI2_TO = dr.GetDecimal(dr.GetOrdinal("CRI2_TO"));


                        if (!dr.IsDBNull(dr.GetOrdinal("CHAR3_ID")))
                            test.CHAR3_ID = dr.GetDecimal(dr.GetOrdinal("CHAR3_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SECC3_DESC")))
                            test.SECC3_DESC = dr.GetString(dr.GetOrdinal("SECC3_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("IND3_TR")))
                            test.IND3_TR = dr.GetString(dr.GetOrdinal("IND3_TR"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI3_FROM")))
                            test.CRI3_FROM = dr.GetDecimal(dr.GetOrdinal("CRI3_FROM"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI3_TO")))
                            test.CRI3_TO = dr.GetDecimal(dr.GetOrdinal("CRI3_TO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("CHAR4_ID")))
                            test.CHAR4_ID = dr.GetDecimal(dr.GetOrdinal("CHAR4_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SECC4_DESC")))
                            test.SECC4_DESC = dr.GetString(dr.GetOrdinal("SECC4_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("IND4_TR")))
                            test.IND4_TR = dr.GetString(dr.GetOrdinal("IND4_TR"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI4_FROM")))
                            test.CRI4_FROM = dr.GetDecimal(dr.GetOrdinal("CRI4_FROM"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI4_TO")))
                            test.CRI4_TO = dr.GetDecimal(dr.GetOrdinal("CRI4_TO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("CHAR5_ID")))
                            test.CHAR5_ID = dr.GetDecimal(dr.GetOrdinal("CHAR5_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SECC5_DESC")))
                            test.SECC5_DESC = dr.GetString(dr.GetOrdinal("SECC5_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("IND5_TR")))
                            test.IND5_TR = dr.GetString(dr.GetOrdinal("IND5_TR"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI5_FROM")))
                            test.CRI5_FROM = dr.GetDecimal(dr.GetOrdinal("CRI5_FROM"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CRI5_TO")))
                            test.CRI5_TO = dr.GetDecimal(dr.GetOrdinal("CRI5_TO"));


                        if (!dr.IsDBNull(dr.GetOrdinal("VAL_FORMULA")))
                            test.VAL_FORMULA = dr.GetDecimal(dr.GetOrdinal("VAL_FORMULA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("VAL_MINIMUM")))
                            test.VAL_MINIMUM = dr.GetDecimal(dr.GetOrdinal("VAL_MINIMUM"));

                        test.CALRV_ID = dr.GetDecimal(dr.GetOrdinal("CALRV_ID"));
                        test.CALR_ID = dr.GetDecimal(dr.GetOrdinal("CALR_ID"));
                        test.TEMP_ID = dr.GetDecimal(dr.GetOrdinal("TEMP_ID"));
                        test.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                        lista.Add(test);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lista;
        }


        public int IdCaracteristicaOtro(string owner)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_CAR_OTRO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            int id = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return id;
        }

        public decimal ObtenerTarifaActual(string owner, decimal idTarifa,  DateTime fecha)
        {
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TARIFA_ACTUAL");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, idTarifa);
                oDataBase.AddInParameter(oDbCommand, "@FECHA", DbType.DateTime, fecha);
                decimal id = Convert.ToDecimal(oDataBase.ExecuteScalar(oDbCommand));
                return id;
            }catch(Exception ex){
                return 0;
            }
        }


        public int ObtieneTarifaDescuentoEspecial(decimal RATE_ID)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTIENE_REDONDEO_ESP");
            oDataBase.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, RATE_ID);
            int resp = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return resp;
        }

        public int VALIDAR_REDONDEO_FACTURA(decimal RATE_ID)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("VALIDAR_REDONDEO_FACTURA");
            oDataBase.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, RATE_ID);
            int Retorno = 0;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    Retorno = dr.GetInt32(dr.GetOrdinal("RATE_REDONDEO_ESP"));
                }
            }
            return Retorno;
        }

    }
}

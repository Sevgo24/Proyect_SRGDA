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
    public class DALicenciaReporte
    {
        private Database oDatabase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BELicenciaReporte entidad)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASI_ART_REP_CAB");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
            oDatabase.AddInParameter(oDbCommand, "@SHOW_ID", DbType.Decimal, entidad.SHOW_ID);
            oDatabase.AddInParameter(oDbCommand, "@ARTIST_ID", DbType.Decimal, entidad.ARTIST_ID);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_TYPE", DbType.Decimal, entidad.REPORT_TYPE);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_STATUS", DbType.Decimal, entidad.REPORT_STATUS);
            oDatabase.AddInParameter(oDbCommand, "@MOD_ORIG", DbType.String, entidad.MOD_ORIG.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@MOD_SOC", DbType.String, entidad.MOD_SOC.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@CLASS_COD", DbType.String, entidad.CLASS_COD.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, entidad.MOG_ID.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@RIGHT_COD", DbType.String, entidad.RIGHT_COD.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@MOD_INCID", DbType.String, entidad.MOD_INCID.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@MOD_USAGE", DbType.String, entidad.MOD_USAGE.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@MOD_REPER", DbType.String, entidad.MOD_REPER.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@REPORT_ESOCIETY", DbType.String, entidad.REPORT_ESOCIETY != null ? entidad.REPORT_ESOCIETY.ToUpper() : null);
            oDatabase.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, entidad.TIS_N);
            oDatabase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, entidad.BPS_ID);
            oDatabase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, entidad.LIC_ID);
            oDatabase.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, entidad.EST_ID);
            oDatabase.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, entidad.LIC_PL_ID);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_DATE_FROM", DbType.DateTime, entidad.REPORT_DATE_FROM);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_DATE_TO", DbType.DateTime, entidad.REPORT_DATE_TO);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_INV", DbType.String, entidad.REPORT_INV != null ? entidad.REPORT_INV.ToUpper() : null);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_USGD", DbType.Decimal, entidad.REPORT_USGD);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_TIMES", DbType.Decimal, entidad.REPORT_TIMES);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_CALC", DbType.Decimal, entidad.REPORT_CALC);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_DIST_CODE", DbType.String, entidad.REPORT_DIST_CODE != null ? entidad.REPORT_DIST_CODE.ToUpper() : null);
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, entidad.LOG_USER_CREAT.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@REPORT_DESC", DbType.String, entidad.REPORT_DESC != null ? entidad.REPORT_DESC.ToUpper() : null);

            oDatabase.AddInParameter(oDbCommand, "@NMR_ID", DbType.Decimal, entidad.NMR_ID);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_NUMBER_REFERENCE", DbType.Decimal, entidad.REPORT_NUMBER_REFERENCE);
            
            int n = oDatabase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BELicenciaReporte entidad)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASU_ART_REP_CAB");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_ID", DbType.Decimal, entidad.REPORT_ID);
            oDatabase.AddInParameter(oDbCommand, "@SHOW_ID", DbType.Decimal, entidad.SHOW_ID);
            oDatabase.AddInParameter(oDbCommand, "@ARTIST_ID", DbType.Decimal, entidad.ARTIST_ID);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_TYPE", DbType.Decimal, entidad.REPORT_TYPE);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_STATUS", DbType.Decimal, entidad.REPORT_STATUS);
            oDatabase.AddInParameter(oDbCommand, "@MOD_ORIG", DbType.String, entidad.MOD_ORIG.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@MOD_SOC", DbType.String, entidad.MOD_SOC.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@CLASS_COD", DbType.String, entidad.CLASS_COD.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, entidad.MOG_ID.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@RIGHT_COD", DbType.String, entidad.RIGHT_COD.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@MOD_INCID", DbType.String, entidad.MOD_INCID.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@MOD_USAGE", DbType.String, entidad.MOD_USAGE.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@MOD_REPER", DbType.String, entidad.MOD_REPER.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@REPORT_ESOCIETY", DbType.String, entidad.REPORT_ESOCIETY != null ? entidad.REPORT_ESOCIETY.ToUpper() : null);
            oDatabase.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, entidad.TIS_N);
            oDatabase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, entidad.BPS_ID);
            oDatabase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, entidad.LIC_ID);
            oDatabase.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, entidad.EST_ID);
            oDatabase.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, entidad.LIC_PL_ID);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_DATE_FROM", DbType.DateTime, entidad.REPORT_DATE_FROM);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_DATE_TO", DbType.DateTime, entidad.REPORT_DATE_TO);

            oDatabase.AddInParameter(oDbCommand, "@REPORT_INV", DbType.String, entidad.REPORT_INV != null ? entidad.REPORT_INV.ToUpper() : null);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_USGD", DbType.Decimal, entidad.REPORT_USGD);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_TIMES", DbType.Decimal, entidad.REPORT_TIMES);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_CALC", DbType.Decimal, entidad.REPORT_CALC);

            oDatabase.AddInParameter(oDbCommand, "@REPORT_DIST_CODE", DbType.String, entidad.REPORT_DIST_CODE != null ? entidad.REPORT_DIST_CODE.ToUpper() : null);
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, entidad.LOG_USER_UPDATE != null ? entidad.LOG_USER_UPDATE.ToUpper() : null);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_DESC", DbType.String, entidad.REPORT_DESC != null ? entidad.REPORT_DESC.ToUpper() : null);


            oDatabase.AddInParameter(oDbCommand, "@NMR_ID", DbType.Decimal, entidad.NMR_ID);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_NUMBER_REFERENCE", DbType.Decimal, entidad.REPORT_NUMBER_REFERENCE);
            int n = oDatabase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public List<BELicenciaReporte> ListarPorLicencia(string Owner, decimal IdLicencia, string ModUso,int Anio,int Mes)
        {
            try
            {
                List<BELicenciaReporte> lista = new List<BELicenciaReporte>();
                BELicenciaReporte item = null;

                using (DbCommand cm = oDatabase.GetStoredProcCommand("SGRDASS_ART_REP_LIC"))
                {
                    oDatabase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                    oDatabase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, IdLicencia);
                    oDatabase.AddInParameter(cm, "@MODUSO", DbType.String, ModUso);
                    oDatabase.AddInParameter(cm, "@ANIO", DbType.Int32, Anio);
                    oDatabase.AddInParameter(cm, "@MES", DbType.Int32, Mes);
                    cm.CommandTimeout = 60;

                    using (IDataReader dr = oDatabase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BELicenciaReporte();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.REPORT_ID = dr.GetDecimal(dr.GetOrdinal("REPORT_ID"));

                            if (!dr.IsDBNull(dr.GetOrdinal("SHOW_ID")))
                                item.SHOW_ID = dr.GetDecimal(dr.GetOrdinal("SHOW_ID"));

                            if (!dr.IsDBNull(dr.GetOrdinal("ARTIST_ID")))
                                item.ARTIST_ID = dr.GetDecimal(dr.GetOrdinal("ARTIST_ID"));

                            if (!dr.IsDBNull(dr.GetOrdinal("REPORT_TYPE")))
                                item.REPORT_TYPE = dr.GetDecimal(dr.GetOrdinal("REPORT_TYPE"));

                            if (!dr.IsDBNull(dr.GetOrdinal("REPORT_STATUS")))
                                item.REPORT_STATUS = dr.GetDecimal(dr.GetOrdinal("REPORT_STATUS"));

                            item.MOD_ORIG = dr.GetString(dr.GetOrdinal("MOD_ORIG"));
                            item.MOD_SOC = dr.GetString(dr.GetOrdinal("MOD_SOC"));
                            item.CLASS_COD = dr.GetString(dr.GetOrdinal("CLASS_COD"));
                            item.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                            item.RIGHT_COD = dr.GetString(dr.GetOrdinal("RIGHT_COD"));
                            item.MOD_INCID = dr.GetString(dr.GetOrdinal("MOD_INCID"));
                            item.MOD_USAGE = dr.GetString(dr.GetOrdinal("MOD_USAGE"));
                            item.MOD_REPER = dr.GetString(dr.GetOrdinal("MOD_REPER"));

                            if (!dr.IsDBNull(dr.GetOrdinal("REPORT_ESOCIETY")))
                                item.REPORT_ESOCIETY = dr.GetString(dr.GetOrdinal("REPORT_ESOCIETY"));

                            if (!dr.IsDBNull(dr.GetOrdinal("TIS_N")))
                                item.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));

                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                                item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));

                            if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                                item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));

                            if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                                item.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));

                            if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_ID")))
                                item.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));

                            if (!dr.IsDBNull(dr.GetOrdinal("REPORT_DATE_FROM")))
                                item.REPORT_DATE_FROM = dr.GetDateTime(dr.GetOrdinal("REPORT_DATE_FROM"));

                            if (!dr.IsDBNull(dr.GetOrdinal("REPORT_DATE_TO")))
                                item.REPORT_DATE_TO = dr.GetDateTime(dr.GetOrdinal("REPORT_DATE_TO"));

                            if (!dr.IsDBNull(dr.GetOrdinal("REPORT_INV")))
                                item.REPORT_INV = dr.GetString(dr.GetOrdinal("REPORT_INV"));

                            if (!dr.IsDBNull(dr.GetOrdinal("REPORT_USGD")))
                                item.REPORT_USGD = dr.GetDecimal(dr.GetOrdinal("REPORT_USGD"));

                            if (!dr.IsDBNull(dr.GetOrdinal("REPORT_TIMES")))
                                item.REPORT_TIMES = dr.GetDecimal(dr.GetOrdinal("REPORT_TIMES"));

                            if (!dr.IsDBNull(dr.GetOrdinal("REPORT_CALC")))
                                item.REPORT_CALC = dr.GetDecimal(dr.GetOrdinal("REPORT_CALC"));

                            if (!dr.IsDBNull(dr.GetOrdinal("REPORT_DIST_CODE")))
                                item.REPORT_DIST_CODE = dr.GetString(dr.GetOrdinal("REPORT_DIST_CODE"));

                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                                item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                                item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                                item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                                item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                            if (!dr.IsDBNull(dr.GetOrdinal("REPORT_DESC")))
                                item.REPORT_DESC = dr.GetString(dr.GetOrdinal("REPORT_DESC"));

                            if (!dr.IsDBNull(dr.GetOrdinal("NMR_ID")))
                                item.NMR_ID = dr.GetDecimal(dr.GetOrdinal("NMR_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("REPORT_NUMBER")))
                                item.REPORT_NUMBER = dr.GetDecimal(dr.GetOrdinal("REPORT_NUMBER"));
                            if (!dr.IsDBNull(dr.GetOrdinal("REPORT_NCOPY")))
                                item.REPORT_NCOPY = dr.GetInt32(dr.GetOrdinal("REPORT_NCOPY"));
                            //if (!dr.IsDBNull(dr.GetOrdinal("REPORT_IND")))
                            //    item.REPORT_IND = dr.GetByte(dr.GetOrdinal("REPORT_IND"));

                            //---------------------------------------------------------------------
                            if (!dr.IsDBNull(dr.GetOrdinal("INVT_DESC")))
                                item.INVT_DESC = dr.GetString(dr.GetOrdinal("INVT_DESC"));
                            if (!dr.IsDBNull(dr.GetOrdinal("NMR_SERIAL")))
                                item.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                            if (!dr.IsDBNull(dr.GetOrdinal("INV_NUMBER")))
                                item.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("INV_NUMBER"));
                            if (!dr.IsDBNull(dr.GetOrdinal("LIC_MONTH")))
                                item.LIC_MONTH = dr.GetDecimal(dr.GetOrdinal("LIC_MONTH"));
                            if (!dr.IsDBNull(dr.GetOrdinal("LIC_YEAR")))
                                item.LIC_YEAR = dr.GetDecimal(dr.GetOrdinal("LIC_YEAR"));
                            if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                                item.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));

                            if (!dr.IsDBNull(dr.GetOrdinal("REPORT_NUMBER_REFERENCE")))
                                item.REPORT_NUMBER_REFERENCE = dr.GetDecimal(dr.GetOrdinal("REPORT_NUMBER_REFERENCE"));

                            if (!dr.IsDBNull(dr.GetOrdinal("NAME")))
                                item.NAME = dr.GetString(dr.GetOrdinal("NAME"));
                            lista.Add(item);
                        }
                    }
                }
                return lista;
            }catch(Exception ex){
                return null;
            }
        }

        public BELicenciaReporte Obtener(string Owner, decimal Id)
        {
            BELicenciaReporte item = null;

            using (DbCommand cm = oDatabase.GetStoredProcCommand("SGRDASS_OBTENER_ART_REP_CAB"))
            {
                oDatabase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                oDatabase.AddInParameter(cm, "@REPORT_ID", DbType.Decimal, Id);

                using (IDataReader dr = oDatabase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BELicenciaReporte();
                        item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        item.REPORT_ID = dr.GetDecimal(dr.GetOrdinal("REPORT_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("SHOW_ID")))
                            item.SHOW_ID = dr.GetDecimal(dr.GetOrdinal("SHOW_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ARTIST_ID")))
                            item.ARTIST_ID = dr.GetDecimal(dr.GetOrdinal("ARTIST_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REPORT_TYPE")))
                            item.REPORT_TYPE = dr.GetDecimal(dr.GetOrdinal("REPORT_TYPE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REPORT_STATUS")))
                            item.REPORT_STATUS = dr.GetDecimal(dr.GetOrdinal("REPORT_STATUS"));
                        item.MOD_ORIG = dr.GetString(dr.GetOrdinal("MOD_ORIG"));

                        item.MOD_SOC = dr.GetString(dr.GetOrdinal("MOD_SOC"));
                        item.CLASS_COD = dr.GetString(dr.GetOrdinal("CLASS_COD"));
                        item.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                        item.RIGHT_COD = dr.GetString(dr.GetOrdinal("RIGHT_COD"));
                        item.MOD_INCID = dr.GetString(dr.GetOrdinal("MOD_INCID"));
                        item.MOD_USAGE = dr.GetString(dr.GetOrdinal("MOD_USAGE"));
                        item.MOD_REPER = dr.GetString(dr.GetOrdinal("MOD_REPER"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REPORT_ESOCIETY")))
                            item.REPORT_ESOCIETY = dr.GetString(dr.GetOrdinal("REPORT_ESOCIETY"));

                        if (!dr.IsDBNull(dr.GetOrdinal("TIS_N")))
                            item.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));

                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                            item.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_ID")))
                            item.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REPORT_DATE_FROM")))
                            item.REPORT_DATE_FROM = dr.GetDateTime(dr.GetOrdinal("REPORT_DATE_FROM"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REPORT_DATE_TO")))
                            item.REPORT_DATE_TO = dr.GetDateTime(dr.GetOrdinal("REPORT_DATE_TO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REPORT_INV")))
                            item.REPORT_INV = dr.GetString(dr.GetOrdinal("REPORT_INV"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REPORT_USGD")))
                            item.REPORT_USGD = dr.GetDecimal(dr.GetOrdinal("REPORT_USGD"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REPORT_TIMES")))
                            item.REPORT_TIMES = dr.GetDecimal(dr.GetOrdinal("REPORT_TIMES"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REPORT_CALC")))
                            item.REPORT_CALC = dr.GetDecimal(dr.GetOrdinal("REPORT_CALC"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REPORT_DIST_CODE")))
                            item.REPORT_DIST_CODE = dr.GetString(dr.GetOrdinal("REPORT_DIST_CODE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

                        item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REPORT_DESC")))
                            item.REPORT_DESC = dr.GetString(dr.GetOrdinal("REPORT_DESC"));

                        if (!dr.IsDBNull(dr.GetOrdinal("NMR_ID")))
                            item.NMR_ID = dr.GetDecimal(dr.GetOrdinal("NMR_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REPORT_NUMBER_REFERENCE")))
                            item.REPORT_NUMBER_REFERENCE = dr.GetDecimal(dr.GetOrdinal("REPORT_NUMBER_REFERENCE"));
                    }
                }
            }
            return item;
        }

        public int Eliminar(BELicenciaReporte entidad)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASD_ART_REP_CAB");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_ID", DbType.Decimal, entidad.REPORT_ID);
            int n = oDatabase.ExecuteNonQuery(oDbCommand);
            return n;
        }
        public int Activar(string owner, decimal reportId, string usuModi)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASU_ACTIVA_REPORTE");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_ID", DbType.Decimal, reportId);
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, usuModi);
            int n = oDatabase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public BELicenciaReporte ObtenerSeriePlanilla(string Owner, decimal IdLic, decimal idReport)
        {
            BELicenciaReporte item = null;

            using (DbCommand cm = oDatabase.GetStoredProcCommand("SGRDAS_GET_SERIE_PLANILLA"))
            {
                oDatabase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                oDatabase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, IdLic);
                oDatabase.AddInParameter(cm, "@REPORT_ID", DbType.Decimal, idReport);

                using (IDataReader dr = oDatabase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BELicenciaReporte();
                        if (!dr.IsDBNull(dr.GetOrdinal("NMR_ID")))
                            item.NMR_ID = dr.GetDecimal(dr.GetOrdinal("NMR_ID"));
                    }
                }
            }
            return item;
        }

        public int ActualizarEstadoImpresion(string owner, decimal? idReport)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDAU_ESTADO_IMPRESION_PLANILLA");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_ID", DbType.Decimal, idReport);
            int r = oDatabase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BELicenciaReporte ObtenerEstadoImpresion(string owner, decimal idReport)
        {
            BELicenciaReporte item = null;
            using (DbCommand cm = oDatabase.GetStoredProcCommand("SGRDAS_ESTADO_IMPRESION_PLANILLA"))
            {
                oDatabase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDatabase.AddInParameter(cm, "@REPORT_ID", DbType.Decimal, idReport);

                using (IDataReader dr = oDatabase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BELicenciaReporte();
                        if (!dr.IsDBNull(dr.GetOrdinal("REPORT_IND")))
                            item.REPORT_IND = dr.GetBoolean(dr.GetOrdinal("REPORT_IND"));
                    }
                }
            }
            return item;
        }

        public BELicenciaReporte ObtenerFacturaNotacreditoAnulada(string owner, decimal idLic, decimal idPerFac)
        {
            BELicenciaReporte item = null;
            using (DbCommand cm = oDatabase.GetStoredProcCommand("SGRDAS_ANULADA_NOTACREDITO_FACT"))
            {
                oDatabase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDatabase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);
                oDatabase.AddInParameter(cm, "@LIC_PL_ID", DbType.Decimal, idPerFac);

                using (IDataReader dr = oDatabase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BELicenciaReporte();
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                            item.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_CN_REF")))
                            item.INV_CN_REF = dr.GetDecimal(dr.GetOrdinal("INV_CN_REF"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NULL")))
                            item.INV_NULL = dr.GetDateTime(dr.GetOrdinal("INV_NULL"));
                    }
                }
            }
            return item;
        }

        public int ActualizarNroImpresion(string owner, decimal idReport, string user)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDAU_NRO_EMSION_PLANILLA");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_ID", DbType.Decimal, idReport);
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = oDatabase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        #region plantilla
        public int InsertarPlanillaXML(string owner, string xml)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASI_PLANILLA_XML");
            oDatabase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@xml", DbType.Xml, xml);
            int r = oDatabase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        #endregion



        public List<BELicenciaReporte> ListarBandejaPlanilla(decimal ID_OFICINA,decimal ID_DIVISION, string GRUPO_MODALIDAD, 
                                                              decimal LIC_ID, decimal ID_SOCIO, string FEC_INI, string FEC_FIN, int ESTADO)
        {
            try
            {
                List<BELicenciaReporte> lista = new List<BELicenciaReporte>();
                BELicenciaReporte item = null;

                using (DbCommand cm = oDatabase.GetStoredProcCommand("SGRDASS_BANDEJA_PLANILLAS"))
                {
                    oDatabase.AddInParameter(cm, "@ID_OFICINA", DbType.Decimal, ID_OFICINA);
                    oDatabase.AddInParameter(cm, "@ID_DIVISION", DbType.Decimal, ID_DIVISION);
                    oDatabase.AddInParameter(cm, "@GRUPO_MODALIDAD", DbType.String, GRUPO_MODALIDAD);
                    oDatabase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, LIC_ID);
                    oDatabase.AddInParameter(cm, "@SOCIO", DbType.Decimal, ID_SOCIO);
                    oDatabase.AddInParameter(cm, "@FEC_INI", DbType.String, FEC_INI);
                    oDatabase.AddInParameter(cm, "@FEC_FIN", DbType.String, FEC_FIN);
                    oDatabase.AddInParameter(cm, "@ESTADO", DbType.Int32, ESTADO);
                    cm.CommandTimeout = 180;

                    using (IDataReader dr = oDatabase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BELicenciaReporte();
                            if (!dr.IsDBNull(dr.GetOrdinal("REPORT_ID")))
                                item.REPORT_ID = dr.GetDecimal(dr.GetOrdinal("REPORT_ID"));
                          
                            if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                                item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));

                            if (!dr.IsDBNull(dr.GetOrdinal("MOD_ID")))
                                item.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));

                            if (!dr.IsDBNull(dr.GetOrdinal("LIC_NAME")))
                                item.LICENCIA = dr.GetString(dr.GetOrdinal("LIC_NAME"));

                            if (!dr.IsDBNull(dr.GetOrdinal("GRUPO_MODALIDAD")))
                                item.GRUPO_MODALIDAD = dr.GetString(dr.GetOrdinal("GRUPO_MODALIDAD"));

                            if (!dr.IsDBNull(dr.GetOrdinal("MODALIDAD")))
                                item.MODALIDAD = dr.GetString(dr.GetOrdinal("MODALIDAD"));

                            if (!dr.IsDBNull(dr.GetOrdinal("LIC_DATE")))
                            {
                                item.PLANILLA = dr.GetDateTime(dr.GetOrdinal("LIC_DATE")).Year.ToString()+" - " + dr.GetDateTime(dr.GetOrdinal("LIC_DATE")).Month.ToString();
                            }

                           
                            if (!dr.IsDBNull(dr.GetOrdinal("REPORT_NUMBER")))
                                item.REPORT_NUMBER = dr.GetDecimal(dr.GetOrdinal("REPORT_NUMBER"));
                            if (!dr.IsDBNull(dr.GetOrdinal("REPORT_NCOPY")))
                                item.REPORT_NCOPY = dr.GetInt32(dr.GetOrdinal("REPORT_NCOPY"));

                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                                item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                            if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_ID")))
                                item.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));

                            lista.Add(item);
                        }
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public int GenerarPlanillaoXML(string xml,string usuarioCrea)
        {
            int result = 0;

            try
            {
                using (DbCommand cm = oDatabase.GetStoredProcCommand("SGRDASI_PLANILLAS_GENERAR_MASIVA"))
                {
                    oDatabase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    oDatabase.AddInParameter(cm, "@LOG_USER_CREAT", DbType.String, usuarioCrea);
                    result = oDatabase.ExecuteNonQuery(cm);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }




    }
}

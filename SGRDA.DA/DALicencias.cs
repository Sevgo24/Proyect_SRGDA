using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using SGRDA.Entities;
using System.Data.Common;

namespace SGRDA.DA
{
    public class DALicencias
    {
        public List<BELicencias> usp_Get_LicenciaPage(string owner, decimal LIC_ID, decimal LIC_TYPE, decimal LICS_ID, string CUR_ALPHA, decimal MOD_ID, decimal EST_ID,
                   decimal BPS_ID, string LIC_NAME, string LIC_TEMP, decimal RATE_ID, decimal LICMAS, decimal BPS_GROUP, decimal BPS_GROUP_FACT, int confecha,
                   DateTime finiauto, DateTime ffinauto, string desc_artista, decimal cod_artista_sgs, int estadoLic, int pagina, int cantRegxPag, decimal idOficina = 0)
        {

            try
            {
                Database oDataBase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LICENCIA_LISTARPAGE");
                oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
                oDataBase.AddInParameter(oDbCommand, "@LIC_TYPE", DbType.Decimal, LIC_TYPE);
                oDataBase.AddInParameter(oDbCommand, "@LICS_ID", DbType.Decimal, LICS_ID);
                oDataBase.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, CUR_ALPHA);
                oDataBase.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, MOD_ID);
                oDataBase.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, EST_ID);
                oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, BPS_ID);
                oDataBase.AddInParameter(oDbCommand, "@LIC_NAME", DbType.String, LIC_NAME);
                oDataBase.AddInParameter(oDbCommand, "@RAT_FID", DbType.String, LIC_TEMP);
                oDataBase.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, RATE_ID);
                oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
                oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
                oDataBase.AddInParameter(oDbCommand, "@ID_OFICINA", DbType.Decimal, idOficina);
                oDataBase.AddInParameter(oDbCommand, "@LICMAS", DbType.Decimal, LICMAS);
                oDataBase.AddInParameter(oDbCommand, "@GROUP_ID", DbType.Decimal, BPS_GROUP);
                oDataBase.AddInParameter(oDbCommand, "@GROUP_FACT", DbType.Decimal, BPS_GROUP_FACT);
                oDataBase.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, confecha);
                oDataBase.AddInParameter(oDbCommand, "@AUTO_START", DbType.DateTime, finiauto);
                oDataBase.AddInParameter(oDbCommand, "@AUTO_END", DbType.DateTime, ffinauto);
                oDataBase.AddInParameter(oDbCommand, "@DESC_ARTISTA", DbType.String, desc_artista);
                oDataBase.AddInParameter(oDbCommand, "@ART_COD_SGS", DbType.Decimal, cod_artista_sgs);
                oDataBase.AddInParameter(oDbCommand, "@ESTADO_LIC", DbType.Int32, estadoLic);



                oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
                oDbCommand.CommandTimeout = 60;
                //oDataBase.ExecuteNonQuery(oDbCommand);

                //string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

                //Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
                //DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LICENCIA_LISTARPAGE", owner, LIC_ID, LIC_TYPE, LICS_ID,
                //    CUR_ALPHA, MOD_ID, EST_ID, BPS_ID, LIC_NAME, LIC_TEMP, RATE_ID, LICMAS, pagina, cantRegxPag, idOficina, BPS_GROUP,BPS_GROUP_FACT,
                //    confecha,finiauto,ffinauto,desc_artista, cod_artista_sgs, ParameterDirection.Output);

                var lista = new List<BELicencias>();

                //using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand1))
                using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
                {
                    BELicencias obj = null;
                    while (reader.Read())
                    {
                        obj = new BELicencias();
                        if (!reader.IsDBNull(reader.GetOrdinal("OWNER")))
                            obj.OWNER = reader.GetString(reader.GetOrdinal("OWNER"));
                        if (!reader.IsDBNull(reader.GetOrdinal("LIC_ID")))
                            obj.LIC_ID = reader.GetDecimal(reader.GetOrdinal("LIC_ID"));
                        if (!reader.IsDBNull(reader.GetOrdinal("LIC_TYPE")))
                            obj.LIC_TYPE = reader.GetDecimal(reader.GetOrdinal("LIC_TYPE"));
                        if (!reader.IsDBNull(reader.GetOrdinal("LICS_ID")))
                            obj.LICS_ID = reader.GetDecimal(reader.GetOrdinal("LICS_ID"));
                        if (!reader.IsDBNull(reader.GetOrdinal("CUR_ALPHA")))
                            obj.CUR_ALPHA = reader.GetString(reader.GetOrdinal("CUR_ALPHA"));
                        if (!reader.IsDBNull(reader.GetOrdinal("MOD_ID")))
                            obj.MOD_ID = reader.GetDecimal(reader.GetOrdinal("MOD_ID"));
                        if (!reader.IsDBNull(reader.GetOrdinal("EST_ID")))
                            obj.EST_ID = reader.GetDecimal(reader.GetOrdinal("EST_ID"));
                        if (!reader.IsDBNull(reader.GetOrdinal("BPS_ID")))
                            obj.BPS_ID = reader.GetDecimal(reader.GetOrdinal("BPS_ID"));
                        if (!reader.IsDBNull(reader.GetOrdinal("LIC_NAME")))
                            obj.LIC_NAME = reader.GetString(reader.GetOrdinal("LIC_NAME"));
                        if (!reader.IsDBNull(reader.GetOrdinal("LIC_DESC")))
                            obj.LIC_DESC = reader.GetString(reader.GetOrdinal("LIC_DESC"));
                        if (!reader.IsDBNull(reader.GetOrdinal("LIC_MASTER")))
                            obj.LIC_MASTER = reader.GetDecimal(reader.GetOrdinal("LIC_MASTER"));
                        if (!reader.IsDBNull(reader.GetOrdinal("LIC_AUT_START")))
                            obj.LIC_AUT_START = reader.GetString(reader.GetOrdinal("LIC_AUT_START"));
                        if (!reader.IsDBNull(reader.GetOrdinal("LIC_AUT_END")))
                            obj.LIC_AUT_END = reader.GetString(reader.GetOrdinal("LIC_AUT_END"));

                        if (!reader.IsDBNull(reader.GetOrdinal("LIC_TDESC")))
                            obj.LIC_TDESC = reader.GetString(reader.GetOrdinal("LIC_TDESC"));
                        if (!reader.IsDBNull(reader.GetOrdinal("MOD_DEC")))
                            obj.MOD_DEC = reader.GetString(reader.GetOrdinal("MOD_DEC"));
                        if (!reader.IsDBNull(reader.GetOrdinal("BPS_NAME")))
                            obj.BPS_NAME = reader.GetString(reader.GetOrdinal("BPS_NAME"));
                        if (!reader.IsDBNull(reader.GetOrdinal("EST_NAME")))
                            obj.EST_NAME = reader.GetString(reader.GetOrdinal("EST_NAME"));

                        if (!reader.IsDBNull(reader.GetOrdinal("WRKF_SLABEL")))
                            obj.WRKF_SLABEL = reader.GetString(reader.GetOrdinal("WRKF_SLABEL"));

                        //obj.TotalVirtual = Convert.ToInt32(results);
                        obj.TotalVirtual = reader.GetInt32(reader.GetOrdinal("CANTIDAD"));
                        lista.Add(obj);
                    }
                    //lista.Add(new BELicencias
                    //{
                    //    OWNER = Convert.ToString(reader["OWNER"]),
                    //    LIC_ID = Convert.ToDecimal(reader["LIC_ID"]),
                    //    LIC_TYPE = Convert.ToString(reader["LIC_TYPE"]),
                    //    LICS_ID = Convert.ToString(reader["LICS_ID"]),
                    //    CUR_ALPHA = Convert.ToString(reader["CUR_ALPHA"]),
                    //    MOD_ID = Convert.ToDecimal(reader["MOD_ID"]),
                    //    EST_ID = Convert.ToDecimal(reader["EST_ID"]),
                    //    BPS_ID = Convert.ToDecimal(reader["BPS_ID"]),
                    //    LIC_NAME = Convert.ToString(reader["LIC_NAME"]),
                    //    LIC_DESC = Convert.ToString(reader["LIC_DESC"]),
                    //    LIC_MASTER = !reader.IsDBNull(reader["LIC_DESC"]) ? reader["LIC_DESC"],
                    //    TotalVirtual = Convert.ToInt32(results)
                    //});
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<BELicencias> usp_Get_LicenciaPage2(string owner, decimal LIC_ID,  decimal MOD_ID, decimal EST_ID,
                  decimal BPS_ID, string LIC_NAME, string LIC_TEMP, decimal RATE_ID, 
                  int pagina, int cantRegxPag, decimal idOficina = 0)
        {

            try
            {
                Database oDataBase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LICENCIA_LISTARPAGE2");
                oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
             
                oDataBase.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, MOD_ID);
                oDataBase.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, EST_ID);
                oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, BPS_ID);
                oDataBase.AddInParameter(oDbCommand, "@LIC_NAME", DbType.String, LIC_NAME);
                oDataBase.AddInParameter(oDbCommand, "@RAT_FID", DbType.String, LIC_TEMP);
                oDataBase.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, RATE_ID);
                oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
                oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
                oDataBase.AddInParameter(oDbCommand, "@ID_OFICINA", DbType.Decimal, idOficina);
             
                oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
                oDbCommand.CommandTimeout = 60;
                
                var lista = new List<BELicencias>();

                //using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand1))
                using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
                {
                    BELicencias obj = null;
                    while (reader.Read())
                    {
                        obj = new BELicencias();
                        if (!reader.IsDBNull(reader.GetOrdinal("OWNER")))
                            obj.OWNER = reader.GetString(reader.GetOrdinal("OWNER"));
                        if (!reader.IsDBNull(reader.GetOrdinal("LIC_ID")))
                            obj.LIC_ID = reader.GetDecimal(reader.GetOrdinal("LIC_ID"));
                        if (!reader.IsDBNull(reader.GetOrdinal("LIC_TYPE")))
                            obj.LIC_TYPE = reader.GetDecimal(reader.GetOrdinal("LIC_TYPE"));
                        if (!reader.IsDBNull(reader.GetOrdinal("LICS_ID")))
                            obj.LICS_ID = reader.GetDecimal(reader.GetOrdinal("LICS_ID"));
                        if (!reader.IsDBNull(reader.GetOrdinal("CUR_ALPHA")))
                            obj.CUR_ALPHA = reader.GetString(reader.GetOrdinal("CUR_ALPHA"));
                        if (!reader.IsDBNull(reader.GetOrdinal("MOD_ID")))
                            obj.MOD_ID = reader.GetDecimal(reader.GetOrdinal("MOD_ID"));
                        if (!reader.IsDBNull(reader.GetOrdinal("EST_ID")))
                            obj.EST_ID = reader.GetDecimal(reader.GetOrdinal("EST_ID"));
                        if (!reader.IsDBNull(reader.GetOrdinal("BPS_ID")))
                            obj.BPS_ID = reader.GetDecimal(reader.GetOrdinal("BPS_ID"));
                        if (!reader.IsDBNull(reader.GetOrdinal("LIC_NAME")))
                            obj.LIC_NAME = reader.GetString(reader.GetOrdinal("LIC_NAME"));
                        if (!reader.IsDBNull(reader.GetOrdinal("LIC_DESC")))
                            obj.LIC_DESC = reader.GetString(reader.GetOrdinal("LIC_DESC"));
                        if (!reader.IsDBNull(reader.GetOrdinal("LIC_MASTER")))
                            obj.LIC_MASTER = reader.GetDecimal(reader.GetOrdinal("LIC_MASTER"));
                        if (!reader.IsDBNull(reader.GetOrdinal("LIC_AUT_START")))
                            obj.LIC_AUT_START = reader.GetString(reader.GetOrdinal("LIC_AUT_START"));
                        if (!reader.IsDBNull(reader.GetOrdinal("LIC_AUT_END")))
                            obj.LIC_AUT_END = reader.GetString(reader.GetOrdinal("LIC_AUT_END"));

                        if (!reader.IsDBNull(reader.GetOrdinal("LIC_TDESC")))
                            obj.LIC_TDESC = reader.GetString(reader.GetOrdinal("LIC_TDESC"));
                        if (!reader.IsDBNull(reader.GetOrdinal("MOD_DEC")))
                            obj.MOD_DEC = reader.GetString(reader.GetOrdinal("MOD_DEC"));
                        if (!reader.IsDBNull(reader.GetOrdinal("BPS_NAME")))
                            obj.BPS_NAME = reader.GetString(reader.GetOrdinal("BPS_NAME"));
                        if (!reader.IsDBNull(reader.GetOrdinal("EST_NAME")))
                            obj.EST_NAME = reader.GetString(reader.GetOrdinal("EST_NAME"));

                        if (!reader.IsDBNull(reader.GetOrdinal("WRKF_SLABEL")))
                            obj.WRKF_SLABEL = reader.GetString(reader.GetOrdinal("WRKF_SLABEL"));

                        //obj.TotalVirtual = Convert.ToInt32(results);
                        obj.TotalVirtual = reader.GetInt32(reader.GetOrdinal("CANTIDAD"));
                        lista.Add(obj);
                    }
                   
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<BELicencias> usp_Get_LicenciaPage(string owner, decimal? EstablecimientoId, decimal? Off_id, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_LICENSES_GRAL_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@Est_id", DbType.Decimal, EstablecimientoId);
            oDataBase.AddInParameter(oDbCommand, "@Off_id", DbType.Decimal, Off_id);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);
            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));
            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REC_LICENSES_GRAL_GET_Page", owner, EstablecimientoId, Off_id, pagina, cantRegxPag, ParameterDirection.Output);

            BELicencias item = null;
            var lista = new List<BELicencias>();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand1))
            {
                while (dr.Read())
                {
                    item = new BELicencias();
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        item.LIC_ID = Convert.ToDecimal(dr["LIC_ID"]);

                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_NAME")))
                        item.LIC_NAME = Convert.ToString(dr["LIC_NAME"]);

                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_TDESC")))
                        item.LIC_TDESC = Convert.ToString(dr["LIC_TDESC"]);

                    if (!dr.IsDBNull(dr.GetOrdinal("RAT_FDESC")))
                        item.RAT_FDESC = Convert.ToString(dr["RAT_FDESC"]);

                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_DEC")))
                        item.MOD_DEC = Convert.ToString(dr["MOD_DEC"]);

                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                        item.BPS_NAME = Convert.ToString(dr["BPS_NAME"]);

                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_AUT_START")))
                        item.LIC_AUT_START = Convert.ToString(dr["LIC_AUT_START"]);

                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_AUT_END")))
                        item.LIC_AUT_END = Convert.ToString(dr["LIC_AUT_END"]);
                    item.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(item);
                }
            }
            return lista;
        }

        public int Eliminar(BELicencias LIC)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASD_REC_LICENSES_GRAL");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, LIC.OWNER);
                oDataBase.AddInParameter(oDbComand, "@LIC_ID", DbType.Decimal, LIC.LIC_ID);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDAT", DbType.String, LIC.LOG_USER_UPDAT);
                int r = oDataBase.ExecuteNonQuery(oDbComand);
                return r;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public decimal Insertar(BELicencias entidad)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_LICENCIA");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, entidad.OWNER);
            oDataBase.AddInParameter(oDbComand, "@LIC_TYPE", DbType.String, entidad.LIC_TYPE);
            oDataBase.AddInParameter(oDbComand, "@LIC_MASTER", DbType.Decimal, entidad.LIC_MASTER);
            oDataBase.AddInParameter(oDbComand, "@LICS_ID", DbType.Decimal, entidad.LICS_ID);
            oDataBase.AddInParameter(oDbComand, "@CUR_ALPHA", DbType.String, entidad.CUR_ALPHA);
            //oDataBase.AddInParameter(oDbComand, "@LIC_TEMP", DbType.String, entidad.LIC_TEMP);
            oDataBase.AddInParameter(oDbComand, "@LIC_NAME", DbType.String, entidad.LIC_NAME.ToUpper());
            oDataBase.AddInParameter(oDbComand, "@LIC_DESC", DbType.String, entidad.LIC_DESC.ToUpper());
            oDataBase.AddInParameter(oDbComand, "@LIC_DREQ", DbType.String, entidad.LIC_DREQ);
            oDataBase.AddInParameter(oDbComand, "@LIC_CREQ", DbType.String, entidad.LIC_CREQ);
            oDataBase.AddInParameter(oDbComand, "@INVF_ID", DbType.Decimal, entidad.INVF_ID);
            oDataBase.AddInParameter(oDbComand, "@LIC_DISC", DbType.String, entidad.LIC_DISC);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, entidad.LOG_USER_CREAT.ToUpper());
            oDataBase.AddInParameter(oDbComand, "@MOD_ID", DbType.Decimal, entidad.MOD_ID);
            oDataBase.AddInParameter(oDbComand, "@EST_ID", DbType.Decimal, entidad.EST_ID);
            oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Decimal, entidad.BPS_ID);
            //oDataBase.AddInParameter(oDbComand, "@INVG_ID", DbType.Decimal, entidad.INVG_ID);
            oDataBase.AddInParameter(oDbComand, "@RAT_FID", DbType.Decimal, entidad.RAT_FID);
            oDataBase.AddInParameter(oDbComand, "@RATE_ID", DbType.Decimal, entidad.RATE_ID);
            oDataBase.AddInParameter(oDbComand, "@PAY_ID", DbType.String, entidad.PAY_ID);
            oDataBase.AddOutParameter(oDbComand, "@LIC_ID", DbType.Decimal, 10);


            int r = oDataBase.ExecuteNonQuery(oDbComand);
            string results = Convert.ToString(oDataBase.GetParameterValue(oDbComand, "@LIC_ID"));
            return Convert.ToDecimal(results);

        }
        public decimal Actualizar(BELicencias entidad)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_LICENCIA");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, entidad.OWNER);
            oDataBase.AddInParameter(oDbComand, "@LIC_TYPE", DbType.String, entidad.LIC_TYPE);
            oDataBase.AddInParameter(oDbComand, "@LIC_MASTER", DbType.Decimal, entidad.LIC_MASTER);
            oDataBase.AddInParameter(oDbComand, "@LICS_ID", DbType.Decimal, entidad.LICS_ID);
            oDataBase.AddInParameter(oDbComand, "@CUR_ALPHA", DbType.String, entidad.CUR_ALPHA);
            //oDataBase.AddInParameter(oDbComand, "@LIC_TEMP", DbType.String, entidad.LIC_TEMP);
            oDataBase.AddInParameter(oDbComand, "@LIC_NAME", DbType.String, entidad.LIC_NAME.ToUpper());
            oDataBase.AddInParameter(oDbComand, "@LIC_DESC", DbType.String, entidad.LIC_DESC.ToUpper());
            oDataBase.AddInParameter(oDbComand, "@LIC_DREQ", DbType.String, entidad.LIC_DREQ);
            oDataBase.AddInParameter(oDbComand, "@LIC_CREQ", DbType.String, entidad.LIC_CREQ);
            oDataBase.AddInParameter(oDbComand, "@INVF_ID", DbType.String, entidad.INVF_ID);
            oDataBase.AddInParameter(oDbComand, "@LIC_DISC", DbType.String, entidad.LIC_DISC);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDAT", DbType.String, entidad.LOG_USER_UPDAT.ToUpper());
            oDataBase.AddInParameter(oDbComand, "@MOD_ID", DbType.Decimal, entidad.MOD_ID);
            oDataBase.AddInParameter(oDbComand, "@EST_ID", DbType.Decimal, entidad.EST_ID);
            oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Decimal, entidad.BPS_ID);
            //oDataBase.AddInParameter(oDbComand, "@INVG_ID", DbType.Decimal, entidad.INVG_ID);
            oDataBase.AddInParameter(oDbComand, "@LIC_ID", DbType.Decimal, entidad.LIC_ID);
            oDataBase.AddInParameter(oDbComand, "@RAT_FID", DbType.Decimal, entidad.RAT_FID);
            oDataBase.AddInParameter(oDbComand, "@RATE_ID", DbType.Decimal, entidad.RATE_ID);
            oDataBase.AddInParameter(oDbComand, "@PAY_ID", DbType.String, entidad.PAY_ID);

            int r = oDataBase.ExecuteNonQuery(oDbComand);

            return r;

        }
        public int UpdateLicenciaFacturacion(decimal codLic, string owner, string docReq, string actReq, string plaReq, string desVis, string Envio, decimal facGruop, decimal facForm, string LIC_EMI_MEN)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            var r = 0;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASU_LICENCIA_FACTURACION"))
            {
                db.AddInParameter(cm, "@LIC_ID", DbType.Decimal, codLic);
                db.AddInParameter(cm, "@LIC_DREQ", DbType.String, docReq);
                db.AddInParameter(cm, "@LIC_CREQ", DbType.String, actReq);
                db.AddInParameter(cm, "@LIC_PREQ", DbType.String, plaReq);
                db.AddInParameter(cm, "@LIC_SEND", DbType.String, Envio);
                db.AddInParameter(cm, "@LIC_DISC", DbType.String, desVis);
                db.AddInParameter(cm, "@INVG_ID", DbType.Decimal, facGruop);
                db.AddInParameter(cm, "@INVF_ID", DbType.Decimal, facForm);
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@LIC_EMI_MEN", DbType.String, LIC_EMI_MEN);


                r = db.ExecuteNonQuery(cm);
            }
            return r;
        }
        public List<string> ListarTabsXEstadoLic(string estadoTipoLic, string empresa)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            List<string> tabs = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_TABS_X_ESTADO"))
            {
                oDataBase.AddInParameter(cm, "@LICS_ID", DbType.Decimal, estadoTipoLic);
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, empresa);
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    tabs = new List<string>();
                    while (dr.Read())
                    {
                        //tabs.Add(dr.GetString(dr.GetOrdinal("TAB_NAME")));
                        tabs.Add(Convert.ToString(dr.GetInt32(dr.GetOrdinal("INDEX"))));

                    }
                }
            }
            return tabs;
        }
        public BELicencias ObtenerLicenciaXCodigo(decimal idLicencia, string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            BELicencias item = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_LICENCIA"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLicencia);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        if (dr.Read())
                        {
                            item = new BELicencias();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                            item.LIC_TYPE = dr.GetDecimal(dr.GetOrdinal("LIC_TYPE"));

                            if (!dr.IsDBNull(dr.GetOrdinal("LIC_MASTER")))
                            {
                                item.LIC_MASTER = dr.GetDecimal(dr.GetOrdinal("LIC_MASTER"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LIC_DESC"))) { item.LIC_DESC = dr.GetString(dr.GetOrdinal("LIC_DESC")); }

                            item.LICS_ID = dr.GetDecimal(dr.GetOrdinal("LICS_ID"));
                            item.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));

                            if (!dr.IsDBNull(dr.GetOrdinal("RATE_FID"))) { item.RAT_FID = dr.GetDecimal(dr.GetOrdinal("RATE_FID")); }
                            if (!dr.IsDBNull(dr.GetOrdinal("RATE_ID"))) { item.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID")); }
                            item.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                            //  item.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));

                            item.LIC_DREQ = dr.GetString(dr.GetOrdinal("LIC_DREQ"));
                            item.LIC_CREQ = dr.GetString(dr.GetOrdinal("LIC_CREQ"));
                            if (!dr.IsDBNull(dr.GetOrdinal("LIC_PREQ")))
                                item.LIC_PREQ = dr.GetString(dr.GetOrdinal("LIC_PREQ"));
                            item.INVF_ID = dr.GetDecimal(dr.GetOrdinal("INVF_ID"));
                            item.LIC_DISC = dr.GetString(dr.GetOrdinal("LIC_DISC"));
                            item.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                            {
                                item.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                            }
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));

                            if (!dr.IsDBNull(dr.GetOrdinal("INVG_ID")))
                            {
                                item.INVG_ID = dr.GetDecimal(dr.GetOrdinal("INVG_ID"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            {
                                item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDAT")))
                            {
                                item.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            {
                                item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            {
                                item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("LIC_SEND")))
                            {
                                item.LIC_SEND = dr.GetDecimal(dr.GetOrdinal("LIC_SEND"));
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("LIC_INVD")))
                            {
                                item.LIC_INVD = dr.GetString(dr.GetOrdinal("LIC_INVD"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("PAY_ID")))
                            {
                                item.PAY_ID = dr.GetString(dr.GetOrdinal("PAY_ID"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("LIC_EMI_MENSUAL")))
                            {
                                item.LIC_EMI_MENSUAL = dr.GetString(dr.GetOrdinal("LIC_EMI_MENSUAL"));
                            }
                        }
                    }
                }
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool ValidarLicenciaMultiple(string owner, decimal tipoLic, decimal idLicValidar)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_VALIDAR_LIC_MULTIPLE");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@LIC_TYPE", DbType.String, tipoLic);
            oDataBase.AddInParameter(oDbComand, "@LIC_MASTER", DbType.Decimal, idLicValidar);
            oDataBase.AddOutParameter(oDbComand, "@VALIDO", DbType.Boolean, 1);

            int r = oDataBase.ExecuteNonQuery(oDbComand);
            return Convert.ToBoolean(oDataBase.GetParameterValue(oDbComand, "@VALIDO"));


        }
        public int ActualizarEstadoLicenciaXActionsMapping(string owner, decimal idLic, decimal amidWrkf)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_LICENCIA_ESTADO_X_ACTIONSMAPPING"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLic);
                db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, amidWrkf);
                db.AddOutParameter(oDbCommand, "@LICS_ID", DbType.Decimal, 10);


                int r = db.ExecuteNonQuery(oDbCommand);
                string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@LICS_ID"));

                return Convert.ToInt32(results);
            }
        }
        public int ActualizarEstadoLicencia(string owner, decimal idLic, decimal sidWrkf)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_LICENCIA_ESTADO"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLic);
                db.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, sidWrkf);

                int r = db.ExecuteNonQuery(oDbCommand);

                return r;
            }
        }

        public List<BELicencias> ListarFacturaMasiva_LicenciaSubGrilla(string owner, DateTime fini, DateTime ffin,
                       string mogId, decimal modId, decimal dadId, decimal bpsId,
                       decimal offId, decimal e_bpsId, decimal tipoEstId, decimal subTipoEstId, decimal licId, string monedaId, decimal LibConfi, int historico, string periodoEstado
                       , decimal idBpsGroup, decimal groupfact, int oficina, int EmiMensual)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_LICENCIAS_GRAL_SUBGRILLA");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@FINI", DbType.DateTime, fini);
                db.AddInParameter(oDbCommand, "@FFIN", DbType.DateTime, ffin);
                db.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, mogId);
                db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, modId);

                db.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, dadId);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, bpsId);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, offId);
                db.AddInParameter(oDbCommand, "@E_BPS_ID", DbType.Decimal, e_bpsId);
                db.AddInParameter(oDbCommand, "@ESTT_ID", DbType.Decimal, tipoEstId);

                db.AddInParameter(oDbCommand, "@SUBE_ID", DbType.Decimal, subTipoEstId);
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, licId);
                db.AddInParameter(oDbCommand, "@VAR_ID", DbType.Decimal, LibConfi);
                db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, monedaId);
                db.AddInParameter(oDbCommand, "@HISTORICO", DbType.Int32, historico);
                db.AddInParameter(oDbCommand, "@PERIODO_ESTADO", DbType.String, periodoEstado);
                db.AddInParameter(oDbCommand, "@BPS_GROUP", DbType.Decimal, idBpsGroup);
                db.AddInParameter(oDbCommand, "@GROUP_FACT", DbType.Decimal, groupfact);
                db.AddInParameter(oDbCommand, "@OFICINA", DbType.Decimal, oficina);
                db.AddInParameter(oDbCommand, "@FACT_EMI_MENSUAL", DbType.Int32, EmiMensual);
                oDbCommand.CommandTimeout = 4000;

                //db.ExecuteNonQuery(oDbCommand);
                var lista = new List<BELicencias>();

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BELicencias licencia = null;
                    while (dr.Read())
                    {
                        licencia = new BELicencias();
                        licencia.OWNER = owner;
                        licencia.Nro = Convert.ToDecimal(dr.GetInt64(dr.GetOrdinal("Nro")));
                        licencia.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        licencia.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        licencia.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                        licencia.PAY_ID = dr.GetString(dr.GetOrdinal("PAY_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVG_ID")))
                            licencia.INVG_ID = dr.GetDecimal(dr.GetOrdinal("INVG_ID"));
                        licencia.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME")).ToUpper();
                        licencia.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));
                        licencia.Modalidad = dr.GetString(dr.GetOrdinal("MOD_DEC")).ToUpper();
                        licencia.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                        licencia.Establecimiento = dr.GetString(dr.GetOrdinal("EST_NAME")).ToUpper();
                        //licencia.SUBTOTAL = dr.GetDecimal(dr.GetOrdinal("SUBTOTAL"));
                        //licencia.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                        //licencia.IMPUESTO =Convert.ToDecimal( 0.18);
                        licencia.LIC_CREQ = dr.GetString(dr.GetOrdinal("LIC_CREQ"));
                        lista.Add(licencia);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public List<BELicencias> ListarLicenciaBorrador(string owner, DateTime fini, DateTime ffin,
                                                     decimal tipoLic, string idMoneda, decimal idGrufact,
                                                     decimal idBps, decimal idCorrelativo, string idTipoPago,
                                                     int conFecha, decimal idLic, decimal idfactura, decimal off_id)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_LICENCIAS_BORRADOR");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@FINI", DbType.DateTime, fini);
            db.AddInParameter(oDbCommand, "@FFIN", DbType.DateTime, ffin);
            db.AddInParameter(oDbCommand, "@LIC_TYPE", DbType.Decimal, tipoLic);
            db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, idMoneda);
            db.AddInParameter(oDbCommand, "@INVG_ID", DbType.Decimal, idGrufact);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, idBps);
            db.AddInParameter(oDbCommand, "@INV_NMR", DbType.Decimal, idCorrelativo);
            db.AddInParameter(oDbCommand, "@PAY_ID", DbType.String, idTipoPago);
            db.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, conFecha);
            db.AddInParameter(oDbCommand, "@IDLIC", DbType.Decimal, idLic);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idfactura);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, off_id);
            //db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BELicencias>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BELicencias licencia = null;
                while (dr.Read())
                {
                    licencia = new BELicencias();
                    licencia.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                    licencia.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    licencia.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                    licencia.Modalidad = dr.GetString(dr.GetOrdinal("MOD_DEC"));
                    licencia.Establecimiento = dr.GetString(dr.GetOrdinal("EST_NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_GROSS")))
                        licencia.INVL_GROSS = dr.GetDecimal(dr.GetOrdinal("INVL_GROSS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_DISC")))
                        licencia.INVL_DISC = dr.GetDecimal(dr.GetOrdinal("INVL_DISC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_BASE")))
                        licencia.INVL_BASE = dr.GetDecimal(dr.GetOrdinal("INVL_BASE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_TAXES")))
                        licencia.INVL_TAXES = dr.GetDecimal(dr.GetOrdinal("INVL_TAXES"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_NET")))
                        licencia.INVL_NET = dr.GetDecimal(dr.GetOrdinal("INVL_NET"));
                    lista.Add(licencia);
                }
            }
            return lista;
        }

        public List<BELicencias> ListarFacturaPendienteDetalle(string owner, decimal idfact)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_DETALLE_FAC_LIC");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idfact);
            db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BELicencias>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BELicencias licencia = null;
                while (dr.Read())
                {
                    licencia = new BELicencias();
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                        licencia.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        licencia.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_NAME")))
                        licencia.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_DEC")))
                        licencia.Modalidad = dr.GetString(dr.GetOrdinal("MOD_DEC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                        licencia.Establecimiento = dr.GetString(dr.GetOrdinal("EST_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_BASE")))
                        licencia.INVL_BASE = dr.GetDecimal(dr.GetOrdinal("INVL_BASE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_TAXES")))
                        licencia.INVL_TAXES = dr.GetDecimal(dr.GetOrdinal("INVL_TAXES"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_NET")))
                        licencia.INVL_NET = dr.GetDecimal(dr.GetOrdinal("INVL_NET"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_BALANCE")))
                        licencia.INVL_BALANCE = dr.GetDecimal(dr.GetOrdinal("INVL_BALANCE"));
                    lista.Add(licencia);
                }
            }
            return lista;
        }

        #region cadenas

        //Listar Establecimientos  de las licencias Hijas x Cod Mult
        public List<BELicencias> ListarLicenciasHijasxCodMultiple(string owner, decimal CodLicPadre)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_ESTAB_HIJAS_MULTI");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LICMASTER", DbType.Decimal, CodLicPadre);
            db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BELicencias>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BELicencias licencia = null;
                while (dr.Read())
                {
                    licencia = new BELicencias();
                    if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                        licencia.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_DESC")))
                        licencia.LIC_DESC = dr.GetString(dr.GetOrdinal("LIC_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        licencia.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_NAME")))
                        licencia.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                    lista.Add(licencia);
                }

            }

            return lista;

        }

        public void InactivaLicenciasHijas(decimal CodLic, string owner, decimal licmaster)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_INACTIVA_LICENCIA_HIJA");
            db.AddInParameter(oDbCommand, "@CODest", DbType.Decimal, CodLic);
            db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@licmaster", DbType.Decimal, licmaster);
            db.ExecuteNonQuery(oDbCommand);
            //RETURN IRRELEVANTE (Preguntar Que se Puede retornar
            //return 1;

        }

        public int ValidarLicenciaMultipleHija(decimal CodLic)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_VALIDAR_LICENCIA_MULTIPLE_HIJA");
            db.AddInParameter(oDbCommand, "@COD_LIC", DbType.Decimal, CodLic);

            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        public int ValidarLicenciaMultiplePadre(decimal codLic)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRTDAS_VALIDAR_SI_ES_LICENCIA_MULTIPLE");
            db.AddInParameter(oDbCommand, "@CODLIC", DbType.Decimal, codLic);

            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;

        }

        //Elimnar Las Licencias Padre y Hijas
        //Recibe La Entida
        public int EliminarLicPadreHija(BELicencias LIC)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASD_REC_LICENSES_GRAL_PADRE");
                oDataBase.AddInParameter(oDbComand, "@LICID", DbType.Decimal, LIC.LIC_ID);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDATE", DbType.String, LIC.LOG_USER_UPDAT);
                int r = oDataBase.ExecuteNonQuery(oDbComand);
                return r;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Listar Licencias Hijas Por Licencia Padre
        public List<BELicencias> ListarLienciasHijas(decimal CodLic)
        {
            var lista = new List<BELicencias>();
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTA_LISTAS_LIC_HIJAS_X_PADRE");
                db.AddInParameter(oDbCommand, "@LICID", DbType.Decimal, CodLic);
                db.ExecuteNonQuery(oDbCommand);
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BELicencias licencia = null;
                    while (dr.Read())
                    {
                        licencia = new BELicencias();
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            licencia.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));


                        lista.Add(licencia);
                    }

                }
            }
            catch (Exception ex)
            {
            }
            return lista;
        }

        public List<BELicenciaPlaneamiento> ListarLicPlanxLicHija(decimal LicId)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_PLANIFICACION");
            db.AddInParameter(oDbCommand, "@LICID", DbType.Decimal, LicId);
            //db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BELicenciaPlaneamiento>();
            try
            {
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BELicenciaPlaneamiento licencia = null;
                    while (dr.Read())
                    {
                        licencia = new BELicenciaPlaneamiento();
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_ID")))
                            licencia.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            licencia.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));

                        lista.Add(licencia);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lista;


        }
        //Valida Si un Establecimiento se encuentra insertado
        public int ValidarEstblecimientoInsert(decimal CodEst)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_VALIDA_EST_ID_REG");
            db.AddInParameter(oDbCommand, "@CODEST", DbType.Decimal, CodEst);

            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        //Listar Codigo Licencia Hija Mediante el Cod de Est
        public decimal RecuperaCodigoLicHijxCodEst(decimal EstId, string owner, decimal licmaster)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTA_IDLIC_X_IEEST");
            db.AddInParameter(oDbCommand, "@CODEST", DbType.Decimal, EstId);
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@licmaster", DbType.Decimal, licmaster);
            decimal r = Convert.ToDecimal(db.ExecuteScalar(oDbCommand));
            return r;

        }

        //Permite validar si una licencia posee caracteristicas insertadas

        public int ValidaCaractLicencia(string owner, decimal licid)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_VALIDA_CARACT_X_LIC");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LICID", DbType.Decimal, licid);

            int res = Convert.ToInt32(db.ExecuteScalar(oDbCommand));

            return res;

        }

        //Autogenera El codigo de Licencia Multiple ,que va a ser igual a el codigo de licencia del padre
        public decimal AutogeneraCodLimpieza()
        {

            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand cm = db.GetStoredProcCommand("SGRDASS_AUTOGENERA_LIC_MASTER");

            decimal res = Convert.ToDecimal(db.ExecuteScalar(cm));
            return res;
        }

        #region Insercion y Modificacion de licencias(XML)
        //ACTUALIZANDO Lista de Licencias MEDIANTE XML
        public int ActualizaLicenciasHijasXML(string owner, string xml)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_LICENCIA_HIJA_XML");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@xmlLst", DbType.Xml, xml);

            int r = db.ExecuteNonQuery(oDbCommand);

            return r;
        }
        //Inserta Lista de Licencias Mediante XML
        public List<BELicencias> InsertaLicenciaHijaXML(string owner, string xml)
        {
            List<BELicencias> lista = null;
            BELicencias entidad = null;
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_LICENCIA_HIJA_XML");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@xmlLst", DbType.Xml, xml);

            using (IDataReader dr = (db.ExecuteReader(oDbCommand)))
            {
                lista = new List<BELicencias>();
                while (dr.Read())
                {
                    entidad = new BELicencias();

                    if (!dr.IsDBNull(dr.GetOrdinal("newLIC_ID")))
                        entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("newLIC_ID"));
                    lista.Add(entidad);
                }
            }
            //no devuelve nada por que es un insert masivo.. 1 inserto 0 no inserto
            return lista;
        }

        //ACTUALIZANDO Lista de Licencias MEDIANTE XML
        public int ActualizaLicenciasFacturacionHijasXML(string owner, string xml)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_LICENCIA_FACTURA_XML");
            db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@xml", DbType.Xml, xml);

            int r = db.ExecuteNonQuery(oDbCommand);

            return r;
        }

        #endregion

        /// <summary>
        /// LISTA LAS LICENCIAS ASOCIADAS A UN ESTABLECIMIENTO 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="ESTID"></param>
        /// <returns></returns>
        public List<BELicencias> ListaLicenciaxCodigoEst(string owner, decimal ESTID)
        {
            List<BELicencias> lista = null;
            BELicencias entidad = null;
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_LISTA_LIC_X_COD_EST");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@CODEST", DbType.Decimal, ESTID);

            using (IDataReader dr = (oDatabase.ExecuteReader(oDbCommand)))
            {
                lista = new List<BELicencias>();
                while (dr.Read())
                {
                    entidad = new BELicencias();
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));

                    lista.Add(entidad);
                }
            }
            return lista;
        }
        /// <summary>
        /// LISTA LOS ESTABLECIMIENTOS ID SEGUN SU LICENCIA 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="LICID"></param>
        /// <returns></returns>
        public List<BELicencias> ListaCodigoEstxCodigoLicencia(string owner, decimal LICID)
        {
            List<BELicencias> lista = null;
            BELicencias entidad = null;
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_LISTA_ESTID_X_LICID");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@LICID", DbType.Decimal, LICID);

            using (IDataReader dr = (oDatabase.ExecuteReader(oDbCommand)))
            {
                lista = new List<BELicencias>();
                while (dr.Read())
                {
                    entidad = new BELicencias();
                    if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                        entidad.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));

                    lista.Add(entidad);
                }

            }
            return lista;
        }
        /// <summary>
        /// LISTA LA LICENCIA MAESTRA O PADRE DE UNA LICENCIA HIJA
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="LIC_ID"></param>
        /// <returns></returns>
        public BELicencias ListaLicenciaMaestraxLicid(string owner, decimal LIC_ID)
        {
            BELicencias entidad = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTA_LIC_MAESTRA_X_LIC_HIJA");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);

            using (IDataReader dr = (oDataBase.ExecuteReader(oDbCommand)))
            {
                if (dr.Read())
                {
                    entidad = new BELicencias();

                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_MASTER")))
                        entidad.LIC_MASTER = dr.GetDecimal(dr.GetOrdinal("LIC_MASTER"));

                }
            }

            return entidad;
        }
        /// <summary>
        /// OBTIENE LA LICENCIA Y VALIDA SI PERTENECE A LOCALES PERMANENTES
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="LICID"> ID DE LA LICENCIA</param>
        /// <returns></returns>
        public int ValidaModalidadLicencia(string owner, decimal LICID)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_VALIDA_GRUPO_MODALIDAD_X_LIC");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LICID);

            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        #endregion

        #region Descuentos
        public List<BELicencias> ListarLicenciasxCodigoSocio(string owner, decimal bpsid)
        {
            List<BELicencias> lista = new List<BELicencias>();
            BELicencias entidad = null;

            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_LICENCIAS_X_CODIGO_SOCIO");
            db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BPSID", DbType.Decimal, bpsid);

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    entidad = new BELicencias();
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));

                    lista.Add(entidad);
                }
            }


            return lista;
        }


        #endregion

        #region Megaconciertos
        public decimal ObtienePrimeraFacturaAutorizacion(string owner, decimal lic_id)
        {
            decimal monto = 0;
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_FACTURA_X_LIC");
            db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, lic_id);

            monto = Convert.ToDecimal(db.ExecuteScalar(oDbCommand));
            return monto;
        }

        public decimal ObtienePrimeraFacturaCandesPlanilla(string owner, decimal lic_id, decimal mes, decimal anio)
        {
            decimal monto = 0;
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_MONTO_FACT_PL");
            db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, lic_id);
            db.AddInParameter(oDbCommand, "@lic_month", DbType.Decimal, mes);
            db.AddInParameter(oDbCommand, "@lic_year", DbType.Decimal, anio);

            monto = Convert.ToDecimal(db.ExecuteScalar(oDbCommand));
            return monto;
        }

        public string ObtieneSerieNumFacturaCandenaPlanilla(string owner, decimal lic_id, decimal mes, decimal anio)
        {
            string numfact;
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_SERI_N_FACT_");
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, lic_id);
            db.AddInParameter(oDbCommand, "@LIC_MONTH", DbType.Decimal, mes);
            db.AddInParameter(oDbCommand, "@LIC_YEAR", DbType.Decimal, anio);

            numfact = Convert.ToString(db.ExecuteScalar(oDbCommand));
            return numfact;
        }

        public string ObtieneSerieNumFacturaEspectLic(decimal lic_id)
        {
            string numfact;
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_NUMSERIE_FACT_PL");
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, lic_id);

            numfact = Convert.ToString(db.ExecuteScalar(oDbCommand));
            return numfact;
        }

        #endregion

        #region EMISION MENSUAL
        public List<BELicencias> ListarLicenciaNoAlDiaEmisionMensual(string xml, string owner)
        {
            List<BELicencias> lista = null;
            BELicencias entidad = new BELicencias();

            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_VALIDA_LICENCIA_EMISION_MENSUAL");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@lstxml", DbType.String, xml);

            using (IDataReader dr = (oDataBase.ExecuteReader(oDbCommand)))
            {
                lista = new List<BELicencias>();
                while (dr.Read())
                {
                    entidad = new BELicencias();
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));

                    lista.Add(entidad);

                }

            }
            return lista;
        }

        #endregion

        #region FACTURACION_MASIVA
        //public int ActualizarMontoLirics(decimal idLicencia, decimal MontoLirics)
        //{
        //    Database db = new DatabaseProviderFactory().Create("conexion");
        //    DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_LICENCIA_PRECIO_LIRICS");
        //    db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLicencia);
        //    db.AddInParameter(oDbCommand, "@MONTO_LIRICS", DbType.Decimal, MontoLirics);
        //    int r = db.ExecuteNonQuery(oDbCommand);
        //    return r;
        //}

        public int ActualizarMontoLirics(string xml)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            int result = 0;
            try
            {
                //using (DbCommand cm = db.GetStoredProcCommand("SGRDASU_RECIBO_DETALLE_BALANCE_XML"))
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASU_LICENCIA_PRECIO_LIRICS_XML"))
                {
                    db.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    result = db.ExecuteNonQuery(cm);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }


        public int ValidaLicenciaPlanificacionAutorizacion(decimal LIC_ID, int ACCION, decimal LIC_PL_ID)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            int resp = 0;

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_VALIDAR_PLANIFICACION");
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
            db.AddInParameter(oDbCommand, "@ACCION", DbType.Decimal, ACCION);
            db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, LIC_PL_ID);

            resp = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return resp;

        }

        #endregion
        #region Mandar al Historico
        public int EnviarAlHistorico(decimal LIC_ID, decimal BPS_ID, string LOG_USER)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            int resp = 0;

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_MANDAR_AL_HISTORICO");
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, BPS_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, LOG_USER);
            oDbCommand.CommandTimeout = 4000;
            resp = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return resp;

        }
        #endregion


        #region TRASLADO DE LICENCIAS 
        public int ActualizaLicenciasDivision(string xmllist, string xmllistAgente, decimal div_id, string owner, string log_user_modifi)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_LICENCIAS_DIVISION");
            db.AddInParameter(oDbCommand, "@xmlLst", DbType.String, xmllist);
            db.AddInParameter(oDbCommand, "@xmlAgenLst", DbType.String, xmllistAgente);
            db.AddInParameter(oDbCommand, "@DIV_ID", DbType.Decimal, div_id);
            db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LOG_USER_MODIFI", DbType.String, log_user_modifi);
            //resp = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }
        #endregion

        #region ACTUALIA CADENA LICENCIA

        public int ActualizaLicenciaCadena(string owner, decimal LIC_ID, decimal LIC_MASTER, string usuario)
        {
            int r = 0;
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTUALIZA_LICENCIA_CADENA");
                db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
                db.AddInParameter(oDbCommand, "@LIC_MASTER", DbType.Decimal, LIC_MASTER);
                db.AddInParameter(oDbCommand, "@USUARIO", DbType.String, usuario);

                r = db.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
                return r;
            }

            return r;
        }
        #endregion


        #region ACTUALIZAR MONTO BRUTO - DESC- NET 
        public int ActualizaLicenciaMontos(decimal LIC_ID, decimal BRUTO, decimal DESC, decimal NET, decimal DESC_REDOND)
        {
            int r = 0;
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_LICENCIA_MONTOS");
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
                db.AddInParameter(oDbCommand, "@BRUTO", DbType.Decimal, BRUTO);
                db.AddInParameter(oDbCommand, "@NET", DbType.Decimal, NET);
                db.AddInParameter(oDbCommand, "@DESC", DbType.Decimal, DESC);
                db.AddInParameter(oDbCommand, "@DESC_REDOND", DbType.Decimal, DESC_REDOND);

                r = db.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
                return r;
            }

            return r;
        }
        #endregion

        #region Validar Usuario Moroso

        public int ValidarUsuarioMoroso(decimal BPS_ID)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTIENE_SOCIO_MOROSO");
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, BPS_ID);

            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;

        }

        public int ValidaSocioTelefCorreo(decimal BPS_ID,decimal LIC_ID )
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_VALIDA_SOCIO");
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, BPS_ID);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);

            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;

        }
        #endregion

        public int ValidaLicenciaLocalRequerimiento(decimal CodigoLicencia, int CodigoRequerimiento)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTIENE_VALIDACION_LIC_LOCAL_REQ");
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, CodigoLicencia);
            db.AddInParameter(oDbCommand, "@TIPO_REQ", DbType.Int32, CodigoRequerimiento);

            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;

        }

        public List<BELicenciaTipoInactivacion> ListarTipoInactivacionLicencia()
        {
            List<BELicenciaTipoInactivacion> lista = new List<BELicenciaTipoInactivacion>();
            BELicenciaTipoInactivacion entidad = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTA_TIPO_INACTIVACIONES_LICENCIA");

            using (IDataReader dr = (oDataBase.ExecuteReader(oDbCommand)))
            {
                while (dr.Read())
                {
                    entidad = new BELicenciaTipoInactivacion();

                    if (!dr.IsDBNull(dr.GetOrdinal("VALUE")))
                        entidad.Valor = dr.GetString(dr.GetOrdinal("VALUE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("VDESC")))
                        entidad.Texto = dr.GetString(dr.GetOrdinal("VDESC"));

                    lista.Add(entidad);
                }
            }

            return lista;
        }

        public int ValidarFacturacion(decimal LIC_ID, decimal OFF_ID)
        {

            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("ValidarFacturacion_X_MODALIDAD");
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, OFF_ID);
            db.AddOutParameter(oDbCommand, "@RESULT", DbType.Int32, 0);
            int n = db.ExecuteNonQuery(oDbCommand);
            int result = Convert.ToInt32(db.GetParameterValue(oDbCommand, "@RESULT"));

            return result;
        }
    }
}




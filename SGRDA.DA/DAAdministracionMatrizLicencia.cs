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
    public class DAAdministracionMatrizLicencia
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");



        public List<BEAdministracionMatrizLicencia> listarLicencia(int skip, int take, int page, int pageSize, decimal LIC_ID, decimal BPS_ID, string RAZ_SOC, string NUM_IDE, string NOM_SOC, string APE_SOC, string MAT_SOC, string EST_NAM, string MOG_ID, int CON_FEC, string FEC_INI, string FEC_FIN, decimal DIV_ID, decimal DEP_ID, decimal PROV_ID, decimal DIST_ID, decimal OFF_ID, int ESTADO_LIC, int ESTADO_LIC_FACT,int ESTADO_PL_BLOQ,decimal CODIGO_AGENTE,int OPCION,string FEC_INI_BUS,string FEC_FIN_BUS)
        {
            List<BEAdministracionMatrizLicencia> lista = new List<BEAdministracionMatrizLicencia>();
            BEAdministracionMatrizLicencia entidad = null;


            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_MATRIZ_LICENCIA_X_OFICINA");

                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, BPS_ID);
                db.AddInParameter(oDbCommand, "@RAZ_SOC", DbType.String, RAZ_SOC);
                db.AddInParameter(oDbCommand, "@NUM_IDE", DbType.String, NUM_IDE);
                db.AddInParameter(oDbCommand, "@NOM_SOC", DbType.String, NOM_SOC);
                db.AddInParameter(oDbCommand, "@APE_SOC", DbType.String, APE_SOC);
                db.AddInParameter(oDbCommand, "@MAT_SOC", DbType.String, MAT_SOC);
                db.AddInParameter(oDbCommand, "@EST_NAM", DbType.String, EST_NAM);
                db.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, MOG_ID);
                db.AddInParameter(oDbCommand, "@CON_FEC", DbType.Int32, CON_FEC);
                db.AddInParameter(oDbCommand, "@FEC_INI", DbType.String, FEC_INI);
                db.AddInParameter(oDbCommand, "@FEC_FIN", DbType.String, FEC_FIN);
                db.AddInParameter(oDbCommand, "@DIV_ID", DbType.Decimal, DIV_ID);
                db.AddInParameter(oDbCommand, "@DEP_ID", DbType.Decimal, DEP_ID);
                db.AddInParameter(oDbCommand, "@PROV_ID", DbType.Decimal, PROV_ID);
                db.AddInParameter(oDbCommand, "@DIST_ID", DbType.Decimal, DIST_ID);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, OFF_ID);
                db.AddInParameter(oDbCommand, "@ESTADO_LI", DbType.Int32, ESTADO_LIC);
                db.AddInParameter(oDbCommand, "@ESTADO_LIC_FACT", DbType.Int32, ESTADO_LIC_FACT);
                db.AddInParameter(oDbCommand, "@ESTADO_PL_BLOQ", DbType.String, ESTADO_PL_BLOQ);
                db.AddInParameter(oDbCommand, "@CODIGO_AGENTE", DbType.Decimal, CODIGO_AGENTE);
                db.AddInParameter(oDbCommand, "@OPCION", DbType.Int32, OPCION);
                db.AddInParameter(oDbCommand, "@FEC_INI_BUS", DbType.String, FEC_INI_BUS);
                db.AddInParameter(oDbCommand, "@FEC_FIN_BUS", DbType.String, FEC_FIN_BUS);
                

                db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, page);
                db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, pageSize);
                oDbCommand.CommandTimeout = 200;


                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionMatrizLicencia();

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("LIC_NAME")))
                        //    entidad.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            entidad.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NUMERO_DOCUMENTO")))
                            entidad.NUMERO_DOCUMENTO = dr.GetString(dr.GetOrdinal("NUMERO_DOCUMENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                            entidad.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DIRECCION")))
                            entidad.DIRECCION = dr.GetString(dr.GetOrdinal("DIRECCION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO")))
                            entidad.UBIGEO = dr.GetString(dr.GetOrdinal("UBIGEO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MODALIDAD")))
                            entidad.MODALIDAD = dr.GetString(dr.GetOrdinal("MODALIDAD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ULT_PER_FACT")))
                            entidad.ULT_PER_FACT = dr.GetString(dr.GetOrdinal("ULT_PER_FACT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO")))
                            entidad.MONTO = dr.GetDecimal(dr.GetOrdinal("MONTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                            entidad.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PER_NO_FACT")))
                            entidad.PER_NO_FACT = dr.GetString(dr.GetOrdinal("PER_NO_FACT"));
                        
                        lista.Add(entidad);

                    }
                }
            }catch(Exception EX)
            {
                return null;
            }
                

            return lista;
        }
        

        public List<BEAdministracionMatrizLicencia> listaHuecos(int skip, int take, int page, int pageSize, decimal LIC_ID, decimal BPS_ID, string RAZ_SOC, string NUM_IDE, string NOM_SOC, string APE_SOC, string MAT_SOC, string EST_NAM, string MOG_ID, int CON_FEC, string FEC_INI, string FEC_FIN, decimal DIV_ID, decimal DEP_ID, decimal PROV_ID, decimal DIST_ID, decimal OFF_ID, int ESTADO_LIC, int ESTADO_LIC_FACT, int ESTADO_PL_BLOQ, decimal CODIGO_AGENTE, int OPCION, string FEC_INI_BUS, string FEC_FIN_BUS)
        {
            List<BEAdministracionMatrizLicencia> lista = new List<BEAdministracionMatrizLicencia>();
            BEAdministracionMatrizLicencia entidad = null;


            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_MATRIZ_LICENCIA_HUECO_X_OFICINA");

                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, BPS_ID);
                db.AddInParameter(oDbCommand, "@RAZ_SOC", DbType.String, RAZ_SOC);
                db.AddInParameter(oDbCommand, "@NUM_IDE", DbType.String, NUM_IDE);
                db.AddInParameter(oDbCommand, "@NOM_SOC", DbType.String, NOM_SOC);
                db.AddInParameter(oDbCommand, "@APE_SOC", DbType.String, APE_SOC);
                db.AddInParameter(oDbCommand, "@MAT_SOC", DbType.String, MAT_SOC);
                db.AddInParameter(oDbCommand, "@EST_NAM", DbType.String, EST_NAM);
                db.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, MOG_ID);
                db.AddInParameter(oDbCommand, "@CON_FEC", DbType.Int32, CON_FEC);
                db.AddInParameter(oDbCommand, "@FEC_INI", DbType.String, FEC_INI);
                db.AddInParameter(oDbCommand, "@FEC_FIN", DbType.String, FEC_FIN);
                db.AddInParameter(oDbCommand, "@DIV_ID", DbType.Decimal, DIV_ID);
                db.AddInParameter(oDbCommand, "@DEP_ID", DbType.Decimal, DEP_ID);
                db.AddInParameter(oDbCommand, "@PROV_ID", DbType.Decimal, PROV_ID);
                db.AddInParameter(oDbCommand, "@DIST_ID", DbType.Decimal, DIST_ID);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, OFF_ID);
                db.AddInParameter(oDbCommand, "@ESTADO_LI", DbType.Int32, ESTADO_LIC);
                db.AddInParameter(oDbCommand, "@ESTADO_LIC_FACT", DbType.Int32, ESTADO_LIC_FACT);
                db.AddInParameter(oDbCommand, "@ESTADO_PL_BLOQ", DbType.String, ESTADO_PL_BLOQ);
                db.AddInParameter(oDbCommand, "@CODIGO_AGENTE", DbType.Decimal, CODIGO_AGENTE);
                db.AddInParameter(oDbCommand, "@OPCION", DbType.Int32, OPCION);
                db.AddInParameter(oDbCommand, "@FEC_INI_BUS", DbType.String, FEC_INI_BUS);
                db.AddInParameter(oDbCommand, "@FEC_FIN_BUS", DbType.String, FEC_FIN_BUS);


                db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, page);
                db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, pageSize);
                oDbCommand.CommandTimeout = 200;


                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionMatrizLicencia();

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            entidad.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NUMERO_DOCUMENTO")))
                            entidad.NUMERO_DOCUMENTO = dr.GetString(dr.GetOrdinal("NUMERO_DOCUMENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                            entidad.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DIRECCION")))
                            entidad.DIRECCION = dr.GetString(dr.GetOrdinal("DIRECCION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO")))
                            entidad.UBIGEO = dr.GetString(dr.GetOrdinal("UBIGEO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MODALIDAD")))
                            entidad.MODALIDAD = dr.GetString(dr.GetOrdinal("MODALIDAD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ULT_PER_FACT")))
                            entidad.ULT_PER_FACT = dr.GetString(dr.GetOrdinal("ULT_PER_FACT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO")))
                            entidad.MONTO = dr.GetDecimal(dr.GetOrdinal("MONTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                            entidad.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PER_NO_FACT")))
                            entidad.PER_NO_FACT = dr.GetString(dr.GetOrdinal("PER_NO_FACT"));

                        lista.Add(entidad);

                    }
                }
            }
            catch (Exception EX)
            {
                return null;
            }


            return lista;
        }


        public List<BEAdministracionMatrizLicencia> listaLicenciasValidacionMensual(int skip, int take, int page, int pageSize, decimal LIC_ID, decimal BPS_ID, string RAZ_SOC, string NUM_IDE, string NOM_SOC, string APE_SOC, string MAT_SOC, string EST_NAM, string MOG_ID, int CON_FEC, string FEC_INI, string FEC_FIN, decimal DIV_ID, decimal DEP_ID, decimal PROV_ID, decimal DIST_ID, decimal OFF_ID, int ESTADO_LIC, int ESTADO_LIC_FACT, int ESTADO_PL_BLOQ, decimal CODIGO_AGENTE, int OPCION, string FEC_INI_BUS, string FEC_FIN_BUS)
        {
            List<BEAdministracionMatrizLicencia> lista = new List<BEAdministracionMatrizLicencia>();
            BEAdministracionMatrizLicencia entidad = null;


            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_MATRIZ_LICENCIA_VALIDACION");

                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, BPS_ID);
                db.AddInParameter(oDbCommand, "@RAZ_SOC", DbType.String, RAZ_SOC);
                db.AddInParameter(oDbCommand, "@NUM_IDE", DbType.String, NUM_IDE);
                db.AddInParameter(oDbCommand, "@NOM_SOC", DbType.String, NOM_SOC);
                db.AddInParameter(oDbCommand, "@APE_SOC", DbType.String, APE_SOC);
                db.AddInParameter(oDbCommand, "@MAT_SOC", DbType.String, MAT_SOC);
                db.AddInParameter(oDbCommand, "@EST_NAM", DbType.String, EST_NAM);
                db.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, MOG_ID);
                db.AddInParameter(oDbCommand, "@CON_FEC", DbType.Int32, CON_FEC);
                db.AddInParameter(oDbCommand, "@FEC_INI", DbType.String, FEC_INI);
                db.AddInParameter(oDbCommand, "@FEC_FIN", DbType.String, FEC_FIN);
                db.AddInParameter(oDbCommand, "@DIV_ID", DbType.Decimal, DIV_ID);
                db.AddInParameter(oDbCommand, "@DEP_ID", DbType.Decimal, DEP_ID);
                db.AddInParameter(oDbCommand, "@PROV_ID", DbType.Decimal, PROV_ID);
                db.AddInParameter(oDbCommand, "@DIST_ID", DbType.Decimal, DIST_ID);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, OFF_ID);
                db.AddInParameter(oDbCommand, "@ESTADO_LI", DbType.Int32, ESTADO_LIC);
                db.AddInParameter(oDbCommand, "@ESTADO_LIC_FACT", DbType.Int32, ESTADO_LIC_FACT);
                db.AddInParameter(oDbCommand, "@ESTADO_PL_BLOQ", DbType.String, ESTADO_PL_BLOQ);
                db.AddInParameter(oDbCommand, "@CODIGO_AGENTE", DbType.Decimal, CODIGO_AGENTE);
                db.AddInParameter(oDbCommand, "@OPCION", DbType.Int32, OPCION);
                db.AddInParameter(oDbCommand, "@FEC_INI_BUS", DbType.String, FEC_INI_BUS);
                db.AddInParameter(oDbCommand, "@FEC_FIN_BUS", DbType.String, FEC_FIN_BUS);


                db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, page);
                db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, pageSize);
                oDbCommand.CommandTimeout = 60;


                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionMatrizLicencia();

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            entidad.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NUMERO_DOCUMENTO")))
                            entidad.NUMERO_DOCUMENTO = dr.GetString(dr.GetOrdinal("NUMERO_DOCUMENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                            entidad.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DIRECCION")))
                            entidad.DIRECCION = dr.GetString(dr.GetOrdinal("DIRECCION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO")))
                            entidad.UBIGEO = dr.GetString(dr.GetOrdinal("UBIGEO"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("MODALIDAD")))
                        //    entidad.MODALIDAD = dr.GetString(dr.GetOrdinal("MODALIDAD"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("ULT_PER_FACT")))
                        //    entidad.ULT_PER_FACT = dr.GetString(dr.GetOrdinal("ULT_PER_FACT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO")))
                            entidad.MONTO = dr.GetDecimal(dr.GetOrdinal("MONTO"));
                        entidad.MODALIDAD = "";
                        entidad.ULT_PER_FACT = "";
                        entidad.PER_NO_FACT = "";
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                            entidad.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("PER_NO_FACT")))
                        //    entidad.PER_NO_FACT = dr.GetString(dr.GetOrdinal("PER_NO_FACT"));

                        lista.Add(entidad);

                    }
                }
            }
            catch (Exception EX)
            {
                return null;
            }


            return lista;
        }

        public List<BEAdministracionMatrizLicencia> listaLicenciasValidacionMensualPaso(int skip, int take, int page, int pageSize, decimal LIC_ID, decimal BPS_ID, string RAZ_SOC, string NUM_IDE, string NOM_SOC, string APE_SOC, string MAT_SOC, string EST_NAM, string MOG_ID, int CON_FEC, string FEC_INI, string FEC_FIN, decimal DIV_ID, decimal DEP_ID, decimal PROV_ID, decimal DIST_ID, decimal OFF_ID, int ESTADO_LIC, int ESTADO_LIC_FACT, int ESTADO_PL_BLOQ, decimal CODIGO_AGENTE, int OPCION, string FEC_INI_BUS, string FEC_FIN_BUS)
        {
            List<BEAdministracionMatrizLicencia> lista = new List<BEAdministracionMatrizLicencia>();
            BEAdministracionMatrizLicencia entidad = null;


            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_MATRIZ_LICENCIA_VALIDACION_PASO");

                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, BPS_ID);
                db.AddInParameter(oDbCommand, "@RAZ_SOC", DbType.String, RAZ_SOC);
                db.AddInParameter(oDbCommand, "@NUM_IDE", DbType.String, NUM_IDE);
                db.AddInParameter(oDbCommand, "@NOM_SOC", DbType.String, NOM_SOC);
                db.AddInParameter(oDbCommand, "@APE_SOC", DbType.String, APE_SOC);
                db.AddInParameter(oDbCommand, "@MAT_SOC", DbType.String, MAT_SOC);
                db.AddInParameter(oDbCommand, "@EST_NAM", DbType.String, EST_NAM);
                db.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, MOG_ID);
                db.AddInParameter(oDbCommand, "@CON_FEC", DbType.Int32, CON_FEC);
                db.AddInParameter(oDbCommand, "@FEC_INI", DbType.String, FEC_INI);
                db.AddInParameter(oDbCommand, "@FEC_FIN", DbType.String, FEC_FIN);
                db.AddInParameter(oDbCommand, "@DIV_ID", DbType.Decimal, DIV_ID);
                db.AddInParameter(oDbCommand, "@DEP_ID", DbType.Decimal, DEP_ID);
                db.AddInParameter(oDbCommand, "@PROV_ID", DbType.Decimal, PROV_ID);
                db.AddInParameter(oDbCommand, "@DIST_ID", DbType.Decimal, DIST_ID);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, OFF_ID);
                db.AddInParameter(oDbCommand, "@ESTADO_LI", DbType.Int32, ESTADO_LIC);
                db.AddInParameter(oDbCommand, "@ESTADO_LIC_FACT", DbType.Int32, ESTADO_LIC_FACT);
                db.AddInParameter(oDbCommand, "@ESTADO_PL_BLOQ", DbType.String, ESTADO_PL_BLOQ);
                db.AddInParameter(oDbCommand, "@CODIGO_AGENTE", DbType.Decimal, CODIGO_AGENTE);
                db.AddInParameter(oDbCommand, "@OPCION", DbType.Int32, OPCION);
                db.AddInParameter(oDbCommand, "@FEC_INI_BUS", DbType.String, FEC_INI_BUS);
                db.AddInParameter(oDbCommand, "@FEC_FIN_BUS", DbType.String, FEC_FIN_BUS);


                db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, page);
                db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, pageSize);
                oDbCommand.CommandTimeout = 60;


                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionMatrizLicencia();

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            entidad.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NUMERO_DOCUMENTO")))
                            entidad.NUMERO_DOCUMENTO = dr.GetString(dr.GetOrdinal("NUMERO_DOCUMENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                            entidad.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DIRECCION")))
                            entidad.DIRECCION = dr.GetString(dr.GetOrdinal("DIRECCION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO")))
                            entidad.UBIGEO = dr.GetString(dr.GetOrdinal("UBIGEO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO")))
                            entidad.MONTO = dr.GetDecimal(dr.GetOrdinal("MONTO"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("MODALIDAD")))
                        //    entidad.MODALIDAD = dr.GetString(dr.GetOrdinal("MODALIDAD"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("ULT_PER_FACT")))
                        //    entidad.ULT_PER_FACT = dr.GetString(dr.GetOrdinal("ULT_PER_FACT"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("MONTO")))
                        //    entidad.MONTO = dr.GetDecimal(dr.GetOrdinal("MONTO"));
                        entidad.MODALIDAD = "";
                        entidad.ULT_PER_FACT = "";
                        entidad.PER_NO_FACT = "";
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                            entidad.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("PER_NO_FACT")))
                        //    entidad.PER_NO_FACT = dr.GetString(dr.GetOrdinal("PER_NO_FACT"));

                        lista.Add(entidad);

                    }
                }
            }
            catch (Exception EX)
            {
                return null;
            }


            return lista;
        }


        public int  ObtieneValidacionOficinaPadre( decimal CodigoOficina)
        {
            var Resp = 0; // no paso
                          // 1 = si paso
            try
            {
                DbCommand OdbCommand = db.GetStoredProcCommand("SGRDASS_VALIDA_OFICINA_PADRE");
                db.AddInParameter(OdbCommand, "@OFF_ID", DbType.Decimal, CodigoOficina);


                Resp =Convert.ToInt32(  db.ExecuteScalar(OdbCommand));



            }catch(Exception ex)
            {
                Resp = 2; // error 
            }

            return Resp;
        }

    }
}

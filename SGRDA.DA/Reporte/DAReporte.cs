using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using SGRDA.Entities.Reporte;
using SGRDA.Entities;
using SGRDA.Entities.WorkFlow;

namespace SGRDA.DA.Reporte
{
    public class DAReporte
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public BELicencia ObtenerDatosLicencia(string owner, decimal idLic)
        {
            BELicencia item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_LICENCIA_RPT"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BELicencia();
                        if (!dr.IsDBNull(dr.GetOrdinal("OWNER")))
                            item.Owner = dr.GetString(dr.GetOrdinal("OWNER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("IdLicencia")))
                            item.IdLicencia = dr.GetDecimal(dr.GetOrdinal("IdLicencia"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NombreLicencia")))
                            item.NombreLicencia = dr.GetString(dr.GetOrdinal("NombreLicencia"));
                        if (!dr.IsDBNull(dr.GetOrdinal("RazonSocial")))
                            item.RazonSocial = dr.GetString(dr.GetOrdinal("RazonSocial"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DocRazonSocial")))
                            item.DocRazonSocial = dr.GetString(dr.GetOrdinal("DocRazonSocial"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NumRazonSocial")))
                            item.NumRazonSocial = dr.GetString(dr.GetOrdinal("NumRazonSocial"));
                        if (!dr.IsDBNull(dr.GetOrdinal("GiroLocal")))
                            item.GiroLocal = dr.GetString(dr.GetOrdinal("GiroLocal"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NombreLocal")))
                            item.NombreLocal = dr.GetString(dr.GetOrdinal("NombreLocal"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DireccionLocal")))
                            item.DireccionLocal = dr.GetString(dr.GetOrdinal("DireccionLocal"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Representante")))
                            item.Representante = dr.GetString(dr.GetOrdinal("Representante"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DireccionRepresentante")))
                            item.DireccionRepresentante = dr.GetString(dr.GetOrdinal("DireccionRepresentante"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DocRepresentante")))
                            item.DocRepresentante = dr.GetString(dr.GetOrdinal("DocRepresentante"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NumRepresentante")))
                            item.NumRepresentante = dr.GetString(dr.GetOrdinal("NumRepresentante"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Tis_n_Representante")))
                            item.Tis_n_Representante = dr.GetDecimal(dr.GetOrdinal("Tis_n_Representante"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Geo_id_Representante")))
                            item.Geo_id_Representante = dr.GetDecimal(dr.GetOrdinal("Geo_id_Representante"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Tis_n_Establecimiento")))
                            item.Tis_n_Establecimiento = dr.GetDecimal(dr.GetOrdinal("Tis_n_Establecimiento"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Geo_id_Establecimiento")))
                            item.Geo_id_Establecimiento = dr.GetDecimal(dr.GetOrdinal("Geo_id_Establecimiento"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            item.FechaCreacionLicencia = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DireccionRazonSocial")))
                            item.DireccionRazonSocial = dr.GetString(dr.GetOrdinal("DireccionRazonSocial"));  
                    }
                }
            }
            return item;
        }
        
        public BELicencia ObtenerDatosDireccionCobranza(string owner, decimal idLic)
        {
            BELicencia item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_GEO_ID_COBRANZA_RPT"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BELicencia();
                        if (!dr.IsDBNull(dr.GetOrdinal("OWNER")))
                            item.Owner = dr.GetString(dr.GetOrdinal("OWNER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("IdLicencia")))
                            item.IdLicencia = dr.GetDecimal(dr.GetOrdinal("IdLicencia"));
                        if (!dr.IsDBNull(dr.GetOrdinal("IdRepresentante")))
                            item.IdRepresentante = dr.GetDecimal(dr.GetOrdinal("IdRepresentante"));
                        if (!dr.IsDBNull(dr.GetOrdinal("IdEstablecimiento")))
                            item.IdEstablecimiento = dr.GetDecimal(dr.GetOrdinal("IdEstablecimiento"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DireccionCobranza")))
                            item.DireccionCobranza = dr.GetString(dr.GetOrdinal("DireccionCobranza"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Tis_n_DireccionCobranza")))
                            item.Tis_n_DirCobranza = dr.GetDecimal(dr.GetOrdinal("Tis_n_DireccionCobranza"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Geo_id_DireccionCobranza")))
                            item.Geo_id_DirCobranza = dr.GetDecimal(dr.GetOrdinal("Geo_id_DireccionCobranza"));
                    }
                }
            }
            return item;
        }

        public BEUbigeoRpt ObtenerUbigeo(string owner, string Tins, string dadvid)
        {
            BEUbigeoRpt item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_UBIGEO_RPT"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@TIS_N", DbType.Decimal, Convert.ToDecimal(Tins));
                oDataBase.AddInParameter(cm, "@DADV_ID", DbType.Decimal, Convert.ToDecimal(dadvid));

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BEUbigeoRpt();
                        if (!dr.IsDBNull(dr.GetOrdinal("Departamento")))
                            item.Departamento = dr.GetString(dr.GetOrdinal("Departamento"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Provincia")))
                            item.Provincia = dr.GetString(dr.GetOrdinal("Provincia"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Distrito")))
                            item.Distrito = dr.GetString(dr.GetOrdinal("Distrito"));
                    }
                }
            }
            return item;
        }

        public BELicencia ValorUnidadMusical(string owner)
        {
            BELicencia item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_VUM_TAR"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BELicencia();
                        if (!dr.IsDBNull(dr.GetOrdinal("Mes")))
                            item.Mes = dr.GetString(dr.GetOrdinal("Mes"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Anio")))
                            item.Anio = dr.GetString(dr.GetOrdinal("Anio"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Valor")))
                            item.Valor = Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("Valor")));

                    }
                }
            }
            return item;
        }

        public List<BELicencia> NivelIncidenciaMusical(string owner)
        {
            BELicencia item = null;
            var lista = new List<BELicencia>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_INCIDENCIA_RPT"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BELicencia();
                        if (!dr.IsDBNull(dr.GetOrdinal("NivelIncidencia")))
                            item.NivelIncidencia = dr.GetString(dr.GetOrdinal("NivelIncidencia"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }

        public string FechaCartaInformativa(string owner, decimal idLicencia)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_FECHA_CARTA_INF");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.String, idLicencia);
            string r = Convert.ToString(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public string FechaCartaReiterativa(string owner, decimal idLicencia)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_FECHA_CARTA_REI");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.String, idLicencia);
            string r = Convert.ToString(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public string FechaCartaRequerimientoAutorizacion(string owner, decimal idLicencia)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_REQUEMIENTO_AUT_FECHA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.String, idLicencia);
            string r = Convert.ToString(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public string FechaCartaVerificaionUsoAutorizacionObrasMusicales(string owner, decimal idLicencia)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_VERIFICACION_USO_AUT_FECHA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.String, idLicencia);
            string r = Convert.ToString(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public string FechaActualShort()
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_FECHA_ACTUAL_SHORT");
            string r = Convert.ToString(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public List<BEModalidadIncidencia> ListarTipo(string owner)
        {
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_INCIDENCIA_OBRA_TIPO_REP");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);

            var lista = new List<BEModalidadIncidencia>();
            BEModalidadIncidencia obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BEModalidadIncidencia();
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_IDESC")))
                        obs.MOD_IDESC = dr.GetString(dr.GetOrdinal("MOD_IDESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_PRC")))
                        obs.MOD_PRC = dr.GetDecimal(dr.GetOrdinal("MOD_PRC"));
                    lista.Add(obs);
                }
            }
            return lista;
        }

        public BEApoderadoLegal ObtenerDatosApoderadoLegal(string owner, decimal idLic)
        {
            BEApoderadoLegal item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_APODERADO_LEGAL"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BEApoderadoLegal();
                        if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                            item.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                            item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPTION")))
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS")))
                            item.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIS_N")))
                            item.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        if (!dr.IsDBNull(dr.GetOrdinal("GEO_ID")))
                            item.GEO_ID = dr.GetDecimal(dr.GetOrdinal("GEO_ID"));
                    }
                }
            }
            return item;
        }

        public BEEstablecimientos ObtenerDatosEstablecimiento(string owner, decimal idLic)
        {
            BEEstablecimientos item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_ESTABLECIMIENTO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BEEstablecimientos();
                        if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                            item.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS")))
                            item.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIPO")))
                            item.TIPO = dr.GetString(dr.GetOrdinal("TIPO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SUBTIPO")))
                            item.SUBTIPO = dr.GetString(dr.GetOrdinal("SUBTIPO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                            item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIS_N")))
                            item.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        if (!dr.IsDBNull(dr.GetOrdinal("GEO_ID")))
                            item.GEO_ID = dr.GetDecimal(dr.GetOrdinal("GEO_ID"));
                    }
                }
            }
            return item;
        }

        public BEUsuarioDerecho ObtenerUsuarioDerecho(string owner, decimal idLic)
        {
            BEUsuarioDerecho item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_USUARIO_DERECHO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BEUsuarioDerecho();
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                            item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                            item.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS")))
                            item.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIS_N")))
                            item.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        if (!dr.IsDBNull(dr.GetOrdinal("GEO_ID")))
                            item.GEO_ID = dr.GetDecimal(dr.GetOrdinal("GEO_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_PARTIDA")))
                            item.BPS_PARTIDA = dr.GetString(dr.GetOrdinal("BPS_PARTIDA"));
                    }
                }
            }
            return item;
        }

        public BEAgenteRecaudo ObtenerAgenteRecaudo(string owner, decimal idLic)
        {
            BEAgenteRecaudo item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_AGENTE_RECAUDO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BEAgenteRecaudo();
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                            item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                    }
                }
            }
            return item;
        }

        public BERepresentanteLegal ObtenerRepresentanteLegal(string owner, decimal idLic)
        {
            BERepresentanteLegal item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_REPRESENTANE_LEGAL"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BERepresentanteLegal();
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                            item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                            item.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS")))
                            item.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIS_N")))
                            item.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        if (!dr.IsDBNull(dr.GetOrdinal("GEO_ID")))
                            item.GEO_ID = dr.GetDecimal(dr.GetOrdinal("GEO_ID"));
                    }
                }
            }
            return item;
        }

        public BEUsuarioDerecho ObtenerPartidaZonaSedeUsuarioDerecho(string owner, decimal idLic)
        {
            BEUsuarioDerecho item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_PARTIDA_ZONA_SEDE"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BEUsuarioDerecho();
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_PARTIDA")))
                            item.BPS_PARTIDA = dr.GetString(dr.GetOrdinal("BPS_PARTIDA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ZONA")))
                            item.BPS_ZONA = dr.GetString(dr.GetOrdinal("BPS_ZONA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_SEDE")))
                            item.BPS_SEDE = dr.GetString(dr.GetOrdinal("BPS_SEDE"));
                    }
                }
            }
            return item;
        }

        public BEOficina ObtenerDatosOficina(string owner, decimal idLic)
        {
            BEOficina item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_OFICINA"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BEOficina();
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                            item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OFF_NAME")))
                            item.OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS")))
                            item.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIS_N")))
                            item.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        if (!dr.IsDBNull(dr.GetOrdinal("GEO_ID")))
                            item.GEO_ID = dr.GetDecimal(dr.GetOrdinal("GEO_ID"));
                    }
                }
            }
            return item;
        }

        public List<string> ObtenerArtistas(string owner, decimal idLic)
        {
            string item = string.Empty;
            var lista = new List<string>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_LISTAR_ARTISTAS"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.String, idLic);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(dr.GetOrdinal("NAME")))
                            item = dr.GetString(dr.GetOrdinal("NAME"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }

        public BEPlanilla ObtenerDatosPlanilla(string owner, decimal idLic, decimal idReport)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_DATOS_PLANILLA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLic);
            oDataBase.AddInParameter(oDbCommand, "@REPORT_ID", DbType.Decimal, idReport);
            
            BEPlanilla item = null;

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEPlanilla();
                    if (!dr.IsDBNull(dr.GetOrdinal("REPORT_NUMBER")))
                        item.REPORT_NUMBER = dr.GetDecimal(dr.GetOrdinal("REPORT_NUMBER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RUM")))
                        item.RUM = dr.GetString(dr.GetOrdinal("RUM"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_YEAR")))
                        item.LIC_YEAR = dr.GetDecimal(dr.GetOrdinal("LIC_YEAR"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_MONTH")))
                        item.LIC_MONTH = dr.GetDecimal(dr.GetOrdinal("LIC_MONTH"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SHOW_ID")))
                        item.SHOW = dr.GetDecimal(dr.GetOrdinal("SHOW_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_DEC")))
                        item.MODALIDAD = dr.GetString(dr.GetOrdinal("MOD_DEC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOG_DESC")))
                        item.GRUPO_MODALIDAD = dr.GetString(dr.GetOrdinal("MOG_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        item.LOG_DATE_CREAT = dr.GetString(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ARTISTA_DESC")))
                        item.ARTISTA_DESC = dr.GetString(dr.GetOrdinal("ARTISTA_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOG_ID")))
                        item.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_ID")))
                        item.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MES_EVENTO")))
                        item.MES_EVENTO = dr.GetString(dr.GetOrdinal("MES_EVENTO"));
                    
                }
                return item;
            }
        }

        public BEAutoridadPrincipal ObtenerAutoridadPrincipal(string owner, decimal idLic)
        {
            BEAutoridadPrincipal item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_AUTORIDAD_PRINCIPAL"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BEAutoridadPrincipal();
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                            item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                            item.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PHONE_NUMBER")))
                            item.PHONE_NUMBER = dr.GetString(dr.GetOrdinal("PHONE_NUMBER"));
                    }
                }
            }
            return item;
        }

        public BEParametro ObtenerParametro(string owner, decimal idLic, decimal varId)
        {
            BEParametro item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAS_PARAMETRO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);
                oDataBase.AddInParameter(cm, "@var_id", DbType.Decimal, varId);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BEParametro();
                        if (!dr.IsDBNull(dr.GetOrdinal("PAR_VALUE")))
                            item.PAR_VALUE = dr.GetString(dr.GetOrdinal("PAR_VALUE"));
                    }
                }
            }
            return item;
        }


        public List<BEPeriodoDeuda> ListaPeriodoDeuda(string owner, decimal idLic)
        {
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDAS_PERIODO_DEUDA");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@LIC_ID", DbType.Decimal, idLic);

            var lista = new List<BEPeriodoDeuda>();
            BEPeriodoDeuda item = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    item = new BEPeriodoDeuda();
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                        item.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_YEAR")))
                        item.LIC_YEAR = dr.GetDecimal(dr.GetOrdinal("LIC_YEAR"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_MONTH")))
                        item.LIC_MONTH = dr.GetDecimal(dr.GetOrdinal("LIC_MONTH"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_MONTH_NAME")))
                        item.LIC_MONTH_NAME = dr.GetString(dr.GetOrdinal("LIC_MONTH_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                        item.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public int ObtenerEstadoTrace(string owner, decimal idTrace)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_ESTADO_TRACES");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@WRKF_TID", DbType.Decimal, idTrace);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public decimal ObtenerPrerequisito(string owner, decimal idLicencia, decimal idTrace)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_OBTENER_PREREQUISITO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@WRKF_REF1", DbType.Decimal, idLicencia);
            oDataBase.AddInParameter(oDbCommand, "@WRKF_TID", DbType.Decimal, idTrace);
            decimal r = Convert.ToDecimal(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public WORKF_TRACES ObtenerFechaCarta(string owner, decimal idPrerequisito, decimal estado)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_OBTENER_FECHA_CARTA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@WRKF_AID", DbType.Decimal, idPrerequisito);
            oDataBase.AddInParameter(oDbCommand, "@WRKF_SID", DbType.Decimal, estado);
            WORKF_TRACES item = null;

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new WORKF_TRACES();
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_TID")))
                        item.WRKF_TID = dr.GetDecimal(dr.GetOrdinal("WRKF_TID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_ADATE")))
                        item.WRKF_ADATE = dr.GetDateTime(dr.GetOrdinal("WRKF_ADATE"));
                }
                return item;
            }
        }

        public decimal TotalDeudaFactura(string owner, decimal idLicencia)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_TOTAL_DEUDA_FACT");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLicencia);
            decimal r = Convert.ToDecimal(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public BEContacto ObtenerContacto(string owner, decimal idLicencia)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_CONTACTO_REPORT");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLicencia);

            BEContacto item = new BEContacto();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                        item.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                        item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TAXN_NAME")))
                        item.TAXN_NAME = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                        item.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ROL_DESC")))
                        item.ROL_DESC = dr.GetString(dr.GetOrdinal("ROL_DESC"));
                }
                return item;
            }
        }
        #region cadenas 

        public BELicenciaPlaneamiento obtenerperiodominimo(string owner, decimal idLicencia)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_PERIODO_MIN_LIC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLicencia);

            BELicenciaPlaneamiento item = new BELicenciaPlaneamiento();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_MONTH")))
                        item.LIC_MONTH = dr.GetDecimal(dr.GetOrdinal("LIC_MONTH"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_YEAR")))
                        item.LIC_YEAR = dr.GetDecimal(dr.GetOrdinal("LIC_YEAR"));

                }
                return item;
            }
        }
        public BELicenciaPlaneamiento obtenerperiodomaximo(string owner, decimal idLicencia)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_PERIODO_MAX_LIC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLicencia);

            BELicenciaPlaneamiento item = new BELicenciaPlaneamiento();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_MONTH")))
                        item.LIC_MONTH = dr.GetDecimal(dr.GetOrdinal("LIC_MONTH"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_YEAR")))
                        item.LIC_YEAR = dr.GetDecimal(dr.GetOrdinal("LIC_YEAR"));

                }
                return item;
            }
        }
        public List<BEAsociado> AsociadoXSocio(decimal idBps, string owner)
        {
            List<BEAsociado> parametros = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_ASOCIADO_BPS"))
            {
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idBps);
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {

                    BEAsociado ObjObs = null;
                    parametros = new List<BEAsociado>();
                    while (dr.Read())
                    {
                        ObjObs = new BEAsociado();

                        ObjObs.ROL_ID = dr.GetString(dr.GetOrdinal("ROL_ID"));
                        ObjObs.ROL_DESC = dr.GetString(dr.GetOrdinal("ROL_DESC"));
                        ObjObs.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        ObjObs.BPSA_ID = dr.GetDecimal(dr.GetOrdinal("BPSA_ID"));
                        ObjObs.SEQUENCE = dr.GetDecimal(dr.GetOrdinal("SEQUENCE"));

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
        #endregion


        public List<BEPlanilla> ObtieneFacturasxIdReporte(decimal idReporte, string owner)
        {
            List<BEPlanilla> parametros = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTIENE_FACTURAS_X_IDREPORTE"))
            {
                oDataBase.AddInParameter(cm, "@ID_REPORTE", DbType.Decimal, idReporte);
                //oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {

                    BEPlanilla ObjObs = null;
                    parametros = new List<BEPlanilla>();
                    while (dr.Read())
                    {
                        ObjObs = new BEPlanilla();

                        if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTO")))
                        {
                            ObjObs.DOCUMENTO = dr.GetString(dr.GetOrdinal("DOCUMENTO"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                        {
                            ObjObs.MONTO = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CANCEL")))
                        {
                            ObjObs.FECHA_CANCEL = dr.GetString(dr.GetOrdinal("FECHA_CANCEL"));
                        }
                        
                        parametros.Add(ObjObs);

                    }
                }
            }

            return parametros;
        }


        public BEPlanilla ObtieneDescripcionxModalidad(string MOG_ID, string owner)
        {
            BEPlanilla ObjObs = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTA_PLANILLA_MODALIDAD"))
            {
                oDataBase.AddInParameter(cm, "@MOG_ID", DbType.String, MOG_ID);
                //oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {


                    if(dr.Read())
                    {
                        ObjObs = new BEPlanilla();

                        if (!dr.IsDBNull(dr.GetOrdinal("VDESC")))
                        {
                            ObjObs.MODALIDAD = dr.GetString(dr.GetOrdinal("VDESC"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("VALUE")))
                        {
                            ObjObs.ARTISTA_DESC = dr.GetString(dr.GetOrdinal("VALUE"));
                        }

                    }
                }
            }

            return ObjObs;
        }

        /// <summary>
        /// obtiene la descripcion de la fila 1
        /// </summary>
        /// <param name="MOG_ID"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public BEPlanilla ObtieneDescripcionxModalidad2(string MOG_ID, decimal MOD_ID, string owner)
        {
            BEPlanilla ObjObs = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTA_DATOS_PLANILLA"))
            {
                oDataBase.AddInParameter(cm, "@MOG_ID", DbType.String, MOG_ID);
                oDataBase.AddInParameter(cm, "@MOD_ID", DbType.Decimal, MOD_ID);
                //oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {


                    if (dr.Read())
                    {
                        ObjObs = new BEPlanilla();

                        if (!dr.IsDBNull(dr.GetOrdinal("VDESC")))
                        {
                            ObjObs.MODALIDAD = dr.GetString(dr.GetOrdinal("VDESC"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("VALUE")))
                        {
                            ObjObs.ARTISTA_DESC = dr.GetString(dr.GetOrdinal("VALUE"));
                        }

                    }
                }
            }

            return ObjObs;
        }



        /// <summary>
        /// obtiene la descripcion de la fila 2
        /// </summary>
        /// <param name="MOG_ID"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public BEPlanilla ObtieneDescripcionxModalidad3(string MOG_ID,decimal MOD_ID, string owner)
        {
            BEPlanilla ObjObs = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTA_DATOS_PLANILLA_2"))
            {
                oDataBase.AddInParameter(cm, "@MOG_ID", DbType.String, MOG_ID);
                oDataBase.AddInParameter(cm, "@MOD_ID", DbType.Decimal, MOD_ID);
                //oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {

                   
                    if (dr.Read())
                    {
                        ObjObs = new BEPlanilla();

                        if (!dr.IsDBNull(dr.GetOrdinal("VDESC")))
                        {
                            ObjObs.MODALIDAD = dr.GetString(dr.GetOrdinal("VDESC"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("VALUE")))
                        {
                            ObjObs.ARTISTA_DESC = dr.GetString(dr.GetOrdinal("VALUE"));
                        }

                    }
                }
            }

            return ObjObs;
        }

        public string ObtieneDescripcionEstadoLicencia(decimal LIC_ID, string owner)
        {
            string estado= null;

            using (DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_ESTADO_LIC_AUTO")) 
            {
                oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                //oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);

                estado = Convert.ToString( oDataBase.ExecuteScalar(oDbCommand));
            }

            return estado;
        }

        public string ObtieneInfoxLicencia(decimal LIC_ID)
        {
            string info = null;

            using (DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTIENE_INF_QR_LICENCIA"))
            {
                oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);

                info = Convert.ToString(oDataBase.ExecuteScalar(oDbCommand));
            }
            return info;
        }

        #region  ObtieneMontoLicencias


        public decimal ObtieneMontoLicencias(decimal LIC_ID)
        {
            decimal monto = 0;

            using (DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTIENE_MONTO_LICENCIA"))
            {
                oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);

                monto = Convert.ToDecimal(oDataBase.ExecuteScalar(oDbCommand));
            }
            return monto;
        }


        public string ObtienAgenciasLicencia(decimal LIC_ID,int TIPO)
        {
            string TEXT = "";

            using (DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTA_AGENCIAS"))
            {
                oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
                oDataBase.AddInParameter(oDbCommand, "@TIPO", DbType.Int32, TIPO);

                TEXT = Convert.ToString(oDataBase.ExecuteScalar(oDbCommand));
            }
            return TEXT;
        }

        #endregion

    }
}

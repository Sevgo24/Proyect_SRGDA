using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common;
using SGRDA.Entities;
using System.Data.Common;

namespace SGRDA.DA
{
    public class DATrasladoAgentesRecaudo
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<SocioNegocio> BUSCAR_RECAUDADORES_X_NOMBRE(string Owner, string datos)
        {
            List<SocioNegocio> lst = new List<SocioNegocio>();
            SocioNegocio item = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("UPS_BUSCAR_RECAUDADORES_X_NOMBRE"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                    oDataBase.AddInParameter(cm, "@DATOS", DbType.String, datos);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new SocioNegocio();
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                            item.BPS_FIRST_NAME = dr.GetString(dr.GetOrdinal("BPS_FIRST_NAME"));
                            item.BPS_FATH_SURNAME = dr.GetString(dr.GetOrdinal("BPS_FATH_SURNAME"));
                            item.BPS_MOTH_SURNAME = dr.GetString(dr.GetOrdinal("BPS_MOTH_SURNAME"));
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

        public SocioNegocio BuscarAgenterecaudadorXtipodocumento(decimal idTipoDocumento, string nroDocumento)
        {
            SocioNegocio socio = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_AGENTERECAUDADOR_TIPODOC"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                    oDataBase.AddInParameter(cm, "@TAXT_ID", DbType.String, idTipoDocumento);
                    oDataBase.AddInParameter(cm, "@TAX_ID", DbType.String, nroDocumento);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        if (dr.Read())
                        {
                            socio = new SocioNegocio();
                            socio.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            socio.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return socio;
        }

        public BETrasladoAgentesRecaudo ObtenerOficinaActualAgente(string owner, decimal idAgente)
        {
            BETrasladoAgentesRecaudo TrasladoAgentesRecaudo = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_AGENTE_OFIC_ACTUAL"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@BPS_ID", DbType.String, idAgente);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        if (dr.Read())
                        {
                            TrasladoAgentesRecaudo = new BETrasladoAgentesRecaudo();
                            TrasladoAgentesRecaudo.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                            TrasladoAgentesRecaudo.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            TrasladoAgentesRecaudo.OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return TrasladoAgentesRecaudo;
        }

        public List<BETrasladoAgentesRecaudo> usp_Get_TrasladoAgentesRecaudoPage(string owner, decimal agente, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TRASLADO_AGENTE_PAGE");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, agente);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_TRASLADO_AGENTE_PAGE", owner, agente, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BETrasladoAgentesRecaudo>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BETrasladoAgentesRecaudo(reader, Convert.ToInt32(results)));
            }
            return lista;
        }
        public int InsertarB(string OWNER,decimal? OFF_ID, decimal BPS_ID, decimal? LEVEL_ID,string LOG_USER_CREAT)
        {
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_OFICINA_TRASLADO_RECAUDADOR");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, OWNER);
                oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, OFF_ID);
                oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, BPS_ID);
                oDataBase.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, LEVEL_ID);
                oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, LOG_USER_CREAT);
                int r = oDataBase.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int Insertar(BETrasladoAgentesRecaudo Traslado)
        {
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_OFICINA_TRASLADO_AGENTE");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, Traslado.OWNER);
                oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, Traslado.OFF_IDAux);
                oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, Traslado.BPS_ID);
                oDataBase.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, Traslado.LEVEL_ID);
                oDataBase.AddInParameter(oDbCommand, "@START", DbType.DateTime, Traslado.START);
                oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, Traslado.LOG_USER_CREAT);
                oDataBase.AddInParameter(oDbCommand, "@ENDS", DbType.DateTime, Traslado.ENDS);
                int r = oDataBase.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {
                return 0; 
            }
        }

        public int ActualizarOficina(BETrasladoAgentesRecaudo traslado)
        {
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_OFICINA_AGENTE");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, traslado.OWNER);
                oDataBase.AddInParameter(oDbCommand, "@OFF_IDactual", DbType.Decimal, traslado.OFF_ID);
                oDataBase.AddInParameter(oDbCommand, "@OFF_IDnuevo", DbType.Decimal, traslado.OFF_IDAux);
                oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, traslado.BPS_ID);
                oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, traslado.LOG_USER_UPDAT);
                int r = oDataBase.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {
                return 0; 
            }
        }

        public int ActualizarOficinaTraslado(BETrasladoAgentesRecaudo traslado)
        {
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_OFICINA_TRASLADO_AGENTE");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, traslado.OWNER);
                oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, traslado.BPS_ID);
                oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, traslado.LOG_USER_UPDAT);
                int r = oDataBase.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {
                return 0; 
            }
        }

        public BETrasladoAgentesRecaudo ValidarTrasladoOficinaAgente(string owner, decimal idOficina)
        {
            BETrasladoAgentesRecaudo TrasladoAgentesRecaudo = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_VALIDAR_TRASLADO_AGENTE"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@OFF_ID", DbType.Decimal, idOficina);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        if (dr.Read())
                        {
                            TrasladoAgentesRecaudo = new BETrasladoAgentesRecaudo();
                            TrasladoAgentesRecaudo.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return TrasladoAgentesRecaudo;
        }

        public BETrasladoAgentesRecaudo ObtenerDatosOficina(string owner, decimal idOficina, decimal idAgente)
        {
            BETrasladoAgentesRecaudo TrasladoAgentesRecaudo = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DATOS_OFICINA"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@OFF_ID", DbType.Decimal, idOficina);
                    oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idAgente);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        if (dr.Read())
                        {
                            TrasladoAgentesRecaudo = new BETrasladoAgentesRecaudo();
                            TrasladoAgentesRecaudo.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                            TrasladoAgentesRecaudo.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            TrasladoAgentesRecaudo.OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"));
                            TrasladoAgentesRecaudo.LEVEL_ID = dr.GetDecimal(dr.GetOrdinal("LEVEL_ID"));
                            TrasladoAgentesRecaudo.START = dr.GetDateTime(dr.GetOrdinal("START"));

                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                TrasladoAgentesRecaudo.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return TrasladoAgentesRecaudo;
        }

        public int Actualizar(BETrasladoAgentesRecaudo traslado)
        {
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_OFICINA_TRASLADO");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, traslado.OWNER);
                oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, traslado.OFF_ID);
                oDataBase.AddInParameter(oDbCommand, "@auxOFF_ID", DbType.Decimal, traslado.OFF_IDAux);
                oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, traslado.BPS_ID);
                oDataBase.AddInParameter(oDbCommand, "@START", DbType.DateTime, traslado.START);
                oDataBase.AddInParameter(oDbCommand, "@ENDS", DbType.DateTime, traslado.ENDS);
                oDataBase.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, traslado.LEVEL_ID);
                oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, traslado.LOG_USER_UPDAT);
                int r = oDataBase.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}

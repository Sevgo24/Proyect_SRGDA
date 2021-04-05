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
using SGRDA.Entities.FacturaElectronica;
using System.Data.Common;
using System.Data;
using System.Xml;

namespace SGRDA.DA
{
    public class DAFactura
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public bool InsertarBorradorXML(string xml, string owner)
        {
            bool exito = false;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASI_INVOICES_GRAL_BORRADOR"))
                {
                    oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    exito = oDataBase.ExecuteNonQuery(cm) > 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return exito;
        }

        public bool ActualizarEstadoDefinitivo(BEFactura en)
        {
            bool exito = false;
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_COMPROBANTE_MANUAL");
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, en.INV_ID);
            db.AddInParameter(oDbCommand, "@INV_NMR", DbType.Decimal, en.INV_NMR);
            db.AddInParameter(oDbCommand, "@INV_NUMBER", DbType.Decimal, en.INV_NUMBER);
            db.AddInParameter(oDbCommand, "@INV_DATE", DbType.DateTime, en.INV_DATE);
            db.AddInParameter(oDbCommand, "@INV_PHASE", DbType.String, en.INV_PHASE);
            //db.AddInParameter(oDbCommand, "@INV_TYPE", DbType.Decimal, en.INV_TYPE);
            db.AddInParameter(oDbCommand, "@ADD_ID", DbType.Decimal, en.ADD_ID);
            db.AddInParameter(oDbCommand, "@INV_BASE", DbType.Decimal, en.INV_BASE);
            db.AddInParameter(oDbCommand, "@INV_TAXES", DbType.Decimal, en.INV_TAXES);
            db.AddInParameter(oDbCommand, "@INV_NET", DbType.Decimal, en.INV_NET);
            db.AddInParameter(oDbCommand, "@INV_BALANCE", DbType.Decimal, en.INV_BALANCE);
            db.AddInParameter(oDbCommand, "@INV_REPORT_DETAILS", DbType.String, en.INV_REPORT_DETAILS);
            db.AddInParameter(oDbCommand, "@INV_REPORT_STATUS", DbType.Boolean, en.INV_REPORT_STATUS);
            db.AddInParameter(oDbCommand, "@INV_MANUAL", DbType.Boolean, en.INV_MANUAL);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, en.OFF_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            exito = db.ExecuteNonQuery(oDbCommand) > 0;
            return exito;
        }

        public List<BEFactura> ObtenerCorrelativo(decimal serie)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_CORRELATIVO");
            db.AddInParameter(oDbCommand, "@INV_NMR", DbType.Decimal, serie);
            db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEFactura>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEFactura factura = null;
                while (dr.Read())
                {
                    factura = new BEFactura();
                    if (!dr.IsDBNull(dr.GetOrdinal("NMR_NOW")))
                        factura.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("NMR_NOW"));
                    //factura.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("NMR_NOW"));
                    lista.Add(factura);
                }
            }
            return lista;
        }



        public List<BEFacturaConsulta> ListarConsultaFacturaPage(string owner, string numSerial, decimal numFact, decimal idSoc,
                                                decimal grupoFact, string moneda, decimal idLic,
                                                DateTime Fini, DateTime Ffin, decimal idFact,
                                                decimal licTipo, decimal idBpsAgen,
                                                int pagina, int cantRegxPag)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_FACTURA_CONSULTA_PAGE");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@NMR_SERIAL", DbType.String, numSerial);
                db.AddInParameter(oDbCommand, "@INV_NUMBER", DbType.Decimal, numFact);
                db.AddInParameter(oDbCommand, "@BPS_SOC", DbType.Decimal, idSoc);
                db.AddInParameter(oDbCommand, "@INVG_ID", DbType.Decimal, grupoFact);
                db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, moneda);
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLic);
                db.AddInParameter(oDbCommand, "@FINI", DbType.DateTime, Fini);
                db.AddInParameter(oDbCommand, "@FFIN", DbType.DateTime, Ffin);
                db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFact);
                db.AddInParameter(oDbCommand, "@LIC_TYPE", DbType.Decimal, licTipo);
                db.AddInParameter(oDbCommand, "@E_BPS_ID", DbType.Decimal, idBpsAgen);
                db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
                db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
                db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);

                db.ExecuteNonQuery(oDbCommand);

                string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

                var lista = new List<BEFacturaConsulta>();

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEFacturaConsulta factura = null;
                    while (dr.Read())
                    {
                        factura = new BEFacturaConsulta();
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                            factura.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_TYPE")))
                            factura.INV_TYPE = dr.GetDecimal(dr.GetOrdinal("INV_TYPE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NMR_SERIAL")))
                            factura.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVT_DESC")))
                            factura.INVT_DESC = dr.GetString(dr.GetOrdinal("INVT_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NUMBER")))
                            factura.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("INV_NUMBER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_DATE")))
                            factura.INV_DATE = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NULL")))
                            factura.INV_NULL = dr.GetDateTime(dr.GetOrdinal("INV_NULL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            factura.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CUR_DESC")))
                            factura.MONEDA = dr.GetString(dr.GetOrdinal("CUR_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                            factura.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));

                        factura.TotalVirtual = Convert.ToInt32(results);

                        lista.Add(factura);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<BEFactura> ListarFacturaBorrador(string owner, DateTime fini, DateTime ffin,
                                                     decimal tipoLic, string idMoneda, decimal idGrufact,
                                                     decimal idBps, decimal idCorrelativo, string idTipoPago,
                                                     int conFecha, decimal idLic, decimal idFactura, decimal off_id)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_FACTURAS_BORRADOR");
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
                db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFactura);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, off_id);
                var lista = new List<BEFactura>();
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEFactura factura = null;
                    while (dr.Read())
                    {
                        factura = new BEFactura();
                        factura.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        factura.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                        factura.LIC_TYPE = dr.GetDecimal(dr.GetOrdinal("LIC_TYPE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVG_ID")))
                            factura.INVG_ID = dr.GetDecimal(dr.GetOrdinal("INVG_ID"));
                        factura.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));

                        factura.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                        factura.INV_DATE = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));
                        factura.MONEDA = dr.GetString(dr.GetOrdinal("MONEDA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIPO_PAGO")))
                            factura.TIPO_PAGO = dr.GetString(dr.GetOrdinal("TIPO_PAGO")).ToUpper();
                        if (!dr.IsDBNull(dr.GetOrdinal("INVG_DESC")))
                            factura.GRUPO_FACT = dr.GetString(dr.GetOrdinal("INVG_DESC")).ToUpper();

                        factura.INV_BASE = dr.GetDecimal(dr.GetOrdinal("INV_BASE"));
                        factura.INV_TAXES = dr.GetDecimal(dr.GetOrdinal("INV_TAXES"));
                        factura.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                        factura.DESCUENTO = dr.GetDecimal(dr.GetOrdinal("DESCUENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_ID_BPS")))
                            factura.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID_BPS"));
                        factura.PAY_ID = dr.GetString(dr.GetOrdinal("PAY_ID"));
                        lista.Add(factura);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<BEFactura> ListarFacturaBorradorSerie(string owner, DateTime fini, DateTime ffin,
                                                     decimal tipoLic, string idMoneda, decimal idGrufact,
                                                     decimal idBps, decimal idCorrelativo, string idTipoPago,
                                                     int conFecha, decimal idLic, decimal idFactura, decimal off_id)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_FACTURAS_BORRADOR_SERIE");
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
                db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFactura);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, off_id);
                var lista = new List<BEFactura>();
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEFactura factura = null;
                    while (dr.Read())
                    {
                        factura = new BEFactura();
                        factura.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        factura.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                        factura.LIC_TYPE = dr.GetDecimal(dr.GetOrdinal("LIC_TYPE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVG_ID")))
                            factura.INVG_ID = dr.GetDecimal(dr.GetOrdinal("INVG_ID"));
                        factura.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));

                        factura.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                        factura.INV_DATE = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));
                        factura.MONEDA = dr.GetString(dr.GetOrdinal("MONEDA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIPO_PAGO")))
                            factura.TIPO_PAGO = dr.GetString(dr.GetOrdinal("TIPO_PAGO")).ToUpper();
                        if (!dr.IsDBNull(dr.GetOrdinal("INVG_DESC")))
                            factura.GRUPO_FACT = dr.GetString(dr.GetOrdinal("INVG_DESC")).ToUpper();

                        factura.INV_BASE = dr.GetDecimal(dr.GetOrdinal("INV_BASE"));
                        factura.INV_TAXES = dr.GetDecimal(dr.GetOrdinal("INV_TAXES"));
                        factura.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                        factura.DESCUENTO = dr.GetDecimal(dr.GetOrdinal("DESCUENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_ID_BPS")))
                            factura.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID_BPS"));
                        factura.PAY_ID = dr.GetString(dr.GetOrdinal("PAY_ID"));
                        lista.Add(factura);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<BEFactura> ListarFacturaMasivaSubGrilla(string owner, DateTime fini, DateTime ffin,
          string mogId, decimal modId, decimal dadId, decimal bpsId,
          decimal offId, decimal e_bpsId, decimal tipoEstId, decimal subTipoEstId, decimal licId, string monedaId, decimal LibConfi, int historico, string periodoEstado, decimal idBpsGroup, decimal groupfact, int oficina,int EmiMensual
          )
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_FACTURAS_GRAL_SUBGRILLA");
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

                var lista = new List<BEFactura>();
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEFactura factura = null;
                    while (dr.Read())
                    {
                        factura = new BEFactura();
                        factura.Nro = Convert.ToDecimal(dr.GetInt64(dr.GetOrdinal("Nro")));
                        factura.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        factura.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO")).ToUpper();
                        factura.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                        factura.MONEDA = dr.GetString(dr.GetOrdinal("MONEDA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIPO_PAGO")))
                            factura.TIPO_PAGO = dr.GetString(dr.GetOrdinal("TIPO_PAGO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("GF")))
                            factura.GRUPO_FACT = dr.GetString(dr.GetOrdinal("GF")).ToUpper();
                        else
                            factura.GRUPO_FACT = string.Empty;

                        factura.LIC_PL_STATUS = dr.GetString(dr.GetOrdinal("LIC_PL_STATUS"));

                        if (!dr.IsDBNull(dr.GetOrdinal("DIRECCION")))
                            factura.DIRECCION = dr.GetString(dr.GetOrdinal("DIRECCION")).ToUpper();
                        else
                            factura.DIRECCION = string.Empty;


                        lista.Add(factura);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<BEFactura> ListarFacturaPendientePago(string owner, decimal usuDerecho, decimal importe, string moneda)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_LISTAR_FAC_PEND_PAGO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, usuDerecho);
            db.AddInParameter(oDbCommand, "@INV_BASE", DbType.Decimal, importe);
            db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, moneda);
            db.ExecuteNonQuery(oDbCommand);

            var lista = new List<BEFactura>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEFactura factura = null;
                while (dr.Read())
                {
                    factura = new BEFactura();
                    factura.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_EXPID")))
                        factura.INV_EXPID = dr.GetDecimal(dr.GetOrdinal("INV_EXPID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_EXPDATE")))
                        factura.INV_EXPDATE = dr.GetDateTime(dr.GetOrdinal("INV_EXPDATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_BASE")))
                        factura.INV_BASE = dr.GetDecimal(dr.GetOrdinal("INV_BASE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_TAXES")))
                        factura.INV_TAXES = dr.GetDecimal(dr.GetOrdinal("INV_TAXES"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                        factura.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVT_DESC")))
                        factura.INVT_DESC = dr.GetString(dr.GetOrdinal("INVT_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_BALANCE")))
                        factura.INV_BALANCE = dr.GetDecimal(dr.GetOrdinal("INV_BALANCE"));
                    lista.Add(factura);
                }
            }
            return lista;
        }

        public bool ActualizarEstadoDefinitvoXML(string xml)
        {
            bool exito = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASU_FACTURA_EST_DEFINITIVO"))
            {
                oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                exito = oDataBase.ExecuteNonQuery(cm) > 0;
            }
            return exito;
        }

        public BEFactura ObtenerFacturaAplicar(string owner, decimal idFactura)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_OBTENER_FAC");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFactura);
            db.ExecuteNonQuery(oDbCommand);

            BEFactura factura = null;

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    factura = new BEFactura();
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                        factura.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));

                    if (!dr.IsDBNull(dr.GetOrdinal("INV_COLLECTB")))
                        factura.INV_COLLECTB = dr.GetDecimal(dr.GetOrdinal("INV_COLLECTB"));

                    if (!dr.IsDBNull(dr.GetOrdinal("INV_COLLECTT")))
                        factura.INV_COLLECTT = dr.GetDecimal(dr.GetOrdinal("INV_COLLECTT"));

                    if (!dr.IsDBNull(dr.GetOrdinal("INV_COLLECTD")))
                        factura.INV_COLLECTD = dr.GetDecimal(dr.GetOrdinal("INV_COLLECTD"));

                    if (!dr.IsDBNull(dr.GetOrdinal("INV_COLLECTN")))
                        factura.INV_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INV_COLLECTN"));

                    if (!dr.IsDBNull(dr.GetOrdinal("INV_BALANCE")))
                        factura.INV_BALANCE = dr.GetDecimal(dr.GetOrdinal("INV_BALANCE"));
                }
            }
            return factura;
        }

        public int ActualizarCollects(BEFactura en)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_FACTURA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, en.INV_ID);
            db.AddInParameter(oDbCommand, "@INV_COLLECTB", DbType.Decimal, en.INV_COLLECTB);
            db.AddInParameter(oDbCommand, "@INV_COLLECTT", DbType.Decimal, en.INV_COLLECTT);
            db.AddInParameter(oDbCommand, "@INV_COLLECTN", DbType.Decimal, en.INV_COLLECTN);
            db.AddInParameter(oDbCommand, "@INV_BALANCE", DbType.Decimal, en.INV_BALANCE);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEFactura> ListarConsulta(string owner, string numSerial, decimal numFact, decimal idSoc,
                                      decimal grupoFact, string moneda, decimal idLic,
                                      DateTime Fini, DateTime Ffin, decimal idFact,
                                      int impresas, int anuladas, decimal licTipo, decimal agenteBpsId,
                                      int conFecha, int tipoDoc, decimal idOficina, decimal valorDivision, int estado, decimal idBpsGroup, decimal idPlanificacion = 0)
        {

            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_FACTURA_CONSULTA");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@NMR_SERIAL", DbType.String, numSerial);
                db.AddInParameter(oDbCommand, "@INV_NUMBER", DbType.Decimal, numFact);
                db.AddInParameter(oDbCommand, "@BPS_SOC", DbType.Decimal, idSoc);
                db.AddInParameter(oDbCommand, "@INVG_ID", DbType.Decimal, grupoFact);
                db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, moneda);
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLic);
                db.AddInParameter(oDbCommand, "@FINI", DbType.DateTime, Fini);
                db.AddInParameter(oDbCommand, "@FFIN", DbType.DateTime, Ffin);
                db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFact);
                db.AddInParameter(oDbCommand, "@IMPRESA", DbType.Int32, impresas);
                db.AddInParameter(oDbCommand, "@ANULADA", DbType.Int32, anuladas);
                db.AddInParameter(oDbCommand, "@LIC_TYPE", DbType.Decimal, licTipo);
                db.AddInParameter(oDbCommand, "@E_BPS_ID", DbType.Decimal, agenteBpsId);
                db.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, conFecha);
                db.AddInParameter(oDbCommand, "@INV_TYPE", DbType.Int32, tipoDoc);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficina);
                db.AddInParameter(oDbCommand, "@DADV_ID", DbType.Decimal, valorDivision);
                db.AddInParameter(oDbCommand, "@ESTADO", DbType.Decimal, estado);
                db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, idPlanificacion);
                db.AddInParameter(oDbCommand, "@BPS_GROUP", DbType.Decimal, idBpsGroup);


                var lista = new List<BEFactura>();

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEFactura factura = null;
                    while (dr.Read())
                    {
                        factura = new BEFactura();
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                            factura.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_TYPE")))
                            factura.INV_TYPE = dr.GetDecimal(dr.GetOrdinal("INV_TYPE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NMR_SERIAL")))
                            factura.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));

                        if (!dr.IsDBNull(dr.GetOrdinal("INVT_DESC")))
                            factura.INVT_DESC = dr.GetString(dr.GetOrdinal("INVT_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NUMBER")))
                            factura.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("INV_NUMBER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_DATE")))
                            factura.INV_DATE = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NULL")))
                            factura.INV_NULL = dr.GetDateTime(dr.GetOrdinal("INV_NULL"));

                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            factura.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TAXN_NAME")))
                            factura.TAXN_NAME = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                            factura.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CUR_DESC")))
                            factura.MONEDA = dr.GetString(dr.GetOrdinal("CUR_DESC"));

                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                            factura.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_BALANCE")))
                            factura.INV_BALANCE = dr.GetDecimal(dr.GetOrdinal("INV_BALANCE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_CN_REF")))
                            factura.INV_CN_REF = dr.GetDecimal(dr.GetOrdinal("INV_CN_REF"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_COLLECTN")))
                            factura.INV_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INV_COLLECTN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                            factura.EST_FACT = dr.GetInt32(dr.GetOrdinal("ESTADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CANCELACION")))
                            factura.FECHA_CANCELACION = dr.GetDateTime(dr.GetOrdinal("FECHA_CANCELACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_MANUAL")))
                            factura.INV_MANUAL = dr.GetBoolean(dr.GetOrdinal("INV_MANUAL"));
                        else
                            factura.INV_MANUAL = false;

                        //ESTADO QUE DEVUELVE SUNAT PARA LA CONSULTA DE FACTURAS
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_SUNAT")))
                            factura.ESTADO_SUNAT = dr.GetString(dr.GetOrdinal("ESTADO_SUNAT"));

                        lista.Add(factura);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public List<BEFactura> ListaReporteCabeceraConsulta(string owner, decimal id)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_CAB_FACTURA_CONSULTA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.String, id);
            //db.ExecuteNonQuery(oDbCommand);

            var lista = new List<BEFactura>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEFactura factura = null;
                while (dr.Read())
                {
                    factura = new BEFactura();
                    factura.TAXT_ID = dr.GetDecimal(dr.GetOrdinal("TAXT_ID"));
                    factura.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                    factura.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                    factura.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                    factura.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("INV_NUMBER"));
                    factura.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RUM")))
                        factura.RUM = dr.GetString(dr.GetOrdinal("RUM")).ToUpper();
                    if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS")))
                        factura.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS")).ToUpper();

                    if (!dr.IsDBNull(dr.GetOrdinal("TIPO_FACT")))
                        factura.TIPO_FACT = dr.GetString(dr.GetOrdinal("TIPO_FACT")).ToUpper();

                    factura.INV_DATE = dr.GetDateTime(dr.GetOrdinal("FECHA_EMI"));
                    lista.Add(factura);
                }
            }
            return lista;
        }

        public List<BEFacturaDetalle> ListaReporteDetalleConsulta(string owner, decimal id)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_DET_FACTURA_CONSULTA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.String, id);
            //db.ExecuteNonQuery(oDbCommand);

            var lista = new List<BEFacturaDetalle>();
            var listaFinal = new List<BEFacturaDetalle>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEFacturaDetalle detalle = null;
                while (dr.Read())
                {
                    detalle = new BEFacturaDetalle();
                    detalle.ReporteAgrupado = dr.GetInt32(dr.GetOrdinal("AGRUPADO"));
                    detalle.INVL_ID = dr.GetDecimal(dr.GetOrdinal("INVL_ID"));
                    detalle.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                    detalle.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_NAME")))
                        detalle.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                        detalle.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                    detalle.TOTAL = dr.GetDecimal(dr.GetOrdinal("TOTAL"));
                    detalle.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                    lista.Add(detalle);

                }

                if (detalle.ReporteAgrupado == 1)
                {
                    string detalleReporte = string.Empty;
                    foreach (var item in lista)
                    {
                        detalleReporte += item.LIC_NAME + "\r\n";
                        item.LIC_NAME = detalleReporte;
                    }
                    listaFinal.Add(lista.LastOrDefault());
                }
                else
                {
                    listaFinal = lista;
                }

            }
            return listaFinal;
        }

        public List<BEFacturaConsulta> ListarConsultaFacturaBecPage(string owner, string numSerial, decimal numFact, decimal idSoc,
                                             decimal grupoFact, string moneda, decimal idLic,
                                             DateTime Fini, DateTime? Ffin, decimal idFact,
                                             decimal licTipo, decimal idBpsAgen, int conFecha, decimal idOficinaUsuario,
                                             int pagina, int cantRegxPag)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_FACTURA_CONSULTA_BEC_PAGE");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@NMR_SERIAL", DbType.String, numSerial);
                db.AddInParameter(oDbCommand, "@INV_NUMBER", DbType.Decimal, numFact);
                db.AddInParameter(oDbCommand, "@BPS_SOC", DbType.Decimal, idSoc);
                db.AddInParameter(oDbCommand, "@INVG_ID", DbType.Decimal, grupoFact);
                db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, moneda);
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLic);
                db.AddInParameter(oDbCommand, "@FINI", DbType.DateTime, Fini);
                db.AddInParameter(oDbCommand, "@FFIN", DbType.DateTime, Ffin);
                db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFact);
                db.AddInParameter(oDbCommand, "@LIC_TYPE", DbType.Decimal, licTipo);
                db.AddInParameter(oDbCommand, "@E_BPS_ID", DbType.Decimal, idBpsAgen);
                db.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, conFecha);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficinaUsuario);
                db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
                db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
                db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 0);
                db.ExecuteNonQuery(oDbCommand);
                string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));


                var lista = new List<BEFacturaConsulta>();

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEFacturaConsulta factura = null;
                    while (dr.Read())
                    {
                        factura = new BEFacturaConsulta();
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                            factura.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_TYPE")))
                            factura.INV_TYPE = dr.GetDecimal(dr.GetOrdinal("INV_TYPE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NMR_SERIAL")))
                            factura.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVT_DESC")))
                            factura.INVT_DESC = dr.GetString(dr.GetOrdinal("INVT_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NUMBER")))
                            factura.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("INV_NUMBER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_DATE")))
                            factura.INV_DATE = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NULL")))
                            factura.INV_NULL = dr.GetDateTime(dr.GetOrdinal("INV_NULL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            factura.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CUR_DESC")))
                            factura.MONEDA = dr.GetString(dr.GetOrdinal("CUR_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                            factura.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                        factura.TotalVirtual = Convert.ToInt32(results);
                        lista.Add(factura);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public BEFactura ObtenerFacturaBec(string owner, decimal idFactura)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_OBTENER_FACT_BEC");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFactura);
            db.ExecuteNonQuery(oDbCommand);
            BEFactura factura = null;

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    factura = new BEFactura();
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                        factura.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("INV_NMR")))
                        factura.INV_NMR = dr.GetDecimal(dr.GetOrdinal("INV_NMR"));

                    if (!dr.IsDBNull(dr.GetOrdinal("NMR_SERIAL")))
                        factura.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));

                    if (!dr.IsDBNull(dr.GetOrdinal("INV_NUMBER")))
                        factura.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("INV_NUMBER"));

                    if (!dr.IsDBNull(dr.GetOrdinal("INV_BASE")))
                        factura.INV_BASE = dr.GetDecimal(dr.GetOrdinal("INV_BASE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("INV_TAXES")))
                        factura.INV_TAXES = dr.GetDecimal(dr.GetOrdinal("INV_TAXES"));

                    if (!dr.IsDBNull(dr.GetOrdinal("INV_TDEDUCTIONS")))
                        factura.INV_TDEDUCTIONS = dr.GetDecimal(dr.GetOrdinal("INV_TDEDUCTIONS"));

                    if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                        factura.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));

                    if (!dr.IsDBNull(dr.GetOrdinal("DISCOUNTS")))
                        factura.INV_DISCOUNTS = dr.GetDecimal(dr.GetOrdinal("DISCOUNTS"));


                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        factura.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("TD")))
                        factura.TIPO_FACT = dr.GetString(dr.GetOrdinal("TD"));

                    if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                        factura.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));

                    if (!dr.IsDBNull(dr.GetOrdinal("CUR_ALPHA")))
                        factura.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));

                    if (!dr.IsDBNull(dr.GetOrdinal("MONEDA")))
                        factura.MONEDA = dr.GetString(dr.GetOrdinal("MONEDA"));

                    if (!dr.IsDBNull(dr.GetOrdinal("PENDIENTE_APLICACION")))
                        factura.PENDIENTE_APLICACION = dr.GetDecimal(dr.GetOrdinal("PENDIENTE_APLICACION"));
                }
            }
            return factura;
        }

        public int AnularFactura(BEFactura factura)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_FACTURA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, factura.OWNER);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, factura.INV_ID);
            db.AddInParameter(oDbCommand, "@INV_NULLREASON", DbType.String, factura.INV_NULLREASON.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, factura.LOG_USER_UPDATE.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_OFF_ID", DbType.Decimal, factura.OFF_ID);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ActualizarObs(BEFactura factura)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_OBS_FACTURA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, factura.OWNER);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, factura.INV_ID);
            db.AddInParameter(oDbCommand, "@CODE_DESCRIPTION", DbType.String, factura.CODE_DESCRIPTION);
            db.AddInParameter(oDbCommand, "@INV_NULLREASON", DbType.String, factura.INV_NULLREASON.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, factura.LOG_USER_UPDATE.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        //**********FAC CAN SIN ANUL
        #region NOTA DE CREDITO
        public int FacturaCancSinAnul(BEFactura factura)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_FACTURA_CAN_SIN_ANUL");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, factura.OWNER);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, factura.INV_ID);
            db.AddInParameter(oDbCommand, "@INV_NULLREASON", DbType.String, factura.INV_NULLREASON);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, factura.LOG_USER_UPDATE.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_OFF_ID", DbType.Decimal, factura.OFF_ID);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
        public int GuardarNuevaNotaCredito(BENotaCredito factura)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_NEW_NC");           
            db.AddInParameter(oDbCommand, "@INV_ID_F", DbType.Decimal, factura.facturaId);
            db.AddInParameter(oDbCommand, "@TIPO_NC", DbType.Int32, factura.tipoNC);
            db.AddInParameter(oDbCommand, "@MONT_PORC", DbType.Decimal, factura.textoTipoNC);
            db.AddInParameter(oDbCommand, "@FECHA_EMI_NC", DbType.DateTime, factura.fechaEmision);
            db.AddInParameter(oDbCommand, "@TIPO_NC_SUNAT", DbType.String, factura.TipoSunat);
            db.AddInParameter(oDbCommand, "@RAZON", DbType.String, factura.Observacion);
            db.AddInParameter(oDbCommand, "@USER_CREAT", DbType.String, factura.UsuarioCreacion.ToUpper());
            int r = Convert.ToInt32( db.ExecuteScalar(oDbCommand));
            return r;
        }
        public BEFactura Aplica_Nota_Credito(decimal INV_ID, string USU, string TIP_NOT_CRE, string OBSERV, decimal SERIE)
        {
            BEFactura item = null;
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_APLICA_NOTA_CREDITO");
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, INV_ID);
            db.AddInParameter(oDbCommand, "@USU", DbType.String, USU);
            db.AddInParameter(oDbCommand, "@TIP_NOT_CRE", DbType.String, TIP_NOT_CRE);
            db.AddInParameter(oDbCommand, "@OBS", DbType.String, OBSERV);
            db.AddInParameter(oDbCommand, "@SERIE", DbType.Decimal, SERIE);
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEFactura();
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                        item.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TIPO")))
                        item.INV_TYPE = dr.GetDecimal(dr.GetOrdinal("TIPO"));

                }
            }
            return item;
        }

        #endregion
        //*********

        public BEFactura CabeceraFacturaNotaCredito(string owner, decimal Id)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_CAB_INVOICE_NOTACREDITO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, Id);
            BEFactura item = null;

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEFactura();
                    if (!dr.IsDBNull(dr.GetOrdinal("NMR_SERIAL")))
                        item.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_NUMBER")))
                        item.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("INV_NUMBER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_TYPE")))
                        item.INV_TYPE = dr.GetDecimal(dr.GetOrdinal("INV_TYPE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CUR_DESC")))
                        item.CUR_DESC = dr.GetString(dr.GetOrdinal("CUR_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CUR_ALPHA")))
                        item.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                        item.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_DATE")))
                        item.INV_DATE = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_EXPDATE")))
                        item.INV_EXPDATE = dr.GetDateTime(dr.GetOrdinal("INV_EXPDATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                        item.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_CN_TOTAL")))
                        item.INV_CN_TOTAL = dr.GetDecimal(dr.GetOrdinal("INV_CN_TOTAL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("GENERAR_NC")))
                        item.GENERAR_NC = dr.GetInt32(dr.GetOrdinal("GENERAR_NC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OBS_NC")))
                        item.OBS_NC = dr.GetString(dr.GetOrdinal("OBS_NC"));
                }
            }
            return item;
        }

        public List<BEFacturaDetalle> DetalleFacturaNotaCredito(string owner, decimal Id)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_DET_INVOICE_NOTACREDITO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, Id);
            BEFacturaDetalle item = null;
            List<BEFacturaDetalle> lista = new List<BEFacturaDetalle>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEFacturaDetalle();
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                        item.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_ID")))
                        item.INVL_ID = dr.GetDecimal(dr.GetOrdinal("INVL_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_NAME")))
                        item.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                        item.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PERIODO")))
                        item.PERIODO = dr.GetString(dr.GetOrdinal("PERIODO"));
                    //if (!dr.IsDBNull(dr.GetOrdinal("REC_DATE")))
                    //    item.REC_DATE = dr.GetString(dr.GetOrdinal("REC_DATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_BASE")))
                        item.INVL_BASE = dr.GetDecimal(dr.GetOrdinal("INVL_BASE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_TAXES")))
                        item.INVL_TAXES = dr.GetDecimal(dr.GetOrdinal("INVL_TAXES"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_NET")))
                        item.INVL_NET = dr.GetDecimal(dr.GetOrdinal("INVL_NET"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_COLLECTB")))
                        item.INVL_COLLECTB = dr.GetDecimal(dr.GetOrdinal("INVL_COLLECTB"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_COLLECTT")))
                        item.INVL_COLLECTT = dr.GetDecimal(dr.GetOrdinal("INVL_COLLECTT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_COLLECTN")))
                        item.INVL_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INVL_COLLECTN"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_BALANCE")))
                        item.INVL_BALANCE = dr.GetDecimal(dr.GetOrdinal("INV_BALANCE"));
                    lista.Add(item);
                }
            }
            return lista;
        }
        public List<BEFacturaDetalle> DetalleFacturaNotaCredito2(string owner, decimal Id, string anio, string mes, decimal idLic)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_DET_INVOICE_NOTACREDITO2");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, Id);
            db.AddInParameter(oDbCommand, "@MES", DbType.String, mes);
            db.AddInParameter(oDbCommand, "@ANIO", DbType.String, anio);
            db.AddInParameter(oDbCommand, "@LIC_ID_F", DbType.Decimal, idLic);
            BEFacturaDetalle item = null;
            List<BEFacturaDetalle> lista = new List<BEFacturaDetalle>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEFacturaDetalle();
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                        item.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                    //if (!dr.IsDBNull(dr.GetOrdinal("INVL_ID")))
                    //    item.INVL_ID = dr.GetDecimal(dr.GetOrdinal("INVL_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_NAME")))
                        item.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                        item.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                    //if (!dr.IsDBNull(dr.GetOrdinal("PERIODO")))
                    //    item.PERIODO = dr.GetString(dr.GetOrdinal("PERIODO"));
                    //if (!dr.IsDBNull(dr.GetOrdinal("REC_DATE")))
                    //    item.REC_DATE = dr.GetString(dr.GetOrdinal("REC_DATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_BASE")))
                        item.INVL_BASE = dr.GetDecimal(dr.GetOrdinal("INVL_BASE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_TAXES")))
                        item.INVL_TAXES = dr.GetDecimal(dr.GetOrdinal("INVL_TAXES"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_NET")))
                        item.INVL_NET = dr.GetDecimal(dr.GetOrdinal("INVL_NET"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_COLLECTB")))
                        item.INVL_COLLECTB = dr.GetDecimal(dr.GetOrdinal("INVL_COLLECTB"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_COLLECTT")))
                        item.INVL_COLLECTT = dr.GetDecimal(dr.GetOrdinal("INVL_COLLECTT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_COLLECTN")))
                        item.INVL_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INVL_COLLECTN"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_BALANCE")))
                        item.INVL_BALANCE = dr.GetDecimal(dr.GetOrdinal("INV_BALANCE"));
                    lista.Add(item);
                }
            }
            return lista;
        }
        public List<BEFacturaDetalle> DetalleFacturaNotaCredito2Periodo(string owner, decimal Id, string anio, string mes, decimal idLic)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_DET_INVOICE_NOTACREDITO2_PERIODOS");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, Id);
            db.AddInParameter(oDbCommand, "@MES", DbType.String, mes);
            db.AddInParameter(oDbCommand, "@ANIO", DbType.String, anio);
            db.AddInParameter(oDbCommand, "@LIC_ID_F", DbType.Decimal, idLic);
            BEFacturaDetalle item = null;
            List<BEFacturaDetalle> lista = new List<BEFacturaDetalle>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEFacturaDetalle();
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                        item.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_ID")))
                        item.INVL_ID = dr.GetDecimal(dr.GetOrdinal("INVL_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_NAME")))
                        item.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                        item.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PERIODO")))
                        item.PERIODO = dr.GetString(dr.GetOrdinal("PERIODO"));
                    //if (!dr.IsDBNull(dr.GetOrdinal("REC_DATE")))
                    //    item.REC_DATE = dr.GetString(dr.GetOrdinal("REC_DATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_BASE")))
                        item.INVL_BASE = dr.GetDecimal(dr.GetOrdinal("INVL_BASE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_TAXES")))
                        item.INVL_TAXES = dr.GetDecimal(dr.GetOrdinal("INVL_TAXES"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_NET")))
                        item.INVL_NET = dr.GetDecimal(dr.GetOrdinal("INVL_NET"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_COLLECTB")))
                        item.INVL_COLLECTB = dr.GetDecimal(dr.GetOrdinal("INVL_COLLECTB"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_COLLECTT")))
                        item.INVL_COLLECTT = dr.GetDecimal(dr.GetOrdinal("INVL_COLLECTT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_COLLECTN")))
                        item.INVL_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INVL_COLLECTN"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_BALANCE")))
                        item.INVL_BALANCE = dr.GetDecimal(dr.GetOrdinal("INVL_BALANCE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_YEAR")))
                        item.LIC_YEAR = dr.GetDecimal(dr.GetOrdinal("LIC_YEAR"));
                    lista.Add(item);
                    
                }
            }
            return lista;
        }
        public List<BERecibo> ListarRecibosFactura(string owner, decimal IdFactura)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_RECIBOS_FACTURA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, IdFactura);
            //db.ExecuteNonQuery(oDbCommand);

            var lista = new List<BERecibo>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BERecibo item = null;
                while (dr.Read())
                {
                    item = new BERecibo();
                    item.REC_ID = dr.GetDecimal(dr.GetOrdinal("REC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                        item.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("REC_DATE")))
                        item.REC_DATE = dr.GetDateTime(dr.GetOrdinal("REC_DATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("REC_TBASE")))
                        item.REC_TBASE = dr.GetDecimal(dr.GetOrdinal("REC_TBASE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("REC_TTAXES")))
                        item.REC_TTAXES = dr.GetDecimal(dr.GetOrdinal("REC_TTAXES"));
                    if (!dr.IsDBNull(dr.GetOrdinal("REC_TTOTAL")))
                        item.REC_TTOTAL = dr.GetDecimal(dr.GetOrdinal("REC_TTOTAL"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public List<BEFacturaConsulta> ListarTipoFactura(string owner)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_FACTURA_TIPO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            var lista = new List<BEFacturaConsulta>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEFacturaConsulta factura = null;
                while (dr.Read())
                {
                    factura = new BEFacturaConsulta();
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_TYPE")))
                        factura.INV_TYPE = dr.GetDecimal(dr.GetOrdinal("INV_TYPE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVT_DESC")))
                        factura.INVT_DESC = dr.GetString(dr.GetOrdinal("INVT_DESC"));
                    lista.Add(factura);
                }
            }
            return lista;
        }

        public int InsertarFactura(BEFactura en)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAI_FACTURACAB");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddOutParameter(oDbCommand, "@INV_ID", DbType.Decimal, Convert.ToInt32(en.INV_ID));
            db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, en.CUR_ALPHA);
            db.AddInParameter(oDbCommand, "@INV_NMR", DbType.Decimal, en.INV_NMR);
            db.AddInParameter(oDbCommand, "@INV_NUMBER", DbType.Decimal, en.INV_NUMBER);
            db.AddInParameter(oDbCommand, "@INV_DATE", DbType.DateTime, en.INV_DATE);
            db.AddInParameter(oDbCommand, "@INV_TYPE", DbType.Decimal, en.INV_TYPE);
            db.AddInParameter(oDbCommand, "@INV_DETAIL", DbType.String, en.INV_DETAIL);
            db.AddInParameter(oDbCommand, "@INV_PHASE", DbType.String, en.INV_PHASE);
            db.AddInParameter(oDbCommand, "@INV_REPRINTS", DbType.Decimal, en.INV_REPRINTS);
            db.AddInParameter(oDbCommand, "@INV_COPIES", DbType.Decimal, en.INV_COPIES);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@ADD_ID", DbType.Decimal, en.ADD_ID);
            db.AddInParameter(oDbCommand, "@INV_PRE", DbType.Decimal, en.INV_PRE);
            db.AddInParameter(oDbCommand, "@INV_PROCES", DbType.Decimal, en.INV_PROCES);
            db.AddInParameter(oDbCommand, "@INV_PRINT_DATE", DbType.DateTime, en.INV_PRINT_DATE);
            db.AddInParameter(oDbCommand, "@INV_REPRINT_DATE", DbType.DateTime, en.INV_REPRINT_DATE);
            db.AddInParameter(oDbCommand, "@INV_COPY_DATE", DbType.DateTime, en.INV_COPY_DATE);
            db.AddInParameter(oDbCommand, "@INV_NULL", DbType.DateTime, en.INV_NULL);
            db.AddInParameter(oDbCommand, "@INV_NULLREASON", DbType.String, en.INV_NULLREASON);
            db.AddInParameter(oDbCommand, "@INV_ACCOUNTED", DbType.DateTime, en.INV_ACCOUNTED);
            db.AddInParameter(oDbCommand, "@INV_ACC_PROCESS", DbType.Decimal, en.INV_ACC_PROCESS);
            db.AddInParameter(oDbCommand, "@INV_LIQ_COM_TOT", DbType.DateTime, en.INV_LIQ_COM_TOT);
            db.AddInParameter(oDbCommand, "@INV_OBSERV", DbType.String, en.INV_OBSERV);
            db.AddInParameter(oDbCommand, "@INV_CN_IND", DbType.String, en.INV_CN_IND);
            db.AddInParameter(oDbCommand, "@INV_CN_REF", DbType.Decimal, en.INV_CN_REF);

            db.AddInParameter(oDbCommand, "@INV_BASE", DbType.Decimal, en.INV_BASE);
            db.AddInParameter(oDbCommand, "@INV_TAXES", DbType.Decimal, en.INV_TAXES);
            db.AddInParameter(oDbCommand, "@INV_NET", DbType.Decimal, en.INV_NET);
            db.AddInParameter(oDbCommand, "@INV_TBASE", DbType.Decimal, null);
            db.AddInParameter(oDbCommand, "@INV_TTAXES", DbType.String, null);
            db.AddInParameter(oDbCommand, "@INV_TDEDUCTIONS", DbType.Decimal, null);
            db.AddInParameter(oDbCommand, "@INV_TNET", DbType.Decimal, null);
            db.AddInParameter(oDbCommand, "@INV_COLLECTB", DbType.Decimal, en.INV_COLLECTB);
            db.AddInParameter(oDbCommand, "@INV_COLLECTT", DbType.Decimal, en.INV_COLLECTT);
            db.AddInParameter(oDbCommand, "@INV_COLLECTD", DbType.Decimal, null);
            db.AddInParameter(oDbCommand, "@INV_COLLECTN", DbType.Decimal, en.INV_COLLECTN);
            db.AddInParameter(oDbCommand, "@INV_BALANCE", DbType.Decimal, en.INV_BALANCE);

            db.AddInParameter(oDbCommand, "@INV_DISTRIBUTED", DbType.DateTime, en.INV_DISTRIBUTED);
            db.AddInParameter(oDbCommand, "@INV_DIST_PROCESS", DbType.Decimal, en.INV_DIST_PROCESS);
            db.AddInParameter(oDbCommand, "@INV_KEY", DbType.String, en.INV_KEY);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_OFI_ID", DbType.Decimal, en.OFF_ID);
            db.AddInParameter(oDbCommand, "@INV_CN_TOTAL", DbType.Decimal, en.INV_CN_TOTAL);
            db.AddInParameter(oDbCommand, "@INV_IND_NC_TOTAL", DbType.Decimal, en.INV_IND_NC_TOTAL);
            int n = db.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(db.GetParameterValue(oDbCommand, "@INV_ID"));
            return id;
        }

        public int ActualizarReferenciaNotaCreditoFactura(string owner, decimal idFact, decimal idFactNew, string user)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_REF_INVOCICE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFact);
            db.AddInParameter(oDbCommand, "@INV_CN_REF", DbType.Decimal, idFactNew);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BELicencias> ListarFactLicConsulta(string owner, string numSerial, decimal numFact, decimal idSoc,
                                       decimal grupoFact, string moneda, decimal idLic,
                                       DateTime Fini, DateTime Ffin, decimal idFact,
                                       int impresas, int anuladas, decimal licTipo, decimal agenteBpsId,
                                       int conFecha, int tipoDoc, decimal idOficina, decimal valorDivision, int estado, decimal idBpsGroup, decimal idPlanificacion = 0)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_LICENCIAS_FACT_CONSULTA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@NMR_SERIAL", DbType.String, numSerial);
            db.AddInParameter(oDbCommand, "@INV_NUMBER", DbType.Decimal, numFact);
            db.AddInParameter(oDbCommand, "@BPS_SOC", DbType.Decimal, idSoc);
            db.AddInParameter(oDbCommand, "@INVG_ID", DbType.Decimal, grupoFact);
            db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, moneda);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLic);
            db.AddInParameter(oDbCommand, "@FINI", DbType.DateTime, Fini);
            db.AddInParameter(oDbCommand, "@FFIN", DbType.DateTime, Ffin);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFact);
            db.AddInParameter(oDbCommand, "@IMPRESA", DbType.Int32, impresas);
            db.AddInParameter(oDbCommand, "@ANULADA", DbType.Int32, anuladas);
            db.AddInParameter(oDbCommand, "@LIC_TYPE", DbType.Decimal, licTipo);
            db.AddInParameter(oDbCommand, "@E_BPS_ID", DbType.Decimal, agenteBpsId);
            db.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, conFecha);
            db.AddInParameter(oDbCommand, "@INV_TYPE", DbType.Int32, tipoDoc);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficina);
            db.AddInParameter(oDbCommand, "@DADV_ID", DbType.Decimal, valorDivision);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.Decimal, estado);
            db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, idPlanificacion);
            db.AddInParameter(oDbCommand, "@BPS_GROUP", DbType.Decimal, idBpsGroup);

            var lista = new List<BELicencias>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BELicencias factura = null;
                while (dr.Read())
                {
                    factura = new BELicencias();
                    factura.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                    factura.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    factura.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                    factura.Modalidad = dr.GetString(dr.GetOrdinal("MOD_DEC"));
                    factura.Establecimiento = dr.GetString(dr.GetOrdinal("EST_NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_GROSS")))
                        factura.INVL_GROSS = dr.GetDecimal(dr.GetOrdinal("INVL_GROSS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_DISC")))
                        factura.INVL_DISC = dr.GetDecimal(dr.GetOrdinal("INVL_DISC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_BASE")))
                        factura.INVL_BASE = dr.GetDecimal(dr.GetOrdinal("INVL_BASE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_TAXES")))
                        factura.INVL_TAXES = dr.GetDecimal(dr.GetOrdinal("INVL_TAXES"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_NET")))
                        factura.INVL_NET = dr.GetDecimal(dr.GetOrdinal("INVL_NET"));
                    lista.Add(factura);
                }
            }
            return lista;
        }

        public List<BEFacturaDetalle> ListarFac_LicPlanemientoSubGrilla
                                        (string owner, string numSerial, decimal numFact, decimal idSoc,
                                        decimal grupoFact, string moneda, decimal idLic,
                                        DateTime Fini, DateTime Ffin, decimal idFact,
                                        int impresas, int anuladas, decimal licTipo, decimal agenteBpsId,
                                        int conFecha, int tipoDoc, decimal idOficina, decimal valorDivision, int estado, decimal idBpsGroup, decimal idPlanificacion = 0)
        {
            decimal id = 0;
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_DET_FACTURA_CONSULTA");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@NMR_SERIAL", DbType.String, numSerial);
                db.AddInParameter(oDbCommand, "@INV_NUMBER", DbType.Decimal, numFact);
                db.AddInParameter(oDbCommand, "@BPS_SOC", DbType.Decimal, idSoc);
                db.AddInParameter(oDbCommand, "@INVG_ID", DbType.Decimal, grupoFact);
                db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, moneda);
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLic);
                db.AddInParameter(oDbCommand, "@FINI", DbType.DateTime, Fini);
                db.AddInParameter(oDbCommand, "@FFIN", DbType.DateTime, Ffin);
                db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFact);
                db.AddInParameter(oDbCommand, "@IMPRESA", DbType.Int32, impresas);
                db.AddInParameter(oDbCommand, "@ANULADA", DbType.Int32, anuladas);
                db.AddInParameter(oDbCommand, "@LIC_TYPE", DbType.Decimal, licTipo);
                db.AddInParameter(oDbCommand, "@E_BPS_ID", DbType.Decimal, agenteBpsId);
                db.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, conFecha);
                db.AddInParameter(oDbCommand, "@INV_TYPE", DbType.Int32, tipoDoc);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficina);
                db.AddInParameter(oDbCommand, "@DADV_ID", DbType.Decimal, valorDivision);
                db.AddInParameter(oDbCommand, "@ESTADO", DbType.Decimal, estado);
                db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, idPlanificacion);
                db.AddInParameter(oDbCommand, "@BPS_GROUP", DbType.Decimal, idBpsGroup);
                //db.ExecuteNonQuery(oDbCommand);
                var lista = new List<BEFacturaDetalle>();

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEFacturaDetalle licPlaneamiento = null;
                    while (dr.Read())
                    {
                        licPlaneamiento = new BEFacturaDetalle();
                        licPlaneamiento.INVL_ID = dr.GetDecimal(dr.GetOrdinal("INVL_ID"));
                        id = licPlaneamiento.INVL_ID;
                        licPlaneamiento.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        licPlaneamiento.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        licPlaneamiento.LIC_DATE = dr.GetDateTime(dr.GetOrdinal("LIC_DATE"));
                        licPlaneamiento.LIC_YEAR = dr.GetDecimal(dr.GetOrdinal("LIC_YEAR"));
                        licPlaneamiento.LIC_MONTH = dr.GetDecimal(dr.GetOrdinal("LIC_MONTH"));

                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_GROSS")))
                            licPlaneamiento.INVL_GROSS = dr.GetDecimal(dr.GetOrdinal("INVL_GROSS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_DISC")))
                            licPlaneamiento.INVL_DISC = dr.GetDecimal(dr.GetOrdinal("INVL_DISC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_BASE")))
                            licPlaneamiento.INVL_BASE = dr.GetDecimal(dr.GetOrdinal("INVL_BASE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_TAXES")))
                            licPlaneamiento.INVL_TAXES = dr.GetDecimal(dr.GetOrdinal("INVL_TAXES"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_NET")))
                            licPlaneamiento.INVL_NET = dr.GetDecimal(dr.GetOrdinal("INVL_NET"));

                        licPlaneamiento.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));
                        licPlaneamiento.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                        lista.Add(licPlaneamiento);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                decimal dec = id;
                return null;
            }
        }


        public List<BEFacturaDetalle> ReporteFactConsulta
                                    (string owner, string numSerial, decimal numFact, decimal idSoc,
                                    decimal grupoFact, string moneda, decimal idLic,
                                    DateTime Fini, DateTime Ffin, decimal idFact,
                                    int impresas, int anuladas, decimal licTipo, decimal agenteBpsId,
                                    int conFecha, int tipoDoc, decimal idOficina, decimal valorDivision, int estado)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_FACTURA_CONSULTA");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@NMR_SERIAL", DbType.String, numSerial);
                db.AddInParameter(oDbCommand, "@INV_NUMBER", DbType.Decimal, numFact);
                db.AddInParameter(oDbCommand, "@BPS_SOC", DbType.Decimal, idSoc);
                db.AddInParameter(oDbCommand, "@INVG_ID", DbType.Decimal, grupoFact);
                db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, moneda);
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLic);
                db.AddInParameter(oDbCommand, "@FINI", DbType.DateTime, Fini);
                db.AddInParameter(oDbCommand, "@FFIN", DbType.DateTime, Ffin);
                db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFact);
                db.AddInParameter(oDbCommand, "@IMPRESA", DbType.Int32, impresas);
                db.AddInParameter(oDbCommand, "@ANULADA", DbType.Int32, anuladas);
                db.AddInParameter(oDbCommand, "@LIC_TYPE", DbType.Decimal, licTipo);
                db.AddInParameter(oDbCommand, "@E_BPS_ID", DbType.Decimal, agenteBpsId);
                db.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, conFecha);
                db.AddInParameter(oDbCommand, "@INV_TYPE", DbType.Int32, tipoDoc);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficina);
                db.AddInParameter(oDbCommand, "@DADV_ID", DbType.Decimal, valorDivision);
                db.AddInParameter(oDbCommand, "@ESTADO", DbType.Decimal, estado);
                db.ExecuteNonQuery(oDbCommand);
                var lista = new List<BEFacturaDetalle>();

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEFacturaDetalle licPlaneamiento = null;
                    while (dr.Read())
                    {
                        licPlaneamiento = new BEFacturaDetalle();
                        licPlaneamiento.INVL_ID = dr.GetDecimal(dr.GetOrdinal("INVL_ID"));
                        licPlaneamiento.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        licPlaneamiento.INV_DATE = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));
                        licPlaneamiento.INVT_DESC = dr.GetString(dr.GetOrdinal("INVT_DESC"));
                        licPlaneamiento.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                        licPlaneamiento.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("INV_NUMBER"));
                        licPlaneamiento.TAXN_NAME = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                        licPlaneamiento.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                        licPlaneamiento.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                        licPlaneamiento.LIC_DATE = dr.GetDateTime(dr.GetOrdinal("LIC_DATE"));
                        //licPlaneamiento.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));
                        int est = dr.GetInt32(dr.GetOrdinal("ESTADO"));
                        switch (est)
                        {
                            case 4: licPlaneamiento.ESTADO = "ANULADA"; break;
                            case 2: licPlaneamiento.ESTADO = "CANCELADO"; break;
                            case 1: licPlaneamiento.ESTADO = "CANCELADA PARCIAL"; break;
                            case 3: licPlaneamiento.ESTADO = "PENDIENTE PAGO"; break;
                            default: licPlaneamiento.ESTADO = ""; break;
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_NET")))
                            licPlaneamiento.INVL_NET = dr.GetDecimal(dr.GetOrdinal("INVL_NET"));
                        if (est == 2)
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CANCELACION")))
                                licPlaneamiento.FECHA_CANCELACION = dr.GetDateTime(dr.GetOrdinal("FECHA_CANCELACION"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                            licPlaneamiento.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));

                        lista.Add(licPlaneamiento);
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// OBTIENE LAS FACTURAS GENERADAS PARA UNA PLANIFICACION ASOCIADA A LA LICENCVIA.
        /// </summary>
        /// <param name="idPlanificacion"></param>
        /// <returns></returns>
        public List<BELicenciaPlaneamientoDetalle> FacturaXPlanificacion(decimal idPlanificacion)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_FACTURAS_X_PLANIFICACION");
            db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, idPlanificacion);
            db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BELicenciaPlaneamientoDetalle>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BELicenciaPlaneamientoDetalle factura = null;
                while (dr.Read())
                {
                    factura = new BELicenciaPlaneamientoDetalle();
                    factura.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                    factura.LIC_PL_ID_SQ = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID_SQ"));
                    factura.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                    factura.LIC_INVOICE_VAL = dr.GetDecimal(dr.GetOrdinal("LIC_INVOICE_VAL"));
                    factura.LIC_INVOICE_LINE = dr.GetDecimal(dr.GetOrdinal("LIC_INVOICE_LINE"));
                    factura.LIC_DATEI = dr.GetDateTime(dr.GetOrdinal("LIC_DATEI"));
                    factura.LIC_PL_PARTIAL = dr.GetBoolean(dr.GetOrdinal("LIC_PL_PARTIAL"));
                    lista.Add(factura);
                }
            }
            return lista;
        }

        #region FACTURACION_MANUAL
        public int InsertarBorradorManual(string xml, string owner)
        {
            //bool exito = false;
            int id = 0;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASI_INVOICES_GRAL_BORRADOR_MANUAL"))
                {
                    oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddOutParameter(cm, "@INV_ID", DbType.Decimal, 0);
                    //exito = oDataBase.ExecuteNonQuery(cm) > 0;
                    int r = oDataBase.ExecuteNonQuery(cm);
                    id = Convert.ToInt32(oDataBase.GetParameterValue(cm, "@INV_ID"));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return id;
        }

        #endregion

        public List<BEValoresConfig> ListaTipoFacturacionManual()
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_TIPO_FACTURACION_INDIVIDUAL");
            var lista = new List<BEValoresConfig>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEValoresConfig valor = null;
                while (dr.Read())
                {
                    valor = new BEValoresConfig();
                    valor.VALUE = dr.GetString(dr.GetOrdinal("VALUE"));
                    valor.VDESC = dr.GetString(dr.GetOrdinal("VDESC"));
                    lista.Add(valor);
                }
            }
            return lista;
        }



        public int ActualizarReferenciaNotaCreditoFactura2(string owner, decimal idFact, decimal idFactNew, string user)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_REF_INVOCICE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFact);
            db.AddInParameter(oDbCommand, "@INV_CN_REF", DbType.Decimal, idFactNew);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEFactura> ObtenerTipoDocumento(decimal id)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_TIPO_DOCUMENTO_IDENTIDAD");
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, id);
            var lista = new List<BEFactura>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEFactura valor = null;
                while (dr.Read())
                {
                    valor = new BEFactura();
                    valor.TAXT_ID = dr.GetDecimal(dr.GetOrdinal("TAXT_ID"));
                    lista.Add(valor);
                }
            }
            return lista;
        }

        public List<BEFactura> ObtenerTipoComprobante(decimal serie)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_TIPO_DOCUMENTO");
            db.AddInParameter(oDbCommand, "@NMR_ID", DbType.Decimal, serie);
            var lista = new List<BEFactura>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEFactura valor = null;
                while (dr.Read())
                {
                    valor = new BEFactura();
                    valor.TIPO_FACT = dr.GetString(dr.GetOrdinal("NMR_TYPE"));
                    lista.Add(valor);
                }
            }
            return lista;
        }

        public int LimpiarBorradores(decimal idoficina)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_FACTURA_BORRADOR");
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idoficina);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int LimpiarBorradorexLicencia(decimal LIC_ID)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_FACTURA_BORRADOR_X_LICENCIA");
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }


        public int ValidarPermisoEmisionMensual(decimal idoficina)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_PERMISO_EMISION_MENSUAL");
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idoficina);
            int r = Convert.ToInt32( db.ExecuteScalar(oDbCommand));
            return r;
        }

        #region   FACTURACION MASIVA SUNAT XML

        public List<BECabeceraFactura> ListaCabezeraMasivaSunat(string owner, string xml)
        {
            List<BECabeceraFactura> lista = new List<BECabeceraFactura>();
            BECabeceraFactura item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_INTERFAZ_EMISION_CAB_MASIVA_PRUEBASUNAT"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@lstXml", DbType.Xml, xml);

                cm.CommandTimeout = 16000;
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BECabeceraFactura();
                        item.INV_ID= dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        item.TipoDTE = dr.GetString(dr.GetOrdinal("TipoDTE"));
                        item.Serie = dr.GetString(dr.GetOrdinal("Serie"));
                        item.Correlativo = dr.GetString(dr.GetOrdinal("Correlativo"));
                        item.FChEmis = dr.GetString(dr.GetOrdinal("FChEmis")).Substring(0, 10);
                        item.FChVen = dr.GetString(dr.GetOrdinal("FChVen"));
                        item.TipoMoneda = dr.GetString(dr.GetOrdinal("TipoMoneda"));
                        item.RUTEmisor = dr.GetString(dr.GetOrdinal("RUTEmisor"));
                        item.TipoRucEmis = dr.GetString(dr.GetOrdinal("TipoRucEmis"));
                        item.RznSocEmis = dr.GetString(dr.GetOrdinal("RznSocEmis"));
                        item.NomComer = dr.GetString(dr.GetOrdinal("NomComer"));
                        item.DirEmis = dr.GetString(dr.GetOrdinal("DirEmis"));
                        item.CodiComu = dr.GetString(dr.GetOrdinal("CodiComu"));
                        item.TipoRUTRecep = dr.GetString(dr.GetOrdinal("TipoRUTRecep"));
                        item.CodiUsuario = dr.GetString(dr.GetOrdinal("CodiUsuario"));
                        item.RUTRecep = dr.GetString(dr.GetOrdinal("RUTRecep"));
                        item.RznSocRecep = dr.GetString(dr.GetOrdinal("RznSocRecep"));
                        item.DirRecep = dr.GetString(dr.GetOrdinal("DirRecep"));
                        item.Grupo = dr.GetString(dr.GetOrdinal("Grupo"));
                        item.CorreoUsuario = dr.GetString(dr.GetOrdinal("CorreoUsuario"));
                        item.MntNeto = dr.GetDecimal(dr.GetOrdinal("MntNeto"));
                        item.MntExe = dr.GetDecimal(dr.GetOrdinal("MntExe"));
                        item.MntExo = dr.GetDecimal(dr.GetOrdinal("MntExo"));
                        item.MntTotal = dr.GetDecimal(dr.GetOrdinal("MntTotal"));
                        item.TipoOper = dr.GetString(dr.GetOrdinal("TipoOper"));
                        item.OficinaRecaudo = dr.GetString(dr.GetOrdinal("OficinaRecaudo"));
                        item.EsManual = dr.GetInt32(dr.GetOrdinal("EsManual"));
                        item.HoraEmision = dr.GetString(dr.GetOrdinal("FChEmis")).Substring(11, 8);
                        item.CodigoLocal = "0000";
                        item.FormaPago = dr.GetString(dr.GetOrdinal("FormaPago"));
                        item.MontoNetoPendPago = dr.GetDecimal(dr.GetOrdinal("MontoNetoPendPago"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }

        public List<BEDetalleFactura> ListaDetalleaMasivaSunat(string owner, string xml)
        {
            List<BEDetalleFactura> lista = new List<BEDetalleFactura>();
            BEDetalleFactura item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_DET_EMISION_INTERFAZ_MASIVA"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@lstXml", DbType.Xml, xml);
                cm.CommandTimeout = 16000;
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEDetalleFactura();
                        item.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        item.NroLinDet = dr.GetString(dr.GetOrdinal("NroLinDet"));
                        item.QtyItem = dr.GetString(dr.GetOrdinal("QtyItem"));
                        item.VlrCodigo = dr.GetString(dr.GetOrdinal("VlrCodigo"));
                        item.NmbItem = dr.GetString(dr.GetOrdinal("NmbItem"));
                        item.PrcItem = dr.GetDecimal(dr.GetOrdinal("PrcItem"));
                        item.DescuentoMonto = dr.GetDecimal(dr.GetOrdinal("DescuentoMonto"));
                        item.PrcItemSinIgv = dr.GetDecimal(dr.GetOrdinal("PrcItemSinIgv"));
                        item.DescuentoMonto = dr.GetDecimal(dr.GetOrdinal("DescuentoMonto"));
                        item.MontoItem = dr.GetDecimal(dr.GetOrdinal("MontoItem"));
                        item.Observacion = dr.GetString(dr.GetOrdinal("Observacion"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }
        #endregion

        public int ValidaSerieNCDocumentoAplicar(decimal NMR_ID, decimal INV_ID)
        {
            int r = 0;
            using (DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_VALIDA_SERIE_NC_A_DOCUMENTO_APLICAR"))
            {
                oDataBase.AddInParameter(oDbCommand, "@NMR_ID", DbType.Decimal, NMR_ID);
                oDataBase.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, INV_ID);

                r=Convert.ToInt32( oDataBase.ExecuteScalar(oDbCommand));
            }
            return r;
        }
        public int ValidaQuiebra(decimal INV_ID)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("Valida_Quiebra");
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, INV_ID);
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }
        public int EnviarQuiebra(decimal INV_ID, string OBS, string USER)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("Enviar_Quiebra");
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, INV_ID);
            db.AddInParameter(oDbCommand, "@OBS", DbType.String, OBS);
            db.AddInParameter(oDbCommand, "@USER", DbType.String, USER);
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        public int ObtenerDiaMinimoFechaManual()
        {
            Database db = new DatabaseProviderFactory().Create("conexion");

            int r = 0;
            using (DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DIA_MIN_FECHA_MANUAL"))
            {


                r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            }
            return r;
        }

        #region Anula NC (REVERT)

        public int AnularNCRevert(decimal INV_ID,string observacion)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");

            int r = 0;
            using (DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRASU_ANULA_NC_ELECTRONICA"))
            {
                db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, INV_ID);
                db.AddInParameter(oDbCommand, "@observacion", DbType.String, observacion);

                r = Convert.ToInt32(db.ExecuteNonQuery(oDbCommand));
            }
            return r;
        }

        #endregion

        #region ValidaEmisionIndividual
        public bool ObtieneValidacionIndividual()
        {
            Database db = new DatabaseProviderFactory().Create("conexion");

            bool r;
            using (DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_VALIDACION_INDIVIDUAL"))
            {
                r = Convert.ToBoolean( db.ExecuteScalar(oDbCommand));
            }
            return r;
        }

        public int ACTUALIZA_LICENCIA_VALIDACION(decimal LIC_ID)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");

            int r;
            using (DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_LICENCIA_VALIDACION"))
            {
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID); 
                 r = (db.ExecuteNonQuery(oDbCommand));
            }
            return r;
        }

        public int ObtienePermisoActualLicencia(decimal LIC_ID)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");

            int r;
            using (DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LICENCIA_VALIDACION"))
            {
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
                r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            }
            return r;
        }
        #endregion

        #region REENVIO MASIVO - EMISION MENSUAL LOCALES Y TRANS 
        public List<BECabeceraFactura> ListaCabezeraMasivaSunatEmiMensualLocTrans(DateTime fechaInicio, DateTime fechaFin ,decimal Oficina)
        {
            List<BECabeceraFactura> lista = new List<BECabeceraFactura>();
            BECabeceraFactura item = null;
            //using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_INTERFAZ_EMISION_CABEZERA_MASIVA_LOC_TRANS"))
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_INTERFAZ_EMISION_CABEZERA_MASIVA_LOC_TRANS_PRUEBASUNAT"))
            {
                oDataBase.AddInParameter(cm, "@FECHAINICIO", DbType.DateTime, fechaInicio);
                oDataBase.AddInParameter(cm, "@FECHAFIN", DbType.DateTime, fechaFin);
                oDataBase.AddInParameter(cm, "@OFICINA", DbType.Decimal, Oficina);

                cm.CommandTimeout = 16000;
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BECabeceraFactura();
                        item.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        item.TipoDTE = dr.GetString(dr.GetOrdinal("TipoDTE"));
                        item.Serie = dr.GetString(dr.GetOrdinal("Serie"));
                        item.Correlativo = dr.GetString(dr.GetOrdinal("Correlativo"));
                        item.FChEmis = dr.GetString(dr.GetOrdinal("FChEmis")).Substring(0, 10);
                        item.FChVen = dr.GetString(dr.GetOrdinal("FChVen"));
                        item.TipoMoneda = dr.GetString(dr.GetOrdinal("TipoMoneda"));
                        item.RUTEmisor = dr.GetString(dr.GetOrdinal("RUTEmisor"));
                        item.TipoRucEmis = dr.GetString(dr.GetOrdinal("TipoRucEmis"));
                        item.RznSocEmis = dr.GetString(dr.GetOrdinal("RznSocEmis"));
                        item.NomComer = dr.GetString(dr.GetOrdinal("NomComer"));
                        item.DirEmis = dr.GetString(dr.GetOrdinal("DirEmis"));
                        item.CodiComu = dr.GetString(dr.GetOrdinal("CodiComu"));
                        item.TipoRUTRecep = dr.GetString(dr.GetOrdinal("TipoRUTRecep"));
                        item.CodiUsuario = dr.GetString(dr.GetOrdinal("CodiUsuario"));
                        item.RUTRecep = dr.GetString(dr.GetOrdinal("RUTRecep"));
                        item.RznSocRecep = dr.GetString(dr.GetOrdinal("RznSocRecep"));
                        item.DirRecep = dr.GetString(dr.GetOrdinal("DirRecep"));
                        item.Grupo = dr.GetString(dr.GetOrdinal("Grupo"));
                        item.CorreoUsuario = dr.GetString(dr.GetOrdinal("CorreoUsuario"));
                        item.MntNeto = dr.GetDecimal(dr.GetOrdinal("MntNeto"));
                        item.MntExe = dr.GetDecimal(dr.GetOrdinal("MntExe"));
                        item.MntExo = dr.GetDecimal(dr.GetOrdinal("MntExo"));
                        item.MntTotal = dr.GetDecimal(dr.GetOrdinal("MntTotal"));
                        item.TipoOper = dr.GetString(dr.GetOrdinal("TipoOper"));
                        item.OficinaRecaudo = dr.GetString(dr.GetOrdinal("OficinaRecaudo"));
                        item.HoraEmision = dr.GetString(dr.GetOrdinal("FChEmis")).Substring(11, 8);
                        item.CodigoLocal = "0000";
                        item.FormaPago = dr.GetString(dr.GetOrdinal("FormaPago"));
                        item.MontoNetoPendPago = dr.GetDecimal(dr.GetOrdinal("MontoNetoPendPago"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }

        public List<BEDetalleFactura> ListaDetalleaMasivaSunatEmisionMensualLocTrans(DateTime fechaInicio, DateTime fechaFin, decimal Oficina)
        {
            List<BEDetalleFactura> lista = new List<BEDetalleFactura>();
            BEDetalleFactura item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_DETALLE_EMISION_INTERFAZ_MASIVA_LOC_TRANS"))
            {
                oDataBase.AddInParameter(cm, "@FECHAINICIO", DbType.DateTime, fechaInicio);
                oDataBase.AddInParameter(cm, "@FECHAFIN", DbType.DateTime, fechaFin);
                oDataBase.AddInParameter(cm, "@OFICINA", DbType.Decimal, Oficina);
                cm.CommandTimeout = 16000;
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEDetalleFactura();
                        item.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        item.NroLinDet = dr.GetString(dr.GetOrdinal("NroLinDet"));
                        item.QtyItem = dr.GetString(dr.GetOrdinal("QtyItem"));
                        item.VlrCodigo = dr.GetString(dr.GetOrdinal("VlrCodigo"));
                        item.NmbItem = dr.GetString(dr.GetOrdinal("NmbItem"));
                        item.PrcItem = dr.GetDecimal(dr.GetOrdinal("PrcItem"));
                        item.DescuentoMonto = dr.GetDecimal(dr.GetOrdinal("DescuentoMonto"));
                        item.PrcItemSinIgv = dr.GetDecimal(dr.GetOrdinal("PrcItemSinIgv"));
                        item.DescuentoMonto = dr.GetDecimal(dr.GetOrdinal("DescuentoMonto"));
                        item.MontoItem = dr.GetDecimal(dr.GetOrdinal("MontoItem"));
                        item.Observacion = dr.GetString(dr.GetOrdinal("Observacion"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }
        #endregion

        #region Envio Manuales

        public List<BECabeceraFactura> ListaCabezeraMasivaSunatManuales(DateTime fechaInicio, DateTime fechaFin, decimal Oficina)
        {
            List<BECabeceraFactura> lista = new List<BECabeceraFactura>();
            BECabeceraFactura item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_INTERFAZ_EMISION_CABEZERA_MASIVA_MANUALES"))
            {
                oDataBase.AddInParameter(cm, "@FECHAINICIO", DbType.DateTime, fechaInicio);
                oDataBase.AddInParameter(cm, "@FECHAFIN", DbType.DateTime, fechaFin);
                oDataBase.AddInParameter(cm, "@OFICINA", DbType.Decimal, Oficina);

                cm.CommandTimeout = 16000;
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BECabeceraFactura();
                        item.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        item.TipoDTE = dr.GetString(dr.GetOrdinal("TipoDTE"));
                        item.Serie = dr.GetString(dr.GetOrdinal("Serie"));
                        item.Correlativo = dr.GetString(dr.GetOrdinal("Correlativo"));
                        item.FChEmis = dr.GetString(dr.GetOrdinal("FChEmis"));
                        item.FChVen = dr.GetString(dr.GetOrdinal("FChVen"));
                        item.TipoMoneda = dr.GetString(dr.GetOrdinal("TipoMoneda"));
                        item.RUTEmisor = dr.GetString(dr.GetOrdinal("RUTEmisor"));
                        item.TipoRucEmis = dr.GetString(dr.GetOrdinal("TipoRucEmis"));
                        item.RznSocEmis = dr.GetString(dr.GetOrdinal("RznSocEmis"));
                        item.NomComer = dr.GetString(dr.GetOrdinal("NomComer"));
                        item.DirEmis = dr.GetString(dr.GetOrdinal("DirEmis"));
                        item.CodiComu = dr.GetString(dr.GetOrdinal("CodiComu"));
                        item.TipoRUTRecep = dr.GetString(dr.GetOrdinal("TipoRUTRecep"));
                        item.CodiUsuario = dr.GetString(dr.GetOrdinal("CodiUsuario"));
                        item.RUTRecep = dr.GetString(dr.GetOrdinal("RUTRecep"));
                        item.RznSocRecep = dr.GetString(dr.GetOrdinal("RznSocRecep"));
                        item.DirRecep = dr.GetString(dr.GetOrdinal("DirRecep"));
                        item.Grupo = dr.GetString(dr.GetOrdinal("Grupo"));
                        item.CorreoUsuario = dr.GetString(dr.GetOrdinal("CorreoUsuario"));
                        item.MntNeto = dr.GetDecimal(dr.GetOrdinal("MntNeto"));
                        item.MntExe = dr.GetDecimal(dr.GetOrdinal("MntExe"));
                        item.MntExo = dr.GetDecimal(dr.GetOrdinal("MntExo"));
                        item.MntTotal = dr.GetDecimal(dr.GetOrdinal("MntTotal"));
                        item.TipoOper = dr.GetString(dr.GetOrdinal("TipoOper"));
                        item.OficinaRecaudo = dr.GetString(dr.GetOrdinal("OficinaRecaudo"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }

        #endregion
    }
}

;
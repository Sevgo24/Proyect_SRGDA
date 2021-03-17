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
    public class DAMetodoPago
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEMetodoPago> ListarPaginado(string owner, string param, bool confirmacion, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_METODOPAGO_PAGE");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@REC_AUT", DbType.Boolean, confirmacion);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);
            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEMetodoPago>();
            var item = new BEMetodoPago();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEMetodoPago();
                    item.REC_PWID = dr.GetString(dr.GetOrdinal("REC_PWID"));
                    item.Automaticamente = dr.GetString(dr.GetOrdinal("REC_AUT"));
                    item.REC_PWDESC = dr.GetString(dr.GetOrdinal("REC_PWDESC"));

                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ESTADO = "ACTIVO";
                    else
                        item.ESTADO = "INACTIVO";

                    item.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(item);
                }
            }
            return lista;
        }

        public int Eliminar(BEMetodoPago en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAD_PAYMENT_WAY");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@REC_PWID", DbType.String, en.REC_PWID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Insertar(BEMetodoPago en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAI_PAYMENT_WAY");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@REC_PWID", DbType.String, en.REC_PWID.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@REC_PWDESC", DbType.String, en.REC_PWDESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@REC_AUT", DbType.String, en.REC_AUT);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public BEMetodoPago Obtener(string owner, string id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_Obtener_PAYMENT_WAY");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@REC_PWID", DbType.String, id);

            BEMetodoPago item = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEMetodoPago();
                    item.REC_PWID = dr.GetString(dr.GetOrdinal("REC_PWID"));
                    item.REC_PWDESC = dr.GetString(dr.GetOrdinal("REC_PWDESC")).ToUpper();
                    item.REC_PWDESCAux = dr.GetString(dr.GetOrdinal("REC_PWDESC")).ToUpper();
                    item.REC_AUT = dr.GetBoolean(dr.GetOrdinal("REC_AUT"));
                }
            }
            return item;
        }

        public int Actualizar(BEMetodoPago en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAU_PAYMENT_WAY");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@REC_PWID", DbType.String, en.REC_PWID.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@REC_PWDESC", DbType.String, en.REC_PWDESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@REC_AUT", DbType.String, en.REC_AUT);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ObtenerXDescripcion(BEMetodoPago en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_PAY_DESC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@REC_PWDESC", DbType.String, en.REC_PWDESC);
            oDataBase.AddInParameter(oDbCommand, "@REC_PWID", DbType.String, en.REC_PWID);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public int ObtenerXCodigo(BEMetodoPago en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_PAY_COD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@REC_PWID", DbType.String, en.REC_PWID);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public List<BEMetodoPago> ListarMetodoPago(string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDAS_LISTAR_METODO_PAG");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);

            var lista = new List<BEMetodoPago>();
            BEMetodoPago item;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    item = new BEMetodoPago();
                    item.REC_PWID = Convert.ToString(reader["REC_PWID"]);
                    item.REC_PWDESC = Convert.ToString(reader["REC_PWDESC"]);
                    lista.Add(item);
                }
            }
            return lista;
        }

        // COMPROBANTES DE PAGO
        public List<BEDetalleMetodoPago> ListarDepositosBancarios(string owner, string idBanco, string idTipoPago,
                                                                  string idMoneda, string idEstadoConfirmacion, string CodigoDeposito,
                                                                  int conFecha, DateTime FIni, DateTime FFin, decimal IdBps,
                                                                  string idBancoDestino, string idCuentaDestino, string montoDepositado,
                                                                  int conFechaIngreso, DateTime FIniIngreso, DateTime FFinIngreso, decimal IdOficina,
                                                                  decimal IdVoucher, string CodigoConfirmacion, decimal becEspecial, decimal becEspecialAprobacion, decimal idCobro,
                                                                  int page, int pageSize)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DEPOSITOS_BANCARIOS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);

            oDataBase.AddInParameter(oDbCommand, "@BANCO", DbType.String, idBanco);
            oDataBase.AddInParameter(oDbCommand, "@TIPO_PAGO", DbType.String, idTipoPago);
            oDataBase.AddInParameter(oDbCommand, "@MONEDA", DbType.String, idMoneda);
            oDataBase.AddInParameter(oDbCommand, "@ESTADO_CONFIR", DbType.String, idEstadoConfirmacion);
            oDataBase.AddInParameter(oDbCommand, "@VOUCHER", DbType.String, CodigoDeposito);
            oDataBase.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, conFecha);
            oDataBase.AddInParameter(oDbCommand, "@FINI", DbType.DateTime, FIni);
            oDataBase.AddInParameter(oDbCommand, "@FFIN", DbType.DateTime, FFin);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.String, IdBps);
            oDataBase.AddInParameter(oDbCommand, "@BANCO_DESTINO", DbType.String, idBancoDestino);
            oDataBase.AddInParameter(oDbCommand, "@CUENTA_DESTINO", DbType.String, idCuentaDestino);
            oDataBase.AddInParameter(oDbCommand, "@MONTO", DbType.String, montoDepositado);

            oDataBase.AddInParameter(oDbCommand, "@CON_FECHA_ING", DbType.Int32, conFechaIngreso);
            oDataBase.AddInParameter(oDbCommand, "@FINI_ING", DbType.DateTime, FIniIngreso);
            oDataBase.AddInParameter(oDbCommand, "@FFIN_ING", DbType.DateTime, FFinIngreso);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, IdOficina);
            oDataBase.AddInParameter(oDbCommand, "@REC_PID", DbType.Decimal, IdVoucher);
            oDataBase.AddInParameter(oDbCommand, "@REC_CODECONFIRMED", DbType.String, CodigoConfirmacion);
            oDataBase.AddInParameter(oDbCommand, "@BEC_ESPECIAL", DbType.Decimal, becEspecial);
            oDataBase.AddInParameter(oDbCommand, "@REC_BEC_ESPECIAL_APPROVED", DbType.Decimal, becEspecialAprobacion);
            oDataBase.AddInParameter(oDbCommand, "@MREC_ID", DbType.Decimal, idCobro);

            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, page);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, pageSize);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 0);
            //oDataBase.ExecuteNonQuery(oDbCommand);

            //string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));
            var lista = new List<BEDetalleMetodoPago>();

            oDbCommand.CommandTimeout = 1800;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                BEDetalleMetodoPago depositoBancario = null;
                while (dr.Read())
                {
                    depositoBancario = new BEDetalleMetodoPago();
                    depositoBancario.REC_PID = dr.GetDecimal(dr.GetOrdinal("REC_PID"));
                    depositoBancario.BNK_ID = dr.GetString(dr.GetOrdinal("BNK_ID"));
                    depositoBancario.BNK_NAME = dr.GetString(dr.GetOrdinal("BNK_NAME"));
                    depositoBancario.CTA_CLIENTE = dr.GetString(dr.GetOrdinal("NUM_CUENTA"));
                    depositoBancario.REC_PWID = dr.GetString(dr.GetOrdinal("REC_PWID"));

                    depositoBancario.REC_PWDESC = dr.GetString(dr.GetOrdinal("REC_PWDESC"));
                    depositoBancario.FECHA_DEP = String.Format("{0:dd/MM/yyyy}", dr.GetString(dr.GetOrdinal("FECHA_DEP")));
                    depositoBancario.REC_REFERENCE = dr.GetString(dr.GetOrdinal("REC_REFERENCE"));
                    //depositoBancario.REC_PVALUE = dr.GetDecimal(dr.GetOrdinal("REC_PVALUE")); //depositoBancario.REC_PVALUE.ToString("### ##0.000");
                    depositoBancario.MONTO_DEPOSITADO = dr.GetDecimal(dr.GetOrdinal("REC_PVALUE")).ToString("###,###,###.#0");
                    depositoBancario.REC_CONFIRMED = dr.GetString(dr.GetOrdinal("REC_CONFIRMED"));

                    depositoBancario.ESTADO_DEPOSITO = dr.GetString(dr.GetOrdinal("ESTADO_DEPOSITO"));
                    depositoBancario.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    depositoBancario.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    depositoBancario.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                    depositoBancario.MONEDA = dr.GetString(dr.GetOrdinal("MONEDA"));

                    depositoBancario.BANCO_DESTINO = dr.GetString(dr.GetOrdinal("BANCO_DESTINO"));
                    depositoBancario.CUENTA_DESTINO = dr.GetString(dr.GetOrdinal("CUENTA_DESTINO"));
                    depositoBancario.OFICINA_RECAUDO = dr.GetString(dr.GetOrdinal("OFICINA_RECAUDO"));
                    depositoBancario.FECHA_INGRESO = dr.GetString(dr.GetOrdinal("FECHA_INGRESO"));
                    if (depositoBancario.REC_CONFIRMED == "C")
                        depositoBancario.REC_CODECONFIRMED = dr.GetString(dr.GetOrdinal("REC_CODECONFIRMED"));
                    else
                        depositoBancario.REC_CODECONFIRMED = "";

                    if (depositoBancario.REC_CONFIRMED == "C")
                        depositoBancario.FECHA_CONFIRMACION = dr.GetString(dr.GetOrdinal("FECHA_CONFIRMACION"));
                    else
                        depositoBancario.FECHA_CONFIRMACION = "";

                    depositoBancario.MREC_ID = dr.GetDecimal(dr.GetOrdinal("MREC_ID"));
                    depositoBancario.SALDO_MONTO_DEPOSITADO = dr.GetDecimal(dr.GetOrdinal("REC_BALANCE")).ToString("###,###,###.#0");

                    depositoBancario.TotalVirtual = dr.GetInt32(dr.GetOrdinal("RecordCount"));
                    depositoBancario.DOC_ID = dr.GetDecimal(dr.GetOrdinal("DOC_ID"));
                    depositoBancario.DOC_VERSION = dr.GetDecimal(dr.GetOrdinal("DOC_VERSION"));

                    if (!dr.IsDBNull(dr.GetOrdinal("REC_BEC_ESPECIAL")))
                        depositoBancario.REC_BEC_ESPECIAL = dr.GetDecimal(dr.GetOrdinal("REC_BEC_ESPECIAL"));
                    else
                        depositoBancario.REC_BEC_ESPECIAL = 0;

                    if (!dr.IsDBNull(dr.GetOrdinal("REC_BEC_ESPECIAL_APPROVED")))
                        depositoBancario.REC_BEC_ESPECIAL_APPROVED = dr.GetDecimal(dr.GetOrdinal("REC_BEC_ESPECIAL_APPROVED"));
                    else
                        depositoBancario.REC_BEC_ESPECIAL_APPROVED = 0;


                    lista.Add(depositoBancario);
                }
            }
            return lista;
        }

        //APLICAR ACTUALIZACION DE LAS FACTURAS - COMPROBANTES BANCARIOS
        public List<BEReciboDetalle> ObtenerRecibosDetalle_x_Comprobante(string owner, decimal id)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_RECIBO_DETALLE_X_COMPROBANTE");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@REC_PID", DbType.Decimal, id);

            List<BEReciboDetalle> listaRecibo = new List<BEReciboDetalle>();
            BEReciboDetalle recibo;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    recibo = new BEReciboDetalle();
                    recibo.OWNER = Convert.ToString(reader["OWNER"]);
                    recibo.MREC_ID = Convert.ToDecimal(reader["MREC_ID"]);
                    recibo.REC_DID = Convert.ToDecimal(reader["REC_DID"]);
                    recibo.REC_ID = Convert.ToDecimal(reader["REC_ID"]);
                    recibo.INV_ID = Convert.ToDecimal(reader["INV_ID"]);
                    recibo.REC_TOTAL = Convert.ToDecimal(reader["REC_TOTAL"]);
                    recibo.CUR_ALPHA = Convert.ToString(reader["CUR_ALPHA"]);
                    recibo.CUR_VALUE = Convert.ToDecimal(reader["CUR_VALUE"]);
                    recibo.CONVERSION_TOTAL_SOLES = Convert.ToDecimal(reader["CONVERSION_TOTAL_SOLES"]);
                    recibo.CONVERSION_BALANCE_SOLES = Convert.ToDecimal(reader["CONVERSION_BALANCE_SOLES"]);
                    listaRecibo.Add(recibo);
                }
            }
            return listaRecibo;
        }

        public List<BEFactura> ObtenerFactura_x_Comprobante(string owner, decimal id)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_FACTURAS_X_COMPROBANTE");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@REC_PID", DbType.Decimal, id);

            List<BEFactura> listaFactura = new List<BEFactura>();
            BEFactura factura;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    factura = new BEFactura();
                    factura.OWNER = Convert.ToString(reader["OWNER"]);
                    factura.REC_ID = Convert.ToDecimal(reader["REC_ID"]);
                    factura.INV_ID = Convert.ToDecimal(reader["INV_ID"]);
                    factura.CUR_ALPHA = Convert.ToString(reader["CUR_ALPHA"]);

                    factura.INV_BASE = Convert.ToDecimal(reader["INV_BASE"]);
                    factura.INV_TAXES = Convert.ToDecimal(reader["INV_TAXES"]);
                    factura.INV_DISCOUNTS = Convert.ToDecimal(reader["INV_DISCOUNTS"]);
                    factura.INV_NET = Convert.ToDecimal(reader["INV_NET"]);
                    factura.INV_BALANCE = Convert.ToDecimal(reader["INV_BALANCE"]);

                    factura.CUR_VALUE = Convert.ToDecimal(reader["CUR_VALUE"]);
                    factura.INV_BASE_SOLES = Convert.ToDecimal(reader["INV_BASE_SOLES"]);
                    factura.INV_TAXES_SOLES = Convert.ToDecimal(reader["INV_TAXES_SOLES"]);
                    factura.INV_DISCOUNTS_SOLES = Convert.ToDecimal(reader["INV_DISCOUNTS_SOLES"]);
                    factura.INV_NET_SOLES = Convert.ToDecimal(reader["INV_NET_SOLES"]);
                    factura.INV_BALANCE_SOLES = Convert.ToDecimal(reader["INV_BALANCE_SOLES"]);
                    factura.FECHA_CANCELACION = Convert.ToDateTime(reader["FECHA_CANCELACION"]);
                    listaFactura.Add(factura);
                }
            }
            return listaFactura;
        }

        public List<BEFacturaDetalle> ObtenerFacturaDetalle_x_Comprobante(string owner, decimal id)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_FACTURAS_DETALLE_X_COMPROBANTE");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@REC_PID", DbType.Decimal, id);

            List<BEFacturaDetalle> listaFactDetalle = new List<BEFacturaDetalle>();
            BEFacturaDetalle factDetalle;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    factDetalle = new BEFacturaDetalle();
                    factDetalle.OWNER = Convert.ToString(reader["OWNER"]);
                    factDetalle.REC_ID = Convert.ToDecimal(reader["REC_ID"]);
                    factDetalle.INV_ID = Convert.ToDecimal(reader["INV_ID"]);
                    factDetalle.INVL_ID = Convert.ToDecimal(reader["INVL_ID"]);
                    factDetalle.LIC_ID = Convert.ToDecimal(reader["LIC_ID"]);
                    factDetalle.CUR_ALPHA = Convert.ToString(reader["CUR_ALPHA"]);

                    factDetalle.INVL_GROSS = Convert.ToDecimal(reader["INVL_GROSS"]);
                    factDetalle.INVL_TAXES = Convert.ToDecimal(reader["INVL_TAXES"]);
                    factDetalle.INVL_DISC = Convert.ToDecimal(reader["INVL_DISC"]);
                    factDetalle.INVL_NET = Convert.ToDecimal(reader["INVL_NET"]);
                    factDetalle.INVL_BALANCE = Convert.ToDecimal(reader["INVL_BALANCE"]);

                    factDetalle.CUR_VALUE = Convert.ToDecimal(reader["CUR_VALUE"]);
                    factDetalle.INVL_GROSS_SOLES = Convert.ToDecimal(reader["INVL_GROSS_SOLES"]);
                    factDetalle.INVL_TAXES_SOLES = Convert.ToDecimal(reader["INVL_TAXES_SOLES"]);
                    factDetalle.INVL_DISC_SOLES = Convert.ToDecimal(reader["INVL_DISC_SOLES"]);
                    factDetalle.INVL_NET_SOLES = Convert.ToDecimal(reader["INVL_NET_SOLES"]);
                    factDetalle.INVL_BALANCE_SOLES = Convert.ToDecimal(reader["INVL_BALANCE_SOLES"]);
                    listaFactDetalle.Add(factDetalle);
                }
            }
            return listaFactDetalle;
        }

        //public int ActualizarFacturaCab_x_Cobro(BEFactura factura)
        //{
        //    int result = 0;
        //    try
        //    {
        //        DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_FACTURAS_X_COBRO");
        //        oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, factura.OWNER);
        //        oDataBase.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, factura.INV_ID);
        //        oDataBase.AddInParameter(oDbCommand, "@INV_COLLECTB", DbType.Decimal, factura.INV_COLLECTB);
        //        oDataBase.AddInParameter(oDbCommand, "@INV_COLLECTT", DbType.Decimal, factura.INV_COLLECTT);
        //        oDataBase.AddInParameter(oDbCommand, "@INV_COLLECTD", DbType.Decimal, factura.INV_COLLECTD);
        //        oDataBase.AddInParameter(oDbCommand, "@INV_COLLECTN", DbType.Decimal, factura.INV_COLLECTN);
        //        result = Convert.ToInt32(oDataBase.ExecuteNonQuery(oDbCommand));
        //    }
        //    catch (Exception ex)
        //    {
        //        result = 0;
        //    }
        //    return result;
        //}

        //public int ActualizarFacturaDet_x_Cobro(BEFacturaDetalle detalle)
        //{
        //    int result = 0;
        //    try
        //    {
        //        DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_FACTURAS_DETALLE_X_COBRO");
        //        oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, detalle.OWNER);
        //        oDataBase.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, detalle.INV_ID);
        //        oDataBase.AddInParameter(oDbCommand, "@INVL_ID", DbType.Decimal, detalle.INVL_ID);
        //        oDataBase.AddInParameter(oDbCommand, "@INVL_COLLECTB", DbType.Decimal, detalle.INVL_COLLECTB);
        //        oDataBase.AddInParameter(oDbCommand, "@INVL_COLLECTT", DbType.Decimal, detalle.INVL_COLLECTT);
        //        oDataBase.AddInParameter(oDbCommand, "@INVL_COLLECTD", DbType.Decimal, detalle.INVL_COLLECTD);
        //        oDataBase.AddInParameter(oDbCommand, "@INVL_COLLECTN", DbType.Decimal, detalle.INVL_COLLECTN);
        //        result = Convert.ToInt32(oDataBase.ExecuteNonQuery(oDbCommand));
        //    }
        //    catch (Exception ex)
        //    {
        //        result = 0;
        //    }
        //    return result;
        //}


        public int ActualizarFacturaCab_x_CobroXML(string xml)
        {
            int result = 0;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASU_FACTURAS_X_COBRO_XML"))
                {
                    oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    result = oDataBase.ExecuteNonQuery(cm);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public int ActualizarFacturaDetalle_x_Cobro_XML(string xml)
        {
            int result = 0;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASU_FACTURAS_DETALLE_X_COBRO_XML"))
                {
                    oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    result = oDataBase.ExecuteNonQuery(cm);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public int ActualizarReciboDetalleFact_x_CobroXML(string xml)
        {
            int result = 0;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASU_RECIBO_DETALLE_BALANCE_XML"))
                {
                    oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    result = oDataBase.ExecuteNonQuery(cm);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public int RegistrarDetalleCobro_x_DepositoXML(string xml)
        {
            int result = 0;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASI_DETALLE_COBRO_X_DEPOSITO_XML"))
                {
                    oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    result = oDataBase.ExecuteNonQuery(cm);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }


        public List<BELicenciaDivision> ObtenerOficinaXLicencia(string owner, string xml)
        {
            List<BELicenciaDivision> ListaLicenciaDivision = new List<BELicenciaDivision>();
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DIVISION_OFICINA_X_LICENCIA"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@xmlLst", DbType.Xml, xml);
                    using (IDataReader reader = oDataBase.ExecuteReader(cm))
                    {
                        BELicenciaDivision LicDivision;
                        while (reader.Read())
                        {
                            LicDivision = new BELicenciaDivision();
                            LicDivision.LIC_ID = Convert.ToDecimal(reader["LIC_ID"]);
                            LicDivision.DAD_ID = Convert.ToDecimal(reader["DAD_ID"]);
                            LicDivision.OFF_ID = Convert.ToDecimal(reader["OFF_ID"]);
                            ListaLicenciaDivision.Add(LicDivision);
                        }
                    }
                }
                return ListaLicenciaDivision;
            }
            catch (Exception ex)
            {
                return ListaLicenciaDivision;
            }
        }

        public int ActualizarDepositoBecEspecial(BEDetalleMetodoPago en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTUALIZAR_BEC_ESPECIAL");
            oDataBase.AddInParameter(oDbCommand, "@REC_PID", DbType.Decimal, en.REC_PID);
            oDataBase.AddInParameter(oDbCommand, "@REC_BEC_ESPECIAL_APPROVED", DbType.Decimal, en.REC_BEC_ESPECIAL_APPROVED);
            oDataBase.AddInParameter(oDbCommand, "@REC_BEC_ESPECIAL_OBSERVATION", DbType.String, en.REC_BEC_ESPECIAL_OBSERVATION);
            oDataBase.AddInParameter(oDbCommand, "@REC_BEC_ESPECIAL_USER_APPROVED", DbType.String, en.REC_BEC_ESPECIAL_USER_APPROVED);
            int r = Convert.ToInt32(oDataBase.ExecuteNonQuery(oDbCommand));
            return r;
        }


        public List<BEDetalleMetodoPago> ObtenerPermisoXoficina(decimal idOficina)
        {
            List<BEDetalleMetodoPago> ListaPermnisoxOficina = new List<BEDetalleMetodoPago>();
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_PERMISO_X_OFICINA_DEPOSITOS"))
                {
                    oDataBase.AddInParameter(cm, "@ID_OFICINA", DbType.Decimal, idOficina);
                    using (IDataReader reader = oDataBase.ExecuteReader(cm))
                    {
                        BEDetalleMetodoPago permiso;
                        while (reader.Read())
                        {
                            permiso = new BEDetalleMetodoPago();
                            permiso.PERMISO = Convert.ToString(reader["PERMISO"]);
                            ListaPermnisoxOficina.Add(permiso);
                        }
                    }
                }
                return ListaPermnisoxOficina;
            }
            catch (Exception ex)
            {
                return ListaPermnisoxOficina;
            }
        }



        // COMPROBANTES DE PAGO
        public List<BEDetalleMetodoPago> ListarDepositosBancarios_Reporte(string owner, string idBanco, string idTipoPago,
                                                                  string idMoneda, string idEstadoConfirmacion, string CodigoDeposito,
                                                                  int conFecha, DateTime? FIni, DateTime? FFin, decimal IdBps,
                                                                  string idBancoDestino, string idCuentaDestino, string montoDepositado,
                                                                  int conFechaIngreso, DateTime? FIniIngreso, DateTime? FFinIngreso, decimal IdOficina,
                                                                  decimal IdVoucher, string CodigoConfirmacion, decimal becEspecial, decimal becEspecialAprobacion, decimal idCobro
                                                                    )
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DEPOSITOS_BANCARIOS_REPORTE");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);

            oDataBase.AddInParameter(oDbCommand, "@BANCO", DbType.String, idBanco);
            oDataBase.AddInParameter(oDbCommand, "@TIPO_PAGO", DbType.String, idTipoPago);
            oDataBase.AddInParameter(oDbCommand, "@MONEDA", DbType.String, idMoneda);
            oDataBase.AddInParameter(oDbCommand, "@ESTADO_CONFIR", DbType.String, idEstadoConfirmacion);
            oDataBase.AddInParameter(oDbCommand, "@VOUCHER", DbType.String, CodigoDeposito);
            oDataBase.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, conFecha);
            oDataBase.AddInParameter(oDbCommand, "@FINI", DbType.DateTime, FIni);
            oDataBase.AddInParameter(oDbCommand, "@FFIN", DbType.DateTime, FFin);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.String, IdBps);
            oDataBase.AddInParameter(oDbCommand, "@BANCO_DESTINO", DbType.String, idBancoDestino);
            oDataBase.AddInParameter(oDbCommand, "@CUENTA_DESTINO", DbType.String, idCuentaDestino);
            oDataBase.AddInParameter(oDbCommand, "@MONTO", DbType.String, montoDepositado);

            oDataBase.AddInParameter(oDbCommand, "@CON_FECHA_ING", DbType.Int32, conFechaIngreso);
            oDataBase.AddInParameter(oDbCommand, "@FINI_ING", DbType.DateTime, FIniIngreso);
            oDataBase.AddInParameter(oDbCommand, "@FFIN_ING", DbType.DateTime, FFinIngreso);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, IdOficina);
            oDataBase.AddInParameter(oDbCommand, "@REC_PID", DbType.Decimal, IdVoucher);
            oDataBase.AddInParameter(oDbCommand, "@REC_CODECONFIRMED", DbType.String, CodigoConfirmacion);
            oDataBase.AddInParameter(oDbCommand, "@BEC_ESPECIAL", DbType.Decimal, becEspecial);
            oDataBase.AddInParameter(oDbCommand, "@REC_BEC_ESPECIAL_APPROVED", DbType.Decimal, becEspecialAprobacion);
            oDataBase.AddInParameter(oDbCommand, "@MREC_ID", DbType.Decimal, idCobro);

            decimal id = 0;
            try
            {
                var lista = new List<BEDetalleMetodoPago>();
                oDbCommand.CommandTimeout = 1800;
                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    BEDetalleMetodoPago depositoBancario = null;
                    while (dr.Read())
                    {
                        depositoBancario = new BEDetalleMetodoPago();
                        depositoBancario.REC_PID = dr.GetDecimal(dr.GetOrdinal("REC_PID"));
                        id = dr.GetDecimal(dr.GetOrdinal("REC_PID"));
                        depositoBancario.BNK_ID = dr.GetString(dr.GetOrdinal("BNK_ID"));
                        depositoBancario.BNK_NAME = dr.GetString(dr.GetOrdinal("BNK_NAME"));
                        depositoBancario.CTA_CLIENTE = dr.GetString(dr.GetOrdinal("NUM_CUENTA"));
                        depositoBancario.REC_PWID = dr.GetString(dr.GetOrdinal("REC_PWID"));

                        depositoBancario.REC_PWDESC = dr.GetString(dr.GetOrdinal("REC_PWDESC"));
                        depositoBancario.FECHA_DEP = String.Format("{0:dd/MM/yyyy}", dr.GetString(dr.GetOrdinal("FECHA_DEP")));
                        depositoBancario.REC_REFERENCE = dr.GetString(dr.GetOrdinal("REC_REFERENCE"));
                        depositoBancario.REC_PVALUE = dr.GetDecimal(dr.GetOrdinal("REC_PVALUE")); //depositoBancario.REC_PVALUE.ToString("### ##0.000");
                                                                                                  //depositoBancario.MONTO_DEPOSITADO = dr.GetDecimal(dr.GetOrdinal("REC_PVALUE")).ToString("###,###,###.#0");
                        depositoBancario.REC_CONFIRMED = dr.GetString(dr.GetOrdinal("REC_CONFIRMED"));

                        depositoBancario.ESTADO_DEPOSITO = dr.GetString(dr.GetOrdinal("ESTADO_DEPOSITO"));
                        depositoBancario.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        depositoBancario.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        depositoBancario.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                        depositoBancario.MONEDA = dr.GetString(dr.GetOrdinal("MONEDA"));

                        depositoBancario.BANCO_DESTINO = dr.GetString(dr.GetOrdinal("BANCO_DESTINO"));
                        depositoBancario.CUENTA_DESTINO = dr.GetString(dr.GetOrdinal("CUENTA_DESTINO"));
                        depositoBancario.OFICINA_RECAUDO = dr.GetString(dr.GetOrdinal("OFICINA_RECAUDO"));
                        depositoBancario.FECHA_INGRESO = dr.GetString(dr.GetOrdinal("FECHA_INGRESO"));
                        if (depositoBancario.REC_CONFIRMED == "C")
                            depositoBancario.REC_CODECONFIRMED = dr.GetString(dr.GetOrdinal("REC_CODECONFIRMED"));
                        else
                            depositoBancario.REC_CODECONFIRMED = "";

                        if (depositoBancario.REC_CONFIRMED == "C")
                            depositoBancario.FECHA_CONFIRMACION = dr.GetString(dr.GetOrdinal("FECHA_CONFIRMACION"));
                        else
                            depositoBancario.FECHA_CONFIRMACION = "";

                        depositoBancario.MREC_ID = dr.GetDecimal(dr.GetOrdinal("MREC_ID"));
                        depositoBancario.SALDO_MONTO_DEPOSITADO = dr.GetDecimal(dr.GetOrdinal("REC_BALANCE")).ToString("###,###,###.#0");

                        //depositoBancario.TotalVirtual = dr.GetInt32(dr.GetOrdinal("RecordCount"));
                        depositoBancario.DOC_ID = dr.GetDecimal(dr.GetOrdinal("DOC_ID"));
                        depositoBancario.DOC_VERSION = dr.GetDecimal(dr.GetOrdinal("DOC_VERSION"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REC_BEC_ESPECIAL")))
                            depositoBancario.REC_BEC_ESPECIAL = dr.GetDecimal(dr.GetOrdinal("REC_BEC_ESPECIAL"));
                        else
                            depositoBancario.REC_BEC_ESPECIAL = 0;

                        if (!dr.IsDBNull(dr.GetOrdinal("REC_BEC_ESPECIAL_APPROVED")))
                            depositoBancario.REC_BEC_ESPECIAL_APPROVED = dr.GetDecimal(dr.GetOrdinal("REC_BEC_ESPECIAL_APPROVED"));
                        else
                            depositoBancario.REC_BEC_ESPECIAL_APPROVED = 0;

                        depositoBancario.CUR_VALUE = dr.GetDecimal(dr.GetOrdinal("CUR_VALUE"));


                        lista.Add(depositoBancario);
                    }
                }
                return lista;

            }
            catch (Exception ex)
            {
                var test = id;
                return null;
            }
        }



    }
}

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
using System.Xml;

namespace SGRDA.DA
{
    public class DAFacturaDetalle
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public bool InsertarDetalleBorradorXML(string xml, string owner)
        {
            bool exito = false;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASI_INVOICES_LINES_BORRADOR"))
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

        public List<BEFacturaDetalle> ListarFacturaBorrador_LicPlanemientoSubGrilla(string owner, DateTime fini, DateTime ffin,
                                             decimal tipoLic, string idMoneda,
                                             decimal idGrufact, decimal idBps, decimal idCorreativo,
                                             string idTipoPago, int conFecha, decimal idLic, decimal idFactura,decimal off_id)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_DET_FACTURA_BORRADOR");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@FINI", DbType.DateTime, fini);
            db.AddInParameter(oDbCommand, "@FFIN", DbType.DateTime, ffin);
            db.AddInParameter(oDbCommand, "@LIC_TYPE", DbType.Decimal, tipoLic);
            db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, idMoneda);
            db.AddInParameter(oDbCommand, "@INVG_ID", DbType.Decimal, idGrufact);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, idBps);
            db.AddInParameter(oDbCommand, "@INV_NMR", DbType.Decimal, idCorreativo);
            db.AddInParameter(oDbCommand, "@PAY_ID", DbType.String, idTipoPago);
            db.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, conFecha);
            db.AddInParameter(oDbCommand, "@IDLIC", DbType.Decimal, idLic);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFactura);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, off_id);                
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
                    licPlaneamiento.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    licPlaneamiento.LIC_DATE = dr.GetDateTime(dr.GetOrdinal("LIC_DATE"));

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

                    licPlaneamiento.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                    licPlaneamiento.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));
                    licPlaneamiento.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                    lista.Add(licPlaneamiento);
                }
            }
            return lista;
        }

        public List<BEFacturaDetalle> ListarFacturaPendienteDetalle_subDetalle(string owner, decimal idfact)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_DET_DETALLE_FAC_LIC_DET");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idfact);
            db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEFacturaDetalle>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEFacturaDetalle licPlaneamiento = null;
                while (dr.Read())
                {
                    licPlaneamiento = new BEFacturaDetalle();
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_ID")))
                        licPlaneamiento.INVL_ID = dr.GetDecimal(dr.GetOrdinal("INVL_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                        licPlaneamiento.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        licPlaneamiento.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_DATE")))
                        licPlaneamiento.LIC_DATE = dr.GetDateTime(dr.GetOrdinal("LIC_DATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_BASE")))
                        licPlaneamiento.INVL_BASE = dr.GetDecimal(dr.GetOrdinal("INVL_BASE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_TAXES")))
                        licPlaneamiento.INVL_TAXES = dr.GetDecimal(dr.GetOrdinal("INVL_TAXES"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_NET")))
                        licPlaneamiento.INVL_NET = dr.GetDecimal(dr.GetOrdinal("INVL_NET"));
                    //licPlaneamiento.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));
                    lista.Add(licPlaneamiento);
                }
            }
            return lista;
        }

        public bool ActualizarDetalleFactXML(string owner, string xml)
        {
            bool exito = false;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASU_INVOICES_LINES_BORRADOR"))
                {
                    oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    exito = oDataBase.ExecuteNonQuery(cm) > 0;
                }
                exito = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return exito;
        }

        public bool InsertarValoresCaracteristicaDetalleXML(string xml, string owner)
        {
            bool exito = false;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASI_INVOICES_LINES_CHARAC"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    exito = oDataBase.ExecuteNonQuery(cm) > 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return exito;
        }

        public List<BEFacturaDetalle> ListarFacturaDetalleAplicar(string owner, decimal idfact)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_OBTENER_FACTURA_DET");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idfact);
            db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEFacturaDetalle>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEFacturaDetalle item = null;
                while (dr.Read())
                {
                    item = new BEFacturaDetalle();
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_ID")))
                        item.INVL_ID = dr.GetDecimal(dr.GetOrdinal("INVL_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                        item.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
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
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_COLLECTD")))
                        item.INVL_COLLECTD = dr.GetDecimal(dr.GetOrdinal("INVL_COLLECTD"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_COLLECTN")))
                        item.INVL_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INVL_COLLECTN"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_BALANCE")))
                        item.INVL_BALANCE = dr.GetDecimal(dr.GetOrdinal("INVL_BALANCE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_ORDER")))
                        item.INVL_ORDER = dr.GetDecimal(dr.GetOrdinal("INVL_ORDER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_ID")))
                        item.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_ID")))
                        item.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                        item.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ADD_ID")))
                        item.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RATE_ID")))
                        item.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_COLLECTD")))
                        item.INVL_COLLECTD = dr.GetDecimal(dr.GetOrdinal("INVL_COLLECTD"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_COLLECTN")))
                        item.INVL_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INVL_COLLECTN"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_VAR1")))
                        item.INVL_VAR1 = dr.GetDecimal(dr.GetOrdinal("INVL_VAR1"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_VAR2")))
                        item.INVL_VAR2 = dr.GetDecimal(dr.GetOrdinal("INVL_VAR2"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_VAR3")))
                        item.INVL_VAR3 = dr.GetDecimal(dr.GetOrdinal("INVL_VAR3"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_VAR4")))
                        item.INVL_VAR4 = dr.GetDecimal(dr.GetOrdinal("INVL_VAR4"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_VAR5")))
                        item.INVL_VAR5 = dr.GetDecimal(dr.GetOrdinal("INVL_VAR5"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_VAR6")))
                        item.INVL_VAR6 = dr.GetDecimal(dr.GetOrdinal("INVL_VAR6"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_VAR7")))
                        item.INVL_VAR7 = dr.GetDecimal(dr.GetOrdinal("INVL_VAR7"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_GROSS")))
                        item.INVL_GROSS = dr.GetDecimal(dr.GetOrdinal("INVL_GROSS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_DISC")))
                        item.INVL_DISC = dr.GetDecimal(dr.GetOrdinal("INVL_DISC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_SURC")))
                        item.INVL_SURC = dr.GetDecimal(dr.GetOrdinal("INVL_SURC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_TBASE")))
                        item.INV_TBASE = dr.GetDecimal(dr.GetOrdinal("INV_TBASE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_TTAXES")))
                        item.INV_TTAXES = dr.GetDecimal(dr.GetOrdinal("INV_TTAXES"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_TDEDUCTIONS")))
                        item.INV_TDEDUCTIONS = dr.GetDecimal(dr.GetOrdinal("INV_TDEDUCTIONS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_TNET")))
                        item.INV_TNET = dr.GetDecimal(dr.GetOrdinal("INV_TNET"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_CN_TOTAL")))
                        item.INVL_CN_TOTAL = dr.GetDecimal(dr.GetOrdinal("INVL_CN_TOTAL"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public int AplicarPagoDetalleFactura(BEFacturaDetalle en)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_INVOICE_LINE_PAGO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@INVL_ID", DbType.Decimal, en.INVL_ID);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, en.INV_ID);
            db.AddInParameter(oDbCommand, "@INVL_COLLECTB", DbType.Decimal, en.INVL_COLLECTB);
            db.AddInParameter(oDbCommand, "@INVL_COLLECTT", DbType.Decimal, en.INVL_COLLECTT);
            db.AddInParameter(oDbCommand, "@INVL_COLLECTN", DbType.Decimal, en.INVL_COLLECTN);
            db.AddInParameter(oDbCommand, "@INVL_BALANCE", DbType.Decimal, en.INVL_BALANCE);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            db.AddInParameter(oDbCommand, "@INVL_COLLECTD", DbType.Decimal, en.INVL_COLLECTD);
            db.AddInParameter(oDbCommand, "@INVL_CN_TOTAL", DbType.Decimal, en.INVL_CN_TOTAL);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        //public int InsertarFacturaDetalle(BEFacturaDetalle en)
        //{
        //    Database db = new DatabaseProviderFactory().Create("conexion");
        //    DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAI_FACTURADET");
        //    db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
        //    db.AddInParameter(oDbCommand, "@INV_ID", DbType.String, en.INV_ID);
        //    db.AddInParameter(oDbCommand, "@INVL_ORDER", DbType.Decimal, en.INVL_ORDER);
        //    db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, en.LIC_ID);
        //    db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, en.LIC_PL_ID);
        //    db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, en.MOD_ID);
        //    db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, en.EST_ID);
        //    db.AddInParameter(oDbCommand, "@ADD_ID", DbType.String, en.ADD_ID);
        //    db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, en.RATE_ID);
        //    db.AddInParameter(oDbCommand, "@INVL_VAR1", DbType.Decimal, en.INVL_VAR1);
        //    db.AddInParameter(oDbCommand, "@INVL_VAR2", DbType.Decimal, en.INVL_VAR2);
        //    db.AddInParameter(oDbCommand, "@INVL_VAR3", DbType.Decimal, en.INVL_VAR3);
        //    db.AddInParameter(oDbCommand, "@INVL_VAR4", DbType.Decimal, en.INVL_VAR4);
        //    db.AddInParameter(oDbCommand, "@INVL_VAR5", DbType.Decimal, en.INVL_VAR5);
        //    db.AddInParameter(oDbCommand, "@INVL_VAR6", DbType.String, en.INVL_VAR6);
        //    db.AddInParameter(oDbCommand, "@INVL_VAR7", DbType.Decimal, en.INVL_VAR7);
        //    db.AddInParameter(oDbCommand, "@INVL_GROSS", DbType.Decimal, null);
        //    db.AddInParameter(oDbCommand, "@INVL_DISC", DbType.Decimal, null);
        //    db.AddInParameter(oDbCommand, "@INVL_SURC", DbType.Decimal, null);
        //    db.AddInParameter(oDbCommand, "@INVL_BASE", DbType.Decimal, en.INVL_BASE);
        //    db.AddInParameter(oDbCommand, "@INVL_TAXES", DbType.Decimal, en.INVL_TAXES);
        //    db.AddInParameter(oDbCommand, "@INVL_NET", DbType.String, en.INVL_NET);
        //    db.AddInParameter(oDbCommand, "@INV_TBASE", DbType.Decimal, null);
        //    db.AddInParameter(oDbCommand, "@INV_TTAXES", DbType.Decimal, null);
        //    db.AddInParameter(oDbCommand, "@INV_TDEDUCTIONS", DbType.Decimal, null);
        //    db.AddInParameter(oDbCommand, "@INV_TNET", DbType.Decimal, null);
        //    db.AddInParameter(oDbCommand, "@INVL_COLLECTB", DbType.Decimal, en.INVL_COLLECTB);
        //    db.AddInParameter(oDbCommand, "@INVL_COLLECTT", DbType.Decimal, en.INVL_COLLECTT);
        //    db.AddInParameter(oDbCommand, "@INVL_COLLECTD", DbType.Decimal, null);
        //    db.AddInParameter(oDbCommand, "@INVL_COLLECTN", DbType.Decimal, en.INVL_COLLECTN);
        //    db.AddInParameter(oDbCommand, "@INVL_BALANCE", DbType.Decimal, en.INVL_BALANCE);
        //    db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
        //    int r = db.ExecuteNonQuery(oDbCommand);
        //    return r;
        //}

        public int InsertarFacturaDetalle(BEFacturaDetalle en)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAI_FACTURADET");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.String, en.INV_ID);
            db.AddInParameter(oDbCommand, "@INVL_ORDER", DbType.Decimal, en.INVL_ORDER);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, en.LIC_ID);
            db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, en.LIC_PL_ID);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, en.MOD_ID);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, en.EST_ID);
            db.AddInParameter(oDbCommand, "@ADD_ID", DbType.String, en.ADD_ID);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, en.RATE_ID);
            db.AddInParameter(oDbCommand, "@INVL_VAR1", DbType.Decimal, en.INVL_VAR1);
            db.AddInParameter(oDbCommand, "@INVL_VAR2", DbType.Decimal, en.INVL_VAR2);
            db.AddInParameter(oDbCommand, "@INVL_VAR3", DbType.Decimal, en.INVL_VAR3);
            db.AddInParameter(oDbCommand, "@INVL_VAR4", DbType.Decimal, en.INVL_VAR4);
            db.AddInParameter(oDbCommand, "@INVL_VAR5", DbType.Decimal, en.INVL_VAR5);
            db.AddInParameter(oDbCommand, "@INVL_VAR6", DbType.String, en.INVL_VAR6);
            db.AddInParameter(oDbCommand, "@INVL_VAR7", DbType.Decimal, en.INVL_VAR7);
            db.AddInParameter(oDbCommand, "@INVL_GROSS", DbType.Decimal, en.INVL_GROSS);
            db.AddInParameter(oDbCommand, "@INVL_DISC", DbType.Decimal, en.INVL_DISC);
            db.AddInParameter(oDbCommand, "@INVL_SURC", DbType.Decimal, null);
            db.AddInParameter(oDbCommand, "@INVL_BASE", DbType.Decimal, en.INVL_BASE);
            db.AddInParameter(oDbCommand, "@INVL_TAXES", DbType.Decimal, en.INVL_TAXES);
            db.AddInParameter(oDbCommand, "@INVL_NET", DbType.String, en.INVL_NET);
            db.AddInParameter(oDbCommand, "@INV_TBASE", DbType.Decimal, null);
            db.AddInParameter(oDbCommand, "@INV_TTAXES", DbType.Decimal, null);
            db.AddInParameter(oDbCommand, "@INV_TDEDUCTIONS", DbType.Decimal, null);
            db.AddInParameter(oDbCommand, "@INV_TNET", DbType.Decimal, null);
            db.AddInParameter(oDbCommand, "@INVL_COLLECTB", DbType.Decimal, en.INVL_COLLECTB);
            db.AddInParameter(oDbCommand, "@INVL_COLLECTT", DbType.Decimal, en.INVL_COLLECTT);
            db.AddInParameter(oDbCommand, "@INVL_COLLECTD", DbType.Decimal, null);
            db.AddInParameter(oDbCommand, "@INVL_COLLECTN", DbType.Decimal, en.INVL_COLLECTN);
            db.AddInParameter(oDbCommand, "@INVL_BALANCE", DbType.Decimal, en.INVL_BALANCE);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            db.AddInParameter(oDbCommand, "@INVL_CN_TOTAL", DbType.Decimal, en.INVL_CN_TOTAL);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
        public bool RegistrarDetalleDescuento(string owner, string xml)
        {
            bool exito = false;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASI_DETALLE_DESCUENTO_FACT"))
                {
                    oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    exito = oDataBase.ExecuteNonQuery(cm) > 0;
                }
                exito = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return exito;
        }

        public bool ActualizarDetalleFactura(BEFacturaDetalle detalle)
        {
            bool exito = false;
            //var lista = new List<BEFacturaDetalle>();
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_DETALLE_COMPROBANTE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@INVL_ID", DbType.Decimal, detalle.INVL_ID);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, detalle.INV_ID);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, detalle.LIC_ID);
            db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, detalle.LIC_PL_ID);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, detalle.EST_ID);
            //db.AddInParameter(oDbCommand, "@ADD_ID", DbType.Decimal, detalle.ADD_ID);
            db.AddInParameter(oDbCommand, "@INVL_GROSS", DbType.Decimal, detalle.INVL_GROSS);
            db.AddInParameter(oDbCommand, "@INVL_DISC", DbType.Decimal, detalle.INVL_DISC);
            db.AddInParameter(oDbCommand, "@INVL_BASE", DbType.Decimal, detalle.INVL_BASE);
            db.AddInParameter(oDbCommand, "@INVL_TAXES", DbType.Decimal, detalle.INVL_TAXES);
            db.AddInParameter(oDbCommand, "@INVL_NET", DbType.Decimal, detalle.INVL_NET);
            db.AddInParameter(oDbCommand, "@INVL_BALANCE", DbType.Decimal, detalle.INVL_BALANCE);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, detalle.RATE_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, detalle.LOG_USER_UPDATE);
            exito = db.ExecuteNonQuery(oDbCommand) > 0;
            return exito;
        }

        public bool ActualizarDetalleDescuento(BEFacturaDescuento detalle)
        {
            bool exito = false;
            //var lista = new List<BEFacturaDetalle>();
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_DETALLE_DESCUENTO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, detalle.INV_ID);
            db.AddInParameter(oDbCommand, "@INVL_ID", DbType.Decimal, detalle.INVL_ID);
            db.AddInParameter(oDbCommand, "@DISC_ID", DbType.Decimal, detalle.DISC_ID);
            db.AddInParameter(oDbCommand, "@DISC_SIGN", DbType.String, detalle.DISC_SIGN);
            db.AddInParameter(oDbCommand, "@DISC_VALUE", DbType.Decimal, detalle.DISC_VALUE);
            db.AddInParameter(oDbCommand, "@DISC_ACC", DbType.Decimal, detalle.DISC_ACC);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, detalle.LOG_USER_CREAT);
            exito = db.ExecuteNonQuery(oDbCommand) > 0;
            return exito;
        }

        public bool ActualizarPeriodosFacturandose(string owner, string xml,int OPCION)
        {
            bool exito = false;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASU_DETALLE_PLANEAMIENTO_FACT"))
                {
                    oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@VALOR", DbType.Int32, OPCION);
                    exito = oDataBase.ExecuteNonQuery(cm) > 0;
                }
                exito = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return exito;
        }
    }
}

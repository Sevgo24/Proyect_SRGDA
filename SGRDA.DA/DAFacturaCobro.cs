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
    public class DAFacturaCobro
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEFactura> ListarFacturaPendientePago(string owner, decimal usuDerecho, decimal serie, decimal numero)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_LISTAR_FAC_PEND_PAGO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, usuDerecho);
            db.AddInParameter(oDbCommand, "@INV_NMR", DbType.Decimal, serie);
            db.AddInParameter(oDbCommand, "@INV_NUMBER", DbType.Decimal, numero);
            db.ExecuteNonQuery(oDbCommand);

            var lista = new List<BEFactura>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEFactura factura = null;
                while (dr.Read())
                {
                    factura = new BEFactura();
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
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
                    if (!dr.IsDBNull(dr.GetOrdinal("EstadosPago")))
                        factura.estadosPago = dr.GetString(dr.GetOrdinal("EstadosPago"));
                    if (!dr.IsDBNull(dr.GetOrdinal("NMR_SERIAL")))
                        factura.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_NUMBER")))
                        factura.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("INV_NUMBER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        factura.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    lista.Add(factura);
                }
            }
            return lista;
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
            db.AddInParameter(oDbCommand, "@INV_COLLECTD", DbType.Decimal, en.INV_COLLECTD);
            db.AddInParameter(oDbCommand, "@INV_CN_TOTAL", DbType.Decimal, en.INV_CN_TOTAL);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BEFactura ObtenerFacturaAplicar(string owner, decimal idFactura)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_OBTENER_FAC");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFactura);
            //db.ExecuteNonQuery(oDbCommand);

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
                    //------------------------------------------------------------------//
                    if (!dr.IsDBNull(dr.GetOrdinal("CUR_ALPHA")))
                        factura.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_NMR")))
                        factura.INV_NMR = dr.GetDecimal(dr.GetOrdinal("INV_NMR"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_NUMBER")))
                        factura.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("INV_NUMBER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_DATE")))
                        factura.INV_DATE = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_TYPE")))
                        factura.INV_TYPE = dr.GetDecimal(dr.GetOrdinal("INV_TYPE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_DETAIL")))
                        factura.INV_DETAIL = dr.GetString(dr.GetOrdinal("INV_DETAIL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_PHASE")))
                        factura.INV_PHASE = dr.GetString(dr.GetOrdinal("INV_PHASE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_REPRINTS")))
                        factura.INV_REPRINTS = dr.GetDecimal(dr.GetOrdinal("INV_REPRINTS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_COPIES")))
                        factura.INV_COPIES = dr.GetDecimal(dr.GetOrdinal("INV_COPIES"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        factura.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ADD_ID")))
                        factura.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_PRE")))
                        factura.INV_PRE = dr.GetDecimal(dr.GetOrdinal("INV_PRE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_PROCES")))
                        factura.INV_PROCES = dr.GetDecimal(dr.GetOrdinal("INV_PROCES"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_PRINT_DATE")))
                        factura.INV_PRINT_DATE = dr.GetDateTime(dr.GetOrdinal("INV_PRINT_DATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_REPRINT_DATE")))
                        factura.INV_REPRINT_DATE = dr.GetDateTime(dr.GetOrdinal("INV_REPRINT_DATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_COPY_DATE")))
                        factura.INV_COPY_DATE = dr.GetDateTime(dr.GetOrdinal("INV_COPY_DATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_NULL")))
                        factura.INV_NULL = dr.GetDateTime(dr.GetOrdinal("INV_NULL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_NULLREASON")))
                        factura.INV_NULLREASON = dr.GetString(dr.GetOrdinal("INV_NULLREASON"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_ACCOUNTED")))
                        factura.INV_ACCOUNTED = dr.GetDateTime(dr.GetOrdinal("INV_ACCOUNTED"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_ACC_PROCESS")))
                        factura.INV_ACC_PROCESS = dr.GetDecimal(dr.GetOrdinal("INV_ACC_PROCESS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_LIQ_COM_TOT")))
                        factura.INV_LIQ_COM_TOT = dr.GetDateTime(dr.GetOrdinal("INV_LIQ_COM_TOT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_OBSERV")))
                        factura.INV_OBSERV = dr.GetString(dr.GetOrdinal("INV_OBSERV"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_CN_IND")))
                        factura.INV_CN_IND = dr.GetString(dr.GetOrdinal("INV_CN_IND"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_CN_REF")))
                        factura.INV_CN_REF = dr.GetDecimal(dr.GetOrdinal("INV_CN_REF"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_BASE")))
                        factura.INV_BASE = dr.GetDecimal(dr.GetOrdinal("INV_BASE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_TAXES")))
                        factura.INV_TAXES = dr.GetDecimal(dr.GetOrdinal("INV_TAXES"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_TBASE")))
                        factura.INV_TBASE = dr.GetDecimal(dr.GetOrdinal("INV_TBASE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_TTAXES")))
                        factura.INV_TTAXES = dr.GetDecimal(dr.GetOrdinal("INV_TTAXES"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_TDEDUCTIONS")))
                        factura.INV_TDEDUCTIONS = dr.GetDecimal(dr.GetOrdinal("INV_TDEDUCTIONS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_TNET")))
                        factura.INV_TNET = dr.GetDecimal(dr.GetOrdinal("INV_TNET"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_DISTRIBUTED")))
                        factura.INV_DISTRIBUTED = dr.GetDateTime(dr.GetOrdinal("INV_DISTRIBUTED"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_DIST_PROCESS")))
                        factura.INV_DIST_PROCESS = dr.GetDecimal(dr.GetOrdinal("INV_DIST_PROCESS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_KEY")))
                        factura.INV_KEY = dr.GetString(dr.GetOrdinal("INV_KEY"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        factura.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_DISCOUNTS")))
                        factura.INV_DISCOUNTS = dr.GetDecimal(dr.GetOrdinal("INV_DISCOUNTS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_CN_TOTAL")))
                        factura.INV_CN_TOTAL = dr.GetDecimal(dr.GetOrdinal("INV_CN_TOTAL"));

                    if (!dr.IsDBNull(dr.GetOrdinal("OFF_ID")))
                        factura.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));


                }
            }
            return factura;
        }
    }
}

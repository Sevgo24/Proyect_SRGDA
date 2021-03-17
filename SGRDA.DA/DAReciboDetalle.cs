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
    public class DAReciboDetalle
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public int InsertarDetRecibo(BEReciboDetalle detRecibo)
        {
            int result = 0;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASI_RECIBO_DETALLE"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, detRecibo.OWNER);
                    db.AddInParameter(cm, "@REC_ID", DbType.Decimal, detRecibo.REC_ID);
                    db.AddInParameter(cm, "@INV_ID", DbType.Decimal, detRecibo.INV_ID);

                    db.AddInParameter(cm, "@REC_BASE", DbType.Decimal, detRecibo.REC_BASE);
                    db.AddInParameter(cm, "@REC_TAXES", DbType.Decimal, detRecibo.REC_TAXES);
                    db.AddInParameter(cm, "@REC_DISCOUNTS", DbType.Decimal, detRecibo.REC_DISCOUNTS);
                    db.AddInParameter(cm, "@REC_DEDUCTIONS", DbType.Decimal, detRecibo.REC_DEDUCTIONS);
                    db.AddInParameter(cm, "@REC_TOTAL", DbType.Decimal, detRecibo.REC_TOTAL);
                    db.AddInParameter(cm, "@CUR_ALPHA", DbType.String, detRecibo.CUR_ALPHA);

                    db.AddInParameter(cm, "@LOG_USER_CREAT", DbType.String, detRecibo.LOG_USER_CREAT);
                    db.AddOutParameter(cm, "@REC_DID", DbType.Decimal, Convert.ToInt32(detRecibo.REC_DID));
                    result = Convert.ToInt32(db.ExecuteScalar(cm));
                }
            }
            catch (Exception ex)
            {
                result = 0;
                throw;
            }
            return result;
        }

        public List<BEReciboDetalle> ObtenerRecibosDetalle(string owner, decimal idRecibo, string version)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_RECIBO_FACTURA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@REC_ID", DbType.Decimal, idRecibo);
            db.AddInParameter(oDbCommand, "@VERSION", DbType.String, version);

            List<BEReciboDetalle> Lista = new List<BEReciboDetalle>();
            BEReciboDetalle item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEReciboDetalle();
                    item.REC_ID = dr.GetDecimal(dr.GetOrdinal("REC_ID"));
                    item.REC_DID = dr.GetDecimal(dr.GetOrdinal("REC_DID"));
                    item.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                    item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    item.TIPO_DOC = dr.GetString(dr.GetOrdinal("TIPO_DOC"));
                    item.FACTURA = dr.GetString(dr.GetOrdinal("NUM_DOC"));
                    item.REC_BASE = dr.GetDecimal(dr.GetOrdinal("REC_BASE"));
                    item.REC_TAXES = dr.GetDecimal(dr.GetOrdinal("REC_TAXES"));
                    item.REC_DISCOUNTS = dr.GetDecimal(dr.GetOrdinal("REC_DISCOUNTS"));
                    item.REC_DEDUCTIONS = dr.GetDecimal(dr.GetOrdinal("REC_DEDUCTIONS"));
                    item.SALDO_PENDIENTE = dr.GetDecimal(dr.GetOrdinal("SALDO_PENDIENTE"));
                    item.REC_TOTAL = dr.GetDecimal(dr.GetOrdinal("MONTO_APLICAR"));
                    item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    item.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                    item.MONEDA = dr.GetString(dr.GetOrdinal("MONEDA"));
                    item.REC_BALANCE = dr.GetDecimal(dr.GetOrdinal("PENDIENTE_APLICACION"));
                    Lista.Add(item);
                }
            }
            return Lista;
        }


        public BEReciboDetalle ObtenerReciboDetalle(BEReciboDetalle DetalleRecibo)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDSS_OBTENER_RECIBO_DETALLE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, DetalleRecibo.OWNER);
            db.AddInParameter(oDbCommand, "@REC_DID", DbType.Decimal, DetalleRecibo.REC_DID);
            db.AddInParameter(oDbCommand, "@REC_ID", DbType.Decimal, DetalleRecibo.REC_ID);
            db.ExecuteNonQuery(oDbCommand);

            BEReciboDetalle item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEReciboDetalle();
                    item.REC_DID = dr.GetDecimal(dr.GetOrdinal("REC_DID"));
                    item.REC_ID = dr.GetDecimal(dr.GetOrdinal("REC_ID"));
                    item.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                    item.REC_BASE = dr.GetDecimal(dr.GetOrdinal("REC_BASE"));
                    item.REC_TAXES = dr.GetDecimal(dr.GetOrdinal("REC_TAXES"));
                    item.REC_DISCOUNTS = dr.GetDecimal(dr.GetOrdinal("REC_DISCOUNTS"));
                    item.REC_DEDUCTIONS = dr.GetDecimal(dr.GetOrdinal("REC_DEDUCTIONS"));
                    item.REC_TOTAL = dr.GetDecimal(dr.GetOrdinal("REC_TOTAL"));
                    item.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                }
            }
            return item;
        }

        public int ActualizarDetRecibo(BEReciboDetalle detRecibo)
        {
            int result = 0;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASU_RECIBO_DETALLE"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, detRecibo.OWNER);
                    db.AddInParameter(cm, "@REC_DID", DbType.Decimal, detRecibo.REC_DID);
                    db.AddInParameter(cm, "@REC_ID", DbType.Decimal, detRecibo.REC_ID);

                    db.AddInParameter(cm, "@REC_BASE", DbType.Decimal, detRecibo.REC_BASE);
                    db.AddInParameter(cm, "@REC_TAXES", DbType.Decimal, detRecibo.REC_TAXES);
                    db.AddInParameter(cm, "@REC_DISCOUNTS", DbType.Decimal, detRecibo.REC_DISCOUNTS);
                    db.AddInParameter(cm, "@REC_DEDUCTIONS", DbType.Decimal, detRecibo.REC_DEDUCTIONS);
                    db.AddInParameter(cm, "@REC_TOTAL", DbType.Decimal, detRecibo.REC_TOTAL);

                    db.AddInParameter(cm, "@LOG_USER_UPDATE", DbType.String, detRecibo.LOG_USER_UPDATE);
                    result = Convert.ToInt32(db.ExecuteNonQuery(cm));
                }
            }
            catch (Exception ex)
            {
                result = 0;
                throw;
            }
            return result;
        }


    }
}

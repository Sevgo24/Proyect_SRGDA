using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGRDA.Entities.FacturaElectronica;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
namespace SGRDA.DA.FacturacionElectronica
{
    public class DADetalleFactura
    {
        Database db = new DatabaseProviderFactory().Create("conexion");

        public List<BEDetalleFactura> ListarDetalleFactura(string owner, decimal IdFactura)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_DET_FACTURA_INTERFAZ");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, IdFactura);
            oDbCommand.CommandTimeout = 1800;
            //db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEDetalleFactura>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEDetalleFactura factura = null;
                while (dr.Read())
                {
                    factura = new BEDetalleFactura();
                    factura.NroLinDet = dr.GetString(dr.GetOrdinal("NroLinDet"));
                    factura.QtyItem = dr.GetString(dr.GetOrdinal("QtyItem"));
                    factura.VlrCodigo = dr.GetString(dr.GetOrdinal("VlrCodigo"));
                    factura.NmbItem = dr.GetString(dr.GetOrdinal("NmbItem"));
                    factura.PrcItem = dr.GetDecimal(dr.GetOrdinal("PrcItem"));
                    factura.DescuentoMonto = dr.GetDecimal(dr.GetOrdinal("DescuentoMonto"));
                    factura.PrcItemSinIgv = dr.GetDecimal(dr.GetOrdinal("PrcItemSinIgv"));
                    factura.DescuentoMonto = dr.GetDecimal(dr.GetOrdinal("DescuentoMonto"));
                    factura.MontoItem = dr.GetDecimal(dr.GetOrdinal("MontoItem"));
                    factura.Observacion = dr.GetString(dr.GetOrdinal("Observacion"));
                    lista.Add(factura);
                }
            }
            return lista;
        }

        public List<BEDetalleFactura> ListarDetalleFacturaMasiva(string owner, decimal IdFactura)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_DET_EMISION_INTERFAZ");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, IdFactura);
            oDbCommand.CommandTimeout = 1800;
            db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEDetalleFactura>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEDetalleFactura factura = null;
                while (dr.Read())
                {
                    factura = new BEDetalleFactura();
                    factura.NroLinDet = dr.GetString(dr.GetOrdinal("NroLinDet"));
                    factura.QtyItem = dr.GetString(dr.GetOrdinal("QtyItem"));
                    factura.VlrCodigo = dr.GetString(dr.GetOrdinal("VlrCodigo"));
                    factura.NmbItem = dr.GetString(dr.GetOrdinal("NmbItem"));
                    factura.PrcItem = dr.GetDecimal(dr.GetOrdinal("PrcItem"));
                    factura.DescuentoMonto = dr.GetDecimal(dr.GetOrdinal("DescuentoMonto"));
                    factura.PrcItemSinIgv = dr.GetDecimal(dr.GetOrdinal("PrcItemSinIgv"));
                    factura.DescuentoMonto = dr.GetDecimal(dr.GetOrdinal("DescuentoMonto"));
                    factura.MontoItem = dr.GetDecimal(dr.GetOrdinal("MontoItem"));
                    factura.Observacion = dr.GetString(dr.GetOrdinal("Observacion"));
                    lista.Add(factura);
                }
            }
            return lista;
        }

        public List<BEDetalleFactura> ListarDetalleFacturaNC(string owner, decimal IdFactura)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_DET_FACTURA_INTERFAZ_NC");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, IdFactura);
            oDbCommand.CommandTimeout = 1800;
            db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEDetalleFactura>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEDetalleFactura factura = null;
                while (dr.Read())
                {
                    factura = new BEDetalleFactura();
                    factura.NroLinDet = dr.GetString(dr.GetOrdinal("NroLinDet"));
                    factura.QtyItem = dr.GetString(dr.GetOrdinal("QtyItem"));
                    factura.VlrCodigo = dr.GetString(dr.GetOrdinal("VlrCodigo"));
                    factura.NmbItem = dr.GetString(dr.GetOrdinal("NmbItem"));
                    factura.PrcItem = dr.GetDecimal(dr.GetOrdinal("PrcItem"));
                    factura.DescuentoMonto = dr.GetDecimal(dr.GetOrdinal("DescuentoMonto"));
                    factura.PrcItemSinIgv = dr.GetDecimal(dr.GetOrdinal("PrcItemSinIgv"));
                    factura.MontoItem = dr.GetDecimal(dr.GetOrdinal("MontoItem"));
                    factura.Observacion = dr.GetString(dr.GetOrdinal("Observacion"));
                    lista.Add(factura);
                }
            }
            return lista;
        }
    }
}

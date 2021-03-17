using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;

namespace SGRDA.BL
{
    public class BLFacturaCobro
    {
        public BEFactura ListarFacturaPendientePago(string owner, decimal usuDerecho, decimal serie, decimal numero,
                                                    decimal idFact, decimal idRec)
        {
            decimal idFactura = 0;
            decimal idUsuarioDerecho = 0;
            BEFactura Factura = new BEFactura();
            List<BELicencias> ListaLicencias = new List<BELicencias>();
            List<BEFacturaDetalle> ListaFacturaDetalles = new List<BEFacturaDetalle>();

            Factura.ListarFactura = new DAFacturaCobro().ListarFacturaPendientePago(owner, usuDerecho, serie, numero);// OK
            if (Factura.ListarFactura.Count > 0)
            {
                foreach (var fact in Factura.ListarFactura)
                {
                    idFactura = fact.INV_ID;
                    var licencia = new DALicencias().ListarFacturaPendienteDetalle(owner, idFactura);
                    var detalleFactura = new DAFacturaDetalle().ListarFacturaPendienteDetalle_subDetalle(owner, idFactura);
                    foreach (var lic in licencia)
                    {
                        ListaLicencias.Add(lic);
                    }
                    foreach (var detaFact in detalleFactura)
                    {
                        ListaFacturaDetalles.Add(detaFact);
                    }
                }
                Factura.ListarLicencia = ListaLicencias;
                Factura.ListarDetalleFactura = ListaFacturaDetalles;
               
                //RECIBOS
                idUsuarioDerecho = Factura.ListarFactura.FirstOrDefault().BPS_ID;
                Factura.ListarRecibosPendientes = new DARecibo().ListarRecibosPendientes(owner, idUsuarioDerecho);
                List<BEDetalleMetodoPago> ListaDetalles = new List<BEDetalleMetodoPago>();
                foreach (var recibo in Factura.ListarRecibosPendientes)
                {
                    var detalles = new DADetalleMetodoPago().ListarMetodoPagoComoDetalle(owner, recibo.REC_ID);
                    foreach (var item in detalles)
                    {
                        ListaDetalles.Add(item);
                    }
                }
                Factura.ListarDetalleRecibosPedientes = ListaDetalles;
            }
            return Factura;
        }

        //public BEFactura ListarFacturaPendientePago(string owner, decimal usuDerecho, decimal serie, decimal numero,
        //                                           decimal idFact, decimal idRec)
        //{
        //    BEFactura Factura = new BEFactura();
        //    Factura.ListarFactura = new DAFacturaCobro().ListarFacturaPendientePago(owner, usuDerecho, serie, numero);
        //    Factura.ListarLicencia = new DALicencias().ListarFacturaPendienteDetalle(owner, idFact);
        //    Factura.ListarDetalleFactura = new DAFacturaDetalle().ListarFacturaPendienteDetalle_subDetalle(owner, idFact);

        //    Factura.ListarRecibosPendientes = new DARecibo().ListarRecibosPendientes(owner, usuDerecho);
        //    //crear otro sp - Factura.ListarRecibosPendientes = new DARecibo().ListarRecibosPendientes(owner, usuDerecho);
        //    Factura.ListarDetalleRecibosPedientes = new DADetalleMetodoPago().ListarMetodoPagoComoDetalle(owner, idRec);
        //    //crear otro sp - Factura.ListarDetalleRecibosPedientes = new DADetalleMetodoPago().ListarMetodoPagoComoDetalle(owner, idRec);
        //    return Factura;
        //}

        public BEFactura ObtenerFacturaAplicar(string owner, decimal idFactura)
        {
            return new DAFacturaCobro().ObtenerFacturaAplicar(owner, idFactura);
        }

        public int ActualizarCollects(BEFactura en)
        {
            return new DAFacturaCobro().ActualizarCollects(en);
        }
    }
}


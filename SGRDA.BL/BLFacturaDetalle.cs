using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLFacturaDetalle
    {
        public List<BEFacturaDetalle> ListarFacturaDetalleAplicar(string owner, decimal idfact)
        {
            return new DAFacturaDetalle().ListarFacturaDetalleAplicar(owner, idfact);
        }

        public int AplicarPagoDetalleFactura(BEFacturaDetalle en)
        {
            return new DAFacturaDetalle().AplicarPagoDetalleFactura(en);
        }

        public int InsertarFacturaDetalle(BEFacturaDetalle en)
        {
            return new DAFacturaDetalle().InsertarFacturaDetalle(en);
        }

        public bool ActualizarFacturaDetalle(BEFacturaDetalle det)
        {
            bool exitoDetalle = new DAFacturaDetalle().ActualizarDetalleFactura(det);
            return exitoDetalle;
        }

        public bool ActualizarDetalleDescuento(BEFacturaDescuento en)
        {
            var exitoDetalleDescuento = new DAFacturaDetalle().ActualizarDetalleDescuento(en);
            return exitoDetalleDescuento;
        }
    }
}

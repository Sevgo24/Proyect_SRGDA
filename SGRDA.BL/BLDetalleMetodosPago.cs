using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;
using System.Transactions;

namespace SGRDA.BL
{
    public class BLDetalleMetodosPago
    {
        public int Insertar(BEDetalleMetodoPago en)
        {
            return new DADetalleMetodoPago().Insertar(en);
        }

        public List<BEDetalleMetodoPago> ListarMetodoPago(string owner, decimal IdRecibo)
        {
            return new DADetalleMetodoPago().ListarMetodoPago(owner, IdRecibo);
        }

        public List<BEDetalleMetodoPago> ListarMetodoPagoComoDetalle(string owner, decimal IdRecibo)
        {
            return new DADetalleMetodoPago().ListarMetodoPagoComoDetalle(owner, IdRecibo);
        }

        public BEMetodoPago ObtenerConfirmed(string owner, string Idpay)
        {
            return new DADetalleMetodoPago().ObtenerConfirmed(owner, Idpay);
        }

        public REF_CURRENCY_VALUES ObtenerTipoCambio(string IdMoneda)
        {
            return new DADetalleMetodoPago().ObtenerTipoCambio(IdMoneda);
        }

        public int Eliminar(BEDetalleMetodoPago en, BERecibo re)
        {
            var result = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                result = new DADetalleMetodoPago().Eliminar(en);

                //var act = new BLRecibo().ActualizarTotalQuitar(re);

                transa.Complete();
            }
            return result;
        }

        public int ObtenerDetalleEliminar(BEDetalleMetodoPago en)
        {
            return new DADetalleMetodoPago().ObtenerDetalleEliminar(en);
        }

        public List<BEDetalleMetodoPago> ObtenerRecibosVoucher(string owner, decimal idRecibo, string version)
        {
            return new DADetalleMetodoPago().ObtenerRecibosVoucher(owner, idRecibo, version);
        }

        public BEDetalleMetodoPago ObtenerRecibosVoucherXid(string owner, decimal red_pid)
        {
            return new DADetalleMetodoPago().ObtenerRecibosVoucherXid(owner, red_pid);
        }
    }
}

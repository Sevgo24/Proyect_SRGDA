using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;
using SGRDA.Utility;
using System.Transactions;

namespace SGRDA.BL
{
    public class BLMetodoPago
    {
        public List<BEMetodoPago> ListarPaginado(string owner, string param, bool confirmacion, int st, int pagina, int cantRegxPag)
        {
            return new DAMetodoPago().ListarPaginado(owner, param, confirmacion, st, pagina, cantRegxPag);
        }

        public int Eliminar(BEMetodoPago en)
        {
            return new DAMetodoPago().Eliminar(en);
        }

        public int Insertar(BEMetodoPago en)
        {
            return new DAMetodoPago().Insertar(en);
        }

        public BEMetodoPago Obtener(string owner, string id)
        {
            return new DAMetodoPago().Obtener(owner, id);
        }

        public int Actualizar(BEMetodoPago en)
        {
            return new DAMetodoPago().Actualizar(en);
        }

        public int ObtenerXDescripcion(BEMetodoPago en)
        {
            return new DAMetodoPago().ObtenerXDescripcion(en);
        }

        public int ObtenerXCodigo(BEMetodoPago en)
        {
            return new DAMetodoPago().ObtenerXCodigo(en);
        }

        public List<BEMetodoPago> ListarMetodoPago(string owner)
        {
            return new DAMetodoPago().ListarMetodoPago(owner);
        }
        // COMPROBANTES DE PAGO

        public List<BEDetalleMetodoPago> ListarDepositosBancarios(string owner, string idBanco, string idTipoPago,
                                                                  string idMoneda, string idEstadoConfirmacion, string CodigoDeposito,
                                                                  int conFecha, DateTime FIni, DateTime FFin, decimal IdBps,
                                                                  string idBancoDestino, string idCuentaDestino, string montoDepositado,
                                                                  int conFechaIngreso, DateTime FIniIngreso, DateTime FFinIngreso, decimal IdOficina,
                                                                  decimal IdVoucher, string CodigoConfirmacion, decimal becEspecial, decimal becEspecialAprobacion,decimal idCobro,
                                                                  int page, int pageSize)
        {
            return new DAMetodoPago().ListarDepositosBancarios(owner, idBanco, idTipoPago, idMoneda, idEstadoConfirmacion,
                                                                CodigoDeposito, conFecha, FIni, FFin, IdBps,
                                                                 idBancoDestino, idCuentaDestino, montoDepositado,
                                                                 conFechaIngreso, FIniIngreso, FFinIngreso, IdOficina, IdVoucher, CodigoConfirmacion, becEspecial, becEspecialAprobacion, idCobro,
                                                                 page, pageSize);
        }

        public List<BEReciboDetalle> ObtenerRecibos_x_Comprobante(string owner, decimal id)
        {
            return new DAMetodoPago().ObtenerRecibosDetalle_x_Comprobante(owner, id);
        }

        public List<BEFactura> ObtenerFactura_x_Comprobante(string owner, decimal id)
        {
            return new DAMetodoPago().ObtenerFactura_x_Comprobante(owner, id);
        }

        public List<BEFacturaDetalle> ObtenerFacturaDetalle_x_Comprobante(string owner, decimal id)
        {
            return new DAMetodoPago().ObtenerFacturaDetalle_x_Comprobante(owner, id);
        }

        public BEDetalleMetodoPago ObtenerComprobante(string owner, decimal id)
        {
            return new DADetalleMetodoPago().ObtenerComprobante(owner, id);
        }

        public int ActualizarComprobante(BEDetalleMetodoPago comprobante)
        {
            int resultAplicarCobros = 0;
            int resultadoActComprobante = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                resultadoActComprobante = new DADetalleMetodoPago().ActualizarComprobante(comprobante);
                if (comprobante.REC_CONFIRMED == Util.EstadosConfirmacion.CONFIRMACION)
                    resultAplicarCobros = AplicarCobrosXdeposito(comprobante);
                transa.Complete();
            }
            return resultadoActComprobante;
        }

        public int AplicarCobrosXListaDepositos(List<BEDetalleMetodoPago> ListaDepositoBancario)
        {
            int resultAplicarCobros = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                foreach (var itemDeposito in ListaDepositoBancario)
                {
                    resultAplicarCobros = AplicarCobrosXdeposito(itemDeposito);
                }
                transa.Complete();
            }
            return resultAplicarCobros;
        }

        public int AplicarCobrosXdeposito(BEDetalleMetodoPago depositoBancario)
        {
            int resultado = 0;
            decimal AcumularCobroSoles = 0;
            //Actualizar confirmacion de Comprobante
            //int resultadoActComprobante = new DADetalleMetodoPago().ActualizarComprobante(comprobante);
            //using (TransactionScope transa = new TransactionScope())
            //{
            var deposito = new DADetalleMetodoPago().ObtenerComprobante(GlobalVars.Global.OWNER, depositoBancario.REC_PID);
            var listaReciboDetalle = new DAMetodoPago().ObtenerRecibosDetalle_x_Comprobante(GlobalVars.Global.OWNER, depositoBancario.REC_PID);
            var listaFactura = new DAMetodoPago().ObtenerFactura_x_Comprobante(GlobalVars.Global.OWNER, depositoBancario.REC_PID);
            var listaFacturaDetalle = new DAMetodoPago().ObtenerFacturaDetalle_x_Comprobante(GlobalVars.Global.OWNER, depositoBancario.REC_PID);


            // *** Obtener Division X Licencia ***
            List<BELicencias> ListaLicencia = new List<BELicencias>();
            string xmlLicenciasAgrupadas = string.Empty;
            var licenciasAgrupadas = from p in listaFacturaDetalle
                                     group p by new { p.LIC_ID } into grupo
                                     select grupo;

            foreach (var item in licenciasAgrupadas)
            {
                ListaLicencia.Add(new BELicencias() { LIC_ID = item.Key.LIC_ID });
            }
            xmlLicenciasAgrupadas = Utility.Util.SerializarEntity(ListaLicencia);
            var ObtenerListOficinaXLicencia = new DAMetodoPago().ObtenerOficinaXLicencia(GlobalVars.Global.OWNER, xmlLicenciasAgrupadas);
            // ******************************************************************************************************




            if (deposito != null && listaReciboDetalle != null && listaFactura != null && listaFacturaDetalle != null &&
                deposito.CONVERSION_SOLES_BALANCE > 0 && listaReciboDetalle.Count > 0 && listaFactura.Count > 0 && listaFacturaDetalle.Count > 0)
            {   //// PAGO PROPORCIONAL POR FACTURA - RECIBO
                decimal TipoCambioDolares = deposito.CUR_VALUE; // Tipo cambio de $ a soles.
                decimal depositoBalanceConvertidoSoles = deposito.CONVERSION_SOLES_BALANCE; // Monto en soles del comprobante.                    
                decimal TotalReciboDetalleBalanceSoles = listaReciboDetalle.Sum(x => x.CONVERSION_BALANCE_SOLES); // Total de recibos detalle pendientes de pago - convertido a soles.
                decimal porcentajeDetalleReciboSoles = depositoBalanceConvertidoSoles / TotalReciboDetalleBalanceSoles; // 1º Porcentaje

                if (porcentajeDetalleReciboSoles >= 1) porcentajeDetalleReciboSoles = 1;

                foreach (var reciboDetalle in listaReciboDetalle) // ReciboDetalle por C/Factura
                {   //// Cabecera Factura
                    reciboDetalle.APLICAR_COBRO_SOLES = reciboDetalle.CONVERSION_BALANCE_SOLES * porcentajeDetalleReciboSoles;
                    reciboDetalle.LOG_USER_UPDATE = depositoBancario.LOG_USER_UPDATE;
                    foreach (var itemFactura in listaFactura.Where(x => x.INV_ID == reciboDetalle.INV_ID))
                    {
                        decimal porcentajeFactura = 0;
                        itemFactura.LOG_USER_UPDATE = depositoBancario.LOG_USER_UPDATE;
                        if (itemFactura.CUR_ALPHA == Util.TipoMoneda.SOLES) // SOLES
                        {
                            porcentajeFactura = reciboDetalle.APLICAR_COBRO_SOLES / itemFactura.INV_NET_SOLES;//% para distriubuie el cobro en montos proporcionales
                            itemFactura.INV_COLLECTB = itemFactura.INV_BASE_SOLES * porcentajeFactura;
                            itemFactura.INV_COLLECTT = itemFactura.INV_TAXES_SOLES * porcentajeFactura;
                            itemFactura.INV_COLLECTD = itemFactura.INV_DISCOUNTS_SOLES * porcentajeFactura;
                        }
                        else if (itemFactura.CUR_ALPHA == Util.TipoMoneda.DOLARES) // DOLARES
                        {
                            porcentajeFactura = (reciboDetalle.APLICAR_COBRO_SOLES / TipoCambioDolares) / itemFactura.INV_NET;
                            itemFactura.INV_COLLECTB = itemFactura.INV_BASE * porcentajeFactura;
                            itemFactura.INV_COLLECTT = itemFactura.INV_TAXES * porcentajeFactura;
                            itemFactura.INV_COLLECTD = itemFactura.INV_DISCOUNTS * porcentajeFactura;
                        }
                        itemFactura.INV_COLLECTN = itemFactura.INV_COLLECTB + itemFactura.INV_COLLECTT - itemFactura.INV_COLLECTD;

                        //// Actualizar Montos de Cobros.
                        reciboDetalle.REC_BALANCE = itemFactura.INV_COLLECTN;// Actualizar el saldo pendiente- Cuanto realmente se cobro x factura.
                        if (itemFactura.CUR_ALPHA == Util.TipoMoneda.SOLES) //Para el saldo de los comprobante.
                            AcumularCobroSoles += itemFactura.INV_COLLECTN;
                        else if (itemFactura.CUR_ALPHA == Util.TipoMoneda.DOLARES)
                            AcumularCobroSoles += itemFactura.INV_COLLECTN * TipoCambioDolares;


                        //// int ResultFactura = new DAMetodoPago().ActualizarFacturaCab_x_Cobro(itemFactura);
                        //// Detalle Factura
                        foreach (var itemDeta in listaFacturaDetalle.Where(x => x.INV_ID == itemFactura.INV_ID))
                        {
                            itemDeta.REC_PID = deposito.REC_PID;
                            itemDeta.MREC_ID = deposito.MREC_ID;
                            itemDeta.REC_ID = reciboDetalle.REC_ID;
                            itemDeta.LOG_USER_UPDATE = depositoBancario.LOG_USER_UPDATE;
                            if (itemDeta.CUR_ALPHA == Util.TipoMoneda.SOLES)// SOLES
                            {
                                itemDeta.INVL_COLLECTB = itemDeta.INVL_GROSS_SOLES * porcentajeFactura;
                                itemDeta.INVL_COLLECTT = itemDeta.INVL_TAXES_SOLES * porcentajeFactura;
                                itemDeta.INVL_COLLECTD = itemDeta.INVL_DISC_SOLES * porcentajeFactura;
                            }
                            else if (itemDeta.CUR_ALPHA == Util.TipoMoneda.DOLARES) // DOLARES
                            {
                                itemDeta.INVL_COLLECTB = itemDeta.INVL_GROSS * porcentajeFactura;
                                itemDeta.INVL_COLLECTT = itemDeta.INVL_TAXES * porcentajeFactura;
                                itemDeta.INVL_COLLECTD = itemDeta.INVL_DISC * porcentajeFactura;
                            }

                            itemDeta.INVL_COLLECTN = itemDeta.INVL_COLLECTB + itemDeta.INVL_COLLECTT - itemDeta.INVL_COLLECTD;
                            itemDeta.INVL_COLLECTN_SOLES = (itemDeta.CUR_ALPHA == Util.TipoMoneda.DOLARES) ? itemDeta.INVL_COLLECTN * TipoCambioDolares : itemDeta.INVL_COLLECTN;

                            //// Identificador de oficina de Facturación y División.
                            if (ObtenerListOficinaXLicencia.Where(x => x.LIC_ID == itemDeta.LIC_ID).Count() > 0)
                            {
                                itemDeta.OFF_ID = ObtenerListOficinaXLicencia.Where(x => x.LIC_ID == itemDeta.LIC_ID).FirstOrDefault().OFF_ID;
                                itemDeta.DAD_ID = ObtenerListOficinaXLicencia.Where(x => x.LIC_ID == itemDeta.LIC_ID).FirstOrDefault().DAD_ID;
                            }
                            else
                            {
                                itemDeta.OFF_ID = deposito.OFF_ID;
                                itemDeta.DAD_ID = 0;
                            }

                        }
                    }
                }



                if (deposito.CUR_ALPHA == Util.TipoMoneda.SOLES)// SOLES
                    deposito.REC_BALANCE = AcumularCobroSoles;
                else if (deposito.CUR_ALPHA == Util.TipoMoneda.DOLARES) // DOLARES
                    deposito.REC_BALANCE = AcumularCobroSoles / TipoCambioDolares;

                int ActComprobanteSaldo = new DADetalleMetodoPago().ActualizarComprobanteSaldo(deposito);

                string xmlFactura = string.Empty; string xmlFacturaDetalle = string.Empty; string xmlReciboDetalle = string.Empty;
                xmlReciboDetalle = Utility.Util.SerializarEntity(listaReciboDetalle);
                xmlFactura = Utility.Util.SerializarEntity(listaFactura);
                xmlFacturaDetalle = Utility.Util.SerializarEntity(listaFacturaDetalle);

                int resultadoCobroDetalleFact = new DAMetodoPago().ActualizarReciboDetalleFact_x_CobroXML(xmlReciboDetalle);
                int resultadoActFactura = new DAMetodoPago().ActualizarFacturaCab_x_CobroXML(xmlFactura);
                int resultadoActDetalleFactura = new DAMetodoPago().ActualizarFacturaDetalle_x_Cobro_XML(xmlFacturaDetalle);
                int resultadoRegistrarDetalleCobro = new DAMetodoPago().RegistrarDetalleCobro_x_DepositoXML(xmlFacturaDetalle);
                // obtemner regsitro en la nueva tabla
            }
            //    transa.Complete();
            //}
            return resultado;
        }


        public int ActualizarSinConfirmarVoucher(BEDetalleMetodoPago voucher)
        {
            return new DADetalleMetodoPago().ActualizarSinConfirmarVoucher(voucher);

        }

        public int ActualizarDepositoBecEspecial(BEDetalleMetodoPago en)
        {
            return new DAMetodoPago().ActualizarDepositoBecEspecial(en);
        }

        public List<BEDetalleMetodoPago> ObtenerPermisoXoficina(decimal idOficina)
        {
            return new DAMetodoPago().ObtenerPermisoXoficina(idOficina);
        }


        public List<BEDetalleMetodoPago> ListarDepositosBancarios_Reporte(string owner, string idBanco, string idTipoPago,
                                                          string idMoneda, string idEstadoConfirmacion, string CodigoDeposito,
                                                          int conFecha, DateTime? FIni, DateTime? FFin, decimal IdBps,
                                                          string idBancoDestino, string idCuentaDestino, string montoDepositado,
                                                          int conFechaIngreso, DateTime? FIniIngreso, DateTime? FFinIngreso, decimal IdOficina,
                                                          decimal IdVoucher, string CodigoConfirmacion, decimal becEspecial, decimal becEspecialAprobacion, decimal idCobro)
        {
            return new DAMetodoPago().ListarDepositosBancarios_Reporte(owner, idBanco, idTipoPago, idMoneda, idEstadoConfirmacion,
                                                                CodigoDeposito, conFecha, FIni, FFin, IdBps,
                                                                 idBancoDestino, idCuentaDestino, montoDepositado,  conFechaIngreso, FIniIngreso, 
                                                                 FFinIngreso, IdOficina, IdVoucher, CodigoConfirmacion, becEspecial, 
                                                                 becEspecialAprobacion, idCobro
                                                                );
        }


    }
}

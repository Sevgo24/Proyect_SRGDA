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
    public class BLMultiRecibo
    {
        public decimal Insertar(string tipo, List<BERecibo> ListaCabeceraRecibo, List<BEReciboDetalle> ListaDetalleRecibo,
                             List<BEDetalleMetodoPago> listaDetalleVoucher,
                             BEMultiRecibo multiRecibo)
        {
            //XXX MULTI RECIBO XXX            
            decimal IdMultiRecibo = 0;
            decimal IdRecIdRecibo = 0;

            try
            {
                using (TransactionScope transa = new TransactionScope())
                {
                    IdMultiRecibo = new DAMultiRecibo().Insertar(multiRecibo);
                    //XXX RECIBO XXX            
                    foreach (var recibo in ListaCabeceraRecibo)
                    {
                        recibo.REC_ID = new DARecibo().InsertarCabRecibo(recibo);//Código del Recibo. 
                        IdRecIdRecibo = recibo.REC_ID;//variable para obtener el REC ID y hacer un reenvio                                         //XXX RECIBO DETALLE - FACTURA
                        if (ListaDetalleRecibo != null && ListaDetalleRecibo.Count > 0)
                        {
                            foreach (var detalleRecibo in ListaDetalleRecibo.Where(X => X.BPS_ID == recibo.BPS_ID))// Obteniendo fact. por usuario.
                            {
                                detalleRecibo.REC_ID = recibo.REC_ID;
                                
                                detalleRecibo.REC_DID = new DAReciboDetalle().InsertarDetRecibo(detalleRecibo); // 2                
                            }
                        }
                    }

                    // METODO DE PAGO
                    foreach (var detalleVoucher in listaDetalleVoucher) // Obteniendo fact. por usuario.
                    {
                        detalleVoucher.MREC_ID = IdMultiRecibo;
                        var resultDetRecibo = new DADetalleMetodoPago().InsertarDetallePagoBEC(detalleVoucher); // 2.2
                    }

                    //XX MULTI RECIBO DETALLE XXX
                    foreach (var itemMultiDetalle in ListaCabeceraRecibo)
                    {
                        var id = new DAMultiRecibo().InsertarDetalle(new BEMultiReciboDetalle
                        {
                            OWNER = itemMultiDetalle.OWNER,
                            MREC_ID = IdMultiRecibo,
                            REC_ID = itemMultiDetalle.REC_ID,
                            LOG_USER_CREAT = itemMultiDetalle.LOG_USER_CREAT
                        });
                    }
                    transa.Complete();
                    return IdRecIdRecibo;
                }
            }catch(Exception ex)
            {
                return 0;
            }

        }

        public int Actualizar(List<BERecibo> ListaCabeceraRecibo, List<BEReciboDetalle> ListaDetalleRecibo,
                                BEMultiRecibo multiRecibo)
        {
            //XXX MULTI RECIBO XXX            
            var result = 0;
            try {
                using (TransactionScope transa = new TransactionScope())
                {
                    result = new DAMultiRecibo().Actualizar(multiRecibo);
                    decimal IdMultiRecibo = multiRecibo.MREC_ID;
                    //XXX RECIBO XXX            
                    foreach (var recibo in ListaCabeceraRecibo)
                    {
                        var obj = new DARecibo().ObtenerMultiReciboDetalle(multiRecibo.OWNER, IdMultiRecibo, recibo.REC_ID);
                        if (obj == null) // Recibo nuevo
                        {   // Código del Recibo. 
                            recibo.REC_ID = new DARecibo().InsertarCabRecibo(recibo);
                            // Recibo detalle - Factura
                            if (ListaDetalleRecibo != null && ListaDetalleRecibo.Count > 0)
                            {
                                foreach (var detalleRecibo in ListaDetalleRecibo.Where(X => X.BPS_ID == recibo.BPS_ID))// Obteniendo fact. por usuario.
                                {
                                    detalleRecibo.REC_ID = recibo.REC_ID;
                                    detalleRecibo.REC_DID = new DAReciboDetalle().InsertarDetRecibo(detalleRecibo); // 2                
                                }
                            }
                            // Registrar MultiReciboDetalle
                            var id = new DAMultiRecibo().InsertarDetalle(new BEMultiReciboDetalle
                            {
                                OWNER = recibo.OWNER,
                                MREC_ID = IdMultiRecibo,
                                REC_ID = recibo.REC_ID,
                                LOG_USER_CREAT = recibo.LOG_USER_CREAT
                            });
                        }
                        else
                        {
                            if (obj.REC_TTOTAL != recibo.REC_TTOTAL)
                            {
                                var actRecibo = new DARecibo().ActualizarCabRecibo(recibo);
                                if (ListaDetalleRecibo != null && ListaDetalleRecibo.Count > 0)
                                {
                                    foreach (var detalleRecibo in ListaDetalleRecibo.Where(X => X.BPS_ID == recibo.BPS_ID))// Obteniendo fact. por usuario.
                                    {
                                        detalleRecibo.REC_ID = recibo.REC_ID;
                                        //var detalle = new DAReciboDetalle().ObtenerReciboDetalle(detalleRecibo.OWNER, detalleRecibo.REC_DID, detalleRecibo.REC_ID);
                                        var detalle = new DAReciboDetalle().ObtenerReciboDetalle(detalleRecibo);
                                        if (detalle == null)
                                            detalleRecibo.REC_DID = new DAReciboDetalle().InsertarDetRecibo(detalleRecibo);
                                        else
                                            detalleRecibo.REC_DID = new DAReciboDetalle().ActualizarDetRecibo(detalleRecibo);
                                    }
                                }
                            }
                        }

                    }

                    //////XX MULTI RECIBO DETALLE XXX
                    ////foreach (var itemMultiDetalle in ListaCabeceraRecibo)
                    ////{
                    ////    var id = new DAMultiRecibo().InsertarDetalle(new BEMultiReciboDetalle
                    ////    {
                    ////        OWNER = itemMultiDetalle.OWNER,
                    ////        MREC_ID = IdMultiRecibo,
                    ////        REC_ID = itemMultiDetalle.REC_ID,
                    ////        LOG_USER_CREAT = itemMultiDetalle.LOG_USER_CREAT
                    ////    });
                    ////}
                    transa.Complete();
                }
                return 1;
            }catch(Exception ex)
            {
                return 0;
            }
        }

        public List<BEMultiRecibo> Listar(string owner, decimal idSerie, string voucher,
                                     decimal idBanco, decimal idCuenta, DateTime? fini, DateTime? ffin,
                                        int conFecha, int bpsId, int estado, decimal idOficina, int estadoConfirmacion,int estadoCobro, decimal numRecibo, decimal idCobro, int pagina, int cantRegxPag)
        {

            return new DAMultiRecibo().Listar(owner, idSerie, voucher,
                                              idBanco, idCuenta, fini, ffin, conFecha, bpsId, estado, idOficina, estadoConfirmacion,numRecibo,  idCobro, estadoCobro,
                                                pagina, cantRegxPag);
        }

        public BEMultiRecibo ObtenerCab(string owner, decimal Id)
        {
            return new DAMultiRecibo().ObtenerCab(owner, Id);
        }

        public List<BEReciboDetalle> ObtenerDet(string owner, decimal Id)
        {
            return new DAMultiRecibo().ObtenerDet(owner, Id);
        }

        public BEMultiRecibo ObtenerCabecera_PagoXDoc(string owner, decimal idRecibo, string version)
        {
            return new DAMultiRecibo().ObtenerCabecera_PagoXDoc(owner, idRecibo, version);
        }

        public List<BEValoresConfig> ValoresConfig(string tipo, string subTipo)
        {
            return new DAMultiRecibo().ValoresConfig(tipo, subTipo);
        }

        public List<BEMultiRecibo> ObtenerRecibosXIdCobro(string owner, decimal idCobro)
        {
            return new DAMultiRecibo().ObtenerRecibosXIdCobro(owner, idCobro);
        }
        public int ActualizarBanco(BEMultiRecibo multiRecibo)
        {
            return new DAMultiRecibo().ActualizarBanco(multiRecibo);
        }

        public int EliminarCobro(BEMultiRecibo multiRecibo)
        {
            return new DAMultiRecibo().EliminarCobro(multiRecibo);
        }
        public decimal ObtenerMontoMinimo()
        {
            return new DAMultiRecibo().ObtenerMontoMinimo();
        }
        public int ObtenerValidacionMontoBEC(decimal CodigoCobro)
        {
            return new DAMultiRecibo().ObtenerValidacionMontoBEC(CodigoCobro);
        }
        public int ActualizarBancoFecDeposito(BEMultiRecibo multiRecibo)
        {
            return new DAMultiRecibo().ActualizarBancoFecDeposito(multiRecibo);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
using System.Transactions;

namespace SGRDA.BL
{
    public class BLRecibo
    {
        public int Insertar(BERecibo en)
        {
            var codigoGen = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                codigoGen = new DARecibo().Insertar(en);

                if (en.MetodoPago != null)
                {
                    foreach (var item in en.MetodoPago)
                    {
                        var result = new DADetalleMetodoPago().Insertar(new BEDetalleMetodoPago
                        {
                            OWNER = en.OWNER,
                            REC_ID = codigoGen,
                            REC_PWID = item.REC_PWID,
                            REC_PVALUE = item.REC_PVALUE,
                            REC_CONFIRMED = item.REC_CONFIRMED,
                            REC_DATEDEPOSITE = item.REC_DATEDEPOSITE,
                            BNK_ID = item.BNK_ID,
                            BRCH_ID = item.BRCH_ID,
                            BACC_NUMBER = item.BACC_NUMBER,
                            REC_REFERENCE = item.REC_REFERENCE,
                            LOG_USER_CREAT = en.LOG_USER_CREAT
                        });
                    }
                }

                var dato = new DARecibo().ActualizarSerie(en.OWNER, en.NMR_ID, "RC", en.LOG_USER_UPDATE);

                transa.Complete();
            }
            return codigoGen;
        }

        public int Actualizar(BERecibo en)
        {
            var codigoGen = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                //codigoGen = new DARecibo().ActualizarTotalAgregar(en);
                if (en.MetodoPago != null)
                {
                    foreach (var item in en.MetodoPago)
                    {
                        //buscar si existe para que no la vuelva a buscar
                        var dato = new DADetalleMetodoPago().ObtenerDetalleValidar(en.OWNER, item.REC_PID);
                        if (dato == 0)
                        {
                            BEDetalleMetodoPago obj = new BEDetalleMetodoPago();
                            obj.OWNER = en.OWNER;
                            obj.REC_ID = en.REC_ID;
                            obj.REC_PWID = item.REC_PWID;
                            obj.REC_PVALUE = item.REC_PVALUE;
                            obj.REC_CONFIRMED = item.REC_CONFIRMED;
                            obj.REC_DATEDEPOSITE = item.REC_DATEDEPOSITE;
                            obj.BNK_ID = item.BNK_ID;
                            obj.BRCH_ID = item.BRCH_ID;
                            obj.BACC_NUMBER = item.BACC_NUMBER;
                            obj.REC_REFERENCE = item.REC_REFERENCE;
                            obj.LOG_USER_CREAT = en.LOG_USER_CREAT;
                            var result = new DADetalleMetodoPago().Insertar(obj);
                        }
                    }
                }
                transa.Complete();
            }
            return codigoGen;
        }

        #region Aplicacion Facturas - Cobros
        public int AplicarFactura_Proporcional(BEReciboDetalle enr, decimal inv_base, decimal inv_taxes)
        {
            var result = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                result = new DARecibo().InsertarDetalle(enr);

                BEFactura f = new BEFactura();
                List<BEFacturaDetalle> fd = new List<BEFacturaDetalle>();
                BEFactura collect = new BEFactura();
                f = new BLFacturaCobro().ObtenerFacturaAplicar(GlobalVars.Global.OWNER, enr.INV_ID);
                fd = new BLFacturaDetalle().ListarFacturaDetalleAplicar(GlobalVars.Global.OWNER, enr.INV_ID);

                collect.OWNER = GlobalVars.Global.OWNER;
                collect.LOG_USER_UPDATE = enr.LOG_USER_UPDATE;
                collect.INV_ID = enr.INV_ID;
                collect.INV_COLLECTB = f.INV_COLLECTB + enr.REC_BASE;
                collect.INV_COLLECTT = f.INV_COLLECTT + enr.REC_TAXES;
                collect.INV_COLLECTN = collect.INV_COLLECTB + collect.INV_COLLECTT;
                collect.INV_BALANCE = f.INV_NET - collect.INV_COLLECTN;

                var x = new BLFacturaCobro().ActualizarCollects(collect);

                #region Aplicar pago a factura detalle
                foreach (var item in fd)
                {
                    var porDet = ((item.INVL_NET * 100) / f.INV_NET);
                    var valPorDet = (porDet / 100);
                    var collectb = valPorDet * collect.INV_COLLECTB;
                    var collectt = valPorDet * collect.INV_COLLECTT;
                    var collectn = collectb + collectt;

                    BEFacturaDetalle en = new BEFacturaDetalle();
                    en.OWNER = GlobalVars.Global.OWNER;
                    en.INVL_ID = item.INVL_ID;
                    en.INV_ID = item.INV_ID;
                    en.INVL_COLLECTB = collectb;
                    en.INVL_COLLECTT = collectt;
                    en.INVL_COLLECTN = collectn;
                    en.INVL_BALANCE = item.INVL_NET - en.INVL_COLLECTN;
                    en.LOG_USER_UPDATE = enr.LOG_USER_UPDATE;

                    var rpt = new BLFacturaDetalle().AplicarPagoDetalleFactura(en);
                }
                #endregion

                transa.Complete();
            }
            return result;
        }

        public int AplicarFactura_ImpuestoBase(BEReciboDetalle enr, decimal inv_base, decimal inv_taxes, bool impuestoCero, decimal TotalFactura, decimal TotalImpuesto, decimal NetoFactAcumlado, decimal BaseACumulado)
        {
            var result = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                result = new DARecibo().InsertarDetalle(enr);

                BEFactura collect = new BEFactura();
                collect.OWNER = GlobalVars.Global.OWNER;
                collect.LOG_USER_UPDATE = enr.LOG_USER_UPDATE;
                collect.INV_ID = enr.INV_ID;

                var f = new BLFacturaCobro().ObtenerFacturaAplicar(GlobalVars.Global.OWNER, enr.INV_ID);
                var fd = new BLFacturaDetalle().ListarFacturaDetalleAplicar(GlobalVars.Global.OWNER, enr.INV_ID);

                if (impuestoCero)
                {
                    //collect.INV_COLLECTB = f.INV_COLLECTB + enr.REC_BASE;
                    //collect.INV_COLLECTT = f.INV_COLLECTT + enr.REC_TAXES;
                    //collect.INV_COLLECTN = collect.INV_COLLECTB + collect.INV_COLLECTT;
                    //collect.INV_BALANCE = f.INV_NET - collect.INV_COLLECTN;
                    collect.INV_COLLECTB = inv_base;
                    collect.INV_COLLECTT = inv_taxes;
                    //collect.INV_COLLECTN = enr.REC_TOTAL;
                    collect.INV_COLLECTN = collect.INV_COLLECTB + collect.INV_COLLECTT;
                    //collect.INV_BALANCE = TotalFactura - collect.INV_COLLECTN;
                    collect.INV_BALANCE = TotalFactura - enr.REC_TOTAL_PAGAR;

                    #region Aplicar pago a factura detalle
                    foreach (var item in fd)
                    {
                        var porDet = ((item.INVL_NET * 100) / f.INV_NET);
                        var valPorDet = (porDet / 100);
                        var collectb = valPorDet * collect.INV_COLLECTB;
                        var collectt = valPorDet * collect.INV_COLLECTT;
                        var collectn = collectb + collectt;

                        BEFacturaDetalle en = new BEFacturaDetalle();
                        en.OWNER = GlobalVars.Global.OWNER;
                        en.INVL_ID = item.INVL_ID;
                        en.INV_ID = item.INV_ID;
                        en.INVL_COLLECTB = collectb;
                        en.INVL_COLLECTT = collectt;
                        en.INVL_COLLECTN = collectn;
                        en.INVL_BALANCE = item.INVL_NET - en.INVL_COLLECTN;
                        en.LOG_USER_UPDATE = enr.LOG_USER_UPDATE;

                        var rpt = new BLFacturaDetalle().AplicarPagoDetalleFactura(en);
                    }
                    #endregion
                }
                else
                {
                    //NetoFactAcumlado son los impuestos que se han venido pagando
                    collect.INV_COLLECTB = enr.REC_BASE + BaseACumulado;
                    collect.INV_COLLECTT = enr.REC_TAXES + NetoFactAcumlado;
                    //collect.INV_COLLECTN = enr.REC_TOTAL;
                    collect.INV_COLLECTN = collect.INV_COLLECTB + collect.INV_COLLECTT;
                    //collect.INV_COLLECTN = NetoFactAcumlado + enr.REC_TOTAL_PAGAR;
                    //collect.INV_BALANCE = TotalFactura - collect.INV_COLLECTN;
                    collect.INV_BALANCE = TotalFactura - enr.REC_TOTAL_PAGAR;

                    #region Aplicar pago a factura detalle
                    foreach (var item in fd)
                    {
                        var porDet = ((item.INVL_NET * 100) / f.INV_NET);
                        var valPorDet = (porDet / 100);
                        var collectb = valPorDet * collect.INV_COLLECTB;
                        var collectt = valPorDet * collect.INV_COLLECTT;
                        var collectn = collectb + collectt;

                        BEFacturaDetalle en = new BEFacturaDetalle();
                        en.OWNER = GlobalVars.Global.OWNER;
                        en.INVL_ID = item.INVL_ID;
                        en.INV_ID = item.INV_ID;
                        en.INVL_COLLECTB = collectb;
                        en.INVL_COLLECTT = collectt;
                        en.INVL_COLLECTN = collectn;
                        en.INVL_BALANCE = item.INVL_NET - en.INVL_COLLECTN;
                        en.LOG_USER_UPDATE = enr.LOG_USER_UPDATE;

                        var rpt = new BLFacturaDetalle().AplicarPagoDetalleFactura(en);
                    }
                    #endregion
                }

                var x = new BLFacturaCobro().ActualizarCollects(collect);

                transa.Complete();
            }
            return result;
        }

        public int AplicarFactura_BaseImpuesto(BEReciboDetalle enr, decimal inv_base, decimal inv_taxes, bool baseCero, decimal TotalFactura, decimal TotalBase)
        {
            var result = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                result = new DARecibo().InsertarDetalle(enr);

                BEFactura collect = new BEFactura();
                collect.OWNER = GlobalVars.Global.OWNER;
                collect.LOG_USER_UPDATE = enr.LOG_USER_UPDATE;
                collect.INV_ID = enr.INV_ID;

                var f = new BLFacturaCobro().ObtenerFacturaAplicar(GlobalVars.Global.OWNER, enr.INV_ID);
                var fd = new BLFacturaDetalle().ListarFacturaDetalleAplicar(GlobalVars.Global.OWNER, enr.INV_ID);

                if (baseCero)
                {
                    collect.INV_COLLECTB = inv_base;
                    collect.INV_COLLECTT = inv_taxes;
                    //collect.INV_COLLECTN = enr.REC_TOTAL;
                    collect.INV_COLLECTN = collect.INV_COLLECTB + collect.INV_COLLECTT;
                    //collect.INV_BALANCE = TotalFactura - collect.INV_COLLECTN;
                    collect.INV_BALANCE = TotalFactura - enr.REC_TOTAL_PAGAR;

                    #region Aplicar pago a factura detalle
                    foreach (var item in fd)
                    {
                        var porDet = ((item.INVL_NET * 100) / f.INV_NET);
                        var valPorDet = (porDet / 100);
                        var collectb = valPorDet * collect.INV_COLLECTB;
                        var collectt = valPorDet * collect.INV_COLLECTT;
                        var collectn = collectb + collectt;

                        BEFacturaDetalle en = new BEFacturaDetalle();
                        en.OWNER = GlobalVars.Global.OWNER;
                        en.INVL_ID = item.INVL_ID;
                        en.INV_ID = item.INV_ID;
                        en.INVL_COLLECTB = collectb;
                        en.INVL_COLLECTT = collectt;
                        en.INVL_COLLECTN = collectn;
                        en.INVL_BALANCE = item.INVL_NET - en.INVL_COLLECTN;
                        en.LOG_USER_UPDATE = enr.LOG_USER_UPDATE;

                        var rpt = new BLFacturaDetalle().AplicarPagoDetalleFactura(en);
                    }
                    #endregion
                }
                else
                {
                    collect.INV_COLLECTB = enr.REC_BASE;
                    collect.INV_COLLECTT = enr.REC_TAXES;
                    //collect.INV_COLLECTN = enr.REC_TOTAL;
                    collect.INV_COLLECTN = collect.INV_COLLECTB;
                    //collect.INV_BALANCE = TotalFactura - collect.INV_COLLECTN;
                    collect.INV_BALANCE = TotalFactura - enr.REC_TOTAL_PAGAR;

                    #region Aplicar pago a factura detalle
                    foreach (var item in fd)
                    {
                        var porDet = ((item.INVL_NET * 100) / f.INV_NET);
                        var valPorDet = (porDet / 100);
                        var collectb = valPorDet * collect.INV_COLLECTB;
                        var collectt = valPorDet * collect.INV_COLLECTT;
                        var collectn = collectb + collectt;

                        BEFacturaDetalle en = new BEFacturaDetalle();
                        en.OWNER = GlobalVars.Global.OWNER;
                        en.INVL_ID = item.INVL_ID;
                        en.INV_ID = item.INV_ID;
                        en.INVL_COLLECTB = collectb;
                        en.INVL_COLLECTT = collectt;
                        en.INVL_COLLECTN = collectn;
                        en.INVL_BALANCE = item.INVL_NET - en.INVL_COLLECTN;
                        en.LOG_USER_UPDATE = enr.LOG_USER_UPDATE;

                        var rpt = new BLFacturaDetalle().AplicarPagoDetalleFactura(en);
                    }
                    #endregion
                }

                var x = new BLFacturaCobro().ActualizarCollects(collect);

                transa.Complete();
            }
            return result;
        }
        #endregion

        #region Aplicar Factura - Nota de crédito
        public int AplicarFactura_Proporcional_NC(BEReciboDetalle enr, decimal inv_base, decimal inv_taxes, decimal inv_discount, int idfactnuevo, decimal totalCn)
        {
            var result = 0;
            using (TransactionScope transa = new TransactionScope())
            {

                BEFactura f = new BEFactura();
                List<BEFacturaDetalle> fd = new List<BEFacturaDetalle>();
                BEFactura collect = new BEFactura();
                f = new BLFacturaCobro().ObtenerFacturaAplicar(GlobalVars.Global.OWNER, enr.INV_ID);
                fd = new BLFacturaDetalle().ListarFacturaDetalleAplicar(GlobalVars.Global.OWNER, enr.INV_ID);

                collect.OWNER = GlobalVars.Global.OWNER;
                collect.LOG_USER_UPDATE = enr.LOG_USER_UPDATE;
                collect.INV_ID = enr.INV_ID;
                collect.INV_COLLECTB = f.INV_COLLECTB + enr.REC_BASE;
                collect.INV_COLLECTT = f.INV_COLLECTT + enr.REC_TAXES;
                collect.INV_COLLECTD = f.INV_COLLECTD + enr.REC_DISCOUNTS;
                collect.INV_COLLECTN = collect.INV_COLLECTB + collect.INV_COLLECTT - collect.INV_COLLECTD;
                collect.INV_BALANCE = f.INV_NET - collect.INV_COLLECTN;
                collect.INV_CN_TOTAL = f.INV_CN_TOTAL + (totalCn - f.INV_CN_TOTAL);
                result = new BLFacturaCobro().ActualizarCollects(collect);


                #region Aplicar pago a factura detalle
                //Prueba listando factura para ver que trae
                List<BEFacturaDetalle> fdnc = new List<BEFacturaDetalle>();
                fdnc = new BLFacturaDetalle().ListarFacturaDetalleAplicar(GlobalVars.Global.OWNER, idfactnuevo);

                foreach (var y in fdnc)
                {
                    foreach (var item in fd)
                    {
                        if (item.LIC_PL_ID == y.LIC_PL_ID)
                        {
                            //
                            var porDet = ((item.INVL_NET * 100) / f.INV_NET);
                            var valPorDet = (porDet / 100);
                            var collectt = valPorDet * collect.INV_COLLECTT;
                            //asi deberia ser :  " var collectb = item.INVL_COLLECTB+y.INVL_BASE; "
                            //El valor Ingresado * el balance  
                            var newInvlDisc = y.INVL_NET / item.INVL_NET;
                            //el total de descuento = totaldescuento +(descuento * newInvlDis)
                            var collectd = item.INVL_COLLECTD + (item.INVL_DISC * newInvlDisc);
                            //asi estaba 21-09-16 17:21
                            //var collectb = (item.INVL_COLLECTB + y.INVL_BASE) + collectd;
                            //
                            var collectb = (item.INVL_COLLECTB) + (item.INVL_GROSS * newInvlDisc);
                            //ANTES SE SUMABA item.invl_collectn +(collectb+collectt)-collectd
                            var collectn = (collectb + collectt) - collectd;

                            BEFacturaDetalle en = new BEFacturaDetalle();
                            en.OWNER = GlobalVars.Global.OWNER;
                            en.INVL_ID = item.INVL_ID;
                            en.INV_ID = item.INV_ID;
                            en.INVL_COLLECTB = collectb;
                            en.INVL_COLLECTT = collectt;
                            en.INVL_COLLECTD = collectd;//
                            en.INVL_COLLECTN = collectn;//No cuadran los 
                            en.INVL_BALANCE = item.INVL_NET - en.INVL_COLLECTN;
                            en.LOG_USER_UPDATE = enr.LOG_USER_UPDATE;
                            en.INVL_CN_TOTAL = item.INVL_CN_TOTAL + y.INVL_COLLECTN;
                            result = new BLFacturaDetalle().AplicarPagoDetalleFactura(en);
                        }
                    }
                }

                #endregion

                transa.Complete();
            }
            return result;
        }

        public int AplicarFactura_ImpuestoBase_NC(BEReciboDetalle enr, decimal inv_base, decimal inv_taxes, bool impuestoCero, decimal TotalFactura, decimal TotalImpuesto, decimal NetoFactAcumlado, decimal BaseACumulado)
        {
            var result = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                BEFactura collect = new BEFactura();
                collect.OWNER = GlobalVars.Global.OWNER;
                collect.LOG_USER_UPDATE = enr.LOG_USER_UPDATE;
                collect.INV_ID = enr.INV_ID;

                var f = new BLFacturaCobro().ObtenerFacturaAplicar(GlobalVars.Global.OWNER, enr.INV_ID);
                var fd = new BLFacturaDetalle().ListarFacturaDetalleAplicar(GlobalVars.Global.OWNER, enr.INV_ID);

                if (impuestoCero)
                {
                    collect.INV_COLLECTB = inv_base;
                    collect.INV_COLLECTT = inv_taxes;
                    collect.INV_COLLECTN = collect.INV_COLLECTB + collect.INV_COLLECTT;
                    collect.INV_BALANCE = TotalFactura - enr.REC_TOTAL_PAGAR;

                    #region Aplicar pago a factura detalle
                    foreach (var item in fd)
                    {
                        var porDet = ((item.INVL_NET * 100) / f.INV_NET);
                        var valPorDet = (porDet / 100);
                        var collectb = valPorDet * collect.INV_COLLECTB;
                        var collectt = valPorDet * collect.INV_COLLECTT;
                        var collectn = collectb + collectt;

                        BEFacturaDetalle en = new BEFacturaDetalle();
                        en.OWNER = GlobalVars.Global.OWNER;
                        en.INVL_ID = item.INVL_ID;
                        en.INV_ID = item.INV_ID;
                        en.INVL_COLLECTB = collectb;
                        en.INVL_COLLECTT = collectt;
                        en.INVL_COLLECTN = collectn;
                        en.INVL_BALANCE = item.INVL_NET - en.INVL_COLLECTN;
                        en.LOG_USER_UPDATE = enr.LOG_USER_UPDATE;

                        result = new BLFacturaDetalle().AplicarPagoDetalleFactura(en);
                    }
                    #endregion
                }
                else
                {
                    collect.INV_COLLECTB = enr.REC_BASE + BaseACumulado;
                    collect.INV_COLLECTT = enr.REC_TAXES + NetoFactAcumlado;
                    collect.INV_COLLECTN = collect.INV_COLLECTB + collect.INV_COLLECTT;
                    collect.INV_BALANCE = TotalFactura - enr.REC_TOTAL_PAGAR;

                    #region Aplicar pago a factura detalle
                    foreach (var item in fd)
                    {
                        var porDet = ((item.INVL_NET * 100) / f.INV_NET);
                        var valPorDet = (porDet / 100);
                        var collectb = valPorDet * collect.INV_COLLECTB;
                        var collectt = valPorDet * collect.INV_COLLECTT;
                        var collectn = collectb + collectt;

                        BEFacturaDetalle en = new BEFacturaDetalle();
                        en.OWNER = GlobalVars.Global.OWNER;
                        en.INVL_ID = item.INVL_ID;
                        en.INV_ID = item.INV_ID;
                        en.INVL_COLLECTB = collectb;
                        en.INVL_COLLECTT = collectt;
                        en.INVL_COLLECTN = collectn;
                        en.INVL_BALANCE = item.INVL_NET - en.INVL_COLLECTN;
                        en.LOG_USER_UPDATE = enr.LOG_USER_UPDATE;

                        result = new BLFacturaDetalle().AplicarPagoDetalleFactura(en);
                    }
                    #endregion
                }

                result = new BLFacturaCobro().ActualizarCollects(collect);

                transa.Complete();
            }
            return result;
        }

        public int AplicarFactura_BaseImpuesto_NC(BEReciboDetalle enr, decimal inv_base, decimal inv_taxes, bool baseCero, decimal TotalFactura, decimal TotalBase)
        {
            var result = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                BEFactura collect = new BEFactura();
                collect.OWNER = GlobalVars.Global.OWNER;
                collect.LOG_USER_UPDATE = enr.LOG_USER_UPDATE;
                collect.INV_ID = enr.INV_ID;

                var f = new BLFacturaCobro().ObtenerFacturaAplicar(GlobalVars.Global.OWNER, enr.INV_ID);
                var fd = new BLFacturaDetalle().ListarFacturaDetalleAplicar(GlobalVars.Global.OWNER, enr.INV_ID);

                if (baseCero)
                {
                    collect.INV_COLLECTB = inv_base;
                    collect.INV_COLLECTT = inv_taxes;
                    collect.INV_COLLECTN = collect.INV_COLLECTB + collect.INV_COLLECTT;
                    collect.INV_BALANCE = TotalFactura - enr.REC_TOTAL_PAGAR;

                    #region Aplicar pago a factura detalle
                    foreach (var item in fd)
                    {
                        var porDet = ((item.INVL_NET * 100) / f.INV_NET);
                        var valPorDet = (porDet / 100);
                        var collectb = valPorDet * collect.INV_COLLECTB;
                        var collectt = valPorDet * collect.INV_COLLECTT;
                        var collectn = collectb + collectt;

                        BEFacturaDetalle en = new BEFacturaDetalle();
                        en.OWNER = GlobalVars.Global.OWNER;
                        en.INVL_ID = item.INVL_ID;
                        en.INV_ID = item.INV_ID;
                        en.INVL_COLLECTB = collectb;
                        en.INVL_COLLECTT = collectt;
                        en.INVL_COLLECTN = collectn;
                        en.INVL_BALANCE = item.INVL_NET - en.INVL_COLLECTN;
                        en.LOG_USER_UPDATE = enr.LOG_USER_UPDATE;

                        result = new BLFacturaDetalle().AplicarPagoDetalleFactura(en);
                    }
                    #endregion
                }
                else
                {
                    collect.INV_COLLECTB = enr.REC_BASE;
                    collect.INV_COLLECTT = enr.REC_TAXES;
                    collect.INV_COLLECTN = collect.INV_COLLECTB;
                    collect.INV_BALANCE = TotalFactura - enr.REC_TOTAL_PAGAR;

                    #region Aplicar pago a factura detalle
                    foreach (var item in fd)
                    {
                        var porDet = ((item.INVL_NET * 100) / f.INV_NET);
                        var valPorDet = (porDet / 100);
                        var collectb = valPorDet * collect.INV_COLLECTB;
                        var collectt = valPorDet * collect.INV_COLLECTT;
                        var collectn = collectb + collectt;

                        BEFacturaDetalle en = new BEFacturaDetalle();
                        en.OWNER = GlobalVars.Global.OWNER;
                        en.INVL_ID = item.INVL_ID;
                        en.INV_ID = item.INV_ID;
                        en.INVL_COLLECTB = collectb;
                        en.INVL_COLLECTT = collectt;
                        en.INVL_COLLECTN = collectn;
                        en.INVL_BALANCE = item.INVL_NET - en.INVL_COLLECTN;
                        en.LOG_USER_UPDATE = enr.LOG_USER_UPDATE;

                        result = new BLFacturaDetalle().AplicarPagoDetalleFactura(en);
                    }
                    #endregion
                }

                result = new BLFacturaCobro().ActualizarCollects(collect);

                transa.Complete();
            }
            return result;
        }
        #endregion


        public List<BERecibo> ListarRecibosPendientes(string owner, decimal usuDerecho)
        {
            return new DARecibo().ListarRecibosPendientes(owner, usuDerecho);
        }

        public BERecibo ObtenerDatos(string owner, decimal idRecibo)
        {
            return new DARecibo().ObtenerDatos(owner, idRecibo);
        }

        public int ActualizarTotalQuitar(BERecibo en)
        {
            return new DARecibo().ActualizarTotalQuitar(en);
        }

        public int ActualizarTotalAgregar(BERecibo en)
        {
            return new DARecibo().ActualizarTotalAgregar(en);
        }

        public BEReciboDetalle ObtenerDatosDetalle(string owner, decimal idRecibo)
        {
            return new DARecibo().ObtenerDatosDetalle(owner, idRecibo);
        }

        public int ActualizarSerie(string owner, decimal? id, string tipo, string user)
        {
            return new DARecibo().ActualizarSerie(owner, id, tipo, user);
        }
        public int VoucherDuplicidad(string owner, string idBanco, string fechaDeposito, string Voucher, decimal idVoucher)
        {
            return new DARecibo().VoucherDuplicidad(owner, idBanco, fechaDeposito, Voucher, idVoucher);
        }

        public SocioNegocio ObtenerCliente(string owner, decimal idBps)
        {
            return new DARecibo().ObtenerCliente(owner, idBps);
        }

        public List<BERecibo> ObtenerRecibosCliente(string owner, decimal idRecibo, string version)
        {
            return new DARecibo().ObtenerRecibosCliente(owner, idRecibo, version);
        }

        public List<BEReciboDetalle> ObtenerRecibosDetalle(string owner, decimal idRecibo, string version)
        {
            return new DAReciboDetalle().ObtenerRecibosDetalle(owner, idRecibo, version);
        }
        
        public int VoucherRepetidosConfirmados(decimal idVoucher, string nro_confirmacion)
        {
            return new DARecibo().VoucherRepetidosConfirmados(idVoucher, nro_confirmacion);
        }

    }
}

using SGRDA.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;

namespace Proyect_Apdayc.Controllers.Recaudacion
{
    public class FacturacionController : Base
    {
        //
        // GET: /Facturacion/

        public ActionResult Index()
        {
            return View();
        }

        public class DescuentoAprobaciones
        {
            public const int Aprobado = 1;
            public const int Rechazado = 2;
            public const int Pendiente = 0;

        }

        /// <summary>
        /// OBTIENE LOS MONTOS DETALLE DE FACTURA.
        /// SI LA FACTURA NO HA SIDO CREADA MUESTRA EL DETALLE DE MONTOS A FACTURAR
        /// SI YA FUE FACTURADO TRAE LOS MONTOS QUE APARECEN EN LA FACTURA.
        /// </summary>
        /// <param name="idLicencia"></param>
        /// <param name="idPlanificacion"></param>
        /// <returns></returns>
        public DTOMontoTotal obtenerMontoFactura(decimal idLicencia, decimal idPlanificacion)
        {

            BLLicencias servLicencia = new BLLicencias();


            DTOMontoTotal retorno = new DTOMontoTotal();
            retorno.ValorDescuento = 0;
            retorno.ValorCargo = 0;
            retorno.ValorImpuesto = 0;
            retorno.ValorTarifa = 0;
            retorno.TieneFacturacion = false;


            var licencia = servLicencia.ObtenerLicenciaXCodigo(idLicencia, GlobalVars.Global.OWNER);
            var tarifa = new BLREC_RATES_GRAL().Obtener(GlobalVars.Global.OWNER, licencia.RATE_ID);

            var resp = new BLTarifaTest().ObtieneTarifaDescuentoEspecial(licencia.RATE_ID);
            if (licencia != null)
            {
                /*
                 * OBTENER EL ESTADO DE LA PLANIFICACION
                 * VERIFICAR SI ESTA ABIERTO O CERRADA PARA OBTNER MONTOS.
                 */
                BLLicenciaPlaneamiento ServPlan = new BLLicenciaPlaneamiento();
                var planLicencia = ServPlan.ObtenerPlanificacion(GlobalVars.Global.OWNER, idPlanificacion);
                if (planLicencia != null)
                {
                    if (planLicencia.LIC_PL_STATUS != null && planLicencia.LIC_PL_STATUS == "A")
                    {
                        #region MONTOSA FACTURAR CUANDO LA PLANIFICACION NO HA GENERADO FACTRURAS, TODAVIA



                        BLLicenciaDescuento servDescto = new BLLicenciaDescuento();

                        /*TEST DE TARIFA*/
                        Tarifa.TarifaTestController testTarifa = new Tarifa.TarifaTestController();
                        decimal resultTT = testTarifa.CalcularTestTarifa(licencia.RATE_ID, idLicencia, idPlanificacion);
                        if (resultTT == -999 || resultTT == -998) resultTT = 0;
                        /*FIN TEST DE TARIFA*/
                        int cantDecimales = 0;

                        if (tarifa.RATE_REDONDEO == 0)
                            cantDecimales = this.getCantDecimal((double)(resultTT));
                        else
                            cantDecimales = Convert.ToInt32(tarifa.RATE_MDECI);

                        decimal totalImpuestoAcum = 0;
                        decimal impAcumPer = 0;
                        decimal impAcumVal = 0;
                        var impuestos = new BLLicenciaImpuesto().ListaImpuesto(GlobalVars.Global.OWNER, licencia.EST_ID);
                        if (impuestos != null)
                        {
                            impuestos.ForEach(x =>
                            {
                                impAcumPer = impAcumPer + x.TAXV_VALUEP;
                                impAcumVal = impAcumVal + x.TAXV_VALUEM;
                            });
                            totalImpuestoAcum = ((resultTT * impAcumPer) / 100) + impAcumVal;
                        }

                        decimal mBaseTT = resultTT;
                        decimal valNeto = 0;
                        decimal NetoActualizado = 0;
                        decimal DsctoActumuladoB = 0;
                        decimal DsctoActumulado = 0;
                        decimal CargoAcumulado = 0;

                        var descuentos = servDescto.ListaDescuentos(GlobalVars.Global.OWNER, idLicencia);
                        descuentos = descuentos.Where(z => z.DISC_ESTADO == DescuentoAprobaciones.Aprobado).ToList();
                        foreach (var item in descuentos.OrderBy(y => y.ORDEN))
                        {
                            decimal valDesto = 0;
                            if (item.FORMATO == "%")
                            {
                                valDesto = (((item.DISC_ORG == "B" ? mBaseTT : NetoActualizado) * item.DISC_VALUE) / 100);
                            }
                            else
                            {
                                valDesto = item.DISC_VALUE;
                            }

                            //valDesto = (decimal)this.Truncate((double)valDesto, cantDecimales);
                            var esSuma = item.DISC_SIGN == "+";
                            if (esSuma)
                            {
                                DsctoActumuladoB = DsctoActumuladoB - valDesto;
                                CargoAcumulado = CargoAcumulado + valDesto;
                            }
                            else
                            {
                                DsctoActumuladoB = DsctoActumuladoB + valDesto;
                                DsctoActumulado = DsctoActumulado + valDesto;
                            }

                            if (item.DISC_ORG == "B")
                            {
                                if (esSuma)
                                    valNeto = mBaseTT + valDesto;
                                else
                                    valNeto = mBaseTT - valDesto;
                            }
                            else
                            {
                                valNeto = mBaseTT - DsctoActumuladoB;
                            }
                            NetoActualizado = valNeto;
                        }
                        //retorno.ValorDescuento = (decimal)this.Truncate((double)DsctoActumulado, cantDecimales);
                        retorno.ValorDescuento = (decimal)Math.Round(DsctoActumulado, cantDecimales);
                        retorno.ValorCargo = (decimal)this.Truncate((double)CargoAcumulado, cantDecimales);
                        retorno.ValorImpuesto = (decimal)this.Truncate((double)totalImpuestoAcum, cantDecimales);
                        //retorno.ValorTarifa = (decimal)this.Truncate((double)resultTT, cantDecimales);
                        retorno.ValorTarifa = (decimal)Math.Round(resultTT, cantDecimales);


                        retorno.ValorFinal = (((retorno.ValorTarifa.HasValue ? retorno.ValorTarifa : 0) + (retorno.ValorCargo.HasValue ? retorno.ValorCargo : 0) + (retorno.ValorImpuesto.HasValue ? retorno.ValorImpuesto : 0)) - (retorno.ValorDescuento.HasValue ? retorno.ValorDescuento : 0));
                        retorno.ValorFinal = retorno.ValorFinal == 0 ? null : retorno.ValorFinal;
                        /*END ADDON DBS  */

                        #endregion
                    }
                    else if (planLicencia.LIC_PL_STATUS == "P")
                    {
                        #region MONTO A FACTURAR DESPUES DE REALIZARCE UNA FACTURA



                        BLLicenciaDescuento servDescto = new BLLicenciaDescuento();

                        /*TEST DE TARIFA*/
                        Tarifa.TarifaTestController testTarifa = new Tarifa.TarifaTestController();
                        decimal resultTT = testTarifa.CalcularTestTarifa(licencia.RATE_ID, idLicencia, idPlanificacion);
                        if (resultTT == -999 || resultTT == -998) resultTT = 0;
                        /*FIN TEST DE TARIFA*/

                        var cantDecimales = this.getCantDecimal((double)(resultTT));
                        decimal totalImpuestoAcum = 0;
                        decimal impAcumPer = 0;
                        decimal impAcumVal = 0;
                        var impuestos = new BLLicenciaImpuesto().ListaImpuesto(GlobalVars.Global.OWNER, licencia.EST_ID);
                        if (impuestos != null)
                        {
                            impuestos.ForEach(x =>
                            {
                                impAcumPer = impAcumPer + x.TAXV_VALUEP;
                                impAcumVal = impAcumVal + x.TAXV_VALUEM;
                            });
                            totalImpuestoAcum = ((resultTT * impAcumPer) / 100) + impAcumVal;
                        }

                        decimal mBaseTT = resultTT;
                        decimal valNeto = 0;
                        decimal NetoActualizado = 0;
                        decimal DsctoActumuladoB = 0;
                        decimal DsctoActumulado = 0;
                        decimal CargoAcumulado = 0;

                        var descuentos = servDescto.ListaDescuentos(GlobalVars.Global.OWNER, idLicencia);
                        descuentos = descuentos.Where(z => z.DISC_ESTADO == DescuentoAprobaciones.Aprobado).ToList();
                        foreach (var item in descuentos.OrderBy(y => y.ORDEN))
                        {
                            decimal valDesto = 0;
                            if (item.FORMATO == "%")
                            {
                                valDesto = (((item.DISC_ORG == "B" ? mBaseTT : NetoActualizado) * item.DISC_VALUE) / 100);
                            }
                            else
                            {
                                valDesto = item.DISC_VALUE;
                            }

                            //valDesto = (decimal)this.Truncate((double)valDesto, cantDecimales); // desocmentar si sale error 
                            var esSuma = item.DISC_SIGN == "+";
                            if (esSuma)
                            {
                                DsctoActumuladoB = DsctoActumuladoB - valDesto;
                                CargoAcumulado = CargoAcumulado + valDesto;
                            }
                            else
                            {
                                DsctoActumuladoB = DsctoActumuladoB + valDesto;
                                DsctoActumulado = DsctoActumulado + valDesto;
                            }

                            if (item.DISC_ORG == "B")
                            {
                                if (esSuma)
                                    valNeto = mBaseTT + valDesto;
                                else
                                    valNeto = mBaseTT - valDesto;
                            }
                            else
                            {
                                valNeto = mBaseTT - DsctoActumuladoB;
                            }
                            NetoActualizado = valNeto;
                        }
                        //retorno.ValorDescuento = (decimal)this.Truncate((double)DsctoActumulado, cantDecimales); mal
                        //retorno.ValorDescuento = (decimal)Math.Round(DsctoActumulado, cantDecimales); bien // desocmentar si sale error 
                        retorno.ValorDescuento = (decimal)DsctoActumulado; //COMENTAR SI SALE ERROR

                        retorno.ValorCargo = (decimal)this.Truncate((double)CargoAcumulado, cantDecimales);
                        retorno.ValorImpuesto = (decimal)this.Truncate((double)totalImpuestoAcum, cantDecimales);
                        //retorno.ValorTarifa = (decimal)this.Truncate((double)resultTT, cantDecimales);
                        retorno.ValorTarifa = (decimal)Math.Round(resultTT, cantDecimales);


                        retorno.ValorFinal = (((retorno.ValorTarifa.HasValue ? retorno.ValorTarifa : 0) + (retorno.ValorCargo.HasValue ? retorno.ValorCargo : 0) + (retorno.ValorImpuesto.HasValue ? retorno.ValorImpuesto : 0)) - (retorno.ValorDescuento.HasValue ? retorno.ValorDescuento : 0));
                        retorno.ValorFinal = retorno.ValorFinal == 0 ? null : retorno.ValorFinal;
                        /*END ADDON DBS  */

                        #endregion
                        #region  MONTOS PLANIFICACION YA FACTUREADO
                        //retorno.ValorFinal = planLicencia.LIC_PL_AMOUNT;
                        retorno.TieneFacturacion = true;
                        BLFactura servFactura = new BLFactura();
                        var facturas = servFactura.FacturaXPlanificacion(idPlanificacion);
                        if (facturas != null && facturas.Count > 0) retorno.ValorFinalFacturado = facturas.Sum(x => x.LIC_INVOICE_LINE);

                        #endregion
                    }
                    else if (planLicencia.LIC_PL_STATUS == "T")
                    {
                        #region  MONTOS PLANIFICACION YA FACTUREADO
                        retorno.ValorFinal = planLicencia.LIC_PL_AMOUNT;
                        retorno.TieneFacturacion = true;
                        BLFactura servFactura = new BLFactura();
                        var facturas = servFactura.FacturaXPlanificacion(idPlanificacion);
                        if (facturas != null && facturas.Count > 0) retorno.ValorFinalFacturado = facturas.Sum(x => x.LIC_INVOICE_LINE);

                        #endregion
                    }

                    //if (resp == 1)
                    //{

                    //    double decimales = Convert.ToDouble( retorno.ValorFinal) - Convert.ToDouble( (long)retorno.ValorFinal);

                    //    if (decimales > 0.5 && decimales < 1)

                    //            decimales = 0.5;

                    //    else if (decimales < 0.5)

                    //            decimales = 0;

                    //    retorno.ValorDescuentoRedondeoEspecial = (decimal)Math.Round((Convert.ToDecimal(Convert.ToDouble(retorno.ValorFinal) - ((long)retorno.ValorFinal + decimales)))  , Convert.ToInt32( tarifa.RATE_MDECI)); ;
                    //    retorno.ValorFinal = Convert.ToDecimal( (long)retorno.ValorFinal+ decimales);



                    //}
                }

            }

            return retorno;
        }

        public DTOMontoTotal obtenerMontoFacturaCalc(decimal idLicencia, decimal idPlanificacion)
        {

            BLLicencias servLicencia = new BLLicencias();


            DTOMontoTotal retorno = new DTOMontoTotal();
            retorno.ValorDescuento = 0;
            retorno.ValorCargo = 0;
            retorno.ValorImpuesto = 0;
            retorno.ValorTarifa = 0;
            retorno.TieneFacturacion = false;


            var licencia = servLicencia.ObtenerLicenciaXCodigo(idLicencia, GlobalVars.Global.OWNER);
            var resp = new BLTarifaTest().ObtieneTarifaDescuentoEspecial(licencia.RATE_ID);
            if (licencia != null)
            {
                /*
                 * OBTENER EL ESTADO DE LA PLANIFICACION
                 * VERIFICAR SI ESTA ABIERTO O CERRADA PARA OBTNER MONTOS.
                 */
                BLLicenciaPlaneamiento ServPlan = new BLLicenciaPlaneamiento();
                var planLicencia = ServPlan.ObtenerPlanificacion(GlobalVars.Global.OWNER, idPlanificacion);
                if (planLicencia != null)
                {

                    #region MONTOSA FACTURAR CUANDO LA PLANIFICACION NO HA GENERADO FACTRURAS, TODAVIA



                    BLLicenciaDescuento servDescto = new BLLicenciaDescuento();

                    /*TEST DE TARIFA*/
                    Tarifa.TarifaTestController testTarifa = new Tarifa.TarifaTestController();
                    decimal resultTT = testTarifa.CalcularTestTarifa(licencia.RATE_ID, idLicencia, idPlanificacion);
                    if (resultTT == -999 || resultTT == -998) resultTT = 0;
                    /*FIN TEST DE TARIFA*/

                    var cantDecimales = this.getCantDecimal((double)(resultTT));
                    decimal totalImpuestoAcum = 0;
                    decimal impAcumPer = 0;
                    decimal impAcumVal = 0;
                    var impuestos = new BLLicenciaImpuesto().ListaImpuesto(GlobalVars.Global.OWNER, licencia.EST_ID);
                    if (impuestos != null)
                    {
                        impuestos.ForEach(x =>
                        {
                            impAcumPer = impAcumPer + x.TAXV_VALUEP;
                            impAcumVal = impAcumVal + x.TAXV_VALUEM;
                        });
                        totalImpuestoAcum = ((resultTT * impAcumPer) / 100) + impAcumVal;
                    }

                    decimal mBaseTT = resultTT;
                    decimal valNeto = 0;
                    decimal NetoActualizado = 0;
                    decimal DsctoActumuladoB = 0;
                    decimal DsctoActumulado = 0;
                    decimal CargoAcumulado = 0;

                    var descuentos = servDescto.ListaDescuentos(GlobalVars.Global.OWNER, idLicencia);
                    descuentos = descuentos.Where(z => z.DISC_ESTADO == DescuentoAprobaciones.Aprobado).ToList();
                    foreach (var item in descuentos.OrderBy(y => y.ORDEN))
                    {
                        decimal valDesto = 0;
                        if (item.FORMATO == "%")
                        {
                            valDesto = (((item.DISC_ORG == "B" ? mBaseTT : NetoActualizado) * item.DISC_VALUE) / 100);
                        }
                        else
                        {
                            valDesto = item.DISC_VALUE;
                        }

                        valDesto = (decimal)this.Truncate((double)valDesto, cantDecimales);
                        var esSuma = item.DISC_SIGN == "+";
                        if (esSuma)
                        {
                            DsctoActumuladoB = DsctoActumuladoB - valDesto;
                            CargoAcumulado = CargoAcumulado + valDesto;
                        }
                        else
                        {
                            DsctoActumuladoB = DsctoActumuladoB + valDesto;
                            DsctoActumulado = DsctoActumulado + valDesto;
                        }

                        if (item.DISC_ORG == "B")
                        {
                            if (esSuma)
                                valNeto = mBaseTT + valDesto;
                            else
                                valNeto = mBaseTT - valDesto;
                        }
                        else
                        {
                            valNeto = mBaseTT - DsctoActumuladoB;
                        }
                        NetoActualizado = valNeto;
                    }
                    //retorno.ValorDescuento = (decimal)this.Truncate((double)DsctoActumulado, cantDecimales);
                    retorno.ValorDescuento = (decimal)Math.Round(DsctoActumulado, cantDecimales);
                    retorno.ValorCargo = (decimal)this.Truncate((double)CargoAcumulado, cantDecimales);
                    retorno.ValorImpuesto = (decimal)this.Truncate((double)totalImpuestoAcum, cantDecimales);
                    //retorno.ValorTarifa = (decimal)this.Truncate((double)resultTT, cantDecimales);
                    retorno.ValorTarifa = (decimal)Math.Round(resultTT, cantDecimales);


                    retorno.ValorFinal = (((retorno.ValorTarifa.HasValue ? retorno.ValorTarifa : 0) + (retorno.ValorCargo.HasValue ? retorno.ValorCargo : 0) + (retorno.ValorImpuesto.HasValue ? retorno.ValorImpuesto : 0)) - (retorno.ValorDescuento.HasValue ? retorno.ValorDescuento : 0));
                    retorno.ValorFinal = retorno.ValorFinal == 0 ? null : retorno.ValorFinal;
                    /*END ADDON DBS  */

                    if (resp == 1)
                    {

                        double decimales = Convert.ToDouble(retorno.ValorFinal) - Convert.ToDouble((long)retorno.ValorFinal);

                        if (decimales > 0.5 && decimales < 1)

                            decimales = 0.5;

                        else if (decimales < 0.5)

                            decimales = 0;

                        retorno.ValorDescuentoRedondeoEspecial = Convert.ToDecimal(Convert.ToDouble(retorno.ValorFinal) - ((long)retorno.ValorFinal + decimales));
                        retorno.ValorFinal = Convert.ToDecimal((long)retorno.ValorFinal + decimales);



                    }
                    #endregion


                }

            }

            return retorno;
        }
    }
}

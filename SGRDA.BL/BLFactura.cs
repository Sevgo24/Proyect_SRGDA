using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
using SGRDA.Entities.FacturaElectronica;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
namespace SGRDA.BL
{
    public class BLFactura
    {
        #region VARIABLES
        string msjErrorValores = "No se realizo correctamente el calculo de la tarifa, verificar los valores de la caracteristicas.";
        string msjErrorActualizarValores = "Los valores de la licencia deben ser actualizados antes de proceder al calculo de la licencia.";
        string msjErrorRegistrarValores = "No existen valores de las caracteristicas.";
        string msjPeriodosEnBorrador = "PERIODO(S) EN BORRADOR";
        string msjPeriodosEnFacturacion = "PERIODO(S) FACTURANDOSE";
        public class Seleccion
        {
            public const string NO = "0";
            public const string SI = "1";
            public const string MANUAL = "2";
        }
        public class LetraCar
        {
            public const string A = "A";
            public const string B = "B";
            public const string C = "C";
            public const string D = "D";
            public const string E = "E";
        }
        public class LetraReg
        {
            public const string R = "R";
            public const string V = "V";
            public const string T = "T";
            public const string W = "W";
            public const string X = "X";
            public const string Y = "Y";
            public const string Z = "Z";
            public const string Rmin = "Rmin";
        }
        private class Variables
        {
            public const int SI = 1;
            public const int NO = 0;
            public const int CERO = 0;
            public const int FACTURACION_MASIVA = 1;
            public const int FACTURACION_INDIVIDUAL = 0;
            public const string OBSERVACION_EMISION_INDIVIDUAL = "LA LICENCIA POSEE MAS DE DOS DOCUMENTOS PENDIENTES";
        }

        /// <summary>
        /// CLASE DONDE SE ENCUENTRA LAS VARIABLES A UTILIZAR PARA LOS DESCUENTOS DE LA PLANTILLA
        /// </summary>
        public class DescPlantilla
        {
            /// <summary>
            /// SI = 1
            /// </summary>
            public const int SI = 1;
            /// <summary>
            /// NO = 0
            /// </summary>
            public const int NO = 0;
            /// <summary>
            /// ANUAL =12
            /// </summary>
            public const int ANUAL = 12;
            /// <summary>
            /// SEMESTRAL = 6
            /// </summary>
            public const int SEMESTRAL = 6;
            /// <summary>
            /// ABIERTO=A
            /// </summary>
            public const string ABIERTO = "A";
            /// <summary>
            /// PENDIENTE=P
            /// </summary>
            public const string PENDIENTE = "P";
            /// <summary>
            /// DESCUENTO PARA USUARIOS GENERALES DE LOCALES PERMANENTES (NO CADENAS) //DISC_ID
            /// </summary>
            public const int DESCUENTO_INDIVIDUAL = 35;
            /// <summary>
            /// USUARIOS DE MUSICA INDISPENSABLE
            /// </summary>
            public const int MUSICA_INDISPENSABLE = 0;
        }
        //public decimal VUM = new BLValormusica().ObtenerActivo(GlobalVars.Global.OWNER).VUM_VAL;


        #endregion

        #region COBRO
        //***************** COBRO **************************
        //public BEFactura ListarFacturaPendientePago(string owner, decimal usuDerecho, decimal importe, string moneda, decimal idFact)
        //{
        //    BEFactura Factura = new BEFactura();
        //    Factura.ListarFactura = new DAFactura().ListarFacturaPendientePago(owner, usuDerecho, importe, moneda);
        //    Factura.ListarLicencia = new DALicencias().ListarFacturaPendienteDetalle(owner, idFact);
        //    Factura.ListarDetalleFactura = new DAFacturaDetalle().ListarFacturaPendienteDetalle_subDetalle(owner, idFact);

        //    Factura.ListarRecibosPendientes = new DARecibo().ListarRecibosPendientes(owner, usuDerecho);
        //    return Factura;
        //}    


        //public BEFactura ObtenerFacturaAplicar(string owner, decimal idFactura)
        //{
        //    return new DAFactura().ObtenerFacturaAplicar(owner, idFactura);
        //}

        //public int ActualizarCollects(BEFactura en)
        //{
        //    return new DAFactura().ActualizarCollects(en);
        //}
        //***************************************************************************
        #endregion

        #region FACTURACION
        public bool ActualizarEstadoDefinitivaXML(string owner,
                         List<BEFactura> listaFactura, List<BEFacturaDetalle> ListaDetalleFact,
                         List<BELicenciaPlaneamiento> ListaPlanificacion,
                         List<BELicenciaPlaneamientoDetalle> listaDetallePlaneamiento,
                         List<BEFacturaDescuento> ListaDescuentoXdetalle)
        {
            bool result = false;
            bool exitoFacturaVencimiento = false;
            bool exitoFacturaDetalle = false;
            bool exitoPlanificacionDetalle = false;
            bool exitoPlanificacion = false;

            string xmlFactura = string.Empty;
            string xmlDetalleFactura = string.Empty;
            string xmlDetalleDescuento = string.Empty;
            string xmlPlanificacionDetalle = string.Empty;
            string xmlPlanificacion = string.Empty;
            xmlFactura = Utility.Util.SerializarEntity(listaFactura);

            using (TransactionScope transa = new TransactionScope())
            {
                result = new DAFactura().ActualizarEstadoDefinitvoXML(xmlFactura);
                if (result)
                {
                    exitoFacturaVencimiento = new DAFacturaVencimiento().RegistrarVencimientoFactXML(xmlFactura);

                    xmlDetalleFactura = Utility.Util.SerializarEntity(ListaDetalleFact);
                    exitoFacturaDetalle = new DAFacturaDetalle().ActualizarDetalleFactXML(owner, xmlDetalleFactura);

                    xmlPlanificacionDetalle = Utility.Util.SerializarEntity(listaDetallePlaneamiento);
                    exitoPlanificacionDetalle = new DALicenciaPlaneamientoDetalle().InsertarXML(xmlPlanificacionDetalle);

                    xmlPlanificacion = Utility.Util.SerializarEntity(ListaPlanificacion);
                    exitoPlanificacion = new DALicenciaPlaneamiento().ActualizarPlanificacionFacturaXML(xmlPlanificacion);

                    xmlDetalleDescuento = Utility.Util.SerializarEntity(ListaDescuentoXdetalle);
                    exitoFacturaDetalle = new DAFacturaDetalle().RegistrarDetalleDescuento(owner, xmlDetalleDescuento);
                }
                transa.Complete();
            }
            return result;
        }

        public bool InsertarDetalleFacturaxml(string owner, List<BEFacturaDetalle> ListaDetalleFact)
        {
            bool exitoFacturaDetalle = false;
            string xmlDetalleFactura = string.Empty;

            xmlDetalleFactura = Utility.Util.SerializarEntity(ListaDetalleFact);
            exitoFacturaDetalle = new DAFacturaDetalle().ActualizarDetalleFactXML(owner, xmlDetalleFactura);

            return exitoFacturaDetalle;
        }

        public bool InsertarPlanificacionxml(string owner, List<BELicenciaPlaneamiento> ListaPlanificacion)
        {
            bool exitoPlanificacion = false;
            string xmlPlanificacion = string.Empty;

            xmlPlanificacion = Utility.Util.SerializarEntity(ListaPlanificacion);
            exitoPlanificacion = new DALicenciaPlaneamiento().ActualizarPlanificacionFacturaXML(xmlPlanificacion);

            return exitoPlanificacion;
        }

        public bool ActualizaPlanificacionDetallexml(string owner, List<BELicenciaPlaneamientoDetalle> listaDetallePlaneamiento)
        {
            bool exitoPlanificacionDetalle = false;
            string xmlPlanificacionDetalle = string.Empty;

            xmlPlanificacionDetalle = Utility.Util.SerializarEntity(listaDetallePlaneamiento);
            exitoPlanificacionDetalle = new DALicenciaPlaneamientoDetalle().InsertarXML(xmlPlanificacionDetalle);

            return exitoPlanificacionDetalle;
        }

        public bool InsertaDescuento(string owner, List<BEFacturaDescuento> ListaDescuentoXdetalle)
        {
            bool exitoFacturaDetalle = false;
            string xmlDetalleDescuento = string.Empty;

            xmlDetalleDescuento = Utility.Util.SerializarEntity(ListaDescuentoXdetalle);
            exitoFacturaDetalle = new DAFacturaDetalle().RegistrarDetalleDescuento(owner, xmlDetalleDescuento);

            return exitoFacturaDetalle;
        }

        public bool ActualizarFacturaCabecera(BEFactura fac)
        {
            bool result = false;
            bool exitoVencimiento = false;

            result = new DAFactura().ActualizarEstadoDefinitivo(fac);
            exitoVencimiento = new DAFacturaVencimiento().InsertarVencimiento(fac);
            return result;
        }

        //NUEVO PROCEDIMIENTO PARA INSERTAR LA FACTURACION MASIVA DE UNO A UNO
        //public bool ActualizarEstadoDefinitivo(List<BEFactura> listaFactura, List<BEFacturaDetalle> ListaDetalleFact,
        //         List<BELicenciaPlaneamiento> ListaPlanificacion,
        //         List<BELicenciaPlaneamientoDetalle> listaDetallePlaneamiento,
        //         List<BEFacturaDescuento> ListaDescuentoXdetalle)

        public bool ActualizarEstadoDefinitivo(BEFactura fac, BEFacturaDetalle det, BELicenciaPlaneamiento pl, BELicenciaPlaneamientoDetalle plDet, BEFacturaDescuento desc)
        {
            //BEFacturaDetalle detalle = new BEFacturaDetalle();
            bool result = false;
            bool exitoVencimiento = false;
            bool exitoDetalle = false;

            result = new DAFactura().ActualizarEstadoDefinitivo(fac);
            exitoVencimiento = new DAFacturaVencimiento().InsertarVencimiento(fac);
            if (result && exitoVencimiento)
            {
                exitoDetalle = new DAFacturaDetalle().ActualizarDetalleFactura(det);

                if (exitoDetalle)
                {
                    var exitoPlanificacion = new DALicenciaPlaneamiento().ActualizarPlanificacion(pl);
                }

                var exitoPlanificacionDetalle = new DALicenciaPlaneamientoDetalle().ActualizarDetallePlanificacion(plDet);
                var exitoDetalleDescuento = new DAFacturaDetalle().ActualizarDetalleDescuento(desc);
            }

            //    if (listaFactura != null)
            //    {
            //        foreach (var item in listaFactura)
            //        {
            //            result = new DAFactura().ActualizarEstadoDefinitivo(item);
            //            exitoVencimiento = new DAFacturaVencimiento().InsertarVencimiento(item);

            //            if (result && exitoVencimiento)
            //            {
            //                //VALIDA SI TIENE DETALLE
            //                if (ListaDetalleFact != null)
            //                {
            //                    //INSERTA LOS DETALLES DEL COMPROBANTE
            //                    foreach (var item1 in ListaDetalleFact)
            //                    {
            //                        exitoDetalle = new DAFacturaDetalle().ActualizarDetalleFactura(item1);
            //                    }

            //                    if (exitoDetalle)
            //                    {
            //                        //VALIDA SI TIENE PLANIFICACION
            //                        if (ListaPlanificacion != null)
            //                        {
            //                            foreach (var item2 in ListaPlanificacion)
            //                            {
            //                                var exitoPlanificacion = new DALicenciaPlaneamiento().ActualizarPlanificacion(item2);
            //                            }
            //                        }

            //                        //VALIDA SI TIENE PLANIFICACION
            //                        if (listaDetallePlaneamiento != null)
            //                        {
            //                            //INSERTA EL DETALLE
            //                            foreach (var item3 in listaDetallePlaneamiento)
            //                            {
            //                                var exitoPlanificacionDetalle = new DALicenciaPlaneamientoDetalle().ActualizarDetallePlanificacion(item3);
            //                            }
            //                        }

            //                        //VALIDA SI TIENE DESCUENTOS
            //                        if (ListaDescuentoXdetalle != null)
            //                        {
            //                            //INSERTA LOS DESCUENTOS
            //                            foreach (var item4 in ListaDescuentoXdetalle)
            //                            {
            //                                var exitoDetalleDescuento = new DAFacturaDetalle().ActualizarDetalleDescuento(item4);
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //}
            return result;
        }

        public List<BEFactura> ObtenerCorrelativo(decimal serie)
        {
            return new DAFactura().ObtenerCorrelativo(serie);
        }

        //public bool ActualizarCorreativo(decimal idserie, decimal Correlativo)
        //{
        //    return new DAFactura().ActualizarCorrelativo(idserie, Correlativo);
        //}

        public bool InsertarBorradorXML(List<BEFactura> listaFactura, string owner)
        {
            bool result = false;
            bool exitoFactura = false;
            bool exitoFacturaDetalle = false;
            string xmlFactura = string.Empty;

            using (TransactionScope transa = new TransactionScope())
            {
                xmlFactura = Utility.Util.SerializarEntity(listaFactura);
                result = new DAFactura().InsertarBorradorXML(xmlFactura, owner);
                if (result)
                {
                    var resultDet = new DAFacturaDetalle().InsertarDetalleBorradorXML(xmlFactura, owner);
                    if (resultDet)
                    {
                        var resultValCar = new DAFacturaDetalle().InsertarValoresCaracteristicaDetalleXML(xmlFactura, owner);
                    }
                }
                transa.Complete();
            }
            return result;
        }

        public BEFactura ListarFacturaBorrador(string owner, DateTime fini, DateTime ffin,
                                                decimal tipoLic, string idMoneda, decimal idGrufact,
                                                decimal idBps, decimal idCorrelativo, string idTipoPago,
                                                int conFecha, decimal idLic, decimal idfactura, decimal off_id)
        {
            BEFactura FacturaMasiva = new BEFactura();
            FacturaMasiva.ListarFactura = new DAFactura().ListarFacturaBorrador(owner, fini, ffin,
                                                         tipoLic, idMoneda, idGrufact, idBps, idCorrelativo, idTipoPago,
                                                         conFecha, idLic, idfactura, off_id);
            FacturaMasiva.ListarLicencia = new DALicencias().ListarLicenciaBorrador(owner, fini, ffin,
                                                         tipoLic, idMoneda, idGrufact, idBps, idCorrelativo, idTipoPago,
                                                         conFecha, idLic, idfactura, off_id);
            FacturaMasiva.ListarDetalleFactura = new DAFacturaDetalle().ListarFacturaBorrador_LicPlanemientoSubGrilla(
                                                         owner, fini, ffin,
                                                         tipoLic, idMoneda, idGrufact, idBps, idCorrelativo, idTipoPago,
                                                         conFecha, idLic, idfactura, off_id);

            //*********** Obtener Descuentos *****************
            List<BELicencias> listaLicencia = new List<BELicencias>();
            BELicencias objLicencia = null;
            var licenciaTemp = from c in FacturaMasiva.ListarLicencia
                               group c by new
                               {
                                   c.OWNER,
                                   c.LIC_ID,
                               } into gcs
                               select new
                               {
                                   owner = gcs.Key.OWNER,
                                   licId = gcs.Key.LIC_ID, //Children = gcs.ToList(),
                               };

            foreach (var item in licenciaTemp)
            {
                objLicencia = new BELicencias();
                objLicencia.OWNER = item.owner;
                objLicencia.LIC_ID = item.licId;
                listaLicencia.Add(objLicencia);
            }
            //**** Obteniedo datos de las tarifas de la Licencia **********************
            string xmlLicencia = string.Empty;
            xmlLicencia = Utility.Util.SerializarEntity(listaLicencia);
            FacturaMasiva.ListarDescuentos = new DADescuentos().ObtenerDescuentoLicXML(owner, xmlLicencia);
            //*************************************************************
            return FacturaMasiva;
        }

        public BEFactura ListarFacturaBorradorSerie(string owner, DateTime fini, DateTime ffin,
                                                decimal tipoLic, string idMoneda, decimal idGrufact,
                                                decimal idBps, decimal idCorrelativo, string idTipoPago,
                                                int conFecha, decimal idLic, decimal idfactura, decimal off_id)
        {
            BEFactura FacturaMasiva = new BEFactura();
            FacturaMasiva.ListarFactura = new DAFactura().ListarFacturaBorradorSerie(owner, fini, ffin,
                                                         tipoLic, idMoneda, idGrufact, idBps, idCorrelativo, idTipoPago,
                                                         conFecha, idLic, idfactura, off_id);
            FacturaMasiva.ListarLicencia = new DALicencias().ListarLicenciaBorrador(owner, fini, ffin,
                                                         tipoLic, idMoneda, idGrufact, idBps, idCorrelativo, idTipoPago,
                                                         conFecha, idLic, idfactura, off_id);
            FacturaMasiva.ListarDetalleFactura = new DAFacturaDetalle().ListarFacturaBorrador_LicPlanemientoSubGrilla(
                                                         owner, fini, ffin,
                                                         tipoLic, idMoneda, idGrufact, idBps, idCorrelativo, idTipoPago,
                                                         conFecha, idLic, idfactura, off_id);

            //*********** Obtener Descuentos *****************
            List<BELicencias> listaLicencia = new List<BELicencias>();
            BELicencias objLicencia = null;
            var licenciaTemp = from c in FacturaMasiva.ListarLicencia
                               group c by new
                               {
                                   c.OWNER,
                                   c.LIC_ID,
                               } into gcs
                               select new
                               {
                                   owner = gcs.Key.OWNER,
                                   licId = gcs.Key.LIC_ID, //Children = gcs.ToList(),
                               };

            foreach (var item in licenciaTemp)
            {
                objLicencia = new BELicencias();
                objLicencia.OWNER = item.owner;
                objLicencia.LIC_ID = item.licId;
                listaLicencia.Add(objLicencia);
            }
            //**** Obteniedo datos de las tarifas de la Licencia **********************
            string xmlLicencia = string.Empty;
            xmlLicencia = Utility.Util.SerializarEntity(listaLicencia);
            FacturaMasiva.ListarDescuentos = new DADescuentos().ObtenerDescuentoLicXML(owner, xmlLicencia);
            //*************************************************************
            return FacturaMasiva;
        }

        //Incluye el calculo de la tarifa
        public BEFactura ListarFacturaMasivaSubGrilla(string owner, DateTime fini, DateTime ffin,
                                                                string mogId, decimal modId, decimal dadId, decimal bpsId,
                                                                decimal offId, decimal e_bpsId, decimal tipoEstId, decimal subTipoEstId, decimal licId, string monedaId,
                                                                decimal LibConfi, decimal VUMactual, int historico, string periodoEstado, decimal idBpsGroup, decimal groupfact, int oficina, int tipoFact, int EmiMensual)
        {

            BEFactura FacturaMasiva = new BEFactura();

            FacturaMasiva.ListarFactura = new DAFactura().ListarFacturaMasivaSubGrilla(owner,
                                                          fini, ffin, mogId, modId, dadId, bpsId,
                                                            offId, e_bpsId, tipoEstId, subTipoEstId, licId, monedaId, LibConfi, historico, periodoEstado
                                                           , idBpsGroup, groupfact, oficina, EmiMensual);
            FacturaMasiva.ListarLicencia = new DALicencias().ListarFacturaMasiva_LicenciaSubGrilla(owner,
                                                         fini, ffin, mogId, modId, dadId, bpsId,
                                                           offId, e_bpsId, tipoEstId, subTipoEstId, licId, monedaId, LibConfi, historico, periodoEstado
                                                          , idBpsGroup, groupfact, oficina, EmiMensual);
            FacturaMasiva.ListarLicenciaPlaneamiento = new DALicenciaPlaneamiento().ListarFacturaMasiva_LicPlanemientoSubGrilla(owner,
                                                         fini, ffin, mogId, modId, dadId, bpsId,
                                                           offId, e_bpsId, tipoEstId, subTipoEstId, licId, monedaId, LibConfi, historico, periodoEstado
                                                          , idBpsGroup, groupfact, oficina, EmiMensual);



            if (FacturaMasiva.ListarFactura.Count > 0)
            {
                //*********** Calcular Tarifa *****************
                List<BEREC_RATES_GRAL> listaTarifa = new List<BEREC_RATES_GRAL>();
                BEREC_RATES_GRAL objTarifa = null;
                var tarifaTemp = from c in FacturaMasiva.ListarLicenciaPlaneamiento
                                 group c by new
                                 {
                                     c.OWNER,
                                     c.RATE_ID,
                                 } into gcs
                                 select new
                                 {
                                     owner = gcs.Key.OWNER,
                                     rateId = gcs.Key.RATE_ID, //Children = gcs.ToList(),
                                 };

                foreach (var item in tarifaTemp)
                {
                    objTarifa = new BEREC_RATES_GRAL();
                    objTarifa.OWNER = owner;
                    objTarifa.RATE_ID = item.rateId;
                    listaTarifa.Add(objTarifa);
                }
                //**** Obteniedo datos de las tarifas de la Licencia **********************
                string xmlTarifa = string.Empty; string xmlLicencia = string.Empty; string xmlDetalle = string.Empty;
                List<BELicencias> lista_licencia_emision_mensual = null;
                xmlTarifa = Utility.Util.SerializarEntity(listaTarifa);
                xmlLicencia = Utility.Util.SerializarEntity(FacturaMasiva.ListarLicencia);
                xmlDetalle = Utility.Util.SerializarEntity(FacturaMasiva.ListarLicenciaPlaneamiento);
                FacturaMasiva.TT_Tarifa = new DATarifaTest().ObtenerTarifasXML(xmlTarifa);
                var ListaVUM = new DAValormusica().ListarHistorico(owner);
                //**************** VALIDANDO LICENCIAS DE EMISION MENSUAL*************************************
                var validaemimensualofi = new BLOffices().ValidaEmisionMensualOficina(oficina);
                var PideValidacionIndividual = new DAFactura().ObtieneValidacionIndividual();
                var PasoValidacionIndividual = true;
                if (tipoFact == Variables.FACTURACION_MASIVA && validaemimensualofi == Variables.SI)//cambiar esta validacion cadena y transporte
                {
                    lista_licencia_emision_mensual = ListarLicenciaNoAlDiaEmisionMensual(xmlLicencia);

                    //quitar detalle 
                    foreach (var y in lista_licencia_emision_mensual)
                    {
                        FacturaMasiva.ListarLicenciaPlaneamiento = FacturaMasiva.ListarLicenciaPlaneamiento.Where(z => z.LIC_ID != y.LIC_ID).ToList();//quitamos detalle de licencias q no pasaron validacion
                        FacturaMasiva.ListarLicencia = FacturaMasiva.ListarLicencia.Where(z => z.LIC_ID != y.LIC_ID).ToList();//quitamos licencia que no pasaron validacion
                                                                                                                              //FacturaMasiva.ListarFactura = FacturaMasiva.ListarFactura.Where(z => z.LIC_ID != y.LIC_ID).ToList();

                    }

                    foreach (var y in FacturaMasiva.ListarFactura)
                    {

                        if (FacturaMasiva.ListarLicencia.Where(z => z.BPS_ID == y.BPS_ID).Count() == 0)//  si no tiene licencias  no se muestra la cabezera
                        {
                            FacturaMasiva.ListarFactura = FacturaMasiva.ListarFactura.Where(z => z.BPS_ID != y.BPS_ID).ToList(); //removemos el socio que no tiene licencias a facturar 
                        }

                    }



                }
                if (tipoFact == Variables.FACTURACION_INDIVIDUAL && validaemimensualofi == Variables.SI)
                {
                    if (PideValidacionIndividual == true)
                    {
                        /*
                            Cuando es emision individual y trae datos quiere decir que la licencia no puede emitir
                        */
                        lista_licencia_emision_mensual = ListarLicenciaNoAlDiaEmisionMensual(xmlLicencia);
                        if (lista_licencia_emision_mensual.Count > Variables.CERO)
                        {
                            PasoValidacionIndividual = false; // no paso validacion
                        }

                    }

                }

                //********************************************************************************************


                if (FacturaMasiva.TT_Tarifa.Count > 0)
                {
                    FacturaMasiva.TT_Regla = new DATarifaRegla().ListarReglaTarifaTestXML(owner, xmlTarifa);
                    FacturaMasiva.TT_Caracteristica = new DATarifaTest().ListarCaracteristicaXML(owner, xmlDetalle);
                    FacturaMasiva.TT_Test = new DATarifaTest().ListarXML(owner, xmlTarifa);
                    FacturaMasiva.TT_Descuento = new DADescuentos().ObtenerDescuentoLicXML(owner, xmlLicencia);
                }
                //*********Obteniendo las licencias a evaluar *********************************
                String listalicenciaxml = string.Empty;
                listalicenciaxml = Utility.Util.SerializarEntity(FacturaMasiva.ListarLicencia);
                var licenciasNoAlDia = new DALicenciaPlaneamiento().ValidaLicenciaAlDia(owner, listalicenciaxml);
                //*****************************************************************************

                //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                //CALCULANDO EL MONTO DE LA TARIFA
                //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                bool estado_calc = false;
                BEREC_RATES_GRAL tarifa = new BEREC_RATES_GRAL(); // Inicializado
                foreach (var item in FacturaMasiva.ListarLicenciaPlaneamiento)
                {
                    //BEREC_RATES_GRAL tarifa = new BEREC_RATES_GRAL(); inicializando afuera para poder usar en otro foreach
                    List<BETarifaRegla> listaReglas = new List<BETarifaRegla>();
                    List<BETarifaCaracteristica> listaCaracteristicas = new List<BETarifaCaracteristica>();
                    List<BETarifaTest> listaTest = new List<BETarifaTest>();
                    List<BEDescuentos> listaDescuento = new List<BEDescuentos>();
                    decimal tarifaId = item.RATE_ID;
                    string LIC_CREQ = item.LIC_CREQ;
                    bool validacionCarReqAct = true;
                    bool validacionPeriodoNoBorradorFacturado = true; //facturacion electronica dav
                    bool validacionIndividual = PasoValidacionIndividual; // esta variable se calcula al pasar la validacion

                    decimal VUM = 0;

                    //Validacion de la licencia cuando las
                    //Caracteristicas deben estar actualizadas con le feha actual.
                    if (LIC_CREQ == "1" || PideValidacionIndividual == true)
                    {
                        var periodo = FacturaMasiva.TT_Caracteristica.Where(p => p.LIC_PL_ID == item.LIC_PL_ID).ToList();

                        if (item.LIC_PL_STATUS_FACT == "B" || item.LIC_PL_STATUS_FACT == "F")//facturacion electronica dav
                            validacionPeriodoNoBorradorFacturado = false;//facturacion electronica dav

                        if (periodo == null || periodo.Count == 0)
                            validacionCarReqAct = false;

                        if (!validacionCarReqAct)
                        {
                            item.SUB_MONTO = 0;
                            item.DESCUENTO = 0;
                            item.TAXV_VALUEP = 0;
                            item.MONTO = 0;
                            item.STATE_CALC = false;
                            item.OBSERVACION = msjErrorRegistrarValores;
                        }

                        if (!validacionPeriodoNoBorradorFacturado)
                        {
                            item.SUB_MONTO = 0;
                            item.DESCUENTO = 0;
                            item.TAXV_VALUEP = 0;
                            item.MONTO = 0;
                            item.STATE_CALC_FACT = false;
                            item.STATE_CALC = true;
                            item.OBSERVACION = item.LIC_PL_STATUS_FACT == "B" ? msjPeriodosEnBorrador : msjPeriodosEnFacturacion; // DEVOLVIENDO LA RAZON
                        }
                        if (!PasoValidacionIndividual)
                        {
                            item.SUB_MONTO = 0;
                            item.DESCUENTO = 0;
                            item.TAXV_VALUEP = 0;
                            item.MONTO = 0;
                            item.STATE_CALC_FACT = false;
                            item.STATE_CALC = true;
                            item.OBSERVACION = Variables.OBSERVACION_EMISION_INDIVIDUAL;
                        }

                    }
                    VUM = (historico == 1) ? ObtenerVUMHistorico(ListaVUM, item.LIC_DATE, VUMactual) : VUMactual;

                    if (validacionCarReqAct && validacionPeriodoNoBorradorFacturado && PasoValidacionIndividual) //facturacion electronica dav

                    {
                        tarifa = FacturaMasiva.TT_Tarifa.Where(x => x.RATE_ID == tarifaId).FirstOrDefault();
                        listaCaracteristicas = FacturaMasiva.TT_Caracteristica.Where(x => x.RATE_ID == tarifaId && x.LIC_ID == item.LIC_ID && x.LIC_PL_ID == item.LIC_PL_ID).ToList();
                        listaReglas = FacturaMasiva.TT_Regla.Where(x => x.RATE_ID == tarifaId).ToList();
                        listaTest = FacturaMasiva.TT_Test.Where(x => x.RATE_ID == tarifaId).ToList();
                        listaDescuento = FacturaMasiva.TT_Descuento.Where(x => x.LIC_ID == item.LIC_ID).ToList();

                        foreach (var regla in listaReglas)
                        {
                            var ListaCaracteristicaXregla = listaCaracteristicas.Where(x => x.RATE_CALC == regla.CALR_ID).ToList();
                            var ListavaloresXregla = listaTest.Where(v => v.CALR_ID == regla.CALR_ID).ToList();
                            if (regla.CALR_ACCUM == Seleccion.SI && regla.CALR_ADJUST == Seleccion.SI)
                            {
                                regla.VALUE_R = ATsiAUsi(regla, ListaCaracteristicaXregla, ListavaloresXregla, VUM);
                            }
                            else if (regla.CALR_ACCUM == Seleccion.SI && regla.CALR_ADJUST == Seleccion.NO)
                            {
                                regla.VALUE_R = ATsiAUno(regla, ListaCaracteristicaXregla, ListavaloresXregla, VUM);
                            }
                            else if (regla.CALR_ACCUM == Seleccion.NO && regla.CALR_ADJUST == Seleccion.SI)
                            {
                                regla.VALUE_R = ATnoAUsi(regla, ListaCaracteristicaXregla, ListavaloresXregla, VUM);
                            }
                            else if (regla.CALR_ACCUM == Seleccion.NO && regla.CALR_ADJUST == Seleccion.NO)
                            {
                                regla.VALUE_R = ATnoAUno(regla, ListaCaracteristicaXregla, ListavaloresXregla, VUM);
                                //regla.VALUE_R = ATnoAUno(regla, listaCaracteristicas.Where(x => x.RATE_CALC == regla.CALR_ID).ToList(), listaTest, VUM);
                            }
                        }

                        int stateAcum = 0;//Acumulacion paraidentificar que las  reglas hayan realizado el calculo correctamente.
                        foreach (var regla in listaReglas)
                        {
                            if (regla.STATE_CALC && regla.VALUE_R > 0) stateAcum += 1;
                        }
                        //Comparacion de acumulacion y el total de las reglas
                        if (stateAcum == listaReglas.Count)
                        {
                            decimal descuentoAcum = 0;
                            decimal resultBase = CalcularTarifa(tarifa, listaReglas, VUM);
                            decimal resultNeto = resultBase;
                            //resultNeto = new BLDescuentos().TarifaDescuentosPlantilla(tarifa.RATE_ID, resultBase, listaCaracteristicas);//este es para los dsc de licencia y usuario
                            //resultBase = new BLDescuentos().TarifaDescuentosPlantilla(tarifa.RATE_ID, resultBase, listaCaracteristicas);//este se muestra en la tarifa 
                            //Agregando un acumulador de Descuentos Mora*
                            decimal descuentoMoraAcum = 0;

                            //david ************calculando descuentos plantilla********
                            var valida_mega = new BLDescuentos().ValidaLicenciaMegaconcierto(item.LIC_ID);
                            if (valida_mega == 0 && item.LIC_MASTER > 0)
                            {
                                if ((bpsId > 0 || idBpsGroup > 0) || tipoFact == 1)  //--agregar tambien busqueda de grupo empresarial
                                { // es Facturaicon Masiva
                                    decimal lic_id = 0;
                                    int Aprobado = 1;
                                    if (listaDescuento != null && listaDescuento.Count > 0)//Recupera ID DE LA LICENCIA ACTUAL
                                        lic_id = listaDescuento[0].LIC_ID;//descuento por licencia
                                    foreach (var n in licenciasNoAlDia.Where(n => n.LIC_ID == lic_id)) //item.LIC_ID
                                    {
                                        Aprobado = 0;
                                        break;
                                    }

                                    if (Aprobado == 1)
                                    {
                                        decimal BPS_ID_X_LIC = 0;
                                        if (lic_id > 0)//si trae descuentos x licencia obtener el id de las licencias
                                            BPS_ID_X_LIC = new BLLicencias().ObtenerLicenciaXCodigo(lic_id, owner).BPS_ID;//comentar
                                        else if ((bpsId > 0 && idBpsGroup == 0))
                                            BPS_ID_X_LIC = bpsId;
                                        else if (bpsId == 0 && idBpsGroup > 0)
                                            BPS_ID_X_LIC = idBpsGroup;
                                        else
                                            BPS_ID_X_LIC = idBpsGroup;

                                        listaDescuento = DescuentosPlantillaMasiva(listaDescuento, licId, FacturaMasiva.ListarLicenciaPlaneamiento, BPS_ID_X_LIC);//bpsId


                                    }

                                }

                            }
                            else if (valida_mega == 1) // si es 1 
                            {
                                var listaDescuentoPl = new BLDescuentos().ListaDescuentosPlantilla(tarifaId, listaDescuento, listaCaracteristicas);
                                FacturaMasiva.ListarDescuentos = listaDescuentoPl;
                            }
                            //**********************************************************

                            foreach (var descuento in listaDescuento.OrderByDescending(p => p.DISC_VALUE))
                            {
                                var valormultiplicar = 0;
                                if (descuento.DISC_PERC >= 1)//si el desc es 5 convertir a 0,05 si ya es 0,05 multiplicar por el mismo valor
                                    valormultiplicar = 100;
                                else
                                    valormultiplicar = 1;

                                if (descuento.DISC_ORG == "B")
                                {
                                    if (descuento.DISC_VALUE != 0)
                                    {

                                        //Si el Sigo es - Entonces es un descuento
                                        if (descuento.DISC_SIGN == "-")
                                        {
                                            resultNeto -= descuento.DISC_VALUE;
                                            descuentoAcum += descuento.DISC_VALUE;
                                        }
                                        else
                                        {//si el sigo es + entonces es una penalidad ,mora 
                                            resultNeto += descuento.DISC_VALUE;
                                            descuentoMoraAcum += descuento.DISC_VALUE;
                                        }
                                    }
                                    else
                                    {

                                        //Si el Sigo es - Entonces es un descuento
                                        if (descuento.DISC_SIGN == "-")
                                        {
                                            resultNeto -= (resultBase * (descuento.DISC_PERC / valormultiplicar));
                                            descuentoAcum += (resultBase * (descuento.DISC_PERC / valormultiplicar));
                                        }
                                        else
                                        { //si el sigo es + entonces es una penalidad ,mora 
                                            if (descuento.DISC_PERC > 0)
                                            {
                                                resultNeto += (resultBase * (descuento.DISC_PERC / valormultiplicar));
                                                descuentoMoraAcum += (resultBase * (descuento.DISC_PERC / valormultiplicar));
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (descuento.DISC_VALUE != 0)
                                    {
                                        //Si el Sigo es - Entonces es un descuento
                                        if (descuento.DISC_SIGN == "-")
                                        {
                                            descuentoAcum += descuento.DISC_VALUE;
                                            resultNeto -= descuento.DISC_VALUE;

                                        }
                                        else
                                        {//si el sigo es + entonces es una penalidad ,mora 
                                            resultNeto += descuento.DISC_VALUE;
                                            descuentoMoraAcum += descuento.DISC_VALUE;
                                        }
                                    }
                                    else
                                    { //Si el Sigo es - Entonces es un descuento
                                        if (descuento.DISC_SIGN == "-")
                                        {
                                            descuentoAcum += (resultNeto * (descuento.DISC_PERC / valormultiplicar));
                                            resultNeto -= (resultNeto * (descuento.DISC_PERC / valormultiplicar));
                                        }
                                        else
                                        {//si el sigo es + entonces es una penalidad ,mora 
                                            resultNeto += (resultNeto * (descuento.DISC_PERC / valormultiplicar));
                                            descuentoMoraAcum += (resultNeto * (descuento.DISC_PERC / valormultiplicar));
                                        }
                                    }
                                }
                            }

                            //if(tarifa.RATE_REDONDEO==1)
                            //item.SUB_MONTO = Math.Round(resultBase, 3);
                            //item.DESCUENTO = Math.Round(descuentoAcum, 3);
                            //item.MONTO = Math.Round(resultBase, 3) - Math.Round(descuentoAcum, 3);
                            //item.STATE_CALC = true; // validar puede que no calcule bien la tarifa
                            int decimales = Convert.ToInt32(tarifa.RATE_FDECI);
                            if (tarifa.RATE_REDONDEO == 1)
                            {
                                item.SUB_MONTO = Math.Round(resultBase, decimales);
                                item.DESCUENTO = Math.Round(descuentoAcum, decimales);
                                //SUmando lso descuentos mora con el total a pagar - los descuentos por socio y licencia individual hija
                                item.MONTO = (Math.Round(resultBase, decimales) + Math.Round(descuentoMoraAcum, decimales)) - Math.Round(descuentoAcum, decimales);

                                //if(tarifa.RATE_REDONDEO_ESP==Variables.SI)
                                //{
                                //    double Ndecimales = Convert.ToDouble(item.MONTO) - Convert.ToDouble((long)item.MONTO);
                                //    if (Ndecimales > 0.5 && Ndecimales < 1)
                                //        Ndecimales = 0.5;
                                //    else if (Ndecimales < 0.5)
                                //        Ndecimales = 0;
                                //    item.DESCUENTO = item.DESCUENTO + (item.MONTO - Convert.ToDecimal((long)item.MONTO + Ndecimales));
                                //    item.MONTO = Math.Round(Convert.ToDecimal((long)item.MONTO + Ndecimales), decimales);

                                //}
                            }
                            else
                            {
                                item.SUB_MONTO = Math.Round(resultBase, decimales);  //resultBase;
                                item.DESCUENTO = Math.Round(descuentoAcum, decimales);
                                item.MONTO = (Math.Round(resultBase, decimales) + Math.Round(descuentoMoraAcum, decimales)) - Math.Round(descuentoAcum, decimales);

                                //// COMENTANDO DESCUENTO POR TARIFA 

                                //if (tarifa.RATE_REDONDEO_ESP == Variables.SI)
                                //{
                                //    double Ndecimales = Convert.ToDouble(item.MONTO) - Convert.ToDouble((long)item.MONTO);
                                //    if (Ndecimales > 0.5 && Ndecimales < 1)
                                //        Ndecimales = 0.5;
                                //    else if (Ndecimales < 0.5)
                                //        Ndecimales = 0;
                                //    item.DESCUENTO = item.DESCUENTO + (item.MONTO - Convert.ToDecimal((long)item.MONTO + Ndecimales));
                                //    item.MONTO = Convert.ToDecimal((long)item.MONTO + Ndecimales);

                                //}
                            }
                            item.STATE_CALC = true; // validar puede que no calcule bien la tarifa
                            item.STATE_CALC_FACT = true;
                        }
                        else
                        {
                            item.SUB_MONTO = 0;
                            item.DESCUENTO = 0;
                            item.MONTO = 0;
                            item.STATE_CALC = false;
                            item.OBSERVACION = msjErrorValores;
                        }
                    }
                }

                //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


                //////////Redondeo Monto Total
                //decimal RedondeoMontoTotal = 0;
                //foreach (var i in FacturaMasiva.ListarLicenciaPlaneamiento)
                //{
                //    RedondeoMontoTotal += i.MONTO;
                //}

                //var PrimerPeriodo = FacturaMasiva.ListarLicenciaPlaneamiento.FirstOrDefault();
                //if (tarifa.RATE_REDONDEO_ESP == Variables.SI)
                //{
                //    //decimal Decimales= Decimal.Round(RedondeoMontoTotal, 2);

                //    float numDecimal = float.Parse("0," + Convert.ToString(RedondeoMontoTotal).Split('.')[1]);
                //    string DecuentoString = "0." + numDecimal;
                //    double Descuento = Convert.ToDouble(DecuentoString);
                //    if (Descuento < 0.5 && numDecimal != 0)
                //    {
                //        PrimerPeriodo.DESCUENTO = Convert.ToDecimal(Descuento) + PrimerPeriodo.DESCUENTO;
                //        PrimerPeriodo.MONTO = PrimerPeriodo.MONTO - Convert.ToDecimal(Descuento);
                //    }
                //    else if (Descuento > 0.5 && numDecimal != 0)
                //    {
                //        double Desc = Descuento - 0.5;
                //        PrimerPeriodo.DESCUENTO = Convert.ToDecimal(Desc) + PrimerPeriodo.DESCUENTO;
                //        PrimerPeriodo.MONTO = PrimerPeriodo.MONTO - Convert.ToDecimal(Descuento);
                //    }
                //}


            }
            return FacturaMasiva;
        }

        public decimal ObtenerVUMHistorico(List<BEValormusica> lista, DateTime fecha, decimal vumActual)
        {
            decimal VUM = 0;
            var resultado = lista.Where(x => fecha >= x.START &&
                                        ((x.ENDS == null) || (x.ENDS != null && fecha <= x.ENDS))
                                        ).ToList();

            if (resultado.Count > 0 && resultado != null)
                VUM = resultado.OrderBy(x => x.LOG_DATE_CREAT).FirstOrDefault().VUM_VAL;
            else
                VUM = vumActual;
            return VUM;
        }
        public decimal? ATnoAUsi(BETarifaRegla regla, List<BETarifaCaracteristica> caracteristicas,
                                List<BETarifaTest> valores, decimal VUM)
        {
            decimal? valorFinal = 0;
            try
            {
                #region Obtener Valores
                decimal vA = 0; string tA = string.Empty; // v=valor; t=tramo    
                decimal vB = 0; string tB = string.Empty;
                decimal vC = 0; string tC = string.Empty;
                decimal vD = 0; string tD = string.Empty;
                decimal vE = 0; string tE = string.Empty;

                int totCaracteristicas = caracteristicas.ToList().Count;
                if (totCaracteristicas > 0)
                {
                    vA = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.A).FirstOrDefault().LIC_CHAR_VAL;
                    tA = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.A).FirstOrDefault().IND_TR;
                }
                if (totCaracteristicas > 1)
                {
                    vB = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.B).FirstOrDefault().LIC_CHAR_VAL;
                    tB = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.B).FirstOrDefault().IND_TR;
                }
                if (totCaracteristicas > 2)
                {
                    vC = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.C).FirstOrDefault().LIC_CHAR_VAL;
                    tC = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.C).FirstOrDefault().IND_TR;
                }
                if (totCaracteristicas > 3)
                {
                    vD = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.D).FirstOrDefault().LIC_CHAR_VAL;
                    tD = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.D).FirstOrDefault().IND_TR;
                }
                if (totCaracteristicas > 4)
                {
                    vE = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.E).FirstOrDefault().LIC_CHAR_VAL;
                    tE = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.E).FirstOrDefault().IND_TR;
                }

                //Buscar R
                List<BETarifaTest> listaObtenerR = new List<BETarifaTest>();
                if (totCaracteristicas > 0)
                {
                    if (tA == Seleccion.SI)
                        listaObtenerR = valores.Where(v => vA >= v.CRI1_FROM && vA <= v.CRI1_TO).ToList();
                    else if (tA == Seleccion.NO)
                        listaObtenerR = valores.Where(v => vA == v.CRI1_FROM).ToList();
                }

                if (totCaracteristicas > 1)
                {
                    if (tB == Seleccion.SI)
                        listaObtenerR = listaObtenerR.Where(v => vB >= v.CRI2_FROM && vB <= v.CRI2_TO).ToList();
                    else if (tB == Seleccion.NO)
                        listaObtenerR = listaObtenerR.Where(v => vB == v.CRI2_FROM).ToList();
                }

                if (totCaracteristicas > 2)
                {
                    if (tC == Seleccion.SI)
                        listaObtenerR = listaObtenerR.Where(v => vC >= v.CRI3_FROM && vC <= v.CRI3_TO).ToList();
                    else if (tC == Seleccion.NO)
                        listaObtenerR = listaObtenerR.Where(v => vC == v.CRI3_FROM).ToList();
                }

                if (totCaracteristicas > 3)
                {
                    if (tD == Seleccion.SI)
                        listaObtenerR = listaObtenerR.Where(v => vD >= v.CRI4_FROM && vD <= v.CRI4_TO).ToList();
                    else if (tD == Seleccion.NO)
                        listaObtenerR = listaObtenerR.Where(v => vD == v.CRI4_FROM).ToList();
                }

                if (totCaracteristicas > 4)
                {
                    if (tE == Seleccion.SI)
                        listaObtenerR = listaObtenerR.Where(v => vE >= v.CRI5_FROM && vE <= v.CRI5_TO).ToList();
                    else if (tE == Seleccion.NO)
                        listaObtenerR = listaObtenerR.Where(v => vE == v.CRI5_FROM).ToList();
                }
                #endregion

                decimal? vFormula = 0;
                decimal? vMinimo = 0;
                decimal? valorR = 0;
                decimal? valorRmin = 0;//

                #region Calcular
                vFormula = listaObtenerR.FirstOrDefault().VAL_FORMULA;
                vMinimo = listaObtenerR.FirstOrDefault().VAL_MINIMUM;

                valorR = vFormula;
                valorRmin = vMinimo;//
                //if (vMinimo > vFormula)


                //if (tA == Seleccion.MANUAL || tB == Seleccion.MANUAL ||
                //    tC == Seleccion.MANUAL || tD == Seleccion.MANUAL || tE == Seleccion.MANUAL)
                //{
                decimal? Ra = 0;
                decimal? Rb = 0;
                decimal? Rc = 0;
                decimal? Rd = 0;
                decimal? Re = 0;

                string formula;
                string formulaMinima;
                string[] listaOperandos = new string[10];
                double[] listaValores = new double[10];
                DataTable Tbl = new DataTable();

                formula = regla.CALC_FORMULA;
                formulaMinima = regla.CALC_MINIMUM.Replace(LetraReg.R, LetraReg.Rmin);//

                foreach (var car in caracteristicas)
                {
                    if (car.CHAR_ORI_REG == LetraCar.A) Ra = car.LIC_CHAR_VAL;
                    if (car.CHAR_ORI_REG == LetraCar.B) Rb = car.LIC_CHAR_VAL;
                    if (car.CHAR_ORI_REG == LetraCar.C) Rc = car.LIC_CHAR_VAL;
                    if (car.CHAR_ORI_REG == LetraCar.D) Rd = car.LIC_CHAR_VAL;
                    if (car.CHAR_ORI_REG == LetraCar.E) Re = car.LIC_CHAR_VAL;
                }

                listaOperandos[0] = LetraCar.A;
                listaOperandos[1] = LetraCar.B;
                listaOperandos[2] = LetraCar.C;
                listaOperandos[3] = LetraCar.D;
                listaOperandos[4] = LetraCar.E;
                listaOperandos[5] = LetraReg.R;
                listaOperandos[6] = LetraReg.V;
                listaOperandos[7] = LetraReg.Rmin;

                listaValores[0] = Convert.ToDouble(Ra);
                listaValores[1] = Convert.ToDouble(Rb);
                listaValores[2] = Convert.ToDouble(Rc);
                listaValores[3] = Convert.ToDouble(Rd);
                listaValores[4] = Convert.ToDouble(Re);

                listaValores[5] = Convert.ToDouble(valorR);
                listaValores[6] = Convert.ToDouble(VUM);
                listaValores[7] = Convert.ToDouble(valorRmin);

                //Tbl.Columns.Add("variable", typeof(string));
                Tbl.Columns.Add(listaOperandos[0], typeof(double));
                Tbl.Columns.Add(listaOperandos[1], typeof(double));
                Tbl.Columns.Add(listaOperandos[2], typeof(double));
                Tbl.Columns.Add(listaOperandos[3], typeof(double));
                Tbl.Columns.Add(listaOperandos[4], typeof(double));
                Tbl.Columns.Add(listaOperandos[5], typeof(double));
                Tbl.Columns.Add(listaOperandos[6], typeof(double));
                Tbl.Columns.Add(listaOperandos[7], typeof(double));
                Tbl.Columns.Add("Tarifa", typeof(double), formula);
                Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

                {
                    // crea una nueva línea 
                    DataRow linea = Tbl.NewRow();
                    linea[0] = listaValores[0];
                    linea[1] = listaValores[1];
                    linea[2] = listaValores[2];
                    linea[3] = listaValores[3];
                    linea[4] = listaValores[4];
                    linea[5] = listaValores[5];
                    linea[6] = listaValores[6];
                    linea[7] = listaValores[7];
                    Tbl.Rows.Add(linea);
                }

                regla.VALUE_FORMULA = Convert.ToDecimal(Tbl.Rows[0][8].ToString());
                regla.VALUE_MINIMUN = Convert.ToDecimal(Tbl.Rows[0][9].ToString());

                if (regla.VALUE_MINIMUN > regla.VALUE_FORMULA)
                    regla.VALUE_R = regla.VALUE_MINIMUN;
                else
                    regla.VALUE_R = regla.VALUE_FORMULA;

                valorFinal = regla.VALUE_R;
                //}
                //else
                //{
                //    regla.VALUE_FORMULA = vFormula;
                //    regla.VALUE_MINIMUN = vMinimo;
                //    if (regla.VALUE_MINIMUN > regla.VALUE_FORMULA)
                //        regla.VALUE_R = regla.VALUE_MINIMUN;
                //    else
                //        regla.VALUE_R = regla.VALUE_FORMULA;
                //    valorFinal = regla.VALUE_R;
                //}
                #endregion
                regla.STATE_CALC = true;
                return valorFinal;
            }
            catch (Exception ex)
            {
                regla.STATE_CALC = false;
                return 0;
            }

        }  // CASO 3 - OK

        public decimal CalcularTarifa(BEREC_RATES_GRAL tarifa, List<BETarifaRegla> ReglaAsoc, decimal VUM)
        {
            decimal valorR = 0;
            try
            {
                decimal? Rt = 0;
                decimal? Rw = 0;
                decimal? Rx = 0;
                decimal? Ry = 0;
                decimal? Rz = 0;
                //MINIMO
                decimal? Mt = 0;
                decimal? Mw = 0;
                decimal? Mx = 0;
                decimal? My = 0;
                decimal? Mz = 0;

                string formula;
                string formulaMinima;
                string[] listaOperandos = new string[10];
                double[] listaValores = new double[10];
                //MIN
                double[] listaValoresMin = new double[10];
                DataTable Tbl = new DataTable();
                //MIN
                DataTable TblMin = new DataTable();

                formula = tarifa.RATE_FORMULA;
                formulaMinima = tarifa.RATE_MINIMUM;

                foreach (var regla in ReglaAsoc)
                {
                    if (regla.RATE_CALC_VAR == LetraReg.T) Rt = regla.VALUE_FORMULA;
                    if (regla.RATE_CALC_VAR == LetraReg.T) Mt = regla.VALUE_MINIMUN;
                    if (regla.RATE_CALC_VAR == LetraReg.W) Rw = regla.VALUE_FORMULA;
                    if (regla.RATE_CALC_VAR == LetraReg.W) Mw = regla.VALUE_MINIMUN;
                    if (regla.RATE_CALC_VAR == LetraReg.X) Rx = regla.VALUE_FORMULA;
                    if (regla.RATE_CALC_VAR == LetraReg.X) Mx = regla.VALUE_MINIMUN;
                    if (regla.RATE_CALC_VAR == LetraReg.Y) Ry = regla.VALUE_FORMULA;
                    if (regla.RATE_CALC_VAR == LetraReg.Y) My = regla.VALUE_MINIMUN;
                    if (regla.RATE_CALC_VAR == LetraReg.Z) Rz = regla.VALUE_FORMULA;
                    if (regla.RATE_CALC_VAR == LetraReg.Z) Mz = regla.VALUE_MINIMUN;
                }

                listaOperandos[0] = LetraReg.T;
                listaOperandos[1] = LetraReg.W;
                listaOperandos[2] = LetraReg.X;
                listaOperandos[3] = LetraReg.Y;
                listaOperandos[4] = LetraReg.Z;
                listaOperandos[5] = LetraReg.R;
                listaOperandos[6] = LetraReg.V;

                listaValores[0] = Convert.ToDouble(Rt);
                listaValores[1] = Convert.ToDouble(Rw);
                listaValores[2] = Convert.ToDouble(Rx);
                listaValores[3] = Convert.ToDouble(Ry);
                listaValores[4] = Convert.ToDouble(Rz);

                listaValores[5] = 0;
                listaValores[6] = Convert.ToDouble(VUM);

                Tbl.Columns.Add(listaOperandos[0], typeof(double));
                Tbl.Columns.Add(listaOperandos[1], typeof(double));
                Tbl.Columns.Add(listaOperandos[2], typeof(double));
                Tbl.Columns.Add(listaOperandos[3], typeof(double));
                Tbl.Columns.Add(listaOperandos[4], typeof(double));
                Tbl.Columns.Add(listaOperandos[5], typeof(double));
                Tbl.Columns.Add(listaOperandos[6], typeof(double));
                Tbl.Columns.Add("Tarifa", typeof(double), formula);
                Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

                {
                    // crea una nueva línea 
                    DataRow linea = Tbl.NewRow();
                    linea[0] = listaValores[0];
                    linea[1] = listaValores[1];
                    linea[2] = listaValores[2];
                    linea[3] = listaValores[3];
                    linea[4] = listaValores[4];
                    linea[5] = listaValores[5];
                    linea[6] = listaValores[6];
                    Tbl.Rows.Add(linea);
                }
                if (tarifa.RATE_REDONDEO == 1)
                    tarifa.VALUE_FORMULA = RedondeoTarifa(Convert.ToDecimal(Tbl.Rows[0][7].ToString()));
                else
                    tarifa.VALUE_FORMULA = Math.Round(Convert.ToDecimal(Tbl.Rows[0][7].ToString()), 2);
                //Convert.ToInt32(tarifa.RATE_FDECI));

                //MIN
                listaValoresMin[0] = Convert.ToDouble(Mt);
                listaValoresMin[1] = Convert.ToDouble(Mw);
                listaValoresMin[2] = Convert.ToDouble(Mx);
                listaValoresMin[3] = Convert.ToDouble(My);
                listaValoresMin[4] = Convert.ToDouble(Mz);
                listaValoresMin[5] = 0;
                listaValoresMin[6] = Convert.ToDouble(VUM);

                TblMin.Columns.Add(listaOperandos[0], typeof(double));
                TblMin.Columns.Add(listaOperandos[1], typeof(double));
                TblMin.Columns.Add(listaOperandos[2], typeof(double));
                TblMin.Columns.Add(listaOperandos[3], typeof(double));
                TblMin.Columns.Add(listaOperandos[4], typeof(double));
                TblMin.Columns.Add(listaOperandos[5], typeof(double));
                TblMin.Columns.Add(listaOperandos[6], typeof(double));
                TblMin.Columns.Add("Tarifa", typeof(double), formula);
                TblMin.Columns.Add("Minimo", typeof(double), formulaMinima);
                {
                    // crea una nueva línea 
                    DataRow lineaMin = TblMin.NewRow();
                    lineaMin[0] = listaValoresMin[0];
                    lineaMin[1] = listaValoresMin[1];
                    lineaMin[2] = listaValoresMin[2];
                    lineaMin[3] = listaValoresMin[3];
                    lineaMin[4] = listaValoresMin[4];
                    lineaMin[5] = listaValoresMin[5];
                    lineaMin[6] = listaValoresMin[6];
                    TblMin.Rows.Add(lineaMin);
                }
                if (tarifa.RATE_REDONDEO == 1)
                    tarifa.VALUE_MINIMUN = RedondeoTarifa(Convert.ToDecimal(TblMin.Rows[0][8].ToString()));
                else
                    tarifa.VALUE_MINIMUN = Math.Round(Convert.ToDecimal(TblMin.Rows[0][8].ToString()), 2);
                //Convert.ToInt32(tarifa.RATE_MDECI));

                if (tarifa.VALUE_FORMULA >= tarifa.VALUE_MINIMUN)
                    valorR = tarifa.VALUE_FORMULA;
                else
                    valorR = tarifa.VALUE_MINIMUN;

                return valorR;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public decimal? ATsiAUsi(BETarifaRegla regla, List<BETarifaCaracteristica> caracteristicas,
                                List<BETarifaTest> valores, decimal VUM)
        {
            decimal? valorFinal = 0;
            try
            {
                #region Obtener Valores
                decimal? vA = 0; string tA = string.Empty;
                decimal? vB = 0; string tB = string.Empty;
                decimal? vC = 0; string tC = string.Empty;
                decimal? vD = 0; string tD = string.Empty;
                decimal? vE = 0; string tE = string.Empty;

                int totCaracteristicas = caracteristicas.ToList().Count;
                if (totCaracteristicas > 0)
                {
                    vA = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.A).FirstOrDefault().LIC_CHAR_VAL;
                    tA = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.A).FirstOrDefault().IND_TR;
                }
                if (totCaracteristicas > 1)
                {
                    vB = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.B).FirstOrDefault().LIC_CHAR_VAL;
                    tB = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.B).FirstOrDefault().IND_TR;
                }
                if (totCaracteristicas > 2)
                {
                    vC = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.C).FirstOrDefault().LIC_CHAR_VAL;
                    tC = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.C).FirstOrDefault().IND_TR;
                }
                if (totCaracteristicas > 3)
                {
                    vD = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.D).FirstOrDefault().LIC_CHAR_VAL;
                    tD = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.D).FirstOrDefault().IND_TR;
                }
                if (totCaracteristicas > 4)
                {
                    vE = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.E).FirstOrDefault().LIC_CHAR_VAL;
                    tE = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.E).FirstOrDefault().IND_TR;
                }


                //Buscar R
                //Buscar R
                List<BETarifaTest> listaObtenerR = new List<BETarifaTest>();
                if (totCaracteristicas > 0)
                {
                    if (tA == Seleccion.SI)
                        listaObtenerR = valores.Where(
                                        v => (vA >= v.CRI1_FROM && vA <= v.CRI1_TO) || (v.CRI1_TO <= vA)
                                        ).ToList();
                    else if (tA == Seleccion.NO)
                        listaObtenerR = valores.Where(v => vA == v.CRI1_FROM).ToList();
                }

                if (totCaracteristicas > 1)
                {
                    if (tB == Seleccion.SI)
                        listaObtenerR = listaObtenerR.Where(
                                        v => (vB >= v.CRI2_FROM && vB <= v.CRI2_TO) || (v.CRI2_TO <= vB)
                                        ).ToList();
                    else if (tB == Seleccion.NO)
                        listaObtenerR = listaObtenerR.Where(v => vB == v.CRI2_FROM).ToList();
                }

                if (totCaracteristicas > 2)
                {
                    if (tC == Seleccion.SI)
                        listaObtenerR = listaObtenerR.Where(
                                        v => (vC >= v.CRI3_FROM && vC <= v.CRI3_TO) || (v.CRI3_TO <= vC)
                                        ).ToList();
                    else if (tC == Seleccion.NO)
                        listaObtenerR = listaObtenerR.Where(v => vC == v.CRI3_FROM).ToList();
                }

                if (totCaracteristicas > 3)
                {
                    if (tD == Seleccion.SI)
                        listaObtenerR = listaObtenerR.Where(
                                        v => (vD >= v.CRI4_FROM && vD <= v.CRI4_TO) || (v.CRI4_TO <= vD)
                                        ).ToList();
                    else if (tD == Seleccion.NO)
                        listaObtenerR = listaObtenerR.Where(v => vD == v.CRI4_FROM).ToList();
                }

                if (totCaracteristicas > 4)
                {
                    if (tE == Seleccion.SI)
                        listaObtenerR = listaObtenerR.Where(
                            v => (vE >= v.CRI5_FROM && vE <= v.CRI5_TO) || (v.CRI5_TO <= vE)
                            ).ToList();
                    else if (tE == Seleccion.NO)
                        listaObtenerR = listaObtenerR.Where(v => vE == v.CRI5_FROM).ToList();
                }
                #endregion

                #region Calcular
                decimal? acumularR = 0;
                decimal? acumularRminimo = 0;
                decimal? vFormula = 0; decimal? vFormulaTemp = 0;
                decimal? vMinimo = 0; decimal? vMinimoTemp = 0;
                decimal? valorR = 0;
                decimal? valorRmin = 0;

                foreach (var item in listaObtenerR)
                {
                    acumularR += item.VAL_FORMULA;
                    acumularRminimo += item.VAL_MINIMUM;
                }
                vFormula = listaObtenerR.FirstOrDefault().VAL_FORMULA;
                vMinimo = listaObtenerR.FirstOrDefault().VAL_MINIMUM;

                valorR = vFormula;
                valorRmin = vMinimo;//

                //if (vMinimo > vFormula)
                //    valorR = vMinimo;
                //else
                //    valorR = vFormula;

                if (tA == Seleccion.MANUAL || tB == Seleccion.MANUAL ||
                    tC == Seleccion.MANUAL || tD == Seleccion.MANUAL || tE == Seleccion.MANUAL)
                {
                    decimal? Ra = 0;
                    decimal? Rb = 0;
                    decimal? Rc = 0;
                    decimal? Rd = 0;
                    decimal? Re = 0;

                    string formula;
                    string formulaMinima;
                    string[] listaOperandos = new string[10];
                    double[] listaValores = new double[10];
                    DataTable Tbl = new DataTable();

                    formula = regla.CALC_FORMULA;
                    formulaMinima = regla.CALC_MINIMUM.Replace(LetraReg.R, LetraReg.Rmin);//

                    foreach (var car in caracteristicas)
                    {
                        if (car.CHAR_ORI_REG == LetraCar.A) Ra = car.LIC_CHAR_VAL;
                        if (car.CHAR_ORI_REG == LetraCar.B) Rb = car.LIC_CHAR_VAL;
                        if (car.CHAR_ORI_REG == LetraCar.C) Rc = car.LIC_CHAR_VAL;
                        if (car.CHAR_ORI_REG == LetraCar.D) Rd = car.LIC_CHAR_VAL;
                        if (car.CHAR_ORI_REG == LetraCar.E) Re = car.LIC_CHAR_VAL;
                    }

                    listaOperandos[0] = LetraCar.A;
                    listaOperandos[1] = LetraCar.B;
                    listaOperandos[2] = LetraCar.C;
                    listaOperandos[3] = LetraCar.D;
                    listaOperandos[4] = LetraCar.E;
                    listaOperandos[5] = LetraReg.R;
                    listaOperandos[6] = LetraReg.V;
                    listaOperandos[7] = LetraReg.Rmin;

                    listaValores[0] = Convert.ToDouble(Ra);
                    listaValores[1] = Convert.ToDouble(Rb);
                    listaValores[2] = Convert.ToDouble(Rc);
                    listaValores[3] = Convert.ToDouble(Rd);
                    listaValores[4] = Convert.ToDouble(Re);

                    listaValores[5] = Convert.ToDouble(valorR);
                    listaValores[6] = Convert.ToDouble(VUM);
                    listaValores[7] = Convert.ToDouble(valorRmin);

                    //Tbl.Columns.Add("variable", typeof(string));
                    Tbl.Columns.Add(listaOperandos[0], typeof(double));
                    Tbl.Columns.Add(listaOperandos[1], typeof(double));
                    Tbl.Columns.Add(listaOperandos[2], typeof(double));
                    Tbl.Columns.Add(listaOperandos[3], typeof(double));
                    Tbl.Columns.Add(listaOperandos[4], typeof(double));
                    Tbl.Columns.Add(listaOperandos[5], typeof(double));
                    Tbl.Columns.Add(listaOperandos[6], typeof(double));
                    Tbl.Columns.Add(listaOperandos[7], typeof(double));
                    Tbl.Columns.Add("Tarifa", typeof(double), formula);
                    Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

                    {
                        // crea una nueva línea 
                        DataRow linea = Tbl.NewRow();
                        linea[0] = listaValores[0];
                        linea[1] = listaValores[1];
                        linea[2] = listaValores[2];
                        linea[3] = listaValores[3];
                        linea[4] = listaValores[4];
                        linea[5] = listaValores[5];
                        linea[6] = listaValores[6];
                        linea[7] = listaValores[7];
                        Tbl.Rows.Add(linea);
                    }

                    regla.VALUE_FORMULA = Convert.ToDecimal(Tbl.Rows[0][8].ToString());
                    regla.VALUE_MINIMUN = Convert.ToDecimal(Tbl.Rows[0][9].ToString());

                    if (regla.VALUE_MINIMUN > regla.VALUE_FORMULA)
                        regla.VALUE_R = regla.VALUE_MINIMUN;
                    else
                        regla.VALUE_R = regla.VALUE_FORMULA;
                }
                else
                {
                    regla.VALUE_FORMULA = vFormula;
                    regla.VALUE_MINIMUN = vMinimo;
                    if (regla.VALUE_MINIMUN > regla.VALUE_FORMULA)
                        regla.VALUE_R = regla.VALUE_MINIMUN;
                    else
                        regla.VALUE_R = regla.VALUE_FORMULA;
                }

                #endregion
                regla.STATE_CALC = true;
                valorFinal = regla.VALUE_R;
            }
            catch (Exception ex)
            {
                regla.STATE_CALC = false;
                valorFinal = 0;
            }
            return valorFinal;
        }  // CASO 1 -OK

        public decimal? ATsiAUno(BETarifaRegla regla, List<BETarifaCaracteristica> caracteristicas,
                                List<BETarifaTest> valores, decimal VUM)
        {
            decimal? valorFinal = 0;
            try
            {
                #region Obtener Valores
                decimal? vA = 0; string tA = string.Empty;
                decimal? vB = 0; string tB = string.Empty;
                decimal? vC = 0; string tC = string.Empty;
                decimal? vD = 0; string tD = string.Empty;
                decimal? vE = 0; string tE = string.Empty;


                int totCaracteristicas = caracteristicas.ToList().Count;
                if (totCaracteristicas > 0)
                {
                    vA = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.A).FirstOrDefault().LIC_CHAR_VAL;
                    tA = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.A).FirstOrDefault().IND_TR;
                }
                if (totCaracteristicas > 1)
                {
                    vB = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.B).FirstOrDefault().LIC_CHAR_VAL;
                    tB = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.B).FirstOrDefault().IND_TR;
                }
                if (totCaracteristicas > 2)
                {
                    vC = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.C).FirstOrDefault().LIC_CHAR_VAL;
                    tC = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.C).FirstOrDefault().IND_TR;
                }
                if (totCaracteristicas > 3)
                {
                    vD = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.D).FirstOrDefault().LIC_CHAR_VAL;
                    tD = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.D).FirstOrDefault().IND_TR;
                }
                if (totCaracteristicas > 4)
                {
                    vE = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.E).FirstOrDefault().LIC_CHAR_VAL;
                    tE = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.E).FirstOrDefault().IND_TR;
                }


                //Buscar R
                List<BETarifaTest> listaObtenerR = new List<BETarifaTest>();
                if (totCaracteristicas > 0)
                {
                    if (tA == Seleccion.SI)
                        listaObtenerR = valores.Where(
                                        v => (vA >= v.CRI1_FROM && vA <= v.CRI1_TO) || (v.CRI1_TO <= vA)
                                        ).ToList();
                    else if (tA == Seleccion.NO)
                        listaObtenerR = valores.Where(v => vA == v.CRI1_FROM).ToList();
                }

                if (totCaracteristicas > 1)
                {
                    if (tB == Seleccion.SI)
                        listaObtenerR = listaObtenerR.Where(
                                        v => (vB >= v.CRI2_FROM && vB <= v.CRI2_TO) || (v.CRI2_TO <= vB)
                                        ).ToList();
                    else if (tB == Seleccion.NO)
                        listaObtenerR = listaObtenerR.Where(v => vB == v.CRI2_FROM).ToList();
                }

                if (totCaracteristicas > 2)
                {
                    if (tC == Seleccion.SI)
                        listaObtenerR = listaObtenerR.Where(
                                        v => (vC >= v.CRI3_FROM && vC <= v.CRI3_TO) || (v.CRI3_TO <= vC)
                                        ).ToList();
                    else if (tC == Seleccion.NO)
                        listaObtenerR = listaObtenerR.Where(v => vC == v.CRI3_FROM).ToList();
                }

                if (totCaracteristicas > 3)
                {
                    if (tD == Seleccion.SI)
                        listaObtenerR = listaObtenerR.Where(
                                        v => (vD >= v.CRI4_FROM && vD <= v.CRI4_TO) || (v.CRI4_TO <= vD)
                                        ).ToList();
                    else if (tD == Seleccion.NO)
                        listaObtenerR = listaObtenerR.Where(v => vD == v.CRI4_FROM).ToList();
                }

                if (totCaracteristicas > 4)
                {
                    if (tE == Seleccion.SI)
                        listaObtenerR = listaObtenerR.Where(
                            v => (vE >= v.CRI5_FROM && vE <= v.CRI5_TO) || (v.CRI5_TO <= vE)
                            ).ToList();
                    else if (tE == Seleccion.NO)
                        listaObtenerR = listaObtenerR.Where(v => vE == v.CRI5_FROM).ToList();
                }
                #endregion

                #region CalcularAcumulado
                decimal? tempR = 1;
                decimal? tempRminimo = 1;
                decimal? sumarR = 0;
                decimal? sumarRminimo = 0;
                foreach (var item in listaObtenerR)
                {
                    tempR = 1;
                    tempRminimo = 1;

                    if (tA == Seleccion.SI)
                    {
                        if (item.CRI1_TO < vA)
                        {
                            tempR *= item.CRI1_TO;
                            tempRminimo *= item.CRI1_TO;
                        }
                        else
                        {
                            tempR *= (vA - (item.CRI1_FROM - 1));
                            tempRminimo *= (vA - (item.CRI1_FROM - 1));
                        }
                    }

                    if (tB == Seleccion.SI)
                    {
                        if (item.CRI2_TO < vB)
                        {
                            tempR *= item.CRI2_TO;
                            tempRminimo *= item.CRI2_TO;
                        }
                        else
                        {
                            tempR *= (vB - (item.CRI2_FROM - 1));
                            tempRminimo *= (vB - (item.CRI2_FROM - 1));
                        }
                    }

                    if (tC == Seleccion.SI)
                    {
                        if (item.CRI3_TO < vC)
                        {
                            tempR *= item.CRI3_TO;
                            tempRminimo *= item.CRI3_TO;
                        }
                        else
                        {
                            tempR *= (vC - (item.CRI3_FROM - 1));
                            tempRminimo *= (vC - (item.CRI3_FROM - 1));
                        }
                    }

                    if (tD == Seleccion.SI)
                    {
                        if (item.CRI4_TO < vD)
                        {
                            tempR *= item.CRI4_TO;
                            tempRminimo *= item.CRI4_TO;
                        }
                        else
                        {
                            tempR *= (vD - (item.CRI4_FROM - 1));
                            tempRminimo *= (vD - (item.CRI4_FROM - 1));
                        }
                    }

                    if (tE == Seleccion.SI)
                    {
                        if (item.CRI5_TO < vE)
                        {
                            tempR *= item.CRI5_TO;
                            tempRminimo *= item.CRI5_TO;
                        }
                        else
                        {
                            tempR *= (vE - (item.CRI5_FROM - 1));
                            tempRminimo *= (vE - (item.CRI5_FROM - 1));
                        }
                    }


                    sumarR += (tempR * item.VAL_FORMULA);
                    sumarRminimo += (tempRminimo * item.VAL_MINIMUM);
                }
                #endregion

                decimal? vFormula = 0;
                decimal? vMinimo = 0;
                decimal? valorR = 0;
                decimal? valorRmin = 0;

                #region Calcular
                vFormula = sumarR;
                vMinimo = sumarRminimo;

                valorR = vFormula;
                valorRmin = vMinimo;//

                //if (vMinimo > vFormula)
                //    valorR = vMinimo;
                //else
                //    valorR = vFormula;


                if (tA == Seleccion.MANUAL || tB == Seleccion.MANUAL ||
                    tC == Seleccion.MANUAL || tD == Seleccion.MANUAL || tE == Seleccion.MANUAL)
                {
                    decimal? Ra = 0;
                    decimal? Rb = 0;
                    decimal? Rc = 0;
                    decimal? Rd = 0;
                    decimal? Re = 0;

                    string formula;
                    string formulaMinima;
                    string[] listaOperandos = new string[10];
                    double[] listaValores = new double[10];
                    DataTable Tbl = new DataTable();

                    formula = regla.CALC_FORMULA;
                    formulaMinima = regla.CALC_MINIMUM.Replace(LetraReg.R, LetraReg.Rmin);//

                    foreach (var car in caracteristicas)
                    {
                        if (car.CHAR_ORI_REG == LetraCar.A) Ra = car.LIC_CHAR_VAL;
                        if (car.CHAR_ORI_REG == LetraCar.B) Rb = car.LIC_CHAR_VAL;
                        if (car.CHAR_ORI_REG == LetraCar.C) Rc = car.LIC_CHAR_VAL;
                        if (car.CHAR_ORI_REG == LetraCar.D) Rd = car.LIC_CHAR_VAL;
                        if (car.CHAR_ORI_REG == LetraCar.E) Re = car.LIC_CHAR_VAL;
                    }

                    listaOperandos[0] = LetraCar.A;
                    listaOperandos[1] = LetraCar.B;
                    listaOperandos[2] = LetraCar.C;
                    listaOperandos[3] = LetraCar.D;
                    listaOperandos[4] = LetraCar.E;
                    listaOperandos[5] = LetraReg.R;
                    listaOperandos[6] = LetraReg.V;
                    listaOperandos[7] = LetraReg.Rmin;

                    listaValores[0] = Convert.ToDouble(Ra);
                    listaValores[1] = Convert.ToDouble(Rb);
                    listaValores[2] = Convert.ToDouble(Rc);
                    listaValores[3] = Convert.ToDouble(Rd);
                    listaValores[4] = Convert.ToDouble(Re);

                    listaValores[5] = Convert.ToDouble(valorR);
                    //public decimal VUM = new BLValormusica().ObtenerActivo(GlobalVars.Global.OWNER).VUM_VAL;
                    listaValores[6] = Convert.ToDouble(VUM);
                    listaValores[7] = Convert.ToDouble(valorRmin);

                    //Tbl.Columns.Add("variable", typeof(string));
                    Tbl.Columns.Add(listaOperandos[0], typeof(double));
                    Tbl.Columns.Add(listaOperandos[1], typeof(double));
                    Tbl.Columns.Add(listaOperandos[2], typeof(double));
                    Tbl.Columns.Add(listaOperandos[3], typeof(double));
                    Tbl.Columns.Add(listaOperandos[4], typeof(double));
                    Tbl.Columns.Add(listaOperandos[5], typeof(double));
                    Tbl.Columns.Add(listaOperandos[6], typeof(double));
                    Tbl.Columns.Add(listaOperandos[7], typeof(double));
                    Tbl.Columns.Add("Tarifa", typeof(double), formula);
                    Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

                    {
                        // crea una nueva línea 
                        DataRow linea = Tbl.NewRow();
                        linea[0] = listaValores[0];
                        linea[1] = listaValores[1];
                        linea[2] = listaValores[2];
                        linea[3] = listaValores[3];
                        linea[4] = listaValores[4];
                        linea[5] = listaValores[5];
                        linea[6] = listaValores[6];
                        linea[7] = listaValores[7];
                        Tbl.Rows.Add(linea);
                    }

                    regla.VALUE_FORMULA = Convert.ToDecimal(Tbl.Rows[0][8].ToString());
                    regla.VALUE_MINIMUN = Convert.ToDecimal(Tbl.Rows[0][9].ToString());

                    if (regla.VALUE_MINIMUN > regla.VALUE_FORMULA)
                        regla.VALUE_R = regla.VALUE_MINIMUN;
                    else
                        regla.VALUE_R = regla.VALUE_FORMULA;

                    valorFinal = regla.VALUE_R;
                }
                else
                {
                    regla.VALUE_FORMULA = sumarR;
                    regla.VALUE_MINIMUN = sumarRminimo;

                    if (sumarRminimo > sumarR)
                        regla.VALUE_R = sumarRminimo;
                    else
                        regla.VALUE_R = sumarR;

                    valorFinal = regla.VALUE_R;
                }
                #endregion
                regla.STATE_CALC = true;
                return valorFinal;
            }
            catch (Exception ex)
            {
                regla.STATE_CALC = false;
                return 0;
            }
        }  // CASO 2

        public decimal? ATnoAUno(BETarifaRegla regla, List<BETarifaCaracteristica> caracteristicas,
                                List<BETarifaTest> valores, decimal VUM)
        {
            decimal? valorFinal = 0;
            try
            {
                #region Obtener Valores
                decimal? vA = 0; string tA = string.Empty;
                decimal? vB = 0; string tB = string.Empty;
                decimal? vC = 0; string tC = string.Empty;
                decimal? vD = 0; string tD = string.Empty;
                decimal? vE = 0; string tE = string.Empty;
                decimal? vFormulaTemp = 0;
                decimal? vMinimoTemp = 0;
                decimal? acumular = 1;

                int totCaracteristicas = caracteristicas.ToList().Count;
                if (totCaracteristicas > 0)
                {
                    vA = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.A).FirstOrDefault().LIC_CHAR_VAL;
                    tA = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.A).FirstOrDefault().IND_TR;
                }
                if (totCaracteristicas > 1)
                {
                    vB = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.B).FirstOrDefault().LIC_CHAR_VAL;
                    tB = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.B).FirstOrDefault().IND_TR;
                }
                if (totCaracteristicas > 2)
                {
                    vC = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.C).FirstOrDefault().LIC_CHAR_VAL;
                    tC = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.C).FirstOrDefault().IND_TR;
                }
                if (totCaracteristicas > 3)
                {
                    vD = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.D).FirstOrDefault().LIC_CHAR_VAL;
                    tD = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.D).FirstOrDefault().IND_TR;
                }
                if (totCaracteristicas > 4)
                {
                    vE = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.E).FirstOrDefault().LIC_CHAR_VAL;
                    tE = caracteristicas.Where(c => c.CHAR_ORI_REG == LetraCar.E).FirstOrDefault().IND_TR;
                }

                //Buscar R
                List<BETarifaTest> listaObtenerR = new List<BETarifaTest>();
                if (totCaracteristicas > 0)
                {
                    if (tA == Seleccion.SI)
                    {
                        listaObtenerR = valores.Where(v => vA >= v.CRI1_FROM && vA <= v.CRI1_TO).ToList();
                        acumular *= vA;
                    }
                    else if (tA == Seleccion.NO)
                        listaObtenerR = valores.Where(v => vA == v.CRI1_FROM).ToList();
                }

                if (totCaracteristicas > 1)
                {
                    if (tB == Seleccion.SI)
                    {
                        listaObtenerR = listaObtenerR.Where(v => vB >= v.CRI2_FROM && vB <= v.CRI2_TO).ToList();
                        acumular *= vB;
                    }
                    else if (tB == Seleccion.NO)
                        listaObtenerR = listaObtenerR.Where(v => vB == v.CRI2_FROM).ToList();
                }

                if (totCaracteristicas > 2)
                {
                    if (tC == Seleccion.SI)
                    {
                        listaObtenerR = listaObtenerR.Where(v => vC >= v.CRI3_FROM && vC <= v.CRI3_TO).ToList();
                        acumular *= vC;
                    }
                    else if (tC == Seleccion.NO)
                        listaObtenerR = listaObtenerR.Where(v => vC == v.CRI3_FROM).ToList();
                }

                if (totCaracteristicas > 3)
                {
                    if (tD == Seleccion.SI)
                    {
                        listaObtenerR = listaObtenerR.Where(v => vD >= v.CRI4_FROM && vD <= v.CRI4_TO).ToList();
                        acumular *= vD;
                    }
                    else if (tD == Seleccion.NO)
                        listaObtenerR = listaObtenerR.Where(v => vD == v.CRI4_FROM).ToList();
                }

                if (totCaracteristicas > 4)
                {
                    if (tE == Seleccion.SI)
                    {
                        listaObtenerR = listaObtenerR.Where(v => vE >= v.CRI5_FROM && vE <= v.CRI5_TO).ToList();
                        acumular *= vE;
                    }
                    else if (tE == Seleccion.NO)
                        listaObtenerR = listaObtenerR.Where(v => vE == v.CRI5_FROM).ToList();
                }
                #endregion

                #region Calcular

                decimal? vFormula = 0;
                decimal? vMinimo = 0;
                decimal? valorR = 0;
                decimal? valorRmin = 0;
                vFormulaTemp = listaObtenerR.FirstOrDefault().VAL_FORMULA;
                vMinimoTemp = listaObtenerR.FirstOrDefault().VAL_MINIMUM;

                vFormula = acumular * vFormulaTemp;
                vMinimo = acumular * vMinimoTemp;

                valorR = vFormula;
                valorRmin = vMinimo;//
                //if (vMinimo > vFormula)
                //    valorR = vMinimo;
                //else
                //    valorR = vFormula;

                //if (tA == Seleccion.MANUAL || tB == Seleccion.MANUAL ||
                //    tC == Seleccion.MANUAL || tD == Seleccion.MANUAL || tE == Seleccion.MANUAL)
                //{
                decimal? Ra = 0;
                decimal? Rb = 0;
                decimal? Rc = 0;
                decimal? Rd = 0;
                decimal? Re = 0;

                string formula;
                string formulaMinima;
                string[] listaOperandos = new string[10];
                double[] listaValores = new double[10];
                DataTable Tbl = new DataTable();

                formula = regla.CALC_FORMULA;
                formulaMinima = regla.CALC_MINIMUM.Replace(LetraReg.R, LetraReg.Rmin);//

                foreach (var car in caracteristicas)
                {
                    if (car.CHAR_ORI_REG == LetraCar.A) Ra = car.LIC_CHAR_VAL;
                    if (car.CHAR_ORI_REG == LetraCar.B) Rb = car.LIC_CHAR_VAL;
                    if (car.CHAR_ORI_REG == LetraCar.C) Rc = car.LIC_CHAR_VAL;
                    if (car.CHAR_ORI_REG == LetraCar.D) Rd = car.LIC_CHAR_VAL;
                    if (car.CHAR_ORI_REG == LetraCar.E) Re = car.LIC_CHAR_VAL;
                }

                listaOperandos[0] = LetraCar.A;
                listaOperandos[1] = LetraCar.B;
                listaOperandos[2] = LetraCar.C;
                listaOperandos[3] = LetraCar.D;
                listaOperandos[4] = LetraCar.E;
                listaOperandos[5] = LetraReg.R;
                listaOperandos[6] = LetraReg.V;
                listaOperandos[7] = LetraReg.Rmin;

                listaValores[0] = Convert.ToDouble(Ra);
                listaValores[1] = Convert.ToDouble(Rb);
                listaValores[2] = Convert.ToDouble(Rc);
                listaValores[3] = Convert.ToDouble(Rd);
                listaValores[4] = Convert.ToDouble(Re);

                listaValores[5] = Convert.ToDouble(valorR);
                listaValores[6] = Convert.ToDouble(VUM);
                listaValores[7] = Convert.ToDouble(valorRmin);

                //Tbl.Columns.Add("variable", typeof(string));
                Tbl.Columns.Add(listaOperandos[0], typeof(double));
                Tbl.Columns.Add(listaOperandos[1], typeof(double));
                Tbl.Columns.Add(listaOperandos[2], typeof(double));
                Tbl.Columns.Add(listaOperandos[3], typeof(double));
                Tbl.Columns.Add(listaOperandos[4], typeof(double));
                Tbl.Columns.Add(listaOperandos[5], typeof(double));
                Tbl.Columns.Add(listaOperandos[6], typeof(double));
                Tbl.Columns.Add(listaOperandos[7], typeof(double));
                Tbl.Columns.Add("Tarifa", typeof(double), formula);
                Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

                {
                    // crea una nueva línea 
                    DataRow linea = Tbl.NewRow();
                    linea[0] = listaValores[0];
                    linea[1] = listaValores[1];
                    linea[2] = listaValores[2];
                    linea[3] = listaValores[3];
                    linea[4] = listaValores[4];
                    linea[5] = listaValores[5];
                    linea[6] = listaValores[6];
                    linea[7] = listaValores[7];
                    Tbl.Rows.Add(linea);
                }

                regla.VALUE_FORMULA = Convert.ToDecimal(Tbl.Rows[0][8].ToString());
                regla.VALUE_MINIMUN = Convert.ToDecimal(Tbl.Rows[0][9].ToString());

                if (regla.VALUE_MINIMUN > regla.VALUE_FORMULA)
                    regla.VALUE_R = regla.VALUE_MINIMUN;
                else
                    regla.VALUE_R = regla.VALUE_FORMULA;
                //}
                //else
                //{
                //    regla.VALUE_FORMULA = vFormula;
                //    regla.VALUE_MINIMUN = vMinimo;

                //    if (regla.VALUE_MINIMUN > regla.VALUE_FORMULA)
                //        regla.VALUE_R = regla.VALUE_MINIMUN;
                //    else
                //        regla.VALUE_R = regla.VALUE_FORMULA;
                //}
                #endregion
                regla.STATE_CALC = true;
                valorFinal = regla.VALUE_R;
                return valorFinal;
            }
            catch (Exception ex)
            {
                regla.STATE_CALC = false;
                return 0;
            }
        }  // CASO 4 - OK

        public List<BEFacturaConsulta> ListarConsultaFacturaPage(string owner, string numSerial, decimal numFact, decimal idSoc,
                                                decimal grupoFact, string moneda, decimal idLic,
                                                DateTime Fini, DateTime Ffin, decimal idFact,
                                                decimal licTipo, decimal idBpsAgen,
                                                int pagina, int cantRegxPag)
        {
            return new DAFactura().ListarConsultaFacturaPage(owner, numSerial, numFact, idSoc,
                                                 grupoFact, moneda, idLic,
                                                 Fini, Ffin, idFact,
                                                 licTipo, idBpsAgen,
                                                 pagina, cantRegxPag);
        }

        public BEFactura ListarConsulta(string owner, string numSerial, decimal numFact, decimal idSoc,
                                                decimal grupoFact, string moneda, decimal idLic,
                                                DateTime Fini, DateTime Ffin, decimal idFact,
                                                int impresas, int anuladas, decimal licTipo, decimal agenteBpsId,
                                                int conFecha, int tipoDoc, decimal idOficina, decimal valorDivision, int estado, decimal idPlan, decimal idBpsGroup)
        {
            BEFactura Factura = new BEFactura();
            Factura.ListarFactura = new DAFactura().ListarConsulta(owner, numSerial, numFact, idSoc,
                                                 grupoFact, moneda, idLic,
                                                 Fini, Ffin, idFact, impresas, anuladas, licTipo, agenteBpsId,
                                                 conFecha, tipoDoc, idOficina, valorDivision, estado, idBpsGroup, idPlan);
            Factura.ListarLicencia = new DAFactura().ListarFactLicConsulta(owner, numSerial, numFact, idSoc,
                                                 grupoFact, moneda, idLic,
                                                 Fini, Ffin, idFact, impresas, anuladas, licTipo, agenteBpsId,
                                                 conFecha, tipoDoc, idOficina, valorDivision, estado, idBpsGroup, idPlan);
            Factura.ListarDetalleFactura = new DAFactura().ListarFac_LicPlanemientoSubGrilla
                                                        (owner, numSerial, numFact, idSoc,
                                                grupoFact, moneda, idLic,
                                                Fini, Ffin, idFact, impresas, anuladas, licTipo, agenteBpsId,
                                                conFecha, tipoDoc, idOficina, valorDivision, estado, idBpsGroup, idPlan);

            return Factura;
        }

        public List<BEFactura> ListaReporteCabeceraConsulta(string owner, decimal id)
        {
            return new DAFactura().ListaReporteCabeceraConsulta(owner, id);
        }


        public List<BEFacturaDetalle> ListaReporteDetalleConsulta(string owner, decimal id)
        {
            List<BEFacturaDetalle> ListaDetalle = new List<BEFacturaDetalle>();
            ListaDetalle = new DAFactura().ListaReporteDetalleConsulta(owner, id);
            foreach (var item in ListaDetalle)
            {
                item.TOTAL_LETRA = Utility.Util.NumeroALetras(item.INV_NET.ToString());
            }
            return ListaDetalle;
        }

        public List<BEFacturaConsulta> ListarConsultaFacturaBecPage(string owner, string numSerial, decimal numFact, decimal idSoc,
                                                     decimal grupoFact, string moneda, decimal idLic,
                                                     DateTime Fini, DateTime? Ffin, decimal idFact,
                                                     decimal licTipo, decimal idBpsAgen, int conFecha, decimal idOficinaUsuario,
                                                     int pagina, int cantRegxPag)
        {
            return new DAFactura().ListarConsultaFacturaBecPage(owner, numSerial, numFact, idSoc,
                                                 grupoFact, moneda, idLic,
                                                 Fini, Ffin, idFact,
                                                 licTipo, idBpsAgen, conFecha, idOficinaUsuario,
                                                 pagina, cantRegxPag);
        }

        public BEFactura ObtenerFacturaBec(string owner, decimal idFactura)
        {
            return new DAFactura().ObtenerFacturaBec(owner, idFactura);
        }

        public int AnularFactura(BEFactura factura)
        {
            return new DAFactura().AnularFactura(factura);
        }
        //Fact Can sin ANular*************
        public int FacturaCancSinAnul(BEFactura factura)
        {
            return new DAFactura().FacturaCancSinAnul(factura);
        }
        public BEFactura Aplica_Nota_Credito(decimal INV_ID, string USU, string TIP_NOT_CRE, string OBSERV, decimal SERIE)
        {
            return new DAFactura().Aplica_Nota_Credito(INV_ID, USU, TIP_NOT_CRE, OBSERV, SERIE);
        }
        //***********************************
        public decimal RedondeoTarifa(decimal monto)
        {
            decimal redondeo = 0;
            decimal num = 5;
            decimal resta = monto % num;
            if (resta < (num / 2))
                redondeo = monto - resta;
            else
                redondeo = monto + num - resta;
            return redondeo;
        }

        public List<BEFacturaDetalle> ReporteFactConsulta
                                   (string owner, string numSerial, decimal numFact, decimal idSoc,
                                   decimal grupoFact, string moneda, decimal idLic,
                                   DateTime Fini, DateTime Ffin, decimal idFact,
                                   int impresas, int anuladas, decimal licTipo, decimal agenteBpsId,
                                   int conFecha, int tipoDoc, decimal idOficina, decimal valorDivision, int estado)
        {
            return new DAFactura().ReporteFactConsulta(owner, numSerial, numFact, idSoc,
                                                  grupoFact, moneda, idLic,
                                                  Fini, Ffin, idFact, impresas, anuladas, licTipo, agenteBpsId,
                                                  conFecha, tipoDoc, idOficina, valorDivision, estado);
        }

        #endregion

        #region NotaCredito
        public BEFactura CabeceraFacturaNotaCredito(string owner, decimal Id)
        {
            return new DAFactura().CabeceraFacturaNotaCredito(owner, Id);
        }

        public BEFacturaDetalle DetalleFacturaNotaCredito(string owner, decimal Id)
        {
            BEFacturaDetalle FacturaDetalle = new BEFacturaDetalle();
            FacturaDetalle.FacturaDetalle = new DAFactura().DetalleFacturaNotaCredito(owner, Id);
            FacturaDetalle.Recibos = new DAFactura().ListarRecibosFactura(owner, Id);
            return FacturaDetalle;
        }

        public List<BEFacturaConsulta> ListarTipoFactura(string owner)
        {
            return new DAFactura().ListarTipoFactura(owner);
        }

        public int ActualizarReferenciaNotaCreditoFactura(string owner, decimal idFact, decimal idFactNew, string user)
        {
            return new DAFactura().ActualizarReferenciaNotaCreditoFactura(owner, idFact, idFactNew, user);
        }

        public int InsertarNotaCredito(BEFactura enFat, List<BEFacturaDetalle> enDetFact, string user, decimal idFact)
        {
            int code = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                //Inserta En uno Nuevo. 
                code = new DAFactura().InsertarFactura(enFat);

                foreach (var item in enDetFact)
                {
                    item.INV_ID = code;
                    var det = new DAFacturaDetalle().InsertarFacturaDetalle(item);
                }

                var dato = new BLRecibo().ActualizarSerie(GlobalVars.Global.OWNER, enFat.INV_NMR, "NC", user);

                var referencia = new BLFactura().ActualizarReferenciaNotaCreditoFactura(GlobalVars.Global.OWNER, idFact, code, user);

                transa.Complete();
            }
            return code;
        }
        #endregion


        /// <summary>
        /// OBTIENE LAS FACTURAS GENERADAS PARA UNA PLANIFICACION ASOCIADA A LA LICENCVIA.
        /// </summary>
        /// <param name="idPlanificacion"></param>
        /// <returns></returns>
        public List<BELicenciaPlaneamientoDetalle> FacturaXPlanificacion(decimal idPlanificacion)
        {
            return new DAFactura().FacturaXPlanificacion(idPlanificacion);
        }

        public int InsertarBorradorManual(List<BEFactura> listaFactura, string owner)
        {
            int idFactura = 0;
            //bool exitoFactura = false;
            //bool exitoFacturaDetalle = false;
            string xmlFactura = string.Empty;

            using (TransactionScope transa = new TransactionScope())
            {
                xmlFactura = Utility.Util.SerializarEntity(listaFactura);
                idFactura = new DAFactura().InsertarBorradorManual(xmlFactura, owner);
                if (idFactura > 0)
                {
                    var resultDet = new DAFacturaDetalle().InsertarDetalleBorradorXML(xmlFactura, owner);
                    if (resultDet)
                    {
                        var resultValCar = new DAFacturaDetalle().InsertarValoresCaracteristicaDetalleXML(xmlFactura, owner);
                    }
                }
                transa.Complete();
            }
            return idFactura;
        }

        public int ActualizarObs(BEFactura factura)
        {
            return new DAFactura().ActualizarObs(factura);
        }

        #region Descuentos Plantilla
        #region CADENAS
        /// <summary>
        /// OBTIENE EL VALOR DE LA FORMULA DE LOS DESCUENTOS
        /// </summary>
        /// <param name="lista">LISTA DE DESCUENTOS DE LICENCIA</param>
        /// <param name="LICID">ID DE LA LICENCIA</param>
        /// <param name="planeamiento">LISTA DE PLANIFICACION POR LICENCIA</param>
        /// <param name="bpsId"> ID DEL SOCIO</param>
        /// <returns></returns>
        public List<BEDescuentos> DescuentosPlantillaMasiva(List<BEDescuentos> lista, decimal LICID, List<BELicenciaPlaneamiento> planeamiento, decimal bpsId)
        {
            if (lista != null && lista.Count > 0)
                LICID = lista[0].LIC_ID;
            var respuestamodalidad = new BLLicencias().ValidaModalidadLicencia(LICID);//valida modalidad de licencia //comentar
            if (respuestamodalidad == 1)//comentar
            {//comentar
                foreach (var x in lista.OrderBy(x => x.DISC_ID))
                {
                    int res = new BLDescuentos().ValidaDescuentoPlantilla(x.DISC_ID);
                    if (res == DescPlantilla.SI)//Si el Descuento Posee Plantilla  ==1
                    {


                        //Cadenas tiene como maximo 3 variables para evaluar..
                        var ListCaractxDesc = new BLDescuentos().listaPlantillaxDISCID(x.DISC_ID);//Obtengo las Caracteristicas ...
                        var VALIDALICMAESTRA = new BLLicencias().ListaLicenciaMaestraxLicid(LICID);

                        if (x.DISC_ID != DescPlantilla.DESCUENTO_INDIVIDUAL)//*************VALIDA SI ES CADENA  (PARA QUE NO OBTENGA 2 DESCUENTOS  )
                        {
                            #region LICENCIA MULTIPLE(CADENA)
                            decimal? CHAR1 = null, CHAR2 = null, CHAR3 = null;
                            decimal? param1 = null, param2 = null, param3 = null;
                            var numperiodos = planeamiento.Where(p => p.LIC_ID == LICID && (p.LIC_PL_STATUS == DescPlantilla.ABIERTO || p.LIC_PL_STATUS == DescPlantilla.PENDIENTE)).Count(); //A Y P
                            //****************por eso se manda en Duro ************************************
                            if (ListCaractxDesc.Count > 0) //char 1
                            {
                                CHAR1 = ListCaractxDesc[0].CHAR_ID;
                                var LICMAESTRA = new BLLicencias().ListaLicenciaMaestraxLicid(LICID);
                                decimal licmaster = Convert.ToDecimal(LICMAESTRA.LIC_MASTER);//convirtiendo los datos de la lic maestra.

                                var listalichijas = new BLLicencias().ListarLicHijasxPadre(licmaster);

                                param1 = listalichijas.Count; //NUMERO DE LOCALES DE LA LICENCIA

                            }
                            if (ListCaractxDesc.Count > 1)//char 2
                            {
                                CHAR2 = ListCaractxDesc[1].CHAR_ID;
                                if (numperiodos >= DescPlantilla.ANUAL) //12
                                    param2 = 3;
                                else if (numperiodos >= DescPlantilla.SEMESTRAL && numperiodos < DescPlantilla.ANUAL) //6 y 12
                                    param2 = 2;
                                else if (numperiodos < DescPlantilla.SEMESTRAL) //6 
                                    param2 = 1;
                                else
                                    param2 = null;
                            }
                            if (ListCaractxDesc.Count > 2)//char 3
                                CHAR3 = ListCaractxDesc[2].CHAR_ID;
                            //--********************FIN DE LA DECLARACION DE CHAR *************************
                            //*********************ENVIAR VARIABLES **************************************
                            x.DISC_PERC = new BLDescuentos().ObtieneDescuentoPlantillaCadena(x.DISC_ID, param1, CHAR1, param2, CHAR2, param3, CHAR3);
                            x.monto = DescPlantilla.SI;//borrar esto si sale error *
                            x.FORMATO = "%";
                            #endregion
                        }
                        else //***********dESCUENTO POR LOCALES INDIVIDUALES  (ES LO MISMO PERO SE VALIDA PARA QUE NO INGRESE A LOS 2 
                        {
                            #region LICENCIA INDIVIDUAL(LOCALES)
                            if (VALIDALICMAESTRA.LIC_MASTER == 0)
                            {
                                decimal? CHAR1 = null, CHAR2 = null, CHAR3 = null;
                                decimal? param1 = null, param2 = null, param3 = null;
                                var numperiodos = planeamiento.Where(p => p.LIC_ID == LICID && (p.LIC_PL_STATUS == DescPlantilla.ABIERTO || p.LIC_PL_STATUS == DescPlantilla.PENDIENTE)).Count(); //A Y P
                                //****************por eso se manda en Duro ************************************
                                if (ListCaractxDesc.Count > 0) //char 1
                                {
                                    CHAR1 = ListCaractxDesc[0].CHAR_ID;
                                    if (numperiodos >= DescPlantilla.ANUAL) //12
                                        param1 = 2;
                                    else if (numperiodos >= DescPlantilla.SEMESTRAL && numperiodos < DescPlantilla.ANUAL) //6 y 12
                                        param1 = 1;

                                }
                                //--********************FIN DE LA DECLARACION DE CHAR *************************
                                //*********************ENVIAR VARIABLES **************************************
                                x.DISC_PERC = new BLDescuentos().ObtieneDescuentoPlantillaCadena(x.DISC_ID, param1, CHAR1, param2, CHAR2, param3, CHAR3);
                                x.monto = DescPlantilla.SI;//borrar esto si sale error *
                                x.FORMATO = "%";
                            }
                            #endregion
                        }

                    }
                }
            }//comentar
            return lista;
        }

        /// <summary>
        /// OBTIENE EL MONTO TOTAL DE LOS DESCUENTOS PLANTILLA POR LICENCIA INDIVIDUAL
        /// </summary>
        /// <param name="LIC_ID"></param>
        /// <param name="numperiodos"></param>
        /// <returns></returns>
        public List<BEDescuentos> DescuentoPlantillaIndvidual(decimal LIC_ID, decimal MONTO_DET, int numperiodos) // otros campos si se necesita
        {
            List<BEDescuentos> lista = null;
            var respuestamodalidad = new BLLicencias().ValidaModalidadLicencia(LIC_ID);//valida modalidad de licencia //comentar
            if (respuestamodalidad == 1)//comentar
            {//comentar
                decimal monto = 0;
                decimal desc_acum = 0;

                string owner = GlobalVars.Global.OWNER;

                lista = new BLLicenciaDescuento().ListaDescuentos(owner, LIC_ID);//OBTENGO LOS DESCUENTOS..
                decimal? CHAR1 = null, CHAR2 = null, CHAR3 = null;
                decimal? param1 = null, param2 = null, param3 = null;

                foreach (var x in lista)
                {
                    int res = new BLDescuentos().ValidaDescuentoPlantilla(x.DISC_ID);
                    x.LIC_ID = LIC_ID;
                    if (res == 1)//Si el Descuento Posee Plantilla 
                    {
                        var ListCaractxDesc = new BLDescuentos().listaPlantillaxDISCID(x.DISC_ID);//Obtengo las Caracteristicas ...
                        var VALIDALICMAESTRA = new BLLicencias().ListaLicenciaMaestraxLicid(LIC_ID);
                        #region  licencia multiple(CADENA)
                        if (x.DISC_ID != DescPlantilla.DESCUENTO_INDIVIDUAL)
                        {
                            if (ListCaractxDesc.Count > 0) //char 1
                            {
                                CHAR1 = ListCaractxDesc[0].CHAR_ID;
                                var LICMAESTRA = new BLLicencias().ListaLicenciaMaestraxLicid(LIC_ID);

                                decimal licmaster = Convert.ToDecimal(LICMAESTRA.LIC_MASTER);//convirtiendo los datos de la lic maestra.

                                var listalichijas = new BLLicencias().ListarLicHijasxPadre(licmaster);

                                param1 = listalichijas.Count; //NUMERO DE LOCALES DE LA LICENCIA

                            }
                            if (ListCaractxDesc.Count > 1)//char 2
                            {
                                CHAR2 = ListCaractxDesc[1].CHAR_ID;
                                if (numperiodos >= DescPlantilla.ANUAL) //12
                                    param2 = 3;
                                else if (numperiodos >= DescPlantilla.SEMESTRAL && numperiodos < DescPlantilla.ANUAL) //6 y 12
                                    param2 = 2;
                                else if (numperiodos < DescPlantilla.SEMESTRAL) //6 
                                    param2 = 1;
                                else
                                    param2 = null;
                            }
                            if (ListCaractxDesc.Count > 2)//char 3
                                CHAR3 = ListCaractxDesc[2].CHAR_ID;

                            //--********************FIN DE LA DECLARACION DE CHAR *************************
                            //*********************ENVIAR VARIABLES **************************************
                            desc_acum = new BLDescuentos().ObtieneDescuentoPlantillaCadena(x.DISC_ID, param1, CHAR1, param2, CHAR2, param3, CHAR3); //acumulando el desc total
                            //monto = monto + (MONTO_DET * (desc_acum/100));
                            x.monto = (MONTO_DET * (desc_acum / 100));
                            x.DISC_PERC = desc_acum;
                            //   x.FORMATO = "%";
                        }
                        #endregion
                        #region Licencia Individual(Locales)
                        else
                        {
                            if (VALIDALICMAESTRA.LIC_MASTER == 0)
                            {
                                if (ListCaractxDesc.Count > 0) //char 1
                                {
                                    CHAR2 = null; param2 = null;
                                    CHAR1 = ListCaractxDesc[0].CHAR_ID;
                                    if (numperiodos >= DescPlantilla.ANUAL) //12
                                        param1 = 2;
                                    else if (numperiodos >= DescPlantilla.SEMESTRAL && numperiodos < DescPlantilla.ANUAL) //6 y 12
                                        param1 = 1;

                                }
                                //--********************FIN DE LA DECLARACION DE CHAR *************************
                                //*********************ENVIAR VARIABLES **************************************
                                desc_acum = new BLDescuentos().ObtieneDescuentoPlantillaCadena(x.DISC_ID, param1, CHAR1, param2, CHAR2, param3, CHAR3); //acumulando el desc total
                                //monto = monto + (MONTO_DET * (desc_acum/100));
                                x.monto = (MONTO_DET * (desc_acum / 100));
                                x.DISC_PERC = desc_acum;
                                //   x.FORMATO = "%";
                            }
                        }
                        #endregion
                    }
                }

            }
            return lista;
        }
        #endregion

        #region Valida Descuentos

        public List<BELicencias> ValidaLicenciaAlDia(List<BELicencias> lista)
        {
            string owner = GlobalVars.Global.OWNER;
            string listaxml = string.Empty;
            listaxml = Utility.Util.SerializarEntity(lista);
            return new DALicenciaPlaneamiento().ValidaLicenciaAlDia(owner, listaxml);
        }
        #endregion


        #endregion

        #region EMISION MENSUAL
        public List<BELicencias> ListarLicenciaNoAlDiaEmisionMensual(string xml)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DALicencias().ListarLicenciaNoAlDiaEmisionMensual(xml, owner);
        }
        #endregion

        #region Planilla
        #endregion


        public List<BEValoresConfig> ListaTipoFacturacionManual()
        {
            return new DAFactura().ListaTipoFacturacionManual();
        }

        public List<BEFactura> ObtenerTipoDocumento(decimal idfactura)
        {
            return new DAFactura().ObtenerTipoDocumento(idfactura);
        }

        public List<BEFactura> ObtenerTipoComprobante(decimal serie)
        {
            return new DAFactura().ObtenerTipoComprobante(serie);
        }

        public int LimpiarBorradores(decimal idoficina)
        {
            return new DAFactura().LimpiarBorradores(idoficina);
        }
        public int LimpiarBorradorexLicencia(decimal LIC_ID)
        {
            return new DAFactura().LimpiarBorradorexLicencia(LIC_ID);
        }

        public bool ActualizarPeriodosFacturandose(string owner, List<BELicenciaPlaneamiento> ListaPlaneamiento, int OPCION)
        {
            bool exitoFacturaDetalle = false;
            string xmllistaplaneamiento = string.Empty;

            xmllistaplaneamiento = Utility.Util.SerializarEntity(ListaPlaneamiento);
            exitoFacturaDetalle = new DAFacturaDetalle().ActualizarPeriodosFacturandose(owner, xmllistaplaneamiento, OPCION);

            return exitoFacturaDetalle;
        }


        public int ValidarPermisoEmisionMensual(decimal OFF_ID)
        {



            int r = new DAFactura().ValidarPermisoEmisionMensual(OFF_ID);

            return r;
        }


        #region FACTURACION MASIVA SUNAT XML
        public List<BECabeceraFactura> ListaCabezeraMasivaSunat(string owner, List<BEFactura> listacabseleccionada)
        {
            string xmlCabezera = string.Empty;

            xmlCabezera = Utility.Util.SerializarEntity(listacabseleccionada);
            return new DAFactura().ListaCabezeraMasivaSunat(owner, xmlCabezera);

        }

        public List<BEDetalleFactura> ListaDetalleaMasivaSunat(string owner, List<BEFactura> listacabseleccionada)
        {
            string xmlCabezera = string.Empty;

            xmlCabezera = Utility.Util.SerializarEntity(listacabseleccionada);
            return new DAFactura().ListaDetalleaMasivaSunat(owner, xmlCabezera);

        }

        public int ValidaSerieNCDocumentoAplicar(decimal NMR_ID, decimal INV_ID)
        {

            return new DAFactura().ValidaSerieNCDocumentoAplicar(NMR_ID, INV_ID);
        }

        #endregion

        public int ValidaQuiebra(decimal INV_ID)
        {
            int r = new DAFactura().ValidaQuiebra(INV_ID);
            return r;
        }
        public int EnviarQuiebra(decimal INV_ID, string OBS, string USER)
        {
            int r = new DAFactura().EnviarQuiebra(INV_ID, OBS, USER);
            return r;
        }

        public int ObtenerDiaMinimoFechaManual()
        {
            return new DAFactura().ObtenerDiaMinimoFechaManual();
        }

        public int AnularNCRevert(decimal INV_ID, string OBSERVACION)
        {
            return new DAFactura().AnularNCRevert(INV_ID, OBSERVACION);
        }

        public int ActualizaLicenciaValidacion(decimal LIC_ID)
        {
            return new DAFactura().ACTUALIZA_LICENCIA_VALIDACION(LIC_ID);
        }

        public int ObtienePermisoActualLicencia(decimal LIC_ID)
        {
            return new DAFactura().ObtienePermisoActualLicencia(LIC_ID);
        }

        #region REENVIO MASIVO - EMISION MENSUAL LOCALES Y TRANS 

        public List<BECabeceraFactura> ListaCabezeraMasivaSunatEmiMensualLocTrans(DateTime fechaInicio, DateTime fechaFin, decimal Oficina)
        {

            return new DAFactura().ListaCabezeraMasivaSunatEmiMensualLocTrans(fechaInicio, fechaFin, Oficina);

        }


        public List<BEDetalleFactura> ListaDetalleaMasivaSunatEmisionMensualLocTrans(DateTime fechaInicio, DateTime fechaFin, decimal Oficina)
        {

            return new DAFactura().ListaDetalleaMasivaSunatEmisionMensualLocTrans(fechaInicio, fechaFin, Oficina);

        }
        #endregion

        #region Envio Sunat Manuales

        public List<BECabeceraFactura> ListaCabezeraMasivaSunatManuales(DateTime fechaInicio, DateTime fechaFin, decimal Oficina)
        {

            return new DAFactura().ListaCabezeraMasivaSunatManuales(fechaInicio, fechaFin, Oficina);

        }
        #endregion

    }
}
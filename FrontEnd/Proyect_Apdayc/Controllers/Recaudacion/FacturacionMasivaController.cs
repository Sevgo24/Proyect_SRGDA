
using System;
using SGRDA.BL;
using SGRDA.Entities;
using SGRDA.Entities.FacturaElectronica;
using SGRDA.BL.FacturaElectronica;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases.Factura_Electronica;
using Proyect_Apdayc.Clases;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using SGRDA.Utility;
using System.Threading;
using SGRDA.DA;

namespace Proyect_Apdayc.Controllers.Recaudacion
{
    public class FacturacionMasivaController : Base
    {
        //
        // GET: /FacturacionMasiva/

        int resultado = 0;
        string MSG_SUNAT = "";
        private DateTime FechaSistema = new BLREC_RATES_GRAL().ObtenerFechaSistema();
        public const string nomAplicacion = "SRGDA";
        private const string K_SESION_FACTURACION_LISTA_TMP = "___DTOBeFacturacioListaTmp";
        private const string K_SESION_FACTURACION_MASIVA = "___DTOFacturacionMasiva";
        private const string K_SESION_FACTURACION_MASIVA_2 = "___DTOFacturacionMasiva2";
        private const string K_SESION_LICENCIA_MASIVA = "___DTOLicenciaMasiva";
        private const string K_SESION_LICENCIA_DETALLE_MASIVA = "___DTOLicenciaDetalleMasiva";

        private const string K_SESION_BOR_FACTURACION_LISTA_TMP = "___DTOBorBeFacturacioListaTmp";
        private const string K_SESION_BOR_FACTURACION_MASIVA = "___DTOBorFacturacionMasiva";
        private const string K_SESION_BOR_LICENCIA_MASIVA = "___DTOBorLicenciaMasiva";
        private const string K_SESION_BOR_LICENCIA_DETALLE_MASIVA = "___DTOBorLicenciaDetalleMasiva";
        private const string K_SESION_BOR_LICENCIA_DESCUENTO = "___DTOBorLicenciaDescuento";
        private const string K_SESION_BOR_LICENCIA_DESCUENTOC_PLANTILLA = "___DTOBorLicenciaDescuentoPlantilla";
        private decimal libConfigFact = Convert.ToDecimal(System.Web.Configuration.WebConfigurationManager.AppSettings["LibConfigFactura"]);
        public decimal VUM = new BLValormusica().ObtenerActivo(GlobalVars.Global.OWNER).VUM_VAL;
        List<DTOFactura> facturacionMasiva = new List<DTOFactura>();
        List<DTOLicencia> licenciaMasiva = new List<DTOLicencia>();
        List<DTOLicenciaPlaneamiento> licenciaDetalleMasiva = new List<DTOLicenciaPlaneamiento>();
        List<DTOFactura> borfacturacionMasiva = new List<DTOFactura>();
        public static List<DTOLicencia> LicenciaMasivaTmp_2 = new List<DTOLicencia>();
        List<DTOLicencia> borlicenciaMasiva = new List<DTOLicencia>();
        List<DTOFacturaDetallle> borlicenciaDetalleMasiva = new List<DTOFacturaDetallle>();
        List<DTODescuento> borlicenciaDescuento = new List<DTODescuento>();
        List<DTODescuentoPlantilla> borlicenciaDescuentoPlantilla = new List<DTODescuentoPlantilla>();

        DTOFactura Factura = new DTOFactura();

        //FACTURA ELECTRONICA
        ComprobanteElectronica FE = new ComprobanteElectronica();

        #region VISTAS
        public ActionResult Index()
        {
            Init(false);
            Session.Remove(K_SESION_FACTURACION_LISTA_TMP);
            Session.Remove(K_SESION_FACTURACION_MASIVA);
            Session.Remove(K_SESION_LICENCIA_MASIVA);
            Session.Remove(K_SESION_LICENCIA_DETALLE_MASIVA);
            Session.Remove(K_SESION_BOR_LICENCIA_DESCUENTOC_PLANTILLA);
            return View();
        }

        public ActionResult ConsultaBorrador()
        {
            Init(false);
            return View();
        }

        public ActionResult ReporteErroresLicencia()
        {
            ViewBag.Fecha = DateTime.Now.ToShortDateString();
            ViewBag.Hora = DateTime.Now.ToShortTimeString();
            ViewBag.Usuario = UsuarioActual;
            Init(false);
            return View();
        }
        #endregion

        #region VARIABLES
        public List<BEFactura> listaTempFacturacionMasivaTmp
        {
            get
            {
                return (List<BEFactura>)Session[K_SESION_FACTURACION_LISTA_TMP];
            }
            set
            {
                Session[K_SESION_FACTURACION_LISTA_TMP] = value;
            }
        }

        public List<DTOFactura> FacturacionMasivaTmp
        {
            get
            {
                return (List<DTOFactura>)Session[K_SESION_FACTURACION_MASIVA];
            }
            set
            {
                Session[K_SESION_FACTURACION_MASIVA] = value;
            }
        }

        public List<DTOLicencia> LicenciaMasivaTmp
        {
            get
            {
                return (List<DTOLicencia>)Session[K_SESION_LICENCIA_MASIVA];
            }
            set
            {
                Session[K_SESION_LICENCIA_MASIVA] = value;
            }
        }

        //public List<DTOLicencia> LicenciaMasivaTmp_2
        //{
        //    get
        //    {
        //        return (List<DTOLicencia>)Session[K_SESION_FACTURACION_MASIVA_2];
        //    }
        //    set
        //    {
        //        Session[K_SESION_FACTURACION_MASIVA_2] = value;
        //    }
        //}

        public List<DTOLicenciaPlaneamiento> LicenciaDetalleMasivaTmp
        {
            get
            {
                return (List<DTOLicenciaPlaneamiento>)Session[K_SESION_LICENCIA_DETALLE_MASIVA];
            }
            set
            {
                Session[K_SESION_LICENCIA_DETALLE_MASIVA] = value;
            }
        }
        //----------------------------------------------------------------------
        public List<BEFactura> listaTempBorFacturacionMasivaTmp
        {
            get
            {
                return (List<BEFactura>)Session[K_SESION_BOR_FACTURACION_LISTA_TMP];
            }
            set
            {
                Session[K_SESION_BOR_FACTURACION_LISTA_TMP] = value;
            }
        }

        public List<DTOFactura> BorFacturacionMasivaTmp
        {
            get
            {
                return (List<DTOFactura>)Session[K_SESION_BOR_FACTURACION_MASIVA];
            }
            set
            {
                Session[K_SESION_BOR_FACTURACION_MASIVA] = value;
            }
        }

        public List<DTOLicencia> BorLicenciaMasivaTmp
        {
            get
            {
                return (List<DTOLicencia>)Session[K_SESION_BOR_LICENCIA_MASIVA];
            }
            set
            {
                Session[K_SESION_BOR_LICENCIA_MASIVA] = value;
            }
        }

        public List<DTOFacturaDetallle> BorLicenciaDetalleMasivaTmp
        {
            get
            {
                return (List<DTOFacturaDetallle>)Session[K_SESION_BOR_LICENCIA_DETALLE_MASIVA];
            }
            set
            {
                Session[K_SESION_BOR_LICENCIA_DETALLE_MASIVA] = value;
            }
        }

        public List<DTODescuento> borLicenciaDescuentosTmp
        {
            get
            {
                return (List<DTODescuento>)Session[K_SESION_BOR_LICENCIA_DESCUENTO];
            }
            set
            {
                Session[K_SESION_BOR_LICENCIA_DESCUENTO] = value;
            }
        }
        public List<DTODescuentoPlantilla> borLicenciaDescuentosPlantillaTmp
        {
            get
            {
                return (List<DTODescuentoPlantilla>)Session[K_SESION_BOR_LICENCIA_DESCUENTOC_PLANTILLA];
            }
            set
            {
                Session[K_SESION_BOR_LICENCIA_DESCUENTOC_PLANTILLA] = value;
            }
        }

        #endregion

        private class Variables
        {
            public const int SI = 1;
            public const int NO = 0;
            public const int CERO = 0;
            public const int FACTURACION_MASIVA = 1;
            public const int FACTURACION_INDIVIDUAL = 0;
            public const string MSJ_LICENCIA_SIN_PERMISO_PARA_FACTURAR = "LA LICENCIA NO CUENTA CON APROBACION PARA FACTURAR | "
                + " ES NECESARIO ADJUNTAR LA FICHA DE LEVANTAMIENTO Y ESPERAR LA APROBACION ";
        }

        #region FACTURACION MASIVA
        public JsonResult ListarFacturaMasivaSubGrilla(DateTime fini, DateTime ffin,
                                  string mogId, decimal modId, decimal dadId, decimal bpsId,
                                  decimal offId, decimal e_bpsId, decimal tipoEstId,
                                  decimal subTipoEstId, decimal licId, string monedaId, int historico, decimal idBpsGroup, decimal groupfact, int tipoFact, int EmiMensual)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    Session.Remove(K_SESION_FACTURACION_LISTA_TMP);
                    Session.Remove(K_SESION_FACTURACION_MASIVA);
                    Session.Remove(K_SESION_LICENCIA_MASIVA);
                    Session.Remove(K_SESION_LICENCIA_DETALLE_MASIVA);
                    string estadoPeriodo = Constantes.EstadoPeriodo.ABIERTO;
                    int oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                    oficina = new BLOffices().ValidaOficina(oficina);//si es oficina de admin o conta devuelve 0 para poder visualizar todo
                    var FacturaMasiva = new BLFactura().ListarFacturaMasivaSubGrilla(GlobalVars.Global.OWNER, fini, ffin,
                                                                                                mogId, modId, dadId, bpsId,
                                                                                                offId, e_bpsId, tipoEstId,
                                                                                                subTipoEstId, licId, monedaId,
                                                                                                libConfigFact, VUM, historico, estadoPeriodo, idBpsGroup, groupfact, oficina, tipoFact, EmiMensual);
                    #region GuardaDescuentoPlantillaTemporal
                    //Recupera los Descuentos en temporal (necesario para guardar el monto calculado         
                    var listaTTDES = FacturaMasiva.TT_Descuento;
                    borLicenciaDescuentosPlantillaTmp = new List<DTODescuentoPlantilla>();
                    if (listaTTDES != null)
                    {
                        listaTTDES.ForEach(x =>
                        {
                            borLicenciaDescuentosPlantillaTmp.Add(new DTODescuentoPlantilla
                            {
                                Id = x.DISC_ID,
                                LicId = x.LIC_ID,
                                Orden = x.ORDEN,
                                DesOrigen = x.DISC_ORG,
                                Tipo = x.DISC_PERC == 0 ? "V" : "P",
                                Valor = x.DISC_PERC == 0 ? x.DISC_VALUE : x.DISC_PERC,
                                Formato = x.DISC_SIGN,
                                Cuenta = x.DISC_ACC,
                                TEMP_ID_DSC = x.monto //Solo momentaneo
                            });
                        });
                    }


                    #endregion

                    if (FacturaMasiva.ListarLicenciaPlaneamiento != null)
                    {
                        licenciaDetalleMasiva = new List<DTOLicenciaPlaneamiento>();
                        FacturaMasiva.ListarLicenciaPlaneamiento.ForEach(s =>
                        {
                            licenciaDetalleMasiva.Add(new DTOLicenciaPlaneamiento
                            {
                                Nro = s.Nro,
                                codigoLP = s.LIC_PL_ID,
                                codigoLic = s.LIC_ID,
                                codigoTarifa = s.RATE_ID,
                                fecha = s.LIC_DATE,
                                anio = s.LIC_YEAR,
                                mesNom = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(s.LIC_MONTH)).ToUpper(),
                                codigoMod = s.MOD_ID,
                                codigoEst = s.EST_ID,
                                SubMonto = s.SUB_MONTO,
                                Descuento = s.DESCUENTO,
                                Monto = s.MONTO,
                                valorImpuesto = s.TAXV_VALUEP,
                                Observacion = s.OBSERVACION
                            });
                        });
                        LicenciaDetalleMasivaTmp = licenciaDetalleMasiva;

                        //if (LicenciaDetalleMasivaTmp.Count > 0)
                        //{
                        //    #region Redondeo Por Factura

                        //    decimal RedondeoMontoTotal = 0;

                        //    foreach (var i in LicenciaDetalleMasivaTmp)
                        //    {
                        //        RedondeoMontoTotal += i.Monto;
                        //    }

                        //    var PrimerPeriodo = LicenciaDetalleMasivaTmp.FirstOrDefault();  
                        //    //Primer Periodo Seleccionado                                                                                            
                        //    //var PrimerPeriodo = LicenciaDetalleMasivaTmp.Where(P => P.codigoLP == Pr.codigoLP).FirstOrDefault(); // Completando DAtos Primer Periodo Seleccionado

                        //    //if (tarifa.RATE_REDONDEO_ESP == Variables.SI)                                                                                            
                        //    //{                                                                                           
                        //    //decimal Decimales= Decimal.Round(RedondeoMontoTotal, 2);

                        //    float numDecimal = float.Parse("0," + Convert.ToString(RedondeoMontoTotal).Split('.')[1]);
                        //    string DecuentoString = "0." + numDecimal;
                        //    double Descuento = Convert.ToDouble(DecuentoString);
                        //    if (Descuento < 0.5 && numDecimal != 0)
                        //    {
                        //        PrimerPeriodo.Descuento = Convert.ToDecimal(Descuento) + PrimerPeriodo.Descuento;
                        //        PrimerPeriodo.Monto = PrimerPeriodo.Monto - Convert.ToDecimal(Descuento);
                        //    }
                        //    else if (Descuento > 0.5 && numDecimal != 0)
                        //    {
                        //        decimal Desc = Convert.ToDecimal(Descuento) - Convert.ToDecimal("0.5");
                        //        PrimerPeriodo.Descuento = Desc + PrimerPeriodo.Descuento;
                        //        PrimerPeriodo.Monto = PrimerPeriodo.Monto - Desc;
                        //    }
                        //    #endregion
                        //}


                    }


                    if (FacturaMasiva.ListarLicencia != null)
                    {
                        licenciaMasiva = new List<DTOLicencia>();
                        FacturaMasiva.ListarLicencia.ForEach(s =>
                        {
                            decimal SubTotalAcumulado = 0;
                            decimal DescuentoAcumulado = 0;
                            decimal TotalAcumulado = 0;
                            decimal EstadoVisualizarDetalle = 0;
                            var listaDetalle = FacturaMasiva.ListarLicenciaPlaneamiento.Where(x => x.LIC_ID == s.LIC_ID).ToList();
                            foreach (var item in listaDetalle)
                            {
                                SubTotalAcumulado += item.SUB_MONTO;
                                DescuentoAcumulado += item.DESCUENTO;
                                TotalAcumulado += item.MONTO;
                                if (item.STATE_CALC) EstadoVisualizarDetalle += 1;

                                //  if (!item.STATE_CALC_FACT) {

                                s.STATE_CALC_FACT_L = item.STATE_CALC_FACT; //asignando falso a la licencia .
                                s.OBSERVACION = item.OBSERVACION;
                                //}
                            }

                            s.SUBTOTAL = SubTotalAcumulado;
                            s.DESCUENTO = DescuentoAcumulado;
                            s.TOTAL = TotalAcumulado;
                            s.STATE_CALC = (listaDetalle.Count == EstadoVisualizarDetalle) ? true : false;
                            ////decimal Total = 0;
                            ////foreach(var i in FacturaMasiva.ListarLicencia)
                            ////{
                            ////    Total= i.MONTO
                            ////}
                            //if (s.TOTAL.ToString().Contains('.'))
                            //{
                            //    float numDecimal = float.Parse("0," + Convert.ToString(s.TOTAL).Split('.')[1]);
                            //    string DecuentoString = "0." + numDecimal;
                            //    double Descuento = Convert.ToDouble(DecuentoString);

                            //    if (Descuento < 0.5 && numDecimal != 0)
                            //    {
                            //        s.DESCUENTO = Convert.ToDecimal(Descuento) + s.DESCUENTO;
                            //        s.TOTAL = s.TOTAL - Convert.ToDecimal(Descuento);
                            //    }
                            //    else if (Descuento > 0.5 && numDecimal != 0)
                            //    {
                            //        decimal Desc = Convert.ToDecimal(Descuento) - Convert.ToDecimal("0.5");
                            //        s.DESCUENTO = Desc + s.DESCUENTO;
                            //        s.TOTAL = s.TOTAL - Desc;
                            //    }
                            //}


                            licenciaMasiva.Add(new DTOLicencia
                            {
                                Nro = s.Nro,
                                codLicencia = s.LIC_ID,
                                nombreLicencia = s.LIC_NAME,
                                Modalidad = s.Modalidad,
                                Establecimiento = s.Establecimiento,
                                Observacion = s.OBSERVACION,
                                SubTotal = s.SUBTOTAL,

                                Descuento = s.DESCUENTO,
                                Total = s.TOTAL,


                            });
                        });
                        LicenciaMasivaTmp = licenciaMasiva;
                        LicenciaMasivaTmp_2 = licenciaMasiva;



                    }

                    if (GlobalVars.Global.RegMontoLirics == true)
                    {
                        decimal resultLicencia = 0;
                        List<BELicencias> ListaMontoLirics = new List<BELicencias>();
                        foreach (var itemLic in LicenciaMasivaTmp_2)
                        {
                            decimal Descuento = 0;
                            if (itemLic.Total.ToString().Contains('.'))
                            {
                                float numDecimal = float.Parse("0." + Convert.ToString(itemLic.Total).Split('.')[1]);
                                //string DecuentoString = "0." + numDecimal;
                                Descuento = Convert.ToDecimal(numDecimal);

                                if (Descuento < Convert.ToDecimal("0.5") && numDecimal != 0)
                                {
                                    itemLic.DescuentoRedondeo = Convert.ToDecimal(Descuento) + itemLic.DescuentoRedondeo;
                                    itemLic.Total = itemLic.Total - Convert.ToDecimal(Descuento);
                                }
                                else if (Descuento > Convert.ToDecimal("0.5") && numDecimal != 0)
                                {
                                    Descuento = Convert.ToDecimal(Descuento) - Convert.ToDecimal("0.5");
                                    itemLic.DescuentoRedondeo = Descuento + itemLic.DescuentoRedondeo;
                                    itemLic.Total = itemLic.Total - Descuento;
                                }
                            }
                            //resultLicencia = new BLLicencias().ActualizarMontoLirics(itemLic.codLicencia, itemLic.Total);
                            ListaMontoLirics.Add(new BELicencias { LIC_ID = itemLic.codLicencia, MONTO_LIRICS_BRUTO = itemLic.SubTotal,
                                MONTO_LIRICS_DCTO = itemLic.Descuento, MONTO_LIRICS_NETO = itemLic.Total , DESCUENTO_REDONDEO=itemLic.DescuentoRedondeo });
                        }
                        resultLicencia = new BLLicencias().ActualizarMontoLirics(ListaMontoLirics);
                    }


                    if (FacturaMasiva.ListarFactura != null)
                    {
                        facturacionMasiva = new List<DTOFactura>();
                        FacturaMasiva.ListarFactura.ForEach(s =>
                        {
                            bool validacionSinErrores = true;
                            decimal SubTotalAcumulado = 0;
                            decimal DescuentoAcumulado = 0;
                            decimal TotalAcumulado = 0;
                            decimal EstadoVisualizar = 0;
                            var lista = FacturaMasiva.ListarLicencia.Where(x => x.Nro == s.Nro).ToList();
                            foreach (var item in lista)
                            {
                                SubTotalAcumulado += item.SUBTOTAL;
                                DescuentoAcumulado += item.DESCUENTO;
                                TotalAcumulado += item.TOTAL;
                                if (item.STATE_CALC) EstadoVisualizar += 1;
                                //     if (!item.STATE_CALC_FACT_L)
                                //{
                                s.STATE_CALC_FACT_F = item.STATE_CALC_FACT_L;
                                s.OBSERVACION_CALC_FACT_F = item.OBSERVACION;

                                //   }

                            }
                            s.SUBTOTAL = SubTotalAcumulado;
                            s.DESCUENTO = DescuentoAcumulado;
                            s.TOTAL = TotalAcumulado;

                            validacionSinErrores = (lista.Count == EstadoVisualizar) ? true : false;

                            if (string.IsNullOrEmpty(s.DIRECCION))
                                validacionSinErrores = false;

                            facturacionMasiva.Add(new DTOFactura
                            {
                                Nro = s.Nro,
                                idBps = s.BPS_ID,
                                socio = s.SOCIO,
                                idMoneda = s.CUR_ALPHA,
                                moneda = s.MONEDA,
                                tipo_pago = s.TIPO_PAGO,
                                grupo_fact = s.GRUPO_FACT,
                                //estadoVisualizar = (lista.Count == EstadoVisualizar) ? true : false,
                                estadoVisualizar = validacionSinErrores,
                                subTotal = s.SUBTOTAL,
                                descuento = s.DESCUENTO,
                                total = s.TOTAL,
                                Direccion = s.DIRECCION,
                                Valida_Periodo_Fact = s.STATE_CALC_FACT_F,
                                observacion = s.OBSERVACION_CALC_FACT_F,
                            });
                        });
                        FacturacionMasivaTmp = facturacionMasiva;

                        if (LicenciaDetalleMasivaTmp.Count() > 0)
                        {
                            decimal tarifaId = LicenciaDetalleMasivaTmp[0].codigoTarifa;
                            var RATE_REDONDEO_ESP = new DATarifaTest().VALIDAR_REDONDEO_FACTURA(tarifaId);
                            if (RATE_REDONDEO_ESP == Variables.SI)
                            {
                                if (facturacionMasiva.Count > 0)
                                {
                                    decimal Descuento = 0;
                                    //decimal Total = 0;
                                    foreach (var i in facturacionMasiva)
                                    {
                                        if (i.total.ToString().Contains('.'))
                                        {
                                            float numDecimal = float.Parse("0." + Convert.ToString(i.total).Split('.')[1]);
                                            //string DecuentoString = "0." + numDecimal;
                                            Descuento = Convert.ToDecimal(numDecimal);

                                            if (Descuento < Convert.ToDecimal("0.5") && numDecimal != 0)
                                            {
                                                i.descuento = Convert.ToDecimal(Descuento) + i.descuento;
                                                i.total = i.total - Convert.ToDecimal(Descuento);
                                            }
                                            else if (Descuento > Convert.ToDecimal("0.5") && numDecimal != 0)
                                            {
                                                Descuento = Convert.ToDecimal(Descuento) - Convert.ToDecimal("0.5");
                                                i.descuento = Descuento + i.descuento;
                                                i.total = i.total - Descuento;
                                            }
                                        }

                                        var PrimeraLicencia = licenciaMasiva.Where(x => x.Nro == i.Nro).FirstOrDefault();
                                        PrimeraLicencia.Total = PrimeraLicencia.Total - Convert.ToDecimal(Descuento);
                                        PrimeraLicencia.Descuento = PrimeraLicencia.Descuento + Convert.ToDecimal(Descuento);

                                        var PrimerPeriodoLicencia = licenciaDetalleMasiva.Where(x => x.codigoLic == PrimeraLicencia.codLicencia).FirstOrDefault();
                                        PrimerPeriodoLicencia.Monto = PrimerPeriodoLicencia.Monto - Convert.ToDecimal(Descuento);
                                        PrimerPeriodoLicencia.Descuento = PrimerPeriodoLicencia.Descuento + Convert.ToDecimal(Descuento);
                                    }


                                }
                            }
                        }
                        




                    }

                    var cantRepErrores = FacturacionMasivaTmp.Where(x => x.estadoVisualizar == false).Count();
                    if (cantRepErrores == 0)
                        retorno.result = 1;
                    else
                        retorno.result = 2;
                    //retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(FacturaMasiva, JsonRequestBehavior.AllowGet);
                    retorno.Code = FacturaMasiva.ListarLicencia.Count();


                    var cabecerasCorrectas = FacturacionMasivaTmp.Where(x => x.estadoVisualizar == true);
                    var cabecerasErrores = FacturacionMasivaTmp.Where(x => x.estadoVisualizar == false);

                    var LicCorrectas = from CC in cabecerasCorrectas
                                       join L in LicenciaMasivaTmp on CC.Nro equals L.Nro
                                       select L;

                    var LicErrores = from CE in cabecerasErrores
                                     join L in LicenciaMasivaTmp on CE.Nro equals L.Nro
                                     select L;
                    string cantTotalLic = FacturaMasiva.ListarLicencia.Count().ToString();
                    string cantLicCorrectas = LicCorrectas.Count().ToString();
                    string cantLicErrores = LicErrores.Count().ToString();

                    retorno.message = "CANTIDAD DE LICENCIAS: " + cantTotalLic + "     |     " +
                                      " CORRECTAS: " + cantLicCorrectas + "     |     " +
                                      " ERRORES: " + cantLicErrores;


                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarFacturaMasivaSubGrilla", ex);
            }
            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult ListarFacturaMasivaCabecera(decimal estado = 1)
        {
            facturacionMasiva = FacturacionMasivaTmp;
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblFacturaMasiva' border=0 width='100%;' class='k-grid k-widget' id='tblFacturaMasiva'>");
                shtml.Append("<thead><tr>");

                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='display:none;' >Id</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='display:none;' >Identificador</th>");
                if (estado == 1)
                {
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='text-align:center;width:25px'>");
                    shtml.Append("<input type='checkbox' id='idCheck' name='Check' class='Check' onchange='clickCheck()'> </th>");
                }
                else
                {
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='text-align:center;width:25px'> </th>");
                }
                if (estado == 1)
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                else
                    shtml.Append("<th style='display:none;' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");

                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Socio</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Moneda</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Tipo de Pago</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Grupo Fact.</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:100px'>Sub Total</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:80px'>Descuento</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:100px'>Total</th>");
                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:55px'>Dirección</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  >Dirección</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  >OBSERVACION</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'   style='width:15px'>Modificar Datos</th>");

                if (facturacionMasiva != null)
                {
                    bool factEstado = estado == 1 ? true : false;
                    //foreach (var item in facturacionMasiva.Where(x => x.estadoVisualizar == factEstado).OrderBy(x => x.id))
                    foreach (var item in facturacionMasiva.Where(x => x.estadoVisualizar == factEstado).OrderBy(x => x.socio))
                    {
                        shtml.Append("<tr style='background-color:white'>");
                        shtml.AppendFormat("<td style='display:none;' class='IDCell'>{0}</td>", item.Nro);
                        shtml.AppendFormat("<td style='display:none;' class='IDENTIFICADORCell'>{0}</td>", "F");
                        if (estado == 1 && item.Valida_Periodo_Fact == true)
                            shtml.AppendFormat("<td style='text-align:center;width:25px'             ><input type='checkbox' id='{0}' name='Check' class='Check'/></td>", "chkFact" + item.Nro);
                        else
                            shtml.AppendFormat("<td style='text-align:center;width:25px'></td>");
                        //shtml.AppendFormat("<td style='text-align:center;width:25px'><input type='checkbox' id='{0}' name='Check' class='Check' disabled=true/></td>", "chkFact" + item.Nro);
                        //shtml.AppendFormat("<td style='display:none;text-align:center;width:25px'><input type='checkbox' id='{0}' /></td>", "chkFact" + item.Nro);

                        shtml.AppendFormat("<td style='width:25px'> ");
                        if (estado == 1)
                            shtml.AppendFormat("<a href=# onclick='verDetaFactura({0});'><img id='expand" + item.Nro + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.Nro);
                        else
                            shtml.AppendFormat("<a href=# onclick='verDetaFactura({0});'><img id='expand" + item.Nro + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.Nro);
                        //shtml.AppendFormat("<a href=# onclick='verDetaFactura({0});'><img id='expand" + item.Nro + "'  src='../Images/botones/less.png'  width=20px     title='Ocultar detalle.' alt='Ocultar detalle.' border=0></a>", item.Nro);
                        shtml.Append("</td>");

                        shtml.AppendFormat("<td nowrap>{0}</td>", item.socio);
                        shtml.AppendFormat("<td >{0}</td>", item.moneda);
                        shtml.AppendFormat("<td >{0}</td>", item.tipo_pago);
                        shtml.AppendFormat("<td >{0}</td>", item.grupo_fact);
                        shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.subTotal));
                        shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.descuento));
                        shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.total));
                        if (estado == 1)
                        {
                            shtml.AppendFormat("<td style='padding-left:5px'>{0}</td>", item.Direccion);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(item.Direccion))
                                shtml.AppendFormat("<td style='padding-left:5px; color:red'>{0}</td>", "Ingrese la dirección del socio y marque como principal.");
                            else
                                shtml.AppendFormat("<td style='padding-left:5px'>{0}</td>", item.Direccion);
                        }
                        if (estado == 1 && item.Valida_Periodo_Fact == false)
                        {
                            shtml.AppendFormat("<td >{0}</td>", item.observacion);
                        }
                        else
                        {
                            shtml.AppendFormat("<td ></td>");
                        }

                        shtml.AppendFormat("<td style='width:15px;text-align:center; '>  <a href=# onclick='abrirSocio({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.idBps);
                        shtml.Append("</tr>");

                        shtml.Append("<tr style='background-color:white'>");
                        shtml.Append("<td></td>");
                        if (estado == 1)
                            shtml.Append("<td></td>");
                        shtml.Append("<td colspan='20'>");

                        if (estado == 1)
                            shtml.Append("<div style='display:none;' id='" + "div" + item.Nro.ToString() + "'  > ");
                        else
                            shtml.Append("<div id='" + "div" + item.Nro.ToString() + "'  > ");



                        //shtml.Append(getHtmlTableDetaLicencia(item.Nro, estado));

                        shtml.Append("</div>");
                        shtml.Append("</td>");
                        shtml.Append("</tr>");
                    }
                }
                shtml.Append("</table>");
                retorno.message = shtml.ToString();
                retorno.result = 1;

                //if (GlobalVars.Global.RegMontoLirics == true)
                //{
                //    decimal resultLicencia = 0;
                //    List<BELicencias> ListaMontoLirics = new List<BELicencias>();
                //    foreach (var itemLic in LicenciaMasivaTmp_2)
                //    {
                //        //resultLicencia = new BLLicencias().ActualizarMontoLirics(itemLic.codLicencia, itemLic.Total);
                //        ListaMontoLirics.Add(new BELicencias { LIC_ID = itemLic.codLicencia, MONTO_LIRICS_BRUTO = itemLic.SubTotal, MONTO_LIRICS_DCTO = itemLic.Descuento, MONTO_LIRICS_NETO = itemLic.Total });
                //    }
                //    resultLicencia = new BLLicencias().ActualizarMontoLirics(ListaMontoLirics);
                //}

            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarFacturaMasivaCabecera", ex);
            }
            //return Json(retorno, JsonRequestBehavior.AllowGet);

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public StringBuilder getHtmlTableDetaLicencia(decimal sNro, decimal estado)
        {
            var licencias = LicenciaMasivaTmp.Where(p => p.Nro == sNro).ToList();
            StringBuilder shtml = new StringBuilder();
            shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
            shtml.Append("<thead>");

            shtml.Append("<tr>");
            shtml.Append("<th class='k-header' style='display:none'>Id Factura</th>");
            shtml.Append("<th class='k-header' style='width:40px;text-align:center;'>Id</th>");//Id Licencia
            shtml.Append("<th class='k-header' style='padding-left:10px'></th>");
            shtml.Append("<th class='k-header' style='width:400px;padding-left:10px'>Licencia</th>");
            shtml.Append("<th class='k-header' style='width:550px;padding-left:10px'>Modalidad</th>");
            shtml.Append("<th class='k-header' style='width:330px;padding-left:10px'>Establecimiento</th>");

            if (estado == 0)
                shtml.Append("<th class='k-header' style='width:155px;' >SubTotal</th>");
            else
                shtml.Append("<th class='k-header' style='width:130px;' >SubTotal</th>");
            if (estado == 0)
                shtml.Append("<th class='k-header' style='width:140px;'>Descuento</th>");
            else
                shtml.Append("<th class='k-header' style='width:130px;'>Descuento</th>");
            if (estado == 0)
                shtml.Append("<th class='k-header' style='width:160px;'>Total</th>");
            else
                shtml.Append("<th class='k-header' style='width:130px;'>Total</th>");

            if (licencias != null && licencias.Count > 0)
            {
                foreach (var item in licencias.OrderBy(x => x.codLicencia))
                {
                    shtml.Append("<tr style='background-color:white'>");
                    shtml.AppendFormat("<td style='width:120px;display:none'>{0}</td>", item.Nro);
                    shtml.AppendFormat("<td style='width:40px;text-align:center;'>{0}</td>", item.codLicencia);
                    shtml.AppendFormat("<td style='width:25px'> ");
                    if (estado == 1)
                        shtml.AppendFormat("<a href=# onclick='verDetaLic({0});'><img id='expandLic" + item.codLicencia + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.codLicencia);
                    else
                        shtml.AppendFormat("<a href=# onclick='verDetaLic({0});'><img id='expandLic" + item.codLicencia + "'  src='../Images/botones/less.png'  width=20px     title='Ocultar detalle.' alt='Ocultar detalle.' border=0></a>", item.codLicencia);
                    shtml.Append("</td>");
                    shtml.AppendFormat("<td style='width:400px'>{0}</td>", item.nombreLicencia);
                    shtml.AppendFormat("<td style='width:550px'>{0}</td>", item.Modalidad);
                    shtml.AppendFormat("<td style='width:330px'>{0}</td>", item.Establecimiento);
                    shtml.AppendFormat("<td style='width:130px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.SubTotal));
                    shtml.AppendFormat("<td style='width:80px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Descuento));
                    shtml.AppendFormat("<td style='width:130px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Total));
                    shtml.Append("</td>");
                    shtml.Append("</tr>");
                    shtml.Append("<tr style='background-color:white'>");

                    if (estado == 0)
                    {
                        shtml.Append("<td colspan='3'></td>");
                        shtml.Append("<td colspan='10'>");
                    }
                    else
                    {
                        shtml.Append("<td colspan='4'></td>");
                        shtml.Append("<td colspan='10'>");
                    }

                    if (estado == 1)
                        shtml.Append("<div style='display:none;' id='" + "divLic" + item.codLicencia.ToString() + "'  > ");

                    else
                        shtml.Append("<div id='" + "divLic" + item.codLicencia.ToString() + "'  > ");

                    //shtml.Append("<div style='display:none;' id='" + "divLic" + item.codLicencia.ToString() + "'  > ");
                    shtml.Append(getHtmlTableDetaLicPlan(item.codLicencia, estado));

                    shtml.Append("</div>");
                    shtml.Append("</td>");
                    shtml.Append("</tr>");

                }
            }
            shtml.Append("</table>");
            return shtml;
        }

        public StringBuilder getHtmlTableDetaLicPlan(decimal codLic, decimal estado)
        {
            var detalle = LicenciaDetalleMasivaTmp.Where(p => p.codigoLic == codLic).ToList();

            StringBuilder shtml = new StringBuilder();
            shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'");
            shtml.Append("<thead>");

            shtml.Append("<tr>");
            shtml.Append("<th class='k-header' style='display:none'>Id</th>");
            shtml.Append("<th class='k-header' style='display:none'>Id Licencia</th>");
            if (estado == 0)
                shtml.Append("<th class='k-header' style='width:420px;' >Observación</th>");
            if (estado == 0)
                shtml.Append("<th class='k-header' style='width:40px;text-align:center'>Tarifa</th>");
            else
                shtml.Append("<th class='k-header' style='width:120px;text-align:center'>Tarifa</th>");
            if (estado == 0)
                shtml.Append("<th class='k-header' style='width:70px;text-align:center'>Periodo</th>");
            else
                shtml.Append("<th class='k-header' style='width:120px;text-align:center'>Periodo</th>");
            shtml.Append("<th class='k-header' style='width:100px;'>SubMonto</th>");
            shtml.Append("<th class='k-header' style='width:100px;'>Descuento</th>");
            shtml.Append("<th class='k-header' style='width:100px;'>Monto</th>");

            if (detalle != null && detalle.Count > 0)
            {
                foreach (var item in detalle.OrderBy(x => x.fecha))
                {
                    shtml.Append("<tr style='background-color:white'>");
                    shtml.AppendFormat("<td style='display:none'>{0}</td>", item.codigoLP);
                    shtml.AppendFormat("<td style='display:none'>{0}</td>", item.codigoLic);
                    if (estado == 0)
                        shtml.AppendFormat("<td style='width:420px;'>{0}</td>", item.Observacion);
                    shtml.AppendFormat("<td style='text-align:center; width:40px'>{0}</td>", item.codigoTarifa);
                    shtml.AppendFormat("<td style='text-align:center; width:70px''>{0} - {1}</td>", item.anio.ToString(), item.mesNom);
                    shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.SubMonto));
                    shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Descuento));
                    shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Monto));
                    shtml.Append("</td>");
                    shtml.Append("</tr>");
                }
            }
            shtml.Append("</table>");
            return shtml;
        }

        public ActionResult mostrarDetalleFactura(decimal nro, decimal estado)
        {
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append(getHtmlTableDetaLicencia(nro, estado));
                retorno.result = 1;
                retorno.message = shtml.ToString();
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "mostrarDetalleFactura", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ObtenerFacturasSeleccionadas(List<BEFactura> ReglaValor)
        {
            Resultado retorno = new Resultado();
            try
            {
                List<BEFactura> ListaSelFactBorrador = new List<BEFactura>();
                BEFactura entidad = null;
                ReglaValor.ForEach(s =>
                {
                    decimal off_id = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                    var factura = FacturacionMasivaTmp.Where(p => p.Nro == s.Nro).FirstOrDefault();
                    var licencia = LicenciaMasivaTmp.Where(p => p.Nro == s.Nro);
                    var listaDetalle = LicenciaDetalleMasivaTmp.Where(p => p.Nro == s.Nro);

                    //Detalle
                    foreach (var detalle in listaDetalle)
                    {
                        entidad = new BEFactura();
                        entidad.OWNER = GlobalVars.Global.OWNER;
                        entidad.LOG_USER_CREAT = UsuarioActual;
                        entidad.Nro = s.Nro;
                        entidad.LIC_ID = detalle.codigoLic;
                        entidad.INV_DATE = DateTime.Now;
                        entidad.CUR_ALPHA = factura.idMoneda;
                        entidad.INV_DETAIL = LicenciaMasivaTmp.Where(p => p.Nro == s.Nro).ToList().Count > 1 ?
                                                Constantes.FacturaIndDet.VARIAS : Constantes.FacturaIndDet.UNA;
                        entidad.INV_PHASE = Constantes.FacturaFase.BORRADOR;
                        entidad.INV_REPRINTS = 0;
                        entidad.INV_COPIES = 0;
                        entidad.BPS_ID = factura.idBps;
                        entidad.LIC_PL_ID = detalle.codigoLP;
                        entidad.MOD_ID = detalle.codigoMod;
                        entidad.EST_ID = detalle.codigoEst;
                        entidad.LIC_PL_DATE = detalle.fecha;

                        entidad.SUBTOTAL = detalle.SubMonto;
                        entidad.DESCUENTO = detalle.Descuento;
                        entidad.MONTO_DET = detalle.Monto;
                        entidad.TAXV_VALUEP = detalle.valorImpuesto;
                        entidad.INV_TYPE = Constantes.FacturaTipo.FACTURA;
                        entidad.OFF_ID = off_id;
                        ListaSelFactBorrador.Add(entidad);
                    }
                });

                foreach (var fact in FacturacionMasivaTmp.OrderBy(x => x.Nro))
                {
                    var key = SGRDA.Utility.Util.NextInt();
                    var id = fact.Nro;
                    foreach (var detalle in ListaSelFactBorrador.Where(x => x.Nro == id).ToList())
                    {
                        detalle.INV_KEY = key;
                    }
                }

                var dato = new BLFactura().InsertarBorradorXML(ListaSelFactBorrador, GlobalVars.Global.OWNER);

                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO + " " + MSG_SUNAT;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerFacturasSeleccionadas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region FACTURACION BORRADOR
        public JsonResult ListarFacturacionBorrador(DateTime fini, DateTime ffin, decimal tipoLic, string idMoneda,
                                                    decimal idGrufact, decimal idBps, decimal idCorrelativo, string idTipoPago, int conFecha)
        {

            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    Session.Remove(K_SESION_BOR_FACTURACION_LISTA_TMP);
                    Session.Remove(K_SESION_BOR_FACTURACION_MASIVA);
                    Session.Remove(K_SESION_BOR_LICENCIA_MASIVA);
                    Session.Remove(K_SESION_BOR_LICENCIA_DETALLE_MASIVA);
                    decimal off_id = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                    var FacturaBorrador = new BLFactura().ListarFacturaBorrador(GlobalVars.Global.OWNER,
                                                        fini, ffin, tipoLic, idMoneda, idGrufact, idBps, idCorrelativo,
                                                        idTipoPago, conFecha, 0, 0, off_id);

                    if (FacturaBorrador.ListarDetalleFactura != null)
                    {
                        borlicenciaDetalleMasiva = new List<DTOFacturaDetallle>();
                        FacturaBorrador.ListarDetalleFactura.ForEach(s =>
                        {
                            borlicenciaDetalleMasiva.Add(new DTOFacturaDetallle
                            {
                                Id = s.INVL_ID,
                                codFactura = s.INV_ID,
                                codLicencia = s.LIC_ID,
                                codLicPlanificacion = s.LIC_PL_ID,
                                FechaPlanificacion = s.LIC_DATE,
                                //
                                valorBruto = s.INVL_GROSS,
                                valorDescuento = s.INVL_DISC,
                                valorBase = s.INVL_BASE,
                                valorImpuesto = s.INVL_TAXES,
                                valorNeto = s.INVL_NET,
                                //
                                rateId = s.RATE_ID,
                                codEstablecimiento = s.EST_ID

                            });

                        });

                        if (FacturaBorrador.ListarDescuentos != null)
                        {
                            borlicenciaDescuento = new List<DTODescuento>();
                            FacturaBorrador.ListarDescuentos.ForEach(s =>
                            {
                                borlicenciaDescuento.Add(new DTODescuento
                                {
                                    Id = s.DISC_ID,
                                    LicId = s.LIC_ID,
                                    Orden = s.ORDEN,
                                    DesOrigen = s.DISC_ORG,
                                    Tipo = s.DISC_PERC == 0 ? "V" : "P",
                                    Valor = s.DISC_PERC == 0 ? s.DISC_VALUE : s.DISC_PERC,
                                    Formato = s.DISC_SIGN,
                                    Cuenta = s.DISC_ACC
                                });
                            });
                            borLicenciaDescuentosTmp = borlicenciaDescuento;
                        }

                        if (FacturaBorrador.ListarLicencia != null)
                        {
                            borlicenciaMasiva = new List<DTOLicencia>();
                            FacturaBorrador.ListarLicencia.ForEach(s =>
                            {
                                borlicenciaMasiva.Add(new DTOLicencia
                                {
                                    codFactura = s.INV_ID,
                                    codLicencia = s.LIC_ID,
                                    nombreLicencia = s.LIC_NAME,
                                    Modalidad = s.Modalidad,
                                    Establecimiento = s.Establecimiento,
                                    //
                                    Monto = s.INVL_GROSS,
                                    Descuento = s.INVL_DISC,
                                    SubTotal = s.INVL_BASE,
                                    Impuesto = s.INVL_TAXES,
                                    Total = s.INVL_NET,
                                    //
                                });
                            });
                            BorLicenciaMasivaTmp = borlicenciaMasiva;
                        }

                        if (FacturaBorrador.ListarFactura != null)
                        {
                            borfacturacionMasiva = new List<DTOFactura>();

                            FacturaBorrador.ListarFactura.ForEach(s =>
                            {
                                borfacturacionMasiva.Add(new DTOFactura
                                {
                                    id = s.INV_ID,
                                    idMoneda = s.CUR_ALPHA,
                                    idTipoLic = s.LIC_TYPE,
                                    idGF = s.INVG_ID,
                                    idBps = s.BPS_ID,

                                    socio = s.SOCIO,
                                    fechaFact = s.INV_DATE,
                                    moneda = s.MONEDA,
                                    tipo_pago = s.TIPO_PAGO,
                                    grupo_fact = s.GRUPO_FACT,
                                    //
                                    valorBase = s.INV_BASE,
                                    valorDescuento = s.DESCUENTO,
                                    valoImpuesto = s.INV_TAXES,
                                    valorFinal = s.INV_NET,
                                    //
                                    idDireccionBps = s.ADD_ID,
                                    idTipoPago = s.PAY_ID,
                                });
                            });
                            BorFacturacionMasivaTmp = borfacturacionMasiva;
                        }
                        BorLicenciaDetalleMasivaTmp = borlicenciaDetalleMasiva;
                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(FacturaBorrador, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                    retorno.Code = FacturaBorrador.ListarFactura.Count;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarFacturacionBorrador", ex);
            }
            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

            //return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarFacturacionBorradorSerie(DateTime fini, DateTime ffin, decimal tipoLic, string idMoneda,
                                                    decimal idGrufact, decimal idBps, decimal idCorrelativo, string idTipoPago, int conFecha)
        {

            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    Session.Remove(K_SESION_BOR_FACTURACION_LISTA_TMP);
                    Session.Remove(K_SESION_BOR_FACTURACION_MASIVA);
                    Session.Remove(K_SESION_BOR_LICENCIA_MASIVA);
                    Session.Remove(K_SESION_BOR_LICENCIA_DETALLE_MASIVA);
                    decimal off_id = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                    var FacturaBorrador = new BLFactura().ListarFacturaBorradorSerie(GlobalVars.Global.OWNER,
                                                        fini, ffin, tipoLic, idMoneda, idGrufact, idBps, idCorrelativo,
                                                        idTipoPago, conFecha, 0, 0, off_id);

                    if (FacturaBorrador.ListarDetalleFactura != null)
                    {
                        borlicenciaDetalleMasiva = new List<DTOFacturaDetallle>();
                        FacturaBorrador.ListarDetalleFactura.ForEach(s =>
                        {
                            borlicenciaDetalleMasiva.Add(new DTOFacturaDetallle
                            {
                                Id = s.INVL_ID,
                                codFactura = s.INV_ID,
                                codLicencia = s.LIC_ID,
                                codLicPlanificacion = s.LIC_PL_ID,
                                FechaPlanificacion = s.LIC_DATE,
                                //
                                valorBruto = s.INVL_GROSS,
                                valorDescuento = s.INVL_DISC,
                                valorBase = s.INVL_BASE,
                                valorImpuesto = s.INVL_TAXES,
                                valorNeto = s.INVL_NET,
                                //
                                rateId = s.RATE_ID,
                                codEstablecimiento = s.EST_ID

                            });

                        });

                        if (FacturaBorrador.ListarDescuentos != null)
                        {
                            borlicenciaDescuento = new List<DTODescuento>();
                            FacturaBorrador.ListarDescuentos.ForEach(s =>
                            {
                                borlicenciaDescuento.Add(new DTODescuento
                                {
                                    Id = s.DISC_ID,
                                    LicId = s.LIC_ID,
                                    Orden = s.ORDEN,
                                    DesOrigen = s.DISC_ORG,
                                    Tipo = s.DISC_PERC == 0 ? "V" : "P",
                                    Valor = s.DISC_PERC == 0 ? s.DISC_VALUE : s.DISC_PERC,
                                    Formato = s.DISC_SIGN,
                                    Cuenta = s.DISC_ACC
                                });
                            });
                            borLicenciaDescuentosTmp = borlicenciaDescuento;
                        }

                        if (FacturaBorrador.ListarLicencia != null)
                        {
                            borlicenciaMasiva = new List<DTOLicencia>();
                            FacturaBorrador.ListarLicencia.ForEach(s =>
                            {
                                borlicenciaMasiva.Add(new DTOLicencia
                                {
                                    codFactura = s.INV_ID,
                                    codLicencia = s.LIC_ID,
                                    nombreLicencia = s.LIC_NAME,
                                    Modalidad = s.Modalidad,
                                    Establecimiento = s.Establecimiento,
                                    //
                                    Monto = s.INVL_GROSS,
                                    Descuento = s.INVL_DISC,
                                    SubTotal = s.INVL_BASE,
                                    Impuesto = s.INVL_TAXES,
                                    Total = s.INVL_NET,
                                    //
                                });
                            });
                            BorLicenciaMasivaTmp = borlicenciaMasiva;
                        }

                        if (FacturaBorrador.ListarFactura != null)
                        {
                            borfacturacionMasiva = new List<DTOFactura>();

                            FacturaBorrador.ListarFactura.ForEach(s =>
                            {
                                borfacturacionMasiva.Add(new DTOFactura
                                {
                                    id = s.INV_ID,
                                    idMoneda = s.CUR_ALPHA,
                                    idTipoLic = s.LIC_TYPE,
                                    idGF = s.INVG_ID,
                                    idBps = s.BPS_ID,

                                    socio = s.SOCIO,
                                    fechaFact = s.INV_DATE,
                                    moneda = s.MONEDA,
                                    tipo_pago = s.TIPO_PAGO,
                                    grupo_fact = s.GRUPO_FACT,
                                    //
                                    valorBase = s.INV_BASE,
                                    valorDescuento = s.DESCUENTO,
                                    valoImpuesto = s.INV_TAXES,
                                    valorFinal = s.INV_NET,
                                    //
                                    idDireccionBps = s.ADD_ID,
                                    idTipoPago = s.PAY_ID,
                                });
                            });
                            BorFacturacionMasivaTmp = borfacturacionMasiva;
                        }
                        BorLicenciaDetalleMasivaTmp = borlicenciaDetalleMasiva;
                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(FacturaBorrador, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                    retorno.Code = FacturaBorrador.ListarFactura.Count;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarFacturacionBorrador", ex);
            }
            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

            //return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarBorradorFactMasivaCabecera(decimal idCaracteristica = 0)
        {
            borfacturacionMasiva = BorFacturacionMasivaTmp;
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblFacturaMasiva' border=0 width='100%;' class='k-grid k-widget' id='tblFacturaMasiva'>");
                shtml.Append("<thead><tr>");

                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='display:none;' >Identificador</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='text-align:center;width:25px'>");
                shtml.Append("<input type='checkbox' id='idCheck' name='Check' class='Check' onchange='clickCheck()'> </th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Id</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Socio</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Fecha</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Moneda</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Tipo de Pago</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Grupo Fact.</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:58px'>Sub Total</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:35px'>Descuento</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:35px'>Impuesto</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:58px'>Total</th>");

                if (borfacturacionMasiva != null)
                {
                    foreach (var item in borfacturacionMasiva.OrderByDescending(x => x.id))
                    {
                        shtml.Append("<tr style='background-color:white'>");
                        shtml.AppendFormat("<td style='display:none;' class='IDENTIFICADORCell'>{0}</td>", "F");
                        shtml.AppendFormat("<td style='text-align:center;width:25px'><input type='checkbox' id='{0}' name='Check' class='Check' /></td>", "chkFact" + item.id);

                        shtml.AppendFormat("<td style='width:25px'> ");
                        shtml.AppendFormat("<a href=# onclick='verDetaFactura({0});'><img id='expand" + item.id + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.id);
                        shtml.Append("</td>");
                        shtml.AppendFormat("<td class='IDCell'>{0}</td>", item.id);

                        shtml.AppendFormat("<td nowrap>{0}</td>", item.socio);
                        shtml.AppendFormat("<td >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.fechaFact));
                        shtml.AppendFormat("<td >{0}</td>", item.moneda);
                        shtml.AppendFormat("<td >{0}</td>", item.tipo_pago);
                        shtml.AppendFormat("<td >{0}</td>", item.grupo_fact);
                        shtml.AppendFormat("<td style='width:58px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorBase));
                        shtml.AppendFormat("<td style='width:35px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorDescuento));
                        shtml.AppendFormat("<td style='width:35px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valoImpuesto));
                        shtml.AppendFormat("<td style='width:58px;text-align:right;  padding-right:15px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorFinal));
                        //shtml.AppendFormat("<td style='text-align:center'>");
                        //shtml.AppendFormat("<a href=# onclick='eliminarFactura({0});'><img src='../Images/iconos/delete.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.id, "Anular Factura");
                        //shtml.AppendFormat("</td>");

                        shtml.Append("</tr>");

                        shtml.Append("<tr style='background-color:white'>");
                        shtml.Append("<td></td>");
                        shtml.Append("<td></td>");
                        shtml.Append("<td style='width:100%' colspan='10'>");

                        shtml.Append("<div style='display:none;' id='" + "div" + item.id.ToString() + "'  > ");
                        //shtml.Append(getHtmlTableDetaLicenciaBorrador(item.id));

                        shtml.Append("</div>");
                        shtml.Append("</td>");
                        shtml.Append("</tr>");
                    }
                }
                shtml.Append("</table>");
                retorno.message = shtml.ToString();
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarBorradorFactMasivaCabecera", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public StringBuilder getHtmlTableDetaLicenciaBorrador(decimal codFact)
        {
            var licencias = BorLicenciaMasivaTmp.Where(p => p.codFactura == codFact).ToList();

            StringBuilder shtml = new StringBuilder();
            shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
            shtml.Append("<thead>");

            shtml.Append("<tr>");
            shtml.Append("<th class='k-header' style='width:120px;display:none'>Id Factura</th>");
            shtml.Append("<th class='k-header' style='width:120px;display:none'>Id Licencia</th>");
            shtml.Append("<th class='k-header' style='width: 25px;padding-left:10px'></th>");
            shtml.Append("<th class='k-header' style='width:150px'>Licencia</th>");
            shtml.Append("<th class='k-header' style='width:350px'>Modalidad</th>");
            shtml.Append("<th class='k-header' style='width:350px'>Establecimiento</th>");
            shtml.Append("<th class='k-header' style='width:110px'>Monto</th>");
            shtml.Append("<th class='k-header' style='width:85px'>Descuento</th>");
            shtml.Append("<th class='k-header' style='width:85px'>Impuesto</th>");
            shtml.Append("<th class='k-header' style='width:115px'>Total</th>");

            if (licencias != null && licencias.Count > 0)
            {
                foreach (var item in licencias.OrderBy(x => x.codLicencia))
                {
                    shtml.Append("<tr style='background-color:white'>");
                    shtml.AppendFormat("<td style='width:120px;display:none'>{0}</td>", item.codFactura);
                    shtml.AppendFormat("<td style='width:120px;display:none'>{0}</td>", item.codLicencia);
                    shtml.AppendFormat("<td style='width:25px'> ");
                    shtml.AppendFormat("<a href=# onclick='verDetaLic({0},{1});'><img id='expandLic" + item.codFactura + "-" + item.codLicencia + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.codFactura.ToString(), item.codLicencia.ToString());
                    shtml.Append("</td>");
                    shtml.AppendFormat("<td style='width:350px'>{0}</td>", item.nombreLicencia);
                    shtml.AppendFormat("<td style='width:350px'>{0}</td>", item.Modalidad);
                    shtml.AppendFormat("<td style='width:330px'>{0}</td>", item.Establecimiento);
                    shtml.AppendFormat("<td style='width:110px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Monto));
                    shtml.AppendFormat("<td style='width:85px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Descuento));
                    shtml.AppendFormat("<td style='width:85px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Impuesto));
                    shtml.AppendFormat("<td style='width:115px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Total));
                    shtml.Append("</td>");
                    shtml.Append("</tr>");

                    shtml.Append("<tr style='background-color:white'>");
                    shtml.Append("<td></td>");
                    shtml.Append("<td></td>");
                    shtml.Append("<td></td>");
                    shtml.Append("<td colspan='6'>");
                    shtml.Append("<div style='display:none;' id='" + "divLic" + item.codFactura.ToString() + "-" + item.codLicencia.ToString() + "'  > ");
                    shtml.Append(getHtmlTableDetaLicPlanBorrador(item.codLicencia, item.codFactura));

                    shtml.Append("</div>");
                    shtml.Append("</td>");
                    shtml.Append("</tr>");
                }
            }
            shtml.Append("</table>");
            return shtml;
        }

        public StringBuilder getHtmlTableDetaLicPlanBorrador(decimal codLic, decimal codFact)
        {
            var detalle = BorLicenciaDetalleMasivaTmp.Where(p => p.codLicencia == codLic && p.codFactura == codFact).ToList();
            StringBuilder shtml = new StringBuilder();
            shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
            shtml.Append("<thead>");

            shtml.Append("<tr>");
            shtml.Append("<th class='k-header' style='display:none'>Id</th>");
            shtml.Append("<th class='k-header' style='display:none'>Id Licencia</th>");
            shtml.Append("<th class='k-header' style='text-align:center'>Fecha</th>");
            shtml.Append("<th class='k-header' >Monto Bruto</th>");
            shtml.Append("<th class='k-header' >Descuento</th>");
            shtml.Append("<th class='k-header' >Impuesto</th>");
            shtml.Append("<th class='k-header' >Sub Total</th>");

            if (detalle != null && detalle.Count > 0)
            {
                foreach (var item in detalle.OrderBy(x => x.FechaPlanificacion))
                {
                    shtml.Append("<tr style='background-color:white'>");
                    shtml.AppendFormat("<td style='display:none'>{0}</td>", item.codFactura);
                    shtml.AppendFormat("<td style='display:none'>{0}</td>", item.codLicencia);
                    shtml.AppendFormat("<td style='text-align:center'>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FechaPlanificacion));
                    shtml.AppendFormat("<td style='width:85px;text-align:right;  padding-right:10px'>S/.  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorBruto));
                    shtml.AppendFormat("<td style='width:78px;text-align:right;  padding-right:10px'>S/.  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorDescuento));
                    shtml.AppendFormat("<td style='width:70px;text-align:right;  padding-right:10px'>S/.  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorImpuesto));
                    shtml.AppendFormat("<td style='width:90px;text-align:right;  padding-right:10px'>S/.  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorNeto));
                    shtml.Append("</td>");
                    shtml.Append("</tr>");
                }
            }
            shtml.Append("</table>");
            return shtml;
        }

        public ActionResult mostrarDetalleFacturaBorrador(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append(getHtmlTableDetaLicenciaBorrador(id));
                retorno.result = 1;
                retorno.message = shtml.ToString();
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "mostrarDetalleFacturaBorrador", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ObtenerFacturasBorradorSeleccionadas(List<BEFactura> ReglaValor)
        {
            Resultado retorno = new Resultado();
            var detalleReporteV = string.Empty;
            try
            #region Nuevo
            {
                List<BEFactura> bListafacturasSel = new List<BEFactura>();
                BEFactura bfactura = new BEFactura();
                int contador = 0;
                foreach (var item in ReglaValor)
                {
                    bfactura = new BEFactura();
                    string[] separador = item.C.Split('#');
                    bfactura.INV_ID = Convert.ToDecimal(separador[0]);
                    bfactura.INV_NMR = Convert.ToDecimal(separador[1]);
                    detalleReporteV = separador[2];
                    bfactura.INV_NUMBER = contador;
                    contador += 1;
                    bListafacturasSel.Add(bfactura);
                }

                List<BEFactura> ListaSelCabFactBorrador = new List<BEFactura>();
                List<BEFacturaDetalle> ListaDetFactBorrador = new List<BEFacturaDetalle>();
                List<BEFacturaDescuento> ListaDescuentoXdetalle = new List<BEFacturaDescuento>();
                List<BELicenciaPlaneamiento> ListaPlanificacion = new List<BELicenciaPlaneamiento>();
                List<BELicenciaPlaneamientoDetalle> ListaDetallePlanificacion = new List<BELicenciaPlaneamientoDetalle>();

                BEFactura entCabFacBorrador = null;
                BEFacturaDetalle detEntidad = null;
                BEFacturaDescuento DescuentoXdetalle = null;
                BELicenciaPlaneamientoDetalle entDetallePlanificacion = null;
                BELicenciaPlaneamiento entPlanificacion = null;

                int TotalNoEmitidas = 0;
                int TotalEmitidas = 0;

                //Se obtiene el último correlativo
                decimal off_id = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);

                //Correlativo = new BLFactura().ObtenerCorrelativo(bfactura.INV_NMR).FirstOrDefault().INV_NUMBER;

                #region  CAMBIAR ESTADO PERIODOS A FACTURANDOSE
                List<BELicenciaPlaneamiento> ListaPlaneamientoActualizar = new List<BELicenciaPlaneamiento>();
                BorLicenciaDetalleMasivaTmp.ForEach(S =>
                {
                    ListaPlaneamientoActualizar.Add(new BELicenciaPlaneamiento
                    {
                        LIC_PL_ID = S.codLicPlanificacion
                    });

                });
                new BLFactura().ActualizarPeriodosFacturandose(GlobalVars.Global.OWNER, ListaPlaneamientoActualizar, 1); //1 PONER EN FACTURANDO
                #endregion


                bListafacturasSel.ForEach(s =>
                {
                    entCabFacBorrador = new BEFactura();

                    var factura = BorFacturacionMasivaTmp.Where(p => p.id == s.INV_ID).FirstOrDefault();
                    var licencia = BorLicenciaMasivaTmp.Where(p => p.codFactura == s.INV_ID);
                    var listaDetalle = BorLicenciaDetalleMasivaTmp.Where(p => p.codFactura == s.INV_ID);

                    entCabFacBorrador.OWNER = GlobalVars.Global.OWNER;
                    entCabFacBorrador.INV_ID = factura.id;
                    entCabFacBorrador.INV_NMR = s.INV_NMR;
                    entCabFacBorrador.INV_NUMBER = s.INV_NUMBER; //s.INV_NUMBER;
                    entCabFacBorrador.INV_TYPE = Constantes.FacturaTipo.FACTURA;
                    entCabFacBorrador.INV_PHASE = Constantes.FacturaFase.DEFINITIVA;
                    entCabFacBorrador.ADD_ID = factura.idDireccionBps;
                    entCabFacBorrador.LOG_USER_UPDATE = UsuarioActual;

                    entCabFacBorrador.INV_BASE = factura.valorBase;
                    entCabFacBorrador.INV_TAXES = factura.valoImpuesto;
                    entCabFacBorrador.INV_NET = factura.valorFinal;
                    entCabFacBorrador.INV_BALANCE = factura.valorFinal;
                    entCabFacBorrador.PAY_ID = factura.idTipoPago;
                    entCabFacBorrador.INV_MANUAL = false;
                    entCabFacBorrador.INV_DATE = FechaSistema;
                    entCabFacBorrador.OFF_ID = off_id;
                    if (!string.IsNullOrEmpty(detalleReporteV))
                    {
                        entCabFacBorrador.INV_REPORT_STATUS = true;
                        entCabFacBorrador.INV_REPORT_DETAILS = detalleReporteV.ToUpper();
                    }
                    ListaSelCabFactBorrador.Add(entCabFacBorrador);

                    #region ENVIO A LA SUITE

                    //if (GlobalVars.Global.FE == true)
                    //{                      
                    //    // RECORRE LOS DESCUENTOS
                    //    foreach (var item in listaDetalle)
                    //    {
                    //        detEntidad = new BEFacturaDetalle();
                    //        detEntidad.OWNER = GlobalVars.Global.OWNER;
                    //        detEntidad.INVL_ID = item.Id;
                    //        detEntidad.INV_ID = item.codFactura;
                    //        detEntidad.LIC_ID = item.codLicencia;
                    //        detEntidad.LIC_PL_ID = item.codLicPlanificacion;
                    //        detEntidad.EST_ID = item.codEstablecimiento;
                    //        detEntidad.ADD_ID = factura.idDireccionBps;
                    //        detEntidad.LIC_DATE = item.FechaPlanificacion;

                    //        detEntidad.INVL_GROSS = item.valorBruto;
                    //        detEntidad.INVL_DISC = item.valorDescuento;
                    //        detEntidad.INVL_BASE = item.valorBase;
                    //        detEntidad.INVL_TAXES = item.valorImpuesto;
                    //        detEntidad.INVL_NET = item.valorNeto;
                    //        detEntidad.INVL_BALANCE = item.valorNeto;

                    //        detEntidad.LOG_USER_UPDATE = UsuarioActual;
                    //        detEntidad.RATE_ID = item.rateId;
                    //        detEntidad.LIC_PL_STATUS = Constantes.EstadoPeriodo.TOTAL;

                    //        ListaDetFactBorrador.Add(detEntidad);

                    //        //PANIFICACION                          
                    //        entPlanificacion = new BELicenciaPlaneamiento();
                    //        entPlanificacion.OWNER = GlobalVars.Global.OWNER;
                    //        entPlanificacion.LIC_PL_ID = item.codLicPlanificacion;
                    //        entPlanificacion.LIC_PL_AMOUNT = item.valorNeto;
                    //        entPlanificacion.LIC_PL_STATUS = Constantes.EstadoPeriodo.TOTAL;
                    //        entPlanificacion.LOG_USER_UPDATE = UsuarioActual;

                    //        ListaPlanificacion.Add(entPlanificacion);

                    //        //PLANIFICACION DETALLE                    
                    //        entDetallePlanificacion = new BELicenciaPlaneamientoDetalle();
                    //        entDetallePlanificacion.OWNER = GlobalVars.Global.OWNER;
                    //        entDetallePlanificacion.LIC_PL_ID = item.codLicPlanificacion;
                    //        entDetallePlanificacion.INV_ID = item.codFactura;
                    //        entDetallePlanificacion.LIC_INVOICE_VAL = item.valorNeto;
                    //        entDetallePlanificacion.LIC_INVOICE_LINE = item.valorNeto;
                    //        entDetallePlanificacion.LOG_USER_CREAT = UsuarioActual;
                    //        entDetallePlanificacion.LIC_PL_PARTIAL = true;

                    //        ListaDetallePlanificacion.Add(entDetallePlanificacion);


                    //        #region DESCUENTOS
                    //        decimal resultBase = item.valorBruto;
                    //        decimal resultNeto = resultBase;
                    //        decimal valorDescuento = 0;
                    //        var listaDescuentos = borLicenciaDescuentosTmp.Where(p => p.LicId == item.codLicencia);

                    //        #region DESCUENTOS PLANTILLA ASIGNAR
                    //        //***************que recupera de la tarifa (sirve para no insertar valores erroneos en la BD)
                    //        List<DTODescuentoPlantilla> listaDescuentosPlant = new List<DTODescuentoPlantilla>();
                    //        if (borLicenciaDescuentosPlantillaTmp != null && borLicenciaDescuentosPlantillaTmp.Count > 0)
                    //            listaDescuentosPlant = borLicenciaDescuentosPlantillaTmp.Where(p => p.LicId == item.codLicencia).ToList();
                    //        foreach (var x in listaDescuentos)
                    //        {
                    //            foreach (var y in listaDescuentosPlant)
                    //            {
                    //                if ((x.Id == y.Id))
                    //                    x.Valor = y.Valor;
                    //                if (y.TEMP_ID_DSC > 0)
                    //                    x.Tipo = y.Tipo;
                    //            }
                    //        }

                    //        //************************
                    //        #endregion

                    //        foreach (var descuento in listaDescuentos.OrderByDescending(p => p.Valor))
                    //        {
                    //            var valormultiplicar = 0;
                    //            if (descuento.Valor >= 1)//si el desc es 5 convertir a 0,05 si ya es 0,05 multiplicar por el mismo valor
                    //                valormultiplicar = 100;
                    //            else
                    //                valormultiplicar = 1;

                    //            if (descuento.DesOrigen == "B")
                    //            {
                    //                if (descuento.Tipo == "V")
                    //                {
                    //                    valorDescuento = descuento.Valor;
                    //                    resultNeto -= descuento.Valor;
                    //                }
                    //                else
                    //                {
                    //                    valorDescuento = (resultBase * (descuento.Valor / valormultiplicar));
                    //                    resultNeto -= (resultBase * (descuento.Valor / valormultiplicar));
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (descuento.Tipo == "V")
                    //                {
                    //                    valorDescuento = descuento.Valor;
                    //                    resultNeto -= descuento.Valor;
                    //                }
                    //                else
                    //                {
                    //                    valorDescuento = (resultNeto * (descuento.Valor / valormultiplicar));
                    //                    resultNeto -= (resultNeto * (descuento.Valor / valormultiplicar));
                    //                }
                    //            }
                    //            DescuentoXdetalle = new BEFacturaDescuento();
                    //            DescuentoXdetalle.OWNER = GlobalVars.Global.OWNER;
                    //            DescuentoXdetalle.INV_ID = item.codFactura;
                    //            DescuentoXdetalle.INVL_ID = item.Id;
                    //            DescuentoXdetalle.DISC_ID = descuento.Id;
                    //            DescuentoXdetalle.DISC_SIGN = descuento.Formato;
                    //            DescuentoXdetalle.DISC_VALUE = valorDescuento;
                    //            DescuentoXdetalle.DISC_ACC = descuento.Cuenta;
                    //            DescuentoXdetalle.LOG_USER_CREAT = UsuarioActual;

                    //            ////Agrego los Descuentos
                    //            //var Desc = new BLFacturaDetalle().ActualizarDetalleDescuento(DescuentoXdetalle);
                    //            ListaDescuentoXdetalle.Add(DescuentoXdetalle);
                    //        }
                    //        #endregion
                    //    }

                    //    new BLFactura().InsertaDescuento(GlobalVars.Global.OWNER, ListaDescuentoXdetalle);

                    //    //ENVIO A SUNAT
                    //    MSG_SUNAT = FE.EnvioComprobanteElectronicoMasivo(entCabFacBorrador.INV_ID, entCabFacBorrador.INV_NMR, entCabFacBorrador.ADD_ID);




                    //    if (MSG_SUNAT != Constantes.Mensaje_Sunat.MSG_ERROR)
                    //    {
                    //        //OBTENGO EL CORRELATIVO
                    //        var Correlativo = new BLFactura().ObtenerCorrelativo(entCabFacBorrador.INV_NMR);

                    //        entCabFacBorrador.INV_NUMBER = Correlativo.FirstOrDefault().INV_NUMBER;

                    //        //Actualizo la Cabecera de la Factura
                    //        var fac = new BLFactura().ActualizarFacturaCabecera(entCabFacBorrador);

                    //        new BLFactura().InsertarDetalleFacturaxml(GlobalVars.Global.OWNER, ListaDetFactBorrador);
                    //        new BLFactura().InsertarPlanificacionxml(GlobalVars.Global.OWNER, ListaPlanificacion);
                    //        new BLFactura().ActualizaPlanificacionDetallexml(GlobalVars.Global.OWNER, ListaDetallePlanificacion);
                    //        new BLLicenciaReporte().InsertarPlanillaXML(ListaDetallePlanificacion);  //insertando planilla a los periodos seleccionados

                    //        ListaDetFactBorrador.Clear();
                    //        ListaPlanificacion.Clear();
                    //        ListaDetallePlanificacion.Clear();

                    //        TotalEmitidas += 1;
                    //    }

                    // //   if (MSG_SUNAT != Constantes.Mensaje_Sunat.MSG_DOK_SUNAT && MSG_SUNAT != Constantes.Mensaje_Sunat.MSG_EXISTE_SUNAT)
                    // //   {

                    // //       FE.ConsultaEstado(entCabFacBorrador.INV_ID); // consultar 

                    //        //si esta en sunat o se sigue procesando (actualizar dok)

                    //        // ActualizarEstadoSunat(idfactura, obj_ResultadoMensaje.Codigo, obj_ResultadoMensaje.Mensajes);
                    //    //}
                    //    else
                    //    {
                    //        TotalNoEmitidas += 1;

                    //    }
                    //}
                    #endregion

                    #region SIN ENVIO A LA SUITE
                    //       else
                    //       {
                    foreach (var item in listaDetalle)
                    {
                        detEntidad = new BEFacturaDetalle();
                        detEntidad.OWNER = GlobalVars.Global.OWNER;
                        detEntidad.INVL_ID = item.Id;
                        detEntidad.INV_ID = item.codFactura;
                        detEntidad.LIC_ID = item.codLicencia;
                        detEntidad.LIC_PL_ID = item.codLicPlanificacion;
                        detEntidad.EST_ID = item.codEstablecimiento;
                        detEntidad.ADD_ID = factura.idDireccionBps;
                        detEntidad.LIC_DATE = item.FechaPlanificacion;

                        detEntidad.INVL_GROSS = item.valorBruto;
                        detEntidad.INVL_DISC = item.valorDescuento;
                        detEntidad.INVL_BASE = item.valorBase;
                        detEntidad.INVL_TAXES = item.valorImpuesto;
                        detEntidad.INVL_NET = item.valorNeto;
                        detEntidad.INVL_BALANCE = item.valorNeto;

                        detEntidad.LOG_USER_UPDATE = UsuarioActual;
                        detEntidad.RATE_ID = item.rateId;
                        detEntidad.LIC_PL_STATUS = Constantes.EstadoPeriodo.TOTAL;
                        ListaDetFactBorrador.Add(detEntidad);

                        //PANIFICACION                          
                        entPlanificacion = new BELicenciaPlaneamiento();
                        entPlanificacion.OWNER = GlobalVars.Global.OWNER;
                        entPlanificacion.LIC_PL_ID = item.codLicPlanificacion;
                        entPlanificacion.LIC_PL_AMOUNT = item.valorNeto;
                        entPlanificacion.LIC_PL_STATUS = Constantes.EstadoPeriodo.TOTAL;
                        ListaPlanificacion.Add(entPlanificacion);

                        //PLANIFICACION DETALLE                    
                        entPlanificacion = new BELicenciaPlaneamiento();
                        entDetallePlanificacion = new BELicenciaPlaneamientoDetalle();
                        entDetallePlanificacion.OWNER = GlobalVars.Global.OWNER;
                        entDetallePlanificacion.LIC_PL_ID = item.codLicPlanificacion;
                        entDetallePlanificacion.INV_ID = item.codFactura;
                        entDetallePlanificacion.LIC_INVOICE_VAL = item.valorNeto;
                        entDetallePlanificacion.LIC_INVOICE_LINE = item.valorNeto;
                        entDetallePlanificacion.LOG_USER_CREAT = UsuarioActual;
                        entDetallePlanificacion.LIC_PL_PARTIAL = true;
                        ListaDetallePlanificacion.Add(entDetallePlanificacion);


                        #region DESCUENTOS
                        decimal resultBase = item.valorBruto;
                        decimal resultNeto = resultBase;
                        decimal valorDescuento = 0;
                        var listaDescuentos = borLicenciaDescuentosTmp.Where(p => p.LicId == item.codLicencia);
                        #region DESCUENTOS PLANTILLA ASIGNAR
                        //***************que recupera de la tarifa (sirve para no insertar valores erroneos en la BD)
                        List<DTODescuentoPlantilla> listaDescuentosPlant = new List<DTODescuentoPlantilla>();
                        if (borLicenciaDescuentosPlantillaTmp != null && borLicenciaDescuentosPlantillaTmp.Count > 0)
                            listaDescuentosPlant = borLicenciaDescuentosPlantillaTmp.Where(p => p.LicId == item.codLicencia).ToList();
                        foreach (var x in listaDescuentos)
                        {
                            foreach (var y in listaDescuentosPlant)
                            {
                                if ((x.Id == y.Id))
                                    x.Valor = y.Valor;
                                if (y.TEMP_ID_DSC > 0)
                                    x.Tipo = y.Tipo;
                            }
                        }

                        //************************
                        #endregion
                        foreach (var descuento in listaDescuentos.OrderByDescending(p => p.Valor))
                        {
                            var valormultiplicar = 0;
                            if (descuento.Valor >= 1)//si el desc es 5 convertir a 0,05 si ya es 0,05 multiplicar por el mismo valor
                                valormultiplicar = 100;
                            else
                                valormultiplicar = 1;

                            if (descuento.DesOrigen == "B")
                            {
                                if (descuento.Tipo == "V")
                                {
                                    valorDescuento = descuento.Valor;
                                    resultNeto -= descuento.Valor;
                                }
                                else
                                {
                                    valorDescuento = (resultBase * (descuento.Valor / valormultiplicar));
                                    resultNeto -= (resultBase * (descuento.Valor / valormultiplicar));
                                }
                            }
                            else
                            {
                                if (descuento.Tipo == "V")
                                {
                                    valorDescuento = descuento.Valor;
                                    resultNeto -= descuento.Valor;
                                }
                                else
                                {
                                    valorDescuento = (resultNeto * (descuento.Valor / valormultiplicar));
                                    resultNeto -= (resultNeto * (descuento.Valor / valormultiplicar));
                                }
                            }
                            DescuentoXdetalle = new BEFacturaDescuento();
                            DescuentoXdetalle.OWNER = GlobalVars.Global.OWNER;
                            DescuentoXdetalle.INV_ID = item.codFactura;
                            DescuentoXdetalle.INVL_ID = item.Id;
                            DescuentoXdetalle.DISC_ID = descuento.Id;
                            DescuentoXdetalle.DISC_SIGN = descuento.Formato;
                            DescuentoXdetalle.DISC_VALUE = valorDescuento;
                            DescuentoXdetalle.DISC_ACC = descuento.Cuenta;
                            DescuentoXdetalle.LOG_USER_CREAT = UsuarioActual;
                            ListaDescuentoXdetalle.Add(DescuentoXdetalle);
                        }
                        #endregion
                    }
                    //}
                    #endregion

                });

                //var listaplaneamiento = ListaPlaneamientoActualizar.Where(x=>x.LIC_PL_ID==)

                new BLFactura().ActualizarPeriodosFacturandose(GlobalVars.Global.OWNER, ListaPlaneamientoActualizar, 0); //0 PONER EN TERMIADO/FACTURADO


                var dato = new BLFactura().ActualizarEstadoDefinitivaXML(GlobalVars.Global.OWNER,
                                                     ListaSelCabFactBorrador, ListaDetFactBorrador,
                                                     ListaPlanificacion, ListaDetallePlanificacion,
                                                     ListaDescuentoXdetalle);

                new BLLicenciaReporte().InsertarPlanillaXML(ListaDetallePlanificacion);//insertando planilla a los periodos seleccionados

                if (GlobalVars.Global.FE == true)
                {

                    var vExtras = new BLExtras().ListarExtras(GlobalVars.Global.OWNER, 0);
                    List<BECabeceraFactura> vCabecera = new BLFactura().ListaCabezeraMasivaSunat(GlobalVars.Global.OWNER, ListaSelCabFactBorrador);
                    List<BEDetalleFactura> vDetalle = new BLFactura().ListaDetalleaMasivaSunat(GlobalVars.Global.OWNER, ListaSelCabFactBorrador); ;
                    List<BEDescuentoRecargo> vDescuento = new BLDescuentoRecargo().ListarDescuentoFactura(GlobalVars.Global.OWNER, 0);

                    foreach (var s in vCabecera)
                    {
                        List<BECabeceraFactura> vCabeceraIndividual = vCabecera.Where(x => x.INV_ID == s.INV_ID).ToList();
                        List<BEDetalleFactura> vDetalleIndividual = vDetalle.Where(x => x.INV_ID == s.INV_ID).ToList();
                        MSG_SUNAT = FE.EnvioComprobanteElectronicoMasivoAct(vExtras, vCabeceraIndividual, vDetalleIndividual, vDescuento);

                        #region Consulta y vuelve a Actualizar Estado SUNAT

                        var RespuestaConsultaSunat = FE.ConsultaEstado(s.INV_ID);

                        if (RespuestaConsultaSunat.Contains("DOK"))
                        {
                            FE.ActualizarEstadoSunat(s.INV_ID, "DOK", "PROCESADO POR SUNAT");
                        }
                        else if (RespuestaConsultaSunat.Contains("RCH"))
                        {
                            FE.ActualizarEstadoSunat(s.INV_ID, "RCH", "RECHAZADO POR SUNAT");
                        }
                        else if (RespuestaConsultaSunat.Contains("FIR"))
                        {
                            FE.ActualizarEstadoSunat(s.INV_ID, "FIR", "REVISION POR SUNAT");
                        }
                        else if (RespuestaConsultaSunat.Contains("PEN"))
                        {
                            FE.ActualizarEstadoSunat(s.INV_ID, "PEN", "PENDIENTE DE REVISION POR SUNAT");
                        }
                        else if (RespuestaConsultaSunat.Contains("ERDTE"))
                        {
                            FE.ActualizarEstadoSunat(s.INV_ID, "ERDTE", "DOCUMENTO YA EXISTE EN LA SUITE");
                        }
                        else if (RespuestaConsultaSunat.Contains("ANU") && RespuestaConsultaSunat.Contains("Anulacion Aceptada"))
                        {
                            FE.ActualizarEstadoSunat(s.INV_ID, "DOK", "DOCUMENTO ANULADO EXITOSAMENTE");
                        }
                        else
                            FE.ActualizarEstadoSunat(s.INV_ID, "", "VOLVER A CONSULTAR");
                        #endregion

                        if ( !(RespuestaConsultaSunat.Contains("RCH")))
                            TotalEmitidas += 1;
                        else
                            TotalNoEmitidas += 1;

                        vCabeceraIndividual = new List<BECabeceraFactura>();
                        vDetalleIndividual = new List<BEDetalleFactura>();
                    }
                }



                //retorno.result = 1;
                //retorno.TotalFacturas = TotalEmitidas;
                //retorno.Code = TotalNoEmitidas;
                //retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                //}

                retorno.result = 1;
                retorno.TotalFacturas = TotalEmitidas;
                retorno.Code = TotalNoEmitidas;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
            }
            #endregion

            catch (Exception ex)
            {//Abrir los periodos que se encuentran facturados
                List<BELicenciaPlaneamiento> ListaPlaneamientoActualizar = new List<BELicenciaPlaneamiento>();
                BorLicenciaDetalleMasivaTmp.ForEach(S =>
                {
                    ListaPlaneamientoActualizar.Add(new BELicenciaPlaneamiento
                    {
                        LIC_PL_ID = S.codLicPlanificacion
                    });

                });
                new BLFactura().ActualizarPeriodosFacturandose(GlobalVars.Global.OWNER, ListaPlaneamientoActualizar, 2);//abrir periodos sin factura

                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerFacturasSeleccionadas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LimpiarBorradores()
        {
            Resultado retorno = new Resultado();
            try
            {
                decimal off_id = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                var id = new BLFactura().LimpiarBorradores(off_id);
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ValidarSerie", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public void AnularFacturaMasiva(decimal id)
        {
            BEFactura fac = new BEFactura();
            fac.OWNER = GlobalVars.Global.OWNER;
            fac.INV_ID = id;
            fac.INV_NULLREASON = "ERROR DE DATOS";
            fac.LOG_USER_UPDATE = UsuarioActual;
            fac.OFF_ID = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
            var exito = new BLFactura().AnularFactura(fac);
        }


        public JsonResult ValidarPermisoEmisionMensual()
        {
            Resultado retorno = new Resultado();
            try
            {
                var ValidarSiEntraAlModulo = new BLEmisionMensual().RecuperaQueModuloUtilizar();

                if (ValidarSiEntraAlModulo == 1)
                {
                    decimal off_id = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                    var res = new BLFactura().ValidarPermisoEmisionMensual(off_id);
                    if (res == 1) retorno.result = 1;
                    if (res == 0) { retorno.message = "LA OFICINA NO SE ENCUENTRA EN HORA DE EMISION"; retorno.result = 0; }
                    if (res == 2) { retorno.message = "LA OFICINA NO TIENE CONFIGURADA UNA HORA DE EMISION"; retorno.result = 0; }
                }
                else
                {
                    if (ValidarSiEntraAlModulo == 0) { retorno.message = "LA EMISION MENSUAL DEBE REALIZARCE DESDE EL MODULO  ADMINISTRACION EMISION MENSUAL"; retorno.result = 0; }
                }

            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ValidarSerie", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}


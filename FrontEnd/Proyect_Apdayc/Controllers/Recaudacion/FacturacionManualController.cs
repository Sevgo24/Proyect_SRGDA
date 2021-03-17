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
using SGRDA.DA;

namespace Proyect_Apdayc.Controllers.Recaudacion
{
    public class FacturacionManualController : Base
    {
        //
        // GET: /FacturacionManual/
        #region VARABLES

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

        int resultado = 0;
        string MSG_SUNAT = "";
        public const string nomAplicacion = "SRGDA";
        // FACTURACION 
        private const string K_SESION_FACTURACION_MANUAL_LISTA_TMP = "___FmDTOBeFacturacioListaTmp";
        private const string K_SESION_FACTURACION_MANUAL_MASIVA = "___FmDTOFacturacionMasiva";
        private const string K_SESION_LICENCIA_MANUAL_MASIVA = "___FmDTOLicenciaMasiva";
        private const string K_SESION_LICENCIA_MANUAL_DETALLE_MASIVA = "___FmDTOLicenciaDetalleMasiva";
        //BORRADOR
        private const string K_SESION_BOR_MANUAL_FACTURACION_LISTA_TMP = "___FmDTOBorBeFacturacioListaTmp";
        private const string K_SESION_BOR_MANUAL_FACTURACION_MASIVA = "___FmDTOBorFacturacionMasiva";
        private const string K_SESION_BOR_MANUAL_LICENCIA_MASIVA = "___FmDTOBorLicenciaMasiva";
        private const string K_SESION_BOR_MANUAL_LICENCIA_DETALLE_MASIVA = "___FmDTOBorLicenciaDetalleMasiva";
        private const string K_SESION_BOR_MANUAL_LICENCIA_DESCUENTO = "___FmDTOBorLicenciaDescuento";
        //CONSULTA
        public static string K_SESION_MANUAL_CONSULTA = "__DTOFacturacionManualTmp";
        private const string K_SESION_MANUAL_DETALLE_FACTURA = "___DTODetalleFacturaManual";
        private const string K_SESION_MANUAL_FACTURA = "___DTOFacturaManual";
        private const string K_SESION_MANUAL_RECIBO = "___DTOReciboManual";
        private const string K_SESION_MANUAL_LICENCIA = "___DTOConLicenciaManual";
        private const string K_SESION_MANUAL_LICENCIA_DETALLE = "___DTOConLicenciaDetalleManual";
        private const string K_SESION_BOR_LICENCIA_DESCUENTOC_PLANTILLA = "___DTOBorLicenciaDescuentoPlantilla";
        private decimal libConfigFact = Convert.ToDecimal(System.Web.Configuration.WebConfigurationManager.AppSettings["LibConfigFactura"]);
        public decimal VUM = new BLValormusica().ObtenerActivo(GlobalVars.Global.OWNER).VUM_VAL;
        //FACTURACION
        List<DTOFactura> facturacionMasiva = new List<DTOFactura>();
        List<DTOLicencia> licenciaMasiva = new List<DTOLicencia>();
        List<DTOLicenciaPlaneamiento> licenciaDetalleMasiva = new List<DTOLicenciaPlaneamiento>();
        //BORRADOR
        List<DTOFactura> borfacturacionMasiva = new List<DTOFactura>();
        List<DTOLicencia> borlicenciaMasiva = new List<DTOLicencia>();
        List<DTOFacturaDetallle> borlicenciaDetalleMasiva = new List<DTOFacturaDetallle>();
        List<DTODescuento> borlicenciaDescuento = new List<DTODescuento>();
        //CONSULTA
        List<DTOFactura> listaConsulta = new List<DTOFactura>();
        List<DTOFactura> listar = new List<DTOFactura>();
        DTOFactura Factura = new DTOFactura();
        DTOFactura FacturaAUX = new DTOFactura();
        private DateTime FechaSistema = new BLREC_RATES_GRAL().ObtenerFechaSistema();
        List<DTOFacturaDetallle> detalleFactura = new List<DTOFacturaDetallle>();
        List<DTORecibo> recibos = new List<DTORecibo>();
        List<DTOLicencia> ConLicencia = new List<DTOLicencia>();
        List<DTOFacturaDetallle> ConLicenciaDetalle = new List<DTOFacturaDetallle>();
        List<DTODescuentoPlantilla> borlicenciaDescuentoPlantilla = new List<DTODescuentoPlantilla>();

        //FACTURA ELECTRONICA
        ComprobanteElectronica FE = new ComprobanteElectronica();

        #endregion

        #region VISTAS
        public ActionResult Index()
        {
            Init(false);
            //FACTURA
            Session.Remove(K_SESION_FACTURACION_MANUAL_LISTA_TMP);
            Session.Remove(K_SESION_FACTURACION_MANUAL_MASIVA);
            Session.Remove(K_SESION_LICENCIA_MANUAL_MASIVA);
            Session.Remove(K_SESION_LICENCIA_MANUAL_DETALLE_MASIVA);
            //BORRRADOR
            Session.Remove(K_SESION_BOR_MANUAL_FACTURACION_LISTA_TMP);
            Session.Remove(K_SESION_BOR_MANUAL_FACTURACION_MASIVA);
            Session.Remove(K_SESION_BOR_MANUAL_LICENCIA_MASIVA);
            Session.Remove(K_SESION_BOR_MANUAL_LICENCIA_DETALLE_MASIVA);
            Session.Remove(K_SESION_BOR_MANUAL_LICENCIA_DESCUENTO);
            //CONSULTA
            Session.Remove(K_SESION_MANUAL_CONSULTA);
            Session.Remove(K_SESION_MANUAL_DETALLE_FACTURA);
            Session.Remove(K_SESION_MANUAL_FACTURA);
            Session.Remove(K_SESION_MANUAL_RECIBO);
            Session.Remove(K_SESION_MANUAL_LICENCIA);
            Session.Remove(K_SESION_MANUAL_LICENCIA_DETALLE);
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

        #region TEMPORALES
        public List<BEFactura> listaTempFacturacionMasivaTmp
        {
            get
            {
                return (List<BEFactura>)Session[K_SESION_FACTURACION_MANUAL_LISTA_TMP];
            }
            set
            {
                Session[K_SESION_FACTURACION_MANUAL_LISTA_TMP] = value;
            }
        }

        public List<DTOFactura> FacturacionMasivaTmp
        {
            get
            {
                return (List<DTOFactura>)Session[K_SESION_FACTURACION_MANUAL_MASIVA];
            }
            set
            {
                Session[K_SESION_FACTURACION_MANUAL_MASIVA] = value;
            }
        }

        public List<DTOLicencia> LicenciaMasivaTmp
        {
            get
            {
                return (List<DTOLicencia>)Session[K_SESION_LICENCIA_MANUAL_MASIVA];
            }
            set
            {
                Session[K_SESION_LICENCIA_MANUAL_MASIVA] = value;
            }
        }

        public List<DTOLicenciaPlaneamiento> LicenciaDetalleMasivaTmp
        {
            get
            {
                return (List<DTOLicenciaPlaneamiento>)Session[K_SESION_LICENCIA_MANUAL_DETALLE_MASIVA];
            }
            set
            {
                Session[K_SESION_LICENCIA_MANUAL_DETALLE_MASIVA] = value;
            }
        }
        //----------------------------------------------------------------------
        public List<BEFactura> listaTempBorFacturacionMasivaTmp
        {
            get
            {
                return (List<BEFactura>)Session[K_SESION_BOR_MANUAL_FACTURACION_LISTA_TMP];
            }
            set
            {
                Session[K_SESION_BOR_MANUAL_FACTURACION_LISTA_TMP] = value;
            }
        }

        public List<DTOFactura> BorFacturacionMasivaTmp
        {
            get
            {
                return (List<DTOFactura>)Session[K_SESION_BOR_MANUAL_FACTURACION_MASIVA];
            }
            set
            {
                Session[K_SESION_BOR_MANUAL_FACTURACION_MASIVA] = value;
            }
        }

        public List<DTOLicencia> BorLicenciaMasivaTmp
        {
            get
            {
                return (List<DTOLicencia>)Session[K_SESION_BOR_MANUAL_LICENCIA_MASIVA];
            }
            set
            {
                Session[K_SESION_BOR_MANUAL_LICENCIA_MASIVA] = value;
            }
        }

        public List<DTOFacturaDetallle> BorLicenciaDetalleMasivaTmp
        {
            get
            {
                return (List<DTOFacturaDetallle>)Session[K_SESION_BOR_MANUAL_LICENCIA_DETALLE_MASIVA];
            }
            set
            {
                Session[K_SESION_BOR_MANUAL_LICENCIA_DETALLE_MASIVA] = value;
            }
        }

        public List<DTODescuento> borLicenciaDescuentosTmp
        {
            get
            {
                return (List<DTODescuento>)Session[K_SESION_BOR_MANUAL_LICENCIA_DESCUENTO];
            }
            set
            {
                Session[K_SESION_BOR_MANUAL_LICENCIA_DESCUENTO] = value;
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

        #region FACTURACION MASIVA
        public JsonResult ListarFacturaMasivaSubGrilla(DateTime fini, DateTime ffin,
                                         string mogId, decimal modId, decimal dadId, decimal bpsId,
                                         decimal offId, decimal e_bpsId, decimal tipoEstId,
                                         decimal subTipoEstId, decimal licId, string monedaId, int historico, decimal idBpsGroup, decimal groupfact, int tipoFact, int EmiMensual)
        {
            Resultado retorno = new Resultado();
            try
            {
                Session.Remove(K_SESION_FACTURACION_MANUAL_LISTA_TMP);
                Session.Remove(K_SESION_FACTURACION_MANUAL_MASIVA);
                Session.Remove(K_SESION_LICENCIA_MANUAL_MASIVA);
                Session.Remove(K_SESION_LICENCIA_MANUAL_DETALLE_MASIVA);
                string busqEstadoPeriodo = Constantes.EstadoPeriodo.TODOS;
                int oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                var TienePermisoFacturar = new BLFactura().ObtienePermisoActualLicencia(licId);
                oficina = new BLOffices().ValidaOficina(oficina); //si es oficina de admin o conta devuelve 0 para poder visualizar todo
                var FacturaMasiva = new BLFactura().ListarFacturaMasivaSubGrilla(GlobalVars.Global.OWNER, fini, ffin,
                                                                        mogId, modId, dadId, bpsId,
                                                                        offId, e_bpsId, tipoEstId,
                                                                        subTipoEstId, licId, monedaId,
                                                                        libConfigFact, VUM, historico, busqEstadoPeriodo, idBpsGroup, groupfact, oficina, tipoFact, EmiMensual);

                if (FacturaMasiva.ListarLicenciaPlaneamiento != null)
                {
                    licenciaDetalleMasiva = new List<DTOLicenciaPlaneamiento>();
                    FacturaMasiva.ListarLicenciaPlaneamiento.ForEach(s =>
                    {
                        string estadoPeriodo = string.Empty;
                        if (s.LIC_PL_STATUS == Constantes.EstadoPeriodo.ABIERTO)
                            estadoPeriodo = Constantes.EstadoPeriodoDes.ABIERTO;
                        else if (s.LIC_PL_STATUS == Constantes.EstadoPeriodo.PARCIAL)
                            estadoPeriodo = Constantes.EstadoPeriodoDes.PARCIAL;
                        else if (s.LIC_PL_STATUS == Constantes.EstadoPeriodo.TOTAL)
                        {
                            estadoPeriodo = "CERRADO";
                            s.OBSERVACION = "Periodo Facturado.";
                        }

                        licenciaDetalleMasiva.Add(new DTOLicenciaPlaneamiento
                        {
                            Nro = s.Nro,
                            codigoLP = s.LIC_PL_ID,
                            codigoLic = s.LIC_ID,
                            codigoTarifa = s.RATE_ID,
                            anio = s.LIC_YEAR,
                            mesNom = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(s.LIC_MONTH)).ToUpper(),
                            fecha = s.LIC_DATE,
                            codigoMod = s.MOD_ID,
                            codigoEst = s.EST_ID,

                            SubMonto = s.SUB_MONTO,
                            Descuento = s.DESCUENTO,
                            Monto = s.MONTO,

                            valorImpuesto = s.TAXV_VALUEP,
                            Observacion = s.OBSERVACION,
                            EstadoVisualizar = s.STATE_CALC,
                            EstadoPeriodo = s.LIC_PL_STATUS,
                            EstadoPeriodoDes = estadoPeriodo,
                            EstadoPeriodoElectronico = s.STATE_CALC_FACT
                        });
                    });
                    LicenciaDetalleMasivaTmp = licenciaDetalleMasiva;
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
                            s.STATE_CALC_FACT_L = item.STATE_CALC_FACT; //asignando falso a la licencia .
                            s.OBSERVACION = item.OBSERVACION;
                        }

                        s.SUBTOTAL = SubTotalAcumulado;
                        s.DESCUENTO = DescuentoAcumulado;
                        s.TOTAL = TotalAcumulado;
                        s.STATE_CALC = (listaDetalle.Count == EstadoVisualizarDetalle) ? true : false;

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
                }

                if (FacturaMasiva.ListarFactura != null)
                {
                    facturacionMasiva = new List<DTOFactura>();
                    FacturaMasiva.ListarFactura.ForEach(s =>
                    {
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
                            s.STATE_CALC_FACT_F = item.STATE_CALC_FACT_L;
                            s.OBSERVACION_CALC_FACT_F = item.OBSERVACION;

                        }
                        s.SUBTOTAL = SubTotalAcumulado;
                        s.DESCUENTO = DescuentoAcumulado;
                        s.TOTAL = TotalAcumulado;

                        facturacionMasiva.Add(new DTOFactura
                        {
                            Nro = s.Nro,
                            idBps = s.BPS_ID,
                            socio = s.SOCIO,
                            idMoneda = s.CUR_ALPHA,
                            moneda = s.MONEDA,
                            tipo_pago = s.TIPO_PAGO,
                            grupo_fact = s.GRUPO_FACT,
                            estadoVisualizar = (lista.Count == EstadoVisualizar) ? true : false,
                            subTotal = s.SUBTOTAL,
                            descuento = s.DESCUENTO,
                            total = s.TOTAL,
                            Valida_Periodo_Fact = s.STATE_CALC_FACT_F,
                            observacion = s.OBSERVACION_CALC_FACT_F,
                        });
                    });
                    FacturacionMasivaTmp = facturacionMasiva;
                }

                //Agregando DESC PLANTILLA QUE NO SEAN CADENAS NUEVO*
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
                            TEMP_ID_DSC = 1 //Solo momentaneo
                        });
                    });
                }
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                retorno.data = Json(FacturaMasiva, JsonRequestBehavior.AllowGet);
                retorno.result = 1;

                if (tipoFact == Variables.FACTURACION_INDIVIDUAL && FacturaMasiva.ListarLicencia.Count == Variables.CERO && TienePermisoFacturar == Variables.NO)
                {

                    retorno.message = Variables.MSJ_LICENCIA_SIN_PERMISO_PARA_FACTURAR;
                    retorno.result = Variables.NO;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarFacturaMasivaSubGrilla", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //FACTURACION MANUAL
        public JsonResult ListarFacturaLicencia(decimal estado)
        {
            Resultado retorno = new Resultado();
            try
            {
                var detalle = LicenciaDetalleMasivaTmp.ToList();
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblPeriodos' border=0 width='100%;' class='k-grid k-widget' id='tblPeriodos'>");
                shtml.Append("<thead>");
                shtml.Append("<tr>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='width:50px;text-align:center' >Id</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='width:50px;text-align:center;display:none' >Nro</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='width:60px;text-align:center'>Tarifa</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='width:120px;text-align:center'>Periodo</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='width:120px;text-align:center'>Mes</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='width:100px;'>Monto Bruto</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='width:100px;'>Descuento</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='width:100px;'>Monto Neto</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='width:100px;display:none'>IdEstado</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='width:100px;'>Estado</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Obseración</th>");

                if (detalle != null && detalle.Count > 0)
                {
                    foreach (var item in detalle.OrderBy(x => x.fecha))
                    {
                        shtml.Append("<tr style='background-color:white'>");
                        if (item.EstadoVisualizar && item.EstadoPeriodo != Constantes.EstadoPeriodo.TOTAL && item.EstadoPeriodoElectronico == true)
                            shtml.AppendFormat("<td style='text-align:center;width:25px'><input type='checkbox' id='chkPL{0}' name='Check' class='Check'/></td>", item.codigoLP);
                        else
                            //shtml.AppendFormat("<td style='text-align:center;width:25px'></td>");
                            shtml.AppendFormat("<td style='text-align:center;width:25px'></td>");

                        shtml.AppendFormat("<td style='text-align:right;padding-right:10px' class='idLP'>{0}</td>", item.codigoLP);
                        shtml.AppendFormat("<td style='display:none' class='IdLic'>{0}</td>", item.codigoLic);
                        shtml.AppendFormat("<td style='text-align:center; width:40px;display:none' class='IdNro'>{0}</td>", item.Nro);
                        shtml.AppendFormat("<td style='text-align:center; width:42px'>{0}</td>", item.codigoTarifa);
                        //shtml.AppendFormat("<td style='text-align:center;'>{0}</td>", item.fecha.ToString("MMMMM").ToUpper());
                        shtml.AppendFormat("<td style='text-align:center;'>{0}</td>", item.anio);
                        shtml.AppendFormat("<td style='text-align:center; width:70px''>{0}</td>", item.mesNom);
                        //shtml.AppendFormat("<td style='text-align:center; width:70px''>{0}</td>", String.Format("{0:MM}", item.mes));

                        shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.SubMonto));
                        shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Descuento));
                        shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Monto));
                        shtml.AppendFormat("<td style='text-align:center; width:42px;display:none' class='idEstadoPeriodo'>{0}</td>", item.EstadoPeriodo);
                        shtml.AppendFormat("<td style='text-align:center; width:42px'>{0}</td>", item.EstadoPeriodoDes);
                        shtml.AppendFormat("<td >{0}</td>", item.Observacion);
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarFacturaMasivaCabecera", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerFacturasSeleccionadas(List<BELicenciaPlaneamiento> ReglaValor)
        {
            Resultado retorno = new Resultado();
            try
            {
                //decimal RedondeoMontoTotal = 0;
                //foreach (var i in ReglaValor)
                //{
                //    RedondeoMontoTotal += i.MONTO;
                //}

                //var PrimerPeriodo = ReglaValor.FirstOrDefault();
                ////if (tarifa.RATE_REDONDEO_ESP == Variables.SI)
                ////{
                //    //decimal Decimales= Decimal.Round(RedondeoMontoTotal, 2);

                //    float numDecimal = float.Parse("0," + Convert.ToString(RedondeoMontoTotal).Split('.')[1]);
                //    string DecuentoString = "0." + numDecimal;
                //    double Descuento = Convert.ToDouble(DecuentoString);
                //    if (Descuento < 0.5 && numDecimal != 0)
                //    {
                //        PrimerPeriodo.DESCUENTO = Convert.ToDecimal(Descuento);
                //        PrimerPeriodo.MONTO = PrimerPeriodo.MONTO - PrimerPeriodo.DESCUENTO;
                //    }
                //    else if (Descuento > 0.5 && numDecimal != 0)
                //    {
                //        double Desc = Descuento - 0.5;
                //        PrimerPeriodo.DESCUENTO = Convert.ToDecimal(Desc);
                //        PrimerPeriodo.MONTO = PrimerPeriodo.MONTO - PrimerPeriodo.DESCUENTO;
                //    }
                ////}


                //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                var codlicencia = LicenciaMasivaTmp[0].codLicencia; //ide la licencia
                List<BEFactura> ListaSelFactBorrador = new List<BEFactura>();
                int respuestamodalidad = new BLLicencias().ValidaModalidadLicencia(codlicencia);//valida modalidad de licencia 
                int Aprobado = 1;

                if (respuestamodalidad == 1)//si es un local permite inicializar los descuentos para  llenarlo con los descuentos de pronto a pago 
                    borLicenciaDescuentosPlantillaTmp = new List<DTODescuentoPlantilla>();
                else
                    Aprobado = 0;
                //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                BEFactura entidad = null;
                //Recueperando Validacion Licencia al Dia
                List<BELicencias> listalicenciashermanas = new List<BELicencias>();

                var licmaster = new BLLicencias().ListaLicenciaMaestraxLicid(codlicencia);// recuperamos su licencia padre
                string owner = GlobalVars.Global.OWNER;
                if (licmaster.LIC_MASTER == null || licmaster.LIC_MASTER == 0) //si es null es por que es un local simple (debe de evaluar tmb si merece o no descuento
                    listalicenciashermanas.Add(new BELicencias { LIC_ID = codlicencia });

                if (licmaster.LIC_MASTER > 0)
                    listalicenciashermanas = new BLLicencias().ListarLicenciaHijasxCodMult(owner, Convert.ToDecimal(licmaster.LIC_MASTER));//licencias hermanas

                var listalicenciasnoaldia = new BLFactura().ValidaLicenciaAlDia(listalicenciashermanas);

                foreach (var n in listalicenciasnoaldia.Where(n => n.LIC_ID == codlicencia)) //item.LIC_ID
                {
                    Aprobado = 0;
                }


                #region Redondeo Por Factura  
                decimal tarifaId = LicenciaDetalleMasivaTmp[0].codigoTarifa;
                var RATE_REDONDEO_ESP = new DATarifaTest().VALIDAR_REDONDEO_FACTURA(tarifaId);

                if (RATE_REDONDEO_ESP == Variables.SI)
                {
                    decimal RedondeoMontoTotal = 0;
                    foreach (var ii in ReglaValor)
                    {
                        foreach (var i in LicenciaDetalleMasivaTmp.Where(P => P.codigoLP == ii.LIC_PL_ID))
                        {
                            RedondeoMontoTotal += i.Monto;
                        }
                    }


                    var Pr = ReglaValor.FirstOrDefault();  //Primer Periodo Seleccionado
                    var PrimerPeriodo = LicenciaDetalleMasivaTmp.Where(P => P.codigoLP == Pr.LIC_PL_ID).FirstOrDefault(); // Completando DAtos Primer Periodo Seleccionado
                                                                                                                          //if (tarifa.RATE_REDONDEO_ESP == Variables.SI)
                                                                                                                          //{
                                                                                                                          //decimal Decimales= Decimal.Round(RedondeoMontoTotal, 2);
                    if (RedondeoMontoTotal.ToString().Contains("."))
                    {
                        float numDecimal = float.Parse("." + Convert.ToString(RedondeoMontoTotal).Split('.')[1]);
                        decimal Descuento = Convert.ToDecimal(numDecimal);
                        if (Descuento < Convert.ToDecimal("0.5") && numDecimal != 0)
                        {
                            PrimerPeriodo.Descuento = Convert.ToDecimal(Descuento) + PrimerPeriodo.Descuento;
                            PrimerPeriodo.Monto = PrimerPeriodo.Monto - Convert.ToDecimal(Descuento);
                        }
                        else if (Descuento > Convert.ToDecimal("0.5") && numDecimal != 0)
                        {
                            decimal Desc = Convert.ToDecimal(Descuento) - Convert.ToDecimal("0.5");
                            PrimerPeriodo.Descuento = Desc + PrimerPeriodo.Descuento;
                            PrimerPeriodo.Monto = PrimerPeriodo.Monto - Desc;
                        }
                    }
                   
                }
                #endregion

                //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                ReglaValor.ForEach(s =>
                {
                    decimal off_id = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                    var factura = FacturacionMasivaTmp.Where(p => p.Nro == s.Nro).FirstOrDefault();
                    var licencia = LicenciaMasivaTmp.Where(p => p.Nro == s.Nro);
                    var listaDetalle = LicenciaDetalleMasivaTmp.Where(p => p.Nro == s.Nro && p.codigoLP == s.LIC_PL_ID);

                    //Detalle
                    foreach (var detalle in listaDetalle)
                    {
                        List<BEDescuentos> desc_PlantillaIndiv = new List<BEDescuentos>();
                        #region GuardaDescuentoPlantillaTemporal
                        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                        if (Aprobado == 1)
                        {
                            desc_PlantillaIndiv = new BLFactura().DescuentoPlantillaIndvidual(detalle.codigoLic, detalle.Monto, ReglaValor.Count);

                            //David Detalle¨********************************
                            ///***********GUARDAMOS LOS DESCUENTOS Que se obtuvieron del Calculo de plantilla en el BLFACTURA
                            if (desc_PlantillaIndiv != null)
                            {
                                desc_PlantillaIndiv.ForEach(x =>
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
                        }
                        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                        #endregion


                        entidad = new BEFactura();
                        entidad.OWNER = GlobalVars.Global.OWNER;
                        entidad.LOG_USER_CREAT = UsuarioActual;
                        entidad.Nro = s.Nro;
                        entidad.LIC_ID = detalle.codigoLic;
                        entidad.INV_DATE = DateTime.Now;
                        entidad.CUR_ALPHA = factura.idMoneda;
                        entidad.INV_DETAIL = ReglaValor.ToList().Count > 1 ?
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
                        //entidad.DESCUENTO = detalle.Descuento;
                        entidad.DESCUENTO = detalle.Descuento + desc_PlantillaIndiv.Sum(x => x.monto); //Se agrego el Descuento Individual se suma
                        entidad.MONTO_DET = detalle.Monto - desc_PlantillaIndiv.Sum(x => x.monto); //Se Resto el Descuento Individual se resta
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

                var dato = new BLFactura().InsertarBorradorManual(ListaSelFactBorrador, GlobalVars.Global.OWNER);
                int cantParcial = ReglaValor.Where(x => x.LIC_PL_STATUS == Constantes.EstadoPeriodo.PARCIAL).Count();
                if (cantParcial > 0)
                    retorno.valor = Constantes.EstadoPeriodo.PARCIAL;
                else
                    retorno.valor = Constantes.EstadoPeriodo.TOTAL;
                retorno.Code = dato;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                retorno.result = 1;
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

        #region FACTURACION_BORRADOR

        #region LISTAR_BORRADORES
        public JsonResult ListarFacturacionBorrador(DateTime fini, DateTime ffin, decimal tipoLic, string idMoneda,
                                                    decimal idGrufact, decimal idBps, decimal idCorrelativo,
                                                    string idTipoPago, int conFecha, decimal idLic, string tipoFacturacion,
                                                    int idFacturaGenerado)
        {
            Resultado retorno = new Resultado();
            try
            {
                //TEMPORALES BORRADOR
                Session.Remove(K_SESION_BOR_MANUAL_FACTURACION_LISTA_TMP);
                Session.Remove(K_SESION_BOR_MANUAL_FACTURACION_MASIVA);
                Session.Remove(K_SESION_BOR_MANUAL_LICENCIA_MASIVA);
                Session.Remove(K_SESION_BOR_MANUAL_LICENCIA_DETALLE_MASIVA);
                decimal off_id = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);

                BEFactura FacturaBorrador = new BEFactura();
                FacturaBorrador = new BLFactura().ListarFacturaBorrador(GlobalVars.Global.OWNER,
                                                      fini, ffin, tipoLic, idMoneda, idGrufact, idBps, idCorrelativo,
                                                      idTipoPago, conFecha, idLic, idFacturaGenerado, off_id);

                List<BELicenciaPlaneamientoDetalle> pagosParciales = new List<BELicenciaPlaneamientoDetalle>();
                if (tipoFacturacion == Constantes.EstadoPeriodo.PARCIAL)
                    pagosParciales = new BLLicenciaPlaneamientoDetalle().ObtenerPagosParciales(GlobalVars.Global.OWNER, idFacturaGenerado);

                if (FacturaBorrador.ListarDetalleFactura != null)
                {
                    borlicenciaDetalleMasiva = new List<DTOFacturaDetallle>();
                    FacturaBorrador.ListarDetalleFactura.ForEach(s =>
                    {
                        decimal PagParcial = 0;
                        if (tipoFacturacion == Constantes.EstadoPeriodo.PARCIAL)
                            PagParcial = pagosParciales.Where(x => x.LIC_PL_ID == s.LIC_PL_ID).Count() == 0 ? 0 : pagosParciales.Where(x => x.LIC_PL_ID == s.LIC_PL_ID).FirstOrDefault().LIC_INVOICE_LINE;
                        borlicenciaDetalleMasiva.Add(new DTOFacturaDetallle
                        {
                            Id = s.INVL_ID,
                            codFactura = s.INV_ID,
                            codLicencia = s.LIC_ID,
                            codLicPlanificacion = s.LIC_PL_ID,
                            FechaPlanificacion = s.LIC_DATE,
                            valorBruto = s.INVL_GROSS,
                            valorDescuento = s.INVL_DISC,
                            valorBase = s.INVL_BASE,
                            valorImpuesto = s.INVL_TAXES,
                            valorNeto = s.INVL_NET,
                            rateId = s.RATE_ID,
                            codEstablecimiento = s.EST_ID,
                            PagoParcial = PagParcial,
                            SaldoPendiente = s.INVL_NET - PagParcial
                        });
                    });
                    BorLicenciaDetalleMasivaTmp = borlicenciaDetalleMasiva;

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

                                Monto = s.INVL_GROSS,
                                Descuento = s.INVL_DISC,
                                SubTotal = s.INVL_BASE,
                                Impuesto = s.INVL_TAXES,
                                Total = s.INVL_NET,

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

                                valorBase = s.INV_BASE,
                                valorDescuento = s.DESCUENTO,
                                valoImpuesto = s.INV_TAXES,
                                valorFinal = s.INV_NET,

                                idDireccionBps = s.ADD_ID,
                                idTipoPago = s.PAY_ID,
                            });
                        });
                        BorFacturacionMasivaTmp = borfacturacionMasiva;
                    }
                }

                retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                retorno.data = Json(FacturaBorrador, JsonRequestBehavior.AllowGet);
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarFacturacionBorrador", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarBorradorFactMasivaCabecera(string tipofacturacion)
        {
            borfacturacionMasiva = BorFacturacionMasivaTmp;
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblFacturaBorrador' border=0 width='75%;' class='k-grid k-widget' id='tblFacturaBorrador'>");
                shtml.Append("<thead><tr>");

                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='display:none;' >Identificador</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Id</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Socio</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Moneda</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Tipo de Pago</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Grupo Fact.</th>");
                if (borfacturacionMasiva != null)
                {
                    foreach (var item in borfacturacionMasiva.OrderBy(x => x.id))
                    {
                        shtml.Append("<tr style='background-color:white'>");
                        shtml.AppendFormat("<td style='display:none;' class='IDENTIFICADORCell'>{0}</td>", "F");
                        shtml.AppendFormat("<td style='width:120px;padding-left:10px;' class='IDCell'>{0}</td>", item.id);
                        shtml.AppendFormat("<td >{0}</td>", item.socio);
                        shtml.AppendFormat("<td >{0}</td>", item.moneda);
                        shtml.AppendFormat("<td >{0}</td>", item.tipo_pago);
                        shtml.AppendFormat("<td >{0}</td>", item.grupo_fact);
                        shtml.AppendFormat("<td style='text-align:center'>");
                        shtml.AppendFormat("</td>");
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

        public JsonResult ListarDetaLicenciaBorrador(string tipofacturacion)
        {
            Resultado retorno = new Resultado();
            try
            {
                var licencias = BorLicenciaMasivaTmp.ToList();
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
                shtml.Append("<thead>");

                shtml.Append("<tr>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:120px;display:none'>Id Factura</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:120px;'>Id Licencia</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:150px'>Licencia</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:350px'>Modalidad</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:350px'>Establecimiento</th>");
                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:120px;text-align:right; padding-right:10px'>Monto</th>");
                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:120px;text-align:right; padding-right:10px'>Descuento</th>");
                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:120px;text-align:right; padding-right:10px'>Impuesto</th>");
                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:120px;text-align:right; padding-right:10px'>Total</th>");
                //if (tipofacturacion == Clases.Constantes.EstadoPeriodo.PARCIAL)
                //{
                //    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:120px; padding-right:10px'>Cobrado</th>");
                //    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='width:120px; padding-right:10px'>Saldo Pendiente</th>");
                //}

                if (licencias != null && licencias.Count > 0)
                {
                    foreach (var item in licencias.OrderBy(x => x.codLicencia))
                    {
                        shtml.Append("<tr style='background-color:white'>");
                        shtml.AppendFormat("<td style='width:120px;display:none'>{0}</td>", item.codFactura);
                        shtml.AppendFormat("<td style='width:120px;padding-left:10px;'>{0}</td>", item.codLicencia);
                        shtml.AppendFormat("<td style='width:350px'>{0}</td>", item.nombreLicencia);
                        shtml.AppendFormat("<td style='width:350px'>{0}</td>", item.Modalidad);
                        shtml.AppendFormat("<td style='width:330px'>{0}</td>", item.Establecimiento);
                        //shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Monto));
                        //shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Descuento));
                        //shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Impuesto));
                        //shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Total));
                        //if (tipofacturacion == Clases.Constantes.EstadoPeriodo.PARCIAL)
                        //{
                        //    shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", 1));
                        //    shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", 2));
                        //}
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

        public JsonResult ListarDetaLicPlanBorrador(string tipofacturacion)
        {
            Resultado retorno = new Resultado();
            try
            {
                var detalle = BorLicenciaDetalleMasivaTmp.ToList();
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table  border=0 width='100%;' id='tblPlaneamientoFactura' >");
                shtml.Append("<thead>");
                shtml.Append("<tr>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='display:none'>Id Planificacion</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='display:none'>Id Factura</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='display:none'>Id Licencia</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='text-align:center;width:70px;'>Fecha</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='text-align:center;width:50px;'>Periodo</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='text-align:center;width:70px;'>Mes</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='text-align:right; padding-right:10px'>Monto Bruto</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='text-align:right; padding-right:10px'>Descuento</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='text-align:right; padding-right:10px'>Impuesto</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'  style='text-align:right; padding-right:10px'>Monto Neto</th>");
                if (tipofacturacion == Clases.Constantes.EstadoPeriodo.PARCIAL)
                {
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='text-align:center; padding-right:10px'  >Facturado</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='text-align:center; padding-right:10px'  >Por Facturar</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='text-align:center; padding-right:10px;display:none'>Por Facturar</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='text-align:center; padding-right:10px'  >Distribución de Montos</th>");
                }
                if (detalle != null && detalle.Count > 0)
                {
                    decimal valorBrutoAcum = 0;
                    decimal valorDescuentoAcum = 0;
                    decimal valorImpuestoAcum = 0;
                    decimal valorNetoAcum = 0;
                    decimal pagoParcialAcum = 0;
                    decimal saldoPendienteAcum = 0;
                    foreach (var item in detalle.OrderBy(x => x.codLicPlanificacion))
                    {
                        shtml.Append("<tr style='background-color:white; height:25px;'>");
                        shtml.AppendFormat("<td style='display:none' class='IdPlanificacion'>{0}</td>", item.codLicPlanificacion);
                        shtml.AppendFormat("<td style='display:none'>{0}</td>", item.codFactura);
                        shtml.AppendFormat("<td style='display:none'>{0}</td>", item.codLicencia);
                        shtml.AppendFormat("<td style='text-align:center'>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FechaPlanificacion));
                        shtml.AppendFormat("<td style='text-align:center'>{0}</td>", String.Format("{0:yyyy}", item.FechaPlanificacion));
                        shtml.AppendFormat("<td style='padding-left:10px;'>{0}</td>", item.FechaPlanificacion.ToString("MMMMM").ToUpper());
                        shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px;background-color: #CCC'>S/.  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorBruto));
                        shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px;background-color: #CCC'>S/.  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorDescuento));
                        shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px;background-color: #CCC'>S/.  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorImpuesto));
                        shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px;background-color: #CCC'>S/.  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorNeto));
                        if (tipofacturacion == Clases.Constantes.EstadoPeriodo.PARCIAL)
                        {
                            shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'> S/.  {0}   </td>", string.Format("{0:# ### ### ##0.##########}", item.PagoParcial));
                            shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'> S/.  {0}   </td>", string.Format("{0:# ### ### ##0.##########}", item.SaldoPendiente));
                            shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px; display:none' class='saldoPendiente'>{0}</td>", string.Format("{0:#########0.##########}", item.SaldoPendiente));
                            //shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>  <input id=txtDistribucion{0} class='cssValCaract k-formato-numerico'  type='txtDistribucion{0}' value={1} />  </td>", item.codLicPlanificacion, 0.00); // string.Format(new CultureInfo("en-SG", false), "{0:c0}", 123423.083234));
                            decimal? cero = Convert.ToDecimal(0.00);
                            //shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>  <input id=txtDistribucion{0} class='cssValCaract k-formato-numerico'             type='txtDistribucion{0}' value={1} />  </td>", item.codLicPlanificacion, 0.00); // string.Format(new CultureInfo("en-SG", false), "{0:c0}", 123423.083234));
                            //shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>  <input id=txtDistribucion{0} class='requerido cssValCaract k-formato-numerico'   type='txtDistribucion{0}' value='{1}' style='width:150px;' maxlength='18'    /></td>", item.codLicPlanificacion, (cero != null ? cero.Value.ToString("N", CultureInfo.InvariantCulture) : ""));
                            shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>  <input id=txtDistribucion{0} class='requerido cssValCaract'   type='txtDistribucion{0}' value='{1}' style='width:150px;' maxlength='18'/></td>", item.codLicPlanificacion, (cero != null ? cero.Value.ToString("N4") : ""));
                            //shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>  <input type='text' id='{1}' class='requerido cssValCaract'  value='{0}' style='width:150px;' maxlength='18'    /></td>", (car.LIC_CHAR_VAL != null ? car.LIC_CHAR_VAL.Value.ToString("N4") : ""), "txt" + item.Letra);
                            pagoParcialAcum += item.PagoParcial;
                            saldoPendienteAcum += item.SaldoPendiente;
                        }
                        shtml.Append("</td>");
                        shtml.Append("</tr>");

                        valorBrutoAcum += item.valorBruto;
                        valorDescuentoAcum += item.valorDescuento;
                        valorImpuestoAcum += item.valorImpuesto;
                        valorNetoAcum += item.valorNeto;
                    }
                    shtml.Append("<tr style='background-color:white; height:25px;'>");
                    shtml.AppendFormat("<td style='display:none' class='IdPlanificacion'>{0}</td>", 0);
                    shtml.AppendFormat("<td style='display:none'>{0}</td>", 0);// item.codFactura);
                    shtml.AppendFormat("<td style='display:none'>{0}</td>", 0);// item.codLicencia);
                    shtml.AppendFormat("<td style='text-align:center'>{0}</td>", "");
                    shtml.AppendFormat("<td style='text-align:center'>{0}</td>", "");
                    shtml.AppendFormat("<td style='padding-left:10px;'>{0}</td>", "TOTALES");
                    shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px;'>S/.  {0}</td>", string.Format("{0:# ### ### ##0.##########}", valorBrutoAcum));
                    shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px;'>S/.  {0}</td>", string.Format("{0:# ### ### ##0.##########}", valorDescuentoAcum));
                    shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px;'>S/.  {0}</td>", string.Format("{0:# ### ### ##0.##########}", valorImpuestoAcum));
                    shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px;'>S/.  {0}</td>", string.Format("{0:# ### ### ##0.##########}", valorNetoAcum));

                    if (tipofacturacion == Clases.Constantes.EstadoPeriodo.PARCIAL)
                    {
                        shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'> S/.  {0}   </td>", string.Format("{0:# ### ### ##0.##########}", pagoParcialAcum));
                        shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'> S/.  {0}   </td>", string.Format("{0:# ### ### ##0.##########}", saldoPendienteAcum));
                        //shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px; display:none' class='saldoPendiente'>{0}</td>", string.Format("{0:#########0.##########}", saldoPendienteAcum));
                        //shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>  <input id=txtDistribucion{0} class='currency'  type='txtDistribucion{0}' value={1} />  </td>", item.codLicPlanificacion, 0); // string.Format(new CultureInfo("en-SG", false), "{0:c0}", 123423.083234));
                    }
                    shtml.Append("</td>");
                    shtml.Append("</tr>");
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
        #endregion

        #region OBTENER_FACTURAS_SELECCIONADAS_BORADORES
        [HttpPost]
        public JsonResult ObtenerFacturasTotales(decimal serie, string detalleReporte, bool Manual, int Numero, DateTime fecEmision, int ValidaFecha)
        {
            Resultado retorno = new Resultado();
            try
            {

                List<BEFactura> ListaCabFactBorrador = new List<BEFactura>();
                List<BEFacturaDetalle> ListaDetFactBorrador = new List<BEFacturaDetalle>();
                List<BELicenciaPlaneamientoDetalle> ListaPlaneamientoDetalle = new List<BELicenciaPlaneamientoDetalle>();
                List<BEFacturaDescuento> ListaDescuentoXdetalle = new List<BEFacturaDescuento>();
                List<BELicenciaPlaneamientoDetalle> ListaDetallePlanificacion = new List<BELicenciaPlaneamientoDetalle>();
                List<BELicenciaPlaneamiento> ListaPlanificacion = new List<BELicenciaPlaneamiento>();

                BEFactura entCabFacBorrador = new BEFactura();
                BEFacturaDetalle entDetalleFactura = null;
                BELicenciaPlaneamientoDetalle entDetallePlanificacion = null;
                BEFacturaDescuento DescuentoXdetalle = null;
                BELicenciaPlaneamiento entPlanificacion = null;

                decimal off_id = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                var factura = BorFacturacionMasivaTmp.FirstOrDefault();
                var licencia = BorLicenciaMasivaTmp;
                var listaDetalle = BorLicenciaDetalleMasivaTmp;
                var Licencia = BorLicenciaDetalleMasivaTmp.FirstOrDefault().codLicencia;
                new BLFactura().ActualizaLicenciaValidacion(Licencia);

                DateTime fechaEmision = FechaSistema;
                if (Manual == true)
                    fechaEmision = fecEmision;

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


                #region CrearCabeceraFactura
                //CABECERA FACTURA
                entCabFacBorrador.OWNER = GlobalVars.Global.OWNER;
                entCabFacBorrador.INV_ID = factura.id;
                entCabFacBorrador.INV_NMR = serie;
                //entCabFacBorrador.INV_NUMBER = 0;
                entCabFacBorrador.INV_NUMBER = (Manual == true) ? Numero : 0;
                entCabFacBorrador.INV_TYPE = Constantes.FacturaTipo.FACTURA;
                entCabFacBorrador.INV_PHASE = Constantes.FacturaFase.DEFINITIVA;
                entCabFacBorrador.ADD_ID = factura.idDireccionBps;
                entCabFacBorrador.LOG_USER_UPDATE = UsuarioActual;
                entCabFacBorrador.PAY_ID = factura.idTipoPago;
                entCabFacBorrador.INV_BASE = factura.valorBase;
                entCabFacBorrador.INV_TAXES = factura.valoImpuesto;
                entCabFacBorrador.INV_NET = factura.valorFinal;
                entCabFacBorrador.INV_BALANCE = factura.valorFinal;
                //entCabFacBorrador.INV_MANUAL = true;
                entCabFacBorrador.INV_MANUAL = Manual;
                entCabFacBorrador.OFF_ID = off_id;
                entCabFacBorrador.INV_DATE = fechaEmision;

                // Agregar Campos Facturacion UBL 2.1
                entCabFacBorrador.Hora_Emision = fechaEmision.ToString("hh:mm:ss");
                entCabFacBorrador.CodigoLocalAnexo = "0000";


                if (!string.IsNullOrEmpty(detalleReporte))
                {
                    entCabFacBorrador.INV_REPORT_STATUS = true;
                    entCabFacBorrador.INV_REPORT_DETAILS = detalleReporte.ToUpper();
                }
                ListaCabFactBorrador.Add(entCabFacBorrador);

                #endregion

                #region CrearDetalleFactura
                foreach (var item in listaDetalle)
                {
                    //FACTURA - DETALLE
                    entDetalleFactura = new BEFacturaDetalle();
                    entDetalleFactura.OWNER = GlobalVars.Global.OWNER;
                    entDetalleFactura.INVL_ID = item.Id;
                    entDetalleFactura.INV_ID = item.codFactura;
                    entDetalleFactura.LIC_ID = item.codLicencia;
                    entDetalleFactura.LIC_PL_ID = item.codLicPlanificacion;
                    entDetalleFactura.EST_ID = item.codEstablecimiento;
                    entDetalleFactura.LIC_DATE = item.FechaPlanificacion;

                    //entDetalleFactura.INVL_GROSS = item.valorBruto * porcentaje;
                    //entDetalleFactura.INVL_DISC = item.valorDescuento * porcentaje;
                    //entDetalleFactura.INVL_BASE = entDetalleFactura.INVL_GROSS - entDetalleFactura.INVL_DISC;
                    //entDetalleFactura.INVL_TAXES = item.valorImpuesto * porcentaje;
                    //entDetalleFactura.INVL_NET = valorParcial;
                    //entDetalleFactura.INVL_BALANCE = valorParcial;
                    entDetalleFactura.INVL_GROSS = item.valorBruto;
                    entDetalleFactura.INVL_DISC = item.valorDescuento;
                    entDetalleFactura.INVL_BASE = item.valorBase;
                    entDetalleFactura.INVL_TAXES = item.valorImpuesto;
                    entDetalleFactura.INVL_NET = item.valorNeto;
                    entDetalleFactura.INVL_BALANCE = item.valorNeto;

                    entDetalleFactura.LOG_USER_UPDATE = UsuarioActual;
                    entDetalleFactura.RATE_ID = item.rateId;
                    ListaDetFactBorrador.Add(entDetalleFactura);
                    #endregion

                    //if (GlobalVars.Global.FE == true)
                    //{
                    //    MSG_SUNAT = FE.EnvioComprobanteElectronico(factura.id);
                    //    //MSG_SUNAT = FE.EnvioComprobanteElectronicoMasivo(entCabFacBorrador.INV_ID, entCabFacBorrador.INV_NMR, entCabFacBorrador.ADD_ID);
                    //}




                    //PLANIFICACION
                    entPlanificacion = new BELicenciaPlaneamiento();
                    entPlanificacion.OWNER = GlobalVars.Global.OWNER;
                    entPlanificacion.LIC_PL_ID = item.codLicPlanificacion;
                    entPlanificacion.LIC_PL_AMOUNT = item.valorNeto;
                    entPlanificacion.LIC_PL_STATUS = Constantes.EstadoPeriodo.TOTAL;
                    ListaPlanificacion.Add(entPlanificacion);

                    //PLANIFICACION DETALLE
                    entDetallePlanificacion = new BELicenciaPlaneamientoDetalle();
                    entDetallePlanificacion.OWNER = GlobalVars.Global.OWNER;
                    entDetallePlanificacion.LIC_PL_ID = item.codLicPlanificacion;
                    entDetallePlanificacion.INV_ID = item.codFactura;
                    entDetallePlanificacion.LIC_INVOICE_VAL = item.valorNeto;// factura.valorFinal;
                    entDetallePlanificacion.LIC_INVOICE_LINE = item.valorNeto;
                    entDetallePlanificacion.LOG_USER_CREAT = UsuarioActual;
                    entDetallePlanificacion.LIC_PL_PARTIAL = false;
                    ListaDetallePlanificacion.Add(entDetallePlanificacion);

                    #region DESCUENTO
                    decimal resultBase = item.valorBruto;
                    decimal resultNeto = resultBase;
                    decimal valorDescuento = 0;
                    var listaDescuentos = borLicenciaDescuentosTmp.Where(p => p.LicId == item.codLicencia);
                    #region DESCUENTOS PLANTILLA ASIGNAR
                    //***************que recupera de la tarifa (sirve para no insertar valores erroneos en la BD)
                    var listaDescuentosPlant = borLicenciaDescuentosPlantillaTmp.Where(p => p.LicId == item.codLicencia);
                    foreach (var x in listaDescuentos)
                    {
                        foreach (var y in listaDescuentosPlant)
                        {
                            if ((x.Id == y.Id))
                            {
                                x.Valor = y.Valor;
                                if (y.TEMP_ID_DSC > 0)
                                    x.Tipo = y.Tipo;
                            }
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



                ComprobanteElectronica ClaseFactElectronico = new ComprobanteElectronica();
                if (factura.idDireccionBps != 0 && factura.idDireccionBps != null)
                {

                    var dato = new BLFactura().ActualizarEstadoDefinitivaXML(GlobalVars.Global.OWNER,
                                                    ListaCabFactBorrador, ListaDetFactBorrador,
                                                    ListaPlanificacion, ListaDetallePlanificacion,
                                                    ListaDescuentoXdetalle);

                    //INVOCAR LA WEB SERVICES - FACTURACION ELECTRONICA
                    if (GlobalVars.Global.FE == true && Manual == false)
                    {
                        MSG_SUNAT = FE.EnvioComprobanteElectronico(factura.id);

                        #region Consulta y vuelve a Actualizar Estado SUNAT

                        var RespuestaConsultaSunat = FE.ConsultaEstado(factura.id);

                        if (RespuestaConsultaSunat.Contains("DOK"))
                        {
                            FE.ActualizarEstadoSunat(factura.id, "DOK", "PROCESADO POR SUNAT");
                        }
                        else if (RespuestaConsultaSunat.Contains("RCH"))
                        {
                            FE.ActualizarEstadoSunat(factura.id, "RCH", "RECHAZADO POR SUNAT");
                        }
                        else if (RespuestaConsultaSunat.Contains("FIR"))
                        {
                            FE.ActualizarEstadoSunat(factura.id, "FIR", "REVISION POR SUNAT");
                        }
                        else if (RespuestaConsultaSunat.Contains("PEN"))
                        {
                            FE.ActualizarEstadoSunat(factura.id, "PEN", "PENDIENTE DE REVISION POR SUNAT");
                        }
                        else if (RespuestaConsultaSunat.Contains("ERDTE"))
                        {
                            FE.ActualizarEstadoSunat(factura.id, "ERDTE", "DOCUMENTO YA EXISTE EN LA SUITE");
                        }
                        else if (RespuestaConsultaSunat.Contains("ANU") && RespuestaConsultaSunat.Contains("Anulacion Aceptada"))
                        {
                            FE.ActualizarEstadoSunat(factura.id, "DOK", "DOCUMENTO ANULADO EXITOSAMENTE");
                        }
                        else
                            FE.ActualizarEstadoSunat(factura.id, "", "VOLVER A CONSULTAR");
                        #endregion


                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO + " " + RespuestaConsultaSunat;
                    }
                    else
                    {
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    }
                }
                else
                {
                    //ClaseFactElectronico.ActualizarEstadoSunat(factura.id, "EL", "ERROR DE ENVIO - ANULE LA FACTURA");
                    retorno.result = 2;
                    retorno.Code = Convert.ToInt32(factura.idBps);
                    retorno.message = "El Socio de Negocio no tiene una dirección asignada o no está seleccionada como principal.";
                }
                new BLFactura().ActualizarPeriodosFacturandose(GlobalVars.Global.OWNER, ListaPlaneamientoActualizar, 0); //0 PONER EN TERMIADO/FACTURADO

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerFacturasTotales", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerFacturasParciales(List<BELicenciaPlaneamientoDetalle> ReglaValor)
        {
            Resultado retorno = new Resultado();
            try
            {
                List<BEFactura> ListaCabFactBorrador = new List<BEFactura>();
                List<BEFacturaDetalle> ListaDetFactBorrador = new List<BEFacturaDetalle>();
                List<BELicenciaPlaneamiento> ListaPlanificacion = new List<BELicenciaPlaneamiento>();
                List<BELicenciaPlaneamientoDetalle> ListaDetallePlanificacion = new List<BELicenciaPlaneamientoDetalle>();
                List<BEFacturaDescuento> ListaDescuentoXdetalle = new List<BEFacturaDescuento>();

                BEFactura entCabFacBorrador = new BEFactura();
                BEFacturaDetalle entDetalleFactura = null;
                BELicenciaPlaneamientoDetalle entDetallePlanificacion = null;
                BELicenciaPlaneamiento entPlanificacion = null;
                BEFacturaDescuento DescuentoXdetalle = null;

                decimal off_id = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                var factura = BorFacturacionMasivaTmp.FirstOrDefault();
                var licencia = BorLicenciaMasivaTmp;
                var listaDetalle = BorLicenciaDetalleMasivaTmp;
                decimal totalPagoParcial = 0;
                if (ReglaValor.Count > 0)
                    totalPagoParcial = ReglaValor.Sum(x => x.LIC_INVOICE_LINE);
                //decimal serie = 83;
                decimal serie = ReglaValor.Count > 0 ? ReglaValor.FirstOrDefault().SERIE : 0;
                string detalleReporte = ReglaValor.Count > 0 ? ReglaValor.FirstOrDefault().INV_REPORT_DETAILS : string.Empty;

                bool manual = ReglaValor.FirstOrDefault().TIPO_MANUAL;
                int numero = ReglaValor.FirstOrDefault().NUMERO;
                DateTime fechaEmision = FechaSistema;
                if (manual == true)
                    fechaEmision = ReglaValor.FirstOrDefault().FECHA_EMISION;


                //FACTURA - DETALLE
                foreach (var item in listaDetalle)
                {
                    #region DESCUENTO
                    decimal resultBase = item.valorBruto;
                    decimal resultNeto = resultBase;
                    decimal valorDescuento = 0;
                    var listaDescuentos = borLicenciaDescuentosTmp.Where(p => p.LicId == item.codLicencia);
                    #region DESCUENTOS PLANTILLA ASIGNAR
                    //***************que recupera de la tarifa (sirve para no insertar valores erroneos en la BD)
                    var listaDescuentosPlant = borLicenciaDescuentosPlantillaTmp.Where(p => p.LicId == item.codLicencia);
                    foreach (var x in listaDescuentos)
                    {
                        foreach (var y in listaDescuentosPlant)
                        {
                            if ((x.Id == y.Id))
                            {
                                x.Valor = y.Valor;
                                if (y.TEMP_ID_DSC > 0)
                                    x.Tipo = y.Tipo;
                            }
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

                    decimal valorParcial = ReglaValor.Where(x => x.LIC_PL_ID == item.codLicPlanificacion).FirstOrDefault().LIC_INVOICE_LINE;
                    decimal porcentaje = 0;

                    porcentaje = (((valorParcial * 100) / item.valorNeto) / 100);
                    entDetalleFactura = new BEFacturaDetalle();
                    entDetalleFactura.OWNER = GlobalVars.Global.OWNER;
                    entDetalleFactura.INVL_ID = item.Id;
                    entDetalleFactura.INV_ID = item.codFactura;
                    entDetalleFactura.LIC_ID = item.codLicencia;
                    entDetalleFactura.LIC_PL_ID = item.codLicPlanificacion;
                    entDetalleFactura.EST_ID = item.codEstablecimiento;
                    entDetalleFactura.LIC_DATE = item.FechaPlanificacion;

                    entDetalleFactura.INVL_GROSS = item.valorBruto * porcentaje;
                    entDetalleFactura.INVL_DISC = item.valorDescuento * porcentaje;
                    entDetalleFactura.INVL_BASE = entDetalleFactura.INVL_GROSS - entDetalleFactura.INVL_DISC;
                    entDetalleFactura.INVL_TAXES = item.valorImpuesto * porcentaje;
                    entDetalleFactura.INVL_NET = valorParcial;
                    entDetalleFactura.INVL_BALANCE = valorParcial;

                    entDetalleFactura.LOG_USER_UPDATE = UsuarioActual;
                    entDetalleFactura.RATE_ID = item.rateId;
                    ListaDetFactBorrador.Add(entDetalleFactura);

                    //DESCUENTO                   
                    foreach (var itemDesc in ListaDescuentoXdetalle)
                    {
                        if (itemDesc.INVL_ID == item.Id)
                            // itemDesc.DISC_VALUE = item.valorDescuento * porcentaje; antes
                            itemDesc.DISC_VALUE = itemDesc.DISC_VALUE * porcentaje;//despues
                    }

                    //PANIFICACION                          
                    entPlanificacion = new BELicenciaPlaneamiento();
                    entPlanificacion.OWNER = GlobalVars.Global.OWNER;
                    entPlanificacion.LIC_PL_ID = item.codLicPlanificacion;
                    entPlanificacion.LIC_PL_AMOUNT = item.valorNeto;
                    if (item.SaldoPendiente == valorParcial)
                        entPlanificacion.LIC_PL_STATUS = Constantes.EstadoPeriodo.TOTAL;
                    else
                        entPlanificacion.LIC_PL_STATUS = Constantes.EstadoPeriodo.PARCIAL;
                    ListaPlanificacion.Add(entPlanificacion);

                    //PLANIFICACION DETALLE                    
                    entPlanificacion = new BELicenciaPlaneamiento();
                    entDetallePlanificacion = new BELicenciaPlaneamientoDetalle();
                    entDetallePlanificacion.OWNER = GlobalVars.Global.OWNER;
                    entDetallePlanificacion.LIC_PL_ID = item.codLicPlanificacion;
                    entDetallePlanificacion.INV_ID = item.codFactura;
                    entDetallePlanificacion.LIC_INVOICE_VAL = item.valorNeto;
                    entDetallePlanificacion.LIC_INVOICE_LINE = valorParcial;
                    entDetallePlanificacion.LOG_USER_CREAT = UsuarioActual;
                    entDetallePlanificacion.LIC_PL_PARTIAL = true;
                    ListaDetallePlanificacion.Add(entDetallePlanificacion);
                }

                //CABECERA FACTURA
                entCabFacBorrador.OWNER = GlobalVars.Global.OWNER;
                entCabFacBorrador.INV_ID = factura.id;
                entCabFacBorrador.INV_NMR = serie;
                //entCabFacBorrador.INV_NUMBER = 0;
                entCabFacBorrador.INV_TYPE = Constantes.FacturaTipo.FACTURA;
                entCabFacBorrador.INV_PHASE = Constantes.FacturaFase.DEFINITIVA;
                entCabFacBorrador.ADD_ID = factura.idDireccionBps;
                entCabFacBorrador.LOG_USER_UPDATE = UsuarioActual;
                entCabFacBorrador.PAY_ID = factura.idTipoPago;
                entCabFacBorrador.INV_BASE = ListaDetFactBorrador.Sum(x => x.INVL_GROSS);//totalPagoParcial;
                entCabFacBorrador.INV_TAXES = ListaDetFactBorrador.Sum(x => x.INVL_TAXES);//factura.valoImpuesto;
                entCabFacBorrador.INV_NET = totalPagoParcial;//factura.valorFinal;
                entCabFacBorrador.INV_BALANCE = totalPagoParcial;//factura.valorFinal;                
                entCabFacBorrador.INV_MANUAL = manual;// true;
                entCabFacBorrador.OFF_ID = off_id;
                entCabFacBorrador.INV_NUMBER = numero;
                entCabFacBorrador.INV_DATE = fechaEmision;

                if (!string.IsNullOrEmpty(detalleReporte))
                {
                    entCabFacBorrador.INV_REPORT_STATUS = true;
                    entCabFacBorrador.INV_REPORT_DETAILS = detalleReporte.ToUpper();
                }
                ListaCabFactBorrador.Add(entCabFacBorrador);

                //var dato = new BLFactura().ActualizarEstadoDefinitivaXML(GlobalVars.Global.OWNER,
                //                                ListaCabFactBorrador, ListaDetFactBorrador,
                //                                ListaPlanificacion, ListaDetallePlanificacion,
                //                                ListaDescuentoXdetalle);

                //INVOCAR LA WEB SERVICES - FACTURACION ELECTRONICA
                ComprobanteElectronica ClaseFactElectronico = new ComprobanteElectronica();
                if (factura.idDireccionBps != 0 && factura.idDireccionBps != null)
                {

                    var dato = new BLFactura().ActualizarEstadoDefinitivaXML(GlobalVars.Global.OWNER,
                                                    ListaCabFactBorrador, ListaDetFactBorrador,
                                                    ListaPlanificacion, ListaDetallePlanificacion,
                                                    ListaDescuentoXdetalle);
                    if (GlobalVars.Global.FE == true && manual == false)
                    {
                        MSG_SUNAT = FE.EnvioComprobanteElectronico(factura.id);
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO + " " + MSG_SUNAT;
                    }
                    else
                    {
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    }
                }
                else
                {
                    //ClaseFactElectronico.ActualizarEstadoSunat(factura.id, "EL", "ERROR DE ENVIO - ANULE LA FACTURA");
                    retorno.result = 2;
                    retorno.Code = Convert.ToInt32(factura.idBps);
                    retorno.message = "El Socio de Negocio no tiene una dirección asignada o no está seleccionada como principal.";
                }


            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerFacturasManuales", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region FACURACION CONSULTA

        #region TEMPORALES_CONSUTA
        public List<DTOFactura> ListaConsultaTmp
        {
            get
            {
                return (List<DTOFactura>)Session[K_SESION_MANUAL_CONSULTA];
            }
            set
            {
                Session[K_SESION_MANUAL_CONSULTA] = value;
            }
        }
        public List<DTOFacturaDetallle> DetalleFacturaTmp
        {
            get
            {
                return (List<DTOFacturaDetallle>)Session[K_SESION_MANUAL_DETALLE_FACTURA];
            }
            set
            {
                Session[K_SESION_MANUAL_DETALLE_FACTURA] = value;
            }
        }
        public DTOFactura FacturaTmp
        {
            get
            {
                return (DTOFactura)Session[K_SESION_MANUAL_FACTURA];
            }
            set
            {
                Session[K_SESION_MANUAL_FACTURA] = value;
            }
        }
        public List<DTORecibo> RecibosTmp
        {
            get
            {
                return (List<DTORecibo>)Session[K_SESION_MANUAL_RECIBO];
            }
            set
            {
                Session[K_SESION_MANUAL_RECIBO] = value;
            }
        }
        public List<DTOLicencia> LicenciaTmp
        {
            get
            {
                return (List<DTOLicencia>)Session[K_SESION_MANUAL_LICENCIA];
            }
            set
            {
                Session[K_SESION_MANUAL_LICENCIA] = value;
            }
        }
        public List<DTOFacturaDetallle> LicenciaDetalleTmp
        {
            get
            {
                return (List<DTOFacturaDetallle>)Session[K_SESION_MANUAL_LICENCIA_DETALLE];
            }
            set
            {
                Session[K_SESION_MANUAL_LICENCIA_DETALLE] = value;
            }
        }
        public List<DTOEstablecimiento> EstablecimientoSocioEmpresarialTmp
        {
            get
            {
                return (List<DTOEstablecimiento>)Session[K_SESION_MANUAL_LICENCIA_DETALLE];
            }
            set
            {
                Session[K_SESION_MANUAL_LICENCIA_DETALLE] = value;
            }
        }
        #endregion

        #region LISTAR_CONSULTA
        public JsonResult ListarConsulta(string numSerial, decimal numFact, decimal idSoc,
                                                    decimal grupoFact, string moneda, decimal idLic,
                                                    DateTime Fini, DateTime Ffin, decimal idFact,
                                                    int impresas, int anuladas, decimal licTipo, decimal agenteBpsId,
                                                    int conFecha, int tipoDoc, decimal idOficina, decimal valorDivision, int estado, decimal idBpsGroup, decimal idPlan = 0)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    Session.Remove(K_SESION_MANUAL_CONSULTA);
                    var FacturaConsulta = new BLFactura().ListarConsulta(GlobalVars.Global.OWNER, numSerial, numFact, idSoc,
                                           grupoFact, moneda, idLic,
                                           Fini, Ffin, idFact, impresas, anuladas, licTipo,
                                           agenteBpsId, conFecha, tipoDoc, idOficina, valorDivision, estado, idPlan, idBpsGroup);


                    if (FacturaConsulta != null)
                    {

                        if (FacturaConsulta.ListarLicencia != null)
                        {
                            ConLicenciaDetalle = new List<DTOFacturaDetallle>();
                            FacturaConsulta.ListarDetalleFactura.ForEach(s =>
                            {
                                ConLicenciaDetalle.Add(new DTOFacturaDetallle
                                {
                                    Id = s.INVL_ID,
                                    codFactura = s.INV_ID,
                                    codLicencia = s.LIC_ID,
                                    codLicPlanificacion = s.LIC_PL_ID,
                                    FechaPlanificacion = s.LIC_DATE,
                                    anio = s.LIC_YEAR,
                                    mes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(s.LIC_MONTH)).ToUpper(),
                                    valorBruto = s.INVL_GROSS,
                                    valorDescuento = s.INVL_DISC,
                                    valorBase = s.INVL_BASE,
                                    valorImpuesto = s.INVL_TAXES,
                                    valorNeto = s.INVL_NET,

                                    codEstablecimiento = s.EST_ID
                                });
                            });
                            LicenciaDetalleTmp = ConLicenciaDetalle;
                        }

                        if (FacturaConsulta.ListarLicencia != null)
                        {
                            ConLicencia = new List<DTOLicencia>();
                            FacturaConsulta.ListarLicencia.ForEach(s =>
                            {
                                ConLicencia.Add(new DTOLicencia
                                {
                                    codFactura = s.INV_ID,
                                    codLicencia = s.LIC_ID,
                                    nombreLicencia = s.LIC_NAME,
                                    Modalidad = s.Modalidad,
                                    Establecimiento = s.Establecimiento,

                                    Monto = s.INVL_GROSS,
                                    Descuento = s.INVL_DISC,
                                    SubTotal = s.INVL_BASE,
                                    Impuesto = s.INVL_TAXES,
                                    Total = s.INVL_NET,

                                });
                            });
                            LicenciaTmp = ConLicencia;
                        }

                        if (FacturaConsulta.ListarFactura != null)
                        {
                            listaConsulta = new List<DTOFactura>();
                            FacturaConsulta.ListarFactura.ForEach(s =>
                            {
                                listaConsulta.Add(new DTOFactura
                                {
                                    id = s.INV_ID,
                                    tipo = s.INVT_DESC,
                                    serial = s.NMR_SERIAL,
                                    numFacturaActual = s.INV_NUMBER,
                                    fechaFact = s.INV_DATE,
                                    fechaAnulacion = s.INV_NULL,
                                    socio = s.SOCIO,
                                    doc_Identificacion = s.TAXN_NAME,
                                    num_Identificacion = s.TAX_ID,
                                    moneda = s.MONEDA,
                                    valorFinal = s.INV_NET,
                                    cobradoNeto = s.INV_COLLECTN,
                                    saldoFactura = s.INV_BALANCE,
                                    factRefNotCred = s.INV_CN_REF,
                                    Estado_Sunat = s.ESTADO_SUNAT,
                                    estadoFact = s.EST_FACT,
                                    fecha_cancelacion = s.FECHA_CANCELACION

                                });
                            });
                            ListaConsultaTmp = listaConsulta;
                        }
                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(FacturaConsulta, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarConsulta", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarFactConsulta()
        {
            listar = ListaConsultaTmp;
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblFacturaMasiva' border=0 width='100%;' class='k-grid k-widget' id='tblFacturaMasiva'>");
                shtml.Append("<thead><tr>");
                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='text-align:center;width:25px'>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Id</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Serie</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >#</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Fecha</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Cancelación</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Anulado</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Ident.</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >N° Ident.</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Socio de negocio</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Moneda</th>");
                //shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Facturado</th>");
                //shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Cobrado</th>");
                //shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Saldo Pendiente</th>");
                //shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Ref N.C</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style'width:120px'>Estado</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style'width:45px'>Estado Sunat</th>");

                if (listar != null)
                {
                    foreach (var item in listar.OrderBy(x => x.id))
                    {
                        shtml.Append("<tr style='background-color:white'>");

                        //shtml.AppendFormat("<td style='width:25px'> ");
                        //shtml.AppendFormat("<a href=# onclick='verDetaCon({0});'><img id='expandCon" + item.id + "'  src='../Images/botones/less.png'  width=20px     title='Ocultar detalle.' alt='ver detalle.' border=0></a>", item.id);
                        //shtml.Append("</td>");
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:45px' onclick='return obtenerId({0},{1});' class='IDCell' >{0}</td>", item.id, item.fechaAnulacion == null ? 0 : 1);
                        //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2});'>{0}</td>", item.tipo, item.id, item.fechaAnulacion == null ? 0 : 1);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right' onclick='return obtenerId({1},{2});'>{0}</td>", item.serial, item.id, item.fechaAnulacion == null ? 0 : 1);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right' onclick='return obtenerId({1},{2});' >{0}</td>", item.numFacturaActual, item.id, item.fechaAnulacion == null ? 0 : 1);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2});' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.fechaFact), item.id, item.fechaAnulacion == null ? 0 : 1);
                        if (item.estadoFact == 2)// Constantes.EstadoFactura.CANCELADO
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2});' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.fecha_cancelacion), item.id, item.fechaAnulacion == null ? 0 : 1);
                        else
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2});' >{0}</td>", "", item.id, item.fechaAnulacion == null ? 0 : 1);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2});' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.fechaAnulacion), item.id, item.fechaAnulacion == null ? 0 : 1);
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2});' >{0}</td>", item.doc_Identificacion, item.id, item.fechaAnulacion == null ? 0 : 1);
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2});' >{0}</td>", item.num_Identificacion, item.id, item.fechaAnulacion == null ? 0 : 1);
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2});' >{0}</td>", item.socio, item.id, item.fechaAnulacion == null ? 0 : 1);
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2});' >{0}</td>", item.moneda, item.id, item.fechaAnulacion == null ? 0 : 1);
                        //shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' onclick='return obtenerId({1},{2});' >S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorFinal), item.id, item.fechaAnulacion == null ? 0 : 1);
                        //shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' onclick='return obtenerId({1},{2});' >S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.cobradoNeto), item.id, item.fechaAnulacion == null ? 0 : 1);
                        //shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' onclick='return obtenerId({1},{2});' >S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.saldoFactura), item.id, item.fechaAnulacion == null ? 0 : 1);
                        //if (item.factRefNotCred != 0)
                        //    shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2});' style='text-align:right; width:150px; padding-right:10px'>{0}</td>", item.factRefNotCred, item.id, item.fechaAnulacion == null ? 0 : 1);
                        //else
                        //    shtml.AppendFormat("<td style='cursor:pointer;' style='text-align:right; width:150px; padding-right:10px'> </td>");
                        switch (item.estadoFact)
                        {
                            case 4: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2});' style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", Constantes.EstadoFactura.ANULADA, item.id, item.fechaAnulacion == null ? 0 : 1); break;
                            case 2: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2});' style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.CANCELADO, item.id, item.fechaAnulacion == null ? 0 : 1); break;
                            case 1: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2});' style='text-align:right; width:150px; padding-right:10px'> <font color='green'> {0} </font> </td>", Constantes.EstadoFactura.CANCELADA_PARCIAL, item.id, item.fechaAnulacion == null ? 0 : 1); break;
                            case 3: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2});' style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.PENDIENTE_PAGO, item.id, item.fechaAnulacion == null ? 0 : 1); break;
                            default: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2});' style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", Constantes.EstadoFactura.ANULADA, item.id, item.fechaAnulacion == null ? 0 : 1); break;
                        }

                        //ESTADO SUNAT
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2});' >{0}</td>", item.Estado_Sunat, item.id, item.fechaAnulacion == null ? 0 : 1);

                        shtml.AppendFormat("<td style='cursor:pointer;' style='text-align:center'>");
                        shtml.AppendFormat("</td>");

                        shtml.Append("</tr>");

                        shtml.Append("</div>");
                        shtml.Append("</td>");
                        shtml.Append("</tr>");
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarFactConsulta", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarLicenciaConsulta()
        {
            Resultado retorno = new Resultado();
            try
            {
                //var licencias = LicenciaTmp.Where(p => p.codFactura == codFact).ToList();

                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
                shtml.Append("<thead>");

                shtml.Append("<tr>");
                shtml.Append("<th class='k-header' style='width:120px;display:none'>Id Factura</th>");
                shtml.Append("<th class='k-header' style='width:120px;display:none'>Id Licencia</th>");
                //shtml.Append("<th class='k-header' style='width: 25px;padding-left:10px'></th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='width:150px'>Licencia</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='width:350px'>Modalidad</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='width:350px'>Establecimiento</th>");
                //shtml.Append("<th class='k-header' style='width:120px'>Monto</th>");
                //shtml.Append("<th class='k-header' style='width:120px'>Descuento</th>");
                //shtml.Append("<th class='k-header' style='width:120px'>Impuesto</th>");
                //shtml.Append("<th class='k-header' style='width:120px'>Total</th>");

                if (LicenciaTmp != null && LicenciaTmp.Count > 0)
                {
                    foreach (var item in LicenciaTmp.OrderBy(x => x.codLicencia))
                    {
                        shtml.Append("<tr style='background-color:white'>");
                        shtml.AppendFormat("<td style='width:120px;display:none'>{0}</td>", item.codFactura);
                        shtml.AppendFormat("<td style='width:120px;display:none'>{0}</td>", item.codLicencia);
                        //shtml.AppendFormat("<td style='width:25px'> ");
                        //shtml.AppendFormat("<a href=# onclick='verDetaLicCon({0},{1});'><img id='expandLicCon" + item.codFactura + "-" + item.codLicencia + "'  src='../Images/botones/less.png'  width=20px     title='Ocultar detalle.' alt='ver detalle.' border=0></a>", item.codFactura.ToString(), item.codLicencia.ToString());
                        //shtml.Append("</td>");
                        shtml.AppendFormat("<td style='width:350px'>{0}</td>", item.nombreLicencia);
                        shtml.AppendFormat("<td style='width:350px'>{0}</td>", item.Modalidad);
                        shtml.AppendFormat("<td style='width:330px'>{0}</td>", item.Establecimiento);
                        //shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Monto));
                        //shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Descuento));
                        //shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Impuesto));
                        //shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:10px'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Total));
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarLicenciaConsulta", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarLicenciaPlanConsulta()
        {
            Resultado retorno = new Resultado();
            try
            {
                decimal totalPorPeriodoAcum = 0;
                StringBuilder shtml = new StringBuilder();

                //shtml.Append("<table border=0 width='100%;' class='k-grid k-widget' id='FiltroTabla'>");
                shtml.Append("<table  border=0 width='30%;' id='FiltroTabla'>");
                shtml.Append("<thead>");

                shtml.Append("<tr>");
                shtml.Append("<th class='k-header' style='display:none'>Id</th>");
                shtml.Append("<th class='k-header' style='display:none'>Id Licencia</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='text-align:center'>Periodo</th>");
                //shtml.Append("<th class='k-header' >Monto Bruto</th>");
                //shtml.Append("<th class='k-header' >Descuento</th>");
                //shtml.Append("<th class='k-header' >Impuesto</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Total Por Periodo</th>");
                if (LicenciaDetalleTmp != null && LicenciaDetalleTmp.Count > 0)
                {
                    foreach (var item in LicenciaDetalleTmp.OrderBy(x => x.codLicPlanificacion))
                    {
                        shtml.Append("<tr style='background-color:white'>");
                        shtml.AppendFormat("<td style='display:none'>{0}</td>", item.codFactura);
                        shtml.AppendFormat("<td style='display:none'>{0}</td>", item.codLicencia);
                        shtml.AppendFormat("<td style='text-align:center'>{0} - {1}</td>", item.anio.ToString(), item.mes);
                        //shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>S/.  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorBruto));
                        //shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>S/.  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorDescuento));
                        //shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>S/.  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorImpuesto));
                        shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px'>S/.  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorNeto));
                        shtml.Append("</td>");
                        shtml.Append("</tr>");
                        totalPorPeriodoAcum += item.valorNeto;
                    }
                }
                shtml.Append("<tr style='background-color:white'>");
                shtml.AppendFormat("<td style='display:none'>{0}</td>", 0);
                shtml.AppendFormat("<td style='display:none'>{0}</td>", 0);
                shtml.AppendFormat("<td style='text-align:center;font-weight:bold'>{0}</td>", "TOTAL");
                shtml.AppendFormat("<td style='width:100px;text-align:right;  padding-right:10px;font-weight:bold'>S/. {0}</td>", string.Format("{0:# ### ### ##0.##########}", totalPorPeriodoAcum));
                shtml.Append("</td>");
                shtml.Append("</tr>");

                shtml.Append("</table>");
                retorno.message = shtml.ToString();
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarLicenciaPlanConsulta", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ValidarSerie(decimal idfactura, decimal serie,string des_serie)
        {
            Resultado retorno = new Resultado();
            try
            {
                if(des_serie=="F333" || des_serie == "B333")
                {
                    retorno.result = 2;
                    retorno.message = "La serie seleccionada es valida solo para Facturas Online.";
                }
                else
                {
                    var factura = new BLFactura().ObtenerTipoDocumento(idfactura);
                    var tipo = new BLFactura().ObtenerTipoComprobante(serie);

                    // SI ES DNI Y TIPO DE COMPROBANTE ES FACTURA
                    if (factura.FirstOrDefault().TAXT_ID == 2 && tipo.FirstOrDefault().TIPO_FACT == "FC")
                    {
                        retorno.result = 2;
                        retorno.message = "El Tipo de Documento y/o serie seleccionada es incorrecta para una Persona Natural.";
                    }
                    else
                    {
                        retorno.result = 1;
                    }
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

        #endregion

        #region ReporteFacturacion
        //public ActionResult ReporteFacturacion(string serie, decimal id, string glosa,string imp)
        public ActionResult ReporteFacturacion(string serie, decimal id, int tipo, string numero, string fecha, string glosa, string imp)
        {
            //Init(false);//add sysseg
            Resultado retorno = new Resultado();
            string format = "PDF";
            //decimal Total = Convert.ToDecimal(imp);
            try
            {
                var FechaEmision = DateTime.Now.ToString("yyyy-MM-dd");

                if (imp == "0")
                {
                    imp = Convert.ToString(BorFacturacionMasivaTmp.FirstOrDefault().valorFinal);
                    //imp = String.Format("{0:N0}", BorFacturacionMasivaTmp.FirstOrDefault().valorFinal).Replace(",", "");
                }
                else
                {
                    String.Format("{0:N4}", imp);
                }


                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reportes/RptFacturaElectronica.rdlc");

                List<BECabeceraFactura> listaCab = new List<BECabeceraFactura>();
                listaCab = new BLCabeceraFactura().ListarCabeceraPreview(GlobalVars.Global.OWNER, id);
                //total = listaCab.FirstOrDefault().MntTotal;

                var correlativo = new BLCabeceraFactura().ObtenerCorrelativo(GlobalVars.Global.OWNER, serie);

                List<BEDetalleFactura> listaDet = new List<BEDetalleFactura>();
                listaDet = new BLDetalleFactura().ListarDetalleFactura(GlobalVars.Global.OWNER, id);

                if (listaCab != null && listaDet != null && listaCab.Count > 0 && listaDet.Count > 0)
                {
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = listaCab;
                    localReport.DataSources.Add(reportDataSource);

                    ReportDataSource reportDataSourceDet = new ReportDataSource();
                    reportDataSourceDet.Name = "DataSet2";
                    reportDataSourceDet.Value = listaDet;
                    localReport.DataSources.Add(reportDataSourceDet);

                    ReportParameter parametro = new ReportParameter();
                    parametro = new ReportParameter("Monto_Letra", Util.NumeroALetras(imp.ToString()));
                    localReport.SetParameters(parametro);

                    ReportParameter parametroSerie = new ReportParameter();
                    parametroSerie = new ReportParameter("Serie", serie);
                    localReport.SetParameters(parametroSerie);

                    ReportParameter parametroCorrelativo = new ReportParameter();
                    ReportParameter parametroFecha = new ReportParameter();
                    if (tipo == 1)
                    {
                        parametroCorrelativo = new ReportParameter("Correlativo", correlativo.FirstOrDefault().Correlativo);
                        parametroFecha = new ReportParameter("FechaEmision", FechaEmision);
                    }
                    else
                    {
                        parametroCorrelativo = new ReportParameter("Correlativo", Convert.ToString(numero));
                        parametroFecha = new ReportParameter("FechaEmision", Convert.ToString(fecha));
                    }

                    localReport.SetParameters(parametroCorrelativo);
                    localReport.SetParameters(parametroFecha);

                    ReportParameter parametroGlosa = new ReportParameter();
                    parametroGlosa = new ReportParameter("Glosa", glosa);
                    localReport.SetParameters(parametroGlosa);

                    ReportParameter parametroImporte = new ReportParameter();
                    parametroImporte = new ReportParameter("Importe", imp);
                    localReport.SetParameters(parametroImporte);

                    string reportType = format;
                    string mimeType;
                    string encoding;
                    string fileNameExtension;

                    //The DeviceInfo settings should be changed based on the reportType            
                    //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
                    string deviceInfo = "<DeviceInfo>" +
                        "  <OutputFormat>" + format + "</OutputFormat>" +
                        "  <PageWidth>8.5in</PageWidth>" +
                        "  <PageHeight>11in</PageHeight>" +
                        "  <MarginTop>0.5in</MarginTop>" +
                        "  <MarginLeft>0.5in</MarginLeft>" +
                        "  <MarginRight>0.5in</MarginRight>" +
                        "  <MarginBottom>0.5in</MarginBottom>" +
                        "</DeviceInfo>";

                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;

                    renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.result = 1;

                    localReport.DisplayName = "Reporte Consulta de Factura";
                    if (format == null)
                    {
                        return File(renderedBytes, "image/jpeg");
                    }
                    else if (format == "PDF")
                    {
                        return File(renderedBytes, mimeType);
                    }
                    else if (format == "EXCEL")
                    {
                        return File(renderedBytes, mimeType);
                    }
                    else
                    {
                        return File(renderedBytes, "image/jpeg");
                    }
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;

                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Reporte", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region VALIDACION
        public JsonResult ValidarSerieNumero(decimal idSerie, decimal numero)
        {
            Resultado retorno = new Resultado();
            try
            {

                if (!isLogout(ref retorno))
                {
                    var datos = new BLREC_NUMBERING().ValidarSerieNumero(idSerie, numero);
                    if (datos == 0)
                    {
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ValidarUbigeoXOficia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public JsonResult LimpiarBorradoresxLicencia(decimal LIC_ID)
        {
            Resultado retorno = new Resultado();
            try
            {
                //decimal off_id = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                var id = new BLFactura().LimpiarBorradorexLicencia(LIC_ID);
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


        public JsonResult ValidaFechaManual(string FechaSelect)
        {
            Resultado retorno = new Resultado();
            try
            {
                int dias_minimos = new BLFactura().ObtenerDiaMinimoFechaManual();


                var fecha_sistema = FechaSistema;

                var dia_actual = fecha_sistema.Day;
                var mes_actual = fecha_sistema.Month;
                var anio_Actual = fecha_sistema.Year;

                var mes_select = Convert.ToDateTime(FechaSelect).Month;
                var dia_select = Convert.ToDateTime(FechaSelect).Day;

                if (mes_actual == mes_select && dia_actual - dia_select <= dias_minimos)
                {

                    retorno.result = 1; // permite proseguir
                    //retorno.message = "";

                }
                else
                {
                    retorno.result = 2; // no se cumplio con la validacion de Fecha .
                    retorno.message = "NO SE CUMPLIO CON LA VALIDACION DE FECHA PERMITIDA | MINIMO " + dias_minimos.ToString() + " Y DENTRO DEL MES ACTUAL |";
                }

            }
            catch (Exception ex)
            {
                retorno.result = 0; // hubo un error 
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarSerieTipoSocio(string idSerie, decimal documento)
        {
            Resultado retorno = new Resultado();
            try
            {

                if (!isLogout(ref retorno))
                {
                    retorno.result= new BLREC_NUMBERING().ValidarSerieTipoSocio(idSerie, documento);

                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ValidarUbigeoXOficia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


    }
}

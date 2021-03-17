using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;
using System.Text.RegularExpressions;

namespace Proyect_Apdayc.Controllers.Recaudacion
{
    public class CobroController : Base
    {
        // 
        // GET: /Cobro/
        public const string NomAplicacion = "SRGDA";

        public ActionResult Index()
        {
            Init(false);
            Session.Remove(K_SESION_BOR_FACTURACION);
            Session.Remove(K_SESION_BOR_LICENCIA);
            Session.Remove(K_SESION_BOR_LICENCIA_DETALLE);
            Session.Remove(K_SESION_RECIBOS_PENDIENTES);
            Session.Remove(K_SESION_METODO_PAGO_DET);
            Session.Remove(K_SESION_METODO_PAGO);
            Session.Remove(K_SESION_METODO_PAGO_APLICAR);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            return View();
        }

        #region Sesion
        private const string K_SESION_BOR_FACTURACION = "___DTOBorFacturacion";
        private const string K_SESION_BOR_LICENCIA = "___DTOBorLicencia";
        private const string K_SESION_BOR_LICENCIA_DETALLE = "___DTOBorLicenciaDetalle";
        private const string K_SESION_RECIBOS_PENDIENTES = "___DTOBorLicenciaRecibosPendientes";
        private const string K_SESION_METODO_PAGO_DET = "___DTODetalleMetodoPago";
        private const string K_SESION_METODO_PAGO = "___DTOMetodoPago";
        private const string K_SESION_METODO_PAGO_APLICAR = "___DTODetalleMetodoPagoAplicar";
        List<DTOFactura> borfacturacionMasiva = new List<DTOFactura>();
        List<DTOLicencia> borlicenciaMasiva = new List<DTOLicencia>();
        List<DTOFacturaDetallle> borlicenciaDetalleMasiva = new List<DTOFacturaDetallle>();
        List<DTORecibo> recibosPendientes = new List<DTORecibo>();
        List<DTODetalleMetodoPago> detalleMetodopago = new List<DTODetalleMetodoPago>();
        List<DTODetalleMetodoPago> metodopago = new List<DTODetalleMetodoPago>();
        List<DTODetalleMetodoPago> detalleMetodopagoAplicar = new List<DTODetalleMetodoPago>();

        public List<DTOFactura> BorFacturacionTmp
        {
            get
            {
                return (List<DTOFactura>)Session[K_SESION_BOR_FACTURACION];
            }
            set
            {
                Session[K_SESION_BOR_FACTURACION] = value;
            }
        }

        public List<DTOLicencia> BorLicenciaTmp
        {
            get
            {
                return (List<DTOLicencia>)Session[K_SESION_BOR_LICENCIA];
            }
            set
            {
                Session[K_SESION_BOR_LICENCIA] = value;
            }
        }

        public List<DTOFacturaDetallle> BorLicenciaDetalleTmp
        {
            get
            {
                return (List<DTOFacturaDetallle>)Session[K_SESION_BOR_LICENCIA_DETALLE];
            }
            set
            {
                Session[K_SESION_BOR_LICENCIA_DETALLE] = value;
            }
        }

        public List<DTORecibo> RecibosPendientesTmp
        {
            get
            {
                return (List<DTORecibo>)Session[K_SESION_RECIBOS_PENDIENTES];
            }
            set
            {
                Session[K_SESION_RECIBOS_PENDIENTES] = value;
            }
        }

        public List<DTODetalleMetodoPago> DetalleMetodoPagoTmp
        {
            get
            {
                return (List<DTODetalleMetodoPago>)Session[K_SESION_METODO_PAGO_DET];
            }
            set
            {
                Session[K_SESION_METODO_PAGO_DET] = value;
            }
        }

        public List<DTODetalleMetodoPago> MetodoPagoTmp
        {
            get
            {
                return (List<DTODetalleMetodoPago>)Session[K_SESION_METODO_PAGO];
            }
            set
            {
                Session[K_SESION_METODO_PAGO] = value;
            }
        }

        public List<DTODetalleMetodoPago> DetalleMetodoPagoAplicarTmp
        {
            get
            {
                return (List<DTODetalleMetodoPago>)Session[K_SESION_METODO_PAGO_APLICAR];
            }
            set
            {
                Session[K_SESION_METODO_PAGO_APLICAR] = value;
            }
        }
        #endregion

        [HttpPost()]
        public JsonResult ListarFacturacionPendienteCobro(decimal usuDerecho, decimal serie, decimal numero)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var idFacTodos = 0;
                    var idRecTodos = 0;
                    var facturapedpago = new BLFacturaCobro().ListarFacturaPendientePago(GlobalVars.Global.OWNER, usuDerecho,
                                                                                        serie, numero, idFacTodos, idRecTodos);

                    if (facturapedpago.ListarFactura != null)
                    {
                        borfacturacionMasiva = new List<DTOFactura>();
                        facturapedpago.ListarFactura.ForEach(s =>
                        {
                            borfacturacionMasiva.Add(new DTOFactura
                            {
                                id = s.INV_ID,
                                idFecha = s.INV_EXPID,
                                fecha_venc = s.INV_EXPDATE,
                                valorBase = s.INV_BASE,
                                valoImpuesto = s.INV_TAXES,
                                valorFinal = s.INV_NET,
                                tipo = s.INVT_DESC,
                                saldoPendiente = s.INV_BALANCE,
                                estadopago = s.estadosPago,
                                serial = s.NMR_SERIAL,
                                numFactura = s.INV_NUMBER.ToString()
                            });
                        });
                        BorFacturacionTmp = borfacturacionMasiva;
                    }

                    if (facturapedpago.ListarLicencia != null)
                    {
                        borlicenciaMasiva = new List<DTOLicencia>();
                        facturapedpago.ListarLicencia.ForEach(s =>
                        {
                            borlicenciaMasiva.Add(new DTOLicencia
                            {
                                codFactura = s.INV_ID,
                                codLicencia = s.LIC_ID,
                                nombreLicencia = s.LIC_NAME,
                                Modalidad = s.Modalidad,
                                Establecimiento = s.Establecimiento,

                                Monto = s.INVL_BASE,
                                Impuesto = s.INVL_TAXES,
                                SubTotal = s.INVL_NET,
                                Pendiente = s.INVL_BALANCE
                            });
                        });
                        BorLicenciaTmp = borlicenciaMasiva;
                    }

                    if (facturapedpago.ListarDetalleFactura != null)
                    {
                        borlicenciaDetalleMasiva = new List<DTOFacturaDetallle>();
                        facturapedpago.ListarDetalleFactura.ForEach(s =>
                        {
                            borlicenciaDetalleMasiva.Add(new DTOFacturaDetallle
                            {
                                Id = s.INVL_ID,
                                codFactura = s.INV_ID,
                                codLicencia = s.LIC_ID,
                                FechaPlanificacion = s.LIC_DATE,

                                valorBase = s.INVL_BASE,
                                valorImpuesto = s.INVL_TAXES,
                                valorNeto = s.INVL_NET
                            });
                        });
                        BorLicenciaDetalleTmp = borlicenciaDetalleMasiva;
                    }

                    if (facturapedpago.ListarRecibosPendientes != null)
                    {
                        recibosPendientes = new List<DTORecibo>();
                        facturapedpago.ListarRecibosPendientes.ForEach(s =>
                        {
                            recibosPendientes.Add(new DTORecibo
                            {
                                Id = s.REC_ID,
                                IdUsuDerecho = s.BPS_ID,
                                Serie = s.SERIE,
                                FechaRegistro = s.REC_DATE,
                                Base = s.REC_TBASE,
                                Total = s.REC_TTOTAL,
                                Observacion = s.REC_OBSERVATION
                            });
                        });
                        RecibosPendientesTmp = recibosPendientes;
                    }

                    if (facturapedpago.ListarDetalleRecibosPedientes != null)
                    {
                        detalleMetodopago = new List<DTODetalleMetodoPago>();
                        facturapedpago.ListarDetalleRecibosPedientes.ForEach(s =>
                            {
                                detalleMetodopago.Add(new DTODetalleMetodoPago
                                    {
                                        Id = s.REC_PID,
                                        IdRecibo = s.REC_ID,
                                        IdMetodoPago = s.REC_PWID,
                                        MetodoPago = s.REC_PWDESC,
                                        IdBanco = s.BNK_ID,
                                        Banco = s.BNK_NAME,
                                        IdSucursal = s.BRCH_ID,
                                        Sucursal = s.BRCH_NAME,
                                        FechaDeposito = s.REC_DATEDEPOSITE,
                                        Voucher = s.REC_REFERENCE,
                                        ValorIgreso = s.REC_PVALUE
                                    });
                            });
                        DetalleMetodoPagoTmp = detalleMetodopago;
                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(facturapedpago, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarFacturacionPendienteCobro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult ObtenerDetalleFormaPago(decimal idRecibo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (DetalleMetodoPagoAplicarTmp != null) DetalleMetodoPagoAplicarTmp = new List<DTODetalleMetodoPago>();
                    var detallePago = new BLDetalleMetodosPago().ListarMetodoPago(GlobalVars.Global.OWNER, idRecibo);

                    if (detallePago != null)
                    {
                        detalleMetodopagoAplicar = new List<DTODetalleMetodoPago>();

                        foreach (var item in detallePago)
                        {
                            var obj = new DTODetalleMetodoPago();
                            obj.Id = item.REC_PID;
                            obj.IdRecibo = item.REC_ID;
                            obj.IdMetodoPago = item.REC_PWID;
                            obj.MetodoPago = item.REC_PWDESC;
                            obj.IdBanco = item.BNK_ID;
                            obj.Banco = item.BNK_NAME;
                            obj.IdSucursal = item.BRCH_ID;
                            obj.Sucursal = item.BRCH_NAME;
                            obj.FechaDeposito = item.REC_DATEDEPOSITE;
                            obj.Voucher = item.REC_REFERENCE;
                            obj.ValorIgreso = item.REC_PVALUE;

                            detalleMetodopagoAplicar.Add(obj);
                        }
                        DetalleMetodoPagoAplicarTmp = detalleMetodopagoAplicar;

                        retorno.data = Json(detalleMetodopagoAplicar, JsonRequestBehavior.AllowGet);
                        retorno.message = "Detalle método pago encontrado";
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.message = "No tiene detalle, método pago";
                        retorno.result = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Obtener datos establecimiento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GrabarRecibo(BERecibo en)
        {
            Resultado retorno = new Resultado();
            int IdRecibo = 0;
            try
            {
                if (!isLogout(ref retorno))
                {
                    BERecibo obj = new BERecibo();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.REC_ID = en.REC_ID;
                    obj.NMR_ID = en.NMR_ID;
                    obj.REC_NUMBER = en.REC_NUMBER;
                    obj.BPS_ID = en.BPS_ID;
                    obj.REC_TBASE = en.REC_TBASE;
                    obj.REC_TTOTAL = en.REC_TTOTAL;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    obj.LOG_USER_UPDATE = UsuarioActual;
                    obj.MetodoPago = obtenerMetodosPago();
                    if (obj.REC_ID == 0)
                    {
                        IdRecibo = new BLRecibo().Insertar(obj);
                    }
                    else
                    {
                        IdRecibo = new BLRecibo().Actualizar(obj);
                    }
                    retorno.result = 1;
                    retorno.data = Json(IdRecibo, JsonRequestBehavior.AllowGet);
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Grabar Recibo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BEDetalleMetodoPago> obtenerMetodosPago()
        {
            List<BEDetalleMetodoPago> datos = new List<BEDetalleMetodoPago>();

            if (MetodoPagoTmp != null)
            {
                MetodoPagoTmp.ForEach(x =>
                {
                    datos.Add(new BEDetalleMetodoPago
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        //REC_ID
                        REC_PID = x.Id,
                        REC_PWID = x.IdMetodoPago,
                        REC_PVALUE = x.ValorIgreso,
                        REC_CONFIRMED = x.ConfirmacionIngreso,
                        REC_DATEDEPOSITE = x.FechaDeposito,
                        BNK_ID = x.IdBanco,
                        BRCH_ID = x.IdSucursal,
                        BACC_NUMBER = x.IdCuentaBancaria,
                        REC_REFERENCE = x.Voucher,
                        LOG_USER_CREAT = UsuarioActual
                    });
                });
            }
            return datos;
        }

        [HttpPost]
        public JsonResult GrabarDetalleMetodoPago(BEDetalleMetodoPago en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEDetalleMetodoPago obj = new BEDetalleMetodoPago();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.REC_ID = en.REC_ID;
                    obj.REC_PWID = en.REC_PWID;
                    obj.REC_PVALUE = en.REC_PVALUE;
                    var valor = new BLDetalleMetodosPago().ObtenerConfirmed(obj.OWNER, obj.REC_PWID).Confirmed;
                    obj.REC_CONFIRMED = valor.ToString();
                    obj.REC_DATEDEPOSITE = en.REC_DATEDEPOSITE;
                    obj.BNK_ID = en.BNK_ID;
                    obj.BRCH_ID = en.BRCH_ID;
                    obj.BACC_NUMBER = en.BACC_NUMBER;
                    obj.REC_REFERENCE = en.REC_REFERENCE;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    var datos = new BLDetalleMetodosPago().Insertar(obj);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Grabar detalle método de pago", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region ListarFacturaConDetalle
        [HttpPost]
        public JsonResult ListarFacturaCabecera(decimal idFactura = 0)
        {
            borfacturacionMasiva = BorFacturacionTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    //onMouseover="this.bgColor='#EEEEEE'"
                    //onMouseout="this.bgColor='#FFFFFF'"
                    StringBuilder shtml = new StringBuilder();
                    var clase = "'ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'";
                    shtml.Append("<table class='k-grid k-widget' border=0 width='100%;' id='tblFactura'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class=" + clase + " style='width:20px'></th>");
                    //shtml.Append("<th class=" + clase + " style='width:20px'></th>");
                    shtml.Append("<th class=" + clase + " style='width:40px'>Fact.</th>");
                    shtml.Append("<th class=" + clase + " style='width:250px'>Estado</th>");
                    shtml.Append("<th class=" + clase + " style='width:80px'>Tipo</th>");
                    shtml.Append("<th class=" + clase + " style='width:80px'>Serie</th>");
                    shtml.Append("<th class=" + clase + " style='width:70px'>F. venc.</th>");
                    shtml.Append("<th class=" + clase + " style='width:120px'>Importe</th>");
                    shtml.Append("<th class=" + clase + " style='width:120px'>Impuesto</th>");
                    shtml.Append("<th class=" + clase + " style='width:120px'>Total</th>");
                    shtml.Append("<th class=" + clase + " style='width:120px'>Saldo Pendiente</th>");

                    if (borfacturacionMasiva.Count > 0)
                    {
                        foreach (var item in borfacturacionMasiva.OrderBy(x => x.id))
                        {
                            shtml.Append("<tr style='background-color:white'>");
                            //shtml.AppendFormat("<td style='text-align:center;width:25px'><input type='checkbox' id='{0}' /></td>", "chkFact" + item.id);
                            shtml.AppendFormat("<td style='width:25px; cursor:pointer;'>");
                            shtml.AppendFormat("<a href=# onclick='verDeta({0});'><img id='expand" + item.id + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.id);
                            shtml.Append("</td>");

                            shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.id);

                            if (item.estadopago == "CANCELADA PARCIAL")
                                shtml.AppendFormat("<td style='cursor:pointer;'><font color='red'>{0}</font></td>", item.estadopago);
                            else
                                shtml.AppendFormat("<td style='cursor:pointer;'><font color='Blue'>{0}</font></td>", item.estadopago);

                            shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.tipo);
                            shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.serial + " - " + item.numFactura);
                            shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.fecha_venc), item.id);
                            shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:20px; cursor:pointer;'>S/. {0}</td>", item.valorBase.ToString("# ### ###.00"), item.id);
                            shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:15px; cursor:pointer;'>S/. {0}</td>", item.valoImpuesto.ToString("# ### ###.00"), item.id);
                            shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:20px; cursor:pointer;'>S/. {0}</td>", item.valorFinal.ToString("# ### ###.00"), item.id);
                            shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:20px'>S/. {0}</td>", item.saldoPendiente.ToString("# ### ###.00"));
                            //shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.estado_factura, item.id);
                            shtml.Append("</tr>");
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td colspan='10'>");
                            shtml.Append("<div style='display:none;' id='" + "div" + item.id.ToString() + "'  > ");
                            shtml.Append(getHtmlTableDetaLicenciaBorrador(item.id));
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        }
                    }
                    else
                    {
                        shtml.Append("<tr id='trMesanje' style='background-color:white'><td colspan=14><b><center>No existen facturas pendientes de pago.</center></b></td></tr>");
                    }

                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarFacturaCabecera", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public StringBuilder getHtmlTableDetaLicenciaBorrador(decimal codFact)
        {
            var licencias = BorLicenciaTmp.Where(p => p.codFactura == codFact).ToList();

            StringBuilder shtml = new StringBuilder();
            shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
            shtml.Append("<thead>");

            shtml.Append("<tr>");
            shtml.Append("<th class='k-header' style='width:120px;display:none'>Id Factura</th>");
            shtml.Append("<th class='k-header' style='width:120px;display:none'>Id Licencia</th>");
            shtml.Append("<th class='k-header' style='width: 25px; padding-left:10px; cursor:pointer;'></th>");
            shtml.Append("<th class='k-header' style='width:150px; cursor:pointer;'>Licencia</th>");
            shtml.Append("<th class='k-header' style='width:350px; cursor:pointer;'>Modalidad</th>");
            shtml.Append("<th class='k-header' style='width:350px; cursor:pointer;'>Establecimiento</th>");
            shtml.Append("<th class='k-header' style='width:130px; cursor:pointer;'>Monto</th>");
            shtml.Append("<th class='k-header' style='width:130px; cursor:pointer;'>Impuesto</th>");
            shtml.Append("<th class='k-header' style='width:130px; cursor:pointer;'>SubTotal</th>");
            shtml.Append("<th class='k-header' style='width:130px; cursor:pointer;'>Saldo Pendiente</th>");

            if (licencias != null && licencias.Count > 0)
            {
                foreach (var item in licencias.OrderBy(x => x.codLicencia))
                {
                    //Random r = new Random();
                    //int val = r.Next();

                    shtml.Append("<tr style='background-color:white'>");
                    shtml.AppendFormat("<td style='width:120px;display:none'>{0}</td>", item.codFactura);
                    shtml.AppendFormat("<td style='width:120px;display:none'>{0}</td>", item.codLicencia);
                    shtml.AppendFormat("<td style='width:25px'> ");
                    shtml.AppendFormat("<a href=# onclick='verDetaLic({0});'><img id='expandLic" + item.codFactura + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.codFactura);
                    shtml.Append("</td>");
                    shtml.AppendFormat("<td style='width:350px; cursor:pointer;'>{0}</td>", item.nombreLicencia);
                    shtml.AppendFormat("<td style='width:350px; cursor:pointer;'>{0}</td>", item.Modalidad);
                    shtml.AppendFormat("<td style='width:330px; cursor:pointer;'>{0}</td>", item.Establecimiento);
                    shtml.AppendFormat("<td style='width:130px; text-align:right; cursor:pointer; padding-right:20px'>S/. {0}</td>", item.Monto.ToString("### ###.00"));
                    shtml.AppendFormat("<td style='width:130px; text-align:right; cursor:pointer; padding-right:20px'>S/. {0}</td>", item.Impuesto.ToString("### ###.00"));
                    shtml.AppendFormat("<td style='width:130px; text-align:right; cursor:pointer; padding-right:20px'>S/. {0}</td>", item.SubTotal.ToString("### ###.00"));
                    shtml.AppendFormat("<td style='width:130px; text-align:right; cursor:pointer; padding-right:20px'>S/. {0}</td>", item.Pendiente.ToString("### ###.00"));
                    shtml.Append("</td>");
                    shtml.Append("</tr>");

                    shtml.Append("<tr style='background-color:white; cursor:pointer;'>");
                    shtml.Append("<td></td>");
                    shtml.Append("<td></td>");
                    shtml.Append("<td></td>");
                    shtml.Append("<td colspan='6'>");
                    //shtml.Append("<div style='display:none;' id='" + "divLic" + item.codLicencia.ToString() + "'  > ");
                    shtml.Append("<div style='display:none;' id='" + "divLic" + item.codFactura + "'  > ");
                    //shtml.Append(getHtmlTableDetaLicPlanBorrador(item.codLicencia));
                    shtml.Append(getHtmlTableDetaLicPlanBorrador(item.codFactura));
                    shtml.Append("</div>");
                    shtml.Append("</td>");
                    shtml.Append("</tr>");
                }
            }
            shtml.Append("</table>");
            return shtml;
        }

        public StringBuilder getHtmlTableDetaLicPlanBorrador(decimal codLic)
        {
            var detalle = BorLicenciaDetalleTmp.Where(p => p.codFactura == codLic).ToList();

            StringBuilder shtml = new StringBuilder();

            shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
            shtml.Append("<thead>");

            shtml.Append("<tr>");
            shtml.Append("<th class='k-header' style='display:none'>Id</th>");
            shtml.Append("<th class='k-header' style='display:none'>Id Licencia</th>");
            shtml.Append("<th class='k-header' style='text-align:center'>Fecha</th>");
            shtml.Append("<th class='k-header' >Monto</th>");
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
                    shtml.AppendFormat("<td style='width:120px;text-align:right; padding-right:10px; cursor:pointer;'>S/. {0}</td>", item.valorBase.ToString("### ###.00"));
                    shtml.AppendFormat("<td style='width:120px;text-align:right; padding-right:10px; cursor:pointer;'>S/. {0}</td>", item.valorImpuesto.ToString("### ###.00"));
                    shtml.AppendFormat("<td style='width:120px;text-align:right; padding-right:10px; cursor:pointer;'>S/. {0}</td>", item.valorNeto.ToString("### ###.00"));
                    shtml.Append("</td>");
                    shtml.Append("</tr>");
                }
            }
            shtml.Append("</table>");
            return shtml;
        }
        #endregion

        #region ListarRecibosPendientesConDetalle
        [HttpPost]
        public JsonResult ListarRecibosPendientes()
        {
            recibosPendientes = RecibosPendientesTmp;

            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    var clase = "'ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'";
                    shtml.Append("<table class='k-grid k-widget' border=0 width='100%;' id='tblRecibo'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class=" + clase + " style='width:5px'></th>");
                    shtml.Append("<th class=" + clase + " style='width:50px'>Recibo</th>");
                    shtml.Append("<th class=" + clase + " style='width:50px; display:none;'>IdUsuDerecho</th>");
                    shtml.Append("<th class=" + clase + " style='width:60px'>Serie-Nro</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px'>Fecha Reg.</th>");
                    shtml.Append("<th class=" + clase + " style='width:400px'>Observación</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px'>Base</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px'>Impuesto</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px'>Total</th>");

                    if (recibosPendientes.Count > 0)
                    {
                        foreach (var item in recibosPendientes.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            shtml.AppendFormat("<td style='width:10px'> ");
                            shtml.AppendFormat("<a href=# onclick='verDetaMetodosPago({0});'><img id='expandDet" + item.Id + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.Id);
                            shtml.Append("</td>");
                            shtml.AppendFormat("<td onMouseOver='return color1()'  onMouseOut='return color2()'  onclick='return obtenerId({0});' style='cursor:pointer; text-align:center;'>{0}</td>", item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerId({1},{0});' style='cursor:pointer; text-align:center; display:none;'>{0}</td>", item.IdUsuDerecho, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerId({1},{2});' style='cursor:pointer; text-align:center;'>{0}</td>", item.Serie, item.Id, item.IdUsuDerecho);
                            shtml.AppendFormat("<td onclick='return obtenerId({1},{2});' style='cursor:pointer; text-align:center;'>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FechaRegistro), item.Id, item.IdUsuDerecho);
                            shtml.AppendFormat("<td onclick='return obtenerId({1},{2});'>{0}</td>", item.Observacion, item.Id, item.IdUsuDerecho);
                            shtml.AppendFormat("<td onclick='return obtenerId({1},{2});' style='width:120px;text-align:right;  padding-right:20px; cursor:pointer;'>{0}</td>", item.Base, item.Id, item.IdUsuDerecho);
                            shtml.AppendFormat("<td onclick='return obtenerId({1},{2});' style='width:120px;text-align:right;  padding-right:20px; cursor:pointer;'>{0}</td>", "", item.Id, item.IdUsuDerecho);
                            shtml.AppendFormat("<td onclick='return obtenerId({1},{2});' style='width:120px;text-align:right;  padding-right:20px; cursor:pointer;'>{0}</td>", item.Total, item.Id, item.IdUsuDerecho);
                            shtml.Append("</td>");
                            shtml.Append("</tr>");

                            shtml.Append("<tr style='background-color:white'>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td colspan='10'>");
                            shtml.Append("<div style='display:none;' id='" + "div" + item.Id.ToString() + "'  > ");
                            shtml.Append(ListarDetalleFormaPagoReciboPendientes(item.Id));
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        }
                    }
                    else
                    {
                        shtml.Append("<tr style='background-color:white'><td colspan=14><b><center>No existen recibos pendientes por aplicar.</center></b></td></tr>");
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarRecibosPendientes", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public StringBuilder ListarDetalleFormaPagoReciboPendientes(decimal idRecibo = 0)
        {
            var detalle = DetalleMetodoPagoTmp.Where(p => p.IdRecibo == idRecibo).ToList();

            REF_CURRENCY_VALUES en = new REF_CURRENCY_VALUES();
            StringBuilder shtml = new StringBuilder();
            var clase = "'ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'";
            shtml.Append("<table class='k-grid k-widget' border=0 width='100%;' id='tblMetodosPago'>");
            shtml.Append("<thead><tr>");
            shtml.Append("<th class=" + clase + " style='width:50px'>Forma de pago</th>");
            shtml.Append("<th class=" + clase + " style='width:30px'>Entidad</th>");
            shtml.Append("<th class=" + clase + " style='width:30px'>Fecha Operación</th>");
            shtml.Append("<th class=" + clase + " style='width:50px'>Voucher / N Ope. - Tarj</th>");
            shtml.Append("<th class=" + clase + " style='width:10px;'>Importe</th>");

            if (detalle != null && detalle.Count > 0)
            {
                foreach (var item in detalle.OrderBy(x => x.Id))
                {
                    shtml.Append("<tr style='background-color:white'>");
                    shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.MetodoPago);
                    shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.Banco);
                    shtml.AppendFormat("<td style='cursor:pointer; text-align:center;'>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FechaDeposito));
                    shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.Voucher, item.Voucher);
                    shtml.AppendFormat("<td style='text-align:right; padding-right:20px'>S/. {0}</td>", item.ValorIgreso);
                    shtml.Append("</td>");
                    shtml.Append("</tr>");
                }
            }
            shtml.Append("</table>");
            return shtml;
        }
        #endregion

        [HttpPost]
        public JsonResult ListarFormaPago()
        {
            metodopago = MetodoPagoTmp;

            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    REF_CURRENCY_VALUES en = new REF_CURRENCY_VALUES();
                    StringBuilder shtml = new StringBuilder();
                    var clase = "'ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'";
                    shtml.Append("<table class='k-grid k-widget' border=0 width='100%;' id='tblMetodosPago'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class=" + clase + " style='width:30px'>Forma de pago</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px'>Entidad</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px'>Fecha Operación</th>");
                    shtml.Append("<th class=" + clase + " style='width:50px'>Voucher / N Ope. - Tarj</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px'>Importe Rec.</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px; display:none'>Importe</th>");
                    shtml.Append("<th class=" + clase + " style='width:3px'></th>");

                    if (metodopago != null && metodopago.Count > 0)
                    {
                        foreach (var item in metodopago.OrderBy(x => x.Id))
                        {
                            //en = new BLDetalleMetodosPago().ObtenerTipoCambio(item.IdMoneda);
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.AppendFormat("<td>{0}</td>", item.MetodoPago);
                            shtml.AppendFormat("<td>{0}</td>", item.Banco);
                            shtml.AppendFormat("<td>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FechaDeposito));
                            shtml.AppendFormat("<td>{0}</td>", item.Voucher, item.Voucher);
                            shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:20px'>S/. {0}</td>", item.ValorIgreso);
                            shtml.AppendFormat("<td style='width:120px;text-aligwn:right;  padding-right:20px; display:none'>{0}</td>", item.ValorIgreso);
                            //shtml.AppendFormat("<td>{0}</td>", en.CUR_VALUE);
                            shtml.AppendFormat("<td style='width: 10px; text-align:center'><a href=# onclick='delDetalle({0},{3},{4});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, "delete.png", "Eliminar Detalle", item.ValorIgreso, item.IdRecibo);
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        }
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarFormaPago", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarFormaPagoAplicarFactura()
        {
            //var metodopago = DetalleMetodoPagoTmp;
            var metodopago = DetalleMetodoPagoAplicarTmp;

            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    REF_CURRENCY_VALUES en = new REF_CURRENCY_VALUES();
                    StringBuilder shtml = new StringBuilder();
                    var clase = "'ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'";
                    shtml.Append("<table class='k-grid k-widget' border=0 width='100%;' id='tblMetodosPago'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class=" + clase + " style='width:30px'>Forma de pago</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px'>Entidad</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px'>Fecha Operación</th>");
                    shtml.Append("<th class=" + clase + " style='width:50px'>Voucher / N Ope. - Tarj</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px'>Importe Rec.</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px; display:none'>Importe</th>");

                    if (metodopago != null && metodopago.Count > 0)
                    {
                        foreach (var item in metodopago.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.AppendFormat("<td>{0}</td>", item.MetodoPago);
                            shtml.AppendFormat("<td>{0}</td>", item.Banco);
                            shtml.AppendFormat("<td>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FechaDeposito));
                            shtml.AppendFormat("<td>{0}</td>", item.Voucher, item.Voucher);
                            shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:20px'>S/. {0}</td>", item.ValorIgreso);
                            shtml.AppendFormat("<td style='width:120px;text-aligwn:right;  padding-right:20px; display:none'>{0}</td>", item.ValorIgreso);
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        }
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarFormaPagoAplicarFactura", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddMetodoPago(DTODetalleMetodoPago entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    metodopago = MetodoPagoTmp;

                    if (metodopago == null) metodopago = new List<DTODetalleMetodoPago>();
                    if (Convert.ToInt32(entidad.Id) <= 0)
                    {
                        decimal nuevoId = 1;
                        if (metodopago.Count > 0) nuevoId = metodopago.Max(x => x.Id) + 1;
                        entidad.Id = nuevoId;
                        if (entidad.Banco == "--SELECCIONE--") entidad.Banco = string.Empty;
                        entidad.ConfirmacionIngreso = new BLDetalleMetodosPago().ObtenerConfirmed(GlobalVars.Global.OWNER, entidad.IdMetodoPago).Confirmed;
                        entidad.Activo = true;
                        entidad.EnBD = false;
                        entidad.UsuarioCrea = UsuarioActual;
                        entidad.FechaCrea = DateTime.Now;
                        metodopago.Add(entidad);
                    }
                    else
                    {
                        var item = metodopago.Where(x => x.Id == entidad.Id).FirstOrDefault();
                        entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                        entidad.Activo = item.Activo;
                        entidad.UsuarioCrea = item.UsuarioCrea;
                        entidad.FechaCrea = item.FechaCrea;
                        if (entidad.EnBD)
                        {
                            entidad.UsuarioModifica = UsuarioActual;
                            entidad.FechaModifica = DateTime.Now;
                        }
                        metodopago.Remove(item);
                        metodopago.Add(entidad);
                    }
                    MetodoPagoTmp = metodopago;
                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "AddMetodoPago", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarFacturaFormaPago(decimal? idFactura = 0)
        {
            borfacturacionMasiva = BorFacturacionTmp;
            var factura = BorFacturacionTmp;

            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    var clase = "'ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'";
                    shtml.Append("<table class='k-grid k-widget' border=0 width='100%;' id='tblFacturaPago'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class=" + clase + " style='width:10px'></th>");
                    shtml.Append("<th class=" + clase + " style='width:50px; display:none'>FacturaId</th>");
                    shtml.Append("<th class=" + clase + " style='width:80px'>Factura</th>");
                    shtml.Append("<th class=" + clase + " style='width:80px; display:none'>Id Fecha</th>");
                    shtml.Append("<th class=" + clase + " style='width:100px'>F. venc.</th>");
                    shtml.Append("<th class=" + clase + " style='width:150px'>Importe</th>");
                    shtml.Append("<th class=" + clase + " style='width:150px'>Impuesto</th>");
                    shtml.Append("<th class=" + clase + " style='width:150px'>Total</th>");
                    shtml.Append("<th class=" + clase + " style='width:100px'>Saldo Pendiente</th>");
                    shtml.Append("<th class=" + clase + " style='width:60px'>Valor Cancelar</th>");

                    if (factura != null && factura.Count > 0)
                    {
                        var i = 1;
                        foreach (var item in factura.OrderBy(x => x.id))
                        {
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.AppendFormat("<td onchange='return Habilitar({1});' style='text-align:center;width:25px'><input type='checkbox' id='{0}' /></td>", "chkFact" + item.id, item.id);
                            shtml.AppendFormat("<td style='display:none'><input type='text' id='txtFacturaId_{0}' value={1} style='width: 30px; text-align:center' readonly='true'></input></td>", item.id, item.id);
                            shtml.AppendFormat("<td>{0}</td>", item.id);
                            shtml.AppendFormat("<td style='display:none'><input type='text' id='txtIdFecha_{0}' value={1} style='width: 30px; text-align:center' readonly='true'></input></td>", item.id, item.idFecha);
                            shtml.AppendFormat("<td>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.fecha_venc), item.id);
                            shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:20px'>S/. {0}</td>", item.valorBase.ToString("# ### ###.00"));
                            shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:15px'>S/. {0}</td>", item.valoImpuesto.ToString("# ### ###.00"));
                            shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:20px'>S/. {0}</td>", item.valorFinal.ToString("# ### ###.00"));
                            shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:20px'>S/. {0}</td>", item.saldoPendiente.ToString("# ### ###.00")); //onkeyup='return calcularTotalFactura({0});' 
                            shtml.AppendFormat("<td style='text-align:center;'><input type='text' id='txtValorCancelar_{0}' value={1} style='width: 80px; text-align:right;' disabled='disabled' name='valor_{0}' class='elm'></input></td>", item.id, "0");
                            shtml.Append("</tr>");
                            i++;
                        }
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarFacturaFormaPago", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost] 
        //public JsonResult DellAddMetodoPago(decimal id, decimal Total, decimal idRecibo)
        //{
        //    Resultado retorno = new Resultado();

        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            //consulta si existe en bd
        //            BEDetalleMetodoPago en = new BEDetalleMetodoPago();
        //            en.OWNER = GlobalVars.Global.OWNER;
        //            en.REC_PID = id;
        //            en.LOG_USER_UPDATE = UsuarioActual;
        //            var dato = new BLDetalleMetodosPago().ObtenerDetalleEliminar(en);

        //            if (dato == 0)
        //            {
        //                detalleMetodopago = DetalleMetodoPagoTmp;
        //                if (detalleMetodopago != null)
        //                {
        //                    var objDel = detalleMetodopago.Where(x => x.Id == id).FirstOrDefault();
        //                    if (objDel != null)
        //                    {
        //                        detalleMetodopago.Remove(objDel);
        //                        DetalleMetodoPagoTmp = detalleMetodopago;
        //                        retorno.result = 1;
        //                        //retorno.result = 2;
        //                        retorno.message = "OK";
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                BERecibo er = new BERecibo();
        //                er.OWNER = GlobalVars.Global.OWNER;
        //                er.REC_ID = idRecibo;
        //                er.REC_TBASE = Total;
        //                er.REC_TTOTAL = Total;
        //                er.LOG_USER_UPDATE = UsuarioActual;
        //                var del = new BLDetalleMetodosPago().Eliminar(en, er);
        //                retorno.result = 1;
        //                retorno.message = "OK";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = ex.Message;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "DellAddMetodoPago", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult DellAddMetodoPago(decimal id, decimal Total, decimal idRecibo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    metodopago = MetodoPagoTmp;
                    if (metodopago != null)
                    {
                        var objDel = metodopago.Where(x => x.Id == id).FirstOrDefault();
                        if (objDel != null)
                        {
                            metodopago.Remove(objDel);
                            MetodoPagoTmp = metodopago;
                            retorno.result = 1;
                            retorno.message = "OK";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "DellAddMetodoPago", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneDatosRecibo(decimal idRecibo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLRecibo servicio = new BLRecibo();
                    var item = servicio.ObtenerDatos(GlobalVars.Global.OWNER, idRecibo);

                    if (item != null)
                    {
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(item, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se han encontrado datos";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "obtener datos del recibo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerFacturasAplicar(List<DTOFacturaAplicar> Factura)
        {
            Resultado retorno = new Resultado();
            int result = 0;
            try
            {
                if (Factura != null)
                {
                    List<DTOFacturaAplicar> FacturaAplicar = new List<DTOFacturaAplicar>();
                    List<DTOFactura> factura = new List<DTOFactura>();
                    foreach (var item in Factura)
                    {
                        if (item.TotalPagar != 0)
                        {
                            factura = BorFacturacionTmp.Where(p => p.id == item.Id).ToList();

                            foreach (var x in factura)
                            {
                                DTOFacturaAplicar dto = new DTOFacturaAplicar();
                                dto.Id = x.id;
                                dto.FechaExp = x.fecha_venc;
                                dto.Impuesto = x.valoImpuesto;
                                dto.Base = x.valorBase;
                                dto.Total = x.valorFinal;
                                dto.TotalPagar = item.TotalPagar;
                                dto.idFecha = item.idFecha;
                                dto.idRecibo = item.idRecibo;
                                FacturaAplicar.Add(dto);
                            }
                        }
                    }

                    foreach (var item in FacturaAplicar)
                    {
                        if (GlobalVars.Global.PagoReparto == "PROPORCIONAL")
                        {
                            var por_Base = ((item.Base * 100) / item.Total);
                            var por_Impuesto = ((item.Impuesto * 100) / item.Total);

                            var val_Recibo_base = (item.TotalPagar * (por_Base / 100));
                            var val_Recibo_Impuesto = (item.TotalPagar * (por_Impuesto / 100));

                            var total = (por_Base + por_Impuesto);
                            var totpagado = (val_Recibo_base + val_Recibo_Impuesto);

                            BEReciboDetalle enr = new BEReciboDetalle();
                            enr.OWNER = GlobalVars.Global.OWNER;
                            enr.REC_ID = item.idRecibo;
                            enr.INV_ID = item.Id;
                            enr.INV_EXPID = item.idFecha;
                            enr.REC_BASE = val_Recibo_base;
                            enr.REC_TAXES = val_Recibo_Impuesto;
                            enr.REC_TOTAL = totpagado;
                            enr.LOG_USER_CREAT = UsuarioActual;
                            enr.LOG_USER_UPDATE = UsuarioActual;
                            result = new BLRecibo().AplicarFactura_Proporcional(enr, item.Base, item.Impuesto);
                        }

                        else if (GlobalVars.Global.PagoReparto == "IMPUESTO-BASE")
                        {
                            bool ImpuestoCero = false;
                            BEFactura f = new BEFactura();

                            f = new BLFacturaCobro().ObtenerFacturaAplicar(GlobalVars.Global.OWNER, item.Id);
                            decimal _Base = 0;
                            decimal _Impuesto = 0;
                            decimal total = 0;
                            decimal NewRecTotal = 0;
                            decimal ImpuestoTotal = 0;

                            ImpuestoTotal = item.Impuesto;

                            if (f.INV_COLLECTB == 0) _Base = item.Base;
                            else _Base = (item.Base - f.INV_COLLECTB); NewRecTotal = f.INV_COLLECTB;

                            if (f.INV_COLLECTT == 0) _Impuesto = item.Impuesto;
                            else _Impuesto = (item.Impuesto - f.INV_COLLECTT);

                            if (f.INV_BALANCE == 0) total = item.Base + item.Impuesto;
                            else total = _Base + _Impuesto;


                            //impuesto queda 746
                            //pagamos 1000 
                            //se paga el impuesto con los 1000 y queda 254

                            //nuevo total 10446.000000

                            //REC_BASE = 254
                            //REC_TAXES= 746
                            //REC_TOTAL= 1000

                            //INV_NET     = 11446
                            //INV_COLLECTB= 9700 - 254 = 9446 ---> 254
                            //INV_COLLECTT= 1746
                            //INV_COLLECTN= 1746 + 254 = 2000 
                            //INV_BALANCE = 11446 - 200 = 9446

                            //if (item.TotalPagar > item.Impuesto)
                            if (item.TotalPagar > _Impuesto)
                            {
                                BEReciboDetalle enr = new BEReciboDetalle();
                                ImpuestoCero = true;
                                decimal Impuesto = 0;

                                if (_Impuesto != 0)
                                {
                                    var BaseACumulado = f.INV_COLLECTB;
                                    var NetoACumulado = f.INV_COLLECTN;
                                    var Base = item.TotalPagar - _Impuesto;
                                    Impuesto = _Impuesto;
                                    enr.OWNER = GlobalVars.Global.OWNER;
                                    enr.REC_ID = item.idRecibo;
                                    enr.INV_ID = item.Id;
                                    enr.INV_EXPID = item.idFecha;
                                    enr.REC_BASE = Base;
                                    enr.REC_TAXES = Impuesto;
                                    enr.REC_TOTAL = item.TotalPagar;
                                    enr.REC_TOTAL_PAGAR = item.TotalPagar;
                                    enr.LOG_USER_CREAT = UsuarioActual;
                                    enr.LOG_USER_UPDATE = UsuarioActual;
                                    result = new BLRecibo().AplicarFactura_ImpuestoBase(enr, Base, ImpuestoTotal, ImpuestoCero, total, ImpuestoTotal, NetoACumulado, BaseACumulado);
                                }
                                else
                                {
                                    //impuesto queda 0
                                    //pagamos 1000 

                                    //nuevo total 10446
                                    //REC_BASE = 9700 - 254 = 9446 --> 1000
                                    //REC_TAXES= 0
                                    //REC_TOTAL= 1000 

                                    //INV_NET     = 10446
                                    //INV_COLLECTB= 9446 --> 1000
                                    //INV_COLLECTT= 0
                                    //INV_COLLECTN= INV_COLLECTB + TOTAL_PAGAR = 3000,
                                    //INV_BALANCE = totalfactura - INV_COLLECTN = 8446
                                    var NetoACumulado = f.INV_COLLECTN;
                                    var BaseACumulado = f.INV_COLLECTB;
                                    //var ImpTotal = f.INV_COLLECTT;

                                    //NetoACumulado = 0;
                                    ImpuestoCero = false;

                                    if (_Impuesto == 0)
                                    {
                                        Impuesto = ImpuestoTotal;
                                        NetoACumulado = 0;
                                    }
                                    else
                                        Impuesto = 0;

                                    //if (item.TotalPagar > _Impuesto)
                                    //    Impuesto = ImpuestoTotal;
                                    //else
                                    //    Impuesto = 0;

                                    var Base = item.TotalPagar;
                                    enr.OWNER = GlobalVars.Global.OWNER;
                                    enr.REC_ID = item.idRecibo;
                                    enr.INV_ID = item.Id;
                                    enr.INV_EXPID = item.idFecha;
                                    enr.REC_BASE = Base;
                                    enr.REC_TAXES = Impuesto;
                                    enr.REC_TOTAL = item.TotalPagar;
                                    enr.REC_TOTAL_PAGAR = item.TotalPagar;
                                    enr.LOG_USER_CREAT = UsuarioActual;
                                    enr.LOG_USER_UPDATE = UsuarioActual;
                                    result = new BLRecibo().AplicarFactura_ImpuestoBase(enr, item.Base, item.Impuesto, ImpuestoCero, total, ImpuestoTotal, NetoACumulado, BaseACumulado);
                                }
                            }
                            else
                            {
                                var BaseACumulado = f.INV_COLLECTB;
                                var NetoACumulado = f.INV_COLLECTN;
                                //var Impuesto = (item.Impuesto - item.TotalPagar);
                                //var Base = _Base;

                                var Impuesto = item.TotalPagar;
                                var Base = 0;

                                BEReciboDetalle enr = new BEReciboDetalle();
                                enr.OWNER = GlobalVars.Global.OWNER;
                                enr.REC_ID = item.idRecibo;
                                enr.INV_ID = item.Id;
                                enr.INV_EXPID = item.idFecha;
                                enr.REC_BASE = Base;
                                enr.REC_TAXES = Impuesto;
                                enr.REC_TOTAL = item.TotalPagar;
                                enr.REC_TOTAL_PAGAR = item.TotalPagar;
                                enr.LOG_USER_CREAT = UsuarioActual;
                                enr.LOG_USER_UPDATE = UsuarioActual;
                                result = new BLRecibo().AplicarFactura_ImpuestoBase(enr, item.Base, item.Impuesto, ImpuestoCero, total, ImpuestoTotal, NetoACumulado, BaseACumulado);
                            }
                        }

                        else if (GlobalVars.Global.PagoReparto == "BASE-IMPUESTO")
                        {
                            bool BaseCero = false;
                            BEFactura f = new BEFactura();

                            f = new BLFacturaCobro().ObtenerFacturaAplicar(GlobalVars.Global.OWNER, item.Id);
                            decimal _Base = 0;
                            decimal _Impuesto = 0;
                            decimal total = 0;
                            decimal NewRecTotal = 0;
                            decimal BaseTotal = 0;

                            BaseTotal = item.Base;

                            if (f.INV_COLLECTB == 0) _Base = item.Base;
                            else _Base = (item.Base - f.INV_COLLECTB); NewRecTotal = f.INV_COLLECTB;

                            if (f.INV_COLLECTT == 0) _Impuesto = item.Impuesto;
                            else _Impuesto = (item.Impuesto - f.INV_COLLECTT);

                            if (f.INV_BALANCE == 0) total = item.Base + item.Impuesto;
                            else total = _Base + _Impuesto;

                            if (item.TotalPagar > _Base)
                            {
                                BEReciboDetalle enr = new BEReciboDetalle();
                                BaseCero = true;
                                decimal Base = 0;
                                decimal Impuesto = 0;

                                if (_Base != 0)
                                {
                                    //Base = (_Base - item.TotalPagar); // valor negativo
                                    //Impuesto = _Impuesto + Base;
                                    //enr.REC_BASE = Base * -1;                                    
                                    ////enr.REC_TAXES = Impuesto;
                                    //enr.REC_TAXES = item.TotalPagar - _Base;
                                    //_Base = BaseTotal;
                                    //_Impuesto = f.INV_COLLECTT + item.TotalPagar;

                                    //enr.REC_BASE = BaseTotal;
                                    enr.REC_BASE = _Base;
                                    enr.REC_TAXES = (_Base - item.TotalPagar) * -1;
                                    if (enr.REC_TAXES < 0)
                                    {
                                        var rec_taxes = enr.REC_TAXES * -1;
                                        enr.REC_TAXES = rec_taxes;
                                    }
                                    enr.REC_TOTAL = enr.REC_BASE + enr.REC_TAXES;
                                    _Base = BaseTotal;
                                    _Impuesto = (enr.REC_BASE - item.TotalPagar) * -1;

                                    if (_Impuesto < 0)
                                    {
                                        var imp = _Impuesto * -1;
                                        _Impuesto = imp;
                                    }
                                }
                                else
                                {
                                    Impuesto = _Impuesto - item.TotalPagar;
                                    enr.REC_TAXES = item.TotalPagar;
                                    enr.REC_BASE = 0;
                                    enr.REC_TOTAL = enr.REC_BASE + enr.REC_TAXES;
                                    _Base = BaseTotal;
                                    _Impuesto = f.INV_COLLECTT + item.TotalPagar;
                                    if (_Impuesto < 0)
                                    {
                                        var imp = _Impuesto * -1;
                                        _Impuesto = imp;
                                    }
                                }


                                enr.OWNER = GlobalVars.Global.OWNER;
                                enr.REC_ID = item.idRecibo;
                                enr.INV_ID = item.Id;
                                enr.INV_EXPID = item.idFecha;
                                enr.REC_TOTAL = item.TotalPagar;
                                enr.REC_TOTAL_PAGAR = item.TotalPagar;
                                enr.LOG_USER_CREAT = UsuarioActual;
                                enr.LOG_USER_UPDATE = UsuarioActual;
                                result = new BLRecibo().AplicarFactura_BaseImpuesto(enr, _Base, _Impuesto, BaseCero, total, BaseTotal);
                            }
                            else
                            {
                                var Base = (_Base - item.TotalPagar);
                                var Impuesto = _Impuesto;
                                BEReciboDetalle enr = new BEReciboDetalle();
                                enr.OWNER = GlobalVars.Global.OWNER;
                                enr.REC_ID = item.idRecibo;
                                enr.INV_ID = item.Id;
                                enr.INV_EXPID = item.idFecha;
                                //enr.REC_BASE = Base;
                                enr.REC_BASE = item.Base - Base;
                                //enr.REC_TAXES = Impuesto;
                                enr.REC_TAXES = 0;
                                enr.REC_TOTAL = NewRecTotal + item.TotalPagar; //item.TotalPagar;
                                enr.REC_TOTAL_PAGAR = item.TotalPagar;
                                enr.LOG_USER_CREAT = UsuarioActual;
                                enr.LOG_USER_UPDATE = UsuarioActual;
                                result = new BLRecibo().AplicarFactura_BaseImpuesto(enr, _Base, _Impuesto, BaseCero, total, BaseTotal);
                            }
                        }
                    }

                    if (result == 1)
                    {
                        retorno.result = 1;
                        retorno.message = "Se aplicaron recibo(s) a factura";
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha podido aplicar el recibo a la factura";
                    }
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "No se ha podido aplicar el recibo a la factura";
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtenerFacturasAplicar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

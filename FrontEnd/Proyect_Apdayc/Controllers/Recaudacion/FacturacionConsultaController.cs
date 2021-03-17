using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases.Factura_Electronica;
using SGRDA.Entities.FacturaElectronica;
using SGRDA.BL.FacturaElectronica;
using Proyect_Apdayc.Clases;
using System.Text;
using System.Text.RegularExpressions;
using SGRDA.BL.Reporte;
using System.Globalization;
using SGRDA.Utility;
using System.IO;


namespace Proyect_Apdayc.Controllers.Recaudacion
{
    public class FacturacionConsultaController : Base
    {
        //
        // GET: /FacturacionConsulta/
        #region SESION

        string MSG_SUNAT = "";
        public const string nomAplicacion = "SRGDA";
        public static string K_SESSION_CONSULTA = "__DTOFacturacionTmp";
        private const string K_SESION_DETALLE_FACTURA = "___DTODetalleFactura";
        private const string K_SESION_FACTURA = "___DTOFactura";
        private const string K_SESION_RECIBO = "___DTORecibo";
        private const string K_SESION_LICENCIA = "___DTOConLicencia";
        private const string K_SESION_LICENCIA_DETALLE = "___DTOConLicenciaDetalle";

        List<DTOFactura> listaConsulta = new List<DTOFactura>();
        List<DTOFactura> listar = new List<DTOFactura>();
        DTOFactura Factura = new DTOFactura();
        DTOFactura FacturaAUX = new DTOFactura();
        private DateTime FechaSistema = new BLREC_RATES_GRAL().ObtenerFechaSistema();
        List<DTOFacturaDetallle> detalleFactura = new List<DTOFacturaDetallle>();
        List<DTORecibo> recibos = new List<DTORecibo>();
        List<DTOLicencia> ConLicencia = new List<DTOLicencia>();
        List<DTOFacturaDetallle> ConLicenciaDetalle = new List<DTOFacturaDetallle>();

        //FACTURACION ELECTRONICA
        ComprobanteElectronica FE = new ComprobanteElectronica();

        public class Variables
        {
            public const int NC = 3;
        }


        public List<DTOFactura> ListaConsultaTmp
        {
            get
            {
                return (List<DTOFactura>)Session[K_SESSION_CONSULTA];
            }
            set
            {
                Session[K_SESSION_CONSULTA] = value;
            }
        }
        public List<DTOFacturaDetallle> DetalleFacturaTmp
        {
            get
            {
                return (List<DTOFacturaDetallle>)Session[K_SESION_DETALLE_FACTURA];
            }
            set
            {
                Session[K_SESION_DETALLE_FACTURA] = value;
            }
        }
        public DTOFactura FacturaTmp
        {
            get
            {
                return (DTOFactura)Session[K_SESION_FACTURA];
            }
            set
            {
                Session[K_SESION_FACTURA] = value;
            }
        }
        public List<DTORecibo> RecibosTmp
        {
            get
            {
                return (List<DTORecibo>)Session[K_SESION_RECIBO];
            }
            set
            {
                Session[K_SESION_RECIBO] = value;
            }
        }
        public List<DTOLicencia> LicenciaTmp
        {
            get
            {
                return (List<DTOLicencia>)Session[K_SESION_LICENCIA];
            }
            set
            {
                Session[K_SESION_LICENCIA] = value;
            }
        }
        public List<DTOFacturaDetallle> LicenciaDetalleTmp
        {
            get
            {
                return (List<DTOFacturaDetallle>)Session[K_SESION_LICENCIA_DETALLE];
            }
            set
            {
                Session[K_SESION_LICENCIA_DETALLE] = value;
            }
        }
        #endregion

        #region PoPup_ConsultaFactura
        public JsonResult ListarConsultaFactura(int skip, int take, int page, int pageSize, string numSerial, decimal numFact, decimal idSoc,
                                                decimal grupoFact, string moneda, decimal idLic,
                                                DateTime Fini, DateTime Ffin, decimal idFact,
                                                decimal licTipo, decimal idBpsAgen)
        {
            var lista = ListaConsulta(GlobalVars.Global.OWNER, numSerial, numFact, idSoc,
                                                grupoFact, moneda, idLic,
                                                Fini, Ffin, idFact,
                                                licTipo, idBpsAgen,
                                                page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEFacturaConsulta { ListaConsultaFactura = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEFacturaConsulta { ListaConsultaFactura = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEFacturaConsulta> ListaConsulta(string owner, string numSerial, decimal numFact, decimal idSoc,
                                                decimal grupoFact, string moneda, decimal idLic,
                                                DateTime Fini, DateTime Ffin, decimal idFact,
                                                decimal licTipo, decimal idBpsAgen,
                                                int pagina, int cantRegxPag)
        {
            return new BLFactura().ListarConsultaFacturaPage(GlobalVars.Global.OWNER, numSerial, numFact, idSoc,
                                                 grupoFact, moneda, idLic,
                                                 Fini, Ffin, idFact,
                                                 licTipo, idBpsAgen,
                                                 pagina, cantRegxPag);
        }
        #endregion

        #region FACTURA_CONSULTA
        public ActionResult Index()
        {
            Session.Remove(K_SESSION_CONSULTA);
            Session.Remove(K_SESION_DETALLE_FACTURA);
            Session.Remove(K_SESION_FACTURA);
            Session.Remove(K_SESION_RECIBO);
            Session.Remove(K_SESION_LICENCIA);
            Session.Remove(K_SESION_LICENCIA_DETALLE);
            Init(false);
            return View();
        }

        // BANDEJA DE CONSULTA FACTURA
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
                    Session.Remove(K_SESSION_CONSULTA);

                    LicenciaDetalleTmp = null;
                    LicenciaTmp = null;
                    ListaConsultaTmp = null;
                    idOficina = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                    var FacturaConsulta = new BLFactura().ListarConsulta(GlobalVars.Global.OWNER, numSerial, numFact, idSoc,
                                             grupoFact, moneda, idLic,
                                             Fini, Ffin, idFact, impresas, anuladas, licTipo,
                                             agenteBpsId, conFecha, tipoDoc, idOficina, valorDivision, estado, idPlan, idBpsGroup);

                    if (FacturaConsulta != null)
                    {

                        if (idPlan == 0)
                        {
                            #region Logica inicial
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
                                        fecha_cancelacion = s.FECHA_CANCELACION,
                                        tipoFacturaDes = s.INV_MANUAL ? "M" : "A"
                                    });

                                });
                                ListaConsultaTmp = listaConsulta;
                            }
                            #endregion
                        }
                        else
                        {
                            #region Logica Consulta Facturas desde Licencias
                            if (FacturaConsulta.ListarLicencia != null)
                            {
                                ConLicenciaDetalle = new List<DTOFacturaDetallle>();
                                var ConFactDetalleTMP = new List<DTOFacturaDetallle>();
                                var ConFactDetalleTMP2 = new List<DTOFacturaDetallle>();
                                if (LicenciaDetalleTmp == null) LicenciaDetalleTmp = new List<DTOFacturaDetallle>();
                                FacturaConsulta.ListarDetalleFactura.ForEach(s =>
                                {
                                    if (idPlan == s.LIC_PL_ID)
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

                                        LicenciaDetalleTmp.Add(new DTOFacturaDetallle
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

                                    }
                                });
                                ConFactDetalleTMP = ConLicenciaDetalle;

                                FacturaConsulta.ListarDetalleFactura.ForEach(s =>
                                {
                                    ConFactDetalleTMP.ForEach(x =>
                                    {
                                        if (x.codFactura == s.INV_ID && x.codLicencia == s.LIC_ID && x.Id != s.INVL_ID)
                                        {
                                            LicenciaDetalleTmp.Add(new DTOFacturaDetallle
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
                                        }
                                    });
                                });
                            }
                            if (FacturaConsulta.ListarLicencia != null)
                            {
                                ConLicencia = new List<DTOLicencia>();
                                FacturaConsulta.ListarLicencia.ForEach(s =>
                                {
                                    ConLicenciaDetalle.ForEach(x =>
                                    {
                                        if (x.codFactura == s.INV_ID && x.codLicencia == s.LIC_ID)
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
                                        }
                                    });
                                });
                                LicenciaTmp = ConLicencia;
                            }

                            if (FacturaConsulta.ListarFactura != null)
                            {
                                listaConsulta = new List<DTOFactura>();
                                FacturaConsulta.ListarFactura.ForEach(s =>
                                {
                                    decimal idFactLast = 0;
                                    LicenciaDetalleTmp.ForEach(x =>
                                    {
                                        if (s.INV_ID == x.codFactura && x.codLicPlanificacion == idPlan)// idFactLast != s.INV_ID)
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
                                                estadoFact = s.EST_FACT,
                                                fecha_cancelacion = s.FECHA_CANCELACION,
                                                tipoFacturaDes = s.INV_MANUAL ? "M" : "A"
                                            });

                                        }
                                        idFactLast = s.INV_ID;
                                    });
                                });
                                ListaConsultaTmp = listaConsulta;
                            }

                            #endregion
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
            //return Json(retorno, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        /// <summary>
        /// Estado 0 muestra checks
        /// </summary>
        /// <param name="estado"></param>
        /// <returns></returns>
        public JsonResult ListarFactConsulta(decimal estado = 0)
        {
            DateTime fechaMinAnulacion = Convert.ToDateTime(FechaSistema.AddDays(-GlobalVars.Global.DiasFechaAnulacion));

            listar = ListaConsultaTmp;
            int habNC = 0;
            bool habOficina = false;
            int idOficina = Convert.ToInt32(Session[Constantes.Sesiones.CodigoOficina]);
            if (idOficina == 10081 || idOficina == 10154)// TI - GENAREC
                habOficina = true;
            else
                habOficina = false;

            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblFacturaMasiva' border=0 width='100%;' class='k-grid k-widget' id='tblFacturaMasiva'>");
                shtml.Append("<thead><tr>");

                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='text-align:center;width:25px'>");
                if (estado == 0)
                {
                    shtml.Append("<input type='checkbox' id='idCheck' name='Check' class='Check' onchange='clickCheck()'>");
                }
                shtml.Append("</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Id</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Tipo</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Serie</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >#</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Fecha</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Cancelación</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Anulado</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Ident.</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >N° Ident.</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Socio de negocio</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Moneda</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Facturado</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Cobrado</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Saldo Pendiente</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Ref N.C</th>");
                //shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Tipo</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Estado</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Estado Sunat</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Ver</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");

                if (listar != null)
                {
                    foreach (var item in listar) //.OrderByDescending(x => x.id))
                    {
                        if (item.tipo != "NC" && habOficina)
                            habNC = 1;
                        else
                            habNC = 0;

                        shtml.Append("<tr style='background-color:white'>");
                        shtml.Append("<td style='text-align:center;width:25px'>");
                        if (estado == 0)
                        {
                            shtml.AppendFormat("<input type='checkbox' id='{0}' name='Check' class='Check' />", "chkFact" + item.id);
                        }

                        shtml.Append("</td>");

                        shtml.AppendFormat("<td style='width:25px'> ");
                        if (estado == 0)
                        {
                            shtml.AppendFormat("<a href=# onclick='verDetaFactura({0});'><img id='expand" + item.id + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.id);
                        }
                        shtml.Append("</td>");
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:45px' onclick='return obtenerId({0},{1});' class='IDCell' >{0}</td>", item.id, item.fechaAnulacion == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});'>{0}</td>", item.tipo, item.id, item.fechaAnulacion == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right' onclick='return obtenerId({1},{2},{3});'>{0}</td>", item.serial, item.id, item.fechaAnulacion == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right' onclick='return obtenerId({1},{2},{3});' >{0}</td>", item.numFacturaActual, item.id, item.fechaAnulacion == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.fechaFact), item.id, item.fechaAnulacion == null ? 0 : 1, habNC);
                        if (item.estadoFact == 2)// Constantes.EstadoFactura.CANCELADO
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.fecha_cancelacion), item.id, item.fechaAnulacion == null ? 0 : 1, habNC);
                        else
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", "", item.id, item.fechaAnulacion == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.fechaAnulacion), item.id, item.fechaAnulacion == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", item.doc_Identificacion, item.id, item.fechaAnulacion == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", item.num_Identificacion, item.id, item.fechaAnulacion == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", item.socio, item.id, item.fechaAnulacion == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", item.moneda, item.id, item.fechaAnulacion == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' onclick='return obtenerId({1},{2},{3});' >{0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorFinal), item.id, item.fechaAnulacion == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' onclick='return obtenerId({1},{2},{3});' > {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.cobradoNeto), item.id, item.fechaAnulacion == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' onclick='return obtenerId({1},{2},{3});' > {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.saldoFactura), item.id, item.fechaAnulacion == null ? 0 : 1, habNC);

                        if (item.factRefNotCred != 0)
                            shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'><font color='red'>{0} </font></td>", item.factRefNotCred, item.id, item.fechaAnulacion == null ? 0 : 1, habNC);
                        else
                            shtml.AppendFormat("<td style='cursor:pointer;' style='text-align:right; width:150px; padding-right:10px'> </td>");

                        //shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:center; width:150px; padding-right:70px'>{0}</td>", item.tipoFacturaDes, item.id, item.fechaAnulacion == null ? 0 : 1, habNC);

                        //if (item.INV_TYPE == 1 || item.INV_TYPE == 2)
                        //{
                            switch (item.estadoFact)
                            {
                                case 4: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", Constantes.EstadoFactura.ANULADA, item.id, item.fechaAnulacion == null ? 0 : 1, habNC); break;
                                case 2: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.CANCELADO, item.id, item.fechaAnulacion == null ? 0 : 1, habNC); break;
                                case 1: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'> <font color='green'> {0} </font> </td>", Constantes.EstadoFactura.CANCELADA_PARCIAL, item.id, item.fechaAnulacion == null ? 0 : 1, habNC); break;
                                case 3: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.PENDIENTE_PAGO, item.id, item.fechaAnulacion == null ? 0 : 1, habNC); break;
                                default: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", Constantes.EstadoFactura.ANULADA, item.id, item.fechaAnulacion == null ? 0 : 1, habNC); break;
                            }
                        //}

                        ////ESTADO SUNAT
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});' width:150px; padding-right:10px'>{0}</td>", item.Estado_Sunat, item.id, item.fechaAnulacion == null ? 0 : 1, habNC);


                        //if (item.fechaAnulacion == null)
                        //{
                        //    if (item.saldoFactura == 0)
                        //        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2});' style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.CANCELADO, item.id, item.fechaAnulacion == null ? 0 : 1);
                        //    else if ((item.saldoFactura != 0) && (item.saldoFactura < item.valorFinal))
                        //        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2});' style='text-align:right; width:150px; padding-right:10px'> <font color='green'> {0} </font> </td>", Constantes.EstadoFactura.CANCELADA_PARCIAL, item.id, item.fechaAnulacion == null ? 0 : 1);
                        //    else if (item.saldoFactura == item.valorFinal)
                        //        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2});' style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.PENDIENTE_PAGO, item.id, item.fechaAnulacion == null ? 0 : 1);
                        //}
                        //else
                        //    shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2});' style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", Constantes.EstadoFactura.ANULADA, item.id, item.fechaAnulacion == null ? 0 : 1);


                        shtml.AppendFormat("<td style='text-align:center'>");
                        shtml.AppendFormat("<label onclick='verReporte({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta2.png' border=0 title='{1}'></label>&nbsp;&nbsp;", item.id, "Ver Comprobante");
                        shtml.AppendFormat("</td>");
                        shtml.AppendFormat("<td style='text-align:center'>");

                        //REENVIO DE COMPROBANTE A SUNAT
                        if (item.Estado_Sunat != Constantes.Mensaje_Sunat.MSG_ACEPTADO)
                        {
                            if (item.Estado_Sunat == Constantes.Mensaje_Sunat.MSG_RECHAZADO)
                            {
                                shtml.AppendFormat("<label style='display: none' onclick='ReenvioSunat({0});'><img style='cursor:pointer;' src='../Images/iconos/undoMoney.png' border=0 title='{1}'></label>&nbsp;&nbsp;", item.id, "Reenvío de Comprobante");
                            }
                            else
                            {
                                shtml.AppendFormat("<label onclick='ReenvioSunat({0});'><img style='cursor:pointer;' src='../Images/iconos/undoMoney.png' border=0 title='{1}'></label>&nbsp;&nbsp;", item.id, "Reenvío de Comprobante");
                            }
                        }

                        if (item.saldoFactura == item.valorFinal)
                        {
                            DateTime fechaEmision = item.fechaFact.Value;
                            if (item.tipoFacturaDes == "M" && item.fechaFact.Value.Month == FechaSistema.Month && estado == 0 && item.estadoFact != 4)
                            {
                                shtml.AppendFormat("<a href=# onclick='eliminarFactura({0});'><img src='../Images/iconos/delete.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.id, "Anular Comprobante");
                            }
                            //else if (item.tipoFacturaDes == "A" && item.fechaFact.Value.Month == FechaSistema.Month && estado == 0 && item.estadoFact != 4)
                            else if (item.tipoFacturaDes == "A" && estado == 0 && item.estadoFact != 4
                                      && (item.fechaFact.Value.CompareTo(fechaMinAnulacion) >= 0)
                                    )
                            {
                                shtml.AppendFormat("<a href=# onclick='eliminarFactura({0});'><img src='../Images/iconos/delete.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.id, "Anular Comprobante");
                            }
                        }
                        shtml.AppendFormat("</td>");
                        shtml.Append("</tr>");

                        shtml.Append("</div>");
                        shtml.Append("</td>");
                        shtml.Append("</tr>");

                        shtml.Append("</tr>");

                        shtml.Append("<tr style='background-color:white'>");
                        shtml.Append("<td></td>");
                        shtml.Append("<td></td>");
                        shtml.Append("<td style='width:100%' colspan='20'>");


                        shtml.Append("<div style='display:none;' id='" + "div" + item.id.ToString() + "'  > ");
                        //shtml.Append(getHtmlTableDetaLicenciaBorrador(item.id));

                        shtml.Append("</div>");
                        shtml.Append("</td>");
                        shtml.Append("</tr>");

                        shtml.Append("<tr><td colspan='20' style='background-color:#DBDBDE'></hr></td></tr>");
                    }
                }
                shtml.Append("</table>");
                retorno.message = shtml.ToString();
                retorno.Code = listar.Count;
                retorno.result = 1;
                ///
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

        public StringBuilder getHtmlTableDetaLicenciaBorrador(decimal codFact)
        {
            var licencias = LicenciaTmp.Where(p => p.codFactura == codFact).ToList();

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
            shtml.Append("<th class='k-header' style='width:120px'>Monto</th>");
            shtml.Append("<th class='k-header' style='width:120px'>Descuento</th>");
            shtml.Append("<th class='k-header' style='width:120px'>Impuesto</th>");
            shtml.Append("<th class='k-header' style='width:120px'>Total</th>");

            if (licencias != null && licencias.Count > 0)
            {
                foreach (var item in licencias.OrderBy(x => x.codLicencia))
                {
                    shtml.Append("<tr style='background-color:white'>");
                    shtml.AppendFormat("<td style='width:120px;display:none'>{0}</td>", item.codFactura);
                    shtml.AppendFormat("<td style='width:120px;display:none'>{0}</td>", item.codLicencia);
                    shtml.AppendFormat("<td style='width:25px'> ");
                    shtml.AppendFormat("<a href=# onclick='verDetaLic({0},{1});'><img id='expandLic" + item.codFactura + "-" + item.codLicencia + "'  src='../Images/botones/less.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.codFactura.ToString(), item.codLicencia.ToString());
                    shtml.Append("</td>");
                    shtml.AppendFormat("<td style='width:350px'>{0}</td>", item.nombreLicencia);
                    shtml.AppendFormat("<td style='width:350px'>{0}</td>", item.Modalidad);
                    shtml.AppendFormat("<td style='width:330px'>{0}</td>", item.Establecimiento);
                    shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:10px'> {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Monto));
                    shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:10px'> {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Descuento));
                    shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:10px'> {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Impuesto));
                    shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:10px'> {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.Total));
                    shtml.Append("</td>");
                    shtml.Append("</tr>");

                    shtml.Append("<tr style='background-color:white'>");
                    shtml.Append("<td></td>");
                    shtml.Append("<td></td>");
                    shtml.Append("<td></td>");
                    shtml.Append("<td colspan='6'>");

                    shtml.Append("<div style='display:inline;' id='" + "divLic" + item.codFactura.ToString() + "-" + item.codLicencia.ToString() + "'  > ");

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
            var detalle = LicenciaDetalleTmp.Where(p => p.codLicencia == codLic && p.codFactura == codFact).ToList();
            StringBuilder shtml = new StringBuilder();
            shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
            shtml.Append("<thead>");

            shtml.Append("<tr>");
            shtml.Append("<th class='k-header' style='display:none'>Id</th>");
            shtml.Append("<th class='k-header' style='display:none'>Id Licencia</th>");
            shtml.Append("<th class='k-header' style='text-align:center'>Periodo</th>");
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
                    shtml.AppendFormat("<td style='text-align:center'>{0} - {1}</td>", item.anio.ToString(), item.mes);
                    shtml.AppendFormat("<td style='width:95px;text-align:right;  padding-right:10px'>  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorBruto));
                    shtml.AppendFormat("<td style='width:95px;text-align:right;  padding-right:10px'>  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorDescuento));
                    shtml.AppendFormat("<td style='width:95px;text-align:right;  padding-right:10px'>  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorImpuesto));
                    shtml.AppendFormat("<td style='width:90px;text-align:right;  padding-right:10px'>  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.valorNeto));
                    shtml.Append("</td>");
                    shtml.Append("</tr>");
                }
            }
            shtml.Append("</table>");
            return shtml;
        }

        #region ReenvioComprobante

        [HttpPost]
        public JsonResult ReenvioSunat(decimal Id, decimal tipo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    //var listar = ListaConsultaTmp;

                    if (GlobalVars.Global.FE == true)
                    {
                        if (tipo == 3)
                        {
                            MSG_SUNAT = FE.EnvioNotaCredito(Id);
                        }
                        else
                        {
                            MSG_SUNAT = FE.EnvioComprobanteElectronico(Id);
                        }

                        retorno.result = 1;
                        retorno.message = MSG_SUNAT;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "obtener Obtiene Cabecera Factura", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult mostrarDetalleFactura(decimal id)
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "mostrarDetalleFactura", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReporteFacturacionConsulta(decimal id)
        {
            //Init(false);//add sysseg
            Resultado retorno = new Resultado();
            string format = "PDF";

            try
            {

                decimal total = 0;
                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reportes/rptFacturaElectronicaConsulta.rdlc");

                List<BECabeceraFactura> listaCab = new List<BECabeceraFactura>();
                listaCab = new BLCabeceraFactura().ListarCabeceraFactura(GlobalVars.Global.OWNER, id);
                total = listaCab.FirstOrDefault().MntTotal;

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
                    parametro = new ReportParameter("Monto_Letra", Util.NumeroALetras(total.ToString()));
                    localReport.SetParameters(parametro);

                    //ReportParameter parametroFecha = new ReportParameter();
                    //parametroFecha = new ReportParameter("Fecha", String.Format("{0:dd/MM/yyyy}", listaCab.FirstOrDefault().INV_DATE));
                    //localReport.SetParameters(parametroFecha);

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
                    //Render the report            
                    renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                    //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension); 

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    //retorno.data = Json(FacturaMasiva, JsonRequestBehavior.AllowGet);
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

        public ActionResult ReporteFacturacion(string serie, decimal id)
        {
            //Init(false);//add sysseg
            Resultado retorno = new Resultado();
            string format = "PDF";

            try
            {
                var FechaEmision = DateTime.Now.ToString("yyyy-MM-dd");
                decimal total = 0;
                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reportes/RptFacturaElectronica.rdlc");

                var correlativo = new BLCabeceraFactura().ObtenerCorrelativo(GlobalVars.Global.OWNER, serie);

                List<BECabeceraFactura> listaCab = new List<BECabeceraFactura>();
                listaCab = new BLCabeceraFactura().ListarCabeceraPreview(GlobalVars.Global.OWNER, id);
                total = listaCab.FirstOrDefault().MntTotal;

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
                    parametro = new ReportParameter("Monto_Letra", Util.NumeroALetras(total.ToString()));
                    localReport.SetParameters(parametro);

                    ReportParameter parametroSerie = new ReportParameter();
                    parametroSerie = new ReportParameter("Serie", serie);
                    localReport.SetParameters(parametroSerie);

                    ReportParameter parametroCorrelativo = new ReportParameter();
                    parametroCorrelativo = new ReportParameter("Correlativo", correlativo.FirstOrDefault().Correlativo);
                    localReport.SetParameters(parametroCorrelativo);

                    ReportParameter parametroFecha = new ReportParameter();
                    parametroFecha = new ReportParameter("FechaEmision", FechaEmision);
                    localReport.SetParameters(parametroFecha);

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

        public ActionResult ReporteManual(decimal id)
        {
            //Init(false);//add sysseg
            Resultado retorno = new Resultado();
            string format = "PDF";

            try
            {

                decimal total = 0;
                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reportes/RptFacturaConsulta.rdlc");

                List<BEFactura> listaCab = new List<BEFactura>();
                listaCab = new BLFactura().ListaReporteCabeceraConsulta(GlobalVars.Global.OWNER, id);
                //total = listaCab.FirstOrDefault().MntTotal;

                List<BEFacturaDetalle> listaDet = new List<BEFacturaDetalle>();
                listaDet = new BLFactura().ListaReporteDetalleConsulta(GlobalVars.Global.OWNER, id);

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

                    //ReportParameter parametro = new ReportParameter();
                    //parametro = new ReportParameter("Monto_Letra", Util.NumeroALetras(total.ToString()));
                    //localReport.SetParameters(parametro);

                    ReportParameter parametro = new ReportParameter();
                    parametro = new ReportParameter("Usuario", UsuarioActual.Trim());
                    localReport.SetParameters(parametro);

                    ReportParameter parametroFecha = new ReportParameter();
                    parametroFecha = new ReportParameter("Fecha", String.Format("{0:dd/MM/yyyy}", listaCab.FirstOrDefault().INV_DATE));
                    localReport.SetParameters(parametroFecha);

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
                    //Render the report            
                    renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                    //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension); 

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    //retorno.data = Json(FacturaMasiva, JsonRequestBehavior.AllowGet);
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

        //ELECTRONICA
        public ActionResult Reporte(decimal id)
        {
            //Init(false);//add sysseg
            Resultado retorno = new Resultado();
            string[] obj_ArchivoPDF = null;
            try
            {
                SGRDA_PDF.consultaDocumentosPeruSoapClient obj_ServicioDocumento = new SGRDA_PDF.consultaDocumentosPeruSoapClient();

                List<BECabeceraFactura> listaCab = new List<BECabeceraFactura>();
                listaCab = new BLCabeceraFactura().ListarCabeceraFactura(GlobalVars.Global.OWNER, id);

                string Monto_Total = Convert.ToString(listaCab.FirstOrDefault().MntTotal).Replace(",", ".").ToString();
                obj_ArchivoPDF = obj_ServicioDocumento.getDocumentoPDF(GlobalVars.Global.RucApdayc, listaCab.FirstOrDefault().Correlativo, listaCab.FirstOrDefault().TipoDTE, listaCab.FirstOrDefault().Serie, listaCab.FirstOrDefault().FChEmis, Monto_Total).ToArray();

                string cadena = obj_ArchivoPDF[1].ToString();
                if (cadena != "")
                {
                    using (FileStream stream = System.IO.File.Create(GlobalVars.Global.RutaFisicaFE + obj_ArchivoPDF[0]))
                    {
                        byte[] code_byte = Convert.FromBase64String(obj_ArchivoPDF[1]);
                        stream.Write(code_byte, 0, code_byte.Length);
                    }

                    string ruta = GlobalVars.Global.RutaWebFE + obj_ArchivoPDF[0];

                    retorno.result = 1;
                    retorno.valor = ruta;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
                else
                {
                    retorno.result = 2;
                    retorno.valor = obj_ArchivoPDF[2];
                    retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;

                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.Mensaje_Sunat.MSG_ERROR_PDF;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Reporte", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ReporteCompleto(string numSerial, decimal numFact, decimal idSoc,
                                                decimal grupoFact, string moneda, decimal idLic,
                                                string Fini, string Ffin, decimal idFact,
                                                int impresas, int anuladas, decimal licTipo, decimal agenteBpsId,
                                                int conFecha, int tipoDoc, decimal idOficina, decimal valorDivision, int estado,
                                                string format)
        {
            //Init(false);//add sysseg
            Resultado retorno = new Resultado();

            try
            {
                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reportes/RptFacturaConsultaVentas.rdlc");

                List<BEFacturaDetalle> listaCab = new List<BEFacturaDetalle>();
                listaCab = new BLFactura().ReporteFactConsulta(GlobalVars.Global.OWNER, numSerial, numFact, idSoc,
                                                  grupoFact, moneda, idLic,
                                                  Convert.ToDateTime(Fini),
                                                  Convert.ToDateTime(Ffin),
                                                  idFact, impresas, anuladas, licTipo, agenteBpsId,
                                                  conFecha, tipoDoc, idOficina, valorDivision, estado);


                if (listaCab.Count > 0)
                {
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = listaCab;
                    localReport.DataSources.Add(reportDataSource);

                    ReportParameter parametro = new ReportParameter();
                    parametro = new ReportParameter("Usuario", UsuarioActual.Trim());
                    localReport.SetParameters(parametro);
                    ReportParameter parametroFecha = new ReportParameter();
                    parametroFecha = new ReportParameter("Fecha", DateTime.Now.ToShortDateString());
                    localReport.SetParameters(parametroFecha);

                    string reportType = format;
                    string mimeType;
                    string encoding;
                    string fileNameExtension;

                    //The DeviceInfo settings should be changed based on the reportType            
                    //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
                    string deviceInfo = "<DeviceInfo>" +
                        "  <OutputFormat>" + format + "</OutputFormat>" +
                        //"  <PageWidth>8.5in</PageWidth>" +
                        "  <PageWidth>18.5in</PageWidth>" +
                        "  <PageHeight>11in</PageHeight>" +
                        "  <MarginTop>0.5in</MarginTop>" +
                        "  <MarginLeft>0.5in</MarginLeft>" +
                        "  <MarginRight>0.5in</MarginRight>" +
                        "  <MarginBottom>0.5in</MarginBottom>" +
                        "</DeviceInfo>";

                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;
                    //Render the report            
                    renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                    //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension); 

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    //retorno.data = Json(FacturaMasiva, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                    localReport.DisplayName = "Reporte Consulta de Facturas";
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ReporteCompleto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult AnularFactura(decimal id, string observacion, decimal tipoF, decimal tipoDoc)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEFactura factura = new BEFactura();
                    int oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                    int revert;
                    factura.OWNER = GlobalVars.Global.OWNER;
                    factura.INV_ID = id;
                    factura.INV_NULLREASON = observacion;
                    factura.LOG_USER_UPDATE = UsuarioActual;
                    factura.OFF_ID = oficina_id;

                    //if (n > 0)
                    //{
                    if (tipoF == 2) //AUTO
                    {
                        //INVOCO LA WEB SERVICES PARA DAR DE BAJA AL DOCUMENTO
                        if (GlobalVars.Global.FE == true)
                        {
                            var res = FE.AnularDocumento(id, observacion);
                            var Rspt = res.Contains("Ya se encuentra registrado"); //like '%amp%' BOOL

                            //var res = Constantes.Mensaje_Sunat.MSG_ANULACION_EXITOSA;

                            if ((res == Constantes.Mensaje_Sunat.MSG_ANULACION_EXITOSA || Rspt) && tipoDoc != Variables.NC)
                            {
                                int n = new BLFactura().AnularFactura(factura);
                                if (n > 0)
                                {

                                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR_SUNAT;
                                    retorno.result = 1;
                                }
                                else
                                {
                                    retorno.message = Constantes.MensajeGenerico.MSG_ERROR_ELIMINAR;
                                    retorno.result = 0;
                                }
                            }
                            else
                            {
                                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_ELIMINAR;
                                retorno.result = 0;
                            }
                        }

                    }
                    else if (tipoF == 1 && tipoDoc != Variables.NC) //MANUAL
                    {
                        int n = new BLFactura().AnularFactura(factura);
                        if (n > 0)
                        {
                            retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR_SUNAT;
                            retorno.result = 1;
                        }
                        else
                        {
                            retorno.message = Constantes.MensajeGenerico.MSG_ERROR_ELIMINAR;
                            retorno.result = 0;
                        }
                    }



                    if (tipoDoc == Variables.NC)
                    {
                        if (GlobalVars.Global.PermitirRevertNC == true)
                        {
                            revert = new BLFactura().AnularNCRevert(id, observacion);
                        }
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR_SUNAT;
                        retorno.result = 1;

                    }

                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "generarAnulacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult ObsNotaCredito(decimal id, string observacion)
        public void ObsNotaCredito(decimal id, string TipoNotaCredito, string observacion)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEFactura factura = new BEFactura();
                    factura.OWNER = GlobalVars.Global.OWNER;
                    factura.INV_ID = id;
                    factura.CODE_DESCRIPTION = TipoNotaCredito;
                    factura.INV_NULLREASON = observacion;
                    factura.LOG_USER_UPDATE = UsuarioActual;

                    retorno.result = 1;
                    int n = new BLFactura().ActualizarObs(factura);
                    if (n > 0)
                    {
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_ELIMINAR;
                        retorno.result = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "generarAnulacion", ex);
            }
        }

        /*
         Habilitar Periodo sin anular
         */
        [HttpPost]
        public ActionResult FacturaCanSinAnular(decimal id, string TIPO_NOT_CRE, string OBSERV, decimal SERIE)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    var fact = new BLFactura().Aplica_Nota_Credito(id, UsuarioActual, TIPO_NOT_CRE, OBSERV, SERIE);

                    if (fact.INV_ID > 0)
                    {

                        if (GlobalVars.Global.FE == true && fact.INV_TYPE == 1) //1 = F O B ELECTRONICA
                            MSG_SUNAT = FE.EnvioNotaCredito(fact.INV_ID);

                        int oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                        BEFactura factura = new BEFactura();
                        factura.OWNER = GlobalVars.Global.OWNER;
                        factura.INV_ID = id;
                        //factura.INV_NULLREASON = observacion;
                        factura.LOG_USER_UPDATE = UsuarioActual;
                        factura.OFF_ID = oficina_id;
                        retorno.result = 1;
                        int n = new BLFactura().FacturaCancSinAnul(factura);
                        if (n > 0)
                        {
                            retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                            retorno.result = 1;
                        }
                        else
                        {
                            retorno.message = Constantes.MensajeGenerico.MSG_ERROR_ELIMINAR;
                            retorno.result = 0;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "generarAnulacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        //
        [HttpPost]
        public JsonResult ObtenerFacturasSelImpresion(List<BEFactura> ReglaValor)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    List<BEPreImpresion> ListaPreImpresion = new List<BEPreImpresion>();
                    BEPreImpresion preImpresion = null;
                    ReglaValor.ForEach(s =>
                    {
                        preImpresion = new BEPreImpresion();
                        preImpresion.CodigoDocumento = s.INV_ID;
                        preImpresion.CodigoUsuario = 3;
                        preImpresion.CodigLocal = 2;
                        preImpresion.Estado = "PEN";
                        //preImpresion.Host = System.Net.Dns.GetHostName();                                            
                        preImpresion.Host = Request.ServerVariables["REMOTE_ADDR"];
                        ListaPreImpresion.Add(preImpresion);
                    });

                    var dato = new BLPreImpresion().RegistrarPreImpresionMasiva(ListaPreImpresion);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
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

        #region CONSULTA_FACTURACION_BEC
        public JsonResult ListarConsultaFacturaBec(int skip, int take, int page, int pageSize, string numSerial
                                                , decimal numFact, decimal idSoc,
                                                decimal grupoFact, string moneda, decimal idLic,
                                                string Fini, string Ffin, decimal idFact, int conFecha,
                                                decimal licTipo, decimal idBpsAgen
            )
        {
            decimal idOficinaUsuario = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
            var lista = ListaConsultaBec(GlobalVars.Global.OWNER, numSerial, numFact, idSoc,
                                                grupoFact, moneda, idLic,
                                                Convert.ToDateTime(Fini), Convert.ToDateTime(Ffin), idFact,
                                                licTipo, idBpsAgen, conFecha, idOficinaUsuario,
                                                page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEFacturaConsulta { ListaConsultaFactura = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEFacturaConsulta { ListaConsultaFactura = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEFacturaConsulta> ListaConsultaBec(string owner, string numSerial, decimal numFact, decimal idSoc,
                                                decimal grupoFact, string moneda, decimal idLic,
                                                DateTime Fini, DateTime? Ffin, decimal idFact,
                                                decimal licTipo, decimal idBpsAgen, int conFecha, decimal idOficinaUsuario,
                                                int pagina, int cantRegxPag)
        {
            return new BLFactura().ListarConsultaFacturaBecPage(GlobalVars.Global.OWNER, numSerial, numFact, idSoc,
                                                 grupoFact, moneda, idLic,
                                                 Fini, Ffin, idFact,
                                                 licTipo, idBpsAgen, conFecha, idOficinaUsuario,
                                                 pagina, cantRegxPag);
        }
        #endregion

        #region NOTA_CREDITO

        [HttpPost]
        public JsonResult ObtieneCabeceraFactura(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLFactura servicio = new BLFactura();
                    var datos = new BLFactura().CabeceraFacturaNotaCredito(GlobalVars.Global.OWNER, Id);
                    if (datos != null)
                    {
                        if (datos.GENERAR_NC == 1)//Si Generar NC
                            retorno.result = 1;
                        else if (datos.GENERAR_NC == 0)//NO GENERAR NC PORQUE YA SE GENERO ANTERIORMENTE
                        {
                            retorno.result = 2;
                            retorno.message = datos.OBS_NC;
                        }
                        retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se encontraron datos";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "obtener Obtiene Cabecera Factura", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult ObtenerDetalleFactura(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var detalle = new BLFactura().DetalleFacturaNotaCredito(GlobalVars.Global.OWNER, Id);

                    if (detalle.FacturaDetalle != null)
                    {
                        detalleFactura = new List<DTOFacturaDetallle>();
                        detalle.FacturaDetalle.ForEach(s =>
                        {
                            detalleFactura.Add(new DTOFacturaDetallle
                            {
                                Id = s.INVL_ID,
                                codFactura = s.INV_ID,
                                codLicencia = s.LIC_ID,
                                NombreLicencia = s.LIC_NAME,
                                NombreEstablecimiento = s.EST_NAME,
                                Periodo = s.PERIODO,
                                FechaPago = s.REC_DATE == null ? string.Empty : s.REC_DATE.ToString(),
                                valorBase = s.INVL_BASE,
                                valorImpuesto = s.INVL_TAXES,
                                valorNeto = s.INVL_NET,
                                BaseCobrado = s.INVL_COLLECTB,
                                ImpuestoCobrado = s.INVL_COLLECTT,
                                NetoCobrado = s.INVL_COLLECTN,
                                Pendiente = s.INVL_BALANCE
                            });
                        });
                        DetalleFacturaTmp = detalleFactura;
                    }

                    if (detalle.Recibos != null)
                    {
                        recibos = new List<DTORecibo>();
                        detalle.Recibos.ForEach(s =>
                        {
                            recibos.Add(new DTORecibo
                            {
                                Id = s.REC_ID,
                                Serie = s.SERIE,
                                FechaPago = s.REC_DATE,
                                Base = s.REC_TBASE,
                                Impuesto = s.REC_TTAXES,
                                Total = s.REC_TTOTAL
                            });
                        });
                        RecibosTmp = recibos;
                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(detalle, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerDetalleFactura", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarDetalleFactura()
        {
            detalleFactura = DetalleFacturaTmp;

            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    var clase = "'ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'";
                    shtml.Append("<table class='tblDetalleFactura' border=0 width='100%;' class='k-grid k-widget' id='tblDetalleFactura'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class=" + clase + " style='width:20px'></th>");
                    shtml.Append("<th class=" + clase + " style='width:3px'></th>");
                    shtml.Append("<th class=" + clase + " style='width:60px;'>Id</th>");
                    shtml.Append("<th class=" + clase + " style='width:0px; display:none'>FacturaId</th>");
                    shtml.Append("<th class=" + clase + " style='width:0px; display:none'>DetalleId</th>");
                    shtml.Append("<th class=" + clase + " style='width:350px;'>Licencia</th>");
                    shtml.Append("<th class=" + clase + " style='width:350px;'>Establecimiento</th>");
                    shtml.Append("<th class=" + clase + " style='width:80px;'>Periodo</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px;'>Base</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px;'>Impuesto</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px;'>Neto</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px;'>Base Cobrado</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px;'>Imp Cobrado</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px;'>Neto Cobrado</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px;'>Pendiente</th>");
                    shtml.Append("<th class=" + clase + " style='width:20px;'>NOTA C</th>");
                    shtml.Append("</tr></thead>");

                    if (detalleFactura.Count > 0)
                    {
                        foreach (var item in detalleFactura.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.AppendFormat("<td style='width:25px; cursor:pointer;'>");
                            shtml.AppendFormat("<a href=# onclick='verDeta({0});'><img id='expand" + item.Id + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.Id);
                            shtml.Append("</td>");

                            shtml.AppendFormat("<td onchange='return Habilitar({1});' style='text-align:center;width:25px'><input type='checkbox' id='{0}'/></td>", "chkFact" + item.Id, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0});' style='cursor:pointer;' class='idPl_Invoice'>{0}</td>", item.Id);
                            shtml.AppendFormat("<td style='display:none'><input type='text' id='txtFacturaId_{0}' value={1} style='width: 0px; text-align:center' readonly='true'></input></td>", item.Id, item.codFactura);
                            shtml.AppendFormat("<td style='display:none'><input type='text' id='txtDetalleId_{0}' value={1} style='width: 0px; text-align:center' readonly='true'></input></td>", item.Id, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer;'>{0}</td>", item.NombreLicencia, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer;'>{0}</td>", item.NombreEstablecimiento, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer; text-align:center;'>{0}</td>", item.Periodo, item.Id);

                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer;' class='sumB'>{0}</td>", item.valorBase, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer;' class='sumI'>{0}</td>", item.valorImpuesto, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer;' class='sumN'>{0}</td>", item.valorNeto, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer;' class='sumBC'>{0}</td>", item.BaseCobrado, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer;' class='sumIC'>{0}</td>", item.ImpuestoCobrado, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer;' class='sumNC'>{0}</td>", item.NetoCobrado, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer;' class='sumP'>{0}</td>", item.Pendiente, item.Id);
                            //shtml.AppendFormat("<td style='text-align:center;'><input type='text' id='txtValorNotaCredito_{0}' value={1} style='width: 60px; text-align:right;' disabled='disabled' name='valor_{0}' class='elm' onkeyup='return obtenerValor({0},{2});'></input></td>", item.Id, "0", item.Pendiente);
                            shtml.AppendFormat("<td style='text-align:center;'><input type='text' id='txtValorNotaCredito_{0}' value={1} style='width: 60px; text-align:right;' disabled='disabled' name='valor_{0}' class='elm' onkeyup='return obtenerValor({0},{2});'></input></td>", item.Id, "''", item.Pendiente);
                            shtml.Append("</tr>");

                            shtml.Append("<tr style='background-color:white'>");
                            shtml.Append("<td style='width:20px'></td>");
                            shtml.Append("<td style='width:3px'></td>");
                            shtml.Append("<td style='width:350px;'></td>");
                            shtml.Append("<td style='width:350px;'></td>");
                            shtml.Append("<td style='width:80px;'></td>");
                            shtml.Append("<td style='width:30px;'></td>");
                            shtml.Append("<td style='width:100%' colspan='10'>");
                            shtml.Append("<div style='display:none;' id='" + "div" + item.Id.ToString() + "'  > ");
                            shtml.Append(getHtmlTableRecibo());
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        }
                    }
                    else
                    {
                        shtml.Append("<tr style='background-color:white'><td colspan=14><b><center>No existen detalles en esta factura.</center></b></td></tr>");
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarDetalleFactura", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DetalleValorNeto()
        {
            Resultado retorno = new Resultado();
            detalleFactura = DetalleFacturaTmp;
            decimal valorNeto = 0;
            decimal pendiente = 0;
            
            if (detalleFactura.Count > 0)
            {
                foreach (var item in detalleFactura.OrderBy(x => x.Id))
                {
                    pendiente += item.Pendiente;
                    valorNeto += item.valorNeto;
                }
                if (pendiente != 0) { valorNeto = pendiente; }
            }
            retorno.message = valorNeto.ToString();
            retorno.result = 1;
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ListarDetalleFactura2()
        {
            detalleFactura = DetalleFacturaTmp;

            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    var clase = "'ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'";
                    shtml.Append("<table class='tblDetalleFactura2' border=0 width='100%;' class='k-grid k-widget' id='tblDetalleFactura2'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class=" + clase + " style='width:20px'></th>");
                    shtml.Append("<th class=" + clase + " style='width:3px'></th>");
                    shtml.Append("<th class=" + clase + " style='width:60px;'>Id</th>");
                    shtml.Append("<th class=" + clase + " style='width:0px; display:none'>FacturaId</th>");
                    shtml.Append("<th class=" + clase + " style='width:0px; display:none'>DetalleId</th>");
                    shtml.Append("<th class=" + clase + " style='width:350px;'>Licencia</th>");
                    shtml.Append("<th class=" + clase + " style='width:350px;'>Establecimiento</th>");
                    shtml.Append("<th class=" + clase + " style='width:80px;'>Periodo</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px;'>Base</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px;'>Impuesto</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px;'>Neto</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px;'>Base Cobrado</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px;'>Imp Cobrado</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px;'>Neto Cobrado</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px;'>Pendiente</th>");
                    shtml.Append("<th class=" + clase + " style='width:20px;'>NOTA C</th>");
                    shtml.Append("</tr></thead>");

                    if (detalleFactura.Count > 0)
                    {
                        foreach (var item in detalleFactura.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.AppendFormat("<td style='width:25px; cursor:pointer;'>");
                            shtml.AppendFormat("<a href=# onclick='verDeta({0});'><img id='expand" + item.Id + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.Id);
                            shtml.Append("</td>");

                            shtml.AppendFormat("<td onchange='return Habilitar({1});' style='text-align:center;width:25px'><input type='checkbox' id='{0}'/></td>", "chkFact" + item.Id, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0});' style='cursor:pointer;' class='idPl_Invoice'>{0}</td>", item.Id);
                            shtml.AppendFormat("<td style='display:none'><input type='text' id='txtFacturaId_{0}' value={1} style='width: 0px; text-align:center' readonly='true'></input></td>", item.Id, item.codFactura);
                            shtml.AppendFormat("<td style='display:none'><input type='text' id='txtDetalleId_{0}' value={1} style='width: 0px; text-align:center' readonly='true'></input></td>", item.Id, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer;'>{0}</td>", item.NombreLicencia, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer;'>{0}</td>", item.NombreEstablecimiento, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer; text-align:center;'>{0}</td>", item.Periodo, item.Id);

                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer;' class='sumB'>{0}</td>", item.valorBase, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer;' class='sumI'>{0}</td>", item.valorImpuesto, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer;' class='sumN'>{0}</td>", item.valorNeto, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer;' class='sumBC'>{0}</td>", item.BaseCobrado, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer;' class='sumIC'>{0}</td>", item.ImpuestoCobrado, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer;' class='sumNC'>{0}</td>", item.NetoCobrado, item.Id);
                            shtml.AppendFormat("<td onclick='return obtenerIdDetalle({0},{1});' style='cursor:pointer;' class='sumP'>{0}</td>", item.Pendiente, item.Id);
                            //shtml.AppendFormat("<td style='text-align:center;'><input type='text' id='txtValorNotaCredito_{0}' value={1} style='width: 60px; text-align:right;' disabled='disabled' name='valor_{0}' class='elm' onkeyup='return obtenerValor({0},{2});'></input></td>", item.Id, "0", item.Pendiente);
                            shtml.AppendFormat("<td style='text-align:center;'><input type='text' id='txtValorNotaCredito_{0}' value={1} style='width: 60px; text-align:right;' disabled='disabled' name='valor_{0}' class='elm' onkeyup='return obtenerValor({0},{2});'></input></td>", item.Id, "''", item.Pendiente);
                            shtml.Append("</tr>");

                            shtml.Append("<tr style='background-color:white'>");
                            shtml.Append("<td style='width:20px'></td>");
                            shtml.Append("<td style='width:3px'></td>");
                            shtml.Append("<td style='width:350px;'></td>");
                            shtml.Append("<td style='width:350px;'></td>");
                            shtml.Append("<td style='width:80px;'></td>");
                            shtml.Append("<td style='width:30px;'></td>");
                            shtml.Append("<td style='width:100%' colspan='10'>");
                            shtml.Append("<div style='display:none;' id='" + "div" + item.Id.ToString() + "'  > ");
                            shtml.Append(getHtmlTableRecibo());
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        }
                    }
                    else
                    {
                        shtml.Append("<tr style='background-color:white'><td colspan=14><b><center>No existen detalles en esta factura.</center></b></td></tr>");
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarDetalleFactura", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public StringBuilder getHtmlTableRecibo()
        {
            var recibos = RecibosTmp;

            StringBuilder shtml = new StringBuilder();
            shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
            shtml.Append("<thead>");
            shtml.Append("<tr>");
            shtml.Append("<th class='k-header' style='width:10px; cursor:pointer;'>Id</th>");
            shtml.Append("<th class='k-header' style='width:20px; cursor:pointer;'>Serie</th>");
            shtml.Append("<th class='k-header' style='width:20px; cursor:pointer;'>Fecha Pago</th>");
            shtml.Append("<th class='k-header' style='width:60px; cursor:pointer;'>Base</th>");
            shtml.Append("<th class='k-header' style='width:60px; cursor:pointer;'>Impuesto</th>");
            shtml.Append("<th class='k-header' style='width:60px; cursor:pointer;'>Total</th>");

            if (recibos != null && recibos.Count > 0)
            {
                foreach (var item in recibos.OrderBy(x => x.Id))
                {
                    shtml.Append("<tr style='background-color:white'>");
                    shtml.AppendFormat("<td style='width:10px; text-align:center; cursor:pointer;'>{0}</td>", item.Id);
                    shtml.AppendFormat("<td style='width:20px; text-align:center; cursor:pointer;'>{0}</td>", item.Serie);
                    shtml.AppendFormat("<td style='width:20px; text-align:center; cursor:pointer;'>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FechaPago));
                    shtml.AppendFormat("<td style='width:60px; text-align:center; cursor:pointer;'> {0}</td>", item.Base.ToString("### ###.00"));
                    shtml.AppendFormat("<td style='width:60px; text-align:center; cursor:pointer;'> {0}</td>", item.Impuesto.ToString("### ###.00"));
                    shtml.AppendFormat("<td style='width:60px; text-align:center; cursor:pointer;'> {0}</td>", item.Total.ToString("### ###.00"));
                    shtml.Append("</tr>");
                }
            }
            shtml.Append("</table>");
            return shtml;
        }

        //Factura a aplicar nota de crédito
        public JsonResult ObtenerTipoSerieFactura(string Tipo, string IdSerie, decimal Correlativo)
        {
            Resultado retorno = new Resultado();

            try
            {
                FacturaAUX = new DTOFactura();
                FacturaAUX.tipoFact = Tipo;
                FacturaAUX.idSerieFact = int.Parse(IdSerie);
                FacturaAUX.CorrelativoNC = Correlativo;
                FacturaTmp = FacturaAUX;

                retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                retorno.data = Json(Factura, JsonRequestBehavior.AllowGet);
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerTipoSerieFactura", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GrabarNotaCredito(List<DTOFacturaDetallle> detalleFactura)
        {
            Resultado retorno = new Resultado();

            try
            {
                BEFactura f = new BEFactura();
                BEFactura factura = new BEFactura();
                List<BEFacturaDetalle> fd = new List<BEFacturaDetalle>();
                decimal InvNet = 0;
                decimal InvDescuento = 0;
                decimal InvlNet = 0;
                decimal balanceOriginalDoc = 0;
                int result = 0;

                var TipoNotaCredito = detalleFactura.FirstOrDefault().TipoNotaCredito;
                var Obs = detalleFactura.FirstOrDefault().Motivo;

                if (detalleFactura != null && detalleFactura.Count() != 0)
                {
                    var datoFactura = FacturaTmp;

                    var idFactura = detalleFactura[0].codFactura == null ? 0 : detalleFactura[0].codFactura;

                    if (idFactura != 0)
                    {
                        f = new BLFacturaCobro().ObtenerFacturaAplicar(GlobalVars.Global.OWNER, idFactura);
                        fd = new BLFacturaDetalle().ListarFacturaDetalleAplicar(GlobalVars.Global.OWNER, idFactura);
                        balanceOriginalDoc = f.INV_BALANCE;
                        InvNet = f.INV_NET;
                        InvDescuento = f.INV_DISCOUNTS;
                        foreach (var y in detalleFactura)
                        {
                            foreach (var x in fd)
                            {
                                InvlNet = x.INVL_NET;

                                if (x.INVL_ID == y.Id)
                                {
                                    x.VAL_NOTACREDITO = y.ValorNotaCredito;
                                    x.INVL_NET = y.ValorNotaCredito;
                                    x.INVL_COLLECTN = y.ValorNotaCredito;
                                    x.LOG_USER_CREAT = UsuarioActual;
                                    x.OWNER = GlobalVars.Global.OWNER;

                                    var newInvlNet = ((x.VAL_NOTACREDITO * 100) / InvlNet);

                                    x.INVL_GROSS = (x.INVL_GROSS * (newInvlNet / 100)); //
                                    x.INVL_DISC = (x.INVL_DISC * (newInvlNet / 100)); //
                                    x.INVL_BASE = (x.INVL_BASE * (newInvlNet / 100));
                                    x.INVL_TAXES = (x.INVL_TAXES * (newInvlNet / 100));


                                    x.INVL_COLLECTB = (x.INVL_COLLECTB * (newInvlNet / 100));
                                    x.INVL_COLLECTT = (x.INVL_COLLECTT * (newInvlNet / 100));
                                    x.INVL_COLLECTD = (x.INVL_COLLECTD * (newInvlNet / 100));
                                    //x.INVL_BALANCE = (x.INVL_BALANCE * (newInvlNet / 100));
                                    //x.INVL_BALANCE = (InvlNet - y.ValorNotaCredito);
                                    x.INVL_BALANCE = (x.INVL_NET - x.INVL_COLLECTN);
                                    //INV_CN_TOTAL =Total ingresado de nota de credito
                                    //x.INVL_CN_TOTAL = (x.INVL_CN_TOTAL + y.ValorNotaCredito);
                                    x.INVL_CN_TOTAL = y.ValorNotaCredito;
                                }
                            }
                        }

                        var rateSum = fd.Sum(d => d.VAL_NOTACREDITO);

                        var detalle = fd.Where(p => p.VAL_NOTACREDITO != 0).ToList();
                        //f.INV_TYPE = Convert.ToDecimal(FacturaTmp.tipoFact);
                        f.INV_TYPE = 3;
                        f.INV_NMR = FacturaTmp.idSerieFact;
                        f.INV_NUMBER = FacturaTmp.CorrelativoNC;
                        f.OWNER = GlobalVars.Global.OWNER;
                        f.LOG_USER_CREAT = UsuarioActual;

                        var newInvNet = ((100 * rateSum) / InvNet);
                        f.INV_NET = rateSum;
                        f.INV_COLLECTN = rateSum;
                        f.INV_BASE = f.INV_BASE * (newInvNet / 100);
                        f.INV_TAXES = f.INV_TAXES * (newInvNet / 100);
                        f.INV_COLLECTB = f.INV_COLLECTB * (newInvNet / 100);
                        f.INV_COLLECTT = f.INV_COLLECTT * (newInvNet / 100);
                        //f.INV_BALANCE = f.INV_BALANCE * (newInvNet / 100);
                        //f.INV_BALANCE = (InvNet - rateSum);
                        f.INV_BALANCE = (f.INV_NET - f.INV_COLLECTN);
                        f.INV_KEY = idFactura.ToString();
                        //decimal ofiid = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                        //f.OFF_ID = ofiid;                        
                        f.INV_CN_IND = "S";

                        //INV_CN_TOTAL =TOTAL DE NOTA DE CREDITO INGRESADO
                        f.INV_CN_TOTAL = f.INV_CN_TOTAL + rateSum;

                        //varaible para enviar el total y poder sumar el valor de INV_CN_TOTAL
                        decimal totalCN = f.INV_CN_TOTAL;

                        //inseerta el detalle y la factura en uno nuevo.*
                        //necesito el valor de la nueva  ID DE FACT*

                        decimal indicadorNCTotal = 0;
                        if (balanceOriginalDoc == 0)
                            indicadorNCTotal = 1;
                        else
                            indicadorNCTotal = 0;
                        f.INV_IND_NC_TOTAL = indicadorNCTotal;
                        var idnuevafactura = result = new BLFactura().InsertarNotaCredito(f, detalle, UsuarioActual, idFactura);

                        #region Aplicación Nota de crédito - factura
                        factura = new BLFacturaCobro().ObtenerFacturaAplicar(GlobalVars.Global.OWNER, idFactura);

                        if (factura != null && balanceOriginalDoc > 0)
                        {
                            if (GlobalVars.Global.PagoReparto == "PROPORCIONAL")
                            {
                                var por_Base = ((factura.INV_BASE * 100) / factura.INV_NET);
                                var por_Impuesto = ((factura.INV_TAXES * 100) / factura.INV_NET);
                                var por_Descuento = ((factura.INV_DISCOUNTS * 100) / factura.INV_NET);//

                                var val_Recibo_base = (rateSum * (por_Base / 100));
                                var val_Recibo_Impuesto = (rateSum * (por_Impuesto / 100));
                                var val_Recibo_Descuento = (rateSum * (por_Descuento / 100));//

                                //var total = (por_Base + por_Impuesto);
                                //var totpagado = (val_Recibo_base + val_Recibo_Impuesto);
                                var total = (por_Base + por_Impuesto - por_Descuento);
                                var totpagado = (val_Recibo_base + val_Recibo_Impuesto - val_Recibo_Descuento);

                                BEReciboDetalle enr = new BEReciboDetalle();
                                enr.OWNER = GlobalVars.Global.OWNER;
                                enr.INV_ID = idFactura;
                                enr.REC_BASE = val_Recibo_base;
                                enr.REC_TAXES = val_Recibo_Impuesto;
                                enr.REC_DISCOUNTS = val_Recibo_Descuento;//
                                enr.REC_TOTAL = totpagado;
                                enr.LOG_USER_CREAT = UsuarioActual;
                                enr.LOG_USER_UPDATE = UsuarioActual;

                                //result = new BLRecibo().AplicarFactura_Proporcional_NC(enr, factura.INV_BASE, factura.INV_TAXES );

                                //debo enviar detalle = var detalle = fd.Where(p => p.VAL_NOTACREDITO != 0).ToList();
                                //para asi poder obtener la sumatoria
                                result = new BLRecibo().AplicarFactura_Proporcional_NC(enr, factura.INV_BASE, factura.INV_TAXES, factura.INV_DISCOUNTS, idnuevafactura, totalCN);
                            }
                            else if (GlobalVars.Global.PagoReparto == "IMPUESTO-BASE")
                            {
                                bool ImpuestoCero = false;
                                BEFactura facturaIB = new BEFactura();

                                facturaIB = new BLFacturaCobro().ObtenerFacturaAplicar(GlobalVars.Global.OWNER, idFactura);
                                decimal _Base = 0;
                                decimal _Impuesto = 0;
                                decimal total = 0;
                                decimal NewRecTotal = 0;
                                decimal ImpuestoTotal = 0;

                                ImpuestoTotal = facturaIB.INV_TAXES;

                                if (facturaIB.INV_COLLECTB == 0) _Base = facturaIB.INV_BASE;
                                else _Base = (facturaIB.INV_BASE - facturaIB.INV_COLLECTB); NewRecTotal = facturaIB.INV_COLLECTB;

                                if (facturaIB.INV_COLLECTT == 0) _Impuesto = facturaIB.INV_TAXES;
                                else _Impuesto = (facturaIB.INV_TAXES - facturaIB.INV_COLLECTT);

                                if (facturaIB.INV_BALANCE == 0) total = facturaIB.INV_BASE + facturaIB.INV_TAXES;
                                else total = _Base + _Impuesto;


                                if (rateSum > _Impuesto)
                                {
                                    BEReciboDetalle enr = new BEReciboDetalle();
                                    ImpuestoCero = true;
                                    decimal Impuesto = 0;

                                    if (_Impuesto != 0)
                                    {
                                        var BaseACumulado = facturaIB.INV_COLLECTB;
                                        var NetoACumulado = facturaIB.INV_COLLECTN;
                                        var Base = rateSum - _Impuesto;
                                        Impuesto = _Impuesto;
                                        enr.OWNER = GlobalVars.Global.OWNER;
                                        enr.INV_ID = idFactura;
                                        enr.REC_BASE = Base;
                                        enr.REC_TAXES = Impuesto;
                                        enr.REC_TOTAL = rateSum;
                                        enr.REC_TOTAL_PAGAR = rateSum;
                                        enr.LOG_USER_CREAT = UsuarioActual;
                                        enr.LOG_USER_UPDATE = UsuarioActual;
                                        result = new BLRecibo().AplicarFactura_ImpuestoBase_NC(enr, Base, ImpuestoTotal, ImpuestoCero, total, ImpuestoTotal, NetoACumulado, BaseACumulado);
                                    }
                                    else
                                    {
                                        var NetoACumulado = facturaIB.INV_COLLECTN;
                                        var BaseACumulado = facturaIB.INV_COLLECTB;

                                        ImpuestoCero = false;

                                        if (_Impuesto == 0)
                                        {
                                            Impuesto = ImpuestoTotal;
                                            NetoACumulado = 0;
                                        }
                                        else
                                            Impuesto = 0;

                                        var Base = rateSum;
                                        enr.OWNER = GlobalVars.Global.OWNER;
                                        enr.INV_ID = idFactura;
                                        enr.REC_BASE = Base;
                                        enr.REC_TAXES = Impuesto;
                                        enr.REC_TOTAL = rateSum;
                                        enr.REC_TOTAL_PAGAR = rateSum;
                                        enr.LOG_USER_CREAT = UsuarioActual;
                                        enr.LOG_USER_UPDATE = UsuarioActual;
                                        result = new BLRecibo().AplicarFactura_ImpuestoBase_NC(enr, facturaIB.INV_BASE, facturaIB.INV_TAXES, ImpuestoCero, total, ImpuestoTotal, NetoACumulado, BaseACumulado);
                                    }
                                }
                                else
                                {
                                    var BaseACumulado = facturaIB.INV_COLLECTB;
                                    var NetoACumulado = facturaIB.INV_COLLECTN;

                                    var Impuesto = rateSum;
                                    var Base = 0;

                                    BEReciboDetalle enr = new BEReciboDetalle();
                                    enr.OWNER = GlobalVars.Global.OWNER;
                                    enr.INV_ID = idFactura;
                                    enr.REC_BASE = Base;
                                    enr.REC_TAXES = Impuesto;
                                    enr.REC_TOTAL = rateSum;
                                    enr.REC_TOTAL_PAGAR = rateSum;
                                    enr.LOG_USER_CREAT = UsuarioActual;
                                    enr.LOG_USER_UPDATE = UsuarioActual;
                                    result = new BLRecibo().AplicarFactura_ImpuestoBase_NC(enr, facturaIB.INV_BASE, facturaIB.INV_TAXES, ImpuestoCero, total, ImpuestoTotal, NetoACumulado, BaseACumulado);
                                }
                            }
                            else if (GlobalVars.Global.PagoReparto == "BASE-IMPUESTO")
                            {
                                bool BaseCero = false;
                                BEFactura facturaBI = new BEFactura();

                                facturaBI = new BLFacturaCobro().ObtenerFacturaAplicar(GlobalVars.Global.OWNER, idFactura);
                                decimal _Base = 0;
                                decimal _Impuesto = 0;
                                decimal total = 0;
                                decimal NewRecTotal = 0;
                                decimal BaseTotal = 0;

                                BaseTotal = facturaBI.INV_BASE;

                                if (facturaBI.INV_COLLECTB == 0) _Base = facturaBI.INV_BASE;
                                else _Base = (facturaBI.INV_BASE - facturaBI.INV_COLLECTB); NewRecTotal = facturaBI.INV_COLLECTB;

                                if (facturaBI.INV_COLLECTT == 0) _Impuesto = facturaBI.INV_TAXES;
                                else _Impuesto = (facturaBI.INV_TAXES - facturaBI.INV_COLLECTT);

                                if (facturaBI.INV_BALANCE == 0) total = facturaBI.INV_BASE + facturaBI.INV_TAXES;
                                else total = _Base + _Impuesto;

                                if (rateSum > _Base)
                                {
                                    BEReciboDetalle enr = new BEReciboDetalle();
                                    BaseCero = true;
                                    decimal Impuesto = 0;

                                    if (_Base != 0)
                                    {
                                        enr.REC_BASE = _Base;
                                        enr.REC_TAXES = (_Base - rateSum) * -1;
                                        if (enr.REC_TAXES < 0)
                                        {
                                            var rec_taxes = enr.REC_TAXES * -1;
                                            enr.REC_TAXES = rec_taxes;
                                        }
                                        enr.REC_TOTAL = enr.REC_BASE + enr.REC_TAXES;
                                        _Base = BaseTotal;
                                        _Impuesto = (enr.REC_BASE - rateSum) * -1;

                                        if (_Impuesto < 0)
                                        {
                                            var imp = _Impuesto * -1;
                                            _Impuesto = imp;
                                        }
                                    }
                                    else
                                    {
                                        Impuesto = _Impuesto - rateSum;
                                        enr.REC_TAXES = rateSum;
                                        enr.REC_BASE = 0;
                                        enr.REC_TOTAL = enr.REC_BASE + enr.REC_TAXES;
                                        _Base = BaseTotal;
                                        _Impuesto = facturaBI.INV_COLLECTT + rateSum;
                                        if (_Impuesto < 0)
                                        {
                                            var imp = _Impuesto * -1;
                                            _Impuesto = imp;
                                        }
                                    }


                                    enr.OWNER = GlobalVars.Global.OWNER;
                                    enr.INV_ID = idFactura;
                                    enr.REC_TOTAL = rateSum;
                                    enr.REC_TOTAL_PAGAR = rateSum;
                                    enr.LOG_USER_CREAT = UsuarioActual;
                                    enr.LOG_USER_UPDATE = UsuarioActual;
                                    result = new BLRecibo().AplicarFactura_BaseImpuesto(enr, _Base, _Impuesto, BaseCero, total, BaseTotal);
                                }
                                else
                                {
                                    var Base = (_Base - rateSum);
                                    var Impuesto = _Impuesto;
                                    BEReciboDetalle enr = new BEReciboDetalle();
                                    enr.OWNER = GlobalVars.Global.OWNER;
                                    enr.INV_ID = idFactura;
                                    enr.REC_BASE = facturaBI.INV_BASE - Base;
                                    enr.REC_TAXES = 0;
                                    enr.REC_TOTAL = NewRecTotal + rateSum;
                                    enr.REC_TOTAL_PAGAR = rateSum;
                                    enr.LOG_USER_CREAT = UsuarioActual;
                                    enr.LOG_USER_UPDATE = UsuarioActual;
                                    result = new BLRecibo().AplicarFactura_BaseImpuesto(enr, _Base, _Impuesto, BaseCero, total, BaseTotal);
                                }
                            }
                        }
                        else //nc_amount - Actualizar
                        {

                        }
                        #endregion


                        //Agrega el motivo por el cual emite la nota de credito
                        ObsNotaCredito(idnuevafactura, TipoNotaCredito, Obs);

                        //INVOCAR LA WEB SERVICE NOTA DE CREDITO

                        if (GlobalVars.Global.FE == true)
                        {
                            MSG_SUNAT = FE.EnvioNotaCredito(idnuevafactura);
                        }


                        if (result != 0)
                        {
                            retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR + " " + MSG_SUNAT;
                            retorno.data = Json(detalleFactura, JsonRequestBehavior.AllowGet);
                            retorno.result = 1;
                        }
                        else
                        {
                            retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GRABAR;
                            retorno.data = Json(detalleFactura, JsonRequestBehavior.AllowGet);
                            retorno.result = 0;
                        }
                    }
                }
                else
                {
                    retorno.message = "No se ha podido guardar, Ingrese valor mayor a cero para la nota de crédito";
                    retorno.data = Json(detalleFactura, JsonRequestBehavior.AllowGet);
                    retorno.result = 0;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "GrabarNotaCredito", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult ValidarFechaNotaCredito(decimal Id)
        {
            Resultado retorno = new Resultado();

            try
            {
                BEFactura f = new BEFactura();
                f = new BLFacturaCobro().ObtenerFacturaAplicar(GlobalVars.Global.OWNER, Id);
                string fe = String.Format("{0:dd/MM/yyyy}", f.LOG_DATE_CREAT);
                var varFecha = new BLReporte().FechaActualShort();
                DateTime fecha = Convert.ToDateTime(varFecha);

                if (DateTime.Parse(fe).Year == fecha.Year)
                {
                    if (DateTime.Parse(fe).Month < fecha.Month)
                    {
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                    }
                }
                else
                    retorno.result = 0;
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ValidarFechaNotaCredito", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ValidarNCaDocumentoAplicar(decimal NMR_ID, decimal INV_ID)
        {
            Resultado retorno = new Resultado();
            try
            {
                var resp = new BLFactura().ValidaSerieNCDocumentoAplicar(NMR_ID, INV_ID);

                retorno.result = resp;

            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ValidarNCaDocumentoAplicar", ex);
            }


            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion

        [HttpPost]
        public ActionResult QuiebraFactura(decimal id, string observacion)
        {
            Resultado retorno = new Resultado();
            BLFactura bl = new BLFactura();
            int r = bl.ValidaQuiebra(id);

            try
            {
                if (r == 0)
                {
                    bl.EnviarQuiebra(id, observacion, UsuarioActual);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "Ya se encuentra en Quiebra.";
                }
            }
            catch (Exception e)
            {
                var message = e.Message;
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, message);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}

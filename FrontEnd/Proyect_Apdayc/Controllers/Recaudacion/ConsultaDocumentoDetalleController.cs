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
using SGRDA.BL.Consulta;
using System.Globalization;
using SGRDA.Utility;
using System.IO;

namespace Proyect_Apdayc.Controllers.Recaudacion
{
    public class ConsultaDocumentoDetalleController : Base
    {
        // GET: ConsultaDocumentoDetalle
        public const string nomAplicacion = "SRGDA";
        private DateTime FechaSistema = new BLREC_RATES_GRAL().ObtenerFechaSistema();

        #region DETALLE_CONSULTA_DOCUMENTO
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ObtenerCabecera(decimal idFactura)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    List<BEFactura> ListaCabecera = new List<BEFactura>();
                    ListaCabecera = new BLConsultaDocumento().CD_DETALLE_CABECERA(idFactura);

                    StringBuilder htmlCabecera = new StringBuilder();
                    htmlCabecera = GenerarGrillaCabecera(ListaCabecera);
                    retorno.message = htmlCabecera.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerCabecera", ex);
            }
            //return Json(retorno, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult ObtenerLicencia(decimal idFactura)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    List<BELicencias> ListaLicencia = new List<BELicencias>();
                    ListaLicencia = new BLConsultaDocumento().CD_DETALLE_LICENCIA(idFactura);

                    List<BEFacturaDetalle> ListaPeriodos = new List<BEFacturaDetalle>();
                    ListaPeriodos = new BLConsultaDocumento().CD_DETALLE_PERIODOS(idFactura);

                    StringBuilder htmlLicencia = new StringBuilder();
                    htmlLicencia = GenerarGrillaLicencia(ListaLicencia, ListaPeriodos);


                    retorno.message = htmlLicencia.ToString();
                    retorno.TotalFacturas = ListaLicencia.Count();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerLicencia", ex);
            }
            //return Json(retorno, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public StringBuilder GenerarGrillaCabecera(List<BEFactura> ListaCabecera)
        {
            DateTime fechaMinAnulacion = Convert.ToDateTime(FechaSistema.AddDays(-GlobalVars.Global.DiasFechaAnulacion));

            //listar = ListaConsultaTmp;
            int habNC = 0;
            bool habOficina = false;
            int idOficina = Convert.ToInt32(Session[Constantes.Sesiones.CodigoOficina]);
            if (idOficina == 10081 || idOficina == 10154)// TI - GENAREC
                habOficina = true;
            else
                habOficina = false;

            //Resultado retorno = new Resultado();
            StringBuilder shtml = new StringBuilder();
            try
            {

                shtml.Append("<table class='tblFacturaMasiva' border=0 width='100%;' class='k-grid k-widget' id='tblFacturaMasiva'>");
                shtml.Append("<thead><tr>");

                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='text-align:center;width:25px'>");
                ////if (estado == 0)
                ////{
                //    shtml.Append("<input type='checkbox' id='idCheck' name='Check' class='Check' onchange='clickCheck()'>");
                ////}
                //shtml.Append("</th>");
                //shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");

                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Id</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >T.E</th>");
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
                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Ver</th>");
                //shtml.Append("<th style'width:80px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                //shtml.Append("<th style'width:80px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");


                if (ListaCabecera != null)
                {
                    foreach (var item in ListaCabecera.OrderByDescending(x => x.NMR_SERIAL).OrderByDescending(x => x.INV_NUMBER)) //.OrderByDescending(x => x.id))
                    {
                        if (item.INVT_DESC != "NC" && habOficina)
                            habNC = 1;
                        else
                            habNC = 0;

                        shtml.Append("<tr style='background-color:white'>");
                        //shtml.AppendFormat("<td style='width:25px'> ");
                        ////shtml.AppendFormat("<a href=# onclick='verDetaFactura({0});'><img id='expand" + item.INV_ID + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.INV_ID);
                        //shtml.AppendFormat("<a href=# onclick='verDetalleDocumento({0});'><img id='expand" + item.INV_ID + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.INV_ID);
                        //shtml.Append("</td>");
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:45px' onclick='return obtenerId({0},{1});' class='IDCell' >{0}</td>", item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});'>{0}</td>", item.TIPO_EMI_DOC, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); //TIPO EMI M o  A
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});'>{0}</td>", item.INVT_DESC, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); //TIPO DOC
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right' onclick='return obtenerId({1},{2},{3});'>{0}</td>", item.NMR_SERIAL, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // SERIE
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right' onclick='return obtenerId({1},{2},{3});' >{0}</td>", item.INV_NUMBER, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // NUMERO
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.INV_DATE), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);// FECHA EMISION
                        if (item.EST_FACT == 2)// Constantes.EstadoFactura.CANCELADO
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FECHA_CANCELACION), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);// FECHA CANCELACION
                        else
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", "", item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.INV_NULL), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", item.TAXN_NAME, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);// TIPO ODC IDENTIFICACION


                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", item.TAX_ID, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // NUMEROC
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", item.SOCIO, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // SOCIO
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", item.MONEDA, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' onclick='return obtenerId({1},{2},{3});' >{0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INV_NET), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // VALOR NETO
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' onclick='return obtenerId({1},{2},{3});' > {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INV_COLLECTN), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // COBRADO
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' onclick='return obtenerId({1},{2},{3});' > {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INV_BALANCE), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // SALDO

                        if (item.INV_CN_REF != 0) //factRefNotCred
                            shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'><font color='red'>{0} </font></td>", item.INV_CN_REF, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        else
                            shtml.AppendFormat("<td style='cursor:pointer;' style='text-align:right; width:150px; padding-right:10px'> </td>");


                        if (item.INV_TYPE == 1 || item.INV_TYPE == 2)
                        {


                            switch (item.EST_FACT)
                        {
                            case 4: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", Constantes.EstadoFactura.ANULADA, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); break;
                            case 2: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.CANCELADO, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); break;
                            case 1: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'> <font color='green'> {0} </font> </td>", Constantes.EstadoFactura.CANCELADA_PARCIAL, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); break;
                            case 3: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.PENDIENTE_PAGO, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); break;
                            case 11: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'> <font color='green'> {0} </font> </td>", Constantes.EstadoFactura.COBRANZA_DUDOSA, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); break;
                            case 12: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.CASTIGO, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); break;
                            default: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", Constantes.EstadoFactura.ANULADA, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); break;
                            }
                            }
                               else
                        {
                            if (item.EST_FACT == 2 && item.INV_IND_NC_TOTAL == 1) // NC - DEVOLUCION
                                shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'> <font color='green'> {0} </font> </td>", Constantes.EstadoFactura.NC_DEVOLUCION, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                            else if (item.EST_FACT == 2 && item.INV_IND_NC_TOTAL == 0)
                            {
                                if (item.EST_FACT == 2 && item.INV_STATUS_NC == Constantes.EstadosFacturaValor.NC_ANULACION) shtml.AppendFormat("<td style='cursor:pointer;'  style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.NC_ANULACION);
                                if (item.EST_FACT == 2 && item.INV_STATUS_NC == Constantes.EstadosFacturaValor.NC_DESCUENTO) shtml.AppendFormat("<td style='cursor:pointer;'  style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.NC_DESCUENTO);
                                if (item.EST_FACT == 2 && item.INV_STATUS_NC == Constantes.EstadosFacturaValor.NC_ANULADO) shtml.AppendFormat("<td style='cursor:pointer;'  style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.NC_ANULADO);
                                if (item.EST_FACT == 2 && item.INV_STATUS_NC == Constantes.EstadosFacturaValor.NC_OTRO) shtml.AppendFormat("<td style='cursor:pointer;'  style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.NC_OTRO);
                            }

                        }


                            ////ESTADO SUNAT 
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});' width:150px; padding-right:10px'>{0}</td>", item.ESTADO_SUNAT, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        //shtml.AppendFormat("<td style='text-align:center'>");
                        //shtml.AppendFormat("<label onclick='verReporte({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta2.png' border=0 title='{1}'></label>&nbsp;&nbsp;", item.INV_ID, "Ver Comprobante");
                        //shtml.AppendFormat("</td>");



                        //shtml.AppendFormat("<td style='text-align:center ;style='width:80px'>");
                        ////REENVIO DE COMPROBANTE A SUNAT
                        //if (item.ESTADO_SUNAT != Constantes.Mensaje_Sunat.MSG_ACEPTADO)
                        //{
                        //    if (item.TIPO_EMI_DOC == "A")
                        //        shtml.AppendFormat("<label onclick='ReenvioSunat({0});'><img style='cursor:pointer;' src='../Images/iconos/undoMoney.png' border=0 title='{1}'></label>&nbsp;&nbsp;", item.INV_ID, "Reenvío de Comprobante");
                        //    //shtml.AppendFormat("<a href=# onclick='ReenvioSunat({0});'><img src='../Images/iconos/undoMoney.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.id, "Reenvío de Comprobante");
                        //}
                        //shtml.AppendFormat("</td>");


                        //shtml.AppendFormat("<td style='text-align:center ;style='width:80px'>");
                        ////if (item.saldoFactura == item.valorFinal)
                        //if (item.INV_BALANCE == item.INV_NET)
                        //{
                        //    DateTime fechaEmision = item.INV_DATE.Value;
                        //    if (item.TIPO_EMI_DOC == "M" && item.INV_DATE.Value.Month == FechaSistema.Month && item.EST_FACT != 4)
                        //    {
                        //        shtml.AppendFormat("<a href=# onclick='eliminarFactura({0});'><img src='../Images/iconos/delete.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.INV_ID, "Anular Comprobante");
                        //    }
                        //    //else if (item.tipoFacturaDes == "A" && item.fechaFact.Value.Month == FechaSistema.Month && estado == 0 && item.estadoFact != 4)
                        //    else if (item.TIPO_EMI_DOC == "A" && item.EST_FACT != 4
                        //              && (item.INV_DATE.Value.CompareTo(fechaMinAnulacion) >= 0)
                        //            )
                        //    {
                        //        shtml.AppendFormat("<a href=# onclick='eliminarFactura({0});'><img src='../Images/iconos/delete.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.INV_ID, "Anular Comprobante");
                        //    }
                        //}
                        //shtml.AppendFormat("</td>");


                        //shtml.Append("</tr>");
                        //shtml.Append("<tr><td colspan='30' style='background-color:#DBDBDE'></hr></td></tr>");

                    }
                }
                shtml.Append("</table>");
                //retorno.message = shtml.ToString();
                //retorno.Code = listar.Count;
                //retorno.result = 1;
            }
            catch (Exception ex)
            {
                //retorno.message = ex.Message;
                //retorno.result = 0;
                shtml = null;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerCabecera", ex);
            }
            return shtml;
            //var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
        }

        public StringBuilder GenerarGrillaLicencia(List<BELicencias> ListaLicencia, List<BEFacturaDetalle> ListaPeriodos)
        {
            StringBuilder shtml = new StringBuilder();
            shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
            shtml.Append("<thead>");
            shtml.Append("<tr>");
            shtml.Append("<th class='k-header' style='width:120px;display:none'>Id Factura</th>");
            shtml.Append("<th class='k-header' style='width: 25px;padding-left:10px'></th>");
            shtml.Append("<th class='k-header' style='width: 50px;display:display'>Id Licencia</th>");
            shtml.Append("<th class='k-header' style='width:150px'>Licencia</th>");
            shtml.Append("<th class='k-header' style='width:350px'>Modalidad</th>");
            shtml.Append("<th class='k-header' style='width:350px'>Establecimiento</th>");
            shtml.Append("<th class='k-header' style='width:120px'>Monto</th>");
            shtml.Append("<th class='k-header' style='width:120px'>Descuento</th>");
            shtml.Append("<th class='k-header' style='width:120px'>Impuesto</th>");
            shtml.Append("<th class='k-header' style='width:120px'>Total</th>");

            if (ListaLicencia != null && ListaLicencia.Count > 0)
            {
                foreach (var item in ListaLicencia.OrderBy(x => x.LIC_NAME))
                {
                    shtml.Append("<tr style='background-color:white'>");
                    shtml.AppendFormat("<td style='width:120px;display:none'>{0}</td>", item.INV_ID);

                    shtml.AppendFormat("<td style='width:25px'> ");
                    shtml.AppendFormat("<a href=# onclick='verDetaLic({0},{1});'><img id='expandLic" + item.INV_ID + "-" + item.LIC_ID + "'  src='../Images/botones/less.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.INV_ID.ToString(), item.LIC_ID.ToString());
                    shtml.Append("</td>");
                    shtml.AppendFormat("<td style='width:50px;display:display'>{0}</td>", item.LIC_ID);
                    shtml.AppendFormat("<td style='width:350px'>{0}</td>", item.LIC_NAME);
                    shtml.AppendFormat("<td style='width:350px'>{0}</td>", item.Modalidad);
                    shtml.AppendFormat("<td style='width:330px'>{0}</td>", item.Establecimiento);
                    shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:10px'> {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INVL_GROSS));
                    shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:10px'> {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INVL_DISC));
                    shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:10px'> {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INVL_TAXES));
                    shtml.AppendFormat("<td style='width:120px;text-align:right;  padding-right:10px'> {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INVL_BASE));
                    shtml.Append("</td>");
                    shtml.Append("</tr>");

                    shtml.Append("<tr style='background-color:white'>");
                    shtml.Append("<td></td>");
                    shtml.Append("<td></td>");
                    shtml.Append("<td></td>");
                    shtml.Append("<td></td>");
                    shtml.Append("<td colspan='6'>");

                    shtml.Append("<div style='display:inline;' id='" + "divLic" + item.INV_ID.ToString() + "-" + item.LIC_ID.ToString() + "'  > ");

                    var detallePeriodosXLic = ListaPeriodos.Where(p => p.LIC_ID == item.LIC_ID && p.INV_ID == item.INV_ID).ToList();
                    //shtml.Append(getHtmlTableDetaLicPlanBorrador(item.LIC_ID, item.INV_ID));
                    shtml.Append(getHtmlTableDetaLicPlanBorrador(detallePeriodosXLic));

                    shtml.Append("</div>");
                    shtml.Append("</td>");
                    shtml.Append("</tr>");

                }
            }
            shtml.Append("</table>");
            return shtml;
        }

        public StringBuilder getHtmlTableDetaLicPlanBorrador(List<BEFacturaDetalle> detallePeriodosXLic)
        {
            //var detalle = LicenciaDetalleTmp.Where(p => p.codLicencia == codLic && p.codFactura == codFact).ToList();
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

            if (detallePeriodosXLic != null && detallePeriodosXLic.Count > 0)
            {
                foreach (var item in detallePeriodosXLic.OrderBy(x => x.LIC_DATE))
                {
                    string mes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(item.LIC_MONTH)).ToUpper();
                    shtml.Append("<tr style='background-color:white'>");
                    shtml.AppendFormat("<td style='display:none'>{0}</td>", item.INV_ID);
                    shtml.AppendFormat("<td style='display:none'>{0}</td>", item.LIC_ID);
                    shtml.AppendFormat("<td style='text-align:center'>{0} - {1}</td>", item.LIC_YEAR.ToString(), mes);
                    shtml.AppendFormat("<td style='width:95px;text-align:right;  padding-right:10px'>  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INVL_GROSS));
                    shtml.AppendFormat("<td style='width:95px;text-align:right;  padding-right:10px'>  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INVL_DISC));
                    shtml.AppendFormat("<td style='width:95px;text-align:right;  padding-right:10px'>  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INVL_TAXES));
                    shtml.AppendFormat("<td style='width:90px;text-align:right;  padding-right:10px'>  {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INVL_NET));
                    shtml.Append("</td>");
                    shtml.Append("</tr>");
                }
            }
            shtml.Append("</table>");
            return shtml;
        }

        #endregion


        #region LICENCIA_VER_FACTURA
        public JsonResult VerDetalleFacturaLicencia(decimal idPeriodo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    List<BEFactura> ListaCabecera = new List<BEFactura>();
                    ListaCabecera = new BLConsultaDocumento().LICENCIA_DETALLE_FACTURA_X_PERIODO(idPeriodo);
                    StringBuilder htmlCabecera = new StringBuilder();
                    htmlCabecera = GenerarDetalleFacturaLicencia(ListaCabecera);
                    retorno.message = htmlCabecera.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "VerDetalleFacturaLicencia", ex);
            }
            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public StringBuilder GenerarDetalleFacturaLicencia(List<BEFactura> ListaCabecera)
        {
            DateTime fechaMinAnulacion = Convert.ToDateTime(FechaSistema.AddDays(-GlobalVars.Global.DiasFechaAnulacion));

            int habNC = 0;
            bool habOficina = false;
            int idOficina = Convert.ToInt32(Session[Constantes.Sesiones.CodigoOficina]);
            if (idOficina == 10081 || idOficina == 10154)// TI - GENAREC
                habOficina = true;
            else
                habOficina = false;

            StringBuilder shtml = new StringBuilder();
            try
            {

                shtml.Append("<table class='tblFacturaMasiva' border=0 width='100%;' class='k-grid k-widget' id='tblFacturaMasiva'>");
                shtml.Append("<thead><tr>");

                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='text-align:center;width:25px'>");
                ////if (estado == 0)
                ////{
                //    shtml.Append("<input type='checkbox' id='idCheck' name='Check' class='Check' onchange='clickCheck()'>");
                ////}
                //shtml.Append("</th>");
                //shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");


                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Ver</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Id</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >T.E</th>");
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
                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Ver</th>");
                //shtml.Append("<th style'width:80px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                //shtml.Append("<th style'width:80px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");


                if (ListaCabecera != null)
                {
                    foreach (var item in ListaCabecera.OrderByDescending(x => x.NMR_SERIAL).OrderByDescending(x => x.INV_NUMBER)) //.OrderByDescending(x => x.id))
                    {
                        if (item.INVT_DESC != "NC" && habOficina)
                            habNC = 1;
                        else
                            habNC = 0;

                        shtml.Append("<tr style='background-color:white'>");
                        shtml.Append("</td>");
                        shtml.AppendFormat("<td style='width:25px'> ");
                        //shtml.AppendFormat("<a href=# onclick='verDetaFactura({0});'><img id='expand" + item.INV_ID + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.INV_ID);
                        shtml.AppendFormat("<a href=# onclick='verDetalleDocumento({0});'><img id='expand" + item.INV_ID + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.INV_ID);
                        shtml.Append("</td>");
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:45px' onclick='return obtenerId({0},{1});' class='IDCell' >{0}</td>", item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", item.TIPO_EMI_DOC, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); //TIPO EMI M o  A
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;'>{0}</td>", item.INVT_DESC, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); //TIPO DOC
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right' >{0}</td>", item.NMR_SERIAL, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // SERIE
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right' >{0}</td>", item.INV_NUMBER, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // NUMERO
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.INV_DATE), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);// FECHA EMISION
                        if (item.EST_FACT == 2)// Constantes.EstadoFactura.CANCELADO
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FECHA_CANCELACION), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);// FECHA CANCELACION
                        else
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", "", item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.INV_NULL), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;' >{0}</td>", item.TAXN_NAME, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);// TIPO ODC IDENTIFICACION


                        shtml.AppendFormat("<td style='cursor:pointer;' >{0}</td>", item.TAX_ID, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // NUMEROC
                        shtml.AppendFormat("<td style='cursor:pointer;' >{0}</td>", item.SOCIO, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // SOCIO
                        shtml.AppendFormat("<td style='cursor:pointer;'  >{0}</td>", item.MONEDA, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px'  >{0}</td>", string.Format("{0:#,###,###,##0.00}", item.INV_NET), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // VALOR NETO
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' > {0}</td>", string.Format("{0:#,###,###,##0.00}", item.INV_COLLECTN), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // COBRADO
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' > {0}</td>", string.Format("{0:#,###,###,##0.00}", item.INV_BALANCE), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // SALDO

                        if (item.INV_CN_REF != 0) //factRefNotCred
                            shtml.AppendFormat("<td style='cursor:pointer;' style='text-align:right; width:150px; padding-right:10px'><font color='red'>{0} </font></td>", item.INV_CN_REF, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        else
                            shtml.AppendFormat("<td style='cursor:pointer;' style='text-align:right; width:150px; padding-right:10px'> </td>");

                        switch (item.EST_FACT)
                        {
                            case 4: shtml.AppendFormat("<td style='cursor:pointer;' style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", Constantes.EstadoFactura.ANULADA, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); break;
                            case 2: shtml.AppendFormat("<td style='cursor:pointer;' style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.CANCELADO, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); break;
                            case 1: shtml.AppendFormat("<td style='cursor:pointer;' style='text-align:right; width:150px; padding-right:10px'> <font color='green'> {0} </font> </td>", Constantes.EstadoFactura.CANCELADA_PARCIAL, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); break;
                            case 3: shtml.AppendFormat("<td style='cursor:pointer;'  style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.PENDIENTE_PAGO, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); break;
                            default: shtml.AppendFormat("<td style='cursor:pointer;'  style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", Constantes.EstadoFactura.ANULADA, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); break;
                        }

                        ////ESTADO SUNAT 
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;'  width:150px; padding-right:10px'>{0}</td>", item.ESTADO_SUNAT, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);


                        shtml.Append("</tr>");
                        shtml.Append("<tr><td colspan='30' style='background-color:#DBDBDE'></hr></td></tr>");

                    }
                }
                shtml.Append("</table>");
                //retorno.message = shtml.ToString();
                //retorno.Code = listar.Count;
                //retorno.result = 1;
            }
            catch (Exception ex)
            {
                //retorno.message = ex.Message;
                //retorno.result = 0;
                shtml = null;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerCabecera", ex);
            }
            return shtml;
            //var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
        }

        #endregion

        #region REFERENCIA
        public JsonResult ObtenerReferencia(decimal idFactura)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    List<BEFactura> ListaCabecera = new List<BEFactura>();
                    ListaCabecera = new BLConsultaDocumento().CD_DETALLE_REFERENCIA(idFactura);

                    StringBuilder htmlCabecera = new StringBuilder();
                    htmlCabecera = GenerarGrillaReferencia(ListaCabecera);
                    retorno.message = htmlCabecera.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerReferencia", ex);
            }
            //return Json(retorno, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public StringBuilder GenerarGrillaReferencia(List<BEFactura> ListaCabecera)
        {
            DateTime fechaMinAnulacion = Convert.ToDateTime(FechaSistema.AddDays(-GlobalVars.Global.DiasFechaAnulacion));

            //listar = ListaConsultaTmp;
            int habNC = 0;
            bool habOficina = false;
            int idOficina = Convert.ToInt32(Session[Constantes.Sesiones.CodigoOficina]);
            if (idOficina == 10081 || idOficina == 10154)// TI - GENAREC
                habOficina = true;
            else
                habOficina = false;

            //Resultado retorno = new Resultado();
            StringBuilder shtml = new StringBuilder();
            try
            {

                shtml.Append("<table class='tblFacturaMasiva' border=0 width='100%;' class='k-grid k-widget' id='tblFacturaMasiva'>");
                shtml.Append("<thead><tr>");

                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='text-align:center;width:25px'>");
                ////if (estado == 0)
                ////{
                //    shtml.Append("<input type='checkbox' id='idCheck' name='Check' class='Check' onchange='clickCheck()'>");
                ////}
                //shtml.Append("</th>");
                //shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");

                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Id</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >T.E</th>");
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
                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Ver</th>");
                //shtml.Append("<th style'width:80px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                //shtml.Append("<th style'width:80px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");


                if (ListaCabecera != null)
                {
                    foreach (var item in ListaCabecera.OrderByDescending(x => x.NMR_SERIAL).OrderByDescending(x => x.INV_NUMBER)) //.OrderByDescending(x => x.id))
                    {
                        if (item.INVT_DESC != "NC" && habOficina)
                            habNC = 1;
                        else
                            habNC = 0;

                        shtml.Append("<tr style='background-color:white'>");
                        //shtml.AppendFormat("<td style='width:25px'> ");
                        ////shtml.AppendFormat("<a href=# onclick='verDetaFactura({0});'><img id='expand" + item.INV_ID + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.INV_ID);
                        //shtml.AppendFormat("<a href=# onclick='verDetalleDocumento({0});'><img id='expand" + item.INV_ID + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.INV_ID);
                        //shtml.Append("</td>");
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:45px' onclick='return obtenerId({0},{1});' class='IDCell' >{0}</td>", item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});'>{0}</td>", item.TIPO_EMI_DOC, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); //TIPO EMI M o  A
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});'>{0}</td>", item.INVT_DESC, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); //TIPO DOC
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right' onclick='return obtenerId({1},{2},{3});'>{0}</td>", item.NMR_SERIAL, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // SERIE
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right' onclick='return obtenerId({1},{2},{3});' >{0}</td>", item.INV_NUMBER, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // NUMERO
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.INV_DATE), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);// FECHA EMISION
                        if (item.EST_FACT == 2)// Constantes.EstadoFactura.CANCELADO
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FECHA_CANCELACION), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);// FECHA CANCELACION
                        else
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", "", item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.INV_NULL), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", item.TAXN_NAME, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);// TIPO ODC IDENTIFICACION


                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", item.TAX_ID, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // NUMEROC
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", item.SOCIO, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // SOCIO
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' >{0}</td>", item.MONEDA, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' onclick='return obtenerId({1},{2},{3});' >{0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INV_NET), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // VALOR NETO
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' onclick='return obtenerId({1},{2},{3});' > {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INV_COLLECTN), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // COBRADO
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' onclick='return obtenerId({1},{2},{3});' > {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INV_BALANCE), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC); // SALDO

                        if (item.INV_CN_REF != 0) //factRefNotCred
                            shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'><font color='red'>{0} </font></td>", item.INV_CN_REF, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        else
                            shtml.AppendFormat("<td style='cursor:pointer;' style='text-align:right; width:150px; padding-right:10px'> </td>");

                        if (item.INV_TYPE == 1 || item.INV_TYPE == 2)
                        {
                            switch (item.EST_FACT)
                            {
                                case 4: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", Constantes.EstadoFactura.ANULADA, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                case 2: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.CANCELADO, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                case 1: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='green'> {0} </font> </td>", Constantes.EstadoFactura.CANCELADA_PARCIAL, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                case 3: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.PENDIENTE_PAGO, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                //case 5: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.SOLICITUD_Nota_Credito, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                //case 6: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.SOLICITUD_QUIEBRA, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                case 11: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='green'> {0} </font></td>", Constantes.EstadoFactura.COBRANZA_DUDOSA, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                case 12: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.CASTIGO, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                default: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", Constantes.EstadoFactura.ANULADA, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                            }
                        }
                        else
                        {

                            if (item.EST_FACT == 2 && item.INV_IND_NC_TOTAL == 1 && item.INV_F1_NC_F2 == 1)
                            {
                                shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'> <font color='green'> {0} </font> </td>", Constantes.EstadoFactura.NC_F1_F2, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);

                            }
                            else
                            {
                                if (item.EST_FACT == 2 && item.INV_IND_NC_TOTAL == 1) // NC - DEVOLUCION
                                    shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'> <font color='green'> {0} </font> </td>", Constantes.EstadoFactura.NC_DEVOLUCION, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                                else if (item.EST_FACT == 2 && item.INV_IND_NC_TOTAL == 0)
                                {
                                    if (item.EST_FACT == 2 && item.INV_STATUS_NC == Constantes.EstadosFacturaValor.NC_ANULACION) shtml.AppendFormat("<td style='cursor:pointer;'  style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.NC_ANULACION);
                                    if (item.EST_FACT == 2 && item.INV_STATUS_NC == Constantes.EstadosFacturaValor.NC_DESCUENTO) shtml.AppendFormat("<td style='cursor:pointer;'  style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.NC_DESCUENTO);
                                    if (item.EST_FACT == 2 && item.INV_STATUS_NC == Constantes.EstadosFacturaValor.NC_ANULADO) shtml.AppendFormat("<td style='cursor:pointer;'  style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.NC_ANULADO);
                                    if (item.EST_FACT == 2 && item.INV_STATUS_NC == Constantes.EstadosFacturaValor.NC_OTRO) shtml.AppendFormat("<td style='cursor:pointer;'  style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.NC_OTRO);
                                }
                                else
                                {
                                    shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", "", item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO);
                                }
                            }
                        }

                        ////ESTADO SUNAT 
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3});' width:150px; padding-right:10px'>{0}</td>", item.ESTADO_SUNAT, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        //shtml.AppendFormat("<td style='text-align:center'>");
                        //shtml.AppendFormat("<label onclick='verReporte({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta2.png' border=0 title='{1}'></label>&nbsp;&nbsp;", item.INV_ID, "Ver Comprobante");
                        //shtml.AppendFormat("</td>");



                        //shtml.AppendFormat("<td style='text-align:center ;style='width:80px'>");
                        ////REENVIO DE COMPROBANTE A SUNAT
                        //if (item.ESTADO_SUNAT != Constantes.Mensaje_Sunat.MSG_ACEPTADO)
                        //{
                        //    if (item.TIPO_EMI_DOC == "A")
                        //        shtml.AppendFormat("<label onclick='ReenvioSunat({0});'><img style='cursor:pointer;' src='../Images/iconos/undoMoney.png' border=0 title='{1}'></label>&nbsp;&nbsp;", item.INV_ID, "Reenvío de Comprobante");
                        //    //shtml.AppendFormat("<a href=# onclick='ReenvioSunat({0});'><img src='../Images/iconos/undoMoney.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.id, "Reenvío de Comprobante");
                        //}
                        //shtml.AppendFormat("</td>");


                        //shtml.AppendFormat("<td style='text-align:center ;style='width:80px'>");
                        ////if (item.saldoFactura == item.valorFinal)
                        //if (item.INV_BALANCE == item.INV_NET)
                        //{
                        //    DateTime fechaEmision = item.INV_DATE.Value;
                        //    if (item.TIPO_EMI_DOC == "M" && item.INV_DATE.Value.Month == FechaSistema.Month && item.EST_FACT != 4)
                        //    {
                        //        shtml.AppendFormat("<a href=# onclick='eliminarFactura({0});'><img src='../Images/iconos/delete.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.INV_ID, "Anular Comprobante");
                        //    }
                        //    //else if (item.tipoFacturaDes == "A" && item.fechaFact.Value.Month == FechaSistema.Month && estado == 0 && item.estadoFact != 4)
                        //    else if (item.TIPO_EMI_DOC == "A" && item.EST_FACT != 4
                        //              && (item.INV_DATE.Value.CompareTo(fechaMinAnulacion) >= 0)
                        //            )
                        //    {
                        //        shtml.AppendFormat("<a href=# onclick='eliminarFactura({0});'><img src='../Images/iconos/delete.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.INV_ID, "Anular Comprobante");
                        //    }
                        //}
                        //shtml.AppendFormat("</td>");


                        //shtml.Append("</tr>");
                        //shtml.Append("<tr><td colspan='30' style='background-color:#DBDBDE'></hr></td></tr>");

                    }
                }
                shtml.Append("</table>");
                //retorno.message = shtml.ToString();
                //retorno.Code = listar.Count;
                //retorno.result = 1;
            }
            catch (Exception ex)
            {
                //retorno.message = ex.Message;
                //retorno.result = 0;
                shtml = null;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerCabecera", ex);
            }
            return shtml;
            //var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
        }


        #endregion

        #region Cobro

        public JsonResult GenerarGrillaCobro(decimal CodigoFactura)
         {

            var Lista = new BLConsultaDocumento().CobrosxFactura(CodigoFactura);
            Resultado retorno = new Resultado();
            StringBuilder shtml = new StringBuilder();
            try
            {

                shtml.Append("<table class='tblFacturaMasiva' border=0 width='100%;' class='k-grid k-widget' id='tblFacturaMasiva'>");
                shtml.Append("<thead><tr>");

                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Codigo</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA CONFIRMACION</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MONTO DEPOSITO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >VOUCHER</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA DEPOSITO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA CREACION</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >OFICINA</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ESTADO</th>");

                if (Lista != null)
                {
                    foreach (var item in Lista.OrderByDescending(x => x.MREC_ID)) //.OrderByDescending(x => x.id))
                    {

                        shtml.Append("<tr style='background-color:white'>");
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' );  onclick='return ObtenerCobro({1},{2})'>{0}</td>", item.MREC_ID,item.REC_ID, item.VERSION); //TIPO EMI M o  A
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' );  onclick='return ObtenerCobro({1},{2})' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FECHA_CONFIR), item.REC_ID, item.VERSION);// FECHA CANCELACION
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' );  onclick='return ObtenerCobro({1},{2}) '>{0}</td>", item.MONTO, item.REC_ID, item.VERSION); //TIPO EMI M o  A
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' );  onclick='return ObtenerCobro({1},{2}) '>{0}</td>", item.VOUCHER, item.REC_ID, item.VERSION); //TIPO EMI M o  A
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' );  onclick='return ObtenerCobro({1},{2}) ' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FECH_DEPO), item.REC_ID, item.VERSION);// FECHA CANCELACION
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' );  onclick='return ObtenerCobro({1},{2}) ' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.MREC_DATE), item.REC_ID, item.VERSION);// FECHA CANCELACION
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' );  onclick='return ObtenerCobro({1},{2}) '>{0}</td>", item.OFICINA, item.REC_ID, item.VERSION); //TIPO EMI M o  A
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' );  onclick='return ObtenerCobro({1},{2}) '>{0}</td>", item.ESTADO_COBRO, item.REC_ID , item.VERSION); //TIPO EMI M o  A
                        

                    }
                }
                shtml.Append("</table>");

                retorno.message = shtml.ToString();
                retorno.result = 1;

            }
            catch (Exception ex)
            {

                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerCobroConsultaDetalle", ex);
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        #endregion

    }
}
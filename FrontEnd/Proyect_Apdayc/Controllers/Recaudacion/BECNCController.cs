using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System.Xml;
using System.Text;
using System.Drawing;
using System.IO;
using System.Net;
using System.Globalization;
using SGRDA.BL.Reporte;

namespace Proyect_Apdayc.Controllers.Recaudacion
{
    public class BECNCController : Base
    {
        // GET: BECNC
        private class Variables
        {
            public const string SessionListaCABCOBROSNC = "___K_SESSION_LISTA_COBRO_NC";
            public const string SessionListaDETCOBROSNC = "___K_SESSION_LISTA_COBRO_DET_NC";
            public const int Si = 1;
            public const int No = 0;
            public const int Cero = 0;
            public const int Uno = 1;
            public const int Observacion = 2;
            public const bool Activo = true;
            public const bool Inactivo = false;
            public const string MensajeInsertCobroExitoso = "EL COBRO FUE INSERTADO CORRECTAMENTE";
            public const string MensajeErrorListarCabezera = "OCURRIO UN ERROR AL LISTAR LA CABEZERA DEL DOCUMENTO(S) | CONTACTAR CON EL ADMINISTRADOR DEL MODULO";
            public const string MensajeErrorListarDetalle = "OCURRIO UN ERROR AL LISTAR EL DETALLE  DEL DOCUMENTO(S) | CONTACTAR CON EL ADMINISTRADOR DEL MODULO";
            public const string MensajeNoHayTemporales = "NO SE ENCONTRO LA INFORMACION SOLICITADA | REFRESCAR EL NAVEGADOR SI EL PROBLEMA CONTINUA CONTACTAR CON EL ADMINISTRADOR DEL MODULO";
            public const string MnesajeErrorInsertCobro = "EL COBRO NO FUE INSERTADO  | POR FAVOR CONTACTAR CON EL ADMINISTRADOR DEL MODULO ";
            public const string MensajeMontoDetalleMayorMontoCabezera = " EL MONTO DEL DETALLE ES MAYOR AL MONTO DE LA NOTA DE CREDITO | POR FAVOR ELIJA UN DOCUMENTO MENOR  O IGUAL AL MONTO DE LA NC";
        }


        private List<BEFactura> ListaCABCOBROSNC
        {
            get
            {
                return (List<BEFactura>)Session[Variables.SessionListaCABCOBROSNC];
            }
            set
            {
                Session[Variables.SessionListaCABCOBROSNC] = value;
            }
        }

        private List<BEFactura> ListaDETCOBROSNC
        {
            get
            {
                return (List<BEFactura>)Session[Variables.SessionListaDETCOBROSNC];
            }
            set
            {
                Session[Variables.SessionListaDETCOBROSNC] = value;
            }
        }

        public ActionResult Index()
        {
            Session.Remove(Variables.SessionListaCABCOBROSNC);
            Session.Remove(Variables.SessionListaDETCOBROSNC);
            Init(false);
            return View();
        }


        public JsonResult ListarCOBRONC(decimal CodigoNC, decimal COdigoSerie, int NUmeroDocumento, int CONFECHA, DateTime FechaEmision, decimal CodigoOficina,int Tipo)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    Session.Remove(Variables.SessionListaCABCOBROSNC);
                    Session.Remove(Variables.SessionListaDETCOBROSNC);

                    var lista = new BLAdministracionCOBRONC().ListarCOBRONC(CodigoNC, COdigoSerie, NUmeroDocumento, CONFECHA, FechaEmision, CodigoOficina, Tipo);

                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblAdministracionCOBRONC' border=0 width='100%;' class='k-grid k-widget' id='tblAdministracionCOBRONC'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO DOCUMENTO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >SERIE </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >NUMERO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA EMISION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >SOCIO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MONTO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >OFICINA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DOCUMENTO REFERENCIA</th>");
                    if (lista != null)
                    {
                        lista.ForEach(item =>
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                            shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center';> ");

                            if(Tipo==Variables.Si)
                                shtml.AppendFormat("<a href=# onclick='verDetalleCobroNCSocio({0});'><img id='expand" + item.INV_ID + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.INV_ID);
                            else
                                shtml.AppendFormat("<a href=# onclick='AbrirPoPupAddFactura({0},{1});'> <img src='../Images/botones/invoice_more.png' title='Agregar factura.' border=0></a>", item.BPS_ID,item.INV_ID);

                            shtml.Append("</td>");
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDEstOri'>{0}</td>", item.INV_ID);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDNomEstOri'>{0}</td>", item.NMR_SERIAL);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDDivEstOri'>{0}</td>", item.INV_NUMBER);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.INV_DATE);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.SOCIO);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.INV_NET);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.NombreOficina);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.INV_CN_REF);
                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:left'; class='IDOfiEstOri'><a href=# onclick='VerNCDetalle({0});'><img src='../Images/iconos/report_deta.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.INV_ID, "Ver");
                            shtml.Append("</tr>");
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td style='width:100%' colspan='20'>");
                            shtml.Append("<div style='display:none;' id='" + "div" + item.INV_ID.ToString() + "'  > ");
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");


                            shtml.AppendFormat("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</tr>");
                        });
                    }
                    shtml.Append("</table>");
                    retorno.result = Variables.Si;
                    retorno.Code = lista.Count;
                    retorno.message = shtml.ToString();

                    retorno.result = Variables.Si;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MensajeErrorListarCabezera;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public JsonResult ListarDetalleCOBRONC(decimal CodigoNC)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var lista = new BLAdministracionCOBRONC().ListarDetalleCOBRONC(CodigoNC); ;
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
                    shtml.Append("<thead>");
                    shtml.Append("<tr>");
                    //shtml.Append("<th class='k-header' style='width:120px'></th>");
                    shtml.Append("<th class='k-header' style='width:120px'>CODIGO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>SERIE</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>NUMERO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>FECHA EMISION</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>SOCIO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>MONTO NETO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>OFICINA</th>");
//                    shtml.Append("<th class='k-header' style='width:120px'>VER.</th>");
                    if (lista != null)
                    {
                        lista.ForEach(item =>
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDEstOri'>{0}</td>", item.INV_ID);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDNomEstOri'>{0}</td>", item.NMR_SERIAL);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDDivEstOri'>{0}</td>", item.INV_NUMBER);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.INV_DATE);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.SOCIO);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.INV_NET);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.NombreOficina);

                            shtml.Append("</tr>");
                        });
                    }
                    shtml.Append("</table>");
                    retorno.result = Variables.Si;
                    retorno.Code = lista.Count;
                    retorno.message = shtml.ToString();

                    retorno.result = Variables.Si;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MensajeErrorListarDetalle;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public JsonResult ListarCOBRONCREG(decimal CodigoNC, decimal COdigoSerie, int NUmeroDocumento, int CONFECHA, DateTime FechaEmision, decimal CodigoOficina, int Tipo)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    Session.Remove(Variables.SessionListaCABCOBROSNC);
                    Session.Remove(Variables.SessionListaDETCOBROSNC);

                    var lista = new BLAdministracionCOBRONC().ListarCOBRONC(CodigoNC, COdigoSerie, NUmeroDocumento, CONFECHA, FechaEmision, CodigoOficina, Tipo);

                        ListaCABCOBROSNC = lista;

                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblAdministracionCOBRONC' border=0 width='100%;' class='k-grid k-widget' id='tblAdministracionCOBRONC'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO DOCUMENTO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >SERIE </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >NUMERO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA EMISION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >SOCIO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MONTO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >OFICINA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DOCUMENTO REFERENCIA</th>");
                    if (lista != null)
                    {
                        lista.ForEach(item =>
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                            shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center';> ");

                            if (Tipo == Variables.Si)
                                shtml.AppendFormat("<a href=# onclick='verDetalleCobroNCSocio({0});'><img id='expand" + item.INV_ID + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.INV_ID);
                            else
                                shtml.AppendFormat("<a href=# onclick='AbrirPoPupAddFactura({0},{1});'> <img src='../Images/botones/invoice_more.png' title='Agregar factura.' border=0></a>", item.BPS_ID, item.INV_ID);

                            shtml.Append("</td>");
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDEstOri'>{0}</td>", item.INV_ID);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDNomEstOri'>{0}</td>", item.NMR_SERIAL);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDDivEstOri'>{0}</td>", item.INV_NUMBER);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.INV_DATE);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.SOCIO);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.INV_NET);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.NombreOficina);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.INV_CN_REF);
                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:left'; class='IDOfiEstOri'><a href=# onclick='VerNCDetalle({0});'><img src='../Images/iconos/report_deta.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.INV_ID, "Ver");
                            shtml.Append("</tr>");
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td style='width:100%' colspan='20'>");
                            shtml.Append("<div style='display:none;' id='" + "divreg" + item.INV_ID.ToString() + "'  > ");
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");


                            shtml.AppendFormat("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</tr>");
                        });
                    }
                    shtml.Append("</table>");
                    retorno.result = Variables.Si;
                    retorno.Code = lista.Count;
                    retorno.message = shtml.ToString();

                    retorno.result = Variables.Si;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MensajeErrorListarCabezera;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public JsonResult ListarDetalleCOBRONCREG(decimal CodigoDocumento)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var lista = new BLAdministracionCOBRONC().ListarDetalleCOBRONCFactSeleccionada(CodigoDocumento); ;

                        ListaDETCOBROSNC = lista;


                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
                    shtml.Append("<thead>");
                    shtml.Append("<tr>");
                    //shtml.Append("<th class='k-header' style='width:120px'></th>");
                    shtml.Append("<th class='k-header' style='width:120px'>CODIGO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>SERIE</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>NUMERO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>FECHA EMISION</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>SOCIO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>MONTO NETO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>OFICINA</th>");
                    //                    shtml.Append("<th class='k-header' style='width:120px'>VER.</th>");
                    if (lista != null)
                    {
                        lista.ForEach(item =>
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDEstOri'>{0}</td>", item.INV_ID);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDNomEstOri'>{0}</td>", item.NMR_SERIAL);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDDivEstOri'>{0}</td>", item.INV_NUMBER);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.INV_DATE);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.SOCIO);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.INV_NET);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.NombreOficina);

                            shtml.Append("</tr>");
                        });
                    }
                    shtml.Append("</table>");
                    retorno.result = Variables.Si;
                    retorno.Code = lista.Count;
                    retorno.message = shtml.ToString();

                    retorno.result = Variables.Si;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MensajeErrorListarDetalle;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult InsertarBECNC()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if((ListaCABCOBROSNC!=null && ListaCABCOBROSNC.Count==Variables.Uno) && (ListaDETCOBROSNC!=null && ListaDETCOBROSNC.Count == Variables.Uno))
                    {
                        if (ListaCABCOBROSNC.FirstOrDefault().INV_NET >= ListaDETCOBROSNC.FirstOrDefault().INV_NET)
                        {
                            var INV_ID_NC = ListaCABCOBROSNC.FirstOrDefault().INV_ID; // obteniendo LA NC
                            var DOCUMENTO1 = ListaCABCOBROSNC.FirstOrDefault().INV_CN_REF; // Documento a crearle el cobro
                            var DOCUMENTO2 = ListaDETCOBROSNC.FirstOrDefault().INV_ID;
                            var MONTO_APLICAR = ListaDETCOBROSNC.FirstOrDefault().INV_NET; // Monto
                            var Usuario = UsuarioActual;

                            var exito = new BLAdministracionCOBRONC().InsertarBECNC(INV_ID_NC, DOCUMENTO1, DOCUMENTO2, MONTO_APLICAR, Usuario); ;

                            if (exito)
                            {
                                retorno.result = Variables.Si;
                                retorno.message = Variables.MensajeInsertCobroExitoso;
                            }
                        }
                        else
                        {
                            retorno.result = Variables.No;
                            retorno.message = Variables.MensajeMontoDetalleMayorMontoCabezera;
                        }
                    }
                    else
                    {
                        retorno.result = Variables.No;
                        retorno.message = Variables.MensajeNoHayTemporales;
                    }


                }
            }
            catch(Exception ex)
            {
                retorno.result = Variables.Cero;
                retorno.message = Variables.MnesajeErrorInsertCobro;
            }

            return Json(retorno,JsonRequestBehavior.AllowGet);
        }
    }
}
using Proyect_Apdayc.Clases;
using SGRDA.BL;
using SGRDA.BL.Consulta;
using SGRDA.BL.Empadronamiento;
using SGRDA.Entities;
using SGRDA.Entities.Empadronamiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Controllers.Empadronamiento
{
    public class DetalleEmpadronamientoController : Base
    {
        // GET: DetalleEmpadronamiento
        public const string nomAplicacion = "SRGDA";
        private DateTime FechaSistema = new BLREC_RATES_GRAL().ObtenerFechaSistema();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ObtenerEmpadronamientoDetalle(decimal LIC_ID)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    List<BEDetalleEmpadronamiento> ListaDetalleEmpadronamiento = new List<BEDetalleEmpadronamiento>();
                    ListaDetalleEmpadronamiento = new BLDetalleEmpadronamiento().ObtenerLista_Matriz_Detalle_EMPADRONAMIENTO(LIC_ID);

                    StringBuilder htmlCabecera = new StringBuilder();
                    htmlCabecera = GenerarGrillaCabecera(ListaDetalleEmpadronamiento);
                    retorno.message = htmlCabecera.ToString();
                    retorno.result = 1;
                    string Name = new BLDetalleEmpadronamiento().Nombre_x_Licencia(Convert.ToInt32(LIC_ID));
                    retorno.valor = Name;

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

        public StringBuilder GenerarGrillaCabecera(List<BEDetalleEmpadronamiento> ListaDetalleEmpadronamiento)
        {
            //DateTime fechaMinAnulacion = Convert.ToDateTime(FechaSistema.AddDays(-GlobalVars.Global.DiasFechaAnulacion));

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
               
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Id</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >T.E</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Tipo</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Serie</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >#</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Fecha</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Cancelación</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Periodo</th>");
                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Anulado</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Ident.</th>");

                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >N° Ident.</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Socio de negocio</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Moneda</th>");
                shtml.Append("<th style'width:35px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Facturado</th>");
                shtml.Append("<th style'width:35px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Cobrado</th>");
                shtml.Append("<th style'width:35px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Saldo Pendiente</th>");
                shtml.Append("<th style'width:35px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Ref N.C</th>");
                //shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Tipo</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Estado</th>");
                shtml.Append("<th style'width:35px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Estado Sunat</th>");
                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Ver</th>");
                //shtml.Append("<th style'width:80px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                //shtml.Append("<th style'width:80px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");


                if (ListaDetalleEmpadronamiento != null)
                {
                    foreach (var item in ListaDetalleEmpadronamiento.OrderByDescending(x=> x.INV_ID)) //.OrderByDescending(x => x.id))
                    {
                        if (item.TIPO != "NC" && habOficina)
                            habNC = 1;
                        else
                            habNC = 0;

                        shtml.Append("<tr style='background-color:white'>");
                        //shtml.AppendFormat("<td style='width:25px'> ");
                        ////shtml.AppendFormat("<a href=# onclick='verDetaFactura({0});'><img id='expand" + item.INV_ID + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.INV_ID);
                        //shtml.AppendFormat("<a href=# onclick='verDetalleDocumento({0});'><img id='expand" + item.INV_ID + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.INV_ID);
                        //shtml.Append("</td>");
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right'; width:45px' class='IDCell' >{0}</td>", item.INV_ID);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center';>{0}</td>", item.TE); //TIPO EMI M o  A
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center';>{0}</td>", item.TIPO); //TIPO DOC
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right' >{0}</td>", item.SERIE); // SERIE
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right'  >{0}</td>", item.NRO); // NUMERO
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FECHA_EMISION));// FECHA EMISION
                        if (item.ESTADO == 2)// Constantes.EstadoFactura.CANCELADO
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FECHA_CANCELACION));// FECHA CANCELACION
                        else
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;'  >{0}</td>", "");
                        shtml.AppendFormat("<td style='cursos:pointer;text-align:center;'>{0}</td>",item.PERIODO);
                        //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.INV_NULL), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;'  >{0}</td>",item.IDENT);// TIPO ODC IDENTIFICACION


                        shtml.AppendFormat("<td style='cursor:pointer;'  >{0}</td>",item.NRO_IDENT); // NUMEROC
                        shtml.AppendFormat("<td style='cursor:pointer;' >{0}</td>",item.SOCIO); // SOCIO
                        shtml.AppendFormat("<td style='cursor:pointer;' >{0}</td>",item.MONEDA);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px'>{0}</td>",item.FACTURADO); // VALOR NETO
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px'> {0}</td>",item.COBRADO); // COBRADO
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px'> {0}</td>",item.SALDO); // SALDO

                        if (item.NC != 0) //factRefNotCred
                            shtml.AppendFormat("<td style='cursor:pointer; style='text-align:right; width:150px; padding-right:10px'><font color='red'>{0} </font></td>");
                        else
                            shtml.AppendFormat("<td style='cursor:pointer;' style='text-align:right; width:150px; padding-right:10px'> </td>");

                        switch (item.ESTADO)
                        {
                            case 4: shtml.AppendFormat("<td style='cursor:pointer; style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", Constantes.EstadoFactura.ANULADA); break;
                            case 2: shtml.AppendFormat("<td style='cursor:pointer; style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.CANCELADO); break;
                            case 1: shtml.AppendFormat("<td style='cursor:pointer; style='text-align:right; width:150px; padding-right:10px'> <font color='green'> {0} </font> </td>", Constantes.EstadoFactura.CANCELADA_PARCIAL); break;
                            case 3: shtml.AppendFormat("<td style='cursor:pointer; style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.PENDIENTE_PAGO); break;
                            default:shtml.AppendFormat("<td style='cursor:pointer; style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", Constantes.EstadoFactura.ANULADA, item.INV_ID); break;
                        }

                        ////ESTADO SUNAT 
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center; width:150px; padding-right:10px'>{0}</td>", item.ESTADO_SUNAT);
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
    }
}
using Proyect_Apdayc.Clases;
using SGRDA.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Controllers.AdministracionArtista
{
    public class AdministracionArtistaController : Base
    {
        // GET: AdministracionArtista
        public ActionResult Index()
        {
            //Session.Remove(Variables.SessionListaReporteCobrosParciales);
            Init(false);
            return View();
        }

        public JsonResult Listar(decimal Lic_Id)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                   

                    var lista = new BLArtista().Listar_Solicitud_Artista(Lic_Id);

                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblAdministracionArtistas' border=0 width='100%;' class='k-grid k-widget' id='tblAdministracionArtistas'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>");
                    shtml.Append("<input type='checkbox' id='idCheck' name='Check' class='Check' onchange='clickCheck()'>");
                    shtml.Append("</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >COD. UNICO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >COD. LICENCIA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >RUBRO </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MODALIDAD </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >NOMBRE SHOW </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FACTURA </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >F. CANCELACION </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ARTISTA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ESTADO </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >OFICINA </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >OBSERVACION </th>");     
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >#</th>");
                    if (lista != null)
                    {
                        lista.ForEach(item =>

                        


                        {
                            shtml.Append("<tr style='background-color:white'>");

                            shtml.AppendFormat("<td style='text-align:center;width:2%' ><input type='checkbox' id='{0}' name='Check' class='Check' />", "chkEstOrigen" + item.SHOW_ID + "-"+item.ARTIST_ID);
                            //shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center';> ");
                            //shtml.AppendFormat("<a href=# onclick='verDetalleCobroSocio({0});'><img id='expand" + item.LIC_ID + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.CodigoCobro);
                            shtml.Append("</td>");
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDCODUNI'>{0}</td>", item.SHOW_ID + "-" + item.ARTIST_ID);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDEstOri'>{0}</td>", item.LIC_ID);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.RUBRO);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.MODALIDAD);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDNomEstOri'>{0}</td>", item.SHOW_NAME);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.FACTURA);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.FEC_CANCELED);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDDivEstOri'>{0}</td>", item.NAME);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.ESTADO);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.OFF_NAME);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.OBSERVACION);
                            
                            

                            //if (item.ESTADO_ID == 1)
                            //{
                            //    shtml.AppendFormat("<img src='../Images/iconos/proceso_pendiente.png' border=0 title='Pentdiente Agregar Artista'>");

                            //}
                            //else if (item.ESTADO_ID == 3)
                            //{
                            //    //htmlShow.AppendFormat("<img src='../Images/iconos/{1}'  onclick='delArtista({0},{3},{4});'  alt='{2}'title='{2}' border=0 style='cursor: hand' >&nbsp;&nbsp;", x.IP_NAME, !(x.ENDS.HasValue) ? "delete.png" : "activate.png", !(x.ENDS.HasValue) ? "Eliminar artista" : "Activar artista", !(x.ENDS.HasValue) ? 1 : 0, x.SHOW_ID);
                            //    shtml.AppendFormat("<img src='../Images/iconos/delete.png'  title='Eliminar artista'  onclick='delArtista({0},);' border=0 style='cursor: hand' >&nbsp;&nbsp;", x.IP_NAME, 1, x.SHOW_ID, x.ARTIST_ID);
                            //    //htmlShow.AppendFormat("<img src='../Images/botones/buscar.png'  onclick='editarArtista({0},{1});'  alt='Actualizar Show' title='Agregar Obras' style='cursor: hand' border=0>&nbsp;&nbsp;", x.SHOW_ID, x.ARTIST_ID_SGS);
                            //}//shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='InactivarCobro({0},{3});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a></td>", item.CodigoCobro, item.EstadoCobro == Variables.Si ? "delete.png" : "activate.png", item.EstadoCobro == Variables.Si ? "Inactivar Cobro" : "Activar Cobro", item.CodigoRecCobro);
                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:left'; class='IDOfiEstOri'><a href=# onclick='editar({0});'><img src='../Images/iconos/report_deta.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.CodigoRecCobro, "Ver");
                            shtml.Append("</tr>");
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td style='width:100%' colspan='20'>");
                            //shtml.Append("<div style='display:none;' id='" + "div" + item.CodigoCobro.ToString() + "'  > ");
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
                    retorno.result = 1;
                    retorno.Code = lista.Count;
                    retorno.message = shtml.ToString();
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = "ERRROR";
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public class BeCodunico
        {
            public string COD_UNICO { get; set; }
        }
        public JsonResult AprobarSolicitudes(List<BeCodunico> ReglaValor)
        {

            Resultado retorno = new Resultado();
            try
            {


                if (!isLogout(ref retorno))
                {

                    var MSG_SUNAT = "";
                    var cant_anulado = 0;
                    int suma = 0;
                    if (GlobalVars.Global.FE == true)
                    {

                        if (ReglaValor.Count > 0 && ReglaValor != null)
                        {
                            foreach(var item in ReglaValor)
                            {
                                var codigos = item.COD_UNICO.Split('-');

                                decimal Show_id = Convert.ToDecimal(codigos[0]);
                                decimal ArtistID = Convert.ToDecimal(codigos[1]);

                                int contador = new BLArtista().Aprobar_Solicitud_Artista(Show_id, ArtistID);

                                suma = suma + 1;

                            }
                           
                        }
                    }

                    retorno.result = 1;
                    retorno.message = (MSG_SUNAT + " SE APROBARON " + suma.ToString() + " DE " + ReglaValor.Count().ToString());
                }

                //retorno.result = 1;

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "EnviarFacturasSeleccionadas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RechazarSolicitudes(List<BeCodunico> ReglaValor)
        {

            Resultado retorno = new Resultado();
            try
            {


                if (!isLogout(ref retorno))
                {

                    var MSG_SUNAT = "";
                    var cant_anulado = 0;
                    int suma = 0;
                    if (GlobalVars.Global.FE == true)
                    {

                        if (ReglaValor.Count > 0 && ReglaValor != null)
                        {
                            foreach (var item in ReglaValor)
                            {
                                var codigos = item.COD_UNICO.Split('-');

                                decimal Show_id = Convert.ToDecimal(codigos[0]);
                                decimal ArtistID = Convert.ToDecimal(codigos[1]);

                                int contador = new BLArtista().Rechazar_Solicitud_Artista(Show_id, ArtistID);

                                suma = suma + contador;

                            }

                        }
                    }

                    retorno.result = 1;
                    retorno.message = (MSG_SUNAT + " SE APROBARON " + suma.ToString() + " DE " + ReglaValor.Count().ToString());
                }

                //retorno.result = 1;

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "EnviarFacturasSeleccionadas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}
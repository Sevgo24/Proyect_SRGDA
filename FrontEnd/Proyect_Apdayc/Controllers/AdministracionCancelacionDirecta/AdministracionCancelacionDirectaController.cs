using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.BL.Reporte;
using SGRDA.DA;
using SGRDA.Entities;
using Proyect_Apdayc.Clases;
using System.Text;


namespace Proyect_Apdayc.Controllers.AdministracionCancelacionDirecta
{
    public class AdministracionCancelacionDirectaController : Base
    {
        // GET: AdministracionCancelacionDirecta

        private class Variables
        {
            public const int SI = 1;
            public const int NO = 0;
            public const bool VERDADERO = true;
            public const bool FALSO = false;
            public const string MSJ_ERROR_AL_LISTAR = "SE PRODUJO UN ERROR AL LISTAR LAS LICENCIAS | POR FAVOR BRINDE LOS PARAMETROS DE CONSULTA Y CONTACTE AL ADMINISTRADOR";
            public const string MSJ_ACTUALIZACION_CORRECTA = "SE ACTUALIZO CORRECTAMENTE LA LICENCIA ";
            public const string MSJ_ACTUALIZACION_NO_CORRECTA = "SE PRODUJO UN ERROR AL ACTUALIZAR LA LICENCIA | CONTACTE CON EL ADMINSITRADOR Y BRINDE LOS PARAMETROS DE ACTUALIZACION (CODIGO DE LICENCIA )";
            public const string MSJ_OFICINA_SIN_PERMISO = "LA OFICINA NO CUENTA CON PERMISOS PARA REALIZAR MODIFICACIONS | SI ESTE ES UN ERROR COMUNIQUESE CON EL ADMINISTRADOR";
        }
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarCancelacionDirecta(decimal CodigoDocumento, decimal CodigoSerie, decimal NumeroDocumento, decimal CodigoSocio,
            decimal Oficina, int ConFecha, DateTime FechaInicio, DateTime FechaFin)
        {
            Resultado retorno = new Resultado();

            try
            {
                if(!isLogout(ref retorno))
                {

                    var lista = new BLAdministracionCancelacionDirecta().ListarCancelacionDirecta(CodigoDocumento, CodigoSerie, NumeroDocumento, CodigoSocio, Oficina, ConFecha, FechaInicio, FechaFin);

                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblAdministracionCancelacionDirecta' border=0 width='100%;' class='k-grid k-widget' id='tblAdministracionCancelacionDirecta'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ID</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FEC. REGISTRO </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >TIPO CANCELACION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DOC</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FEC. EMI.</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >SOCIO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MONEDA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FACTURADO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >APLICADO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >NRO-MEMO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >OFI. RECAUDO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >OFI. RESPONSABLE</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >PROCEDENCIA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DOC-REFERENCIA</th>");
                    if (lista != null)
                    {
                        lista.ForEach(item =>
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                            shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center'; class='IDEstOri'>{0}</td>", item.ID);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:left'; class='IDNomEstOri'>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FechaCreacion));
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDDivEstOri'>{0}</td>", item.NombreTipoCancelacion);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DescripcionTipoDocumento+' '+ item.Serie+'-' + item.NumeroDoc);
                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DescripcionTipoDocumento);
                            //shtml.AppendFormat("<td style='width:3%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.Serie);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FechaEmision)); // FECHA DE 
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.NombreSocio); // FECHA DE 
                            shtml.AppendFormat("<td style='width:3%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DescripcionTipoMoneda); // FECHA DE 
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.NetoDocumento.ToString("N2")); // FECHA DE 
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.NetoAplicar.ToString("N2")); // FECHA DE 
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.Memo); // FECHA DE 
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.NombreOficina); // FECHA DE 
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.NombreOficinaResponsable); // FECHA DE 
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.Procedencia); // FECHA DE 
                            shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.Referencia); // FECHA DE 
                            

                            shtml.AppendFormat("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</tr>");
                        });
                    }
                    shtml.Append("</table>");
                    retorno.result = Variables.SI;
                    retorno.message = shtml.ToString();

                }

            }catch(Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = Variables.MSJ_ERROR_AL_LISTAR;

            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        public JsonResult ListarTipoCancelacionDirecta()
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLAdministracionCancelacionDirecta().ListarTipoCaancelacionDirecta()
                    .Select(c => new SelectListItem
                    {
                        //Value = Convert.ToString(c.PAR_SUBTYPE),
                        Value = Convert.ToString(c.TipoCancelacion),
                        Text = c.NombreTipoCancelacion
                    });
                    retorno.result = Variables.SI;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = "";
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoRequerimiento", ex);
            }

            return Json(retorno,JsonRequestBehavior.AllowGet);
        }


        public JsonResult ObtieneDocumento(decimal CodigoDocumento)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var entidad = new BLAdministracionCancelacionDirecta().ObtieneDocumento(CodigoDocumento);


                    retorno.data = Json(entidad, JsonRequestBehavior.AllowGet);
                    retorno.result = Variables.SI;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = "";
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoRequerimiento", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarControl()
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLAdministracionCancelacionDirecta().ListarControl()
                    .Select(c => new SelectListItem
                    {
                        //Value = Convert.ToString(c.PAR_SUBTYPE),
                        Value = Convert.ToString(c.ControlId),
                        Text = c.DescripcionControl
                    });
                    retorno.result = Variables.SI;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = "";
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoRequerimiento", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult RegistrarCancelacionDirecta(decimal CodigoDocumento,decimal TipoCancelacion ,decimal MontoAplicar,
            decimal  OficinaComisionar,decimal OficinaaResponsable, string NumeroMemo, string AbrevOficinaMemo,DateTime MemoFecha, decimal Origen,string Concepto )
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    BEAdministracionCancelacionDirecta entidad = new BEAdministracionCancelacionDirecta();


                    entidad.CodigoDocumento = CodigoDocumento;
                    entidad.TipoCancelacion = TipoCancelacion;
                    entidad.NumMemo = NumeroMemo;
                    entidad.CodigoOficinaSeleccionada = OficinaComisionar;
                    entidad.CodigoOficinaResponsable = OficinaaResponsable;
                    entidad.NetoAplicar = MontoAplicar;
                    entidad.AbrevOfiMemo = AbrevOficinaMemo;
                    entidad.MemoDate = MemoFecha;
                    entidad.ControlId = Origen;
                    entidad.DescripcionControl = Concepto;
                    entidad.Usuario = UsuarioActual;

                    var datos = new BLAdministracionCancelacionDirecta().RegistrarCancelacionDirecta(entidad);
 
                    retorno.result = Variables.SI;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = "";
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoRequerimiento", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListarOficinaCancelacionDirecta(decimal CodigoDoc)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLAdministracionCancelacionDirecta().ListarOficinaCancelacionDirecta(CodigoDoc)
                    .Select(c => new SelectListItem
                    {
                        //Value = Convert.ToString(c.PAR_SUBTYPE),
                        Value = Convert.ToString(c.CodigoOficinaSeleccionada),
                        Text = c.NombreOficina
                    });
                    retorno.result = Variables.SI;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = "";
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoRequerimiento", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}
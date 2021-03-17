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
    public class AdministracionSolicitudDocumentosController : Base
    {
        // GET: AdministracionSolicitudDocumentos

        private class Variables
        {
            public const int SI = 1;
            public const int NO = 0;
            public const string MSJ_SOLICITUD_ENVIADA = "Se realizo la Solicitud del Documento Correctamente";
            public const string MSJ_SOLICITUD_NO_ENVIADA = "No Se realizo la Solicitud del Documento Correctamente ";
            public const int ES_QUIEBRA = 3;
            public const string QUIEBRA = "CASTIGO";
            public const int ES_NOTA_CREDITO = 1;
            public const string NOTA_CREDITO = "NOTA DE CREDITO";
            public const int ES_COBRANZA_DUDOSA = 2;
            public const string COBRANZA_DUDOSA = "PROV. COBRANZA DUDOSA";
            

        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ActualizarDocumentoSolicitudAprobacion(decimal INV_ID, int TIPO, string DESC, int RESPT)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    int resp = new BLAdministracionSolicitudAprobacion().SolicitudAprobacionDocumento(INV_ID, TIPO, DESC, UsuarioActual, RESPT);

                    if (resp > Variables.NO)
                    {
                        retorno.message = Variables.MSJ_SOLICITUD_ENVIADA;
                        retorno.result = Variables.SI;
                    }
                    else
                    {
                        retorno.message = Variables.MSJ_SOLICITUD_NO_ENVIADA;
                        retorno.result = Variables.NO;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "SolicitudAprobacion", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Listar(decimal INV_ID, decimal SERIE, decimal INV_NUMBER, int CONFECHA, string FECHA_INI, string FECHA_FIN, decimal OFF_ID, int ESTADO_APROB, int TIPO)
        {
            Resultado retorno = new Resultado();
            try
            {

                if (!isLogout(ref retorno))
                {

                    var lista = new BLAdministracionSolicitudAprobacion().Lista(INV_ID, SERIE, INV_NUMBER, CONFECHA, FECHA_INI, FECHA_FIN, OFF_ID, ESTADO_APROB, TIPO);

                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblLicenciasPrin' border=0 width='100%;' class='k-grid k-widget' id='tblLicenciasPrin'>");
                    shtml.Append("<thead><tr>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO DE DOCUMENTO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >SERIE</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >NUMERO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA EMISION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MONTO NETO</th>");

                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >OFICINA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >USUARIO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA SOLICITADA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ESTADO DE APROBACION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >TIPO DE SOLICITUD</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MODIFICAR APROBACION</th>");
                    //shtml.Append("</tr>"); //descomentar
                    if (lista != null)
                    {
                        foreach (var item in lista.OrderBy(x => x.FECHA_QUIEBRA))
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Check' />", "chkEstFin" + item.BPS_ID);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; onclick='verDetalleDocumento({0});' class='IDEstFin'>{0}</td>", item.INV_ID);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:left'; onclick='verDetalleDocumento({1});' class='IDNomEstFin'>{0}</td>", item.SERIE, item.INV_ID);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:left'; onclick='verDetalleDocumento({1});' class='IDDivEstOri'>{0}</td>", item.NUMERO, item.INV_ID);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:left'; onclick='verDetalleDocumento({1});' class='IDNomEstFin'>{0}</td>", item.FECHA_EMISION, item.INV_ID);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:left'; onclick='verDetalleDocumento({1});' class='IDDivEstOri'>{0}</td>", item.NETO, item.INV_ID);
                            shtml.AppendFormat("<td style='width:20%; cursor:pointer;text-align:left';onclick='verDetalleDocumento({1});' class='IDOfiEstOri'>{0}</td>", item.OFICINA, item.INV_ID);
                            shtml.AppendFormat("<td style='width:20%; cursor:pointer;text-align:left';onclick='verDetalleDocumento({1});' class='IDOfiEstOri'>{0}</td>", item.USUARIO_SOLICITANTE, item.INV_ID);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:left'; onclick='verDetalleDocumento({1});' class='IDOfiEstOri'>{0}</td>", item.FECHA_QUIEBRA, item.INV_ID);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:left'; onclick='verDetalleDocumento({1});' class='IDOfiEstOri'>{0}</td>", item.ESTADO, item.INV_ID);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:left'; onclick='verDetalleDocumento({1});' class='IDOfiEstOri'>{0}</td>", (item.TIPO == Variables.ES_NOTA_CREDITO ? Variables.NOTA_CREDITO :"")+ (item.TIPO==Variables.ES_COBRANZA_DUDOSA? Variables.COBRANZA_DUDOSA : "") + (item.TIPO == Variables.ES_QUIEBRA ? Variables.QUIEBRA : "") , item.INV_ID);
                            //shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.ENDS == null ? "ACTIVO" : "INACTIVO");
                            //shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.LOG_USER_UPDAT == null ? "SIN MODIFICACION" : item.LOG_USER_UPDAT);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><label onclick='ModificarAprobacion({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta.png' border=0 title='{1}'></label>&nbsp;&nbsp;</td>", item.INV_ID, "MODIFICAR ESTADO DE APROBACION");
                            //shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><label onclick='MODUSU({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta.png' border=0 title='{1}'></label>&nbsp;&nbsp;</td>", item.BPS_ID, "MODIFICAR DATOS DEL USUARIO ");
                            //shtml.AppendFormat("<td style='width:100%; cursor:pointer;text-align:left; ';' class='IDCellOri' ><input type='radio' id='{0}' name='radio' class='radio' value={0} />{1}</td>", item.LIC_ID, item.LIC_NAME);


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
                    retorno.result = Variables.SI;
                    retorno.Code = lista.Count();

                }

            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Obtiene(decimal INV_ID)
        {
            Resultado retorno = new Resultado();
            try
            {
                BEAdministracionSolicitudAprobacion dato = new BLAdministracionSolicitudAprobacion().ObtieneSOlicitudAprobacion(INV_ID);

                retorno.data = Json(dato, JsonRequestBehavior.AllowGet);

                retorno.result = Variables.SI;

            }
            catch (Exception EX)
            {
                retorno.result = Variables.NO;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListaTipoSolicitudes()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLAdministracionSolicitudAprobacion().ListarTipoSolicitud()
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.VALUE),
                         Text = Convert.ToString(c.DESCRIPCION)
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaDivisionesXOficina", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

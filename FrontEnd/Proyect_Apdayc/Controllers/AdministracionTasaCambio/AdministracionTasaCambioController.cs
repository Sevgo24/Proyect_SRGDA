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

namespace Proyect_Apdayc.Controllers.AdministracionTasaCambio
{
    public class AdministracionTasaCambioController : Base
    {
        // GET: AdministracionTasaCambio
        public ActionResult Index()
        {
            return View();
        }

        public class Variables
        {
            public const int SI = 1;
            public const int NO = 0;
            public const string MSG_ERROR_CONSULTA = "SE PRODUJO UN ERROR AL REALIZAR LA CONSULTA DE TASAS DE CAMBIO - CONSULTE CON EL ADMINISTRADOR DEL MODULO .";
            public const string MSG_GRABA_OK = "SE INSERTO CORRECTAMENTE LA TASA DE CAMBIO DEL DIA -.";
            public const string MSG_GRABA_NO_OK = "NO SE INSERTO CORRECTAMENTE LA TASA DE CAMBIO DEL DIA  - CONSULTE CON EL ADMINISTRADOR DEL MODULO .";
            public const string MSG_SIN_TASA_CAMBIO = "NO SE ENCONTRO TASA DE CAMBIO REGISTRADA DEL DIA  - CONSULTE CON EL RESPONSABLE DEL REGISTRO.";
        }


        public JsonResult ListaTasaCambio(string FEC_INI,string FEC_FIN)
        {
            Resultado retorno = new Resultado();

            try
            {
                if(!isLogout (ref retorno))
                {
                    var lista = new BLREF_CURRENCY_VALUES().ListaTasaDeCambio(FEC_INI,FEC_FIN);
                    //data

                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblTasaCambio' border=0 width='100%;' class='k-grid k-widget' id='tblLicencias'>");
                    shtml.Append("<thead><tr>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA TASA DE CAMBIO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >VALOR DE TASA DE CAMBIO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA DE CREACION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA DE MODIFICACION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >USUARIO DE CREACION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >USUARIO DE MODIFICACION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ESTADO</th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MODIFICAR USUARIO DERECHO</th>");
                    //shtml.Append("</tr>"); //descomentar
                    if (lista != null)
                    {
                        lista.ForEach(item =>
                        {
                            //  foreach (var item in lista.OrderBy(x => x.CUR_DATE))
                            //{
                            shtml.Append("<tr style='background-color:white'>");

                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input onclick='validaSocioModif(" + item.EST_ID + ")' type='checkbox' id='{0}' name='Check' class='Check' />", "chkEstOrigen" + item.EST_ID);
                            shtml.AppendFormat("<td style='width:15%; cursor:pointer;text-align:center'; class='IDEstOri'>{0}</td>", item.CUR_DATE);
                            shtml.AppendFormat("<td style='width:20%; cursor:pointer;text-align:center'; class='IDNomEstOri'>{0}</td>", item.CUR_VALUE);
                            shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:center'; class='IDDivEstOri'>{0}</td>", item.LOG_DATE_CREAT);
                            shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.LOG_DATE_UPDATE);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.LOG_USER_CREAT);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.LOG_USER_UPDATE);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.ENDS);
                            //shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDOfiEstOri'>{0}</td>", item.LOG_USER_UPDAT == null ? "SIN MODIFICACION" : item.LOG_USER_UPDAT);
                            //shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><label onclick='MODIFICAR({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta.png' border=0 title='{1}'></label>&nbsp;&nbsp;</td>", item.EST_ID, "MODIFICAR DATOS DEL ESTABLECIMIENTO");
                            //shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><label onclick='MODUSU({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta.png' border=0 title='{1}'></label>&nbsp;&nbsp;</td>", item.EST_ID, "MODIFICAR DATOS DEL USUARIO ");


                            shtml.AppendFormat("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</tr>");
                        });
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = Variables.SI;
                }
            }catch(Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = Variables.MSG_ERROR_CONSULTA;
            }
            return Json(retorno,JsonRequestBehavior.AllowGet);
        }


        public JsonResult GrabarTasadeCambio(string FECHA, decimal VALOR)
        {
            Resultado retorno = new Resultado();

            try
            {
                BEREF_CURRENCY_VALUES entidad = new BEREF_CURRENCY_VALUES();

                entidad.CUR_VALUE = VALOR;
                entidad.LOG_USER_CREAT = UsuarioActual;

               var resp= new BLREF_CURRENCY_VALUES().GrabarTasaCambio(entidad);

                if (resp)
                {
                    retorno.result = Variables.SI;
                    retorno.message = Variables.MSG_GRABA_OK;
                }

            }catch(Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = Variables.MSG_GRABA_NO_OK;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ConsultaTasaCambio()
        {
            Resultado retorno = new Resultado();

            try
            {

                var resp = new BLREF_CURRENCY_VALUES().ConsultaTasaCambio();

                if (resp==Variables.SI)
                {
                    retorno.result = Variables.SI;
                    //retorno.message = Variables.MSG_GRABA_OK;
                }
                else
                {
                    retorno.result = Variables.NO;
                    retorno.message = Variables.MSG_SIN_TASA_CAMBIO;
                }

            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = Variables.MSG_GRABA_NO_OK;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}
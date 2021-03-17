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


namespace Proyect_Apdayc.Controllers.AdministracionEmisionMensual
{
    public class EmisionMensualController : Base
    {
        // GET: EmisionMensual
        public class Variables
        {
            public const int SI = 1;
            public const int NO = 0;
            public const int FECHAS_REPETIDAS = 2;
            public const string ACTIVO = "ACTIVO";
            public const string INACTIVO = "INACTIVO";
            public const string MENSAJE = " La oficina ha superado los caracteres permitidos | Ha ocurrido un error y este ha sido registrado  , comuniquese con el Administrador del Sistema";
            public const string MENSAJE_OK_MODIF_ = "SE MODIFICO CORRECTAMENTE LA VALIDACION ";
            public const string MENSAJE_FECHAS_REPETIDAS = "NO SE PUDO INSERTAR|MODIFICAR UNA O MAS OFICINAS SE ENCUENTRAN DENTRO DE EL RANGO DE FECHAS DETALLADAS ";
        }


        public ActionResult Index()
        {
            Init(false);
            return View();
        }



        public JsonResult ListarEmisionMensual(string NOM_OFF, int DIA, string FECHA_INICIO, string FECHA_FIN, int ESTADO)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var LISTA = new BLAdministracionEmisionMensual().lista(NOM_OFF, DIA, FECHA_INICIO, FECHA_FIN, ESTADO);


                    // if (LISTA!= null && LISTA.Count>0)
                    //{
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblEmisionMensual' border=0 width='100%;' class='k-grid k-widget' id='tblEmisionMensual'>");
                    shtml.Append("<thead><tr>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'><input type='checkbox' id='idCheck' name='Check' class='Check' onchange='clickCheckTraslado()'></th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Establecimiento Activos </th>");
                    //shtml.Append("<th style='display:none'  class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>ID</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO DE VALIDACION </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >NOMBRE DE OFICINA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DIA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >HORA INICIAL</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >HORA FINAL</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA DE BAJA </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ESTADO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MODIFICAR</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >INACTIVAR</th>");
                    //shtml.Append("</tr>"); //descomentar
                    if (LISTA != null)
                    {
                        //foreach (var item in LISTA.OrderBy(x => x.RANGO_INICIAL))
                        LISTA.ForEach(item =>
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDEstOri'>{0}</td>", item.ID_VALUE);
                            shtml.AppendFormat("<td style='width:30%; cursor:pointer;text-align:left'; class='IDNomEstOri'>{0}</td>", item.DESC_VALUE);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDDivEstOri'>{0}</td>", item.DIA);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.RANGO_INICIAL);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.RANGO_FINAL);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.FECHA_DE_BAJA);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.ESTADO);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='ModificarEmision({0});'><img src='../Images/iconos/report_deta.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.ID_VALUE, "Modificar");
                            // shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='InactivarEmision({0},{1});'><img src='../Images/iconos/delete.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.ID_VALUE, 2, "Inactivar");

                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='InactivarEmision({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a></td>", item.ID_VALUE, item.ESTADO == Variables.ACTIVO ? "delete.png" : "activate.png", item.ESTADO == Variables.ACTIVO ? "Inactivar Emision" : "Activar Emision");
                            //shtml.AppendFormat("<td style='width:100%; cursor:pointer;text-align:left; ';' class='IDCellOri' ><input type='radio' id='{0}' name='radio' class='radio' value={0} />{1}</td>", item.LIC_ID, item.LIC_NAME);
                            //href = javascript:editar('${DISC_ID}', '${LIC_ID}')

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
                    //}

                    retorno.result = Variables.SI;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ObtieneEmisionMensualIDValue(decimal ID)
        {
            Resultado retorno = new Resultado();

            try
            {

                BEAdministracionEmisionMensual dato = new BLAdministracionEmisionMensual().Obtiene(ID);

                retorno.data = Json(dato, JsonRequestBehavior.AllowGet);

                retorno.result = Variables.SI;

            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InactivarEmisionMensual(decimal ID)
        {
            Resultado retorno = new Resultado();

            try
            {

                var res = new BLAdministracionEmisionMensual().InactivaEmisionMensual(ID);

                if (res == Variables.SI)
                    retorno.result = Variables.SI;
                else
                    retorno.result = Variables.NO;
            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditarEmisionMensual(decimal ID, string NOMBRE_OFI, int DIA, string RANGO_INI, string RANGO_FIN,decimal OFF_ID)
        {
            Resultado retorno = new Resultado();

            try
            {

                var res = new BLAdministracionEmisionMensual().ModificarEmisionMensual(ID, NOMBRE_OFI, DIA, RANGO_INI, RANGO_FIN, OFF_ID);

                if (res == Variables.SI)
                {

                    retorno.result = Variables.SI;
                    retorno.message = Variables.MENSAJE_OK_MODIF_;

                }
                else if (res == Variables.FECHAS_REPETIDAS)
                {
                    retorno.result = Variables.NO;
                    retorno.message = Variables.MENSAJE_FECHAS_REPETIDAS;
                }
                else
                {
                    retorno.result = Variables.NO;
                    retorno.message = Variables.MENSAJE;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;

            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ValidaEmisionMensual(decimal ID, int DIA, string FECHA_INI, string FECHA_FIN)
        {
            Resultado retorno = new Resultado();

            try
            {

                var res = new BLAdministracionEmisionMensual().ValidaEmisionMensual(ID,  DIA,  FECHA_INI,  FECHA_FIN);

                if (res == Variables.SI)
                {

                    retorno.result = Variables.SI;
                    retorno.message = Variables.MENSAJE_OK_MODIF_;

                }
                else if (res == Variables.FECHAS_REPETIDAS)
                {
                    retorno.result = Variables.NO;
                    retorno.message = Variables.MENSAJE_FECHAS_REPETIDAS;
                }
                else
                {
                    retorno.result = Variables.NO;
                    retorno.message = Variables.MENSAJE;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;

            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}
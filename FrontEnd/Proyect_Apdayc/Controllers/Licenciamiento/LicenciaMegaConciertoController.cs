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

namespace Proyect_Apdayc.Controllers.Licenciamiento
{
    public class LicenciaMegaConciertoController : Base
    {
        //
        // GET: /LicenciaMegaConcierto/

        public ActionResult Index()
        {
            return View();
        }

        #region Localidad
        public JsonResult ListarLocalidades(decimal codigoLic)
        {
            var listaLocalidad = new BLLicenciaLocalidad().ListarLocalidad(GlobalVars.Global.OWNER, codigoLic);
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table border=0 width='100%;' class='k-grid k-widget' id='tblLocalidades'>");
                shtml.Append("<thead><tr>");
                shtml.Append("<th class='k-header' >Id</th>");
                shtml.Append("<th class='k-header' >Localidad</th>");
                //shtml.Append("<th class='k-header' >Tickets</th>");
               // shtml.Append("<th class='k-header' >Pre. Venta</th>");
                shtml.Append("<th class='k-header' >Precio Venta</th>");
                shtml.Append("<th class='k-header' >Impuesto</th>");
                shtml.Append("<th class='k-header' >Precio Neto</th>");
                shtml.Append("<th class='k-header' >Color</th>");
                //shtml.Append("<th class='k-header' >Usuario Reg.</th>");
                //shtml.Append("<th class='k-header' >Fecha Reg.</th>");
                //shtml.Append("<th class='k-header' >Usuario Mod.</th>");
                //shtml.Append("<th class='k-header' >Fecha Mod.</th>");
                shtml.Append("<th class='k-header' ></th></tr></thead>");

                if (listaLocalidad != null)
                {
                    foreach (var item in listaLocalidad.OrderBy(x => x.SEC_ID))
                    {
                        shtml.Append("<tr class='k-grid-content'>");
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' class='Id'>{0}</td>", item.SEC_ID);

                        if (item.SEC_DESC == null || item.SEC_DESC == "")
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtSecDesc{1}' maxlength='40'           style='width:150px;text-align:left' onblur='cambiosDatosLocales({1})'> </td>", item.SEC_DESC, item.SEC_ID);
                        else
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' ><input type='text' id='txtSecDesc{1}'  maxlength='40' value='{0}' style='width:150 px;text-align:left' onblur='cambiosDatosLocales({1})'> </td>", item.SEC_DESC, item.SEC_ID);


                        //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtTicket{1}'  maxlength='15' value={0} style='width:75px;text-align:right' onblur='cambiosDatosLocales({1})'> </td>", item.SEC_TICKETS, item.SEC_ID);
                        //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtPreVenta{1}'  maxlength='15' value={0} style='width:75px;text-align:right' onblur='calcularMontosLocalidad({1})'> </td>", item.SEC_VALUE, item.SEC_ID);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtBruto{1}'     maxlength='15' value={0} style='width:75px;text-align:right' onblur='calcularMontosLocalidad({1})'> </td>", item.SEC_GROSS, item.SEC_ID);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtImpuesto{1}'  maxlength='15' value={0} style='width:75px;text-align:right' onblur='calcularMontosLocalidad({1})' disabled='disabled'> </td>", item.SEC_TAXES, item.SEC_ID);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <label id='lblNeto{1}'      maxlength='15' value={0} style='width:75px;text-align:right' onblur='cambiosDatosLocales({1})'> {0}</label> </td>", item.SEC_NET, item.SEC_ID);

                        //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtPreVenta{1}'  maxlength='15' value={0} style='width:75px;text-align:right' onblur='cambiosDatosLocales({1})'> </td>", item.SEC_VALUE, item.LIC_SEC_ID);
                        //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtBruto{1}'     maxlength='15' value={0} style='width:75px;text-align:right' onblur='cambiosDatosLocales({1})'> </td>", item.SEC_GROSS, item.LIC_SEC_ID);
                        //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtImpuesto{1}'  maxlength='15' value={0} style='width:75px;text-align:right' onblur='cambiosDatosLocales({1})'> </td>", item.SEC_TAXES, item.LIC_SEC_ID);
                        //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtNeto{1}'      maxlength='15' value={0} style='width:75px;text-align:right' onblur='cambiosDatosLocales({1})'> </td>", item.SEC_NET, item.LIC_SEC_ID);

                        if (item.SEC_COLOR == null || item.SEC_COLOR == "")
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtColor{1}' maxlength='20'           style='width:75px;text-align:right' onblur='cambiosDatosLocales({1})'> </td>", item.SEC_COLOR, item.SEC_ID);
                        else
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtColor{1}' maxlength='20' value={0} style='width:75px;text-align:right' onblur='cambiosDatosLocales({1})'> </td>", item.SEC_COLOR.ToUpper(), item.SEC_ID);

                        //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", item.LOG_USER_CREAT);
                        //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.LOG_DATE_CREAT));
                        //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", item.LOG_USER_UPDATE);
                        //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.LOG_DATE_UPDATE));

                        shtml.AppendFormat("<td style='text-align:center'>");
                        shtml.AppendFormat("<a href=# onclick='eliminarLocalidad({0});'><img src='../Images/iconos/delete.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.SEC_ID, "Eliminar Localidad.");
                        shtml.AppendFormat("</td>");
                        shtml.Append("</tr>");

                        shtml.Append("</div>");
                        shtml.Append("</td>");
                        shtml.Append("</tr>");
                    }
                }
                shtml.Append("</table>");
                retorno.message = shtml.ToString();
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarLocalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddLocalidades(BELicenciaLocalidad Localidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    Localidad.OWNER = GlobalVars.Global.OWNER;
                    Localidad.LOG_USER_CREAT = UsuarioActual;
                    var result = new BLLicenciaLocalidad().InsertarLocalidad(Localidad);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "AddLocalidades", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActualizarLocalidad(BELicenciaLocalidad Localidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    Localidad.OWNER = GlobalVars.Global.OWNER;
                    Localidad.LOG_USER_UPDATE = UsuarioActual;
                    var result = new BLLicenciaLocalidad().ActualizarLocalidad(Localidad);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ActualizarLocalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarLocalidad(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BELicenciaLocalidad Localidad = new BELicenciaLocalidad();
                    var result = new BLLicenciaLocalidad().Eliminar(GlobalVars.Global.OWNER, id);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "EliminarLocalidadAforo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Aforo
        public JsonResult ListarTipoAforo()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLAforo().Listar(GlobalVars.Global.OWNER)
                        .Select(c => new SelectListItem
                        {
                            Value=Convert.ToString(c.CAP_ID),
                            Text=Convert.ToString(c.CAP_DESC)
                        });
                    retorno.result = 1;
                    retorno.data= Json(datos, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarLocalidadAforo(decimal codigoLic)
        {
            var listaLocalidadAforo = new BLLicenciaAforo().Listar(GlobalVars.Global.OWNER, codigoLic,UsuarioActual);
            var listaTipoAforo = new BLAforo().Listar(GlobalVars.Global.OWNER);
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table border=0 width='100%;' class='k-grid k-widget' id='tblLocalidadAforo'>");
                shtml.Append("<thead><tr>");
                shtml.Append("<th class='k-header' >Id</th>");
                shtml.Append("<th class='k-header' >Localidad</th>");
                shtml.Append("<th class='k-header' style='display:none'>TA</th>");

                foreach (var x in listaTipoAforo.OrderBy(x => x.CAP_ID)) {
                shtml.Append("<th class='k-header' id=" +x.CAP_ID+">"+x.CAP_DESC+"</th>");
                }
                //shtml.Append("<th class='k-header' >Pre-Liquidación Tickets</th>");
                //shtml.Append("<th class='k-header' >Pre-Liquidación Neto</th>");
                //shtml.Append("<th class='k-header' >Liquidación Tickets</th>");
                //shtml.Append("<th class='k-header' >Liquidación Neto</th>");

                //shtml.Append("<th class='k-header' >Usuario Reg.</th>");
                //shtml.Append("<th class='k-header' >Fecha Reg.</th>");
                //shtml.Append("<th class='k-header' >Usuario Mod.</th>");
                //shtml.Append("<th class='k-header' >Fecha Mod.</th>");
                shtml.Append("<th class='k-header' ></th></tr></thead>");

                if (listaLocalidadAforo != null)
                {
                    //foreach (var y in listaTipoAforo.OrderBy(y => y.CAP_ID)) {
                    shtml.Append("<tr class='k-grid-content'>");
                    var cont = 0;
                        foreach (var item in listaLocalidadAforo.OrderBy(x => x.ACOUNT_ID))
                        {
                            if (cont == 0)
                            {
                                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' class='Id'>{0}</td>", item.ACOUNT_ID);
                                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <label id='lblLocalidadAforoDesc{1}'>{0}</label> </td>",  item.SEC_DESC, item.ACOUNT_ID);
                                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;display:none' class='IdLocalidad' > <label id='idLocalidad{1}'>{0}</label> </td>", item.SECID, item.ACOUNT_ID);
                                //cont++;
                            }

                                shtml.AppendFormat("<td syle='cursor:pointer;text-align:center;' > <input type=''text' id='txtPreLiqTickets{1}' maxlength='18' value={0} style='width:75px;text-align:right' onblur='cambiosDatosAforo({1})'> </td>", item.TICKET_PRE, item.ACOUNT_ID);
                                cont++;

                                if (listaTipoAforo.Count == cont)
                                {
                                    //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", item.LOG_USER_CREAT);
                                    //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.LOG_DATE_CREAT));
                                    //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", item.LOG_USER_UPDATE);
                                    //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.LOG_DATE_UPDATE)); ;
                                    shtml.Append("</tr>");
                                    cont = 0;
                                }


                        }
                    shtml.Append("</tr>");
                    shtml.Append("</div>");
                    shtml.Append("</td>");
                    shtml.Append("</tr>");

                    shtml.Append("<tr>");
                    //shtml.AppendFormat("<td><button id='btnPreLiquidacion'>PreliQuidar</button></td>");
                    shtml.Append("</tr>");
                    shtml.Append("</td>");
                    shtml.Append("</tr>");
                    //--
                    shtml.Append("<tr>");
                    //shtml.AppendFormat("<td> <button id='btnLiquidacion' >Liquidación</button> </td>");
                    shtml.Append("</tr>");
                    shtml.Append("</div>");
                    shtml.Append("</tr>");
                    //--
                }
                shtml.Append("</table>");
                retorno.message = shtml.ToString();
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarLocalidadAforo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddLocalidadAforo(BELicenciaAforo Aforo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    Aforo.OWNER = GlobalVars.Global.OWNER;
                    Aforo.LOG_USER_CREAT = UsuarioActual;
                    var result = new BLLicenciaAforo().Insertar(Aforo);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "AddLicenciaLocalidades", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActualizarLocalidadAforo(BELicenciaAforo Aforo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    Aforo.OWNER = GlobalVars.Global.OWNER;
                    Aforo.LOG_USER_UPDATE = UsuarioActual;
                    var result = new BLLicenciaAforo().Actualizar(Aforo);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ActualizarLocalidadAforo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarLocalidadAforo(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BELicenciaAforo Aforo = new BELicenciaAforo();
                    Aforo.OWNER = GlobalVars.Global.OWNER;
                    Aforo.ACOUNT_ID = id;
                    Aforo.LOG_USER_UPDATE = UsuarioActual;
                    var result = new BLLicenciaAforo().Eliminar(Aforo);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "EliminarLocalidadAforo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CalculaMontoLiquidarAforo(string CAP_ID, decimal lic_id)
        {

            Resultado retorno = new Resultado();
            try
            {
                if(!isLogout (ref retorno))
                {
                    decimal total = new BLAforo().CalculaMontoLiquidarAforo(CAP_ID, lic_id);
                    retorno.result = 1;
                    retorno.valor =Convert.ToString(total);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult insertaAforoLic(decimal licid, string capid, string cap_iprelq,decimal total)
        {
            Resultado retorno = new Resultado();
            try
            {
               if(!isLogout (ref retorno))
               {

                   new BLAforo().INSERTAR_AFORO_LIC(licid, capid, cap_iprelq, total, UsuarioActual);
                   retorno.result=1;
               }
            }
            catch (Exception ex)
            {

                retorno.result = 0;
            }

            return Json(retorno,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult listarLicenciaConteo(decimal licid, string tipo)
        {
            Resultado retorno = new Resultado();

            try
            {
                if(!isLogout (ref retorno))
                {
                    BELicenciaLocalidadConteo entidad = new BLLicenciaLocalidad().listarLicenciaConteo(licid, tipo);
                    retorno.data = Json(entidad, JsonRequestBehavior.AllowGet);
                    if (entidad != null)
                        retorno.result = 1;
                    else
                        retorno.result = 0;

                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region IGV
        public JsonResult ObtenerIGV(decimal DIVISION)
        {
            Resultado retorno = new Resultado();
            try
            {

                if (!isLogout(ref retorno))
                {

                    decimal igv = new BLREC_TAXES().ObtenerIGV(DIVISION);

                    retorno.result = 1;
                    retorno.valor = Convert.ToString(igv);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
            }
            return Json(retorno,JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Planilla

        public JsonResult ListarShowxLicencia(decimal codlic)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLShow().ListaShowxLicencia(codlic)
                        .Select(c => new SelectListItem
                        {
                            Value = Convert.ToString(c.SHOW_ID),
                            Text = Convert.ToString(c.SHOW_NAME)
                        });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ex)
            {
                retorno.result = 0;

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion       
    }
}

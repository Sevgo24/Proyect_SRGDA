using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using System.Text;

namespace Proyect_Apdayc.Controllers
{
    public class AutorizacionController : Base
    {
        //
        // GET: /Autorizacion/
        public ActionResult Dialogs()
        {
            Init(false);
            return View();
        }
        public ActionResult Index()
        {
            Init(false);
            return View();
        }
       
        public JsonResult Insertar(DTOAutorizacion entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    var fechaValida = true;
                    DateTime fechaRecep=DateTime.Now;
                      try
                      {
                        var  x = Convert.ToDateTime(entidad.FechaInicio);
                        var y = Convert.ToDateTime(entidad.FechaFin);
                      }
                      catch
                      {
                          fechaValida = false;
                      }
                       


                      if (fechaValida)
                      {

                          if (Convert.ToDateTime(entidad.FechaInicio) <= Convert.ToDateTime(entidad.FechaFin))
                          {
                              BLAutorizacion obj = new BLAutorizacion();
                              obj.Insertar(new BEAutorizacion
                              {
                                  OWNER = GlobalVars.Global.OWNER,
                                  LIC_AUT_END = Convert.ToDateTime(entidad.FechaFin),
                                  LIC_AUT_START = Convert.ToDateTime(entidad.FechaInicio),
                                  LIC_AUT_OBS = entidad.Observacion,
                                  LIC_ID = entidad.CodigoLicencia,
                                  LOG_USER_CREAT = UsuarioActual
                              });

                              retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                              retorno.result = 1;
                          }
                          else
                          {

                              retorno.message = "Las fecha inicio debe ser menor a la fecha fin";
                              retorno.result = 0;

                          }
                      }
                      else
                      {

                          retorno.message = "Las fechas deben tener el siguiente formato MM/DD/YYYY";
                          retorno.result = 0;

                      }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Insertar Licencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneAutorizacion(decimal idLic, decimal idAt)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var auto = new BLAutorizacion().ObtenerAutorizacionXLic(GlobalVars.Global.OWNER, idLic, idAt);
                    var autorizacion = new DTOAutorizacion();

                    autorizacion.CodigoAutorizacion = auto.LIC_AUT_ID;
                    autorizacion.FechaInicio = auto.LIC_AUT_START.ToShortDateString();
                    autorizacion.FechaFin = auto.LIC_AUT_END.ToShortDateString();
                    autorizacion.Observacion = auto.LIC_AUT_OBS;

                    retorno.data = Json(autorizacion, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtieneAutorizacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdAutorizacion(DTOAutorizacion entidad)
        {
            Resultado retorno = new Resultado();
            var autoGral = new BEAutorizacion();
            try
            {
                if (!isLogout(ref retorno))
                {

                    var fechaValida = true;
                    DateTime fechaRecep=DateTime.Now;
                      try
                      {
                        var  x = Convert.ToDateTime(entidad.FechaInicio);
                        var y = Convert.ToDateTime(entidad.FechaFin);
                      }
                      catch
                      {
                          fechaValida = false;
                      }



                      if (fechaValida)
                      {

                          if (Convert.ToDateTime(entidad.FechaInicio) <= Convert.ToDateTime(entidad.FechaFin))
                          {
                              autoGral.LIC_AUT_ID = entidad.CodigoAutorizacion;
                              autoGral.LIC_ID = entidad.CodigoLicencia;
                              autoGral.LIC_AUT_START = Convert.ToDateTime(entidad.FechaInicio);
                              autoGral.LIC_AUT_END = Convert.ToDateTime(entidad.FechaFin);
                              autoGral.LIC_AUT_OBS = entidad.Observacion;
                              autoGral.OWNER = GlobalVars.Global.OWNER;
                              autoGral.LOG_USER_UPDATE = UsuarioActual;

                              var result = new BLAutorizacion().Actualizar(autoGral);

                              retorno.result = 1;
                              retorno.Code = Convert.ToInt32(autoGral.LIC_AUT_ID);
                              retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                          }
                          else
                          {

                              retorno.message = "Las fecha inicio debe ser menor a la fecha fin";
                              retorno.result = 0;

                          }
                      }
                      else
                      {

                          retorno.message = "Las fechas deben tener el siguiente formato MM/DD/YYYY";
                          retorno.result = 0;

                      }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "UpdAutorizacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(decimal id, int EsActivo)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    if (EsActivo == 1)
                        new BLAutorizacion().Eliminar(id, GlobalVars.Global.OWNER, UsuarioActual);
                    else
                        new BLAutorizacion().Activar(id, GlobalVars.Global.OWNER, UsuarioActual);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Eliminar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarAutorizacion(decimal codigoLic)
        {
            Resultado retorno = new Resultado();
            var autorizacion = new BLAutorizacion().AutorizacionXLicencia(codigoLic, GlobalVars.Global.OWNER);

            try
            {
                if (!isLogout(ref retorno))
                {
                    string xhtml = "";
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=1 width='100%;'  style='border-collapse: collapse;'  class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Observación</th>");
                    shtml.Append("<th class='k-header'>Fecha Inicio</th>");
                    shtml.Append("<th class='k-header'>Fecha Fin</th>");
                    shtml.Append("<th class='k-header'>Situacion</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");
                    if (autorizacion != null)
                    {
                        autorizacion.ForEach(c =>
                        {
                            var autorizacionDTO = new DTOAutorizacion();
                            autorizacionDTO.CodigoAutorizacion = c.LIC_AUT_ID;
                            autorizacionDTO.Observacion = c.LIC_AUT_OBS;
                            autorizacionDTO.FechaInicio = Convert.ToString(c.LIC_AUT_START);
                            autorizacionDTO.FechaFin = Convert.ToString(c.LIC_AUT_END);
                            autorizacionDTO.Activo = c.ENDS.HasValue ? false : true;

                            xhtml = string.Format("<img class='imgPlus' src='../Images/iconos/plus.png' id='expand{0}' width='24px;' title='Ver Shows de Autorización.' alt='Ver Shows de Autorización.' onclick='verShows({0});'>", autorizacionDTO.CodigoAutorizacion);
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td style='width:15px;'  >{0} </td>", xhtml);//autorizacionDTO.CodigoAutorizacion
                            shtml.AppendFormat("<td > {0}</td>", autorizacionDTO.Observacion);
                            //shtml.AppendFormat("<td ><textarea style='width:500px; height:30px;' readonly>{0}</textarea></td>", autorizacionDTO.Observacion);
                            shtml.AppendFormat("<td >{0}</td>", autorizacionDTO.FechaInicio);
                            shtml.AppendFormat("<td >{0}</td>", autorizacionDTO.FechaFin);
                            shtml.AppendFormat("<td >{0}</td>", autorizacionDTO.Activo == true ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td style='width:80px;' > ");
                            shtml.AppendFormat("<img src='../Images/iconos/edit.png' onclick='updAddAutorizacion({0});'  alt='Editar Autorización' title='Editar Autorización' border=0>&nbsp;&nbsp;", autorizacionDTO.CodigoAutorizacion);
                            shtml.AppendFormat("<img src='../Images/iconos/{1}' onclick='delAddAutorizacion({0},{3});' alt='{2}' title='{2}' border=0>&nbsp;&nbsp;", autorizacionDTO.CodigoAutorizacion, autorizacionDTO.Activo ? "delete.png" : "activate.png", autorizacionDTO.Activo ? "Eliminar Autorizacion" : "Activar Autorizacion", autorizacionDTO.Activo == true ? 1 : 0);
                            shtml.AppendFormat("<img src='../Images/iconos/show.png' onclick='agregarShow({0});' alt='Agregar Nuevo Show' title='Agregar Nuevo Show' border=0>&nbsp;&nbsp;", autorizacionDTO.CodigoAutorizacion);

                            shtml.AppendFormat("</td>");
                            shtml.Append("</tr>");

                            shtml.Append("<tr>");
                            shtml.Append("<td></td>");
                            shtml.AppendFormat("<td colspan='4' id='tdShow_{0}'> ",autorizacionDTO.CodigoAutorizacion);
                            shtml.AppendFormat("<div id='divShow_{0}' style='display:none; width:100%;'> ", autorizacionDTO.CodigoAutorizacion);
                            shtml.Append("</div> ");
                            shtml.Append("</td>");
                            shtml.Append("<td></td>");
                            shtml.Append("</tr>");
                        });
                    }
                    shtml.Append("</tbody></table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarAutorizacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Lista codigo y descripcion de autorizacion para un dropdownlist
        /// </summary>
        /// <param name="codigoLic"></param>
        /// <returns></returns>
        public JsonResult ListItems(decimal codigoLic) {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var autorizaciones = new BLAutorizacion().AutorizacionXLicencia(codigoLic, GlobalVars.Global.OWNER);
                    var items = autorizaciones.Select(c => new SelectListItem
                      {
                          Value = c.LIC_AUT_ID.ToString(),
                          Text = c.LIC_AUT_OBS
                      });
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(items, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListItems", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

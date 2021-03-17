using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using SGRDA.BL;
using SGRDA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Controllers
{
    public class ShowController : Base
    {
        //
        // GET: /Show/


        
        private class Variables
        {
            public const int Si = 1;
            public const int No = 0;
            public const int Cero = 0;
            public const int Dos = 2;
            public const int Uno = 1;
            public const string Vacio = "";

        }
        public JsonResult Insertar(DTOShow entidad)
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
                              if (Convert.ToDateTime(entidad.FechaInicio) <= Convert.ToDateTime(entidad.FechaFin))
                              {
                                  BLShow servicio = new BLShow();
                                  BEShow objE = new BEShow();

                                  objE.OWNER = GlobalVars.Global.OWNER;
                                  objE.SHOW_START = Convert.ToDateTime(entidad.FechaInicio);
                                  objE.SHOW_ENDS = Convert.ToDateTime(entidad.FechaFin);
                                  objE.SHOW_OBSERV = entidad.Observacion;
                                  objE.SHOW_ID = entidad.Codigo;
                                  objE.SHOW_NAME = entidad.Nombre;
                                  objE.SHOW_ORDER = entidad.Orden;
                                  objE.LIC_AUT_ID = entidad.CodigoAutorizacion;


                                  if (entidad.Codigo > 0)
                                  {
                                    var resp = new BLShow().ValidarShowArtistaPlan(Variables.Cero, objE.SHOW_START.ToString("dd/MM/yyyy"), objE.LIC_AUT_ID, Variables.Dos);
                                    objE.LOG_USER_UPDATE = UsuarioActual;
                                    if (resp == Variables.Si)
                                    {
                                        servicio.Actualizar(objE);
                                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                                        retorno.result = 1;
                                    }
                                    else
                                    {

                                        retorno.message = "Debe tener Planificacion en el mes seleccionado | de no tratarse de un LOCAL PERMANENTE comuniquese con el administrador";
                                        retorno.result = 0;
                                    }
                                }
                                  else
                                  {
                                      objE.LOG_USER_CREAT = UsuarioActual;
                                    var resp = new BLShow().ValidarShowArtistaPlan(Variables.Cero,objE.SHOW_START.ToString("dd/MM/yyyy"), objE.LIC_AUT_ID,Variables.Dos);

                                    if (resp == Variables.Si)
                                    {
                                        servicio.Insertar(objE);
                                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                                        retorno.result = 1;
                                    }
                                    else
                                    {

                                        retorno.message = "Debe tener Planificacion en el mes seleccionado | de no tratarse de un LOCAL PERMANENTE comuniquese con el administrador";
                                        retorno.result = 0;
                                    }

                                  }



                              }
                              else
                              {

                                  retorno.message = "Las fecha inicio debe ser menor a la fecha fin";
                                  retorno.result = 0;

                              }
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

        public JsonResult ListarShowHtml(decimal CodigoAutorizacion)
        {
            Resultado retorno = new Resultado();
            string htmlGrid = "";
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLShow servShow = new BLShow();
                    var listShow = servShow.ShowsXAutorizaciones(CodigoAutorizacion, GlobalVars.Global.OWNER);
                    htmlGrid = crearGridShow(listShow);
                    retorno.message = htmlGrid;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "divHtmlShows", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public string crearGridShow(List<BEShow> listShow)
        {
            StringBuilder htmlShow = new StringBuilder();
            htmlShow.Append("<table border=0 width='100%;' style='border-collapse: collapse;' class='k-grid k-widget'>");
            htmlShow.Append("<thead><tr><th class='k-header' colspan='8' style=' text-align:left;height:20px;' >REGISTRO DE SHOWS.</th></tr><tr><th class='k-header' >Id</th><th  class='k-header'>Show</th>");
            htmlShow.Append("<th class='k-header'>Fecha Inicio</th>");
            htmlShow.Append("<th class='k-header'>Fecha Fin</th>");
            htmlShow.Append("<th class='k-header'>Observacion</th>");
            htmlShow.Append("<th class='k-header'>Orden</th>");
            htmlShow.Append("<th class='k-header'>Situacion</th>");
            htmlShow.Append("<th  class='k-header'></th></tr></thead>");

            if (listShow != null && listShow.Count > 0)
            {

                listShow.ForEach(x =>
                {
                    string xhtml = string.Format("<img class='imgPlus' src='../Images/iconos/plus.png' id='expandArtista{0}' width='24px;' title='Ver Artistas del Shows.' alt='Ver Artistas del Show.' onclick='verArtistas({0});'>", x.SHOW_ID);
                    htmlShow.Append("<tr>");
                    htmlShow.Append("<td style='width:15px;' >");
                    htmlShow.AppendFormat("{0}", xhtml); //x.SHOW_ID,
                    htmlShow.Append("</td>");
                    htmlShow.Append("<td>");
                    htmlShow.Append(x.SHOW_NAME);
                    htmlShow.Append("</td>");
                    htmlShow.Append("<td>");
                    htmlShow.Append(x.SHOW_START);
                    htmlShow.Append("</td>");
                    htmlShow.Append("<td>");
                    htmlShow.Append(x.SHOW_ENDS);
                    htmlShow.Append("</td>");
                    htmlShow.Append("<td>");
                    htmlShow.Append(x.SHOW_OBSERV);
                    htmlShow.Append("</td>");
                    htmlShow.Append("<td>");
                    htmlShow.Append(x.SHOW_ORDER);
                    htmlShow.Append("</td>");
                    htmlShow.Append("<td>");
                    htmlShow.Append(!(x.ENDS.HasValue) ? "Activo" : "Inactivo");
                    htmlShow.Append("</td>");
                    htmlShow.Append("<td style='width:80px;' >");

                    htmlShow.AppendFormat("<img src='../Images/iconos/edit.png'  onclick='editarShow({0});'  alt='Actualizar Show' title='Actualizar Show' style='cursor: hand' border=0>&nbsp;&nbsp;", x.SHOW_ID);
                    htmlShow.AppendFormat("<img src='../Images/iconos/{1}'  onclick='delShow({0},{3},{4});'  alt='{2}'title='{2}' border=0 style='cursor: hand' >&nbsp;&nbsp;", x.SHOW_ID, !(x.ENDS.HasValue) ? "delete.png" : "activate.png", !(x.ENDS.HasValue) ? "Eliminar Autorizacion" : "Activar Autorizacion", !(x.ENDS.HasValue) ? 1 : 0, x.LIC_AUT_ID);
                    htmlShow.AppendFormat("<img src='../Images/iconos/artist.png' onclick='addArtista({0});' alt='Agregar artista' title='Agregar artista' style='cursor: hand;width:16px;' border=0>&nbsp;&nbsp;", x.SHOW_ID);


                    htmlShow.Append("</td>");
                    htmlShow.Append("</tr>");

                    htmlShow.Append("<tr>");
                    htmlShow.Append("<td></td>");
                    htmlShow.AppendFormat("<td colspan='6' id='tdArtista_{0}'> ", x.SHOW_ID);
                    htmlShow.AppendFormat("<div id='divArtista_{0}' style='display:none; width:100%;'> ", x.SHOW_ID);
                    htmlShow.Append("</div> ");
                    htmlShow.Append("</td>");
                    htmlShow.Append("</tr>");

                });
            }
            else
            {
                htmlShow.Append("<tr>");
                htmlShow.Append("<td colspan='8'>");
                htmlShow.Append("<center><b>No se encontraron registros de shows.</b></center>");
                htmlShow.Append("</td>");
                htmlShow.Append("</tr>");
            }
            htmlShow.Append("</table>");
            return htmlShow.ToString();
        }

        public JsonResult Obtener(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLShow servicio = new BLShow();
                    var objE = servicio.ObtenerShow(GlobalVars.Global.OWNER, id);

                    DTOShow entidad = new DTOShow
                    {
                        Codigo = objE.SHOW_ID,
                        CodigoAutorizacion = objE.LIC_AUT_ID,
                        FechaFin = objE.SHOW_ENDS.ToShortDateString(),
                        FechaInicio = objE.SHOW_START.ToShortDateString(),
                        Nombre = objE.SHOW_NAME,
                        Observacion = objE.SHOW_OBSERV,
                        Orden = objE.SHOW_ORDER,
                        Activo = objE.ENDS.HasValue ? false : true
                    };

                    retorno.data = Json(entidad, JsonRequestBehavior.AllowGet);
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Obtener ", ex);
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
                    var resp = new BLShow().ValidarShowArtistaPlan(id, Variables.Vacio, Variables.Cero, Variables.Uno);
                    if (resp == Variables.Si)
                    {
                        if (EsActivo == 1)
                            new BLShow().Eliminar(id, GlobalVars.Global.OWNER, UsuarioActual);
                        else
                            new BLShow().Activar(id, GlobalVars.Global.OWNER, UsuarioActual);

                        retorno.result = Variables.Si;
                        retorno.message = "OK";
                    }
                    else
                    {
                        retorno.result = Variables.Cero;
                        retorno.message = "DEBE DE INACTIVAR TODOS LOS ARTISTAS REGISTRADOS";
                    }


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

        public JsonResult ObtenerNombre(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var dato = new BLShow().ObtenerShow(GlobalVars.Global.OWNER, id);
                    if (dato != null)
                    { retorno.valor = dato.SHOW_NAME; }
                    else
                    { retorno.valor = "Show No Encontrado"; }

                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista codigo y descripcion de shows para un dropdownlist
        /// </summary>
        /// <param name="codigoLic"></param>
        /// <returns></returns>
        public JsonResult ListItems(decimal idAutorizacion)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLShow servShow = new BLShow();
                    var shows = servShow.ShowsXAutorizaciones(idAutorizacion, GlobalVars.Global.OWNER);

                    var items = shows.Select(c => new SelectListItem
                    {
                        Value = c.SHOW_ID.ToString(),
                        Text = c.SHOW_NAME
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

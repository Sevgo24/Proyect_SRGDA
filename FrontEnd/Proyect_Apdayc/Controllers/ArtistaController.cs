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
    public class ArtistaController : Base
    {
        //
        // GET: /Artista/

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public JsonResult Listar(int skip, int take, int page, int pageSize, int flag, string nombre)
        {
            var lista = ListarPag(GlobalVars.Global.OWNER, flag, nombre, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEArtista { listaArtista = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEArtista { listaArtista = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEArtista> ListarPag(string owner, int flag, string nombre, int pagina, int cantRegxPag)
        {
            return new BLArtista().ListaArtistaPaginada(owner, flag, nombre, pagina, cantRegxPag);
        }

        public string crearGridShow(List<BEShowArtista> listShow)
        {
            StringBuilder htmlShow = new StringBuilder();
            htmlShow.Append("<table border=0 width='100%;' style='border-collapse: collapse;' class='k-grid k-widget'  >");
            htmlShow.Append("<thead><tr><th class='k-header' colspan='6' style=' text-align:left;height:20px;' >REGISTRO DE ARTISTAS.</th></tr>");
            //htmlShow.Append("<tr><th class='k-header' >Id</th>");
            htmlShow.Append("<th  class='k-header'>Nombre</th>");
            htmlShow.Append("<th class='k-header'>Es Principal</th>");
            htmlShow.Append("<th class='k-header'>Situación</th>");
            htmlShow.Append("<th  class='k-header'></th></tr></thead>");
            htmlShow.Append("<th  class='k-header'></th></tr></thead>");

            if (listShow != null && listShow.Count > 0)
            {

                listShow.ForEach(x =>
                {
                    htmlShow.Append("<tr>");
                    //htmlShow.Append("<td>");
                    //htmlShow.Append(x.ARTIST_ID);
                    //htmlShow.Append("</td>");
                    htmlShow.Append("<td>");
                    htmlShow.Append(x.NAME);
                    htmlShow.Append("</td>");

                    //x.ARTIST_PPAL

                    htmlShow.AppendFormat("<td><input type='checkbox' grupo='main' id='chkMain_{0}'", x.ARTIST_ID);
                    htmlShow.AppendFormat("{0} onclick='darPrioridad({1},{2});' >", x.ARTIST_PPAL == "0" ? "" : "checked", x.IP_NAME, x.SHOW_ID);

                    htmlShow.Append("</td>");
                    htmlShow.Append("<td>");
                    htmlShow.Append(x.ESTADO);
                    htmlShow.Append("</td>");
                    htmlShow.Append("<td style='width:80px;' >");

                    if (x.ESTADO_ID == 1 )
                    {
                        htmlShow.AppendFormat("<img src='../Images/iconos/proceso_pendiente.png' border=0 title='Pentdiente Agregar Artista'>");

                    }
                    else if (x.ESTADO_ID == 2)
                    {
                        //htmlShow.AppendFormat("<img src='../Images/iconos/{1}'  onclick='delArtista({0},{3},{4});'  alt='{2}'title='{2}' border=0 style='cursor: hand' >&nbsp;&nbsp;", x.IP_NAME, !(x.ENDS.HasValue) ? "delete.png" : "activate.png", !(x.ENDS.HasValue) ? "Eliminar artista" : "Activar artista", !(x.ENDS.HasValue) ? 1 : 0, x.SHOW_ID);
                        htmlShow.AppendFormat("<img src='../Images/iconos/delete.png'  title='Eliminar artista'  onclick='delArtista({0},{1},{2},{3});' border=0 style='cursor: hand' >&nbsp;&nbsp;", x.ARTIST_ID, !(x.ENDS.HasValue) ? 1 : 0, x.SHOW_ID,x.ARTIST_ID);
                        //htmlShow.AppendFormat("<img src='../Images/botones/buscar.png'  onclick='editarArtista({0},{1});'  alt='Actualizar Show' title='Agregar Obras' style='cursor: hand' border=0>&nbsp;&nbsp;", x.SHOW_ID, x.ARTIST_ID_SGS);
                    }else if (x.ESTADO_ID == 3)
                    {
                        //htmlShow.AppendFormat("<img src='../Images/iconos/{1}'  onclick='delArtista({0},{3},{4});'  alt='{2}'title='{2}' border=0 style='cursor: hand' >&nbsp;&nbsp;", x.IP_NAME, !(x.ENDS.HasValue) ? "delete.png" : "activate.png", !(x.ENDS.HasValue) ? "Eliminar artista" : "Activar artista", !(x.ENDS.HasValue) ? 1 : 0, x.SHOW_ID);
                        htmlShow.AppendFormat("<img src='../Images/iconos/proceso_denegado.png'  title='Pentdiente Anular Artista'  onclick='delArtista({0},{1},{2},{3});' border=0 style='cursor: hand' >&nbsp;&nbsp;", x.ARTIST_ID, !(x.ENDS.HasValue) ? 1 : 0, x.SHOW_ID, x.ARTIST_ID);

                    }
                    else if (x.ESTADO_ID == 4)
                    {
                        //htmlShow.AppendFormat("<img src='../Images/iconos/{1}'  onclick='delArtista({0},{3},{4});'  alt='{2}'title='{2}' border=0 style='cursor: hand' >&nbsp;&nbsp;", x.IP_NAME, !(x.ENDS.HasValue) ? "delete.png" : "activate.png", !(x.ENDS.HasValue) ? "Eliminar artista" : "Activar artista", !(x.ENDS.HasValue) ? 1 : 0, x.SHOW_ID);
                        htmlShow.AppendFormat("<img src='../Images/iconos/activate.png'  title='Activar artista'  onclick='ActArtista({0},{1},{2},{3});' border=0 style='cursor: hand' >&nbsp;&nbsp;", x.ARTIST_ID, !(x.ENDS.HasValue) ? 1 : 0, x.SHOW_ID, x.ARTIST_ID);
                    }
                    //else if (x.ESTADO_ID == 5 )
                    //{
                    //    //htmlShow.AppendFormat("<img src='../Images/iconos/{1}'  onclick='delArtista({0},{3},{4});'  alt='{2}'title='{2}' border=0 style='cursor: hand' >&nbsp;&nbsp;", x.IP_NAME, !(x.ENDS.HasValue) ? "delete.png" : "activate.png", !(x.ENDS.HasValue) ? "Eliminar artista" : "Activar artista", !(x.ENDS.HasValue) ? 1 : 0, x.SHOW_ID);
                    //    htmlShow.AppendFormat("<img src='../Images/iconos/proceso_pendiente.png'  title='Pendiente Activar Artista'  onclick='delArtista({0},{1},{2});' border=0 style='cursor: hand' >&nbsp;&nbsp;", x.IP_NAME, 1, x.SHOW_ID);
                    //}

                    htmlShow.Append("</td>");

                    htmlShow.Append("<td style='width:80px;' >");

                    if (x.REPORT_ID == 0)
                    {
                        htmlShow.AppendFormat("<img src='../Images/iconos/PLANILLA_ICONO.png' border=0 title='GENERAR PLANILLA' onclick='ValidaModalidad({0},{1});' border=0 style='cursor: hand'>", x.LIC_ID,x.ARTIST_ID);
                    }
                   
                    htmlShow.Append("</td>");
                    htmlShow.Append("</tr>");

                });
            }
            else
            {
                htmlShow.Append("<tr>");
                htmlShow.Append("<td colspan='5'>");
                htmlShow.Append("<center><b>No se encontraron registros de Artistas para el Show.</b></center>");
                htmlShow.Append("</td>");
                htmlShow.Append("</tr>");

            }
            htmlShow.Append("</table>");
            return htmlShow.ToString();

        }

        public JsonResult ListarArtistaHtml(decimal CodigoShow)
        {
            Resultado retorno = new Resultado();
            string htmlGrid = "";
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLShowArtista servShow = new BLShowArtista();
                    var listShow = servShow.ShowsXArtistas(CodigoShow, GlobalVars.Global.OWNER);

                    htmlGrid = crearGridShow(listShow);
                    retorno.message = htmlGrid;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarArtistaHtml", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(string id, int EsActivo,decimal SHOW_ID)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (EsActivo == 1)
                        new BLArtista().Eliminar(id, GlobalVars.Global.OWNER, UsuarioActual, SHOW_ID);
                    else
                        new BLArtista().Activar(id, GlobalVars.Global.OWNER, UsuarioActual, SHOW_ID);

                    retorno.result = 1;
                    retorno.message = "OK";
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

        public JsonResult Solicitud_Eliminar_Activar(string id, int EsActivo, decimal SHOW_ID,int Tipo,decimal Artist_ID,string Observacion)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {                 
                        new BLArtista().Solicitud_Eliminar_Activar(id, Observacion, UsuarioActual, SHOW_ID, Tipo, Artist_ID);
                   
                    retorno.result = 1;
                    retorno.message = "OK";
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

        public JsonResult Insertar(DTOArtistaShow entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                string nombreArtist = entidad.NombreArtista;
                //var dato = new BLArtista().Obtener(Convert.ToDecimal(entidad.CodigoArtista), GlobalVars.Global.OWNER);
                //if (dato != null) nombreArtist = dato.ART_COMPLETE; 
                if (entidad.Tipo == 1)
                {
                    retorno.Code = Convert.ToInt32(new BLArtista().Insertar(entidad.CodigoShow, entidad.CodigoArtista, entidad.Principal, GlobalVars.Global.OWNER, UsuarioActual, nombreArtist));

                }
                else
                {
                    retorno.Code = Convert.ToInt32(new BLArtista().InsertarSolicitud(entidad.CodigoShow, entidad.CodigoArtista, entidad.Principal, GlobalVars.Global.OWNER, UsuarioActual, nombreArtist, entidad.Observacion));

                }

                if (retorno.Code != -1) //no se inserto
                    retorno.result = 1;
                else
                    retorno.result = 2; //validacion para que no inserte planilla auto

                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Insertar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

     

        public JsonResult Prioridad(string id)
        {
            Resultado retorno = new Resultado();
            try
            {
                new BLArtista().Prioridad(id, GlobalVars.Global.OWNER);

                retorno.result = 1;
                retorno.message = "OK";
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Prioridad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Obtener(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var dato = new BLArtista().Obtener(id, GlobalVars.Global.OWNER);

                    retorno.data = Json(dato, JsonRequestBehavior.AllowGet);
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

        public JsonResult ObtenerNombre(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var dato = new BLArtista().ObtenerArtistaOracle(id);
                    if (dato != null)
                    { retorno.valor = dato.ART_COMPLETE; }
                    else
                    { retorno.valor = "Artista No Encontrado"; }

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
        public JsonResult ListItems(decimal idShow)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLShowArtista servShow = new BLShowArtista();
                    var listShow = servShow.ShowsXArtistas(idShow, GlobalVars.Global.OWNER);

                    var items = listShow.Select(c => new SelectListItem
                    {
                        Value = c.ARTIST_ID.ToString(),
                        Text = c.NAME
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

        public JsonResult ListarOracle(int skip, int take, int page, int pageSize, int flag, string nombre)
        {
            var lista = new BLArtista().ListaArtistaOracle(GlobalVars.Global.OWNER, flag, nombre, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEArtista { listaArtista = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEArtista { listaArtista = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// InsertarArtista
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public JsonResult InsertarArtista(DTOArtista entidad)
        {
            Resultado retorno = new Resultado();
            try
            {

                var objArt = new BLArtista().ObtenerXNombreCompleto(entidad.NombreCompleto, GlobalVars.Global.OWNER);
                //var objArt = new BLArtista().ObtenerArtistaOracle(entidad.NombreCompleto);
                if (objArt == null)
                {
                    var result = new BLArtista().InsertarGeneral(GlobalVars.Global.OWNER, entidad.Nombre, entidad.IpNombre, entidad.PrimerNombre, entidad.NombreCompleto, UsuarioActual);
                    retorno.result = Constantes.MensajeRetorno.OK;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
                else
                {

                    retorno.result = Constantes.MensajeRetorno.DATA_FOUND;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }

            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = Constantes.MensajeRetorno.ERROR;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "InsertarArtista", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListarArtistaxShow(decimal codshow)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLArtista().Listar_Artista_x_Show(codshow)
                        .Select(c => new SelectListItem
                        {
                            Value = Convert.ToString(c.COD_ARTIST_SQ),
                            Text = Convert.ToString(c.NAME)
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

        public JsonResult ListarArtistaSinCodSGS(int skip, int take, int page, int pageSize, string nombre,decimal COD_LIC, string SHOW_NAME)
        {
            var lista = new BLArtista().Listar_Artista_NO_CODSGS_PAGEJSON(GlobalVars.Global.OWNER, nombre,  COD_LIC,SHOW_NAME, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            var TOTAL = lista.FirstOrDefault().TotalVirtual;

            if (tot.Count == 0)
            {
                return Json(new BEArtista { listaArtista = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEArtista { listaArtista = lista, TotalVirtual = TOTAL}, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ActualizaArtistaSGS(decimal codsgs, decimal codartist)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var result = new BLArtista().ActualizarArtistaSGS(codsgs, codartist);
                    if (result > 0)
                    {
                        retorno.result = 1;
                    }
                }

            }
            catch (Exception ex)
            {
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerNombreArtistaSQL(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var owner = GlobalVars.Global.OWNER;
                    var dato = new BLArtista().ObtenerArtista(id, owner);
                    if (dato != null)
                    { retorno.valor = dato.NAME; }
                    else
                    { retorno.valor = "Artista No Encontrado"; }

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

        public JsonResult InsertaPlanillaxArtista( decimal LIC_ID,decimal ARTIST_ID)
        {
            Resultado retorno = new Resultado();

            try
            {
                int respuestamodalidad = new BLArtista().ValidamodEspectBaile(LIC_ID);//valida modalidad de licencia 

                if (respuestamodalidad == 1)
                {//SI ES BAILE  O ESPECT
                    
                    new BLArtista().InsertaPlanillaAutomatica(LIC_ID,ARTIST_ID,UsuarioActual);
                    retorno.result = 1;
                }
                else
                    retorno.result = 0;
            }
            catch (Exception ex)
            {
                retorno.result = 0;

            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region ValidaModalidad VIVO GRABADA

        public JsonResult ValidaModalidadVivo_Grabada(decimal LIC_ID,decimal SHOW_ID)
        {
            Resultado retorno = new Resultado();
            try
            {
                int res = new BLArtista().ValidaModalidadGrabadaenVIVO(LIC_ID,SHOW_ID);
                //if (res == 1)
                
                    retorno.result = res;
                
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

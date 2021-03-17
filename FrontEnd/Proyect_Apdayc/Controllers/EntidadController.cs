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

namespace Proyect_Apdayc.Controllers
{
    public class EntidadController : Base
    {
        //
        // GET: /Entidad/

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public JsonResult Insertar(decimal idBps, decimal idLic)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEEntidadLic en = new BEEntidadLic();
                    en.OWNER = GlobalVars.Global.OWNER;
                    en.BPS_ID = idBps;
                    en.LIC_BPS = idLic;
                    en.LOG_USER_CREAT = UsuarioActual;
                    var result = new BLEntidadLic().Insertar(en);

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Insertar Entidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdEntidad(decimal idEntidad, decimal idBps, decimal idLic)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEEntidadLic en = new BEEntidadLic();
                    en.OWNER = GlobalVars.Global.OWNER;
                    en.BPS_ID = idBps;                    
                    en.LIC_BPS_ID = idEntidad;
                    en.LIC_BPS = idLic;
                    en.LOG_USER_UPDAT = UsuarioActual;
                    var result = new BLEntidadLic().Actualizar(en);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "UpdEntidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(decimal idEntidad, int EsActivo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (EsActivo == 1)
                        new BLEntidadLic().Eliminar(GlobalVars.Global.OWNER, idEntidad, UsuarioActual);
                    else
                        new BLEntidadLic().Activar(GlobalVars.Global.OWNER, idEntidad, UsuarioActual);

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

        public JsonResult ObtenerXCodigo(decimal idEntidad, decimal idLic)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var entidad = new BLEntidadLic().ObtenerEntidad(GlobalVars.Global.OWNER, idEntidad, idLic);
                    DTOEntidadLicencia entidadDTO = null;
                    if (entidad != null)
                    {
                        entidadDTO = new DTOEntidadLicencia();
                        entidadDTO.Id = entidad.LIC_BPS_ID;
                        entidadDTO.IdBps = entidad.BPS_ID;
                        entidadDTO.IdLicencia = entidad.LIC_BPS;
                        entidadDTO.Nombre = entidad.BPS_NAME;
                        entidadDTO.NroDocumento = entidad.TAX_ID;
                        entidadDTO.TipoDocumento = entidad.TAXT_ID;
                    }
                    retorno.result = 1;
                    retorno.data = Json(entidadDTO, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerXCodigo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarEntidad(decimal idLic)
        {
            Resultado retorno = new Resultado();
            var entidad = new BLEntidadLic().Listar(GlobalVars.Global.OWNER, idLic);
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header' style='display:none'>IdBps</th>");
                    shtml.Append("<th class='k-header'>Nombre</th>");
                    shtml.Append("<th class='k-header' style='display:none'>IdLicencia</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (entidad != null)
                    {
                        entidad.ForEach(c =>
                        {
                            var entidadDTO = new DTOEntidadLicencia();
                            entidadDTO.Id = c.LIC_BPS_ID;
                            entidadDTO.IdBps = c.BPS_ID;
                            entidadDTO.Nombre = c.BPS_NAME;
                            entidadDTO.IdLicencia = c.LIC_BPS;
                            entidadDTO.UsuarioCrea = c.LOG_USER_CREAT;
                            entidadDTO.UsuarioModifica = c.LOG_USER_UPDAT;
                            entidadDTO.FechaCrea = c.LOG_DATE_CREAT;
                            entidadDTO.FechaModifica = c.LOG_DATE_UPDATE;
                            entidadDTO.Activo = c.ENDS.HasValue ? false : true;

                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", entidadDTO.Id);
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", entidadDTO.IdBps);
                            shtml.AppendFormat("<td >{0}</td>", entidadDTO.Nombre);
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", entidadDTO.IdLicencia);
                            shtml.AppendFormat("<td >{0}</td>", entidadDTO.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", entidadDTO.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", entidadDTO.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", entidadDTO.FechaModifica);
                            shtml.AppendFormat("<td >{0}</td>", !(entidadDTO.Activo) ? "INACTIVO" : "ACTIVO");
                            shtml.Append("<td>");
                           // shtml.AppendFormat("<a href=# onclick='updAddEntidad({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", entidadDTO.Id);
                            shtml.AppendFormat("<a href=# onclick='delEntidad({0},{3});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", entidadDTO.Id, entidadDTO.Activo ? "delete.png" : "activate.png", entidadDTO.Activo ? "Eliminar Entidad" : "Activar Entidad", entidadDTO.Activo == true ? 1 : 0);
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        });
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarEntidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

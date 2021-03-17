using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;

namespace Proyect_Apdayc.Controllers
{
    public class OrigenModalidadController : Base
    {
        public const string nomAplicacion = "SRGDA";

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            return View();
        }

        public JsonResult Listar(int skip, int take, int page, int pageSize, string group, string dato, int st)
        {
            var lista = ListarOrigen(dato, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEOrigenModalidad { ListaOrigenModalidad = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEOrigenModalidad { ListaOrigenModalidad = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEOrigenModalidad> ListarOrigen(string dato, int st, int pagina, int cantRegxPag)
        {
            return new BLOrigenModalidad().Listar(dato, st, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Eliminar(string id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var servicio = new BLOrigenModalidad();
                    var origen = new BEOrigenModalidad();

                    origen.OWNER = GlobalVars.Global.OWNER;
                    origen.MOD_ORIG = id;
                    origen.LOG_USER_UPDAT = UsuarioActual;
                    servicio.Eliminar(origen);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult Insertar(BEOrigenModalidad origen)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    origen.OWNER = GlobalVars.Global.OWNER;
                    if (string.IsNullOrEmpty(origen.LOG_USER_CREAT))
                    {
                        origen.LOG_USER_CREAT = UsuarioActual;
                        var datos = new BLOrigenModalidad().Insertar(origen);
                    }
                    else
                    {
                        origen.LOG_USER_UPDAT = UsuarioActual;
                        var datos = new BLOrigenModalidad().Actualizar(origen);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpGet()]
        public JsonResult Obtener(string id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLOrigenModalidad servicio = new BLOrigenModalidad();
                    BEOrigenModalidad origen = new BEOrigenModalidad();
                    origen = servicio.Obtener(GlobalVars.Global.OWNER, id);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(origen, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        [HttpPost()]
        public JsonResult ObtenerXDescripcion(BEOrigenModalidad origen)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLOrigenModalidad servicio = new BLOrigenModalidad();
                    origen.OWNER = GlobalVars.Global.OWNER;
                    int resultado = servicio.ObtenerXDescripcion(origen);
                    if (resultado >= 1)
                        retorno.result = 1;
                    else
                        retorno.result = 0;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerXDescripcion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult ObtenerXCodigo(BEOrigenModalidad origen)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLOrigenModalidad servicio = new BLOrigenModalidad();
                    origen.OWNER = GlobalVars.Global.OWNER;
                    int resultado = servicio.ObtenerXCodigo(origen);
                    if (resultado >= 1)
                        retorno.result = 1;
                    else
                        retorno.result = 0;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerXCodigo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

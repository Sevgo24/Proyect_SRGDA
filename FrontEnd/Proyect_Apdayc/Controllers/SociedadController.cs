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
    public class SociedadController : Base
    {
        //
        // GET: /Sociedad/
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
            var lista = ListarSociedad(GlobalVars.Global.OWNER, dato, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BESociedad { ListaSociedad = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BESociedad { ListaSociedad = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BESociedad> ListarSociedad(string owner, string dato, int st, int pagina, int cantRegxPag)
        {
            return new BLSociedad().Listar(owner, dato, st, pagina, cantRegxPag);
        }


        [HttpPost]
        public JsonResult Eliminar(string id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var servicio = new BLSociedad();
                    var sociedad = new BESociedad();

                    sociedad.OWNER = GlobalVars.Global.OWNER;
                    sociedad.MOG_SOC = id;
                    sociedad.LOG_USER_UPDAT = UsuarioActual;
                    servicio.Eliminar(sociedad);

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
        public JsonResult Insertar(BESociedad sociedad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    bool valResultado;
                    string valMsg;
                    validarInsert(out valResultado, out valMsg, sociedad);
                    if (valResultado)
                    {
                        sociedad.OWNER = GlobalVars.Global.OWNER;
                        if (string.IsNullOrEmpty(sociedad.LOG_USER_CREAT))
                        {
                            sociedad.LOG_USER_CREAT = UsuarioActual;
                            var datos = new BLSociedad().Insertar(sociedad);
                        }
                        else
                        {
                            sociedad.LOG_USER_UPDAT = UsuarioActual;
                            var datos = new BLSociedad().Actualizar(sociedad);
                        }
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = valMsg;
                    }
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
                    BLSociedad servicio = new BLSociedad();
                    BESociedad origen = new BESociedad();
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
        public JsonResult ObtenerXDescripcion(BESociedad sociedad)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLSociedad servicio = new BLSociedad();
                sociedad.OWNER = GlobalVars.Global.OWNER;
                int resultado = servicio.ObtenerXDescripcion(sociedad);
                if (resultado >= 1)
                    retorno.result = 1;
                else
                    retorno.result = 0;
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
        public JsonResult ObtenerXCodigo(BESociedad sociedad)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLSociedad servicio = new BLSociedad();
                sociedad.OWNER = GlobalVars.Global.OWNER;
                int resultado = servicio.ObtenerXCodigo(sociedad);
                if (resultado >= 1)
                    retorno.result = 1;
                else
                    retorno.result = 0;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerXCodigo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private static void validarInsert(out bool exito, out string msg_validacion, BESociedad entidad)
        {
            exito = true;
            msg_validacion = string.Empty;
            if (exito && string.IsNullOrEmpty(entidad.MOG_SDESC))
            {
                msg_validacion = "Ingrese descripción.";
                exito = false;
            }
        }
    }
}

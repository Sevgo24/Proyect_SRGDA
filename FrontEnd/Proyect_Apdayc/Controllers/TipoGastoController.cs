using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;

namespace Proyect_Apdayc.Controllers
{
    public class TipoGastoController : Base
    {
        //
        // GET: /TipoGasto/

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Insertar()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            return View();
        }

        public ActionResult Edit()
        {
            Init(false);
            return View();
        }

        public JsonResult Listar_PageJson_TipoGasto(int skip, int take, int page, int pageSize, string group, string parametro, int st)
        {
            Resultado retorno = new Resultado();

            var lista = Listar_Page_TipoGasto(parametro, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BETipoGasto { TipoGasto = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BETipoGasto { TipoGasto = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BETipoGasto> Listar_Page_TipoGasto(string parametro, int st, int pagina, int cantRegxPag)
        {
            return new BLTipoGasto().Listar_Page_TipoGasto(parametro, st, pagina, cantRegxPag);
        }

        private static void validacion(out bool exito, out string msg_validacion, BETipoGasto entidad)
        {
            exito = true;
            msg_validacion = string.Empty;

            if (exito && string.IsNullOrEmpty(entidad.EXP_TYPE))
            {
                msg_validacion = "Ingrese una descripción corta";
                exito = false;
            }

            if (exito && string.IsNullOrEmpty(entidad.EXPT_DESC))
            {
                msg_validacion = "Ingrese una descripción";
                exito = false;
            }
        }

        [HttpPost]
        public JsonResult Insertar(BETipoGasto entidad)
        {
            bool exito = true;
            Resultado retorno = new Resultado();
            string msg_validacion = "";
            try
            {
                if (!isLogout(ref retorno))
                {
                    validacion(out exito, out msg_validacion, entidad);
                    if (exito)
                    {
                        var servicio = new BLTipoGasto();

                        entidad.LOG_USER_CREAT = UsuarioActual;
                        servicio.Insertar(entidad);

                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = msg_validacion;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "insertar tipo gasto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Actualizar(BETipoGasto entidad)
        {
            bool exito = true;
            Resultado retorno = new Resultado();
            string msg_validacion = "";
            try
            {
                if (!isLogout(ref retorno))
                {
                    validacion(out exito, out msg_validacion, entidad);
                    if (exito)
                    {
                        var servicio = new BLTipoGasto();

                        entidad.LOG_USER_UPDATE = UsuarioActual;
                        servicio.Actualizar(entidad);

                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = msg_validacion;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "actualizar tipo gasto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Obtiene(string id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BETipoGasto tipo = new BETipoGasto();
                    var lista = new BLTipoGasto().Obtener(GlobalVars.Global.OWNER, id);

                    if (lista != null)
                    {
                        foreach (var item in lista)
                        {
                            tipo.OWNER = item.OWNER;
                            tipo.EXP_TYPE = item.EXP_TYPE;
                            tipo.EXPT_DESC = item.EXPT_DESC;
                            tipo.LOG_USER_UPDATE = item.LOG_USER_UPDATE;
                        }
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado el tipo de gasto";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtiene tipo gasto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar(string codigo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLTipoGasto servicio = new BLTipoGasto();
                    var result = servicio.Eliminar(new BETipoGasto
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        EXP_TYPE = codigo,
                        LOG_USER_UPDATE = UsuarioActual,
                    });

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "elimina tipo gasto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

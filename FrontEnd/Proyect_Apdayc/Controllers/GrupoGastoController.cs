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
    public class GrupoGastoController : Base
    {
        //
        // GET: /GrupoGasto/

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

        public ActionResult Edit()
        {
            Init(false);
            return View();
        }

        public JsonResult Listar_PageJson_GrupoGasto(int skip, int take, int page, int pageSize, string group, string tipo, string parametro, int st)
        {
            Resultado retorno = new Resultado();

            var lista = Listar_Page_GrupoGasto(tipo, parametro, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEGrupoGasto { GrupoGasto = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEGrupoGasto { GrupoGasto = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEGrupoGasto> Listar_Page_GrupoGasto(string tipo, string parametro, int st, int pagina, int cantRegxPag)
        {
            return new BLGrupoGasto().Listar_Page_GrupoGasto(tipo,parametro, st, pagina, cantRegxPag);
        }

        private static void validacion(out bool exito, out string msg_validacion, BEGrupoGasto entidad)
        {
            exito = true;
            msg_validacion = string.Empty;

            if (exito && string.IsNullOrEmpty(entidad.EXP_TYPE))
            {
                msg_validacion = "Selecione un Tipo de Gasto";
                exito = false;
            }

            if (exito && string.IsNullOrEmpty(entidad.EXPG_ID))
            {
                msg_validacion = "Ingrese una descripción corta";
                exito = false;
            }

            if (exito && string.IsNullOrEmpty(entidad.EXPG_DESC))
            {
                msg_validacion = "Ingrese una descripción";
                exito = false;
            }
        }

        [HttpPost]
        public JsonResult Obtiene(string id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEGrupoGasto tipo = new BEGrupoGasto();
                    var lista = new BLGrupoGasto().Obtener(GlobalVars.Global.OWNER, id);

                    if (lista != null)
                    {
                        foreach (var item in lista)
                        {
                            tipo.OWNER = item.OWNER;
                            tipo.EXPG_ID = item.EXPG_ID;
                            tipo.EXP_TYPE = item.EXP_TYPE;
                            tipo.EXPG_DESC = item.EXPG_DESC;
                            tipo.LOG_USER_CREAT = item.LOG_USER_CREAT;
                        }
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado el grupo de gasto";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtiene el grupo de gasto", ex);
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
                    BLGrupoGasto servicio = new BLGrupoGasto();
                    var result = servicio.Eliminar(new BEGrupoGasto
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        EXPG_ID = codigo,
                        LOG_USER_UPDATE = "USER3",
                    });

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "eliminar grupo de gasto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insertar(BEGrupoGasto entidad)
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
                        var servicio = new BLGrupoGasto();

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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "insertar grupo de gasto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Actualizar(BEGrupoGasto entidad)
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
                        var servicio = new BLGrupoGasto();

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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "actualizar grupo de gasto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

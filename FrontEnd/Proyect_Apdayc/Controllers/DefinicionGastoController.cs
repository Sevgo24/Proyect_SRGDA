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
    public class DefinicionGastoController : Base
    {
        //
        // GET: /DefinicionGasto/

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

        public JsonResult Listar_PageJson_DefinicionGasto(int skip, int take, int page, int pageSize, string group, string owner, string tipo, string grupo, string parametro, int st)
        {
            Resultado retorno = new Resultado();

            var lista = Listar_Page_DefinicionGasto(GlobalVars.Global.OWNER, tipo, grupo, parametro, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEDefinicionGasto { DefinicionGasto = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEDefinicionGasto { DefinicionGasto = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEDefinicionGasto> Listar_Page_DefinicionGasto(string owner, string tipo, string grupo, string parametro, int st, int pagina, int cantRegxPag)
        {
            return new BLDefinicionGasto().Listar_Page_DefinicionGasto(owner, tipo, grupo, parametro, st, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Obtiene(string id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEDefinicionGasto tipo = new BEDefinicionGasto();
                    var lista = new BLDefinicionGasto().Obtener(GlobalVars.Global.OWNER, id);

                    if (lista != null)
                    {
                        foreach (var item in lista)
                        {
                            tipo.OWNER = item.OWNER;
                            tipo.EXP_TYPE = item.EXP_TYPE;
                            tipo.EXPG_ID = item.EXPG_ID;
                            tipo.EXP_ID = item.EXP_ID;
                            tipo.EXP_DESC = item.EXP_DESC;
                            tipo.LOG_USER_CREAT = item.LOG_USER_CREAT;
                        }
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado la definición de gasto";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtiene definición", ex);
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
                    BLDefinicionGasto servicio = new BLDefinicionGasto();
                    var result = servicio.Eliminar(new BEDefinicionGasto
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        EXP_ID = codigo,
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtiene definición", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private static void validacion(out bool exito, out string msg_validacion, BEDefinicionGasto entidad)
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
                msg_validacion = "Seleccione un Grupo de Gasto";
                exito = false;
            }

            if (exito && string.IsNullOrEmpty(entidad.EXP_ID))
            {
                msg_validacion = "Ingrese una descripción corta";
                exito = false;
            }

            if (exito && string.IsNullOrEmpty(entidad.EXP_DESC))
            {
                msg_validacion = "Ingrese una descripción";
                exito = false;
            }
        }

        public JsonResult ObtenerDatosValidar(BEDefinicionGasto en)
        {
            Resultado retorno = new Resultado();
            try
            {
                int resultado = new BLDefinicionGasto().ValidacionInsertarObtener(GlobalVars.Global.OWNER, en.EXP_ID, en.EXP_DESC);
                if (resultado >= 1)
                {
                    retorno.result = 0;
                    retorno.message = "Esta descripción de definición de gasto ya ha sido ingresada";
                }
                else
                {
                    retorno.result = 1;                
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Validacion insertar tipo derecho", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insertar(BEDefinicionGasto entidad)
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
                        var servicio = new BLDefinicionGasto();

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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "insertar definición", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Actualizar(BEDefinicionGasto entidad)
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
                        var servicio = new BLDefinicionGasto();

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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "actualiza definición", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

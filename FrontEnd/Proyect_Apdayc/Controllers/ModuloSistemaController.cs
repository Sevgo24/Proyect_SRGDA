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
    public class ModuloSistemaController : Base
    {
        //
        // GET: /ModuloSistema/

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

        public JsonResult Listar_PageJson_Modulo_Sistema(int skip, int take, int page, int pageSize, string group, string parametro, int st)
        {
            Resultado retorno = new Resultado();

            var lista = Listar_Page_Modulo_Sistema(parametro, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEModulo { Modulo = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEModulo { Modulo = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEModulo> Listar_Page_Modulo_Sistema(string parametro, int st, int pagina, int cantRegxPag)
        {
            return new BLModuloSistema().Listar_Page_Modulo_Sistema(parametro, st, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Obtiene(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEModulo tipo = new BEModulo();
                    var lista = new BLModuloSistema().Obtener(GlobalVars.Global.OWNER, id);

                    if (lista != null)
                    {
                        foreach (var item in lista)
                        {
                            tipo.OWNER = item.OWNER;
                            tipo.PROC_MOD = item.PROC_MOD;
                            tipo.MOD_DESC = item.MOD_DESC;
                            tipo.MOD_CLABEL = item.MOD_CLABEL;
                            tipo.MOD_CAPIKEY = item.MOD_CAPIKEY;
                            tipo.MOD_CSECRETKEY = item.MOD_CSECRETKEY;
                            //tipo.LOG_USER_CREAT = item.LOG_USER_CREAT;
                        }
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado el módulo de sistema.";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Obtiene", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private static void validacion(out bool exito, out string msg_validacion, BEModulo entidad)
        {
            exito = true;
            msg_validacion = string.Empty;

            if (exito && string.IsNullOrEmpty(entidad.MOD_DESC))
            {
                msg_validacion = "Ingrese los datos para el registro";
                exito = false;
            }

            if (exito && string.IsNullOrEmpty(entidad.MOD_CLABEL))
            {
                msg_validacion = "Ingrese los datos para el registro.";
                exito = false;
            }
        }

        [HttpPost]
        public JsonResult Insertar(BEModulo entidad)
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
                        var servicio = new BLModuloSistema();

                        if (entidad.PROC_MOD == 0)
                        {
                            entidad.OWNER = GlobalVars.Global.OWNER;
                            entidad.LOG_USER_CREAT = UsuarioActual;
                            servicio.Insertar(entidad);
                        }
                        else
                        {
                            entidad.OWNER = GlobalVars.Global.OWNER;
                            entidad.LOG_USER_UPDATE = UsuarioActual;
                            servicio.Actualizar(entidad);
                        }
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "insertar un modulo de sistems", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar(decimal codigo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLModuloSistema servicio = new BLModuloSistema();
                    var result = servicio.Eliminar(new BEModulo
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        PROC_MOD = codigo,
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "elimina un modulo de sistema", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

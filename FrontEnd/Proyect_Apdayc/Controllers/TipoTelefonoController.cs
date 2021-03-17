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
    public class TipoTelefonoController : Base
    {
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

        public JsonResult Listar_PageJson_TipoTelefono(int skip, int take, int page, int pageSize, string group, string parametro, int st)
        {
            Resultado retorno = new Resultado();

            var lista = Listar_Page_TipoTelefono(parametro, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BETelefonoType { Telefono = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BETelefonoType { Telefono = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BETelefonoType> Listar_Page_TipoTelefono(string parametro, int st, int pagina, int cantRegxPag)
        {
            return new BLTipoTelefono().Listar_Page_TipoTelefono(parametro, st, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Obtiene(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BETelefonoType tipo = new BETelefonoType();
                    var lista = new BLTipoTelefono().Obtener_Telefono(GlobalVars.Global.OWNER, id);

                    if (lista != null)
                    {
                        foreach (var item in lista)
                        {
                            tipo.OWNER = item.OWNER;
                            tipo.PHONE_TYPE = item.PHONE_TYPE;
                            tipo.PHONE_TDESC = item.PHONE_TDESC;
                            tipo.PHONE_TOBSERV = item.PHONE_TOBSERV;
                            tipo.ENDS = item.ENDS;
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtener ", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private static void validacion(out bool exito, out string msg_validacion, BETelefonoType entidad)
        {
            exito = true;
            msg_validacion = string.Empty;

            if (exito && string.IsNullOrEmpty(entidad.PHONE_TDESC))
            {
                msg_validacion = "Ingrese una descripción";
                exito = false;
            }
        }

        [HttpPost]
        public JsonResult Insertar(BETelefonoType entidad)
        {
            bool exito = true;
            Boolean resultado = true;
            Resultado retorno = new Resultado();
            string msg_validacion = "";
            try
            {
                if (!isLogout(ref retorno))
                {
                    validacion(out exito, out msg_validacion, entidad);
                    if (exito)
                    {
                        ///INICIO DE VALIDACION PARA EL INSERT Y UPDATE DE TIPO DE TELEFONO
                        if (entidad.PHONE_TYPE == 0)
                        {
                            var existeTipo = new BLTipoTelefono().existeTipoTelefono(GlobalVars.Global.OWNER, entidad.PHONE_TDESC);
                            if (existeTipo)
                            {
                                retorno.message = "El Tipo de Teléfono ya existe.";
                                retorno.result = 0;
                                resultado = false;
                                return Json(retorno, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            var existeTipo = new BLTipoTelefono().existeTipoTelefono(GlobalVars.Global.OWNER, entidad.PHONE_TYPE, entidad.PHONE_TDESC);
                            if (existeTipo)
                            {
                                retorno.message = "El Tipo de Teléfono ya existe.";
                                retorno.result = 0;
                                resultado = false;
                                return Json(retorno, JsonRequestBehavior.AllowGet);
                            }
                        }
                        ///FIN DE VALIDACION PARA EL INSERT Y UPDATE DE TIPO DE TELEFONO
                        
                        var servicio = new BLTipoTelefono();

                        if (entidad.PHONE_TYPE == 0)
                        {
                            entidad.LOG_USER_CREAT = UsuarioActual;
                            servicio.Insertar(entidad);
                        }
                        else
                        {
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "insertar ", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar(int codigo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var tipo = new BETelefonoType();
                    tipo.OWNER = GlobalVars.Global.OWNER;
                    tipo.PHONE_TYPE = codigo;
                    tipo.LOG_USER_UPDATE = UsuarioActual;

                    var servicio = new BLTipoTelefono().Eliminar(tipo);
                    if (servicio == 1)
                    {
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "El registro no se puede habilitar.";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "eliminar ", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

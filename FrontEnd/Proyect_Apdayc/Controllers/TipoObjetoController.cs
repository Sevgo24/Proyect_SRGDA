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
    public class TipoObjetoController : Base
    {
        //
        // GET: /TipoObjeto/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            return View();
        }

        public JsonResult Listar_PageJson_TipoObjeto(int skip, int take, int page, int pageSize, string group, string parametro, int st)
        {
            Resultado retorno = new Resultado();

            var lista = Listar_Page_TipoObjeto(parametro, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BETipoObjeto { TipoObjeto = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BETipoObjeto { TipoObjeto = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BETipoObjeto> Listar_Page_TipoObjeto(string parametro, int st, int pagina, int cantRegxPag)
        {
            return new BLTipoObjeto().Listar_Page_TipoObjeto(parametro, st, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Obtiene(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BETipoObjeto tipo = new BETipoObjeto();
                    var lista = new BLTipoObjeto().Obtener(GlobalVars.Global.OWNER, id);

                    if (lista != null)
                    {
                        foreach (var item in lista)
                        {
                            tipo.OWNER = item.OWNER;
                            tipo.WRKF_OTID = item.WRKF_OTID;
                            tipo.WRKF_OTDESC = item.WRKF_OTDESC;
                            tipo.WRKF_OPREF = item.WRKF_OPREF;
                        }

                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Obtiene Tipo de Objeto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private static void validacion(out bool exito, out string msg_validacion, BETipoObjeto entidad)
        {
            exito = true;
            msg_validacion = string.Empty;

            if (exito && string.IsNullOrEmpty(entidad.WRKF_OTDESC))
            {
                msg_validacion = "Ingrese una descripción";
                exito = false;
            }

            if (exito && string.IsNullOrEmpty(entidad.WRKF_OPREF))
            {
                msg_validacion = "Ingrese un prefijo";
                exito = false;
            }
        }

        [HttpPost]
        public JsonResult Insertar(BETipoObjeto entidad)
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
                        ///INICIO DE VALIDACION PARA EL INSERT Y UPDATE DE TIPO DE OBJETO
                        if (entidad.WRKF_OTID == 0)
                        {
                            var existeTipo = new BLTipoObjeto().existeTipoObjeto(GlobalVars.Global.OWNER, entidad.WRKF_OTDESC);
                            if (existeTipo)
                            {
                                retorno.message = "El Tipo de Objeto ya existe.";
                                retorno.result = 0;
                                resultado = false;
                                return Json(retorno, JsonRequestBehavior.AllowGet);
                            }

                            //VALIDA EL PREFIJO
                            var existePref = new BLTipoObjeto().existePrefijo(GlobalVars.Global.OWNER, entidad.WRKF_OPREF);
                            if (existePref)
                            {
                                retorno.message = "El Prefijo ya existe.";
                                retorno.result = 0;
                                resultado = false;
                                return Json(retorno, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            var existeTipo = new BLTipoObjeto().existeTipoObjeto(GlobalVars.Global.OWNER, entidad.WRKF_OTID, entidad.WRKF_OTDESC);
                            if (existeTipo)
                            {
                                retorno.message = "El Tipo de Objeto ya existe.";
                                retorno.result = 0;
                                resultado = false;
                                return Json(retorno, JsonRequestBehavior.AllowGet);
                            }

                            //VALIDA EL PREFIJO
                            var existePref = new BLTipoObjeto().existePrefijo(GlobalVars.Global.OWNER, entidad.WRKF_OTID, entidad.WRKF_OPREF);
                            if (existePref)
                            {
                                retorno.message = "El Prefijo ya existe.";
                                retorno.result = 0;
                                resultado = false;
                                return Json(retorno, JsonRequestBehavior.AllowGet);
                            }
                        }
                        ///FIN DE VALIDACION PARA EL INSERT Y UPDATE DE TIPO DE OBJETO


                        var servicio = new BLTipoObjeto();

                        if (entidad.WRKF_OTID == 0)
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
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Inserta y Actualiza Tipo de Objeto", ex);
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
                    var servicio = new BLTipoObjeto();

                    var tipo = new BETipoObjeto();
                    tipo.OWNER = GlobalVars.Global.OWNER;
                    tipo.WRKF_OTID = codigo;
                    tipo.LOG_USER_UPDATE = UsuarioActual;

                    servicio.Eliminar(tipo);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Cambia el Estado del Tipo de Objeto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

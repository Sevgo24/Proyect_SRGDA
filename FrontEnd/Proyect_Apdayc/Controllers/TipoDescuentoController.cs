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
    public class TipoDescuentoController : Base
    {
        //
        // GET: /TipoDescuento/

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

        public JsonResult Listar_PageJson_TipoDescuento(int skip, int take, int page, int pageSize, string group, string owner, string parametro, int st)
        {
            Resultado retorno = new Resultado();

            var lista = Listar_Page_TipoDescuento(GlobalVars.Global.OWNER, parametro, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BETipoDescuento { TipoDescuento = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BETipoDescuento { TipoDescuento = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BETipoDescuento> Listar_Page_TipoDescuento(string owner, string parametro, int st, int pagina, int cantRegxPag)
        {
            return new BLTipoDescuento().Listar_Page_TipoDescuento(owner, parametro, st, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Obtiene(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BETipoDescuento tipo = new BETipoDescuento();
                    var lista = new BLTipoDescuento().Obtener(GlobalVars.Global.OWNER, id);

                    if (lista != null)
                    {
                        foreach (var item in lista)
                        {
                            tipo.OWNER = item.OWNER;
                            tipo.DISC_TYPE = item.DISC_TYPE;
                            tipo.DISC_TYPE_NAME = item.DISC_TYPE_NAME;
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Obtiene el Tipo de Descuento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private static void validacion(out bool exito, out string msg_validacion, BETipoDescuento entidad)
        {
            exito = true;
            msg_validacion = string.Empty;

            if (exito && string.IsNullOrEmpty(entidad.DISC_TYPE_NAME))
            {
                msg_validacion = "Ingrese una descripción";
                exito = false;
            }
        }

        [HttpPost()]
        public JsonResult Validacion(BETipoDescuento en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!string.IsNullOrEmpty(en.DISC_TYPE_NAME))
                {
                    BLTipoDescuento servicio = new BLTipoDescuento();
                    en.OWNER = GlobalVars.Global.OWNER;
                    int resultado = servicio.ValidacionTipoDescuento(en);
                    if (resultado >= 1)
                    {
                        retorno.result = 0;
                        retorno.message = "Este tipo descuento ya ha sido registrado";
                    }
                    else
                    {
                        retorno.result = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Validacion insertar tipo descuento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insertar(BETipoDescuento entidad)
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
                        var servicio = new BLTipoDescuento();

                        if (entidad.DISC_TYPE == 0)
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Inserta y Actualiza el Tipo de Descuento", ex);
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
                    var servicio = new BLTipoDescuento();

                     var dctoEspec=Convert.ToDecimal(System.Web.Configuration.WebConfigurationManager.AppSettings["idTipoEspecialDscto"]);

                    var dscto=servicio.Obtener(GlobalVars.Global.OWNER, codigo);
                    if ((dscto != null && dscto.Count == 1) && dscto[0].DISC_TYPE != dctoEspec)
                    {
                        var tipo = new BETipoDescuento();
                        tipo.OWNER = GlobalVars.Global.OWNER;
                        tipo.DISC_TYPE = codigo;
                        tipo.LOG_USER_UPDATE = UsuarioActual;

                        servicio.Eliminar(tipo);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Cambia el Estado del Tipo de Descuento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

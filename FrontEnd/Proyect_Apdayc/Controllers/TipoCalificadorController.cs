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
    public class TipoCalificadorController : Base
    {
        //
        // GET: /TipoCalificador/

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

        public JsonResult Listar_PageJson_Tipo_Calificador(int skip, int take, int page, int pageSize, string group, string parametro, int st)
        {
            Resultado retorno = new Resultado();

            var lista = Listar_Page_Tipo_Calificador(parametro, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BETipoCalificador { TipoCalificador = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BETipoCalificador { TipoCalificador = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BETipoCalificador> Listar_Page_Tipo_Calificador(string parametro, int st, int pagina, int cantRegxPag)
        {
            return new BLTipoCalificador().Listar_Page_Tipo_Calificador(parametro, st, pagina, cantRegxPag);
        }

        private static void validacion(out bool exito, out string msg_validacion, BETipoCalificador entidad)
        {
            exito = true;
            msg_validacion = string.Empty;

            if (exito && string.IsNullOrEmpty(entidad.DESCRIPTION))
            {
                msg_validacion = "Ingrese una descripción";
                exito = false;
            }
        }

        [HttpPost]
        public JsonResult Insertar(BETipoCalificador entidad)
        {
            bool exito = true;
            Resultado retorno = new Resultado();
            string msg_validacion = "";
            try
            {
                if (!isLogout(ref retorno))
                {
                    //validacion(out exito, out msg_validacion, entidad);
                    if (exito)
                    {
                        var servicio = new BLTipoCalificador();

                        if (entidad.QUA_ID == 0)
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "insertar tipo calificador", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Obtiene(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BETipoCalificador tipo = new BETipoCalificador();
                    var lista = new BLTipoCalificador().Obtener(id);

                    if (lista != null)
                    {
                        foreach (var item in lista)
                        {
                            tipo.QUA_ID = item.QUA_ID;
                            tipo.DESCRIPTION = item.DESCRIPTION;
                        }
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado el Código postal";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtiene tipo calificador", ex);
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
                    var servicio = new BLTipoCalificador();

                    var tipo = new BETipoCalificador();
                    tipo.OWNER = GlobalVars.Global.OWNER;
                    tipo.QUA_ID = codigo;
                    tipo.LOG_USER_UPDATE = UsuarioActual;

                    servicio.Eliminar(tipo);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "elimina tipo calificador", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

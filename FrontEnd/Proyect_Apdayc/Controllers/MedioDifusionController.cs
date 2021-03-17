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
    public class MedioDifusionController : Base
    {
        //
        // GET: /MedioDifusion/

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

        public JsonResult Listar_PageJson_MediaDifusion(int skip, int take, int page, int pageSize, string group, string parametro, int st)
        {
            Resultado retorno = new Resultado();

            var lista = Listar_Page_MediaDifusion(parametro, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEMedioDifusion { MediaDifusion = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEMedioDifusion { MediaDifusion = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEMedioDifusion> Listar_Page_MediaDifusion(string parametro, int st, int pagina, int cantRegxPag)
        {
            return new BLMedio_Difusion().Listar_Page_MedioDifusion(parametro, st, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Obtiene(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEMedioDifusion tipo = new BEMedioDifusion();
                    var lista = new BLMedio_Difusion().Obtener(GlobalVars.Global.OWNER, id);

                    if (lista != null)
                    {
                        foreach (var item in lista)
                        {
                            tipo.OWNER = item.OWNER;
                            tipo.BROAD_ID = item.BROAD_ID;
                            tipo.BROAD_DESC = item.BROAD_DESC;
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "OBTIENE LA DIFUSION", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private static void validacion(out bool exito, out string msg_validacion, BEMedioDifusion entidad)
        {
            exito = true;
            msg_validacion = string.Empty;

            if (exito && string.IsNullOrEmpty(entidad.BROAD_DESC))
            {
                msg_validacion = "Ingrese una descripción";
                exito = false;
            }
        }

        [HttpPost]
        public JsonResult Insertar(BEMedioDifusion entidad)
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
                        var servicio = new BLMedio_Difusion();

                        if (entidad.BROAD_ID == 0)
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "INSERTA LA DIFUSION", ex);
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
                    var servicio = new BLMedio_Difusion();

                    var tipo = new BEMedioDifusion();
                    tipo.OWNER = GlobalVars.Global.OWNER;
                    tipo.BROAD_ID = codigo;
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ELIMINAR LA DIFUSION", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

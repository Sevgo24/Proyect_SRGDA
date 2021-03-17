using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;
using SGRDA.Entities.WorkFlow;
using SGRDA.BL.WorkFlow;


namespace Proyect_Apdayc.Controllers.WorkFlow
{
    public class EventoController : Base
    {
        //
        // GET: /Evento/
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

        public JsonResult ListarEvento(int skip, int take, int page, int pageSize, string dato, int st)
        {
            var lista = Listar(GlobalVars.Global.OWNER, dato, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new WORKF_EVENTS { listaEv = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new WORKF_EVENTS { listaEv = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<WORKF_EVENTS> Listar(string owner, string dato, int st, int pagina, int cantRegxPag)
        {
            return new BL_WORKF_EVENTS().usp_Get_EventoPage(owner, dato, st, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Obtiene(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BL_WORKF_EVENTS servicio = new BL_WORKF_EVENTS();
                    var dato = servicio.Obtener(GlobalVars.Global.OWNER, id);

                    if (dato != null)
                    {
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(dato, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado datos";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "obtener datos workflow Evento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarDescripcion(string Descripcion)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var count = new BL_WORKF_EVENTS().ValidarDescripcion(GlobalVars.Global.OWNER, Descripcion.ToUpper());

                    if (count == 1)
                    {
                        retorno.message = "El evento ingresado ya ha sigo registrado anteriormente";
                        retorno.result = 0;
                    }
                    else
                        retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Validar Descripción Evento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(decimal idEvento)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BL_WORKF_EVENTS servicio = new BL_WORKF_EVENTS();
                    var result = servicio.Eliminar(new WORKF_EVENTS
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        WRKF_EID = idEvento,
                        LOG_USER_UPDATE = UsuarioActual
                    });
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar Evento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insertar(WORKF_EVENTS en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    WORKF_EVENTS obj = new WORKF_EVENTS();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.WRKF_EID = en.WRKF_EID;
                    obj.WRKF_ENAME = en.WRKF_ENAME;
                    obj.WRKF_ELABEL = en.WRKF_ELABEL;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    obj.LOG_USER_UPDATE = UsuarioActual;
                    if (obj.WRKF_EID == 0)
                    {
                        var datos = new BL_WORKF_EVENTS().Insertar(obj);
                    }
                    else
                    {
                        var datos = new BL_WORKF_EVENTS().Actualizar(obj);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "insert Evento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

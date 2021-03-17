
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System.Text;


namespace Proyect_Apdayc.Controllers
{
    public class AgenteController : Base
    {
        //
        // GET: /Agente/
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

        public JsonResult ListarAgentes(int skip, int take, int page, int pageSize, string group, string dato, int st)
        {
            var lista = ListarAgentesRecaudo(GlobalVars.Global.OWNER, dato, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEAgente { ListaAgente = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEAgente { ListaAgente = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEAgente> ListarAgentesRecaudo(string owner, string dato, int st, int pagina, int cantRegxPag)
        {
            return new BLAgente().ListarAgentes(owner, dato, st, pagina, cantRegxPag);
        }

        #region TreeView

        public ActionResult Treeview()
        {
            Init(false);
            return View();
        }

        public JsonResult getTreeview()
        {
            List<BETreeview> lista = new List<BETreeview>();
            lista = new BLAgente().ListarArbol(GlobalVars.Global.OWNER);
            lista.Add(new BETreeview { cod = 0, text = "", ManagerID = null });

            var padre = lista.Where(x => x.ManagerID == null).FirstOrDefault();
            SetChildren(padre, lista);
            return Json(padre, JsonRequestBehavior.AllowGet);
        }

        public void SetChildren(BETreeview model, List<BETreeview> lista)
        {
            var childs = lista.Where(x => x.ManagerID == model.cod).ToList();
            if (childs.Count > 0)
            {
                foreach (var child in childs)
                {
                    SetChildren(child, lista);
                    model.items.Add(child);
                }
            }
        }

        #endregion

        [HttpPost]
        public JsonResult Eliminar(decimal idNivel)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var servicio = new BLAgente();

                    var agente = new BEAgente();

                    agente.OWNER = GlobalVars.Global.OWNER;
                    agente.LEVEL_ID = idNivel;
                    agente.LOG_USER_UPDAT = UsuarioActual;
                    servicio.Eliminar(agente);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult ObtenerXDescripcion(BEAgente agente)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    BLAgente servicio = new BLAgente();
                    agente.OWNER = GlobalVars.Global.OWNER;
                    var lista = new List<BEAgente>();

                    lista = servicio.ObtenerXDescripcion(agente);
                    if (lista.Count >= 1)
                        retorno.valor = "1";
                    else
                        retorno.valor = "0";
                }
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerXDescripcion", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpGet()]
        public JsonResult Obtener(decimal id)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    BLAgente servicio = new BLAgente();
                    var agente = new BEAgente();
                    agente = servicio.Obtener(GlobalVars.Global.OWNER, id);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(agente, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult InsertarAgenteNivel(BEAgente agente)
        {

            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEAgente obj = new BEAgente();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.DESCRIPTION = agente.DESCRIPTION;
                    obj.LEVEL_ID = agente.LEVEL_ID;
                    obj.LEVEL_DEP = agente.LEVEL_DEP;
                    obj.LOG_USER_CREAT = UsuarioActual;

                    if (obj.LEVEL_ID == 0)
                    {
                        var datos = new BLAgente().Insertar(obj);
                    }
                    else
                    {
                        var datos = new BLAgente().Actualizar(obj);
                    }

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {

                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "InsertarAgenteNivel", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}

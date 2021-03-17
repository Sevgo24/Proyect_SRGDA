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
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using SGRDA.Entities.WorkFlow;
using SGRDA.BL.WorkFlow;


namespace Proyect_Apdayc.Controllers.WorkFlow
{
    public class TransicionesController : Base
    {
        //
        // GET: /Transiciones/
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

        #region Listar
        public JsonResult Listar(int skip, int take, int page, int pageSize, string group, decimal idCiclo, decimal idEvento, decimal idEstadoIni, decimal idEstadoFin, int st)
        {
            var lista = ListarTransiciones(GlobalVars.Global.OWNER, idCiclo, idEvento, idEstadoIni, idEstadoFin, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new WORKF_TRANSITIONS { ListarTransiciones = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new WORKF_TRANSITIONS { ListarTransiciones = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<WORKF_TRANSITIONS> ListarTransiciones(string owner, decimal idCiclo, decimal idEvento, decimal idEstadoIni, decimal idEstadoFin, int estado, int pagina, int cantRegxPag)
        {
            return new  BL_WORKF_TRANSITIONS().Listar(owner,  idCiclo,  idEvento,  idEstadoIni,  idEstadoFin,  estado,  pagina,  cantRegxPag);
        }
        #endregion

        #region Nuevo
        [HttpPost()]
        public ActionResult Eliminar(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                WORKF_TRANSITIONS transicion = new WORKF_TRANSITIONS();
                transicion.OWNER = GlobalVars.Global.OWNER;
                transicion.WRKF_TID = id;
                transicion.LOG_USER_UPDATE = UsuarioActual;
                var datos = new BL_WORKF_TRANSITIONS().Eliminar(transicion);
                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
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
        public ActionResult Insertar(WORKF_TRANSITIONS transicion)
        {
            Resultado retorno = new Resultado();
            try
            {
                transicion.OWNER = GlobalVars.Global.OWNER;
                if (transicion.WRKF_TID == 0)
                {
                    transicion.LOG_USER_CREAT = UsuarioActual;
                    var datos = new BL_WORKF_TRANSITIONS().Insertar(transicion);
                }
                else
                {
                    transicion.LOG_USER_UPDATE = UsuarioActual;
                    var datos = new BL_WORKF_TRANSITIONS().Actualizar(transicion);
                }
                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Obtener(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var cliente = new BL_WORKF_TRANSITIONS().ObtenerTransitions(GlobalVars.Global.OWNER, id);
                    retorno.data = Json(cliente, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}

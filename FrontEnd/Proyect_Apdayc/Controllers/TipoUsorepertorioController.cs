using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases;
using System.Text;

namespace Proyect_Apdayc.Controllers
{
    public class TipoUsorepertorioController : Base
    {
        //
        // GET: /TipoUsorepertorio/
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

        public JsonResult ListarTipoUsoRepertorio(int skip, int take, int page, int pageSize, string dato, int st)
        {
            var lista = ListaTipousorepertorio(dato, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BETipoUsorepertorio { ListaTipoUsoRepertorio = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BETipoUsorepertorio { ListaTipoUsoRepertorio = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BETipoUsorepertorio> ListaTipousorepertorio(string dato, int st, int pagina, int cantRegxPag)
        {
            return new BLTipoUsorepertorio().usp_Get_UsoRepertorioPage(dato, st, pagina, cantRegxPag);
        }

        [HttpGet()]
        public JsonResult Obtiene(string id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLTipoUsorepertorio servicio = new BLTipoUsorepertorio();
                    BETipoUsorepertorio tipo = new BETipoUsorepertorio();
                    tipo = servicio.Obtener(GlobalVars.Global.OWNER, id);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "obtener datos uso repertorio", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insertar(BETipoUsorepertorio TipoUsorepertorio)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BETipoUsorepertorio objUso = new BETipoUsorepertorio();
                    objUso.OWNER = GlobalVars.Global.OWNER;
                    objUso.MOD_REPER = TipoUsorepertorio.MOD_REPER;
                    objUso.MOD_DREPER = TipoUsorepertorio.MOD_DREPER;
                    objUso.valgraba = TipoUsorepertorio.valgraba;
                    objUso.LOG_USER_CREAT = UsuarioActual;
                    if (objUso.valgraba == 0)
                    {
                        var datos = new BLTipoUsorepertorio().Insertar(objUso);
                    }
                    else
                    {
                        objUso.MOD_REPER = TipoUsorepertorio.MOD_REPER;
                        objUso.LOG_USER_UPDAT = UsuarioActual;
                        var datos = new BLTipoUsorepertorio().Actualizar(objUso);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "insert uso repertorio", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(string idUsorepertorio)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLTipoUsorepertorio servicio = new BLTipoUsorepertorio();
                    var result = servicio.Eliminar(new BETipoUsorepertorio
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        MOD_REPER = idUsorepertorio,
                        LOG_USER_UPDAT = UsuarioActual
                    });
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar Tipo uso repertorio", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult ObtenerXDescripcion(BETipoUsorepertorio TipoUsorepertorio)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLTipoUsorepertorio servicio = new BLTipoUsorepertorio();
                TipoUsorepertorio.OWNER = GlobalVars.Global.OWNER;
                int resultado = servicio.ObtenerXDescripcion(TipoUsorepertorio);
                if (resultado >= 1)
                    retorno.result = 1;
                else
                    retorno.result = 0;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Tipo uso repertorio ObtenerXDescripcion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult ObtenerXCodigo(BETipoUsorepertorio TipoUsorepertorio)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLTipoUsorepertorio servicio = new BLTipoUsorepertorio();
                TipoUsorepertorio.OWNER = GlobalVars.Global.OWNER;
                int resultado = servicio.ObtenerXCodigo(TipoUsorepertorio);
                if (resultado >= 1)
                    retorno.result = 1;
                else
                    retorno.result = 0;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Tipo uso repertorio ObtenerXCodigo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

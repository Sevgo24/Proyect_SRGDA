using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;

namespace Proyect_Apdayc.Controllers
{
    public class TransicionesEstadoController : Base
    {
        #region varialbles log
        const string nomAplicacion = "SGRDA";
        #endregion
        //
        // GET: /TransicionesEstado/

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

        public JsonResult Listar(int skip, int take, int page, int pageSize, decimal estadoOri, decimal estadoDes, int st)
        {
            var lista = ListarPag(GlobalVars.Global.OWNER, estadoOri, estadoDes, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BETransicionesEstado { listaTranEstado = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BETransicionesEstado { listaTranEstado = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BETransicionesEstado> ListarPag(string owner, decimal estadoOri, decimal estadoDes, int st, int pagina, int cantRegxpag)
        {
            return new BLTransicionesEstado().ListaTrancionEstadoPaginada(owner, estadoOri, estadoDes, st, pagina, cantRegxpag);
        }

        public JsonResult Obtiene(decimal idori, decimal iddes)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLTransicionesEstado servicio = new BLTransicionesEstado();
                    var transicion = servicio.Obtener(GlobalVars.Global.OWNER, idori, iddes);

                    if (transicion != null)
                    {
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(transicion, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado la transicion de estado";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "obtener datos de transicion de estado", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult Validacion(decimal idori, decimal iddes)
        {
            Resultado retorno = new Resultado();
            BLTransicionesEstado servicio = new BLTransicionesEstado();
            List<BETransicionesEstado> en = new List<BETransicionesEstado>();
            en = servicio.ObtenerDatosValidad(GlobalVars.Global.OWNER, idori, iddes);

            if (en.Count() > 0)
            {
                retorno.result = 0;
                retorno.message = "Esta transición de estado ya existe.";
                retorno.data = Json(en, JsonRequestBehavior.AllowGet);
            }
            else
            {
                retorno.result = 1;
                retorno.data = Json(en, JsonRequestBehavior.AllowGet);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        [HttpPost()]
        public JsonResult Insertar(BETransicionesEstado en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BETransicionesEstado obj = new BETransicionesEstado();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.LICS_ID = en.LICS_ID;
                    obj.LICS_IDT = en.LICS_IDT;
                    obj.auxLICS_ID = en.auxLICS_ID;
                    obj.auxLICS_IDT = en.auxLICS_IDT;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    obj.LOG_USER_UPDAT = UsuarioActual;

                    if (obj.auxLICS_ID == 0 && obj.auxLICS_IDT == 0)
                    {
                        var dato = new BLTransicionesEstado().Insertar(obj);
                    }
                    else
                    {
                        var dato = new BLTransicionesEstado().actualizar(obj);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar transicion estado", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(decimal Idori, decimal Iddest)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLTransicionesEstado servicio = new BLTransicionesEstado();
                    var result = servicio.Eliminar(new BETransicionesEstado
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        LICS_ID = Idori,
                        LICS_IDT = Iddest,
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar Proceso", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

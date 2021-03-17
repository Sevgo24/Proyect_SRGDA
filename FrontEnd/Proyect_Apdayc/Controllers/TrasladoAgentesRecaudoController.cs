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

namespace Proyect_Apdayc.Controllers
{
    public class TrasladoAgentesRecaudoController : Base
    {
        #region varialbles log
        const string nomAplicacion = "SGRDA";
        #endregion

        //
        // GET: /TrasladoAgentesRecaudo/

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

        public ActionResult Editar()
        {
            Init(false);
            return View();
        }

        public JsonResult usp_listar_TrasladoAgentesRecaudoJson(int skip, int take, int page, int pageSize, string owner, decimal agente)
        {
            Resultado retorno = new Resultado();

            var lista = usp_Get_TrasladoAgentesRecaudoPage(GlobalVars.Global.OWNER, agente, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BETrasladoAgentesRecaudo { Traslado = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BETrasladoAgentesRecaudo { Traslado = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BETrasladoAgentesRecaudo> usp_Get_TrasladoAgentesRecaudoPage(string owner, decimal agente, int pagina, int cantRegxPag)
        {
            return new BLTrasladoAgentesRecaudo().usp_Get_TrasladoAgentesRecaudoPage(owner, agente, pagina, cantRegxPag);
        }

        public JsonResult ACBuscarAgenterecaudador()
        {
            string texto = Request.QueryString["term"];
            var datos = new BLTrasladoAgentesRecaudo().BUSCAR_RECAUDADORES_X_NOMBRE(GlobalVars.Global.OWNER, texto);
            List<DTOSocio> establecimientos = new List<DTOSocio>();
            datos.ForEach(x =>
            {
                establecimientos.Add(new DTOSocio
                {
                    Codigo = x.BPS_ID,
                    value = String.Format("{0}", x.BPS_NAME)
                });
            });
            return Json(establecimientos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insertar(BETrasladoAgentesRecaudo TrasladoAgentesRecaudo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BETrasladoAgentesRecaudo obj = new BETrasladoAgentesRecaudo();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.BPS_ID = TrasladoAgentesRecaudo.BPS_ID;
                    obj.OFF_ID = TrasladoAgentesRecaudo.OFF_ID;
                    obj.OFF_IDAux = TrasladoAgentesRecaudo.OFF_IDAux;
                    obj.LEVEL_ID = TrasladoAgentesRecaudo.LEVEL_ID;
                    obj.ENDS = TrasladoAgentesRecaudo.ENDS;
                    if (TrasladoAgentesRecaudo.START == null)
                        obj.START = DateTime.Now;
                    else
                        obj.START = TrasladoAgentesRecaudo.START;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    obj.LOG_USER_UPDAT = UsuarioActual;

                    var datos = new BLTrasladoAgentesRecaudo().Insertar(obj);

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Traslado oficina agente", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Actualizar(BETrasladoAgentesRecaudo TrasladoAgentesRecaudo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BETrasladoAgentesRecaudo obj = new BETrasladoAgentesRecaudo();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.OFF_ID = TrasladoAgentesRecaudo.OFF_ID;
                    obj.BPS_ID = TrasladoAgentesRecaudo.BPS_ID;
                    obj.OFF_IDAux = TrasladoAgentesRecaudo.OFF_IDAux;
                    obj.LEVEL_ID = TrasladoAgentesRecaudo.LEVEL_ID;
                    obj.ENDS = TrasladoAgentesRecaudo.ENDS;
                    if (TrasladoAgentesRecaudo.START == null)
                        obj.START = DateTime.Now;
                    else
                        obj.START = TrasladoAgentesRecaudo.START;
                    obj.LOG_USER_UPDAT = UsuarioActual;

                    var datos = new BLTrasladoAgentesRecaudo().Actualizar(obj);

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Traslado oficina agente", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Obtiene(decimal id, decimal idAgente)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    BLTrasladoAgentesRecaudo servicio = new BLTrasladoAgentesRecaudo();
                    var traslado = servicio.ObtenerDatosOficina(GlobalVars.Global.OWNER, id, idAgente);

                    if (traslado != null)
                    {
                        DTOTrasladoAgentesRecaudo trasladoDto = new DTOTrasladoAgentesRecaudo()
                        {
                            Codigo = traslado.OFF_ID,
                            CodigoSocio = traslado.BPS_ID,
                            Oficina = traslado.OFF_NAME,
                            Level = traslado.LEVEL_ID,
                            FechaInicio = traslado.START.Value.ToShortDateString(),
                            FechaFin = (traslado.ENDS == null) ? string.Empty : traslado.ENDS.Value.ToShortDateString(),
                        };

                        retorno.data = Json(trasladoDto, JsonRequestBehavior.AllowGet);
                        retorno.message = "oficina encontrada";
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.message = "No se ha podido encontrar la oficina";
                        retorno.result = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener datos oficina", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

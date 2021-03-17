using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyect_Apdayc.Clases;

namespace Proyect_Apdayc.Controllers
{
    public class TipoenvioFacturaController : Base
    {
        //
        // GET: /TipoenvioFactura/
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

        public JsonResult Listar(int skip, int take, int page, int pageSize, string dato, int st)
        {
            var lista = Listatipoenviofactura(GlobalVars.Global.OWNER, dato, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BETipoenvioFactura { TipoenvioFactura = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BETipoenvioFactura { TipoenvioFactura = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BETipoenvioFactura> Listatipoenviofactura(string owner, string dato, int st, int pagina, int cantRegxPag)
        {
            return new BLTipoenvioFactura().ListarPaginacion(owner, dato, st, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Eliminar(int codigo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var servicio = new BLTipoenvioFactura();

                    var tipo = new BETipoenvioFactura();
                    tipo.OWNER = GlobalVars.Global.OWNER;
                    tipo.LIC_SEND = codigo;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtiene el tipo de evio factura", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Obtiene(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BETipoenvioFactura tipo = new BETipoenvioFactura();
                    var item = new BLTipoenvioFactura().Obtener(GlobalVars.Global.OWNER, id);

                    if (item != null)
                    {
                        tipo.OWNER = item.OWNER;
                        tipo.LIC_SEND = item.LIC_SEND;
                        tipo.LIC_FSEND = item.LIC_FSEND;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtiene el tipo de envio de factura", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insertar(BETipoenvioFactura TipoenvioFactura)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BETipoenvioFactura obj = new BETipoenvioFactura();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.LIC_SEND = TipoenvioFactura.LIC_SEND;
                    obj.LIC_FSEND = TipoenvioFactura.LIC_FSEND;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    if (obj.LIC_SEND == 0)
                    {
                        var datos = new BLTipoenvioFactura().Insertar(obj);
                    }
                    else
                    {
                        obj.LIC_FSEND = TipoenvioFactura.LIC_FSEND;
                        obj.LOG_USER_UPDATE = UsuarioActual;
                        var datos = new BLTipoenvioFactura().Actualizar(obj);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "insert tipo envio factura", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult ObtenerXDescripcion(BETipoenvioFactura TipoenvioFactura)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLTipoenvioFactura servicio = new BLTipoenvioFactura();
                TipoenvioFactura.OWNER = GlobalVars.Global.OWNER;
                TipoenvioFactura.LIC_FSEND = TipoenvioFactura.LIC_FSEND;
                int resultado = servicio.ObtenerXDescripcion(TipoenvioFactura);
                if (resultado >= 1)
                    retorno.result = 1;
                else
                    retorno.result = 0;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Tipo envio factura ObtenerXDescripcion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

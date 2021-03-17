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
namespace Proyect_Apdayc.Controllers.MetodoPago
{
    public class MetodoPagoController : Base
    {
        //
        // GET: /MetodoPago/
        public const string nomAplicacion = "SRGDA";

        private const string K_SESION_METODOPAGO = "___DTOMetodoPago";
        DTOMetodoPago metodopago = new DTOMetodoPago();
        public DTOMetodoPago MetodoPagoTmp
        {
            get
            {
                return (DTOMetodoPago)Session[K_SESION_METODOPAGO];
            }
            set
            {
                Session[K_SESION_METODOPAGO] = value;
            }
        }

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            if (MetodoPagoTmp == null) MetodoPagoTmp = new DTOMetodoPago();
            return View();
        }

        public JsonResult ListarMetodoPago(int skip, int take, int page, int pageSize, string dato, bool confirmacion, int st)
        {
            var lista = Listametodo(GlobalVars.Global.OWNER, dato, confirmacion, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEMetodoPago { ListaMetodoPago = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEMetodoPago { ListaMetodoPago = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEMetodoPago> Listametodo(string owner, string dato, bool confirmacion, int st, int pagina, int cantRegxPag)
        {
            return new BLMetodoPago().ListarPaginado(owner, dato, confirmacion, st, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Obtiene(string id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLMetodoPago servicio = new BLMetodoPago();
                    var dato = servicio.Obtener(GlobalVars.Global.OWNER, id);

                    if (dato != null)
                    {
                        MetodoPagoTmp.MetoPago = dato.REC_PWDESCAux;
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(dato, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado el método de pago";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "obtener datos método de pago", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insertar(BEMetodoPago en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEMetodoPago obj = new BEMetodoPago();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.REC_PWID = en.REC_PWID;
                    obj.REC_PWDESC = en.REC_PWDESC;
                    obj.REC_AUT = en.REC_AUT;
                    obj.valgraba = en.valgraba;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    if (obj.valgraba == 0)
                    {
                        var datos = new BLMetodoPago().Insertar(obj);
                    }
                    else
                    {
                        obj.REC_PWID = en.REC_PWID;
                        obj.LOG_USER_UPDATE = UsuarioActual;
                        var datos = new BLMetodoPago().Actualizar(obj);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "insert método de pago", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(string Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLMetodoPago servicio = new BLMetodoPago();
                    var result = servicio.Eliminar(new BEMetodoPago
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        REC_PWID = Id,
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar método de pago", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult ObtenerXCodigo(BEMetodoPago en)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLMetodoPago servicio = new BLMetodoPago();
                en.OWNER = GlobalVars.Global.OWNER;
                int resultado = servicio.ObtenerXCodigo(en);
                if (resultado >= 1)
                    retorno.result = 1;
                else
                {
                    retorno.result = 0;
                    //retorno.message = "El código que se quieres registrar ya existe";
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Método de pago ObtenerXCodigo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult ObtenerXDescripcion(BEMetodoPago en)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLMetodoPago servicio = new BLMetodoPago();
                en.OWNER = GlobalVars.Global.OWNER;
                int resultado = servicio.ObtenerXDescripcion(en);
                if (resultado >= 1)
                    retorno.result = 1;
                else
                {
                    retorno.result = 0;
                    //retorno.message = "La descripción que se quieres registrar ya existe";
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Uso repertorio ObtenerXDescripcion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

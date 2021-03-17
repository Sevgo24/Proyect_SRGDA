using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System.Xml;
using System.Text;
using System.Drawing;
using System.IO;
using System.Net;

namespace Proyect_Apdayc.Controllers.Comision
{
    public class ComisionOrigenController : Base
    {
        //
        // GET: /ComisionOrigen/
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

        public JsonResult ListarOrigenComision(int skip, int take, int page, int pageSize, string dato, int st)
        {
            var lista = Lista(GlobalVars.Global.OWNER, dato, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEOrigenComision { ListaOrigenComision = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEOrigenComision { ListaOrigenComision = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEOrigenComision> Lista(string owner, string dato, int st, int pagina, int cantRegxPag)
        {
            return new BLOrigenComision().ListarPage(owner, dato, st, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Obtiene(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLOrigenComision servicio = new BLOrigenComision();
                    var en = servicio.Obtener(GlobalVars.Global.OWNER, Id);

                    if (en != null)
                    {
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(en, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado el origen de comisión";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "obtener datos origen de comisión", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult Validacion(BEOrigenComision en)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLOrigenComision servicio = new BLOrigenComision();
                en.OWNER = GlobalVars.Global.OWNER;
                int resultado = servicio.ValidacionOrigenComision(en);
                if (resultado >= 1)
                {
                    retorno.result = 0;
                    retorno.message = "El origen de comisión ya ha sido registrado";
                }
                else
                {
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Validación insertar origen de comisión", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insertar(BEOrigenComision en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEOrigenComision obj = new BEOrigenComision();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.COM_ORG = en.COM_ORG;
                    obj.COM_DESC = en.COM_DESC;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    if (obj.COM_ORG == 0)
                    {
                        var datos = new BLOrigenComision().Insertar(obj);
                    }
                    else
                    {
                        obj.LOG_USER_UPDAT = UsuarioActual;
                        var datos = new BLOrigenComision().Actualizar(obj);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "insert uso origen comisión", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLOrigenComision servicio = new BLOrigenComision();
                    var result = servicio.Eliminar(new BEOrigenComision
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        COM_ORG = Id,
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar origen comisión", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

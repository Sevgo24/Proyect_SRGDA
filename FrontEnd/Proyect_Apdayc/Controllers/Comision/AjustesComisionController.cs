using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using Microsoft.Reporting.WebForms;
using System.Text;

namespace Proyect_Apdayc.Controllers.Comision
{
    public class AjustesComisionController : Base
    {
        //
        // GET: /AjustesComision/
        public const string NomAplicacion = "SRGDA";

        #region variables de sesion
        private const string K_SESION_AJUSTE = "___DTOAjuste";
        private const string K_SESION_AJUSTE_DEL = "___DTOAjusteDEL";
        private const string K_SESION_AJUSTE_ACT = "___DTOAjusteACT";
        #endregion

        #region DTO set y get
        List<DTOAjusteComision> AjusteComision = new List<DTOAjusteComision>();
        private List<DTOAjusteComision> AjusteComisionTmpUPDEstado
        {
            get
            {
                return (List<DTOAjusteComision>)Session[K_SESION_AJUSTE_ACT];
            }
            set
            {
                Session[K_SESION_AJUSTE_ACT] = value;
            }
        }
        private List<DTOAjusteComision> AjusteComisionTmpDelBD
        {
            get
            {
                return (List<DTOAjusteComision>)Session[K_SESION_AJUSTE_DEL];
            }
            set
            {
                Session[K_SESION_AJUSTE_DEL] = value;
            }
        }
        public List<DTOAjusteComision> AjusteComisionTmp
        {
            get
            {
                return (List<DTOAjusteComision>)Session[K_SESION_AJUSTE];
            }
            set
            {
                Session[K_SESION_AJUSTE] = value;
            }
        }
        #endregion

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESION_AJUSTE_ACT);
            Session.Remove(K_SESION_AJUSTE_DEL);
            Session.Remove(K_SESION_AJUSTE);
            return View();
        }

        public JsonResult ListarAjusteComisiones(int skip, int take, int page, int pageSize, decimal IdAgente, string Fecha, string IdMoneda, decimal IdLicencia, decimal IdModalidad)
        {
            var lista = Lista(GlobalVars.Global.OWNER, IdAgente, Convert.ToDateTime(Fecha), IdMoneda, IdLicencia, IdModalidad, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEAjustesComision { listaAjustesCom = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEAjustesComision { listaAjustesCom = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEAjustesComision> Lista(string owner, decimal IdAgente, DateTime Fecha, string IdMoneda, decimal IdLicencia, decimal IdModalidad, int pagina, int cantRegxPag)
        {
            return new BLAjustesComision().ListarPage(owner, IdAgente, Fecha, IdMoneda, IdLicencia, IdModalidad, pagina, cantRegxPag);
        }

        public JsonResult ObtenerDatos(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var obj = new BLAjustesComision().ObtenerDatos(GlobalVars.Global.OWNER, Id);
                    retorno.data = Json(obj, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Obtener datos", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insertar(BEAjustesComision en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEAjustesComision obj = new BEAjustesComision();
                    en.OWNER = GlobalVars.Global.OWNER;
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.BPS_ID = en.BPS_ID;
                    obj.LIC_ID = en.LIC_ID;
                    obj.COMT_ORIGEN = en.COMT_ORIGEN;
                    obj.COM_VALUE = en.COM_VALUE;
                    var dato = new BLAjustesComision().ObtenerDatosGrabar(en);
                    obj.COMT_ID = dato.COMT_ID == 0 ? null : dato.COMT_ID;
                    obj.LEVEL_ID = dato.LEVEL_ID;
                    obj.COM_PERC = dato.COM_PERC == 0 ? null : dato.COM_PERC;
                    obj.COM_BASE = dato.COM_BASE == 0 ? null : dato.COM_BASE;
                    obj.COM_PPIND = dato.COM_PPIND;
                    obj.COM_PRIMARY = dato.COM_PRIMARY == 0 ? null : dato.COM_PRIMARY;
                    obj.COM_EST = dato.COM_EST;
                    obj.PAY_ID = dato.PAY_ID == 0 ? null : dato.PAY_ID;
                    obj.COM_INVOICE = dato.COM_INVOICE == 0 ? null : dato.COM_INVOICE;
                    obj.COM_LDATE = dato.COM_LDATE;
                    obj.COM_RDATE = dato.COM_RDATE;
                    obj.COM_RDESC = dato.COM_RDESC;
                    obj.COM_PDATE = dato.COM_PDATE;
                    obj.COM_PNUM = dato.COM_PNUM == 0 ? null : dato.COM_PNUM;
                    obj.valgraba = en.valgraba;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    if (obj.valgraba == 0)
                    {
                        var datos = new BLAjustesComision().Insertar(obj);
                    }
                    else
                    {
                        obj.LOG_USER_UPDATE = UsuarioActual;
                        obj.COM_ID = obj.valgraba;
                        var datos = new BLAjustesComision().Actualizar(obj);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "insert Ajuste comision", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TotalValorAjusteComision(BEAjustesComision en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    en.OWNER = GlobalVars.Global.OWNER;
                    var resul = new BLAjustesComision().TotalValorAjusteComision(en);
                    retorno.data = Json(resul, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Obtener total valor ajuste comisión", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult Validacion(BEAjustesComision en)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLAjustesComision servicio = new BLAjustesComision();
                en.OWNER = GlobalVars.Global.OWNER;
                int resultado = servicio.ValidacionAjusteComision(en);
                if (resultado >= 1)
                {
                    retorno.result = 1;                    
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "No existen registros para el representante y licencia seleccionados." + Environment.NewLine + "no se puede agregar ajuste de comisión";
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Validacion insertar ajuste comision", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

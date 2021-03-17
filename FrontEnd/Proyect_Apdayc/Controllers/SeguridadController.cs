using Proyect_Apdayc.Clases;
using SGRDA.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Controllers
{
    public class SeguridadController : Base
    {
        //
        // GET: /Seguridad/
        public ActionResult Index()
        {
            Init(false);
            return View();
        }
        public JsonResult EnableUpdate(decimal idBps)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno)){

                    decimal idOficina = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                    string idPerfil = Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]);
                    var tieneAcceso =  PuedeRegistrarSocioUD(idOficina, idBps, idPerfil);
                    retorno.valor = tieneAcceso ? "1" : "0";
                    retorno.message = tieneAcceso ? "OK" : Constantes.MensajeGenerico.MSG_SIN_PERMISO_USUARIO_OFI;
                    retorno.result = Constantes.MensajeRetorno.OK;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = Constantes.MensajeRetorno.ERROR;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "PermiteActualizar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EnableUpdateUsuRec(decimal idBps)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    decimal idOficina = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                    string idPerfil= Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]);
                    var tieneAcceso = PuedeRegistrarSocioREC(idOficina, idBps, idPerfil);
                    retorno.valor = tieneAcceso ? "1" : "0";
                    retorno.message = tieneAcceso ? "OK" : Constantes.MensajeGenerico.MSG_SIN_PERMISO_USUARIO_OFI;
                    retorno.result = Constantes.MensajeRetorno.OK;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = Constantes.MensajeRetorno.ERROR;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "PermiteActualizar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EnableUpdatLic(decimal idLicencia)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    decimal idOficina = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                    string idPerfil = Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]);
                    var tieneAcceso = PuedeRegistrarSocioREC(idOficina, idLicencia, idPerfil);
                    retorno.valor = tieneAcceso ? "1" : "0";
                    retorno.message = tieneAcceso ? "OK" : Constantes.MensajeGenerico.MSG_SIN_PERMISO_EDIT_LIC;
                    retorno.result = Constantes.MensajeRetorno.OK;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = Constantes.MensajeRetorno.ERROR;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "PermiteActualizar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EnableInsLicXEstableSel(decimal idEst)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    decimal idOficina = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                    string idPerfil = Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]);
                    var tieneAcceso = PuedeRegistrarLic(idOficina, idEst, idPerfil);
                    retorno.valor = tieneAcceso ? "1" : "0";
                    retorno.message = tieneAcceso ? "OK" : Constantes.MensajeGenerico.MSG_SIN_PERMISO_INS_LIC_EST;
                    retorno.result = Constantes.MensajeRetorno.OK;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = Constantes.MensajeRetorno.ERROR;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "PermiteActualizar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public bool PuedeRegistrarSocioUD(decimal idOficina, decimal idBps, string idPerfil)
        {
            SGRDA.BL.BLREF_ROLES obj = new SGRDA.BL.BLREF_ROLES();
            bool resultado = true;
            var idPerfilAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["idPerfilAdminSeg"];

            if (idPerfil != Convert.ToString(idPerfilAdmin))
            {
                resultado = obj.TienePermiso(idOficina, idBps);
            }

            return resultado;
        }
        public bool PuedeRegistrarSocioREC(decimal idOficina, decimal idBps, string idPerfil)
        {
            SGRDA.BL.BLREF_ROLES obj = new SGRDA.BL.BLREF_ROLES();
            bool resultado = true;
            var idPerfilAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["idPerfilAdminSeg"];

            if (idPerfil != Convert.ToString(idPerfilAdmin))
            {
                resultado = obj.TienePermisoUsuRec(idOficina, idBps);
            }

            return resultado;
        }

        public bool PuedeEditarLic(decimal idOficina, decimal idLicencia, string idPerfil)
        {
            SGRDA.BL.BLREF_ROLES obj = new SGRDA.BL.BLREF_ROLES();
            bool resultado = true;
            var idPerfilAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["idPerfilAdminSeg"];

            if (idPerfil != Convert.ToString(idPerfilAdmin))
            {
                resultado = obj.TienePermisoEditarLic(idOficina, idLicencia);
            }

            return resultado;
        }
        public bool PuedeRegistrarLic(decimal idOficina, decimal idEstablecimiento, string idPerfil)
        {
            SGRDA.BL.BLREF_ROLES obj = new SGRDA.BL.BLREF_ROLES();
            bool resultado = true;
            var idPerfilAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["idPerfilAdminSeg"];

            if (idPerfil != Convert.ToString(idPerfilAdmin))
            {
                resultado = obj.TienePermisoRegistrarLic(idOficina, idEstablecimiento);
            }

            return resultado;
        }

    }
}

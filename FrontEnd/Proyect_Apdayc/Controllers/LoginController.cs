using Proyect_Apdayc.Clases;
//using SGRDA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {

            Response.Redirect("~/Home");
            return View();
        }
        public JsonResult Validar(Constantes.DTOLogin entidad)
        {

            Resultado resultado = new Resultado();
            try
            {

                if (entidad.usu == "admin")
                {
                    Session[Constantes.Sesiones.Usuario] = entidad.usu;
                    Session[Constantes.Sesiones.Nombre] = entidad.usu;
                    resultado.result = 1;
                    resultado.message = "Login Correcto";
                    RedirectToAction("Index", "Principal");
                }
                else
                {

                    resultado.result = 0;
                    resultado.message = "*Login Incorecto";


                }



            }
            catch (Exception ex)
            {

                resultado.result = 0;
                resultado.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;

            }

            return Json(resultado, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            //Response.Redirect("~/Login");
            Response.Redirect("~/Home");
            return View();
        }

        public JsonResult ValidarSesion()
        {
            Resultado resultado = new Resultado();
            if (Session[Constantes.Sesiones.Usuario] == null)
            {
                resultado.result = Constantes.MensajeRetorno.LOGOUT;
                resultado.message = Constantes.MensajeGenerico.MSG_LOGOUT;
                resultado.isRedirect = true;
                //resultado.redirectUrl = Url.Action("Index", "Login");
                resultado.redirectUrl = Url.Action("Index", "Home");

            }
            else
            {
                resultado.result = Constantes.MensajeRetorno.OK;
                resultado.message = string.Empty;
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}

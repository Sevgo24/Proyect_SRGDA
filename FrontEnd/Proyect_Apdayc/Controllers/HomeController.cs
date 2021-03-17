using Seg.Componente;
using SSEG.UInterfaces;
using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Proyect_Apdayc.Clases;
using System.Web.Configuration;

namespace SGRDA.MVC.Controllers
{
    public class HomeController : Controller
    {
        public static int s = 0;

        public ActionResult Principal()
        {
            return View();
        } 

        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection frm)
        {
         //   ucLogApp.ucLog.GrabarLogError("SGRDA", "JUAN PEREZ", "Usuario_Login", new Exception("errorrr"));


            if (string.IsNullOrEmpty(frm["user"]) | (string.IsNullOrEmpty(frm["pwd"])))
            {
                ModelState.AddModelError("user", "Ingrese su Usuario de Red y/o Contraseña.");

            }

            var valid = ModelState.IsValid;

            string user = frm["user"];
            string pwd = frm["pwd"];

            var usuario = new USUARIOS()
            {
                USUA_VUSUARIO_RED_USUARIO = user,
                USUA_VPASSWORD_USUARIO = pwd
            };

            if (valid == true)
            {
                 
                    var UsuarioWindow = new UIEncriptador().EncriptarCadena(usuario.USUA_VUSUARIO_RED_USUARIO);
                    var Clave = new UIEncriptador().EncriptarCadena(usuario.USUA_VPASSWORD_USUARIO);
                    var prefijo = System.Web.Configuration.WebConfigurationManager.AppSettings["PrefijoSist"];
                    var obj = new Autentificar().Autentificacion(UsuarioWindow, Clave,prefijo);
                 

                    if (obj != null)
                    {
                        if (obj.result != 0)
                        {
                            Session[Constantes.Sesiones.Nombre] = obj.Nombre;
                            Session[Constantes.Sesiones.Usuario] = obj.Nombre;
                            Session[Constantes.Sesiones.CodigoUsuarioOficina] = obj.CodigoUsuarioOficina;
                            Session[Constantes.Sesiones.CodigoOficina] = obj.CodigoOficina;
                            Session[Constantes.Sesiones.Oficina] = obj.Oficina;
                            Session[Constantes.Sesiones.CodigoPerfil] = obj.CodigoPerfil;
                            Session[Constantes.Sesiones.Perfil] = obj.Perfil;
                            Session[Constantes.Sesiones.CodigoPerdilUsuario] = obj.CodigoPerdilUsuario;

                            string html = new Menu().CargarMenu(Convert.ToInt32(obj.CodigoPerfil),
                                                        Convert.ToString(WebConfigurationManager.AppSettings["PrefijoSist"]),
                                                        Convert.ToInt32(obj.CodigoUsuarioOficina),
                                                        Convert.ToInt32(obj.CodigoPerdilUsuario),
                                                       Convert.ToString(WebConfigurationManager.AppSettings["PaginaMain"]));                           
                            Session[Constantes.Sesiones.MenuCargado] = html;

                        //Response.Redirect("http://192.168.252.105/SGRDA_PRUEBA/Principal/");
                        RedirectToAction("Index", "Principal");
                        return Redirect("~/Principal/");
                    }
                        else
                        {
                            ModelState.AddModelError("user", obj.message);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("user", "*Login Incorrecto.");
                    }

                }
                else
                {
                    ModelState.AddModelError("user", "No se encontró el Usuario Ingresado.");
                     
                }
           
            return View();
        }
 
    }
}

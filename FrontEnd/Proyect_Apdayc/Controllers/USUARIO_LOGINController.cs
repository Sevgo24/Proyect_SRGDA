using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Proyect_Apdayc.Clases;

namespace Proyect_Apdayc.Controllers
{
    public class USUARIO_LOGINController : Base
    {
        public static int s = 0;
        public ActionResult Index(int id = 1, int id2 = 1)
        {
            Init(false);//add sysseg
            s = id;
            if (s == 1) { TempData["active"] = "active"; TempData["active2"] = ""; TempData["active3"] = ""; TempData["active4"] = ""; TempData["active5"] = ""; TempData["active6"] = ""; }
            if (s == 2) { TempData["active"] = ""; TempData["active2"] = "active"; TempData["active3"] = ""; TempData["active4"] = ""; TempData["active5"] = ""; TempData["active6"] = ""; }
            if (s == 3) { TempData["active"] = ""; TempData["active2"] = ""; TempData["active3"] = "active"; TempData["active4"] = ""; TempData["active5"] = ""; TempData["active6"] = ""; }
            if (s == 4) { TempData["active"] = ""; TempData["active2"] = ""; TempData["active3"] = ""; TempData["active4"] = "active"; TempData["active5"] = ""; TempData["active6"] = ""; }
            if (s == 5) { TempData["active"] = ""; TempData["active2"] = ""; TempData["active3"] = ""; TempData["active4"] = ""; TempData["active5"] = "active"; TempData["active6"] = ""; }
            if (s == 6) { TempData["active"] = ""; TempData["active2"] = ""; TempData["active3"] = ""; TempData["active4"] = ""; TempData["active5"] = ""; TempData["active6"] = "active"; }

            string menu = string.Empty;
            string menuBody = loadMenu(id2, ref menu);

            TempData["menu"] = menuBody;
            return View();
        }

        public ActionResult Construccion()
        {
            Init(false);//add sysseg
            return View();
        }

        public ActionResult Principal()
        {
            Init(false);//add sysseg
            return View();
        }

        private string loadMenu(int patherID, ref string menu)
        {
            XElement options = new SGRDA.MVC.Controllers.xmlOptions().menuOptions();

            var result = from item in options.Descendants("Node")
                         where item.Element("patherID").Value == patherID.ToString()
                         select new
                         {
                             id = item.Element("id").Value,
                             patherId = item.Element("patherID").Value,
                             name = item.Element("name").Value,
                             url = item.Element("url").Value,
                             nivel = item.Element("level").Value
                         };

            foreach (var element in result.ToList())
            {
                //var estilo = element.nivel == "2" ? "font-weight:bold;" : "style=color:red";
                var estilo = element.nivel == "2" ? "navi" : "navi2";
                menu += "<ul id='" + estilo + "'>";
                menu += "<li>";
                menu += "<a href='" + element.url + "' target='C'>" + element.name + "</a>";
                menu += "</li>";
                loadMenu(int.Parse(element.id.ToString()), ref menu);
                menu += "</ul>";
            }
            return menu;
        }

         public ActionResult Usuario_Login()
        {

            Init(false);//add sysseg
            return View();
        }

        [HttpPost]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Usuario_Login(FormCollection frm)
        {
            Init(false);//add sysseg

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
                var std = new BLUSUARIOS().USUARIOS_spBuscarLogin(usuario);

                if (std.Count != 0)
                {
                    return RedirectToAction("Index", "Home");
                    
                    //return RedirectToRoute("Login", "USUARIO_LOGIN");
                }
                else
                {
                    ModelState.AddModelError("user", "No se encontró el Usuario Ingresado.");
                    //return 
                }
            }
            else
            {
                TempData["class1"] = "alert alert-danger";
            }

            return View(); //RedirectToAction("Index", "USUARIO_LOGIN");
        }
    }
}

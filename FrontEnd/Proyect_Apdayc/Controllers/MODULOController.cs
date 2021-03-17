using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace SGRDA.MVC.Controllers
{
    public class MODULOController : Controller
    {
        //
        // GET: /MODULO/

        public ActionResult Index()
        {
            string menu = string.Empty;
            string menuBody = loadMenu(0, ref menu);

            //ViewBag.Menu = menuBody;
            TempData["xxx"] = menuBody;
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
                             url = item.Element("url").Value                             
                         };

            foreach (var element in result.ToList())
            {
                menu += "<ul id='menu'>";
                menu += "<li>";
                menu += "<a href='" + element.url + "'>" + element.name + "</a>";
                menu += "</li>";
                loadMenu(int.Parse(element.id.ToString()), ref menu);
                menu += "</ul>";
            }

            return menu;
        }

    }
}

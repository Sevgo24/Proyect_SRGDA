using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases;

namespace Proyect_Apdayc.Controllers
{
    public class Roles_UsuariosController : Controller
    {
        //
        // GET: /Roles_Usuarios/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult usp_listar_RolesUsuariosPage(int skip, int take, int page, int pageSize, string group, string dato)
        {
            Resultado retorno = new Resultado();

            var lista = usp_Get_RolesUsuariosPage(dato, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new Roles_Usuarios { Rol_Usuario = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new Roles_Usuarios { Rol_Usuario = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<Roles_Usuarios> usp_Get_RolesUsuariosPage(string param, int pagina, int cantRegxPag)
        {
            return new BLRoles_Usuarios().usp_Get_RolesUsuariosPage(param, pagina, cantRegxPag);
        }
    }
}

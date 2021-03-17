using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;

namespace Proyect_Apdayc.Controllers
{
    public class ROLES_PERMISOSController : Controller
    {
        //
        // GET: /ROLES_PERMISOS/
        int nivel=0;
        public ActionResult Create()
        {
            ListarRol();
            ListarModulo();
            ListarNivel();
            ListarModuloNivel(3);
            return View();
        }

        private void ListarModuloNivel(int nivel)
        {
            items = new BLROLES_PERMISOS().usp_listarNivelModulo(nivel)
              .Select(c => new SelectListItem
              {
                  Value = c.MODU_INIVEL_MODULO.ToString(),
                  Text = c.MODU_VNOMBRE_MODULO
              });
            ViewData["Lista_ModuloNivel"] = items;
        }

        private void ListarNivel()
        {
            List<SelectListItem> nivel = new List<SelectListItem>();
            nivel.Add(new SelectListItem { Text = "Nivel 1", Value = "1" });
            nivel.Add(new SelectListItem { Text = "Nivel 2", Value = "2" });
            ViewData["Lista_Nivel"] = nivel;
        }

        private void ListarRol()
        {
            items = new BLROLES().usp_listar_Roles()
              .Select(c => new SelectListItem
              {
                  Value = c.ROL_ICODIGO_ROL.ToString(),
                  Text = c.ROL_VNOMBRE_ROL
              });
            ViewData["Lista_Roles"] = items;
        }

        private void ListarModulo()
        {
            items = new BLCABECERA_MODULO().usp_listar_Cabecera_Modulo()
              .Select(c => new SelectListItem
              {
                  Value = c.CABE_ICODIGO_MODULO.ToString(),
                  Text = c.CABE_VNOMBRE_MODULO
              });
            ViewData["Lista_Cabecera_Modulo"] = items;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            if (string.IsNullOrEmpty(frm["cb1"]))
            {
                ModelState.AddModelError("cb1", "Selecione un Rol.");
            }

            if (string.IsNullOrEmpty(frm["cb2"]))
            {
                ModelState.AddModelError("cb2", "Seleccione un opción.");
            }

            if (string.IsNullOrEmpty(frm["cb3"]))
            {
                ModelState.AddModelError("cb3", "Seleccione un Nivel.");
            }

            if (string.IsNullOrEmpty(frm["MODU_VRUTA_PAGINA"]))
            {
                ModelState.AddModelError("MODU_VRUTA_PAGINA", "Ingrese la ruta de la página");
            }

            var valid = ModelState.IsValid;

            var permisos = new ROLES_PERMISOS()
            {
                ROL_ICODIGO_ROL = Convert.ToInt32(frm["cb1"]),
                CABE_ICODIGO_MODULO = Convert.ToInt32(frm["cb2"]),
                MODU_INIVEL_MODULO = Convert.ToInt32(frm["cb3"]),
                MODU_VNOMBRE_MODULO = frm["MODU_VNOMBRE_MODULO"],
                MODU_VRUTA_PAGINA = frm["MODU_VRUTA_PAGINA"],
                MODU_VDESCRIPCION_MODULO = frm["MODU_VDESCRIPCION_MODULO"],
                ROMO_CACTIVO = frm["cboRol"]
            };

            if (valid == true)
            {
                int std = new BLROLES_PERMISOS().usp_Ins_Roles_Permisos(permisos);

                if (std > 0)
                {
                    TempData["msg"] = "Registrado Correctamente";
                    TempData["class"] = "alert alert-success";
                }
                else
                {
                    TempData["msg"] = "Ocurrio un inconveniente, no se pudo Registrar";
                    TempData["class"] = "alert alert-danger";
                }
            }
            else
            {
                TempData["class1"] = "alert alert-danger";
            }

            return View();
        }

        IEnumerable<SelectListItem> items;
        public ActionResult Index()
        {
            ListarRol();
            ListarModulo();
            return View();
        }

        public JsonResult usp_listar_RolesPermisosJson(int skip, int take, int page, int pageSize, string group, int rol, int modulo)
        {
            var lista = usp_Get_RolesPermisosPag(rol, modulo, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            return Json(new ROLES_PERMISOS { ROL_PERMISOS = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);

        }

        public List<ROLES_PERMISOS> usp_Get_RolesPermisosPag(int rol, int mod, int pagina, int cantRegxPag)
        {
            return new BLROLES_PERMISOS().usp_Get_RolesPermisosPage(rol, mod, pagina, cantRegxPag);
        }

        //EDITAR ROLES PERMISOS
        public ActionResult Edit(int id = 0)
        {
            ListarRol();
            ListarModulo();
            ROLES_PERMISOS cod = new ROLES_PERMISOS();
            var lista = new BLROLES_PERMISOS().usp_listar_RolesPermisos_by_codigo(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    cod.ROL_ICODIGO_ROL = item.ROL_ICODIGO_ROL;
                    cod.CABE_ICODIGO_MODULO = item.CABE_ICODIGO_MODULO;
                    cod.MODU_VNOMBRE_MODULO = item.MODU_VNOMBRE_MODULO;
                    cod.MODU_VRUTA_PAGINA = item.MODU_VRUTA_PAGINA;
                    cod.MODU_VDESCRIPCION_MODULO = item.MODU_VDESCRIPCION_MODULO;
                    cod.ROMO_CACTIVO = item.ROMO_CACTIVO;
                    cod.LOG_USER_CREAT = item.LOG_USER_CREAT;
                }
            }

            return View(cod);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm)
        {
            var user = new ROLES_PERMISOS()
            {
                MODU_ICODIGO_MODULO = Convert.ToInt32(frm["MODU_ICODIGO_MODULO"]),
                ROL_ICODIGO_ROL = Convert.ToInt32(frm["ROL_ICODIGO_ROL"]),
                CABE_ICODIGO_MODULO = Convert.ToInt32(frm["CABE_ICODIGO_MODULO"]),
                MODU_INIVEL_MODULO = Convert.ToInt32(frm["MODU_INIVEL_MODULO"]),
                MODU_VNOMBRE_MODULO = frm["MODU_VNOMBRE_MODULO"],
                MODU_VRUTA_PAGINA = frm["MODU_VRUTA_PAGINA"],
                MODU_VDESCRIPCION_MODULO = frm["MODU_VDESCRIPCION_MODULO"],
                ROMO_CACTIVO = frm["ROMO_CACTIVO"],
                LOG_USER_UPDATE = "USERMOD"

            };
            int std = new BLROLES_PERMISOS().usp_Upd_Roles_Permisos(user);

            if (std == 1)
            {
                TempData["msg"] = "Actualizado Correctamente";
                TempData["class"] = "alert alert-success";
            }
            else
            {
                TempData["msg"] = "Ocurrio un inconveniente, no se pudo Actualizar";
                TempData["class"] = "alert alert-danger";
            }

            return RedirectToAction("Edit");
        }
    }
}

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
    public class USUARIOSController : Base
    {
        public ActionResult Index()
        {
            Init(false);//add sysseg
            var lista = usp_Get_UsuariosPag("","", 1, 5);

            return View();
        }

        public List<USUARIOS> USUARIOS_spBuscar(int usua_icodigo_usuario, string usua_vnombre_usuario, string usua_vapellido_paterno_usuario, string usua_vapellido_materno_usuario, char usua_cactivo_usuario)
        {
            return new BLUSUARIOS().USUARIOS_spBuscar(usua_icodigo_usuario, usua_vnombre_usuario, usua_vapellido_paterno_usuario, usua_vapellido_materno_usuario, usua_cactivo_usuario);
        }

        public JsonResult usp_listar_UsuariosJson(int skip, int take, int page, int pageSize, string group, string usuario_red, string dato)
        {
            Init();//add sysseg
            Resultado retorno = new Resultado();

            var lista = usp_Get_UsuariosPag(usuario_red, dato, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

                if (tot.Count == 0)
                {
                 return Json(new USUARIOS { USUARIO = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);   
                }
                else
                {
                    return Json(new USUARIOS { USUARIO = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
                }
        }

        public List<USUARIOS> usp_Get_UsuariosPag(string usuario_red, string param, int pagina, int cantRegxPag)
        {
            return new BLUSUARIOS().usp_Get_UsuariosPage(usuario_red, param, pagina, cantRegxPag);
        }

        public ActionResult Edit(int id = 0)
        {
            Init(false);//add sysseg
            ListarRol();
            USUARIOS user = new USUARIOS();
            var lista = new BLUSUARIOS().usp_listar_Usuarios_by_codigo(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    user.USUA_ICODIGO_USUARIO = item.USUA_ICODIGO_USUARIO;
                    user.USUA_VNOMBRE_USUARIO = item.USUA_VNOMBRE_USUARIO;
                    user.USUA_VAPELLIDO_PATERNO_USUARIO = item.USUA_VAPELLIDO_PATERNO_USUARIO;
                    user.USUA_VAPELLIDO_MATERNO_USUARIO = item.USUA_VAPELLIDO_MATERNO_USUARIO;
                    user.USUA_VUSUARIO_RED_USUARIO = item.USUA_VUSUARIO_RED_USUARIO;
                    user.USUA_VPASSWORD_USUARIO = item.USUA_VPASSWORD_USUARIO;
                    user.ROL_ICODIGO_ROL = item.ROL_ICODIGO_ROL;
                    user.USUA_CACTIVO_USUARIO = item.USUA_CACTIVO_USUARIO;
                }
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm)
        {
            var user = new USUARIOS()
            {
                USUA_ICODIGO_USUARIO = Convert.ToInt32(frm["id"]),
                USUA_VNOMBRE_USUARIO = frm["nombre"],
                USUA_VAPELLIDO_PATERNO_USUARIO = frm["apellidoP"],
                USUA_VAPELLIDO_MATERNO_USUARIO = frm["apellidoM"],
                USUA_VUSUARIO_RED_USUARIO = frm["usuario_red"],
                USUA_VPASSWORD_USUARIO = frm["password"],
                ROL_ICODIGO_ROL = Convert.ToInt32(frm["droles"]),
                USUA_CACTIVO_USUARIO = Convert.ToBoolean(frm["std"]),
                LOG_USER_UPDATE = "USERMOD"

            };
            int std = new BLUSUARIOS().usp_Upd_Usuarios(user);

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


        public ActionResult Create()
        {
            Init(false);//add sysseg
            ListarRol();
            return View();
        }

        IEnumerable<SelectListItem> items;
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

        [HttpPost]
        public ActionResult Delete(List<USUARIOS> dato)
        {
            Init(false);//add sysseg
            foreach (var item in dato)
            {
                var user = new USUARIOS()
                {
                    USUA_ICODIGO_USUARIO = item.USUA_ICODIGO_USUARIO,
                    USUA_CACTIVO_USUARIO = item.USUA_CACTIVO_USUARIO,
                    LOG_USER_UPDATE = "USERMOD"
                };
                int std = new BLUSUARIOS().usp_Upd_estado_Usuarios(user);
            }
            return RedirectToAction("Index");// Json(new { flag = flag });
        }   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {

            if (string.IsNullOrEmpty(frm["nombre"]))
            {
                ModelState.AddModelError("nombre", "Ingrese Nombre");
            }

            if (string.IsNullOrEmpty(frm["txtApellidoP"]))
            {
                ModelState.AddModelError("txtApellidoP", "Ingrese el Apellido Paterno");
            }

            if (string.IsNullOrEmpty(frm["txtUsuarioRed"]))
            {
                ModelState.AddModelError("txtUsuarioRed", "Ingrese su usuario de red");
            }

            if (string.IsNullOrEmpty(frm["txtPassword"]))
            {
                ModelState.AddModelError("txtPassword", "Ingrese su password");
            }

            if (string.IsNullOrEmpty(frm["cboRol"]))
            {
                ModelState.AddModelError("cboRol", "Seleccione un Rol");
            }


            var valid = ModelState.IsValid;
            USUARIOS user = new USUARIOS()
            {
                USUA_VNOMBRE_USUARIO = frm["nombre"],
                USUA_VAPELLIDO_PATERNO_USUARIO = frm["paterno"],
                USUA_VAPELLIDO_MATERNO_USUARIO = frm["materno"],
                USUA_VUSUARIO_RED_USUARIO = frm["red"],
                USUA_VPASSWORD_USUARIO = frm["password"],
                ROL_ICODIGO_ROL = Convert.ToInt32(frm["rol"]),
                USUA_CACTIVO_USUARIO = Convert.ToBoolean(frm["activo"]),
                LOG_USER_UPDATE = "USERCREAT"

            };

            if (valid == true)
            {
                int std = new BLUSUARIOS().usp_Ins_Usuarios(user);

                if (std == 1)
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

    }
}
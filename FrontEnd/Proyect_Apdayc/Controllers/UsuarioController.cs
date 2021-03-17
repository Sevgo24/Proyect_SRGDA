using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases;

namespace Proyect_Apdayc.Controllers
{
    public class UsuarioController : Base
    {
        //
        // GET: /Usuario/

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public JsonResult Listar(int skip, int take, int page, int pageSize, string group, string usuario_red, string dato)
        {
            var lista = usp_Get_UsuariosPag(usuario_red, dato, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            
            return Json(new USUARIOS { USUARIO = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
        }

        public List<USUARIOS> usp_Get_UsuariosPag(string usuario_red, string param, int pagina, int cantRegxPag)
        {
            return new BLUSUARIOS().usp_Get_UsuariosPage(usuario_red, param, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Insertar(USUARIOS entidad)
        {
            bool exito = true;
            Resultado retorno = new Resultado();
            string msg_validacion = "";
            try
            {
                if (!isLogout(ref retorno))
                {
                    validacion(out exito, out msg_validacion, entidad);
                    if (exito)
                    {
                        var servicio = new BLUSUARIOS();


                        if (entidad.USUA_ICODIGO_USUARIO == 0)
                        {
                            entidad.LOG_USER_CREAT = UsuarioActual;
                            entidad.USUA_IUSUARIO_CREA = 1;
                            servicio.usp_Ins_Usuarios(entidad);
                        }
                        else
                        {
                            entidad.LOG_USER_UPDATE = UsuarioActual;
                            servicio.usp_Upd_Usuarios(entidad);
                        }
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;

                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = msg_validacion;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "insertar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private static void validacion(out bool exito, out string msg_validacion, USUARIOS entidad)
        {
            exito = true;
            msg_validacion = string.Empty;
            if (exito && string.IsNullOrEmpty(entidad.USUA_VNOMBRE_USUARIO))
            {
                msg_validacion = "Ingrese Nombre";
                exito = false;
            }

            if (exito && string.IsNullOrEmpty(entidad.USUA_VAPELLIDO_PATERNO_USUARIO))
            {
                msg_validacion = "Ingrese el Apellido Paterno";
                exito = false;
            }

            if (exito && string.IsNullOrEmpty(entidad.USUA_VUSUARIO_RED_USUARIO))
            {
                msg_validacion = "Ingrese su usuario de red";
                exito = false;
            }

            if (exito && string.IsNullOrEmpty(entidad.USUA_VPASSWORD_USUARIO))
            {
                msg_validacion = "Ingrese su password";
                exito = false;
            }


        }
        [HttpPost]
        public JsonResult Obtiene(int id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    USUARIOS user = new USUARIOS();
                    var lista = new BLUSUARIOS().usp_listar_Usuarios_by_codigo(id);

                    if (lista != null)
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
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(user, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtiene", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar(int codigo)
        {
             Resultado retorno = new Resultado();
             try
             {
                 if (!isLogout(ref retorno))
                 {
                     var servicio = new BLUSUARIOS();
                     //foreach (var idUsu in codigos)
                     //{
                     var user = new USUARIOS();
                     user.USUA_ICODIGO_USUARIO = codigo;
                     user.USUA_CACTIVO_USUARIO = false;
                     user.LOG_USER_UPDATE = UsuarioActual;

                     servicio.usp_Upd_estado_Usuarios(user);
                     // }


                     retorno.result = 1;
                     retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;

                 }
                 
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "elimina", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}

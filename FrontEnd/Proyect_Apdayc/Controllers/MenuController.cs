using Seg.Componente;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;

namespace Proyect_Apdayc.Controllers
{
    public class MenuController : Base
    {
        //
        // GET: /Menu/
         
        
        public ActionResult Index()
        {
            Init(false);
            setIdPerfil();
            return View();
        }
        [HttpPost]
        public JsonResult obtenerNavBar()
        {
            Resultado retorno = new Resultado();
            try
            {
                DTOSesion obj = new DTOSesion();
                obj.Usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
                obj.Perfil = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Perfil]);
                obj.Oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);

                retorno.result = 1;
                retorno.data = Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtenerNavBar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CargarMenu()
        {
            try
            {
                string html = "";
                if (Session[Constantes.Sesiones.MenuCargado] == null)
                {
                      html = new Menu().CargarMenu(Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoPerfil]),
                                                        Convert.ToString(WebConfigurationManager.AppSettings["PrefijoSist"]),
                                                        Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoUsuarioOficina]),
                                                        Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoPerdilUsuario]),
                                                       Convert.ToString(WebConfigurationManager.AppSettings["PaginaMain"]));                   

                      Session[Constantes.Sesiones.MenuCargado] = html;
                }
                else {
                    if ( Convert.ToString(Session[Constantes.Sesiones.MenuCambiaRol]) == "S")
                    {
                        html = new Menu().CargarMenu(Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoPerfil]),
                                                       Convert.ToString(WebConfigurationManager.AppSettings["PrefijoSist"]),
                                                       Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoUsuarioOficina]),
                                                       Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoPerdilUsuario]),
                                                       Convert.ToString(WebConfigurationManager.AppSettings["PaginaMain"]));                        
                      Session[Constantes.Sesiones.MenuCargado] = html;                      
                    }
                    html = Convert.ToString(Session[Constantes.Sesiones.MenuCargado]);
                }
                return Json(new
                {
                    result = 1,
                    message =html
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "CargarMenu", ex);
                 return Json(new { result = 0, message =  Constantes.MensajeGenerico.MSG_ERROR_GENERICO }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

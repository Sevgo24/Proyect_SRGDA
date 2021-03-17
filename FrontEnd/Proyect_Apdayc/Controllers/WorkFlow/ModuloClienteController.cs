using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;

namespace Proyect_Apdayc.Controllers.Flujo
{
    public class ModuloClienteController : Base
    {
        //
            // GET: /ModuloCliente/

        public const string nomAplicacion = "SRGDA";

        public ActionResult Index()
        {
            Init(false);
            return View();
        }
            
        public ActionResult Nuevo()
        {
            Init(false);
            return View();
        }
        
        public JsonResult Listar(int skip, int take, int page, int pageSize, string group, string cliente, string etiqueta, int estado)
        {
            Resultado retorno = new Resultado();
            var lista = BLListar(GlobalVars.Global.OWNER, cliente, etiqueta, estado, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEModuloCliente { ListarModuloCliente = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEModuloCliente { ListarModuloCliente = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEModuloCliente> BLListar(string owner, string cliente, string etiqueta, int estado,  int pagina, int cantRegxPag)
        {
            return new BLModuloCliente().Listar(owner, cliente, etiqueta, estado, pagina, cantRegxPag);
        }

        public ActionResult Obtener(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var cliente = new BLModuloCliente().Obtener(GlobalVars.Global.OWNER,id);
                    retorno.data = Json(cliente, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public ActionResult Insertar(BEModuloCliente cliente)
        {
            Resultado retorno = new Resultado();
            try
            {                
                cliente.OWNER = GlobalVars.Global.OWNER;
                if (cliente.PROC_MOD == 0)
                {
                    cliente.LOG_USER_CREAT = UsuarioActual;
                    var datos = new BLModuloCliente().Insertar(cliente);
                }
                else
                {
                    cliente.LOG_USER_UPDATE =  UsuarioActual;
                    var datos=new BLModuloCliente().Actualizar(cliente);
                }
                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar", ex);                
            }
            return Json(retorno,JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public ActionResult Eliminar(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                BEModuloCliente cliente = new BEModuloCliente();
                cliente.OWNER = GlobalVars.Global.OWNER;
                cliente.PROC_MOD = id;
                cliente.LOG_USER_UPDATE = UsuarioActual;
                var datos= new BLModuloCliente().Eliminar(cliente);
                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
            }
            catch(Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar", ex);  
            }
            return Json(retorno,JsonRequestBehavior.AllowGet );
        }

    }
}

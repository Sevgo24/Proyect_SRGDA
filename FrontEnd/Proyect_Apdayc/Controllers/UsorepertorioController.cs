using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;

namespace Proyect_Apdayc.Controllers
{
    public class UsorepertorioController : Base
    {
        //
        // GET: /Usorepertorio/
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

        public JsonResult ListarUsoRepertorio(int skip, int take, int page, int pageSize, string dato, int st)
        {
            var lista = Listausorepertorio(dato, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEUsorepertorio { ListaUsoRepertorio = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEUsorepertorio { ListaUsoRepertorio = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEUsorepertorio> Listausorepertorio(string dato, int st, int pagina, int cantRegxPag)
        {
            return new BLUsorepertorio().usp_Get_UsoRepertorioPage(dato, st, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Obtiene(string id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLUsorepertorio servicio = new BLUsorepertorio();
                    var usorepertorio = servicio.Obtener(GlobalVars.Global.OWNER, id);

                    if (usorepertorio != null)
                    {
                        DTOUsoRepertorio usoRepertorioDto = new DTOUsoRepertorio()
                        {
                            Id = usorepertorio.MOD_USAGE,
                            Descripcion = usorepertorio.MOD_DUSAGE,
                        };
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(usorepertorio, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado el repertorio";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "obtener datos uso repertorio", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insertar(BEUsorepertorio Usorepertorio)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEUsorepertorio objUso = new BEUsorepertorio();
                    objUso.OWNER = GlobalVars.Global.OWNER;
                    objUso.MOD_USAGE = Usorepertorio.MOD_USAGE;
                    objUso.MOD_DUSAGE = Usorepertorio.MOD_DUSAGE;
                    objUso.valgraba = Usorepertorio.valgraba;
                    objUso.LOG_USER_CREAT = UsuarioActual;
                    if (objUso.valgraba == 0)
                    {
                        var datos = new BLUsorepertorio().Insertar(objUso);
                    }
                    else
                    {
                        objUso.MOD_USAGE = Usorepertorio.MOD_USAGE;
                        objUso.LOG_USER_UPDAT = UsuarioActual;
                        var datos = new BLUsorepertorio().Actualizar(objUso);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "insert uso repertorio", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(string idUsorepertorio)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLUsorepertorio servicio = new BLUsorepertorio();
                    var result = servicio.Eliminar(new BEUsorepertorio
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        MOD_USAGE = idUsorepertorio,
                        LOG_USER_UPDAT = UsuarioActual
                    });
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar Uso repertorio", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult ObtenerXDescripcion(BEUsorepertorio Usorepertorio)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLUsorepertorio servicio = new BLUsorepertorio();
                Usorepertorio.OWNER = GlobalVars.Global.OWNER;
                int resultado = servicio.ObtenerXDescripcion(Usorepertorio);
                if (resultado >= 1)
                    retorno.result = 1;
                else
                    retorno.result = 0;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Uso repertorio ObtenerXDescripcion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult ObtenerXCodigo(BEUsorepertorio Usorepertorio)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLUsorepertorio servicio = new BLUsorepertorio();
                Usorepertorio.OWNER = GlobalVars.Global.OWNER;
                int resultado = servicio.ObtenerXCodigo(Usorepertorio);
                if (resultado >= 1)
                    retorno.result = 1;
                else
                    retorno.result = 0;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Uso repertorio ObtenerXCodigo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

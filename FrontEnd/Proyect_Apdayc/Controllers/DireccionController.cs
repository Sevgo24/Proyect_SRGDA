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

namespace Proyect_Apdayc.Controllers
{
    public class DireccionController : Base
    {
        //
        // GET: /Direccion/

        public ActionResult Index()
        {
            Init(false);
            //usp_listar_DireccionJson(1, 10, 1, 5, "", 1);
            //usp_listar_Address(1);
            return View();
        }

        public JsonResult usp_listar_DireccionJson(int skip, int take, int page, int pageSize, string group, decimal dato)
        {
            var lista = usp_Get_DireccionPage(dato, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEDireccion { Direccion = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEDireccion { Direccion = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEDireccion> usp_Get_DireccionPage(decimal param, int pagina, int cantRegxPag)
        {
            return new BLDirecciones().usp_Get_DireccionPage(param, pagina, cantRegxPag);
        }

        //public JsonResult usp_listar_Address(decimal bps_id)
        //{
        //    var lista = USP_REC_ADDRESS_LISTAR(bps_id);

        //    return Json(lista, JsonRequestBehavior.AllowGet);
        //}

        //public List<BEDireccion> USP_REC_ADDRESS_LISTAR(decimal bps_id)
        //{
        //    return new BLDirecciones().USP_REC_ADDRESS_LISTAR(bps_id);
        //}

        public ActionResult Create()
        {
            Init(false);
            return View();
        }

        public ActionResult Edit()
        {
            Init(false);
            return View();
        }

        private static void validacion(out bool exito, out string msg_validacion, BEDireccion entidad)
        {
            exito = true;
            msg_validacion = string.Empty;
            if (exito && entidad.ADD_TYPE == 0)
            {
                msg_validacion = "Seleccione el Tipo de Dirección";
                exito = false;
            }

            if (exito && entidad.TIS_N == 0)
            {
                msg_validacion = "Seleccione el Territorio";
                exito = false;
            }
        }

        [HttpPost]
        public JsonResult Insertar(BEDireccion entidad)
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
                        var servicio = new BLDirecciones();


                        if (entidad.ADD_ID == 0)
                        {
                            entidad.LOG_USER_CREAT = UsuarioActual;
                            servicio.usp_Ins_Direccion(entidad);
                        }
                        else
                        {
                            entidad.LOG_USER_UPDATE = UsuarioActual;
                            servicio.usp_Upd_Direccion(entidad);
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtiene", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult editar(BEDireccion entidad)
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
                        var servicio = new BLDirecciones();


                        if (entidad.ADD_ID == 0)
                        {
                            entidad.LOG_USER_CREAT = UsuarioActual;
                            servicio.usp_Ins_Direccion(entidad);
                        }
                        else
                        {
                            entidad.LOG_USER_UPDATE = UsuarioActual;
                            servicio.usp_Upd_Direccion(entidad);
                        }
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GRABAR;
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtiene", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

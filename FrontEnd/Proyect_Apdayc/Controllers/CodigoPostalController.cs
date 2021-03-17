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
    public class CodigoPostalController : Base
    {
        //
        // GET: /CodigoPostal/

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

        public ActionResult Edit()
        {
            Init(false);
            return View();
        }

        public JsonResult Listar_PageJson_CodigoPostal(int skip, int take, int page, int pageSize, string group, decimal parametro, int st)
        {
            Resultado retorno = new Resultado();

            var lista = Listar_Page_CodigoPostal(parametro, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BECodigoPostal { Codigo_Postal = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BECodigoPostal { Codigo_Postal = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BECodigoPostal> Listar_Page_CodigoPostal(decimal parametro, int st, int pagina, int cantRegxPag)
        {
            return new BLCodigoPostal().Listar_Page_CodigoPostal(parametro, st, pagina, cantRegxPag);
        }

        //private static void validacion(out bool exito, out string msg_validacion, BECodigoPostal entidad)
        //{
        //    exito = true;
        //    msg_validacion = string.Empty;

        //    if (exito && string.IsNullOrEmpty(entidad.TIS_N))
        //    {
        //        msg_validacion = "Selecione un Tipo de Gasto";
        //        exito = false;
        //    }

        //    if (exito && string.IsNullOrEmpty(entidad.POSITIONS))
        //    {
        //        msg_validacion = "Ingrese una descripción corta";
        //        exito = false;
        //    }
        //}

        [HttpPost]
        public JsonResult Obtiene(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BECodigoPostal tipo = new BECodigoPostal();
                    var lista = new BLCodigoPostal().Obtener(id);

                    if (lista != null)
                    {
                        foreach (var item in lista)
                        {
                            tipo.CPO_ID = item.CPO_ID;
                            tipo.TIS_N = item.TIS_N;
                            tipo.POSITIONS = item.POSITIONS;
                            tipo.LOG_USER_CREAT = item.LOG_USER_CREAT;
                            tipo.DescripcionUbigeo = new BLUbigeo().ObtenerDescripcion(604, item.TIS_N).NOMBRE_UBIGEO;
                        }
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado el Código postal";
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
        public JsonResult Insertar(BECodigoPostal entidad)
        {
            bool exito = true;
            Resultado retorno = new Resultado();
            string msg_validacion = "";
            try
            {
                if (!isLogout(ref retorno))
                {
                    //validacion(out exito, out msg_validacion, entidad);
                    if (exito)
                    {
                        var servicio = new BLCodigoPostal();

                        if (entidad.CPO_ID == 0)
                        {
                            entidad.LOG_USER_CREAT = UsuarioActual;
                            servicio.Insertar(entidad);
                        }
                        else
                        {
                            entidad.LOG_USER_UPDATE = UsuarioActual;
                            servicio.Actualizar(entidad);
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
        public JsonResult Eliminar(int codigo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var servicio = new BLCodigoPostal();

                    var tipo = new BECodigoPostal();
                    tipo.CPO_ID = codigo;
                    tipo.LOG_USER_UPDATE = UsuarioActual;

                    servicio.Eliminar(tipo);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
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

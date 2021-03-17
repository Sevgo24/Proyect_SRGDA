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
    public class DescuentosController : Base
    {
        //
        // GET: /Descuentos/

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

        public JsonResult Listar_PageJson_Descuentos(int skip, int take, int page, int pageSize, string group, decimal tipo, string parametro, int st)
        {
            Resultado retorno = new Resultado();

            var lista = Listar_Page_Descuentos(tipo, parametro, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEDescuentos { Descuentos = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEDescuentos { Descuentos = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEDescuentos> Listar_Page_Descuentos(decimal tipo, string parametro, int st, int pagina, int cantRegxPag)
        {
            return new BLDescuentos().Listar_Page_Descuentos(tipo, parametro, st, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Obtiene(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEDescuentos tipo = new BEDescuentos();
                    var lista = new BLDescuentos().Obtener(GlobalVars.Global.OWNER, id);

                    if (lista != null)
                    {
                        foreach (var item in lista)
                        {
                            tipo.OWNER = item.OWNER;
                            tipo.DISC_ID = item.DISC_ID;
                            tipo.DISC_TYPE = item.DISC_TYPE;
                            tipo.DISC_NAME = item.DISC_NAME;
                            tipo.DISC_SIGN = item.DISC_SIGN;
                            tipo.DISC_PERC = item.DISC_PERC;
                            tipo.DISC_VALUE = item.DISC_VALUE;
                            tipo.DISC_ACC = item.DISC_ACC;
                            tipo.DISC_AUT = item.DISC_AUT;
                            tipo.LOG_USER_CREAT = item.LOG_USER_CREAT;
                            tipo.TEMP_ID_DSC = item.TEMP_ID_DSC;
                        }
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado el descuento";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtiene Descuentos", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private static void validacion(out bool exito, out string msg_validacion, BEDescuentos entidad)
        {
            exito = true;
            msg_validacion = string.Empty;

            if (exito && string.IsNullOrEmpty(entidad.DISC_NAME))
            {
                msg_validacion = "Ingrese una descripción";
                exito = false;
            }
            if (exito && string.IsNullOrEmpty(entidad.DISC_SIGN))
            {
                msg_validacion = "Ingrese el signo del Dscto";
                exito = false;
            }
        }

        [HttpPost]
        public JsonResult Insertar(BEDescuentos entidad)
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
                        var servicio = new BLDescuentos();

                        if (entidad.DISC_ID == 0)
                        {
                            entidad.LOG_USER_CREAT = UsuarioActual;
                            int id = servicio.Insertar(entidad);
                            retorno.Code = id;
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
                retorno.message = string.Format("{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "inserta Descuentos", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar(decimal codigo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLDescuentos servicio = new BLDescuentos();
                    var dctoEspec = Convert.ToDecimal(System.Web.Configuration.WebConfigurationManager.AppSettings["idTipoEspecialDscto"]);

                    var dscto = servicio.Obtener(GlobalVars.Global.OWNER, codigo);
                    if ((dscto != null && dscto.Count == 1) && dscto[0].DISC_TYPE != dctoEspec)
                    {
                        var result = servicio.Eliminar(new BEDescuentos
                        {
                            OWNER = GlobalVars.Global.OWNER,
                            DISC_ID = codigo,
                            LOG_USER_UPDATE = UsuarioActual,
                        });
                    }

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtiene Descuentos", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertarTarifaDescuento(BETarifaDescuento tarifaDescuento)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    tarifaDescuento.OWNER = GlobalVars.Global.OWNER;
                    tarifaDescuento.LOG_USER_CREAT = UsuarioActual;
                    int id = new BLDescuentos().InsertarTarifaDescuento(tarifaDescuento);
                    retorno.Code = id;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "InsertarTarifaDescuento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region Descuento Socios
        [HttpPost]
        public JsonResult InsertarDescuentoSocio(decimal BPSID, decimal DISCID)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    String usuarioactual = Convert.ToString(Session[Constantes.Sesiones.Usuario]).ToUpper();
                    int id = 0; //new BLDescuentos().InsertarDescuentoSocioBPS(BPSID, DISCID, usuarioactual);
                    retorno.Code = id;
                    retorno.result = 1;
                }

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Insertar Descuentos SOcios", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);


        }

        #endregion

    }
}

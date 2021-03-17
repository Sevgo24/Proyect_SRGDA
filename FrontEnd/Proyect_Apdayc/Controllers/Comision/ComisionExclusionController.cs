using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System.Xml;
using System.Text;
using System.Drawing;
using System.IO;
using System.Net;

namespace Proyect_Apdayc.Controllers.Comision
{
    public class ComisionExclusionController : Base
    {
        //
        // GET: /ComisionExclusion/
        public const string NomAplicacion = "SRGDA";

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

        public JsonResult ListaComisionesExDiv(int skip, int take, int page, int pageSize, string Owner, string Origen, string Sociedad, string Clases, string Grupo, string Derecho, string Incidencia, string Frecuencia, string Repertorio, decimal? Tarifa, decimal TipoComision, decimal OrigenComision, decimal Division, string FechaIni, string FechaFin, int st)
        {
            var lista = Lista(GlobalVars.Global.OWNER, Origen, Sociedad, Clases, Grupo, Derecho, Incidencia, Frecuencia, Repertorio, Tarifa, TipoComision, OrigenComision, Division, Convert.ToDateTime(FechaIni), Convert.ToDateTime(FechaFin), st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEComisionExclusion { ListaComisionEx = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEComisionExclusion { ListaComisionEx = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEComisionExclusion> Lista(string Owner, string Origen, string Sociedad, string Clases, string Grupo, string Derecho, string Incidencia, string Frecuencia, string Repertorio, decimal? Tarifa, decimal TipoComision, decimal OrigenComision, decimal Division, DateTime FechaIni, DateTime FechaFin, int st, int pagina, int cantRegxPag)
        {
            return new BLComisionExclusion().ListarPage(Owner, Origen, Sociedad, Clases, Grupo, Derecho, Incidencia, Frecuencia, Repertorio, Tarifa, TipoComision, OrigenComision, Division, FechaIni, FechaFin, st, pagina, cantRegxPag);
        }

        public JsonResult ObtieneDatosModalidad(decimal id)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var obj = new BLModalidad().ObtenerCodigosDatosModalidad(GlobalVars.Global.OWNER, id);
                    retorno.data = Json(obj, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Obtener datos", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneDatos(decimal id, decimal idDivAdm)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var obj = new BLComisionExclusion().Obtener(GlobalVars.Global.OWNER, id, idDivAdm);
                    retorno.data = Json(obj, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Obtener datos", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost()]
        //public JsonResult Validacion(BEComisionExclusion en)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        BLComisionExclusion servicio = new BLComisionExclusion();
        //        en.OWNER = GlobalVars.Global.OWNER;
        //        int resultado = servicio.ValidacionInsertar(en);
        //        if (resultado >= 1)
        //        {
        //            retorno.result = 1;
        //            retorno.message = "Ya se ha ingresado un producto con la división administrativa seleccionada";
        //        }
        //        else
        //        {
        //            retorno.result = 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.result = 0;
        //        retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
        //        ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Validacion insertar comision", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult ObtieneTarifaTemporalidad(decimal? idModalidad, decimal idTemporalidad)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    if (idModalidad != null)
                    {
                        var obj = new BLComisionAgenteRecaudo().ObtenerTarifaPorTemporalidad(GlobalVars.Global.OWNER, idModalidad, idTemporalidad);

                        if (obj == null)
                        {
                            retorno.result = 0;
                            retorno.message = "No se ha encontrado una tarifa registrada a esta modalidad con esta periodicidad";
                        }
                        else
                        {
                            retorno.data = Json(obj, JsonRequestBehavior.AllowGet);
                            retorno.result = 1;
                        }
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "Seleccione la modalidad";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Obtener datos Tarifa Temporalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insertar(BEComisionExclusion en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLComisionExclusion servicio = new BLComisionExclusion();
                    en.OWNER = GlobalVars.Global.OWNER;
                    int resultado = servicio.ValidacionInsertar(en);

                    BEComisionExclusion obj = new BEComisionExclusion();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.DADV_ID = en.DADV_ID;
                    obj.auxDADV_ID = en.auxDADV_ID;
                    obj.MOD_ID = en.MOD_ID;
                    obj.RAT_FID = en.RAT_FID;
                    obj.COMT_ID = en.COMT_ID;
                    obj.COM_ORG = en.COM_ORG;
                    obj.COM_START = en.COM_START;
                    if (en.Formato == "P")
                        obj.COM_PER = en.COM_VAL;
                    if (en.Formato == "M")
                        obj.COM_VAL = en.COM_VAL;
                    obj.MOD_ID = en.MOD_ID;
                    obj.valgraba = en.valgraba;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    obj.LOG_USER_UPDATE = UsuarioActual;

                    if (obj.valgraba == 0 && resultado == 0)
                    {
                        var datos = new BLComisionExclusion().Insertar(obj);
                    }
                    else
                    {
                        if (obj.DADV_ID == en.auxDADV_ID)
                        {
                            var datos = new BLComisionExclusion().Actualizar(obj);
                        }
                        else
                        {
                            BLComisionExclusion bl = new BLComisionExclusion();
                            en.OWNER = GlobalVars.Global.OWNER;
                            en.DADV_ID = obj.DADV_ID;
                            en.MOD_ID = obj.MOD_ID;
                            int val = bl.ValidacionInsertar(en);

                            if (val >= 1)
                            {
                                retorno.result = 0;
                                retorno.message = "Ya se ha ingresado un producto con la división administrativa seleccionada";
                                return Json(retorno, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                var update = new BLComisionExclusion().Actualizar(obj);
                            }
                        }
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "insert Comision por exclusion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(decimal Id, decimal idDivAdm)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLComisionExclusion servicio = new BLComisionExclusion();
                    var result = servicio.Eliminar(new BEComisionExclusion
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        MOD_ID = Id,
                        DADV_ID = idDivAdm,
                        LOG_USER_UPDATE = UsuarioActual
                    });
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Eliminar Comisión por división administrativa", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

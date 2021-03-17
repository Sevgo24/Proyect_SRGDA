using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using Microsoft.Reporting.WebForms;
using System.Text;

namespace Proyect_Apdayc.Controllers.Comision
{
    public class ComisionAgenteRecaudoController : Base
    {
        //
        // GET: /ComisionAgenteRecaudo/
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

        public JsonResult ListaComisionesAgente(int skip, int take, int page, int pageSize, string Owner, string Origen, string Sociedad, string Clases, string Grupo, string Derecho, string Incidencia, string Frecuencia, string Repertorio, decimal? Tarifa, decimal TipoComision, decimal OrigenComision, decimal Agente, string FechaIni, string FechaFin, int st)
        {
            var lista = Lista(GlobalVars.Global.OWNER, Origen, Sociedad, Clases, Grupo, Derecho, Incidencia, Frecuencia, Repertorio, Tarifa, TipoComision, OrigenComision, Agente, Convert.ToDateTime(FechaIni), Convert.ToDateTime(FechaFin), st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEComisionAgenteRecaudo { ListaComisionAgenteRecaudos = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEComisionAgenteRecaudo { ListaComisionAgenteRecaudos = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEComisionAgenteRecaudo> Lista(string Owner, string Origen, string Sociedad, string Clases, string Grupo, string Derecho, string Incidencia, string Frecuencia, string Repertorio, decimal? Tarifa, decimal TipoComision, decimal OrigenComision, decimal Agente, DateTime FechaIni, DateTime FechaFin, int st, int pagina, int cantRegxPag)
        {
            return new BLComisionAgenteRecaudo().ListarPage(Owner, Origen, Sociedad, Clases, Grupo, Derecho, Incidencia, Frecuencia, Repertorio, Tarifa, TipoComision, OrigenComision, Agente, FechaIni, FechaFin, st, pagina, cantRegxPag);
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

        public JsonResult ObtenerNivelAgente(decimal idAgente)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLComisionAgenteRecaudo servicio = new BLComisionAgenteRecaudo();
                var dato = servicio.ObtenerNivelAgente(GlobalVars.Global.OWNER, idAgente);
                if (dato != null)
                {
                    retorno.data = Json(dato, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "El agente seleccionado no tiene nivel";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Obtener nivel Agente de recaudo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneDatos(decimal id, decimal idAgente)
        {
            Resultado retorno = new Resultado();
                
            try
            {
                if (!isLogout(ref retorno))
                {
                    var obj = new BLComisionAgenteRecaudo().Obtener(GlobalVars.Global.OWNER, id, idAgente);
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

        public JsonResult ObtieneTarifaTemporalidad(decimal? idModalidad, decimal idTemporalidad)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    if (idModalidad != null)
                    {
                        var obj = new BLComisionAgenteRecaudo().ObtenerTarifaPorTemporalidad(GlobalVars.Global.OWNER,idModalidad, idTemporalidad);

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
        public JsonResult Insertar(BEComisionAgenteRecaudo en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLComisionAgenteRecaudo servicio = new BLComisionAgenteRecaudo();
                    en.OWNER = GlobalVars.Global.OWNER;
                    int resultado = servicio.ValidacionInsertar(en);

                    BEComisionAgenteRecaudo obj = new BEComisionAgenteRecaudo();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.BPS_ID = en.BPS_ID;
                    obj.auxBPS_ID = en.auxBPS_ID;
                    obj.MOD_ID = en.MOD_ID;
                    obj.RAT_FID = en.RAT_FID;
                    obj.COMT_ID = en.COMT_ID;
                    obj.LEVEL_ID = en.LEVEL_ID;
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
                        var datos = new BLComisionAgenteRecaudo().Insertar(obj);
                    }
                    else
                    {
                        if (obj.BPS_ID == en.auxBPS_ID)
                        {
                            var datos = new BLComisionAgenteRecaudo().Actualizar(obj);
                        }
                        else
                        {
                            BLComisionAgenteRecaudo bl = new BLComisionAgenteRecaudo();
                            en.OWNER = GlobalVars.Global.OWNER;
                            en.MOD_ID = obj.MOD_ID;
                            en.BPS_ID = obj.BPS_ID;
                            en.RAT_FID = obj.RAT_FID;
                            en.COM_START = obj.COM_START;
                            int val = bl.ValidacionInsertar(en);

                            if (val >= 1)
                            {
                                retorno.result = 0;
                                retorno.message = "Ya se ha ingresado un producto, agente y temporalidad para la fecha de vigencia seleccionada";
                                return Json(retorno, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                var update = new BLComisionAgenteRecaudo().Actualizar(obj);
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "insert Comision por Agente de recaudo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(decimal Id, decimal idAgent)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLComisionAgenteRecaudo servicio = new BLComisionAgenteRecaudo();
                    var result = servicio.Eliminar(new BEComisionAgenteRecaudo
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        MOD_ID = Id,
                        BPS_ID = idAgent,
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Eliminar valor musica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost()]
        //public JsonResult Validacion(BEComisionAgenteRecaudo en)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        BLComisionAgenteRecaudo servicio = new BLComisionAgenteRecaudo();
        //        en.OWNER = GlobalVars.Global.OWNER;
        //        int resultado = servicio.ValidacionInsertar(en);
        //        if (resultado >= 1)
        //        {
        //            retorno.result = 1;
        //            retorno.message = "Ya se ha ingresado un producto, agente y temporalidad para la fecha de vigencia seleccionada";
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
        //        ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Validacion insertar comision producto", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}
    }
}

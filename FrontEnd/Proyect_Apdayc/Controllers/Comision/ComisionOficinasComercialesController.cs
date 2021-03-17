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
    public class ComisionOficinasComercialesController : Base
    {
        //
        // GET: /ComisionOficinasComerciales/
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

        public JsonResult ListaComisionesOficina(int skip, int take, int page, int pageSize, string Owner, string Origen, string Sociedad, string Clases, string Grupo, string Derecho, string Incidencia, string Frecuencia, string Repertorio, decimal TipoComision, decimal OrigenComision, decimal NivelAgente, decimal Oficina, string FechaIni, string FechaFin, int st)
        {
            var lista = Lista(GlobalVars.Global.OWNER, Origen, Sociedad, Clases, Grupo, Derecho, Incidencia, Frecuencia, Repertorio, TipoComision, OrigenComision, NivelAgente, Oficina, Convert.ToDateTime(FechaIni), Convert.ToDateTime(FechaFin), st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEComisionOficinasComerciales { ListaComisionOficina = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEComisionOficinasComerciales { ListaComisionOficina = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEComisionOficinasComerciales> Lista(string Owner, string Origen, string Sociedad, string Clases, string Grupo, string Derecho, string Incidencia, string Frecuencia, string Repertorio, decimal TipoComision, decimal OrigenComision, decimal NivelAgente, decimal Oficina, DateTime FechaIni, DateTime FechaFin, int st, int pagina, int cantRegxPag)
        {
            return new BLComisionOficinasComerciales().ListarPage(Owner, Origen, Sociedad, Clases, Grupo, Derecho, Incidencia, Frecuencia, Repertorio, TipoComision, OrigenComision, NivelAgente, Oficina, FechaIni, FechaFin, st, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Insertar(BEComisionOficinasComerciales en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLComisionOficinasComerciales servicio = new BLComisionOficinasComerciales();
                    en.OWNER = GlobalVars.Global.OWNER;
                    int resultado = servicio.ValidacionInsertar(en);

                    BEComisionOficinasComerciales obj = new BEComisionOficinasComerciales();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.OFF_ID = en.OFF_ID;
                    obj.auxOFF_ID = en.auxOFF_ID;
                    obj.LEVEL_ID = en.LEVEL_ID;
                    obj.auxLEVEL_ID = en.auxLEVEL_ID;
                    obj.MOD_ID = en.MOD_ID;
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
                        var datos = new BLComisionOficinasComerciales().Insertar(obj);
                    }
                    else
                    {
                        if (obj.OFF_ID == en.auxOFF_ID && obj.LEVEL_ID == en.auxLEVEL_ID)
                        {
                            var datos = new BLComisionOficinasComerciales().Actualizar(obj);
                        }
                        else
                        {
                            BLComisionOficinasComerciales bl = new BLComisionOficinasComerciales();
                            en.OWNER = GlobalVars.Global.OWNER; 
                            en.LEVEL_ID = obj.LEVEL_ID;
                            en.OFF_ID = obj.OFF_ID;
                            en.MOD_ID = obj.MOD_ID;
                            int val = bl.ValidacionInsertar(en);

                            if (val >= 1)
                            {
                                retorno.result = 0;
                                retorno.message = "Ya existe registro con la misma oficina y nivel de agente seleccionados";
                                return Json(retorno, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                var update = new BLComisionOficinasComerciales().Actualizar(obj);
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "insert Comision por oficinas de comerciales", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
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

        public JsonResult ObtieneDatos(decimal id, decimal idNivAgent, decimal idOficina)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var obj = new BLComisionOficinasComerciales().Obtener(GlobalVars.Global.OWNER, id, idNivAgent, idOficina);
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
        //public JsonResult Validacion(BEComisionOficinasComerciales en)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        BLComisionOficinasComerciales servicio = new BLComisionOficinasComerciales();
        //        en.OWNER = GlobalVars.Global.OWNER;
        //        int resultado = servicio.ValidacionInsertar(en);
        //        if (resultado >= 1)
        //        {   
        //            retorno.result = 1;
        //            retorno.message = "Ya se ha ingresado una oficina con el nivel de agente seleccionado";
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
        //        ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Validacion insertar comision oficinas comerciales ", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult Eliminar(decimal Id, decimal idNivAgent, decimal idOficina)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLComisionOficinasComerciales servicio = new BLComisionOficinasComerciales();
                    var result = servicio.Eliminar(new BEComisionOficinasComerciales
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        MOD_ID = Id,
                        LEVEL_ID = idNivAgent,
                        OFF_ID = idOficina,
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Eliminar Comisión por oficina", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

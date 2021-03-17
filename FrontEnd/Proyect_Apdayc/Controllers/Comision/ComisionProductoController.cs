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
    public class ComisionProductoController : Base
    {
        //
        // GET: /ComisionProduto/
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

        public JsonResult ListaComisionesProd(int skip, int take, int page, int pageSize, string Owner, string Origen, string Sociedad, string Clases, string Grupo, string Derecho, string Incidencia, string Frecuencia, string Repertorio, decimal? Tarifa, decimal TipoComision, decimal OrigenComision, decimal NivelAgente, string FechaIni, string FechaFin, int st)
        {
            var lista = Lista(GlobalVars.Global.OWNER, Origen, Sociedad, Clases, Grupo, Derecho, Incidencia, Frecuencia, Repertorio, Tarifa, TipoComision, OrigenComision, NivelAgente, Convert.ToDateTime(FechaIni), Convert.ToDateTime(FechaFin), st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEComisionProducto { ListaComisionPro = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEComisionProducto { ListaComisionPro = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEComisionProducto> Lista(string Owner, string Origen, string Sociedad, string Clases, string Grupo, string Derecho, string Incidencia, string Frecuencia, string Repertorio, decimal? Tarifa, decimal TipoComision, decimal OrigenComision, decimal NivelAgente, DateTime FechaIni, DateTime FechaFin, int st, int pagina, int cantRegxPag)
        {
            return new BLComisionProducto().ListarPage(Owner, Origen, Sociedad, Clases, Grupo, Derecho, Incidencia, Frecuencia, Repertorio, Tarifa, TipoComision, OrigenComision, NivelAgente, FechaIni, FechaFin, st, pagina, cantRegxPag);
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

        public JsonResult ObtieneDatos(decimal id, decimal idNivAgent)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var obj = new BLComisionProducto().Obtener(GlobalVars.Global.OWNER, id, idNivAgent);
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
        //public JsonResult Validacion(BEComisionProducto en)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        BLComisionProducto servicio = new BLComisionProducto();
        //        en.OWNER = GlobalVars.Global.OWNER;
        //        int resultado = servicio.ValidacionInsertar(en);
        //        if (resultado == 1)
        //        {
        //            retorno.result = 0;
        //            if (en.valgraba == 1)
        //                retorno.message = "Ya se ha ingresado un producto con el nivel de agente seleccionado";
        //        }
        //        else
        //        {
        //            retorno.result = 1;
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
        public JsonResult Insertar(BEComisionProducto en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLComisionProducto servicio = new BLComisionProducto();
                    en.OWNER = GlobalVars.Global.OWNER;
                    int resultado = servicio.ValidacionInsertar(en);

                    BEComisionProducto obj = new BEComisionProducto();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.LEVEL_ID = en.LEVEL_ID;
                    obj.auxLEVEL_ID = en.auxLEVEL_ID;
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
                        var datos = new BLComisionProducto().Insertar(obj);
                    }
                    else
                    {
                        if (obj.LEVEL_ID == en.auxLEVEL_ID)
                        {
                            var datos = new BLComisionProducto().Actualizar(obj);
                        }
                        else
                        {
                            BLComisionProducto bl = new BLComisionProducto();
                            en.OWNER = GlobalVars.Global.OWNER;
                            en.LEVEL_ID = obj.LEVEL_ID;
                            en.MOD_ID = obj.MOD_ID;
                            int val = bl.ValidacionInsertar(en);

                            if (val >= 1)
                            {
                                retorno.result = 0;
                                retorno.message = "Ya se ha ingresado un producto con el nivel de agente seleccionado";
                                return Json(retorno, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                var update = new BLComisionProducto().Actualizar(obj);
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

        public JsonResult Eliminar(decimal Id, decimal idNivAgent)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLComisionProducto servicio = new BLComisionProducto();
                    var result = servicio.Eliminar(new BEComisionProducto
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        MOD_ID = Id,
                        LEVEL_ID = idNivAgent,
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Eliminar Comisión por producto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

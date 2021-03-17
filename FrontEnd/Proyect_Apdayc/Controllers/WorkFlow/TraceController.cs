using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyect_Apdayc.Clases;
using SGRDA.BL.WorkFlow;
using SGRDA.Entities.WorkFlow;
using SGRDA.Entities;
//using SGRDA.Utility;

namespace Proyect_Apdayc.Controllers.WorkFlow
{
    public class TraceController : Base
    {
        //
        // GET: /Trace/

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public JsonResult InsertarTracesProcesoLic(decimal aidWrkf, decimal idWrkf, decimal sidWrkf, decimal ref1Wrkf, decimal idProc)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    WORKF_TRACES entidad = new WORKF_TRACES();
                    entidad.OWNER = GlobalVars.Global.OWNER;
                    entidad.WRKF_AID = aidWrkf;
                    entidad.WRKF_ID = idWrkf;
                    entidad.WRKF_SID = sidWrkf;
                    entidad.WRKF_REF1 = ref1Wrkf;
                    entidad.PROC_MOD = Constantes.Modulo.LICENCIAMIENTO;
                    entidad.PROC_ID = idProc;
                    entidad.LOG_USER_CREAT = UsuarioActual;
                    decimal salida;
                    var result = new BL_WORKF_TRACES().InsertarTraceLic(entidad, 0, out salida);

                    if (result == 1)
                    {
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    }
                    else if (result == 2)
                    {
                        retorno.result = 0;
                        retorno.message = "No ha cumplido los pre requisitos para cambiar el estado";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "InsertarTracesProceso", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult ListarLogTraces(decimal codigo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    string fecDesde = "January 1, 2015 00:00:00 am GMT+0";
                    string fecHasta = "February 20, 2015 00:00:00 am GMT+0";
                    var datos = new BL_WORKF_TRACES().LogTraces(GlobalVars.Global.OWNER, codigo);
                    if (datos != null && datos.Count>0)
                    {
                        
                         

                         fecDesde = datos[0].startDate;
                         fecHasta = datos[datos.Count - 1].startDate;
                    }
                    retorno.result = 1;
                    retorno.data = Json(new { rangos = datos, desde = fecDesde, hasta = fecHasta }, JsonRequestBehavior.AllowGet);
                }            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarLogTraces", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

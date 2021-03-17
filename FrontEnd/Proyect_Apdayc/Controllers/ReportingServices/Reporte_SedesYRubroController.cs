using Proyect_Apdayc.Clases;
using SGRDA.BL.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Controllers.ReportingServices
{
    public class Reporte_SedesYRubroController : Base
    {
        private const string K_SESSION_LISTA_REPORTING_SEDES_Y_RUBRO = "___K_SESSION_LISTA_REPORTING_SEDES_Y_RUBRO";

        // GET: Reporte_SedesYRubro
        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTING_SEDES_Y_RUBRO);
            Init(false);
            return View();
        }
        public ActionResult RecuperarOficina()
        {
            Resultado retorno = new Resultado();

            string oficina_id = "0";
            //string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            //string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_id = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina_id));
            if (opcAdm == 1)
            {
                oficina_id = "-1";
            }
            else
            {
                oficina_id = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            }
            retorno.result = 1;
            retorno.valor = oficina_id;
            retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}
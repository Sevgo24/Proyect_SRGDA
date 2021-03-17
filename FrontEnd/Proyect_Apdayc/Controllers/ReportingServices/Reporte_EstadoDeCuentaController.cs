using Proyect_Apdayc.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Controllers.ReportingServices
{
    public class Reporte_EstadoDeCuentaController : Base
    {
        // GET: Reporte_EstadoDeCuenta
        private const string K_SESSION_LISTA_REPORTING_DE_ESTADO_CUENTA = "___K_SESSION_LISTA_REPORTE_DE_ESTADO_CUENTA";

        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTING_DE_ESTADO_CUENTA);
            Init(false);
            return View();
        }
    }
}
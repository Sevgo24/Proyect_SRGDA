using Proyect_Apdayc.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Controllers.ReportingServices
{
    public class Reporte_RegistroDeVentasController : Base
    {
        private const string K_SESSION_LISTA_REPORTE_REGISTRO_DE_VENTAS = "___K_SESSION_LISTA_REPORTE_REGISTRO_DE_VENTAS";

        // GET: Reporte_RegistroDeVentas
        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_REGISTRO_DE_VENTAS);
            Init(false);
            return View();
        }
    }
}
using Proyect_Apdayc.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Controllers.ReportingServices
{
    public class Reporte_FacturasPendientesController : Base
    {
        // GET: Reporte_FacturasPendientes
        public ActionResult Index()
        {
            Init(false);
            return View();
        }
    }
}
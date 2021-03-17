using Proyect_Apdayc.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Controllers.ReportingServices
{
    public class Reporte_Diario_de_CajaController : Base
    {
        // GET: Reporte_Diario_de_Caja
        public ActionResult Index()
        {
            Init(false);
            return View();
        }
    }
}
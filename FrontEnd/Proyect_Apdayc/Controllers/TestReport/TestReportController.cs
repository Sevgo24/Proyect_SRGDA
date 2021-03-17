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

namespace Proyect_Apdayc.Controllers.TestReport
{
    public class TestReportController : Controller
    {
        //
        // GET: /TestReport/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ActivaReporte()
        {
            Resultado retorno = new Resultado();
            try
            {
                retorno.valor = "1"; //estado (como ejemplo)
                retorno.result = 1; //id del socio de negocio (como ejemplo)
            }
            catch (Exception)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

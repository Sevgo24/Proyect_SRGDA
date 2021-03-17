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

namespace Proyect_Apdayc.Controllers
{
    public class LicenciamientoController : Controller
    {
        //
        // GET: /Licenciamiento/

        public ActionResult Index()
        {
            return View();
        }

    }
}

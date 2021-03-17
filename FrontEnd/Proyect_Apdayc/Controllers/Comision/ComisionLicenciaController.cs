using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;

namespace Proyect_Apdayc.Controllers.Comision
{
    public class ComisionLicenciaController : Base
    {
        //
        // GET: /ComisionLicencia/

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

    }
}

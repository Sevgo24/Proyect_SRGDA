using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System.Text;
using System.Text.RegularExpressions;

namespace Proyect_Apdayc.Controllers.Consulta
{
    public class ConsultaLicenciaController : Base
    {
        //
        // GET: /ConsultaLicencia/
        public ActionResult Index()
        {
            Init(false);
            return View();
        }


    }
}

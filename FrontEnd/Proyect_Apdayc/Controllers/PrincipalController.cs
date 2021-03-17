using Proyect_Apdayc.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Controllers
{
    public class PrincipalController : Base
    {
        //
        // GET: /Principal/

        public ActionResult Index()
        {
            Init(false);
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Init(false);
            return View();
        }

    }
}

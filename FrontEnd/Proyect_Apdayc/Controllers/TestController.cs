using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases;


namespace Proyect_Apdayc.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            //ListarRutas("apd","urb");
            return View();
        }

        [HttpPost]
        public JsonResult ListarRutas(string owner, string rou_tsel)
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = new BLRutas().Listar_Rutas(owner, rou_tsel)
                 .Select(c => new SelectListItem
                 {
                     Value = Convert.ToString(c.ROU_ID),
                     Text = c.ROU_COD
                 });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                //log de errores
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}

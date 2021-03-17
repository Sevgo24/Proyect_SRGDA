using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;

namespace Proyect_Apdayc.Controllers
{
    public class DetalleGastoController : Base
    {
        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public JsonResult Listar_PageJson_DetalleGasto(int skip, int take, int page, int pageSize, string group, int id)
        {
            Resultado retorno = new Resultado();

            var lista = Listar_Page_DetalleGasto(id, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEDetalleGasto { DetalleGasto = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEDetalleGasto { DetalleGasto = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEDetalleGasto> Listar_Page_DetalleGasto(int id, int pagina, int cantRegxPag)
        {
            return new BLDetalleGasto().Listar_Page_DefinicionGasto(id, pagina, cantRegxPag);
        }

    }
}

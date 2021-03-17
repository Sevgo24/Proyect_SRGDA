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
    public class TASASCAMBIOController : Base
    {
        //
        // GET: /TASASCAMBIO/

        static string idCUR_ALPHA = string.Empty;
        IEnumerable<SelectListItem> items;
        List<REF_CURRENCY_VALUES> lista = new List<REF_CURRENCY_VALUES>();

        public ActionResult Index()
        {
            Init(false);//add sysseg
            ListarAnio();
            listarMoneda();
            listarMeses();
            return View();
        }

        public JsonResult usp_listar_TasaCambioJson(string mes, string anio, string moneda)
        {
            Init();//add sysseg
            idCUR_ALPHA = moneda;
            var lista = TasaCambioListar(moneda, Convert.ToInt32(anio), Convert.ToInt32(mes));
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {

                return Json(new REF_CURRENCY_VALUES { CURRENCYVALUES = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new REF_CURRENCY_VALUES { CURRENCYVALUES = lista, TotalVirtual = 1 }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<REF_CURRENCY_VALUES> TasaCambioListar(string CUR_ALPHA, int YEAR, int MONTH)
        {
            return new BLREF_CURRENCY_VALUES().usp_Get_REF_CURRENCY_VALUES(CUR_ALPHA, YEAR, MONTH);
        }

        private void listarMeses()
        {
            List<SelectListItem> meses = new List<SelectListItem>();
            meses.Add(new SelectListItem { Text = "Enero", Value = "1" });
            meses.Add(new SelectListItem { Text = "Febrero", Value = "2" });
            meses.Add(new SelectListItem { Text = "Marzo", Value = "3" });
            meses.Add(new SelectListItem { Text = "Abril", Value = "4" });
            meses.Add(new SelectListItem { Text = "Mayo", Value = "5" });
            meses.Add(new SelectListItem { Text = "Junio", Value = "6" });
            meses.Add(new SelectListItem { Text = "Julio", Value = "7" });
            meses.Add(new SelectListItem { Text = "Agosto", Value = "8" });
            meses.Add(new SelectListItem { Text = "Setiembre", Value = "9" });
            meses.Add(new SelectListItem { Text = "Octubre", Value = "10" });
            meses.Add(new SelectListItem { Text = "Noviembre", Value = "11" });
            meses.Add(new SelectListItem { Text = "Diciembre", Value = "12" });
            ViewData["Lista_Meses"] = meses;
        }

        private void listarMoneda()
        {
            items = new BLREF_CURRENCY().ListarMoneda()
              .Select(c => new SelectListItem
              {
                  Value = c.CUR_ALPHA.ToString(),
                  Text = c.CUR_DESC
              });
            ViewData["Lista_Moneda"] = items;
        }

        private void ListarAnio()
        {
            var minOffset = 10;
            var maxOffset = 100;
            var list = new List<int>();
            List<SelectListItem> años = new List<SelectListItem>();
            int year = 0;

            year = Convert.ToInt32(DateTime.Now.AddYears(20).Year);

            for (int i = minOffset; i < maxOffset; i++)
            {
                year -= 1;
                list.Add(year);
            }

            var selectItems = from item in list
                              select new SelectListItem
                              {
                                  Text = item.ToString()
                              };

            ViewData["Lista_años"] = selectItems;
        }

        public ActionResult Edit(string id = "")
        {
            Init(false);//add sysseg
            REF_CURRENCY_VALUES currencyValues = new REF_CURRENCY_VALUES();
            lista = new BLREF_CURRENCY_VALUES().usp_REF_CURRENCY_VALUES_GET(Convert.ToDateTime(id));

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    currencyValues.CUR_ALPHA = item.CUR_ALPHA;
                    currencyValues.CUR_DATE = item.CUR_DATE;
                    currencyValues.CUR_VALUE = item.CUR_VALUE;
                }

                if (lista.Count() == 0)
                    currencyValues.CUR_DATE = Convert.ToDateTime(id);
            }
            return View(currencyValues);
        }

        [HttpPost]
        public ActionResult Edit(REF_CURRENCY_VALUES en)
        {
            var valid = ModelState.IsValid;
            bool accionupdate = true;
            if (en.CUR_ALPHA == null)
                accionupdate = false;
            bool std = true;

            if (valid == true)
            {
                if (accionupdate)
                {
                    en.LOG_USER_UPDATE = UsuarioActual;
                    std = new BLREF_CURRENCY_VALUES().usp_Upd_REF_CURRENCY_VALUES(en);
                }
                else
                {
                    en.LOG_USER_CREAT = UsuarioActual;
                    en.CUR_ALPHA = idCUR_ALPHA;
                    std = new BLREF_CURRENCY_VALUES().insertar(en);
                }

                if (std)
                {
                    TempData["msg"] = "Actualizado Correctamente";
                    TempData["class"] = "alert alert-success";
                }
                else
                {
                    TempData["msg"] = "Ocurrio un inconveniente, no se pudo Actualizar";
                    TempData["class"] = "alert alert-danger";
                }
            }
            else
                TempData["class1"] = "alert alert-danger";

            return RedirectToAction("Edit");
        }
    }
}

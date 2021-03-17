using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data;
using Proyect_Apdayc.Clases;

namespace Proyect_Apdayc.Controllers
{
    public class MONEDAController : Base
    {
        //
        // GET: /MONEDA/
        DataSet ds = new DataSet();

        public ActionResult Index()
        {
            Init(false);//add sysseg
            return View();
        }

        public List<BEREF_CURRENCY> usp_listar_Moneda()
        {
            return new BLREF_CURRENCY().ListarMoneda();
        }

        [HttpPost()]
        public JsonResult usp_listar_MonedaJson(int? skip, int? take, int? page, int? pageSize, string group, string dato, int st)
        {
            Init();//add sysseg
            List<BEREF_CURRENCY> item = new List<BEREF_CURRENCY>();
            List<BEREF_CURRENCY> lista = new List<BEREF_CURRENCY>();
            BLREF_CURRENCY bl = new BLREF_CURRENCY();
            item = null;
            item = bl.REF_CURRENCY_Page(dato, st, page, pageSize);
            lista = item;            
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEREF_CURRENCY { REFCURRENCY = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREF_CURRENCY { REFCURRENCY = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }           
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BEREF_CURRENCY en)
        {
            var valid = ModelState.IsValid;
            en.LOG_USER_CREAT = UsuarioActual;

            if (valid == true)
            {
                bool std = new BLREF_CURRENCY().REF_CURRENCY_Ins(en);

                if (std)
                {
                    TempData["msg"] = "Registrado Correctamente";
                    TempData["class"] = "alert alert-success";
                    TempData["flag"] = 1;
                }
                else
                {
                    TempData["msg"] = "Ocurrio un inconveniente, no se pudo Registrar";
                    TempData["class"] = "alert alert-danger";
                    TempData["flag"] = 0;
                }
            }
            else
            {
                TempData["class1"] = "alert alert-danger";
                TempData["flag"] = 0;
            }
            return View();
        }

        public ActionResult Edit(string id = "")
        {
            Init(false);//add sysseg

            BEREF_CURRENCY moneda = new BEREF_CURRENCY();
            var lista = new BLREF_CURRENCY().REF_CURRENCY_by_CUR_ALPHA(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    moneda.CUR_ALPHA = item.CUR_ALPHA;
                    moneda.CUR_DESC = item.CUR_DESC;
                    moneda.CUR_NUM = item.CUR_NUM;
                    moneda.UNIT_MAJOR = item.UNIT_MAJOR;
                    moneda.UNIT_MINOR = item.UNIT_MINOR;
                    moneda.DECIMALS = item.DECIMALS;
                    moneda.FORMAT = item.FORMAT;
                }
            }
            return View(moneda);
        }

        [HttpPost]
        public ActionResult Edit(BEREF_CURRENCY en)
        {
            var valid = ModelState.IsValid;
            en.LOG_USER_UPDATE = UsuarioActual;

            if (valid == true)
            {
                bool std = new BLREF_CURRENCY().REF_CURRENCY_Upd(en);

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

        [HttpPost]
        public ActionResult Eliminar(List<BEREF_CURRENCY> dato)
        {
            Init(false);//add sysseg
            foreach (var item in dato)
            {
                var moneda = new BEREF_CURRENCY()
                {
                    CUR_ALPHA = item.CUR_ALPHA
                };
                bool std = new BLREF_CURRENCY().REF_CURRENCY_Del(item.CUR_ALPHA);
            }
            return RedirectToAction("Index");
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REF_CURRENCY.rdlc");

            List<BEREF_CURRENCY> lista = new List<BEREF_CURRENCY>();
            lista = usp_listar_Moneda();

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            /*if (territory != null)
            {
                var customerfilterList = from c in customerList
                                         where c.Territory == territory
                                         select c;


                reportDataSource.Value = customerfilterList;
            }
            else*/
            reportDataSource.Value = lista;

            localReport.DataSources.Add(reportDataSource);
            string reportType = "Image";
            string mimeType;
            string encoding;
            string fileNameExtension;
            //The DeviceInfo settings should be changed based on the reportType            
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>jpeg</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report            
            renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension); 
            if (format == null)
            {
                return File(renderedBytes, "image/jpeg");
            }
            else if (format == "PDF")
            {
                return File(renderedBytes, "pdf");
            }
            else
            {
                return File(renderedBytes, "image/jpeg");
            }
        }

        public ActionResult DownloadReport(string format)
        {
            Init(false);//add sysseg

            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REF_CURRENCY.rdlc");

            List<BEREF_CURRENCY> lista = new List<BEREF_CURRENCY>();
            lista = usp_listar_Moneda();

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            reportDataSource.Value = lista;
            localReport.DataSources.Add(reportDataSource);

            ReportParameter parametro = new ReportParameter();
            parametro = new ReportParameter("Usuario", UsuarioActual.Trim());
            localReport.SetParameters(parametro);

            string reportType = format;
            string mimeType;
            string encoding;
            string fileNameExtension;

            //The DeviceInfo settings should be changed based on the reportType            
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + format + "</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report            
            renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension); 
            if (format == null)
            {
                return File(renderedBytes, "image/jpeg");
            }
            else if (format == "PDF")
            {
                return File(renderedBytes, mimeType);
            }
            else if (format == "EXCEL")
            {
                return File(renderedBytes, mimeType);
            }
            else
            {
                return File(renderedBytes, "image/jpeg");
            }
        }
    }
}

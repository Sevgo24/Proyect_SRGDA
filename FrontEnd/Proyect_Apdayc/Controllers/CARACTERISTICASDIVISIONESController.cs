using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using Proyect_Apdayc.Clases;

namespace Proyect_Apdayc.Controllers
{
    public class CARACTERISTICASDIVISIONESController : Base
    {
        //
        // GET: /CARACTERISTICASDIVISIONES/

        public ActionResult Index()
        {
            Init(false);//add sysseg
            //var lista = TipoCaracDivisionesListarPag("", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<BEREF_DIV_CHARAC> usp_listar_TipoCaracDivisiones()
        {
            return new BLREF_DIV_CHARAC().usp_Get_REF_DIV_CHARAC();
        }

        public JsonResult usp_listar_TipoCaracDivisionesJson(int skip, int take, int page, int pageSize, string group, string dato, int st)
        {
            Init();//add sysseg
            var lista = TipoCaracDivisionesListarPag(dato, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEREF_DIV_CHARAC { REFDIVCHARAC = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREF_DIV_CHARAC { REFDIVCHARAC = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREF_DIV_CHARAC> TipoCaracDivisionesListarPag(string dato, int st, int pagina, int cantRegxpag)
        {
            return new BLREF_DIV_CHARAC().usp_REF_DIV_CHARAC_Page(dato, st, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BEREF_DIV_CHARAC en)
        {
            var valid = ModelState.IsValid;
            en.OWNER =GlobalVars.Global.OWNER;
            en.LOG_USER_CREAT = UsuarioActual;           

            if (valid == true)
            {
                bool std = new BLREF_DIV_CHARAC().REF_DIV_CHARAC_Ins(en);

                if (std)
                {
                    TempData["msg"] = "Registrado Correctamente";
                    TempData["class"] = "alert alert-success";
                    TempData["flag"] = 1;
                }
                else
                {
                    TempData["flag"] = 0;
                    TempData["msg"] = "Ocurrio un inconveniente, no se pudo Registrar";
                    TempData["class"] = "alert alert-danger";
                }
            }
            else
            {
                TempData["class1"] = "alert alert-danger";
            }
            return View();
        }

        public ActionResult Edit(string id = "")
        {
            Init(false);//add sysseg

            BEREF_DIV_CHARAC CaracTipodiv = new BEREF_DIV_CHARAC();
            var lista = new BLREF_DIV_CHARAC().usp_Get_REF_DIV_TYPE_by_DAC_ID(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    CaracTipodiv.OWNER = item.OWNER;
                    CaracTipodiv.DAC_ID = item.DAC_ID;
                    CaracTipodiv.DESCRIPTION = item.DESCRIPTION;
                }
            }
            return View(CaracTipodiv);
        }

        [HttpPost]
        public ActionResult Edit(BEREF_DIV_CHARAC en)
        {
            var valid = ModelState.IsValid;
            en.LOG_USER_UPDATE = UsuarioActual;

            if (valid == true)
            {
                bool std = new BLREF_DIV_CHARAC().REF_DIV_CHARAC_Upd(en);

                if (std)
                {
                    TempData["msg"] = "Actualizado Correctamente";
                    TempData["class"] = "alert alert-success";
                    TempData["flag"] = 1;
                }
                else
                {
                    TempData["flag"] = 0;
                    TempData["msg"] = "Ocurrio un inconveniente, no se pudo Actualizar";
                    TempData["class"] = "alert alert-danger";
                }
            }
            else
                TempData["class1"] = "alert alert-danger";

            return RedirectToAction("Edit");
        }

        [HttpPost]
        public ActionResult Eliminar(List<BEREF_DIV_CHARAC> dato)
        {
            Init(false);//add sysseg

            foreach (var item in dato)
            {
                var TipoDiv = new BEREF_DIV_CHARAC()
                {
                    DAC_ID = item.DAC_ID
                };
                bool std = new BLREF_DIV_CHARAC().REF_DIV_CHARAC_Del(item.DAC_ID);
            }
            return RedirectToAction("Index");
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REF_DIV_CHARAC.rdlc");

            List<BEREF_DIV_CHARAC> lista = new List<BEREF_DIV_CHARAC>();
            lista = usp_listar_TipoCaracDivisiones();

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REF_DIV_CHARAC.rdlc");

            List<BEREF_DIV_CHARAC> lista = new List<BEREF_DIV_CHARAC>();
            lista = usp_listar_TipoCaracDivisiones();

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

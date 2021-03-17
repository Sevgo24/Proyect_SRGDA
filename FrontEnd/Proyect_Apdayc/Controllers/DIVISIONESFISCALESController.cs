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
    public class DIVISIONESFISCALESController : Base
    {
        //
        // GET: /DIVISIONESFISCALES/

        public ActionResult Index()
        {
            Init(false);//add sysseg
            //var lista = DivisionesFiscalesListarPag("", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<BEREF_TAX_DIVISION> usp_listar_DivisionesFiscales(string owner)
        {
            return new BLREF_TAX_DIVISION().Get_REF_TAX_DIVISION(owner,604);
        }

        public JsonResult usp_listar_DivisionesFiscalesJson(int skip, int take, int page, int pageSize, string group, string dato, int st)
        {
            Init();//add sysseg
            var lista = DivisionesFiscalesListarPag(dato, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEREF_TAX_DIVISION { RECTAXDIVISION = lista, TotalVirtual = 0}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREF_TAX_DIVISION { RECTAXDIVISION = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
            
        }

        public List<BEREF_TAX_DIVISION> DivisionesFiscalesListarPag(string dato, int st, int pagina, int cantRegxpag)
        {
            return new BLREF_TAX_DIVISION().REF_TAX_DIVISION_Page(dato, st, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            listaTipoTerritorio();
            return View();
        }

        //REF_DIV_TYPE
        IEnumerable<SelectListItem> lista1;
        private void listaTipoTerritorio(decimal idTerritorio = 604)
        {
            lista1 = new BLTerritorio().Listar_Territorio()
            .Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.TIS_N),
                Text = c.NAME_TER
            });
            ViewData["Lista_TipoTerritorio"] = lista1;
            ViewData["Lista_TipoTerritorio"] = new SelectList(lista1, "Value", "Text", idTerritorio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BEREF_TAX_DIVISION en,FormCollection frm)
        {
            var valid = ModelState.IsValid;
            decimal territorio = Convert.ToDecimal(frm["Lista_TipoTerritorio"]);

            en.LOG_USER_CREAT = UsuarioActual;
            en.TIS_N = territorio;

            if (valid == true)
            {
                bool std = new BLREF_TAX_DIVISION().REF_TAX_DIVISION_Ins(en);

                if (std)
                {
                    TempData["msg"] = "Registrado Correctamente";
                    TempData["class"] = "alert alert-success";
                }
                else
                {
                    TempData["msg"] = "Ocurrio un inconveniente, no se pudo Registrar";
                    TempData["class"] = "alert alert-danger";
                }
            }
            else
            {
                TempData["class1"] = "alert alert-danger";
            }
            listaTipoTerritorio(territorio);
            return View();
        }

        public ActionResult Edit(decimal id = 0)
        {
            Init(false);//add sysseg
            BEREF_TAX_DIVISION TipoDirecciones = new BEREF_TAX_DIVISION();
            var lista = new BLREF_TAX_DIVISION().REF_TAX_DIVISION_GET_by_TAXD_ID(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    TipoDirecciones.OWNER = item.OWNER;
                    TipoDirecciones.TAXD_ID = item.TAXD_ID;
                    TipoDirecciones.TIS_N = item.TIS_N;
                    TipoDirecciones.DESCRIPTION = item.DESCRIPTION;
                }
            }
            decimal territorio = Convert.ToDecimal(TipoDirecciones.TIS_N);
            listaTipoTerritorio(territorio);
            return View(TipoDirecciones);
        }

        [HttpPost]
        public ActionResult Edit(BEREF_TAX_DIVISION en, FormCollection frm)
        {
            var valid = ModelState.IsValid;
            en.LOG_USER_UPDATE = UsuarioActual;

            decimal territorio = Convert.ToDecimal(frm["Lista_TipoTerritorio"]);
            en.TIS_N = territorio;

            if (valid == true)
            {
                bool std = new BLREF_TAX_DIVISION().REF_TAX_DIVISION_Upd(en);

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


            listaTipoTerritorio(territorio);
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public ActionResult Eliminar(List<BEREF_TAX_DIVISION> dato)
        {
            Init(false);//add sysseg
            foreach (var item in dato)
            {
                var TipoDirecciones = new BEREF_TAX_DIVISION()
                {
                    TAXD_ID = item.TAXD_ID
                };
                bool std = new BLREF_TAX_DIVISION().REF_TAX_DIVISION_Del(item.TAXD_ID);
            }
            return RedirectToAction("Index");
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REF_TAX_DIVISION.rdlc");

            List<BEREF_TAX_DIVISION> lista = new List<BEREF_TAX_DIVISION>();
            lista = usp_listar_DivisionesFiscales(GlobalVars.Global.OWNER);

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REF_TAX_DIVISION.rdlc");

            List<BEREF_TAX_DIVISION> lista = new List<BEREF_TAX_DIVISION>();
            lista = usp_listar_DivisionesFiscales(GlobalVars.Global.OWNER);

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

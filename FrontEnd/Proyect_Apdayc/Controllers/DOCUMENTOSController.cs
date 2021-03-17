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
    public class DOCUMENTOSController : Base
    {
        //
        // GET: /DOCUMENTOS/
        public static string cod = "";
        public static string Tipodocumentos = "";

        public ActionResult Index()
        {
            Init(false);//add sysseg
            var lista = documentosListarPag("", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<BEREC_DOCUMENTS_GRAL> usp_listar_Tipodocumentos()
        {
            return new BLREC_DOCUMENTS_GRAL().Get_REC_DOCUMENTS_GRAL();
        }

        public JsonResult usp_listar_documentosJson(int skip, int take, int page, int pageSize, string group, string dato)
        {
            Init();//add sysseg
            var lista = documentosListarPag(dato, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            return Json(new BEREC_DOCUMENTS_GRAL { RECDOCUMENTSGRAL = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
        }

        public List<BEREC_DOCUMENTS_GRAL> documentosListarPag(string dato, int pagina, int cantRegxpag)
        {
            return new BLREC_DOCUMENTS_GRAL().REC_DOCUMENTS_GRAL_Page(dato, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            listatipodocumentos();
            ViewData["listatipodocumentos"] = Tipodocumentos;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            BEREC_DOCUMENTS_GRAL en = new BEREC_DOCUMENTS_GRAL();
            var valid = ModelState.IsValid;
            en.LOG_USER_CREAT = UsuarioActual;
            en.DOC_TYPE = Convert.ToDecimal(frm["Lista_Tipodocumentos"]);
            en.DOC_DATE = Convert.ToDateTime(frm["DOC_DATE"]);
            en.DOC_VERSION = Convert.ToDecimal(frm["DOC_VERSION"]);
            en.DOC_USER = frm["DOC_USER"];
            en.DOC_PATH = frm["DOC_PATH"];

            if (valid == true)
            {
                bool std = new BLREC_DOCUMENTS_GRAL().REC_DOCUMENTS_GRAL_Ins(en);

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

            listatipodocumentos();
            return View();
        }

        public ActionResult Edit(string id = "")
        {
            Init(false);//add sysseg
            BEREC_DOCUMENTS_GRAL documentos = new BEREC_DOCUMENTS_GRAL();
            var lista = new BLREC_DOCUMENTS_GRAL().REC_DOCUMENTS_GRAL_GET_by_DOC_ID(id.Split(',')[0], Convert.ToDecimal(id.Split(',')[1]));

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    documentos.OWNER = item.OWNER;
                    documentos.DOC_ID = item.DOC_ID;
                    documentos.DOC_TYPE = item.DOC_TYPE;
                    documentos.DOC_DESC = item.DOC_DESC;
                    documentos.DOC_DATE = item.DOC_DATE;
                    documentos.DOC_VERSION = item.DOC_VERSION;
                    documentos.DOC_USER = item.DOC_USER;
                    documentos.DOC_PATH = item.DOC_PATH;
                    listatipodocumentos();
                }
            }
            return View(documentos);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm)
        {
            BEREC_DOCUMENTS_GRAL en = new BEREC_DOCUMENTS_GRAL();
            var valid = ModelState.IsValid;
            en.LOG_USER_UPDATE = UsuarioActual;
            en.DOC_TYPE = Convert.ToDecimal(frm["Lista_Tipodocumentos"]);
            en.DOC_DATE = Convert.ToDateTime(frm["DOC_DATE"]);
            en.DOC_VERSION = Convert.ToDecimal(frm["DOC_VERSION"]);
            en.DOC_USER = frm["DOC_USER"];
            en.DOC_PATH = frm["DOC_PATH"];
            en.DOC_ID = Convert.ToDecimal(frm["DOC_ID"]);

            if (valid == true)
            {
                bool std = new BLREC_DOCUMENTS_GRAL().REC_DOCUMENTS_GRAL_Upd(en);

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
        public ActionResult Eliminar(List<BEREC_DOCUMENTS_GRAL> dato)
        {
            Init(false);//add sysseg
            foreach (var item in dato)
            {               
                bool std = new BLREC_DOCUMENTS_GRAL().REC_DOCUMENTS_GRAL_Del( GlobalVars.Global.OWNER, Convert.ToDecimal(item.DOC_ID.ToString()));
            }
            return RedirectToAction("Index");
        }

        IEnumerable<SelectListItem> lista1;
        private void listatipodocumentos()
        {
            lista1 = new BLREC_DOCUMENT_TYPE().REC_DOCUMENT_TYPE_GET()
            .Select(c => new SelectListItem
            {
                Value = c.DOC_TYPE.ToString(),
                Text = c.DOC_DESC
            });
            ViewData["Lista_Tipodocumentos"] = lista1;
            ViewData["Lista_Tipodocumentos"] = new SelectList(lista1, "Value", "Text", cod);
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_DOCUMENTS_GRAL.rdlc");

            List<BEREC_DOCUMENTS_GRAL> lista = new List<BEREC_DOCUMENTS_GRAL>();
            lista = usp_listar_Tipodocumentos();

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_DOCUMENTS_GRAL.rdlc");

            List<BEREC_DOCUMENTS_GRAL> lista = new List<BEREC_DOCUMENTS_GRAL>();
            lista = usp_listar_Tipodocumentos();

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

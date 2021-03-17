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
    public class CALIFICADORESSOCIOSNEGOCIOSController : Base
    {
        //
        // GET: /CALIFICADORESSOCIOSNEGOCIOS/
        public static string cod = "";
        public static string Tiposcalificadores = "";

        public ActionResult Index()
        {
            Init(false);//add sysseg
            var lista = calificadoressociosnegocioListarPag("", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<BEREC_BPS_QUALY> usp_listar_calificadoressociosnegocio()
        {
            return new BLREC_BPS_QUALY().Get_REC_BPS_QUALY();
        }

        public JsonResult usp_listar_calificadoressociosnegocioJson(int skip, int take, int page, int pageSize, string group, string dato)
        {
            Init();//add sysseg
            var lista = calificadoressociosnegocioListarPag(dato, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            return Json(new BEREC_BPS_QUALY { RECBPSQUALY = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
        }

        public List<BEREC_BPS_QUALY> calificadoressociosnegocioListarPag(string dato, int pagina, int cantRegxpag)
        {
            return new BLREC_BPS_QUALY().REC_BPS_QUALY_Page(dato, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            listaTiposCalificadores();
            ViewData["ListaTiposCalificadores"] = Tiposcalificadores;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            BEREC_BPS_QUALY en = new BEREC_BPS_QUALY();
            var valid = ModelState.IsValid;
            en.LOG_USER_CREAT = "USERCREAT";
            en.QUC_ID = Convert.ToDecimal(frm["Lista_TiposCalificadores"]);
            en.BPS_ID = Convert.ToDecimal(frm["BPS_ID"]);

            if (valid == true)
            {
                bool std = new BLREC_BPS_QUALY().REC_BPS_QUALY_Ins(en);

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

            listaTiposCalificadores();
            return View();
        }

        public ActionResult Edit(string id = "")
        {
            Init(false);//add sysseg
            BEREC_BPS_QUALY localidades = new BEREC_BPS_QUALY();
            var lista = new BLREC_BPS_QUALY().REC_BPS_QUALY_GET_by_BPS_ID(id.Split(',')[0], Convert.ToDecimal(id.Split(',')[1]));

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    localidades.OWNER = item.OWNER;
                    localidades.BPS_ID = item.BPS_ID;
                    localidades.QUC_ID = item.QUC_ID;
                    localidades.CARACTERISTICA = item.CARACTERISTICA;
                    listaTiposCalificadores();
                }
            }
            return View(localidades);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm)
        {
            BEREC_BPS_QUALY en = new BEREC_BPS_QUALY();
            var valid = ModelState.IsValid;
            en.LOG_USER_UPDAT = "USERCREAT";
            en.QUC_ID = Convert.ToDecimal(frm["Lista_TiposCalificadores"]);
            en.BPS_ID = Convert.ToDecimal(frm["BPS_ID"]);

            if (valid == true)
            {
                bool std = new BLREC_BPS_QUALY().REC_BPS_QUALY_Upd(en);

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
        public ActionResult Eliminar(List<BEREC_BPS_QUALY> dato)
        {
            Init(false);//add sysseg

            foreach (var item in dato)
            {
                bool std = new BLREC_BPS_QUALY().REC_BPS_QUALY_Del( GlobalVars.Global.OWNER, Convert.ToDecimal(item.BPS_ID.ToString()));
            }
            return RedirectToAction("Index");
        }

        IEnumerable<SelectListItem> lista1;
        private void listaTiposCalificadores()
        {
            lista1 = new BLREC_QUALIFY_CHAR().Get_REC_QUALIFY_CHAR()
            .Select(c => new SelectListItem
            {
                Value = c.QUC_ID.ToString(),
                Text = c.DESCRIPTION
            });
            ViewData["Lista_TiposCalificadores"] = lista1;
            ViewData["Lista_TiposCalificadores"] = new SelectList(lista1, "Value", "Text", cod);
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_BPS_QUALY.rdlc");

            List<BEREC_BPS_QUALY> lista = new List<BEREC_BPS_QUALY>();
            lista = usp_listar_calificadoressociosnegocio();

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_BPS_QUALY.rdlc");

            List<BEREC_BPS_QUALY> lista = new List<BEREC_BPS_QUALY>();
            lista = usp_listar_calificadoressociosnegocio();

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

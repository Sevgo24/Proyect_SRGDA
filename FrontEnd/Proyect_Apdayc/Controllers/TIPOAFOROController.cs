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
    public class TIPOAFOROController : Base
    {
        //
        // GET: /TIPOAFORO/

        public ActionResult Index()
        {
            Init(false);//add sysseg
            var lista = tipoaforoListarPag("", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<BEREC_CAPACITY_TYPE> usp_listar_tipoaforo()
        {
            return new BLREC_CAPACITY_TYPE().Get_REC_CAPACITY_TYPE();
        }

        public JsonResult usp_listar_TipoaforoJson(int skip, int take, int page, int pageSize, string group, string dato)
        {
            Init();//add sysseg
            var lista = tipoaforoListarPag(dato, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            return Json(new BEREC_CAPACITY_TYPE { RECCAPACITYTYPE = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
        }

        public List<BEREC_CAPACITY_TYPE> tipoaforoListarPag(string dato, int pagina, int cantRegxpag)
        {
            return new BLREC_CAPACITY_TYPE().REC_CAPACITY_TYPE_Page(dato, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BEREC_CAPACITY_TYPE en)
        {
            var valid = ModelState.IsValid;
            en.LOG_USER_CREAT = UsuarioActual;

            if (valid == true)
            {
                bool std = new BLREC_CAPACITY_TYPE().REC_CAPACITY_TYPE_Ins(en);

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
            return View();
        }

        public ActionResult Edit(string id = "")
        {
            Init(false);//add sysseg
            BEREC_CAPACITY_TYPE tipoaforo = new BEREC_CAPACITY_TYPE();
            var lista = new BLREC_CAPACITY_TYPE().REC_CAPACITY_TYPE_GET_by_CAP_ID(id.Split(',')[1]);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    tipoaforo.OWNER = item.OWNER;
                    tipoaforo.CAP_ID = item.CAP_ID;
                    tipoaforo.CAP_DESC = item.CAP_DESC;
                }
            }
            return View(tipoaforo);
        }

        [HttpPost]
        public ActionResult Edit(BEREC_CAPACITY_TYPE en)
        {
            var valid = ModelState.IsValid;
            en.LOG_USER_UPDAT = UsuarioActual;

            if (valid == true)
            {
                bool std = new BLREC_CAPACITY_TYPE().REC_CAPACITY_TYPE_Upd(en);

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
        public ActionResult Eliminar(List<BEREC_CAPACITY_TYPE> dato)
        {
            Init(false);//add sysseg
            foreach (var item in dato)
            {
                var Tipoaforo = new BEREC_CAPACITY_TYPE()
                {
                    CAP_ID = item.CAP_ID
                };
                bool std = new BLREC_CAPACITY_TYPE().REC_CAPACITY_TYPE_Del(item.CAP_ID);
            }
            return RedirectToAction("Index");
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_CAPACITY_TYPE.rdlc");

            List<BEREC_CAPACITY_TYPE> lista = new List<BEREC_CAPACITY_TYPE>();
            lista = usp_listar_tipoaforo();

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_CAPACITY_TYPE.rdlc");

            List<BEREC_CAPACITY_TYPE> lista = new List<BEREC_CAPACITY_TYPE>();
            lista = usp_listar_tipoaforo();

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

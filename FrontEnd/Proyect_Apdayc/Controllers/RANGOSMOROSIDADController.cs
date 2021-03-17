using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;

namespace Proyect_Apdayc.Controllers
{
    public class RANGOSMOROSIDADController : Base
    {
        //
        // GET: /RANGOSMOROSIDAD/

        public ActionResult Index()
        {
            Init(false);//add sysseg
            var lista = identificadoresRangosmorosidadListarPag("", 0, 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<BEREC_DEBTORS_RANGE> usp_listar_rangosmorosidad()
        {
            return new BLREC_DEBTORS_RANGE().Get_REC_DEBTORS_RANGE();
        }

        public JsonResult usp_listar_rangosmorosidadJson(int skip, int take, int page, int pageSize, string group, string dato, int st)
        {
            //var lista = identificadoresRangosmorosidadListarPag(dato, page, pageSize);
            //var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            //return Json(new BEREC_DEBTORS_RANGE { RECDEBTORSRANGE = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);

            Init();//add sysseg
            Resultado retorno = new Resultado();

            var lista = identificadoresRangosmorosidadListarPag(dato, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEREC_DEBTORS_RANGE { RECDEBTORSRANGE = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_DEBTORS_RANGE { RECDEBTORSRANGE = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            } 

        }

        public List<BEREC_DEBTORS_RANGE> identificadoresRangosmorosidadListarPag(string dato, int st, int pagina, int cantRegxpag)
        {
            return new BLREC_DEBTORS_RANGE().REC_DEBTORS_RANGE_Page(dato, st, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BEREC_DEBTORS_RANGE en)
        {
            var valid = ModelState.IsValid;
            en.LOG_USER_CREAT = UsuarioActual;

            if (valid == true)
            {
                bool std = new BLREC_DEBTORS_RANGE().REC_DEBTORS_RANGE_Ins(en);

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
            BEREC_DEBTORS_RANGE rangomorosidad = new BEREC_DEBTORS_RANGE();
            var lista = new BLREC_DEBTORS_RANGE().REC_DEBTORS_RANGE_GET_by_RANGE_COD(id.Split(',')[0], Convert.ToDecimal(id.Split(',')[1]));

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    rangomorosidad.OWNER = item.OWNER;
                    rangomorosidad.RANGE_COD = item.RANGE_COD;
                    rangomorosidad.DESCRIPTION = item.DESCRIPTION;
                    rangomorosidad.RANGE_FROM = item.RANGE_FROM;
                    rangomorosidad.RANGE_TO = item.RANGE_TO;
                }
            }
            return View(rangomorosidad);
        }

        [HttpPost]
        public ActionResult Edit(BEREC_DEBTORS_RANGE en)
        {
            var valid = ModelState.IsValid;
            en.LOG_USER_UPDATE = UsuarioActual;

            if (valid == true)
            {
                bool std = new BLREC_DEBTORS_RANGE().REC_DEBTORS_RANGE_Upd(en);

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
        public ActionResult Eliminar(List<BEREC_DEBTORS_RANGE> dato)
        {
            Init(false);//add sysseg
            foreach (var item in dato)
            {
                bool std = new BLREC_DEBTORS_RANGE().REC_DEBTORS_RANGE_Del(GlobalVars.Global.OWNER, Convert.ToDecimal(item.RANGE_COD) );
            }
            return RedirectToAction("Index");
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_DEBTORS_RANGE.rdlc");

            List<BEREC_DEBTORS_RANGE> lista = new List<BEREC_DEBTORS_RANGE>();
            lista = usp_listar_rangosmorosidad();

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_DEBTORS_RANGE.rdlc");

            List<BEREC_DEBTORS_RANGE> lista = new List<BEREC_DEBTORS_RANGE>();
            lista = usp_listar_rangosmorosidad();

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

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
    public class TIPOSACTUACIONESLITIGIOController : Base
    {
        //
        // GET: /TIPOSACTUACIONESLITIGIO/

        public ActionResult Index()
        {
            Init(false);//add sysseg
            return View();
        }

        public List<BEREC_LAWSUITE_ACTIVITIES_TYPE> usp_listar_tipoactuacionesLitigio()
        {
            return new BLREC_LAWSUITE_ACTIVITIES_TYPE().Get_REC_LAWSUITE_ACTIVITIES_TYPE();
        }

        public JsonResult usp_listar_tipoactuacionesLitigioJson(int skip, int take, int page, int pageSize, string group, string dato, int st)
        {
            Init();//add sysseg
            var lista = tipoactuacionesLitiListarPag(dato, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEREC_LAWSUITE_ACTIVITIES_TYPE { RECLAWSUITEACTIVITIESTYPE = lista, TotalVirtual = 0}, JsonRequestBehavior.AllowGet);
            }
            else
            {                
                return Json(new BEREC_LAWSUITE_ACTIVITIES_TYPE { RECLAWSUITEACTIVITIESTYPE = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }

        }

        public List<BEREC_LAWSUITE_ACTIVITIES_TYPE> tipoactuacionesLitiListarPag(string dato, int st, int pagina, int cantRegxpag)
        {
            return new BLREC_LAWSUITE_ACTIVITIES_TYPE().REC_LAWSUITE_ACTIVITIES_TYPE_Page(dato, st, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BEREC_LAWSUITE_ACTIVITIES_TYPE en)
        {
            var valid = ModelState.IsValid;
            en.LOG_USER_CREAT = UsuarioActual;

            if (valid == true)
            {
                bool std = new BLREC_LAWSUITE_ACTIVITIES_TYPE().REC_LAWSUITE_ACTIVITIES_TYPE_Ins(en);

                if (std)
                {
                    //TempData["msg"] = "Registrado Correctamente";
                    //TempData["class"] = "alert alert-success";
                    TempData["flag"] = 1;
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
            BEREC_LAWSUITE_ACTIVITIES_TYPE localidades = new BEREC_LAWSUITE_ACTIVITIES_TYPE();
            var lista = new BLREC_LAWSUITE_ACTIVITIES_TYPE().REC_LAWSUITE_ACTIVITIES_TYPE_GET_by_LAWS_ATY(id.Split(',')[0], id.Split(',')[1]);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    localidades.OWNER = item.OWNER;
                    localidades.LAWS_ATY = item.LAWS_ATY;
                    localidades.LAWS_ATDESC = item.LAWS_ATDESC;
                }
            }
            return View(localidades);
        }

        [HttpPost]
        public ActionResult Edit(BEREC_LAWSUITE_ACTIVITIES_TYPE en)
        {
            var valid = ModelState.IsValid;
            en.LOG_USER_UPDAT = UsuarioActual;

            if (valid == true)
            {
                bool std = new BLREC_LAWSUITE_ACTIVITIES_TYPE().REC_LAWSUITE_ACTIVITIES_TYPE_Upd(en);
                if (std)
                {
                    //TempData["msg"] = "Actualizado Correctamente";
                    //TempData["class"] = "alert alert-success";
                    TempData["flag"] = 1;
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
        public ActionResult Eliminar(List<BEREC_LAWSUITE_ACTIVITIES_TYPE> dato)
        {
            Init(false);//add sysseg
            foreach (var item in dato)
            {
                bool std = new BLREC_LAWSUITE_ACTIVITIES_TYPE().REC_LAWSUITE_ACTIVITIES_TYPE_Del(item.LAWS_ATY.Split(',')[0], item.LAWS_ATY.Split(',')[1]);
            }
            return RedirectToAction("Index");
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_LAWSUITE_ACTIVITIES_TYPE.rdlc");

            List<BEREC_LAWSUITE_ACTIVITIES_TYPE> lista = new List<BEREC_LAWSUITE_ACTIVITIES_TYPE>();
            lista = usp_listar_tipoactuacionesLitigio();

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_LAWSUITE_ACTIVITIES_TYPE.rdlc");

            List<BEREC_LAWSUITE_ACTIVITIES_TYPE> lista = new List<BEREC_LAWSUITE_ACTIVITIES_TYPE>();
            lista = usp_listar_tipoactuacionesLitigio();

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

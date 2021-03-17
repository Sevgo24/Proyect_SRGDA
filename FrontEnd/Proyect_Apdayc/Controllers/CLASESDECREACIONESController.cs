using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;

namespace Proyect_Apdayc.Controllers
{
    public class CLASESDECREACIONESController : Base
    {
        //
        // GET: /CLASESDECREACIONES/
        static string auxclasecreacion = "";
        static string auxtipodocumento = "";
        static string auxclase = "";
        IEnumerable<SelectListItem> items;
        public ActionResult Index()
        {
            Init(false);
            listartipoDocumento();
            return View();
        }       

        public JsonResult usp_listar_ClaseCreacionesJson(string clasecreacion, string tipodocumento, string clase)
        {
            var lista = ClasesdeCreacionListar(clasecreacion, tipodocumento, clase);
            return Json(new BEREF_CREATION_CLASS { REFCREATIONCLASS = lista }, JsonRequestBehavior.AllowGet);
        }

        public List<BEREF_CREATION_CLASS> ClasesdeCreacionListar(string CLASS_COD, string CLASS_DESC, string COD_PARENT_CLASS)
        {
            auxclasecreacion = CLASS_COD;
            auxtipodocumento = CLASS_DESC;
            auxclase = COD_PARENT_CLASS;
            return new BLREF_CREATION_CLASS().usp_Get_REF_CREATION_CLASS(CLASS_COD, CLASS_DESC, COD_PARENT_CLASS);
        }

        private void listartipoDocumento()
        {
            items = new BLREC_DOCUMENT_TYPE().REC_DOCUMENT_TYPE_GET()
            .Select(c => new SelectListItem
              {
                  Value = c.DOC_TYPE.ToString(),
                  Text = c.DOC_DESC
              });
            ViewData["Lista_Tipodocumento"] = items;
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REF_CREATION_CLASS.rdlc");

            List<BEREF_CREATION_CLASS> lista = new List<BEREF_CREATION_CLASS>();
            lista = ClasesdeCreacionListar(auxclasecreacion, auxtipodocumento, auxclase);

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
            Init(false);
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REF_CREATION_CLASS.rdlc");

            List<BEREF_CREATION_CLASS> lista = new List<BEREF_CREATION_CLASS>();
            lista = ClasesdeCreacionListar(auxclasecreacion, auxtipodocumento, auxclase);

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

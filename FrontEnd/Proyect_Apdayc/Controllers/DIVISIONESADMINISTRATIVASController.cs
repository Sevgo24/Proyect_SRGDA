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
    public class DIVISIONESADMINISTRATIVASController : Base
    {
        //
        // GET: /DIVISIONESADMINISTRATIVAS/

        public static string cod = "";
        public static string cod1 = "";
        public static string TipoDiv = "";

        public ActionResult Index()
        {
            Init(false);//add sysseg
            var lista = DivisionesAdministrativasPag("", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<BEREF_DIVISIONES> usp_listar_DivisionesAdministrativas()
        {
            return new BLREF_DIVISIONES().Get_REF_DIVISIONES();
        }

        public JsonResult usp_listar_DivisionesAdministrativasJson(int skip, int take, int page, int pageSize, string group, string dato)
        {
            Init();//add sysseg
            var lista = DivisionesAdministrativasPag(dato, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            return Json(new BEREF_DIVISIONES { REFDIVISIONES = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
        }

        public List<BEREF_DIVISIONES> DivisionesAdministrativasPag(string dato, int pagina, int cantRegxpag)
        {
            return new BLREF_DIVISIONES().REF_DIVISIONES_Page(dato, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            listaTiposDivisiones();
            ViewData["TiposDivisiones"] = TipoDiv;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            var valid = ModelState.IsValid;

            BEREF_DIVISIONES en = new BEREF_DIVISIONES();
            en.LOG_USER_CREAT = UsuarioActual;
            //en.OWNER = frm["OWNER"];
            en.DAD_CODE = frm["DAD_CODE"];
            en.DAD_NAME = frm["DAD_NAME"];
            en.DAD_TYPE = frm["Lista_TiposDivisiones"];

            if (valid == true)
            {
                bool std = new BLREF_DIVISIONES().REF_DIVISIONES_Ins(en);

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

            listaTiposDivisiones();

            return View();
        }

        //REF_DIV_TYPE
        IEnumerable<SelectListItem> lista1;
        private void listaTiposDivisiones()
        {
            lista1 = new BLREF_DIV_TYPE().usp_Get_REF_DIV_TYPE()
            .Select(c => new SelectListItem
            {
                Value = c.DAD_TYPE,
                Text = c.DAD_TNAME
            });
            ViewData["Lista_TiposDivisiones"] = lista1;
            ViewData["Lista_TiposDivisiones"] = new SelectList(lista1, "Value", "Text", cod);
        }

        public ActionResult Edit(string id = "")
        {
            Init(false);//add sysseg
            BEREF_DIVISIONES divisionesadminis = new BEREF_DIVISIONES();

            var lista = new BLREF_DIVISIONES().REF_DIVISIONES_GET_by_DAD_ID(Convert.ToDecimal(id.ToString().Split(',')[1]));

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    divisionesadminis.OWNER = item.OWNER;
                    divisionesadminis.DAD_ID = item.DAD_ID;
                    divisionesadminis.DAD_CODE = item.DAD_CODE;
                    divisionesadminis.DAD_NAME = item.DAD_NAME;
                    divisionesadminis.DAD_TYPE = item.DAD_TYPE;
                    divisionesadminis.DAD_TNAME = item.DAD_TNAME;
                    cod = item.DAD_TYPE;
                    cod1 = item.DAD_STYPE;
                    listaTiposDivisiones();
                }
            }
            return View(divisionesadminis);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm)
        {
            var valid = ModelState.IsValid;
            BEREF_DIVISIONES en = new BEREF_DIVISIONES();
            //en.OWNER = frm["OWNER"];
            en.DAD_ID = Convert.ToDecimal(frm["DAD_ID"]);
            en.DAD_CODE = frm["DAD_CODE"];
            en.DAD_NAME = frm["DAD_NAME"];
            en.DAD_TYPE = frm["Lista_TiposDivisiones"];
            en.LOG_USER_UPDATE = UsuarioActual;

            if (valid == true)
            {
                bool std = new BLREF_DIVISIONES().REF_DIVISIONES_Upd(en);

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
        public ActionResult Eliminar(List<BEREF_DIVISIONES> dato)
        {
            Init(false);//add sysseg
            if (dato.Count() > 0)
            {
                foreach (var item in dato)
                {
                    var TipoDiv = new BEREF_DIVISIONES()
                    {
                        DAD_ID = item.DAD_ID
                    };
                    bool std = new BLREF_DIVISIONES().REF_DIVISIONES_Del(item.DAD_ID);
                }
            }
            return RedirectToAction("Index");
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REF_DIVISIONES.rdlc");

            List<BEREF_DIVISIONES> lista = new List<BEREF_DIVISIONES>();
            lista = usp_listar_DivisionesAdministrativas();

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REF_DIVISIONES.rdlc");

            List<BEREF_DIVISIONES> lista = new List<BEREF_DIVISIONES>();
            lista = usp_listar_DivisionesAdministrativas();

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

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
    public class SUBTIPOSDIVISIONESController : Base
    {
        //
        // GET: /SUBTIPOSDIVISIONES/

        public static string DAD_ID = "";
        public static string DAD_BELONGS = "";
        public static string Divisiones = "";
        public static string Dependencia = "";

        public ActionResult Index()
        {
            Init(false);//add sysseg
            var lista = SubTipoDivisionesListarPag("", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<BEREF_DIV_SUBTYPE> usp_listar_SubTipoDivisiones()
        {
            return new BLREF_DIV_SUBTYPE().REF_DIV_SUBTYPE_Get();
        }

        public JsonResult usp_listar_SubTipoDivisionesJson(int skip, int take, int page, int pageSize, string group, string dato)
        {
            Init();//add sysseg
            var lista = SubTipoDivisionesListarPag(dato, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            return Json(new BEREF_DIV_SUBTYPE { REFDIVSUBTYPE = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
        }

        public List<BEREF_DIV_SUBTYPE> SubTipoDivisionesListarPag(string dato, int pagina, int cantRegxpag)
        {
            return new BLREF_DIV_SUBTYPE().REF_DIV_SUBTYPE_Page(dato, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            listaDivisiones();
            listaDependencia(1);
            ViewData["listadependencia"] = Dependencia;
            ViewData["listadivisiones"] = Divisiones;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            var valid = ModelState.IsValid;
            BEREF_DIV_SUBTYPE en = new BEREF_DIV_SUBTYPE();
            en.LOG_USER_CREAT = UsuarioActual;
            en.DAD_ID = Convert.ToDecimal(frm["lista_divisiones"]);
            en.DAD_SNAME = frm["DAD_SNAME"];
            en.DAD_NAME = frm["DAD_NAME"];
            en.DAD_BELONGS = Convert.ToDecimal( frm["lista_dependencia"]);

            if (valid == true)
            {
                bool std = new BLREF_DIV_SUBTYPE().REF_DIV_SUBTYPE_Ins(en);

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
            listaDivisiones();
            listaDependencia(Convert.ToDecimal(1));
            return View();
        }

        public ActionResult Edit(string id = "")
        {
            Init(false);//add sysseg
            BEREF_DIV_SUBTYPE datos = new BEREF_DIV_SUBTYPE();
            var lista = new BLREF_DIV_SUBTYPE().REF_DIV_SUBTYPE_GET_by_DAD_STYPE(Convert.ToDecimal(id.ToString().Split(',')[1]));

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    datos.OWNER = item.OWNER;
                    datos.DAD_ID = item.DAD_ID;
                    datos.DAD_CODE = item.DAD_CODE;
                    datos.DAD_STYPE = item.DAD_STYPE;
                    datos.DAD_SNAME = item.DAD_SNAME;
                    datos.DAD_NAME = item.DAD_NAME;
                    datos.DAD_BELONGS = item.DAD_BELONGS;
                    DAD_ID = item.DAD_ID.ToString();
                    listaDivisiones();
                    listaDependencia(1);
                }
            }
            return View(datos);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm)
        {
            var valid = ModelState.IsValid;
            BEREF_DIV_SUBTYPE en = new BEREF_DIV_SUBTYPE();
            en.LOG_USER_UPDATE = UsuarioActual;
            en.DAD_STYPE = Convert.ToDecimal(frm["DAD_STYPE"]);
            en.DAD_ID = Convert.ToDecimal(frm["lista_divisiones"]);
            en.DAD_SNAME = frm["DAD_SNAME"];
            en.DAD_NAME = frm["DAD_NAME"];
            en.DAD_BELONGS =Convert.ToDecimal( frm["lista_dependencia"]);

            if (valid == true)
            {
                bool std = new BLREF_DIV_SUBTYPE().REF_DIV_SUBTYPE_Upd(en);

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
        public ActionResult Eliminar(List<BEREF_DIV_SUBTYPE> dato)
        {
            Init(false);//add sysseg
            foreach (var item in dato)
            {
                bool std = new BLREF_DIV_SUBTYPE().REF_DIV_SUBTYPE_Del(item.DAD_STYPE.ToString().Split(',')[0], Convert.ToDecimal(item.DAD_STYPE.ToString().Split(',')[1]));
            }
            return RedirectToAction("Index");
        }

        IEnumerable<SelectListItem> listadivisiones;

        //REF_DIVISIONES
        private void listaDivisiones()
        {
            listadivisiones = new BLREF_DIVISIONES().Get_REF_DIVISIONES()
            .Select(c => new SelectListItem
            {
                Value = c.DAD_ID.ToString(),
                Text = c.DAD_NAME
            });
            ViewData["lista_divisiones"] = listadivisiones;
            ViewData["lista_divisiones"] = new SelectList(listadivisiones, "Value", "Text", DAD_ID);
        }

        IEnumerable<SelectListItem> listadependencia;

        //REF_DIVISIONES
        private void listaDependencia(decimal? DAD_ID)
        {
            listadependencia = new BLREF_DIV_SUBTYPE().REF_DIV_SUBTYPE_DAD_BELONGS_GET_by_DAD_ID(DAD_ID.Value)
            .Select(c => new SelectListItem
            {
                Value = c.DAD_BELONGS.ToString(),
                Text = c.DAD_BELONGS.ToString()
            });
            ViewData["lista_dependencia"] = listadependencia;
            ViewData["lista_dependencia"] = new SelectList(listadependencia, "Value", "Text", DAD_BELONGS);
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REF_DIV_SUBTYPE.rdlc");

            List<BEREF_DIV_SUBTYPE> lista = new List<BEREF_DIV_SUBTYPE>();
            lista = usp_listar_SubTipoDivisiones();

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REF_DIV_SUBTYPE.rdlc");

            List<BEREF_DIV_SUBTYPE> lista = new List<BEREF_DIV_SUBTYPE>();
            lista = usp_listar_SubTipoDivisiones();

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

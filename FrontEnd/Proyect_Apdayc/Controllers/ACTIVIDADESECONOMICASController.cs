using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using System.Configuration;
using Proyect_Apdayc.Clases;

namespace Proyect_Apdayc.Controllers
{
    public class ACTIVIDADESECONOMICASController : Base
    {
        //
        // GET: /ACTIVIDADESECONOMICAS/
        string auxECONID = "";
        public ActionResult Index()
        {
            Init(false);//add sysseg
            //var lista = ActividadEconomicaListarPag("", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<BEREC_ECON_ACTIVITIES> usp_listar_ActividadEconomica()
        {
            return new BLREC_ECON_ACTIVITIES().Get_REC_ECON_ACTIVITIES();
        }

        public JsonResult usp_listar_ActividadEconomicaJson(int skip, int take, int page, int pageSize, string group, string owner, string dato, int st)
        {
            Init();//add sysseg
            var lista = ActividadEconomicaListarPag(GlobalVars.Global.OWNER, dato, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEREC_ECON_ACTIVITIES { RECECONACTIVITIES = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_ECON_ACTIVITIES { RECECONACTIVITIES = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREC_ECON_ACTIVITIES> ActividadEconomicaListarPag(string owner, string dato, int st, int pagina, int cantRegxpag)
        {
            return new BLREC_ECON_ACTIVITIES().REC_ECON_ACTIVITIES_Page(owner, dato, st, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            listaActividades();

            ViewData["ListaActividades"] = codAct;
            return View();
        }

        public static string codAct = "";

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm, BEREC_ECON_ACTIVITIES obj)
        {
            var valid = ModelState.IsValid;
            BEREC_ECON_ACTIVITIES en = new BEREC_ECON_ACTIVITIES();
            //en.ECON_ID = frm["ECON_ID"];
            //en.ECON_DESC = frm["ECON_DESC"];
            obj.ECON_BELONGS = frm["Lista_Actividades"];
            obj.LOG_USER_CREAT = "USERCREAT";

            if (valid == true)
            {
                bool std = new BLREC_ECON_ACTIVITIES().REC_ECON_ACTIVITIES_Ins(obj);

                if (std)
                {
                    TempData["msg"] = "Registrado Correctamente";
                    TempData["class"] = "alert alert-success";
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

                TempData["flag"] = 0;
            }
            listaActividades();

            return RedirectToAction("../ACTIVIDADESECONOMICAS/");
        }

        public static string cod = "";
        public ActionResult Edit(string id = "")
        {
            Init(false);//add sysseg
            BEREC_ECON_ACTIVITIES TipoDirecciones = new BEREC_ECON_ACTIVITIES();
            var lista = new BLREC_ECON_ACTIVITIES().REC_ECON_ACTIVITIES_GET_by_ECON_ID(id.Split(',')[1]);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    TipoDirecciones.OWNER = item.OWNER;
                    TipoDirecciones.ECON_ID = item.ECON_ID;
                    TipoDirecciones.ECON_DESC = item.ECON_DESC;
                    TipoDirecciones.ECON_BELONGS = item.ECON_BELONGS;
                    cod = item.ECON_ID_Bellong.ToString();
                    //cod = item.ECON_ID.ToString();
                    listaActividades();
                }
            }
            return View(TipoDirecciones);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm, BEREC_ECON_ACTIVITIES obj)
        {
            var valid = ModelState.IsValid;
            BEREC_ECON_ACTIVITIES en = new BEREC_ECON_ACTIVITIES();
            en.ECON_ID = frm["ECON_ID"];
            en.ECON_DESC = frm["ECON_DESC"];
            en.ECON_BELONGS = frm["Lista_Actividades"];
            en.LOG_USER_UPDAT = "USERCREAT";

            if (valid == true)
            {
                bool std = new BLREC_ECON_ACTIVITIES().REC_ECON_ACTIVITIES_Upd(en);

                if (std)
                {
                    TempData["msg"] = "Actualizado Correctamente";
                    TempData["class"] = "alert alert-success";
                    TempData["flag"] = 1;
                }
                else
                {
                    TempData["msg"] = "Ocurrio un inconveniente, no se pudo Actualizar";
                    TempData["class"] = "alert alert-danger";
                    TempData["flag"] = 0;
                }
            }
            else
            {
                TempData["flag"] = 0;
                TempData["msg"] = "Ocurrio un inconveniente, vuelva a ingresar dato(s).";
                TempData["class"] = "alert alert-danger";
            }

            return RedirectToAction("Edit");
        }

        [HttpPost]
        public ActionResult Eliminar(List<BEREC_ECON_ACTIVITIES> dato)
        {
            Init(false);//add sysseg
            foreach (var item in dato)
            {
                var economica = new BEREC_ECON_ACTIVITIES()
                {
                    ECON_ID = item.ECON_ID
                };
                bool std = new BLREC_ECON_ACTIVITIES().REC_ECON_ACTIVITIES_Del(item.ECON_ID);
            }
            return RedirectToAction("../ACTIVIDADESECONOMICAS/");
        }

        IEnumerable<SelectListItem> lista1;
        private void listaActividades()
        {
            lista1 = new BLREC_ECON_ACTIVITIES().Get_REC_ECON_ACTIVITIES()
            .Select(c => new SelectListItem
            {
                Value = c.ECON_ID.ToString(),
                Text = c.ECON_DESC

            });

            ViewData["Lista_Actividades"] = lista1;
            //ViewData["Lista_Actividades"] = lista1.OrderBy(x => x.Value).ToList();
            if (cod == null) cod = string.Empty;
            ViewData["Lista_Actividades"] = new SelectList(lista1, "Value", "Text", cod);
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_ECON_ACTIVITIES.rdlc");

            List<BEREC_ECON_ACTIVITIES> lista = new List<BEREC_ECON_ACTIVITIES>();
            lista = usp_listar_ActividadEconomica();

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_ECON_ACTIVITIES.rdlc");

            List<BEREC_ECON_ACTIVITIES> lista = new List<BEREC_ECON_ACTIVITIES>();
            lista = usp_listar_ActividadEconomica();

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

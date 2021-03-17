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
    public class SUBTIPOSESTABLECIMIENTOSController : Base
    {
        //
        // GET: /SUBTIPOSESTABLECIMIENTOS/

        public static string cod = "";
        public static string cod1 = "";
        public static string actividadesEconomicas = "";
        public static string tiposestablecimiento = "";
        IEnumerable<SelectListItem> lista1;
        IEnumerable<SelectListItem> lista2;

        public ActionResult Index()
        {
            Init(false);//add sysseg
            //var lista = SubTipoEstablecimientosListarPag("", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<BEREC_EST_SUBTYPE> usp_listar_TipoEstablecimientos()
        {
            return new BLREC_EST_SUBTYPE().REC_EST_SUBTYPE_GET();
        }

        public JsonResult usp_listar_SubTipoEstablecimientosJson(int skip, int take, int page, int pageSize, string group, string dato, decimal TipoEst, int st)
        {
            Init();//add sysseg
            var lista = SubTipoEstablecimientosListarPag(GlobalVars.Global.OWNER, dato, TipoEst, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEREC_EST_SUBTYPE { RECESTSUBTYPE = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_EST_SUBTYPE { RECESTSUBTYPE = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREC_EST_SUBTYPE> SubTipoEstablecimientosListarPag(string owner, string dato, decimal TipoEst, int st, int pagina, int cantRegxpag)
        {
            return new BLREC_EST_SUBTYPE().usp_REC_EST_SUBTYPE_Page(owner, dato, TipoEst, st, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            listaTiposEstablecimientos();
            ViewData["listaTiposEstableciemientos"] = tiposestablecimiento;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            var valid = ModelState.IsValid;
            BEREC_EST_SUBTYPE en = new BEREC_EST_SUBTYPE();
            en.LOG_USER_CREAT = UsuarioActual;
            en.ESTT_ID = Convert.ToDecimal(frm["lista_TiposEstableciemientos"]);
            en.DESCRIPTION = frm["DESCRIPTION"];

            if (frm["DESCRIPTION"] == string.Empty)
            {
                valid = false;
                TempData["validar"] = 1;
            }

            var existe = new BLREC_EST_SUBTYPE().existeSubTipoEstablecimiento(GlobalVars.Global.OWNER, en.ESTT_ID, en.DESCRIPTION);

            if (existe)
            {
                TempData["msg"] = "El Sub Tipo de Establecimiento ya  existe.";
                TempData["class"] = "alert alert-danger";
                TempData["flag"] = 2;
                return RedirectToAction("Create");
            }

            if (valid == true)
            {
                bool std = new BLREC_EST_SUBTYPE().REC_EST_SUBTYPE_Ins(en);

                if (std)
                {
                    TempData["flag"] = 1;
                }
                else
                {
                    TempData["msg"] = "Ocurrio un inconveniente, no se pudo Registrar";
                    TempData["class"] = "alert alert-danger";
                }
            }
            else
                TempData["class1"] = "alert alert-danger";

            listaTiposEstablecimientos();
            return RedirectToAction("../SUBTIPOSESTABLECIMIENTOS/");
        }

        public ActionResult Edit(string id = "")
        {
            Init(false);//add sysseg
            BEREC_EST_SUBTYPE subtipo = new BEREC_EST_SUBTYPE();
            var lista = new BLREC_EST_SUBTYPE().REC_EST_SUBTYPE_by_SUBE_ID(Convert.ToDecimal(id.Split(',')[1]));

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    subtipo.OWNER = item.OWNER;
                    subtipo.SUBE_ID = item.SUBE_ID;
                    subtipo.ESTT_ID = item.ESTT_ID;
                    subtipo.DESCRIPTION = item.DESCRIPTION;
                    subtipo.DESCRIPTIONTYPE = item.DESCRIPTIONTYPE;

                    cod1 = item.ESTT_ID.ToString();
                    listaTiposEstablecimientos();
                }
            }
            return View(subtipo);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm)
        {
            BEREC_EST_SUBTYPE en = new BEREC_EST_SUBTYPE();
            en.LOG_USER_UPDAT = UsuarioActual;
            en.ESTT_ID = Convert.ToDecimal(frm["lista_TiposEstableciemientos"]);
            en.SUBE_ID = Convert.ToDecimal(frm["SUBE_ID"]);
            en.OWNER = frm["OWNER"];
            en.DESCRIPTION = frm["DESCRIPTION"];
            var valid = ModelState.IsValid;

            var existe = new BLREC_EST_SUBTYPE().existeSubTipoEstablecimiento(GlobalVars.Global.OWNER, en.ESTT_ID, en.DESCRIPTION, en.SUBE_ID);

            if (existe)
            {
                //TempData["msg"] = "El SubTipo de Establecimiento ya  existe.";
                //TempData["class"] = "alert alert-danger";
                TempData["flag"] = 2;
                return RedirectToAction("Edit");
            }

            if (valid == true)
            {
                bool std = new BLREC_EST_SUBTYPE().REC_EST_SUBTYPE_Upd(en);

                if (std)
                {
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
        public ActionResult Eliminar(List<BEREC_EST_SUBTYPE> dato)
        {
            Init(false);//add sysseg
            foreach (var item in dato)
            {
                var impuesto = new BEREC_EST_SUBTYPE()
                {
                    SUBE_ID = item.SUBE_ID
                };
                bool std = new BLREC_EST_SUBTYPE().REC_EST_SUBTYPE_Del(item.SUBE_ID);
            }
            return RedirectToAction("../SUBTIPOSESTABLECIMIENTOS/");
        }


        //REC_ECON_ACTIVITIES
        private void listaActividadEconomica()
        {
            lista1 = new BLREC_ECON_ACTIVITIES().Get_REC_ECON_ACTIVITIES()
            .Select(c => new SelectListItem
            {
                Value = c.ECON_ID,
                Text = c.ECON_DESC
            });
            ViewData["lista_ActividadEconomica"] = lista1;
            ViewData["lista_ActividadEconomica"] = new SelectList(lista1, "Value", "Text", cod);
        }

        //REC_EST_TYPE_GET
        private void listaTiposEstablecimientos()
        {
            lista2 = new BLREC_EST_TYPE().REC_EST_TYPE_GET()
             .Select(c => new SelectListItem
             {
                 Value = c.ESTT_ID.ToString(),
                 Text = c.DESCRIPTION
             });
            ViewData["lista_TiposEstableciemientos"] = lista2;
            ViewData["lista_TiposEstableciemientos"] = new SelectList(lista2, "Value", "Text", cod1);
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_EST_SUBTYPE.rdlc");

            List<BEREC_EST_SUBTYPE> lista = new List<BEREC_EST_SUBTYPE>();
            lista = usp_listar_TipoEstablecimientos();

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_EST_SUBTYPE.rdlc");

            List<BEREC_EST_SUBTYPE> lista = new List<BEREC_EST_SUBTYPE>();
            lista = usp_listar_TipoEstablecimientos();

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

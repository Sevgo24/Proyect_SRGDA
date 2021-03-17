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
    public class IDENTIFICADORESFISCALESController : Base
    {
        //
        // GET: /IDENTIFICADORESFISCALES/
        string auxTIS_N = "";
        public ActionResult Index()
        {
            Init(false);//add sysseg
            return View();
        }

        public List<BEREC_TAX_ID> usp_listar_identificadoresFiscales()
        {
            return new BLREC_TAX_ID().Get_REC_TAX_ID();
        }

        public JsonResult usp_listar_IdentificadoresFiscalesJson(int skip, int take, int page, int pageSize, string group, string dato, string owner, int st)
        {
            Init();//add sysseg
            var lista = identificadoresFiscalesListarPag(dato, GlobalVars.Global.OWNER, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEREC_TAX_ID { RECTAXID = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_TAX_ID { RECTAXID = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREC_TAX_ID> identificadoresFiscalesListarPag(string dato, string owner, int st, int pagina, int cantRegxpag)
        {
            return new BLREC_TAX_ID().REC_TAX_ID_Page(dato, owner, st, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            cod = "604";
            listaTerritorio();
            //ViewData["ListaTerritorio"] = codTer;
            return View();
        }

        public static string codTer = "";

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm, BEREC_TAX_ID ent)
        {
            bool estado=true;
            var valid = ModelState.IsValid;

            if (string.IsNullOrEmpty(frm["TAXN_NAME"].Trim()))
                estado = false;
            if (string.IsNullOrEmpty(frm["TAXN_POS"].ToString().Trim()))
                estado = false;
            if (string.IsNullOrEmpty(frm["TEXT_DESCRIPTION"].ToString().Trim()))
                estado = false;

            if (estado == true)
            {
                BEREC_TAX_ID en = new BEREC_TAX_ID();
                en.LOG_USER_CREAT = UsuarioActual;
                en.OWNER = GlobalVars.Global.OWNER;
                en.TAXN_NAME = frm["TAXN_NAME"];
                en.TAXN_POS = Convert.ToDecimal(frm["TAXN_POS"]);
                en.TEXT_DESCRIPTION = frm["TEXT_DESCRIPTION"];
                en.TIS_N = Convert.ToDecimal(frm["Lista_Territorio"]);
                bool std = new BLREC_TAX_ID().REC_TAX_ID_Ins(en);

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
            listaTerritorio();

            return View();
        }

        public static string cod = "";
        public ActionResult Edit(string id = "")
        {
            Init(false);//add sysseg
            BEREC_TAX_ID identificadorfiscal = new BEREC_TAX_ID();
            var lista = new BLREC_TAX_ID().REC_TAX_ID_GET_by_TAXT_ID(Convert.ToDecimal(id.Split(',')[1]));

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    identificadorfiscal.OWNER = item.OWNER;
                    identificadorfiscal.TAXT_ID = item.TAXT_ID;
                    identificadorfiscal.TIS_N = item.TIS_N;
                    identificadorfiscal.TAXN_NAME = item.TAXN_NAME;
                    identificadorfiscal.TAXN_POS = item.TAXN_POS;
                    identificadorfiscal.TEXT_DESCRIPTION = item.TEXT_DESCRIPTION;
                    identificadorfiscal.NAME_TER = item.NAME_TER;
                    cod = item.TIS_N.ToString();
                    listaTerritorio();
                }
            }
            return View(identificadorfiscal);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm, BEREC_TAX_ID ent)
        {
            var valid = ModelState.IsValid;

            if (valid == true)
            {
                BEREC_TAX_ID en = new BEREC_TAX_ID();
                en.LOG_USER_CREAT = UsuarioActual;
                en.OWNER = GlobalVars.Global.OWNER;
                en.TAXT_ID = Convert.ToDecimal(frm["TAXT_ID"]);
                en.TAXN_NAME = frm["TAXN_NAME"];
                en.TAXN_POS = Convert.ToDecimal(frm["TAXN_POS"]);
                en.TEXT_DESCRIPTION = frm["TEXT_DESCRIPTION"];
                en.TIS_N = Convert.ToDecimal(frm["Lista_Territorio"]);
                ent.LOG_USER_CREAT = en.LOG_USER_CREAT;
                ent.OWNER = en.OWNER;
                ent.TAXT_ID = en.TAXT_ID;
                ent.TAXN_NAME = en.TAXN_NAME;
                ent.TAXN_POS = en.TAXN_POS;
                ent.TIS_N = en.TIS_N;

                bool std = new BLREC_TAX_ID().REC_TAX_ID_Upd(ent);

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
                    TempData["flag"] = 0;
                }
            }
            else
            {
                //TempData["class1"] = "alert alert-danger";
                TempData["msg"] = "Ocurrio un inconveniente, vuelva a ingrese dato(s)";
                TempData["class"] = "alert alert-danger";
                TempData["flag"] = 0;
            }

            return RedirectToAction("Edit");
        }

        [HttpPost]
        public ActionResult Eliminar(List<BEREC_TAX_ID> dato)
        {
            Init(false);//add sysseg
            foreach (var item in dato)
            {
                var inden = new BEREC_TAX_ID()
                {
                    TAXT_ID = item.TAXT_ID
                };
                bool std = new BLREC_TAX_ID().REC_TAX_ID_Del(item.TAXT_ID);
            }
            return RedirectToAction("Index");
        }

        IEnumerable<SelectListItem> lista1;
        private void listaTerritorio()
        {
            lista1 = new BLTerritorio().Listar_Territorio()
            .Select(c => new SelectListItem
            {
                Value = c.TIS_N.ToString(),
                Text = c.NAME_TER
            });
            ViewData["Lista_Territorio"] = lista1;
            ViewData["Lista_Territorio"] = new SelectList(lista1, "Value", "Text", cod);
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_TAX_ID.rdlc");

            List<BEREC_TAX_ID> lista = new List<BEREC_TAX_ID>();
            lista = usp_listar_identificadoresFiscales();

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_TAX_ID.rdlc");

            List<BEREC_TAX_ID> lista = new List<BEREC_TAX_ID>();
            lista = usp_listar_identificadoresFiscales();

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

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.Entities;
using SGRDA.BL;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases;

namespace SGRDA.MVC.Controllers
{
    public class BANCOSController : Base
    {
        //
        // GET: /BANCOS/

        public ActionResult Index() 
        {
            Init(false);//add sysseg
            return View();
        }

        public List<BEREC_BANKS_GRAL> usp_listar_bancos()
        {
            return new BLREC_BANKS_GRAL().Get_REC_BANKS_GRAL();
        }

        public JsonResult usp_listar_bancosjson(int skip, int take, int page, int pageSize, string group, string dato, int st)
        {
            Init();//add sysseg
            var lista = REC_BANKS_GRAL_LISTAR_PAG(dato, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEREC_BANKS_GRAL { REC_BA = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_BANKS_GRAL { REC_BA = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult usp_listar_SucursalesXbancosjson(int skip, int take, int page, int pageSize, string group, string owner, decimal id)
        {
            Init();//add sysseg
            var lista = ListarSucursalXBanco(GlobalVars.Global.OWNER, id, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEREC_BANKS_BRANCH { RECBANKSBRANCH = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_BANKS_BRANCH { RECBANKSBRANCH = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult usp_listar_ContactosXbancosjson(int skip, int take, int page, int pageSize, string group, string owner, decimal id)
        {
            Init();//add sysseg
            var lista = ListarContactosXBanco(GlobalVars.Global.OWNER, id, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEREC_BANKS_BPS { RECBANKSBPS = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_BANKS_BPS { RECBANKSBPS = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREC_BANKS_GRAL> REC_BANKS_GRAL_LISTAR_PAG(string dato, int st, int pagina, int cantRegxpag)
        {
            return new BLREC_BANKS_GRAL().USP_GET_DAREC_BANKS_GRAL_PAGE(dato, st, pagina, cantRegxpag);
        }

        public List<BEREC_BANKS_BRANCH> ListarSucursalXBanco(string owner, decimal id, int pagina, int cantRegxPag)
        {
            return new BLREC_BANKS_GRAL().LISTAR_SUCURSAL_X_BANCO_PAGE(owner, id, pagina, cantRegxPag);
        }

        public List<BEREC_BANKS_BPS> ListarContactosXBanco(string owner, decimal id, int pagina, int cantRegxPag)
        {
            return new BLREC_BANKS_GRAL().LISTAR_CONTACTOS_X_BANCO_PAGE(owner, id, pagina, cantRegxPag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            var valid = ModelState.IsValid;
            BEREC_BANKS_GRAL en = new BEREC_BANKS_GRAL();
            en.BNK_NAME = frm["BNK_NAME"];
            en.BNK_C_BRANCH = frm["BNK_C_BRANCH"] == string.Empty ? 0 : Convert.ToDecimal(frm["BNK_C_BRANCH"]);
            en.BNK_C_DC = frm["BNK_C_DC"] == string.Empty ? 0 : Convert.ToDecimal(frm["BNK_C_DC"]);
            en.BNK_C_ACCOUNT = frm["BNK_C_ACCOUNT"] == string.Empty ? 0 : Convert.ToDecimal(frm["BNK_C_ACCOUNT"]); 
            en.LOG_USER_CREAT = "USERCREAT";

            if (frm["BNK_NAME"] == string.Empty)
            {
                valid = false;
                TempData["validar"] = 1;
            }
            if (frm["BNK_C_BRANCH"] == string.Empty)
            {
                valid = false;
                TempData["validar"] = 1;
            }
            if (frm["BNK_C_DC"] == string.Empty)
            {
                valid = false;
                TempData["validar"] = 1;
            }
            if (frm["BNK_C_ACCOUNT"] == string.Empty)
            {
                valid = false;
                TempData["validar"] = 1;
            }

            if (valid == true)
            {
                bool std = new BLREC_BANKS_GRAL().REC_BANKS_GRAL_Ins(en);

                if (std)
                {
                    //TempData["msg"] = "Registrado Correctamente";
                    //TempData["class"] = "alert alert-success";
                    TempData["flag"] = 1;
                }
                else
                {
                    //TempData["msg"] = "Ocurrio un inconveniente, no se pudo Registrar";
                    //TempData["class"] = "alert alert-danger";
                    TempData["flag"] = 0;
                }
            }
            else
            {
                TempData["class1"] = "alert alert-danger";
            }
            return RedirectToAction("../BANCOS/");
        }

        public ActionResult Edit(decimal id = 0)
        {
            Init(false);//add sysseg
            BEREC_BANKS_GRAL bancos = new BEREC_BANKS_GRAL();
            //var lista = new BLREC_BANKS_GRAL().REC_BANKS_GRAL_GET_by_BNK_ID(id.Split(',')[0], id.Split(',')[1]);
            var lista = new BLREC_BANKS_GRAL().REC_BANKS_GRAL_GET_by_BNK_ID(GlobalVars.Global.OWNER, id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    bancos.OWNER = item.OWNER;
                    bancos.BNK_ID = item.BNK_ID;
                    bancos.BNK_NAME = item.BNK_NAME;
                    bancos.BNK_C_BRANCH = item.BNK_C_BRANCH;
                    bancos.BNK_C_DC = item.BNK_C_DC;
                    bancos.BNK_C_ACCOUNT = item.BNK_C_ACCOUNT;
                }
            }
            return View(bancos);
        }

        [HttpPost]
        public ActionResult Edit(BEREC_BANKS_GRAL en)
        {
            var valid = ModelState.IsValid;
            en.LOG_USER_UPDATE = "USERCREAT";

            if (valid == true)
            {
                bool std = new BLREC_BANKS_GRAL().REC_BANKS_GRAL_Upd(en);
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
        public ActionResult Eliminar(List<BEREC_BANKS_GRAL> dato)
        {
            Init(false);//add sysseg
            if (dato == null)
            {                
                return RedirectToAction("Index");
            }

            foreach (var item in dato)
            {
                bool std = new BLREC_BANKS_GRAL().REC_BANKS_GRAL_Del(Convert.ToDecimal(item.BNK_ID));
            }
            return RedirectToAction("Index");
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_BANKS_GRAL.rdlc");

            List<BEREC_BANKS_GRAL> lista = new List<BEREC_BANKS_GRAL>();
            lista = usp_listar_bancos();

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_BANKS_GRAL.rdlc");

            List<BEREC_BANKS_GRAL> lista = new List<BEREC_BANKS_GRAL>();
            lista = usp_listar_bancos();

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            reportDataSource.Value = lista;
            localReport.DataSources.Add(reportDataSource);

            ReportParameter parametro = new ReportParameter();
            //parametro.Add(new ReportParameter("Usuario", UsuarioActual));
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

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
    public class CUENTASBANCARIASController : Base
    {
        //
        // GET: /CUENTASBANCARIAS/cam

        public static string cod = "";
        public static string cod1 = "";
        public static string bancos = "";
        public static string sucursales = "";

        public ActionResult Index()
        {
            Init(false);//add sysseg
            //var lista = CuentasBancariasListarPag("", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<BEREC_BPS_BANKS_ACC> usp_listar_CuentasBancarias()
        {
            return new BLREC_BPS_BANKS_ACC().Get_REC_BPS_BANKS_ACC();
        }

        public JsonResult usp_listar_CuentasBancariasJson(int skip, int take, int page, int pageSize, string group, string owner, string dato, int st)
        {
            Init();//add sysseg
            var lista = CuentasBancariasListarPag(GlobalVars.Global.OWNER, dato, st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEREC_BPS_BANKS_ACC { RECBPSBANKSACC = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_BPS_BANKS_ACC { RECBPSBANKSACC = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREC_BPS_BANKS_ACC> CuentasBancariasListarPag(string owner, string dato, int st, int pagina, int cantRegxpag)
        {
            return new BLREC_BPS_BANKS_ACC().REC_BPS_BANKS_ACC_Page(owner, dato, st, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            listaBancos();
            listaSucursalesBancarias();

            ViewData["Bancos"] = bancos;
            ViewData["SucursalesBancarias"] = sucursales;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            var valid = ModelState.IsValid;
            BEREC_BPS_BANKS_ACC en = new BEREC_BPS_BANKS_ACC();
            en.LOG_USER_CREAT = "USERCREAT";
            en.BPS_ID = Convert.ToDecimal(frm["BPS_ID"]);
            en.BACC_NUMBER = frm["BACC_NUMBER"];
            en.BACC_DC = frm["BACC_DC"];
            en.BACC_TYPE = frm["BACC_TYPE"];
            en.BRCH_ID = frm["lista_SucursalesBancarias"];
            en.BACC_DEF = frm["BACC_DEF"];
            en.BNK_ID = Convert.ToDecimal(frm["lista_Bancos"]);

            if (valid == true)
            {
                bool std = new BLREC_BPS_BANKS_ACC().REC_BPS_BANKS_ACC_Ins(en);

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
                TempData["class1"] = "alert alert-danger";

            listaBancos();
            listaSucursalesBancarias();
            return View();
        }

        public ActionResult Edit(string id = "")
        {
            Init(false);//add sysseg
            char[] delimiterChars = { ',' };
            string text = id;
            string[] words = text.Split(delimiterChars);
            string OWNER = words[0];
            decimal BNK_ID = Convert.ToDecimal(words[1]);
            string BRCH_ID = words[2];


            BEREC_BPS_BANKS_ACC cuenta = new BEREC_BPS_BANKS_ACC();
            var lista = new BLREC_BPS_BANKS_ACC().REC_BPS_BANKS_ACC_GET_by_BNK_ID(OWNER, BNK_ID, BRCH_ID);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    cuenta.OWNER = item.OWNER;
                    cuenta.BPS_ID = item.BPS_ID;
                    cuenta.BNK_ID = item.BNK_ID;
                    cuenta.BRCH_ID = item.BRCH_ID;
                    cuenta.BACC_NUMBER = item.BACC_NUMBER;
                    cuenta.BACC_DC = item.BACC_DC;
                    cuenta.BACC_TYPE = item.BACC_TYPE;
                    cuenta.BACC_DEF = item.BACC_DEF;
                }
            }
            return View(cuenta);
        }

        [HttpPost]
        public ActionResult Edit(BEREC_BPS_BANKS_ACC en)
        {
            //auxBNK_ID = cod;
            //auxBRCH_ID = cod1;
            var valid = ModelState.IsValid;
            en.LOG_USER_UPDATE = "USERCREAT";
            //en.BRCH_ID = frm["lista_SucursalesBancarias"];
            //en.BNK_ID = frm["lista_Bancos"];

            if (valid == true)
            {
                bool std = new BLREC_BPS_BANKS_ACC().REC_BPS_BANKS_ACC_Upd(en);

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
        public ActionResult Eliminar(List<BEREC_BPS_BANKS_ACC> dato)
        {
            Init(false);//add sysseg
            foreach (var item in dato)
            {
                var cuenta = new BEREC_BPS_BANKS_ACC()
                {
                    BNK_ID = item.BNK_ID
                };
                bool std = new BLREC_BPS_BANKS_ACC().REC_BPS_BANKS_ACC_Del(Convert.ToDecimal(item.BNK_ID));
            }
            return RedirectToAction("Index");
        }

        IEnumerable<SelectListItem> lista1;
        IEnumerable<SelectListItem> lista2;

        //REC_BANKS_GRAL
        private void listaBancos()
        {
            lista1 = new BLREC_BANKS_GRAL().Get_REC_BANKS_GRAL()
            .Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.BNK_ID),
                Text = c.BNK_NAME
            });
            ViewData["lista_Bancos"] = lista1;
            ViewData["lista_Bancos"] = new SelectList(lista1, "Value", "Text", cod);
        }

        //REC_BANKS_BRANCH
        private void listaSucursalesBancarias()
        {
            lista2 = new BLREC_BANKS_BRANCH().Get_REC_BANKS_BRANCH()
             .Select(c => new SelectListItem
             {
                 Value = c.BRCH_ID,
                 Text = c.BRCH_NAME
             });
            ViewData["lista_SucursalesBancarias"] = lista2;
            ViewData["lista_SucursalesBancarias"] = new SelectList(lista2, "Value", "Text", cod1);
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_BPS_BANKS_ACC.rdlc");

            List<BEREC_BPS_BANKS_ACC> lista = new List<BEREC_BPS_BANKS_ACC>();
            lista = usp_listar_CuentasBancarias();

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_BPS_BANKS_ACC.rdlc");

            List<BEREC_BPS_BANKS_ACC> lista = new List<BEREC_BPS_BANKS_ACC>();
            lista = usp_listar_CuentasBancarias();

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

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

namespace SGRDA.MVC.Controllers
{
    public class IMPUESTOSController : Base
    {
        //
        // GET: /IMPUESTOS/
        string auxTIS_N = "";
        public static string busqueda;
        public static decimal territorio;
        
        public const string nomAplicacion = "SRGDA";
   
        public ActionResult Index()
        {
            Init(false);//add sysseg
            return View();
        }

        public List<BEREC_TAXES> usp_listar_Impuestos(string owner,string descripcion, decimal territorio)
        {
            return new BLREC_TAXES().REC_TAXES_GET(owner,descripcion, territorio);
        }

        public JsonResult usp_listar_ImpuestoJson(int skip, int take, int page, int pageSize, string group, string dato,decimal territorio, int st)
        {
            Init();//add sysseg
            var lista = ImpuestosListarPag(GlobalVars.Global.OWNER, dato, territorio, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEREC_TAXES { RECTAXES = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_TAXES { RECTAXES = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREC_TAXES> ImpuestosListarPag(string owner, string dato,decimal territorio, int st, int pagina, int cantRegxpag)
        {
            return new BLREC_TAXES().usp_REC_GET_TAXES_Page(owner,dato,territorio, st, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            listaTerritorio();

            ViewData["ListaTerritorio"] = codTer;
            return View();
        }

        public static string codTer = "";

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm, BEREC_TAXES ent)
        {
            var valid = true;
            BEREC_TAXES en = new BEREC_TAXES();
            en.OWNER = GlobalVars.Global.OWNER;
            en.TAX_ID = Convert.ToDecimal(frm["TAX_ID"]);
            en.TIS_N = Convert.ToDecimal(frm["Lista_Territorio"]);
            en.TAX_COD = frm["TAX_COD"];
            en.DESCRIPTION = frm["DESCRIPTION"];
            en.TAX_CACC = Convert.ToDecimal(frm["TAX_CACC"]);
            en.LOG_USER_CREAT = UsuarioActual;
            //en.START = Convert.ToDateTime(frm["datepickervigenia"]);
            en.START = ent.START;

            if (valid == true)
            {
                bool std = new BLREC_TAXES().REC_TAXES_Ins(en);

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
            listaTerritorio();

            return View();
        }

        public static string cod = "";
        public ActionResult Edit(decimal id = 0)
        {
            Init(false);//add sysseg
            BEREC_TAXES impuesto = new BEREC_TAXES();
            var lista = new BLREC_TAXES().REC_TAXES_GET_by_TAX_ID(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    impuesto.OWNER = item.OWNER;
                    impuesto.TAX_ID = item.TAX_ID;
                    impuesto.TIS_N = item.TIS_N;
                    impuesto.TAX_COD = item.TAX_COD;
                    impuesto.DESCRIPTION = item.DESCRIPTION;
                    impuesto.TAX_CACC = item.TAX_CACC;
                    impuesto.NAME_TER = item.NAME_TER;
                    cod = item.TIS_N.ToString();
                    listaTerritorio();
                }
            }
            return View(impuesto);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm)
        {
            var valid = ModelState.IsValid;
            BEREC_TAXES en = new BEREC_TAXES();
            en.OWNER = GlobalVars.Global.OWNER;
            en.TAX_ID = Convert.ToDecimal(frm["TAX_ID"]);
            en.TIS_N = Convert.ToDecimal(frm["Lista_Territorio"]);
            en.TAX_COD = frm["TAX_COD"];
            en.DESCRIPTION = frm["DESCRIPTION"];
            en.TAX_CACC = Convert.ToDecimal(frm["TAX_CACC"]);
            en.LOG_USER_UPDATE = UsuarioActual;

            if (valid == true)
            {
                bool std = new BLREC_TAXES().REC_TAXES_Upd_by_TAX_ID(en);

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

        //public ActionResult Eliminar(FormCollection frm)
        //{
        //    char[] delimiterChars = { ',' };
        //    string id = frm["acep"];
        //    string[] cadena = id.Split(delimiterChars);
        //    foreach (string s in cadena)
        //    {
        //        int std = new BLREC_TAXES().REC_TAXES_Del_by_TAX_ID(Convert.ToDecimal(s));

        //        if (std == 1)
        //        {
        //            TempData["msg"] = "Actualizado Correctamente";
        //            TempData["class"] = "alert alert-success";
        //        }
        //        else
        //        {
        //            TempData["msg"] = "Ocurrio un inconveniente, no se pudo Actualizar";
        //            TempData["class"] = "alert alert-danger";
        //        }
        //    }
        //    return Redirect("Index");
        //}

        [HttpPost]
        public ActionResult Eliminar(List<BEREC_TAXES> dato)
        {
            Init(false);//add sysseg
            foreach (var item in dato)
            {
                var impuesto = new BEREC_TAXES()
                {
                    TAX_ID = item.TAX_ID
                };
                bool std = new BLREC_TAXES().REC_TAXES_Del_by_TAX_ID(item.TAX_ID);
            }
            return RedirectToAction("Index");
        }

        public ActionResult ReportViewer()
        {
            return View();
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_TAXES.rdlc");

            List<BEREC_TAXES> lista = new List<BEREC_TAXES>();
            lista = usp_listar_Impuestos(GlobalVars.Global.OWNER,"",-1);

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

        public JsonResult Reporte(string busqueda, decimal territorio)
        {
            Init();//add sysseg
            PasarValores(busqueda, territorio);
            Resultado retorno = new Resultado();
            try
            {
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public void PasarValores(string busq, decimal ter)
        {
            busqueda = busq;
            territorio = ter;
        }

        public ActionResult DownloadReport(string format)
        {
            Init(false);//add sysseg
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_TAXES.rdlc");

            List<BEREC_TAXES> lista = new List<BEREC_TAXES>();
            lista = usp_listar_Impuestos(GlobalVars.Global.OWNER, busqueda, territorio);

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

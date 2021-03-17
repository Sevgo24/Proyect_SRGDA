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
    public class CARACTERISTICASASIGNADASSUBDIVISIONController : Base
    {
        //
        // GET: /CARACTERISTICASASIGNADASSUBDIVISION/      
        string auxDAC_ID = "";
        string auxDAD_TYPE = "";
        string auxDAD_STYPE = "";
        public ActionResult Index()
        {
            Init(false);//add sysseg
            var lista = CaracteristicaAsignadasubDivisionListarPag("", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<BEREF_DIV_STYPE_CHAR> usp_listarCaracteristicaAsignadasubDivision()
        {
            return new BLREF_DIV_STYPE_CHAR().usp_Get_REF_DIV_STYPE_CHAR();
        }

        public JsonResult usp_listar_CaracteriticaAsigSubDivisionJson(int skip, int take, int page, int pageSize, string group, string dato)
        {
            Init();//add sysseg
            var lista = CaracteristicaAsignadasubDivisionListarPag(dato, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            return Json(new BEREF_DIV_STYPE_CHAR { REFDIVSTYPECHAR = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
        }

        public List<BEREF_DIV_STYPE_CHAR> CaracteristicaAsignadasubDivisionListarPag(string dato, int pagina, int cantRegxpag)
        {
            return new BLREF_DIV_STYPE_CHAR().usp_REF_DIV_STYPE_CHAR_Page(dato, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            listaTiposDivisiones();
            listaSubTiposDivisiones();
            listaCaracterísticasDivisiones();

            ViewData["TiposDivisiones"] = TipoDiv;
            ViewData["CaracterísticasDivisiones"] = CararcteristicaDiv;
            ViewData["SubTiposDivisiones"] = SubTipoDivs;

            return View();
        }
            
        public static string CararcteristicaDiv = "";
        public static string TipoDiv = "";
        public static string SubTipoDivs = "";

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            var valid = ModelState.IsValid;

            BEREF_DIV_STYPE_CHAR en = new BEREF_DIV_STYPE_CHAR();
            en.LOG_USER_CREAT = "USERCREAT";
            en.OWNER = frm["OWNER"];
            en.DAC_ID = frm["lista_CaracterísticasDivisiones"];
            en.DAD_TYPE = frm["Lista_TiposDivisiones"];
            en.DAD_STYPE = frm["Lista_SubTiposDivisiones"];

            if (valid == true)
            {
                bool std = new BLREF_DIV_STYPE_CHAR().REF_DIV_STYPE_CHAR_Ins(en);

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
            listaSubTiposDivisiones();
            listaCaracterísticasDivisiones();

            return View();
        }

        public static string cod = "";
        public static string cod1 = "";
        public static string cod2 = "";
        public ActionResult Edit(string id = "")
        {
            Init(false);//add sysseg
            BEREF_DIV_STYPE_CHAR cararAsgsubdiv = new BEREF_DIV_STYPE_CHAR();

            char[] delimiterChars = { ',' };

            string text = id;
            string[] words = text.Split(delimiterChars);

            string id1 = words[0];
            string id2 = words[1];
            string id3 = words[2];

            var lista = new BLREF_DIV_STYPE_CHAR().usp_REF_DIV_STYPE_CHAR_GET_by_Patametros(id1, id2, id3);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    cararAsgsubdiv.OWNER = item.OWNER;
                    cararAsgsubdiv.DAC_ID = item.DAC_ID;
                    cararAsgsubdiv.DAD_TYPE = item.DAD_TYPE;
                    cararAsgsubdiv.DAD_STYPE = item.DAD_STYPE;
                    cararAsgsubdiv.DAD_TNAME = item.DAD_TNAME;
                    cararAsgsubdiv.DAD_SNAME = item.DAD_SNAME;
                    cararAsgsubdiv.DESCRIPTION = item.DESCRIPTION;
                    cod = item.DAD_TYPE;
                    cod1 = item.DAD_STYPE;
                    cod2 = item.DAC_ID;
                    listaTiposDivisiones();
                    listaSubTiposDivisiones();
                    listaCaracterísticasDivisiones();
                }
            }
            return View(cararAsgsubdiv);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm)
        {
            auxDAC_ID = cod2;
            auxDAD_TYPE = cod;
            auxDAD_STYPE = cod1;

            var valid = ModelState.IsValid;
            BEREF_DIV_STYPE_CHAR en = new BEREF_DIV_STYPE_CHAR();
            en.LOG_USER_UPDATE = UsuarioActual;
            en.OWNER = frm["OWNER"];
            en.DAC_ID = frm["lista_CaracterísticasDivisiones"];
            en.DAD_TYPE = frm["Lista_TiposDivisiones"];
            en.DAD_STYPE = frm["Lista_SubTiposDivisiones"];

            if (valid == true)
            {
                bool std = new BLREF_DIV_STYPE_CHAR().REF_DIV_STYPE_CHAR_Upd(en, auxDAC_ID, auxDAD_TYPE, auxDAD_STYPE);

                if (std)
                {
                    TempData["msg"] = "Actualizado Correctamente";
                    TempData["class"] = "alert alert-success";
                    return RedirectToAction("Index");
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

        //REF_DIV_TYPE
        IEnumerable<SelectListItem> lista1;
        IEnumerable<SelectListItem> lista2;
        IEnumerable<SelectListItem> lista3;
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

        //REF_DIV_SUBTYPE
        private void listaSubTiposDivisiones()
        {
            lista2 = new BLREF_DIV_SUBTYPE().REF_DIV_SUBTYPE_Get()
             .Select(c => new SelectListItem
             {
                 Value = c.DAD_STYPE.ToString(),
                 Text = c.DAD_SNAME
             });
            ViewData["Lista_SubTiposDivisiones"] = lista2;
            ViewData["Lista_SubTiposDivisiones"] = new SelectList(lista2, "Value", "Text", cod1);
        }

        //REF_DIV_CHARAC
        private void listaCaracterísticasDivisiones()
        {
            lista3 = new BLREF_DIV_CHARAC().usp_Get_REF_DIV_CHARAC()
             .Select(c => new SelectListItem
             {
                 Value = c.DAC_ID,
                 Text = c.DESCRIPTION
             });
            ViewData["lista_CaracterísticasDivisiones"] = lista3;
            ViewData["lista_CaracterísticasDivisiones"] = new SelectList(lista3, "Value", "Text", cod2);
        }

        [HttpPost]
        public ActionResult Eliminar(List<BEREF_DIV_STYPE_CHAR> dato)
        {
            Init(false);//add sysseg

            char[] delimiterChars = { ',' };

            foreach (var item in dato)
            {
                string text = item.DAC_ID;
                string[] words = text.Split(delimiterChars);

                string DAC_ID = words[0];
                string DAD_TYPE = words[1];
                string DAD_STYPE = words[2];

                bool std = new BLREF_DIV_STYPE_CHAR().REF_DIV_STYPE_CHAR_Del(DAC_ID, DAD_TYPE, DAD_STYPE);
            }
            return RedirectToAction("Index");
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REF_DIV_STYPE_CHAR.rdlc");

            List<BEREF_DIV_STYPE_CHAR> lista = new List<BEREF_DIV_STYPE_CHAR>();
            lista = usp_listarCaracteristicaAsignadasubDivision();

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REF_DIV_STYPE_CHAR.rdlc");

            List<BEREF_DIV_STYPE_CHAR> lista = new List<BEREF_DIV_STYPE_CHAR>();
            lista = usp_listarCaracteristicaAsignadasubDivision();

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

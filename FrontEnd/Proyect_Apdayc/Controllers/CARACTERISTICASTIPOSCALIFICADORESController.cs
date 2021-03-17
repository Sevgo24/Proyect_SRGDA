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
    public class CARACTERISTICASTIPOSCALIFICADORESController : Base
    {
        //
        // GET: /CARACTERISTICASTIPOSCALIFICADORES/
        public static string cod = "";
        public static string Tiposcalificaciones = "";

        public ActionResult Index()
        {
            Init(false);//add sysseg
            var lista = caracteristicastipocalificacionesListarPag("", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<BEREC_QUALIFY_CHAR> listar_caracteristicastipocalificaciones()
        {
            return new BLREC_QUALIFY_CHAR().Get_REC_QUALIFY_CHAR();
        }

        public JsonResult usp_listar_caracteristicastipocalificacionesJson(int skip, int take, int page, int pageSize, string group, string dato)
        {
            Init();//add sysseg
            var lista = caracteristicastipocalificacionesListarPag(dato, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            return Json(new BEREC_QUALIFY_CHAR { RECQUALIFYCHAR = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
        }

        public List<BEREC_QUALIFY_CHAR> caracteristicastipocalificacionesListarPag(string dato, int pagina, int cantRegxpag)
        {
            return new BLREC_QUALIFY_CHAR().REC_QUALIFY_CHAR_Page(dato, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            listaTiposCalificaciones();
            ViewData["ListaTiposCalificaciones"] = Tiposcalificaciones;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            BEREC_QUALIFY_CHAR en = new BEREC_QUALIFY_CHAR();
            var valid = ModelState.IsValid;
            en.LOG_USER_CREAT = UsuarioActual;
            en.QUA_ID = Convert.ToDecimal(frm["Lista_TiposCalificaciones"]);
            en.DESCRIPTION = frm["DESCRIPTION"];

            if (valid == true)
            {
                bool std = new BLREC_QUALIFY_CHAR().REC_QUALIFY_CHAR_Ins(en);

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

            listaTiposCalificaciones();
            return View();
        }

        public ActionResult Edit(string id = "")
        {
            Init(false);//add sysseg
            BEREC_QUALIFY_CHAR caracteristicas = new BEREC_QUALIFY_CHAR();
            var lista = new BLREC_QUALIFY_CHAR().REC_QUALIFY_CHAR_GET_by_QUA_ID(id.Split(',')[0], Convert.ToDecimal(id.Split(',')[1]));

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    caracteristicas.OWNER = item.OWNER;
                    caracteristicas.QUA_ID = item.QUA_ID;
                    caracteristicas.QUC_ID = item.QUC_ID;
                    caracteristicas.DESCRIPTION = item.DESCRIPTION;
                    caracteristicas.DESCTIPO = item.DESCTIPO;
                    listaTiposCalificaciones();
                }
            }
            return View(caracteristicas);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm)
        {
            BEREC_QUALIFY_CHAR en = new BEREC_QUALIFY_CHAR();
            var valid = ModelState.IsValid;
            en.LOG_USER_UPDATE = UsuarioActual;
            en.QUC_ID = Convert.ToDecimal(frm["QUC_ID"]);
            en.QUA_ID = Convert.ToDecimal(frm["Lista_TiposCalificaciones"]);
            en.DESCRIPTION = frm["DESCRIPTION"];

            if (valid == true)
            {
                bool std = new BLREC_QUALIFY_CHAR().REC_QUALIFY_CHAR_Upd(en);

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
        public ActionResult Eliminar(List<BEREC_QUALIFY_CHAR> dato)
        {
            Init(false);//add sysseg
            foreach (var item in dato)
            {
                var Tipocarac = new BEREC_QUALIFY_CHAR()
                {
                    QUC_ID = item.QUC_ID
                };
                bool std = new BLREC_QUALIFY_CHAR().REC_QUALIFY_CHAR_Del(item.QUC_ID);
            }
            return RedirectToAction("Index");
        }

        IEnumerable<SelectListItem> lista1;
        private void listaTiposCalificaciones()
        {
            lista1 = new BLREC_QUALIFY_TYPE().Get_REC_QUALIFY_TYPE()
            .Select(c => new SelectListItem
            {
                Value = c.QUA_ID.ToString(),
                Text = c.DESCRIPTION
            });
            ViewData["Lista_TiposCalificaciones"] = lista1;
            ViewData["Lista_TiposCalificaciones"] = new SelectList(lista1, "Value", "Text", cod);
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_QUALIFY_CHAR.rdlc");

            List<BEREC_QUALIFY_CHAR> lista = new List<BEREC_QUALIFY_CHAR>();
            lista = listar_caracteristicastipocalificaciones();

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_QUALIFY_CHAR.rdlc");

            List<BEREC_QUALIFY_CHAR> lista = new List<BEREC_QUALIFY_CHAR>();
            lista = listar_caracteristicastipocalificaciones();

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

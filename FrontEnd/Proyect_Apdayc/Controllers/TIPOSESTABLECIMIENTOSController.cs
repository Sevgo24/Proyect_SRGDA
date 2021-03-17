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
    public class TIPOSESTABLECIMIENTOSController : Base
    {
        //
        // GET: /TIPOSESTABLECIMIENTOS/

        public static string cod = "";
        public static string actividadesEconomicas = "";
        IEnumerable<SelectListItem> lista1;

        public ActionResult Index()
        {
            Init(false);//add sysseg
            //var lista = TipoEstablecimientosListarPag("", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<BEREC_EST_TYPE> usp_listar_TipoEstablecimientos()
        {
            return new BLREC_EST_TYPE().REC_EST_TYPE_GET();
        }

        public JsonResult usp_listar_TipoEstablecimientosJson(int skip, int take, int page, int pageSize, string group, string dato, string tipo, int st)
        {
            Init();//add sysseg

            if (tipo == "0") { tipo = ""; }
            var lista = TipoEstablecimientosListarPag(dato, tipo, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEREC_EST_TYPE { RECESTTYPE = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_EST_TYPE { RECESTTYPE = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREC_EST_TYPE> TipoEstablecimientosListarPag(string dato, string tipo, int st, int pagina, int cantRegxpag)
        {
            return new BLREC_EST_TYPE().usp_REC_EST_TYPE_Page(dato, tipo, st, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);//add sysseg
            listaActividadEconomica();
            ViewData["listaActividadEconomica"] = actividadesEconomicas;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            bool estado = false;
            BEREC_EST_TYPE en = new BEREC_EST_TYPE();
            en.LOG_USER_CREAT = UsuarioActual;
            en.ECON_ID = frm["lista_ActividadEconomica"];
            en.DESCRIPTION = frm["DESCRIPTION"];
            var valid = ModelState.IsValid;
            if (!string.IsNullOrEmpty(frm["DESCRIPTION"]))
                estado = true;

            var existe = new BLREC_EST_TYPE().existeTipoEstablecimiento(GlobalVars.Global.OWNER, en.ECON_ID, en.DESCRIPTION);

            if (existe)
            {
                TempData["flag"] = 2;
                return RedirectToAction("Create");  
            }

            if (estado == true)
            {
                bool std = new BLREC_EST_TYPE().REC_EST_TYPE_Ins(en);

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
                    TempData["flag"] = 0;
                }
            }
            else
            {
                TempData["msg"] = "Ingrese los datos para el registro.";
                TempData["class"] = "alert alert-danger";
                TempData["flag"] = 0;
            }

            listaActividadEconomica();
            return View();
        }

        public ActionResult Edit(string id = "")
        {
            Init(false);//add sysseg
            BEREC_EST_TYPE tipos = new BEREC_EST_TYPE();            
            var lista = new BLREC_EST_TYPE().REC_EST_TYPE_GET_by_ESTT_ID(Convert.ToDecimal(id.Split(',')[1]));

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    tipos.OWNER = item.OWNER;
                    tipos.ESTT_ID = item.ESTT_ID;
                    tipos.ECON_ID = item.ECON_ID;
                    tipos.DESCRIPTION = item.DESCRIPTION;
                    tipos.ECON_DESC = item.ECON_DESC;
                    cod = item.ECON_ID;
                    listaActividadEconomica();
                }
            }
            return View(tipos);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection frm, BEREC_EST_TYPE en)
        {
            bool estado = false;
            BEREC_EST_TYPE ent = new BEREC_EST_TYPE();

            en.LOG_USER_UPDATE = UsuarioActual;
            en.ESTT_ID = Convert.ToDecimal(frm["ESTT_ID"]);
            ent.ECON_ID = frm["lista_ActividadEconomica"];
            en.ECON_ID = ent.ECON_ID;
            var valid = ModelState.IsValid;

            var existe = new BLREC_EST_TYPE().existeTipoEstablecimiento(GlobalVars.Global.OWNER, en.ECON_ID, en.DESCRIPTION, en.ESTT_ID);

            if (existe)
            {
                TempData["flag"] = 2;
                return RedirectToAction("Edit");  
            }

            if (valid == true)
            {
                bool std = new BLREC_EST_TYPE().REC_EST_TYPE_Upd(en);

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
                }
            }
            else
            {
                TempData["msg"] = "Ocurrio un inconveniente, vuelva a ingresar dato(s).";
                TempData["class"] = "alert alert-danger";
                TempData["flag"] = 0;
            }
            return RedirectToAction("Edit");  
                  
        }

        [HttpPost]
        public ActionResult Eliminar(List<BEREC_EST_TYPE> dato)
        {
            Init(false);//add sysseg
            foreach (var item in dato)
            {
                var impuesto = new BEREC_EST_TYPE()
                {
                    ESTT_ID = item.ESTT_ID
                };
                bool std = new BLREC_EST_TYPE().REC_EST_TYPE_Del(item.ESTT_ID);
            }
            return RedirectToAction("Index");
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

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_EST_TYPE.rdlc");

            List<BEREC_EST_TYPE> lista = new List<BEREC_EST_TYPE>();
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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_EST_TYPE.rdlc");

            List<BEREC_EST_TYPE> lista = new List<BEREC_EST_TYPE>();
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

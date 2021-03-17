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
    public class IncidenciaobraController : Base
    {
        //
        // GET: /TIPOUSO/ ---- cambiando a incidencia de obra

        public ActionResult Index()
        {
            Init(false);
            //var lista = tipousoListarPag("", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<BEREC_MOD_IMPACT> usp_listar_incidenciaObra()
        {
            return new BlREC_MOD_IMPACT().Get_REC_MOD_IMPACT();
        }

        public JsonResult usp_listar_incidenciaObraJson(int skip, int take, int page, int pageSize, string group, string dato, int st)
        {
            Init();
            var lista = incidenciaobraListarPag(dato, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEREC_MOD_IMPACT { RECUSESTYPE = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_MOD_IMPACT { RECUSESTYPE = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREC_MOD_IMPACT> incidenciaobraListarPag(string dato, int st, int pagina, int cantRegxpag)
        {
            return new BlREC_MOD_IMPACT().REC_MOD_IMPACT_Page(dato, st, pagina, cantRegxpag);
        }

        public ActionResult Create()
        {
            Init(false);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BEREC_MOD_IMPACT en)
        {
            var valid = ModelState.IsValid;
            en.LOG_USER_CREAT = UsuarioActual;

            if (valid == true)
            {
                bool std = new BlREC_MOD_IMPACT().REC_MOD_IMPACT_Ins(en);

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
            return View();
        }

        public ActionResult Edit(string id = "")
        {
            Init(false);
            BEREC_MOD_IMPACT dato = new BEREC_MOD_IMPACT();
            var lista = new BlREC_MOD_IMPACT().REC_MOD_IMPACT_GET_by_MOD_INCID(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var item in lista)
                {
                    dato.OWNER = item.OWNER;
                    dato.MOD_INCID = item.MOD_INCID;
                    dato.MOD_IDESC = item.MOD_IDESC;
                    dato.MOD_IDET = item.MOD_IDET;
                }
            }
            return View(dato);
        }

        [HttpPost]
        public ActionResult Edit(BEREC_MOD_IMPACT en)
        {
            //var valid = ModelState.IsValid;
            var valid = true;
            en.LOG_USER_UPDAT = UsuarioActual;

            if (valid == true)
            {
                bool std = new BlREC_MOD_IMPACT().REC_MOD_IMPACT_Upd(en);

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
                }
            }
            else
                TempData["class1"] = "alert alert-danger";

            return RedirectToAction("Edit");
        }

        public JsonResult Eliminar(string cod)
        {
            Init(false);
            Resultado retorno = new Resultado();
            try
            {
                BlREC_MOD_IMPACT servicio = new BlREC_MOD_IMPACT();
                var result = servicio.Eliminar(new BEREC_MOD_IMPACT
                {
                    OWNER = GlobalVars.Global.OWNER,
                    MOD_INCID = cod,
                    LOG_USER_UPDAT = UsuarioActual
                });
                retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("Incidencia de la obra", UsuarioActual, "Eliminar Incidencia de la obra", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_MOD_IMPACT.rdlc");

            List<BEREC_MOD_IMPACT> lista = new List<BEREC_MOD_IMPACT>();
            lista = usp_listar_incidenciaObra();

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
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_MOD_IMPACT.rdlc");

            List<BEREC_MOD_IMPACT> lista = new List<BEREC_MOD_IMPACT>();
            lista = usp_listar_incidenciaObra();

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

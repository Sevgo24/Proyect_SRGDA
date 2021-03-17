using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using Microsoft.Reporting.WebForms;

namespace Proyect_Apdayc.Controllers
{
    public class DivisionController : Base
    {
        //
        // GET: /Division/

        public ActionResult Index()
        {
            var lista = usp_Get_DivisionPage("", 1, 5);
            return View();
        }

        public List<BEDivision> usp_listar_divisionvalues()
        {
            return new BLDivision().Listar();
        }

        public JsonResult usp_listar_DivisionJson(int skip, int take, int page, int pageSize, string group, string dato)
        {
            Resultado retorno = new Resultado();

            var lista = usp_Get_DivisionPage(dato, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEDivision { Div = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEDivision { Div = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEDivision> usp_Get_DivisionPage(string param, int pagina, int cantRegxPag)
        {
            return new BLDivision().usp_Get_DivisionPage(param, pagina, cantRegxPag);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        private static void validacion(out bool exito, out string msg_validacion, BEDivision entidad)
        {
            exito = true;
            msg_validacion = string.Empty;
            if (exito && entidad.DAD_ID == 0)
            {
                msg_validacion = "Seleccione división";
                exito = false;
            }

            if (exito && entidad.DAD_STYPE == 0)
            {
                msg_validacion = "Seleccione sub división";
                exito = false;
            }

            if (exito && string.IsNullOrEmpty(entidad.DAD_VCODE))
            {
                msg_validacion = "Ingrese código";
                exito = false;
            }

            if (exito && string.IsNullOrEmpty(entidad.DAD_BELONGS))
            {
                msg_validacion = "Ingrese su password";
                exito = false;
            }
        }

        [HttpPost]
        public JsonResult Obtiene(string id)
        {
            Resultado retorno = new Resultado();
            try
            {
                BEDivision datos = new BEDivision();
                var lista = new BLDivision().ListarPorCodigo(Convert.ToDecimal(id));

                if (lista != null)
                {
                    foreach (var item in lista)
                    {
                        datos.OWNER = item.OWNER;
                        datos.DADV_ID = item.DADV_ID;
                        datos.DAD_ID = item.DAD_ID;
                        datos.DAD_NAME = item.DAD_NAME;
                        datos.DAD_STYPE = item.DAD_STYPE;
                        datos.DAD_SNAME = item.DAD_SNAME;
                        datos.DAD_VCODE = item.DAD_VCODE;
                        datos.DAD_VNAME = item.DAD_VNAME;
                        datos.DAD_BELONGS = item.DAD_BELONGS;
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "No se ha encontrado el usuario";
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtiene", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insertar(BEDivision entidad)
        {
            bool exito = true;
            Resultado retorno = new Resultado();
            string msg_validacion = "";
            try
            {
                validacion(out exito, out msg_validacion, entidad);
                if (exito)
                {
                    var servicio = new BLDivision();


                    if (entidad.DADV_ID == 0)
                    {
                        entidad.LOG_USER_CREAT = "USERCREAT";
                        servicio.Insertar(entidad);
                    }
                    else
                    {
                        entidad.LOG_USER_UPDATE = "USERCREAT";
                        servicio.Update(entidad);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = msg_validacion;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "insertar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar(decimal codigo)
        {
            Resultado retorno = new Resultado();
            try
            {
                var servicio = new BLDivision();
                servicio.Eliminar(codigo);
                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "elimina", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REF_DIVISIONES_VALUES.rdlc");

            List<BEDivision> lista = new List<BEDivision>();
            lista = usp_listar_divisionvalues();

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REF_DIVISIONES_VALUES.rdlc");

            List<BEDivision> lista = new List<BEDivision>();
            lista = usp_listar_divisionvalues();

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

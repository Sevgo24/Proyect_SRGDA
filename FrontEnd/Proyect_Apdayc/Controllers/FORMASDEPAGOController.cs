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
    public class FORMASDEPAGOController : Base
    {
        //
        // GET: /FORMASDEPAGO/

        public ActionResult Index()
        {
            Init(false);//add sysseg
            //var lista = FormasPagoListarPag("", 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            return View();
        }

        public ActionResult Edit()
        {
            Init(false);
            return View();
        }

        public List<BEREC_PAYMENT_TYPE> usp_listar_FormasPago()
        {
            return new BLREC_PAYMENT_TYPE().Get_REC_PAYMENT_TYPE();
        }

        public JsonResult usp_listar_FormasPagoJson(int skip, int take, int page, int pageSize, string group, string dato, int st)
        {
            Init();//add sysseg
            var lista = FormasPagoListarPag(dato, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEREC_PAYMENT_TYPE { RECPAYMENTTYPE = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_PAYMENT_TYPE { RECPAYMENTTYPE = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREC_PAYMENT_TYPE> FormasPagoListarPag(string dato, int st, int pagina, int cantRegxpag)
        {
            return new BLREC_PAYMENT_TYPE().REC_PAYMENT_TYPE_Page(dato, st, pagina, cantRegxpag);
        }
        
        [HttpPost]
        public JsonResult Obtiene(string id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEREC_PAYMENT_TYPE tipo = new BEREC_PAYMENT_TYPE();
                    var lista = new BLREC_PAYMENT_TYPE().REC_PAYMENT_TYPE_by_PAY_ID(id);

                    if (lista != null)
                    {
                        foreach (var item in lista)
                        {
                            tipo.PAY_ID = item.PAY_ID;
                            tipo.DESCRIPTION = item.DESCRIPTION;
                            tipo.PAY_DATE_FIX = item.PAY_DATE_FIX;
                            tipo.PAY_DATE_FIX_DAY = item.PAY_DATE_FIX_DAY;
                            tipo.VTO1 = item.VTO1;
                            tipo.VTO2 = item.VTO2;
                            tipo.VTO3 = item.VTO3;
                            tipo.VTO4 = item.VTO4;
                            tipo.VTO5 = item.VTO5;
                            tipo.VTO6 = item.VTO6;
                            tipo.PAY_BANK = item.PAY_BANK;
                            tipo.PAY_BANK_RECEIPT = item.PAY_BANK_RECEIPT;
                            tipo.PAY_AGE_RECEIPT = item.PAY_AGE_RECEIPT;
                            tipo.PAY_TRANSFER = item.PAY_TRANSFER;
                        }

                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private static void validacion(out bool exito, out string msg_validacion, BEREC_PAYMENT_TYPE entidad)
        {
            exito = true;
            msg_validacion = string.Empty;

            if (exito && string.IsNullOrEmpty(entidad.PAY_ID))
            {
                msg_validacion = "Ingrese un código";
                exito = false;
            }

            if (exito && string.IsNullOrEmpty(entidad.DESCRIPTION))
            {
                msg_validacion = "Ingrese una descripción";
                exito = false;
            }
        }

        [HttpPost]
        public JsonResult Insertar(BEREC_PAYMENT_TYPE entidad)
        {
            bool exito = true;
            Resultado retorno = new Resultado();
            string msg_validacion = "";
            try
            {
                if (!isLogout(ref retorno))
                {
                    validacion(out exito, out msg_validacion, entidad);
                    if (exito)
                    {
                        var servicio = new BLREC_PAYMENT_TYPE();
                        entidad.OWNER = GlobalVars.Global.OWNER;
                        entidad.LOG_USER_CREAT = UsuarioActual;
                        servicio.REC_PAYMENT_TYPE_Ins(entidad);

                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = msg_validacion;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "INSERTAR", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Actualizar(BEREC_PAYMENT_TYPE entidad)
        {
            bool exito = true;
            Resultado retorno = new Resultado();
            string msg_validacion = "";
            try
            {
                if (!isLogout(ref retorno))
                {
                    validacion(out exito, out msg_validacion, entidad);
                    if (exito)
                    {
                        var servicio = new BLREC_PAYMENT_TYPE();

                        entidad.OWNER = GlobalVars.Global.OWNER;
                        entidad.LOG_USER_UPDATE = UsuarioActual;
                        servicio.REC_PAYMENT_TYPE_Upd(entidad);

                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = msg_validacion;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "update", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar(string codigo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLREC_PAYMENT_TYPE servicio = new BLREC_PAYMENT_TYPE();
                    var result = servicio.Eliminar(new BEREC_PAYMENT_TYPE
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        PAY_ID = codigo,
                        LOG_USER_UPDATE = UsuarioActual,
                    });

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                }
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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_PAYMENT_TYPE.rdlc");

            List<BEREC_PAYMENT_TYPE> lista = new List<BEREC_PAYMENT_TYPE>();
            lista = usp_listar_FormasPago();

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_PAYMENT_TYPE.rdlc");

            List<BEREC_PAYMENT_TYPE> lista = new List<BEREC_PAYMENT_TYPE>();
            lista = usp_listar_FormasPago();

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

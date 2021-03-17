using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;

namespace Proyect_Apdayc.Controllers
{
    public class RangoMorosidadController : Base
    {
        //
        // GET: /RangoMorosidad/

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            return View();
        }

        public JsonResult Listar_PageJson_RangoMorosidad(int skip, int take, int page, int pageSize, string group, string parametro, int st)
        {
            Resultado retorno = new Resultado();

            var lista = Listar_Page_RangoMorosidad(parametro, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BERangoMorosidad { RangoMorosidad = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BERangoMorosidad { RangoMorosidad = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BERangoMorosidad> Listar_Page_RangoMorosidad(string parametro, int st, int pagina, int cantRegxPag)
        {
            return new BLRangoMorosidad().Listar_Page_Rango_Morosidad(parametro, st, pagina, cantRegxPag);
        }

        private static void validacion(out bool exito, out string msg_validacion, BERangoMorosidad entidad)
        {
            exito = true;
            msg_validacion = string.Empty;

            if (exito && string.IsNullOrEmpty(entidad.DESCRIPTION))
            {
                msg_validacion = "Ingrese una descripción";
                exito = false;
            }
        }

        [HttpPost]
        public JsonResult Insertar(BERangoMorosidad entidad)
        {
            bool exito = true;
            Resultado retorno = new Resultado();
            string msg_validacion = "";
            try
            {
                if (!isLogout(ref retorno))
                {
                    //validacion(out exito, out msg_validacion, entidad);
                    if (exito)
                    {
                        var servicio = new BLRangoMorosidad();

                        if (entidad.RANGE_COD == 0)
                        {
                            entidad.LOG_USER_CREAT = UsuarioActual;
                            servicio.Insertar(entidad);
                        }
                        else
                        {
                            entidad.LOG_USER_UPDATE = UsuarioActual;
                            servicio.Actualizar(entidad);
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
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "insertar rango", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Obtiene(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BERangoMorosidad tipo = new BERangoMorosidad();
                    var lista = new BLRangoMorosidad().Obtener(id);

                    if (lista != null)
                    {
                        foreach (var item in lista)
                        {
                            tipo.RANGE_COD = item.RANGE_COD;
                            tipo.DESCRIPTION = item.DESCRIPTION;
                            tipo.RANGE_FROM = item.RANGE_FROM;
                            tipo.RANGE_TO = item.RANGE_TO;
                        }
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado el Código postal";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtiene rango", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar(decimal codigo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var servicio = new BLRangoMorosidad();
                    var tipo = new BERangoMorosidad();
                    tipo.OWNER = GlobalVars.Global.OWNER;
                    tipo.RANGE_COD = codigo;
                    tipo.LOG_USER_UPDATE = UsuarioActual;
                    servicio.Eliminar(tipo);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "elimina el rango", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
                
        public ActionResult DownloadReport(string format)
        {
            Init(false);//add sysseg
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_DEBTORS_RANGE.rdlc");

            List<BERangoMorosidad> lista = new List<BERangoMorosidad>();
            lista = new BLRangoMorosidad().Listar(GlobalVars.Global.OWNER);

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

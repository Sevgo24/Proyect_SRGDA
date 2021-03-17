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
    public class DOCUMENTOSTIPOController : Base
    {
        //
        // GET: /DOCUMENTOSTIPO/
        const string nomAplicacion = "SGRDA";

        public List<BEREC_DOCUMENT_TYPE> usp_listar_DocumentosTipo()
        {
            return new BLREC_DOCUMENT_TYPE().REC_DOCUMENT_TYPE_GET();
        }

        public JsonResult usp_listar_DocumentosTipoJson(int skip, int take, int page, int pageSize, string group, string dato, int st)
        {
            Init();//add sysseg
            var lista = DocumentosTipoListarPag(dato, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEREC_DOCUMENT_TYPE { RECDOCUMENTTYPE = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_DOCUMENT_TYPE { RECDOCUMENTTYPE = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREC_DOCUMENT_TYPE> DocumentosTipoListarPag(string dato, int st, int pagina, int cantRegxpag)
        {
            return new BLREC_DOCUMENT_TYPE().ListarPage(dato, st, pagina, cantRegxpag);
        }

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

        public JsonResult Obtiene(string id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLREC_DOCUMENT_TYPE servicio = new BLREC_DOCUMENT_TYPE();
                    var item = servicio.Obtener(GlobalVars.Global.OWNER, Convert.ToDecimal(id));

                    if (item != null)
                    {
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(item, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado datos tipo dirección";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "obtener datos de tipo dirección", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insertar(BEREC_DOCUMENT_TYPE en)
        {
            Boolean resultado = true;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEREC_DOCUMENT_TYPE obj = new BEREC_DOCUMENT_TYPE();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.DOC_TYPE = en.DOC_TYPE;
                    obj.DOC_DESC = en.DOC_DESC;
                    obj.DOC_OBSERV = en.DOC_OBSERV;
                    obj.ESTADO = en.ESTADO;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    obj.LOG_USER_UPDATE = UsuarioActual;

                    ///INICIO DE VALIDACION PARA EL INSERT Y UPDATE DE TIPO DE OBSERVACIÓN
                    if (en.DOC_TYPE == 0)
                    {
                        var existeTipo = new BLREC_DOCUMENT_TYPE().existeTipoDocumento(GlobalVars.Global.OWNER, en.DOC_DESC);
                        if (existeTipo)
                        {
                            retorno.message = "El Tipo de Documento ya existe.";
                            retorno.result = 0;
                            resultado = false;
                            return Json(retorno, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        var existeTipo = new BLREC_DOCUMENT_TYPE().existeTipoDocumento(GlobalVars.Global.OWNER, en.DOC_TYPE, en.DOC_DESC);
                        if (existeTipo)
                        {
                            retorno.message = "El Tipo de Documento ya existe.";
                            retorno.result = 0;
                            resultado = false;
                            return Json(retorno, JsonRequestBehavior.AllowGet);
                        }
                    }
                    ///FIN DE VALIDACION PARA EL INSERT Y UPDATE DE TIPO DE OBSERVACIÓN

                    if (obj.DOC_TYPE == 0)
                    {
                        var datos = new BLREC_DOCUMENT_TYPE().Insertar(obj);
                    }
                    else
                    {
                        var datos = new BLREC_DOCUMENT_TYPE().Actualizar(obj);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "insert valor tipo dirección", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLREC_DOCUMENT_TYPE servicio = new BLREC_DOCUMENT_TYPE();
                    var result = servicio.Eliminar(new BEREC_DOCUMENT_TYPE
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        DOC_TYPE = Id,
                        LOG_USER_UPDATE = UsuarioActual
                    });
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar tipo de dirección", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }        

        public FileContentResult GenerateAndDisplayReport(string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_DOCUMENT_TYPE.rdlc");

            List<BEREC_DOCUMENT_TYPE> lista = new List<BEREC_DOCUMENT_TYPE>();
            lista = usp_listar_DocumentosTipo();

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_DOCUMENT_TYPE.rdlc");

            List<BEREC_DOCUMENT_TYPE> lista = new List<BEREC_DOCUMENT_TYPE>();
            lista = usp_listar_DocumentosTipo();

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

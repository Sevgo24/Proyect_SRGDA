using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;

namespace Proyect_Apdayc.Controllers
{
    public class TIPOSDIRECCIONESController : Base
    {
        //
        // GET: /TIPOSDIRECCIONES/
        const string nomAplicacion = "SGRDA";

        public ActionResult Index()
        {
            Init(false);//add sysseg
            //var lista = TiposDireccionesListarPag("", 0, 1, GlobalVars.Global.tamanioPaginacion);
            return View();
        }

        public List<BEREF_ADDRESS_TYPE> usp_listar_TipoDirecciones()
        {
            return new BLREF_ADDRESS_TYPE().ListarDirecciones();
        }

        public JsonResult usp_listar_TiposDireccionesJson(int skip, int take, int page, int pageSize, string group, string dato, int st)
        {
            Init();//add sysseg
            var lista = TiposDireccionesListarPag(dato, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEREF_ADDRESS_TYPE { REFADDRESSTYPE = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREF_ADDRESS_TYPE { REFADDRESSTYPE = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREF_ADDRESS_TYPE> TiposDireccionesListarPag(string dato, int st, int pagina, int cantRegxpag)
        {
            return new BLREF_ADDRESS_TYPE().ListarPage(dato, st, pagina, cantRegxpag);
        }

        public ActionResult Nuevo()
        {
            Init(false);//add sysseg
            return View();
        }

        public ActionResult Edit()
        {
            Init(false);//add sysseg
            return View();
        }

        public JsonResult Obtiene(string id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLREF_ADDRESS_TYPE servicio = new BLREF_ADDRESS_TYPE();
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
        public JsonResult Insertar(BEREF_ADDRESS_TYPE en)
        {
            Resultado retorno = new Resultado();
            Boolean resultado = true;
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEREF_ADDRESS_TYPE obj = new BEREF_ADDRESS_TYPE();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.ADDT_ID = en.ADDT_ID;
                    obj.DESCRIPTION = en.DESCRIPTION;
                    obj.ADDT_OBSERV = en.ADDT_OBSERV;
                    obj.ESTADO = en.ESTADO;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    obj.LOG_USER_UPDATE = UsuarioActual;

                    ///INICIO DE VALIDACION PARA EL INSERT Y UPDATE DE TIPO DE OBSERVACIÓN
                    if (en.ADDT_ID == 0)
                    {
                        var existeTipo = new BLREF_ADDRESS_TYPE().existeTipoDirecciones(GlobalVars.Global.OWNER, en.DESCRIPTION);
                        if (existeTipo)
                        {
                            retorno.message = "El Tipo de Dirección ya existe.";
                            retorno.result = 0;
                            resultado = false;
                            return Json(retorno, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        var existeTipo = new BLREF_ADDRESS_TYPE().existeTipoDirecciones(GlobalVars.Global.OWNER, en.ADDT_ID, en.DESCRIPTION);
                        if (existeTipo)
                        {
                            retorno.message = "El Tipo de Dirección ya existe.";
                            retorno.result = 0;
                            resultado = false;
                            return Json(retorno, JsonRequestBehavior.AllowGet);
                        }
                    }
                    ///FIN DE VALIDACION PARA EL INSERT Y UPDATE DE TIPO DE OBSERVACIÓN

                    
                    if (obj.ADDT_ID == 0)
                    {
                        var datos = new BLREF_ADDRESS_TYPE().Insertar(obj);
                    }
                    else
                    {
                        var datos = new BLREF_ADDRESS_TYPE().Actualizar(obj);
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
                    

                    var tipo = new BEREF_ADDRESS_TYPE();
                    tipo.OWNER = GlobalVars.Global.OWNER;
                    tipo.ADDT_ID = Id;
                    tipo.LOG_USER_UPDATE = UsuarioActual;

                    var servicio = new BLREF_ADDRESS_TYPE().Eliminar(tipo);
                    if (servicio == 1)
                    {
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "El registro no se puede habilitar.";
                    }
                    //servicio.Eliminar(tipo);
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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REF_ADDRESS_TYPE.rdlc");

            List<BEREF_ADDRESS_TYPE> lista = new List<BEREF_ADDRESS_TYPE>();
            lista = usp_listar_TipoDirecciones();

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
            localReport.ReportPath = Server.MapPath("~/Reportes/R_REF_ADDRESS_TYPE.rdlc");

            List<BEREF_ADDRESS_TYPE> lista = new List<BEREF_ADDRESS_TYPE>();
            lista = usp_listar_TipoDirecciones();

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

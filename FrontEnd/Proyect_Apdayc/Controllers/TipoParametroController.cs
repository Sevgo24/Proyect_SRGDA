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
    public class TipoParametroController : Base
    {
        //
        // GET: /TipoParametro/

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

        public JsonResult Listar_PageJson_TipoParametro(int skip, int take, int page, int pageSize, string group, string parametro, int st)
        {
            Resultado retorno = new Resultado();

            var lista = Listar_Page_TipoParametro(parametro, st, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BETipoParametro { TipoParametro = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BETipoParametro { TipoParametro = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BETipoParametro> Listar_Page_TipoParametro(string parametro, int st, int pagina, int cantRegxPag)
        {
            return new BLTipoParametro().Listar_Page_TipoParametro(parametro, st, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Obtiene(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BETipoParametro tipo = new BETipoParametro();
                    var lista = new BLTipoParametro().Obtener_Parametro(GlobalVars.Global.OWNER, id);

                    if (lista != null)
                    {
                        foreach (var item in lista)
                        {
                            tipo.OWNER = item.OWNER;
                            tipo.PAR_TYPE = item.PAR_TYPE;
                            tipo.PAR_DESC = item.PAR_DESC;
                            tipo.PAR_OBSERV = item.PAR_OBSERV;
                            tipo.ENDS = item.ENDS;
                        }

                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado el tipo de parametro";
                    }
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

        private static void validacion(out bool exito, out string msg_validacion, BETipoParametro entidad)
        {
            exito = true;
            msg_validacion = string.Empty;

            if (exito && string.IsNullOrEmpty(entidad.PAR_DESC))
            {
                msg_validacion = "Ingrese una descripción";
                exito = false;
            } 
        }

        [HttpPost]
        public JsonResult Insertar(BETipoParametro entidad)
        {
            bool exito = true;
            Boolean resultado = true;
            Resultado retorno = new Resultado();
            string msg_validacion = "";
            try
            {
                if (!isLogout(ref retorno))
                {
                    validacion(out exito, out msg_validacion, entidad);
                    if (exito)
                    {
                        ///INICIO DE VALIDACION PARA EL INSERT Y UPDATE DE TIPO DE PARAMETRO
                        if (entidad.PAR_TYPE == 0)
                        {
                            var existeTipo = new BLTipoParametro().existeTipoParametro(GlobalVars.Global.OWNER, entidad.PAR_DESC);
                            if (existeTipo)
                            {
                                retorno.message = "El Tipo de Parámetro ya existe.";
                                retorno.result = 0;
                                resultado = false;
                                return Json(retorno, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            var existeTipo = new BLTipoParametro().existeTipoParametro(GlobalVars.Global.OWNER, entidad.PAR_TYPE, entidad.PAR_DESC);
                            if (existeTipo)
                            {
                                retorno.message = "El Tipo de Parámetro ya existe.";
                                retorno.result = 0;
                                resultado = false;
                                return Json(retorno, JsonRequestBehavior.AllowGet);
                            }
                        }
                        ///FIN DE VALIDACION PARA EL INSERT Y UPDATE DE TIPO DE PARAMETRO

                        var servicio = new BLTipoParametro();

                        if (entidad.PAR_TYPE == 0)
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "insertar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar(int codigo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var tipo = new BETipoParametro();
                    tipo.OWNER = GlobalVars.Global.OWNER;
                    tipo.PAR_TYPE = codigo;
                    tipo.LOG_USER_UPDATE = UsuarioActual;

                    var servicio = new BLTipoParametro().Eliminar(tipo);
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

        public ActionResult DownloadReport(string format)
        {
            Init(false);//add sysseg
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_TipoParametro.rdlc");

            List<BETipoParametro> lista = new List<BETipoParametro>();
            BLTipoParametro bl = new BLTipoParametro();
            lista = bl.ListarTipoParametro(GlobalVars.Global.OWNER);

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

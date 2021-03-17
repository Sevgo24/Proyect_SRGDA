using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases;
using Microsoft.Reporting.WebForms;
using System.Text;

namespace Proyect_Apdayc.Controllers.Comision
{
    public class ComisionPagoController : Base
    {
        //
        // GET: /ComisionPago/  
        public const string NomAplicacion = "SRGDA";

        private const string K_SESION_PAGOS = "___PagosComision";
        private const string K_SESION_DATOS_PAGO = "___DatosPagosComision";

        private List<BEAjustesComision> PagosComision
        {
            get
            {
                return (List<BEAjustesComision>)Session[K_SESION_PAGOS];
            }
            set
            {
                Session[K_SESION_PAGOS] = value;
            }
        }

        private BEAjustesComision DatosPagoComision
        {
            get
            {
                return (BEAjustesComision)Session[K_SESION_DATOS_PAGO];
            }
            set 
            { 
                Session[K_SESION_DATOS_PAGO] = value; 
            }
        }

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public JsonResult ListaPagoComision(int skip, int take, int page, int pageSize, decimal IdRepresentante, decimal IdNivel, decimal IdModalidad, decimal IdEstablecimiento, decimal IdLicencia, DateTime FechaIni, DateTime FechaFin)
        {
            var lista = Lista(GlobalVars.Global.OWNER, IdRepresentante, IdNivel, IdModalidad, IdEstablecimiento, IdLicencia, Convert.ToDateTime(FechaIni), Convert.ToDateTime(FechaFin), page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEAjustesComision { listaPago = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEAjustesComision { listaPago = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEAjustesComision> Lista(string owner, decimal IdRepresentante, decimal IdNivel, decimal IdModalidad, decimal IdEstablecimiento, decimal IdLicencia, DateTime FechaIni, DateTime FechaFin, int pagina, int cantRegxPag)
        {
            return new BLAjustesComision().ListarComisionPago(owner, IdRepresentante, IdNivel, IdModalidad, IdEstablecimiento, IdLicencia, FechaIni, FechaFin, pagina, cantRegxPag);
        }

        [HttpPost()]
        public JsonResult ValidacionPerfilAgenteRecaudo(decimal idAsociado)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLSocioNegocio servicio = new BLSocioNegocio();
                var datos = servicio.ObtenerDatos(idAsociado, GlobalVars.Global.OWNER);
                if (datos != null)
                {
                    if (datos.BPS_COLLECTOR.ToString() != "1")
                    {
                        retorno.result = 0;
                        retorno.message = "EL perfil del asociado no es el de Agente de Recaudo.";
                    }
                    else
                        retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ValidacionPerfil", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TotalValor(BEAjustesComision en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    en.OWNER = GlobalVars.Global.OWNER;
                    var resul = new BLAjustesComision().PagoTotal(en);
                    retorno.data = Json(resul, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "TotalValor Pago comisión", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetDatosPrePagos(List<BEAjustesComision> PrePagos)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (PrePagos != null)
                {
                    PagosComision = new List<BEAjustesComision>();
                    BEAjustesComision en;
                    foreach (BEAjustesComision item in PrePagos)
                    {
                        en = new BEAjustesComision();
                        en = new BLAjustesComision().obtenerDatosPorId(GlobalVars.Global.OWNER, item.SEQUENCE);
                        en.Pago = true;
                        PagosComision.Add(en);
                    }
                    retorno.result = 1;
                }
                else
                {
                    retorno.message = "Seleccione una o mas comisiones";
                    retorno.result = 0;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "SetDatosPrePagos", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetDatosPagos(List<BEAjustesComision> Pagos)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (Pagos != null)
                {
                    BLAjustesComision servicio = new BLAjustesComision();
                    BEAjustesComision PagoDato = DatosPagoComision;
                    PagosComision = new List<BEAjustesComision>();
                    BEAjustesComision en;
                    foreach (BEAjustesComision item in Pagos)
                    {
                        en = new BEAjustesComision();
                        en = new BLAjustesComision().obtenerDatosPorId(GlobalVars.Global.OWNER, item.SEQUENCE);
                        en.Pago = true;
                        PagosComision.Add(en);
                        en.OWNER = GlobalVars.Global.OWNER; 
                        en.PAY_ID = PagoDato.PAY_ID;
                        en.COM_PDATE = PagoDato.COM_PDATE;
                        en.COM_PNUM = PagoDato.COM_PNUM;
                        servicio.ActualizarPago(en);
                    }
                    retorno.result = 1; 
                    retorno.message = "Se realizo el pago";
                }
                else
                {
                    retorno.message = "Seleccione una o mas comisiones";
                    retorno.result = 0;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "SetDatosPagos", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadReportPrePagos()
        {
            string format = "PDF";
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_ComisionPrePagos.rdlc");

            //acá se tiene que buscar todos los que están chequeados
            List<BEAjustesComision> lista = new List<BEAjustesComision>();
            lista = PagosComision;

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            reportDataSource.Value = lista;

            localReport.DataSources.Add(reportDataSource);
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

        public ActionResult DownloadReportPagos()
        {
            string format = "PDF";
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_ComisionPagos.rdlc");

            //acá se tiene que buscar todos los que están chequeados
            List<BEAjustesComision> lista = new List<BEAjustesComision>();
            lista = PagosComision;

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            reportDataSource.Value = lista;

            localReport.DataSources.Add(reportDataSource);
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

        [HttpPost]
        public JsonResult AddPago(BEAjustesComision entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                DatosPagoComision = new BEAjustesComision();
                DatosPagoComision = entidad;
                retorno.result = 1;
                retorno.message = "OK";
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "AddPago", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

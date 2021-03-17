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
    public class ComisionPreLiquidarController : Base
    {
        //
        // GET: /ComisionPreLiquidar/ 
        public const string NomAplicacion = "SRGDA";

        private const string K_SESION_LIQUIDACION = "___Liquidacion";

        private List<BEAjustesComision> Liquidacion
        {
            get
            {
                return (List<BEAjustesComision>)Session[K_SESION_LIQUIDACION];
            }
            set
            {
                Session[K_SESION_LIQUIDACION] = value;
            }
        }

        public List<BEAjustesComision> ListaPreLiquidados = null;
        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public JsonResult ListaPreYLiquidacionComision(int skip, int take, int page, int pageSize, decimal IdRepresentante, decimal IdNivel, decimal IdModalidad, decimal IdEstablecimiento, decimal IdLicencia, DateTime FechaIni, DateTime FechaFin)
        {
            var lista = Lista(GlobalVars.Global.OWNER, IdRepresentante, IdNivel, IdModalidad, IdEstablecimiento, IdLicencia, Convert.ToDateTime(FechaIni), Convert.ToDateTime(FechaFin), page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEAjustesComision { listaPreLiquidacionComision = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEAjustesComision { listaPreLiquidacionComision = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEAjustesComision> Lista(string owner, decimal IdRepresentante, decimal IdNivel, decimal IdModalidad, decimal IdEstablecimiento, decimal IdLicencia, DateTime FechaIni, DateTime FechaFin, int pagina, int cantRegxPag)
        {
            return new BLAjustesComision().ListarPreYliquidacionComisiones(owner, IdRepresentante, IdNivel, IdModalidad, IdEstablecimiento, IdLicencia, FechaIni, FechaFin, pagina, cantRegxPag);
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
                    var resul = new BLAjustesComision().PreYLiquidacionTotal(en);
                    retorno.data = Json(resul, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "TotalValor Liquidación y Pre liquidación", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetDatos(List<BEAjustesComision> PreLiquidados)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (PreLiquidados != null)
                {
                    Liquidacion = new List<BEAjustesComision>();
                    BEAjustesComision en;
                    foreach (BEAjustesComision item in PreLiquidados)
                    {
                        en = new BEAjustesComision();
                        en = new BLAjustesComision().obtenerDatosPorId(GlobalVars.Global.OWNER, item.SEQUENCE);
                        en.Liquidacion = true;
                        Liquidacion.Add(en);
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "SetDatos", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetDatosLiquidar(List<BEAjustesComision> Liquidados)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (Liquidados != null)
                {
                    Liquidacion = new List<BEAjustesComision>();
                    BEAjustesComision en;
                    foreach (BEAjustesComision item in Liquidados)
                    {
                        en = new BEAjustesComision();
                        en = new BLAjustesComision().obtenerDatosPorId(GlobalVars.Global.OWNER, item.SEQUENCE);
                        en.Liquidacion = true;
                        Liquidacion.Add(en);
                    }
                    retorno.result = 1;

                    BLAjustesComision servicio = new BLAjustesComision();

                    var nL = Liquidados;

                    foreach (BEAjustesComision i in nL)
                    {
                        var result = servicio.ActivarLiquidacion(new BEAjustesComision
                        {
                            OWNER = GlobalVars.Global.OWNER,
                            SEQUENCE = i.SEQUENCE
                        });
                    }
                    retorno.message = "Actualización satisfactoria";
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "SetDatosLiquidar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //Obtiene las comisiones que tengan check en la lista para Liquidarlas
        //public JsonResult ObtenerLiquidar(List<BEAjustesComision> Liquidados)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (Liquidados != null)
        //        {
        //            BLAjustesComision servicio = new BLAjustesComision();

        //            var nL = Liquidados;

        //            foreach (BEAjustesComision i in nL)
        //            {
        //                var result = servicio.ActivarLiquidacion(new BEAjustesComision
        //                {
        //                    OWNER = GlobalVars.Global.OWNER,
        //                    SEQUENCE = i.SEQUENCE
        //                });
        //            }
        //            retorno.message = "Actualización satisfactoria";
        //            retorno.result = 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.result = 0;
        //        retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
        //        ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtenerComisionesLiquidar", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        //Obtiene las comisiones que tengan check en la lista para Pre Liquidar (Reporte)        
        //public JsonResult ObtenerPreLiquidar(List<BEAjustesComision> PreLiquidados)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (PreLiquidados != null)
        //        {
        //            BEAjustesComision en;
        //            ListaPreLiquidados = new List<BEAjustesComision>();
        //            foreach (BEAjustesComision item in PreLiquidados)
        //            {
        //                en = new BEAjustesComision();
        //                en = new BLAjustesComision().obtenerLiquidacionPorId(GlobalVars.Global.OWNER, item.SEQUENCE);
        //                en.Liquidacion = true;
        //                ListaPreLiquidados.Add(en);
        //            }

        //            //retorno.message = "Reporte generado";
        //            retorno.result = 1;

        //            GenerateAndDisplayReport("PDF", ListaPreLiquidados);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.result = 0;
        //        retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
        //        ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtenerComisionesPreLiquidar", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult DownloadReportPreLiquidadas()
        {
            string format = "PDF";
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_ComisionPreLiquidacion.rdlc");

            //acá se tiene que buscar todos los que están chequeados
            List<BEAjustesComision> lista = new List<BEAjustesComision>();
            lista = Liquidacion;

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DSLiquidacion";
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

        public ActionResult DownloadReportLiquidadas()
        {
            string format = "PDF";
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reportes/R_ComisionLiquidacion.rdlc");

            //acá se tiene que buscar todos los que están chequeados
            List<BEAjustesComision> lista = new List<BEAjustesComision>();
            lista = Liquidacion;

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DSLiquidacion";
            reportDataSource.Value = lista;
            localReport.DataSources.Add(reportDataSource);

            ReportParameter parametro = new ReportParameter();
            //parametro.Add(new ReportParameter("Usuario", UsuarioActual));
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

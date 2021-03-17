using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using System.Text;
using SGRDA.BL.WorkFlow;

namespace Proyect_Apdayc.Controllers
{
    public class ReporteController : Base
    {
        //
        // GET: /Reporte/
        public ActionResult Dialogs()
        {
            Init(false);
            return View();
        }
        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public const string nomAplicacion = "SRGDA";

        public JsonResult Insertar(DTOreporte entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLLicenciaReporte objServicio = new BLLicenciaReporte();
                    int resp =2;
                    BEModalidad mod = new BEModalidad();
                    mod = new BLModalidad().Obtener(GlobalVars.Global.OWNER, entidad.idModalidad);

                    var obj = new BELicenciaReporte();

                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.SHOW_ID = entidad.idShow;
                    obj.ARTIST_ID = entidad.idArtista;
                    obj.REPORT_DESC = entidad.DescPlanilla;
                    obj.REPORT_TYPE = entidad.CodigoTipoRep;
                    obj.REPORT_STATUS = 1;
                    obj.MOD_ORIG = mod.MOD_ORIG;
                    obj.MOD_SOC = mod.MOD_SOC;
                    obj.CLASS_COD = mod.CLASS_COD;
                    obj.MOG_ID = mod.MOG_ID;
                    obj.RIGHT_COD = mod.RIGHT_COD;
                    obj.MOD_INCID = mod.MOD_INCID;
                    obj.MOD_USAGE = mod.MOD_USAGE;
                    obj.MOD_REPER = mod.MOD_REPER;
                    obj.REPORT_ESOCIETY = "007";
                    obj.TIS_N = entidad.CodigoTerritorio;
                    obj.BPS_ID = entidad.idBps;
                    obj.LIC_ID = entidad.CodigoLicencia;
                    obj.EST_ID = entidad.CodigoEstablecimiento;
                    obj.LIC_PL_ID = entidad.CodigoPerFacturacion;
                    obj.REPORT_DATE_FROM = null;
                    obj.REPORT_DATE_TO = null;
                    obj.REPORT_INV = null;
                    obj.REPORT_USGD = null;
                    obj.REPORT_TIMES = null;
                    obj.REPORT_CALC = null;
                    obj.REPORT_DIST_CODE = null;

                    //------------------------------cambio para planillas
                    obj.ModUso = entidad.modUso;
                    //if (obj.ModUso == "TEM")
                    //{
                    //    obj.NMR_ID = null;
                    //    obj.REPORT_NUMBER = null;
                    //    obj.REPORT_IND = null;
                    //}
                    //else
                    //{
                    //    obj.NMR_ID = entidad.IdSerie;
                    //    obj.ARTIST_ID = null;
                    //    obj.SHOW_ID = null;
                    //    obj.REPORT_NUMBER = null;
                    //    obj.REPORT_IND = null;
                    //}
                    obj.NMR_ID = entidad.IdSerie;
                    obj.ARTIST_ID = null;
                    obj.SHOW_ID = null;
                    obj.REPORT_NUMBER = null;
                    obj.REPORT_IND = null;
                    //----------------------------------------------------
                    obj.REPORT_NUMBER_REFERENCE = entidad.NumReporteReferencia;

                    if (entidad.CodigoAutorizacion != null)
                    {
                        BLAutorizacion servAutorizacion = new BLAutorizacion();
                        var objAuto = servAutorizacion.ObtenerAutorizacionXLic(GlobalVars.Global.OWNER, obj.LIC_ID, Convert.ToDecimal(entidad.CodigoAutorizacion));
                        if (objAuto != null)
                        {
                            obj.REPORT_DATE_FROM = objAuto.LIC_AUT_START;
                            obj.REPORT_DATE_TO = objAuto.LIC_AUT_END;
                        }
                    }
                    else
                    {
                        obj.REPORT_DATE_FROM = entidad.FecDesde;
                    }

                    if (entidad.idReporte == 0)
                    {
                        obj.LOG_USER_CREAT = UsuarioActual;
                        resp= objServicio.Insertar(obj);
                    }
                    else
                    {
                        obj.REPORT_ID = entidad.idReporte;
                        obj.LOG_USER_UPDATE = UsuarioActual;
                        resp=objServicio.Actualizar(obj);
                    }

                    if (mod != null)
                    {
                        if ( mod.MOG_ID != "BAI" && mod.MOG_ID != "ESP")
                            retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                        else
                            retorno.message ="NO SE INSERTO LA PLANILLA , AGREGAR ARTISTA PARA GENERAR LA PLANILLA AUTO.";
                    }

                    if (resp == -1 )
                        retorno.message ="EL NUMERO DE PLANILLA MANUAL YA SE ENCUENTRA REGISTRADO EN OTRA LICENCIA ";
                    else if (resp==1)
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;

                    retorno.result = 1;

                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Insertar Reporte", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Eliminar(decimal id, int EsActivo)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    BLLicenciaReporte objServicio = new BLLicenciaReporte();
                    if (EsActivo == 1)
                    {

                        BELicenciaReporte entidad = new BELicenciaReporte();
                        entidad.OWNER = GlobalVars.Global.OWNER;
                        entidad.REPORT_ID = id;
                        ///al elminar falta actualizar el parama auditoria ...KENYYY!!!!
                        objServicio.Eliminar(entidad);

                    }
                    else
                    {
                        objServicio.Activar(GlobalVars.Global.OWNER, id, UsuarioActual);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Eliminar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerSeriePlanilla(decimal idLic, decimal idReport)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var report = new BLLicenciaReporte().ObtenerSeriePlanilla(GlobalVars.Global.OWNER, idLic, idReport);
                    DTOreporte obj = new DTOreporte();
                    obj.IdSerie = report.NMR_ID;
                    retorno.data = Json(obj, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerSeriePlanilla", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //Validacion si se encuentra el correlativo ya no se puede volver a editar o imprimir la planilla
        //public JsonResult ObtenerCorrelativoPLanilla(decimal idReport)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        BLREC_NUMBERING servicio = new BLREC_NUMBERING();
        //        int dato = servicio.ObtenerCorrelativoPLanilla(GlobalVars.Global.OWNER, idReport);
        //        if (dato != 0 && dato != null)
        //            retorno.result = 1;
        //        else
        //            retorno.result = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.result = 0;
        //        retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
        //        ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerCorrelativoPLanilla", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        //Validacion si se encuentra el estado de la planilla (campo REPORT_IND )en 1 ya no se puede volver a editar o imprimir la planilla
        public JsonResult ObtenerEstadoImpresionPLanilla(decimal idReport)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLLicenciaReporte servicio = new BLLicenciaReporte();
                var dato = servicio.ObtenerEstadoImpresion(GlobalVars.Global.OWNER, idReport).REPORT_IND;
                if (dato == true)
                    retorno.result = 1;
                else
                    retorno.result = 0;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerEstadoImpresionPLanilla", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //Validación si la factura esta anulada o tiene nota de credito
        public JsonResult ObtenerFactuarNotaCreditoAnulada(decimal idLic, decimal idPerFac)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLLicenciaReporte servicio = new BLLicenciaReporte();
                var dato = servicio.ObtenerFacturaNotacreditoAnulada(GlobalVars.Global.OWNER, idLic, idPerFac);

                if (dato.INV_NULL != null && dato.INV_CN_REF != 0)
                    retorno.result = 0;
                else
                    retorno.result = 1;

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerFactuarNotaCreditoAnulada", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public JsonResult ActualizarNroImpresionPlanilla(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                var upd = new BLLicenciaReporte().ActualizarNroImpresion(GlobalVars.Global.OWNER, Id, UsuarioActual);
                if (upd != 0)
                    retorno.result = 1;
                else
                    retorno.result = 0;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ActualizarNroImpresionPlanilla", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ObtenerWrkfoId(decimal idModalidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                //var id = GlobalVars.Global.WrkfoId;
                var id = new BL_WORKF_OBJECTS().ObtenerPlantilla(idModalidad);
                if (id != 0)
                {
                    retorno.result = 1;
                    retorno.valor = id.ToString();
                }
                else
                {
                    retorno.result = 0;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerWrkfoId", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult ListarReporte(decimal paramId, string ModUso)
        public JsonResult ListarReporte(decimal paramId, string ModUso,int Anio,int Mes)
        {
            Resultado retorno = new Resultado();
            try
            {
                var reporte = new BLLicenciaReporte().ListarPorLicencia(GlobalVars.Global.OWNER, paramId, ModUso,Anio,Mes);

                if (!isLogout(ref retorno))
                {
                    string xhtml = "";
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border='1' width='100%;' class='k-grid k-widget' style='border-collapse: collapse;' >");
                    shtml.Append("<thead><tr><th  class='k-header'>Planilla</th>");

                    shtml.Append("<th class='k-header'>N° Reporte</th>");
                    shtml.Append("<th class='k-header'>N° Referencia</th>");
                    shtml.Append("<th class='k-header'>Artista</th>");
                    //shtml.Append("<th class='k-header'>Tipo documento</th>");
                    //shtml.Append("<th class='k-header'>Serie</th>");
                    shtml.Append("<th class='k-header'>N° Documentos</th>");
                    shtml.Append("<th class='k-header'>Periodo</th>");
                    shtml.Append("<th class='k-header'>Importe</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");

                    shtml.Append("<th class='k-header'>Total Detalle</th>");
                    shtml.Append("<th class='k-header'>Total Ejecuciones</th>");
                    shtml.Append("<th class='k-header'>Situación</th>");
                    shtml.Append("<th class='k-header'>Impresión</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (reporte != null && reporte.Count > 0)
                    {
                        reporte.ForEach(c =>
                        {

                            var reporteDTO = new DTOreporte();
                            reporteDTO.idReporte = c.REPORT_ID;
                            reporteDTO.DescPlanilla = c.REPORT_DESC;
                            reporteDTO.NumeroDetalle = c.REPORT_USGD;
                            reporteDTO.NumeroEjecuciones = c.REPORT_TIMES;
                            reporteDTO.Activo = c.ENDS.HasValue ? false : true;
                            reporteDTO.CodigoPerFacturacion = c.LIC_PL_ID;
                            reporteDTO.ReporteCopy = c.REPORT_NCOPY;

                            reporteDTO.NumReporte = c.REPORT_NUMBER;
                            reporteDTO.NumReporteReferencia = c.REPORT_NUMBER_REFERENCE;
                            reporteDTO.NAME = c.NAME;
                            reporteDTO.TipoDocumento = c.INVT_DESC;
                            //reporteDTO.Serie = c.NMR_SERIAL;
                            reporteDTO.Documentos = c.NMR_SERIAL;
                            reporteDTO.NumFactura = c.INV_NUMBER;
                            var mont = c.LIC_MONTH.ToString().Length == 1 ? "0" + c.LIC_MONTH : c.LIC_MONTH.ToString();
                            reporteDTO.Periodo = mont + "-" + c.LIC_YEAR;
                            reporteDTO.Importe = c.INV_NET;
                            reporteDTO.Fecha = c.LOG_DATE_CREAT;


                            //xhtml = string.Format("<img class='imgPlus' src='../Images/iconos/plus.png' id='expandReport{0}' width='24px;' title='Ver Detalle de Reporte.' alt='Ver Detalle de Reporte.' onclick='verDetalle({0});'>", reporteDTO.idReporte);
                            shtml.Append("<tr class='k-grid-content'>");
                            //shtml.AppendFormat("<td style='width:15px;' >{0} </td>", xhtml);
                            shtml.AppendFormat("<td style='width:120px;text-align:left;'>{0}</td>", reporteDTO.DescPlanilla);

                            shtml.AppendFormat("<td style='width:80px;text-align:right;padding-right:15px'>{0}</td>", reporteDTO.NumReporte);
                            shtml.AppendFormat("<td style='width:80px;text-align:right;padding-right:15px'>{0}</td>", reporteDTO.NumReporteReferencia);
                            shtml.AppendFormat("<td style='width:80px;text-align:right;padding-right:15px'>{0}</td>", reporteDTO.NAME);
                            //shtml.AppendFormat("<td >{0}</td>", reporteDTO.TipoDocumento);
                            shtml.AppendFormat("<td >{0}</td>", reporteDTO.Documentos);
                            //shtml.AppendFormat("<td >{0}</td>", reporteDTO.NumFactura);
                            shtml.AppendFormat("<td style='text-align:center;'>{0}</td>", reporteDTO.Periodo);
                            //shtml.AppendFormat("<td style='text-align:right;padding-right:15px'>{0}</td>", reporteDTO.Importe==0?"": reporteDTO.Importe.ToString());
                            shtml.AppendFormat("<td style='text-align:right;padding-right:15px'>{0}</td>", reporteDTO.Importe == 0 ? "" : String.Format("{0:0.000}", reporteDTO.Importe));
                            shtml.AppendFormat("<td >{0}</td>", reporteDTO.Fecha);


                            shtml.AppendFormat("<td >{0}</td>", reporteDTO.NumeroDetalle);
                            shtml.AppendFormat("<td >{0}</td>", reporteDTO.NumeroEjecuciones);
                            shtml.AppendFormat("<td style='width:80px;text-align:center;'>{0}</td>", reporteDTO.Activo == true ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td style='width:60px;text-align:center;'>{0}</td>", reporteDTO.ReporteCopy);
                            

                            shtml.AppendFormat("<td  style=' width:90px;'> ");
                            shtml.AppendFormat("<img src='../Images/iconos/edit.png' onclick='updAddReporte({0});'  alt='Editar Reporte' title='Editar Reporte' border=0>&nbsp;&nbsp;", reporteDTO.idReporte);
                            shtml.AppendFormat("<img src='../Images/iconos/{1}' onclick='delAddReporte({0},{3});' alt='{2}' title='{2}' border=0>&nbsp;&nbsp;", reporteDTO.idReporte, reporteDTO.Activo ? "delete.png" : "activate.png", reporteDTO.Activo ? "Eliminar Reporte" : "Activar Reporte", reporteDTO.Activo == true ? 1 : 0);
                            //shtml.AppendFormat("<img src='../Images/iconos/report_deta.png' onclick='agregarDetalle({0});' alt='Agregar  Detalle' title='Agregar Detalle' border=0>&nbsp;&nbsp;", reporteDTO.idReporte);
                            shtml.AppendFormat("<img src='../Images/botones/print.png' onclick='imprimir({0},{1},{2});' height='20' alt='Imprimir Planilla' title='Imprimir Planilla' border=0>&nbsp;&nbsp;", reporteDTO.idReporte, paramId, reporteDTO.CodigoPerFacturacion);
                            shtml.AppendFormat("</td>");
                            
                            shtml.Append("</tr>");
                            shtml.Append("<tr>");
                            shtml.Append("<td></td>");
                            shtml.AppendFormat("<td colspan='12' id='tdDetalleRep_{0}' > ", reporteDTO.idReporte);
                            shtml.AppendFormat("<div id='divDetalleRep_{0}' style='display:none; width:100%;'> ", reporteDTO.idReporte);
                            shtml.Append("</div> ");
                            shtml.Append("</td>");
                            shtml.Append("<td></td>");
                            shtml.Append("</tr>");
                        });
                    }
                    else
                    {
                        shtml.Append("<tr>");
                        shtml.Append("<td></td>");
                        shtml.Append("<td colspan='13'  style='text-align:center;'> ");
                        shtml.Append("No se encontraron Reportes.");
                        shtml.Append("</td>");
                        shtml.Append("</tr>");
                    }

                    shtml.Append("</tbody></table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarReporte", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarReporteDetalle(decimal paramId)
        {
            Resultado retorno = new Resultado();
            try
            {
                var deta = new BLLicenciaReporteDeta().Listar(GlobalVars.Global.OWNER, paramId);

                if (!isLogout(ref retorno))
                {
                    //   string xhtml = "";
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget' style='border-collapse: collapse;'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='k-header'  style=' height :25px;'>Id</th>");
                    shtml.Append("<th class='k-header'>Fecha</th>");
                    shtml.Append("<th class='k-header'>Hora</th>");
                    shtml.Append("<th class='k-header'>Titulo</th>");
                    shtml.Append("<th class='k-header'>IPI</th>");
                    shtml.Append("<th class='k-header'>Autor</th>");
                    shtml.Append("<th class='k-header'>Ejecución</th>");
                    shtml.Append("<th class='k-header'>Detalle</th>");
                    shtml.Append("<th class='k-header'>Situación</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (deta != null && deta.Count > 0)
                    {
                        deta.ForEach(c =>
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", c.REPORT_DID);
                            shtml.AppendFormat("<td >{0}</td>", c.REP_DATE_EMISION.Value.ToShortDateString());
                            shtml.AppendFormat("<td >{0}</td>", c.REP_HOUR_EMISION.Value.ToLongTimeString());
                            shtml.AppendFormat("<td >{0}</td>", c.REP_TITLE);
                            shtml.AppendFormat("<td >{0}</td>", c.IPI_NAME);
                            shtml.AppendFormat("<td >{0}</td>", c.REP_AUTHOR_1);
                            shtml.AppendFormat("<td >{0}</td>", c.REP_TIMES);
                            shtml.AppendFormat("<td >{0}</td>", c.REP_CBASE);
                            shtml.AppendFormat("<td >{0}</td>", !(c.ENDS.HasValue) ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td style=' width:80px;'> ");
                            shtml.AppendFormat("<img src='../Images/iconos/edit.png' onclick='updAddReporteDeta({0},{1});'  alt='Editar Reporte' title='Editar Reporte' border=0>&nbsp;&nbsp;", c.REPORT_DID, c.REPORT_ID);
                            shtml.AppendFormat("<img src='../Images/iconos/{1}' onclick='delAddReporteDeta({0},{3},{4});' alt='{2}' title='{2}' border=0>&nbsp;&nbsp;", c.REPORT_DID, (!c.ENDS.HasValue) ? "delete.png" : "activate.png", (!c.ENDS.HasValue) ? "Eliminar Detalle Reporte" : "Activar Detalle Reporte", (!c.ENDS.HasValue) ? 1 : 0, c.REPORT_ID);
                            shtml.AppendFormat("</td>");
                            shtml.Append("</tr>");

                        });
                    }
                    else
                    {
                        shtml.Append("<tr>");
                        shtml.Append("<td></td>");
                        shtml.Append("<td colspan='9'  style='text-align:center;'> ");
                        shtml.Append("No se encontraron Detalles del Reporte Seleccionado.");
                        shtml.Append("</td>");
                        shtml.Append("</tr>");
                    }

                    shtml.Append("</tbody></table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarReporteDeta", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneReporte(decimal idLic, decimal idRep)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    var report = new BLLicenciaReporte().Obtener(GlobalVars.Global.OWNER, idRep);
                    DTOreporte objEnte = new DTOreporte();

                    objEnte.idReporte = report.REPORT_ID;
                    objEnte.DescPlanilla = report.REPORT_DESC;
                    objEnte.CodigoTerritorio = report.TIS_N;
                    objEnte.idShow = report.SHOW_ID;
                    objEnte.idArtista = report.ARTIST_ID;
                    objEnte.CodigoPerFacturacion = report.LIC_PL_ID;
                    objEnte.CodigoTipoRep = report.REPORT_TYPE;
                    objEnte.IdSerie = report.NMR_ID;
                    objEnte.NumReporteReferencia = report.REPORT_NUMBER_REFERENCE;

                    ///SET CODIGO DE AUDITORIA
                    var dato = new BLShow().ObtenerShow(GlobalVars.Global.OWNER, Convert.ToDecimal(report.SHOW_ID));
                    if (dato != null) retorno.valor = Convert.ToString(dato.LIC_AUT_ID);
                    ///FIN SET CODIGO DE AUDITORIA

                    retorno.data = Json(objEnte, JsonRequestBehavior.AllowGet);
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtieneReporte", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Registra Detalle de un reporte
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult InsertarDeta(DTOReporteDeta data)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var fechaValida = true;
                    var entidad = new BELicenciaReporteDeta();
                    try
                    {
                        entidad.REP_DATE_EMISION = Convert.ToDateTime(data.Fecha);
                    }
                    catch
                    {

                        fechaValida = false;
                    }
                    if (fechaValida)
                    {
                        BLLicenciaReporteDeta objServicio = new BLLicenciaReporteDeta();

                        entidad.OWNER = GlobalVars.Global.OWNER;
                        entidad.REPORT_ID = data.CodigoReporteCab;
                        entidad.REP_TITLE = data.Titulo;
                        entidad.REP_AUTHOR_1 = data.AutorA;
                        entidad.REP_AUTHOR_2 = data.AutorB;
                        entidad.REP_ARTIST = string.Empty;
                        entidad.REP_SHOW = data.Show;

                        entidad.REP_HOUR_EMISION = entidad.REP_DATE_EMISION;
                        entidad.REP_DUR_MIN = data.DuracionMin;
                        entidad.REP_DUR_SEC = data.DuracionSeg;
                        entidad.REP_DUR_TSEC = data.DuracionSegTotal;

                        entidad.REP_TIMES = data.TotalEjecucion;
                        entidad.REP_CBASE = data.TotalDetalle;
                        entidad.ISWC = null;
                        entidad.ISRC = null;
                        entidad.IPI_NAME = null;
                        entidad.NAME_IP = string.Empty;
                        entidad.CUR_ALPHA = data.CodigoMoneda;

                        if (data.CodigoReporteDeta == 0)
                        {
                            entidad.LOG_USER_CREAT = UsuarioActual;
                            objServicio.Insertar(entidad);
                        }
                        else
                        {
                            entidad.REPORT_DID = data.CodigoReporteDeta;
                            entidad.LOG_USER_UPDATE = UsuarioActual;
                            objServicio.Actualizar(entidad);
                        }
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                        retorno.result = 1;
                    }
                    else
                    {

                        retorno.message = "La fecha no tiene el formato correcto MM/DD/YYYY hh:mm";
                        retorno.result = 3;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Insertar Reporte", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Obtiene un detalle de Reporte en seleccion
        /// </summary>
        /// <param name="idLic"></param>
        /// <param name="idRep"></param>
        /// <returns></returns>
        public JsonResult ObtieneReporteDeta(decimal idRepDeta, decimal idRepCab)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    var report = new BLLicenciaReporteDeta().Obtener(GlobalVars.Global.OWNER, idRepDeta, idRepCab);
                    DTOReporteDeta objEnte = new DTOReporteDeta();

                    objEnte.CodigoReporteCab = report.REPORT_ID;
                    objEnte.CodigoReporteDeta = report.REPORT_DID;
                    objEnte.Titulo = report.REP_TITLE;
                    objEnte.Show = report.REP_SHOW;
                    objEnte.AutorA = report.REP_AUTHOR_1;
                    objEnte.AutorB = report.REP_AUTHOR_2;
                    objEnte.TotalDetalle = report.REP_CBASE;
                    objEnte.TotalEjecucion = report.REP_TIMES;
                    objEnte.Fecha = report.REP_DATE_EMISION.Value.ToString();
                    objEnte.CodigoMoneda = report.CUR_ALPHA;
                    objEnte.DuracionMin = report.REP_DUR_MIN;
                    objEnte.DuracionSeg = report.REP_DUR_SEC;
                    objEnte.DuracionSegTotal = report.REP_DUR_TSEC;


                    retorno.data = Json(objEnte, JsonRequestBehavior.AllowGet);
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtieneReporte", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarDeta(decimal id, int EsActivo, decimal idRepCab)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    BLLicenciaReporteDeta objServicio = new BLLicenciaReporteDeta();
                    if (EsActivo == 1)
                    {

                        BELicenciaReporteDeta entidad = new BELicenciaReporteDeta();
                        entidad.OWNER = GlobalVars.Global.OWNER;
                        entidad.REPORT_DID = id;
                        entidad.REPORT_ID = idRepCab;
                        ///al elminar falta actualizar el parama auditoria ...KENYYY!!!!
                        objServicio.Eliminar(entidad);

                    }
                    else
                    {
                        objServicio.Activar(GlobalVars.Global.OWNER, id, idRepCab, UsuarioActual);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Eliminar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult ValidaLicenciaFactCancelada(decimal LIC_ID)
        {
            Resultado retorno = new Resultado();

            try
            {
                int resp = new BLLicenciaReporteDeta().ValidaLicenciaFactCancelada(LIC_ID);

                if (resp > 0)
                    retorno.result = 1;
                else
                    retorno.result = 0;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ValidaLicenciaFactValorizada(decimal LIC_ID)
        {
            Resultado retorno = new Resultado();

            try
            {
                int resp = new BLLicenciaReporteDeta().ValidaLicenciaFactValorizada(LIC_ID);

                if (resp > 0)
                    retorno.result = 1;
                else
                    retorno.result = 0;
            }
            catch (Exception ex)
            {
                retorno.result = 1;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
    }
}

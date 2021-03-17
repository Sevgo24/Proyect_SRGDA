using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System.Xml;
using System.Text;
using System.Drawing;
using System.IO;
using System.Net;
using System.Globalization;
using SGRDA.BL.Reporte;

namespace Proyect_Apdayc.Controllers.AdministracionCobros
{
    public class AdministracionCobrosController : Base
    {
        // GET: AdministracionCobros

        private class Variables
        {
            public const string SessionListaReporteCobrosParciales = "___K_SESSION_LISTA_REPORTE_REGISTRO_VENTA ";
            public const int Si = 1;
            public const int No = 0;
            public const int Cero = 0;
            public const int Uno = 1;
            public const int Observacion = 2;
            public const bool Activo = true;
            public const bool Inactivo = false;
            public const string MensajeErrorAlListarCobros = "OCURRIO UN ERROR AL LISTAR LOS COBROS | COMUNIQUESE CON EL ADMINISTRADOR Y OTORGE LOS PARAMETROS DE BUSQUEDA";
            public const string MensjaeErrorAlMostrarDetalleSocios = "OCURRIO UN ERROR AL LISTAR LOS SOCIOS | COMUNIQUESE CON EL ADMINISTRADOR Y OTORGE EL CODIGO DE COBRO";
            public const string MensjaeErrorAlMostrarDocumentos = "OCURRIO UN ERROR AL LISTAR LOS DOCUMENTOS |  COMUNIQUESE CON EL ADMINISTRADOR Y OTORGE EL CODIGO DE COBRO";
            public const string MensajeErrorAlMostrarReporte = "OCURRIO UN ERROR AL LISTAR EL REPORTE | COMUNIQUESES CON EL ADMINISTRADOR  Y DETALLE LOS FILTROS A BUSCAR";
            public const string MensajeErrorSinDataReporte = "NO SE ENCONTRO INFORMACION CON LOS FILTROS APLICADOS";
            public const string MensajeConsultarAntesDeMostrarPdf = "REALIZE UNA CONSULTA ANTES DE MOSTRAR  PDF/EXCEL";
            public const string MensajeErrorAlActualizarCobro = "OCURRIO UN ERROR AL ACTUALIZAR EL ESTADO DEL COBRO | DETALLE EL CODIGO DEL COBRO AL ADMINISTRADOR PARA LA EVALUACION";
            public const string MensajeActualizacionEstadoCobroExitosa = "EL COBRO FUE ACUTALIZADO CORRECTAMENTE";
        }
        
        public ActionResult Index()
        {
            Session.Remove(Variables.SessionListaReporteCobrosParciales);
            Init(false);
            return View();
        }

        private List<BEAdministracionCobros> ListaReporteCobrosParciales
        {
            get
            {
                return (List<BEAdministracionCobros>)Session[Variables.SessionListaReporteCobrosParciales];
            }
            set
            {
                Session[Variables.SessionListaReporteCobrosParciales] = value;
            }
        }
        public JsonResult Listar(decimal IdCobro, string NumeroOperacion, decimal Monto, decimal IdBancoDestino, decimal IdBancoOrigen, decimal IdCuenta,
                                                                decimal IdOficina, int EstadoCobro, int EstadoConfirmacion, int ConFecha, string FechaInicial, string FechaFinal,
                                                                        decimal IdSocio, decimal IdSerie, decimal NumeroDocumento)
        {
            Resultado retorno = new Resultado();

            try
            {
                if(!isLogout (ref retorno))
                {
                    Session.Remove(Variables.SessionListaReporteCobrosParciales);

                    var lista = new BLAdministracionCobros().Listar(IdCobro, NumeroOperacion, Monto, IdBancoDestino, IdBancoOrigen, IdCuenta, IdOficina, EstadoCobro, EstadoConfirmacion,
                                                                ConFecha, FechaInicial, FechaFinal, IdSocio, IdSerie, NumeroDocumento);

                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblAdministracionCobrosCabezera' border=0 width='100%;' class='k-grid k-widget' id='tblAdministracionCobrosCabezera'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO COBRO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >NUM. REFEREN </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MONTO VOUCHER</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >SALDO PENDIENTE</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MONTO RECAUDO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >BANCO DESTINO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CUENTA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >OFICINA </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ESTADO COBRO </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ESTADO COBRO CONFIRMACION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA CONFIRMACION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ESTADO.</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >VER.</th>");
                    if (lista != null)
                    {
                        lista.ForEach(item =>
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                            shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center';> ");
                            shtml.AppendFormat("<a href=# onclick='verDetalleCobroSocio({0});'><img id='expand" + item.CodigoCobro + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.CodigoCobro);
                            shtml.Append("</td>");
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDEstOri'>{0}</td>", item.CodigoCobro);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDNomEstOri'>{0}</td>", item.CodigoReferencia);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDDivEstOri'>{0}</td>", item.MontoVoucher);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.MontoSaldoPendiente);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.MontoSaldoUsado);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.NombreBancoDestino);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DescripcionCuentaBanco);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.NombreOficinaCobro);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DescripcionEstadoCobro);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DescripcionEstadoCobroConfirmacion);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.FechaCobroConfirmacion);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='InactivarCobro({0},{3});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a></td>", item.CodigoCobro, item.EstadoCobro == Variables.Si ? "delete.png" : "activate.png", item.EstadoCobro == Variables.Si ? "Inactivar Cobro" : "Activar Cobro",item.CodigoRecCobro);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:left'; class='IDOfiEstOri'><a href=# onclick='editar({0});'><img src='../Images/iconos/report_deta.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.CodigoRecCobro, "Ver");
                            shtml.Append("</tr>");
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td style='width:100%' colspan='20'>");
                            shtml.Append("<div style='display:none;' id='" + "div" + item.CodigoCobro.ToString() + "'  > ");
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");


                            shtml.AppendFormat("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</tr>");
                        });
                    }
                    shtml.Append("</table>");
                    retorno.result = Variables.Si;
                    retorno.Code = lista.Count;
                    retorno.message = shtml.ToString();

                    retorno.result = Variables.Si;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MensajeErrorAlListarCobros;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        public JsonResult ListarCabezeraSociosCobros(decimal IdCobro)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var lista = new BLAdministracionCobros().ListarSociosCabezeraCobros(IdCobro); ;
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
                    shtml.Append("<thead>");
                    shtml.Append("<tr>");
                    shtml.Append("<th class='k-header' style='width:120px'></th>");
                    //shtml.Append("<th class='k-header' style='width:120px'>CODIGO COBRO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>SOCIO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>RUC / DNI</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>CANTIDAD DOCUMENTOS</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>VER.</th>");
                    if (lista != null)
                    {
                        lista.ForEach(item =>
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                            shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center';> ");
                            shtml.AppendFormat("<a href=# onclick='verDetalleCobroSocioDocumento({0},{1});'><img id='expandDoc" + item.CodigoCobro + item.CodigoSocioCobro.ToString()+ "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.CodigoCobro ,item.CodigoSocioCobro);
                            shtml.Append("</td>");
                            //shtml.AppendFormat("<td style='width:5%; text-align:center'; display:none class='IDCobros' padding-right:10px';>{0}</td>", item.CodigoCobro);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDNombresCobros' padding-right:10px'>{0}</td>", item.NombreyApelidosSocioCobro);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDRucCobros' padding-right:10px'>{0}</td>", item.DocumentoIdentificacionSocio);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.CantidadDocumentosxSocio);
                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='AprobarControl({0});'><img src='../Images/botones/finalizar.png' border=0 title='{1}'></a>&nbsp;&nbsp;<a href=# onclick='RechazarControl({0});'><img src='../Images/botones/error.png' border=0 title='{2}'></a>&nbsp;&nbsp;</td>", item.CodigoCobro, "Aprobar Control", "Rechazar Control");
                            shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center'; class='IDVerSociosCobros' padding-right:10px'><a href=# onclick='VerSocio({0});'><img src='../Images/iconos/report_deta.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.CodigoSocioCobro, "Ver");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");

                            shtml.Append("<tr style='background-color:white'>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td colspan='6'>");

                            shtml.Append("<div style='display:inline;' id='" + "divDoc" + item.CodigoCobro.ToString() + "-" + item.CodigoSocioCobro.ToString() + "'  > ");

                            //shtml.Append(getHtmlTableDetaLicPlanBorrador(item.codLicencia, item.codFactura));

                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");

                        });
                    }
                    shtml.Append("</table>");
                    retorno.result = Variables.Si;
                    retorno.Code = lista.Count;
                    retorno.message = shtml.ToString();

                    retorno.result = Variables.Si;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MensjaeErrorAlMostrarDetalleSocios;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult ListarCabezeraSociosDetalleCobros(decimal IdCobro,decimal IdSocio)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var lista = new BLAdministracionCobros().ListarSocioDocumentosDetalleCobros(IdCobro,IdSocio); ;
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
                    shtml.Append("<thead>");
                    shtml.Append("<tr>");
                    //shtml.Append("<th class='k-header' style='width:120px'></th>");
                    //shtml.Append("<th class='k-header' style='width:120px'>CODIGO COBRO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>SERIE</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>NUMERO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>IMPORTE</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>ESTADO DOCUMENTO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>VER.</th>");
                    if (lista != null)
                    {
                        lista.ForEach(item =>
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                            //shtml.AppendFormat("<td style='width:5%; text-align:center'; display:none class='IDCobros' padding-right:10px';>{0}</td>", item.CodigoCobro);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDNombresCobros' padding-right:10px'>{0}</td>", item.DescripcionSerie);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDRucCobros' padding-right:10px'>{0}</td>", item.NumeroFactura);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.MontoDocumento);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.EstadoDocumento);
                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='AprobarControl({0});'><img src='../Images/botones/finalizar.png' border=0 title='{1}'></a>&nbsp;&nbsp;<a href=# onclick='RechazarControl({0});'><img src='../Images/botones/error.png' border=0 title='{2}'></a>&nbsp;&nbsp;</td>", item.CodigoCobro, "Aprobar Control", "Rechazar Control");
                            shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center'; class='IDVerSociosCobros' padding-right:10px'><a href=# onclick='VerDocumento({0});'><img src='../Images/iconos/report_deta.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.CodigoDocumento, "Ver");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        });
                    }
                    shtml.Append("</table>");
                    retorno.result = Variables.Si;
                    retorno.Code = lista.Count;
                    retorno.message = shtml.ToString();

                    retorno.result = Variables.Si;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MensjaeErrorAlMostrarDocumentos;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult ReporteTipo(decimal IdCobro, string NumeroOperacion, decimal Monto, decimal IdBancoDestino, decimal IdBancoOrigen, decimal IdCuenta,
                                                                decimal IdOficina, int EstadoCobro, int EstadoConfirmacion, int ConFecha, string FechaInicial, string FechaFinal,
                                                                        decimal IdSocio, decimal IdSerie, decimal NumeroDocumento,string formato)
        {


            Resultado retorno = new Resultado();

            try
            {

                Session.Remove(Variables.SessionListaReporteCobrosParciales);
                List<BEAdministracionCobros> listar = new List<BEAdministracionCobros>();
                ListaReporteCobrosParciales = new BLAdministracionCobros().ListarReporte(IdCobro, NumeroOperacion, Monto, IdBancoDestino, IdBancoOrigen, IdCuenta, IdOficina, EstadoCobro, EstadoConfirmacion,
                                                                ConFecha, FechaInicial, FechaFinal, IdSocio, IdSerie, NumeroDocumento);


                if (ListaReporteCobrosParciales != null && ListaReporteCobrosParciales.Count > Variables.No)
                {
                    retorno.result = Variables.Si;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
                else
                {
                    retorno.result = Variables.No;
                    if(ListaReporteCobrosParciales==null)
                        retorno.message =Variables.MensajeConsultarAntesDeMostrarPdf;
                    else
                        retorno.message = Variables.MensajeErrorSinDataReporte;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MensajeErrorAlMostrarReporte;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte de Artista", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReporteListarCobrosParciales(string formato)
        {
            string format = formato;
            int oficina_id = Variables.Cero;
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            Resultado retorno = new Resultado();

            try
            {

                List<BEAdministracionCobros> lstReporte = new List<BEAdministracionCobros>();
                lstReporte = ListaReporteCobrosParciales;

                if (lstReporte.Count() > Variables.Cero && lstReporte != null)
                {
                    LocalReport localReport = new LocalReport();

                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_LISTAR_COBROS_PARC.rdlc");

                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = lstReporte;
                    localReport.DataSources.Add(reportDataSource);

                    ReportParameter parametroNomusu = new ReportParameter();
                    parametroNomusu = new ReportParameter("NombreUsuario", oficina);
                    localReport.SetParameters(parametroNomusu);

                    ReportParameter parametroNomoficina = new ReportParameter();
                    parametroNomoficina = new ReportParameter("NombreOficina", usuario);
                    localReport.SetParameters(parametroNomoficina);

                    ReportParameter fecha = new ReportParameter();
                    fecha = new ReportParameter("FechaImpresion", DateTime.Now.ToShortDateString());
                    localReport.SetParameters(fecha);

                    string reportType = format;
                    string mimeType;
                    string encoding;

                    //aqui le cambie solo dejar string fileNameExtension en caso de error
                    string fileNameExtension;

                    //CODIGO REPETIBLE
                    //The DeviceInfo settings should be changed based on the reportType            
                    //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
                    string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>" + format + "</OutputFormat>" +
                    //  "  <PageWidth>8.5in</PageWidth>" +
                    "  <PageWidth>9in</PageWidth>" +
                    //"  <PageHeight>11in</PageHeight>" +
                    "  <PageHeight>16.3in</PageHeight>" +
                    "  <MarginTop>0.0in</MarginTop>" +
                    "  <MarginLeft>0.3in</MarginLeft>" +
                    "  <MarginRight>0.0in</MarginRight>" +
                    "  <MarginBottom>0.3in</MarginBottom>" +
                    "</DeviceInfo>";

                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;

                    renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.result = Variables.Si;
                    localReport.DisplayName = "Reporte de consulta de Licencias";

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
                else
                {
                    retorno.message = Variables.MensajeErrorSinDataReporte;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No   ;
                retorno.message = Variables.MensajeErrorAlMostrarReporte;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte de Cobros Parciales", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ActualizarEstadoCobro(decimal IdCobro,decimal IdRecCobro)
        {


            Resultado retorno = new Resultado();

            try
            {
                int r = new BLAdministracionCobros().ActualizaEstadoCobro(IdCobro, IdRecCobro);

                if (r >= Variables.Si)
                {
                    retorno.result = Variables.Si;
                    retorno.message = Variables.MensajeActualizacionEstadoCobroExitosa;
                }
                else if (r == Variables.Observacion)
                {
                    retorno.result = Variables.No;
                    retorno.message = Variables.MensajeErrorAlActualizarCobro;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MensajeErrorAlActualizarCobro;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}
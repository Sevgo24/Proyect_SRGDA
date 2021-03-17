using System;
using SGRDA.BL;
using SGRDA.BL.Reporte;
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
namespace Proyect_Apdayc.Controllers.AdministracionGenerarEmisionMensual
{
    public class AdministracionEmisionMensualController : Base

    {
        // GET: AdministracionEmisionMensual
        private class Variables
        {
            public const int Si = 1;
            public const int No = 0;
            public const int Cero = 0;
            public const int Uno = 1;
            public const int Observacion = 2;
            public const int OficinaYaFacturo = 3;
            public const bool Activo = true;
            public const bool Inactivo = false;
            public const string MsjErrorListarEmisionMensual = "Ocurrio un problema al listar la Emision Mensual | Detalle la oficina y rango de busqueda al Administrador ";
            public const string MsjErrorListarLicenciaMensual = "Ocurrio un problema al listar la Licencia Mensual | Detalle la oficina y rango de busqueda al Administrador ";
            public const string MsjErrorListarLicenciaPeriodoMensual = "Ocurrio un problema al listar el Periodo de Licencia Mensual | Detalle la oficina , rango y Codigo de Licencia de busqueda al Administrador ";
            public const string MsjOkLicenciasCaractActualizadas = "Se Actualizo el calculo y valores a facturar ";
            public const string MsjOkGeneracionEmisionMensual = "La Emision se genero con exito";
            public const string MsjErrirGenerarEmisionMensual = "Ocurrio un Error al Generar La emision Mensual | Detalle la Oficina - Mes - Año al Administrador para resolver el Problema";
            public const string MsjOkLicenciaActualizada = "Se actualizo el Estado de la Licencia ";
            public const string MsjErrorAlActalizar = "Ocurrio un Error al Actualizar la licencia | Detalle el codigo de licencia que desea Actualizar al Administrador";
            public const string MsjNoSeEncuentraEnHoraEmision = "La Oficina no tiene configurada una hora de emision | La oficina ya realizo la emision , por favor de consultar Recaudacion/Consulta de Facturas";
            public const string MsjNoTieneConfiguradaHoraEmision = "La Oficina Se encuentra en hora de emision pero ya facturo | Si esto es un error contacte con el Administrador ";
        }
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListaEmisionMensual(decimal Oficina, int Mes, int Anio, int Estado, decimal CodigoLicencia, decimal CodigoSocio)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                    var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));


                    if (opcAdm == Variables.No) // SI NO PASA LA VALIDACION  SELECCIONAR LA OFICINA A LA QUE PERTENECE EL USUARIO
                        Oficina = oficina;

                    var lista = new BLEmisionMensual().ListaEmisionMensual(Oficina, Mes, Anio, Estado, CodigoLicencia, CodigoSocio);

                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblEmisionMensual' border=0 width='100%;' class='k-grid k-widget' id='tblEmisionMensual'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO SOCIO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >SOCIO </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DOCUMENTO </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >TIPO MONEDA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MONEDA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >GRUPO FACTURACION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DIRECCION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MONTO BRUTO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DESCUENTO </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MONTO NETO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CANTIDAD DE LICENCIAS</th>");

                    if (lista != null)
                    {
                        lista.ForEach(item =>
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                            shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center';> ");
                            shtml.AppendFormat("<a href=# onclick='verDetalleLicenciaSocio({0},{1},{2});'><img id='expand" + item.CodigoSocio + '-' + item.CodigoGrupoFacturacion + '-' + item.CodigoOficina + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.CodigoSocio, item.CodigoGrupoFacturacion, item.CodigoOficina);
                            shtml.Append("</td>");
                            shtml.AppendFormat("<td style='width:3%; text-align:center'; class='IDSocCod'>{0}</td>", item.CodigoSocio);
                            shtml.AppendFormat("<td style='width:6%; text-align:center'; class='IDDescSoc'>{0}</td>", item.DescripcionSocio);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDDescSoc'>{0}</td>", item.DescripcionDocumentoSocio);
                            shtml.AppendFormat("<td style='width:1%; text-align:center'; class='IDTipMon'>{0}</td>", item.DescripcionTipoMoneda);
                            shtml.AppendFormat("<td style='width:1%; text-align:center'; class='IDDescMon'>{0}</td>", item.DescripcionMoneda);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDDescGrupoFact'>{0}</td>", item.DescripcionGrupoFacturacion);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDDescDirSoc'>{0}</td>", item.DescripcionDireccionSocio);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDMontoBrutTot'>{0}</td>", item.MontoBrutoTotalSocioGrupo);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDMontoDescTot'>{0}</td>", item.MontoDesctoTotalSocioGrupo);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDMontoNetTot'>{0}</td>", item.MontoNetoTotalSocioGrupo);
                            shtml.AppendFormat("<td style='width:1%; text-align:center'; class='IDCantLicSocio'>{0}</td>", item.CantidadLicenciasSocioGrupo);
                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:left'; class='IDOfiEstOri'><a href=# onclick='editar({0},{1});'><img src='../Images/iconos/report_deta.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.CodigoSocio,item.CodigoGrupoFacturacion, "Ver");
                            shtml.Append("</tr>");
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td style='width:100%' colspan='20'>");
                            shtml.Append("<div style='display:none;' id='" + "div" + item.CodigoSocio.ToString() + '-' + item.CodigoGrupoFacturacion.ToString() + '-' + item.CodigoOficina.ToString() + "'  > ");
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
                    retorno.TotalFacturas = lista.Count;
                    retorno.valor = lista.Sum(s => s.MontoNetoTotalSocioGrupo).ToString();
                    retorno.TotalFacturasValidas = lista.Count(s => s.MontoNetoTotalSocioGrupo > Variables.Cero);
                    //retorno.Code = lista.Sum(s => s.CantidadLicenciasSocioGrupo) ;
                    retorno.message = shtml.ToString();

                    retorno.result = Variables.Si;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MsjErrorListarEmisionMensual;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        public JsonResult ListarLicenciasSociosEmision(decimal CodigoSocio, decimal CodigoGrupoFact, decimal CodigoOficina, int Mes, int Anio)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var lista = new BLEmisionMensual().ListarLicenciasEmisionMensual(CodigoSocio, CodigoGrupoFact, CodigoOficina, Mes, Anio); ;
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
                    shtml.Append("<thead>");
                    shtml.Append("<tr>");
                    shtml.Append("<th class='k-header' style='width:120px'></th>");
                    //shtml.Append("<th class='k-header' style='width:120px'>CODIGO COBRO</th>");
                    //shtml.Append("<th class='k-header' style='width:120px'>LICENCIA</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>ESTABLECIMIENTO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>DIRECCION</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>UBIGEO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>MONTO BRUTO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>DSCTO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>MONTO NETO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>VER.</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>ESTADO</th>");
                    if (lista != null)
                    {
                        lista.ForEach(item =>
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                            shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center';> ");
                            shtml.AppendFormat("<a href=# onclick='verDetallePlanificacionLicencia({0});'><img id='expandDoc" + item.CodigoLicencia + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.CodigoLicencia);
                            shtml.Append("</td>");
                            //shtml.AppendFormat("<td style='width:5%; text-align:center'; display:none class='IDCobros' padding-right:10px';>{0}</td>", item.CodigoCobro);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDNombresCobros' padding-right:10px'>{0}</td>", item.DescripcionEstablecimiento);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDRucCobros' padding-right:10px'>{0}</td>", item.DescripcionDireccionEstablecimiento);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.DescripcionUbigeo);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.MontoLicenciaBruto);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.MontoLicenciaDscto);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.MontoLicenciaNeto);
                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='AprobarControl({0});'><img src='../Images/botones/finalizar.png' border=0 title='{1}'></a>&nbsp;&nbsp;<a href=# onclick='RechazarControl({0});'><img src='../Images/botones/error.png' border=0 title='{2}'></a>&nbsp;&nbsp;</td>", item.CodigoCobro, "Aprobar Control", "Rechazar Control");
                            shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center'; class='IDVerSociosCobros' ><a href=# onclick='VerLicencia({0});'><img src='../Images/iconos/report_deta.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.CodigoLicencia, "Ver");
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='ModificarPermisoLicencias({0},{3},{4},{5});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a></td>", item.CodigoLicencia, Convert.ToInt32(item.CodigoPermiteFacturacion) == Variables.Si ? "delete.png" : "activate.png", Convert.ToInt32(item.CodigoPermiteFacturacion) == Variables.Si ? "Inactivar en la Emision" : "Activar en la  Emision", item.CodigoSocio, item.CodigoGrupoFacturacion, item.CodigoOficina);
                            shtml.Append("</td>");
                            shtml.Append("</tr>");

                            shtml.Append("<tr style='background-color:white'>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td colspan='6'>");

                            shtml.Append("<div style='display:inline;' id='" + "divDoc" + item.CodigoLicencia.ToString() + "'  > ");

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
                retorno.message = Variables.MsjErrorListarLicenciaMensual;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult ListarPlaneamientoLicenciaEmision(decimal CodigoLicencia, int Mes, int Anio)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var lista = new BLEmisionMensual().ListarLicenciasPeriodos(CodigoLicencia, Mes, Anio); ;
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
                    shtml.Append("<thead>");
                    shtml.Append("<tr>");
                    //shtml.Append("<th class='k-header' style='width:120px'></th>");
                    //shtml.Append("<th class='k-header' style='width:120px'>CODIGO COBRO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>ID LICENCIA</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>PERIODO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>BRUTO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>DESCUENTO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>NETO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>ESTADO PERIODO</th>");
                    //shtml.Append("<th class='k-header' style='width:120px'>VER.</th>");
                    if (lista != null)
                    {
                        lista.ForEach(item =>
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                            //shtml.AppendFormat("<td style='width:5%; text-align:center'; display:none class='IDCobros' padding-right:10px';>{0}</td>", item.CodigoCobro);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDNombresCobros' padding-right:10px'>{0}</td>", item.CodigoLicencia);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDRucCobros' padding-right:10px'>{0}</td>", item.DescripcionPeriodo);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.MontoLicenciaBruto);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.MontoLicenciaDscto);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.MontoLicenciaNeto);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.EstadoPeriodo);
                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='AprobarControl({0});'><img src='../Images/botones/finalizar.png' border=0 title='{1}'></a>&nbsp;&nbsp;<a href=# onclick='RechazarControl({0});'><img src='../Images/botones/error.png' border=0 title='{2}'></a>&nbsp;&nbsp;</td>", item.CodigoCobro, "Aprobar Control", "Rechazar Control");
                            //shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center'; class='IDVerSociosCobros' padding-right:10px'><a href=# onclick='VerDocumento({0});'><img src='../Images/iconos/report_deta.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.CodigoPeriodo, "Ver");
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
                retorno.message = Variables.MsjErrorListarLicenciaPeriodoMensual;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GenerarFacturacionMensual(decimal Oficina, int mes, int anio)
        {
            Resultado retorno = new Resultado();
            try
            {
                var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));


                if (opcAdm == Variables.No) // SI NO PASA LA VALIDACION  SELECCIONAR LA OFICINA A LA QUE PERTENECE EL USUARIO
                    Oficina = oficina;

                //if (opcAdm == Variables.Si && Oficina == Variables.Cero)
                //    Oficina == Variables.Cero;

                var Respuesta = new BLEmisionMensual().GenerarEmisionMensual(Oficina, mes, anio);

                if (Respuesta > Variables.Cero)
                {
                    retorno.result = Variables.Si;
                    retorno.message = Variables.MsjOkGeneracionEmisionMensual;
                }
                else
                {
                    retorno.result = Variables.No;
                    retorno.message = Variables.MsjErrirGenerarEmisionMensual;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MsjErrirGenerarEmisionMensual;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarEstadoLicenciaEmision(decimal CodigoLicencia)
        {

            Resultado retorno = new Resultado();
            try
            {
                var resp = new BLEmisionMensual().ActualizarEstadoLicenciaEmision(CodigoLicencia);

                if (resp == Variables.Si)
                {
                    retorno.result = Variables.Si;
                    retorno.message = Variables.MsjOkLicenciaActualizada;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MsjErrorAlActalizar;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region Recalcular

        public JsonResult ActualizarMontoDescuentosCaracteristicasLicencia(decimal CodigoLicencia, int mes, int year, decimal Oficina)
        {
            Resultado retorno = new Resultado();

            try
            {

                var lista = new BLEmisionMensual().ListarLicenciasPeriodosActualizar(CodigoLicencia, mes, year, Oficina);

                lista.ForEach(s => {

                    Recaudacion.FacturacionController servCalculo = new Recaudacion.FacturacionController();
                    var montos = servCalculo.obtenerMontoFactura(s.CodigoLicencia, s.CodigoPeriodo);

                    new BLLicencias().ActualizaLicenciaMontos(s.CodigoLicencia, Convert.ToDecimal(montos.ValorTarifa), Convert.ToDecimal(montos.ValorDescuento), Convert.ToDecimal(montos.ValorFinal),Convert.ToDecimal(montos.ValorDescuentoRedondeoEspecial));

                });
                retorno.result = Variables.Si;
                retorno.message = Variables.MsjOkLicenciasCaractActualizadas;
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Permisos

        public JsonResult ValidarPermisoEmisionMensual()
        {
            Resultado retorno = new Resultado();
            try
            {

                var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));
                var res = 0;

                if (opcAdm == Variables.No) //SI NO ES UNA OFICINA ADMINISTRADORA , EVALUAR SI TIENE PERMISOS PARA FACTURAR 
                {
                    res = new BLFactura().ValidarPermisoEmisionMensual(oficina);
                    if (res == Variables.Si) retorno.result = Variables.Uno;
                    if (res == Variables.No) { retorno.message = Variables.MsjNoSeEncuentraEnHoraEmision; retorno.result = Variables.No; }
                    if (res == Variables.OficinaYaFacturo) { retorno.message = Variables.MsjNoTieneConfiguradaHoraEmision; retorno.result = Variables.No; }
                }
                else // SI ES UNA OFICINA ADMINISTRADORA DEBE SALTAR LA VALIDACION
                {
                    retorno.result = Variables.Uno;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = Variables.No;
                //ucLogApp.ucLog.GrabarLogError(, UsuarioActual, "ValidarSerie", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
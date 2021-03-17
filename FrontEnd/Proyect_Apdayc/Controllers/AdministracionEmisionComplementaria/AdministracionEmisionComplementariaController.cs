using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.BL.Reporte;
using SGRDA.DA;
using SGRDA.Entities;
using Proyect_Apdayc.Clases;
using System.Text;

namespace Proyect_Apdayc.Controllers.AdministracionEmisionComplementaria
{
    public class AdministracionEmisionComplementariaController : Base
    {
        // GET: AdministracionEmisionComplementaria
        public ActionResult Index()
        {
            Session.Remove(Variables.SessionListaReporteEmisionComplementaria);
            return View();
        }
        private class Variables
        {
            public const string SessionListaReporteEmisionComplementaria = "___K_SESSION_LISTA_REPORTE_LISTA_EMISION_COMP";
            public const int Si = 1;
            public const int No = 0;
            public const int Cero = 0;
            public const int Uno = 1;
            public const int Observacion = 2;
            public const bool Activo = true;
            public const bool Inactivo = false;
            public const string MensajeErrorAlListarComplementario = "OCURRIO UN ERROR AL LISTAR LE EMISION COMPLEMENTARIA CAB | COMUNIQUESE CON EL ADMINISTRADOR Y OTORGE LOS PARAMETROS DE BUSQUEDA";
            public const string MensajeErrorAlActualizarEstadoDet = "OCURRIO UN ERROR AL ACTUALIZAR EL ESTADO | COMUNICARSE CON EL ADMINISTRADOR E INDICAR EL DETALLE A INACTIVAR";
            public const string MensajeErrorAlListarLicenciasCOnsulta = "OCURRIO UN ERROR AL LISTAR LAS LICENCIAS| COMUNICARSE CON EL ADMINISTRADOR E INDICAR EL DETALLE DEL PROBLEMA";
            public const string MensajeExitoActualizarDefinitiva= "SE CREO CORRECTAMENTE LA SOLICITUD DE EMISION COMPLEMENTARIA EXITOSAMENTE";
            public const string MsjOkGeneracionEmisionComplementaria = "SE PROCESO CORRECTAMENTE LA EMISION COMPLEMENTARIA ";
            public const string MsjErrorGeneracionEmisionComplementaria = "NO SE PROCESO CORRECTAMENTE LA EMISION COMPLEMENTARIA | POR FAVOR COMUNIQUE AL ADMINISTRADOR INMEDIATAMENTE Y DETALLE EL CODIGO DE EMISION ";
            public const string MsjErrorAlListarDetalleConsultaCOmplementaria = "OCURRIO UN ERROR AL LISTAR EL DETALLE | POR FAVOR DE COMUNICARSE CON EL ADMINISTRADOR Y OTORGAR EL CODIGO DE EMISION COMPLEMENTARIA";
            public const string MsjErrorAlinsertarPeriodoDetalle = "OCURRIO UN PROBLEMA AL INTENTAR INSERTAR EL DETALLE | POR FAVOR DE COMUNICARSE CON EL ADMINISTRADOR DEL MODULO";
            public const string MsjErrorAlQuitarPeriodoDetalle = "OCURRIO UN PROBLEMA AL INTENTAR QUITAR LA LICENCIA DEL DETALLE | POR FAVOR DE COMUNICARSE CON EL ADMINISTRADOR DEL MODULO";
            public const string MsjErrorAlObtenerEmisionComplementaria = "OCURRIO UN ERROR AL OBTENER LA EMISION COMPLEMENTARIA | POR FAVOR VUELVA A INICIAR SESION O COMUNIQUESE CON EL ADMINISTRADOR DEL MODULO ";

        }

        private List<BEAdministracionEmisionComplementaria> ListaReporteCobrosParciales
        {
            get
            {
                return (List<BEAdministracionEmisionComplementaria>)Session[Variables.SessionListaReporteEmisionComplementaria];
            }
            set
            {
                Session[Variables.SessionListaReporteEmisionComplementaria] = value;
            }
        }

        public JsonResult Listar(decimal COdigoEmision, decimal CodigoLicencia, decimal CodigoOficina,int Estado, int ConFecha, string FechaInicial, string FechaFinal)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    Session.Remove(Variables.SessionListaReporteEmisionComplementaria);

                    var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                    var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));

                    if (opcAdm == Variables.No)
                       CodigoOficina = oficina;
                

                    var lista = new BLAdministracionEmisionComplementaria().Listar(COdigoEmision, CodigoLicencia, CodigoOficina, Estado, ConFecha, FechaInicial, FechaFinal);

                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblAdministracionComplementario' border=0 width='100%;' class='k-grid k-widget' id='tblAdministracionComplementario'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO EMISION COMP</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DESCRIPCION </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA DE PROCESO</th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ESTADO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >OFICINA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CANT FACTURAS</th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA DE BAJA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA DE CREACION </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >USUARIO </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FLUJO APROBACION.</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >VER DOC GENERADOS.</th>");
                    if (lista != null)
                    {
                        lista.ForEach(item =>
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                            shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center';> ");
                            shtml.AppendFormat("<a href=# onclick='verDetalleEmisionComplementaria({0});'><img id='expand" + item.CodigoEmisionComplementaria + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.CodigoEmisionComplementaria);
                            shtml.Append("</td>");
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDEstOri'>{0}</td>", item.CodigoEmisionComplementaria);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDNomEstOri'><a href=# onclick='VerAprobacion({1});'>{0}</td>", item.NombreEmisionComplementaria, item.CodigoEmisionComplementaria);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDDivEstOri'><a href=# onclick='VerAprobacion({1});'>{0}</td>", item.FechaProcesado, item.CodigoEmisionComplementaria);
                            
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'><a href=# onclick='VerAprobacion({1});'>{0}</td>", item.NombreOficina, item.CodigoEmisionComplementaria);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'><a href=# onclick='VerAprobacion({1});'>{0}</td>", item.CantidadPeriodos, item.CodigoEmisionComplementaria);
                            //shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.Ends);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'><a href=# onclick='VerAprobacion({1});'>{0}</td>", item.FechaCreacion, item.CodigoEmisionComplementaria);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'><a href=# onclick='VerAprobacion({1});'>{0}</td>", item.UsuarioCreacion, item.CodigoEmisionComplementaria);
                            if(item.Estado==Variables.Cero && opcAdm==Variables.Si)
                                shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='AprobarComplementario({0});'><img src='../Images/botones/finalizar.png' border=0 title='{1}'></a>&nbsp;&nbsp;<a href=# onclick='RechazarComplementario({0});'><img src='../Images/botones/error.png' border=0 title='{2}'></a>&nbsp;&nbsp;</td>", item.CodigoEmisionComplementaria, "Aprobar Control", "Rechazar Control");
                            //else if (item.Estado == Variables.Cero && opcAdm == Variables.No)
                            //    shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DescripcionEstado);
                            else
                                shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DescripcionEstado);

                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:left'; class='IDOfiEstOri'><a href=# onclick='VerDocComplementario({0});'><img src='../Images/iconos/report_deta.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.CodigoEmisionComplementaria, "Ver");
                            shtml.Append("</tr>");
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td style='width:100%' colspan='20'>");
                            shtml.Append("<div style='display:none;' id='" + "div" + item.CodigoEmisionComplementaria.ToString() + "'  > ");
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
                retorno.message = Variables.MensajeErrorAlListarComplementario;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult ListarDetalleEmisionComplementaria(decimal IdCancelacionComplementaria)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                    var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));

                    var lista = new BLAdministracionEmisionComplementaria().ListarDetalle(IdCancelacionComplementaria); ;
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
                    shtml.Append("<thead>");
                    shtml.Append("<tr>");
                    //shtml.Append("<th class='k-header' style='width:120px'></th>");
                    //shtml.Append("<th class='k-header' style='width:120px'>CODIGO COBRO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>LICENCIA</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>GRUPO DE FACTURACION</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>SOCIO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>DOCUMENTO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>PERIODO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>MONTO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>ESTADO</th>");
                    if (lista != null)
                    {
                        lista.ForEach(item =>
                        {
                            var color = "white";

                            if (item.EstadoObsDetalle > Variables.Cero)
                                color = "#bb5555";
                            else
                                color = "white";


                            shtml.Append("<tr style='background-color:"+ color + "'>");

                            //EstadoObsDetalle

                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDNombresCobros' padding-right:10px'>{0}</td>", item.CodigoLicencia);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDNombresCobros' padding-right:10px'>{0}</td>", item.GrupoFacturacion);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDRucCobros' padding-right:10px'>{0}</td>", item.Socio);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.Documento);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.periodo);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.MontoPeriodo);
                            if(item.EstadoCab==Variables.Cero && opcAdm==Variables.Si)
                                shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='InactivarEmisionComplementaria({0},{3});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a></td>", item.CodigoEmisionComplementariaDet, item.EndsDet == Variables.Si ? "delete.png" : "activate.png", item.EndsDet == Variables.Si ? "Inactivar Cobro" : "Activar Cobro", item.CodigoEmisionComplementariaCab);
                            else if(item.EstadoDet==Variables.Si)
                                shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>PROCESADO</td>", item.MontoPeriodo);
                            else if (item.EstadoDet==Variables.No && item.EstadoCab==Variables.Si)
                                shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>NO PROCESADO INACTIVADO</td>", item.MontoPeriodo);
                            else
                                shtml.Append("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'></td>");
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
                retorno.message = Variables.MsjErrorAlListarDetalleConsultaCOmplementaria;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ActualizarEstadDetalleComplementario(decimal IdEmisionComplementaria)
        {


            Resultado retorno = new Resultado();

            try
            { 
            
                int r = new BLAdministracionEmisionComplementaria().ActualizarEstadoDetEmisionComplementaria(IdEmisionComplementaria);

                if (r >= Variables.Si)
                {
                    retorno.result = Variables.Si;
                }
                else if (r == Variables.Observacion)
                {
                    retorno.result = Variables.No;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MensajeErrorAlActualizarEstadoDet;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertaActualizaCabEmiComplementaria(decimal CodCabComple, string nombre,string descripcion)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {

                    var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                    var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));

                    BEAdministracionEmisionComplementaria entidad = new BEAdministracionEmisionComplementaria();
                    entidad.CodigoEmisionComplementaria = CodCabComple;
                    entidad.NombreEmisionComplementaria = nombre;
                    entidad.RespuestaEmisionComplementaria = descripcion;
                    entidad.UsuarioCreacion = UsuarioActual;
                    entidad.CodigoOficina = oficina;

                    decimal r = new BLAdministracionEmisionComplementaria().InsertaActualizaCabEmiComplementaria(entidad);

                    if (r >= Variables.Si)
                    {
                        retorno.result = Variables.Si;
                        retorno.Code = Convert.ToInt32(r);
                    }
                    else if (r == Variables.Observacion)
                    {
                        retorno.result = Variables.No;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MensajeErrorAlActualizarEstadoDet;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListarConsultaLicenciaDetalle(decimal codLicencia,decimal CodSocio,int anio,int mes,decimal codcab)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {

                    var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                    var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));


                    if (opcAdm == Variables.Si) 
                        oficina=Variables.Cero;

                    var lista = new BLAdministracionEmisionComplementaria().ListarConsultaLicenciaDetalle(codLicencia,CodSocio,mes,anio, oficina, codcab); ;
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
                    shtml.Append("<thead>");
                    shtml.Append("<tr>");
                    //shtml.Append("<th class='k-header' style='width:120px'></th>");
                    //shtml.Append("<th class='k-header' style='width:120px'>CODIGO COBRO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>LICENCIA</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>ANIO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>MES</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>MONTO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>AGREGAR</th>");
                    if (lista != null)
                    {
                        lista.ForEach(item =>
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                            //shtml.AppendFormat("<td style='width:5%; text-align:center'; display:none class='IDCobros' padding-right:10px';>{0}</td>", item.CodigoCobro);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDNombresCobros' padding-right:10px'>{0}</td>", item.CodigoLicencia);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDRucCobros' padding-right:10px'>{0}</td>", item.anioperiodo);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.mesperiodo);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.MontoPeriodo);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='Agregar({0},{1});'><img src='../Images/botones/next.png' border=0 title='{2}'></a>&nbsp;&nbsp;</td>", item.CodigoLicencia,item.CodigoPeriodo, "Modificar");
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
                retorno.message = Variables.MensajeErrorAlListarLicenciasCOnsulta;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public JsonResult ListarLicenciarRegistradaDetalle(decimal codcab)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {

                    var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                    var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));


                    if (opcAdm == Variables.Si)
                        oficina = Variables.Cero;

                    var lista = new BLAdministracionEmisionComplementaria().ListarLicenciarRegistradaDetalle(codcab); ;
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table  border=0 width='100%; margin:0;padding:0 ;position:top' id='FiltroTabla'>");
                    shtml.Append("<thead>");//borrar el margin hasta el top*
                    shtml.Append("<tr>");
                    //shtml.Append("<th class='k-header' style='width:120px'></th>");
                    //shtml.Append("<th class='k-header' style='width:120px'>CODIGO COBRO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>QUITAR</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>LICENCIA</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>ANIO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>MES</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>MONTO</th>");
                    
                    if (lista != null)
                    {
                        lista.ForEach(item =>
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='quitar({0});'><img src='../Images/botones/back2.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.CodigoEmisionComplementariaDet, "Modificar");
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDNombresCobros' padding-right:10px'>{0}</td>", item.CodigoLicencia);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDRucCobros' padding-right:10px'>{0}</td>", item.anioperiodo);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.mesperiodo);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.MontoPeriodo);
                            
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
                retorno.message = Variables.MsjErrorAlListarDetalleConsultaCOmplementaria;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult InsertarLicenciaPlaneamientoDetalle(decimal CodCab,decimal CodLic , decimal CodPl)
        {


            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    BEAdministracionEmisionComplementariaDetalle entidad = new BEAdministracionEmisionComplementariaDetalle();


                    entidad.OWNER = GlobalVars.Global.OWNER;
                    entidad.CodigoEmisionComplementariaCab = CodCab;
                    entidad.CodigoPeriodo = CodPl;
                    entidad.CodigoLicencia = CodLic;
                    entidad.UsuarioCreacionCompDet = UsuarioActual;

                    int r = new BLAdministracionEmisionComplementaria().InsertarLicenciaPlaneamientoDetalle(entidad);

                    if (r == Variables.Si)
                    {
                        retorno.result = Variables.Si;
                    }
                    else if (r == Variables.Observacion)
                    {
                        retorno.result = Variables.No;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MsjErrorAlinsertarPeriodoDetalle;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public ActionResult QuitarLicenciaPlaneamientoDetalle(decimal CodDet)
        {


            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    BEAdministracionEmisionComplementariaDetalle entidad = new BEAdministracionEmisionComplementariaDetalle();


                    entidad.CodigoEmisionComplementariaDet = CodDet;
                    entidad.UsuarioCreacionCompDet = UsuarioActual;

                    int r = new BLAdministracionEmisionComplementaria().QuitarLicenciaPlaneamientoDetalle(entidad);

                    if (r == Variables.Si)
                    {
                        retorno.result = Variables.Si;
                    }
                    else if (r == Variables.Observacion)
                    {
                        retorno.result = Variables.No;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MsjErrorAlQuitarPeriodoDetalle;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ActualizaDefinitivaCabDetComplementario(decimal Codcab)
        {


            Resultado retorno = new Resultado();

            try
            {

                if (!isLogout(ref retorno))
                {

                    int r = new BLAdministracionEmisionComplementaria().ActualizaDefinitivaCabDetComplementario(Codcab, UsuarioActual);

                    if (r >= Variables.Si)
                    {
                        retorno.result = Variables.Si;
                        retorno.message = Variables.MensajeExitoActualizarDefinitiva;
                    }
                    else if (r == Variables.No)
                    {
                        retorno.result = Variables.No;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MensajeErrorAlActualizarEstadoDet;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RespuestaGenerarFacturacionMensual(decimal CodCab,int RespuestaSoli)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                    var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));



                    int Respuesta = Variables.Cero;

                    if (opcAdm == Variables.Si) // SI NO PASA LA VALIDACION  SELECCIONAR LA OFICINA A LA QUE PERTENECE EL USUARIO
                    {
                        if (RespuestaSoli == Variables.Si)
                            Respuesta = new BLAdministracionEmisionComplementaria().GenerarEmisionComplementaria(CodCab, UsuarioActual);
                        else if (RespuestaSoli == Variables.No)
                            Respuesta = new BLAdministracionEmisionComplementaria().RechazaSolicitudEmisionComplementaria(CodCab, UsuarioActual);
                    }

                    if (Respuesta > Variables.Cero)
                    {
                        retorno.result = Variables.Si;
                        retorno.message = Variables.MsjOkGeneracionEmisionComplementaria;
                    }
                    else
                    {
                        retorno.result = Variables.No;
                        retorno.message = Variables.MsjErrorGeneracionEmisionComplementaria;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MsjErrorGeneracionEmisionComplementaria;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

  
        public JsonResult ListaDocumentoGeneradoxEmiComplementaria(decimal codcab)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {

                    var lista = new BLAdministracionEmisionComplementaria().ListaDocumentoGeneradoxEmiComplementaria(codcab); ;
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table  border=0 width='100%; margin:0;padding:0 ;position:top' id='FiltroTabla'>");
                    shtml.Append("<thead>");//borrar el margin hasta el top*
                    shtml.Append("<tr>");
                    //shtml.Append("<th class='k-header' style='width:120px'></th>");
                    //shtml.Append("<th class='k-header' style='width:120px'>CODIGO COBRO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'></th>");
                    shtml.Append("<th class='k-header' style='width:120px'>COD DOC</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>TIPO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>SERIE</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>NUMERO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>IDENT</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>DOCUMENTO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>SOCIO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>FEC EMISION</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>NETO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>ESTADO</th>");
                    shtml.Append("<th class='k-header' style='width:120px'>ESTADO SUNAT</th>");

                    if (lista != null)
                    {
                        lista.ForEach(item =>
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            shtml.Append("<td style='width:5%; text-align:center'; class='IDNombresCobros' padding-right:10px'>");
                            shtml.AppendFormat("<a href=# onclick='verDetalleDocumento({0});'><img id='expand" + item.INV_ID + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.INV_ID);
                            shtml.Append("</td>");
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDNombresCobros' padding-right:10px'>{0}</td>", item.INV_ID);
                            shtml.AppendFormat("<td style='width:5%; text-align:center'; class='IDRucCobros' padding-right:10px'>{0}</td>", item.TIPO_EMI_DOC);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.NMR_SERIAL);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.INV_NUMBER);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.INVT_DESC);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.TAX_ID);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.SOCIO);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.INV_DATE);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.INV_NET);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.estadoFactura);
                            shtml.AppendFormat("<td style='width:2%; text-align:center'; class='IDCantidadDocumentosCobros' padding-right:10px'>{0}</td>", item.ESTADO_SUNAT);

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
                retorno.message = Variables.MensajeErrorAlListarLicenciasCOnsulta;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        //


        public JsonResult ObtenerEmisionComplementaria(decimal codcab)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEAdministracionEmisionComplementaria entidad = new BLAdministracionEmisionComplementaria().ObtenerEmisionComplementaria(codcab);
                    if (entidad != null)
                    {
                        retorno.data = Json(entidad, JsonRequestBehavior.AllowGet);
                        retorno.result = Variables.Si;
                    }
                }


            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MsjErrorAlObtenerEmisionComplementaria;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerEmisionComplementaria", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


    }
}
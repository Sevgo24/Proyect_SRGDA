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

namespace Proyect_Apdayc.Controllers.AdministracionSocioMoroso
{
    public class AdministracionSocioMorosoController : Base
    {
        // GET: AdministracionSocioMoroso

        private const string K_SESSION_LISTA_REPORTE_SOCIO_RENUENTE = "___K_SESSION_LISTA_REPORTE_SOCIO_RENUENTE";
        private class Variables
        {
            public const int Si = 1;
            public const int No = 0;
            public const int Cero = 0;
            public const int Uno = 1;
            public const string Vacio = "";
            public const string Guion = "-";
            public const string MsgUsuarioRegistradoCorrectamente = "El Usuario ha sido registrado como Renuente exitosamente ";
            public const string MsgUsuarioNoRegistradoCorrectamente = "Hubo un error al Registrar el Usuario como Renuente | Por favor comunicarse con el Responsable y indicar los parametros a registrar";
            public const string MsgUsuarioActualizadoCorrectamente = "El Usuario ha sido Actualizado exitosamente ";
            public const string MsgUsuarioNoActualizadoCorrectamente = "El Usuario NO ha sido Actualizado | Por favor de comunicarse con el administrador de  ";
            public const string MsgUsuarioMorosoErrorAlObtener = "Ocurrio un problema al obtener el Socio | Por favor detalle el codigo de Socio a el administrador del Modulo ";
            public const string MsgEstadoSocioMorosoActualizadoOK = "Se actualizo correctamente el Socio ";
            public const string MsgEstadoSocioMorosoActualizadoNoOk = "No Se actualizo correctamente el Socio | No tiene permiso para realizar esta Accion | Por favor de comunicarse con el Administrador del Modulo ";
            public const string MsgEstadoSocioMorosoInactivarNoOk = "No Se Inactivo/Activo correctamente el Socio | No tiene permiso para realizar esta Accion | Por favor de comunicarse con el Administrador del Modulo ";
        }

        public ActionResult Index()
        {
            Session.Remove(K_SESSION_LISTA_REPORTE_SOCIO_RENUENTE);
            return View();
        }

        private List<BEAdministracionSocioMoroso> ListaReporte
        {
            get
            {
                return (List<BEAdministracionSocioMoroso>)Session[K_SESSION_LISTA_REPORTE_SOCIO_RENUENTE];
            }
            set
            {
                Session[K_SESSION_LISTA_REPORTE_SOCIO_RENUENTE] = value;
            }
        }

        public JsonResult ListarSociosMoroso(decimal CodisoSocio,int ConFecha,string FechaInicial,string FechaFinal,int Tipo,int Estado)
        {
            Resultado retorno = new Resultado();
            try
            {
                var lista = new BLAdministracionSocioMoroso().lista(CodisoSocio,ConFecha, FechaInicial, FechaFinal, Tipo,Estado);

                ListaReporte = lista;

                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblLicencias' border=0 width='100%;' class='k-grid k-widget' id='tblLicencias'>");
                    shtml.Append("<thead><tr>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO</th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO SOCIO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >RAZON SOCIAL | SOCIO</th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >TIPO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DOCUMENTO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >REPRESENTANTE LEGAL</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >EVENTO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA EVENTO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >LOCAL</th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DIRECCION</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >OFICINA</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ESTADO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Editar</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    //shtml.Append("</tr>"); //descomentar
                    if (lista != null)
                    {
                        foreach (var item in lista.OrderBy(x => x.CodigoSocio))
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input onclick='validaSocioModif(" + item.CodigoSocio + ")' type='checkbox' id='{0}' name='Check' class='Check' />", "chkEstOrigen" + item.BPS_ID);
                            //shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center'; class='IDEstOri'>{0}</td>", item.CodigoSocioMoroso);
                            //shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center'; class='IDNomEstOri'>{0}</td>", item.CodigoSocio);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDDivEstOri'>{0}</td>", item.RazonSocial==Variables.Vacio ? item.Socio :item.RazonSocial);
                            //shtml.AppendFormat("<td style='width:1%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.TipoSocio);
                            shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DocumentoIdentificacion);
                            shtml.AppendFormat("<td style='width:3%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.Representante);
                            shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.NombreEvento);
                            shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.FechaEvento);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.Local);
                            //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.Direccion);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.NombreOficina);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DescripcionEstado);
                            shtml.AppendFormat("<td style='width:1%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='editarSocio({0});'><img src='../Images/iconos/report_deta.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.CodigoSocioMoroso, "Ver");
                            shtml.AppendFormat("<td style='width:1%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='InactivarSocioMoroso({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a></td>", item.CodigoSocioMoroso,item.DescripcionEst == Variables.Guion ? "delete.png" : "activate.png", item.DescripcionEst == Variables.Guion ? "Inactivar Socio Renuente" : "Activar Socio Renuente");
                            //shtml.AppendFormat("<td style='width:100%; cursor:pointer;text-align:left; ';' class='IDCellOri' ><input type='radio' id='{0}' name='radio' class='radio' value={0} />{1}</td>", item.LIC_ID, item.LIC_NAME);


                            shtml.AppendFormat("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                            shtml.Append("</tr>");
                        }
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = Variables.Si;
                }


            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
            }


            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult InsertarSocioMoroso(decimal CodigoSocio,string Descripcion , decimal CodigoLicencia)
        {
            Resultado retorno = new Resultado();
            var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            try
            {
                if(!isLogout (ref retorno))
                {
                    var resp = new BLAdministracionSocioMoroso().InsertarUsuarioMoroso(CodigoSocio,Descripcion,UsuarioActual,  CodigoLicencia, oficina);

                    if (resp)
                    {
                        retorno.result = Variables.Si;
                        retorno.message = Variables.MsgUsuarioRegistradoCorrectamente;
                    }

                }
            }catch(Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MsgUsuarioNoRegistradoCorrectamente;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InactivarUsuarioMoroso(decimal CodigoSocio)
        {
            Resultado retorno = new Resultado();

            try
            {
                if(!isLogout (ref retorno))
                {
                    bool resp = false;
                    var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                    var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));

                    if (opcAdm == Variables.Si)
                    {
                        resp = new BLAdministracionSocioMoroso().InactivarUsuarioMoroso(CodigoSocio, UsuarioActual);
                    }
                
                    if (resp)
                    {
                        retorno.result = Variables.Si;
                        retorno.message = Variables.MsgUsuarioActualizadoCorrectamente;
                    }
                    else
                    {
                        retorno.result = Variables.No;
                        retorno.message = Variables.MsgEstadoSocioMorosoInactivarNoOk;
                    }

                }
            }catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MsgUsuarioNoActualizadoCorrectamente;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ObtienerSocioMoroso(decimal ID)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEAdministracionSocioMoroso entidad = new BLAdministracionSocioMoroso().Obtener(ID);
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
                retorno.message = Variables.MsgUsuarioMorosoErrorAlObtener;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerSocioMoroso", ex);
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarEstadoSocioMoroso(decimal CodigoSocio,int Estado)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    bool resp=false;
                    var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                    var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));

                    if (opcAdm == Variables.Si)
                    {
                        resp = new BLAdministracionSocioMoroso().ActualizarEstadoSocioMoroso(CodigoSocio, Estado, UsuarioActual);
                    }

                    if (resp)
                    {
                        retorno.result = Variables.Si;
                        retorno.message = Variables.MsgEstadoSocioMorosoActualizadoOK;
                    }
                    else
                    {
                        retorno.result = Variables.No;
                        retorno.message = Variables.MsgEstadoSocioMorosoActualizadoNoOk;
                    }

                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MsgEstadoSocioMorosoActualizadoNoOk;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReporteSociosMorosos(string formato)
        {
            string format = formato;
            int oficina_id = Variables.Cero;
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            Resultado retorno = new Resultado();

            try
            {

                List<BEAdministracionSocioMoroso> lstReporte = new List<BEAdministracionSocioMoroso>();
                lstReporte = ListaReporte;

                if (lstReporte.Count() > Variables.Cero && lstReporte != null)
                {
                    LocalReport localReport = new LocalReport();

                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REP_SOCIOS_MOROSOS.rdlc");

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
                    localReport.DisplayName = "Reporte de Socio Morosos";

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
                    retorno.message = "REALIZAR LA CONSULTA ANTES DE MOSTRAR EL PDF | EL REPORTE NO TIENE REGISTROS QUE MOSTRAR";
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte de MATRIZ DE LICENCIA", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }


        public ActionResult ReporteTipo()
        {
            Resultado retorno = new Resultado();

            try
            {

                List<BEAdministracionSocioMoroso> listar = new List<BEAdministracionSocioMoroso>();
                listar = ListaReporte;


                if (listar != null && listar.Count > Variables.Cero)
                {
                    retorno.result = Variables.Si;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
                else
                {
                    retorno.result = Variables.No;
                    retorno.message = "REALIZAR BUSQUEDA ANTES DE (VER PDF / REP. EXCEL) |  LA BUSQUEDA NO DEVOLVIO NINGUN RESULTADO";
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte de Artista", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}
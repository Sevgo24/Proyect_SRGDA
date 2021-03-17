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
namespace Proyect_Apdayc.Controllers.AdministracionNotificacionLicencias
{
    public class AdministracionNotificacionLicenciasController : Base
    {
        // GET: AdministracionNotificacionLicencias

        private class Variables
        {
            public const int Si = 1;
            public const int No = 0;
            public const int Cero = 0;
            public const bool Falso = false;
            public const bool Verdadero = true;
            public const string MsjNoSeEncontroLicencia = "No se ha seleccionado ninguna Licencia ";
            public const string MsjNoSeEncontroModalidad = "No se ha seleccionado ninguna Modalidad ";
            public const string MsjNoSeEncontroTarifa = "No se ha seleccionado ninguna Tarifa ";
            public const string MsjNoSeEncontroEstablecimiento = "No se ha seleccionado ningun Establecimiento ";
            public const string MsjNoSeEncontroSocio = "No se ha seleccionado ninguna Socio ";
            public const string MsjActualizadoCorrectamente = "Se actualizo la Licencia correctamente";
            public const string MsjErrorAlActualizar = "Ocurrio un error al Actualizar la licencia | Detalle los parametros a actualizar y el codigo de licencia al Administrador del Modulo";
            public const string MsjErrorAlListarLicencia = "Ocurrio un error al Listar las licencias | Detalle  el codigo de licencia al Administrador del Modulo";
            public const string MsjLicenciaYafueModificada = "La Licencia ya Tiene Un Documento Emitido y no puede modificarse";
        }

        public ActionResult Index()
        {
            Init(false);
            return View();
        }


        public JsonResult ListarLicenciasxAsignar(decimal Oficina, decimal CodigoLicencia, string NombreLicencia, string NombreEstablecimiento, int ConFecha, string FechaInicial, string FechaFin,int EstadoLicencia)
        {
            Resultado retorno = new Resultado();
            try
            {


                var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));




                if (opcAdm == Variables.No) // SI NO PASA LA VALIDACION  SELECCIONAR LA OFICINA A LA QUE PERTENECE EL USUARIO
                    Oficina = oficina;

                if (opcAdm == Variables.Si && Oficina == Variables.Cero) // PARA QUE TRAIGA TODA LA BUSQUEDA 
                    Oficina = Variables.Cero;

                var lista = new BLAdministracionNotificacionLicencias().ListaNotificacionesEventos(CodigoLicencia, Oficina, NombreLicencia, NombreEstablecimiento, ConFecha, FechaInicial, FechaFin,EstadoLicencia);

                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblAdministracionRequerimiento' border=0 width='100%;' class='k-grid k-widget' id='tblAdministracionRequerimiento'>");
                shtml.Append("<thead><tr>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO LICENCIA</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA  INICIO DE EVENTO </th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FECHA FIN DE EVENTO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >UBIGEO </th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DIRECCION</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >NOMBRE DEL ESTABLECIMIENTO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DOCUMENTOS</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ESTADO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Modificar.</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Ver.</th>");
                if (lista != null)
                {
                    lista.ForEach(item =>
                    {
                        shtml.Append("<tr style='background-color:white'>");

                        //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                        shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:center'; class='IDEstOri'>{0}</td>", item.IdLicencia);
                        shtml.AppendFormat("<td style='width:3%; cursor:pointer;text-align:center'; class='IDNomEstOri'>{0}</td>", (item.FechaInicioEvento).ToString("dd/MM/yyyy"));
                        shtml.AppendFormat("<td style='width:3%; cursor:pointer;text-align:center'; class='IDDivEstOri'>{0}</td>", (item.FechaFinEvento).ToString("dd/MM/yyyy"));
                        shtml.AppendFormat("<td style='width:10%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DescripcionUbigeo);
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DescripcionDireccion);
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.NombreEstablecimiento);
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DocumentosDescripcion);
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DescripcionEstado);
                        //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='AprobarControl({0});'><img src='../Images/botones/finalizar.png' border=0 title='{1}'></a>&nbsp;&nbsp;<a href=# onclick='RechazarControl({0});'><img src='../Images/botones/error.png' border=0 title='{2}'></a>&nbsp;&nbsp;</td>", item.LIC_ID, "Aprobar Control", "Rechazar Control");
                        shtml.AppendFormat("<td style='width:1%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='editarLicencia({0},{1});'><img src='../Images/iconos/report_deta.png' border=0 title='{2}'></a>&nbsp;&nbsp;</td>", item.IdLicencia,item.IdEstablecimiento, "Ver");
                        shtml.AppendFormat("<td style='width:1%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='VerLicenciVentanaNueva({0});'><img src='../Images/iconos/report_deta.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.IdLicencia, "Ver");
                        //shtml.AppendFormat("<td style='width:100%; cursor:pointer;text-align:left; ';' class='IDCellOri' ><input type='radio' id='{0}' name='radio' class='radio' value={0} />{1}</td>", item.LIC_ID, item.LIC_NAME);
                        //href = javascript:editar('${DISC_ID}', '${LIC_ID}')

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
                retorno.message = shtml.ToString();

            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MsjErrorAlListarLicencia;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ActualizarEstadoLicenciaNotificada(decimal CodigoLicencia, decimal CodigoModalidad, decimal CodigoTarifa, decimal CodigoEstablecimiento, decimal CodigoSocio)
        {
            Resultado retorno = new Resultado();
            try
            {
                bool FlagExito = Variables.Verdadero;

                if (CodigoLicencia == Variables.Cero) { FlagExito = Variables.Falso; retorno.result = Variables.Cero; retorno.message = Variables.MsjNoSeEncontroLicencia; }
                if (CodigoModalidad == Variables.Cero) { FlagExito = Variables.Falso; retorno.result = Variables.Cero; retorno.message = Variables.MsjNoSeEncontroModalidad; }
                if (CodigoTarifa == Variables.Cero) { FlagExito = Variables.Falso; retorno.result = Variables.Cero; retorno.message = Variables.MsjNoSeEncontroTarifa; }
                if (CodigoEstablecimiento == Variables.Cero) { FlagExito = Variables.Falso; retorno.result = Variables.Cero; retorno.message = Variables.MsjNoSeEncontroEstablecimiento; }
                if (CodigoSocio == Variables.Cero) { FlagExito = Variables.Falso; retorno.result = Variables.Cero; retorno.message = Variables.MsjNoSeEncontroSocio; }

                if (FlagExito)
                {
                    int r = new BLAdministracionNotificacionLicencias().ActualizaLicenciaNotificacion(CodigoLicencia, CodigoModalidad, CodigoTarifa, CodigoEstablecimiento, CodigoSocio);

                    retorno.result = Variables.Si;
                    if (r >= Variables.Si)
                        retorno.message = Variables.MsjActualizadoCorrectamente;
                    else
                        retorno.message = Variables.MsjLicenciaYafueModificada;
                }
            }
            catch (Exception ex)
            {
                retorno.result = Variables.No;
                retorno.message = Variables.MsjErrorAlActualizar;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}
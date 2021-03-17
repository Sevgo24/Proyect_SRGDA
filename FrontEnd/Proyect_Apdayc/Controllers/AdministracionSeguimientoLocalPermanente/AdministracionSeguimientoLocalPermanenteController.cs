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

namespace Proyect_Apdayc.Controllers.AdministracionSeguimientoLocalPermanente
    {
        public class AdministracionSeguimientoLocalPermanenteController : Base
        {
        // GET: AdministracionSeguimientoLocalPermanente
            public class VARIABLES
         {
            public const int SI = 1;
            public const string SILETRAS = "SI";
            public const string NOLETRAS = "NO";
            public const int NO = 0;
            public const int CERO = 0;
            public const string MSG_ERROR_AL_LISTAR_MATRIZ = "OCURRIO UN ERROR AL LISTAR LAS LICENCIAS | POR FAVOR DETALLE LOS PARAMETROS AL ADMINISTRADOR RESPONSABLE DEL MODULO PARA SU VERIFICACION";
            public const int ULTIMO_PERIODO_FACTURADO = 1;
            public const int HUECOS_PERIODOS = 2;
            public const int VALIDACION_MENSUAL_PASO = 3;
            public const int VALIDACION_MENSUAL_NO_PASO = 4;
            public const string MSG_SELECCIONE_OFICINA = "NO HA SELECCIONADO UNA OFICINA VALIDA";
            public const string K_SESSION_LISTA_REPORTE_SEGUIMIENTO_LICENCIA = "___K_SESSION_LISTA_REPORTE_MATRIZ_LICENCIA";
            public const string K_SESSION_LISTA_rEPORTE_SEGUIMIENTO_APROBADA_FILTRO = "___K_SESSION_LISTA_REPORTE_LICENCIA_SEGUIMIENTO_APROB";
            public const string K_SESSION_LISTA_rEPORTE_SEGUIMIENTO_OBSERVADA_FILTRO = "___K_SESSION_LISTA_REPORTE_LICENCIA_SEGUIMIENTO_OBS";
            public const int ENERO = 1;
            public const int FEBRERO = 2;
            public const int MARZO = 3;
            public const int ABRIL = 4;
            public const int MAYO = 5;
            public const int JUNIO = 6;
            public const int JULIO = 7;
            public const int AGOSTO = 8;
            public const int SEPTIEMBRE = 9;
            public const int OCTUBRE = 10;
            public const int NOVIEMBRE = 11;
            public const int DICIEMBRE = 12;
            public const string FORMATOCERO = "0.000000";
            public const string FORMATOGUION = "-";
            public const string MOROSIDAD = "MOROSIDAD";
            public const string RECALCULAR = "RECALCULAR";
            public const string MARCAREMISION = "UNCHECK";
            public const string PERIODOBLOQUEADO = "BLOQUEADO";
            public const int UNO = 1;
            public const int DOS = 2;
            public const int TRES = 3;
            public const int CUATRO = 4;
        }

        public class ENVIO_DATA
        {
            public  int CantidadLicencias = 0;
            public int CantidadLicenciasActivas = 0;
            public int CantidadLicenciasInactivas = 0;
            public int CantidadLicenciasAprobadas = 0;
            public int CantidadLicenciasObservadas = 0;
            public int CantidadLicenciasListaParaEmisionSeleccionada = 0;
            public int CantidadLicenciasobservadasRazon = 0;
            public int CantidadLicenciasPagoAdelantado = 0;
            public int CantidadLicenciasSinPeriodo = 0;
        }
         private List<BEAdministracionSeguimientoLocalPermanente> ListaReporte
         {
             get
             {
                 return (List<BEAdministracionSeguimientoLocalPermanente>)Session[VARIABLES.K_SESSION_LISTA_REPORTE_SEGUIMIENTO_LICENCIA];
             }
             set
             {
                 Session[VARIABLES.K_SESSION_LISTA_REPORTE_SEGUIMIENTO_LICENCIA] = value;
             }
         }

        private List<BEAdministracionSeguimientoLocalPermanente> ListaReporteAprob
        {
            get
            {
                return (List<BEAdministracionSeguimientoLocalPermanente>)Session[VARIABLES.K_SESSION_LISTA_rEPORTE_SEGUIMIENTO_APROBADA_FILTRO];
            }
            set
            {
                Session[VARIABLES.K_SESSION_LISTA_rEPORTE_SEGUIMIENTO_APROBADA_FILTRO] = value;
            }
        }

        private List<BEAdministracionSeguimientoLocalPermanente> ListaReporteObs
        {
            get
            {
                return (List<BEAdministracionSeguimientoLocalPermanente>)Session[VARIABLES.K_SESSION_LISTA_rEPORTE_SEGUIMIENTO_OBSERVADA_FILTRO];
            }
            set
            {
                Session[VARIABLES.K_SESSION_LISTA_rEPORTE_SEGUIMIENTO_OBSERVADA_FILTRO] = value;
            }
        }
        public ActionResult Index()
        {
            Session.Remove(VARIABLES.K_SESSION_LISTA_REPORTE_SEGUIMIENTO_LICENCIA);
            Session.Remove(VARIABLES.K_SESSION_LISTA_rEPORTE_SEGUIMIENTO_APROBADA_FILTRO);
            Session.Remove(VARIABLES.K_SESSION_LISTA_rEPORTE_SEGUIMIENTO_OBSERVADA_FILTRO);
            return View();
        }
        
        
        public JsonResult ListarLicenciaSeguimiento(string anio , decimal CodigoOficina,string CodigoModalidad,int MesEvaluar)
        {
            Resultado retorno = new Resultado();
            var lista = new List<BEAdministracionSeguimientoLocalPermanente>();


            try
            {
                lista = new BLAdministracionSeguimientoLocalPermanente().ListarLicenciaSeguimiento(anio, CodigoOficina, CodigoModalidad, MesEvaluar);
                ListaReporte = lista;

                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblAdministracionRequerimiento' border=0 width='100%;' class='k-grid k-widget' id='tblAdministracionRequerimiento'>");
                shtml.Append("<thead><tr>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >EMISION AUTO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >RAZON</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >LOCAL</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DISTRITO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ENERO </th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FEBRERO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MARZO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ABRIL</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MAYO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >JUNIO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >JULIO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >AGOSTO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >SEPTIEMBRE</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >OCTUBRE</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >NOVIEMBRE</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DICIEMBRE</th>");
                if (lista != null)
                {
                    lista.ForEach(item =>
                    {
                        shtml.Append("<tr style='background-color:white'>");

                        //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDEstOri'>{0}</td>", item.CODIGO_LIC);
                        shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:left'; class='IDNomEstOri'>{0}</td>", item.EMISION_PROD);
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDDivEstOri'>{0}</td>", item.RAZONE);
                        shtml.AppendFormat("<td style='width:8%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.LOCAL);
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DISTRITO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.ENERO );
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.FEBRERO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.MARZO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.ABRIL);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.MAYO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.JUNIO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.JULIO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.AGOSTO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.SEPTIEMBRE);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.OCTUBRE);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.NOVIEMBRE);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DICIEMBRE);
                        //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='ModificarRequerimiento({0});'><img src='../Images/iconos/report_deta.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.ID_REQ, "Modificar");
                        //href = javascript:editar('${DISC_ID}', '${LIC_ID}')

                        shtml.AppendFormat("</td>");
                        shtml.Append("</tr>");
                        shtml.Append("</div>");
                        shtml.Append("</td>");
                        shtml.Append("</tr>");
                        shtml.Append("</tr>");
                    });
                }

                ENVIO_DATA datos = new ENVIO_DATA();
                datos.CantidadLicencias = lista.Count();
                datos.CantidadLicenciasAprobadas= lista.Where(x=>x.EMISION_PROD==VARIABLES.SILETRAS).Count();
                datos.CantidadLicenciasObservadas=lista.Where(x => x.EMISION_PROD == VARIABLES.NOLETRAS).Count();

                shtml.Append("</table>");
                retorno.result = VARIABLES.SI;
                retorno.message = shtml.ToString();
                retorno.data= Json(datos, JsonRequestBehavior.AllowGet);




            }
            catch(Exception ex)
            {
                retorno.result = VARIABLES.NO;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult ListarLicenciaSeguimientoAprobados( int Mes,int Tipo)
        {
            Resultado retorno = new Resultado();
            var lista = new List<BEAdministracionSeguimientoLocalPermanente>();
//            var listaFiltro = new List<BEAdministracionSeguimientoLocalPermanente>();
            int LicenciaCantidadDiferenteMes = 0;
            int LicenciaSinperiodoMes = 0;
            int LicenciasQueSaldranenEmision = 0;

            try
            {
                lista = ListaReporte.Where(x => x.EMISION_PROD == VARIABLES.SILETRAS).ToList();
                if (Mes == VARIABLES.ENERO)
                {
                    LicenciaCantidadDiferenteMes = lista.Where(x => x.ENERO != VARIABLES.FORMATOCERO && x.ENERO != VARIABLES.FORMATOGUION).Count();
                    LicenciaSinperiodoMes = lista.Where(x => x.ENERO == VARIABLES.FORMATOGUION).Count();
                    LicenciasQueSaldranenEmision= lista.Where(x => x.ENERO == VARIABLES.FORMATOCERO).Count();
                    if (Tipo == VARIABLES.UNO)
                        lista = lista.Where(x => x.ENERO == VARIABLES.FORMATOCERO).ToList();
                    else if (Tipo == VARIABLES.DOS)
                        lista = lista.Where(x => x.ENERO != VARIABLES.FORMATOCERO && x.ENERO!= VARIABLES.FORMATOGUION).ToList();
                    else if (Tipo == VARIABLES.TRES)
                        lista = lista.Where(x => x.ENERO == VARIABLES.FORMATOGUION).ToList();

                }
                else if (Mes == VARIABLES.FEBRERO)
                {
                    LicenciaCantidadDiferenteMes = lista.Where(x => x.FEBRERO != VARIABLES.FORMATOCERO && x.FEBRERO != VARIABLES.FORMATOGUION).Count();
                    LicenciaSinperiodoMes = lista.Where(x => x.FEBRERO == VARIABLES.FORMATOGUION).Count();
                    LicenciasQueSaldranenEmision = lista.Where(x => x.FEBRERO == VARIABLES.FORMATOCERO).Count();
                    if (Tipo == VARIABLES.UNO)
                        lista = lista.Where(x => x.FEBRERO == VARIABLES.FORMATOCERO).ToList();
                    else if (Tipo == VARIABLES.DOS)
                        lista = lista.Where(x => x.FEBRERO != VARIABLES.FORMATOCERO && x.FEBRERO != VARIABLES.FORMATOGUION ).ToList();
                    else if (Tipo == VARIABLES.TRES)
                        lista = lista.Where(x => x.FEBRERO == VARIABLES.FORMATOGUION).ToList();

                }
                else if (Mes == VARIABLES.MARZO)
                {
                    LicenciaCantidadDiferenteMes = lista.Where(x => x.MARZO != VARIABLES.FORMATOCERO && x.MARZO != VARIABLES.FORMATOGUION).Count();
                    LicenciaSinperiodoMes = lista.Where(x => x.MARZO == VARIABLES.FORMATOGUION).Count();
                    LicenciasQueSaldranenEmision = lista.Where(x => x.MARZO == VARIABLES.FORMATOCERO).Count();
                    if (Tipo == VARIABLES.UNO)
                        lista = lista.Where(x => x.MARZO == VARIABLES.FORMATOCERO).ToList();
                    else if (Tipo == VARIABLES.DOS)
                        lista = lista.Where(x => x.MARZO != VARIABLES.FORMATOCERO && x.MARZO != VARIABLES.FORMATOGUION).ToList();
                    else if (Tipo == VARIABLES.TRES)
                        lista = lista.Where(x => x.MARZO == VARIABLES.FORMATOGUION).ToList();
                }
                else if (Mes == VARIABLES.ABRIL)
                {
                    LicenciaCantidadDiferenteMes = lista.Where(x => x.ABRIL != VARIABLES.FORMATOCERO && x.ABRIL != VARIABLES.FORMATOGUION).Count();
                    LicenciaSinperiodoMes = lista.Where(x => x.ABRIL == VARIABLES.FORMATOGUION).Count();
                    LicenciasQueSaldranenEmision = lista.Where(x => x.ABRIL == VARIABLES.FORMATOCERO).Count();
                    if (Tipo == VARIABLES.UNO)
                        lista = lista.Where(x => x.ABRIL == VARIABLES.FORMATOCERO).ToList();
                    else if (Tipo == VARIABLES.DOS)
                        lista = lista.Where(x => x.ABRIL != VARIABLES.FORMATOCERO && x.ABRIL != VARIABLES.FORMATOGUION).ToList();
                    else if (Tipo == VARIABLES.TRES)
                        lista = lista.Where(x => x.ABRIL == VARIABLES.FORMATOGUION).ToList();

                }
                else if (Mes == VARIABLES.MAYO)
                {
                    LicenciaCantidadDiferenteMes = lista.Where(x => x.MAYO != VARIABLES.FORMATOCERO && x.MAYO != VARIABLES.FORMATOGUION).Count();
                    LicenciaSinperiodoMes = lista.Where(x => x.MAYO == VARIABLES.FORMATOGUION).Count();
                    LicenciasQueSaldranenEmision = lista.Where(x => x.MAYO == VARIABLES.FORMATOCERO).Count();
                    if (Tipo == VARIABLES.UNO)
                        lista = lista.Where(x => x.MAYO == VARIABLES.FORMATOCERO).ToList();
                    else if (Tipo == VARIABLES.DOS)
                        lista = lista.Where(x => x.MAYO != VARIABLES.FORMATOCERO && x.MAYO != VARIABLES.FORMATOGUION ).ToList();
                    else if (Tipo == VARIABLES.TRES)
                        lista = lista.Where(x => x.MAYO == VARIABLES.FORMATOGUION).ToList();

                }
                else if (Mes == VARIABLES.JUNIO)
                {
                    LicenciaCantidadDiferenteMes = lista.Where(x => x.JUNIO != VARIABLES.FORMATOCERO && x.JUNIO != VARIABLES.FORMATOGUION).Count();
                    LicenciaSinperiodoMes = lista.Where(x => x.JUNIO == VARIABLES.FORMATOGUION).Count();
                    LicenciasQueSaldranenEmision = lista.Where(x => x.JUNIO == VARIABLES.FORMATOCERO).Count();
                    if (Tipo == VARIABLES.UNO)
                        lista = lista.Where(x => x.JUNIO == VARIABLES.FORMATOCERO).ToList();
                    else if (Tipo == VARIABLES.DOS)
                        lista = lista.Where(x => x.JUNIO != VARIABLES.FORMATOCERO && x.JUNIO != VARIABLES.FORMATOGUION).ToList();
                    else if (Tipo == VARIABLES.TRES)
                        lista = lista.Where(x => x.JUNIO == VARIABLES.FORMATOGUION).ToList();

                }
                else if (Mes == VARIABLES.JULIO)
                {
                    LicenciaCantidadDiferenteMes = lista.Where(x => x.JULIO != VARIABLES.FORMATOCERO && x.JULIO != VARIABLES.FORMATOGUION).Count();
                    LicenciaSinperiodoMes = lista.Where(x => x.JULIO == VARIABLES.FORMATOGUION).Count();
                    LicenciasQueSaldranenEmision = lista.Where(x => x.JULIO == VARIABLES.FORMATOCERO).Count();
                    if (Tipo == VARIABLES.UNO)
                        lista = lista.Where(x => x.JULIO == VARIABLES.FORMATOCERO).ToList();
                    else if (Tipo == VARIABLES.DOS)
                        lista = lista.Where(x => x.JULIO != VARIABLES.FORMATOCERO && x.JULIO != VARIABLES.FORMATOGUION).ToList();
                    else if (Tipo == VARIABLES.TRES)
                        lista = lista.Where(x => x.JULIO == VARIABLES.FORMATOGUION).ToList();

                }
                else if (Mes == VARIABLES.AGOSTO)
                {
                    LicenciaCantidadDiferenteMes = lista.Where(x => x.AGOSTO != VARIABLES.FORMATOCERO && x.AGOSTO != VARIABLES.FORMATOGUION).Count();
                    LicenciaSinperiodoMes = lista.Where(x => x.AGOSTO == VARIABLES.FORMATOGUION).Count();
                    LicenciasQueSaldranenEmision = lista.Where(x => x.AGOSTO == VARIABLES.FORMATOCERO).Count();
                    if (Tipo == VARIABLES.UNO)
                        lista = lista.Where(x => x.AGOSTO == VARIABLES.FORMATOCERO).ToList();
                    else if (Tipo == VARIABLES.DOS)
                        lista = lista.Where(x => x.AGOSTO != VARIABLES.FORMATOCERO && x.AGOSTO != VARIABLES.FORMATOGUION).ToList();
                    else if (Tipo == VARIABLES.TRES)
                        lista = lista.Where(x => x.AGOSTO == VARIABLES.FORMATOGUION).ToList();

                }
                else if (Mes == VARIABLES.SEPTIEMBRE)
                {
                    LicenciaCantidadDiferenteMes = lista.Where(x => x.SEPTIEMBRE != VARIABLES.FORMATOCERO && x.SEPTIEMBRE != VARIABLES.FORMATOGUION).Count();
                    LicenciaSinperiodoMes = lista.Where(x => x.SEPTIEMBRE == VARIABLES.FORMATOGUION).Count();
                    LicenciasQueSaldranenEmision = lista.Where(x => x.SEPTIEMBRE == VARIABLES.FORMATOCERO).Count();
                    if (Tipo == VARIABLES.UNO)
                        lista = lista.Where(x => x.SEPTIEMBRE == VARIABLES.FORMATOCERO).ToList();
                    else if (Tipo == VARIABLES.DOS)
                        lista = lista.Where(x => x.SEPTIEMBRE != VARIABLES.FORMATOCERO && x.SEPTIEMBRE != VARIABLES.FORMATOGUION).ToList();
                    else if (Tipo == VARIABLES.TRES)
                        lista = lista.Where(x => x.SEPTIEMBRE == VARIABLES.FORMATOGUION).ToList();

                }
                else if (Mes == VARIABLES.OCTUBRE)
                {
                    LicenciaCantidadDiferenteMes = lista.Where(x => x.OCTUBRE != VARIABLES.FORMATOCERO && x.OCTUBRE != VARIABLES.FORMATOGUION).Count();
                    LicenciaSinperiodoMes = lista.Where(x => x.OCTUBRE == VARIABLES.FORMATOGUION).Count();
                    LicenciasQueSaldranenEmision = lista.Where(x => x.OCTUBRE == VARIABLES.FORMATOCERO).Count();
                    if (Tipo == VARIABLES.UNO)
                        lista = lista.Where(x => x.OCTUBRE == VARIABLES.FORMATOCERO).ToList();
                    else if (Tipo == VARIABLES.DOS)
                        lista = lista.Where(x => x.OCTUBRE != VARIABLES.FORMATOCERO && x.OCTUBRE != VARIABLES.FORMATOGUION).ToList();
                    else if (Tipo == VARIABLES.TRES)
                        lista = lista.Where(x => x.OCTUBRE == VARIABLES.FORMATOGUION).ToList();

                }
                else if (Mes == VARIABLES.NOVIEMBRE)
                {
                    LicenciaCantidadDiferenteMes = lista.Where(x => x.NOVIEMBRE != VARIABLES.FORMATOCERO && x.NOVIEMBRE != VARIABLES.FORMATOGUION).Count();
                    LicenciaSinperiodoMes = lista.Where(x => x.NOVIEMBRE == VARIABLES.FORMATOGUION).Count();
                    LicenciasQueSaldranenEmision = lista.Where(x => x.NOVIEMBRE == VARIABLES.FORMATOCERO).Count();
                    if (Tipo == VARIABLES.UNO)
                        lista = lista.Where(x => x.NOVIEMBRE == VARIABLES.FORMATOCERO).ToList();
                    else if (Tipo == VARIABLES.DOS)
                        lista = lista.Where(x => x.NOVIEMBRE != VARIABLES.FORMATOCERO && x.NOVIEMBRE != VARIABLES.FORMATOGUION).ToList();
                    else if (Tipo == VARIABLES.TRES)
                        lista = lista.Where(x => x.NOVIEMBRE == VARIABLES.FORMATOGUION).ToList();


                }
                else if (Mes == VARIABLES.DICIEMBRE)
                {
                    LicenciaCantidadDiferenteMes = lista.Where(x => x.DICIEMBRE != VARIABLES.FORMATOCERO && x.DICIEMBRE != VARIABLES.FORMATOGUION).Count();
                    LicenciaSinperiodoMes = lista.Where(x => x.DICIEMBRE == VARIABLES.FORMATOGUION).Count();
                    LicenciasQueSaldranenEmision = lista.Where(x => x.DICIEMBRE == VARIABLES.FORMATOCERO).Count();
                    if (Tipo == VARIABLES.UNO)
                        lista = lista.Where(x => x.DICIEMBRE == VARIABLES.FORMATOCERO).ToList();
                    else if (Tipo == VARIABLES.DOS)
                        lista = lista.Where(x => x.DICIEMBRE != VARIABLES.FORMATOCERO && x.DICIEMBRE != VARIABLES.FORMATOGUION).ToList();
                    else if (Tipo == VARIABLES.TRES)
                        lista = lista.Where(x => x.DICIEMBRE == VARIABLES.FORMATOGUION).ToList();

                }
                else
                    lista = ListaReporte.Where(x => x.EMISION_PROD == VARIABLES.SILETRAS).ToList();
               

                ListaReporteAprob = lista;

                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblAdministracionRequerimiento' border=0 width='100%;' class='k-grid k-widget' id='tblAdministracionRequerimiento'>");
                shtml.Append("<thead><tr>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >EMISION AUTO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >RAZON</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >LOCAL</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DISTRITO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ENERO </th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FEBRERO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MARZO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ABRIL</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MAYO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >JUNIO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >JULIO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >AGOSTO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >SEPTIEMBRE</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >OCTUBRE</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >NOVIEMBRE</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DICIEMBRE</th>");
                if (lista != null)
                {
                    lista.ForEach(item =>
                    {
                        shtml.Append("<tr style='background-color:white'>");

                        //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDEstOri'>{0}</td>", item.CODIGO_LIC);
                        shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:left'; class='IDNomEstOri'>{0}</td>", item.EMISION_PROD);
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDDivEstOri'>{0}</td>", item.RAZONE);
                        shtml.AppendFormat("<td style='width:8%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.LOCAL);
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DISTRITO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.ENERO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.FEBRERO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.MARZO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.ABRIL);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.MAYO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.JUNIO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.JULIO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.AGOSTO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.SEPTIEMBRE);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.OCTUBRE);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.NOVIEMBRE);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DICIEMBRE);
                        //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='ModificarRequerimiento({0});'><img src='../Images/iconos/report_deta.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.ID_REQ, "Modificar");
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

                ENVIO_DATA datos = new ENVIO_DATA();
                datos.CantidadLicencias = ListaReporte.Where(x => x.EMISION_PROD == VARIABLES.SILETRAS).Count(); ;
                datos.CantidadLicenciasListaParaEmisionSeleccionada = LicenciasQueSaldranenEmision;
                datos.CantidadLicenciasPagoAdelantado = LicenciaCantidadDiferenteMes;
                datos.CantidadLicenciasSinPeriodo = LicenciaSinperiodoMes;



                retorno.result = VARIABLES.SI;
                retorno.message = shtml.ToString();
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                retorno.result = VARIABLES.NO;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult ListarLicenciaSeguimientoObservados(int razon)
        {
            Resultado retorno = new Resultado();
            var lista = new List<BEAdministracionSeguimientoLocalPermanente>();


            try
            {
                lista = ListaReporte.Where(x => x.EMISION_PROD == VARIABLES.NOLETRAS).ToList();
                if (razon == VARIABLES.UNO)
                    lista = lista.Where(x => x.RAZONE == VARIABLES.MOROSIDAD).ToList();
                else if (razon == VARIABLES.DOS)
                    lista = lista.Where(x => x.RAZONE == VARIABLES.RECALCULAR).ToList();
                else if (razon == VARIABLES.TRES)
                    lista = lista.Where(x => x.RAZONE == VARIABLES.MARCAREMISION).ToList();
                else if (razon == VARIABLES.CUATRO)
                    lista = lista.Where(x => x.RAZONE == VARIABLES.PERIODOBLOQUEADO).ToList();
                else
                    lista = ListaReporte.Where(x => x.EMISION_PROD == VARIABLES.NOLETRAS).ToList();

                ListaReporteObs = lista;

                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblAdministracionRequerimiento' border=0 width='100%;' class='k-grid k-widget' id='tblAdministracionRequerimiento'>");
                shtml.Append("<thead><tr>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >CODIGO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >EMISION AUTO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >RAZON</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >LOCAL</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DISTRITO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ENERO </th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >FEBRERO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MARZO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >ABRIL</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MAYO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >JUNIO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >JULIO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >AGOSTO</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >SEPTIEMBRE</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >OCTUBRE</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >NOVIEMBRE</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DICIEMBRE</th>");
                if (lista != null)
                {
                    lista.ForEach(item =>
                    {
                        shtml.Append("<tr style='background-color:white'>");

                        //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Checko' />", "chkEstOrigen" + item.codLicencia);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDEstOri'>{0}</td>", item.CODIGO_LIC);
                        shtml.AppendFormat("<td style='width:2%; cursor:pointer;text-align:left'; class='IDNomEstOri'>{0}</td>", item.EMISION_PROD);
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDDivEstOri'>{0}</td>", item.RAZONE);
                        shtml.AppendFormat("<td style='width:8%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.LOCAL);
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DISTRITO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.ENERO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.FEBRERO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.MARZO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.ABRIL);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.MAYO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.JUNIO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.JULIO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.AGOSTO);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.SEPTIEMBRE);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.OCTUBRE);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.NOVIEMBRE);
                        shtml.AppendFormat("<td style='width:4%; cursor:pointer;text-align:center'; class='IDOfiEstOri'>{0}</td>", item.DICIEMBRE);
                        //shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center'; class='IDOfiEstOri'><a href=# onclick='ModificarRequerimiento({0});'><img src='../Images/iconos/report_deta.png' border=0 title='{1}'></a>&nbsp;&nbsp;</td>", item.ID_REQ, "Modificar");
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


                ENVIO_DATA datos = new ENVIO_DATA();
                datos.CantidadLicenciasObservadas = ListaReporte.Where(x => x.EMISION_PROD == VARIABLES.NOLETRAS).Count();
                datos.CantidadLicenciasobservadasRazon = lista.Count();




                retorno.result = VARIABLES.SI;
                retorno.data = Json(datos,JsonRequestBehavior.AllowGet);
                retorno.message = shtml.ToString();




            }
            catch (Exception ex)
            {
                retorno.result = VARIABLES.NO;
            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult ReporteLicenciaSeguimiento(string formato,int ventana,string oficina,string periodo,string subtipo)
        {
            string format = formato;
            int oficina_id = 0;
//            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            string TipoReporte = "";

            Resultado retorno = new Resultado();

            try
            {

                List<BEAdministracionSeguimientoLocalPermanente> lstReporte = new List<BEAdministracionSeguimientoLocalPermanente>();

                if (ventana == 1)
                {
                    lstReporte = ListaReporte;
                    TipoReporte = "LISTADO GENERAL";
                }
                else if (ventana == 2)
                {
                    lstReporte = ListaReporteAprob;
                    TipoReporte = "LISTADO SIN OBSERVACION";
                }
                else if (ventana == 3)
                {
                    lstReporte = ListaReporteObs;
                    TipoReporte = "LISTADO CON OBSERVACIONES";
                }


               

                if (lstReporte.Count() > 0 && lstReporte != null)
                {
                    LocalReport localReport = new LocalReport();

                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_SEGUIMIENTO_LOCALES.rdlc");

                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = lstReporte;
                    localReport.DataSources.Add(reportDataSource);

                    ReportParameter paraOficinaSeleccionada = new ReportParameter();
                    paraOficinaSeleccionada = new ReportParameter("OficinaSeleccionada", oficina);
                    localReport.SetParameters(paraOficinaSeleccionada);

                    ReportParameter paraPeriodoEvaluar = new ReportParameter();
                    paraPeriodoEvaluar = new ReportParameter("PeriodoEvaluado", periodo);
                    localReport.SetParameters(paraPeriodoEvaluar);

                    ReportParameter paraTipoReporte = new ReportParameter();
                    paraTipoReporte = new ReportParameter("TipoReporte", TipoReporte);
                    localReport.SetParameters(paraTipoReporte);

                    ReportParameter paraSubTipoReporte = new ReportParameter();
                    paraSubTipoReporte = new ReportParameter("SubTipoReporte", subtipo);
                    localReport.SetParameters(paraSubTipoReporte);

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
                    //string deviceInfo = "<DeviceInfo>" +
                    //"  <OutputFormat>" + format + "</OutputFormat>" +
                    ////  "  <PageWidth>8.5in</PageWidth>" +
                    //"  <PageWidth>9in</PageWidth>" +
                    ////"  <PageHeight>11in</PageHeight>" +
                    //"  <PageHeight>16.3in</PageHeight>" +
                    //"  <MarginTop>0.0in</MarginTop>" +
                    //"  <MarginLeft>0.3in</MarginLeft>" +
                    //"  <MarginRight>0.0in</MarginRight>" +
                    //"  <MarginBottom>0.3in</MarginBottom>" +
                    //"</DeviceInfo>";
                    string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>" + format + "</OutputFormat>" +
                    //  "  <PageWidth>8.5in</PageWidth>" +
                    "  <PageWidth>11in</PageWidth>" +
                    //"  <PageHeight>11in</PageHeight>" +
                    "  <PageHeight>8.3in</PageHeight>" +
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
                    retorno.result = 1;
                    localReport.DisplayName = "Reporte  de Licencias Seguimiento ";

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
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "REPORTE  DE LICENCIA SEGUIMIENTO", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }


        public ActionResult ReporteTipo()
        {
            Resultado retorno = new Resultado();
          
            try
            {

                List<BEAdministracionSeguimientoLocalPermanente> listar = new List<BEAdministracionSeguimientoLocalPermanente>();
                listar = ListaReporte;              

                if (listar != null && listar.Count > 0)
                {
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "REALIZAR BUSQUEDA ANTES DE (VER PDF / REP. EXCEL) |  LA BUSQUEDA NO DEVOLVIO NINGUN RESULTADO";
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "REPORTE DE LICENCIA SEGUIMIENTO", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Recalcular(string anio ,int MesEva)
        {
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            
            Resultado retorno = new Resultado();
            var lista = new List<BEAdministracionSeguimientoLocalPermanente>();
            List<BELicencias> ListaMontoLirics = new List<BELicencias>();
            decimal resultLicencia = 0;
            try
            {

                //List<BEAdministracionSeguimientoLocalPermanente> listar = new List<BEAdministracionSeguimientoLocalPermanente>();
                //listar = ListaReporte;                
                lista = ListaReporte.Where(x => x.EMISION_PROD == VARIABLES.NOLETRAS).ToList();
                lista = lista.Where(x => x.RAZONE == VARIABLES.RECALCULAR).ToList();
                if (lista != null && lista.Count > 0)
                {
                    foreach (var item in lista)
                    {
                        if (item.ENERO != "-" && item.ENERO != "0.000000")
                        {
                            var valida = ValidarLicenciaPadre(item.CODIGO_LIC);
                            decimal lic_pl_id = new BLAdministracionSeguimientoLocalPermanente().Recuperar_Lic_PL_ID(item.CODIGO_LIC, anio, MesEva);
                            if (valida == 0 && lic_pl_id!=0) // SI ES UNA LIC INDIVIDUAL 
                            {
                                Recaudacion.FacturacionController servCalculo = new Recaudacion.FacturacionController();
                                var montos = servCalculo.obtenerMontoFacturaCalc(item.CODIGO_LIC, lic_pl_id);

                                ListaMontoLirics.Add(new BELicencias
                                {
                                    LIC_ID = item.CODIGO_LIC,
                                    MONTO_LIRICS_BRUTO = Convert.ToDecimal(montos.ValorTarifa),
                                    MONTO_LIRICS_DCTO = Convert.ToDecimal(montos.ValorDescuento),
                                    MONTO_LIRICS_NETO = Convert.ToDecimal(montos.ValorFinal),
                                    DESCUENTO_REDONDEO = Convert.ToDecimal(montos.ValorDescuentoRedondeoEspecial),
                                    LOG_USER_UPDAT = usuario

                                });


                                //new BLLicencias().ActualizaLicenciaMontos(item.CODIGO_LIC, Convert.ToDecimal(montos.ValorTarifa), Convert.ToDecimal(montos.ValorDescuento), Convert.ToDecimal(montos.ValorFinal), Convert.ToDecimal(montos.ValorDescuentoRedondeoEspecial));
                                //    // UPDATE  DE MONTOS .}
                            }
                        }

                    }
                    resultLicencia = new BLLicencias().ActualizarMontoLirics(ListaMontoLirics);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "REALIZAR BUSQUEDA ANTES DE (VER PDF / REP. EXCEL) |  LA BUSQUEDA NO DEVOLVIO NINGUN RESULTADO";
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "REPORTE DE LICENCIA SEGUIMIENTO", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public int ValidarLicenciaPadre(decimal CodLic)
        {
            Resultado retorno = new Resultado();
            int respuesta = 0;
            try
            {
                if (!isLogout(ref retorno))
                {
                    respuesta = new BLLicencias().ValidarLicenciasMultiplesPadres(CodLic);
                }

            }
            catch (Exception ex)
            {
            }
            return respuesta;

        }
    }
}



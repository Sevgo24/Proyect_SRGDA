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
using SGRDA.BL.BLAlfresco;
using System.Xml.Xsl;
using Microsoft.Office.Interop.Word;



namespace Proyect_Apdayc.Controllers.Licenciamiento
{
    public class LicenciaController : Base
    {
        //Lista Necesaria para guardar las Caracteristicas
        List<BECaracteristicaLic> ListarCaracterRegxLic = new List<BECaracteristicaLic>();

        //Temporales
        private class Variables
        {
            public const int SI = 1;
            public const int NO = 0;
            public const int ERROR = -1;
            public const string MENSAJE_OK_INSERT_PL = "SE INSERTO CORRECTAMENTE EL PLANEMIENTO";
            public const string MENSAJE_NO_OK_INSERT_PL = "NO SE INSERTO EL PLANEMIENTO | YA EXISTE PLANEAMIENTO EN EL RANGO DETALLADO";
            public const string MENSAJE_OK_ACTUALIZO_CADENA = "SE AGREGO LA LICENCIA A LA CADENA CORRECTAMENTE";
            public const string MENSAJE_NO_OK_ACTUALIZO_CADENA = " |NO SE AGREGO LA LICENCIA A LA CADENA | NO ES UN LOCAL PERMANENTE | ";
            public const string MENSAJE_OK_ACTULIZA_PERIODO = "SE ACTUALIZO CORRECTAMENTE EL ESTADO DEL PERIODO";
            public const string MENSAJE_NO_OK_ACTULIZA_PERIODO = " NO SE ACTUALIZO CORRECTAMENTE EL ESTADO DEL PERIODO | CONSULTE CON EL ADMINISTRADOR ENCARGADO";
            public const string MENSAJE_NO_VALIDA_CORRECTAMENTE_PERIODO = "NO SE VALIDO CORRECTAMENTE EL PERIODO | CONTACTE CON EL ADMINISTRADOR Y BRINDE EL CODIGO DE LICENCIA PARA SU ATENCION";
            public const string MENSAJE_PERIODOS_ACTUALIZADOS_SIN_CAMBIOS = "SE ACTUALIZO EL ESTADO DE PERIODO | SIN CAMBIOS";
            public const string MENSAJE_USUARIO_MOROSO = "USUARIO RENUENTE: CONTACTARSE CON LA JEFATURA DE CANTA AUTOR EN RUTA";
            public const string MENSAJE_ERROR_AL_VALIDAR_USUARIO_MOROSO = "OCURRIO UN ERROR AL VALIDAR EL USUARIO Y LA MOROSIDAD | CONTACTE CON EL ADMINISTRADOR DEL MODULO ";
            public const string MENSAJE_SOCIO_NO_TIENE_TELEF_CORREO = "EL SOCIO NO TIENE REGISTRADO : CORREO O TELEFONO . REGISTRELO PARA PODER GRABAR LOS CAMBIOS | SI ESTO ES UN ERROR CONTACTE CON EL ADMINISTRADOR DEL MODULO ";
            public const string MENSAJE_ERROR_AL_VALIDAR_USUARIO_TELEFT_CORREO = "OCURRIO UN ERROR AL VALIDAR EL USUARIO , SUS CORREOS Y TELEFONOS | CONTACTE CON EL ADMINISTRADOR DEL MODULO ";
            public const string MENSAJE_LICENCIA_NO_CUMPLE_REQUISITOS = "POR FAVOR DE AGREGAR TODOS LOS DOCUMENTOS NECESARIOS PARA PODER INACTIVAR ESTA LICENCIA";
            public const string MENSAJE_ERROR_VALIDAR_LICENCIA_REQ = "OCURRIO UN ERROR AL VALIDAR LA LICENCIA , CONTACTE CON EL ADMINISTRADOR DEL MODULO ;";
            
        }


        private const string K_SESION_LICENCIA_TRASLADO = "___dTOPLicenciaTraslado_LIC";
        private const string K_SESION_LICENCIA_TRASLADO_DEST = "___dTOPLicenciaTrasladoDest_LIC";
        private const string K_SESION_LICENCIA_SELECCIONADAS = "___DTOLicenciasSeleccionadas";
        private const string K_SESION_PLANIFICACION = "___DTOPlanificacion";
        private const string K_SESION_PLANIFICACION_ACT = "___DTOPlanificacionACT";
        private const string K_SESION_DESCUENTO = "___DTODescuento";
        private const string K_SESION_TARIFA_CAR = "___DTOTarifaCar";
        //Sesiones para Cadenas
        public static string K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP = "__DTOEstablecimientoSocioEmpresarialTmp";
        public static string K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP_SEG = "__DTOEstablecimientoSocioEmpresarialSegTmp";

        List<DTOLicenciaPlaneamiento> planificacion = new List<DTOLicenciaPlaneamiento>();
        List<DTOLicencia> ListaLicencias = new List<DTOLicencia>();

        //Variables Grobales Para Saber SI ES UNA LICENCIA MULTIPLE 
        int Global_Valida_Lic_Mult = 0;
        private List<DTOLicenciaPlaneamiento> PlanificacionTmpUP
        {
            get
            {
                return (List<DTOLicenciaPlaneamiento>)Session[K_SESION_PLANIFICACION_ACT];
            }
            set
            {
                Session[K_SESION_PLANIFICACION_ACT] = value;
            }
        }
        private List<DTOLicenciaPlaneamiento> PlanificacionTmp
        {
            get
            {
                return (List<DTOLicenciaPlaneamiento>)Session[K_SESION_PLANIFICACION];
            }
            set
            {
                Session[K_SESION_PLANIFICACION] = value;
            }
        }
        public List<DTOEstablecimiento> EstablecimientoSocioEmpresarialTmp
        {
            get
            {
                return (List<DTOEstablecimiento>)Session[K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP];
            }
            set
            {
                Session[K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP] = value;
            }
        }

        public List<DTOEstablecimiento> EstablecimientoSocioEmpresarialDestinoTmp
        {
            get
            {
                return (List<DTOEstablecimiento>)Session[K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP_SEG];
            }
            set
            {
                Session[K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP_SEG] = value;
            }
        }
        public List<DTOLicencia> licenciaTemporalTmp
        {
            get
            {
                return (List<DTOLicencia>)Session[K_SESION_LICENCIA_TRASLADO];
            }
            set
            {
                Session[K_SESION_LICENCIA_TRASLADO] = value;
            }
        }

        public List<DTOLicencia> LicenciaTemporalDestinoTmp
        {
            get
            {
                return (List<DTOLicencia>)Session[K_SESION_LICENCIA_TRASLADO_DEST];
            }
            set
            {
                Session[K_SESION_LICENCIA_TRASLADO_DEST] = value;
            }
        }

        public List<BELicencias> LicenciaTemporalSeleccionadas
        {
            get
            {
                return (List<BELicencias>)Session[K_SESION_LICENCIA_SELECCIONADAS];
            }
            set
            {
                Session[K_SESION_LICENCIA_SELECCIONADAS] = value;
            }
        }
        //
        // GET: /Licencia/

        public ActionResult Index()
        {
            Init(false);
            return View();
        }
        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESION_PLANIFICACION_ACT);
            Session.Remove(K_SESION_PLANIFICACION);
            Session.Remove(K_SESION_DESCUENTO);
            Session.Remove(K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP);
            Session.Remove(K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP_SEG);
            ViewBag.HasAccess = true;
            //if (Request.QueryString["set"]!=null  )
            //{
            //    //decimal codLicencia=0;
            //    //var isDecimal = decimal.TryParse(Convert.ToString(Request.QueryString["set"]), out codLicencia);
            //    //if (codLicencia > 0)
            //    //{
            //    //    var tienePermiso = new SeguridadController().PuedeEditarLic(Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]), codLicencia, Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]));
            //    //    if (!tienePermiso) //si no tiene permiso
            //    //    {
            //    //        ViewBag.HasAccess = false;
            //    //    }
            //    //}
            //}
            return View();
        }

        public JsonResult USP_LICENCIAMIENTO_LISTARPAGEJSON(int skip, int take, int page, int pageSize, decimal LIC_ID, decimal LIC_TYPE, decimal LICS_ID, string CUR_ALPHA, decimal MOD_ID, decimal EST_ID, decimal BPS_ID, string LIC_NAME, string LIC_TEMP, decimal RATE_ID, decimal LICMAS, decimal BPS_GROUP, decimal BPS_GROUP_FACT, int conFecha, string finiauto, string ffinauto, string desc_artista, decimal cod_artista_sgs, int estadoLic)
        {
            Resultado retorno = new Resultado();
            List<BELicencias> lista = new List<BELicencias>();

            try
            {
                DateTime finiautodate = Convert.ToDateTime(finiauto);
                DateTime ffinautodate = Convert.ToDateTime(ffinauto);
                lista = USP_LICENCIA_LISTARPAGE(GlobalVars.Global.OWNER, LIC_ID, LIC_TYPE, LICS_ID, CUR_ALPHA, MOD_ID, EST_ID, BPS_ID, LIC_NAME, LIC_TEMP, RATE_ID, LICMAS, BPS_GROUP, BPS_GROUP_FACT, conFecha, finiautodate, ffinautodate, desc_artista, cod_artista_sgs, estadoLic, page, pageSize);

            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "LISTAR Licencia", ex);
            }

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BELicencias { ListaLicencias = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BELicencias { ListaLicencias = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult USP_LICENCIAMIENTO_LISTARPAGEJSON2(int skip, int take, int page, int pageSize, decimal LIC_ID, decimal MOD_ID, decimal EST_ID, decimal BPS_ID, string LIC_NAME, string LIC_TEMP, decimal RATE_ID)
        {
            Resultado retorno = new Resultado();
            List<BELicencias> lista = new List<BELicencias>();

            try
            {
               
                lista = USP_LICENCIA_LISTARPAGE2(GlobalVars.Global.OWNER, LIC_ID, MOD_ID, EST_ID, BPS_ID, LIC_NAME, LIC_TEMP, RATE_ID, page, pageSize);
                if (lista != null)
                {
                    ListaLicencias = new List<DTOLicencia>();
                    lista.ForEach(s =>
                    {
                        int valEst = 0; //  
                        if (LicenciaTemporalDestinoTmp != null)
                            valEst = LicenciaTemporalDestinoTmp.Where(x => x.codLicencia == s.LIC_ID).Count();// SI LA LISTA DESTINO TIENE UN CODIGO DE LICENCIA IGUAL NO LO LISTA EN EL ORIGEN

                        if (valEst == 0) // si no existe en la lista destino pues listar
                        {
                            ListaLicencias.Add(new DTOLicencia
                            {
                                codLicencia = s.LIC_ID,
                                nombreLicencia = s.LIC_NAME,
                                DIVISION = s.DIVISION,
                                OFICINA = s.OFICINA,
                                codEstablecimiento=s.EST_ID,
                                codUsuDerecho = s.BPS_ID
                                //DIVISION= s.DIS
                            });
                        }
                    });
                    licenciaTemporalTmp = ListaLicencias;


                }
            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "LISTAR Licencia", ex);
            }

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BELicencias { ListaLicencias = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BELicencias { ListaLicencias = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }
        public List<BELicencias> USP_LICENCIA_LISTARPAGE(string owner, decimal LIC_ID, decimal LIC_TYPE, decimal LICS_ID, string CUR_ALPHA, decimal MOD_ID, decimal EST_ID, decimal BPS_ID, string LIC_NAME, string LIC_TEMP, decimal RATE_ID, decimal LICMAS, decimal BPS_GROUP, decimal BPS_GROUP_FACT, int confecha, DateTime finiauto, DateTime ffinauto, string desc_artista, decimal cod_artista_sgs, int estadoLic, int pagina, int cantRegxPag)
        {
            decimal idOficina = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
            string idPerfil = Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]);
            var idPerfilAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["idPerfilAdminSeg"];
            int oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina_id));
            //10081,  10082
            //if (idPerfil == Convert.ToString(idPerfilAdmin))
            if (opcAdm == 1)
            {
                idOficina = 0;
            }
            return new BLLicencias().usp_Get_LicenciaPage(owner, LIC_ID, LIC_TYPE, LICS_ID, CUR_ALPHA, MOD_ID, EST_ID, BPS_ID, LIC_NAME, LIC_TEMP, RATE_ID, LICMAS, BPS_GROUP, BPS_GROUP_FACT, confecha, finiauto, ffinauto, desc_artista, cod_artista_sgs, pagina, cantRegxPag, idOficina, estadoLic);
        }
        public List<BELicencias> USP_LICENCIA_LISTARPAGE2(string owner, decimal LIC_ID, decimal MOD_ID, decimal EST_ID, decimal BPS_ID, string LIC_NAME, string LIC_TEMP, decimal RATE_ID,  int pagina, int cantRegxPag)
        {
            decimal idOficina = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
            string idPerfil = Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]);
            var idPerfilAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["idPerfilAdminSeg"];
            int oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina_id));
            //10081,  10082
            //if (idPerfil == Convert.ToString(idPerfilAdmin))
            if (opcAdm == 1)
            {
                idOficina = 0;
            }
            return new BLLicencias().usp_Get_LicenciaPage2(owner, LIC_ID, MOD_ID, EST_ID, BPS_ID, LIC_NAME, LIC_TEMP, RATE_ID, pagina, cantRegxPag, idOficina);
        }

        public JsonResult ListarLicenciasTrasladoDestino()
        {
            Resultado retorno = new Resultado();

            try
            {

                List<DTOLicencia> lista = LicenciaTemporalDestinoTmp;
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table class='tblLicenciasFin' border=0 width='100%;' class='k-grid k-widget' id='tblLicenciasFin'>");
                    shtml.Append("<thead><tr>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'><input type='checkbox' id='idCheckd' name='Check' class='Checkd' onchange='clickCheckDestino()'></th>");
                    //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >X</th>");
                    //shtml.Append("<th style='display:none'  class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>ID</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Código </th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Tipo</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Nombre</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Responsable</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Modalidad</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Estado</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Establecimiento</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >MONTO</th>");
                    shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >DESCUENTO</th>");
                    if (lista != null)
                    {
                        foreach (var item in lista.OrderBy(x => x.codLicencia))
                        {
                            shtml.Append("<tr style='background-color:white'>");

                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellFin' ><input type='checkbox' id='{0}' name='Check' class='Checkd' />", "chkEstFin" + item.codLicencia);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:right'; class='IDEstFin'>{0}</td>", item.codLicencia);
                            shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:right'; class='IDTipFin'>{0}</td>", item.tipoLicencia);
                            shtml.AppendFormat("<td style='width:30%; cursor:pointer;text-align:left';'class='IDNomEstFin'>{0}</td>", item.nombreLicencia);
                            shtml.AppendFormat("<td style='width:25%;cursor:pointer;text-align:left'; class='IDRespFin'>{0}</td>", item.UsuarioDerecho);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDModalidadFin'>{0}</td>", item.Modalidad);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDEstadoFin'>{0}</td>", item.estadoLicencia);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDEstFin'>{0}</td>", item.Establecimiento);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDMontoFin'>{0}</td>", item.Monto);
                            shtml.AppendFormat("<td style='width:25%; cursor:pointer;text-align:left'; class='IDDescFin'>{0}</td>", item.descLicencia);

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
                    retorno.result = 1;
                }



            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Eliminar(decimal codigo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLLicencias licencia = new BLLicencias();
                    var resultado = licencia.Eliminar(new BELicencias
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        LIC_ID = codigo,
                        LOG_USER_UPDAT = UsuarioActual
                    });
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Eliminar Licencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region DavidInsertarLicenciaIndvMult
        public JsonResult Insertar(DTOLicencia entidad)
        {
            Resultado retorno = new Resultado();
            //variable
            var autorizaActualizacion = 0;
            List<BELicencias> listalicpadre = new List<BELicencias>();
            try
            {
                if (!isLogout(ref retorno))
                {

                    bool tienePermiso = true;
                    if (entidad.codLicencia > 0)
                    {
                        tienePermiso = new SeguridadController().PuedeEditarLic(Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]), entidad.codLicencia, Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]));
                    }
                    else
                    {

                        tienePermiso = true;//aplicar logica cuando es nuevo registro de licencia
                    }
                    if (!tienePermiso)
                    {
                        retorno.message = Constantes.MensajeGenerico.MSG_SIN_PERMISO_EDIT_LIC;
                        retorno.result = 0;
                    }
                    else
                    {
                        int oficina = Convert.ToInt32(Session[Constantes.Sesiones.CodigoOficina].ToString());
                        int validacionUbigeo = new BLDivision().
                            ValidarUbigeoXEstablecimiento(GlobalVars.Global.OWNER, Convert.ToInt32(entidad.codEstablecimiento), oficina);

                        if (validacionUbigeo == 0)
                        {
                            retorno.result = 2;
                            retorno.message = System.Configuration.ConfigurationManager.AppSettings["MSG_VAL_UBIGEO"];
                        }
                        else
                        {
                            BLLicencias licencia = new BLLicencias();
                            bool flag_exito = validarInsert(entidad, retorno);
                            if (flag_exito)
                            {

                                #region LicencianInsertaModificaCabezera
                                BELicencias obj = new BELicencias();

                                obj.LIC_ID = entidad.codLicencia;
                                obj.LIC_CREQ = entidad.IndUpdCaracteristicas == "true" ? "1" : "0";
                                obj.LIC_DISC = entidad.IndDscVisible == "true" ? "1" : "0";
                                obj.LIC_DREQ = entidad.IndReqReporte == "true" ? "1" : "0";
                                obj.INVG_ID = entidad.GrupoFacturacion;
                                obj.INVF_ID = Convert.ToDecimal(entidad.FormaEntregaFact);
                                obj.LIC_NAME = entidad.nombreLicencia;
                                obj.LIC_DESC = entidad.descLicencia == null ? "" : entidad.descLicencia;
                                obj.CUR_ALPHA = entidad.codMoneda;
                                obj.LIC_TYPE = entidad.tipoLicencia;
                                obj.LIC_MASTER = entidad.codLicenciaPadre;
                                obj.LICS_ID = entidad.codEstado;
                                obj.MOD_ID = entidad.codModalidad;
                                obj.EST_ID = entidad.codEstablecimiento;
                                obj.BPS_ID = entidad.codUsuDerecho;
                                obj.OWNER = GlobalVars.Global.OWNER;
                                obj.RATE_ID = entidad.codTarifa;
                                obj.RAT_FID = entidad.codTemporalidad;
                                obj.PAY_ID = entidad.codTipoPago;
                                obj.LOG_USER_CREAT = UsuarioActual;

                                var generado = obj.LIC_ID;
                                if (obj.LIC_ID > 0)
                                {
                                    obj.LOG_USER_UPDAT = UsuarioActual;
                                    licencia.Actualizar(obj);

                                    if (obj.LIC_MASTER != null || obj.LIC_MASTER > 0)
                                        autorizaActualizacion = 1;
                                }
                                else
                                {

                                    autorizaActualizacion = 0;

                                    generado = licencia.Insertar(obj);
                                }
                                //Verificando si se inserto una Licencia Multiple 
                                ValidarLicenciaMultiplesPadre(generado);

                                retorno.valor = Convert.ToString(generado);
                                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                                retorno.result = 1;
                                #endregion

                                //Si la Cabezera Se Actualiza Se Buscan Sus licencias Hijas Si es que tiene 
                                if (autorizaActualizacion == 1)
                                {
                                    String owner = GlobalVars.Global.OWNER;
                                    Decimal codLicPad = entidad.codLicenciaPadre;
                                    listalicpadre = licencia.ListarLicenciaHijasxCodMult(owner, codLicPad);
                                    List<DTOEstablecimiento> listaEst = new List<DTOEstablecimiento>();
                                    listaEst = new List<DTOEstablecimiento>();

                                }
                                #region LicenciaActualizarMultiple
                                //Bucle Par Insertar 
                                //if (EstablecimientoSocioEmpresarialDestinoTmp != null)
                                //SI ES NULO ES POR QUE NO SE HAN BUSCADO  ESTABLECIMIENTOS Y SOLO ES ESTA ACTUALZIANDO DATOS DE LA LICENCIA 
                                if (Global_Valida_Lic_Mult == 1)//valida licencia padre
                                {
                                    //Solo Modifica
                                    #region modifica
                                    //BUCLE PARA RECORRER SI  LOS ESTABLECIMIENTOS SON IGUALES SE RECUPERA EL LIC_ID ,ESTO SERIA NECESARIO PARA MODIFICAR 
                                    List<BELicencias> listalicenciasxml = new List<BELicencias>();
                                    List<BELicencias> listalicenciasinsertxml = new List<BELicencias>();
                                    if (listalicpadre != null && listalicpadre.Count > 0)
                                    {
                                        foreach (var Item2 in listalicpadre.OrderBy(x => x.EST_ID))
                                        {
                                            BELicencias obj2 = new BELicencias();
                                            obj2.LIC_ID = Item2.LIC_ID;
                                            obj2.LIC_CREQ = entidad.IndUpdCaracteristicas == "true" ? "1" : "0";
                                            obj2.LIC_DISC = entidad.IndDscVisible == "true" ? "1" : "0";
                                            obj2.LIC_DREQ = entidad.IndReqReporte == "true" ? "1" : "0";
                                            obj2.INVG_ID = entidad.GrupoFacturacion;
                                            obj2.INVF_ID = Convert.ToDecimal(entidad.FormaEntregaFact);
                                            //No modificar el Nombre de la Licencia
                                            obj2.LIC_NAME = Item2.LIC_NAME;
                                            //obj2.LIC_DESC = entidad.descLicencia; 
                                            obj2.LIC_DESC = "Esta Licencia ,Esta Asociada con La Licencia :" + entidad.codLicencia + ".";
                                            obj2.CUR_ALPHA = entidad.codMoneda;
                                            obj2.LIC_TYPE = 2;
                                            obj2.LIC_MASTER = generado; //cambiar entidad.codLicenciaPadre
                                            obj2.LICS_ID = entidad.codEstado;
                                            obj2.MOD_ID = entidad.codModalidad;
                                            obj2.EST_ID = Item2.EST_ID;
                                            obj2.BPS_ID = entidad.codUsuDerecho;
                                            obj2.OWNER = GlobalVars.Global.OWNER;
                                            obj2.RATE_ID = entidad.codTarifa;
                                            obj2.RAT_FID = entidad.codTemporalidad;
                                            obj2.PAY_ID = entidad.codTipoPago;
                                            obj2.LOG_USER_CREAT = UsuarioActual;

                                            var generado2 = obj2.LIC_ID;
                                            if (obj2.LIC_ID > 0)
                                            {
                                                obj2.LOG_USER_UPDAT = UsuarioActual;
                                                // licencia.Actualizar(obj2);
                                                listalicenciasxml.Add(obj2);
                                            }
                                            //Recupera el Temporal con la lista que solo se va a insertar 
                                            EstablecimientoSocioEmpresarialDestinoTmp = EstablecimientoSocioEmpresarialDestinoTmp.Where(x => x.Codigo != Item2.EST_ID).ToList();


                                        }
                                        new BLLicencias().ActualizaLicenciasHijasXML(listalicenciasxml);
                                        //retorno.valor = Convert.ToString(generado2);
                                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                                        retorno.result = 1;
                                    }
                                    //Bucle para poder Insertar
                                    if ((EstablecimientoSocioEmpresarialDestinoTmp != null))
                                    {
                                        foreach (var item in EstablecimientoSocioEmpresarialDestinoTmp.OrderBy(x => x.Codigo))
                                        {
                                            BELicencias obj2 = new BELicencias();
                                            //obj2.LIC_ID = Item2.LIC_ID;
                                            obj2.LIC_CREQ = entidad.IndUpdCaracteristicas == "true" ? "1" : "0";
                                            obj2.LIC_DISC = entidad.IndDscVisible == "true" ? "1" : "0";
                                            obj2.LIC_DREQ = entidad.IndReqReporte == "true" ? "1" : "0";
                                            obj2.INVG_ID = entidad.GrupoFacturacion;
                                            obj2.INVF_ID = Convert.ToDecimal(entidad.FormaEntregaFact);
                                            //PONER NOMBRE EN DURO PARA NO CONFUNDIR "entidad.nombreLicencia"
                                            obj2.LIC_NAME = "Licencia Hija " + item.Nombre;
                                            //obj2.LIC_DESC = entidad.descLicencia == null ? "" : entidad.descLicencia + " " + Item.Nombre; 
                                            obj2.LIC_DESC = "Esta Licencia ,Esta Asociada con La Licencia :" + entidad.codLicencia + ".";
                                            obj2.CUR_ALPHA = entidad.codMoneda;
                                            obj2.LIC_TYPE = 2;
                                            obj2.LIC_MASTER = generado; //cambiar entidad.codLicenciaPadre
                                            obj2.LICS_ID = entidad.codEstado;
                                            obj2.MOD_ID = entidad.codModalidad;
                                            obj2.EST_ID = item.Codigo;
                                            obj2.BPS_ID = entidad.codUsuDerecho;
                                            obj2.OWNER = GlobalVars.Global.OWNER;
                                            obj2.RATE_ID = entidad.codTarifa;
                                            obj2.RAT_FID = entidad.codTemporalidad;
                                            obj2.PAY_ID = entidad.codTipoPago;
                                            obj2.LOG_USER_CREAT = UsuarioActual;

                                            var generado2 = obj2.LIC_ID;

                                            if (obj2.LIC_ID == 0)
                                            {
                                                //Lista las licencias Hijas por codigo de licencia padre
                                                listalicenciasinsertxml.Add(obj2);
                                            }

                                        }  //  generado2 = licencia.Insertar(obj2);

                                        var listalic = licencia.ListarLicenciaHijasxCodMult(GlobalVars.Global.OWNER, entidad.codLicenciaPadre);

                                        List<BELicencias> ListaLicenciasInsertadasXml = new BLLicencias().InsertaLicenciaHijaXML(listalicenciasinsertxml);
                                        //*********************************************************************************************************************************************************************************************
                                        //Insertar Automaticamente Los Descuentos Al crear 

                                        List<BELicenciaPlaneamiento> lista = new List<BELicenciaPlaneamiento>();
                                        foreach (var l in ListaLicenciasInsertadasXml.OrderBy(l => l.LIC_ID))
                                        {
                                            ////**********************descuentos*************************************************
                                            //    BLLicenciaDescuento servicio = new BLLicenciaDescuento();
                                            //    BEDescuentos en = new BEDescuentos();
                                            //    en.Descuentos = servicio.ListaDescuentos(GlobalVars.Global.OWNER, l.LIC_ID);
                                            ////*********************************************************************************

                                            //******************************* PLANEAMIENTO ******************************************************

                                            //Aqui deberia realizarce el Insert  Del planeamiento y las caractersticas}
                                            if (listalic.Count > 0 && listalic != null) //si listalic.count =0 quiere decir que es la primera vez que se inserta ...(Y no debieria realizar el bucle)
                                            {
                                                decimal licid = listalic.OrderBy(x => x.LIC_ID).FirstOrDefault().LIC_ID;  //Trae el Id de una licencia ya registrada en la licencia padre para poder obtener el historial de su planeamiento

                                                //planeamiento y caracteristicas automaticamente.
                                                int res = new BLLicencias().ValidaLicenciaCaract(GlobalVars.Global.OWNER, licid);//SI RES =1 TIENE CARACT REGISTRADAS , RES=0 NO

                                                //lista el planeamiento por licencia hija
                                                var listaplan = new BLLicenciaPlaneamiento().ListaPlaneamientoxLicHij(GlobalVars.Global.OWNER, licid);

                                                if ((listaplan.Count > 0 && listaplan != null) && res == 1)  //SI LA LSITA DE PLAN ES MAYOR A 0 Y RES==1 ENTONCES REALIZAR
                                                {
                                                    foreach (var lic in listaplan.OrderBy(lic => lic.LIC_YEAR))
                                                    {
                                                        BELicenciaPlaneamiento objplan = new BELicenciaPlaneamiento();
                                                        objplan.LIC_ID = l.LIC_ID;
                                                        objplan.LIC_YEAR = lic.LIC_YEAR;
                                                        objplan.LIC_MONTH_DESC = Convert.ToString(lic.LIC_MONTH);
                                                        objplan.LIC_ORDER = lic.LIC_ORDER;
                                                        objplan.LIC_DATE = lic.LIC_DATE;
                                                        objplan.LOG_USER_CREAT = UsuarioActual;
                                                        //objplan.BLOCK_ID = null;
                                                        objplan.PAY_ID = lic.PAY_ID;
                                                        //  new BLLicenciaPlaneamiento().Insertar(GlobalVars.Global.OWNER, l.LIC_ID, lic.LIC_YEAR, Convert.ToString(lic.LIC_MONTH), lic.LIC_ORDER, lic.LIC_DATE, UsuarioActual, null, lic.PAY_ID, true);
                                                        lista.Add(objplan);
                                                    }
                                                    //Metodo que permite Insertar caracteristicas con el CODLIC recuperado
                                                    // InsertarCaractAutoxPeriodo(l.LIC_ID);
                                                }
                                            }


                                        }

                                        List<BELicenciaPlaneamiento> listap = new BLLicenciaPlaneamiento().InsertaPlaneamientoLicHijaXML(lista);//enviando la lista que recupere
                                        //Las caracteristicas una vez insertado EL PLAN
                                        // foreach (var l in ListaLicenciasInsertadasXml.OrderBy(l => l.LIC_ID))
                                        //  {
                                        InsertarCaractAutoxPeriodo(ListaLicenciasInsertadasXml, listap);
                                        //}

                                    }
                                    // retorno.valor = Convert.ToString(generado2);
                                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                                    retorno.result = 1;

                                    #endregion
                                }
                                #endregion
                            }
                            else
                            {
                                retorno.result = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Insertar Licencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public void InsertaModificaLicencia(DTOLicencia entidad)
        {

        }

        #endregion

        private bool validarInsert(DTOLicencia entidad, Resultado retorno)
        {
            bool flag_exito = true;
            BLLicencias licencia = new BLLicencias();
            if (entidad.codModalidad == 0) { flag_exito = false; retorno.message = "Seleccione Modalidad."; }
            if (entidad.codLicenciaPadre != 0)
            {
                if (flag_exito && entidad.codEstablecimiento == 0) { flag_exito = false; retorno.message = "Seleccione Establecimiento."; }
            }
            if (flag_exito && entidad.codUsuDerecho == 0) { flag_exito = false; retorno.message = "Seleccione Responsable."; }
            if (flag_exito && !(licencia.ValidarLicenciaMultiple(GlobalVars.Global.OWNER, entidad.tipoLicencia, entidad.codLicenciaPadre))) { flag_exito = false; retorno.message = "Código de Licencia Multiple no existe."; }
            if (flag_exito && entidad.codTarifa == 0) { flag_exito = false; retorno.message = "Seleccione Temporalidad."; }
            if (flag_exito && entidad.codTemporalidad == 0) { flag_exito = false; retorno.message = "Seleccione Modalidad."; }

            return flag_exito;
        }

        public JsonResult ListarTabs(decimal codigoEstado, decimal codigoWF)
        {
            Resultado retorno = new Resultado();
            try
            {
                //BLLicencias licencia = new BLLicencias();
                //var resultado = licencia.ListarTabsXEstadoLic(Convert.ToString(codigoEstado), GlobalVars.Global.OWNER);

                var data = new BLREC_LIC_TAB_STAT().TabxEstado(GlobalVars.Global.OWNER, codigoEstado, codigoWF);
                var indicesActivo = new List<string>();
                data.ForEach(x =>
                {
                    if (!(x.ENDS.HasValue))
                    {
                        indicesActivo.Add(Convert.ToString(x.TAB_ID));
                    }
                });

                if (data != null && data.Count > 0)
                {
                    retorno.data = Json(indicesActivo, JsonRequestBehavior.AllowGet);
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.result = 1;
                }
                else
                {

                    retorno.data = Json(indicesActivo, JsonRequestBehavior.AllowGet);
                    retorno.message = "No se ha configurado Pestañas para el estado de la licencia. ";
                    retorno.result = 0;
                }



            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTabs", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerLocalidadXCodigo(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLLicenciaLocalidad servicio = new BLLicenciaLocalidad();
                    var localidad = servicio.ObtenerLicLocalidadXCod(GlobalVars.Global.OWNER, id);
                    if (localidad != null)
                    {
                        DTOLicenciaLocalidad localidadDTO = new DTOLicenciaLocalidad()
                        {
                            //   idLicenciaLocalidad = localidad.LIC_SEC_ID,
                            CodigoTipoLocalidad = localidad.SEC_ID,
                            //   CodigoTipoAforo = localidad.CAP_ID,
                            Color = localidad.SEC_COLOR,
                            //   Funcion = localidad.SEC_PERFOMANCE,
                            ImporteBruto = localidad.SEC_GROSS,
                            Impuesto = localidad.SEC_TAXES,
                            ImporteNeto = localidad.SEC_NET,
                            Ticket = localidad.SEC_TICKETS,
                            PrecVenta = localidad.SEC_VALUE
                        };
                        retorno.result = 1;
                        retorno.data = Json(localidadDTO, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtener localidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerLicenciaXCodigo(decimal idLicencia)
        {
            Resultado retorno = new Resultado();
            try
            {

                if (!isLogout(ref retorno))
                {
                    bool tienePermiso = true;

                    BLLicencias servicio = new BLLicencias();
                    BLModalidad servicioMod = new BLModalidad();
                    var licencia = servicio.ObtenerLicenciaXCodigo(idLicencia, GlobalVars.Global.OWNER);

                    if (licencia != null)
                    {
                        tienePermiso = new SeguridadController().PuedeEditarLic(Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]), idLicencia, Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]));
                        if (!tienePermiso)
                        {
                            retorno.message = Constantes.MensajeGenerico.MSG_SIN_PERMISO_EDIT_LIC;
                            retorno.result = Constantes.MensajeRetorno.NO_ACCESS;
                        }
                        else
                        {
                            //if (licencia.ENDS == null)
                            //{
                            DTOLicencia licenciaDTO = new DTOLicencia()
                            {
                                codLicencia = licencia.LIC_ID,
                                tipoLicencia = licencia.LIC_TYPE,
                                codMultiple = licencia.LIC_MASTER,
                                codEstado = licencia.LICS_ID,
                                codMoneda = licencia.CUR_ALPHA,
                                codTemporalidad = licencia.RAT_FID,
                                descLicencia = licencia.LIC_DESC,
                                codModalidad = licencia.MOD_ID,
                                codTarifa = licencia.RATE_ID,
                                codEstablecimiento = licencia.EST_ID,
                                codUsuDerecho = licencia.BPS_ID,
                                IndUpdCaracteristicas = licencia.LIC_CREQ,
                                IndUpdPlanilla = licencia.LIC_PREQ,
                                IndDscVisible = licencia.LIC_DISC,
                                IndReqReporte = licencia.LIC_DREQ,
                                IndFactDeta = licencia.LIC_INVD,
                                GrupoFacturacion = licencia.INVG_ID,
                                FormaEntregaFact = licencia.INVF_ID,
                                nombreLicencia = licencia.LIC_NAME,
                                TipoEnvioFact = licencia.LIC_SEND,
                                codTipoPago = licencia.PAY_ID,
                                IndEmiMensual = licencia.LIC_EMI_MENSUAL

                            };

                            var modda = servicioMod.Obtener(GlobalVars.Global.OWNER, licencia.MOD_ID);
                            if (modda != null)
                            {
                                licenciaDTO.codWorkFlow = modda.WRFK_ID;
                                licenciaDTO.codModUso = modda.MOD_USAGE;
                            }

                            BLLicenciaPlaneamiento servicioPlan = new BLLicenciaPlaneamiento();
                            var planning = servicioPlan.ListarFechaPlanificacion(GlobalVars.Global.OWNER, licencia.LIC_ID);
                            licenciaDTO.hasPlanning = false;
                            if (planning != null && planning.Count > 0)
                            {
                                licenciaDTO.hasPlanning = true;
                            }
                            retorno.data = Json(licenciaDTO, JsonRequestBehavior.AllowGet);

                            if (licencia.ENDS == null)
                            {
                                retorno.result = Constantes.MensajeRetorno.OK;

                            }
                            else
                            {
                                retorno.result = Constantes.MensajeRetorno.NO_DATA;
                                retorno.message = string.Format("La Licencia con el c&oacute;digo: {0} ha sido Inactivada. fecha : " + licencia.ENDS.ToString(), idLicencia);
                            }
                        }
                    }
                    else
                    {
                        retorno.result = Constantes.MensajeRetorno.NO_DATA;
                        retorno.message = string.Format("No existe Licencia con el c&oacute;digo: {0} .", idLicencia);
                    }
                }
                // }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = Constantes.MensajeRetorno.ERROR; ;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtener licencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarNuevaPlanificacion(decimal idTemp, decimal anio, DateTime fecha, decimal mes, string tipoPagoPadre)
        {
            Resultado retorno = new Resultado();
            //Creat una nueva planificacion vacia
            var plan = new BLLicenciaPlaneamiento().ListarNuevaPlanificacion(GlobalVars.Global.OWNER, anio, idTemp, mes);


            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget' id='tbPlaniamiento'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Periodo</th>");
                    shtml.Append("<th class='k-header' >Mes</th><th  class='k-header'>Fecha</th>");
                    shtml.Append("<th  class='k-header'>Bloqueo</th>");
                    shtml.Append("<th class='k-header'>Tipo de Pago</th>");
                    shtml.Append("<th class='k-header'>Monto Facturar.</th>");
                    shtml.Append("<th class='k-header'>Monto Facturado.</th>");
                    shtml.Append("<th class='k-header'>Ver Fact.</th>");
                    shtml.Append("<th class='k-header'>Situación</th></tr></thead><tbody>");
                    if (plan != null)
                    {
                        GeneralController gen = new GeneralController();
                        var items = gen.ListarBloqueoItems();
                        var itemsTp = gen.ListarTipoPagoItems();


                        var x = 1;
                        plan.ForEach(c =>
                        {

                            string option = "<option value='0'>--SELECCIONE--</option>";
                            string optionTp = "<option value=' '>--SELECCIONE--</option>";


                            option += itemsT(items, Convert.ToString(c.BLOCK_ID), "-1");
                            optionTp += itemsT(itemsTp, c.PAY_ID, tipoPagoPadre);

                            var planificacionDTO = new DTOLicenciaPlaneamiento();
                            planificacionDTO.codigoLP = c.LIC_PL_ID;
                            planificacionDTO.anio = c.LIC_YEAR;
                            planificacionDTO.descMes = c.LIC_MONTH_DESC;
                            planificacionDTO.fecha = x == 1 ? fecha : c.LIC_DATE;
                            planificacionDTO.codBloqueo = c.BLOCK_ID;
                            planificacionDTO.codTipoPago = tipoPagoPadre;
                            planificacionDTO.situacion = "ABIERTO";

                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td ><input type='hidden' value='{0}' id='hidCodigoLP_{1}' />{0}</td>", planificacionDTO.codigoLP, x);
                            shtml.AppendFormat("<td >{0}</td>", planificacionDTO.anio);
                            shtml.AppendFormat("<td >{0}</td>", planificacionDTO.descMes);
                            shtml.AppendFormat("<td align='center' ><input type='text' id='txtFechaBloqueo_{0}' value='{1}' onchange='return UpdPlanificacion({0});' /></td>", x, planificacionDTO.fecha);
                            shtml.AppendFormat("<td align='center' ><input type='checkbox' id='ckbBloqueo_{0}' onclick='bloquearLista(this,{1});'  style='display:none;' />", x, x);
                            shtml.AppendFormat("<input type='hidden' id='hidBloqueo_{0}' value='{1}'/> <select id='lstBloqueo_{2}'  onchange='return UpdPlanificacion({0});'>{3}</select></td>", x, planificacionDTO.codBloqueo, x, option);
                            shtml.AppendFormat("<td><input type='hidden' id='hidTipoPago_{0}' value='{1}'/><select id='ddlTipoPago_{0}' onchange='return UpdPlanificacion({0});'>{2}</select></td>", x, planificacionDTO.codTipoPago, optionTp);
                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.AppendFormat("<td align='center'>{0}</td>", planificacionDTO.situacion);
                            shtml.Append("</tr>");
                            x++;
                            planificacion.Add(planificacionDTO);
                        });
                        if (planificacion != null)
                            PlanificacionTmp = planificacion;

                        shtml.Append("</tbody></table>");
                        retorno.message = shtml.ToString();
                        retorno.result = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarNuevaPlanificacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarPlaneamiento(decimal idLic, decimal idTemp, string anio, string tipoPagoPadre, decimal idTarifa, string mes, decimal idTemp2)
        {
            Resultado retorno = new Resultado();
            //Estavariables se necesitan para poder seguir agregando Planeamientos
            //mes y idTemp2
            //Aqui ya tengo el plan de la nueva planificacion
            //A veces El mes ingresa nulo cuando viene de busqueda de combo
            List<BELicenciaPlaneamiento> plan = null;
            if (mes != "" && mes != "0")
            {
                plan = new BLLicenciaPlaneamiento().ListarNuevaPlanificacion(GlobalVars.Global.OWNER, Convert.ToDecimal(anio), idTemp2, Convert.ToDecimal(mes));
            }
            try
            {
                // var
                if (anio == "" || anio == "null") anio = "0";

                List<BELicenciaPlaneamiento> planeamientos = new BLLicenciaPlaneamiento().ListarXLicAnio(GlobalVars.Global.OWNER, idTemp, idLic, Convert.ToInt32(anio)); ;
                /*Aqui recorrer la primera lista 
                 
                 */

                if (plan != null)
                {

                    #region obtenerlistaAgregar
                    foreach (var x in planeamientos.OrderBy(x => x.LIC_ORDER))
                    {
                        foreach (var y in plan.OrderBy(y => y.LIC_PL_ID))
                        {
                            plan = plan.Where(z => z.LIC_PL_ID != x.LIC_ORDER).ToList();
                        }
                    }
                    #endregion
                    //Agregando la lista una vez obtenido los valores
                    //   foreach (var x in planeamientos.OrderBy(x => x.LIC_ORDER))
                    //   {
                    foreach (var y in plan.OrderBy(y => y.LIC_PL_ID))
                    {
                        planeamientos.Add(new BELicenciaPlaneamiento
                        {
                            LIC_ORDER = y.LIC_PL_ID,
                            LIC_ID = 0,
                            LIC_YEAR = y.LIC_YEAR,
                            LIC_MONTH = y.LIC_PL_ID,
                            FRQ_DESC = y.LIC_MONTH_DESC,
                            LIC_DATE = y.LIC_DATE,
                            BLOCK_ID = 0,
                            PAY_ID = "CO",
                            NroFactura = "0",
                            NroSerie = "0",
                            ImporteFactura = "0",
                            LIC_PL_STATUS = "A"
                        });
                        //   }
                    }
                }
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget' id='tbPlaniamiento' cellspacing=0 cellpadding=0>");
                    shtml.Append("<thead><tr style=' height:20px;'><th  class='k-header'>Periodo</th>");
                    shtml.Append("<th class='k-header' >Mes</th><th  class='k-header'>Fecha</th>");
                    shtml.Append("<th  class='k-header'>Bloqueo</th>");
                    shtml.Append("<th class='k-header'>Tipo de Pago</th>");
                    shtml.Append("<th class='k-header'>Monto Facturar.</th>");
                    shtml.Append("<th class='k-header'>Monto Facturado.</th>");
                    shtml.Append("<th class='k-header'>Ver Fact.</th>");
                    shtml.Append("<th class='k-header'>Situación</th>");
                    shtml.Append("<th class='k-header'>Act Situación</th></tr></thead><tbody>");
                    //shtml.Append("<th class='k-header'>Situación Electronica</th></tr></thead><tbody>");
                    //shtml.Append("<th class='k-header'>  </th></tr></thead><tbody>");

                    if (planeamientos != null)
                    {
                        GeneralController gen = new GeneralController();
                        var items = gen.ListarBloqueoItems();
                        var itemsTp = gen.ListarTipoPagoItems();
                        var x = 1;
                        //ORDENANDO
                        planeamientos.OrderBy(z => z.LIC_ORDER);

                        foreach (var s in planeamientos.OrderBy(s => s.LIC_ORDER))
                        {
                            string situacionPlan = "";
                            string situacionPlanFact = "";

                            string disableChk = "";
                            var planLicencia = new BLLicenciaPlaneamiento().ObtenerPlanificacion(GlobalVars.Global.OWNER, s.LIC_PL_ID);
                            if (planLicencia != null)
                            {
                                if (planLicencia.LIC_PL_STATUS != null && planLicencia.LIC_PL_STATUS == "T")
                                {
                                    disableChk = " disabled=disabled ";
                                    situacionPlan = "CERRADO";
                                }

                                if (planLicencia.LIC_PL_STATUS != null && planLicencia.LIC_PL_STATUS == "A")
                                    situacionPlan = "ABIERTO";
                                if (planLicencia.LIC_PL_STATUS != null && planLicencia.LIC_PL_STATUS == "P")
                                    situacionPlan = "PARCIAL";


                                #region PL_STATUS_FACT

                                if (planLicencia.LIC_PL_STATUS_FACT != null && planLicencia.LIC_PL_STATUS_FACT == "T")
                                {
                                    disableChk = " disabled=disabled ";
                                    situacionPlanFact = "FACTURADO";
                                }

                                if (planLicencia.LIC_PL_STATUS_FACT != null && planLicencia.LIC_PL_STATUS_FACT == "A")
                                    situacionPlanFact = "ABIERTO";

                                if (planLicencia.LIC_PL_STATUS_FACT != null && planLicencia.LIC_PL_STATUS_FACT == "B")
                                    situacionPlanFact = "BORRADOR";

                                if (planLicencia.LIC_PL_STATUS_FACT != null && planLicencia.LIC_PL_STATUS_FACT == "F")
                                    situacionPlanFact = "FACTURANDO";

                                #endregion

                            }
                            string option = "<option value='0'>--SELECCIONE--</option>";
                            string optionTp = "<option value=' '>--SELECCIONE--</option>";


                            option += itemsT(items, Convert.ToString(s.BLOCK_ID), "-1");
                            optionTp += itemsT(itemsTp, s.PAY_ID, tipoPagoPadre);


                            var planeamientoDTO = new DTOLicenciaPlaneamiento();
                            planeamientoDTO.codigoLP = s.LIC_PL_ID;
                            planeamientoDTO.codigoLic = s.LIC_ID;
                            planeamientoDTO.anio = s.LIC_YEAR;
                            planeamientoDTO.mes = s.LIC_MONTH;
                            planeamientoDTO.descMes = s.FRQ_DESC;
                            planeamientoDTO.fecha = s.LIC_DATE;
                            planeamientoDTO.codBloqueo = s.BLOCK_ID;
                            planeamientoDTO.codTipoPago = s.PAY_ID;
                            planeamientoDTO.situacion = situacionPlan == "" ? "ABIERTO" : situacionPlan;
                            planeamientoDTO.situacion_Electronica = situacionPlanFact == "" ? "ABIERTO" : situacionPlanFact;

                            planeamientoDTO.NroFactura = s.NroFactura;
                            planeamientoDTO.NroSerie = s.NroSerie;
                            planeamientoDTO.ImporteFactura = s.ImporteFactura;

                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td ><input type='hidden' value='{0}' id='hidCodigoLP_{1}' />{2}</td>", planeamientoDTO.codigoLP, x, planeamientoDTO.anio);
                            shtml.AppendFormat("<td >{0}</td>", planeamientoDTO.descMes);
                            shtml.AppendFormat("<td align='center' ><input type='text' id='txtFechaBloqueo_{0}' {2} value='{1}'  onchange='return UpdPlanificacion({0});' /></td>", x, planeamientoDTO.fecha, disableChk);
                            shtml.AppendFormat("<td align='center' ><input type='checkbox' id='ckbBloqueo_{0}' {2} onclick='bloquearLista(this,{1});' style='display:none;' />", x, x, disableChk);
                            shtml.AppendFormat("<input type='hidden' id='hidBloqueo_{0}' value='{1}'/> <select id='lstBloqueo_{2}' {4}  onchange='return UpdPlanificacion({0});'>{3}</select></td>", x, planeamientoDTO.codBloqueo, x, option, disableChk);
                            shtml.AppendFormat("<td><input type='hidden' id='hidTipoPago_{0}' value='{1}'/><select id='ddlTipoPago_{0}' onchange='return UpdPlanificacion({0});' {3} >{2}</select></td>", x, planeamientoDTO.codTipoPago, optionTp, disableChk);

                            #region POR CAMBIO FACTURACION MANUAL - 20151105
                            /*BEGIN ADDON DBS  */
                            Recaudacion.FacturacionController servCalculo = new Recaudacion.FacturacionController();

                            try
                            {
                                var montos = servCalculo.obtenerMontoFactura(planeamientoDTO.codigoLic, planeamientoDTO.codigoLP);
                                string stitle = "Monto Total a Facturar.";
                                string linkVerFact = "";
                                if (!montos.TieneFacturacion) stitle = string.Format("[Total Tarifa: {0}]  [Total Impuesto: {1}]  [Total Cargo: {2}]  [Total Descuento: {3}] [Total Descuento Redondeo: {4}]", montos.ValorTarifa, montos.ValorImpuesto, montos.ValorCargo, montos.ValorDescuento, montos.ValorDescuentoRedondeoEspecial);
                                else linkVerFact = string.Format("<a href='' title='Ver facturas generadas' onclick='return verFactura({0},{1});'>ver Facturas</a>", planeamientoDTO.codigoLic, planeamientoDTO.codigoLP);
                                /*END ADDON DBS  */
                                #endregion

                                //shtml.AppendFormat("<td title='{1}' style='cursor:pointer;' >{0}</td>", montos.ValorFinal >= 0 ? montos.ValorFinal.ToString() : string.Empty, stitle);
                                //shtml.AppendFormat("<td >{0}</td>", montos.ValorFinalFacturado);
                                shtml.AppendFormat("<td title='{1}' style='cursor:pointer;' >{0}</td>", montos.ValorFinal >= 0 ? string.Format("{0:#,###,###,##0.00}", montos.ValorFinal) : string.Empty, stitle);
                                shtml.AppendFormat("<td >{0}</td>", string.Format("{0:#,###,###,##0.00}", montos.ValorFinalFacturado));
                                shtml.AppendFormat("<td  align='right' >{0}</td>", linkVerFact);
                            }
                            catch (Exception ex)
                            {
                                shtml.AppendFormat("<td title='{1}' style='cursor:pointer;' >{0}</td>", 0, "");
                                shtml.AppendFormat("<td >{0}</td>", string.Format("{0:#,###,###,##0.00}", 0));
                            }

                            shtml.AppendFormat("<td align='center'>{0}</td>", planeamientoDTO.situacion);
                            shtml.AppendFormat("<td align='center'> <a href=# onclick='ValidaPeriodoLicenciaAct({0});'><img src='../Images/iconos/activate.png' border=0></a></td>", planeamientoDTO.codigoLP);
                            //shtml.AppendFormat("<td align='center'>{0}</td>", planeamientoDTO.situacion_Electronica);
                            //shtml.AppendFormat("<td><img src='../Images/iconos/report_deta.png' onclick='imprimirautorizacion({0});' alt='Agregar  Detalle' title='Imprimir Autorizacion' border=0>&nbsp;&nbsp;</td>", planeamientoDTO.codigoLP);
                            shtml.Append("</tr>");
                            x++;
                            planificacion.Add(planeamientoDTO);
                        }
                        if (planificacion != null)
                            PlanificacionTmp = planificacion;
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarPlaneamiento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarDocumentoLyrics(decimal codigoLic, string tipoOrigen)
        {
            Resultado retorno = new Resultado();
            if (tipoOrigen == "1")
            {
                tipoOrigen = "56";
            }
            if (tipoOrigen == "2")
            {
                tipoOrigen = "57";
            }
            var documentos = new BLDocumentoGral().ObtenerDocXLicencia(codigoLic, GlobalVars.Global.OWNER, Convert.ToInt32(tipoOrigen));


            //List<BEDocumentoGral> documentosB = documentos.Where(u => (u.DOC_ORG == tipoOrigen || tipoOrigen == "")).OrderByDescending(y => y.LOG_DATE_CREAT).ToList();
            //documentos = documentosB;
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Tipo Documento</th>");
                    shtml.Append("<th class='k-header' >Fecha Recepción</th><th  class='k-header'>Archivo</th>");
                    //shtml.Append("<th class='k-header'>Word</th>");
                    shtml.Append("<th class='k-header'>Origen</th>");
                    shtml.Append("<th class='k-header'>Archivo</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th><th  class='k-header'>Estado</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");


                    if (documentos != null)
                    {
                        //var documentosDTO = new List<DTODocumento>();
                        if (documentos != null)
                        {
                            documentos.ForEach(s =>
                            {
                                var newDTODocumento = new DTODocumento();
                                newDTODocumento.Id = s.DOC_ID;
                                newDTODocumento.Archivo = s.DOC_PATH;
                                newDTODocumento.TipoDocumento = Convert.ToString(s.DOC_TYPE);
                                newDTODocumento.TipoDocumentoDesc = new BLREC_DOCUMENT_TYPE().Obtener(GlobalVars.Global.OWNER, s.DOC_TYPE).DOC_DESC;
                                newDTODocumento.FechaRecepcion = Convert.ToString(s.DOC_DATE);
                                newDTODocumento.EnBD = true;
                                newDTODocumento.UsuarioCrea = s.LOG_USER_CREAT;
                                newDTODocumento.FechaCrea = s.LOG_DATE_CREAT;
                                newDTODocumento.UsuarioModifica = s.LOG_USER_UPDATE;
                                newDTODocumento.FechaModifica = s.LOG_DATE_UPDATE;
                                newDTODocumento.Activo = s.ENDS.HasValue ? false : true;
                                //documentosDTO.Add(newDTODocumento);

                                string Origen = "-";
                                string pathWeb = "";
                                var editable = true;
                                var eliminable = true;
                                var showFormatoWord = false;
                                string archivoNombre = "";
                                if (s.DOC_ORG == "I")
                                {
                                    pathWeb = GlobalVars.Global.RutaDocLicEntradaWeb;
                                    Origen = "Entrada a Proceso";
                                    eliminable = false;
                                    archivoNombre = newDTODocumento.Archivo;
                                }
                                else if (s.DOC_ORG == "O")
                                {
                                    pathWeb = GlobalVars.Global.RutaDocLicSalidaWeb;
                                    Origen = "Salida de Proceso";
                                    editable = false;
                                    eliminable = false;
                                    showFormatoWord = true;
                                    archivoNombre = newDTODocumento.Archivo;
                                }
                                else
                                {
                                    //Origen = "LYRICS";
                                    pathWeb = GlobalVars.Global.RutaTabDocumentoLicWeb;
                                    archivoNombre = newDTODocumento.Archivo;
                                    Origen = "Otros";
                                }

                                var ruta = Path.Combine(pathWeb, newDTODocumento.Archivo);
                                var pathWord = string.Format("{0}{1}", pathWeb, newDTODocumento.Archivo);
                                string rutaWord = pathWord.Replace(".pdf", ".docx");

                                string enlaceFile = "";

                                string enlaceWord = "";

                                if (s.DOC_ORG == "O")
                                {
                                    enlaceFile = string.Format("<a href='#' onclick=verImagen('{0}');  title='ver formato pdf.'><img src='../Images/iconos/pdf.png' border=0></a>", ruta);
                                    enlaceWord = string.Format(" &nbsp;<a href='#' onclick=verWord('{0}'); title='ver formato word.'><img src='../Images/iconos/word.png' border=0></a>", rutaWord);

                                }
                                else
                                {
                                    enlaceFile = string.Format("<a href='#' onclick=verImagen('{0}'); title='ver archivo.'><img src='../Images/iconos/file.png' border=0></a>", ruta);
                                }


                                //if (showFormatoWord)
                                //{
                                //    var pathWord = string.Format("{0}{1}", pathWeb, newDTODocumento.Archivo);
                                //    rutaWord = pathWord.Replace(".pdf", ".docx");
                                //    enlaceWord = string.Format(" &nbsp;<a href='#' onclick=verWord('{0}'); title='ver formato word.'><img src='../Images/iconos/word.png' border=0></a>", rutaWord);
                                //    enlaceFile = string.Format("<a href='#' onclick=verImagen('{0}');  title='ver formato pdf.'><img src='../Images/iconos/pdf.png' border=0></a>", ruta);

                                //}

                                shtml.Append("<tr class='k-grid-content'>");
                                shtml.AppendFormat("<td >{0}</td>", newDTODocumento.Id);
                                shtml.AppendFormat("<td >{0}</td>", newDTODocumento.TipoDocumentoDesc);
                                shtml.AppendFormat("<td >{0}</td>", newDTODocumento.FechaRecepcion.Substring(0, 10));
                                shtml.AppendFormat("<td >{0} &nbsp; {1}</td>", enlaceFile, enlaceWord);
                                shtml.AppendFormat("<td >{0}</td>", Origen);
                                shtml.AppendFormat("<td >{0}</td>", archivoNombre);
                                shtml.AppendFormat("<td >{0}</td>", newDTODocumento.UsuarioCrea);
                                shtml.AppendFormat("<td >{0}</td>", newDTODocumento.FechaCrea);
                                shtml.AppendFormat("<td >{0}</td>", newDTODocumento.UsuarioModifica);
                                shtml.AppendFormat("<td >{0}</td>", newDTODocumento.FechaModifica);
                                shtml.AppendFormat("<td >{0}</td>", newDTODocumento.Activo ? "Activo" : "Inactivo");

                                if (editable && eliminable)
                                {
                                    shtml.AppendFormat("<td> <a href=# onclick='updAddDocumento({0});' title='Actualizar documento' alt='Actualizar documento'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", newDTODocumento.Id);
                                    shtml.AppendFormat("<a href=# onclick='delAddDocumento({0},{3});' title='Eliminar documento' alt='Eliminar documento'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", newDTODocumento.Id, newDTODocumento.Activo ? "delete.png" : "activate.png", newDTODocumento.Activo ? "Eliminar Documento" : "Activar Documento", newDTODocumento.Activo ? 1 : 0);
                                }
                                else if (editable && !eliminable)
                                {
                                    shtml.AppendFormat("<td> <a href=# onclick='updAddDocumento({0});' title='Actualizar documento' alt='Actualizar documento'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", newDTODocumento.Id);
                                    //shtml.AppendFormat("<a href=# onclick='delAddDocumento({0},{3});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", newDTODocumento.Id, newDTODocumento.Activo ? "delete.png" : "activate.png", newDTODocumento.Activo ? "Eliminar Documento" : "Activar Documento", newDTODocumento.Activo ? 1 : 0);
                                }
                                else if (!editable && !eliminable)
                                {

                                    shtml.AppendFormat("<td> ");
                                }
                                shtml.Append("</td>");
                                shtml.Append("</tr>");
                            });
                        }
                    }

                    shtml.Append(" </table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarDocumento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddObservacion(decimal codLic, decimal Id, decimal TipoObservacion, string Observacion)
        {
            Resultado retorno = new Resultado();
            var obsGral = new BEObservationGral();

            try
            {
                if (!isLogout(ref retorno))
                {
                    obsGral.OBS_ID = Id;
                    obsGral.OWNER = GlobalVars.Global.OWNER;
                    obsGral.OBS_TYPE = Convert.ToInt32(TipoObservacion);
                    obsGral.OBS_VALUE = Observacion;
                    obsGral.ENT_ID = Convert.ToInt32(Constantes.ENTIDAD.LICENCIAMIENTO);
                    obsGral.LOG_USER_CREAT = UsuarioActual;
                    obsGral.OBS_USER = UsuarioActual;

                    var codigoGenObs = new BLObservationGral().Insertar(obsGral);
                    var result = new BLObservationLic().Insertar(new BEObservationLic
                    {
                        LIC_ID = codLic,
                        OBS_ID = codigoGenObs,
                        OWNER = obsGral.OWNER,
                        LOG_USER_CREAT = obsGral.LOG_USER_CREAT
                    });

                    retorno.result = 1;
                    retorno.Code = Convert.ToInt32(obsGral.OBS_ID);
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "AddObservacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddDocumento(decimal codLic, decimal Id, string TipoDocumento, string Archivo, string FechaRecepcion)
        {
            Resultado retorno = new Resultado();

            //DirectoryInfo di = new DirectoryInfo(@"C:\Users\jchayguaque\Desktop\Archivos");
            ////Console.WriteLine("No search pattern returns:");
            //int count = 0;
            //foreach (var fi in di.GetFiles())
            //{
            //    var docGral = new BEDocumentoGral();
            //    string[] separadas;
            //    separadas = fi.Name.Split('-');
            //    var Cod_Licencia = separadas[0];

            //    var Mes = separadas[1].Substring(0, 2);
            //    var Anio = separadas[1].Substring(2, 2);
            //    //Console.WriteLine(fi.Name);
            //    docGral.DOC_ID = Id;
            //    docGral.OWNER = GlobalVars.Global.OWNER;
            //    docGral.DOC_TYPE = Convert.ToInt32(12);
            //    docGral.DOC_PATH = fi.Name;
            //    docGral.DOC_DATE = Convert.ToDateTime("01/" + Mes + "/20" + Anio);
            //    docGral.ENT_ID = Convert.ToInt32(Constantes.ENTIDAD.LICENCIAMIENTO);
            //    docGral.DOC_USER = UsuarioActual;
            //    docGral.LOG_USER_CREAT = UsuarioActual;
            //    docGral.DOC_VERSION = 1;

            //    var codigoGenDoc = new BLDocumentoGral().Insertar(docGral, new BEDocumentoLic
            //    {
            //        LIC_ID = Convert.ToDecimal(Cod_Licencia),
            //        OWNER = GlobalVars.Global.OWNER,
            //        LOG_USER_CREAT = UsuarioActual,
            //        DOC_ORG = Constantes.OrigenDocumento.EXTERNO
            //    });
            //    count++;
            //}
            //var a = count;
            //return null;

            try
            {
                if (!isLogout(ref retorno))
                {
                    var docGral = new BEDocumentoGral();
                    docGral.DOC_ID = Id;
                    docGral.OWNER = GlobalVars.Global.OWNER;
                    docGral.DOC_TYPE = Convert.ToInt32(TipoDocumento);
                    docGral.DOC_PATH = "";
                    docGral.DOC_DATE = Convert.ToDateTime(FechaRecepcion);
                    docGral.ENT_ID = Convert.ToInt32(Constantes.ENTIDAD.LICENCIAMIENTO);
                    docGral.DOC_USER = UsuarioActual;
                    docGral.LOG_USER_CREAT = UsuarioActual;
                    docGral.DOC_VERSION = 1;

                    var codigoGenDoc = new BLDocumentoGral().Insertar(docGral, new BEDocumentoLic
                    {
                        LIC_ID = codLic,
                        OWNER = docGral.OWNER,
                        LOG_USER_CREAT = docGral.LOG_USER_CREAT,
                        DOC_ORG = Constantes.OrigenDocumento.EXTERNO
                    });
                    //var result = new BLDocumentoLic().Insertar(new BEDocumentoLic
                    //{
                    //    LIC_ID = codLic,
                    //    DOC_ID = codigoGenDoc,
                    //    OWNER = docGral.OWNER,
                    //    LOG_USER_CREAT = docGral.LOG_USER_CREAT, 
                    //    DOC_ORG=Constantes.OrigenDocumento.EXTERNO
                    //});

                    retorno.result = 1;
                    retorno.Code = Convert.ToInt32(codigoGenDoc);
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "AddDocumento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarComision(decimal codigoLic)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    List<DTOLicenciaComision> comisiones = new List<DTOLicenciaComision>();
                    StringBuilder shtml = new StringBuilder();

                    for (var i = 1; i <= 5; i++)
                    {
                        DTOLicenciaComision obj = new DTOLicenciaComision();
                        var nombre = "";
                        var descripcion = "";
                        if (i == 1)
                        {
                            nombre = "Producto";
                            descripcion = "Nombre del Producto";
                        }
                        else if (i == 2)
                        {
                            nombre = "Division";
                            descripcion = "Nombre de la Division";
                        }
                        else if (i == 3)
                        {
                            nombre = "Oficina";
                            descripcion = "Nombre de la Oficina";
                        }
                        else if (i == 4)
                        {
                            nombre = "Agente";
                            descripcion = "Nombre del Agente";
                        }
                        else
                        {
                            nombre = "Prueba";
                            descripcion = "Nombre de la Prueba";
                        }
                        obj.Nombre = nombre;
                        obj.TipoComision = "Tipo de Comision";
                        obj.Descripcion = descripcion;
                        obj.Porcentaje = Convert.ToString(i * 10) + "%";
                        obj.Valor = i * 8;
                        obj.Fecha = "29/12/2014";
                        obj.Activo = true;

                        comisiones.Add(obj);
                    };

                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th  class='k-header'>Nombre</th>");
                    shtml.Append("<th class='k-header'>Tipo de Comision</th>");
                    shtml.Append("<th class='k-header'>Descripción</th>");
                    shtml.Append("<th class='k-header'>Porcentaje</th>");
                    shtml.Append("<th class='k-header'>Valor</th>");
                    shtml.Append("<th class='k-header'>Fecha</th>");
                    shtml.Append("<th class='k-header'>Situacion</th></thead>");
                    //shtml.Append("<th  class='k-header'></th></tr></thead>");

                    comisiones.ForEach(x =>
                    {
                        shtml.Append("<tr class='k-grid-content'>");
                        shtml.AppendFormat("<td >{0}</td>", x.Nombre);
                        shtml.AppendFormat("<td >{0}</td>", x.TipoComision);
                        shtml.AppendFormat("<td >{0}</td>", x.Descripcion);
                        shtml.AppendFormat("<td >{0}</td>", x.Porcentaje);
                        shtml.AppendFormat("<td >{0}</td>", x.Valor);
                        shtml.AppendFormat("<td >{0}</td>", x.Fecha);
                        shtml.AppendFormat("<td >{0}</td>", !(x.Activo) ? "Inactivo" : "Activo");
                        //shtml.AppendFormat("<td> <a href=# onclick='updAddLocalidad({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", x.idComision);
                        //shtml.AppendFormat("<td><a href=# onclick='delAddComision({0},{3});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", x.idComision, x.Activo ? "delete.png" : "activate.png", x.Activo ? "Eliminar Localidad" : "Activar Localidad", x.Activo == true ? 1 : 0);
                        //shtml.Append("</td>");
                        shtml.Append("</tr>");
                    });

                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarComision", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarLocalidad(decimal codigoLic)
        {
            Resultado retorno = new Resultado();
            var localidades = new BLLicenciaLocalidad().ListarLicenciaLocalidad(GlobalVars.Global.OWNER, codigoLic);

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Aforo</th>");
                    shtml.Append("<th class='k-header'>Localidad</th>");
                    shtml.Append("<th class='k-header'>Ticket Vendidos</th>");
                    shtml.Append("<th class='k-header'>Prec Venta</th>");
                    shtml.Append("<th class='k-header'>Importe Bruto</th>");
                    shtml.Append("<th class='k-header'>Importe Neto</th>");
                    shtml.Append("<th class='k-header'>Situacion</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (localidades != null)
                    {
                        localidades.ForEach(c =>
                        {
                            DTOLicenciaLocalidad localidadDTO = new DTOLicenciaLocalidad();
                            //  localidadDTO.idLicenciaLocalidad = c.LIC_SEC_ID;
                            //  localidadDTO.CodigoTipoAforo = c.CAP_ID;
                            // localidadDTO.TipoAforo = new BLAforo().ObtenerAforoXCod(GlobalVars.Global.OWNER, c.CAP_ID).CAP_DESC;
                            localidadDTO.CodigoTipoLocalidad = c.SEC_ID;
                            localidadDTO.TipoLocalidad = new BLLocalidad().ObtenerLocalidadXCod(GlobalVars.Global.OWNER, c.SEC_ID).SEC_DESC;
                            localidadDTO.Ticket = c.SEC_TICKETS;
                            localidadDTO.PrecVenta = c.SEC_VALUE;
                            localidadDTO.ImporteBruto = c.SEC_GROSS;
                            localidadDTO.ImporteNeto = c.SEC_NET;
                            localidadDTO.Activo = c.ENDS.HasValue ? false : true;

                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", localidadDTO.idLicenciaLocalidad);
                            shtml.AppendFormat("<td >{0}</td>", localidadDTO.TipoAforo);
                            shtml.AppendFormat("<td >{0}</td>", localidadDTO.TipoLocalidad);
                            shtml.AppendFormat("<td >{0}</td>", localidadDTO.Ticket);
                            shtml.AppendFormat("<td >{0}</td>", localidadDTO.PrecVenta);
                            shtml.AppendFormat("<td >{0}</td>", localidadDTO.ImporteBruto);
                            shtml.AppendFormat("<td >{0}</td>", localidadDTO.ImporteNeto);
                            shtml.AppendFormat("<td >{0}</td>", !(localidadDTO.Activo) ? "Inactivo" : "Activo");
                            shtml.AppendFormat("<td> <a href=# onclick='updAddLocalidad({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", localidadDTO.idLicenciaLocalidad);
                            shtml.AppendFormat("<a href=# onclick='delAddLocalidad({0},{3});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", localidadDTO.idLicenciaLocalidad, localidadDTO.Activo ? "delete.png" : "activate.png", localidadDTO.Activo ? "Eliminar Localidad" : "Activar Localidad", localidadDTO.Activo == true ? 1 : 0);
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        });
                    }

                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarLocalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListarObservacion(decimal codigoLic)
        {
            Resultado retorno = new Resultado();
            var observaciones = new BLObservationGral().ObternerObsXLic(codigoLic, GlobalVars.Global.OWNER, Constantes.ENTIDAD.LICENCIAMIENTO);
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Tipo Observación</th>");
                    shtml.Append("<th class='k-header'>Observación</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (observaciones != null)
                    {
                        observaciones.ForEach(s =>
                        {
                            var observacionDTO = new DTOObservacion();

                            observacionDTO.Id = s.OBS_ID;
                            observacionDTO.Observacion = s.OBS_VALUE;
                            observacionDTO.TipoObservacion = Convert.ToString(s.OBS_TYPE);
                            observacionDTO.TipoObservacionDesc = new BLTipoObservacion().Obtener(GlobalVars.Global.OWNER, s.OBS_TYPE).OBS_DESC;
                            observacionDTO.EnBD = true;
                            observacionDTO.UsuarioCrea = s.LOG_USER_CREAT;
                            observacionDTO.FechaCrea = s.LOG_DATE_CREAT;
                            observacionDTO.UsuarioModifica = s.LOG_USER_UPDATE;
                            observacionDTO.FechaModifica = s.LOG_DATE_UPDATE;
                            observacionDTO.Activo = s.ENDS.HasValue ? false : true;

                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", observacionDTO.Id);
                            shtml.AppendFormat("<td >{0}</td>", observacionDTO.TipoObservacionDesc);
                            shtml.AppendFormat("<td >{0}</td>", observacionDTO.Observacion);
                            shtml.AppendFormat("<td >{0}</td>", observacionDTO.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", observacionDTO.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", observacionDTO.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", observacionDTO.FechaModifica);
                            shtml.AppendFormat("<td >{0}</td>", observacionDTO.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td> <a href=# onclick='updAddObservacion({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", observacionDTO.Id);
                            shtml.AppendFormat("<a href=# onclick='delAddObservacion({0},{3});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", observacionDTO.Id, observacionDTO.Activo ? "delete.png" : "activate.png", observacionDTO.Activo ? "Eliminar Observacion" : "Activar Observacion", observacionDTO.Activo ? 1 : 0);
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        });
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarObservacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarParametro(decimal codigoLic)
        {

            Resultado retorno = new Resultado();
            var parametros = new BLParametroGral().ObtenerParXLic(codigoLic, GlobalVars.Global.OWNER, Constantes.ENTIDAD.LICENCIAMIENTO);

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Tipo Observación</th>");
                    shtml.Append("<th class='k-header'>Observación</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th><th class='k-header'>Estado</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (parametros != null)
                    {
                        parametros.ForEach(s =>
                        {
                            var parametroDTO = new DTOParametro();

                            parametroDTO.Id = (s.PAR_ID);
                            parametroDTO.Descripcion = s.PAR_VALUE;
                            parametroDTO.TipoParametro = Convert.ToString(s.PAR_TYPE);
                            parametroDTO.TipoParametroDesc = new BLTipoParametro().Obtener(GlobalVars.Global.OWNER, s.PAR_TYPE).PAR_DESC;
                            parametroDTO.EnBD = true;
                            parametroDTO.UsuarioCrea = s.LOG_USER_CREAT;
                            parametroDTO.FechaCrea = s.LOG_DATE_CREAT;
                            parametroDTO.UsuarioModifica = s.LOG_USER_UPDATE;
                            parametroDTO.FechaModifica = s.LOG_DATE_UPDATE;
                            parametroDTO.Activo = s.ENDS.HasValue ? false : true;

                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", parametroDTO.Id);
                            shtml.AppendFormat("<td >{0}</td>", parametroDTO.TipoParametroDesc);
                            shtml.AppendFormat("<td >{0}</td>", parametroDTO.Descripcion);

                            shtml.AppendFormat("<td >{0}</td>", parametroDTO.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", parametroDTO.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", parametroDTO.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", parametroDTO.FechaModifica);
                            shtml.AppendFormat("<td >{0}</td>", parametroDTO.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td> <a href=# onclick='updAddParametro({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", parametroDTO.Id);
                            shtml.AppendFormat("<a href=# onclick='delAddParametro({0},{3});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", parametroDTO.Id, parametroDTO.Activo ? "delete.png" : "activate.png", parametroDTO.Activo ? "Eliminar Parametro" : "Activar Parametro", parametroDTO.Activo ? 1 : 0);
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        });

                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarParametro", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista las caracteristicas asociadas al establecimiento y Modalidad al que pertenece la Licencia
        /// </summary>
        /// <param name="idEst"></param>
        /// <param name="idMod"></param>
        /// <returns></returns>
        public JsonResult ListarCaracteristica(decimal codigoLic, string fecha, decimal codigoLicPlan)
        {
            Resultado retorno = new Resultado();


            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' id='tblCaracteristica' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr> <th  class='k-header'>Descripción</th>");
                    shtml.Append("<th class='k-header'>Periodo</th>");
                    shtml.Append("<th class='k-header'>Tipo</th>");
                    shtml.Append("<th class='k-header'>Fecha de Registro</th>");
                    shtml.Append("<th class='k-header'>Valor</th> ");
                    shtml.Append("<th class='k-header'>No es Valor real</th> ");
                    shtml.Append("<th class='k-header'>Comentario</th></tr></thead>");
                    var caracteristicas = new BLCaracteristica().ListarCaractLicencia(GlobalVars.Global.OWNER, codigoLic, fecha, codigoLicPlan);

                    var caracteristicas_dscPlantillaxTarifa = new BLCaracteristica().ListarCaractDescPlantillaxTarifa(codigoLic);
                    caracteristicas_dscPlantillaxTarifa = caracteristicas_dscPlantillaxTarifa.Where(z => z.CHAR_ID != 80).ToList();
                    caracteristicas_dscPlantillaxTarifa = caracteristicas_dscPlantillaxTarifa.Where(z => z.CHAR_ID != 79).ToList();
                    foreach (var c in caracteristicas.OrderBy(y => y.CHAR_ID))
                    {
                        foreach (var y in caracteristicas_dscPlantillaxTarifa.OrderBy(y => y.CHAR_ID))
                        {
                            if (c.CHAR_ID == y.CHAR_ID)//solo debe listar las caracteristicas que necesita mostrar 

                                caracteristicas = caracteristicas.Where(z => z.CHAR_ID != y.CHAR_ID).ToList();
                            // visible=" style='display:none'";
                        }
                    }
                    if (caracteristicas != null && codigoLicPlan > 0)
                    {
                        Int16 cuenta = 0;

                        // var lista = caracteristicas.OrderBy(Y => Y.CHAR_LONG).ToList();
                        var lista = caracteristicas.Where(Y => Y.CHAR_LONG != null).ToList();//Solo que liste los periodos que pertenecen a la tarifa
                        foreach (var c in caracteristicas.OrderBy(c => c.CHAR_ID))
                        {
                            String visible = "";
                            #region visible

                            // foreach (var x in caracteristicas.OrderBy(x => x.CHAR_ID))
                            //{
                            //foreach (var y in caracteristicas_dscPlantillaxTarifa.OrderBy(y => y.CHAR_ID))
                            //{
                            //    if (c.CHAR_ID == y.CHAR_ID)//solo debe listar las caracteristicas que necesita mostrar 

                            //        caracteristicas = caracteristicas.Where(z => z.CHAR_ID != y.CHAR_ID).ToList();
                            //       // visible=" style='display:none'";
                            //}

                            //}
                            #endregion


                            DTOLicenciaCaracteristica licCar = new DTOLicenciaCaracteristica();
                            licCar.CodigoCaracteristica = c.CHAR_ID;
                            licCar.CodigoLic = codigoLic;
                            licCar.DescCarateristica = c.CHAR_LONG;
                            licCar.FechaCaracteristicaLic = c.START;
                            licCar.Tipo = c.LIC_VAL_ORIGEN;
                            licCar.Valor = c.LIC_CHAR_VAL;
                            licCar.EsCaractAlterada = c.FLG_MANUAL;
                            licCar.CaractAlteradaDesc = c.COMMENT_FLG;

                            /*begin addon dbs - cambio por incluir planificacion de factura liencia*/
                            BLLicenciaPlaneamiento servLicPlan = new BLLicenciaPlaneamiento();
                            string desPeriodo = "";
                            var obj = servLicPlan.ObtenerPlanificacion(GlobalVars.Global.OWNER, codigoLicPlan);
                            if (obj != null)
                            {
                                desPeriodo = string.Format("{0} - {1}", obj.LIC_YEAR, obj.LIC_MONTH_DESC);
                            }
                            /*end addon dbs - cambio por incluir planificacion de factura liencia*/

                            shtml.Append("<tr class='k-grid-content'" + visible + ">");
                            shtml.AppendFormat("<td ><input type='hidden'  id='hidIdCaract_{2}' value='{1}' /> {0}</td>", licCar.DescCarateristica, licCar.CodigoCaracteristica, cuenta);
                            shtml.AppendFormat("<td >{0}</td>", desPeriodo);
                            shtml.AppendFormat("<td ><input type='hidden'  id='hidIdLic_{1}' value='{2}' /><input type='hidden'  id='hidTipo_{1}' value='{0}' />{0}</td>", licCar.Tipo, cuenta, codigoLic);
                            shtml.AppendFormat("<td >{0}</td>", licCar.FechaCaracteristicaLic.HasValue ? licCar.FechaCaracteristicaLic.Value.ToString("dd/MM/yyyy HH:mm") : "");
                            shtml.AppendFormat("<td ><input type='text' id='{1}' class='cssValCaract'  value='{0}' style='width:150px;' maxlength='18'    /></td>", (licCar.Valor != null ? licCar.Valor.Value.ToString("N4") : ""), cuenta);
                            //class='cssValCaract k-formato-numerico'
                            shtml.Append("</td>");
                            //shtml.Append("<td ></td >");
                            //shtml.Append("<td ></td >");
                            shtml.AppendFormat("<td ><input type='checkbox' id='checkFlagChar_{0}' class='cssTabLicGridCheck' {1} /></td>", cuenta, licCar.EsCaractAlterada.HasValue && licCar.EsCaractAlterada.Value ? " checked='checked'" : string.Empty);
                            shtml.AppendFormat("<td ><textarea id='txtComentChar_{0}'  {2} >{1}</textarea></td>", cuenta, licCar.CaractAlteradaDesc, licCar.EsCaractAlterada.HasValue && licCar.EsCaractAlterada.Value ? string.Empty : " disabled=disabled ");
                            shtml.Append("</tr>");
                            cuenta++;
                        }
                    }

                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarCaracteristica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddGrupoFacturacion(string GrupoDescripcion, decimal idBps, decimal idMod)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var result = new BLGrupoFacturacion().InsertarXModId(GlobalVars.Global.OWNER, GrupoDescripcion, idBps, idMod, UsuarioActual);

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "AddGrupoFacturacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddLicenciaLocalidad(DTOLicenciaLocalidad localidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var result = new BLLicenciaLocalidad().Insertar(GlobalVars.Global.OWNER, localidad.CodigoLicencia, localidad.CodigoTipoAforo, localidad.CodigoTipoLocalidad, localidad.Funcion, localidad.Color, localidad.Ticket, localidad.PrecVenta, localidad.ImporteBruto, localidad.Impuesto, localidad.ImporteNeto, UsuarioActual);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "AddLicenciaLocalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddParametro(decimal codLic, decimal Id, decimal TipoParametro, string Descripcion)
        {
            Resultado retorno = new Resultado();
            var parGral = new BEParametroGral();
            try
            {
                if (!isLogout(ref retorno))
                {
                    parGral.PAR_ID = Id;
                    parGral.OWNER = GlobalVars.Global.OWNER;
                    parGral.PAR_TYPE = Convert.ToInt32(TipoParametro);
                    parGral.PAR_VALUE = Descripcion;
                    parGral.ENT_ID = Constantes.ENTIDAD.LICENCIAMIENTO;
                    parGral.LOG_USER_CREAT = UsuarioActual;

                    var codigoGenPara = new BLParametroGral().Insertar(parGral);
                    var result = new BLParametroLic().Insertar(new BEParametroLic
                    {
                        LIC_ID = codLic,
                        PAR_ID = codigoGenPara,
                        OWNER = parGral.OWNER,
                        LOG_USER_CREAT = parGral.LOG_USER_CREAT
                    });

                    retorno.result = 1;
                    retorno.Code = Convert.ToInt32(parGral.PAR_ID);
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "AddParametro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdLicenciaLocalidad(DTOLicenciaLocalidad localidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var result = new BLLicenciaLocalidad().Actualizar(GlobalVars.Global.OWNER, localidad.idLicenciaLocalidad, localidad.CodigoTipoAforo, localidad.CodigoTipoLocalidad, localidad.Funcion, localidad.Color, localidad.Ticket, localidad.PrecVenta, localidad.ImporteBruto, localidad.Impuesto, localidad.ImporteNeto, UsuarioActual);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "UpdLicenciaLocalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdParametro(decimal codLic, decimal Id, decimal TipoParametro, string Descripcion)
        {
            Resultado retorno = new Resultado();
            var parGral = new BEParametroGral();
            try
            {
                if (!isLogout(ref retorno))
                {
                    parGral.PAR_ID = Id;
                    parGral.OWNER = GlobalVars.Global.OWNER;
                    parGral.PAR_TYPE = Convert.ToInt32(TipoParametro);
                    parGral.PAR_VALUE = Descripcion;
                    parGral.ENT_ID = Constantes.ENTIDAD.LICENCIAMIENTO;
                    parGral.LOG_USER_UPDATE = UsuarioActual;

                    var result = new BLParametroGral().Update(parGral);

                    retorno.result = 1;
                    retorno.Code = Convert.ToInt32(parGral.PAR_ID);
                    retorno.message = "OK";
                }

            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "UpdParametro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdObservacion(decimal codLic, decimal Id, decimal TipoObservacion, string Observacion)
        {
            Resultado retorno = new Resultado();
            var obsGral = new BEObservationGral();
            try
            {
                if (!isLogout(ref retorno))
                {
                    obsGral.OBS_ID = Id;
                    obsGral.OWNER = GlobalVars.Global.OWNER;
                    obsGral.OBS_TYPE = Convert.ToInt32(TipoObservacion);
                    obsGral.OBS_VALUE = Observacion;
                    obsGral.ENT_ID = Convert.ToInt32(Constantes.ENTIDAD.LICENCIAMIENTO);
                    obsGral.LOG_USER_UPDATE = UsuarioActual;
                    obsGral.OBS_USER = UsuarioActual;

                    var result = new BLObservationGral().Update(obsGral);

                    retorno.result = 1;
                    retorno.Code = Convert.ToInt32(obsGral.OBS_ID);
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "UpdObservacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdDocumento(decimal codLic, DTODocumento documento)
        {
            Resultado retorno = new Resultado();
            var docGral = new BEDocumentoGral();
            try
            {

                if (!isLogout(ref retorno))
                {
                    docGral.DOC_ID = documento.Id;
                    docGral.OWNER = GlobalVars.Global.OWNER;
                    docGral.DOC_TYPE = Convert.ToInt32(documento.TipoDocumento);
                    docGral.DOC_PATH = documento.Archivo;
                    docGral.DOC_DATE = Convert.ToDateTime(documento.FechaRecepcion);
                    docGral.ENT_ID = Convert.ToInt32(Constantes.ENTIDAD.LICENCIAMIENTO);
                    docGral.DOC_USER = UsuarioActual;
                    docGral.LOG_USER_UPDATE = UsuarioActual;
                    //docGral.DOC_VERSION = 1;

                    var result = new BLDocumentoGral().Update(docGral);

                    retorno.result = 1;
                    retorno.Code = Convert.ToInt32(docGral.DOC_ID);
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "UpdDocumento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtieneParametro(decimal idLic, decimal idPar)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var par = new BLParametroGral().ObtenerParaXCodLic(GlobalVars.Global.OWNER, idPar, idLic, Constantes.ENTIDAD.LICENCIAMIENTO);
                    var parametro = new DTOParametro();

                    parametro.Id = par.PAR_ID;
                    parametro.TipoParametro = Convert.ToString(par.PAR_TYPE);
                    parametro.Descripcion = par.PAR_VALUE;

                    retorno.data = Json(parametro, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtieneParametro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtieneObservacion(decimal idLic, decimal idObs)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var obs = new BLObservationGral().ObtenerObsXCodLic(GlobalVars.Global.OWNER, idObs, idLic, Constantes.ENTIDAD.LICENCIAMIENTO);
                    var observacion = new DTOObservacion();

                    observacion.Id = obs.OBS_ID;
                    observacion.TipoObservacion = Convert.ToString(obs.OBS_TYPE);
                    observacion.Observacion = obs.OBS_VALUE;

                    retorno.data = Json(observacion, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }

            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtieneObservacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtieneDocumento(decimal idLic, decimal idDoc)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var doc = new BLDocumentoGral().ObtenerDocLic(GlobalVars.Global.OWNER, idDoc, idLic, Constantes.ENTIDAD.LICENCIAMIENTO);
                    var documento = new DTODocumento();

                    documento.Id = doc.DOC_ID;
                    documento.Archivo = doc.DOC_PATH;
                    documento.TipoDocumento = Convert.ToString(doc.DOC_TYPE);
                    documento.FechaRecepcion = Convert.ToString(doc.DOC_DATE);

                    retorno.data = Json(documento, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtieneDocumento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DellAddDocumento(decimal id, bool esActivo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var docGral = new BEDocumentoGral();
                    docGral.DOC_ID = id;
                    docGral.OWNER = GlobalVars.Global.OWNER;
                    docGral.LOG_USER_UPDATE = UsuarioActual;

                    if (esActivo)
                    {
                        new BLDocumentoGral().Eliminar(docGral.OWNER, docGral.DOC_ID, docGral.LOG_USER_UPDATE);
                    }
                    else
                    {
                        new BLDocumentoGral().Activar(docGral.OWNER, docGral.DOC_ID, docGral.LOG_USER_UPDATE);

                    }
                    retorno.result = 1;
                    retorno.Code = Convert.ToInt32(docGral.DOC_ID);
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "DellAddDocumento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult delAddLocalidad(decimal id, int EsActivo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (EsActivo == 1)
                        new BLLicenciaLocalidad().Eliminar(GlobalVars.Global.OWNER, id);
                    else
                        new BLLicenciaLocalidad().Activar(GlobalVars.Global.OWNER, id);

                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "delAddLocalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DellAddParametro(decimal id, bool esActivo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (esActivo)
                        new BLParametroGral().Eliminar(GlobalVars.Global.OWNER, id, UsuarioActual);
                    else
                        new BLParametroGral().Activar(GlobalVars.Global.OWNER, id, UsuarioActual);

                    retorno.result = 1;
                    retorno.Code = Convert.ToInt32(id);
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "DellAddParametro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DellAddObservacion(decimal id, bool esActivo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (esActivo)
                        new BLObservationGral().Eliminar(GlobalVars.Global.OWNER, id, UsuarioActual);
                    else
                        new BLObservationGral().Activar(GlobalVars.Global.OWNER, id, UsuarioActual);

                    retorno.result = 1;
                    retorno.Code = Convert.ToInt32(id);
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "DellAddObservacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdPlanificacion(decimal id, decimal idBlock, string idTipoPago, string fecha)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var plan = PlanificacionTmp;
                    if (plan.Count > 0)
                    {
                        plan.ForEach(c =>
                        {
                            if (c.codigoLP == id)
                            {
                                c.fecha = Convert.ToDateTime(fecha);
                                c.codBloqueo = idBlock;
                                c.codTipoPago = idTipoPago;
                            }
                        });
                        int r = new BLLicenciaPlaneamiento().BloqueaPlaneamientoLicenciaIndividual(id, idBlock);//actualiando el periodo indiv

                        PlanificacionTmp = plan;
                    }
                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "UpPlanificacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        //REALIZAR EL BUCLE PARA INSERTAR EN LICENCIAS HIJAS Y PADRES

        [HttpPost]
        public JsonResult InsertarLicenciaFactura(decimal codLic, string docReq, string actReq, string plaReq, string desVis, string EmiMen, string Envio, decimal facGruop, decimal facForm, bool esInsert, decimal idTemp)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    ValidarLicenciaMultiplesPadre(codLic);//Esta validacion devolvera 1= si ES PADRE o 0= Si es una LIcencia Individual sin Codigo de Licencia Multiple     
                    var plan = PlanificacionTmp;
                    if (Global_Valida_Lic_Mult != 1)//Solo Aactualiza Licencia si es diferente a 1 (licencia Multiple)
                    {
                        var result = new BLLicencias().UpdateLicenciaFacturacion(codLic, GlobalVars.Global.OWNER, docReq, actReq, plaReq, desVis, Envio, facGruop, facForm, EmiMen);
                    }
                    List<BELicencias> ListaDeLicenciasHijasxPadre = new List<BELicencias>();//Lista para gUARDAR LA DATA QUE DEVUELVE EL METODO BL
                    ListaDeLicenciasHijasxPadre = new BLLicencias().ListarLicHijasxPadre(codLic);//Listar Codigo de Licencias de Planeamiento x LIcencia HIj
                    List<BELicenciaPlaneamiento> ListaDeLicenciasPlamxHija = new List<BELicenciaPlaneamiento>();

                    if (Global_Valida_Lic_Mult == 1)//Inserta Cabezera de LicenciaFactura en las Licencias Hijas
                    {
                        List<BELicencias> listaLicFacturacionHijas = new List<BELicencias>();
                        BELicencias EntidadLicFacturacionHijas = null;
                        foreach (var item in ListaDeLicenciasHijasxPadre.OrderBy(x => x.LIC_ID))
                        {
                            EntidadLicFacturacionHijas = new BELicencias();
                            if (item.INVG_ID != 0 && facGruop == 0)// Si El invg_id que trae la lista de licencias hija !=0 y fact group es 0entnoces que tome el valor de la lista
                                facGruop = item.INVG_ID;

                            EntidadLicFacturacionHijas.LIC_ID = item.LIC_ID;
                            EntidadLicFacturacionHijas.LIC_DREQ = docReq;
                            EntidadLicFacturacionHijas.LIC_CREQ = actReq;
                            EntidadLicFacturacionHijas.LIC_PREQ = plaReq;
                            EntidadLicFacturacionHijas.LIC_SEND = Convert.ToDecimal(Envio);
                            EntidadLicFacturacionHijas.LIC_DISC = desVis;
                            EntidadLicFacturacionHijas.INVG_ID = facGruop;
                            EntidadLicFacturacionHijas.INVF_ID = facForm;
                            EntidadLicFacturacionHijas.LIC_EMI_MENSUAL = EmiMen;

                            listaLicFacturacionHijas.Add(EntidadLicFacturacionHijas);

                            // var result2 = new BLLicencias().UpdateLicenciaFacturacion(item.LIC_ID, GlobalVars.Global.OWNER, docReq, actReq, desVis, Envio, facGruop, facForm);
                        }
                        new BLLicencias().ActualizaLicenciasFacturacionHijasXML(listaLicFacturacionHijas); //insertando masivamente
                    }
                    //decimal anioSet = 0;
                    //if (plan != null)
                    //{
                    //    if (Global_Valida_Lic_Mult != 1) //No debe de Crear Periodos para la licencia Padre 
                    //    {
                    //        List<BELicenciaPlaneamiento> lista = new List<BELicenciaPlaneamiento>();
                    //        List<BELicenciaPlaneamiento> listapm = new List<BELicenciaPlaneamiento>(); ;//lista que se envia para modificar
                    //        int VALIDA = new BLLicencias().ValidarLicenciasMultiplesHijas(codLic);//valida si es licencia hija
                    //        int respuestamodalidad = new BLLicencias().ValidaModalidadLicencia(codLic);//valida modalidad de licencia 

                    //        plan.ForEach(c =>
                    //        {
                    //            if (c.codBloqueo == 0) { c.codBloqueo = null; }
                    //            if (VALIDA == 0 && c.mes == 0) { c.mes = c.codigoLP; }//INSERTA ORDEN EN LIC INDIV QUE NO SON LIC HIJAS
                    //            if (VALIDA == 1 && c.codigoLP != 0) { c.mes = c.codigoLP; }
                    //            // if (esInsert == true && c.codigoLP == 0) { c.codigoLP = c.mes; }//esto solo es para que pueda insertar el OrderId la primera vez Si no no lista..
                    //            BELicenciaPlaneamiento objplan = new BELicenciaPlaneamiento();
                    //            objplan.LIC_ID = codLic;
                    //            objplan.LIC_YEAR = c.anio;
                    //            objplan.LIC_MONTH_DESC = c.descMes;
                    //            objplan.LIC_ORDER = c.mes;//mes CodigoLp, Order
                    //            //objplan.LIC_ORDER = c.mes; estaba bien
                    //            objplan.LIC_DATE = c.fecha;
                    //            objplan.LOG_USER_CREAT = UsuarioActual;
                    //            objplan.BLOCK_ID = Convert.ToDecimal(c.codBloqueo);
                    //            objplan.PAY_ID = c.codTipoPago;
                    //            objplan.LIC_PL_ID = c.codigoLP;
                    //            lista.Add(objplan);
                    //            //************** Agrego para poder modificar ********
                    //            if (c.codigoLP != 0)
                    //                listapm.Add(objplan);
                    //            //***************************************************
                    //            anioSet = c.anio;
                    //        });

                    //        //*******STrae Periodos Antigos de esta licencia para que no se inserten repetidos
                    //        List<BELicenciaPlaneamiento> listaplanxlicenciaHijaIndiv = new BLLicenciaPlaneamiento().ListaPlaneamientoxLicHij(GlobalVars.Global.OWNER, codLic);
                    //        List<BELicenciaPlaneamiento> listap = null;//lista que se envia para insertar

                    //        if (listaplanxlicenciaHijaIndiv.Count == 0)//primera vez que esta insertando
                    //            listap = new BLLicenciaPlaneamiento().InsertaPlaneamientoLicHijaXML(lista);

                    //        if (VALIDA == 0 && listaplanxlicenciaHijaIndiv.Count > 0)//SI ES UNA LICENCIA INDIVIDUAL NORMAL (NO ES LICENCIA HIJA )
                    //        {
                    //            //***************************** INSERTA PLANEAMIENTO *********************************************************
                    //            if (listaplanxlicenciaHijaIndiv != null && listaplanxlicenciaHijaIndiv.Count > 0)
                    //            {
                    //                listaplanxlicenciaHijaIndiv.ForEach(l =>//VALUE =PL_LIC_ID
                    //                {
                    //                    foreach (var x in lista.OrderBy(x => x.LIC_PL_ID))//Obteniendo el plan que vamos a insertar.
                    //                    {
                    //                        lista = lista.Where(z => z.LIC_PL_ID != Convert.ToDecimal(l.LIC_PL_ID)).ToList();//Obteniendo solo 
                    //                    }
                    //                });
                    //            }
                    //            listap = new BLLicenciaPlaneamiento().InsertaPlaneamientoLicHijaXML(lista);
                    //        }


                    //        if (VALIDA == 1 || respuestamodalidad == 1)
                    //        {
                    //            if (listap == null)//Eso quiere decir que esta agregando planifiacion
                    //            {
                    //                //***************************** INSERTA PLANEAMIENTO *********************************************************
                    //                if (listaplanxlicenciaHijaIndiv != null && listaplanxlicenciaHijaIndiv.Count > 0)
                    //                {
                    //                    listaplanxlicenciaHijaIndiv.ForEach(l =>//VALUE =PL_LIC_ID
                    //                    {
                    //                        foreach (var x in lista.OrderBy(x => x.LIC_PL_ID))//Obteniendo el plan que vamos a insertar.
                    //                        {
                    //                            lista = lista.Where(z => z.LIC_PL_ID != Convert.ToDecimal(l.LIC_PL_ID)).ToList();//Obteniendo solo 
                    //                        }
                    //                    });
                    //                }
                    //                listap = new BLLicenciaPlaneamiento().InsertaPlaneamientoLicHijaXML(lista);//Inserta Planeamiento 
                    //                new BLLicenciaPlaneamiento().ActualizaPlaneamientoLicenciaHijaIndividualXML(listapm);
                    //                //***************************************************************************************************************
                    //            }
                    //            //**Setenado el codigo de licenciaHija a la lista (SOlo sera 1)**************************************************
                    //            List<BELicencias> listalic = new List<BELicencias>();
                    //            listalic.Add(new BELicencias { LIC_ID = codLic });
                    //            //***************************************************************************************************************

                    //            InsertarCaractAutoxPeriodo(listalic, listap);

                    //        }
                    //    }


                    //    if (Global_Valida_Lic_Mult == 1)//bucle para insertar Planificacion en las licencias Hijas
                    //    {
                    //        if (esInsert == true)
                    //        {
                    //            List<BELicenciaPlaneamiento> lista = new List<BELicenciaPlaneamiento>();
                    //            foreach (var item in ListaDeLicenciasHijasxPadre.OrderBy(item => item.LIC_ID))
                    //            {
                    //                // ListarCaracterRegxLic = new BLEstablecimiento().ListarCaracteristicaRegxLic(item.LIC_ID);//Primero Recuperar La lista 
                    //                if (plan != null)
                    //                {

                    //                    plan.ForEach(c =>
                    //                    {
                    //                        if (c.codBloqueo == 0) { c.codBloqueo = null; }
                    //                        BELicenciaPlaneamiento objplan = new BELicenciaPlaneamiento();
                    //                        objplan.LIC_ID = item.LIC_ID;
                    //                        objplan.LIC_YEAR = c.anio;
                    //                        objplan.LIC_MONTH_DESC = c.descMes;
                    //                        objplan.LIC_ORDER = c.codigoLP;
                    //                        objplan.LIC_DATE = c.fecha;
                    //                        objplan.LOG_USER_CREAT = UsuarioActual;
                    //                        //objplan.BLOCK_ID = null;
                    //                        objplan.PAY_ID = c.codTipoPago;
                    //                        lista.Add(objplan);

                    //                        anioSet = c.anio;
                    //                    });
                    //                }
                    //            }
                    //            List<BELicenciaPlaneamiento> listap = new BLLicenciaPlaneamiento().InsertaPlaneamientoLicHijaXML(lista);//enviando la lista que recupere

                    //            InsertarCaractAutoxPeriodo(ListaDeLicenciasHijasxPadre, listap);
                    //        }
                    //        PlanificacionTmp = null;
                    //    }
                    //    retorno.result = 1;
                    //    retorno.message = retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    //    retorno.valor = anioSet.ToString();
                    //}
                    retorno.result = 1;
                    retorno.message = retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "InsertarLicenciaFactura", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult ValidarPeriodoRepetido(decimal codLic, decimal anio)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var result = 0;
                    int respuesta = new BLLicencias().ValidarLicenciasMultiplesPadres(codLic);
                    //Si es Licencia Padre entra
                    if (respuesta == 1)
                    {
                        //Recuepera las Licencias Hijas del padre
                        List<BELicencias> ListaDeLicenciasHijasxPadre = new List<BELicencias>();
                        ListaDeLicenciasHijasxPadre = new BLLicencias().ListarLicHijasxPadre(codLic);
                        //recorre la lista
                        foreach (var x in ListaDeLicenciasHijasxPadre.OrderBy(x => x.LIC_ID))
                        {//Valida Periodo repetido con las licencias Hijas
                            result = new BLLicenciaPlaneamiento().ValidarPeriodoRepetido(GlobalVars.Global.OWNER, x.LIC_ID, anio);
                            if (result > 0)
                            {//Si una sola licencia Ya tiene el Planeamiento del año Finaliza el Bucle
                                retorno.message = Constantes.MensajeLicenciamiento.MSG_VALIDACION_PERIODO_LIC_MULTIPLE;
                                break;
                            }
                        }
                    }
                    else
                    {
                        //Si es una Licencia Normal Debe continuar con la programacion habitual
                        result = new BLLicenciaPlaneamiento().ValidarPeriodoRepetido(GlobalVars.Global.OWNER, codLic, anio);
                        retorno.message = Constantes.MensajeLicenciamiento.MSG_VALIDACION_PERIODO;
                    }
                    if (result > 0)
                    {

                        retorno.result = 0;
                        retorno.message = Constantes.MensajeLicenciamiento.MSG_VALIDACION_PERIODO_LIC_MULTIPLE;

                    }
                    else
                    {
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                }

            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ValidarPeriodoRepetido", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public List<DTOTarifaTestCaracteristica> CaracteristicaTmp
        {
            get
            {
                return (List<DTOTarifaTestCaracteristica>)Session[K_SESION_TARIFA_CAR];
            }
            set
            {
                Session[K_SESION_TARIFA_CAR] = value;
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult InsertarLicenciaCaract(string caracteristicas)
        {
            Resultado retorno = new Resultado();
            try
            {
                //CaracteristicaTmp.Count;
                List<DTOLicenciaCaracteristica> lista = new List<DTOLicenciaCaracteristica>();
                if (!isLogout(ref retorno))
                {
                    System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                    List<DTOLicenciaCaracteristica> lista2 = js.Deserialize<List<DTOLicenciaCaracteristica>>(caracteristicas);
                    lista = lista2.Where(z => z.CaractAlteradaDesc != null).ToList(); // se repite la lista con valores null  Solucionado

                    var respuesta = new BLLicencias().ValidarLicenciasMultiplesHijas(lista[0].CodigoLic);//valida si es licencia Hija

                    var respuestamodalidad = new BLLicencias().ValidaModalidadLicencia(lista[0].CodigoLic);//valida modalidad de licencia 

                    //condicion
                    #region LICENCIA_INDIVIDUAL
                    if (respuesta == 0 && respuestamodalidad == 0)
                    {
                        if (lista != null && lista.Count > 0)
                        {
                            BLLicenciaPlaneamiento ServPlan = new BLLicenciaPlaneamiento();
                            var planLicencia = ServPlan.ObtenerPlanificacion(GlobalVars.Global.OWNER, lista[0].CodigoLicPlan);
                            if (planLicencia != null)
                            {//if (planLicencia.LIC_PL_STATUS != null && (planLicencia.LIC_PL_STATUS == "A" || planLicencia.LIC_PL_STATUS == "P"))
                                if ((planLicencia.LIC_PL_STATUS != null && (planLicencia.LIC_PL_STATUS == "A" || planLicencia.LIC_PL_STATUS == "P")))
                                {
                                    List<BECaracteristicaLic> chars = new List<BECaracteristicaLic>();

                                    // lista.ForEach(x =>  EsValorAlter ValorAlterDes
                                    bool flgExitoParseDec = true;
                                    foreach (var x in lista)
                                    {
                                        decimal valorCarac = 0;
                                        try
                                        {
                                            if (x.ValorString == "")
                                            {
                                                valorCarac = 0;
                                            }
                                            else
                                            {
                                                valorCarac = decimal.Parse(x.ValorString);
                                            }
                                        }
                                        catch
                                        {
                                            flgExitoParseDec = false;
                                        }

                                        chars.Add(new BECaracteristicaLic
                                        {
                                            LIC_CAR_ID = 0,
                                            LIC_ID = x.CodigoLic,
                                            CHAR_ID = x.CodigoCaracteristica,
                                            LIC_CHAR_VAL = valorCarac,
                                            OWNER = GlobalVars.Global.OWNER,
                                            LOG_USER_CREAT = UsuarioActual,
                                            LIC_VAL_ORIGEN = x.Tipo,
                                            LIC_PL_ID = x.CodigoLicPlan,
                                            FLG_MANUAL = x.EsCaractAlterada,
                                            COMMENT_FLG = x.EsCaractAlterada != null && x.EsCaractAlterada.Value ? x.CaractAlteradaDesc : string.Empty
                                        });
                                    }//);
                                    if (flgExitoParseDec)
                                    {

                                        var result = new BLCaracteristica().InsertarCaractLicencia(chars);
                                        retorno.result = 1;
                                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                                    }
                                    else
                                    {
                                        retorno.result = 0;
                                        retorno.message = "Ingrese correctamente el valor de las caracteristicas.";
                                    }
                                }
                                else
                                {
                                    retorno.result = 0;
                                    retorno.message = "No se puede modificar, el Periodo seleccionado está cerrado.";
                                }
                            }
                        }
                        else
                        {
                            retorno.result = 0;
                            retorno.message = "No se ha inicializado la lista de caracteristicas para registrar";
                        }
                    }
                    #endregion
                    else //armando las caracteristicas para poder hacer el insert Xml
                    {
                        List<BECaracteristicaLic> chars = new List<BECaracteristicaLic>();

                        foreach (var x in lista)
                        {

                            chars.Add(new BECaracteristicaLic
                            {
                                LIC_CAR_ID = 0,
                                LIC_ID = x.CodigoLic,
                                CHAR_ID = x.CodigoCaracteristica,
                                LIC_CHAR_VAL = decimal.Parse(x.ValorString),
                                OWNER = GlobalVars.Global.OWNER,
                                LOG_USER_CREAT = UsuarioActual,
                                LIC_VAL_ORIGEN = x.Tipo,
                                LIC_PL_ID = 0,
                                FLG_MANUAL = x.EsCaractAlterada,
                                COMMENT_FLG = x.EsCaractAlterada != null && x.EsCaractAlterada.Value ? x.CaractAlteradaDesc : string.Empty
                            });
                        }

                        ActualizaCaracteristicasXML(chars);

                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    }

                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "InsertarLicenciaCaract", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Upload(DTODocumento documento, decimal codLic)
        {
            Resultado retorno = new Resultado();
            try
            {
                var docGral = new BEDocumentoGral();
                var resultado = Convert.ToInt32(documento.Id);
                var fec = DateTime.Now.ToString("yyyyMMddHHmmss");
                var guid = Guid.NewGuid().ToString();
                var file = Request.Files["Filedata"];

                if (documento.Id == 0)
                {
                    var name = CleanInput(file.FileName);
                    var nombreGenerado = "";
                    nombreGenerado = String.Format("{0}_{1}_{2}_{3}", fec, resultado, guid, name);
                    documento.Archivo = nombreGenerado;
                    var path = GlobalVars.Global.RutaTabDocumentoLic;// System.Web.Configuration.WebConfigurationManager.AppSettings["RutaFisicaImgLicenciaDoc"];
                    string savePath = String.Format("{0}{1}", path, nombreGenerado);
                    file.SaveAs(savePath);

                    docGral.DOC_ID = documento.Id;
                    docGral.OWNER = GlobalVars.Global.OWNER;
                    docGral.DOC_TYPE = Convert.ToInt32(documento.TipoDocumento);
                    docGral.DOC_PATH = documento.Archivo;
                    docGral.DOC_DATE = Convert.ToDateTime(documento.FechaRecepcion);
                    docGral.ENT_ID = Convert.ToInt32(Constantes.ENTIDAD.LICENCIAMIENTO);
                    docGral.DOC_USER = UsuarioActual;
                    docGral.LOG_USER_CREAT = UsuarioActual;
                    docGral.DOC_VERSION = 1;

                    var codigoGenDoc = new BLDocumentoGral().Insertar(docGral, new BEDocumentoLic
                    {
                        LIC_ID = codLic,
                        OWNER = docGral.OWNER,
                        LOG_USER_CREAT = docGral.LOG_USER_CREAT,
                        DOC_ORG = Constantes.OrigenDocumento.EXTERNO
                    });
                    //var result = new BLDocumentoLic().Insertar(new BEDocumentoLic
                    //{
                    //    LIC_ID = codLic,
                    //    DOC_ID = codigoGenDoc,
                    //    OWNER = docGral.OWNER,
                    //    LOG_USER_CREAT = docGral.LOG_USER_CREAT,
                    //    DOC_ORG = Constantes.OrigenDocumento.EXTERNO
                    //});

                    retorno.result = 1;
                    retorno.Code = Convert.ToInt32(docGral.DOC_ID);
                    retorno.message = "OK";

                }
                else
                {
                    docGral.DOC_ID = documento.Id;
                    docGral.OWNER = GlobalVars.Global.OWNER;
                    docGral.DOC_TYPE = Convert.ToInt32(documento.TipoDocumento);
                    docGral.DOC_PATH = documento.Archivo;
                    docGral.DOC_DATE = Convert.ToDateTime(documento.FechaRecepcion);
                    docGral.ENT_ID = Convert.ToInt32(Constantes.ENTIDAD.LICENCIAMIENTO);
                    docGral.DOC_USER = UsuarioActual;
                    docGral.LOG_USER_UPDATE = UsuarioActual;

                    var path = System.Web.Configuration.WebConfigurationManager.AppSettings["RutaFisicaImgLicenciaDoc"];
                    string savePath = String.Format("{0}{1}", path, documento.Archivo);
                    if (System.IO.File.Exists(savePath))
                    {
                        System.IO.File.Delete(savePath);
                    }
                    file.SaveAs(savePath);

                    var result = new BLDocumentoGral().Update(docGral);

                    retorno.result = 1;
                    retorno.Code = Convert.ToInt32(docGral.DOC_ID);
                    retorno.message = "OK";
                }
                //var pathWeb = System.Web.Configuration.WebConfigurationManager.AppSettings["RutaWebImgRecaudador"];
                //return Content(String.Format("{0}{1}", pathWeb, nombreGenerado));
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Upload", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarImpuesto(decimal codigoEstab)
        {
            Resultado retorno = new Resultado();
            var impuestos = new BLLicenciaImpuesto().ListaImpuesto(GlobalVars.Global.OWNER, codigoEstab);
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget' style='border-collapse: collapse;' >");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Impuesto</th>");
                    shtml.Append("<th class='k-header'>Valor %</th>");
                    shtml.Append("<th class='k-header'>Valor $</th>");
                    shtml.Append("</tr></thead>");
                    decimal acumPer = 0;
                    decimal acumVal = 0;
                    if (impuestos != null && impuestos.Count > 0)
                    {
                        impuestos.ForEach(s =>
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", s.TAX_ID);
                            shtml.AppendFormat("<td >{0}</td>", s.IMPUESTO);
                            shtml.AppendFormat("<td >{0}</td>", s.TAXV_VALUEP);
                            shtml.AppendFormat("<td >{0}</td>", s.TAXV_VALUEM);
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                            acumPer = acumPer + s.TAXV_VALUEP;
                            acumVal = acumVal + s.TAXV_VALUEM;
                        });
                    }
                    else
                    {

                        if (codigoEstab == -1)
                            shtml.Append("<tr><td colspan='12' style='height:30px;'><center><b>Seleccione un periodo planificación.</b></center></td></tr>");
                        else
                            shtml.Append("<tr class='k-grid-content'><td colspan=4><b><center>No se encontraron impuestos para establecimiento asociado.</center></b></td></tr>");
                    }
                    shtml.Append("</table>");
                    retorno.message = shtml.ToString();
                    retorno.data = Json(new { totalImpPer = acumPer, totalImpVal = acumVal }, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarIMPUESTO", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        private string itemsT(List<SelectListItem> items, string codeId, string tipoPagoPadre)
        {
            string option = "";

            foreach (var item in items)
            {
                string selected = "";
                if (item.Value == codeId)
                {
                    selected = " selected='selected' ";
                }
                else if (tipoPagoPadre == item.Value)
                {
                    selected = " selected=selected ";
                }
                option += "<option value='" + item.Value + "'  " + selected + "  >" + item.Text + "</option>";

            }
            return option;
        }
        //[HttpPost]
        //public ActionResult UploadTrace(DTODocumento documento, decimal codLic)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        var docGral = new BEDocumentoGral();
        //        var resultado = Convert.ToInt32(documento.Id);
        //        var fec = DateTime.Now.ToString("yyyyMMddHHmmss");
        //        var guid = Guid.NewGuid().ToString();
        //        var file = Request.Files["Filedata"];

        //        if (documento.Id == 0)
        //        {
        //            var name = CleanInput(file.FileName);
        //            var nombreGenerado = "";
        //            nombreGenerado = String.Format("{0}_{1}_{2}_{3}", fec, resultado, guid, name);
        //            documento.Archivo = nombreGenerado;
        //            var path = GlobalVars.Global.RutaDocLicEntrada; 
        //            string savePath = String.Format("{0}{1}", path, nombreGenerado);
        //            file.SaveAs(savePath);

        //            docGral.DOC_ID = documento.Id;
        //            docGral.OWNER = GlobalVars.Global.OWNER;
        //            docGral.DOC_TYPE = Convert.ToInt32(documento.TipoDocumento);
        //            docGral.DOC_PATH = documento.Archivo;
        //            docGral.DOC_DATE =DateTime.Now;
        //            docGral.ENT_ID = Convert.ToInt32(Constantes.ENTIDAD.LICENCIAMIENTO);
        //            docGral.DOC_USER = UsuarioActual;
        //            docGral.LOG_USER_CREAT = UsuarioActual;
        //            docGral.DOC_VERSION = 1;

        //            var codigoGenDoc = new BLDocumentoGral().Insertar(docGral, new BEDocumentoLic
        //            {
        //                LIC_ID = codLic,
        //                OWNER = docGral.OWNER,
        //                LOG_USER_CREAT = docGral.LOG_USER_CREAT,
        //                DOC_ORG = Constantes.OrigenDocumento.ENTRADA
        //            });


        //            retorno.result = 1;
        //            retorno.Code = Convert.ToInt32(docGral.DOC_ID);
        //            retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        var path = GlobalVars.Global.RutaDocLicEntrada;
        //        string savePath = String.Format("{0}{1}", path, documento.Archivo);
        //        if (System.IO.File.Exists(savePath))
        //        {
        //            System.IO.File.Delete(savePath);
        //        }
        //        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Upload", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult EliminarDocumento(decimal idDoc)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var document = new BLDocumentoGral().Obtener(GlobalVars.Global.OWNER, idDoc);
                    if (document != null)
                    {
                        var path = GlobalVars.Global.RutaDocLicEntrada;
                        string savePath = String.Format("{0}{1}", path, document.DOC_PATH);
                        if (System.IO.File.Exists(savePath))
                        {
                            System.IO.File.Delete(savePath);
                        }
                        var result = new BLDocumentoGral().EliminarFisico(GlobalVars.Global.OWNER, idDoc);
                    }
                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "EliminarDocumento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region LOCALIDADES

        //#region Localidad
        //public JsonResult ListarLocalidades(decimal codigoLic)
        //{
        //    var listaLocalidad = new BLLicenciaLocalidad().ListarLocalidad(GlobalVars.Global.OWNER, codigoLic);
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        StringBuilder shtml = new StringBuilder();
        //        shtml.Append("<table border=0 width='100%;' class='k-grid k-widget' id='tblLocalidades'>");
        //        shtml.Append("<thead><tr>");
        //        shtml.Append("<th class='k-header' >Id</th>");
        //        shtml.Append("<th class='k-header' >Localidad</th>");
        //        //shtml.Append("<th class='k-header' >Tickets</th>");
        //        shtml.Append("<th class='k-header' >Pre. Venta</th>");
        //        shtml.Append("<th class='k-header' >Imp. Bruto</th>");
        //        shtml.Append("<th class='k-header' >Impuesto</th>");
        //        shtml.Append("<th class='k-header' >Neto</th>");
        //        shtml.Append("<th class='k-header' >Color</th>");
        //        shtml.Append("<th class='k-header' >Usuario Reg.</th>");
        //        shtml.Append("<th class='k-header' >Fecha Reg.</th>");
        //        shtml.Append("<th class='k-header' >Usuario Mod.</th>");
        //        shtml.Append("<th class='k-header' >Fecha Mod.</th>");
        //        shtml.Append("<th class='k-header' ></th></tr></thead>");

        //        if (listaLocalidad != null)
        //        {
        //            foreach (var item in listaLocalidad.OrderBy(x => x.SEC_ID))
        //            {
        //                shtml.Append("<tr class='k-grid-content'>");
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' class='Id'>{0}</td>", item.SEC_ID);

        //                if (item.SEC_DESC == null || item.SEC_DESC == "")
        //                    shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtSecDesc{1}' maxlength='40'           style='width:150px;text-align:right' onblur='cambiosDatosLocales({1})'> </td>", item.SEC_DESC, item.SEC_ID);
        //                else
        //                    shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' ><input type='text' id='txtSecDesc{1}'  maxlength='40' value='{0}' style='width:150 px;text-align:right' onblur='cambiosDatosLocales({1})'> </td>", item.SEC_DESC, item.SEC_ID);


        //                //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtTicket{1}'  maxlength='15' value={0} style='width:75px;text-align:right' onblur='cambiosDatosLocales({1})'> </td>", item.SEC_TICKETS, item.SEC_ID);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtPreVenta{1}'  maxlength='15' value={0} style='width:75px;text-align:right' onblur='calcularMontosLocalidad({1})'> </td>", item.SEC_VALUE, item.SEC_ID);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtBruto{1}'     maxlength='15' value={0} style='width:75px;text-align:right' onblur='calcularMontosLocalidad({1})'> </td>", item.SEC_GROSS, item.SEC_ID);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtImpuesto{1}'  maxlength='15' value={0} style='width:75px;text-align:right' onblur='calcularMontosLocalidad({1})'> </td>", item.SEC_TAXES, item.SEC_ID);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <label id='lblNeto{1}'      maxlength='15' value={0} style='width:75px;text-align:right' onblur='cambiosDatosLocales({1})'> {0}</label> </td>", item.SEC_NET, item.SEC_ID);

        //                //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtPreVenta{1}'  maxlength='15' value={0} style='width:75px;text-align:right' onblur='cambiosDatosLocales({1})'> </td>", item.SEC_VALUE, item.LIC_SEC_ID);
        //                //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtBruto{1}'     maxlength='15' value={0} style='width:75px;text-align:right' onblur='cambiosDatosLocales({1})'> </td>", item.SEC_GROSS, item.LIC_SEC_ID);
        //                //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtImpuesto{1}'  maxlength='15' value={0} style='width:75px;text-align:right' onblur='cambiosDatosLocales({1})'> </td>", item.SEC_TAXES, item.LIC_SEC_ID);
        //                //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtNeto{1}'      maxlength='15' value={0} style='width:75px;text-align:right' onblur='cambiosDatosLocales({1})'> </td>", item.SEC_NET, item.LIC_SEC_ID);

        //                if (item.SEC_COLOR == null || item.SEC_COLOR == "")
        //                    shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtColor{1}' maxlength='20'           style='width:75px;text-align:right' onblur='cambiosDatosLocales({1})'> </td>", item.SEC_COLOR, item.SEC_ID);
        //                else
        //                    shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtColor{1}' maxlength='20' value={0} style='width:75px;text-align:right' onblur='cambiosDatosLocales({1})'> </td>", item.SEC_COLOR.ToUpper(), item.SEC_ID);

        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", item.LOG_USER_CREAT);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.LOG_DATE_CREAT));
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", item.LOG_USER_UPDATE);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.LOG_DATE_UPDATE));

        //                shtml.AppendFormat("<td style='text-align:center'>");
        //                shtml.AppendFormat("<a href=# onclick='eliminarLocalidad({0});'><img src='../Images/iconos/delete.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.SEC_ID, "Eliminar Localidad.");
        //                shtml.AppendFormat("</td>");
        //                shtml.Append("</tr>");

        //                shtml.Append("</div>");
        //                shtml.Append("</td>");
        //                shtml.Append("</tr>");
        //            }
        //        }
        //        shtml.Append("</table>");
        //        retorno.message = shtml.ToString();
        //        retorno.result = 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = ex.Message;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarLocalidad", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult AddLocalidades(BELicenciaLocalidad Localidad)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            Localidad.OWNER = GlobalVars.Global.OWNER;
        //            Localidad.LOG_USER_CREAT = UsuarioActual;
        //            var result = new BLLicenciaLocalidad().InsertarLocalidad(Localidad);
        //            retorno.result = 1;
        //            retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "AddLocalidades", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult ActualizarLocalidad(BELicenciaLocalidad Localidad)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            Localidad.OWNER = GlobalVars.Global.OWNER;
        //            Localidad.LOG_USER_UPDATE = UsuarioActual;
        //            var result = new BLLicenciaLocalidad().ActualizarLocalidad(Localidad);
        //            retorno.result = 1;
        //            retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ActualizarLocalidad", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult EliminarLocalidad(decimal id)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            BELicenciaLocalidad Localidad = new BELicenciaLocalidad();
        //            var result = new BLLicenciaLocalidad().Eliminar(GlobalVars.Global.OWNER, id);
        //            retorno.result = 1;
        //            retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "EliminarLocalidadAforo", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        //#region Aforo
        //public JsonResult ListarLocalidadAforo(decimal codigoLic)
        //{
        //    var listaLocalidadAforo = new BLLicenciaAforo().Listar(GlobalVars.Global.OWNER, codigoLic);
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        StringBuilder shtml = new StringBuilder();
        //        shtml.Append("<table border=0 width='100%;' class='k-grid k-widget' id='tblLocalidadAforo'>");
        //        shtml.Append("<thead><tr>");
        //        shtml.Append("<th class='k-header' >Id</th>");
        //        shtml.Append("<th class='k-header' >Localidad</th>");
        //        shtml.Append("<th class='k-header' style='display:none'>TA</th>");    

        //        shtml.Append("<th class='k-header' >Pre-Liquidación Tickets</th>");
        //        shtml.Append("<th class='k-header' >Pre-Liquidación Neto</th>");
        //        shtml.Append("<th class='k-header' >Liquidación Tickets</th>");
        //        shtml.Append("<th class='k-header' >Liquidación Neto</th>");

        //        shtml.Append("<th class='k-header' >Usuario Reg.</th>");
        //        shtml.Append("<th class='k-header' >Fecha Reg.</th>");
        //        shtml.Append("<th class='k-header' >Usuario Mod.</th>");
        //        shtml.Append("<th class='k-header' >Fecha Mod.</th>");
        //        shtml.Append("<th class='k-header' ></th></tr></thead>");

        //        if (listaLocalidadAforo != null)
        //        {
        //            foreach (var item in listaLocalidadAforo.OrderBy(x => x.CAP_LIC_ID))
        //            {
        //                shtml.Append("<tr class='k-grid-content'>");
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' class='Id'>{0}</td>", item.CAP_LIC_ID);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <label id='lblLocalidadAforoDesc{1}'>{0}</label> </td>", item.SEC_DESC, item.CAP_LIC_ID);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;display:none' class='IdLocalidad' > <label id='idLocalidad{1}'>{0}</label> </td>", item.SECID, item.CAP_LIC_ID);

        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type=''text' id='txtPreLiqTickets{1}' maxlength='18' value={0} style='width:75px;text-align:right' onblur='cambiosDatosAforo({1})'> </td>", item.TICKET_PRE, item.CAP_LIC_ID);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <label id='lblPreLiqNeto{1}' maxlength='18' style='width:75px;text-align:right''> {0}</label> </td>", item.NETO_PRE, item.CAP_LIC_ID);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type=''text' id='txtLiqTickets{1}' maxlength='18' value={0} style='width:75px;text-align:right' onblur='cambiosDatosAforo({1})'> </td>", item.TICKET_LIQ, item.CAP_LIC_ID);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <label id='lblLiqNeto{1}' maxlength='18' style='width:75px;text-align:right' '> {0} </label> </td>", item.NETO_LIQ, item.CAP_LIC_ID);

        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", item.LOG_USER_CREAT);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.LOG_DATE_CREAT));
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", item.LOG_USER_UPDATE);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.LOG_DATE_UPDATE)); ;

        //                //if (item.CAP_IPRE)
        //                //    shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='radio' id='chkPLiquidar{1}' onchange='changeLiquidar({1})' name='chkLiquidar{1}' ' checked='checked' /> </td>", item.CAP_IPRE, item.CAP_LIC_ID);
        //                //else
        //                //    shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='radio' id='chkPLiquidar{1}' onchange='changeLiquidar({1})' name='chkLiquidar{1}'  /> </td>", item.CAP_IPRE, item.CAP_LIC_ID);

        //                //if (item.CAP_ILIQ)
        //                //    shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='radio' id='chkLiquidar{1}'  onchange='changeLiquidar({1})' name='chkLiquidar{1}'  checked='checked' /> </td>", item.CAP_ILIQ, item.CAP_LIC_ID);
        //                //else
        //                //    shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='radio' id='chkLiquidar{1}'  onchange='changeLiquidar({1})' name='chkLiquidar{1}'  /> </td>", item.CAP_ILIQ, item.CAP_LIC_ID);

        //                //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type=''text' id='txtCapTickets{1}'  maxlength='18' value={0} style='width:75px;text-align:right' onblur='cambiosDatosAforo({1})'> </td>", item.CAP_TICKETS, item.CAP_LIC_ID);
        //                //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type=''text' id='txtCapTicketsV{1}' maxlength='18' value={0} style='width:75px;text-align:right' onblur='cambiosDatosAforo({1})'> </td>", item.CAP_TICKETSV, item.CAP_LIC_ID);


        //                //shtml.AppendFormat("<td style='text-align:center'>");
        //                //shtml.AppendFormat("<a href=# onclick='eliminarAforo({0});'><img src='../Images/iconos/delete.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.CAP_LIC_ID, "Eliminar Aforo.");
        //                //shtml.AppendFormat("</td>");
        //                shtml.Append("</tr>");

        //                shtml.Append("</div>");
        //                shtml.Append("</td>");
        //                shtml.Append("</tr>");
        //            }

        //            //--
        //            shtml.Append("<tr class='k-grid-content'>");
        //            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' colspan='2'> Total Pre-Liquidación</td>");
        //            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <label id='lblTotalTicketsPreLiq{1}' maxlength='18' style='width:75px;text-align:right;font-weight:bold;'> {0} </label> </td>", 1, 1);
        //            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <label id='lblNetoTicketsPreLiq{1}'  maxlength='18' style='width:75px;text-align:right;font-weight:bold;'> {0} </label> </td>", 2, 2);
        //            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' colspan='2'>  </td>");
        //            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;'  > <button id='btnPreLiquidacion' >Pre-Liquidación</button> </td>");
        //            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' colspan='4' > </td>", 4, 4);            
        //            shtml.Append("</tr>");
        //            shtml.Append("</td>");
        //            shtml.Append("</tr>");
        //            //--
        //            shtml.Append("<tr class='k-grid-content'>");
        //            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' colspan='2'> Total Liquidación</td>");
        //            shtml.AppendFormat("<td  colspan='2' ></td>");
        //            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <label id='lblTotalTicketsLiq{1}' maxlength='18' style='width:75px;text-align:right;font-weight:bold;'> {0} </label> </td>", 3, 3);
        //            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <label id='lblNetoTicketsLiq{1}'  maxlength='18' style='width:75px;text-align:right;font-weight:bold;'> {0} </label> </td>", 4, 4);
        //            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <button id='btnLiquidacion' >Liquidación</button> </td>");
        //            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' colspan='4' > </td>", 4, 4);
        //            shtml.Append("</tr>");
        //            shtml.Append("</div>");
        //            shtml.Append("</tr>");
        //            //--
        //        }
        //        shtml.Append("</table>");
        //        retorno.message = shtml.ToString();
        //        retorno.result = 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = ex.Message;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarLocalidadAforo", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult AddLocalidadAforo(BELicenciaAforo Aforo)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            Aforo.OWNER = GlobalVars.Global.OWNER;
        //            Aforo.LOG_USER_CREAT = UsuarioActual;
        //            var result = new BLLicenciaAforo().Insertar(Aforo);
        //            retorno.result = 1;
        //            retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "AddLicenciaLocalidades", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult ActualizarLocalidadAforo(BELicenciaAforo Aforo)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            Aforo.OWNER = GlobalVars.Global.OWNER;
        //            Aforo.LOG_USER_UPDATE = UsuarioActual;
        //            var result = new BLLicenciaAforo().Actualizar(Aforo);
        //            retorno.result = 1;
        //            retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ActualizarLocalidadAforo", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult EliminarLocalidadAforo(decimal id)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            BELicenciaAforo Aforo = new BELicenciaAforo();
        //            Aforo.OWNER = GlobalVars.Global.OWNER;
        //            Aforo.CAP_LIC_ID = id;
        //            Aforo.LOG_USER_UPDATE = UsuarioActual;
        //            var result = new BLLicenciaAforo().Eliminar(Aforo);
        //            retorno.result = 1;
        //            retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "EliminarLocalidadAforo", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        //#region Matriz
        //public JsonResult ListarMatrizLocalidad(decimal codigoLic)
        //{
        //    List<BELicenciaLocalidadConteo> listaMatriz = new List<BELicenciaLocalidadConteo>();
        //    listaMatriz = new BLLicenciaLocalidad().ListarMatrizLocalidad(GlobalVars.Global.OWNER, codigoLic);

        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        StringBuilder shtml = new StringBuilder();
        //        shtml.Append("<table border=0 width='100%;' class='k-grid k-widget' id='tblMatriz'>");
        //        shtml.Append("<thead><tr>");
        //        shtml.Append("<th class='k-header' >id</th>");
        //        shtml.Append("<th class='k-header' style='display:none'>idLic</th>");
        //        shtml.Append("<th class='k-header' style='display:none'>idAfoLic</th>");
        //        shtml.Append("<th class='k-header' style='display:none'>idLocLic</th>");

        //        shtml.Append("<th class='k-header' >Aforo</th>");
        //        shtml.Append("<th class='k-header' >Localidad</th>");

        //        shtml.Append("<th class='k-header' >Tickets</th>");
        //        shtml.Append("<th class='k-header' style='display:none'>valBruto</th>");
        //        shtml.Append("<th class='k-header' style='display:none'>valImpuesto</th>");
        //        shtml.Append("<th class='k-header' style='display:none'>valNeto</th>");

        //        shtml.Append("<th class='k-header' >Bruto</th>");
        //        shtml.Append("<th class='k-header' >Impuesto</th>");
        //        shtml.Append("<th class='k-header' >Neto</th>");

        //        shtml.Append("<th class='k-header' >Usuario Reg.</th>");
        //        shtml.Append("<th class='k-header' >Fecha Reg.</th>");
        //        shtml.Append("<th class='k-header' >Usuario Mod.</th>");
        //        shtml.Append("<th class='k-header' >Fecha Mod.</th>");
        //        shtml.Append("</tr></thead>");

        //        if (listaMatriz != null)
        //        {
        //            foreach (var item in listaMatriz)
        //            {
        //                shtml.Append("<tr class='k-grid-content'>");
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' class='Id'>{0}</td>", item.Nro);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;display:none' >{0}</td>", item.LIC_ID);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;display:none' class='CAP_LIC_ID'>{0}</td>", item.CAP_LIC_ID);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;display:none' class='LIC_SEC_ID'>{0}</td>", item.LIC_SEC_ID);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center' class='AFORO'>{0}</td>", item.CAP_DESC);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center' class='LOCALIDAD'>{0}</td>", item.SEC_DESC);

        //                if (item.SEC_TICKETS != null)
        //                    shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtTickets{0}'  maxlength='15' class='requeridoMV' onkeyup='calcularMontosMatriz({0})' style='width:75px;text-align:right' value={1}></td>", item.Nro, item.SEC_TICKETS);
        //                else
        //                    shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <input type='text' id='txtTickets{0}'  maxlength='15' class='requeridoMV' onkeyup='calcularMontosMatriz({0})' style='width:75px;text-align:right'></td>", item.Nro, item.SEC_TICKETS);

        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;display:none'> <label id='lblValBruto{0}'> {1} </label> </td>", item.Nro, item.LOC_SEC_GROSS);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;display:none'> <label id='lblValImp{0}'> {1} </label> </td>", item.Nro, item.LOC_SEC_TAXES);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;display:none'> <label id='lblValNeto{0}'> {1} </label> </td>", item.Nro, item.LOC_SEC_NET);

        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <label id='lblBruto{0}'> {1} </label> </td>", item.Nro, item.SEC_GROSS);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <label id='lblImp{0}'> {1} </label> </td>", item.Nro, item.SEC_TAXES);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' > <label id='lblNeto{0}'> {1} </label> </td>", item.Nro, item.SEC_NET);

        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", UsuarioActual);
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", String.Format("{0:dd/MM/yyyy}", DateTime.Now));
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", "");
        //                shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' >{0}</td>", "");

        //                shtml.Append("</tr>");
        //            }
        //        }
        //        shtml.Append("</table>");
        //        retorno.message = shtml.ToString();
        //        retorno.result = 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = ex.Message;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarMatrizLocalidades", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult ObtenerMatrizValor(List<BELicenciaLocalidadConteo> ReglaValor)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            List<BELicenciaLocalidadConteo> ListaMatriz = new List<BELicenciaLocalidadConteo>();
        //            BETarifaReglaValor entidad = null;
        //            decimal idLic = 0;
        //            if (ReglaValor != null)
        //            {
        //                ReglaValor.ForEach(s =>
        //                {
        //                    s.OWNER = GlobalVars.Global.OWNER;
        //                    s.LOG_USER_CREAT = UsuarioActual;
        //                    s.LOG_USER_UPDATE = UsuarioActual;
        //                    idLic = s.LIC_ID;
        //                });

        //                bool resultMatriz;
        //                int cant = new BLLicenciaLocalidad().ObtenerCantMatLocActivas(GlobalVars.Global.OWNER, idLic);
        //                if (cant > 0)
        //                    resultMatriz = new BLLicenciaLocalidad().ActualizarMatrizLocalidadesXML(ReglaValor, GlobalVars.Global.OWNER);
        //                else
        //                    resultMatriz = new BLLicenciaLocalidad().InsertarMatrizLocalidadesXML(ReglaValor, GlobalVars.Global.OWNER);
        //            }
        //            retorno.result = 1;
        //            retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerMatrizValor", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult EliminarMatrizLocalidades(decimal id)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            var result = new BLLicenciaLocalidad().EliminarMatrizLocalidades(GlobalVars.Global.OWNER, id);
        //            retorno.result = 1;
        //            retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "EliminarMatrizLocalidades", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        #endregion

        #region Cadenas
        //Inactivando Una Licencia
        public ActionResult InactivarLicenciasHiajs(decimal CodLic, decimal licmaster)

        //decimal CodLic
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    //foreach(var Item in entidad.))

                    new BLLicencias().InactivarLicenciasHijas(CodLic, licmaster);
                    retorno.result = 1;

                }

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "InactivarLicenciasHijas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ValidarLicenciasMultiplesHijas(decimal CodLic)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    int respuesta = new BLLicencias().ValidarLicenciasMultiplesHijas(CodLic);

                    if (respuesta == 1)
                    {
                        retorno.result = 1;
                        //retorno.Code =  CodLic;

                    }
                    else
                    {

                        retorno.result = 0;
                    }



                }
            }
            catch (Exception ex)
            {
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        //Validar Licencias Multiples Padres 
        public ActionResult ValidarLicenciaMultiplesPadre(decimal CodLic)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    int respuesta = new BLLicencias().ValidarLicenciasMultiplesPadres(CodLic);
                    if (respuesta == 1)
                    {
                        retorno.result = 1;
                        Global_Valida_Lic_Mult = 1;

                    }
                    else
                    {
                        Global_Valida_Lic_Mult = 0;
                        retorno.result = 0;
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        //Eliminar Las Licencia Padre y Licencia Hija

        public JsonResult EliminarLicPadreyHija(decimal codigo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLLicencias licencia = new BLLicencias();
                    var resultado = licencia.EliminarLicPadreHija(new BELicencias
                    {
                        LIC_ID = codigo,
                        LOG_USER_UPDAT = UsuarioActual
                    });
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Eliminar Licencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        //Recuperar Codigo De licencia Hija mediante Codigo de Establecimiento
        public JsonResult RecuperaCodigoLicHijxCodEst(decimal CodEst, decimal licmaster)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    decimal respuesta = new BLLicencias().RecuperaCodigoLicHijxCodEst(CodEst, licmaster);
                    if (respuesta > 0)
                    {
                        retorno.result = 1;
                        retorno.valor = Convert.ToString(respuesta);
                    }
                    else
                    {
                        retorno.result = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, " Licencia No Encontrada", ex);

            }

            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        //Metodo para realizar La insercion de Caracteristicas Automaticamente
        public void InsertarCaractAutoxPeriodo(List<BELicencias> listalicenciasHijas, List<BELicenciaPlaneamiento> listaplan)
        {
            List<BECaracteristicaLic> caracteristicas = new List<BECaracteristicaLic>(); ;
            BECaracteristicaLic entidad = null;
            List<BECaracteristicaLic> listacar = null;
            int CONT = Variables.NO;

            if ((listalicenciasHijas != null && listalicenciasHijas.Count > 0) && (listaplan != null && listaplan.Count > 0))
            {
                foreach (var l in listalicenciasHijas.OrderBy(l => l.LIC_ID))
                {
                    foreach (var x in listaplan.OrderBy(x => x.LIC_PL_ID))
                    {
                        if (l.LIC_ID == x.LIC_ID)
                        {
                            //AQUI ya tenemos la data de las caracteristicas de las licencias pasadas
                            if (CONT == Variables.NO)
                                listacar = new BLCaracteristica().ListarCaractLicencia(GlobalVars.Global.OWNER, l.LIC_ID, "0", x.LIC_PL_ID);

                            foreach (var y in listacar.OrderBy(y => y.CHAR_ID))
                            {
                                if (y.LIC_CHAR_VAL != null)
                                {
                                    entidad = new BECaracteristicaLic();
                                    //variables necesarias para la insercion de caract
                                    entidad.OWNER = GlobalVars.Global.OWNER;
                                    entidad.LIC_ID = l.LIC_ID;
                                    entidad.CHAR_ID = y.CHAR_ID;
                                    entidad.LIC_CHAR_VAL = y.LIC_CHAR_VAL;
                                    entidad.LIC_VAL_ORIGEN = y.LIC_VAL_ORIGEN;
                                    entidad.LOG_USER_CREAT = UsuarioActual;
                                    entidad.LIC_PL_ID = x.LIC_PL_ID;
                                    entidad.FLG_MANUAL = y.FLG_MANUAL;
                                    entidad.COMMENT_FLG = y.COMMENT_FLG;

                                    caracteristicas.Add(entidad);
                                }
                            }
                            CONT++;

                        }
                    }
                    CONT = Variables.NO;
                }
            }
            List<BECaracteristicaLic> listaCaractInsert = new BLCaracteristica().InsertaCaractersiticasLicHijaXML(caracteristicas);
            //Insertar Automaticamente Los Descuentos Al crear 
            InsertaDescuentosLicenciaHija(listalicenciasHijas);
        }
        //Obteniendo el Codigo de Licencia Maestra Autogenerada
        public JsonResult AutogeneraCOdigo()
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    decimal res = new BLLicencias().AutogeneraCodLicpadre();

                    if (res != null && res != 0)
                    {
                        retorno.result = 1;
                        retorno.Code = Convert.ToInt32(res);
                    }
                    else
                        retorno.result = 0;


                }

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = ex.Message;

            }

            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
        //Validar Si tiene Planeamiento 
        public JsonResult ValidarPLaneamientoxLicencia(decimal idLic, decimal idTemp, string anio, string tipoPagoPadre, decimal idTarifa)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var planeamiento = new BLLicenciaPlaneamiento().ListarXLicAnio(GlobalVars.Global.OWNER, idTemp, idLic, Convert.ToInt32(anio)); ;

                }

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = ex.Message;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //InsertaDescuentos de Licencia Hija 
        public void InsertaDescuentosLicenciaHija(List<BELicencias> lista)
        {
            //****************************XML*************************************
            new BLLicenciaDescuento().InsertaDescuentosLicenciaXML(lista);
            //********************************************************************
        }

        /// <summary>
        /// Actualiza las Caracteristicas x Licencia
        /// </summary>
        /// <param name="listacaract"></param>
        public void ActualizaCaracteristicasXML(List<BECaracteristicaLic> listacaract)
        {
            decimal LICID = listacaract[0].LIC_ID; //el id de licencia es igual en todos
            var EST_ID = new BLLicencias().ListaCodigoEstxCodigoLicencia(LICID);
            List<BECaracteristicaLic> lista = new List<BECaracteristicaLic>();
            BECaracteristicaLic entidad = null;

            //*******************recupera las licencias asociadas a ese ESTABLECIMIENTO
            var listalic = new BLLicencias().ListaLicenciaxCodigoEst(EST_ID[0].EST_ID);

            if (listalic != null && listalic.Count > 0)
            {

                foreach (var l in listalic.OrderBy(l => l.LIC_ID))
                {
                    var listaplan = new BLLicenciaPlaneamiento().ListaTodaPlanificacionxLicencia(l.LIC_ID);
                    //Armando La lista para Inactivar y Actualizar
                    foreach (var x in listaplan.OrderBy(x => x.LIC_PL_ID))
                    {
                        if ((x.LIC_PL_STATUS != null && (x.LIC_PL_STATUS == "A" || x.LIC_PL_STATUS == "P")))
                        {
                            foreach (var y in listacaract.OrderBy(y => y.CHAR_ID))
                            {
                                entidad = new BECaracteristicaLic();
                                //variables necesarias para la insercion de caract
                                entidad.OWNER = GlobalVars.Global.OWNER;
                                entidad.LIC_ID = l.LIC_ID;
                                entidad.CHAR_ID = y.CHAR_ID;
                                entidad.LIC_CHAR_VAL = y.LIC_CHAR_VAL;
                                entidad.LIC_VAL_ORIGEN = y.LIC_VAL_ORIGEN;
                                entidad.LOG_USER_CREAT = UsuarioActual;
                                entidad.LIC_PL_ID = x.LIC_PL_ID;
                                entidad.FLG_MANUAL = y.FLG_MANUAL;
                                entidad.COMMENT_FLG = y.COMMENT_FLG;

                                lista.Add(entidad);
                            }
                        }
                    }
                }
                new BLCaracteristica().ActualizaCaracteristicasXML(lista);//Inserta Todas las Caracteristicas y las inactiva...


                foreach (var z in listacaract)//recorre para actualizar caracteristicas de su establecimiento
                {
                    BECaracteristicaEst entidad2 = new BECaracteristicaEst();
                    //entidad.OWNER = GlobalVars.Global.OWNER;
                    entidad2.CHAR_ID = z.CHAR_ID;
                    entidad2.LIC_ID = z.LIC_ID;
                    entidad2.VALUE = Convert.ToDecimal(z.LIC_CHAR_VAL);
                    entidad2.LOG_USER_UPDAT = GlobalVars.Global.OWNER;
                    new BLCaracteristica().ActualizarCaractersiticasEst(entidad2);
                }

            }

        }
        #endregion

        #region  Licencia Validacion Orden 
        public JsonResult ValidaLicenciaPlanificacionAutorizacion(decimal LIC_ID, int ACCION, decimal LIC_PL_ID)
        {
            Resultado retorno = new Resultado();

            try
            {
                int resp = new BLLicencias().ValidaLicenciaPlanificacionAutorizacion(LIC_ID, ACCION, LIC_PL_ID);

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

        #endregion

        #region MandarHistorico

        public JsonResult MandarAlHistorico(decimal LIC_ID, decimal BPS_ID)
        {
            Resultado retorno = new Resultado();
            try
            {

                retorno.Code = Convert.ToInt32(new BLLicencias().EnviarAlHistorico(LIC_ID, BPS_ID, UsuarioActual));

                retorno.result = 1;
                //retorno.message=

            }
            catch (Exception ex)
            {
                retorno.result = 0;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region INSERTAR PLANEAMIENTO ACTUAL

        public JsonResult InsertaPlaneamientoActual(decimal LIC_ID, int ANIO, int MES, int DIA)
        {
            Resultado retorno = new Resultado();

            try
            {
                int RespuestaModalidad = new BLLicencias().ValidaModalidadLicencia(LIC_ID);//valida modalidad de licencia 
                var Licencias = new BLLicenciaPlaneamiento().InsertaPlaneamientoActual(LIC_ID, ANIO, MES, DIA, UsuarioActual);

                List<BELicencias> listalic = new List<BELicencias>();


                if (Licencias.Count > 0)
                {

                    if (RespuestaModalidad == Variables.SI)
                    {
                        Licencias.ForEach(x =>
                        {
                            //**Setenado el codigo de licenciaHija a la lista (SOlo sera 1)**************************************************
                            if (listalic.Where(z => z.LIC_ID == x.LIC_ID).Count() == Variables.NO)
                            {
                                listalic.Add(new BELicencias { LIC_ID = x.LIC_ID });
                            }

                        });

                        InsertarCaractAutoxPeriodo(listalic, Licencias);

                    }
                    retorno.result = 1;
                    retorno.message = Variables.MENSAJE_OK_INSERT_PL;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = Variables.MENSAJE_NO_OK_INSERT_PL;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region AGREGAR CADENA LICENCIA
        public JsonResult AgregarCadenaLicencia(decimal LIC_ID, decimal LIC_MASTER)
        {
            Resultado retorno = new Resultado();
            try
            {
                int R = new BLLicencias().ActualizaLicenciaCadena(LIC_ID, LIC_MASTER, UsuarioActual);
                if (R > Variables.NO)
                {
                    retorno.result = Variables.SI;
                    retorno.message = Variables.MENSAJE_OK_ACTUALIZO_CADENA;
                }
                else
                {
                    retorno.result = Variables.NO;
                    retorno.message = Variables.MENSAJE_NO_OK_ACTUALIZO_CADENA;
                }

            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public JsonResult ValidaLicenciaLocalPermanente(decimal LIC_ID)
        {
            Resultado retorno = new Resultado();

            try
            {
                int RespuestaModalidad = new BLLicencias().ValidaModalidadLicencia(LIC_ID);//valida modalidad de licencia 

                retorno.result = RespuestaModalidad;
            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;

            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        #region Actualiza Estado de Periodos
        public JsonResult ValidaEstadoPeriodoLicencia(decimal LIC_PL_ID)
        {
            Resultado retorno = new Resultado();

            try
            {
                int res = new BLLicenciaPlaneamiento().ValidaPeriodoLicencia(LIC_PL_ID);

                retorno.result = res;

                if (res == Variables.NO)
                    retorno.message = Variables.MENSAJE_PERIODOS_ACTUALIZADOS_SIN_CAMBIOS;
            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = Variables.MENSAJE_NO_VALIDA_CORRECTAMENTE_PERIODO;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);

        }


        public JsonResult ActualizaEstadoPeriodoLicencia(decimal LIC_PL_ID, int OPCION)
        {
            Resultado retorno = new Resultado();

            try
            {


                int res = new BLLicenciaPlaneamiento().ActualizarPeriodoLicenciaAct(LIC_PL_ID, OPCION, UsuarioActual);

                retorno.result = res;
                retorno.message = Variables.MENSAJE_OK_ACTULIZA_PERIODO;

            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = Variables.MENSAJE_NO_OK_ACTULIZA_PERIODO;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        #endregion


        #region ACTUALIZAR MONTO BRUTO - DESC- NET 

        public JsonResult ActualizaMontosLicencia(decimal LIC_PL_ID, decimal LIC_ID)
        {
            /*LIC_PL_ID = codigo de el planeamiento | LIC_ID = el codigo de la licencia | puede ser mulitple o individual */

            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var valida = ValidarLicenciaPadre(LIC_ID);

                    if (valida == Variables.NO) // SI ES UNA LIC INDIVIDUAL 
                    {

                        Recaudacion.FacturacionController servCalculo = new Recaudacion.FacturacionController();
                        var montos = servCalculo.obtenerMontoFacturaCalc(LIC_ID, LIC_PL_ID);

                        new BLLicencias().ActualizaLicenciaMontos(LIC_ID, Convert.ToDecimal(montos.ValorTarifa), Convert.ToDecimal(montos.ValorDescuento), Convert.ToDecimal(montos.ValorFinal), Convert.ToDecimal(montos.ValorDescuentoRedondeoEspecial));
                        // UPDATE  DE MONTOS .}
                    }

                }
                retorno.result = Variables.SI;


            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = ex.Message;
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


        #endregion

        #region
        public JsonResult ListarDocumento(decimal codigoLic, string TipoDocumento)
        {
            Resultado retorno = new Resultado();
            //var documentos = new BLDocumentoGral().ObtenerDocXLicencia(codigoLic, GlobalVars.Global.OWNER, Constantes.ENTIDAD.LICENCIAMIENTO);
            string QueryAlfresco = new BLAlfresco().Query_Alfresco(Convert.ToInt32(TipoDocumento));
            var documentos = new List<BEDocumentoGral>();
            int cantidad = 0;
            if (GlobalVars.Global.EnviarDocumento == "T")
            {
                List<BEDocumentoGral> documentosB = new BLAlfresco().ListaDocumento(codigoLic, QueryAlfresco);
                documentos = documentosB;
                cantidad = documentosB.Count();
            }

            //List<BEDocumentoGral> documentosB = documentos.Where(u => (u.DOC_ORG == tipoOrigen || tipoOrigen == "")).OrderByDescending(y => y.LOG_DATE_CREAT).ToList();

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<th class='k-header'>ID </th>");
                    shtml.Append("<th class='k-header' >Fecha Recepción</th><th  class='k-header'>Archivo</th>");
                    //shtml.Append("<th class='k-header'>Word</th>");
                    shtml.Append("<th class='k-header'>Origen</th>");
                    shtml.Append("<th class='k-header'>Archivo</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    //shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th><th  class='k-header'>Estado</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    //EnviarDocumento
                    if (GlobalVars.Global.EnviarDocumento == "T")
                    {
                        if (documentos != null)
                        {
                            //var documentosDTO = new List<DTODocumento>();
                            if (documentos != null)
                            {
                                documentos.ForEach(s =>
                                {
                                    var newDTODocumento = new DTODocumento();
                                    newDTODocumento.codigo_alfresco = s.DOC_USER;
                                    newDTODocumento.Archivo = s.DOC_PATH.Replace(".docx", ".doc");
                                    //newDTODocumento.TipoDocumento = "CONTRATO";
                                    //newDTODocumento.TipoDocumentoDesc = "CONTRATO";
                                    newDTODocumento.FechaRecepcion = Convert.ToString(s.LOG_DATE_CREAT);
                                    newDTODocumento.EnBD = true;
                                    newDTODocumento.UsuarioCrea = s.LOG_USER_CREAT;
                                    newDTODocumento.FechaCrea = s.LOG_DATE_CREAT;
                                    //newDTODocumento.UsuarioModifica = s.LOG_USER_UPDATE;
                                    newDTODocumento.FechaModifica = s.LOG_DATE_UPDATE;
                                    newDTODocumento.UsuarioModifica = s.OWNER;
                                    newDTODocumento.Activo = true;
                                    newDTODocumento.ArchivoBytes = s.ArchivoBytes;
                                    var mimetype = newDTODocumento.UsuarioModifica;
                                    //documentosDTO.Add(newDTODocumento);
                                    var ruta = s.DOC_ORG;
                                    var showFormatoWord = true;
                                    string archivoNombre = newDTODocumento.Archivo;
                                    //var RutaEntrada = "\\\\192.168.252.105\\Archivos\\ArchivosAlfresco";
                                    var RutaEntrada = GlobalVars.Global.RutaEntradaDocumentosAlfresco;
                                    var RutaSalida = GlobalVars.Global.RutaSalidaWeb;
                                    var RutaSalidaArchivo = Path.Combine(RutaSalida, newDTODocumento.Archivo.Replace(".doc", ".pdf"));

                                    //string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                                    var RutaEntradaArchivo = Path.Combine(RutaEntrada, (newDTODocumento.Archivo.Replace(".docx", ".pdf")).Replace(".doc", ".pdf"));
                                    var RutaEntradaArchivoWord = Path.Combine(RutaEntrada, newDTODocumento.Archivo.Replace(".docx", "").Replace(".doc", "").Replace(".pdf", "") + ".docx");

                                    #region Convertir Archivos binarios Word a Word - Convertir Archivos binarios PDF to PDf
                                    //Convertir Archivos binarios Word a Word
                                    if (mimetype != "application/pdf")
                                    {
                                        var Existe_Copia2 = System.IO.File.Exists(RutaEntradaArchivo);
                                        var Existe_Copia_word2 = System.IO.File.Exists(RutaEntradaArchivoWord);
                                        if (Existe_Copia_word2 == false)
                                        {
                                            //var E = System.IO.File.Exists(RutaEntradaArchivo.Replace(".pdf", ".bin"));
                                            //if (E == false)
                                            //{
                                            //    System.IO.File.Copy(ruta, RutaEntradaArchivo.Replace(".pdf", ".bin"), true);
                                            //}
                                            //FileStream fileStream = new FileStream();
                                            //FileStream fileStream = new FileStream(ruta, FileMode.Open);
                                            StreamReader objLeerArchivo = new StreamReader(newDTODocumento.ArchivoBytes);
                                            byte[] data;
                                            using (objLeerArchivo)
                                            {
                                                using (MemoryStream ms = new MemoryStream())
                                                {
                                                    objLeerArchivo.BaseStream.CopyTo(ms);
                                                    data = ms.ToArray();
                                                }
                                            }
                                            System.IO.File.WriteAllBytes(RutaEntradaArchivoWord, data);
                                        }
                                        if (Existe_Copia2 == false)
                                        {
                                            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                                            object oMissing = System.Reflection.Missing.Value;
                                            DirectoryInfo dirInfo = new DirectoryInfo(RutaEntrada);
                                            FileInfo[] wordFiles = dirInfo.GetFiles(archivoNombre.Replace(".doc", ".docx"));
                                            word.Visible = false;
                                            word.ScreenUpdating = false;

                                            foreach (FileInfo wordFile in wordFiles)
                                            {
                                                // Cast as Object for word Open method
                                                Object filename = (Object)wordFile.FullName;

                                                // Use the dummy value as a placeholder for optional arguments
                                                Document doc = word.Documents.Open(ref filename, ref oMissing,
                                                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                                                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                                                    ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                                                doc.Activate();

                                                object outputFileName = wordFile.FullName.Replace(".docx", ".pdf");
                                                object fileFormat = WdSaveFormat.wdFormatPDF;

                                                // Save document into PDF Format
                                                doc.SaveAs(ref outputFileName,
                                                    ref fileFormat, ref oMissing, ref oMissing,
                                                    ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                                                    ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                                                    ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                                                // Close the Word document, but leave the Word application open.
                                                // doc has to be cast to type _Document so that it will find the
                                                // correct Close method.                
                                                object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
                                                ((_Document)doc).Close(ref saveChanges, ref oMissing, ref oMissing);
                                                doc = null;
                                            }
                                            //SautinSoft.PdfFocus ArchivoPdf = new SautinSoft.PdfFocus();
                                            //ArchivoPdf.OpenPdf(RutaEntradaArchivo);
                                            //ArchivoPdf.ToWord(RutaEntradaArchivoWord);


                                        }
                                    }
                                    //Convertir Archivos binarios PDF to PDf
                                    else if (mimetype == "application/pdf")
                                    {
                                        var Existe_Copia = System.IO.File.Exists(RutaEntradaArchivo);
                                        var Existe_Copia_word = System.IO.File.Exists(RutaEntradaArchivoWord);
                                        if (Existe_Copia == false)
                                        {
                                            //var E = System.IO.File.Exists(RutaEntradaArchivo.Replace(".pdf", ".bin"));
                                            //if (E == false)
                                            //{
                                            //    System.IO.File.Copy(ruta, RutaEntradaArchivo.Replace(".pdf", ".bin"), true);
                                            //}
                                            //FileStream fileStream = new FileStream(RutaEntradaArchivo.Replace(".pdf", ".bin"), FileMode.Open);
                                            //FileStream fileStream = new FileStream(ruta, FileMode.Open);
                                            StreamReader objLeerArchivo = new StreamReader(newDTODocumento.ArchivoBytes);
                                            byte[] data;
                                            using (objLeerArchivo)
                                            {
                                                using (MemoryStream ms = new MemoryStream())
                                                {
                                                    objLeerArchivo.BaseStream.CopyTo(ms);
                                                    data = ms.ToArray();
                                                }
                                            }
                                            System.IO.File.WriteAllBytes(RutaEntradaArchivo, data);
                                        }
                                        if (Existe_Copia_word == false)
                                        {

                                            SautinSoft.PdfFocus ArchivoPdf = new SautinSoft.PdfFocus();
                                            ArchivoPdf.OpenPdf(RutaEntradaArchivo);
                                            ArchivoPdf.ToWord(RutaEntradaArchivoWord);


                                        }

                                    }

                                    #endregion


                                    string RutaTemporal1 = Path.GetTempPath();

                                    string RutaTemporal2 = Environment.GetEnvironmentVariable("Temp");


                                    string enlaceFile = string.Format("<a href='#' onclick=verImagen('{0}'); title='ver archivo.'><img src='../Images/iconos/file.png' border=0></a>", RutaSalidaArchivo);

                                    string rutaWord = "";
                                    string enlaceWord = "";
                                    if (showFormatoWord)
                                    {
                                        var pathWord = string.Format("{0}{1}", RutaSalidaArchivo, newDTODocumento.Archivo);
                                        rutaWord = RutaSalidaArchivo.Replace(".pdf", ".docx");
                                        //rutaWord = pathWord.Replace(".pdf", ".docx");
                                        enlaceWord = string.Format(" &nbsp;<a href='#' onclick=verWord('{0}'); title='ver formato word.'><img src='../Images/iconos/word.png' border=0></a>", rutaWord);
                                        enlaceFile = string.Format("<a href='#' onclick=verImagen('{0}');  title='ver formato pdf.'><img src='../Images/iconos/pdf.png' border=0></a>", RutaSalidaArchivo);

                                    }

                                    shtml.Append("<tr class='k-grid-content'>");
                                    shtml.AppendFormat("<td >{0}</td>", newDTODocumento.codigo_alfresco);
                                    //shtml.AppendFormat("<td >{0}</td>", newDTODocumento.TipoDocumentoDesc);
                                    shtml.AppendFormat("<td >{0}</td>", newDTODocumento.FechaRecepcion.Substring(0, 10));
                                    shtml.AppendFormat("<td >{0} &nbsp; {1}</td>", enlaceFile, enlaceWord);
                                    shtml.AppendFormat("<td >{0}</td>", "ALFFRESCO");
                                    shtml.AppendFormat("<td >{0}</td>", archivoNombre);
                                    shtml.AppendFormat("<td >{0}</td>", newDTODocumento.UsuarioCrea);
                                    shtml.AppendFormat("<td >{0}</td>", newDTODocumento.FechaCrea);
                                    //shtml.AppendFormat("<td >{0}</td>", newDTODocumento.UsuarioModifica);
                                    shtml.AppendFormat("<td >{0}</td>", newDTODocumento.FechaModifica);
                                    shtml.AppendFormat("<td >{0}</td>", newDTODocumento.Activo ? "Activo" : "Inactivo");


                                    shtml.Append("</td>");
                                    shtml.Append("</tr>");
                                });
                            }
                        }
                    }


                    shtml.Append(" </table>");
                    retorno.message = shtml.ToString();
                    retorno.valor = QueryAlfresco + Convert.ToString(cantidad);
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.valor = QueryAlfresco + Convert.ToString(cantidad); ;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarDocumento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public JsonResult ActivarAlfresco()
        {
            Resultado retorno = new Resultado();
            try
            {
                var ActivarAlfresco = GlobalVars.Global.EnviarDocumento;
                retorno.message = ActivarAlfresco;
                retorno.valor = ActivarAlfresco;
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.valor = "0"; ;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        #region  Valida Usuario Moroso

        public JsonResult ValidaUsuarioMoroso(decimal BPS_ID)
        {
            Resultado retorno = new Resultado();
            try
            {


                retorno.result = new BLLicencias().ValidarUsuarioMoros(BPS_ID);
                retorno.message = Variables.MENSAJE_USUARIO_MOROSO;

            }
            catch (Exception ex)
            {
                retorno.result = Variables.ERROR;
                retorno.message = Variables.MENSAJE_ERROR_AL_VALIDAR_USUARIO_MOROSO;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SocioTelefCorreo

        public JsonResult ValidaLicenciaLocalRequerimiento(decimal CodigoLic, int COdigoReq)
        {
            Resultado retorno = new Resultado();
            try
            {


                retorno.result = new BLLicencias().ValidaLicenciaLocalRequerimiento(CodigoLic, COdigoReq);
                if (retorno.result != Variables.SI)
                    retorno.message = Variables.MENSAJE_LICENCIA_NO_CUMPLE_REQUISITOS;


            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = Variables.MENSAJE_ERROR_VALIDAR_LICENCIA_REQ;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public JsonResult ListaTipoInactivacionLicencia()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BLLicencias().ListarTipoInactivacionLicencia()
                     .Select(c => new BESelectListItem
                     {
                         Value = c.Valor,
                         Text = c.Texto

                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarTipoInactivacionLicencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarFacturacion(decimal LIC_ID)
        {
            Resultado retorno = new Resultado();
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLLicencias bl = new BLLicencias();
                    int result = bl.ValidarFacturacion(LIC_ID, Convert.ToDecimal(oficina));
                    if (result >= 1)
                    {
                        retorno.result = 1;
                        retorno.message = "Usted no cuenta con permisos para anular esta factura.";
                    }
                    else
                    {
                        retorno.result = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Validar_Anulacion_X_Modalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DescuentosMasivos() 
        {
            Init(false);
            return View();

        }
    }
}

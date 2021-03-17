using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;

namespace Proyect_Apdayc.Controllers
{
    public class ConsultaEstablecimientoController : Base
    {
        //
        // GET: /ConsultaEstablecimiento/
        public const string NomAplicacion = "SRGDA";

        #region VARIABLES
        private const string K_SESSION_CON_EST_DIRECCION = "___DTOConEstDirecciones";
        private const string K_SESSION_CON_EST_CARACTERISTICA = "___DTOConEstCaracteristica";
        private const string K_SESSION_CON_EST_PARAMETRO = "___DTOConEstParametro";
        private const string K_SESSION_CON_EST_ASOCIADO = "___DTOConEstAsociadoUD";
        private const string K_SESSION_CON_EST_LICENCIA = "___DTOConLicencia";
        private const string K_SESSION_CON_EST_OBSERVACION = "___DTOConEstObservacion";
        private const string K_SESSION_CON_EST_DOCUMENTO = "___DTOConEstDocumento";
        private const string K_SESSION_CON_EST_DIFUSION = "___DTOConEstDifusionUD";

        List<DTODireccion> direcciones = new List<DTODireccion>();
        List<DTOCaracteristica> caracteristicas = new List<DTOCaracteristica>();
        List<DTOParametro> parametros = new List<DTOParametro>();
        List<DTOAsociado> asociados = new List<DTOAsociado>();
        List<DTOLicencia> licencias = new List<DTOLicencia>();
        List<DTOObservacion> observaciones = new List<DTOObservacion>();
        List<DTODocumento> documentos = new List<DTODocumento>();
        List<DTODifusion> difusion = new List<DTODifusion>();

        public List<DTODireccion> DireccionesTmp
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESSION_CON_EST_DIRECCION];
            }
            set
            {
                Session[K_SESSION_CON_EST_DIRECCION] = value;
            }
        }
        public List<DTOCaracteristica> CaracteristicaTmp
        {
            get
            {
                return (List<DTOCaracteristica>)Session[K_SESSION_CON_EST_CARACTERISTICA];
            }
            set
            {
                Session[K_SESSION_CON_EST_CARACTERISTICA] = value;
            }
        }
        public List<DTOParametro> ParametrosTmp
        {
            get
            {
                return (List<DTOParametro>)Session[K_SESSION_CON_EST_PARAMETRO];
            }
            set
            {
                Session[K_SESSION_CON_EST_PARAMETRO] = value;
            }
        }
        public List<DTOAsociado> AsociadosTmp
        {
            get
            {
                return (List<DTOAsociado>)Session[K_SESSION_CON_EST_ASOCIADO];
            }
            set
            {
                Session[K_SESSION_CON_EST_ASOCIADO] = value;
            }
        }
        public List<DTOLicencia> LicenciasTmp
        {
            get
            {
                return (List<DTOLicencia>)Session[K_SESSION_CON_EST_LICENCIA];
            }
            set
            {
                Session[K_SESSION_CON_EST_LICENCIA] = value;
            }
        }
        public List<DTOObservacion> ObservacionesTmp
        {
            get
            {
                return (List<DTOObservacion>)Session[K_SESSION_CON_EST_OBSERVACION];
            }
            set
            {
                Session[K_SESSION_CON_EST_OBSERVACION] = value;
            }
        }
        public List<DTODocumento> DocumentosTmp
        {
            get
            {
                return (List<DTODocumento>)Session[K_SESSION_CON_EST_DOCUMENTO];
            }
            set
            {
                Session[K_SESSION_CON_EST_DOCUMENTO] = value;
            }
        }
        public List<DTODifusion> DifusionTmp
        {
            get
            {
                return (List<DTODifusion>)Session[K_SESSION_CON_EST_DIFUSION];
            }
            set
            {
                Session[K_SESSION_CON_EST_DIFUSION] = value;
            }
        }
        #endregion

        #region VISTAS
        public ActionResult Index()
        {
            Init(false);
            return View();
        }
        public ActionResult Ver()
        {
            Session.Remove(K_SESSION_CON_EST_DIRECCION);
            Session.Remove(K_SESSION_CON_EST_CARACTERISTICA);
            Session.Remove(K_SESSION_CON_EST_PARAMETRO);
            Session.Remove(K_SESSION_CON_EST_ASOCIADO);
            Session.Remove(K_SESSION_CON_EST_LICENCIA);
            Session.Remove(K_SESSION_CON_EST_OBSERVACION);
            Session.Remove(K_SESSION_CON_EST_DOCUMENTO);
            Session.Remove(K_SESSION_CON_EST_DIFUSION);
            Init(false);
            return View();
        }
        #endregion

        #region CONSULTA
        public JsonResult ListarConsulta(int skip, int take, int page, int pageSize, string group,
                                        decimal idEst, string nombre, decimal idSoc,
                                        decimal tipo, decimal subTipo, decimal idDivision,
                                        decimal ubigeo)
        {
            Resultado retorno = new Resultado();
            var lista = new BLEstablecimiento().ConsultaEstablecimiento(GlobalVars.Global.OWNER, idEst, nombre,
                                        idSoc, tipo, subTipo, idDivision, ubigeo, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEEstablecimiento { Establecimiento = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEEstablecimiento { Establecimiento = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region VER
        public JsonResult ObtenerEstablecimiento(decimal idEst)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var establecimiento = new BLEstablecimiento().CabeceraConsultaEst(idEst, GlobalVars.Global.OWNER, Constantes.ENTIDAD.OTROS);
                    if (establecimiento != null)
                    {
                        #region direccion
                        if (establecimiento.Direccion != null)
                        {
                            direcciones = new List<DTODireccion>();
                            establecimiento.Direccion.ForEach(s =>
                            {
                                direcciones.Add(new DTODireccion
                                {
                                    Id = s.ADD_ID,
                                    TipoDireccion = Convert.ToString(s.ADD_TYPE),
                                    Territorio = Convert.ToString(s.TIS_N),
                                    RazonSocial = s.ADDRESS,
                                    Lote = s.HOU_LOT,
                                    Manzana = s.HOU_MZ,
                                    Numero = Convert.ToString(s.HOU_NRO),
                                    CodigoUbigeo = Convert.ToString(s.GEO_ID),
                                    Referencia = s.ADD_REFER,
                                    TipoAvenida = Convert.ToString(s.ROU_ID),
                                    Avenida = s.ROU_NAME,
                                    TipoEtapa = s.HOU_TETP,
                                    Etapa = s.HOU_NETP,
                                    TipoUrb = s.HOU_TURZN,
                                    Urbanizacion = s.HOU_URZN,
                                    CodigoPostal = Convert.ToString(s.CPO_ID),
                                    EsPrincipal = Convert.ToString(s.MAIN_ADD),
                                    TipoDireccionDesc = new BLREF_ADDRESS_TYPE().Obtiene(GlobalVars.Global.OWNER, s.ADD_TYPE).DESCRIPTION,
                                    EnBD = true,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    FechaCrea = s.LOG_DATE_CREAT,
                                    UsuarioModifica = s.LOG_USER_UPDATE,
                                    FechaModifica = s.LOG_DATE_UPDATE,
                                    Activo = s.ENDS.HasValue ? false : true,
                                    DescripcionUbigeo = new BLUbigeo().ObtenerDescripcion(s.TIS_N, s.GEO_ID).NOMBRE_UBIGEO
                                });
                            });
                            DireccionesTmp = direcciones;
                        }
                        #endregion
                        #region caracteristica
                        if (establecimiento.Caracteristicas != null)
                        {
                            caracteristicas = new List<DTOCaracteristica>();
                            establecimiento.Caracteristicas.ForEach(s =>
                            {
                                caracteristicas.Add(new DTOCaracteristica
                                {
                                    Id = s.CHAR_ID,
                                    caracteristica = s.CHAR_LONG,
                                    Idcaracteristica = s.CHAR_ID.ToString(),
                                    IdEstablecimiento = Convert.ToString(s.EST_ID),
                                    TipoEstablecimiento = Convert.ToString(s.ESTT_ID),
                                    IdSubTipoEstablecimiento = Convert.ToString(s.SUBE_ID),
                                    Valor = s.VALUE.ToString(),
                                    usercreate = UsuarioActual,
                                    EnBD = true,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    FechaCrea = s.LOG_DATE_CREAT,
                                    UsuarioModifica = s.LOG_USER_UPDAT,
                                    FechaModifica = s.LOG_DATE_UPDATE,
                                    Activo = s.ENDS.HasValue ? false : true
                                });
                            });
                            CaracteristicaTmp = caracteristicas;
                        }
                        #endregion
                        #region parametro
                        if (establecimiento.Parametros != null)
                        {
                            parametros = new List<DTOParametro>();
                            establecimiento.Parametros.ForEach(s =>
                            {
                                parametros.Add(new DTOParametro
                                {
                                    Id = (s.PAR_ID),
                                    Descripcion = s.PAR_VALUE,
                                    TipoParametro = Convert.ToString(s.PAR_TYPE),
                                    TipoParametroDesc = new BLTipoParametro().Obtener(GlobalVars.Global.OWNER, s.PAR_TYPE).PAR_DESC,
                                    EnBD = true,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    FechaCrea = s.LOG_DATE_CREAT,
                                    UsuarioModifica = s.LOG_USER_UPDATE,
                                    FechaModifica = s.LOG_DATE_UPDATE,
                                    Activo = s.ENDS.HasValue ? false : true
                                });
                            });
                            ParametrosTmp = parametros;
                        }
                        #endregion
                        #region asociado
                        if (establecimiento.Asociados != null)
                        {
                            asociados = new List<DTOAsociado>();
                            foreach (var s in establecimiento.Asociados)
                            {
                                var obj = new DTOAsociado();
                                obj.Id = s.Id;
                                obj.RolTipo = Convert.ToInt32(s.ROL_ID);
                                obj.RolTipoDesc = s.ROL_DESC;
                                obj.CodigoAsociado = s.BPS_ID;
                                obj.EnBD = true;
                                obj.UsuarioCrea = s.LOG_USER_CREAT;
                                obj.FechaCrea = s.LOG_DATE_CREAT;
                                obj.UsuarioModifica = s.LOG_USER_UPDATE;
                                obj.FechaModifica = s.LOG_DATE_UPDATE;
                                obj.EsContacto = s.BPS_MAIN == false ? "0" : "1";
                                obj.Activo = s.ENDS.HasValue ? false : true;

                                var usu = new BLSocioNegocio().ObtenerDatos(s.BPS_ID, GlobalVars.Global.OWNER);
                                if (usu != null)
                                {
                                    obj.NombreAsociado = usu.BPS_NAME;
                                    obj.NroDocAsociado = usu.TAX_ID;
                                }

                                asociados.Add(obj);
                            }
                            AsociadosTmp = asociados;
                        }
                        #endregion
                        #region observacion
                        if (establecimiento.Observaciones != null)
                        {
                            observaciones = new List<DTOObservacion>();
                            establecimiento.Observaciones.ForEach(s =>
                            {
                                observaciones.Add(new DTOObservacion
                                {
                                    Id = s.OBS_ID,
                                    Observacion = s.OBS_VALUE,
                                    TipoObservacion = Convert.ToString(s.OBS_TYPE),
                                    TipoObservacionDesc = new BLTipoObservacion().Obtener(GlobalVars.Global.OWNER, s.OBS_TYPE).OBS_DESC,
                                    EnBD = true,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    FechaCrea = s.LOG_DATE_CREAT,
                                    UsuarioModifica = s.LOG_USER_UPDATE,
                                    FechaModifica = s.LOG_DATE_UPDATE,
                                    Activo = s.ENDS.HasValue ? false : true
                                });
                            });
                            ObservacionesTmp = observaciones;
                        }
                        #endregion
                        #region difusion
                        if (establecimiento.Difusion != null)
                        {
                            difusion = new List<DTODifusion>();
                            establecimiento.Difusion.ForEach(s =>
                            {
                                difusion.Add(new DTODifusion
                                {
                                    Id = s.SEQUENCE,
                                    idEstablecimiento = s.EST_ID,
                                    idDifusion = s.BROAD_ID,
                                    Difusion = s.BROAD_DESC,
                                    NroDifusion = s.BROADE_NUM,
                                    almacenamiento = s.BROADE_STORAGE == "1" ? true : false,
                                    EnBD = true,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    FechaCrea = s.LOG_DATE_CREAT,
                                    UsuarioModifica = s.LOG_USER_UPDAT,
                                    FechaModifica = s.LOG_DATE_UPDATE,
                                    Activo = s.ENDS.HasValue ? false : true,
                                });
                            });
                            DifusionTmp = difusion;
                        }
                        #endregion
                        #region documento
                        if (establecimiento.Documentos != null)
                        {
                            documentos = new List<DTODocumento>();
                            establecimiento.Documentos.ForEach(s =>
                            {
                                var newDTODocumento = new DTODocumento();
                                newDTODocumento.Id = s.DOC_ID;
                                newDTODocumento.Archivo = s.DOC_PATH;
                                newDTODocumento.TipoDocumento = Convert.ToString(s.DOC_TYPE);
                                newDTODocumento.TipoDocumentoDesc = new BLREC_DOCUMENT_TYPE().Obtener(GlobalVars.Global.OWNER, s.DOC_TYPE).DOC_DESC;
                                newDTODocumento.FechaRecepcion = s.DOC_DATE.ToShortDateString();
                                newDTODocumento.EnBD = true;
                                newDTODocumento.UsuarioCrea = s.LOG_USER_CREAT;
                                newDTODocumento.FechaCrea = s.LOG_DATE_CREAT;
                                newDTODocumento.UsuarioModifica = s.LOG_USER_UPDATE;
                                newDTODocumento.FechaModifica = s.LOG_DATE_UPDATE;
                                newDTODocumento.Activo = s.ENDS.HasValue ? false : true;
                                documentos.Add(newDTODocumento);
                            });
                            DocumentosTmp = documentos;
                        }
                        #endregion
                        #region licencia
                        if (establecimiento.Licencias != null)
                        {
                            licencias = new List<DTOLicencia>();
                            establecimiento.Licencias.ForEach(s =>
                            {
                                var newDTOLicencia = new DTOLicencia();
                                newDTOLicencia.codLicencia = s.LIC_ID;
                                newDTOLicencia.nombreLicencia = s.LIC_NAME;
                                newDTOLicencia.Moneda = s.MONEDA;
                                newDTOLicencia.TipoPago = s.TIPOPAGO;
                                newDTOLicencia.GrupoFacturacionDes = s.INVG_DESC.ToUpper();
                                newDTOLicencia.UsuarioCrea = s.LOG_USER_CREAT;
                                newDTOLicencia.UsuarioModifica = s.LOG_USER_UPDAT;
                                newDTOLicencia.FechaCrea = s.LOG_DATE_CREAT;
                                newDTOLicencia.FechaModifica = s.LOG_DATE_UPDATE;
                                licencias.Add(newDTOLicencia);
                            });
                            LicenciasTmp = licencias;
                        }
                        #endregion
                    }

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(establecimiento, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtenerEstablecimiento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region LISTAR
        public JsonResult ListarDireccion()
        {
            direcciones = DireccionesTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Tipo Direccion</th>");
                    shtml.Append("<th class='k-header'>Nombre / Razon Social</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Principal</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("</tr></thead>");
                    if (direcciones != null)
                    {
                        string strChecked = "";
                        foreach (var item in direcciones.OrderByDescending(x => x.EsPrincipal))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.TipoDireccionDesc);
                            shtml.AppendFormat("<td >{0}</td>", item.RazonSocial);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            if (item.EsPrincipal == "1")
                                strChecked = " checked='checked'";
                            else
                                strChecked = "";
                            shtml.AppendFormat("<td style='width:80px;text-align:center;'><input type='radio' class='radioDir' onclick='actualizarDirPrincipal({0});' name='rdbtnDir' id='rbtn_{0}' {1} disabled readonly/></td>", item.Id, strChecked);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarDireccion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarCaracteristica()
        {
            caracteristicas = CaracteristicaTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='k-header'>Id</th>");
                    shtml.Append("<th class='k-header'>Característica</th>");
                    shtml.Append("<th class='k-header'>Valor</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("</tr></thead>");

                    if (caracteristicas != null)
                    {
                        foreach (var item in caracteristicas.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.caracteristica);
                            shtml.AppendFormat("<td >{0}</td>", item.Valor);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarCaracteristica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarParametro()
        {
            parametros = ParametrosTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Tipo Parámetro</th>");
                    shtml.Append("<th class='k-header'>Descripción</th><th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("</thead>");

                    if (parametros != null)
                    {
                        foreach (var item in parametros.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.TipoParametroDesc);
                            shtml.AppendFormat("<td >{0}</td>", item.Descripcion);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarParametro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarAsociado()
        {
            asociados = AsociadosTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tbAsociado' border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='k-header' >Id</th>");
                    shtml.Append("<th class='k-header'>Nombre del Asociado</th>");
                    shtml.Append("<th class='k-header'>Rol</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("</tr></thead>");
                    if (asociados != null)
                    {
                        foreach (var item in asociados.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.NombreAsociado);
                            shtml.AppendFormat("<td style='text-align:center'>{0}</td>", item.RolTipoDesc);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");                         
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarAsociado", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarObservacion()
        {
            observaciones = ObservacionesTmp;
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Tipo Observación</th>");
                    shtml.Append("<th class='k-header'>Observación</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("</tr></thead>");

                    if (observaciones != null)
                    {
                        foreach (var item in observaciones.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.TipoObservacionDesc);
                            shtml.AppendFormat("<td >{0}</td>", item.Observacion);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarObservacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarDifusion()
        {
            difusion = DifusionTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th>");
                    shtml.Append("<th  class='k-header'>Medio de difusión</th>");
                    shtml.Append("<th  class='k-header'>Numero de difusión</th>");
                    shtml.Append("<th  class='k-header'>Almacenamiento</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("</tr></thead>");
                    if (difusion != null)
                    {
                        foreach (var item in difusion.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.Difusion);
                            shtml.AppendFormat("<td >{0}</td>", item.NroDifusion);
                            shtml.AppendFormat("<td >{0}</td>", item.almacenamientoDes = item.almacenamiento == true ? "Si" : "No");
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarDocumento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarDocumento()
        {
            documentos = DocumentosTmp;
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='k-header'>Id</th>");
                    shtml.Append("<th class='k-header'>Tipo Documento</th>");
                    shtml.Append("<th class='k-header'>Fecha Recepción</th>");
                    shtml.Append("<th class='k-header'>Archivo</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("</tr></thead>");
                    if (documentos != null)
                    {
                        foreach (var item in documentos.OrderBy(x => x.Id))
                        {
                            var pathWeb = System.Web.Configuration.WebConfigurationManager.AppSettings["RutaWebImgEstablecimiento"];
                            var ruta = string.Format("{0}{1}", pathWeb, item.Archivo);
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.TipoDocumentoDesc);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaRecepcion.Substring(0, 10));
                            shtml.AppendFormat("<td ><a href='#' onclick=verImagen('{0}');>Ver Documento</a></td>", ruta);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            shtml.Append("</tr>");
                        }
                    }
                    shtml.Append(" </table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarDocumento", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarLicencia()
        {
            licencias = LicenciasTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'><thead><tr>");
                    shtml.Append("<th class='k-header'>Id</th>");
                    shtml.Append("<th class='k-header'>Licencia</th>");
                    shtml.Append("<th class='k-header'>Moneda</th>");
                    shtml.Append("<th class='k-header'>Tipo Pago</th>");
                    shtml.Append("<th class='k-header'>Grupo Modalidad</th>"); 
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("</tr></thead>");
                    if (caracteristicas != null)
                    {
                        foreach (var item in licencias.OrderBy(x => x.codLicencia))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.codLicencia);
                            shtml.AppendFormat("<td >{0}</td>", item.nombreLicencia);
                            shtml.AppendFormat("<td >{0}</td>", item.Moneda);
                            shtml.AppendFormat("<td >{0}</td>", item.TipoPago);
                            shtml.AppendFormat("<td >{0}</td>", item.GrupoFacturacionDes);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarLicencia", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}

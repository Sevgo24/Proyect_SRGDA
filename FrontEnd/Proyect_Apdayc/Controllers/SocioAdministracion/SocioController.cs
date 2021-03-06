using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;
using System.IO;
using System.Net;
using System.Web;


namespace Proyect_Apdayc.Controllers.SocioAdministracion
{
    public class SocioController : Base
    {

        #region varialbles log
        const string NomAplicacion = "SGRDA";
        #endregion

        // GET: SocioAdministracion

        List<DTOSocio> SocioNegocioList = new List<DTOSocio>();
        List<DTOSocio> listar = new List<DTOSocio>();
        List<DTOSocio> listarSocioSeg = new List<DTOSocio>();

        List<DTODireccion> direcciones = new List<DTODireccion>();
        List<DTOObservacion> observaciones = new List<DTOObservacion>();
        List<DTODocumento> documentos = new List<DTODocumento>();
        List<DTOParametro> parametros = new List<DTOParametro>();

        List<DTOTelefono> telefonos = new List<DTOTelefono>();
        List<DTOCorreo> correos = new List<DTOCorreo>();
        List<DTORedSocial> redsocial = new List<DTORedSocial>();

        #region Sesiones

        public static string K_SESION_SOCIO_CONSULTA_SOC = "__DTOSocioTmp";
        public static string K_SESION_SOCIO_CONSULTA_SOC_SEG = "__DTOSocioSegTmp";


        private const string K_SESION_DIRECCION = "___DTODirecciones";
        private const string K_SESION_DIRECCION_DEL = "___DTODireccionesDEL";
        private const string K_SESION_DIRECCION_ACT = "___DTODireccionesACT";

        private const string K_SESSION_OBSERVACION = "___DTOObservacion";
        private const string K_SESSION_OBSERVACION_DEL = "___DTOObservacionDEL";
        private const string K_SESSION_OBSERVACION_ACT = "___DTOObservacionACT";

        private const string K_SESSION_DOCUMENTO = "___DTODocumento";
        private const string K_SESSION_DOCUMENTO_DEL = "___DTODocumentoDEL";
        private const string K_SESSION_DOCUMENTO_ACT = "___DTODocumentoACT";

        private const string K_SESSION_PARAMETRO = "___DTOParametro";
        private const string K_SESSION_PARAMETRO_DEL = "___DTOParametroDEL";
        private const string K_SESSION_PARAMETRO_ACT = "___DTOParametroACT";

        private const string K_SESSION_CORREO = "___DTOCorreoUD";
        private const string K_SESSION_CORREO_DEL = "___DTOCorreoDELUD";
        private const string K_SESSION_CORREO_ACT = "___DTOCorreoACTUD";

        private const string K_SESSION_TELEFONO = "___DTOTelefonoUD";
        private const string K_SESSION_TELEFONO_DEL = "___DTOTelefonoDELUD";
        private const string K_SESSION_TELEFONO_ACT = "___DTOTelefonoACTUD";

        private const string K_SESSION_REDSOCIAL = "___DTORedSocialUD";
        private const string K_SESSION_REDSOCIAL_DEL = "___DTORedSocialDELUD";
        private const string K_SESSION_REDSOCIAL_ACT = "___DTORedSocialACTUD";
        //socio
        public static string K_SESSION_DESCUENTO_SOCIO = "__DTODescuentoSocio";
        public static string K_SESSION_DESCUENTO_SOCIOCOMP = "__DTODescuentoSocioComp";
        #endregion


        #region Temporales

        public List<DTOSocio> SocioDestinoTmp
        {
            get
            {
                return (List<DTOSocio>)Session[K_SESION_SOCIO_CONSULTA_SOC_SEG];
            }
            set
            {
                Session[K_SESION_SOCIO_CONSULTA_SOC_SEG] = value;
            }
        }

        public List<DTOSocio> SocioConsultaTmp
        {
            get
            {
                return (List<DTOSocio>)Session[K_SESION_SOCIO_CONSULTA_SOC];
            }
            set
            {
                Session[K_SESION_SOCIO_CONSULTA_SOC] = value;
            }
        }

        private List<DTODireccion> DireccionesTmpUPDEstado
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION_ACT];
            }
            set
            {
                Session[K_SESION_DIRECCION_ACT] = value;
            }
        }
        private List<DTODireccion> DireccionesTmpDelBD
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION_DEL];
            }
            set
            {
                Session[K_SESION_DIRECCION_DEL] = value;
            }
        }
        public List<DTODireccion> DireccionesTmp
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION];
            }
            set
            {
                Session[K_SESION_DIRECCION] = value;
            }
        }


        private List<DTOObservacion> ObservacionesTmpUPDEstado
        {
            get
            {
                return (List<DTOObservacion>)Session[K_SESSION_OBSERVACION_ACT];
            }
            set
            {
                Session[K_SESSION_OBSERVACION_ACT] = value;
            }
        }
        private List<DTOObservacion> ObservacionesTmpDelBD
        {
            get
            {
                return (List<DTOObservacion>)Session[K_SESSION_OBSERVACION_DEL];
            }
            set
            {
                Session[K_SESSION_OBSERVACION_DEL] = value;
            }
        }
        public List<DTOObservacion> ObservacionesTmp
        {
            get
            {
                return (List<DTOObservacion>)Session[K_SESSION_OBSERVACION];
            }
            set
            {
                Session[K_SESSION_OBSERVACION] = value;
            }
        }


        private List<DTODocumento> DocumentosTmpUPDEstado
        {
            get
            {
                return (List<DTODocumento>)Session[K_SESSION_DOCUMENTO_ACT];
            }
            set
            {
                Session[K_SESSION_DOCUMENTO_ACT] = value;
            }
        }
        private List<DTODocumento> DocumentosTmpDelBD
        {
            get
            {
                return (List<DTODocumento>)Session[K_SESSION_DOCUMENTO_DEL];
            }
            set
            {
                Session[K_SESSION_DOCUMENTO_DEL] = value;
            }
        }
        public List<DTODocumento> DocumentosTmp
        {
            get
            {
                return (List<DTODocumento>)Session[K_SESSION_DOCUMENTO];
            }
            set
            {
                Session[K_SESSION_DOCUMENTO] = value;
            }
        }



        private List<DTOParametro> ParametrosTmpUPDEstado
        {
            get
            {
                return (List<DTOParametro>)Session[K_SESSION_PARAMETRO_ACT];
            }
            set
            {
                Session[K_SESSION_PARAMETRO_ACT] = value;
            }
        }
        private List<DTOParametro> ParametrosTmpDelBD
        {
            get
            {
                return (List<DTOParametro>)Session[K_SESSION_PARAMETRO_DEL];
            }
            set
            {
                Session[K_SESSION_PARAMETRO_DEL] = value;
            }
        }
        public List<DTOParametro> ParametrosTmp
        {
            get
            {
                return (List<DTOParametro>)Session[K_SESSION_PARAMETRO];
            }
            set
            {
                Session[K_SESSION_PARAMETRO] = value;
            }
        }


        private List<DTOTelefono> TelefonosTmpUPDEstado
        {
            get
            {
                return (List<DTOTelefono>)Session[K_SESSION_TELEFONO_ACT];
            }
            set
            {
                Session[K_SESSION_TELEFONO_ACT] = value;
            }
        }
        private List<DTOTelefono> TelefonosTmpDelBD
        {
            get
            {
                return (List<DTOTelefono>)Session[K_SESSION_TELEFONO_DEL];
            }
            set
            {
                Session[K_SESSION_TELEFONO_DEL] = value;
            }
        }
        public List<DTOTelefono> TelefonosTmp
        {
            get
            {
                return (List<DTOTelefono>)Session[K_SESSION_TELEFONO];
            }
            set
            {
                Session[K_SESSION_TELEFONO] = value;
            }
        }


        private List<DTOCorreo> CorreosTmpUPDEstado
        {
            get
            {
                return (List<DTOCorreo>)Session[K_SESSION_CORREO_ACT];
            }
            set
            {
                Session[K_SESSION_CORREO_ACT] = value;
            }
        }
        private List<DTOCorreo> CorreosTmpDelBD
        {
            get
            {
                return (List<DTOCorreo>)Session[K_SESSION_CORREO_DEL];
            }
            set
            {
                Session[K_SESSION_CORREO_DEL] = value;
            }
        }
        public List<DTOCorreo> CorreosTmp
        {
            get
            {
                return (List<DTOCorreo>)Session[K_SESSION_CORREO];
            }
            set
            {
                Session[K_SESSION_CORREO] = value;
            }
        }

        private List<DTORedSocial> RedSocialTmpUPDEstado
        {
            get
            {
                return (List<DTORedSocial>)Session[K_SESSION_REDSOCIAL_ACT];
            }
            set
            {
                Session[K_SESSION_REDSOCIAL_ACT] = value;
            }
        }
        private List<DTORedSocial> RedSocialTmpDelBD
        {
            get
            {
                return (List<DTORedSocial>)Session[K_SESSION_REDSOCIAL_DEL];
            }
            set
            {
                Session[K_SESSION_REDSOCIAL_DEL] = value;
            }
        }
        public List<DTORedSocial> RedSocialTmp
        {
            get
            {
                return (List<DTORedSocial>)Session[K_SESSION_REDSOCIAL];
            }
            set
            {
                Session[K_SESSION_REDSOCIAL] = value;
            }
        }

        #endregion

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESION_DIRECCION_ACT);
            Session.Remove(K_SESION_DIRECCION_DEL);
            Session.Remove(K_SESION_DIRECCION);

            Session.Remove(K_SESSION_OBSERVACION);
            Session.Remove(K_SESSION_OBSERVACION_ACT);
            Session.Remove(K_SESSION_OBSERVACION_DEL);


            Session.Remove(K_SESSION_DOCUMENTO);
            Session.Remove(K_SESSION_DOCUMENTO_ACT);
            Session.Remove(K_SESSION_DOCUMENTO_DEL);

            Session.Remove(K_SESSION_PARAMETRO);
            Session.Remove(K_SESSION_PARAMETRO_ACT);
            Session.Remove(K_SESSION_PARAMETRO_DEL);

            Session.Remove(K_SESSION_CORREO);
            Session.Remove(K_SESSION_CORREO_ACT);
            Session.Remove(K_SESSION_CORREO_DEL);

            Session.Remove(K_SESSION_TELEFONO);
            Session.Remove(K_SESSION_TELEFONO_ACT);
            Session.Remove(K_SESSION_TELEFONO_DEL);

            Session.Remove(K_SESSION_REDSOCIAL);
            Session.Remove(K_SESSION_REDSOCIAL_ACT);
            Session.Remove(K_SESSION_REDSOCIAL_DEL);
            Session.Remove(K_SESSION_DESCUENTO_SOCIO);
            Session.Remove(K_SESSION_DESCUENTO_SOCIOCOMP);

            ViewBag.DesEmpresa = System.Web.Configuration.WebConfigurationManager.AppSettings["NombreEmpresa"];

            return View();
        }

        public JsonResult USP_SOCIOS_LISTARPAGEJSON(int skip, int take, int page, int pageSize, string group, decimal tipo, string nro_tipo, string nombre, Boolean derecho, Boolean asociacion, Boolean grupo, Boolean recaudador, Boolean proveedor, Boolean empleado, int estado)
        {
            Resultado retorno = new Resultado();
            var flt = "0";
            //if (filtro) flt = "0";
            if (!derecho && !asociacion && !grupo && !recaudador && !proveedor && !empleado) flt = "0";
            else flt = "2";


            var BPS_USER = flt;
            var BPS_ASSOCIATION = flt;
            var BPS_GROUP = flt;
            var BPS_COLLECTOR = flt;
            var BPS_SUPPLIER = flt;
            var BPS_EMPLOYEE = flt;

            if (derecho) BPS_USER = "1";
            if (asociacion) BPS_ASSOCIATION = "1";
            if (grupo) BPS_GROUP = "1";
            if (recaudador) BPS_COLLECTOR = "1";
            if (proveedor) BPS_SUPPLIER = "1";
            if (empleado) BPS_EMPLOYEE = "1";

            var lista = USP_SOCIOS_LISTARPAGE(tipo, nro_tipo, nombre, page, pageSize, BPS_USER, BPS_ASSOCIATION, BPS_GROUP, BPS_COLLECTOR, BPS_SUPPLIER, BPS_EMPLOYEE, estado);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new SocioNegocio { Socio_Negocio = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new SocioNegocio { Socio_Negocio = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<SocioNegocio> USP_SOCIOS_LISTARPAGE(decimal tipo, string nro_tipo, string nombre, int pagina, int cantRegxPag, string BPS_USER, string BPS_ASSOCIATION, string BPS_GROUP, string BPS_COLLECTOR, string BPS_SUPPLIER, string BPS_EMPLOYEE, int estado)
        {
            return new BLSocioNegocio().USP_SOCIOS_LISTARPAGE(tipo, nro_tipo, nombre, pagina, cantRegxPag, BPS_USER, BPS_ASSOCIATION, BPS_GROUP, BPS_COLLECTOR, BPS_SUPPLIER, BPS_EMPLOYEE, estado);
        }

        [HttpPost]
        public JsonResult AddDireccion(DTODireccion direccion)
        {
            Resultado retorno = new Resultado();
            try
            {
                direcciones = DireccionesTmp;
                direccion.RazonSocial = getRazonSocial(direccion);
                if (direcciones == null) direcciones = new List<DTODireccion>();

                if (Convert.ToInt32(direccion.Id) <= 0)
                {
                    decimal nuevoId = 1;
                    if (direcciones.Count > 0) nuevoId = direcciones.Max(x => x.Id) + 1;
                    direccion.Id = nuevoId;
                    direccion.Activo = true;
                    direccion.EnBD = false;
                    direccion.EsPrincipal = direcciones.Count == 0 ? "1" : "0";
                    direccion.UsuarioCrea = UsuarioActual;
                    direccion.FechaCrea = DateTime.Now;
                    direcciones.Add(direccion);
                }
                else
                {

                    var item = direcciones.Where(x => x.Id == direccion.Id).FirstOrDefault();
                    direccion.EnBD = item.EnBD;//indicador que item viene de la BD
                    direccion.Activo = item.Activo;
                    direccion.EsPrincipal = "0";
                    direccion.UsuarioCrea = item.UsuarioCrea;
                    direccion.FechaCrea = item.FechaCrea;
                    if (direccion.EnBD)
                    {
                        direccion.UsuarioModifica = UsuarioActual;
                        direccion.FechaModifica = DateTime.Now;
                    }
                    direcciones.Remove(item);
                    direcciones.Add(direccion);
                }

                DireccionesTmp = direcciones;
                retorno.result = 1;
                retorno.message = "OK";


            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "add Diteccion", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddObservacion(DTOObservacion entidad)
        {
            Resultado retorno = new Resultado();
            try
            {

                observaciones = ObservacionesTmp;
                if (observaciones == null) observaciones = new List<DTOObservacion>();
                if (Convert.ToInt32(entidad.Id) <= 0)
                {
                    decimal nuevoId = 1;
                    if (observaciones.Count > 0) nuevoId = observaciones.Max(x => x.Id) + 1;
                    entidad.Id = nuevoId;
                    entidad.Activo = true;
                    entidad.EnBD = false;
                    entidad.UsuarioCrea = UsuarioActual;
                    entidad.FechaCrea = DateTime.Now;
                    observaciones.Add(entidad);
                }
                else
                {
                    var item = observaciones.Where(x => x.Id == entidad.Id).FirstOrDefault();
                    entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                    entidad.Activo = item.Activo;
                    entidad.UsuarioCrea = item.UsuarioCrea;
                    entidad.FechaCrea = item.FechaCrea;
                    if (entidad.EnBD)
                    {
                        entidad.UsuarioModifica = UsuarioActual;
                        entidad.FechaModifica = DateTime.Now;
                    }
                    observaciones.Remove(item);
                    observaciones.Add(entidad);
                }
                ObservacionesTmp = observaciones;
                retorno.result = 1;
                retorno.message = "OK";
            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult AddTelefono(DTOTelefono entidad)
        {
            Resultado retorno = new Resultado();
            try
            {

                telefonos = TelefonosTmp;
                if (telefonos == null) telefonos = new List<DTOTelefono>();
                if (Convert.ToInt32(entidad.Id) <= 0)
                {
                    decimal nuevoId = 1;
                    if (telefonos.Count > 0) nuevoId = telefonos.Max(x => x.Id) + 1;
                    entidad.Id = nuevoId;
                    entidad.Activo = true;
                    entidad.EnBD = false;
                    entidad.UsuarioCrea = UsuarioActual;
                    entidad.FechaCrea = DateTime.Now;
                    telefonos.Add(entidad);
                }
                else
                {
                    var item = telefonos.Where(x => x.Id == entidad.Id).FirstOrDefault();
                    entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                    entidad.Activo = item.Activo;
                    entidad.UsuarioCrea = item.UsuarioCrea;
                    entidad.FechaCrea = item.FechaCrea;
                    if (entidad.EnBD)
                    {
                        entidad.UsuarioModifica = UsuarioActual;
                        entidad.FechaModifica = DateTime.Now;
                    }
                    telefonos.Remove(item);
                    telefonos.Add(entidad);
                }
                TelefonosTmp = telefonos;
                retorno.result = 1;
                retorno.message = "OK";
            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult AddCorreo(DTOCorreo entidad)
        {
            Resultado retorno = new Resultado();
            try
            {

                correos = CorreosTmp;
                if (correos == null) correos = new List<DTOCorreo>();
                if (Convert.ToInt32(entidad.Id) <= 0)
                {
                    decimal nuevoId = 1;
                    if (correos.Count > 0) nuevoId = correos.Max(x => x.Id) + 1;
                    entidad.Id = nuevoId;
                    entidad.Activo = true;
                    entidad.EnBD = false;
                    entidad.UsuarioCrea = UsuarioActual;
                    entidad.FechaCrea = DateTime.Now;
                    correos.Add(entidad);
                }
                else
                {
                    var item = correos.Where(x => x.Id == entidad.Id).FirstOrDefault();
                    entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                    entidad.Activo = item.Activo;
                    entidad.UsuarioCrea = item.UsuarioCrea;
                    entidad.FechaCrea = item.FechaCrea;
                    if (entidad.EnBD)
                    {
                        entidad.UsuarioModifica = UsuarioActual;
                        entidad.FechaModifica = DateTime.Now;
                    }
                    correos.Remove(item);
                    correos.Add(entidad);
                }
                CorreosTmp = correos;
                retorno.result = 1;
                retorno.message = "OK";
            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult AddDocumento(DTODocumento documento)
        {
            Resultado retorno = new Resultado();
            int codigo = 0;
            try
            {
                if (!isLogout(ref retorno))
                {
                    documentos = DocumentosTmp;
                    if (documentos == null) documentos = new List<DTODocumento>();

                    // if (Convert.ToInt32(documento.Id) <= 0) documento.Id = Convert.ToString(documentos.Count+1);
                    if (Convert.ToInt32(documento.Id) <= 0)
                    {
                        decimal nuevoId = 1;
                        if (documentos.Count > 0) nuevoId = documentos.Max(x => x.Id) + 1;
                        documento.Id = nuevoId;
                        documento.Activo = true;
                        documento.EnBD = false;
                        documento.UsuarioCrea = UsuarioActual;
                        documento.FechaCrea = DateTime.Now;
                        documento.Archivo = string.Empty;
                        documentos.Add(documento);
                        codigo = Convert.ToInt32(nuevoId);
                    }
                    else
                    {
                        var item = documentos.Where(x => x.Id == documento.Id).FirstOrDefault();
                        documento.EnBD = item.EnBD;//indicador que item viene de la BD
                        documento.Activo = item.Activo;
                        documento.Archivo = item.Archivo;
                        documento.UsuarioCrea = item.UsuarioCrea;
                        documento.FechaCrea = item.FechaCrea;
                        if (documento.EnBD)
                        {
                            documento.UsuarioModifica = UsuarioActual;
                            documento.FechaModifica = DateTime.Now;
                        }
                        documentos.Remove(item);
                        documentos.Add(documento);
                        codigo = Convert.ToInt32(documento.Id);
                    }
                    DocumentosTmp = documentos;

                    retorno.result = 1;
                    retorno.Code = codigo;
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
        [HttpPost]
        public JsonResult AddParametro(DTOParametro entidad)
        {
            Resultado retorno = new Resultado();
            try
            {

                parametros = ParametrosTmp;
                if (parametros == null) parametros = new List<DTOParametro>();
                if (Convert.ToInt32(entidad.Id) <= 0)
                {
                    decimal nuevoId = 1;
                    if (parametros.Count > 0) nuevoId = parametros.Max(x => x.Id) + 1;
                    entidad.Id = nuevoId;
                    entidad.Activo = true;
                    entidad.EnBD = false;
                    entidad.UsuarioCrea = UsuarioActual;
                    entidad.FechaCrea = DateTime.Now;
                    parametros.Add(entidad);
                }
                else
                {
                    var item = parametros.Where(x => x.Id == entidad.Id).FirstOrDefault();
                    entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                    entidad.Activo = item.Activo;
                    entidad.UsuarioCrea = item.UsuarioCrea;
                    entidad.FechaCrea = item.FechaCrea;
                    if (entidad.EnBD)
                    {
                        entidad.UsuarioModifica = UsuarioActual;
                        entidad.FechaModifica = DateTime.Now;
                    }
                    parametros.Remove(item);
                    parametros.Add(entidad);
                }
                ParametrosTmp = parametros;
                retorno.result = 1;
                retorno.message = "OK";
            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult AddRedes(DTORedSocial entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    redsocial = RedSocialTmp;

                    if (redsocial == null) redsocial = new List<DTORedSocial>();
                    if (Convert.ToInt32(entidad.Id) <= 0)
                    {
                        decimal nuevoId = 1;
                        if (redsocial.Count > 0) nuevoId = redsocial.Max(x => x.Id) + 1;
                        entidad.Id = nuevoId;
                        entidad.Activo = true;
                        entidad.EnBD = false;
                        entidad.UsuarioCrea = UsuarioActual;
                        entidad.FechaCrea = DateTime.Now;
                        redsocial.Add(entidad);
                    }
                    else
                    {
                        var item = redsocial.Where(x => x.Id == entidad.Id).FirstOrDefault();
                        entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                        entidad.Activo = item.Activo;
                        entidad.UsuarioCrea = item.UsuarioCrea;
                        entidad.FechaCrea = item.FechaCrea;
                        if (entidad.EnBD)
                        {
                            entidad.UsuarioModifica = UsuarioActual;
                            entidad.FechaModifica = DateTime.Now;
                        }
                        redsocial.Remove(item);
                        redsocial.Add(entidad);
                    }
                    RedSocialTmp = redsocial;
                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult DellAddDireccion(decimal id)
        {
            Resultado retorno = new Resultado();

            try
            {
                direcciones = DireccionesTmp;
                if (direcciones != null)
                {
                    var objDel = direcciones.Where(x => x.Id == id).FirstOrDefault();
                    if (objDel != null)
                    {
                        if (objDel.EnBD)
                        {
                            bool blActivo = !objDel.Activo;

                            if (DireccionesTmpUPDEstado == null) DireccionesTmpUPDEstado = new List<DTODireccion>();
                            if (DireccionesTmpDelBD == null) DireccionesTmpDelBD = new List<DTODireccion>();

                            var itemUpd = DireccionesTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                            var itemDel = DireccionesTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                            if (!(objDel.Activo))
                            {
                                if (itemUpd == null) DireccionesTmpUPDEstado.Add(objDel);
                                if (itemDel != null) DireccionesTmpDelBD.Remove(itemDel);
                            }
                            else
                            {
                                if (itemDel == null) DireccionesTmpDelBD.Add(objDel);
                                if (itemUpd != null) DireccionesTmpUPDEstado.Remove(itemUpd);
                            }
                            objDel.Activo = blActivo;
                            direcciones.Remove(objDel);
                            direcciones.Add(objDel);
                        }
                        else
                        {
                            direcciones.Remove(objDel);
                        }

                        DireccionesTmp = direcciones;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public JsonResult DellAddObservacion(decimal id)
        {
            Resultado retorno = new Resultado();

            try
            {
                observaciones = ObservacionesTmp;
                if (observaciones != null)
                {
                    var objDel = observaciones.Where(x => x.Id == id).FirstOrDefault();
                    if (objDel != null)
                    {
                        if (objDel.EnBD)
                        {
                            bool blActivo = !objDel.Activo;

                            if (ObservacionesTmpUPDEstado == null) ObservacionesTmpUPDEstado = new List<DTOObservacion>();
                            if (ObservacionesTmpDelBD == null) ObservacionesTmpDelBD = new List<DTOObservacion>();

                            var itemUpd = ObservacionesTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                            var itemDel = ObservacionesTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                            if (!(objDel.Activo))
                            {
                                if (itemUpd == null) ObservacionesTmpUPDEstado.Add(objDel);
                                if (itemDel != null) ObservacionesTmpDelBD.Remove(itemDel);
                            }
                            else
                            {
                                if (itemDel == null) ObservacionesTmpDelBD.Add(objDel);
                                if (itemUpd != null) ObservacionesTmpUPDEstado.Remove(itemUpd);
                            }
                            objDel.Activo = blActivo;
                            observaciones.Remove(objDel);
                            observaciones.Add(objDel);
                        }
                        else
                        {
                            observaciones.Remove(objDel);
                        }
                        ObservacionesTmp = observaciones;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult DellAddDocumento(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                documentos = DocumentosTmp;
                if (documentos != null)
                {
                    var objDel = documentos.Where(x => x.Id == id).FirstOrDefault();
                    if (objDel != null)
                    {

                        if (objDel.EnBD)
                        {
                            bool blActivo = !objDel.Activo;

                            if (DocumentosTmpUPDEstado == null) DocumentosTmpUPDEstado = new List<DTODocumento>();
                            if (DocumentosTmpDelBD == null) DocumentosTmpDelBD = new List<DTODocumento>();

                            var itemUpd = DocumentosTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                            var itemDel = DocumentosTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                            if (!(objDel.Activo))
                            {
                                if (itemUpd == null) DocumentosTmpUPDEstado.Add(objDel);
                                if (itemDel != null) DocumentosTmpDelBD.Remove(itemDel);
                            }
                            else
                            {
                                if (itemDel == null) DocumentosTmpDelBD.Add(objDel);
                                if (itemUpd != null) DocumentosTmpUPDEstado.Remove(itemUpd);
                            }
                            objDel.Activo = blActivo;
                            documentos.Remove(objDel);
                            documentos.Add(objDel);
                        }
                        else
                        {
                            var path = System.Web.Configuration.WebConfigurationManager.AppSettings["RutaFisicaImgSocio"];
                            string delPath = String.Format("{0}{1}", path, objDel.Archivo);
                            if (System.IO.File.Exists(delPath))
                            {
                                try
                                {
                                    System.IO.File.Delete(delPath);
                                }
                                catch (System.IO.IOException e)
                                {
                                    // return;
                                }
                            }

                            documentos.Remove(objDel);
                        }
                        DocumentosTmp = documentos;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);


        }

        public JsonResult DellAddParametro(decimal id)
        {
            Resultado retorno = new Resultado();

            try
            {
                parametros = ParametrosTmp;
                if (parametros != null)
                {
                    var objDel = parametros.Where(x => x.Id == id).FirstOrDefault();
                    if (objDel != null)
                    {
                        if (objDel.EnBD)
                        {
                            bool blActivo = !objDel.Activo;

                            if (ParametrosTmpUPDEstado == null) ParametrosTmpUPDEstado = new List<DTOParametro>();
                            if (ParametrosTmpDelBD == null) ParametrosTmpDelBD = new List<DTOParametro>();

                            var itemUpd = ParametrosTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                            var itemDel = ParametrosTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                            if (!(objDel.Activo))
                            {
                                if (itemUpd == null) ParametrosTmpUPDEstado.Add(objDel);
                                if (itemDel != null) ParametrosTmpDelBD.Remove(itemDel);
                            }
                            else
                            {
                                if (itemDel == null) ParametrosTmpDelBD.Add(objDel);
                                if (itemUpd != null) ParametrosTmpUPDEstado.Remove(itemUpd);
                            }
                            objDel.Activo = blActivo;
                            parametros.Remove(objDel);
                            parametros.Add(objDel);
                        }
                        else
                        {
                            parametros.Remove(objDel);
                        }

                        ParametrosTmp = parametros;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);


        }

        public JsonResult DellAddTelefono(decimal Id)
        {
            Resultado retorno = new Resultado();

            try
            {
                telefonos = TelefonosTmp;
                if (telefonos != null)
                {
                    var objDel = telefonos.Where(x => x.Id == Id).FirstOrDefault();
                    if (objDel != null)
                    {
                        if (objDel.EnBD)
                        {
                            bool blActivo = !objDel.Activo;

                            if (TelefonosTmpUPDEstado == null) TelefonosTmpUPDEstado = new List<DTOTelefono>();
                            if (TelefonosTmpDelBD == null) TelefonosTmpDelBD = new List<DTOTelefono>();

                            var itemUpd = TelefonosTmpUPDEstado.Where(x => x.Id == Id).FirstOrDefault();
                            var itemDel = TelefonosTmpDelBD.Where(x => x.Id == Id).FirstOrDefault();

                            if (!(objDel.Activo))
                            {
                                if (itemUpd == null) TelefonosTmpUPDEstado.Add(objDel);
                                if (itemDel != null) TelefonosTmpDelBD.Remove(itemDel);
                            }
                            else
                            {
                                if (itemDel == null) TelefonosTmpDelBD.Add(objDel);
                                if (itemUpd != null) TelefonosTmpUPDEstado.Remove(itemUpd);
                            }
                            objDel.Activo = blActivo;
                            telefonos.Remove(objDel);
                            telefonos.Add(objDel);
                        }
                        else
                        {
                            telefonos.Remove(objDel);
                        }

                        TelefonosTmp = telefonos;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DellAddCorreo(decimal Id)
        {
            Resultado retorno = new Resultado();

            try
            {
                correos = CorreosTmp;
                if (telefonos != null)
                {
                    var objDel = correos.Where(x => x.Id == Id).FirstOrDefault();
                    if (objDel != null)
                    {
                        if (objDel.EnBD)
                        {
                            bool blActivo = !objDel.Activo;

                            if (CorreosTmpUPDEstado == null) CorreosTmpUPDEstado = new List<DTOCorreo>();
                            if (CorreosTmpDelBD == null) CorreosTmpDelBD = new List<DTOCorreo>();

                            var itemUpd = CorreosTmpUPDEstado.Where(x => x.Id == Id).FirstOrDefault();
                            var itemDel = CorreosTmpDelBD.Where(x => x.Id == Id).FirstOrDefault();

                            if (!(objDel.Activo))
                            {
                                if (itemUpd == null) CorreosTmpUPDEstado.Add(objDel);
                                if (itemDel != null) CorreosTmpDelBD.Remove(itemDel);
                            }
                            else
                            {
                                if (itemDel == null) CorreosTmpDelBD.Add(objDel);
                                if (itemUpd != null) CorreosTmpUPDEstado.Remove(itemUpd);
                            }
                            objDel.Activo = blActivo;
                            correos.Remove(objDel);
                            correos.Add(objDel);
                        }
                        else
                        {
                            correos.Remove(objDel);
                        }

                        CorreosTmp = correos;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DellAddRedes(decimal Id)
        {
            Resultado retorno = new Resultado();

            try
            {
                redsocial = RedSocialTmp;
                if (redsocial != null)
                {
                    var objDel = redsocial.Where(x => x.Id == Id).FirstOrDefault();
                    if (objDel != null)
                    {
                        if (objDel.EnBD)
                        {
                            bool blActivo = !objDel.Activo;

                            if (RedSocialTmpUPDEstado == null) RedSocialTmpUPDEstado = new List<DTORedSocial>();
                            if (RedSocialTmpDelBD == null) RedSocialTmpDelBD = new List<DTORedSocial>();

                            var itemUpd = RedSocialTmpUPDEstado.Where(x => x.Id == Id).FirstOrDefault();
                            var itemDel = RedSocialTmpDelBD.Where(x => x.Id == Id).FirstOrDefault();

                            if (!(objDel.Activo))
                            {
                                if (itemUpd == null) RedSocialTmpUPDEstado.Add(objDel);
                                if (itemDel != null) RedSocialTmpDelBD.Remove(itemDel);
                            }
                            else
                            {
                                if (itemDel == null) RedSocialTmpDelBD.Add(objDel);
                                if (itemUpd != null) RedSocialTmpUPDEstado.Remove(itemUpd);
                            }
                            objDel.Activo = blActivo;
                            redsocial.Remove(objDel);
                            redsocial.Add(objDel);
                        }
                        else
                        {
                            redsocial.Remove(objDel);
                        }

                        RedSocialTmp = redsocial;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

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
                    shtml.Append("<th  class='k-header'></th>");
                    shtml.Append("</tr></thead>");

                    if (direcciones != null)
                    {

                        string strChecked = "";
                        foreach (var item in direcciones.OrderBy(x => x.Id))
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



                            shtml.AppendFormat("<td ><input type='radio' class='radioDir' onclick='actualizarDirPrincipal({0});' name='rdbtnDir' id='rbtn_{0}' {1} /></td>", item.Id, strChecked);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td> <a href=# onclick='updAddDireccion({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                            shtml.AppendFormat("<a href=# onclick='delAddDireccion({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Dirección" : "Activar Dirección");
                            shtml.Append("</td>");
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

        public ActionResult ListarDireccionxPerfil(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEDireccion tipo = new BEDireccion();
                    tipo.Direccion = new BLDirecciones().Obtener(GlobalVars.Global.OWNER, id);

                    if (tipo.Direccion != null)
                    {
                        direcciones = new List<DTODireccion>();
                        if (tipo.Direccion != null)
                        {
                            tipo.Direccion.ForEach(s =>
                            {
                                direcciones.Add(new DTODireccion
                                {
                                    Id = s.ADD_ID,
                                    TipoPerfil = s.ENT_DESC,
                                    TipoDireccion = s.DESCRIPTION,
                                    RazonSocial = s.ADDRESS
                                });
                            });
                            DireccionesTmp = direcciones;

                            retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                            retorno.message = "OK";
                            retorno.result = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarDireccionAll()
        {
            direcciones = DireccionesTmp;
            Resultado retorno = new Resultado();

            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                shtml.Append("<thead><tr><th class='k-header' ></th><th  class='k-header'>Id</th>");
                shtml.Append("<th  class='k-header'>Perfil</th>");
                shtml.Append("<th class='k-header'>Tipo Direccion</th>");
                shtml.Append("<th class='k-header'>Nombre / Razon Social</th>");
                shtml.Append("<th  class='k-header'></th>");
                shtml.Append("</tr></thead>");

                if (direcciones != null)
                {
                    foreach (var item in direcciones.OrderBy(x => x.Id))
                    {
                        shtml.Append("<tr class='k-grid-content'>");
                        shtml.AppendFormat("<td ><input type='checkbox' class='chksel' name='chksel' id='chksel_{0}'/></td>", item.Id);
                        shtml.AppendFormat("<td >{0}</td>", item.Id);
                        shtml.AppendFormat("<td >{0}</td>", item.TipoPerfil);
                        shtml.AppendFormat("<td >{0}</td>", item.TipoDireccion);
                        shtml.AppendFormat("<td >{0}</td>", item.RazonSocial);
                        shtml.Append("</td>");
                        shtml.Append("</tr>");
                    }
                }
                shtml.Append("</table>");
                retorno.message = shtml.ToString();
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;

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
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

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
                            shtml.AppendFormat("<td> <a href=# onclick='updAddObservacion({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                            shtml.AppendFormat("<a href=# onclick='delAddObservacion({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Observacion" : "Activar Observacion");
                            shtml.Append("</td>");
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

        public JsonResult ListarTelefono()
        {
            telefonos = TelefonosTmp;
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Tipo Telefono</th>");
                    shtml.Append("<th class='k-header'>Teléfono</th>");
                    shtml.Append("<th class='k-header'>Observación</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (telefonos != null)
                    {
                        foreach (var item in telefonos.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.TipoDesc);
                            shtml.AppendFormat("<td >{0}</td>", item.Numero);
                            shtml.AppendFormat("<td >{0}</td>", item.Observacion);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td> <a href=# onclick='updAddTelefono({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                            shtml.AppendFormat("<a href=# onclick='delAddTelefono({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar telefono" : "Activar telefono");
                            shtml.Append("</td>");
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

        public JsonResult ListarCorreo()
        {
            correos = CorreosTmp;
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Tipo Correo</th>");
                    shtml.Append("<th class='k-header'>Correo</th>");
                    shtml.Append("<th class='k-header'>Observación</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (correos != null)
                    {
                        foreach (var item in correos.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.TipoDesc);
                            shtml.AppendFormat("<td >{0}</td>", item.Correo);
                            shtml.AppendFormat("<td >{0}</td>", item.Observacion);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td> <a href=# onclick='updAddCorreo({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                            shtml.AppendFormat("<a href=# onclick='delAddCorreo({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Correo" : "Activar Correo");
                            shtml.Append("</td>");
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
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Tipo Documento</th>");
                    shtml.Append("<th class='k-header' >Fecha Recepción</th>");
                    shtml.Append("<th class='k-header'>Documentos</th><th  class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (documentos != null)
                    {
                        foreach (var item in documentos.OrderBy(x => x.Id))
                        {

                            var pathWeb = GlobalVars.Global.RutaTabDocumentoSocioWeb; // System.Web.Configuration.WebConfigurationManager.AppSettings["RutaWebImgSocio"];
                            var ruta = string.Format("{0}{1}", pathWeb, item.Archivo);

                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.TipoDocumentoDesc);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaRecepcion.Substring(0, 10));
                            shtml.AppendFormat("<td ><a href='#' onclick=verImagen('{0}');>Ver Imagen</a></td>", ruta);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td> <a href=# onclick='updAddDocumento({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                            shtml.AppendFormat("<a href=# onclick='delAddDocumento({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Observacion" : "Activar Observacion");
                            shtml.Append("</td>");
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
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Tipo Observación</th>");
                    shtml.Append("<th class='k-header'>Observación</th><th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

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
                            shtml.AppendFormat("<td> <a href=# onclick='updAddParametro({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                            shtml.AppendFormat("<a href=# onclick='delAddParametro({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Parametro" : "Activar Parametro");
                            shtml.Append("</td>");
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

        public JsonResult ListarRedes()
        {
            redsocial = RedSocialTmp;
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Tipo Red Social</th>");
                    shtml.Append("<th class='k-header'>Link</th>");
                    shtml.Append("<th class='k-header'>Observación</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (redsocial != null)
                    {
                        foreach (var item in redsocial.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.TipoDesc);
                            shtml.AppendFormat("<td >{0}</td>", item.Link);
                            shtml.AppendFormat("<td >{0}</td>", item.Observacion);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td> <a href=# onclick='updAddRedes({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                            shtml.AppendFormat("<a href=# onclick='delAddRedes({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Red Social" : "Activar Red Social");
                            shtml.Append("</td>");
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

        public JsonResult ValidarRuc(String ruc, decimal TipoDocumento)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    string nombreRazon = "-";
                    var exito = EstadoRuc(ruc, TipoDocumento, ref nombreRazon);

                    if (exito)
                    {
                        retorno.message = "RUC VALIDO";
                        retorno.valor = nombreRazon;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.message = "RUC INVALIDO";
                        retorno.result = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Validar Ruc", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public Boolean EstadoRuc(String ruc, decimal TipoDocumento, ref string razonSocial)
        {
            var validaruc = System.Web.Configuration.WebConfigurationManager.AppSettings["ValidarRuc"];

            if (validaruc == "S" && TipoDocumento == 1)
            {
                WebRequest request = WebRequest.Create(string.Format("http://www.sunat.gob.pe/w/wapS01Alias?ruc={0}", ruc));
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string res = reader.ReadToEnd();
                var exito = false;
                if (res.Contains("Activo") || res.Contains("ACTIVO"))
                {
                    exito = true;
                    razonSocial = getNombreRazonSocial(res);
                }

                reader.Close();
                response.Close();

                return exito;
            }
            else
            {
                return true;
            }
        }

        public JsonResult ACBuscarSocio()
        {

            string texto = Request.QueryString["term"];
            var datos = new BLSocioNegocio().UPS_BUSCAR_SOCIOS_X_RAZONSOCIAL(GlobalVars.Global.OWNER, texto);

            List<DTOSocio> socios = new List<DTOSocio>();

            datos.ForEach(x =>
            {
                socios.Add(new DTOSocio
                {
                    Codigo = x.BPS_ID,
                    value = String.Format("{0} {1} {2} {3}", x.BPS_NAME, x.BPS_FIRST_NAME, x.BPS_FATH_SURNAME, x.BPS_MOTH_SURNAME)
                });
            });

            //return Json(socios, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(socios, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult Insertar(DTOSocio socio)
        {
            Resultado retorno = new Resultado();
            Boolean resultado = true;
            try
            {
                if (!isLogout(ref retorno))
                {


                    bool tienePermiso = true;
                    //es edicion
                    if (socio.Codigo > 0)
                        tienePermiso = new SeguridadController().PuedeRegistrarSocioUD(Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]), socio.Codigo, Convert.ToString(Session[Constantes.Sesiones.CodigoPerfil]));
                    if (!tienePermiso)
                    {
                        retorno.message = Constantes.MensajeGenerico.MSG_SIN_PERMISO_USUARIO_OFI;
                        retorno.result = 0;
                    }
                    else
                    {


                        string esAsoc = "0"; string esEmpl = "0"; string esGrup = "0"; string esProv = "0"; string esReca = "0"; string esDere = "0";
                        if (socio.EsAsociacion) esAsoc = "1"; if (socio.EsEmpleador) esEmpl = "1"; if (socio.EsGrupoEmp) esGrup = "1"; if (socio.EsProveedor) esProv = "1"; if (socio.EsRecaudador) esReca = "1"; if (socio.EsUsuDerecho) esDere = "1";
                        string nombreRazon = "-";
                        var exito = EstadoRuc(socio.NumDocumento, socio.TipoDocumento, ref nombreRazon);
                        ///INICIO DE VALIDACION PARA EL INSERT Y UPDATE DE SOCIO
                        if (!exito)
                        {
                            retorno.message = "RUC INVALIDO";
                            retorno.valor = nombreRazon;
                            retorno.result = 0;
                            resultado = false;
                        }
                        if (socio.Codigo == 0)
                        {
                            var existeDoc = new BLSocioNegocio().existeSocioXDocumento(GlobalVars.Global.OWNER, socio.TipoDocumento, socio.NumDocumento);
                            if (existeDoc)
                            {
                                retorno.message = "Ya existe un socio con el tipo y número de documento.";
                                retorno.result = 0;
                                resultado = false;
                            }
                        }
                        else
                        {
                            var existeDoc = new BLSocioNegocio().existeSocioXDocumento(GlobalVars.Global.OWNER, socio.TipoDocumento, socio.NumDocumento, socio.Codigo);
                            if (existeDoc)
                            {
                                retorno.message = "Ya existe un socio con el tipo y número de documento.";
                                retorno.result = 0;
                                resultado = false;
                            }
                        }
                        ///FIN DE VALIDACION PARA EL INSERT Y UPDATE DE SOCIO

                        if (resultado)
                        {

                            SocioNegocio obj = new SocioNegocio();
                            obj.OWNER = GlobalVars.Global.OWNER;
                            obj.ENT_TYPE = Convert.ToChar(socio.TipoEntidad);
                            obj.BPS_NAME = socio.RazonSocial;
                            obj.BPS_FIRST_NAME = socio.Nombres;
                            obj.BPS_FATH_SURNAME = socio.Paterno;
                            obj.BPS_MOTH_SURNAME = socio.Materno;
                            obj.TAX_ID = socio.NumDocumento;
                            obj.TAXT_ID = socio.TipoDocumento;
                            obj.BPS_GROUP = Convert.ToChar(esGrup);
                            obj.BPS_ASSOCIATION = Convert.ToChar(esAsoc);
                            obj.BPS_COLLECTOR = Convert.ToChar(esReca);
                            obj.BPS_SUPPLIER = Convert.ToChar(esProv);
                            obj.BPS_USER = Convert.ToChar(esDere);
                            obj.BPS_EMPLOYEE = Convert.ToChar(esEmpl);
                            obj.LOG_USER_CREAT = UsuarioActual;

                            obj.Observaciones = obtenerObservaciones();
                            obj.Parametros = ObtenerParametros();
                            obj.Documentos = obtenerDocumentos();
                            obj.Direcciones = obtenerDirecciones();
                            obj.Correos = obtenerCorreos();
                            obj.Telefonos = obtenerTelefonos();
                            obj.RedSocial = obtenerRedSocial();
                            obj.BPS_TRADE_NAME = socio.NombreComercial;
                            if (socio.Codigo == 0)
                            {
                                var datos = new BLSocioNegocio().Insertar(obj);
                                retorno.Code = datos;
                            }
                            else
                            {
                                obj.BPS_ID = socio.Codigo;
                                obj.LOG_USER_UPDATE = UsuarioActual;

                                #region Setting Edicion

                                //1.setting direcciones eliminar
                                List<BEDireccion> listaDirDel = null;
                                if (DireccionesTmpDelBD != null)
                                {
                                    listaDirDel = new List<BEDireccion>();
                                    DireccionesTmpDelBD.ForEach(x => { listaDirDel.Add(new BEDireccion { ADD_ID = x.Id }); });
                                }
                                //setting direcciones activar
                                List<BEDireccion> listaDirUpdEst = null;
                                if (DireccionesTmpUPDEstado != null)
                                {
                                    listaDirUpdEst = new List<BEDireccion>();
                                    DireccionesTmpUPDEstado.ForEach(x => { listaDirUpdEst.Add(new BEDireccion { ADD_ID = x.Id }); });
                                }

                                //2.setting Observacion eliminar
                                List<BEObservationGral> listaObsDel = null;
                                if (ObservacionesTmpDelBD != null)
                                {
                                    listaObsDel = new List<BEObservationGral>();
                                    ObservacionesTmpDelBD.ForEach(x => { listaObsDel.Add(new BEObservationGral { OBS_ID = x.Id }); });
                                }
                                //setting Observacion activar
                                List<BEObservationGral> listaObsUpdEst = null;
                                if (ObservacionesTmpUPDEstado != null)
                                {
                                    listaObsUpdEst = new List<BEObservationGral>();
                                    ObservacionesTmpUPDEstado.ForEach(x => { listaObsUpdEst.Add(new BEObservationGral { OBS_ID = x.Id }); });
                                }

                                //3.setting Parametro eliminar
                                List<BEParametroGral> listaParDel = null;
                                if (ParametrosTmpDelBD != null)
                                {
                                    listaParDel = new List<BEParametroGral>();
                                    ParametrosTmpDelBD.ForEach(x => { listaParDel.Add(new BEParametroGral { PAR_ID = x.Id }); });
                                }
                                //setting Parametro activar
                                List<BEParametroGral> listaParUpdEst = null;
                                if (ParametrosTmpUPDEstado != null)
                                {
                                    listaParUpdEst = new List<BEParametroGral>();
                                    ParametrosTmpUPDEstado.ForEach(x => { listaParUpdEst.Add(new BEParametroGral { PAR_ID = x.Id }); });
                                }
                                //4.setting Documentos eliminar
                                List<BEDocumentoGral> listaDocDel = null;
                                if (DocumentosTmpDelBD != null)
                                {
                                    listaDocDel = new List<BEDocumentoGral>();
                                    DocumentosTmpDelBD.ForEach(x => { listaDocDel.Add(new BEDocumentoGral { DOC_ID = x.Id }); });
                                }
                                //setting Documentos activar
                                List<BEDocumentoGral> listaDocUpdEst = null;
                                if (DocumentosTmpUPDEstado != null)
                                {
                                    listaDocUpdEst = new List<BEDocumentoGral>();
                                    DocumentosTmpUPDEstado.ForEach(x => { listaDocUpdEst.Add(new BEDocumentoGral { DOC_ID = x.Id }); });
                                }

                                //5.setting Telefono eliminar
                                List<BETelefono> listaTelDel = null;
                                if (TelefonosTmpDelBD != null)
                                {
                                    listaTelDel = new List<BETelefono>();
                                    TelefonosTmpDelBD.ForEach(x => { listaTelDel.Add(new BETelefono { PHONE_ID = x.Id }); });
                                }
                                //setting Telefonos activar
                                List<BETelefono> listaTelUpdEst = null;
                                if (TelefonosTmpUPDEstado != null)
                                {
                                    listaTelUpdEst = new List<BETelefono>();
                                    TelefonosTmpUPDEstado.ForEach(x => { listaTelUpdEst.Add(new BETelefono { PHONE_ID = x.Id }); });
                                }

                                //6.setting Correos eliminar
                                List<BECorreo> listaMailDel = null;
                                if (CorreosTmpDelBD != null)
                                {
                                    listaMailDel = new List<BECorreo>();
                                    CorreosTmpDelBD.ForEach(x => { listaMailDel.Add(new BECorreo { MAIL_ID = x.Id }); });
                                }
                                //setting Correo         activar
                                List<BECorreo> listaMailUpdEst = null;
                                if (CorreosTmpUPDEstado != null)
                                {
                                    listaMailUpdEst = new List<BECorreo>();
                                    CorreosTmpUPDEstado.ForEach(x => { listaMailUpdEst.Add(new BECorreo { MAIL_ID = x.Id }); });
                                }

                                //7.setting Redes Sociales eliminar
                                List<BERedes_Sociales> listaRedSocialDel = null;
                                if (RedSocialTmpDelBD != null)
                                {
                                    listaRedSocialDel = new List<BERedes_Sociales>();
                                    RedSocialTmpDelBD.ForEach(x => { listaRedSocialDel.Add(new BERedes_Sociales { CONT_ID = x.Id }); });
                                }
                                //setting Redes Sociales         activar
                                List<BERedes_Sociales> listaRedSocialUpdEst = null;
                                if (RedSocialTmpUPDEstado != null)
                                {
                                    listaRedSocialUpdEst = new List<BERedes_Sociales>();
                                    RedSocialTmpUPDEstado.ForEach(x => { listaRedSocialUpdEst.Add(new BERedes_Sociales { CONT_ID = x.Id }); });
                                }
                                #endregion
                                var datos = new BLSocioNegocio().Actualizar(obj,
                                                                            listaDirDel, listaDirUpdEst,
                                                                            listaObsDel, listaObsUpdEst,
                                                                            listaDocDel, listaDocUpdEst,
                                                                            listaParDel, listaParUpdEst,
                                                                            listaTelDel, listaTelUpdEst,
                                                                            listaMailDel, listaMailUpdEst,
                                                                            listaRedSocialDel, listaRedSocialUpdEst
                                                                            );

                            }
                            retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                            retorno.result = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "insert socio", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BEObservationGral> obtenerObservaciones()
        {
            List<BEObservationGral> datos = new List<BEObservationGral>();
            if (ObservacionesTmp != null)
            {
                ObservacionesTmp.ForEach(x =>
                {
                    datos.Add(new BEObservationGral
                    {
                        OBS_ID = x.Id,
                        OWNER = GlobalVars.Global.OWNER,
                        OBS_TYPE = Convert.ToInt32(x.TipoObservacion),
                        OBS_VALUE = x.Observacion,
                        ENT_ID = Convert.ToInt32(Constantes.ENTIDAD.OTROS),
                        LOG_USER_CREAT = UsuarioActual,
                        OBS_USER = UsuarioActual
                    });
                });
            }

            return datos;

        }
        private List<BEParametroGral> ObtenerParametros()
        {
            List<BEParametroGral> datos = new List<BEParametroGral>();

            if (ParametrosTmp != null)
            {
                ParametrosTmp.ForEach(x =>
                {
                    datos.Add(new BEParametroGral
                    {
                        PAR_ID = x.Id,
                        OWNER = GlobalVars.Global.OWNER,
                        PAR_TYPE = Convert.ToInt32(x.TipoParametro),
                        PAR_VALUE = x.Descripcion,
                        ENT_ID = Constantes.ENTIDAD.OTROS,
                        LOG_USER_CREAT = UsuarioActual
                    });
                });
            }
            return datos;
        }
        private List<BEDocumentoGral> obtenerDocumentos()
        {
            List<BEDocumentoGral> datos = new List<BEDocumentoGral>();
            if (DocumentosTmp != null)
            {
                DocumentosTmp.ForEach(x =>
                {
                    datos.Add(new BEDocumentoGral
                    {
                        DOC_ID = x.Id,
                        OWNER = GlobalVars.Global.OWNER,
                        DOC_TYPE = Convert.ToInt32(x.TipoDocumento),
                        DOC_PATH = x.Archivo,
                        DOC_DATE = Convert.ToDateTime(x.FechaRecepcion),
                        ENT_ID = Convert.ToInt32(Constantes.ENTIDAD.OTROS),
                        DOC_USER = UsuarioActual,
                        LOG_USER_CREAT = UsuarioActual,
                        DOC_VERSION = 1
                    });
                });
            }
            return datos;
        }
        private List<BEDireccion> obtenerDirecciones()
        {
            List<BEDireccion> datos = new List<BEDireccion>();
            if (DireccionesTmp != null)
            {
                DireccionesTmp.ForEach(x =>
                {
                    var obj = new BEDireccion();
                    obj.ADD_ID = x.Id;
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.ENT_ID = Constantes.ENTIDAD.OTROS;
                    obj.ADD_TYPE = Convert.ToDecimal(x.TipoDireccion);
                    obj.TIS_N = Convert.ToDecimal(x.Territorio);
                    obj.ADDRESS = x.RazonSocial;
                    obj.HOU_LOT = x.Lote;
                    obj.HOU_MZ = x.Manzana;
                    obj.HOU_NRO = Convert.ToDecimal(x.Numero);
                    obj.GEO_ID = Convert.ToDecimal(x.CodigoUbigeo);
                    obj.ADD_REFER = x.Referencia;
                    obj.ROU_ID = Convert.ToDecimal(x.TipoAvenida);
                    obj.ROU_NAME = x.Avenida;
                    obj.ROU_NUM = "1";
                    obj.HOU_TETP = x.TipoEtapa;
                    obj.HOU_NETP = x.Etapa;
                    obj.HOU_TURZN = x.TipoUrb;
                    obj.HOU_URZN = x.Urbanizacion;
                    obj.REMARK = "";
                    obj.CPO_ID = 2;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    obj.LOG_USER_UPDATE = UsuarioActual;
                    obj.MAIN_ADD = Convert.ToChar(x.EsPrincipal == "" ? "0" : x.EsPrincipal);
                    datos.Add(obj);
                });
            }
            return datos;
        }
        private List<BETelefono> obtenerTelefonos()
        {
            List<BETelefono> datos = new List<BETelefono>();

            if (TelefonosTmp != null)
            {
                TelefonosTmp.ForEach(x =>
                {
                    datos.Add(new BETelefono
                    {
                        PHONE_ID = x.EnBD ? x.Id : decimal.Zero,
                        PHONE_TYPE = x.IdTipo,
                        OWNER = GlobalVars.Global.OWNER,
                        PHONE_NUMBER = (x.Numero),
                        PHONE_OBS = x.Observacion,
                        ENT_ID = Convert.ToInt32(Constantes.ENTIDAD.OTROS),
                        LOG_USER_CREAT = UsuarioActual
                    });
                });
            }
            return datos;
        }
        private List<BECorreo> obtenerCorreos()
        {
            List<BECorreo> datos = new List<BECorreo>();

            if (CorreosTmp != null)
            {
                CorreosTmp.ForEach(x =>
                {
                    datos.Add(new BECorreo
                    {
                        MAIL_ID = x.EnBD ? x.Id : decimal.Zero,
                        MAIL_TYPE = x.IdTipo,
                        OWNER = GlobalVars.Global.OWNER,
                        MAIL_DESC = x.Correo,
                        MAIL_OBS = x.Observacion,
                        ENT_ID = Convert.ToInt32(Constantes.ENTIDAD.OTROS),
                        LOG_USER_CREAT = UsuarioActual
                    });
                });
            }
            return datos;
        }
        private List<BERedes_Sociales> obtenerRedSocial()
        {
            List<BERedes_Sociales> datos = new List<BERedes_Sociales>();

            if (RedSocialTmp != null)
            {
                RedSocialTmp.ForEach(x =>
                {
                    datos.Add(new BERedes_Sociales
                    {
                        CONT_ID = x.EnBD ? x.Id : decimal.Zero,
                        CONT_TYPE = x.IdTipo,
                        OWNER = GlobalVars.Global.OWNER,
                        CONT_DESC = x.Link,
                        CONT_OBS = x.Observacion,
                        ENT_ID = Convert.ToInt32(Constantes.ENTIDAD.OTROS),
                        LOG_USER_CREAT = UsuarioActual
                    });
                });
            }
            return datos;
        }

        public JsonResult ObtenerSocioDocumento(decimal tipo, string nro_tipo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLSocioNegocio servicio = new BLSocioNegocio();
                    var socio = servicio.ObtenerSocioDocumento(GlobalVars.Global.OWNER, tipo, nro_tipo);
                    if (socio != null)
                    {
                        DTOSocio socioDTO = new DTOSocio()
                        {
                            Codigo = socio.BPS_ID,
                            TipoPersona = Convert.ToString(socio.ENT_TYPE),
                            RazonSocial = socio.BPS_NAME,
                            TipoDocumento = socio.TAXT_ID,
                            NumDocumento = socio.TAX_ID,
                            Nombres = socio.BPS_FIRST_NAME,
                            Paterno = socio.BPS_FATH_SURNAME,
                            Materno = socio.BPS_MOTH_SURNAME
                        };
                        retorno.data = Json(socioDTO, JsonRequestBehavior.AllowGet);
                        retorno.message = "Socio encontrado";
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.message = "No se ha podido encontrar al socio";
                        retorno.result = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtener socio", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Obtener(decimal codigo)
        {
            parametros = ParametrosTmp;
            Resultado retorno = new Resultado();
            //System.Threading.Thread.Sleep(4000);
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLSocioNegocio servicio = new BLSocioNegocio();
                    var socio = servicio.Obtener(codigo, GlobalVars.Global.OWNER, Constantes.ENTIDAD.OTROS);
                    if (socio != null)
                    {
                        DTOSocio socioDto = new DTOSocio()
                        {
                            Codigo = socio.BPS_ID,
                            RazonSocial = socio.BPS_NAME,
                            TipoDocumento = socio.TAXT_ID,
                            TipoPersona = Convert.ToString(socio.ENT_TYPE),
                            NumDocumento = socio.TAX_ID,
                            EsAsociacion = Convert.ToString(socio.BPS_ASSOCIATION) == "1" ? true : false,
                            EsRecaudador = Convert.ToString(socio.BPS_COLLECTOR) == "1" ? true : false,
                            EsEmpleador = Convert.ToString(socio.BPS_EMPLOYEE) == "1" ? true : false,
                            EsGrupoEmp = Convert.ToString(socio.BPS_GROUP) == "1" ? true : false,
                            EsProveedor = Convert.ToString(socio.BPS_SUPPLIER) == "1" ? true : false,
                            EsUsuDerecho = Convert.ToString(socio.BPS_USER) == "1" ? true : false,
                            Nombres = socio.BPS_FIRST_NAME,
                            Paterno = socio.BPS_FATH_SURNAME,
                            Materno = socio.BPS_MOTH_SURNAME,
                            NombreComercial = socio.BPS_TRADE_NAME,
                            Verificado = socio.VERIFICADO
                        };

                        if (socio.Observaciones != null)
                        {
                            observaciones = new List<DTOObservacion>();
                            if (socio.Observaciones != null)
                            {
                                socio.Observaciones.ForEach(s =>
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
                        }
                        if (socio.Parametros != null)
                        {
                            parametros = new List<DTOParametro>();
                            if (socio.Parametros != null)
                            {
                                socio.Parametros.ForEach(s =>
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
                        }

                        if (socio.Documentos != null)
                        {
                            documentos = new List<DTODocumento>();
                            if (socio.Documentos != null)
                            {
                                socio.Documentos.ForEach(s =>
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
                                    documentos.Add(newDTODocumento);
                                });
                                DocumentosTmp = documentos;
                            }
                        }
                        if (socio.Direcciones != null)
                        {
                            direcciones = new List<DTODireccion>();
                            if (socio.Direcciones != null)
                            {
                                socio.Direcciones.ForEach(s =>
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
                        }

                        if (socio.Telefonos != null)
                        {
                            telefonos = new List<DTOTelefono>();
                            if (socio.Telefonos != null)
                            {
                                socio.Telefonos.ForEach(s =>
                                {
                                    telefonos.Add(new DTOTelefono
                                    {
                                        Id = s.PHONE_ID,
                                        IdTipo = s.PHONE_TYPE,
                                        Numero = s.PHONE_NUMBER,
                                        Observacion = s.PHONE_OBS,
                                        TipoDesc = new BLTipoTelefono().Obtener(GlobalVars.Global.OWNER, s.PHONE_TYPE).PHONE_TDESC,
                                        EnBD = true,
                                        UsuarioCrea = s.LOG_USER_CREAT,
                                        FechaCrea = s.LOG_DATE_CREAT,
                                        UsuarioModifica = s.LOG_USER_UPDATE,
                                        FechaModifica = s.LOG_DATE_UPDATE,
                                        Activo = s.ENDS.HasValue ? false : true
                                    });
                                });
                                TelefonosTmp = telefonos;
                            }
                        }

                        if (socio.Correos != null)
                        {
                            correos = new List<DTOCorreo>();
                            if (socio.Correos != null)
                            {
                                socio.Correos.ForEach(s =>
                                {
                                    correos.Add(new DTOCorreo
                                    {
                                        Id = s.MAIL_ID,
                                        IdTipo = s.MAIL_TYPE,
                                        Correo = s.MAIL_DESC,
                                        Observacion = s.MAIL_OBS,
                                        TipoDesc = new BLTipoCorreo().Obtener(GlobalVars.Global.OWNER, s.MAIL_TYPE).MAIL_TDESC,
                                        EnBD = true,
                                        UsuarioCrea = s.LOG_USER_CREAT,
                                        FechaCrea = s.LOG_DATE_CREAT,
                                        UsuarioModifica = s.LOG_USER_UPDATE,
                                        FechaModifica = s.LOG_DATE_UPDATE,
                                        Activo = s.ENDS.HasValue ? false : true
                                    });
                                });
                                CorreosTmp = correos;
                            }
                        }

                        if (socio.RedSocial != null)
                        {
                            redsocial = new List<DTORedSocial>();
                            if (socio.RedSocial != null)
                            {
                                socio.RedSocial.ForEach(s =>
                                {
                                    redsocial.Add(new DTORedSocial
                                    {
                                        Id = s.CONT_ID,
                                        IdTipo = s.CONT_TYPE,
                                        Link = s.CONT_DESC,
                                        Observacion = s.CONT_OBS,
                                        TipoDesc = new BLRed_Social().Obtener(GlobalVars.Global.OWNER, s.CONT_ID).CONT_TDESC,
                                        EnBD = true,
                                        UsuarioCrea = s.LOG_USER_CREAT,
                                        FechaCrea = s.LOG_DATE_CREAT,
                                        UsuarioModifica = s.LOG_USER_UPDATE,
                                        FechaModifica = s.LOG_DATE_UPDATE,
                                        Activo = s.ENDS.HasValue ? false : true
                                    });
                                });
                                RedSocialTmp = redsocial;
                            }
                        }

                        retorno.data = Json(socioDto, JsonRequestBehavior.AllowGet);
                        retorno.message = "Socio encontrado";
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.message = "No se ha podido encontrar al socio";
                        retorno.result = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "obtener socio", ex);
                //log de errores
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetDirPrincipal(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (DireccionesTmp != null)
                    {
                        foreach (var x in DireccionesTmp.Where(x => x.Id != idDir))
                        {
                            x.EsPrincipal = "0";
                        }
                        foreach (var x in DireccionesTmp.Where(x => x.Id == idDir))
                        {
                            x.EsPrincipal = "1";
                        }
                    }

                    retorno.message = "OK";
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

        public JsonResult ObtieneDireccionTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var direccion = DireccionesTmp.Where(x => x.Id == idDir).FirstOrDefault();
                    retorno.data = Json(direccion, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtieneDireccionTmp", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneObservacionTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var observacion = ObservacionesTmp.Where(x => x.Id == idDir).FirstOrDefault();
                    retorno.data = Json(observacion, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtieneObservacionTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneParametroTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var param = ParametrosTmp.Where(x => x.Id == idDir).FirstOrDefault();
                    retorno.data = Json(param, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtieneParametroTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneDocumentoTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var param = DocumentosTmp.Where(x => x.Id == idDir).FirstOrDefault();
                    retorno.data = Json(param, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtieneDocumentoTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneCorreoTmp(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    string owner = GlobalVars.Global.OWNER;
                    var entidad = CorreosTmp.Where(x => x.Id == Id).FirstOrDefault();
                    retorno.data = Json(entidad, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtieneCorreoTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneRedesSocialesTmp(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    string owner = GlobalVars.Global.OWNER;
                    var entidad = RedSocialTmp.Where(x => x.Id == Id).FirstOrDefault();
                    retorno.data = Json(entidad, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtieneRedSocialTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneTelefonoTmp(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    string owner = GlobalVars.Global.OWNER;
                    var entidad = TelefonosTmp.Where(x => x.Id == Id).FirstOrDefault();
                    retorno.data = Json(entidad, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtieneCorreoTmp", ex);
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
                    BLSocioNegocio servicio = new BLSocioNegocio();
                    var result = servicio.Eliminar(new SocioNegocio
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        BPS_ID = codigo,
                        LOG_USER_UPDATE = UsuarioActual
                    });
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Eliminar Documento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private string getRazonSocial(DTODireccion entidad)
        {

            StringBuilder rz = new StringBuilder();


            //if (!String.IsNullOrEmpty(entidad.Urbanizacion))
            //{
            //    rz.AppendFormat("{0} {1}", entidad.TipoUrbDes, entidad.Urbanizacion);
            //}

            //if (!String.IsNullOrEmpty(entidad.Manzana))
            //{
            //    rz.AppendFormat("    Mz {0}", entidad.Manzana);
            //}

            //if (!String.IsNullOrEmpty(entidad.Lote))
            //{
            //    rz.AppendFormat("  Lote {0}", entidad.Lote);
            //}

            //if (!String.IsNullOrEmpty(entidad.Etapa))
            //{
            //    rz.AppendFormat(" {0} {1}", entidad.TipoEtapaDes, entidad.Etapa);
            //}

            //if (!String.IsNullOrEmpty(entidad.Avenida))
            //{
            //    rz.AppendFormat(" {0} {1}", entidad.TipoAvenidaDes, entidad.Avenida);
            //}

            //if (!String.IsNullOrEmpty(entidad.Numero))
            //{
            //    //rz.AppendFormat("  Nro {0}", entidad.Numero);
            //    rz.AppendFormat("  {0}", entidad.Numero);
            //}

            //if (!String.IsNullOrEmpty(entidad.NroPiso))
            //{
            //    rz.AppendFormat("  {0} {1}", entidad.TipoDepaDes, entidad.NroPiso);
            //}


            if (!String.IsNullOrEmpty(entidad.Avenida))
            {
                rz.AppendFormat(" {0} {1}", entidad.TipoAvenidaDes, entidad.Avenida);
            }

            if (!String.IsNullOrEmpty(entidad.Numero))
            {
                //rz.AppendFormat("  Nro {0}", entidad.Numero);
                rz.AppendFormat(" {0}", entidad.Numero);
            }

            if (!String.IsNullOrEmpty(entidad.Manzana))
            {
                rz.AppendFormat("    Mz {0}", entidad.Manzana);
            }

            if (!String.IsNullOrEmpty(entidad.Lote))
            {
                rz.AppendFormat("  Lote {0}", entidad.Lote);
            }

            if (!String.IsNullOrEmpty(entidad.Etapa))
            {
                rz.AppendFormat(" {0} {1}", entidad.TipoEtapaDes, entidad.Etapa);
            }

            if (!String.IsNullOrEmpty(entidad.NroPiso))
            {
                rz.AppendFormat("  {0} {1}", entidad.TipoDepaDes, entidad.NroPiso);
            }

            if (!String.IsNullOrEmpty(entidad.Urbanizacion))
            {
                rz.AppendFormat(" {0} {1}", entidad.TipoUrbDes, entidad.Urbanizacion);
            }

            return rz.ToString();
        }

        public JsonResult BuscarSocio(int skip, int take, int page, int pageSize, string group, string tipoPersona, decimal? tipo, string nro_tipo, string nombre, decimal ubigeo, Boolean derecho, Boolean asociacion, Boolean grupo, Boolean recaudador, Boolean proveedor, Boolean empleado, int estado)
        {
            Resultado retorno = new Resultado();

            var flt = "0";
            //if (filtro) flt = "0";
            if (!derecho && !asociacion && !grupo && !recaudador && !proveedor && !empleado) flt = "0";

            else flt = "2";


            var BPS_USER = flt;
            var BPS_ASSOCIATION = flt;
            var BPS_GROUP = flt;
            var BPS_COLLECTOR = flt;
            var BPS_SUPPLIER = flt;
            var BPS_EMPLOYEE = flt;

            if (derecho) BPS_USER = "1";
            if (asociacion) BPS_ASSOCIATION = "1";
            if (grupo) BPS_GROUP = "1";
            if (recaudador) BPS_COLLECTOR = "1";
            if (proveedor) BPS_SUPPLIER = "1";
            if (empleado) BPS_EMPLOYEE = "1";

            //var lista = new BLSocioNegocio().BuscarSocio(tipoPersona, tipo, nro_tipo, nombre, ubigeo, estado, page, pageSize);
            //var lista = new BLSocioNegocio().BuscarSocio(tipoPersona, tipo, nro_tipo, nombre, ubigeo, estado, page, pageSize);

            var lista = ListaSocio(tipoPersona, tipo, nro_tipo, nombre, ubigeo, page, pageSize, BPS_USER, BPS_ASSOCIATION, BPS_GROUP, BPS_COLLECTOR, BPS_SUPPLIER, BPS_EMPLOYEE, estado);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new SocioNegocio { Socio_Negocio = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new SocioNegocio { Socio_Negocio = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult BuscarSocioRecaudador(int skip, int take, int page, int pageSize, string group, string tipoPersona, decimal? tipo, string nro_tipo, string nombre, decimal ubigeo, Boolean derecho, Boolean asociacion, Boolean grupo, Boolean recaudador, Boolean proveedor, Boolean empleado, int estado)
        {

            Resultado retorno = new Resultado();
            var flt = "0";
            //if (filtro) flt = "0";
            if (!derecho && !asociacion && !grupo && !recaudador && !proveedor && !empleado) flt = "0";

            else flt = "2";


            var BPS_USER = flt;
            var BPS_ASSOCIATION = flt;
            var BPS_GROUP = flt;
            var BPS_COLLECTOR = flt;
            var BPS_SUPPLIER = flt;
            var BPS_EMPLOYEE = flt;

            if (derecho) BPS_USER = "1";
            if (asociacion) BPS_ASSOCIATION = "1";
            if (grupo) BPS_GROUP = "1";
            if (recaudador) BPS_COLLECTOR = "1";
            if (proveedor) BPS_SUPPLIER = "1";
            if (empleado) BPS_EMPLOYEE = "1";

            var lista = ListaSocio(tipoPersona, tipo, nro_tipo, nombre, ubigeo, page, pageSize, BPS_USER, BPS_ASSOCIATION, BPS_GROUP, BPS_COLLECTOR, BPS_SUPPLIER, BPS_EMPLOYEE, estado);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new SocioNegocio { Socio_Negocio = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new SocioNegocio { Socio_Negocio = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }


        public List<SocioNegocio> ListaSocio(string tipoPersona, decimal? tipo, string nro_tipo, string nombre, decimal ubigeo, int pagina, int cantRegxPag, string BPS_USER, string BPS_ASSOCIATION, string BPS_GROUP, string BPS_COLLECTOR, string BPS_SUPPLIER, string BPS_EMPLOYEE, int estado)
        {
            //string tipoPersona,
            //decimal? tipoId,
            //string nro_tipo,
            //string nombre,
            //decimal ubigeo,
            //int estado,
            //int pagina,
            //int cantRegxPag,
            //string BPS_USER, string BPS_ASSOCIATION, string BPS_GROUP, string BPS_COLLECTOR, string BPS_SUPPLIER, string BPS_EMPLOYEE)
            return new BLSocioNegocio().BuscarSocio(tipoPersona, tipo, nro_tipo, nombre, ubigeo, estado, pagina, cantRegxPag, BPS_USER, BPS_ASSOCIATION, BPS_GROUP, BPS_COLLECTOR, BPS_SUPPLIER, BPS_EMPLOYEE);
        }

        private string getNombreRazonSocial(string trama)
        {
            string varstring = (trama.TrimStart()).TrimEnd();
            string rsFinal = "-";
            int posIni = varstring.IndexOf(@"<small>");
            int posFin = varstring.IndexOf(@"</small>");
            if (posIni != -1 && posFin != -1)
            {
                string rs = varstring.Substring(posIni, posFin - posIni);
                int posIniB = rs.IndexOf("-");
                int posFinB = rs.Length;
                if (posIniB != -1 && posFinB != -1)
                {
                    posFinB = posFinB - 5;
                    posIniB = posIniB + 1;
                    rsFinal = rs.Substring(posIniB, posFinB - posIniB);
                }
            }
            return rsFinal;
        }

        public JsonResult ActualizarNombreDocTmp(string nombre, decimal idDoc)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    retorno.valor = "-";

                    documentos = DocumentosTmp;
                    if (documentos == null) documentos = new List<DTODocumento>();
                    documentos.ForEach(x => { if (x.Id == idDoc) x.Archivo = nombre; });
                    if (documentos.Count == 1) documentos[0].Archivo = nombre;
                    DocumentosTmp = documentos;


                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ActualizarNombreDocTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region GRUPO EMPRESARIAL

        public JsonResult BuscarSocioEmpresarial(int skip, int take, int page, int pageSize, string group, string tipoPersona, decimal? tipo, string nro_tipo, string nombre, decimal ubigeo, Boolean derecho, Boolean asociacion, Boolean grupo, Boolean recaudador, Boolean proveedor, Boolean empleado, int estado, string nombreComercial)
        {
            Resultado retorno = new Resultado();

            var flt = "0";
            //if (filtro) flt = "0";
            if (!derecho && !asociacion && !grupo && !recaudador && !proveedor && !empleado) flt = "0";

            else flt = "2";


            var BPS_USER = flt;
            var BPS_ASSOCIATION = flt;
            var BPS_GROUP = flt;
            var BPS_COLLECTOR = flt;
            var BPS_SUPPLIER = flt;
            var BPS_EMPLOYEE = flt;

            if (derecho) BPS_USER = "1";
            if (asociacion) BPS_ASSOCIATION = "1";
            if (grupo) BPS_GROUP = "1";
            if (recaudador) BPS_COLLECTOR = "1";
            if (proveedor) BPS_SUPPLIER = "1";
            if (empleado) BPS_EMPLOYEE = "1";

            //var lista = new BLSocioNegocio().BuscarSocio(tipoPersona, tipo, nro_tipo, nombre, ubigeo, estado, page, pageSize);
            //var lista = new BLSocioNegocio().BuscarSocio(tipoPersona, tipo, nro_tipo, nombre, ubigeo, estado, page, pageSize);

            var lista = new BLSocioNegocio().BuscarSocioEmpresarial(tipoPersona, tipo, nro_tipo, nombre, ubigeo, estado, page, pageSize, BPS_USER, BPS_ASSOCIATION, BPS_GROUP, BPS_COLLECTOR, BPS_SUPPLIER, BPS_EMPLOYEE, nombreComercial);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new SocioNegocio { Socio_Negocio = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new SocioNegocio { Socio_Negocio = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Descuentos SOcio

        public JsonResult ValidaSocioEmpresarial(decimal bpsid)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    int res = new BLSocioNegocio().ValidaSocioEmpresarial(GlobalVars.Global.OWNER, bpsid);

                    if (res > 0)
                        retorno.result = 1;
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
        #endregion

        public JsonResult ConsultaRucSocio(string ruc, string razsocial)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    Session.Remove(K_SESION_SOCIO_CONSULTA_SOC);
                    Session.Remove(K_SESION_SOCIO_CONSULTA_SOC_SEG);

                    var SocioOrigen = new BLSocioNegocio().ObtenerSocioxRuc(ruc, razsocial);


                    if (SocioOrigen != null)
                    {
                        SocioNegocioList = new List<DTOSocio>();
                        SocioOrigen.ForEach(s =>
                        {
                            int valEst = 0;
                            if (SocioDestinoTmp != null)
                                valEst = SocioDestinoTmp.Where(x => x.Codigo == s.BPS_ID).Count();

                            if (valEst == 0)
                            {
                                SocioNegocioList.Add(new DTOSocio
                                {
                                    Codigo = s.BPS_ID,
                                    Nombres = s.BPS_NAME
                                });
                            }
                        });
                        SocioConsultaTmp = SocioNegocioList;


                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(SocioConsultaTmp, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarConsultaEstablecimientoSocioEmpresarial", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarSocioA()
        {
            listar = SocioConsultaTmp;
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblSocioA' border=0 width='100%;' class='k-grid k-widget' id='tblSocioNegocioA'>");
                shtml.Append("<thead><tr>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >#</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>ID</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Nombre</th>");

                if (listar != null)
                {
                    foreach (var item in listar.OrderBy(x => x.Codigo))
                    {                                                                                                                  //<input type='checkbox' name='chkorigen{0}' value='chkorigen{0}'/></td>"
                        shtml.Append("<tr style='background-color:white'>");
                        shtml.AppendFormat("<td style='width:2.5px; cursor:pointer;text-align:right; width:2.5px';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Check' />", "chkSocioOrigen" + item.Codigo);
                        shtml.AppendFormat("<td style='width:20px;cursor:pointer;text-align:right'; class='IDSocioOri'>{0}</td>", item.Codigo);
                        shtml.AppendFormat("<td style='width:150px;cursor:pointer;text-align:left';'class='NomSocioOri'>{0}</td>", item.Nombres);
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
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarFactConsulta", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarSocioB()
        {
            listar = SocioDestinoTmp;
            if (listar == null)
                listar = new List<DTOSocio>();
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblSocioB' border=0 width='100%;' class='k-grid k-widget' id='tblSocioNegocioB'>");
                shtml.Append("<thead><tr>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >#</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>ID</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Nombre</th>");
                if (listar != null)
                {
                    foreach (var item in listar.OrderBy(x => x.Codigo))
                    {
                        shtml.Append("<tr style='background-color:white'>");
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Check' />", "chkSocioDes" + item.Codigo);
                        shtml.AppendFormat("<td style='width:20%;cursor:pointer;text-align:right;background-color:none'; class='IDSocioDes'>{0}</td>", item.Codigo);
                        shtml.AppendFormat("<td style='width:150%;cursor:pointer;text-align:left'; 'class='NomSocioDes'>{0}</td>", item.Nombres);
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
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarFactConsulta", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SocioArmaTemporalesOriginal(List<DTOSocio> ReglaValor)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ReglaValor != null)
                    {
                        listarSocioSeg = ReglaValor;
                    }
                    else
                    {
                        listarSocioSeg = SocioDestinoTmp;
                    }
                    //Arma la segunda lista origen

                    foreach (var item in listarSocioSeg.OrderBy(x => x.Codigo))
                    {
                        foreach (var item2 in SocioConsultaTmp.Where(x => x.Codigo == item.Codigo))
                        {
                            if (SocioDestinoTmp == null)
                            {
                                SocioDestinoTmp = new List<DTOSocio>();
                            }
                            if (item2 != null)
                            {
                                SocioDestinoTmp.Add(new DTOSocio
                                {
                                    Codigo = item2.Codigo,
                                    Nombres = item2.Nombres
                                });
                            }
                        }
                    }
                    //Arma  La Primera Lista Origen
                    foreach (var item in listarSocioSeg.OrderBy(x => x.Codigo))
                    {
                        SocioConsultaTmp = SocioConsultaTmp.Where(x => x.Codigo != item.Codigo).ToList();
                    }
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(SocioConsultaTmp, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "SocioArmaTemporalesOriginal", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //Elimina  Elementos seleccionados de la lista Destino
        public JsonResult SocioArmaTemporalesDestino(List<DTOSocio> ReglaValor)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    listarSocioSeg = ReglaValor;
                    //Arma  La Primera Lista Origen
                    foreach (var item in listarSocioSeg.OrderBy(x => x.Codigo))
                    {
                        if (SocioConsultaTmp == null)
                        {
                            SocioConsultaTmp = new List<DTOSocio>();
                        }
                        if (item != null)
                        {
                            SocioConsultaTmp.Add(new DTOSocio
                            {
                                Codigo = item.Codigo,
                                Nombres = SocioDestinoTmp.Where(x => x.Codigo == item.Codigo).FirstOrDefault().Nombres
                            });
                        }
                    }

                    //Arma la segunda lista origen
                    foreach (var item in listarSocioSeg.OrderBy(x => x.Codigo))
                    {
                        if (SocioDestinoTmp != null)
                        {
                            SocioDestinoTmp = SocioDestinoTmp.Where(x => x.Codigo != item.Codigo).ToList();
                        }
                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(SocioDestinoTmp, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "SocioArmaTemporalesDestino", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgruparSocio(List<SocioNegocio> ReglaValor)
        {
            Resultado retorno = new Resultado();
            try
            {
                List<SocioNegocio> ListSocioB = new List<SocioNegocio>();
                var exito = false;

                if (SocioDestinoTmp != null)
                {
                    SocioDestinoTmp.ForEach(x => { ListSocioB.Add(new SocioNegocio { BPS_ID = x.Codigo, BPS_VERIFICADO = true, LOG_USER_VERIFICADO = UsuarioActual }); });
                }

                var IdOrigen = ReglaValor.FirstOrDefault().BPS_ID;

                exito = new BLSocioNegocio().AgruparSocio(GlobalVars.Global.OWNER, ListSocioB, IdOrigen);

                if (exito)
                {
                    SocioDestinoTmp.Clear();
                    retorno.result = 1;
                    retorno.message = "El Socio de Negocio ha sido agrupado exitosamente.";
                }
                else
                {
                    retorno.result = 1;
                    retorno.message = "La Agrupación de Socio no ha sido satisfactoria.";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Agrupar Socio", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}
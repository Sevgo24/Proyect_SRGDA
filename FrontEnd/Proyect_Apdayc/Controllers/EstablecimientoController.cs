using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.BL.Reporte;
using SGRDA.Entities;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;
using System.Web;

namespace Proyect_Apdayc.Controllers
{
    public class EstablecimientoController : Base
    {
        #region varialbles log
        const string NomAplicacion = "SGRDA";
        #endregion

        #region variables de sesion
        private const string K_SESION_DIRECCION_EST_ALL = "___DTODirecciones_EST_ALL";
        private const string K_SESION_DIRECCION_EST = "___DTODirecciones_EST";
        private const string K_SESION_DIRECCION_DEL_EST = "___DTODireccionesDEL_EST";
        private const string K_SESION_DIRECCION_ACT_EST = "___DTODireccionesACT_EST";

        private const string K_SESSION_OBSERVACION_EST = "___DTOObservacion_EST";
        private const string K_SESSION_OBSERVACION_DEL_EST = "___DTOObservacionDEL_EST";
        private const string K_SESSION_OBSERVACION_ACT_EST = "___DTOObservacionACT_EST";

        private const string K_SESSION_DOCUMENTO_EST = "___DTODocumento_EST";
        private const string K_SESSION_DOCUMENTO_DEL_EST = "___DTODocumentoDEL_EST";
        private const string K_SESSION_DOCUMENTO_ACT_EST = "___DTODocumentoACT_EST";

        private const string K_SESSION_PARAMETRO_EST = "___DTOParametro_EST";
        private const string K_SESSION_PARAMETRO_DEL_EST = "___DTOParametroDEL_EST";
        private const string K_SESSION_PARAMETRO_ACT_EST = "___DTOParametroACT_EST";

        private const string K_SESSION_CARACTERISTICA_EST = "___DTOCaracteristica_EST";
        private const string K_SESSION_CARACTERISTICA_DEL_EST = "___DTOCaracteristicaDEL_EST";
        private const string K_SESSION_CARACTERISTICA_ACT_EST = "___DTOCaracteristicaACT_EST";
        private const string K_SESSION_CARACTERISTICA_AUT_EST = "__DTOPCaractersiticaAUT_EST";

        private const string K_SESSION_ASOCIADO_EST = "___DTOAsociadoUD_EST";
        private const string K_SESSION_ASOCIADO_DEL_EST = "___DTOAsociadoDELUD_EST";
        private const string K_SESSION_ASOCIADO_ACT_EST = "___DTOAsociadoACTUD_EST";

        private const string K_SESSION_DIVISIONES_EST = "___DTODivisionesUD_EST";
        private const string K_SESSION_DIVISIONES_DEL_EST = "___DTODivisionesDELUD_EST";
        private const string K_SESSION_DIVISIONES_ACT_EST = "___DTODivisionesACTUD_EST";

        private const string K_SESSION_DIFUSION_EST = "___DTODifusionUD_EST";
        private const string K_SESSION_DIFUSION_DEL_EST = "___DTODifusionDELUD_EST";
        private const string K_SESSION_DIFUSION_ACT_EST = "___DTODifusionACTUD_EST";
        //Sesiones para Cadenas
        public static string K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP = "__DTOEstablecimientoSocioEmpresarialTmp";
        public static string K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP_SEG = "__DTOEstablecimientoSocioEmpresarialSegTmp";
        //sesion para Editar establecimientos
        public static string K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP_SEG_EDITAR = "__DTOEstablecimientoSocioEmpresarialSegTmpEditar";

        private const string K_SESSION_CORREO = "___DTOCorreoUD";
        private const string K_SESSION_CORREO_DEL = "___DTOCorreoDELUD";
        private const string K_SESSION_CORREO_ACT = "___DTOCorreoACTUD";

        private const string K_SESSION_TELEFONO = "___DTOTelefonoUD";
        private const string K_SESSION_TELEFONO_DEL = "___DTOTelefonoDELUD";
        private const string K_SESSION_TELEFONO_ACT = "___DTOTelefonoACTUD";

        private const string K_SESSION_REDSOCIAL = "___DTORedSocialUD";
        private const string K_SESSION_REDSOCIAL_DEL = "___DTORedSocialDELUD";
        private const string K_SESSION_REDSOCIAL_ACT = "___DTORedSocialACTUD";

        #endregion
        //
        // GET: /Establecimiento/

        #region DTO set y get
        List<DTODireccion> direcciones = new List<DTODireccion>();
        List<DTOObservacion> observaciones = new List<DTOObservacion>();
        List<DTODocumento> documentos = new List<DTODocumento>();
        List<DTOParametro> parametros = new List<DTOParametro>();
        List<DTOCaracteristica> caracteristicas = new List<DTOCaracteristica>();
        List<DTOAsociado> asociados = new List<DTOAsociado>();
        List<DTODivisiones> divisiones = new List<DTODivisiones>();
        List<DTODifusion> difusion = new List<DTODifusion>();
        List<DTODireccion> ListaDirecciones = new List<DTODireccion>();
        List<DTOEstablecimiento> ConEstablecimientoSocEmp = new List<DTOEstablecimiento>();
        List<DTOEstablecimiento> listar = new List<DTOEstablecimiento>();
        List<DTOEstablecimiento> listarEstSeg = new List<DTOEstablecimiento>();

        List<DTOTelefono> telefonos = new List<DTOTelefono>();
        List<DTOCorreo> correos = new List<DTOCorreo>();
        List<DTORedSocial> redsocial = new List<DTORedSocial>();
        //lista necesaria para la modificacion automatica de caractersiticas por periodo
        //List<DTOCaracteristica> listaCaracteristicaTmp = new List<DTOCaracteristica>();
        int r = 0;

        private const string K_SESION_ESTABLECIMIENTO = "___DTOEstablecimiento";
        DTOEstablecimiento establecimiento = new DTOEstablecimiento();
        public DTOEstablecimiento EstablecimientoTmp
        {
            get
            {
                return (DTOEstablecimiento)Session[K_SESION_ESTABLECIMIENTO];
            }
            set
            {
                Session[K_SESION_ESTABLECIMIENTO] = value;
            }
        }


        private List<DTODireccion> DireccionesTmpUPDEstado
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION_ACT_EST];
            }
            set
            {
                Session[K_SESION_DIRECCION_ACT_EST] = value;
            }
        }
        private List<DTODireccion> DireccionesTmpDelBD
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION_DEL_EST];
            }
            set
            {
                Session[K_SESION_DIRECCION_DEL_EST] = value;
            }
        }
        public List<DTODireccion> DireccionesTmp
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION_EST];
            }
            set
            {
                Session[K_SESION_DIRECCION_EST] = value;
            }
        }

        public List<DTODireccion> DireccionesTmpAll
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION_EST_ALL];
            }
            set
            {
                Session[K_SESION_DIRECCION_EST_ALL] = value;
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

        private List<DTOObservacion> ObservacionesTmpUPDEstado
        {
            get
            {
                return (List<DTOObservacion>)Session[K_SESSION_OBSERVACION_ACT_EST];
            }
            set
            {
                Session[K_SESSION_OBSERVACION_ACT_EST] = value;
            }
        }
        private List<DTOObservacion> ObservacionesTmpDelBD
        {
            get
            {
                return (List<DTOObservacion>)Session[K_SESSION_OBSERVACION_DEL_EST];
            }
            set
            {
                Session[K_SESSION_OBSERVACION_DEL_EST] = value;
            }
        }
        public List<DTOObservacion> ObservacionesTmp
        {
            get
            {
                return (List<DTOObservacion>)Session[K_SESSION_OBSERVACION_EST];
            }
            set
            {
                Session[K_SESSION_OBSERVACION_EST] = value;
            }
        }

        private List<DTODocumento> DocumentosTmpUPDEstado
        {
            get
            {
                return (List<DTODocumento>)Session[K_SESSION_DOCUMENTO_ACT_EST];
            }
            set
            {
                Session[K_SESSION_DOCUMENTO_ACT_EST] = value;
            }
        }
        private List<DTODocumento> DocumentosTmpDelBD
        {
            get
            {
                return (List<DTODocumento>)Session[K_SESSION_DOCUMENTO_DEL_EST];
            }
            set
            {
                Session[K_SESSION_DOCUMENTO_DEL_EST] = value;
            }
        }
        public List<DTODocumento> DocumentosTmp
        {
            get
            {
                return (List<DTODocumento>)Session[K_SESSION_DOCUMENTO_EST];
            }
            set
            {
                Session[K_SESSION_DOCUMENTO_EST] = value;
            }
        }

        private List<DTOParametro> ParametrosTmpUPDEstado
        {
            get
            {
                return (List<DTOParametro>)Session[K_SESSION_PARAMETRO_ACT_EST];
            }
            set
            {
                Session[K_SESSION_PARAMETRO_ACT_EST] = value;
            }
        }
        private List<DTOParametro> ParametrosTmpDelBD
        {
            get
            {
                return (List<DTOParametro>)Session[K_SESSION_PARAMETRO_DEL_EST];
            }
            set
            {
                Session[K_SESSION_PARAMETRO_DEL_EST] = value;
            }
        }
        public List<DTOParametro> ParametrosTmp
        {
            get
            {
                return (List<DTOParametro>)Session[K_SESSION_PARAMETRO_EST];
            }
            set
            {
                Session[K_SESSION_PARAMETRO_EST] = value;
            }
        }

        private List<DTOCaracteristica> CaracteristicaTmpUPDEstado
        {
            get
            {
                return (List<DTOCaracteristica>)Session[K_SESSION_CARACTERISTICA_ACT_EST];
            }
            set
            {
                Session[K_SESSION_CARACTERISTICA_ACT_EST] = value;
            }
        }
        private List<DTOCaracteristica> CaracteristicaTmpDelBD
        {
            get
            {
                return (List<DTOCaracteristica>)Session[K_SESSION_CARACTERISTICA_DEL_EST];
            }
            set
            {
                Session[K_SESSION_CARACTERISTICA_DEL_EST] = value;
            }
        }
        public List<DTOCaracteristica> CaracteristicaTmp
        {
            get
            {
                return (List<DTOCaracteristica>)Session[K_SESSION_CARACTERISTICA_EST];
            }
            set
            {
                Session[K_SESSION_CARACTERISTICA_EST] = value;
            }
        }


        private List<DTOAsociado> AsociadosTmpUPDEstado
        {
            get
            {
                return (List<DTOAsociado>)Session[K_SESSION_ASOCIADO_ACT_EST];
            }
            set
            {
                Session[K_SESSION_ASOCIADO_ACT_EST] = value;
            }
        }
        private List<DTOAsociado> AsociadosTmpDelBD
        {
            get
            {
                return (List<DTOAsociado>)Session[K_SESSION_ASOCIADO_DEL_EST];
            }
            set
            {
                Session[K_SESSION_ASOCIADO_DEL_EST] = value;
            }
        }
        public List<DTOAsociado> AsociadosTmp
        {
            get
            {
                return (List<DTOAsociado>)Session[K_SESSION_ASOCIADO_EST];
            }
            set
            {
                Session[K_SESSION_ASOCIADO_EST] = value;
            }
        }

        private List<DTODivisiones> DivisionesTmpUPDEstado
        {
            get
            {
                return (List<DTODivisiones>)Session[K_SESSION_DIVISIONES_ACT_EST];
            }
            set
            {
                Session[K_SESSION_DIVISIONES_ACT_EST] = value;
            }
        }
        private List<DTODivisiones> DivisionesTmpDelBD
        {
            get
            {
                return (List<DTODivisiones>)Session[K_SESSION_DIVISIONES_DEL_EST];
            }
            set
            {
                Session[K_SESSION_DIVISIONES_DEL_EST] = value;
            }
        }
        public List<DTODivisiones> DivisionesTmp
        {
            get
            {
                return (List<DTODivisiones>)Session[K_SESSION_DIVISIONES_EST];
            }
            set
            {
                Session[K_SESSION_DIVISIONES_EST] = value;
            }
        }

        private List<DTODifusion> DifusionTmpUPDEstado
        {
            get
            {
                return (List<DTODifusion>)Session[K_SESSION_DIFUSION_ACT_EST];
            }
            set
            {
                Session[K_SESSION_DIFUSION_ACT_EST] = value;
            }
        }
        private List<DTODifusion> DifusionTmpDelBD
        {
            get
            {
                return (List<DTODifusion>)Session[K_SESSION_DIFUSION_DEL_EST];
            }
            set
            {
                Session[K_SESSION_DIFUSION_DEL_EST] = value;
            }
        }
        public List<DTODifusion> DifusionTmp
        {
            get
            {
                return (List<DTODifusion>)Session[K_SESSION_DIFUSION_EST];
            }
            set
            {
                Session[K_SESSION_DIFUSION_EST] = value;
            }
        }
        //temporales
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
        //public K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP_SEG_EDITAR 
        public List<DTOEstablecimiento> EstablecimientoSocioEmpresarialDestinoEditarTmp
        {
            get
            {
                return (List<DTOEstablecimiento>)Session[K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP_SEG_EDITAR];
            }
            set
            {
                Session[K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP_SEG_EDITAR] = value;
            }
        }

        #endregion

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Edit()
        {
            Init(false);
            establecimiento.Nuevo = false;
            if (EstablecimientoTmp == null) EstablecimientoTmp = new DTOEstablecimiento();
            EstablecimientoTmp.Nuevo = establecimiento.Nuevo;
            return View();
        }

        public ActionResult Create()
        {
            Init(false);
            establecimiento.Nuevo = true;
            if (EstablecimientoTmp == null) EstablecimientoTmp = new DTOEstablecimiento();
            EstablecimientoTmp.Nuevo = establecimiento.Nuevo;

            Session.Remove(K_SESION_DIRECCION_EST);
            Session.Remove(K_SESION_DIRECCION_ACT_EST);
            Session.Remove(K_SESION_DIRECCION_DEL_EST);

            Session.Remove(K_SESSION_OBSERVACION_EST);
            Session.Remove(K_SESSION_OBSERVACION_ACT_EST);
            Session.Remove(K_SESSION_OBSERVACION_DEL_EST);

            Session.Remove(K_SESSION_DOCUMENTO_EST);
            Session.Remove(K_SESSION_DOCUMENTO_ACT_EST);
            Session.Remove(K_SESSION_DOCUMENTO_DEL_EST);

            Session.Remove(K_SESSION_PARAMETRO_EST);
            Session.Remove(K_SESSION_PARAMETRO_ACT_EST);
            Session.Remove(K_SESSION_PARAMETRO_DEL_EST);

            Session.Remove(K_SESSION_CORREO);
            Session.Remove(K_SESSION_CORREO_ACT);
            Session.Remove(K_SESSION_CORREO_DEL);

            Session.Remove(K_SESSION_TELEFONO);
            Session.Remove(K_SESSION_TELEFONO_ACT);
            Session.Remove(K_SESSION_TELEFONO_DEL);

            Session.Remove(K_SESSION_REDSOCIAL);
            Session.Remove(K_SESSION_REDSOCIAL_ACT);
            Session.Remove(K_SESSION_REDSOCIAL_DEL);

            Session.Remove(K_SESSION_CARACTERISTICA_EST);
            Session.Remove(K_SESSION_CARACTERISTICA_ACT_EST);
            Session.Remove(K_SESSION_CARACTERISTICA_DEL_EST);
            Session.Remove(K_SESSION_CARACTERISTICA_AUT_EST);

            Session.Remove(K_SESSION_ASOCIADO_EST);
            Session.Remove(K_SESSION_ASOCIADO_ACT_EST);
            Session.Remove(K_SESSION_ASOCIADO_DEL_EST);

            Session.Remove(K_SESSION_DIVISIONES_EST);
            Session.Remove(K_SESSION_DIVISIONES_ACT_EST);
            Session.Remove(K_SESSION_DIVISIONES_DEL_EST);

            Session.Remove(K_SESSION_DIFUSION_EST);
            Session.Remove(K_SESSION_DIFUSION_ACT_EST);
            Session.Remove(K_SESSION_DIFUSION_DEL_EST);
            Session.Remove(K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP);
            Session.Remove(K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP_SEG);
            Session.Remove(K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP_SEG_EDITAR);


            return View();
        }

        #region Lista Paginacion

        public JsonResult Listar_Establecimiento_Principal(int skip, int take, int page, int pageSize, string group, decimal tipoEst, decimal? subTipoEst, decimal? IdEstablecimiento, string nombre, int st, decimal bpsId,
            decimal division,decimal subtipo1,decimal subtipo2, decimal subtipo3)
        {
            Resultado retorno = new Resultado();
            var lista = new BLEstablecimiento().Listar_Establecimiento_Principal(GlobalVars.Global.OWNER, tipoEst, subTipoEst, IdEstablecimiento, nombre, st, bpsId,
                division,  subtipo1,  subtipo2,  subtipo3,       page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
                return Json(new BEEstablecimiento { Establecimiento = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            else
                return Json(new BEEstablecimiento { Establecimiento = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult usp_listar_PorSocioNegocioJson(int skip, int take, int page, int pageSize, string group, decimal? IdSocio, int st)
        {
            Resultado retorno = new Resultado();

            var lista = usp_Get_PorSocioNegocioPage(GlobalVars.Global.OWNER, IdSocio, st, page, pageSize);

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

        public JsonResult usp_listar_PorEstablecimientoJson(int skip, int take, int page, int pageSize, string group, decimal tipoEst, decimal? subTipoEst, decimal? IdEstablecimiento, string nombre, int st)
        {
            Resultado retorno = new Resultado();

            var lista = usp_Get_PorEstablecimientoPage(GlobalVars.Global.OWNER, tipoEst, subTipoEst, IdEstablecimiento, nombre, st, page, pageSize);

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

        public JsonResult usp_listar_PorDivisionAdministrativaJson(int skip, int take, int page, int pageSize, string group, string owner, string divTipo, decimal? divAdmin, int st)
        {
            Resultado retorno = new Resultado();

            var lista = usp_Get_DivisionAdministrativaPage(GlobalVars.Global.OWNER, divTipo, divAdmin, st, page, pageSize);

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

        public JsonResult usp_listar_LicenciasJson(int skip, int take, int page, int pageSize, string owner, decimal EstablecimientoId)
        {
            Resultado retorno = new Resultado();
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            var lista = usp_Get_LicenciaPage(GlobalVars.Global.OWNER, EstablecimientoId, Convert.ToDecimal(oficina), page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BELicencias { ListaLicencias = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BELicencias { ListaLicencias = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult usp_listar_PorDireccionJson(int skip, int take, int page, int pageSize, string group, string NombreViaDir, decimal tipodireccion, decimal TipoUrbanizacion, string NombreUrbanizacion, string Manzana, decimal Numero, string Lote, string TipoInterior, string NumeroInterior, string CodigoViaDir, decimal TipoEtapa, string NombreEtapa, decimal TerritorioDir, string ReferenciaDir, decimal? ubigeo, int st)
        {
            Resultado retorno = new Resultado();

            var lista = usp_Get_PorDireccionesPage(GlobalVars.Global.OWNER, NombreViaDir, tipodireccion, TipoUrbanizacion, NombreUrbanizacion, Manzana, Numero, Lote, TipoInterior, NumeroInterior, CodigoViaDir, TipoEtapa, NombreEtapa, TerritorioDir, ReferenciaDir, ubigeo, st, page, pageSize);

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

        public List<BEEstablecimiento> usp_Get_PorSocioNegocioPage(string Owner, decimal? IdSocio, int st, int pagina, int cantRegxPag)
        {
            return new BLEstablecimiento().usp_Get_PorSocioNegocioPage(Owner, IdSocio, st, pagina, cantRegxPag);
        }

        public List<BEEstablecimiento> usp_Get_PorEstablecimientoPage(string Owner, decimal tipoEst, decimal? subTipoEst, decimal? IdEstablecimiento, string nombre, int st, int pagina, int cantRegxPag)
        {
            return new BLEstablecimiento().usp_Get_PorEstablecimientoPage(Owner, tipoEst, subTipoEst, IdEstablecimiento, nombre, st, pagina, cantRegxPag);
        }

        public List<BEEstablecimiento> usp_Get_DivisionAdministrativaPage(string owner, string divTipo, decimal? divAdmin, int st, int pagina, int cantRegxPag)
        {
            return new BLEstablecimiento().usp_Get_DivisionAdministrativaPage(owner, divTipo, divAdmin, st, pagina, cantRegxPag);
        }

        public List<BEEstablecimiento> usp_Get_PorDireccionesPage(string Owner, string NombreViaDir, decimal tipodireccion, decimal TipoUrbanizacion, string NombreUrbanizacion, string Manzana, decimal? Numero, string Lote, string TipoInterior, string NumeroInterior, string CodigoViaDir, decimal TipoEtapa, string NombreEtapa, decimal TerritorioDir, string ReferenciaDir, decimal? ubigeo, int st, int pagina, int cantRegxPag)
        {
            return new BLEstablecimiento().usp_Get_PorDireccionesPage(Owner, NombreViaDir, tipodireccion, TipoUrbanizacion, NombreUrbanizacion, Manzana, Numero, Lote, TipoInterior, NumeroInterior, CodigoViaDir, TipoEtapa, NombreEtapa, TerritorioDir, ReferenciaDir, ubigeo, st, pagina, cantRegxPag);
        }

        public List<BELicencias> usp_Get_LicenciaPage(string owner, decimal? EstablecimientoId, decimal Off_Id, int pagina, int cantRegxPag)
        {
            return new BLEstablecimiento().usp_Get_LicenciaPage(owner, EstablecimientoId, Off_Id, pagina, cantRegxPag);
        }

        public JsonResult ConsultaGeneralEstablecimiento(int skip, int take, int page, int pageSize, string group, decimal Tipoestablecimiento, decimal? SubTipoestableimiento, string Nombreestablecimiento, decimal Socio, string Tipodivision, decimal? Division, int estado)
        {
            Resultado retorno = new Resultado();


            var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));




            if (opcAdm == 1) // SI ES OFICINA CON PERMISOS , MANDAR 0 PARA VISUALIZAR TODO
                oficina = 0;

            var lista = new BLEstablecimiento().ConsultaGeneralEstablecimiento(Tipoestablecimiento, SubTipoestableimiento, Nombreestablecimiento, Socio, Tipodivision, Division, estado, oficina, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEEstablecimiento { Establecimiento = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEEstablecimiento { Establecimiento = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        //(int skip, int take, int page, int pageSize, string group, decimal Tipoestablecimiento, decimal? SubTipoestableimiento, string Nombreestablecimiento, decimal Socio, string Tipodivision, decimal? Division, int estado)
        public JsonResult ConsulaEstablecimientoSocioEmpr(int skip, int take, int page, int pageSize, string group, decimal Socio)
        {
            Resultado retorno = new Resultado();

            var lista = new BLEstablecimiento().ConsultaEstablecimientoSocioEmpr(Socio, page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();

            if (tot.Count == 0)
            {
                return Json(new BEEstablecimiento { Establecimiento = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEEstablecimiento { Establecimiento = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        //AGREGAR
        #region Agregar
        [HttpPost]
        public JsonResult AddDireccion(DTODireccion direccion)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    direcciones = DireccionesTmp;

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

                        if (item.EsPrincipal == "0")
                            direccion.EsPrincipal = "0";
                        else
                            direccion.EsPrincipal = item.EsPrincipal;

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
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "AddDireccion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddObservacion(DTOObservacion entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
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
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "AddObservacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddDocumento(DTODocumento documento)
        {
            Resultado retorno = new Resultado();
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
                        documentos.Add(documento);
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
                    }
                    DocumentosTmp = documentos;

                    retorno.result = 1;
                    retorno.Code = Convert.ToInt32(documento.Id);
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "AddDocumento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public Resultado AddDoc(DTODocumento documento)
        {
            Resultado retorno = new Resultado();
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
                        documentos.Add(documento);
                    }
                    else
                    {
                        var item = documentos.Where(x => x.Id == documento.Id).FirstOrDefault();
                        documento.EnBD = item.EnBD;//indicador que item viene de la BD
                        documento.Activo = item.Activo;
                        documento.UsuarioCrea = item.UsuarioCrea;
                        documento.FechaCrea = item.FechaCrea;
                        if (documento.EnBD)
                        {
                            documento.UsuarioModifica = UsuarioActual;
                            documento.FechaModifica = DateTime.Now;
                        }
                        documentos.Remove(item);
                        documentos.Add(documento);
                    }
                    DocumentosTmp = documentos;

                    retorno.result = 1;
                    retorno.Code = Convert.ToInt32(documento.Id);
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "AddDoc", ex);
            }
            return retorno;/// Json(retorno, JsonRequestBehavior.AllowGet);
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ActualizarNombreDocTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddParametro(DTOParametro entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
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
            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "AddParametro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddCaracteristica(DTOCaracteristica entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (entidad.GetTipo)
                    {
                        var lista = from a in CaracteristicaTmp
                                    where a.caracteristica == entidad.caracteristica
                                    select a;

                        if (lista.Count() > 0)
                        {
                            retorno.result = 1;
                            return Json(retorno, JsonRequestBehavior.AllowGet);
                        }
                    }

                    caracteristicas = CaracteristicaTmp;
                    if (caracteristicas == null) caracteristicas = new List<DTOCaracteristica>();

                    if (Convert.ToInt32(entidad.Id) <= 0)
                    {
                        decimal nuevoId = 1;
                        if (caracteristicas.Count > 0) nuevoId = caracteristicas.Max(x => x.Id) + 1;
                        entidad.Id = nuevoId;
                        entidad.Activo = true;
                        entidad.EnBD = false;
                        entidad.UsuarioCrea = UsuarioActual;
                        entidad.FechaCrea = DateTime.Now;
                        caracteristicas.Add(entidad);
                    }
                    else
                    {
                        //var item = caracteristicas.Where(x => x.Idcaracteristica == entidad.Idcaracteristica).FirstOrDefault();
                        var item = caracteristicas.Where(x => x.Id == entidad.Id).FirstOrDefault();
                        entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                        entidad.Activo = item.Activo;
                        if (item.GetTipo)
                            entidad.GetTipo = true;

                        entidad.UsuarioCrea = item.UsuarioCrea;
                        entidad.FechaCrea = item.FechaCrea;
                        if (entidad.EnBD)
                        {
                            entidad.UsuarioModifica = UsuarioActual;
                            entidad.FechaModifica = DateTime.Now;
                        }

                        caracteristicas.Remove(item);
                        caracteristicas.Add(entidad);
                    }
                    CaracteristicaTmp = caracteristicas;
                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "AddCaracteristica", ex);
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
        public JsonResult AddAsociado(DTOAsociado entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                asociados = AsociadosTmp;
                if (asociados == null) asociados = new List<DTOAsociado>();
                if (Convert.ToInt32(entidad.Id) <= 0)
                {
                    decimal nuevoId = 1;
                    if (asociados.Count > 0) nuevoId = asociados.Max(x => x.Id) + 1;
                    entidad.Id = nuevoId;
                    entidad.Activo = true;
                    entidad.EnBD = false;
                    entidad.EsPrincipal = asociados.Count == 0 ? true : false;
                    entidad.UsuarioCrea = UsuarioActual;
                    entidad.FechaCrea = DateTime.Now;
                    asociados.Add(entidad);
                }
                else
                {
                    var item = asociados.Where(x => x.Id == entidad.Id).FirstOrDefault();
                    entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                    entidad.Activo = item.Activo;
                    entidad.UsuarioCrea = item.UsuarioCrea;

                    if (item.EsPrincipal == false)
                        entidad.EsPrincipal = false;
                    else
                        entidad.EsPrincipal = item.EsPrincipal;

                    entidad.FechaCrea = item.FechaCrea;
                    if (entidad.EnBD)
                    {
                        entidad.UsuarioModifica = UsuarioActual;
                        entidad.FechaModifica = DateTime.Now;
                    }
                    asociados.Remove(item);
                    asociados.Add(entidad);
                }
                AsociadosTmp = asociados;
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

        //[HttpPost]
        //public JsonResult AddDivision(DTODivisiones entidad)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            divisiones = DivisionesTmp;

        //            if (divisiones != null)
        //            {
        //                //Esta validaciòn solo valida que no se repita el valor de la division
        //                //var lista = from a in DivisionesTmp
        //                //            where a.idDivisionValor == entidad.idDivisionValor
        //                //            select a;

        //                if (entidad.idTipoDivision == "ADM")
        //                {
        //                    //Esta validaciòn solo valida que no se repita el tipo de division
        //                    var lista = from a in DivisionesTmp
        //                                //where (a.idTipoDivision == entidad.idTipoDivision && a.Activo == true)
        //                                where (a.idTipoDivision == "ADM" && a.Activo == true)
        //                                select a;

        //                    if (lista.Count() >= 1)
        //                    {
        //                        retorno.result = 0;
        //                        //retorno.message = "La división seleccionada ya ha sido ingresada";
        //                        retorno.message = "Solo se puede ingresar una división administrativa.";
        //                        return Json(retorno, JsonRequestBehavior.AllowGet);
        //                    }
        //                }
        //            }

        //            if (divisiones == null) divisiones = new List<DTODivisiones>();
        //            if (Convert.ToInt32(entidad.Id) <= 0)
        //            {
        //                decimal nuevoId = 1;
        //                if (divisiones.Count > 0) nuevoId = divisiones.Max(x => x.Id) + 1;
        //                entidad.Id = nuevoId;
        //                entidad.Activo = true;
        //                entidad.EnBD = false;
        //                entidad.UsuarioCrea = UsuarioActual;
        //                entidad.FechaCrea = DateTime.Now;
        //                divisiones.Add(entidad);
        //            }
        //            else
        //            {
        //                var item = divisiones.Where(x => x.Id == entidad.Id).FirstOrDefault();
        //                entidad.EnBD = item.EnBD;//indicador que item viene de la BD
        //                entidad.Activo = item.Activo;
        //                entidad.UsuarioCrea = item.UsuarioCrea;
        //                entidad.FechaCrea = item.FechaCrea;
        //                if (entidad.EnBD)
        //                {
        //                    entidad.UsuarioModifica = UsuarioActual;
        //                    entidad.FechaModifica = DateTime.Now;
        //                }
        //                divisiones.Remove(item);
        //                divisiones.Add(entidad);
        //            }
        //            DivisionesTmp = divisiones;
        //            retorno.result = 1;
        //            retorno.message = "OK";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "AddDivision", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult AddDifusion(DTODifusion entidad)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            difusion = DifusionTmp;

        //            if (difusion == null) difusion = new List<DTODifusion>();
        //            if (Convert.ToInt32(entidad.Id) <= 0)
        //            {
        //                decimal nuevoId = 1;
        //                if (difusion.Count > 0) nuevoId = difusion.Max(x => x.Id) + 1;
        //                entidad.Id = nuevoId;
        //                entidad.Activo = true;
        //                entidad.EnBD = false;
        //                entidad.UsuarioCrea = UsuarioActual;
        //                entidad.FechaCrea = DateTime.Now;
        //                difusion.Add(entidad);
        //            }
        //            else
        //            {
        //                var item = difusion.Where(x => x.Id == entidad.Id).FirstOrDefault();
        //                entidad.EnBD = item.EnBD;//indicador que item viene de la BD
        //                entidad.Activo = item.Activo;
        //                entidad.UsuarioCrea = item.UsuarioCrea;
        //                entidad.FechaCrea = item.FechaCrea;
        //                if (entidad.EnBD)
        //                {
        //                    entidad.UsuarioModifica = UsuarioActual;
        //                    entidad.FechaModifica = DateTime.Now;
        //                }
        //                difusion.Remove(item);
        //                difusion.Add(entidad);
        //            }
        //            DifusionTmp = difusion;
        //            retorno.result = 1;
        //            retorno.message = "OK";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "AddDifusion", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region Busqueda Autocompletar
        public JsonResult ACBuscarEstablecimiento()
        {
            string texto = Request.QueryString["term"];
            var datos = new BLEstablecimiento().UPS_BUSCAR_ESTABLECIMIENTO_X_NOMBRE(GlobalVars.Global.OWNER, texto);
            List<DTOEstablecimiento> establecimientos = new List<DTOEstablecimiento>();
            datos.ForEach(x =>
            {
                establecimientos.Add(new DTOEstablecimiento
                {
                    Codigo = x.EST_ID,
                    value = String.Format("{0}", x.EST_NAME)
                });
            });
            //return Json(establecimientos, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(establecimientos, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult ACBuscarSocio()
        {
            string texto = Request.QueryString["term"];
            var datos = new BLSocioNegocio().UPS_BUSCAR_SOCIOS_X_RAZONSOCIAL(GlobalVars.Global.OWNER, texto);
            List<DTOSocio> establecimientos = new List<DTOSocio>();
            datos.ForEach(x =>
            {
                establecimientos.Add(new DTOSocio
                {
                    Codigo = x.BPS_ID,
                    value = String.Format("{0}", x.BPS_NAME)
                });
            });
            return Json(establecimientos, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Validacion
        private static void validacion(out bool exito, out string msg_validacion, BEEstablecimiento entidad)
        {
            exito = true;
            msg_validacion = string.Empty;
            if (exito && entidad.ESTT_ID == 0)
            {
                msg_validacion = "Seleccione Tipo de establecimiento";
                exito = false;
            }

            if (exito && entidad.SUBE_ID == 0)
            {
                msg_validacion = "Seleccione Subtipo de establecimiento";
                exito = false;
            }

            if (exito && string.IsNullOrEmpty(entidad.EST_NAME))
            {
                msg_validacion = "Ingrese nombre del establecimiento";
                exito = false;
            }

            //validar direccion???
        }

        private static void validacionEstablecimientoSocio(out bool exito, out string msg_validacion, BEEstablecimiento entidad)
        {
            exito = true;
            msg_validacion = string.Empty;
            if (exito && entidad.BPS_ID == 0)
            {
                msg_validacion = "Seleccione socio de negocio";
                exito = false;
            }

            if (exito && entidad.EST_ID == 0)
            {
                msg_validacion = "No se encuentra establecimiento";
                exito = false;
            }
        }

        private static void validacionObservacionEst(out bool exito, out string msg_validacion, BEObservationEst entidad)
        {
            exito = true;
            msg_validacion = string.Empty;
            if (exito && entidad.OBS_ID == 0)
            {
                msg_validacion = "Seleccione tipo de observación";
                exito = false;
            }

            if (exito && entidad.EST_ID == 0)
            {
                msg_validacion = "No se encuentra establecimiento";
                exito = false;
            }
        }
        #endregion

        [HttpPost]
        public JsonResult Insertar(BEEstablecimiento establecimiento)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (DireccionesTmp != null)
                    {
                        var lista = from a in DireccionesTmp
                                    where (a.Activo == true)
                                    select a;

                        //var contacto = from a in AsociadosTmp
                        //               where (a.EsContacto == "1")
                        //               select a;

                        //if (contacto.Count() > 0)
                        //{
                        if (lista.Count() > 0)
                        {
                            BEEstablecimiento obj = new BEEstablecimiento();
                            obj.EST_ID = establecimiento.EST_ID;
                            obj.EST_NAME = establecimiento.EST_NAME.ToUpper();
                            obj.ESTT_ID = establecimiento.ESTT_ID;
                            obj.DAD_ID = establecimiento.DAD_ID;
                            obj.SUBE_ID = establecimiento.SUBE_ID;
                            obj.BPS_ID = establecimiento.BPS_ID;
                            obj.LOG_USER_CREAT = UsuarioActual;
                            obj.LATITUD = establecimiento.LATITUD;
                            obj.LONGITUD = establecimiento.LONGITUD;

                            obj.Direccion = obtenerDirecciones();
                            obj.Observaciones = obtenerObservaciones();
                            obj.Documentos = obtenerDocumentos();
                            obj.Parametros = obtenerParametros();
                            obj.Caracteristicas = obtenerCaracteristica();
                            obj.Asociados = obtenerAsociados();
                            //obj.Divisiones = obtenerDivisiones();
                            obj.Difusion = obtenerDifusion();

                            //Agregado 24/10/2017
                            obj.Correos = obtenerCorreos();
                            obj.Telefonos = obtenerTelefonos();
                            obj.RedSocial = obtenerRedSocial();

                            obj.OWNER = GlobalVars.Global.OWNER;

                            if (obj.EST_ID == 0)
                            {
                                var datos = new BLEstablecimiento().Insertar(obj);
                            }
                            else
                            {
                                obj.EST_ID = establecimiento.EST_ID;
                                obj.LOG_USER_UPDAT = UsuarioActual;

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
                                //3.setting Documentos eliminar
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

                                //3.setting Caracteristicas eliminar
                                List<BECaracteristicaEst> listaCarDel = null;
                                if (CaracteristicaTmpDelBD != null)
                                {
                                    listaCarDel = new List<BECaracteristicaEst>();
                                    CaracteristicaTmpDelBD.ForEach(x => { listaCarDel.Add(new BECaracteristicaEst { CHAR_ID = x.Id }); });
                                }
                                //setting Caracteristicas activar
                                List<BECaracteristicaEst> listaCarUpdEst = null;
                                if (CaracteristicaTmpUPDEstado != null)
                                {
                                    listaCarUpdEst = new List<BECaracteristicaEst>();
                                    CaracteristicaTmpUPDEstado.ForEach(x => { listaCarUpdEst.Add(new BECaracteristicaEst { CHAR_ID = x.Id }); });
                                }


                                //7.setting Asociados eliminar
                                List<BEAsociadosEst> listaAsoDel = null;
                                if (AsociadosTmpDelBD != null)
                                {
                                    listaAsoDel = new List<BEAsociadosEst>();
                                    AsociadosTmpDelBD.ForEach(x => { listaAsoDel.Add(new BEAsociadosEst { Id = x.Id }); });
                                }
                                //setting Asociados activar
                                List<BEAsociadosEst> listaAsoUpdEst = null;
                                if (AsociadosTmpUPDEstado != null)
                                {
                                    listaAsoUpdEst = new List<BEAsociadosEst>();
                                    AsociadosTmpUPDEstado.ForEach(x => { listaAsoUpdEst.Add(new BEAsociadosEst { Id = x.Id }); });
                                }

                                /*
                                List<BEDivisionesEst> listaDivDel = null;
                                if (DivisionesTmpDelBD != null)
                                {
                                    listaDivDel = new List<BEDivisionesEst>();
                                    DivisionesTmpDelBD.ForEach(x => { listaDivDel.Add(new BEDivisionesEst { Id = x.Id }); });
                                }
                                List<BEDivisionesEst> listaDivUpdEst = null;
                                if (DivisionesTmpUPDEstado != null)
                                {
                                    listaDivUpdEst = new List<BEDivisionesEst>();
                                    DivisionesTmpUPDEstado.ForEach(x => { listaDivUpdEst.Add(new BEDivisionesEst { Id = x.Id }); });
                                }
                                */

                                List<BEDifusionEst> listaDifDel = null;
                                if (DifusionTmpDelBD != null)
                                {
                                    listaDifDel = new List<BEDifusionEst>();
                                    DifusionTmpDelBD.ForEach(x => { listaDifDel.Add(new BEDifusionEst { SEQUENCE = x.Id }); });
                                }
                                List<BEDifusionEst> listaDifUpdEst = null;
                                if (DifusionTmpUPDEstado != null)
                                {
                                    listaDifUpdEst = new List<BEDifusionEst>();
                                    DifusionTmpUPDEstado.ForEach(x => { listaDifUpdEst.Add(new BEDifusionEst { SEQUENCE = x.Id }); });
                                }


                                //Actualizando todos las Caractersiticas del establecimiento (VALIDAR LUEGO QUE SOLO LO HAGA CUANDO LAS LISTAS
                                //TEMPORALES DE CARACTERSITICAS ANTERIORES SEAN DIFERENTES)
                                listaCaractersiticaLicencia(obj.EST_ID, obj.ESTT_ID, obj.SUBE_ID);//id_establecimiento,tipo de estb, sub tipo de estab*
                                // listaCaractersiticaLicencia(obj.EST_ID);

                                var datos = new BLEstablecimiento().Update(obj,
                                                                            listaDirDel, listaDirUpdEst,
                                                                            listaObsDel, listaObsUpdEst,
                                                                            listaDocDel, listaDocUpdEst,
                                                                            listaParDel, listaParUpdEst,
                                                                            listaTelDel, listaTelUpdEst,
                                                                            listaMailDel, listaMailUpdEst,
                                                                            listaRedSocialDel, listaRedSocialUpdEst,
                                                                            listaCarDel, listaCarUpdEst,
                                                                            listaAsoDel, listaAsoUpdEst,
                                                                            //listaDivDel, listaDivUpdEst,
                                                                            listaDifDel, listaDifUpdEst);
                            }
                            retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                            retorno.result = 1;
                        }
                        else
                        {
                            retorno.message = "No se puede grabar. Debe agregar una dirección activa.";
                            retorno.result = 0;
                        }
                        //}
                        //else
                        //{
                        //    retorno.message = "No se puede grabar. Debe seleccionar un contacto, pestaña entidades asociadas.";
                        //    retorno.result = 0;
                        //}
                    }
                    else
                    {
                        retorno.message = "No se puede grabar. Debe agregar una dirección activa.";
                        retorno.result = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "insert establecimiento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Obtiene(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    //validacion session nuevo 
                    if (id != 0)
                        EstablecimientoTmp.Nuevo = false;

                    BLEstablecimiento servicio = new BLEstablecimiento();
                    var establecimiento = servicio.Obtiene(id, GlobalVars.Global.OWNER, Constantes.ENTIDAD.OTROS);

                    if (establecimiento != null)
                    {
                        DTOEstablecimiento establecimientoDto = new DTOEstablecimiento()
                        {
                            Codigo = establecimiento.EST_ID,
                            CodigoSocio = establecimiento.BPS_ID,
                            SocioNombre = establecimiento.BPS_NAME.Replace('\t', ' ').ToUpper(),
                            Nombre = establecimiento.EST_NAME.ToUpper(),
                            CodigoTipoidentificacionfiscal = establecimiento.TAXT_ID,
                            TipoEstablecimiento = establecimiento.ESTT_ID,
                            SubTipoestablecimiento = establecimiento.SUBE_ID,
                            Codigodivision = establecimiento.DAD_ID,
                            CodigoDivisionfiscal = establecimiento.DIF_ID,
                            NumeroIdentificacionfiscal = establecimiento.TAX_ID,
                            Latitud=establecimiento.LATITUD,
                            Longitud=establecimiento.LONGITUD
                            
                        };

                        if (establecimiento.Observaciones != null)
                        {
                            observaciones = new List<DTOObservacion>();
                            if (establecimiento.Observaciones != null)
                            {
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
                        }
                        if (establecimiento.Parametros != null)
                        {
                            parametros = new List<DTOParametro>();
                            if (establecimiento.Parametros != null)
                            {
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
                        }
                        if (establecimiento.Documentos != null)
                        {
                            documentos = new List<DTODocumento>();
                            if (establecimiento.Documentos != null)
                            {
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
                        }
                        if (establecimiento.Direccion != null)
                        {
                            direcciones = new List<DTODireccion>();
                            if (establecimiento.Direccion != null)
                            {
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
                        }

                        if (establecimiento.Telefonos != null)
                        {
                            telefonos = new List<DTOTelefono>();
                            if (establecimiento.Telefonos != null)
                            {
                                establecimiento.Telefonos.ForEach(s =>
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

                        if (establecimiento.Correos != null)
                        {
                            correos = new List<DTOCorreo>();
                            if (establecimiento.Correos != null)
                            {
                                establecimiento.Correos.ForEach(s =>
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

                        if (establecimiento.RedSocial != null)
                        {
                            redsocial = new List<DTORedSocial>();
                            if (establecimiento.RedSocial != null)
                            {
                                establecimiento.RedSocial.ForEach(s =>
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

                        if (establecimiento.Caracteristicas != null)
                        {
                            caracteristicas = new List<DTOCaracteristica>();
                            if (establecimiento.Caracteristicas != null)
                            {
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
                                //AQUI DEBO DE OBTENER LAS CARACTERISTICAS PARA PODER COMPARA LO QUE SE DEBE DE ACTUALIZAR


                            }

                        }

                        if (establecimiento.Asociados != null)
                        {
                            asociados = new List<DTOAsociado>();
                            if (establecimiento.Asociados != null)
                            {
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
                                    obj.EsPrincipal = s.BPS_MAIN;
                                    obj.Activo = s.ENDS.HasValue ? false : true;

                                    var usu = new BLSocioNegocio().ObtenerDatos(s.BPS_ID, GlobalVars.Global.OWNER);
                                    if (usu != null)
                                    {
                                        obj.NombreAsociado = string.Format("{0} {1} {2} {3}", usu.BPS_NAME, usu.BPS_FIRST_NAME, usu.BPS_FATH_SURNAME, usu.BPS_MOTH_SURNAME);
                                        obj.NroDocAsociado = usu.TAX_ID;
                                    }

                                    asociados.Add(obj);
                                }
                                AsociadosTmp = asociados;
                            }
                        }

                        //if (establecimiento.Divisiones != null)
                        //{
                        //    divisiones = new List<DTODivisiones>();
                        //    if (establecimiento.Divisiones != null)
                        //    {
                        //        establecimiento.Divisiones.ForEach(s =>
                        //        {
                        //            divisiones.Add(new DTODivisiones
                        //            {
                        //                Id = s.Id,
                        //                idEstablecimiento = s.EST_ID,
                        //                idTipoDivision = s.idTIPODIVISION,
                        //                idDivision = s.idDIVISION,
                        //                idSubTipoDivision = s.idSUBTIPODIVISION,
                        //                idDivisionValor = s.idDIVISIONVAL,
                        //                auxidDivisionValor = s.idDIVISIONVAL,
                        //                TipoDivision = s.TIPODIVISION,
                        //                Division = s.DIVISION,
                        //                SubTipoDivision = s.SUBTIPODIVISION,
                        //                DivisionValor = s.DIVISIONVAL,
                        //                EnBD = true,
                        //                UsuarioCrea = s.LOG_USER_CREAT,
                        //                FechaCrea = s.LOG_DATE_CREAT,
                        //                UsuarioModifica = s.LOG_USER_UPDATE,
                        //                FechaModifica = s.LOG_DATE_UPDATE,
                        //                Activo = s.ENDS.HasValue ? false : true,
                        //            });
                        //        });
                        //        DivisionesTmp = divisiones;
                        //    }
                        //}

                        if (establecimiento.Difusion != null)
                        {
                            difusion = new List<DTODifusion>();
                            if (establecimiento.Difusion != null)
                            {
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
                        }

                        retorno.data = Json(establecimientoDto, JsonRequestBehavior.AllowGet);
                        retorno.message = "Establecimiento encontrado";
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.message = "No se ha podido encontrar el establecimiento";
                        retorno.result = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Obtener datos establecimiento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region CaracteristicasXtipo
        [HttpPost]
        public JsonResult ObtenerCaracteristicasXSubtipo(decimal idTipoEstablecimiento, decimal idSubtipoEstablecimiento)
        {
            Resultado retorno = new Resultado();

            if (idSubtipoEstablecimiento != 0)
            {
                List<BECaracteristicaEst> datos = new List<BECaracteristicaEst>();
                datos = new BLCaracteristicaEst().CaracteristicaXSubtipoEstablecimiento(idTipoEstablecimiento, idSubtipoEstablecimiento);
                if (datos.Count == 0)
                {
                    datos = new BLEstablecimiento().ListaCaracteristicasxEstablecimiento(idTipoEstablecimiento, idSubtipoEstablecimiento);
                }

                if (!isLogout(ref retorno))
                {
                    if (datos.Count > 0)
                    {
                        bool nuevo = EstablecimientoTmp.Nuevo;

                        if (nuevo) CaracteristicaTmp = null;


                        if (CaracteristicaTmp == null) CaracteristicaTmp = new List<DTOCaracteristica>();
                        datos.ForEach(x =>
                        {
                            bool existeId = CaracteristicaTmp.Exists(y => y.Id == x.CHAR_ID);

                            if (!(existeId))
                            {
                                CaracteristicaTmp.Add(new DTOCaracteristica
                                {
                                    Id = x.CHAR_ID,
                                    Idcaracteristica = x.CHAR_ID.ToString(),
                                    caracteristica = x.CHAR_LONG,
                                    IdEstablecimiento = Convert.ToString(x.EST_ID),
                                    TipoEstablecimiento = Convert.ToString(x.ESTT_ID),
                                    IdSubTipoEstablecimiento = Convert.ToString(x.SUBE_ID),
                                    //Valor = x.VALUE.ToString(),
                                    Valor = "0",
                                    usercreate = UsuarioActual,
                                    Activo = true,
                                    EnBD = false,
                                    //Identifica la caracteristica viene de tipo de establecimiento
                                    GetTipo = true
                                });
                            }
                        });
                        retorno.data = Json(CaracteristicaTmp, JsonRequestBehavior.AllowGet);
                    }
                    //quitar este codigo v
                    else
                    {

                        CaracteristicaTmp = null;
                    }
                    //^
                }
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QuitarCaracteristicasXSubtipo(string idTipoEstablecimiento)
        {
            Resultado retorno = new Resultado();

            if (!isLogout(ref retorno))
            {
                caracteristicas = CaracteristicaTmp;

                //foreach (DTOCaracteristica item in caracteristicas)
                //{
                //    if (item.caracteristica == caracteristicas[].caracteristica)
                //        item.IsDuplicate = true;
                //}

                //var itemsEquals = CaracteristicaTmp.Where(r => r.caracteristica == caracteristicas[0].caracteristica);


                //if (itemsEquals.ToList().Count > 0)
                //{
                //    var itemsToRemove = CaracteristicaTmp.Where(r => r.IsDuplicate == true);

                //    foreach (var item in itemsToRemove.ToList())
                //            CaracteristicaTmp.Remove(item);

                //    retorno.result = 1;
                //    retorno.message = "Esta caracteristica ya ha sido ingresada";
                //    return Json(retorno, JsonRequestBehavior.AllowGet);
                //}                if (caracteristicas == null) caracteristicas = new List<DTOCaracteristica>();

                if (caracteristicas != null)
                {
                    if (caracteristicas.Count == 0)
                    {
                        retorno.result = 1;
                        return Json(retorno, JsonRequestBehavior.AllowGet);
                    }

                    if (CaracteristicaTmp.Count > 0 && CaracteristicaTmp != null)
                    {
                        var itemsToRemove = CaracteristicaTmp.Where(r => r.GetTipo == true);
                        foreach (var item in itemsToRemove.ToList())
                        {
                            if (idTipoEstablecimiento != item.TipoEstablecimiento)
                                CaracteristicaTmp.Remove(item);
                        }
                        if (CaracteristicaTmp.Count == 0)
                        {
                            CaracteristicaTmp = itemsToRemove.ToList();
                        }
                        retorno.data = Json(CaracteristicaTmp, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Obtener
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
                    obj.HOU_NRO = !string.IsNullOrEmpty(x.Numero) ? Convert.ToString(x.Numero) : "0";
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
                        ENT_ID = 8,
                        LOG_USER_CREAT = UsuarioActual,
                        OBS_USER = UsuarioActual
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
                        ENT_ID = 8,
                        DOC_USER = UsuarioActual,
                        LOG_USER_CREAT = UsuarioActual,
                        DOC_VERSION = 1
                    });
                });
            }
            return datos;
        }

        private List<BEParametroGral> obtenerParametros()
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

        private List<BECaracteristicaEst> obtenerCaracteristica()
        {
            List<BECaracteristicaEst> datos = new List<BECaracteristicaEst>();
            if (CaracteristicaTmp != null)
            {
                CaracteristicaTmp.ForEach(x =>
                {
                    datos.Add(new BECaracteristicaEst
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        CHAR_ID = Convert.ToDecimal(x.Idcaracteristica),
                        ESTT_ID = Convert.ToDecimal(x.TipoEstablecimiento),
                        SUBE_ID = Convert.ToDecimal(x.IdSubTipoEstablecimiento),
                        VALUE = Convert.ToDecimal(x.Valor),
                        LOG_USER_CREAT = UsuarioActual,
                        ENDS = x.Activo ? null : x.FechaCrea
                    });
                });
            }
            return datos;
        }

        //private List<BEDivisionesEst> obtenerDivisiones()
        //{
        //    List<BEDivisionesEst> datos = new List<BEDivisionesEst>();

        //    if (DivisionesTmp != null)
        //    {
        //        DivisionesTmp.ForEach(x =>
        //        {
        //            datos.Add(new BEDivisionesEst
        //            {
        //                Id = x.Id,
        //                idTIPODIVISION = x.idTipoDivision,
        //                idDIVISION = x.idDivision,
        //                idSUBTIPODIVISION = x.idSubTipoDivision,
        //                idDIVISIONVAL = x.idDivisionValor,
        //                auxDADV_ID = x.idDivisionValor,
        //                TIPODIVISION = x.TipoDivision,
        //                DIVISION = x.Division,
        //                SUBTIPODIVISION = x.SubTipoDivision,
        //                DIVISIONVAL = x.DivisionValor,
        //                OWNER = GlobalVars.Global.OWNER,
        //                LOG_USER_CREAT = UsuarioActual
        //            });
        //        });
        //    }
        //    return datos;
        //}

        private List<BEDifusionEst> obtenerDifusion()
        {
            List<BEDifusionEst> datos = new List<BEDifusionEst>();

            if (DifusionTmp != null)
            {
                DifusionTmp.ForEach(x =>
                {
                    datos.Add(new BEDifusionEst
                    {
                        SEQUENCE = x.Id,
                        BROAD_ID = x.idDifusion,
                        BROADE_NUM = x.NroDifusion,
                        BROAD_DESC = x.Difusion,
                        BROADE_STORAGE = Convert.ToBoolean(x.almacenamiento) == true ? "1" : "0",
                        OWNER = GlobalVars.Global.OWNER,
                        LOG_USER_CREAT = UsuarioActual
                    });
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

        private string getRazonSocial(DTODireccion entidad)
        {
            StringBuilder rz = new StringBuilder();

            if (!String.IsNullOrEmpty(entidad.Avenida))
            {
                rz.AppendFormat("{0} {1} ", entidad.TipoAvenidaDes, entidad.Avenida);
            }

            if (!String.IsNullOrEmpty(entidad.Numero))
            {
                rz.AppendFormat("  {0}", entidad.Numero);
            }
            
            if (!String.IsNullOrEmpty(entidad.Manzana))
            {
                rz.AppendFormat("  Mz {0}", entidad.Manzana);
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
        #endregion

        #region Quitar
        [HttpPost]
        public JsonResult DellAddDireccion(decimal id)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
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
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "DellAddDireccion", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddObservacion(decimal id)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
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
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "DellAddObservacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddDocumento(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
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
                                documentos.Remove(objDel);
                            }
                            DocumentosTmp = documentos;
                            retorno.result = 1;
                            retorno.message = "OK";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "DellAddDocumento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddParametro(decimal id)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
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
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "DellAddParametro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddCaracteristica(decimal id)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    caracteristicas = CaracteristicaTmp;
                    if (caracteristicas != null)
                    {
                        var objDel = caracteristicas.Where(x => Convert.ToDecimal(x.Idcaracteristica) == id).FirstOrDefault();
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (CaracteristicaTmpUPDEstado == null) CaracteristicaTmpUPDEstado = new List<DTOCaracteristica>();
                                if (CaracteristicaTmpDelBD == null) CaracteristicaTmpDelBD = new List<DTOCaracteristica>();

                                var itemUpd = CaracteristicaTmpUPDEstado.Where(x => Convert.ToDecimal(x.Idcaracteristica) == id).FirstOrDefault();
                                var itemDel = CaracteristicaTmpDelBD.Where(x => Convert.ToDecimal(x.Idcaracteristica) == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) CaracteristicaTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) CaracteristicaTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) CaracteristicaTmpDelBD.Add(objDel);
                                    if (itemUpd != null) CaracteristicaTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                caracteristicas.Remove(objDel);
                                caracteristicas.Add(objDel);
                            }
                            else
                            {
                                caracteristicas.Remove(objDel);
                            }

                            CaracteristicaTmp = caracteristicas;
                            retorno.result = 1;
                            retorno.message = "OK";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "DellAddCaracteristica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddAsociado(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    asociados = AsociadosTmp;
                    if (asociados != null)
                    {
                        var objDel = asociados.Where(x => x.Id == id).FirstOrDefault();
                        asociados.Where(w => w.Id == id).ToList().ForEach(i => i.EsContacto = "0");
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (AsociadosTmpUPDEstado == null) AsociadosTmpUPDEstado = new List<DTOAsociado>();
                                if (AsociadosTmpDelBD == null) AsociadosTmpDelBD = new List<DTOAsociado>();

                                var itemUpd = AsociadosTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                                var itemDel = AsociadosTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) AsociadosTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) AsociadosTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) AsociadosTmpDelBD.Add(objDel);
                                    if (itemUpd != null) AsociadosTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                asociados.Remove(objDel);
                                asociados.Add(objDel);
                            }
                            else
                            {
                                asociados.Remove(objDel);
                            }
                            AsociadosTmp = asociados;
                            retorno.result = 1;
                            retorno.message = "OK";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "DellAddAsociado", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult DellAddDivision(decimal id)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            divisiones = DivisionesTmp;
        //            if (divisiones != null)
        //            {
        //                var objDel = divisiones.Where(x => x.Id == id).FirstOrDefault();
        //                if (objDel != null)
        //                {

        //                    if (objDel.EnBD)
        //                    {
        //                        bool blActivo = !objDel.Activo;

        //                        if (DivisionesTmpUPDEstado == null) DivisionesTmpUPDEstado = new List<DTODivisiones>();
        //                        if (DivisionesTmpDelBD == null) DivisionesTmpDelBD = new List<DTODivisiones>();

        //                        var itemUpd = DivisionesTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
        //                        var itemDel = DivisionesTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

        //                        if (!(objDel.Activo))
        //                        {
        //                            if (itemUpd == null) DivisionesTmpUPDEstado.Add(objDel);
        //                            if (itemDel != null) DivisionesTmpDelBD.Remove(itemDel);
        //                        }
        //                        else
        //                        {
        //                            if (itemDel == null) DivisionesTmpDelBD.Add(objDel);
        //                            if (itemUpd != null) DivisionesTmpUPDEstado.Remove(itemUpd);
        //                        }
        //                        objDel.Activo = blActivo;
        //                        divisiones.Remove(objDel);
        //                        divisiones.Add(objDel);
        //                    }
        //                    else
        //                    {
        //                        divisiones.Remove(objDel);
        //                    }
        //                    DivisionesTmp = divisiones;
        //                    retorno.result = 1;
        //                    retorno.message = "OK";
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = ex.Message;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "DellAddDivision", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult DellAddDifusion(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    difusion = DifusionTmp;
                    if (difusion != null)
                    {
                        var objDel = difusion.Where(x => x.Id == id).FirstOrDefault();
                        if (objDel != null)
                        {

                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (DifusionTmpUPDEstado == null) DifusionTmpUPDEstado = new List<DTODifusion>();
                                if (DifusionTmpDelBD == null) DifusionTmpDelBD = new List<DTODifusion>();

                                var itemUpd = DifusionTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                                var itemDel = DifusionTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) DifusionTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) DifusionTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) DifusionTmpDelBD.Add(objDel);
                                    if (itemUpd != null) DifusionTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                difusion.Remove(objDel);
                                difusion.Add(objDel);
                            }
                            else
                            {
                                difusion.Remove(objDel);
                            }
                            DifusionTmp = difusion;
                            retorno.result = 1;
                            retorno.message = "OK";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "DellAddDifusion", ex);
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


        #endregion


        #region TEMPORALES
        public List<DTOEstablecimiento> listaTempFacturacionMasivaTmp
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
        #endregion

        #region Listar
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
                    shtml.Append("<th class='k-header'>Dirección</th>");
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

                            //shtml.AppendFormat("<td ><input type='radio' class='radioDir' onclick='actualizarDirPrincipal({0});' name='rdbtnDir' id='rbtn_{0}' {1} /></td>", item.Id, strChecked);
                            //SetDirPrincipal
                            shtml.AppendFormat("<td style='width:80px;text-align:center;'><input type='radio' class='radioDir' onclick='actualizarDirPrincipal({0});' name='rdbtnDir' id='rbtn_{0}' {1} /></td>", item.Id, strChecked);
                            //shtml.AppendFormat("<td ><select id='ddlAplicable_{0}' onchange='validarRol(this,{1});'> </select></td>", item.Id, item.CodigoAsociado);

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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarDireccion", ex);
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarObservacion", ex);
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
                    shtml.Append("<th class='k-header' >Fecha Recepción</th><th  class='k-header'>Archivo</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (documentos != null)
                    {
                        foreach (var item in documentos.OrderBy(x => x.Id))
                        {
                            var pathWeb = System.Web.Configuration.WebConfigurationManager.AppSettings["RutaWebImgEstablecimiento"];
                            var ruta = string.Format("{0}{1}", pathWeb, item.Archivo);

                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.TipoDocumentoDesc);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaRecepcion);
                            shtml.AppendFormat("<td ><a href='#' onclick=verImagen('{0}');>Ver Documento</a></td>", ruta);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td> <a href=# onclick='updAddDocumento({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                            shtml.AppendFormat("<a href=# onclick='delAddDocumento({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Documento" : "Activar Documento");
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarDocumento", ex);

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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarParametro", ex);
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
                    shtml.Append("<thead><tr><th class='k-header' >Id</th>  <th  class='k-header' style='display:none'>IdCaracteristica</th>");
                    shtml.Append("<th class='k-header'>Característica</th><th class='k-header'>Valor</th><th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header' style='display:none'>subtipoId</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (caracteristicas != null)
                    {
                        foreach (var item in caracteristicas.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.Idcaracteristica);
                            shtml.AppendFormat("<td >{0}</td>", item.caracteristica);
                            shtml.AppendFormat("<td >{0}</td>", item.Valor);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.IdSubTipoEstablecimiento);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td> <a href=# onclick='updAddCaracteristica({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Idcaracteristica);
                            shtml.AppendFormat("<a href=# onclick='delAddCaracteristica({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Idcaracteristica, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Característica" : "Activar Característica");
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarCaracteristica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerDireccionesAll(List<DTODireccion> Direcciones)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (Direcciones.Count() > 0)
                {
                    int i = 0;
                    foreach (var item in Direcciones)
                    {
                        i++;
                        var dto = new BLDirecciones().ObtenerDireccionXId(GlobalVars.Global.OWNER, item.Id);

                        if (dto != null)
                        {
                            ListaDirecciones = new List<DTODireccion>();
                            DTODireccion x = new DTODireccion();
                            x.Id = i;
                            x.TipoDireccion = Convert.ToString(dto.ADD_TYPE);
                            x.Territorio = Convert.ToString(dto.TIS_N);
                            x.RazonSocial = dto.ADDRESS;
                            x.Lote = dto.HOU_LOT;
                            x.Manzana = dto.HOU_MZ;
                            x.Numero = Convert.ToString(dto.HOU_NRO);
                            x.CodigoUbigeo = Convert.ToString(dto.GEO_ID);
                            x.Referencia = dto.ADD_REFER;
                            x.TipoAvenida = Convert.ToString(dto.ROU_ID);
                            x.Avenida = dto.ROU_NAME;
                            x.TipoEtapa = dto.HOU_TETP;
                            x.Etapa = dto.HOU_NETP;
                            x.TipoUrb = dto.HOU_TURZN;
                            x.Urbanizacion = dto.HOU_URZN;
                            x.CodigoPostal = Convert.ToString(dto.CPO_ID);
                            x.EsPrincipal = "0";
                            x.TipoDireccionDesc = new BLREF_ADDRESS_TYPE().Obtiene(GlobalVars.Global.OWNER, dto.ADD_TYPE).DESCRIPTION;
                            x.EnBD = true;
                            x.UsuarioCrea = UsuarioActual;
                            //x.FechaCrea = dto.LOG_DATE_CREAT;
                            //x.UsuarioModifica = dto.LOG_USER_UPDATE;
                            //x.FechaModifica = dto.LOG_DATE_UPDATE;
                            x.Activo = true;
                            x.DescripcionUbigeo = new BLUbigeo().ObtenerDescripcion(dto.TIS_N, dto.GEO_ID).NOMBRE_UBIGEO;
                            if (DireccionesTmp == null) { DireccionesTmp = new List<DTODireccion>(); }
                            DireccionesTmp.Add(x);
                            retorno.result = 1;
                        }
                        else
                        {
                            retorno.result = 0;
                            retorno.message = "No se pudo obtener la dirección";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtenerDireccionesAll", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarAsociado()
        {

            asociados = AsociadosTmp;
            Resultado retorno = new Resultado();

            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Nombre del Asociado</th>");
                shtml.Append("<th class='k-header'>ROL</th>");
                shtml.Append("<th class='k-header'>Estado</th>");
                shtml.Append("<th class='k-header'>Principal</th>");
                shtml.Append("<th class='k-header'>Usu. Crea</th>");
                shtml.Append("<th class='k-header'>Fecha Crea</th>");
                shtml.Append("<th class='k-header'>Usu. Modi</th>");
                shtml.Append("<th class='k-header'>Fecha Modi</th>");
                shtml.Append("<th  class='k-header'></th></tr></thead>");

                if (asociados != null)
                {
                    string strChecked = "";
                    foreach (var item in asociados.OrderBy(x => x.Id))
                    {
                        shtml.Append("<tr class='k-grid-content'>");
                        shtml.AppendFormat("<td >{0}</td>", item.Id);
                        shtml.AppendFormat("<td >{0}</td>", item.NombreAsociado);
                        shtml.AppendFormat("<td >{0}</td>", item.RolTipoDesc);
                        shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");

                        if (item.EsPrincipal == true)
                            strChecked = " checked='checked'";
                        else
                            strChecked = "";

                        //shtml.AppendFormat("<td ><input type='radio' class='radioDir' onclick='actualizarDirPrincipal({0});' name='rdbtnDir' id='rbtn_{0}' {1} /></td>", item.Id, strChecked);
                        //SetDirPrincipal
                        shtml.AppendFormat("<td style='width:80px;text-align:center;'><input type='radio' class='radioAsoc' onclick='actualizarAsocPrincipal({0});' name='rdbtnAsoc' id='rbtn_{0}' {1} /></td>", item.Id, strChecked);

                        shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                        shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                        shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                        shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                        shtml.AppendFormat("<td> <a href=# onclick='updAddAsociado({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                        shtml.AppendFormat("<a href=# onclick='delAddAsociado({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Asociado" : "Activar Asociado");
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

        public JsonResult ListarDireccionAll()
        {
            direcciones = DireccionesTmpAll;
            Resultado retorno = new Resultado();

            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table id='tbDireccionAll' border=0 width='100%;' class='k-grid k-widget'>");
                shtml.Append("<thead><tr><th class='k-header' ></th><th  class='k-header'>Id</th>");
                shtml.Append("<th class='k-header' style='text-align:center; display:none;'>Id</th>");
                shtml.Append("<th class='k-header'>Perfil</th>");
                shtml.Append("<th class='k-header'>Tipo Dirección</th>");
                shtml.Append("<th class='k-header'>Nombre / Razón Social</th>");
                shtml.Append("<th class='k-header'></th>");
                shtml.Append("</tr></thead>");

                if (direcciones != null)
                {
                    foreach (var item in direcciones.OrderBy(x => x.Id))
                    {
                        shtml.Append("<tr class='k-grid-content'>");
                        shtml.AppendFormat("<td ><input type='checkbox' class='chksel' name='chksel' id='chksel_{0}'/></td>", item.Id);
                        shtml.AppendFormat("<td style='text-align:center; display:none;'><input type='text' id='txtId_{0}' value={0} style='width: 30px; text-align:center; font-size:6px;' readonly='true'></input></td>", item.Id);
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
                            DireccionesTmpAll = direcciones;

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

        //private string itemRoles(List<SelectListItem> items, string codeId)
        //{
        //    string option = "";
        //    foreach (var item in items)
        //    {
        //        string selected = "";
        //        if (item.Value == codeId)
        //        {
        //            selected = " selected=selected ";
        //        }
        //        option += "<option value='" + item.Value + "'  '" + selected + "'  >" + item.Text + "</option>";
        //    }
        //    return option;
        //}

        [HttpPost()]
        public JsonResult ValidacionPerfil(decimal idRol, decimal idAsociado)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLSocioNegocio servicio = new BLSocioNegocio();
                var datos = servicio.ObtenerDatos(idAsociado, GlobalVars.Global.OWNER);
                if (datos != null)
                {
                    if (idRol == GlobalVars.Global.AgenteServicio || idRol == GlobalVars.Global.AgenteAutorizado)
                    {
                        if (datos.BPS_COLLECTOR.ToString() != "1")
                        {
                            retorno.result = 0;
                            retorno.message = "EL perfil del asociado no es el de Agente de Recaudo.";
                        }
                        else
                            retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 1;
                        //agregando actualizacion de rol
                    }
                }
                else
                    retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Validación asignar rol asociado (Establecimiento)", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult UpdRol(decimal idRol, decimal idAsociado)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            bool result = false;
        //            if (idRol == GlobalVars.Global.AgenteServicio || idRol == GlobalVars.Global.AgenteAutorizado)
        //            {
        //                var IddivAsociado = DivisionAdministrativaAsociado(idAsociado);
        //                if (IddivAsociado != null)
        //                {
        //                    if (IddivAsociado != 0)
        //                    {
        //                        foreach (var item in DivisionesTmp)
        //                        {
        //                            if (IddivAsociado == item.idDivision)
        //                            {
        //                                result = true;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //                result = true;
        //            //----------------------------------------------------------------------------

        //            if (result)
        //            {
        //                BLSocioNegocio servicio = new BLSocioNegocio();
        //                var datos = servicio.ObtenerDatos(idAsociado, GlobalVars.Global.OWNER);
        //                if (datos != null)
        //                {
        //                    if (idRol == GlobalVars.Global.AgenteServicio || idRol == GlobalVars.Global.AgenteAutorizado)
        //                    {
        //                        if (datos.BPS_COLLECTOR.ToString() != "1")
        //                        {
        //                            retorno.result = 0;
        //                            retorno.message = "EL perfil del asociado no es el de Agente de Recaudo.";
        //                        }
        //                        else
        //                        {
        //                            var aso = AsociadosTmp;
        //                            if (aso.Count > 0)
        //                            {
        //                                aso.ForEach(c =>
        //                                {
        //                                    if (c.CodigoAsociado == idAsociado)
        //                                    {
        //                                        c.RolTipo = idRol;
        //                                    }
        //                                });
        //                                AsociadosTmp = aso;
        //                            }
        //                            retorno.result = 1;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        var aso = AsociadosTmp;
        //                        if (aso.Count > 0)
        //                        {
        //                            aso.ForEach(c =>
        //                            {
        //                                if (c.CodigoAsociado == idAsociado)
        //                                {
        //                                    c.RolTipo = idRol;
        //                                }
        //                            });
        //                            AsociadosTmp = aso;
        //                        }
        //                        retorno.result = 1;
        //                    }
        //                }
        //                else
        //                    retorno.result = 1;
        //            }
        //            else
        //            {
        //                retorno.result = 0;
        //                retorno.message = "Este agente no pertenece a la(s) división(es) administrativa(s) del establecimiento.";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "UpdRol", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult ObtenerMatrizAsociados(List<DTOAsociado> Asociado)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (Asociado != null)
        //        {
        //            int i = -1;
        //            foreach (var ac in AsociadosTmp.OrderBy(x => x.Id))
        //            {
        //                i += 1;
        //                Asociado[i].Activo = ac.Activo;
        //                Asociado[i].Id = ac.Id;
        //                Asociado[i].Id = ac.Id;
        //                Asociado[i].Activo = ac.Activo;
        //                Asociado[i].FechaCrea = ac.FechaCrea;
        //                Asociado[i].CodigoAsociado = ac.CodigoAsociado;
        //                Asociado[i].EnBD = ac.EnBD;
        //                Asociado[i].NombreAsociado = ac.NombreAsociado;
        //                Asociado[i].NroDocAsociado = ac.NroDocAsociado;
        //                Asociado[i].RolTipoDesc = ac.RolTipoDesc;
        //                Asociado[i].UsuarioCrea = ac.UsuarioCrea;
        //                retorno.result = 1;
        //            }
        //            AsociadosTmp = Asociado;
        //        }
        //        else
        //        {
        //            retorno.result = 0;
        //            retorno.message = "Debe de ingresar como mínimo dos Asociados con los sgtes roles cada uno: AGENTE AUTORIZADO y AGENTE DE SERVICIO";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.result = 0;
        //        retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
        //        ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtenerMatrizAsociados", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        private List<BEAsociadosEst> obtenerAsociados()
        {
            List<BEAsociadosEst> datos = new List<BEAsociadosEst>();

            if (AsociadosTmp != null)
            {
                AsociadosTmp.ForEach(x =>
                {
                    datos.Add(new BEAsociadosEst
                    {
                        Id = x.EnBD ? x.Id : decimal.Zero,
                        BPS_ID = x.CodigoAsociado,
                        BPS_NAME = x.NombreAsociado,
                        ROL_ID = Convert.ToString(x.RolTipo),
                        ROL_DESC = x.RolTipoDesc,
                        BPS_MAIN = x.EsPrincipal,
                        LOG_USER_CREAT = UsuarioActual,
                        LOG_DATE_CREAT = x.FechaCrea,
                        LOG_USER_UPDATE = UsuarioActual,
                        LOG_DATE_UPDATE = x.FechaModifica
                    });
                });
            }
            return datos;
        }

        //public JsonResult validacionGrabarAsociados()
        //{
        //    Resultado retorno = new Resultado();
        //    bool estadoVal = true;
        //    string msj = "Se debe tener asociados con los sigtes roles y en estado activo :";

        //    try
        //    {
        //        if (AsociadosTmp != null && AsociadosTmp.Count >= 2)
        //        {
        //            var validacionActivos = from item in AsociadosTmp
        //                                    where (item.RolTipo == GlobalVars.Global.AgenteAutorizado || item.RolTipo == GlobalVars.Global.AgenteServicio) && item.Activo == true
        //                                    select item;

        //            var ageAutorizado = from item in AsociadosTmp
        //                                where (item.RolTipo == GlobalVars.Global.AgenteAutorizado) && item.Activo == true
        //                                select item;

        //            var ageServicio = from item in AsociadosTmp
        //                              where (item.RolTipo == GlobalVars.Global.AgenteServicio) && item.Activo == true
        //                              select item;

        //            if (ageServicio.ToList().Count == 0)
        //            {
        //                msj += "\r\n- AGENTE DE SERVICIO";
        //                estadoVal = false;
        //            }

        //            if (ageAutorizado.ToList().Count == 0)
        //            {
        //                msj += "\r\n- AGENTE DE AUTORIZADO";
        //                estadoVal = false;
        //            }

        //            if (estadoVal)
        //            {
        //                retorno.result = 1;
        //            }
        //            else
        //            {
        //                retorno.message = msj;
        //                retorno.result = 0;
        //            }

        //            //if (validacionActivos != null && validacionActivos.ToList().Count >= 2)
        //            //{
        //            //    if (validacionActivos.ToList().Count == 1)
        //            //    {
        //            //        retorno.result = 0;
        //            //        retorno.message = "Los Asociados con los sgtes roles AGENTE AUTORIZADO y AGENTE DE SERVICIO deben de estar activos";
        //            //    }

        //            //    else if (validacionActivos.ToList().Count >= 2)
        //            //    {
        //            //        var queryAgenteAutorizado = from item in AsociadosTmp
        //            //                                    where item.RolTipo == GlobalVars.Global.AgenteAutorizado
        //            //                                    select item;

        //            //        var queryAgenteServicio = from item in AsociadosTmp
        //            //                                  where item.RolTipo == GlobalVars.Global.AgenteServicio
        //            //                                  select item;

        //            //        if (queryAgenteAutorizado != null && queryAgenteServicio != null)
        //            //        {
        //            //            if ((queryAgenteAutorizado.ToList().Count == 0 && queryAgenteServicio.ToList().Count == 0) || (queryAgenteAutorizado.ToList().Count == 1 && queryAgenteServicio.ToList().Count == 0) || (queryAgenteAutorizado.ToList().Count == 0 && queryAgenteServicio.ToList().Count == 1))
        //            //            {
        //            //                retorno.result = 0;
        //            //                retorno.message = "Debe de ingresar como minimo dos Asociados con los sgtes roles cada uno: AGENTE AUTORIZADO y AGENTE DE SERVICIO";
        //            //            }
        //            //            else
        //            //                retorno.result = 1;
        //            //        }
        //            //    }
        //            //}
        //            //else
        //            //{
        //            //    retorno.result = 0;
        //            //    retorno.message = "Debe de ingresar como minimo dos Asociados con los sgtes roles cada uno: AGENTE AUTORIZADO y AGENTE DE SERVICIO";
        //            //}
        //        }
        //        else
        //        {
        //            retorno.result = 0;
        //            retorno.message = "Debe de ingresar como minimo dos Asociados con los sgtes roles cada uno: AGENTE AUTORIZADO y AGENTE DE SERVICIO";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.result = 0;
        //        retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
        //        ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Validacion Grabar Asociados", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult validacionGrabarAsociados()
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (AsociadosTmp != null)
        //        {
        //            if (AsociadosTmp.Count >= 2)
        //            {
        //                //se acumulan los dos roles Agente autorizado agente de servicio
        //                var Asociados = from item in AsociadosTmp
        //                                where (item.RolTipo == GlobalVars.Global.AgenteAutorizado || item.RolTipo == GlobalVars.Global.AgenteServicio)
        //                                select item;

        //                var validacionActivos = from item in AsociadosTmp
        //                                        where (item.RolTipo == GlobalVars.Global.AgenteAutorizado || item.RolTipo == GlobalVars.Global.AgenteServicio) && item.Activo == true
        //                                        select item;

        //                if (Asociados != null)
        //                {
        //                    if (Asociados.ToList().Count >= 2)
        //                    {
        //                        if (validacionActivos.ToList().Count == 1)
        //                        {
        //                            retorno.result = 0;
        //                            retorno.message = "Los Asociados con los sgtes roles AGENTE AUTORIZADO y AGENTE DE SERVICIO deben de estar activos";
        //                        }

        //                        else if (validacionActivos.ToList().Count >= 2)
        //                        {
        //                            var queryAgenteAutorizado = from item in AsociadosTmp
        //                                                        where item.RolTipo == GlobalVars.Global.AgenteAutorizado
        //                                                        select item;

        //                            var queryAgenteServicio = from item in AsociadosTmp
        //                                                      where item.RolTipo == GlobalVars.Global.AgenteServicio
        //                                                      select item;

        //                            if (queryAgenteAutorizado != null && queryAgenteServicio != null)
        //                            {
        //                                if ((queryAgenteAutorizado.ToList().Count == 0 && queryAgenteServicio.ToList().Count == 0) || (queryAgenteAutorizado.ToList().Count == 1 && queryAgenteServicio.ToList().Count == 0) || (queryAgenteAutorizado.ToList().Count == 0 && queryAgenteServicio.ToList().Count == 1))
        //                                {
        //                                    retorno.result = 0;
        //                                    retorno.message = "Debe de ingresar como minimo dos Asociados con los sgtes roles cada uno: AGENTE AUTORIZADO y AGENTE DE SERVICIO";
        //                                }
        //                                else
        //                                    retorno.result = 1;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        retorno.result = 0;
        //                        retorno.message = "Debe de ingresar como minimo dos Asociados con los sgtes roles cada uno: AGENTE AUTORIZADO y AGENTE DE SERVICIO";
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                retorno.result = 0;
        //                retorno.message = "Debe de ingresar como minimo dos Asociados con los sgtes roles cada uno: AGENTE AUTORIZADO y AGENTE DE SERVICIO";
        //            }
        //        }
        //        else
        //        {
        //            retorno.result = 0;
        //            retorno.message = "Debe de ingresar como minimo dos Asociados con los sgtes roles cada uno: AGENTE AUTORIZADO y AGENTE DE SERVICIO";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.result = 0;
        //        retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
        //        ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Validacion Grabar Asociados", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult ListarDivisiones()
        {
            divisiones = DivisionesTmp;
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Tipo División</th>");
                    shtml.Append("<th class='k-header'>División</th>");
                    shtml.Append("<th class='k-header'>Sub División</th>");
                    shtml.Append("<th class='k-header'>Valor Sub División</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");


                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (divisiones != null)
                    {
                        foreach (var item in divisiones.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.TipoDivision);
                            shtml.AppendFormat("<td >{0}</td>", item.Division);
                            shtml.AppendFormat("<td >{0}</td>", item.SubTipoDivision);
                            shtml.AppendFormat("<td >{0}</td>", item.DivisionValor);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");


                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);

                            shtml.AppendFormat("<td> <a href=# onclick='updAddDivision({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                            shtml.AppendFormat("<a href=# onclick='delAddDivision({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar División" : "Activar División");
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarDivisiones", ex);
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
                    shtml.Append("<th  class='k-header' style='display:none'>valAlmacenamiento</th>");
                    shtml.Append("<th  class='k-header'>Almacenamiento</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (difusion != null)
                    {
                        foreach (var item in difusion.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.Difusion);
                            shtml.AppendFormat("<td >{0}</td>", item.NroDifusion);
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.almacenamiento);
                            shtml.AppendFormat("<td >{0}</td>", item.almacenamientoDes = item.almacenamiento == true ? "Si" : "No");
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td> <a href=# onclick='updAddDifusion({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                            shtml.AppendFormat("<a href=# onclick='delAddDifusion({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Difusion" : "Activar Difusion");
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarDocumento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region obtener
        public JsonResult SetConPrincipal(decimal idCon, decimal idAso)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    foreach (DTOAsociado item in AsociadosTmp)
                    {
                        if (item.CodigoAsociado == idAso)
                        {
                            if (item.RolTipo == GlobalVars.Global.AgenteServicio || item.RolTipo == GlobalVars.Global.AgenteAutorizado)
                            {
                                retorno.result = 0;
                                retorno.message = "No puede seleccionar como contacto, a un Agente de servicio o Agente autorizado.";
                                return Json(retorno, JsonRequestBehavior.AllowGet);
                            }
                            else if (!item.Activo)
                            {
                                retorno.result = 0;
                                retorno.message = "No se puede seleccionar como contacto, si esta inactivo.";
                                return Json(retorno, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }

                    if (AsociadosTmp != null)
                    {
                        foreach (var x in AsociadosTmp.Where(x => x.Id != idCon))
                        {
                            x.EsContacto = "0";
                        }
                        foreach (var x in AsociadosTmp.Where(x => x.Id == idCon))
                        {
                            x.EsContacto = "1";
                        }
                    }
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtieneObservacionTmp", ex);
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtieneDireccionTmp", ex);
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtieneDocumentoTmp", ex);
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtieneParametroTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneCaracteristicaTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var caracteristica = CaracteristicaTmp.Where(x => Convert.ToDecimal(x.Idcaracteristica) == idDir).FirstOrDefault();
                    retorno.data = Json(caracteristica, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtieneParametroTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneAsociadoTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var param = AsociadosTmp.Where(x => x.Id == idDir).FirstOrDefault();
                    retorno.data = Json(param, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtieneAsociadoTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult ObtieneDivisionTmp(decimal idDir)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            var division = DivisionesTmp.Where(x => x.Id == idDir).FirstOrDefault();
        //            retorno.data = Json(division, JsonRequestBehavior.AllowGet);
        //            retorno.message = "OK";
        //            retorno.result = 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtieneDivisionTmp", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult ObtieneDifusionTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var difusion = DifusionTmp.Where(x => x.Id == idDir).FirstOrDefault();
                    retorno.data = Json(difusion, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtieneDifusionTmp", ex);
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

        #endregion

        #region Eliminar Establecimiento
        public JsonResult Eliminar(decimal idEstablecimiento)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLEstablecimiento servicio = new BLEstablecimiento();
                    var result = servicio.InactivarEstablecimiento(new BEEstablecimiento
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        EST_ID = idEstablecimiento,
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Eliminar establecimiento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ValidacionDivisionAdministrativaEstablecimiento_Asociado
        public bool ObtieneDivAdmEstablecimiento(decimal divAsociado)
        {
            bool result = true;
            foreach (var item in DivisionesTmp)
            {
                //item.idDivision = DAD_ID
                if (divAsociado == item.idDivision)
                {
                    result = true;
                }
            }
            return result;
        }

        public decimal DivisionAdministrativaAsociado(decimal Id)
        {
            decimal dadid = 0;
            var dato = new BLAsociadosEst().ObtenerDivisionAdministrativa(GlobalVars.Global.OWNER, Id);
            if (dato != null)
            {
                dadid = dato.DAD_ID;
            }
            return dadid;
        }
        #endregion


        //david
        #region establecimientoSocioEmpresarial
        public JsonResult ConsultaEstablecimientoSocioEmpresarial(decimal Socio, decimal? LicMas)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    Session.Remove(K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP);
                    Session.Remove(K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP_SEG);
                    Session.Remove(K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP_SEG_EDITAR);


                    var EstablecimientoLicOrigen = new BLEstablecimiento().ConsultaEstablecimientoSocioEmpr(Socio, LicMas);


                    if (EstablecimientoLicOrigen != null)
                    {
                        ConEstablecimientoSocEmp = new List<DTOEstablecimiento>();
                        EstablecimientoLicOrigen.ForEach(s =>
                        {
                            int valEst = 0;
                            if (EstablecimientoSocioEmpresarialDestinoTmp != null)
                                valEst = EstablecimientoSocioEmpresarialDestinoTmp.Where(x => x.Codigo == s.EST_ID).Count();

                            if (valEst == 0)
                            {
                                ConEstablecimientoSocEmp.Add(new DTOEstablecimiento
                                {
                                    Codigo = s.EST_ID,
                                    Nombre = s.EST_NAME
                                });
                            }
                        });
                        EstablecimientoSocioEmpresarialTmp = ConEstablecimientoSocEmp;


                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(EstablecimientoLicOrigen, JsonRequestBehavior.AllowGet);
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

        public JsonResult ListarEstablecimientoSocioEmpresarial()
        {
            listar = EstablecimientoSocioEmpresarialTmp;
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblEstablecimientosSocEmp' border=0 width='100%;' class='k-grid k-widget' id='tblEstablecimientosSocEmp'>");
                shtml.Append("<thead><tr>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >#</th>");
                shtml.Append("<th style='display:none'  class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>ID</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Nombre</th>");

                if (listar != null)
                {
                    foreach (var item in listar.OrderBy(x => x.Codigo))
                    {                                                                                                                  //<input type='checkbox' name='chkorigen{0}' value='chkorigen{0}'/></td>"
                        shtml.Append("<tr style='background-color:white'>");
                        shtml.AppendFormat("<td style='width:2.5px; cursor:pointer;text-align:right; width:2.5px';' class='IDCellOri' ><input onclick='validaEstablecimiento(" + item.Codigo + ")' type='checkbox' id='{0}' name='Check' class='Check' />", "chkEstOrigen" + item.Codigo);
                        //shtml.AppendFormat("</td>");
                        shtml.AppendFormat("<td style='display:none;cursor:pointer;text-align:right'; class='IDEstOri'>{0}</td>", item.Codigo);
                        shtml.AppendFormat("<td style='width:150px;cursor:pointer;text-align:left';'class='NomEstOri'>{0}</td>", item.Nombre);


                        //shtml.AppendFormat("<td style='cursor:pointer;' style='text-align:center'>");
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

        //Listando la Segunda Fila
        public JsonResult ListarEstablecimientoSocioEmpresarialSeg()
        {
            listarEstSeg = EstablecimientoSocioEmpresarialDestinoTmp;
            if (listarEstSeg == null)
                listarEstSeg = new List<DTOEstablecimiento>();
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblEstablecimientosSocEmpSeg' border=0 width='100%;' class='k-grid k-widget' id='tblEstablecimientosSocEmpSeg'>");
                shtml.Append("<thead><tr>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >#</th>");
                shtml.Append("<th style='display:none'  class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>ID</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Nombre</th>");
                if (listarEstSeg != null)
                {
                    foreach (var item in listarEstSeg.OrderBy(x => x.Codigo))
                    {
                        shtml.Append("<tr style='background-color:white'>");
                        shtml.AppendFormat("<td style='width:5%; cursor:pointer;text-align:center; ';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Check' />", "chkEstFin" + item.Codigo);
                        shtml.AppendFormat("<td style='display:none;cursor:pointer;text-align:right;background-color:none'; class='IDEstOri'>{0}</td>", item.Codigo);
                        shtml.AppendFormat("<td style='width:95%;cursor:pointer;text-align:left'; onclick='EditarLicHija(" + item.Codigo + ");' 'class='IDNomEstFin'>{0}</td>", item.Nombre);
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


        //Elimina  Elementos seleccionados de la lista Original
        public JsonResult EstablecimientoSocEmpArmaTemporalesOriginal(List<DTOEstablecimiento> ReglaValor)
        {
            var agregarlistas = 0;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ReglaValor != null)
                    {
                        listarEstSeg = ReglaValor;
                    }
                    else
                    {

                        listarEstSeg = EstablecimientoSocioEmpresarialDestinoTmp;
                    }
                    //if (EstablecimientoSocioEmpresarialDestinoEditarTmp != null && ReglaValor == null)
                    //{
                    //    listarEstSeg = EstablecimientoSocioEmpresarialDestinoEditarTmp;
                    //}
                    //if (EstablecimientoSocioEmpresarialDestinoEditarTmp != null && ReglaValor != null)
                    //{
                    //    agregarlistas = 1;
                    //}


                    //Arma la segunda lista origen

                    foreach (var item in listarEstSeg.OrderBy(x => x.Codigo))
                    {
                        foreach (var item2 in EstablecimientoSocioEmpresarialTmp.Where(x => x.Codigo == item.Codigo))
                        //foreach (var item2 in EstablecimientoSocioEmpresarialTmp.OrderBy(x=>x.Codigo))
                        {
                            if (EstablecimientoSocioEmpresarialDestinoTmp == null)
                            {
                                EstablecimientoSocioEmpresarialDestinoTmp = new List<DTOEstablecimiento>();
                            }
                            if (item2 != null)
                            {

                                EstablecimientoSocioEmpresarialDestinoTmp.Add(new DTOEstablecimiento
                                {
                                    Codigo = item2.Codigo,
                                    Nombre = item2.Nombre
                                });
                            }
                        }
                    }
                    //Arma  La Primera Lista Origen
                    foreach (var item in listarEstSeg.OrderBy(x => x.Codigo))
                    {
                        EstablecimientoSocioEmpresarialTmp = EstablecimientoSocioEmpresarialTmp.Where(x => x.Codigo != item.Codigo).ToList();


                    }


                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(EstablecimientoSocioEmpresarialTmp, JsonRequestBehavior.AllowGet);
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

        //Elimina  Elementos seleccionados de la lista Destino
        public JsonResult EstablecimientoSocEmpArmaTemporalesDestino(List<DTOEstablecimiento> ReglaValor)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    listarEstSeg = ReglaValor;

                    var nombreEsta = "";

                    //Arma  La Primera Lista Origen
                    foreach (var item in listarEstSeg.OrderBy(x => x.Codigo))
                    {
                        //foreach (var item2 in EstablecimientoSocioEmpresarialTmp.Where(x => x.Codigo != item.Codigo).ToList())
                        //{
                        if (EstablecimientoSocioEmpresarialTmp == null)
                        {
                            EstablecimientoSocioEmpresarialTmp = new List<DTOEstablecimiento>();
                        }
                        //if (item != null & nombreEsta != "")
                        if (item != null)
                        {

                            EstablecimientoSocioEmpresarialTmp.Add(new DTOEstablecimiento
                            {

                                Codigo = item.Codigo,
                                Nombre = EstablecimientoSocioEmpresarialDestinoTmp.Where(x => x.Codigo == item.Codigo).FirstOrDefault().Nombre
                            });
                        }
                    }
                    //Arma la segunda lista origen

                    foreach (var item in listarEstSeg.OrderBy(x => x.Codigo))
                    {
                        if (EstablecimientoSocioEmpresarialDestinoTmp != null)
                        {
                            //nombreEsta = EstablecimientoSocioEmpresarialDestinoTmp.Where(x => x.Codigo == item.Codigo).FirstOrDefault().Nombre;
                            EstablecimientoSocioEmpresarialDestinoTmp = EstablecimientoSocioEmpresarialDestinoTmp.Where(x => x.Codigo != item.Codigo).ToList();
                        }
                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(EstablecimientoSocioEmpresarialTmp, JsonRequestBehavior.AllowGet);
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
        //Lista Establecimientos Grabados ,para la actualizacion de la licencia
        public JsonResult ConsultaEstablecimientosSocEmprGrabados(decimal Socio, decimal licmas)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    //Quitando los temporales
                    //Session.Remove(K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP);
                    Session.Remove(K_SESION_ESTABLECIMIENTO_CONSULTA_SOCEMP_SEG);
                    var EstablecimientoLicDestino = new BLEstablecimiento().ConsultaEstablecimientoSocioEmprGrabado(Socio, licmas);
                    if (EstablecimientoSocioEmpresarialDestinoEditarTmp == null)
                    {
                        EstablecimientoSocioEmpresarialDestinoEditarTmp = new List<DTOEstablecimiento>();
                        //EstablecimientoSocioEmpresarialDestinoTmp = new List<DTOEstablecimiento>();
                    }

                    if (EstablecimientoLicDestino != null)
                    {
                        ConEstablecimientoSocEmp = new List<DTOEstablecimiento>();
                        EstablecimientoLicDestino.ForEach(s =>
                        {
                            int valEst = 0;
                            if (EstablecimientoSocioEmpresarialDestinoTmp != null)
                                valEst = EstablecimientoSocioEmpresarialDestinoTmp.Where(x => x.Codigo == s.EST_ID).Count();

                            if (valEst == 0)
                            {
                                ConEstablecimientoSocEmp.Add(new DTOEstablecimiento
                                {
                                    Codigo = s.EST_ID,
                                    Nombre = s.EST_NAME
                                });
                            }
                        });
                        EstablecimientoSocioEmpresarialDestinoTmp = ConEstablecimientoSocEmp;
                        // EstablecimientoSocioEmpresarialDestinoEditarTmp = ConEstablecimientoSocEmp;
                        //EstablecimientoSocioEmpresarialDestinoTmp = EstablecimientoSocioEmpresarialDestinoEditarTmp;

                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(EstablecimientoLicDestino, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarConsultaEstablecimientoSocioEmpresarialGrabados", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region ValidarEstablecimiento

        public ActionResult ValidarCaracteristicasEsTablecimiento(int IdEstablecimiento)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    int RESPUESTA = new BLEstablecimiento().ValidarEstablecimientoCaracteristica(IdEstablecimiento);
                    if (RESPUESTA == 1)
                    {
                        retorno.result = 1;
                        retorno.Code = IdEstablecimiento;
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
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListarConsultaEstablecimientoSocioEmpresarial", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //Eliminar
        #region CaracteristicasPredefinidas
        public JsonResult ListarCaracteristicasPredefinidas()
        {

            decimal idest = 0;
            decimal idSubest = 0;

            idest = Convert.ToDecimal(CaracteristicaTmp.OrderBy(x => x.Id).FirstOrDefault().TipoEstablecimiento);
            idSubest = Convert.ToDecimal(CaracteristicaTmp.OrderBy(x => x.Id).FirstOrDefault().SubTipoEstablecimiento);

            //caracteristicas = new BLEstablecimiento().ListaCaracteristicasxEstablecimiento(idest, idSubest);

            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th>  <th  class='k-header' style='display:none'>IdCaracteristica</th>");
                    shtml.Append("<th class='k-header'>Característica</th><th class='k-header'>Valor</th><th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header' style='display:none'>subtipoId</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (caracteristicas != null)
                    {
                        foreach (var item in caracteristicas.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.Idcaracteristica);
                            shtml.AppendFormat("<td >{0}</td>", item.caracteristica);
                            shtml.AppendFormat("<td >{0}</td>", item.Valor);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.IdSubTipoEstablecimiento);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td> <a href=# onclick='updAddCaracteristica({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Idcaracteristica);
                            shtml.AppendFormat("<a href=# onclick='delAddCaracteristica({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Idcaracteristica, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Característica" : "Activar Característica");
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarCaracteristica", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //Valida las Caracteristicas Predefinidas

        public JsonResult ValidaCaracteristicasPredefinidas()
        {
            Resultado retorno = new Resultado();
            try
            {
                retorno.result = 1;
                if (CaracteristicaTmp != null)
                {

                    //CARACTERISTICASTMP ES EL TEMPORAL QUE LELNA AL TRAER LAS CARACTERSITICAS PREDEFINIDAS
                    foreach (var x in CaracteristicaTmp.OrderBy(x => x.Id))
                    {
                        if (x.Valor == "0" || x.Valor == "")
                        {

                            retorno.result = 0;
                            break;
                        }
                    }
                }

                //Si no llego a entrar al IF por que su valor es diferente a 0 o a null

            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public void listaCaractersiticaLicencia(decimal CODEST, decimal TIPEST, decimal SUBTIP_EST) //actualize todas las caractersiticas de los periodos abiertos de la licencia
        {
            BECaracteristicaLic entidad = null;
            List<BECaracteristicaLic> lista = new List<BECaracteristicaLic>();
            var licencia = new BLLicencias().ListaLicenciaxCodigoEst(CODEST);//listando todas las licencias activas por codigo de establecimiento que pertenecen a una lic maestra
            if (licencia != null)
            {
                var caracteristica = obtenerCaracteristica();//caracteristicas de el establecimiento
                //**********************************OBTENIENDO LAS CARACTERISTICAS PREDEFINIDAS QUE DEBEN DE INSERTARSE EN LOS PERIODOS****************************************
                var carateristicasPredefinidas = new BLEstablecimiento().ListaCaracteristicasxEstablecimiento(TIPEST, SUBTIP_EST);

                //**************************************************************************************************************************************************************

                List<BECaracteristicaEst> listaCaracteristica = new BLCaracteristicaEst().CaractersiticaxEstablecimiento(GlobalVars.Global.OWNER, CODEST);//validacion
                //CaractersiticaxEstablecimiento
                if ((listaCaracteristica != null && listaCaracteristica.Count > 0) && (caracteristica != null && caracteristica.Count > 0))
                {
                    //--------------------------------armar la lista para insertar--------------------------------------
                    foreach (var y in licencia.OrderBy(y => y.LIC_ID))
                    {
                        //trayendo el Planeamiento 
                        var listaplan = new BLLicenciaPlaneamiento().ListaTodaPlanificacionxLicencia(y.LIC_ID);
                        foreach (var z in listaplan.OrderBy(z => z.LIC_PL_ID))
                        {
                            if ((z.LIC_PL_STATUS != null && (z.LIC_PL_STATUS == "A" || z.LIC_PL_STATUS == "P")))//Solo los Periodos Abiertos o Pendientes
                            {
                                carateristicasPredefinidas.ForEach(cp =>//sOLAMENTE QUE se manden las caracteristicas predefinidas de ese establecimiento
                                 {
                                     foreach (var x in caracteristica.Where(x => x.CHAR_ID == cp.CHAR_ID))
                                     {
                                         entidad = new BECaracteristicaLic();
                                         //variables necesarias para la insercion de caract
                                         entidad.OWNER = GlobalVars.Global.OWNER;
                                         entidad.LIC_ID = y.LIC_ID;
                                         entidad.CHAR_ID = x.CHAR_ID;
                                         entidad.LIC_CHAR_VAL = x.VALUE;
                                         entidad.LIC_VAL_ORIGEN = "E";//por que va "T" Y A VECES "E".... 
                                         entidad.LOG_USER_CREAT = UsuarioActual;
                                         entidad.LIC_PL_ID = z.LIC_PL_ID;
                                         entidad.FLG_MANUAL = null;
                                         entidad.COMMENT_FLG = null;

                                         lista.Add(entidad);
                                     }
                                 });
                            }
                        }
                    }

                    new BLCaracteristica().ActualizaCaracteristicasXML(lista);//Inserta Todas las Caracteristicas y las inactiva...
                }
            }
        }
        #endregion

        #region  Descuentos Socio
        public JsonResult ValidarEstablecimientoModif(decimal ESTID)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    int res = new BLEstablecimiento().ValidaEstablecimientoModif(ESTID);
                    if (res == 1)
                    {
                        retorno.result = 1;
                        retorno.Code = 1;

                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.Code = 0;
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

        public JsonResult SetAsocPrincipal(decimal idAsoc)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (AsociadosTmp != null)
                {
                    foreach (var x in AsociadosTmp.Where(x => x.Id != idAsoc))
                    {
                        x.EsPrincipal = false;
                    }
                    foreach (var x in AsociadosTmp.Where(x => x.Id == idAsoc))
                    {
                        x.EsPrincipal = true;
                    }
                }
                retorno.message = "OK";
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}

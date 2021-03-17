using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL;
using SGRDA.Entities;
using SGRDA.Entities.Reporte;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using System.Text;


namespace Proyect_Apdayc.Controllers
{
    public class AgenteDivisionController : Base
    {
        public const string nomAplicacion = "SRGDA";
        private const string K_SESION_AGENTE = "___DTOAgente";
        private const string K_SESION_AGENTE_DEL = "______DTOAgenteDEL";
        private const string K_SESION_AGENTE_ACT = "___DTOAgenteACT";

        private const string K_SESSION_OBSERVACION_AGE = "___DTOObservacion_Agente";
        private const string K_SESSION_OBSERVACION_DEL_AGE = "___DTOObservacionDEL_Agente";
        private const string K_SESSION_OBSERVACION_ACT_AGE = "___DTOObservacionACT_Agente";

        private const string K_SESSION_PARAMETRO_AGE = "___DTOParametro_Agente";
        private const string K_SESSION_PARAMETRO_DEL_AGE = "___DTOParametroDEL_Agente";
        private const string K_SESSION_PARAMETRO_ACT_AGE = "___DTOParametroACT_Agente";

        //private const string K_SESSION_DOCUMENTO_AGE = "___DTODocumento_AGE";
        //private const string K_SESSION_DOCUMENTO_DEL_AGE = "___DTODocumentoDEL_AGE";
        //private const string K_SESSION_DOCUMENTO_ACT_AGE = "___DTODocumentoACT_AGE";

        private const string K_SESION_DIRECCION_AGE = "___DTODirecciones_Agente";
        private const string K_SESION_DIRECCION_DEL_AGE = "___DTODireccionesDEL_Agente";
        private const string K_SESION_DIRECCION_ACT_AGE = "___DTODireccionesACT_Agente";

        // GET: /AgenteDivision/
        List<DTOSocio> AgenteRecaudo = new List<DTOSocio>();
        List<DTOObservacion> observaciones = new List<DTOObservacion>();
        List<DTODocumento> documentos = new List<DTODocumento>();
        List<DTOParametro> parametros = new List<DTOParametro>();
        List<DTODireccion> direcciones = new List<DTODireccion>();

        #region VISTA
        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESION_AGENTE);
            Session.Remove(K_SESION_AGENTE_DEL);
            Session.Remove(K_SESION_AGENTE_ACT);

            Session.Remove(K_SESSION_OBSERVACION_AGE);
            Session.Remove(K_SESSION_OBSERVACION_ACT_AGE);
            Session.Remove(K_SESSION_OBSERVACION_DEL_AGE);

            Session.Remove(K_SESSION_PARAMETRO_AGE);
            Session.Remove(K_SESSION_PARAMETRO_ACT_AGE);
            Session.Remove(K_SESSION_PARAMETRO_DEL_AGE);

            //Session.Remove(K_SESSION_DOCUMENTO_AGE);
            //Session.Remove(K_SESSION_DOCUMENTO_ACT_AGE);
            //Session.Remove(K_SESSION_DOCUMENTO_DEL_AGE);
            return View();
        }
        #endregion

        #region BANDEJA
        [HttpPost()]
        public JsonResult ListarAgenteRecaudo(int skip, int take, int page, int pageSize, string group, decimal idOficina, decimal idDivision, string agenteRecaudo)
        {
            var lista = new BLAgenteRecaudo().ListarAgenteRecaudoXDivision(GlobalVars.Global.OWNER, idOficina, idDivision, agenteRecaudo, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEAgenteRecaudo { ListarAgenteRecaudo = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEAgenteRecaudo { ListarAgenteRecaudo = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Eliminar(decimal idDad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEDivisionRecaudador en = new BEDivisionRecaudador();
                    en.OWNER = GlobalVars.Global.OWNER;
                    en.DAD_ID = idDad;
                    var lista = new BLDivisionRecaudador().ListarAgenteRecaudo(en);

                    foreach (var item in lista)
                    {
                        BEDivisionRecaudador recaudador = new BEDivisionRecaudador();
                        recaudador.OWNER = GlobalVars.Global.OWNER;
                        recaudador.LOG_USER_UPDAT = UsuarioActual;
                        recaudador.BPS_ID = item.BPS_ID;
                        recaudador.DAD_ID = idDad;
                        int r = new BLDivisionRecaudador().Eliminar(recaudador);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region OBSERVACION
        public List<DTOObservacion> ObservacionesTmp
        {
            get
            {
                return (List<DTOObservacion>)Session[K_SESSION_OBSERVACION_AGE];
            }
            set
            {
                Session[K_SESSION_OBSERVACION_AGE] = value;
            }
        }

        private List<DTOObservacion> ObservacionesTmpUPDEstado
        {
            get
            {
                return (List<DTOObservacion>)Session[K_SESSION_OBSERVACION_ACT_AGE];
            }
            set
            {
                Session[K_SESSION_OBSERVACION_ACT_AGE] = value;
            }
        }

        private List<DTOObservacion> ObservacionesTmpDelBD
        {
            get
            {
                return (List<DTOObservacion>)Session[K_SESSION_OBSERVACION_DEL_AGE];
            }
            set
            {
                Session[K_SESSION_OBSERVACION_DEL_AGE] = value;
            }
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
                        observaciones.Add(entidad);
                    }
                    else
                    {
                        var item = observaciones.Where(x => x.Id == entidad.Id).FirstOrDefault();
                        entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                        entidad.Activo = item.Activo;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddObservacion", ex);
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddObservacion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneObservacionTmp(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var observacion = ObservacionesTmp.Where(x => x.Id == id).FirstOrDefault();
                    retorno.data = Json(observacion, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneObservacionTmp", ex);
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
                    shtml.Append("<thead><tr><th class='ui-state-default ui-th-column ui-th-ltr' >Id</th><th  class='ui-state-default ui-th-column ui-th-ltr' >Tipo Observación</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Observación</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Estado</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'  style='width:40px'></th></tr></thead>");

                    if (observaciones != null)
                    {
                        foreach (var item in observaciones.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td style='width:40px'>{0}</td>", item.Id);
                            shtml.AppendFormat("<td style='width:100px'>{0}</td>", item.TipoObservacionDesc);
                            shtml.AppendFormat("<td style='width:400px'>{0}</td>", item.Observacion);
                            shtml.AppendFormat("<td style='width:40px'>{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td style='width:40px'> <a href=# onclick='updAddObservacion({0});'><img src='../Images/iconos/edit.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.Id, "Modificar Observación");
                            shtml.AppendFormat("                        <a href=# onclick='delAddObservacion({0});'><img src='../Images/iconos/{1}'      title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Observacion" : "Activar Observacion");
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarObservacion", ex);

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
                        ENT_ID = Constantes.ObservacionType.recaudadores.GetHashCode(),
                        LOG_USER_CREAT = UsuarioActual,
                        OBS_USER = UsuarioActual
                    });
                });
            }
            return datos;
        }
        #endregion

        #region PARAMETRO
        public List<DTOParametro> ParametrosTmp
        {
            get
            {
                return (List<DTOParametro>)Session[K_SESSION_PARAMETRO_AGE];
            }
            set
            {
                Session[K_SESSION_PARAMETRO_AGE] = value;
            }
        }

        private List<DTOParametro> ParametrosTmpUPDEstado
        {
            get
            {
                return (List<DTOParametro>)Session[K_SESSION_PARAMETRO_DEL_AGE];
            }
            set
            {
                Session[K_SESSION_PARAMETRO_DEL_AGE] = value;
            }
        }

        private List<DTOParametro> ParametrosTmpDelBD
        {
            get
            {
                return (List<DTOParametro>)Session[K_SESSION_PARAMETRO_DEL_AGE];
            }
            set
            {
                Session[K_SESSION_PARAMETRO_DEL_AGE] = value;
            }
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
                        parametros.Add(entidad);
                    }
                    else
                    {
                        var item = parametros.Where(x => x.Id == entidad.Id).FirstOrDefault();
                        entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                        entidad.Activo = item.Activo;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddParametro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddParametro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);


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
                        ENT_ID = Constantes.ObservacionType.oficinasRecaudo.GetHashCode(),
                        LOG_USER_CREAT = UsuarioActual
                    });
                });
            }
            return datos;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneParametroTmp", ex);
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
                    shtml.Append("<thead><tr><th class='ui-state-default ui-th-column ui-th-ltr'  >Id</th><th class='ui-state-default ui-th-column ui-th-ltr' >Tipo Parámetro</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Descripción</th><th class='ui-state-default ui-th-column ui-th-ltr' >Estado</th><th  class='ui-state-default ui-th-column ui-th-ltr'  style='width:60px'></th></tr></thead>");

                    if (parametros != null)
                    {
                        foreach (var item in parametros.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.TipoParametroDesc);
                            shtml.AppendFormat("<td >{0}</td>", item.Descripcion);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td style='width:60px'> <a href=# onclick='updAddParametro({0});'><img src='../Images/iconos/edit.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.Id, "Modificar Parametro");
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarParametro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region DIRECCION
        private string getRazonSocial(DTODireccion entidad)
        {

            StringBuilder rz = new StringBuilder();

            //rz.AppendFormat("{0} {1}", entidad.TipoUrbDes, entidad.Urbanizacion);

            if (!String.IsNullOrEmpty(entidad.Avenida))
            {
                rz.AppendFormat("{0} {1} ", entidad.TipoAvenidaDes, entidad.Avenida);
            }

            if (!String.IsNullOrEmpty(entidad.Urbanizacion))
            {
                rz.AppendFormat("  {0} {1}", entidad.TipoUrbDes, entidad.Urbanizacion);
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
                rz.AppendFormat("  {0} {1}", entidad.TipoEtapaDes, entidad.Etapa);
            }

            //if (!String.IsNullOrEmpty(entidad.Avenida))
            //{
            //    rz.AppendFormat(" {0} {1}", entidad.TipoAvenidaDes, entidad.Avenida);
            //}

            if (!String.IsNullOrEmpty(entidad.Numero))
            {
                rz.AppendFormat("  Nro {0}", entidad.Numero);
            }

            if (!String.IsNullOrEmpty(entidad.NroPiso))
            {
                rz.AppendFormat("  {0} {1}", entidad.TipoDepaDes, entidad.NroPiso);
            }

            return rz.ToString();


        }
        public List<DTODireccion> DireccionesTmp
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION_AGE];
            }
            set
            {
                Session[K_SESION_DIRECCION_AGE] = value;
            }
        }
        private List<DTODireccion> DireccionesTmpUPDEstado
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION_ACT_AGE];
            }
            set
            {
                Session[K_SESION_DIRECCION_ACT_AGE] = value;
            }
        }
        private List<DTODireccion> DireccionesTmpDelBD
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION_DEL_AGE];
            }
            set
            {
                Session[K_SESION_DIRECCION_DEL_AGE] = value;
            }
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
                    obj.HOU_NRO = Convert.ToString(x.Numero);
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

        #endregion

        #region REGISTRO
        [HttpPost]
        public JsonResult Insertar(BEAgenteRecaudo AgenteRecaudo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    AgenteRecaudo.OWNER = GlobalVars.Global.OWNER;
                    AgenteRecaudo.ListaObservacion = obtenerObservaciones();
                    AgenteRecaudo.ListaDireccion = obtenerDirecciones();

                    if (AgenteRecaudo.COLL_OFF_ID == 0)
                    {
                        AgenteRecaudo.LOG_USER_CREAT = UsuarioActual;
                        decimal resultado = new BLAgenteRecaudo().Insertar(AgenteRecaudo);
                    }
                    else
                    {
                        AgenteRecaudo.LOG_USER_UPDAT = UsuarioActual;
                        #region Observacion
                        List<BEObservationGral> listaObsDel = null;
                        if (ObservacionesTmpDelBD != null)
                        {
                            listaObsDel = new List<BEObservationGral>();
                            ObservacionesTmpDelBD.ForEach(x => { listaObsDel.Add(new BEObservationGral { OBS_ID = x.Id }); });
                        }
                        List<BEObservationGral> listaObsUpdEst = null;
                        if (ObservacionesTmpUPDEstado != null)
                        {
                            listaObsUpdEst = new List<BEObservationGral>();
                            ObservacionesTmpUPDEstado.ForEach(x => { listaObsUpdEst.Add(new BEObservationGral { OBS_ID = x.Id }); });
                        }
                        #endregion

                        #region Direccion
                        //setting direcciones eliminar
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
                        #endregion

                        decimal resultado = new BLAgenteRecaudo().Actualizar(AgenteRecaudo,
                                                                             listaDirDel, listaDirUpdEst,
                                                                             listaObsDel, listaObsUpdEst);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;

                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Obtener(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEAgenteRecaudo AgenteRecaudo = new BEAgenteRecaudo();
                    AgenteRecaudo.OWNER = GlobalVars.Global.OWNER;
                    AgenteRecaudo.COLL_OFF_ID = id;
                    var agente = new BLAgenteRecaudo().Obtener(AgenteRecaudo);
                    if (agente != null)
                    {
                        #region OBSERVACION
                        if (agente.ListaObservacion != null)
                        {
                            observaciones = new List<DTOObservacion>();
                            if (agente.ListaObservacion != null)
                            {
                                agente.ListaObservacion.ForEach(s =>
                                {
                                    observaciones.Add(new DTOObservacion
                                    {
                                        Id = s.OBS_ID,
                                        Observacion = s.OBS_VALUE,
                                        TipoObservacion = Convert.ToString(s.OBS_TYPE),
                                        TipoObservacionDesc = new BLTipoObservacion().Obtener(GlobalVars.Global.OWNER, s.OBS_TYPE).OBS_DESC,
                                        EnBD = true,
                                        Activo = s.ENDS.HasValue ? false : true
                                    });
                                });
                                ObservacionesTmp = observaciones;
                            }
                        }
                        #endregion
                        #region DIRECCION
                        if (agente.ListaDireccion != null)
                        {
                            direcciones = new List<DTODireccion>();
                            if (agente.ListaDireccion != null)
                            {
                                agente.ListaDireccion.ForEach(s =>
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
                        #endregion
                    }
                    retorno.data = Json(agente, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Agregar y Lista los Socios
        public List<DTOSocio> AgenteTmp
        {
            get
            {
                return (List<DTOSocio>)Session[K_SESION_AGENTE];
            }
            set
            {
                Session[K_SESION_AGENTE] = value;
            }
        }

        private List<DTOSocio> AgenteTmpUPDEstado
        {
            get
            {
                return (List<DTOSocio>)Session[K_SESION_AGENTE_DEL];
            }
            set
            {
                Session[K_SESION_AGENTE_DEL] = value;
            }
        }

        private List<DTOSocio> AgenteTmpDelBD
        {
            get
            {
                return (List<DTOSocio>)Session[K_SESION_AGENTE_ACT];
            }
            set
            {
                Session[K_SESION_AGENTE_ACT] = value;
            }
        }

        [HttpPost]
        public JsonResult DellAgenteRecaudo(decimal id)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    AgenteRecaudo = AgenteTmp;
                    if (AgenteRecaudo != null)
                    {
                        var objDel = AgenteRecaudo.Where(x => x.Codigo == id).FirstOrDefault();
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (AgenteTmpUPDEstado == null) AgenteTmpUPDEstado = new List<DTOSocio>();
                                if (AgenteTmpDelBD == null) AgenteTmpDelBD = new List<DTOSocio>();

                                var itemUpd = AgenteTmpUPDEstado.Where(x => x.Codigo == id).FirstOrDefault();
                                var itemDel = AgenteTmpDelBD.Where(x => x.Codigo == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) AgenteTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) AgenteTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) AgenteTmpDelBD.Add(objDel);
                                    if (itemUpd != null) AgenteTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                AgenteRecaudo.Remove(objDel);
                                AgenteRecaudo.Add(objDel);
                            }
                            else
                            {
                                AgenteRecaudo.Remove(objDel);
                            }
                            AgenteTmp = AgenteRecaudo;
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
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarAgenteRecaudo()
        {
            AgenteRecaudo = AgenteTmp;
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Documento</th>");
                    shtml.Append("<th class='k-header'>Numero</th>");
                    shtml.Append("<th class='k-header'>Nombre / Razon Social</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th  class='k-header'></th>");
                    shtml.Append("</tr></thead>");

                    if (AgenteRecaudo != null)
                    {
                        foreach (var item in AgenteRecaudo.OrderBy(x => x.Codigo))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Codigo);
                            shtml.AppendFormat("<td >{0}</td>", item.NombreDocumento);
                            shtml.AppendFormat("<td >{0}</td>", item.NumDocumento);
                            shtml.AppendFormat("<td >{0}</td>", item.Nombres);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            //shtml.AppendFormat("<td> <a href=# onclick='updAddAgenteRecaudo({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Codigo);
                            shtml.AppendFormat("<td> <a href=# onclick='delAddAgenteRecaudo({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Codigo, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Agente" : "Activar Agente");
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

        private List<SocioNegocio> obtenerAgenteRecaudo()
        {
            List<SocioNegocio> datos = new List<SocioNegocio>();

            if (AgenteTmp != null)
            {
                AgenteTmp.ForEach(x =>
                {
                    datos.Add(new SocioNegocio
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        BPS_ID = x.Codigo,
                        TAXN_NAME = x.NombreDocumento,
                        TAX_ID = x.NumDocumento,
                        BPS_NAME = x.Nombres,
                        LOG_USER_CREAT = "klescano",
                        LOG_USER_UPDATE = "KlescanoUPD"
                    });
                });
            }
            return datos;
        }

        public JsonResult ObtenerDatosSocio(decimal codigo)
        {
            Resultado retorno = new Resultado();

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
                            NombreDocumento = socio.TAXN_NAME,
                            NumDocumento = socio.TAX_ID,
                            Nombres = socio.BPS_NAME
                        };

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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "obtener socio", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddAgenteRecaudo(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    List<DTOSocio> query = new List<DTOSocio>();
                    BLSocioNegocio servicio = new BLSocioNegocio();
                    BLREC_TAX_ID bl = new BLREC_TAX_ID();
                    var agente = servicio.Obtener(Id, GlobalVars.Global.OWNER, Constantes.ENTIDAD.OTROS);
                    decimal idDocumento = agente.TAXT_ID;
                    string documento = bl.REC_TAX_ID_GET_by_TAXT_ID(idDocumento)[0].TAXN_NAME;
                    agente.TAXN_NAME = documento;
                    DTOSocio en = new DTOSocio();

                    if (agente != null)
                    {
                        DTOSocio socioDto = new DTOSocio()
                        {
                            Codigo = agente.BPS_ID,
                            NombreDocumento = agente.TAXN_NAME,
                            NumDocumento = agente.TAX_ID,
                            Nombres = agente.BPS_NAME
                        };
                        en = socioDto;
                    }

                    AgenteRecaudo = AgenteTmp;

                    if (AgenteRecaudo != null)
                    {
                        query = (from item in AgenteRecaudo
                                 where item.Codigo == en.Codigo
                                 select item).ToList();

                        if (query.Count > 0)
                        {
                            retorno.result = 0;
                            retorno.message = "Esta transición de estado ya existe.";
                            retorno.data = Json(en, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (query.Count == 0)
                    {
                        en.Codigo = 0;
                        if (AgenteRecaudo == null) AgenteRecaudo = new List<DTOSocio>();
                        if (Convert.ToInt32(en.Codigo) <= 0)
                        {
                            decimal nuevoId = 1;
                            if (AgenteRecaudo.Count > 0) nuevoId = AgenteRecaudo.Max(x => x.Codigo) + 1;
                            //en.Codigo = nuevoId;
                            en.Codigo = Id;
                            en.Activo = true;
                            en.EnBD = false;
                            en.UsuarioCrea = "klescano";
                            //en.UsuarioModifica = "klescano";
                            en.FechaCrea = DateTime.Now;
                            AgenteRecaudo.Add(en);
                        }
                        AgenteTmp = AgenteRecaudo;
                        retorno.result = 1;
                        retorno.Code = Convert.ToInt32(en.Codigo);
                        retorno.message = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddAgente", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region  AGENTE OBLIGATORIO

        public JsonResult ListarSAagentesxDivision(decimal idOficina, decimal idDivision, string agenteRecaudo)
        {
            string owner = GlobalVars.Global.OWNER;

            List<BEAgenteRecaudo> listar = new BLAgenteRecaudo().ListarAgenteRecaudoXDivisionObligatorio(owner,idOficina, idDivision, agenteRecaudo);
            Resultado retorno = new Resultado();
            try
            {
                StringBuilder shtml = new StringBuilder();
                shtml.Append("<table class='tblAgentesxDivisionObli' border=0 width='100%;' class='k-grid k-widget' id='tblAgentesxDivisionObli'>");
                shtml.Append("<thead><tr>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >#</th>");
                shtml.Append("<th style='display:none'  class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>ID</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Nombre</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Division</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Oficina</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Rol</th>");

                if (listar != null)
                {
                    foreach (var item in listar.OrderBy(x => x.COLL_OFF_ID))
                    {                                                                                                                  //<input type='checkbox' name='chkorigen{0}' value='chkorigen{0}'/></td>"
                        shtml.Append("<tr style='background-color:white'>");
                        shtml.AppendFormat("<td style='width:1.5px; cursor:pointer;text-align:center; width:1.5px';' class='IDCellOri' ><input type='checkbox' id='{0}' name='Check' class='Check' />", "chkEstOrigen" + item.COLL_OFF_ID);
                        //shtml.AppendFormat("</td>");
                        shtml.AppendFormat("<td style='display:none;cursor:pointer;text-align:right'; class='IDEstOri'>{0}</td>", item.COLL_OFF_ID);
                        shtml.AppendFormat("<td style='width:150px;cursor:pointer;text-align:left';'class='NomEstOri'>{0}</td>", item.RECAUDADOR);
                        shtml.AppendFormat("<td style='width:50px;cursor:pointer;text-align:left';'class='NomEstOri'>{0}</td>", item.DIVISION);
                        shtml.AppendFormat("<td style='width:50px;cursor:pointer;text-align:left';'class='NomEstOri'>{0}</td>", item.OFF_NAME);
                        shtml.AppendFormat("<td style='width:50px;cursor:pointer;text-align:left';'class='NomEstOri'>{0}</td>", item.ROL);
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
        #endregion

        //public JsonResult ACBuscarRecaudador()
        //{
        //    string texto = Request.QueryString["term"];
        //    var datos = new BLSocioNegocio().AutoCompletarRecaudadores(GlobalVars.Global.OWNER, texto);

        //    List<DTOSocio> socios = new List<DTOSocio>();

        //    datos.ForEach(x =>
        //    {
        //        socios.Add(new DTOSocio
        //        {
        //            Codigo = x.BPS_ID,
        //            value = String.Format("{0} {1} {2} {3}", x.BPS_NAME, x.BPS_FIRST_NAME, x.BPS_FATH_SURNAME, x.BPS_MOTH_SURNAME)
        //        });
        //    });


        //    return Json(socios, JsonRequestBehavior.AllowGet);

        //}

        //[HttpPost]
        //public JsonResult BuscarsocioTipoDocumentoRecaudador(decimal idTipoDocumento, string nroDocumento)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            SocioNegocio recaudador = new SocioNegocio();
        //            recaudador = new BLSocioNegocio().BuscarXtipodocumentoRecaudador(idTipoDocumento, nroDocumento);

        //            retorno.result = 1;
        //            retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
        //            retorno.data = Json(recaudador, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.result = 0;
        //        retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
        //        ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "BuscarsocioTipoDocumentoRecaudador", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}

    }
}

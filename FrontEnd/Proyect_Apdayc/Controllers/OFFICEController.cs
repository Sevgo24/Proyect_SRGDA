
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
    public class OFFICEController : Base
    {
        public const string nomAplicacion = "SRGDA";
        private const string K_SESION_DIRECCION_OFI_VISTA = "___DTODirecciones_OFI_Vista";

        private const string K_SESION_DIRECCION_OFI = "___DTODirecciones_OFI";
        private const string K_SESION_DIRECCION_DEL_OFI = "___DTODireccionesDEL_OFI";
        private const string K_SESION_DIRECCION_ACT_OFI = "___DTODireccionesACT_OFI";

        private const string K_SESSION_OBSERVACION_OFI = "___DTOObservacion_OFI";
        private const string K_SESSION_OBSERVACION_DEL_OFI = "___DTOObservacionDEL_OFI";
        private const string K_SESSION_OBSERVACION_ACT_OFI = "___DTOObservacionACT_OFI";

        private const string K_SESSION_DOCUMENTO_OFI = "___DTODocumento_OFI";
        private const string K_SESSION_DOCUMENTO_DEL_OFI = "___DTODocumentoDEL_OFI";
        private const string K_SESSION_DOCUMENTO_ACT_OFI = "___DTODocumentoACT_OFI";

        private const string K_SESSION_PARAMETRO_OFI = "___DTOParametro_OFI";
        private const string K_SESSION_PARAMETRO_DEL_OFI = "___DTOParametroDEL_OFI";
        private const string K_SESSION_PARAMETRO_ACT_OFI = "___DTOParametroACT_OFI";

        private const string K_SESSION_NUMERACION_OFI = "___DTONumeracion_OFI";
        private const string K_SESSION_NUMERACION_DEL_OFI = "___DTONumeracionDEL_OFI";
        private const string K_SESSION_NUMERACION_ACT_OFI = "___DTONumeracionACT_OFI";

        private const string K_SESSION_OFICINA_OFI = "___DTOOficina_OFI";
        private const string K_SESSION_OFICINA_DEL_OFI = "___DTOOficinaDEL_OFI";
        private const string K_SESSION_OFICINA_ACT_OFI = "___DTOOficinaACT_OFI";

        //private const string K_SESSION_AGENTE_OFI = "___DTOAGENTE_OFI";

        private const string K_SESSION_CONTACTO_OFI = "__DTOOficinaContacto_OFI";
        private const string K_SESSION_CONTACTO_DEL_OFI = "___DTOOficinaContactoDEL_OFI";
        private const string K_SESSION_CONTACTO_ACT_OFI = "___DTOOficinaContactoACT_OFI";

        private const string K_SESSION_GM_OFI = "___DTOOficinaGrupoModalidad_OFI";
        private const string K_SESSION_GM_DEL_OFI = "___DTOOficinaGrupoModalidadDEL_OFI";
        private const string K_SESSION_GM_ACT_OFI = "___DTOOficinaGrupoModalidadACT_OFI";

        private const string K_SESSION_DIV_ADMINISTRATIVA = "___DTODiv_Administrativa";
        private const string K_SESSION_DIV_ADMINISTRATIVA_ACT = "___DTODiv_AdministrativaACT";
        private const string K_SESSION_DIV_ADMINISTRATIVA_DEL = "___DTODiv_AdministrativaDEL";
        private const int K_SESSION_AGENTE_PRINCIPAL = 0;

        //
        // GET: /OFFICE/
        List<DTODireccion> direcciones = new List<DTODireccion>();
        List<DTOObservacion> observaciones = new List<DTOObservacion>();
        List<DTODocumento> documentos = new List<DTODocumento>();
        List<DTOParametro> parametros = new List<DTOParametro>();
        List<DTONumeracion> numeraciones = new List<DTONumeracion>();
        List<DTOOficina> oficinas = new List<DTOOficina>();
        //List<DTORecaudadorBps> agentes = new List<DTORecaudadorBps>();
        List<DTOOficinaContacto> contacto = new List<DTOOficinaContacto>();
        List<DTOOficinaDivision> divisionesAdm = new List<DTOOficinaDivision>();
        List<DTOOficinaGrupoModalidad> divisionGrupoMod = new List<DTOOficinaGrupoModalidad>();

        #region VISTA
        public ActionResult Index()
        {
            Init(false);
            return View();
        }
        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESION_DIRECCION_ACT_OFI);
            Session.Remove(K_SESION_DIRECCION_DEL_OFI);
            Session.Remove(K_SESION_DIRECCION_OFI);

            Session.Remove(K_SESSION_OBSERVACION_OFI);
            Session.Remove(K_SESSION_OBSERVACION_ACT_OFI);
            Session.Remove(K_SESSION_OBSERVACION_DEL_OFI);

            Session.Remove(K_SESSION_DOCUMENTO_OFI);
            Session.Remove(K_SESSION_DOCUMENTO_ACT_OFI);
            Session.Remove(K_SESSION_DOCUMENTO_DEL_OFI);

            Session.Remove(K_SESSION_PARAMETRO_OFI);
            Session.Remove(K_SESSION_PARAMETRO_ACT_OFI);
            Session.Remove(K_SESSION_PARAMETRO_DEL_OFI);

            Session.Remove(K_SESSION_NUMERACION_OFI);
            Session.Remove(K_SESSION_NUMERACION_ACT_OFI);
            Session.Remove(K_SESSION_NUMERACION_DEL_OFI);

            Session.Remove(K_SESSION_OFICINA_OFI);
            Session.Remove(K_SESSION_OFICINA_ACT_OFI);
            Session.Remove(K_SESSION_OFICINA_DEL_OFI);

            Session.Remove(K_SESSION_CONTACTO_OFI);
            Session.Remove(K_SESSION_CONTACTO_DEL_OFI);
            Session.Remove(K_SESSION_CONTACTO_ACT_OFI);

            Session.Remove(K_SESION_DIRECCION_HISTORIAL_ACT);
            Session.Remove(K_SESION_DIRECCION_HISTORIAL_DEL);
            Session.Remove(K_SESION_DIRECCION_HISTORIAL);


            Session.Remove(K_SESSION_GM_OFI);
            Session.Remove(K_SESSION_GM_DEL_OFI);
            Session.Remove(K_SESSION_GM_ACT_OFI);

            Session.Remove(K_SESSION_DIV_ADMINISTRATIVA);
            Session.Remove(K_SESSION_DIV_ADMINISTRATIVA_ACT);
            Session.Remove(K_SESSION_DIV_ADMINISTRATIVA_DEL);
            return View();
        }
        #endregion

        #region Treeview

        public ActionResult Treeview()
        {
            return View();
        }

        public ActionResult ReporteTreeview()
        {
            ViewBag.Fecha = DateTime.Now.ToShortDateString();
            ViewBag.Hora = DateTime.Now.ToShortTimeString();
            ViewBag.Usuario = UsuarioActual;
            return View();
        }

        public JsonResult getTreeview()
        {
            List<BETreeview> lista = new List<BETreeview>();
            lista = new BLOffices().Usp_Get_Rec_Offices_Get_By_Off_Name_By_Hq_Ind(GlobalVars.Global.OWNER);
            BETreeview bePadre = (from p in lista where p.ManagerID == 0 select p).FirstOrDefault();
            //lista.Add(new BETreeview { cod = 0, text = "", ManagerID = null });
            lista.Add(new BETreeview { cod = bePadre.cod, text = bePadre.text + " - (Principal)", ManagerID = null });

            var padre = lista.Where(x => x.ManagerID == null).FirstOrDefault();
            SetChildren(padre, lista);


            return Json(padre, JsonRequestBehavior.AllowGet);
        }

        public void SetChildren(BETreeview model, List<BETreeview> lista)
        {
            var childs = lista.Where(x => x.ManagerID == model.cod).ToList();
            if (childs.Count > 0)
            {
                foreach (var child in childs)
                {
                    SetChildren(child, lista);
                    model.items.Add(child);
                }
            }
        }

        #endregion

        [HttpPost()]
        public JsonResult Listar(int skip, int take, int page, int pageSize, string group, string dato, int estado)
        {
            decimal idOfiTmp = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
            var lista = usp_Get_Rec_Offices_Get_By_Off_Name(GlobalVars.Global.OWNER, idOfiTmp, dato, estado, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEOffices { BEREC_OFFICE = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEOffices { BEREC_OFFICE = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEOffices> usp_Get_Rec_Offices_Get_By_Off_Name(string owner,decimal idOfiTmp, string dato, int estado, int pagina, int cantRegxPag)
        {
            return new BLOffices().usp_Get_Rec_Offices_Get_By_Off_Name(owner, idOfiTmp, dato, estado, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Eliminar(int codigo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var servicio = new BLOffices();
                    var office = new BEOffices();
                    office.OWNER = GlobalVars.Global.OWNER;
                    office.LOG_USER_UPDAT = UsuarioActual;
                    office.OFF_ID = codigo;
                    servicio.Usps_Del_Rec_Offices(office);
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

        public JsonResult ListarHQ_Ind_Treeview()
        {
            var lista = new BLOffices().Usp_Get_Rec_Offices_Get_By_Off_Name_By_Hq_Ind(GlobalVars.Global.OWNER);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        private static void validacion(out bool exito, out string msg_validacion, BEOffices entidad)
        {
            exito = true;
            msg_validacion = string.Empty;
            if (exito && string.IsNullOrEmpty(entidad.OFF_NAME))
            {
                msg_validacion = "Ingrese Oficina";
                exito = false;
            }
        }

        public JsonResult ListarDep(int skip, int take, int page, int pageSize, string group, string dato, decimal offId)
        {

            var lista = Usp_Get_Rec_Offices_By_Offname_Dep(GlobalVars.Global.OWNER, dato, offId, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEOffices { BEREC_OFFICE = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEOffices { BEREC_OFFICE = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEOffices> Usp_Get_Rec_Offices_By_Offname_Dep(string owner, string dato, decimal offId, int pagina, int cantRegxPag)
        {
            return new BLOffices().Usp_Get_Rec_Offices_By_Offname_Dep(owner, dato, offId, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult ActualizarDep(int idOff, int soff)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var servicio = new BLOffices();

                    var office = new BEOffices();

                    office.OWNER = GlobalVars.Global.OWNER;
                    office.OFF_ID = idOff;
                    office.SOFF_ID = soff;
                    office.LOG_USER_UPDAT = UsuarioActual;
                    servicio.Usp_Upd_RecOffices_by_OffId_and_SoffId(office);

                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ActualizarDep", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private static void validacionObs(out bool exito, out string msg_validacion, string msj)
        {
            exito = true;
            msg_validacion = string.Empty;
            if (exito && string.IsNullOrEmpty(msj))
            {
                msg_validacion = "Ingrese Observación";
                exito = false;
            }
        }

        [HttpPost]
        public JsonResult ListarOfiActivas()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    decimal idoff = Convert.ToInt32(Session[Constantes.Sesiones.CodigoOficina]);
                    var datos = new BLOffices().ListarOffActivasSERVICE(GlobalVars.Global.OWNER, idoff)
                     .Select(c => new SelectListItem
                     {
                         Value = Convert.ToString(c.OFF_ID),
                         Text = c.OFF_NAME.ToUpper()
                     });
                    retorno.result = 1;
                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarOfiActivas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ContadorPrincipal()
        {
            Resultado retorno = new Resultado();
            try
            {
                var count = new BLOffices().ObtenerPrincipales(GlobalVars.Global.OWNER);

                if (count >= 1)
                {
                    retorno.result = 1;
                }
                else
                {
                    retorno.result = 0;
                }
                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarOfiActivas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private static void validacionNumerador(out bool exito, out string msg_validacion, decimal offID, decimal nmrID)
        {
            exito = true;
            msg_validacion = string.Empty;
            //if (exito && string.IsNullOrEmpty(msj))
            //{
            //    msg_validacion = "Ingrese Observación";
            //    exito = false;
            //}

        }

        //PESTAÑAS
        #region DIRECCION

        private List<DTODireccion> DireccionesTmpUPDEstado
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION_ACT_OFI];
            }
            set
            {
                Session[K_SESION_DIRECCION_ACT_OFI] = value;
            }
        }

        private List<DTODireccion> DireccionesTmpDelBD
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION_DEL_OFI];
            }
            set
            {
                Session[K_SESION_DIRECCION_DEL_OFI] = value;
            }
        }

        public List<DTODireccion> DireccionesTmp
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION_OFI];
            }
            set
            {
                Session[K_SESION_DIRECCION_OFI] = value;
            }
        }

        [HttpPost]
        public JsonResult AddDireccion(DTODireccion direccion)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
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
                        direccion.EsPrincipal = "1";
                        direcciones.Add(direccion);
                    }
                    else
                    {

                        var item = direcciones.Where(x => x.Id == direccion.Id).FirstOrDefault();
                        direccion.EnBD = item.EnBD;//indicador que item viene de la BD
                        direccion.Activo = item.Activo;
                        direccion.EsPrincipal = "1";
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", "dbalvis", "add Diteccion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BEDireccion> obtenerDirecciones()
        {
            try
            {
                List<BEDireccion> datos = new List<BEDireccion>();
                if (DireccionesTmp != null)
                {
                    DireccionesTmp.ForEach(x =>
                    {
                        var obj = new BEDireccion();
                        obj.ADD_ID = x.Id;
                        obj.OWNER = GlobalVars.Global.OWNER;
                        obj.ENT_ID = Constantes.ObservacionType.oficinasRecaudo.GetHashCode();
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
                        obj.ADD_TINT = x.TipoDepa;
                        obj.ADD_INT = x.NroPiso;
                        obj.REMARK = "";
                        obj.CPO_ID = 2;
                        obj.LOG_USER_CREAT = UsuarioActual;
                        obj.MAIN_ADD = Convert.ToChar("1");
                        datos.Add(obj);
                    });
                }
                return datos;
            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddObservacion", ex);
                return null;
            }
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
                ucLogApp.ucLog.GrabarLogError("SGRDA", "dbalvis", "ObtieneDireccionTmp", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private string getRazonSocial(DTODireccion entidad)
        {

            StringBuilder rz = new StringBuilder();

            //rz.AppendFormat("{0} {1}", entidad.TipoUrbDes, entidad.Urbanizacion);

            if (!String.IsNullOrEmpty(entidad.Urbanizacion))
            {
                rz.AppendFormat("{0} {1}", entidad.TipoUrbDes, entidad.Urbanizacion);
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

            if (!String.IsNullOrEmpty(entidad.Avenida))
            {
                rz.AppendFormat(" {0} {1}", entidad.TipoAvenidaDes, entidad.Avenida);
            }

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

        #endregion

        #region DIRECCION_LISTA

        public List<DTODireccion> DireccionesTmpVista
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION_OFI_VISTA];
            }
            set
            {
                Session[K_SESION_DIRECCION_OFI_VISTA] = value;
            }
        }

        #endregion

        #region OBSERVACION

        public List<DTOObservacion> ObservacionesTmp
        {
            get
            {
                return (List<DTOObservacion>)Session[K_SESSION_OBSERVACION_OFI];
            }
            set
            {
                Session[K_SESSION_OBSERVACION_OFI] = value;
            }
        }

        private List<DTOObservacion> ObservacionesTmpUPDEstado
        {
            get
            {
                return (List<DTOObservacion>)Session[K_SESSION_OBSERVACION_ACT_OFI];
            }
            set
            {
                Session[K_SESSION_OBSERVACION_ACT_OFI] = value;
            }
        }

        private List<DTOObservacion> ObservacionesTmpDelBD
        {
            get
            {
                return (List<DTOObservacion>)Session[K_SESSION_OBSERVACION_DEL_OFI];
            }
            set
            {
                Session[K_SESSION_OBSERVACION_DEL_OFI] = value;
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
                        ENT_ID = Constantes.ObservacionType.oficinasRecaudo.GetHashCode(),
                        LOG_USER_CREAT = UsuarioActual,
                        OBS_USER = UsuarioActual
                    });
                });
            }
            return datos;
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
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'  style='width:60px'></th></tr></thead>");

                    if (observaciones != null)
                    {
                        foreach (var item in observaciones.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td nowrap>{0}</td>", item.TipoObservacionDesc);
                            shtml.AppendFormat("<td >{0}</td>", item.Observacion);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td style='width:60px'> <a href=# onclick='updAddObservacion({0});'><img src='../Images/iconos/edit.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.Id, "Modificar Observación");
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


        #endregion

        #region DOCUMENTO

        public List<DTODocumento> DocumentosTmp
        {
            get
            {
                return (List<DTODocumento>)Session[K_SESSION_DOCUMENTO_OFI];
            }
            set
            {
                Session[K_SESSION_DOCUMENTO_OFI] = value;
            }
        }
        private List<DTODocumento> DocumentosTmpUPDEstado
        {
            get
            {
                return (List<DTODocumento>)Session[K_SESSION_DOCUMENTO_ACT_OFI];
            }
            set
            {
                Session[K_SESSION_DOCUMENTO_ACT_OFI] = value;
            }
        }
        private List<DTODocumento> DocumentosTmpDelBD
        {
            get
            {
                return (List<DTODocumento>)Session[K_SESSION_DOCUMENTO_DEL_OFI];
            }
            set
            {
                Session[K_SESSION_DOCUMENTO_DEL_OFI] = value;
            }
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddDocumento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
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
                        ENT_ID = Constantes.ObservacionType.oficinasRecaudo.GetHashCode(),
                        DOC_USER = UsuarioActual,
                        LOG_USER_CREAT = UsuarioActual,
                        DOC_VERSION = 1
                    });
                });
            }
            return datos;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneDocumentoTmp", ex);
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
                    //StringBuilder shtml = new StringBuilder();
                    //shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    //shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Tipo Documento</th>");
                    //shtml.Append("<th class='k-header' >Fecha Recepción</th><th  class='k-header'>Documento</th>");
                    //shtml.Append("<th class='k-header'>Estado</th><th  class='k-header' style='width:10px></th><th  class='k-header'></th></tr></thead>");

                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='ui-state-default ui-th-column ui-th-ltr' >Id</th><th  class='ui-state-default ui-th-column ui-th-ltr' >Tipo Documento</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'  >Fecha Recepción</th><th class='ui-state-default ui-th-column ui-th-ltr' >Archivo</th>");
                    //shtml.Append("<th class='k-header'>Documentos</th><th  class='k-header'>Estado</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Estado</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Usu. Crea</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Fecha Crea</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Usu. Modi</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Fecha Modi</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' ></th></tr></thead>");

                    if (documentos != null)
                    {
                        foreach (var item in documentos.OrderBy(x => x.Id))
                        {
                            var pathWeb = System.Web.Configuration.WebConfigurationManager.AppSettings["RutaWebImgOficina"];
                            var ruta = string.Format("{0}{1}", pathWeb, item.Archivo);

                            //shtml.Append("<tr class='k-grid-content'>");
                            //shtml.AppendFormat("<td >{0}</td>", item.Id);
                            //shtml.AppendFormat("<td >{0}</td>", item.TipoDocumentoDesc);
                            //shtml.AppendFormat("<td >{0}</td>", item.FechaRecepcion.Substring(0, 10));
                            //shtml.AppendFormat("<td ><a href='#' onclick=verImagen('{0}');>Ver Imagen</a></td>", ruta);
                            ////shtml.AppendFormat("<td >{0}</td>", item.Archivo);
                            //shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            //shtml.AppendFormat("<td style='width:60px'> <a href=# onclick='updAddDocumento  ({0});'><img src='../Images/iconos/edit.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.Id, "Modificar Documento");
                            //shtml.AppendFormat("<a href=# onclick='delAddDocumento({0});'><img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Documento" : "Activar Documento");
                            //shtml.Append("</td>");
                            //shtml.Append("</tr>");

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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarDocumento", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PARAMETRO

        public List<DTOParametro> ParametrosTmp
        {
            get
            {
                return (List<DTOParametro>)Session[K_SESSION_PARAMETRO_OFI];
            }
            set
            {
                Session[K_SESSION_PARAMETRO_OFI] = value;
            }
        }

        private List<DTOParametro> ParametrosTmpUPDEstado
        {
            get
            {
                return (List<DTOParametro>)Session[K_SESSION_PARAMETRO_ACT_OFI];
            }
            set
            {
                Session[K_SESSION_PARAMETRO_ACT_OFI] = value;
            }
        }

        private List<DTOParametro> ParametrosTmpDelBD
        {
            get
            {
                return (List<DTOParametro>)Session[K_SESSION_PARAMETRO_DEL_OFI];
            }
            set
            {
                Session[K_SESSION_PARAMETRO_DEL_OFI] = value;
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

        #region CONTACTO
        public List<DTOOficinaContacto> AsociadosTmp
        {
            get
            {
                return (List<DTOOficinaContacto>)Session[K_SESSION_CONTACTO_OFI];
            }
            set
            {
                Session[K_SESSION_CONTACTO_OFI] = value;
            }
        }
        private List<DTOOficinaContacto> AsociadosTmpUPDEstado
        {
            get
            {
                return (List<DTOOficinaContacto>)Session[K_SESSION_CONTACTO_ACT_OFI];
            }
            set
            {
                Session[K_SESSION_CONTACTO_ACT_OFI] = value;
            }
        }
        private List<DTOOficinaContacto> AsociadosTmpDelBD
        {
            get
            {
                return (List<DTOOficinaContacto>)Session[K_SESSION_CONTACTO_DEL_OFI];
            }
            set
            {
                Session[K_SESSION_CONTACTO_DEL_OFI] = value;
            }
        }

        [HttpPost]
        public JsonResult AddAsociado(DTOOficinaContacto entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    contacto = AsociadosTmp;
                    if (contacto == null) contacto = new List<DTOOficinaContacto>();
                    //if (Convert.ToInt32(entidad.Id) <= 0)
                    if (contacto.Where(x => x.Id == entidad.Id).Count() == 0)
                    {
                        //decimal nuevoId = 1;
                        //if (contacto.Count > 0) nuevoId = contacto.Max(x => x.Id) + 1;
                        //entidad.Id = nuevoId;

                        entidad.Activo = true;
                        entidad.EnBD = false;
                        entidad.UsuarioCrea = UsuarioActual;
                        entidad.FechaCrea = DateTime.Now;
                        contacto.Add(entidad);
                    }
                    else
                    {
                        var item = contacto.Where(x => x.Id == entidad.Id).FirstOrDefault();
                        entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                        entidad.Activo = item.Activo;
                        entidad.UsuarioCrea = item.UsuarioCrea;
                        entidad.FechaCrea = item.FechaCrea;
                        if (entidad.EnBD)
                        {
                            entidad.UsuarioModifica = UsuarioActual;
                            entidad.FechaModifica = DateTime.Now;
                        }
                        contacto.Remove(item);
                        contacto.Add(entidad);
                    }
                    AsociadosTmp = contacto;
                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddAsociado", ex);
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
                    contacto = AsociadosTmp;
                    if (contacto != null)
                    {
                        var objDel = contacto.Where(x => x.Id == id).FirstOrDefault();
                        if (objDel != null)
                        {

                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (AsociadosTmpUPDEstado == null) AsociadosTmpUPDEstado = new List<DTOOficinaContacto>();
                                if (AsociadosTmpDelBD == null) AsociadosTmpDelBD = new List<DTOOficinaContacto>();

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
                                contacto.Remove(objDel);
                                contacto.Add(objDel);
                            }
                            else
                            {
                                contacto.Remove(objDel);
                            }
                            AsociadosTmp = contacto;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddAsociado", ex);
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneAsociadoTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BESocioNegocioOficina> obtenerAsociados()
        {
            List<BESocioNegocioOficina> datos = new List<BESocioNegocioOficina>();

            if (AsociadosTmp != null)
            {
                AsociadosTmp.ForEach(x =>
                {
                    datos.Add(new BESocioNegocioOficina
                    {
                        //SEQUENCE = x.EnBD ? x.Id : decimal.Zero,

                        BPS_ID = x.Id,
                        OFF_ID = x.off_id,
                        ROL_ID = x.rol_id,
                        OWNER = GlobalVars.Global.OWNER,
                        LOG_USER_CREAT = UsuarioActual,

                    });
                });
            }
            return datos;
        }

        public JsonResult ListarAsociado()
        {
            contacto = AsociadosTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='ui-state-default ui-th-column ui-th-ltr' >Id</th><th  class='ui-state-default ui-th-column ui-th-ltr' >Nombre del Contacto</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >ROL</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Estado</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Usu. Crea</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Fecha Crea</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Usu. Modi</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Fecha Modi</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' ></th></tr></thead>");

                    if (contacto != null)
                    {
                        foreach (var item in contacto.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.nombre);
                            shtml.AppendFormat("<td >{0}</td>", item.rol_descripcion);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td> <a href=# onclick='updAddAsociado({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                            shtml.AppendFormat("<a href=# onclick='delAddAsociado({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Contacto" : "Activar Contacto");
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarAsociado", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region DIVISIONES
        public List<DTOOficinaDivision> DivisionTmp
        {
            get
            {
                return (List<DTOOficinaDivision>)Session[K_SESSION_DIV_ADMINISTRATIVA];
            }
            set
            {
                Session[K_SESSION_DIV_ADMINISTRATIVA] = value;
            }
        }
        public List<DTOOficinaDivision> DivisionTmpUPD
        {
            get
            {
                return (List<DTOOficinaDivision>)Session[K_SESSION_DIV_ADMINISTRATIVA_ACT];
            }
            set
            {
                Session[K_SESSION_DIV_ADMINISTRATIVA_ACT] = value;
            }
        }
        public List<DTOOficinaDivision> DivisionTmpDelBD
        {
            get
            {
                return (List<DTOOficinaDivision>)Session[K_SESSION_DIV_ADMINISTRATIVA_DEL];
            }
            set
            {
                Session[K_SESSION_DIV_ADMINISTRATIVA_DEL] = value;
            }
        }

        [HttpPost]
        public JsonResult AddDivisionAdm(decimal Id)
        {
            Resultado retorno = new Resultado();
            DTOOficinaDivision entidad = null;
            try
            {
                if (!isLogout(ref retorno))
                {
                    divisionesAdm = DivisionTmp;
                    if (divisionesAdm == null) divisionesAdm = new List<DTOOficinaDivision>();
                    bool existe = divisionesAdm.Where(x => x.idDivision == Id).ToList().Count > 0 ? true : false;
                    if (!existe)
                    {
                        var division = new BLREF_DIVISIONES().Obtener(GlobalVars.Global.OWNER, Id);

                        if (division != null)
                        {
                            entidad = new DTOOficinaDivision();
                            decimal nuevoId = 1;
                            if (divisionesAdm.Count > 0) nuevoId = divisionesAdm.Max(x => x.Id) + 1;

                            entidad.Id = nuevoId;
                            entidad.idDivision = division.DAD_ID;
                            entidad.Division = division.DAD_NAME;
                            entidad.Detalle = division.DIV_DESCRIPTION;

                            entidad.FechaCrea = DateTime.Now;
                            entidad.UsuarioCrea = UsuarioActual;
                            entidad.Activo = true;
                            entidad.EnBD = false;

                            divisionesAdm.Add(entidad);
                            DivisionTmp = divisionesAdm;
                            retorno.result = 1;
                            retorno.message = "OK";
                        }
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "La división administrativa fue agregada anteriormene.\r\nSeleccione otra división administrativa.";
                    }
                }
            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddDivisionAdm", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult delAddDivisionAdm(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    divisionesAdm = DivisionTmp;
                    if (divisionesAdm != null)
                    {
                        var objDel = divisionesAdm.Where(x => x.Id == id).FirstOrDefault();
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;
                                if (DivisionTmpUPD == null) DivisionTmpUPD = new List<DTOOficinaDivision>();
                                if (DivisionTmpDelBD == null) DivisionTmpDelBD = new List<DTOOficinaDivision>();
                                var itemUpd = DivisionTmpUPD.Where(x => x.Id == id).FirstOrDefault();
                                var itemDel = DivisionTmpDelBD.Where(x => x.Id == id).FirstOrDefault();
                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) DivisionTmpUPD.Add(objDel);
                                    if (itemDel != null) DivisionTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) DivisionTmpDelBD.Add(objDel);
                                    if (itemUpd != null) DivisionTmpUPD.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                divisionesAdm.Remove(objDel);
                                divisionesAdm.Add(objDel);
                            }
                            else
                            {
                                divisionesAdm.Remove(objDel);
                                //if (FacturasDetTmp != null && FacturasDetTmp.Where(s => s.idBps == id).Count() > 0)
                                //    FacturasDetTmp.RemoveAll(s => s.idBps == id);
                            }
                            DivisionTmp = divisionesAdm;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "delAddDivisionAdm", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarDivisionAdm(string accion)
        {
            divisionesAdm = DivisionTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tblCliente' border=0 width='100%;'  ><thead><tr>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Id</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='display:none' >IdDivision</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >División Administrativa</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Detalle</th>");
                    //shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Fecha Vigencia</th>");
                    //shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Fecha Baja</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Agregar Modalidad</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Agregar Numeradores/Serie</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='width:30px'></th></tr></thead>");

                    if (divisionesAdm != null)
                    {
                        int contador = 0;
                        foreach (var item in divisionesAdm.OrderBy(x => x.Id))
                        {
                            contador += 1;
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.AppendFormat("<td style='width: 20px;text-align:center;color: black' class='idDivAdmOficina'>{0}</td>", item.Id);
                            shtml.AppendFormat("<td style='width:100px;text-align:center;color: black; display:none' class='idDivAdm'>{0}</td>", item.idDivision);
                            shtml.AppendFormat("<td style='width:120px;text-align:center;color: black'>{0}</td>", item.Division);
                            shtml.AppendFormat("<td style='width:150px; color: black'>{0}</td>", item.Detalle);
                            //shtml.AppendFormat("<td style='width:170px;text-align:center;color: black' class='FechaIni'>{0}</td>", "FECHA INI.");
                            //shtml.AppendFormat("<td style='width:170px;text-align:center;color: black'>{0}</td>", "FECHA VEN.");
                            shtml.AppendFormat("<td style='width:190px;text-align:center;''> <a href=# onclick='AbrirPoPupAddGrupoModalidad({0});'> <img src='../Images/botones/invoice_more.png' title='Agregar Grupo de Modalidad.' border=0></a>", item.Id);
                            shtml.AppendFormat("<td style='width:190px;text-align:center;''> <a href=# onclick='AbrirPoPupAddNumeradores({0});'> <img src='../Images/botones/invoice_more.png' title='Agregar Grupo de Modalidad.' border=0></a>", item.Id);

                            //if (!item.EnBD)
                            shtml.AppendFormat("<td style='width:30px;text-align:center;'> <a href=# onclick='delDivisionAdm({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Cliente" : "Activar Cliente");
                            shtml.Append("</td></tr>");

                            shtml.Append("<tr style='background-color:white'>");

                            shtml.Append("<td colspan='3'></td>");
                            shtml.Append("<td style='vertical-align: text-top'><div id='divDivAdmGrupoMod" + item.Id.ToString() + "'>");
                            if (GrupoModalidadesTmp != null && GrupoModalidadesTmp.Where(p => p.IdOficinaDiv == item.Id).Count() > 0)
                                shtml.Append(getHtmlListarDivGrupoModalidad(item.Id));
                            shtml.Append("</div></td>");

                            shtml.Append("<td style='vertical-align: text-top'><div id='divDivAdmNumerador" + item.Id.ToString() + "'>");
                            if (NumeracionesTmp != null && NumeracionesTmp.Where(p => p.IdOficinaDiv == item.Id).Count() > 0)
                                shtml.Append(getHtmlListarDivNumeracion(item.Id));
                            shtml.Append("</div></td>");

                            shtml.Append("</tr>");
                            if (divisionesAdm.Count != contador)
                                shtml.Append("<tr><td colspan='20' ><hr style'display: block;height: 1px;border: 0;border-top: 1px solid #ccc;margin: 1em 0;padding: 0;'></hr></td></tr>");
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarAgenteRecaudo", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BEDivisionRecaudador> obtenerDivisionAdm()
        {
            List<BEDivisionRecaudador> datos = new List<BEDivisionRecaudador>();
            if (DivisionTmp != null)
            {
                DivisionTmp.ForEach(x =>
                {
                    datos.Add(new BEDivisionRecaudador
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        ID_COLL_DIV = x.Id,
                        DAD_ID = x.idDivision,
                        LOG_USER_CREAT = UsuarioActual,
                    });
                });
            }
            return datos;
        }

        #endregion

        #region GRUPO_MODALIDAD
        public List<DTOOficinaGrupoModalidad> GrupoModalidadesTmp
        {
            get
            {
                return (List<DTOOficinaGrupoModalidad>)Session[K_SESSION_GM_OFI];
            }
            set
            {
                Session[K_SESSION_GM_OFI] = value;
            }
        }
        private List<DTOOficinaGrupoModalidad> GrupoModalidadesTmpUPDEstado
        {
            get
            {
                return (List<DTOOficinaGrupoModalidad>)Session[K_SESSION_GM_ACT_OFI];
            }
            set
            {
                Session[K_SESSION_GM_ACT_OFI] = value;
            }
        }
        private List<DTOOficinaGrupoModalidad> GrupoModalidadesTmpDelBD
        {
            get
            {
                return (List<DTOOficinaGrupoModalidad>)Session[K_SESSION_GM_DEL_OFI];
            }
            set
            {
                Session[K_SESSION_GM_DEL_OFI] = value;
            }
        }

        [HttpPost]
        public JsonResult AddGrupoModalidad(string idGrupoModalidad, decimal IdOficinaDiv)
        {
            Resultado retorno = new Resultado();
            DTOOficinaGrupoModalidad entidad = null;
            try
            {
                if (!isLogout(ref retorno))
                {
                    divisionGrupoMod = GrupoModalidadesTmp;
                    if (divisionGrupoMod == null) divisionGrupoMod = new List<DTOOficinaGrupoModalidad>();

                    decimal IdDivision = DivisionTmp.Where(d => d.Id == IdOficinaDiv).FirstOrDefault().idDivision;
                    bool existe = (divisionGrupoMod != null && divisionGrupoMod.Where(x => x.IdOficinaDiv == IdOficinaDiv && x.IdGM == idGrupoModalidad).ToList().Count > 0) ? true : false;
                    if (!existe)
                    {
                        var GrupoMod = new BLREC_MOD_GROUP().Obtener(GlobalVars.Global.OWNER, idGrupoModalidad);
                        if (GrupoMod != null)
                        {
                            entidad = new DTOOficinaGrupoModalidad();
                            decimal nuevoId = 1;
                            if (divisionGrupoMod.Count > 0) nuevoId = divisionGrupoMod.Max(x => x.Id) + 1;
                            entidad.Id = nuevoId;
                            entidad.IdOficinaDiv = IdOficinaDiv;
                            entidad.IdDivision = IdDivision;
                            entidad.IdGM = idGrupoModalidad;
                            entidad.DescripcionGM = GrupoMod.MOG_DESC;
                            entidad.FechaCrea = DateTime.Now;
                            entidad.UsuarioCrea = UsuarioActual;
                            entidad.Activo = true;
                            entidad.EnBD = false;
                            divisionGrupoMod.Add(entidad);
                            GrupoModalidadesTmp = divisionGrupoMod;
                            retorno.result = 1;
                            retorno.message = "OK";
                        }
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "El Grupo de la Modalidad fue agregada anteriormene.\r\nSeleccione otro Grupo de la Modalidad.";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddGrupoModalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddGrupoModalidad(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    divisionGrupoMod = GrupoModalidadesTmp;
                    if (divisionGrupoMod != null)
                    {
                        var objDel = divisionGrupoMod.Where(x => x.Id == id).FirstOrDefault();
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (GrupoModalidadesTmpUPDEstado == null) GrupoModalidadesTmpUPDEstado = new List<DTOOficinaGrupoModalidad>();
                                if (GrupoModalidadesTmpDelBD == null) GrupoModalidadesTmpDelBD = new List<DTOOficinaGrupoModalidad>();

                                var itemUpd = GrupoModalidadesTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                                var itemDel = GrupoModalidadesTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) GrupoModalidadesTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) GrupoModalidadesTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) GrupoModalidadesTmpDelBD.Add(objDel);
                                    if (itemUpd != null) GrupoModalidadesTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                divisionGrupoMod.Remove(objDel);
                                divisionGrupoMod.Add(objDel);
                            }
                            else
                            {
                                divisionGrupoMod.Remove(objDel);
                            }
                            GrupoModalidadesTmp = divisionGrupoMod;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddGrupoModalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public StringBuilder getHtmlListarDivGrupoModalidad(decimal IdOficinaDiv)
        {
            var detalleGrupoModalidad = GrupoModalidadesTmp.Where(p => p.IdOficinaDiv == IdOficinaDiv).ToList();
            StringBuilder shtml = new StringBuilder();
            shtml.Append("<table id='tblOficinaDiv' border=0 width='100%;' class='k-grid k-widget'><thead><tr>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Id</th>");
            shtml.Append("<th style='display:none'>IdGrupoModalidad</th>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Grupo Modalidad</th>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='width:30px'></th></tr></thead>");

            if (detalleGrupoModalidad != null)
            {
                foreach (var item in detalleGrupoModalidad.OrderBy(x => x.Id))
                {
                    shtml.Append("<tr style='background-color:white'>");
                    shtml.AppendFormat("<td style='text-align:center; color: black'>{0}</td>", item.Id);
                    shtml.AppendFormat("<td style='display:none'>{0}</td>", item.IdGM);
                    shtml.AppendFormat("<td style='color: black; width:60p'>{0}</td>", item.DescripcionGM);
                    shtml.AppendFormat("<td style='text-align:center; width:30px'> <a href=# onclick='delGrupoModalidad({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Modalidad" : "Activar Modalidad");
                    shtml.Append("</td></tr>");
                }
            }
            shtml.Append(" </table>");
            return shtml;
        }

        private List<BEOficinaDivisionModalidad> obtenerGrupoModalidad()
        {
            List<BEOficinaDivisionModalidad> datos = new List<BEOficinaDivisionModalidad>();
            if (GrupoModalidadesTmp != null)
            {
                GrupoModalidadesTmp.ForEach(x =>
                {
                    datos.Add(new BEOficinaDivisionModalidad
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        DIV_RiGHTS_ID = x.Id,
                        ID_COLL_DIV = x.IdOficinaDiv,
                        DAD_ID = x.IdDivision,
                        MOG_ID = x.IdGM,
                        LOG_USER_CREAT = UsuarioActual,
                        LOG_USER_UPDAT = UsuarioActual
                    });
                });
            }
            return datos;
        }
        #endregion

        #region NUMERADORES
        public List<DTONumeracion> NumeracionesTmp
        {
            get
            {
                return (List<DTONumeracion>)Session[K_SESSION_NUMERACION_OFI];
            }
            set
            {
                Session[K_SESSION_NUMERACION_OFI] = value;
            }
        }
        private List<DTONumeracion> NumeracionesTmpUPDEstado
        {
            get
            {
                return (List<DTONumeracion>)Session[K_SESSION_NUMERACION_ACT_OFI];
            }
            set
            {
                Session[K_SESSION_NUMERACION_ACT_OFI] = value;
            }
        }
        private List<DTONumeracion> NumeracionesTmpDelBD
        {
            get
            {
                return (List<DTONumeracion>)Session[K_SESSION_NUMERACION_DEL_OFI];
            }
            set
            {
                Session[K_SESSION_NUMERACION_DEL_OFI] = value;
            }
        }

        [HttpPost]
        public JsonResult AddNumeracion(decimal IdOficinaDiv, decimal idCorrelativo)
        {
            Resultado retorno = new Resultado();
            DTONumeracion entidad = null;
            try
            {
                if (!isLogout(ref retorno))
                {
                    numeraciones = NumeracionesTmp;
                    if (numeraciones == null) numeraciones = new List<DTONumeracion>();
                    decimal IdDivision = DivisionTmp.Where(d => d.Id == IdOficinaDiv).FirstOrDefault().idDivision;
                    bool existe = (NumeracionesTmp != null && NumeracionesTmp.Where(x => x.IdOficinaDiv == IdOficinaDiv && x.IdNumerador == idCorrelativo).ToList().Count > 0) ? true : false;
                    if (!existe)
                    {
                        var correlativo = new BLREC_NUMBERING().ObtenerNombre(GlobalVars.Global.OWNER, idCorrelativo);
                        if (correlativo != null)
                        {
                            entidad = new DTONumeracion();
                            decimal nuevoId = 1;
                            if (numeraciones.Count > 0) nuevoId = numeraciones.Max(x => x.Id) + 1;
                            entidad.Id = nuevoId;
                            entidad.IdOficinaDiv = IdOficinaDiv;
                            entidad.IdNumerador = idCorrelativo;
                            entidad.IdDivision = IdDivision;

                            entidad.Tipo = correlativo.NMR_TDESC;
                            entidad.Serie = correlativo.NMR_SERIAL;
                            entidad.Descripcion = correlativo.NMR_NAME;

                            entidad.FechaCrea = DateTime.Now;
                            entidad.UsuarioCrea = UsuarioActual;

                            entidad.Activo = true;
                            entidad.EnBD = false;
                            numeraciones.Add(entidad);
                            NumeracionesTmp = numeraciones;
                            retorno.result = 1;
                            retorno.message = "OK";
                        }
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "El Numerador fue agregado anteriormene.\r\nSeleccione otro Grupo de la Modalidad.";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddNumeracion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddNumeracion(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    numeraciones = NumeracionesTmp;
                    if (numeraciones != null)
                    {
                        var objDel = numeraciones.Where(x => x.Id == id).FirstOrDefault();
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (NumeracionesTmpUPDEstado == null) NumeracionesTmpUPDEstado = new List<DTONumeracion>();
                                if (NumeracionesTmpDelBD == null) NumeracionesTmpDelBD = new List<DTONumeracion>();

                                var itemUpd = NumeracionesTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                                var itemDel = NumeracionesTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) NumeracionesTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) NumeracionesTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) NumeracionesTmpDelBD.Add(objDel);
                                    if (itemUpd != null) NumeracionesTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                numeraciones.Remove(objDel);
                                numeraciones.Add(objDel);
                            }
                            else
                            {
                                numeraciones.Remove(objDel);
                            }
                            NumeracionesTmp = numeraciones;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddNumeracion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public StringBuilder getHtmlListarDivNumeracion(decimal IdOficinaDiv)
        {
            var detalleNumeraciones = NumeracionesTmp.Where(p => p.IdOficinaDiv == IdOficinaDiv).ToList();
            StringBuilder shtml = new StringBuilder();
            shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
            shtml.Append("<thead><tr><th class='ui-state-default ui-th-column ui-th-ltr' >Id</th>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Tipo</th>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Serie</th>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'  style='width:30px'></th></tr></thead>");

            if (detalleNumeraciones != null)
            {
                foreach (var item in detalleNumeraciones.OrderBy(x => x.Id))
                {
                    shtml.Append("<tr class='k-grid-content'>");
                    shtml.AppendFormat("<td style='text-align:center;'>{0}</td>", item.Id);
                    shtml.AppendFormat("<td style='text-align:center;'>{0}</td>", item.Tipo);
                    shtml.AppendFormat("<td style='text-align:center;'>{0}</td>", item.Serie); ;
                    shtml.AppendFormat("<td style='width:30px'><a href=# onclick='delNumerador({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Numeración" : "Activar Numeración");
                    shtml.Append("</td></tr>");
                }
            }
            shtml.Append("</table>");
            return shtml;
        }


        private List<BENumeradorOficina> obtenerNumeraciones()
        {
            List<BENumeradorOficina> datos = new List<BENumeradorOficina>();
            if (NumeracionesTmp != null)
            {
                NumeracionesTmp.ForEach(x =>
                {
                    datos.Add(new BENumeradorOficina
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        NUM_ID=x.Id,
                        NMR_ID = x.IdNumerador,
                        ID_COLL_DIV = x.IdOficinaDiv,
                        DAD_ID = x.IdDivision,
                        LOG_USER_CREAT = UsuarioActual
                    });
                });
            }
            return datos;
        }
        #endregion

        [HttpPost]
        public JsonResult ObtenerSocioDocumento(decimal tipo, string nro_tipo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    //BLSocioNegocio servicio = new BLSocioNegocio();
                    BLOffices servicio = new BLOffices();
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
        // ****************************************************************************************************** //
        // INSERTAR OFICINA
        [HttpPost]
        public JsonResult InsertarOficina(DTOOficina oficina)
        {
            bool exito = true;
            Resultado retorno = new Resultado();
            string msg_validacion = "";
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEOffices obj = new BEOffices();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.OFF_NAME = oficina.OFF_NAME;
                    obj.HQ_IND = oficina.HQ_IND;
                    if (oficina.HQ_IND.Equals("Y"))
                        obj.SOFF_ID = 0;
                    else
                        obj.SOFF_ID = Convert.ToInt32(oficina.SOFF_ID);
                    obj.LOG_USER_CREAT = UsuarioActual;
                    obj.ADD_ID = oficina.ADD_ID;
                    obj.OFF_CC = oficina.OFF_CC;
                    obj.OFF_TYPE = oficina.OFF_TYPE;
                    obj.BPS_ID = oficina.BPS_ID;
                    obj.OFF_ID_PRE = oficina.OFF_ID_PRE;

                    obj.Observaciones = obtenerObservaciones();
                    obj.Parametros = ObtenerParametros();
                    obj.Documentos = obtenerDocumentos();
                    obj.Direcciones = obtenerDirecciones();
                    obj.Contacto = obtenerAsociados();

                    obj.DivisionAdm = obtenerDivisionAdm();
                    obj.DivisionAdmGrupoModalidad = obtenerGrupoModalidad();
                    obj.DivisionAdmNumerador = obtenerNumeraciones();

                    if (oficina.OFF_ID == 0)
                    {
                        var datos = new BLOffices().Insertar(obj);
                    }
                    else
                    {
                        obj.OFF_ID = Convert.ToInt32(oficina.OFF_ID);
                        obj.LOG_USER_UPDAT = UsuarioActual;
                        //Eliminar - Activar
                        #region observacion
                        //Observacion eliminar
                        List<BEObservationGral> listaObsDel = null;
                        if (ObservacionesTmpDelBD != null)
                        {
                            listaObsDel = new List<BEObservationGral>();
                            ObservacionesTmpDelBD.ForEach(x => { listaObsDel.Add(new BEObservationGral { OBS_ID = x.Id }); });
                        }
                        //Observacion activar
                        List<BEObservationGral> listaObsUpdEst = null;
                        if (ObservacionesTmpUPDEstado != null)
                        {
                            listaObsUpdEst = new List<BEObservationGral>();
                            ObservacionesTmpUPDEstado.ForEach(x => { listaObsUpdEst.Add(new BEObservationGral { OBS_ID = x.Id }); });
                        }
                        #endregion
                        #region parametro
                        List<BEParametroGral> listaParDel = null;
                        if (ParametrosTmpDelBD != null)
                        {
                            listaParDel = new List<BEParametroGral>();
                            ParametrosTmpDelBD.ForEach(x => { listaParDel.Add(new BEParametroGral { PAR_ID = x.Id }); });
                        }

                        List<BEParametroGral> listaParUpdEst = null;
                        if (ParametrosTmpUPDEstado != null)
                        {
                            listaParUpdEst = new List<BEParametroGral>();
                            ParametrosTmpUPDEstado.ForEach(x => { listaParUpdEst.Add(new BEParametroGral { PAR_ID = x.Id }); });
                        }
                        #endregion
                        #region documento
                        List<BEDocumentoGral> listaDocDel = null;
                        if (DocumentosTmpDelBD != null)
                        {
                            listaDocDel = new List<BEDocumentoGral>();
                            DocumentosTmpDelBD.ForEach(x => { listaDocDel.Add(new BEDocumentoGral { DOC_ID = x.Id }); });
                        }

                        List<BEDocumentoGral> listaDocUpdEst = null;
                        if (DocumentosTmpUPDEstado != null)
                        {
                            listaDocUpdEst = new List<BEDocumentoGral>();
                            DocumentosTmpUPDEstado.ForEach(x => { listaDocUpdEst.Add(new BEDocumentoGral { DOC_ID = x.Id }); });
                        }
                        #endregion
                        #region direccion
                        List<BEDireccion> listaDirDel = null;
                        if (DireccionesTmpDelBD != null)
                        {
                            listaDirDel = new List<BEDireccion>();
                            DireccionesTmpDelBD.ForEach(x => { listaDirDel.Add(new BEDireccion { ADD_ID = x.Id }); });
                        }
                        List<BEDireccion> listaDirUpdEst = null;
                        if (DireccionesTmpUPDEstado != null)
                        {
                            listaDirUpdEst = new List<BEDireccion>();
                            DireccionesTmpUPDEstado.ForEach(x => { listaDirUpdEst.Add(new BEDireccion { ADD_ID = x.Id }); });
                        }
                        #endregion            
                        #region asociados
                        List<BESocioNegocioOficina> listaAsoDel = null;
                        if (AsociadosTmpDelBD != null)
                        {
                            listaAsoDel = new List<BESocioNegocioOficina>();
                            AsociadosTmpDelBD.ForEach(x => { listaAsoDel.Add(new BESocioNegocioOficina { BPS_ID = x.Id }); });
                        }
                        List<BESocioNegocioOficina> listaAsoUpdEst = null;
                        if (AsociadosTmpUPDEstado != null)
                        {
                            listaAsoUpdEst = new List<BESocioNegocioOficina>();
                            AsociadosTmpUPDEstado.ForEach(x => { listaAsoUpdEst.Add(new BESocioNegocioOficina { BPS_ID = x.Id }); });
                        }
                        #endregion

                        #region divisiones
                        //Eliminar
                        List<BEDivisionRecaudador> listaDivisionDel = null;
                        if (DivisionTmpDelBD != null)
                        {
                            listaDivisionDel = new List<BEDivisionRecaudador>();
                            DivisionTmpDelBD.ForEach(x => { listaDivisionDel.Add(new BEDivisionRecaudador { ID_COLL_DIV = x.Id }); });
                        }
                        //Activar
                        List<BEDivisionRecaudador> listaDivisionUpdEst = null;
                        if (DivisionTmpUPD != null)
                        {
                            listaDivisionUpdEst = new List<BEDivisionRecaudador>();
                            DivisionTmpUPD.ForEach(x => { listaDivisionUpdEst.Add(new BEDivisionRecaudador { ID_COLL_DIV = x.Id }); });
                        }
                        #endregion
                        #region grupo_modalidad
                        List<BEGrupoModalidadOficina> listaGMDel = null;
                        if (GrupoModalidadesTmpDelBD != null)
                        {
                            listaGMDel = new List<BEGrupoModalidadOficina>();
                            GrupoModalidadesTmpDelBD.ForEach(x => { listaGMDel.Add(new BEGrupoModalidadOficina { DIV_RiGHTS_ID = x.Id, LOG_USER_CREAT = UsuarioActual, LOG_USER_UPDATE = UsuarioActual }); });
                        }
                        //Grupo Modalidad  activar
                        List<BEGrupoModalidadOficina> listaGMUpdEst = null;
                        if (GrupoModalidadesTmpUPDEstado != null)
                        {
                            listaGMUpdEst = new List<BEGrupoModalidadOficina>();
                            GrupoModalidadesTmpUPDEstado.ForEach(x => { listaGMUpdEst.Add(new BEGrupoModalidadOficina { DIV_RiGHTS_ID = x.Id, LOG_USER_CREAT = UsuarioActual, LOG_USER_UPDATE = UsuarioActual }); });
                        }
                        #endregion
                        #region numeración
                        List<BENumeradorOficina> listaNumDel = null;
                        if (NumeracionesTmpDelBD != null)
                        {
                            listaNumDel = new List<BENumeradorOficina>();
                            NumeracionesTmpDelBD.ForEach(x => { listaNumDel.Add(new BENumeradorOficina { NUM_ID = x.Id, LOG_USER_CREAT = UsuarioActual, LOG_USER_UPDAT = UsuarioActual }); });
                        }
                        List<BENumeradorOficina> listaNumUpdEst = null;
                        if (NumeracionesTmpUPDEstado != null)
                        {
                            listaNumUpdEst = new List<BENumeradorOficina>();
                            NumeracionesTmpUPDEstado.ForEach(x => { listaNumUpdEst.Add(new BENumeradorOficina { NUM_ID = x.Id, LOG_USER_CREAT = UsuarioActual, LOG_USER_UPDAT = UsuarioActual }); });
                        }
                        #endregion

                        var datos = new BLOffices().Actualizar(obj,
                                                                listaDirDel, listaDirUpdEst, // Dirección
                                                                listaObsDel, listaObsUpdEst, // Observaciones
                                                                listaDocDel, listaDocUpdEst, // Documento
                                                                listaParDel, listaParUpdEst, // Parametro

                                                                listaAsoDel, listaAsoUpdEst, // Asociado
                                                                listaDivisionDel, listaDivisionUpdEst,
                                                                listaGMDel, listaGMUpdEst, // Grupo Modalidad
                                                                listaNumDel, listaNumUpdEst // NUmeradores
                                                                );
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "InsertarOficina", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        // OBTENER OFICINA
        [HttpPost]
        public JsonResult ObtieneOficina(decimal id)
        {
            parametros = ParametrosTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLOffices servicio = new BLOffices();
                    var oficina = servicio.Obtener(GlobalVars.Global.OWNER, id);
                    if (oficina != null)
                    {
                        #region obtener_cabecera
                        DTOOficina oficinaDTO = new DTOOficina()
                        {
                            OFF_ID = oficina.OFF_ID,
                            OFF_NAME = oficina.OFF_NAME,
                            HQ_IND = oficina.HQ_IND,
                            SOFF_ID = oficina.SOFF_ID,
                            ADD_ID = oficina.ADD_ID,
                            OFF_TYPE = oficina.OFF_TYPE,
                            OFF_CC = oficina.OFF_CC,
                            BPS_ID = oficina.BPS_ID,
                            SOCIO = oficina.SOCIO
                        };
                        #endregion
                        #region obtener_observaciones
                        if (oficina.Observaciones != null)
                        {
                            observaciones = new List<DTOObservacion>();
                            if (oficina.Observaciones != null)
                            {
                                oficina.Observaciones.ForEach(s =>
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
                        #region obtener_parametros
                        if (oficina.Parametros != null)
                        {
                            parametros = new List<DTOParametro>();
                            if (oficina.Parametros != null)
                            {
                                oficina.Parametros.ForEach(s =>
                                {
                                    parametros.Add(new DTOParametro
                                    {
                                        Id = (s.PAR_ID),
                                        Descripcion = s.PAR_VALUE,
                                        TipoParametro = Convert.ToString(s.PAR_TYPE),
                                        TipoParametroDesc = new BLTipoParametro().Obtener(GlobalVars.Global.OWNER, s.PAR_TYPE).PAR_DESC,
                                        EnBD = true,
                                        Activo = s.ENDS.HasValue ? false : true
                                    });
                                });
                                ParametrosTmp = parametros;
                            }
                        }
                        #endregion
                        #region obtener_documentos
                        if (oficina.Documentos != null)
                        {
                            documentos = new List<DTODocumento>();
                            if (oficina.Documentos != null)
                            {
                                oficina.Documentos.ForEach(s =>
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
                        #endregion

                        #region obtener_direccion
                        if (oficina.Direcciones != null)
                        {
                            direcciones = new List<DTODireccion>();
                            if (oficina.Direcciones != null)
                            {
                                oficina.Direcciones.ForEach(s =>
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
                                        TipoDepa = s.ADD_TINT,
                                        NroPiso = s.ADD_INT,
                                        EnBD = true,
                                        Activo = s.ENDS.HasValue ? false : true,
                                        DescripcionUbigeo = new BLUbigeo().ObtenerDescripcion(s.TIS_N, s.GEO_ID).NOMBRE_UBIGEO
                                    });
                                });
                                DireccionesTmp = direcciones;
                            }
                        }
                        #endregion                  
                        #region obtener_contacto
                        if (oficina.Contacto != null)
                        {
                            contacto = new List<DTOOficinaContacto>();
                            if (oficina.Contacto != null)
                            {
                                foreach (var s in oficina.Contacto)
                                {
                                    var obj = new DTOOficinaContacto();
                                    obj.Id = s.BPS_ID;
                                    obj.nombre = s.NOMBRE;
                                    obj.rol_id = s.ROL_ID;
                                    obj.tipo_documento = s.TAXT_ID;
                                    obj.numero_documento = s.TAX_ID;
                                    obj.rol_descripcion = s.ROL;
                                    obj.EnBD = true;
                                    obj.UsuarioCrea = s.LOG_USER_CREAT;
                                    obj.FechaCrea = s.LOG_DATE_CREAT;
                                    obj.UsuarioModifica = s.LOG_USER_UPDAT;
                                    obj.FechaModifica = s.LOG_DATE_UPDATE;
                                    obj.Activo = s.ENDS.HasValue ? false : true;

                                    contacto.Add(obj);
                                }
                                AsociadosTmp = contacto;
                            }
                        }
                        #endregion
                        #region obtener_direccionHistorial
                        if (oficina.DireccionesHistorial != null)
                        {
                            direccionesHistorial = new List<DTODireccion>();
                            if (oficina.DireccionesHistorial != null)
                            {
                                oficina.DireccionesHistorial.ForEach(s =>
                                {
                                    direccionesHistorial.Add(new DTODireccion
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
                                DireccionesHistorialTmp = direccionesHistorial;
                            }
                        }
                        #endregion
                        #region obtener_parametros
                        if (oficina.Parametros != null)
                        {
                            parametros = new List<DTOParametro>();
                            if (oficina.Parametros != null)
                            {
                                oficina.Parametros.ForEach(s =>
                                {
                                    parametros.Add(new DTOParametro
                                    {
                                        Id = (s.PAR_ID),
                                        Descripcion = s.PAR_VALUE,
                                        TipoParametro = Convert.ToString(s.PAR_TYPE),
                                        TipoParametroDesc = new BLTipoParametro().Obtener(GlobalVars.Global.OWNER, s.PAR_TYPE).PAR_DESC,
                                        EnBD = true,
                                        Activo = s.ENDS.HasValue ? false : true
                                    });
                                });
                                ParametrosTmp = parametros;
                            }
                        }
                        #endregion
                        //TAB - DIVISIONES
                        #region obtener_grupoModalidad
                        if (oficina.DivisionAdmGrupoModalidad != null)
                        {
                            divisionGrupoMod = new List<DTOOficinaGrupoModalidad>();
                            if (oficina.DivisionAdmGrupoModalidad != null)
                            {
                                oficina.DivisionAdmGrupoModalidad.ForEach(s =>
                                {
                                    divisionGrupoMod.Add(new DTOOficinaGrupoModalidad
                                    {
                                        Id = s.DIV_RiGHTS_ID,
                                        IdOficinaDiv = s.ID_COLL_DIV,
                                        IdGM = s.MOG_ID,
                                        DescripcionGM = s.MOG_DESC,
                                        IdDivision = s.DAD_ID,
                                        EnBD = true,
                                        Activo = s.ENDS.HasValue ? false : true
                                    });
                                });
                                GrupoModalidadesTmp = divisionGrupoMod;
                            }
                        }
                        #endregion
                        #region obtener_DivisionAdm
                        if (oficina.DivisionAdm != null)
                        {
                            divisionesAdm = new List<DTOOficinaDivision>();
                            if (oficina.DivisionAdm != null)
                            {
                                oficina.DivisionAdm.ForEach(s =>
                                {
                                    divisionesAdm.Add(new DTOOficinaDivision
                                    {
                                        Id = s.ID_COLL_DIV,
                                        idDivision = s.DAD_ID,
                                        Division = s.DAD_NAME,
                                        Detalle = s.DIV_DESCRIPTION,
                                        EnBD = true,
                                        Activo = s.ENDS.HasValue ? false : true
                                    });
                                });
                                DivisionTmp = divisionesAdm;
                            }
                        }
                        #endregion
                        #region obtener_numeracion
                        if (oficina.DivisionAdmNumerador != null)
                        {
                            numeraciones = new List<DTONumeracion>();
                            if (oficina.DivisionAdmNumerador != null)
                            {
                                oficina.DivisionAdmNumerador.ForEach(s =>
                                {
                                    numeraciones.Add(new DTONumeracion
                                    {
                                        Id = s.NUM_ID,
                                        IdOficinaDiv = s.ID_COLL_DIV,
                                        IdDivision = s.DAD_ID,
                                        IdNumerador = s.NMR_ID,
                                        Tipo = s.TIPO_NUMERADOR,
                                        Serie = s.SERIE_NUMERADOR,
                                        EnBD = true,
                                        Activo = s.ENDS.HasValue ? false : true,
                                    });
                                });
                                NumeracionesTmp = numeraciones;
                            }
                        }
                        #endregion

                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(oficina, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado la Oficina";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneOficina", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        // ****************************************************************************************************** //
        [HttpPost()]
        public JsonResult ObtenerXDescripcion(BEOffices oficina)
        {
            Resultado retorno = new Resultado();

            try
            {
                BLOffices servicio = new BLOffices();
                oficina.OWNER = GlobalVars.Global.OWNER;
                var lista = new List<BEOffices>();

                lista = servicio.ObtenerXDescripcion(oficina);
                if (lista.Count >= 1)
                    retorno.result = 1;
                else
                    retorno.result = 0;

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerXDescripcion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region DIRECCION_LISTA
        private const string K_SESION_DIRECCION_HISTORIAL = "___DTODirecciones_Historial";
        private const string K_SESION_DIRECCION_HISTORIAL_DEL = "___DTODirecciones_Historial_DEL";
        private const string K_SESION_DIRECCION_HISTORIAL_ACT = "___DTODirecciones_Historial_ACT";

        List<DTODireccion> direccionesHistorial = new List<DTODireccion>();
        private List<DTODireccion> DireccionesHistorialTmpUPDEstado
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION_HISTORIAL_ACT];
            }
            set
            {
                Session[K_SESION_DIRECCION_HISTORIAL_ACT] = value;
            }
        }
        private List<DTODireccion> DireccionesHistorialListaTmpDelBD
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION_HISTORIAL_DEL];
            }
            set
            {
                Session[K_SESION_DIRECCION_HISTORIAL_DEL] = value;
            }
        }
        public List<DTODireccion> DireccionesHistorialTmp
        {
            get
            {
                return (List<DTODireccion>)Session[K_SESION_DIRECCION_HISTORIAL];
            }
            set
            {
                Session[K_SESION_DIRECCION_HISTORIAL] = value;
            }
        }

        public JsonResult ListarDireccionHistorial()
        {
            direccionesHistorial = DireccionesHistorialTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Id</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Tipo Direccion</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Dirección</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Estado</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Usu. Crea</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Fecha Crea</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Usu. Modi</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Fecha Modi</th>");
                    shtml.Append("</tr></thead>");

                    if (direccionesHistorial != null)
                    {
                        foreach (var item in direccionesHistorial.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.TipoDireccionDesc);
                            shtml.AppendFormat("<td >{0}</td>", item.RazonSocial);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarDireccionHistorial", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BEDireccion> obtenerDireccionesHistorial()
        {
            List<BEDireccion> datos = new List<BEDireccion>();
            if (DireccionesHistorialTmp != null)
            {
                DireccionesHistorialTmp.ForEach(x =>
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
                    obj.MAIN_ADD = Convert.ToChar(x.EsPrincipal == "" ? "0" : x.EsPrincipal);
                    datos.Add(obj);
                });
            }
            return datos;
        }

        #endregion

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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddDocumento", ex);
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddDocumento", ex);
            }
            return retorno;
        }

        //[HttpPost]
        //public ActionResult Upload(DTODocumento documento)
        //{
        //    //var resultado = AddDoc(documento);
        //    var resultado = AddDoc(documento);
        //    var fec = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    var guid = Guid.NewGuid().ToString();
        //    var file = Request.Files["Filedata"];

        //    var nombreGenerado = "";

        //    if (resultado.result == 1)
        //    {
        //        nombreGenerado = String.Format("{0}_{1}_{2}_{3}", fec, resultado.Code, guid, file.FileName);
        //        documentos = DocumentosTmp;
        //        if (documentos == null) documentos = new List<DTODocumento>();

        //        documentos.ForEach(x =>
        //        {
        //            if (x.Id == resultado.Code) x.Archivo = nombreGenerado;
        //        });

        //        var path = System.Web.Configuration.WebConfigurationManager.AppSettings["RutaFisicaImgOficina"];

        //        string savePath = String.Format("{0}{1}", path, nombreGenerado);
        //        file.SaveAs(savePath);
        //    }
        //    var pathWeb = System.Web.Configuration.WebConfigurationManager.AppSettings["RutaWebImgOficina"];
        //    return Content(String.Format("{0}{1}", pathWeb, nombreGenerado));
        //}

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

    }
}

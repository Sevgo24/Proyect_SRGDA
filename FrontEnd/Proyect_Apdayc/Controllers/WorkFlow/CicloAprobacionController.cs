using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using SGRDA.Entities.WorkFlow;
using SGRDA.BL.WorkFlow;

namespace Proyect_Apdayc.Controllers.Flujo
{
    public class CicloAprobacionController : Base
    {
        //
        // GET: /CicloAprobacion/
        public const string nomAplicacion = "SRGDA";

        private const string K_SESSION_ESTADO = "___DTOEstado";
        private const string K_SESSION_ESTADO_DEL = "___DTOEstadoDEL";
        private const string K_SESSION_ESTADO_ACT = "___DTOEstadoACT";
        private const string K_SESSION_TRANSICION = "___DTOtransicion";
        private const string K_SESSION_TRANSICION_DEL = "___DTOtransicionDEL";
        private const string K_SESSION_TRANSICION_ACT = "___DTOTransicionACT";
        private const string K_SESSION_TAB = "___DTOTab";
        private const string K_SESSION_TAB_DEL = "___DTOTabDEL";
        private const string K_SESSION_TAB_ACT = "___DTOTabACT";
        List<DTOWorkflowEstado> estados = new List<DTOWorkflowEstado>();
        List<DTOTransicion> transiciones = new List<DTOTransicion>();
        List<DTOLicenciaEstadoTab> LicenciaEstadoTab = new List<DTOLicenciaEstadoTab>();

        #region INDEX

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public JsonResult Listar(int skip, int take, int page, int pageSize, string group, string nombre, string etiqueta, decimal idCliente, int estado)
        {
            Resultado retorno = new Resultado();
            var lista = BLListar(GlobalVars.Global.OWNER, nombre, etiqueta, idCliente, estado, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new WORKF_WORKFLOWS { ListaFlujo = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new WORKF_WORKFLOWS { ListaFlujo = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<WORKF_WORKFLOWS> BLListar(string owner, string nombre, string etiqueta, decimal idCliente, int estado, int pagina, int cantRegxPag)
        {
            return new BL_WORKF_WORKFLOWS().Listar(owner, nombre, etiqueta, idCliente, estado, pagina, cantRegxPag);
        }

        #endregion

        #region NUEVO
        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESSION_ESTADO);
            Session.Remove(K_SESSION_ESTADO_DEL);
            Session.Remove(K_SESSION_ESTADO_ACT);
            Session.Remove(K_SESSION_TRANSICION);
            Session.Remove(K_SESSION_TRANSICION_DEL);
            Session.Remove(K_SESSION_TRANSICION_ACT);
            Session.Remove(K_SESSION_TAB);
            Session.Remove(K_SESSION_TAB_DEL);
            Session.Remove(K_SESSION_TAB_ACT);
            return View();
        }

        public ActionResult Obtener(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var ciclo = new BL_WORKF_WORKFLOWS().ObtenerWorkFlow(GlobalVars.Global.OWNER, id);
                    if (ciclo.WorkflowEstados != null)
                    {
                        estados = new List<DTOWorkflowEstado>();
                        ciclo.WorkflowEstados.ForEach(s =>
                        {
                            estados.Add(new DTOWorkflowEstado
                            {
                                Id_Estado = s.WRKF_SID,
                                Id_Estado_origen = s.WRKF_SID,
                                DesEstado = s.WRKF_SLABEL,
                                UsuarioCrea = s.LOG_USER_CREAT,
                                FechaCrea = s.LOG_DATE_CREAT,
                                UsuarioModifica = s.LOG_USER_UPDATE,
                                FechaModifica = s.LOG_DATE_UPDATE,
                                EsPrincipal = s.WRKF_INI,
                                EnBD = true,
                                Activo = s.ENDS.HasValue ? false : true
                            });
                        });
                        EstadosTmp = estados;
                    }
                    if (ciclo.WorkflowTransiciones != null)
                    {
                        transiciones = new List<DTOTransicion>();
                        ciclo.WorkflowTransiciones.ForEach(s =>
                        {
                            transiciones.Add(new DTOTransicion
                            {
                                Id = s.WRKF_TID,
                                IdEstadoInicial = s.WRKF_CSTATE,
                                EstadoInicial = s.ESTADO_INI,
                                IdEstadoFinal = s.WRKF_NSTATE,
                                EstadoFinal = s.ESTADO_FIN,
                                IdEvento = s.WRKF_EID,
                                Evento = s.WRKF_ENAME,

                                UsuarioCrea = s.LOG_USER_CREAT,
                                FechaCrea = s.LOG_DATE_CREAT,
                                UsuarioModifica = s.LOG_USER_UPDATE,
                                FechaModifica = s.LOG_DATE_UPDATE,
                                EnBD = true,
                                Activo = s.ENDS.HasValue ? false : true
                            });
                        });
                        TransicionesTmp = transiciones;
                    }

                    if (ciclo.WorkflowTabs != null)
                    {
                        LicenciaEstadoTab = new List<DTOLicenciaEstadoTab>();
                        if (ciclo.WorkflowTabs != null)
                        {
                            ciclo.WorkflowTabs.ForEach(s =>
                            {
                                LicenciaEstadoTab.Add(new DTOLicenciaEstadoTab
                                {
                                    sequencia = s.SECUENCIA,
                                    IdTab = s.TAB_ID,
                                    IdEstado = s.WORKF_SID,
                                    Nombre = s.TAB_NAME,
                                    NombreEst = s.WRKF_SLABEL,
                                    EnBD = true,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    FechaCrea = s.LOG_DATE_CREAT,
                                    UsuarioModifica = s.LOG_USER_UPDATE,
                                    FechaModifica = s.LOG_DATE_UPDATE,
                                    antIdTab = s.TAB_ID,
                                    Activo = s.ENDS.HasValue ? false : true
                                });
                            });
                            LicenciaEstadoTabTmp = LicenciaEstadoTab;
                        }
                    }
                    retorno.data = Json(ciclo, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost()]
        public ActionResult Insertar(WORKF_WORKFLOWS flujo)
        {
            Resultado retorno = new Resultado();
            try
            {
                flujo.OWNER = GlobalVars.Global.OWNER;
                flujo.WorkflowEstados = obtenerEstados();
                flujo.WorkflowTransiciones = obtenerTransiciones();
                flujo.WorkflowTabs = obtenerLicenciaEstadoTab();

                if (flujo.WRKF_ID == 0)
                {
                    flujo.LOG_USER_CREAT = UsuarioActual;
                    var datos = new BL_WORKF_WORKFLOWS().InsertarWorkFlow(flujo);
                }
                else
                {
                    flujo.LOG_USER_UPDATE = UsuarioActual;
                    //1.setting Estados eliminar
                    List<WORKF_STATES_WORKFLOW> listaEstadoDel = null;
                    if (EstadosTmpDelBD != null)
                    {
                        listaEstadoDel = new List<WORKF_STATES_WORKFLOW>();
                        EstadosTmpDelBD.ForEach(x => { listaEstadoDel.Add(new WORKF_STATES_WORKFLOW { WRKF_SID = x.Id_Estado }); });
                    }
                    //setting Estados activar
                    List<WORKF_STATES_WORKFLOW> listaEstadosUpdEst = null;
                    if (EstadosTmpUPDEstado != null)
                    {
                        listaEstadosUpdEst = new List<WORKF_STATES_WORKFLOW>();
                        EstadosTmpUPDEstado.ForEach(x => { listaEstadosUpdEst.Add(new WORKF_STATES_WORKFLOW { WRKF_SID = x.Id_Estado }); });
                    }

                    //2.setting Transiciones eliminar
                    List<WORKF_TRANSITIONS> listTransicionDel = null;
                    if (TransicionesTmpDelBD != null)
                    {
                        listTransicionDel = new List<WORKF_TRANSITIONS>();
                        TransicionesTmpDelBD.ForEach(x => { listTransicionDel.Add(new WORKF_TRANSITIONS { WRKF_TID = x.Id }); });
                    }
                    //setting Transiciones activar
                    List<WORKF_TRANSITIONS> listaTransicionUpdEst = null;
                    if (TransicionesTmpUPDEstado != null)
                    {
                        listaTransicionUpdEst = new List<WORKF_TRANSITIONS>();
                        TransicionesTmpUPDEstado.ForEach(x => { listaTransicionUpdEst.Add(new WORKF_TRANSITIONS { WRKF_TID = x.Id }); });
                    }

                    //3.setting Tabs eliminar
                    List<BEREC_LIC_TAB_STAT> listaTabDel = null;
                    if (LicenciaEstadoTabTmpDelBD != null)
                    {
                        listaTabDel = new List<BEREC_LIC_TAB_STAT>();
                        LicenciaEstadoTabTmpDelBD.ForEach(x => { listaTabDel.Add(new BEREC_LIC_TAB_STAT { SECUENCIA = x.sequencia }); });
                    }
                    //setting Tabs activar
                    List<BEREC_LIC_TAB_STAT> listaTabUpdEst = null;
                    if (LicenciaEstadoTabTmpUPDEstado != null)
                    {
                        listaTabUpdEst = new List<BEREC_LIC_TAB_STAT>();
                        LicenciaEstadoTabTmpUPDEstado.ForEach(x => { listaTabUpdEst.Add(new BEREC_LIC_TAB_STAT { SECUENCIA = x.sequencia }); });
                    }
                    var datos = new BL_WORKF_WORKFLOWS().ActualizarWorkFlow(flujo, listaEstadoDel, listaEstadosUpdEst,
                                                                                listTransicionDel, listaTransicionUpdEst,
                                                                                listaTabDel, listaTabUpdEst);
                }
                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BEREC_LIC_TAB_STAT> obtenerLicenciaEstadoTab()
        {
            List<BEREC_LIC_TAB_STAT> datos = new List<BEREC_LIC_TAB_STAT>();
            if (LicenciaEstadoTabTmp != null)
            {
                LicenciaEstadoTabTmp.ForEach(x =>
                {
                    var obj = new BEREC_LIC_TAB_STAT();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.SECUENCIA = x.sequencia;
                    obj.TAB_ID = x.IdTab;
                    obj.antTAB_ID = x.antIdTab;
                    obj.WORKF_SID = x.IdEstado;
                    obj.TAB_NAME = x.Nombre;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    obj.LOG_USER_UPDATE = UsuarioActual;
                    datos.Add(obj);
                });
            }
            return datos;
        }

        [HttpPost()]
        public ActionResult Eliminar(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                WORKF_WORKFLOWS flujo = new WORKF_WORKFLOWS();
                flujo.OWNER = GlobalVars.Global.OWNER;
                flujo.WRKF_ID = id;
                flujo.LOG_USER_UPDATE = UsuarioActual;
                var datos = new BL_WORKF_WORKFLOWS().EliminarWorkFlow(flujo);
                retorno.result = 1;
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
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

        #region ESTADO

        public List<DTOWorkflowEstado> EstadosTmp
        {
            get
            {
                return (List<DTOWorkflowEstado>)Session[K_SESSION_ESTADO];
            }
            set
            {
                Session[K_SESSION_ESTADO] = value;
            }
        }

        private List<DTOWorkflowEstado> EstadosTmpUPDEstado
        {
            get
            {
                return (List<DTOWorkflowEstado>)Session[K_SESSION_ESTADO_ACT];
            }
            set
            {
                Session[K_SESSION_ESTADO_ACT] = value;
            }
        }

        private List<DTOWorkflowEstado> EstadosTmpDelBD
        {
            get
            {
                return (List<DTOWorkflowEstado>)Session[K_SESSION_ESTADO_DEL];
            }
            set
            {
                Session[K_SESSION_ESTADO_DEL] = value;
            }
        }

        [HttpPost]
        public JsonResult AddEstado(DTOWorkflowEstado entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    int registroNuevo = 0;
                    int registroModificar = 0;
                    //caracteristicas = CaracteristicasTmp;
                    estados = EstadosTmp;
                    if (estados != null)
                    {
                        registroNuevo = estados.Where(p => p.Id_Estado == entidad.Id_Estado && entidad.Id_Estado_origen == 0).Count();
                        registroModificar = estados.Where(p => p.Id_Estado_origen == entidad.Id_Estado_origen && p.Id_Estado != entidad.Id_Estado).Count();
                    }

                    if ((entidad.Id_Estado_origen == 0 && registroNuevo == 0)
                         || (entidad.Id_Estado_origen != 0 && registroModificar > 0)
                       )
                    {

                        estados = EstadosTmp;
                        if (estados == null) estados = new List<DTOWorkflowEstado>();
                        if (entidad.Id_Estado_origen == 0)
                        {
                            //decimal nuevoId = 1;
                            //if (estados.Count > 0) nuevoId = estados.Max(x => x.Secuencia) + 1;
                            entidad.Id_Estado_origen = entidad.Id_Estado;
                            entidad.UsuarioCrea = UsuarioActual;
                            entidad.FechaCrea = DateTime.Now;
                            entidad.EsPrincipal = entidad.EsPrincipal == false ? true : false; //estados.Count == 0 ? "1" : "0";
                            entidad.Activo = true;
                            entidad.EnBD = false;
                            estados.Add(entidad);
                        }
                        else
                        {
                            var item = estados.Where(x => x.Id_Estado_origen == entidad.Id_Estado_origen).FirstOrDefault();
                            entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                            entidad.Activo = item.Activo;
                            entidad.EsPrincipal = false;
                            entidad.UsuarioModifica = UsuarioActual;
                            entidad.FechaModifica = DateTime.Now;
                            estados.Remove(item);
                            estados.Add(entidad);
                        }
                        EstadosTmp = estados;
                        retorno.result = 1;
                        retorno.message = "OK";

                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "El estado ya existe.";
                    }

                }
            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddEstado", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddEstado(decimal idEstado)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    estados = EstadosTmp;
                    if (estados != null)
                    {
                        var objDel = estados.Where(x => x.Id_Estado == idEstado).FirstOrDefault();
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (EstadosTmpUPDEstado == null) EstadosTmpUPDEstado = new List<DTOWorkflowEstado>();
                                if (EstadosTmpDelBD == null) EstadosTmpDelBD = new List<DTOWorkflowEstado>();

                                var itemUpd = EstadosTmpUPDEstado.Where(x => x.Id_Estado == idEstado).FirstOrDefault();
                                var itemDel = EstadosTmpDelBD.Where(x => x.Id_Estado == idEstado).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) EstadosTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) EstadosTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) EstadosTmpDelBD.Add(objDel);
                                    if (itemUpd != null) EstadosTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                estados.Remove(objDel);
                                estados.Add(objDel);
                            }
                            else
                            {
                                estados.Remove(objDel);
                            }
                            EstadosTmp = estados;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddEstado", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<WORKF_STATES_WORKFLOW> obtenerEstados()
        {
            List<WORKF_STATES_WORKFLOW> datos = new List<WORKF_STATES_WORKFLOW>();
            if (EstadosTmp != null)
            {
                EstadosTmp.ForEach(x =>
                {
                    datos.Add(new WORKF_STATES_WORKFLOW
                    {
                        WRKF_ID = x.Id_Estado,
                        WRKF_SID = x.Id_Estado_origen,
                        WRKF_SID_ORIGEN = x.Id_Estado_origen,
                        WRKF_INI = x.EsPrincipal,  //Convert.ToChar(x.EsPrincipal == "" ? "0" : x.EsPrincipal);
                        OWNER = GlobalVars.Global.OWNER,
                        LOG_USER_CREAT = UsuarioActual,
                        LOG_USER_UPDATE = UsuarioActual
                    });
                });
            }
            return datos;
        }

        public JsonResult ObtieneEstadoTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var observacion = EstadosTmp.Where(x => x.Id_Estado == idDir).FirstOrDefault();
                    retorno.data = Json(observacion, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneEstadoTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarEstado()
        {
            estados = EstadosTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='k-header'>Id</th>");
                    shtml.Append("<th class='k-header' width='30%;' >Estado</th>");
                    shtml.Append("<th class='k-header'>Usuario Crea.</th>");
                    shtml.Append("<th class='k-header' width='150px;'>Fecha Crea.</th>");
                    shtml.Append("<th class='k-header'>Usuario Mod.</th>");
                    shtml.Append("<th class='k-header' width='150px;'>Fecha Mod.</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header' style='width:30px'></th>");
                    shtml.Append("<th class='k-header' style='width:30px'>Inicial</th>");
                    shtml.Append("</tr></thead>");

                    if (estados != null)
                    {
                        string strChecked = "";
                        foreach (var item in estados.OrderBy(x => x.Id_Estado))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id_Estado);
                            shtml.AppendFormat("<td nowrap width='30%;' >{0}</td>", item.DesEstado);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td  width='150px;'>{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td  width='150px;'>{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            if (item.EsPrincipal == true)
                                strChecked = " checked='checked'";
                            else
                                strChecked = "";
                            //shtml.AppendFormat("<td style='width:60px'> <a href=# onclick='updAddEstado({0});'><img src='../Images/iconos/edit.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.Id_Estado, "Modificar Observación");
                            shtml.AppendFormat("<td style='width:60px'> ");
                            shtml.AppendFormat("<a href=# onclick='delAddEstado({0});'><img src='../Images/iconos/{1}'      title='{2}' border=0></a>", item.Id_Estado, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Observacion" : "Activar Observacion");
                            shtml.AppendFormat("<td ><input type='radio' class='radioDir' onclick='actualizarDirPrincipal({0});' name='rdbtnDir' id='rbtn_{0}' {1} /></td>", item.Id_Estado, strChecked);
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        }
                        if (estados.Count == 0)
                        {
                            shtml.Append("</table>");
                            retorno.message = shtml.ToString();
                            retorno.result = 2;
                        }
                        else
                        {
                            shtml.Append("</table>");
                            retorno.message = shtml.ToString();
                            retorno.result = 1;
                        }
                    }
                    else
                    {
                        shtml.Append("</table>");
                        retorno.message = shtml.ToString();
                        retorno.result = 2;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarEstado", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region TRANSICIONES
        public JsonResult ListaEstadosTemporal()
        {
            Resultado retorno = new Resultado();
            try
            {
                var datos = EstadosTmp.Where(x => x.Activo)
                .Select(c => new SelectListItem
                {
                    Value = c.Id_Estado.ToString(),
                    Text = c.DesEstado
                });
                retorno.result = 1;
                retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaEstadosTemporal", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public List<DTOTransicion> TransicionesTmp
        {
            get
            {
                return (List<DTOTransicion>)Session[K_SESSION_TRANSICION];
            }
            set
            {
                Session[K_SESSION_TRANSICION] = value;
            }
        }

        private List<DTOTransicion> TransicionesTmpUPDEstado
        {
            get
            {
                return (List<DTOTransicion>)Session[K_SESSION_TRANSICION_ACT];
            }
            set
            {
                Session[K_SESSION_TRANSICION_ACT] = value;
            }
        }

        private List<DTOTransicion> TransicionesTmpDelBD
        {
            get
            {
                return (List<DTOTransicion>)Session[K_SESSION_TRANSICION_DEL];
            }
            set
            {
                Session[K_SESSION_TRANSICION_DEL] = value;
            }
        }

        [HttpPost]
        public JsonResult AddTransicion(DTOTransicion entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    int registroNuevo = 0;
                    int registroModificar = 0;
                    //caracteristicas = CaracteristicasTmp;
                    if (TransicionesTmp == null)
                        TransicionesTmp = new List<DTOTransicion>();

                    transiciones = TransicionesTmp;
                    if (estados != null)
                    {
                        registroNuevo = transiciones.Where(p => p.IdEstadoInicial == entidad.IdEstadoInicial
                                                             && p.IdEstadoFinal == entidad.IdEstadoFinal
                                                             ).Count();
                        //registroModificar = estados.Where(p => p.Id_Estado_origen == entidad.Id_Estado_origen && p.Id_Estado != entidad.Id_Estado).Count();
                    }

                    if (registroNuevo == 0
                        //|| (entidad.Id_Estado_origen != 0 && registroModificar > 0 )
                       )
                    {
                        transiciones = TransicionesTmp;
                        if (estados == null) estados = new List<DTOWorkflowEstado>();
                        decimal nuevoId = 1;
                        if (transiciones.Count > 0) nuevoId = transiciones.Max(x => x.Id) + 1;
                        entidad.Id = nuevoId;
                        entidad.UsuarioCrea = UsuarioActual;
                        entidad.FechaCrea = DateTime.Now;
                        entidad.Activo = true;
                        entidad.EnBD = false;
                        transiciones.Add(entidad);

                        TransicionesTmp = transiciones;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "la combinación de estados ya existe.";
                    }

                }
            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddTransicion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddTransicion(decimal idTransicion)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    transiciones = TransicionesTmp;
                    if (estados != null)
                    {
                        var objDel = transiciones.Where(x => x.Id == idTransicion).FirstOrDefault();
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (TransicionesTmpUPDEstado == null) TransicionesTmpUPDEstado = new List<DTOTransicion>();
                                if (TransicionesTmpDelBD == null) TransicionesTmpDelBD = new List<DTOTransicion>();

                                var itemUpd = TransicionesTmpUPDEstado.Where(x => x.Id == idTransicion).FirstOrDefault();
                                var itemDel = TransicionesTmpDelBD.Where(x => x.Id == idTransicion).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) TransicionesTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) TransicionesTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) TransicionesTmpDelBD.Add(objDel);
                                    if (itemUpd != null) TransicionesTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                transiciones.Remove(objDel);
                                transiciones.Add(objDel);
                            }
                            else
                            {
                                transiciones.Remove(objDel);
                            }
                            TransicionesTmp = transiciones;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddTransicion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<WORKF_TRANSITIONS> obtenerTransiciones()
        {
            List<WORKF_TRANSITIONS> datos = new List<WORKF_TRANSITIONS>();
            if (TransicionesTmp != null)
            {
                TransicionesTmp.ForEach(x =>
                {
                    datos.Add(new WORKF_TRANSITIONS
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        WRKF_TID=x.Id,
                        WRKF_ID = x.IdWorkFlow,                        
                        WRKF_CSTATE = x.IdEstadoInicial,
                        WRKF_NSTATE = x.IdEstadoFinal,
                        WRKF_EID = x.IdEvento,
                        LOG_USER_CREAT = UsuarioActual,
                        LOG_USER_UPDATE = UsuarioActual
                    });
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
                    if (EstadosTmp != null)
                    {
                        foreach (var x in EstadosTmp.Where(x => x.Id_Estado != idDir))
                        {
                            x.EsPrincipal = false;
                        }
                        foreach (var x in EstadosTmp.Where(x => x.Id_Estado == idDir))
                        {
                            x.EsPrincipal = true;
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

        public JsonResult ObtieneTransicionTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var transicion = TransicionesTmp.Where(x => x.Id == idDir).FirstOrDefault();
                    retorno.data = Json(transicion, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneTransicionTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarTransiciones()
        {
            transiciones = TransicionesTmp;
            //estados = EstadosTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='k-header'>Id</th>");
                    shtml.Append("<th class='k-header' width='15%;' >Estado Inicial</th>");
                    shtml.Append("<th class='k-header' width='15%;' >Estado Final</th>");
                    shtml.Append("<th class='k-header' width='15%;' >Estado Evento</th>");
                    shtml.Append("<th class='k-header'>Usuario Crea.</th>");
                    shtml.Append("<th class='k-header' width='130px;'>Fecha Crea.</th>");
                    shtml.Append("<th class='k-header'>Usuario Mod.</th>");
                    shtml.Append("<th class='k-header' width='130px;'>Fecha Mod.</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header' style='width:40px'></th></tr></thead>");

                    if (transiciones != null)
                    {
                        foreach (var item in transiciones.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td nowrap width='15%;' >{0}</td>", item.EstadoInicial);
                            shtml.AppendFormat("<td nowrap width='15%;' >{0}</td>", item.EstadoFinal);
                            shtml.AppendFormat("<td nowrap width='15%;' >{0}</td>", item.Evento);

                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td  width='130px;'>{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td  width='130px;'>{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            //shtml.AppendFormat("<td style='width:60px'> <a href=# onclick='updAddEstado({0});'><img src='../Images/iconos/edit.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.Id_Estado, "Modificar Observación");
                            shtml.AppendFormat("<td style='width:30px'> ");
                            shtml.AppendFormat("                        <a href=# onclick='delAddTransicion({0});'><img src='../Images/iconos/{1}'      title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Observacion" : "Activar Observacion");
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Listartransiciones", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region TABS

        public List<DTOLicenciaEstadoTab> LicenciaEstadoTabTmp
        {
            get
            {
                return (List<DTOLicenciaEstadoTab>)Session[K_SESSION_TAB];
            }
            set
            {
                Session[K_SESSION_TAB] = value;
            }
        }

        private List<DTOLicenciaEstadoTab> LicenciaEstadoTabTmpUPDEstado
        {
            get
            {
                return (List<DTOLicenciaEstadoTab>)Session[K_SESSION_TAB_ACT];
            }
            set
            {
                Session[K_SESSION_TAB_ACT] = value;
            }
        }

        private List<DTOLicenciaEstadoTab> LicenciaEstadoTabTmpDelBD
        {
            get
            {
                return (List<DTOLicenciaEstadoTab>)Session[K_SESSION_TAB_DEL];
            }
            set
            {
                Session[K_SESSION_TAB_DEL] = value;
            }
        }

        public JsonResult ListarTabs()
        {
            estados = EstadosTmp;
            DTOLicenciaEstadoTab Lic = new DTOLicenciaEstadoTab();
            if (estados != null) { estados.Where(x => x.Id_Estado == Lic.IdEstado);}
            LicenciaEstadoTab = LicenciaEstadoTabTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='k-header' width='11px;'>Id</th>");
                    shtml.Append("<th class='k-header' width='11px; display:none>IdEstado</th>");
                    shtml.Append("<th class='k-header' width='11px;' >Estado</th>");
                    shtml.Append("<th class='k-header' width='11px;'>Tab</th>");                    
                    shtml.Append("<th class='k-header' width='120px;'>Usuario Crea.</th>");
                    shtml.Append("<th class='k-header' width='140px;'>Fecha Crea.</th>");
                    shtml.Append("<th class='k-header' width='120px;'>Usuario Mod.</th>");
                    shtml.Append("<th class='k-header' width='140px;'>Fecha Mod.</th>");
                    shtml.Append("<th class='k-header' width='12px;'>Estado</th>");
                    shtml.Append("<th class='k-header' style='width:40px'></th></tr></thead>");

                    if (LicenciaEstadoTab != null)
                    {
                        foreach (var item in LicenciaEstadoTab.OrderBy(x => x.NombreEst))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td nowrap width='11px;'>{0}</td>", item.sequencia);
                            shtml.AppendFormat("<td nowrap width='11px;' style='display:none'>{0}</td>", item.IdEstado);
                            shtml.AppendFormat("<td nowrap width='11px;' >{0}</td>", item.NombreEst);
                            shtml.AppendFormat("<td nowrap width='11px;'>{0}</td>", item.Nombre);                            
                            shtml.AppendFormat("<td nowrap width='120px;'>{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td nowrap width='140px;'>{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td nowrap width='120px;'>{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td width='140px;'>{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td width='12px;'>{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td> <a href=# onclick='updAddTab({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.sequencia);
                            shtml.AppendFormat("<a href=# onclick='delAddTab({0});'><img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.sequencia, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Tab" : "Activar Tab");
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Listartransiciones", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Agregar
        [HttpPost]
        public JsonResult AddTab(DTOLicenciaEstadoTab entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    bool validacion = true; LicenciaEstadoTab = LicenciaEstadoTabTmp;
                    if (LicenciaEstadoTab == null) LicenciaEstadoTab = new List<DTOLicenciaEstadoTab>();

                    string mensaje = string.Empty;
                    var IdTab = from i in LicenciaEstadoTab
                                where i.IdTab == entidad.IdTab && i.IdEstado == entidad.IdEstado
                                && i.Activo == true
                                select i;

                    if (IdTab.ToList().Count > 0)
                    {
                        mensaje = "La pestaña seleccionada ya ha sido ingresada" + Environment.NewLine;
                        retorno.message = String.Format("{0}", mensaje);
                        retorno.result = 0;
                        validacion = false;
                    }


                    if (validacion)
                    {
                        if (Convert.ToInt32(entidad.sequencia) <= 0)
                        {
                            decimal nuevoId = 1;
                            if (LicenciaEstadoTab.Count > 0) nuevoId = LicenciaEstadoTab.Max(x => x.sequencia) + 1;
                            entidad.sequencia = nuevoId;
                            entidad.Activo = true;
                            entidad.EnBD = false;
                            entidad.FechaCrea = DateTime.Now;
                            entidad.UsuarioCrea = UsuarioActual;
                            LicenciaEstadoTab.Add(entidad);
                        }
                        else
                        {
                            var item = LicenciaEstadoTab.Where(x => x.sequencia == entidad.sequencia).FirstOrDefault();
                            entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                            entidad.antIdTab = item.antIdTab;
                            entidad.Activo = item.Activo;
                            entidad.UsuarioCrea = item.UsuarioCrea;
                            entidad.FechaCrea = item.FechaCrea;
                            if (entidad.EnBD)
                            {
                                entidad.UsuarioModifica = UsuarioActual;
                                entidad.FechaModifica = DateTime.Now;
                            }
                            LicenciaEstadoTab.Remove(item);
                            LicenciaEstadoTab.Add(entidad);
                        }
                        LicenciaEstadoTabTmp = LicenciaEstadoTab;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "add Tab", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Quitar
        [HttpPost]
        public JsonResult DellAddTab(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    LicenciaEstadoTab = LicenciaEstadoTabTmp;
                    if (LicenciaEstadoTab != null)
                    {
                        var objDel = LicenciaEstadoTab.Where(x => x.sequencia == id).FirstOrDefault();
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (LicenciaEstadoTabTmpUPDEstado == null) LicenciaEstadoTabTmpUPDEstado = new List<DTOLicenciaEstadoTab>();
                                if (LicenciaEstadoTabTmpDelBD == null) LicenciaEstadoTabTmpDelBD = new List<DTOLicenciaEstadoTab>();

                                var itemUpd = LicenciaEstadoTabTmpUPDEstado.Where(x => x.sequencia == id).FirstOrDefault();
                                var itemDel = LicenciaEstadoTabTmpDelBD.Where(x => x.sequencia == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) LicenciaEstadoTabTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) LicenciaEstadoTabTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) LicenciaEstadoTabTmpDelBD.Add(objDel);
                                    if (itemUpd != null) LicenciaEstadoTabTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                LicenciaEstadoTab.Remove(objDel);
                                LicenciaEstadoTab.Add(objDel);
                            }
                            else
                            {
                                LicenciaEstadoTab.Remove(objDel);
                            }

                            LicenciaEstadoTabTmp = LicenciaEstadoTab;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "add Tab", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region obtener
        public JsonResult ObtieneLicenciaEstadoTabTmp(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var LicenciaEstadoTab = LicenciaEstadoTabTmp.Where(x => x.sequencia == Id).FirstOrDefault();
                    retorno.data = Json(LicenciaEstadoTab, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneLicenciaEstadoTabTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}

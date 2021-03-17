using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGRDA.BL.WorkFlow;
using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO;
using SGRDA.Entities.WorkFlow;
using System.Text;
using SGRDA.BL;
using SGRDA.Entities;

namespace Proyect_Apdayc.Controllers.WorkFlow
{
    public class MappingController : Base
    {
        const string NomAplicacion = "SGRDA";
        private const string K_SESSION_ACTIONMAPPINGS = "___DTOActionMappings";
        private const string K_SESION_MAPPINGS = "___DTOMappings";
        private const string K_SESION_PARAMETROS = "___DTOObjetoParametro";
        private const string K_SESION_PARAMETROS_TRAN = "___DTOTransicionParametro";
        public List<DTOActionMappings> ActionMappingsTmp
        {
            get
            {
                return (List<DTOActionMappings>)Session[K_SESSION_ACTIONMAPPINGS];
            }
            set
            {
                Session[K_SESSION_ACTIONMAPPINGS] = value;
            }
        }
        public List<DTOParametroWorkflow> ObjetoParametroTmp
        {
            get
            {
                return (List<DTOParametroWorkflow>)Session[K_SESION_PARAMETROS];
            }
            set
            {
                Session[K_SESION_PARAMETROS] = value;
            }
        }
        public List<DTOParametroWorkflow> TransicionParametroTmp
        {
            get
            {
                return (List<DTOParametroWorkflow>)Session[K_SESION_PARAMETROS_TRAN];
            }
            set
            {
                Session[K_SESION_PARAMETROS_TRAN] = value;
            }
        } //se almacenan los parámetros de la transición para hacer el insert
        public DTOActionMappings MappingsTmp
        {
            get
            {
                return (DTOActionMappings)Session[K_SESION_MAPPINGS];
            }
            set
            {
                Session[K_SESION_MAPPINGS] = value;
            }
        }
        List<DTOActionMappings> actionmappings = new List<DTOActionMappings>();
        List<DTOActionMappings> mappings = new List<DTOActionMappings>();
        List<DTOParametroWorkflow> objetoParametro = new List<DTOParametroWorkflow>();
        List<DTOParametroWorkflow> transicionParametro = new List<DTOParametroWorkflow>();

        // GET: /Mapping/
        public const string nomAplicacion = "SRGDA";

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESSION_ACTIONMAPPINGS);
            Session.Remove(K_SESION_MAPPINGS);
            Session.Remove(K_SESION_PARAMETROS);
            Session.Remove(K_SESION_PARAMETROS_TRAN);
            return View();
        }

        public string DescripcionAccionPrerrequisito(decimal? id)
        {
            string nombre = string.Empty;
            foreach (var item in ActionMappingsTmp)
            {
                if (item.CodigoAccion == id)
                    nombre = item.DescripcionAccion;
            }
            return nombre;
        }

        public JsonResult ListarAcctionMappingsValores()
        {
            StringBuilder shtml = new StringBuilder();
            actionmappings = ActionMappingsTmp;
            Resultado retorno = new Resultado();

            try
            {
                int count = 0;
                if (!isLogout(ref retorno))
                {
                    WORKF_OBJECTS obj = new WORKF_OBJECTS();
                    List<BETipoObjeto> tipoObj = new List<BETipoObjeto>();
                    var clase = "'ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'";
                    shtml.Append("<table id='tbMappings' border=0 width='100%;' class='FiltroTabla'>");
                    shtml.Append("<thead><tr><th class=" + clase + " style='width: 10px'>Ord.</th>");
                    shtml.Append("<th class=" + clase + " style='width: 20px; display:none'>NewOrder</th>");
                    shtml.Append("<th class=" + clase + " style='width: 40px;'>IdMap.</th>");
                    shtml.Append("<th colspan='3' class=" + clase + " style='width: 1250px'>Acción</th>");
                    shtml.Append("<th class=" + clase + " style='width: 3px'>Vi.</th>");
                    shtml.Append("<th class=" + clase + " style='width: 30px'>Prior.</th>");
                    shtml.Append("<th class=" + clase + " style='width: 20px'>Oblig.</th>");
                    shtml.Append("<th class=" + clase + " style='width: 45px'>Acción Pre.</th>");
                    shtml.Append("<th class=" + clase + " style='width: 60px; display:none'>Objeto Id</th>");
                    shtml.Append("<th colspan='4' class=" + clase + " style='width: 350px'>Objeto Asociado</th>");
                    shtml.Append("<th class=" + clase + " style='width: 120px'>Evento</th>");
                    shtml.Append("<th class=" + clase + " style='width: 60px; display:none'>IdEvento</th>");
                    shtml.Append("<th colspan='2' class=" + clase + " style='width: 120px'>Transición</th>");
                    shtml.Append("<th colspan='2' class=" + clase + " style='width: 100px'>Mapping Next</th>");
                    shtml.Append("<th colspan='2' class=" + clase + " style='width: 2px'></th>");
                    shtml.Append("<th class=" + clase + " style='width: 15px'></th>");
                    shtml.Append("</tr></thead>");

                    if (actionmappings != null)
                    {
                        GeneralController gen = new GeneralController();
                        var items = gen.ListarEventosWorkf();
                        List<SelectListItem> itemsT = new List<SelectListItem>();
                        List<SelectListItem> itemsP = new List<SelectListItem>();

                        string strCheckedV = "";
                        string strCheckedO = "";
                        foreach (var item in actionmappings.OrderBy(x => Convert.ToInt32(x.OrdenAccion)))
                        {
                            if (item.IndicadorVisibilidad == "Y")
                                strCheckedV = " checked='checked'";
                            else
                                strCheckedV = "";

                            if (item.IndicadorObligatorio == "Y")
                                strCheckedO = " checked='checked'";
                            else
                                strCheckedO = "";

                            string optionT = string.Empty;
                            string option = "<option value='0'>--SELECCIONE--</option>";
                            option += itemsEvento(items, Convert.ToString(item.Etrigger));

                            if (count == 0)
                            {
                                //Lista Transiciones 
                                itemsT = gen.ListaTransicionEstados(item.workFlow, item.estado);
                                count += 1;
                            }
                            optionT = "<option value='0'>--SELECCIONE--</option>";
                            optionT += itemsEstadoTransicion(itemsT, Convert.ToString(item.Transicion));

                            //Lista Prerrequisitos
                            string optionP = string.Empty;
                            itemsP = gen.ListarAccionPreRequisito(item.workFlow, item.estado, item.CodigoAccion);
                            optionP = "<option value='0'>--</option>";
                            optionP += itemsPrerrequisito(itemsP, Convert.ToString(item.CodigoAccionPrerequisito));

                            shtml.Append("<tr class='k-grid-content'>");

                            #region Orden
                            shtml.AppendFormat("<td style='text-align:center'>{0}</td>", item.OrdenAccion);
                            shtml.AppendFormat("<td style='display:none'><input type='text' id='txtOrderId_{0}' value={1} style='width: 30px; text-align:center' readonly='true'></input></td>", item.OrdenAccion, item.OrdenAccionNew);
                            #endregion

                            #region Mapping
                            shtml.AppendFormat("<td style='text-align:center'><label id='lbMapaId_{0}'>{1}</label>  </td>", item.OrdenAccion, item.IdmapaAccion);
                            #endregion

                            #region Accion
                            shtml.AppendFormat("<td style='text-align:center'><input type='text' id='txtvalorAccId_{0}' value={1} style='width: 40px; text-align:center; font-size:6px;' readonly='true'></input></td>", item.OrdenAccion, item.CodigoAccion);
                            shtml.AppendFormat("<td ><label id='lbAccion_{0}' style='font-size:9px;'>{1}</label>  </td>", item.OrdenAccion, item.DescripcionAccion);
                            shtml.AppendFormat("<td style='text-align:right'><a href=# onclick='return BuscarAccion({0});' ><img src='../Images/botones/buscar.png' border=0></a></td>", item.IdmapaAccion);
                            #endregion

                            #region ChkVisible
                            shtml.AppendFormat("<td style='text-align:center'><input type='checkbox' id='chkVisible_{0}' style='width: 30px' class='chk' onclick='return ActualizarV(this,{2})' {1}></input></td>", item.OrdenAccion, strCheckedV, item.IdmapaAccion);
                            #endregion

                            #region Prioridad
                            shtml.AppendFormat("<td style='text-align:center'><input type='text' id='txtPrioridadId_{0}' value={1} style='width: 20px; text-align:center' onkeyup='return ActualizarPri(this,{2});'></input></td>", item.OrdenAccion, item.PrioridadAccion, item.IdmapaAccion);
                            #endregion

                            #region ChkObligatorio
                            shtml.AppendFormat("<td style='text-align:center'><input type='checkbox' id='chkObligatorio_{0}' class='chk' onclick='return ActualizarO(this,{2})' {1}></input></td>", item.OrdenAccion, strCheckedO, item.IdmapaAccion);
                            #endregion

                            #region AccionPrerrequisito
                            //shtml.AppendFormat("<td ><label id='lbAccionPreq_{0}' style='font-size:8px;'>{1}</label>  </td>", item.OrdenAccion, DescripcionAccionPrerrequisito(item.CodigoAccionPrerequisito));
                            //shtml.AppendFormat("<td style='text-align:right'><input type='text' id='txtPreqId_{0}' value={1} style='width: 30px; text-align:center'></input></td>", item.OrdenAccion, item.CodigoAccionPrerequisito, item.IdmapaAccion);
                            //shtml.AppendFormat("<td style='text-align:right'><a href='#' onclick='return ActualizarPre(txtPreqId_{0},{1},{0});' title='Agregar Prerrequisito.'><img src='../Images/botones/ok.png' width=16></a></td>", item.OrdenAccion, item.IdmapaAccion);

                            //shtml.AppendFormat("<td style='text-align:center'><select id='ddlPreq_{0}' onchange='return ActualizarPre(ddlPreq_{0},{1},{0});' style='width: 40px;'>{2}</select></td>", item.OrdenAccion, item.IdmapaAccion, optionP);
                            shtml.AppendFormat("<td style='text-align:center'><select id='ddlPreq_{0}' onchange='return ActualizarPre(this,{1},{0});' style='width: 60px;'>{2}</select></td>", item.OrdenAccion, item.IdmapaAccion, optionP);
                            #endregion

                            #region Objeto
                            shtml.AppendFormat("<td style='display:none'><input type='text' id='txtvalorObjId_{0}' value={1} style='width: 50px; text-align:center' readonly='true'></input></td>", item.OrdenAccion, item.CodigoObjeto);
                            shtml.AppendFormat("<td ><label id='lbObjeto_{0}' style='font-size:9px;'>{1}</label>  </td>", item.OrdenAccion, item.DescripcionObjeto);

                            if (item.CodigoObjeto == 0)
                                shtml.AppendFormat("<td style='text-align:right; visibility:hidden;'><a href=# onclick='return AgregarParametro({0},{1});' ><img src='../Images/botones/more.png' border=0 width='25px'></a></td>", item.CodigoObjeto, item.IdmapaAccion);
                            else
                            {
                                obj = new BL_WORKF_OBJECTS().ObtenerObjects(GlobalVars.Global.OWNER, item.CodigoObjeto);
                                tipoObj = new BLTipoObjeto().Obtener(GlobalVars.Global.OWNER, obj.WRKF_OTID);

                                if (tipoObj[0].WRKF_OPREF == GlobalVars.Global.PrefijoMailTer)
                                    shtml.AppendFormat("<td style='text-align:right;'><a href=# onclick='return AgregarParametro({0},{1});' ><img src='../Images/botones/more.png' border=0 width='25px'></a></td>", item.CodigoObjeto, item.IdmapaAccion);
                                else
                                    shtml.AppendFormat("<td style='text-align:right; visibility:hidden;'><a href=# onclick='return AgregarParametro({0},{1});' ><img src='../Images/botones/more.png' border=0 width='25px'></a></td>", item.CodigoObjeto, item.IdmapaAccion);
                            }
                            shtml.AppendFormat("<td style='text-align:right'><a href=# onclick='return BuscarObjeto({0});' ><img src='../Images/botones/buscar.png' border=0></a></td>", item.IdmapaAccion);
                            shtml.AppendFormat("<td style='text-align:right'><a href=# onclick='return QuitarObjeto({0},{1});' ><img src='../Images/iconos/delete.png' border=0></a></td>", item.CodigoObjeto, item.IdmapaAccion);
                            #endregion

                            #region Evento
                            shtml.AppendFormat("<td style='text-align:center'><select id='ddlVento_{0}' onchange='return ActualizarEvento(this,{1});' style='width: 110px'>{2}</select></td>", item.OrdenAccion, item.IdmapaAccion, option);
                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.Etrigger);
                            #endregion

                            #region Transicion
                            shtml.AppendFormat("<td style='text-align:center'><select id='ddlTransicion_{0}' onchange='return ActualizarTransicion(this,{1});' style='width: 110px;'>{2}</select></td>", item.OrdenAccion, item.IdmapaAccion, optionT);//optionTransicion                            

                            if (item.Transicion != 0)
                                shtml.AppendFormat("<td style='text-align:right;'><a href=# onclick='return agregarParametroTra({0},{1});' ><img src='../Images/botones/more.png' border=0 width='25px'></a></td>", item.CodigoObjeto, item.IdmapaAccion);
                            else
                                shtml.AppendFormat("<td style='text-align:right; visibility:hidden;'><a href=# onclick='return agregarParametroTra({0},{1});' ><img src='../Images/botones/more.png' border=0 width='25px'></a></td>", item.Transicion, item.IdmapaAccion);

                            #endregion

                            #region MappingNext
                            shtml.AppendFormat("<td style='text-align:center'><input type='text' id='txtMappingNext_{0}' value={1} style='width: 30px; text-align:center'></input></td>", item.OrdenAccion, item.Amtrigger, item.IdmapaAccion);
                            shtml.AppendFormat("<td style='text-align:right'><a href='#' onclick='return ActualizarNext(txtMappingNext_{0},{1},{0});' title='Agregar Mapping next.'><img src='../Images/botones/ok.png' width=16px></a></td>", item.OrdenAccion, item.IdmapaAccion);
                            #endregion

                            #region OrdenEliminar
                            shtml.AppendFormat("<td style='text-align:center'><a href='#' onclick='return moverOpcion({0},-1);' title='bajar orden.'><img src='../Images/iconos/down.png' width='16px' ></a></td>", item.OrdenAccion);
                            shtml.AppendFormat("<td style='text-align:center'>&nbsp;&nbsp;<a href='#' onclick='return moverOpcion({0},1);' title='subir orden.'><img src='../Images/iconos/up.png' width='16px' ></a></td>", item.OrdenAccion);
                            shtml.AppendFormat("<td style='text-align:center'><a href='#' onclick='return EliminarMapping({0},1);' title='Eliminar.'><img src='../Images/iconos/delete.png' width='16px'></a></td>", item.IdmapaAccion, item.OrdenAccion);

                            #endregion

                            shtml.Append("</tr>");
                            shtml.AppendFormat("<tr><td colspan='21'> <hr /> </td></tr>");
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarAcctionMappingsValores", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarObjetosParametros()
        {
            StringBuilder shtml = new StringBuilder();
            objetoParametro = ObjetoParametroTmp;
            Resultado retorno = new Resultado();

            try
            {
                var clase = "'ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'";
                shtml.Append("<table id='TblParametros' border=0 width='100%;'>");
                shtml.Append("<thead><tr><th class=" + clase + " style='width: 30px; display:none;'>Id</th>");
                shtml.Append("<th class=" + clase + " style='width: 30px;'>Id</th>");
                shtml.Append("<th class=" + clase + " style='width: 300px;'>Correo Electrónico</th>");
                shtml.Append("<th class=" + clase + " style='width: 150px;'>Usuario Reg.</th>");
                shtml.Append("<th class=" + clase + " style='width: 150px;'>Fecha Reg.</th>");
                shtml.Append("<th class=" + clase + " style='width: 30px;'></th>");
                shtml.Append("</tr></thead>");

                if (objetoParametro != null)
                {
                    foreach (var item in objetoParametro.OrderBy(x => Convert.ToInt32(x.Id)))
                    {
                        shtml.Append("<tr class='k-grid-content'>");
                        shtml.AppendFormat("<td style='display:none;'><input type='text' id='txtId_{0}' value='{0}' style='width: 50px;'></input></td>", item.Id);
                        shtml.AppendFormat("<td style='text-align:center'>{0}</td>", item.Id);
                        shtml.AppendFormat("<td ><input type='email' name='email' id='txtvalor_{0}' value='{1}' style='width: 250px; class='requerido' placeholder='Ingrese correo'></input></td>", item.Id, item.valor);
                        shtml.AppendFormat("<td style='text-align:center'>{0}</td>", item.UsuarioCrea);
                        shtml.AppendFormat("<td style='text-align:center'>{0}</td>", item.FechaCrea);
                        shtml.AppendFormat("<td style='text-align:center'><a href='#' onclick='return Eliminar({0});' title='Eliminar.'><img src='../Images/iconos/delete.png' width=16></a></td>", item.Id);
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
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ListarObjetosParametros", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerParametroTransicion(decimal idAccionMapping, decimal wrkfdtid)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BL_WORKF_PARAMETERS().ObtenerParametroTransicion(GlobalVars.Global.OWNER, idAccionMapping, wrkfdtid);
                    if (datos != null)
                    {
                        retorno.result = 1;
                    }
                    else
                    {
                        TransicionParametroTmp = new List<DTOParametroWorkflow>();
                        retorno.result = 0;
                    }

                    retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtenerParametroTransicion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerTemporalParamentrosTransicion(DTOParametroWorkflow entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                transicionParametro = TransicionParametroTmp;
                if (transicionParametro == null) transicionParametro = new List<DTOParametroWorkflow>();
                decimal nuevoId = 1;
                if (transicionParametro.Count > 0) nuevoId = transicionParametro.Max(x => x.Id) + 1;
                entidad.Id = nuevoId;
                entidad.UsuarioCrea = UsuarioActual;
                entidad.FechaCrea = DateTime.Now;
                transicionParametro.Add(entidad);
                TransicionParametroTmp = transicionParametro;
                retorno.result = 1;
                retorno.message = "OK";
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtenerTemporalParamentrosTransicion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertarParametroTran()
        {
            Resultado retorno = new Resultado();


            //List<WORKF_PARAMETERS> list = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<WORKF_PARAMETERS>>(param);
            //string param
            //entidad.Id = nuevoId;
            //entidad.UsuarioCrea = UsuarioActual;
            //entidad.FechaCrea = DateTime.Now;
            
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (TransicionParametroTmp != null)
                    {
                        DTOParametroWorkflow item = new DTOParametroWorkflow();
                        

                        WORKF_PARAMETERS obj = new WORKF_PARAMETERS();
                        obj.Parametros = obtenerParametrosTransicion();
                        var datos = new BL_WORKF_PARAMETERS().InsertarParametroTransicionB(obj);
                        TransicionParametroTmp = new List<DTOParametroWorkflow>();
                        Session.Remove(K_SESION_PARAMETROS_TRAN);
                        retorno.result = 1;
                        retorno.message = "Se guardo parámetro.";
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha podido guardar el parámetro.";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "insert mappings", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerObjetoParametro(List<DTOParametroWorkflow> objetoParametroCorreo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (objetoParametroCorreo != null)
                {
                    foreach (var x in objetoParametroCorreo)
                    {
                        foreach (var y in ObjetoParametroTmp)
                        {
                            if (x.Id == y.Id)
                            {
                                y.valor = x.valor;
                                retorno.result = 1;
                            }
                        }
                    }
                }
                else
                    retorno.result = 0;
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ObtenerCorreos", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<WORKF_PARAMETERS> obtenerParametrosTransicion()
        {
            List<WORKF_PARAMETERS> datos = new List<WORKF_PARAMETERS>();
            if (TransicionParametroTmp != null)
            {
                TransicionParametroTmp.ForEach(x =>
                {
                    datos.Add(new WORKF_PARAMETERS
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        WRKF_PID = x.codigo,
                        WRKF_PNAME = x.nombre,
                        WRKF_PVALUE = x.valor,
                        WRKF_PORDER = x.orden,
                        WRKF_AMID = x.accionMappingId,
                        WRKF_DTID = x.wrkfdtid,
                        WRKF_PTID = x.wrkfptid,
                        WRKF_OID = x.objetoId,
                        PROC_MOD = x.procmod,
                        LOG_USER_CREAT = UsuarioActual,
                        LOG_USER_UPDATE = UsuarioActual,
                    });
                });
            }
            return datos;
        }

        [HttpPost]
        public JsonResult UpdParametroTran(decimal wrkfdtid, string valor,decimal? orden)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var tmp = TransicionParametroTmp.Where(x => x.wrkfdtid == wrkfdtid).ToList();
                    if (tmp != null && tmp.Count() != 0)
                    {
                         foreach (var item in TransicionParametroTmp)
                        {
                            if (item.wrkfdtid == wrkfdtid)
                            {
                                item.valor = valor; 
                                item.orden = orden;
                            }
                        }
                        retorno.result = 1;
                    }
                    else
                        retorno.result = 0;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "UpdParametroTran", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddTransicionParametro(DTOParametroWorkflow entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    transicionParametro = TransicionParametroTmp;
                    if (transicionParametro == null || transicionParametro.Count() == 0) transicionParametro = new List<DTOParametroWorkflow>();
                    if (Convert.ToInt32(entidad.Id) <= 0)
                    {
                        decimal nuevoId = 1;
                        if (transicionParametro.Count > 0) nuevoId = transicionParametro.Max(x => x.Id) + 1;
                        entidad.Id = nuevoId;
                        entidad.UsuarioCrea = UsuarioActual;
                        entidad.FechaCrea = DateTime.Now;
                        transicionParametro.Add(entidad);
                    }
                    TransicionParametroTmp = transicionParametro;
                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "AddAsociado", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<WORKF_PARAMETERS> obtenerParametrosCorreo()
        {
            List<WORKF_PARAMETERS> datos = new List<WORKF_PARAMETERS>();
            if (ObjetoParametroTmp != null)
            {
                ObjetoParametroTmp.ForEach(x =>
                {
                    datos.Add(new WORKF_PARAMETERS
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        WRKF_PID = x.codigo,
                        WRKF_PNAME = x.nombre,
                        WRKF_PVALUE = x.valor,
                        WRKF_PORDER = x.orden,
                        WRKF_AMID = x.accionMappingId,
                        WRKF_DTID = x.wrkfdtid,
                        WRKF_PTID = x.wrkfptid,
                        WRKF_OID = x.objetoId,
                        PROC_MOD = x.procmod,
                        LOG_USER_CREAT = UsuarioActual,
                        LOG_USER_UPDATE = UsuarioActual,
                    });
                });
            }
            return datos;
        }

        public JsonResult InsertarParametroCorreo(WORKF_PARAMETERS en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ObjetoParametroTmp != null)
                    {
                        WORKF_PARAMETERS obj = new WORKF_PARAMETERS();
                        obj.Parametros = obtenerParametrosCorreo();
                        var datos = new BL_WORKF_PARAMETERS().InsertarCorreo(obj);
                        retorno.result = 1;
                        retorno.message = "Se guardo parámetro.";
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha podido guardar el parámetro.";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "insert mappings", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddParametro(DTOParametroWorkflow entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    objetoParametro = ObjetoParametroTmp;
                    if (objetoParametro == null || objetoParametro.Count() == 0) objetoParametro = new List<DTOParametroWorkflow>();
                    if (Convert.ToInt32(entidad.Id) <= 0)
                    {
                        decimal nuevoId = 1;
                        decimal orden = 0;
                        if (objetoParametro.Count > 0) nuevoId = objetoParametro.Max(x => x.Id) + 1;
                        orden = objetoParametro.Count() + 1;
                        entidad.Id = nuevoId;
                        entidad.orden = orden;
                        entidad.UsuarioCrea = UsuarioActual;
                        entidad.FechaCrea = DateTime.Now;
                        objetoParametro.Add(entidad);
                    }
                    else
                    {
                        var item = objetoParametro.Where(x => x.Id == entidad.Id).FirstOrDefault();
                        entidad.UsuarioCrea = item.UsuarioCrea;
                        entidad.FechaCrea = item.FechaCrea;
                        objetoParametro.Remove(item);
                        objetoParametro.Add(entidad);
                    }
                    ObjetoParametroTmp = objetoParametro;
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

        private string itemsEvento(List<SelectListItem> items, string codeId)
        {
            string option = "";
            foreach (var item in items)
            {
                string selected = "";
                if (item.Value == codeId)
                {
                    selected = " selected=selected ";
                }
                option += "<option value='" + item.Value + "'  '" + selected + "'  >" + item.Text + "</option>";
            }
            return option;
        }

        private string itemsEstadoTransicion(List<SelectListItem> items, string Id)
        {
            string option = "";
            foreach (var item in items)
            {
                string selected = "";
                if (item.Value == Id)
                {
                    selected = " selected=selected ";
                }
                option += "<option value='" + item.Value + "'  '" + selected + "'  >" + item.Text + "</option>";
            }
            return option;
        }

        private string itemsPrerrequisito(List<SelectListItem> items, string codeId)
        {
            string option = "";
            foreach (var item in items)
            {
                string selected = "";
                if (item.Value == codeId)
                {
                    selected = " selected=selected ";
                }
                option += "<option value='" + item.Value + "'  '" + selected + "'  >" + item.Text + "</option>";
            }
            return option;
        }

        public JsonResult ValidarAccion(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (ActionMappingsTmp != null)
                {
                    var Count = (from x in ActionMappingsTmp
                                 where (x.CodigoAccion == Id)
                                 select x).Count();
                    if (Count != 1)
                        retorno.result = 1;
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "El item seleccionado ya ha sido ingresado";
                    }
                }
                else
                    retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ValidarAccion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarObjeto(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (ActionMappingsTmp != null)
                {
                    var Count = (from x in ActionMappingsTmp
                                 where (x.CodigoObjeto == Id)
                                 select x).Count();
                    if (Count != 1)
                        retorno.result = 1;
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "El item seleccionado ya ha sido ingresado";
                    }
                }
                else
                    retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ValidarAccion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Insertar(WORKF_ACTIONS_MAPPINGS en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ActionMappingsTmp != null)
                    {
                        WORKF_ACTIONS_MAPPINGS obj = new WORKF_ACTIONS_MAPPINGS();
                        obj.OWNER = GlobalVars.Global.OWNER;
                        obj.WRKF_ID = en.WRKF_ID;
                        obj.WRKF_SID = en.WRKF_SID;
                        //obj.WRKF_TID = en.WRKF_TID;
                        obj.WRKF_TID = null;
                        obj.LOG_USER_CREAT = UsuarioActual;
                        obj.LOG_USER_UPDATE = UsuarioActual;
                        if (obj.WRKF_ID != 0)
                        {
                            var datos = new BL_WORKF_ACTIONS_MAPPINGS().Insertar(obj);
                            //retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                            retorno.result = 1;
                        }
                        else
                        {
                            retorno.message = "Es obligatorio seleccionar un WorkFlow";
                            retorno.result = 0;
                        }
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha podido grabar. Es obligatorio seleccionar un WorkFlow";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "insert mappings", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarMappings(WORKF_ACTIONS_MAPPINGS en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ActionMappingsTmp != null)
                    {
                        int Parametro = 0;

                        List<WORKF_OBJECTS> lista = new List<WORKF_OBJECTS>();
                        lista = new BL_WORKF_OBJECTS().ListarObjetosParametros(GlobalVars.Global.OWNER, en.WRKF_ID, en.WRKF_SID);

                        foreach (var item in lista)
                        {
                            Parametro = new BL_WORKF_PARAMETERS().ParametroXObjeto(GlobalVars.Global.OWNER, en.WRKF_ID, en.WRKF_SID, item.WRKF_OID);

                            if (Parametro == 0)
                            {
                                retorno.result = 0;
                                retorno.message = "Todo objeto ingresado debe tener como mínimo un parámetro. Ingrese parámetro";
                                return Json(retorno, JsonRequestBehavior.AllowGet);
                            }
                        }

                        var CountAccion = (from x in ActionMappingsTmp
                                           where (x.CodigoAccion != 0)
                                           select x).Count();

                        //var CountAmtrigger = (from x in ActionMappingsTmp
                        //                      where (x.Amtrigger != 0)
                        //                      select x).Count();

                        var CountEvento = (from x in ActionMappingsTmp
                                           where (x.Etrigger != 0)
                                           select x).Count();

                        if (CountAccion > 0)
                        {
                            //if (CountAmtrigger > 0)
                            //{
                            //if (CountEvento == ActionMappingsTmp.Count())
                            if (CountEvento >= 1)
                            {
                                WORKF_ACTIONS_MAPPINGS obj = new WORKF_ACTIONS_MAPPINGS();
                                obj.OWNER = GlobalVars.Global.OWNER;
                                obj.WRKF_SID = en.WRKF_SID;
                                obj.LOG_USER_UPDATE = UsuarioActual;
                                var datos = new BL_WORKF_ACTIONS_MAPPINGS().ActualizarGrabacion(obj);
                                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                                retorno.result = 1;
                            }
                            else
                            {
                                retorno.result = 0;
                                retorno.message = "Las acciones ingresadas deben de tener eventos seleccionados";
                            }
                            //}
                            //else
                            //{
                            //    retorno.result = 0;
                            //    retorno.message = "Debe tener como mínimo un Mapping Next ingresado";
                            //}
                        }
                        else
                        {
                            retorno.result = 0;
                            retorno.message = "Debe tener como mínimo una Acción ingresada";
                        }
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha podido grabar";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "insert mappings", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarAccion(decimal Accion, decimal idMappings)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ActionMappingsTmp != null)
                    {
                        WORKF_ACTIONS_MAPPINGS obj = new WORKF_ACTIONS_MAPPINGS();
                        obj.OWNER = GlobalVars.Global.OWNER;
                        obj.WRKF_AMID = idMappings;
                        obj.WRKF_AID = Accion;
                        obj.LOG_USER_UPDATE = UsuarioActual;
                        var datos = new BL_WORKF_ACTIONS_MAPPINGS().ActualizarAccion(obj);
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha podido actualizar";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ActualizarAccion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarVisible(string Visible, decimal idMappings)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ActionMappingsTmp != null)
                    {
                        WORKF_ACTIONS_MAPPINGS obj = new WORKF_ACTIONS_MAPPINGS();
                        obj.OWNER = GlobalVars.Global.OWNER;
                        obj.WRKF_AMID = idMappings;
                        obj.WRKF_AMVISIBLE = Visible;
                        obj.LOG_USER_UPDATE = UsuarioActual;
                        var datos = new BL_WORKF_ACTIONS_MAPPINGS().ActualizarVisible(obj);
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha podido actualizar";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ActualizarVisible", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarPrioridad(decimal? Prioridad, decimal idMappings)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ActionMappingsTmp != null)
                    {
                        WORKF_ACTIONS_MAPPINGS obj = new WORKF_ACTIONS_MAPPINGS();
                        obj.OWNER = GlobalVars.Global.OWNER;
                        obj.WRKF_AMID = idMappings;
                        obj.WRKF_AMPR = Prioridad;
                        obj.LOG_USER_UPDATE = UsuarioActual;
                        var datos = new BL_WORKF_ACTIONS_MAPPINGS().ActualizarPrioridad(obj);
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha podido actualizar";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ActualizarPrioridad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarObligatorio(string Obligatorio, decimal idMappings)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ActionMappingsTmp != null)
                    {
                        WORKF_ACTIONS_MAPPINGS obj = new WORKF_ACTIONS_MAPPINGS();
                        obj.OWNER = GlobalVars.Global.OWNER;
                        obj.WRKF_AMID = idMappings;
                        obj.WRKF_AMAND = Obligatorio;
                        obj.LOG_USER_UPDATE = UsuarioActual;
                        var datos = new BL_WORKF_ACTIONS_MAPPINGS().ActualizarObligatorio(obj);
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha podido actualizar";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ActualizarObligatorio", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarPrerrequisito(decimal? Prerrequisito, decimal idMappings, decimal NroOrden)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ActionMappingsTmp != null)
                    {
                        WORKF_ACTIONS_MAPPINGS obj = new WORKF_ACTIONS_MAPPINGS();
                        //if (Prerrequisito < NroOrden)
                        //{
                        obj.OWNER = GlobalVars.Global.OWNER;
                        obj.WRKF_AMID = idMappings;
                        obj.WRKF_AIDPRE = Prerrequisito;
                        obj.LOG_USER_UPDATE = UsuarioActual;
                        var datos = new BL_WORKF_ACTIONS_MAPPINGS().ActualizarPrerrequisito(obj);
                        retorno.message = "Prerrequisito registrado";
                        retorno.result = 1;
                        //}
                        //else
                        //{
                        //    retorno.result = 0;
                        //    retorno.message = "Debe escoger como prerrequisitos solo las acciones que se encuentren registradas anteriormente a la fila seleccionada";
                        //}
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha podido actualizar";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ActualizarPrerrequisito", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarNextA(decimal? Next, decimal idMappings, decimal NroOrden)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ActionMappingsTmp != null)
                    {
                        WORKF_ACTIONS_MAPPINGS obj = new WORKF_ACTIONS_MAPPINGS();
                        //if (Next != idAccion)
                        //{
                        obj.OWNER = GlobalVars.Global.OWNER;
                        obj.WRKF_AMID = idMappings;
                        obj.WRKF_AMTRIGGER = Next;
                        obj.LOG_USER_UPDATE = UsuarioActual;
                        var datos = new BL_WORKF_ACTIONS_MAPPINGS().ActualizarNext(obj);
                        retorno.message = "Mapping next registrado";
                        retorno.result = 1;
                        //}
                        //else
                        //{
                        //    retorno.result = 0;
                        //    retorno.message = "La Acción id ingresada no puede ser la misma";
                        //}
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha podido actualizar";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ActualizarPrerrequisito", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarObjeto(decimal Objeto, decimal idMappings)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ActionMappingsTmp != null)
                    {
                        WORKF_ACTIONS_MAPPINGS obj = new WORKF_ACTIONS_MAPPINGS();
                        obj.OWNER = GlobalVars.Global.OWNER;
                        obj.WRKF_AMID = idMappings;
                        obj.WRKF_OID = Objeto;
                        obj.LOG_USER_UPDATE = UsuarioActual;
                        var datos = new BL_WORKF_ACTIONS_MAPPINGS().ActualizarObjeto(obj);
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha podido actualizar";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ActualizarObjeto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult QuitarObjeto(decimal Objeto, decimal idMappings)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ActionMappingsTmp != null)
                    {
                        WORKF_ACTIONS_MAPPINGS obj = new WORKF_ACTIONS_MAPPINGS();
                        obj.OWNER = GlobalVars.Global.OWNER;
                        obj.WRKF_AMID = idMappings;
                        obj.WRKF_OID = Objeto;
                        obj.LOG_USER_UPDATE = UsuarioActual;
                        var datos = new BL_WORKF_ACTIONS_MAPPINGS().EliminarObjeto(obj);
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha podido quitar objeto";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "QuitarObjeto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarEvento(decimal Evento, decimal idMappings)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ActionMappingsTmp != null)
                    {
                        WORKF_ACTIONS_MAPPINGS obj = new WORKF_ACTIONS_MAPPINGS();
                        obj.OWNER = GlobalVars.Global.OWNER;
                        obj.WRKF_AMID = idMappings;
                        obj.WRKF_ETRIGGER = Evento;
                        obj.LOG_USER_UPDATE = UsuarioActual;
                        var datos = new BL_WORKF_ACTIONS_MAPPINGS().ActualizarEvento(obj);
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha podido actualizar";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ActualizarEvento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarTransicion(decimal Transicion, decimal idMappings)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ActionMappingsTmp != null)
                    {
                        WORKF_ACTIONS_MAPPINGS obj = new WORKF_ACTIONS_MAPPINGS();
                        obj.OWNER = GlobalVars.Global.OWNER;
                        obj.WRKF_AMID = idMappings;
                        obj.WRKF_TID = Transicion;
                        obj.LOG_USER_UPDATE = UsuarioActual;
                        var datos = new BL_WORKF_ACTIONS_MAPPINGS().ActualizarTransicion(obj);
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha podido actualizar";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ActualizarTransicion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarParametrosTransicion(decimal idMapping)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ActionMappingsTmp != null)
                    {
                        var datos = new BL_WORKF_PARAMETERS().EliminarTransicionParametro(GlobalVars.Global.OWNER, idMapping, UsuarioActual);
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "Error al resetear parámetros de esta transición";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ActualizarTransicion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult obtenerParametroCorreoXobjeto(decimal idObj)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var datos = new BL_WORKF_PARAMETERS().ListarXObjects(GlobalVars.Global.OWNER, idObj);

                    if (datos != null)
                    {
                        datos.ForEach(s =>
                        {
                            objetoParametro.Add(new DTOParametroWorkflow
                            {
                                Id = s.WRKF_PID,
                                codigo = s.WRKF_PID,
                                nombre = s.WRKF_PNAME,
                                valor = s.WRKF_PVALUE,
                                orden = s.WRKF_PORDER,
                                accionMappingId = s.WRKF_AMID,
                                wrkfdtid = s.WRKF_DTID,
                                wrkfptid = s.WRKF_PTID,
                                objetoId = s.WRKF_OID,
                                procmod = s.PROC_MOD,
                                FechaCrea = s.LOG_DATE_CREAT,
                                UsuarioCrea = s.LOG_USER_CREAT
                            });
                        });
                        ObjetoParametroTmp = objetoParametro;
                        retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                        retorno.message = "Parámetro encontrado";
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.message = "No se ha podido encontrar Parámetros";
                        retorno.result = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "obtenerParametroCorreoXobjeto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult obtener(decimal Idwrk, decimal Idst)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    BL_WORKF_ACTIONS_MAPPINGS servicio = new BL_WORKF_ACTIONS_MAPPINGS();
                    var datos = servicio.Listar(GlobalVars.Global.OWNER, Idwrk, Idst);

                    if (datos != null)
                    {
                        //if (datos.ParametrosTransicion != null)
                        //{
                        //    transicionParametro = new List<DTOParametroWorkflow>();
                        //    datos.ParametrosTransicion.ForEach(x =>
                        //    {
                        //        transicionParametroAux.Add(new DTOParametroWorkflow
                        //        {
                        //            codigo = x.WRKF_PID,
                        //            nombre = x.WRKF_PNAME,
                        //            valor = x.WRKF_PVALUE,
                        //            orden = x.WRKF_PORDER,
                        //            accionMappingId = x.WRKF_AMID,
                        //            wrkfdtid = x.WRKF_DTID,
                        //            wrkfptid = x.WRKF_PTID,
                        //            objetoId = x.WRKF_OID,
                        //            procmod = x.PROC_MOD,
                        //            UsuarioCrea = x.LOG_USER_CREAT,
                        //            FechaCrea = x.LOG_DATE_CREAT,
                        //            UsuarioModifica = x.LOG_USER_UPDATE,
                        //            FechaModifica = x.LOG_DATE_UPDATE,
                        //            Activo = x.ENDS.HasValue ? false : true
                        //        });
                        //    });
                        //    TransicionParametroTmpAux = transicionParametroAux;
                        //}

                        foreach (var item in datos.Mapping)
                        {
                            var dato = new BL_WORKF_ACTIONS().Obtener(GlobalVars.Global.OWNER, item.WRKF_AID);
                            if (dato != null)
                                item.WRKF_ANAME = dato.WRKF_ANAME.ToUpper();

                            if (item.WRKF_OID != null && item.WRKF_OID != 0)
                            {
                                var desc = new BL_WORKF_OBJECTS().ObtenerObjects(GlobalVars.Global.OWNER, item.WRKF_OID);
                                if (desc != null)
                                    item.WRKF_ODESC = desc.WRKF_ODESC.ToUpper();
                            }
                        }

                        if (datos != null)
                        {
                            datos.Mapping.ForEach(s =>
                            {
                                mappings.Add(new DTOActionMappings
                                {
                                    OrdenAccion = s.WRKF_AORDER,
                                    OrdenAccionNew = s.WRKF_AORDERNew,
                                    IdmapaAccion = s.WRKF_AMID,
                                    CodigoAccion = s.WRKF_AID,
                                    Transicion = s.WRKF_TID,
                                    workFlow = s.WRKF_ID,
                                    estado = s.WRKF_SID,
                                    DescripcionAccion = s.WRKF_ANAME,
                                    CodigoAccionAux = s.WRKF_AIDAUX,
                                    IndicadorVisibilidad = s.WRKF_AMVISIBLE,
                                    PrioridadAccion = s.WRKF_AMPR,
                                    IndicadorObligatorio = s.WRKF_AMAND,
                                    CodigoAccionPrerequisito = s.WRKF_AIDPRE,
                                    CodigoObjeto = s.WRKF_OID,
                                    DescripcionObjeto = s.WRKF_ODESC,
                                    Etrigger = s.WRKF_ETRIGGER,
                                    Amtrigger = s.WRKF_AMTRIGGER
                                });
                            });
                            ActionMappingsTmp = mappings;
                            retorno.data = Json(datos, JsonRequestBehavior.AllowGet);
                            retorno.message = "Mappings encontrado";
                            retorno.result = 1;
                        }
                    }
                    else
                    {
                        retorno.message = "No se ha podido encontrar datos del mappings";
                        retorno.result = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "obtener mappings", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtieneDescripcion(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BL_WORKF_TRANSITIONS servicio = new BL_WORKF_TRANSITIONS();
                    var dato = servicio.ObtenerCicloTransitions(GlobalVars.Global.OWNER, id);

                    if (dato != null)
                    {
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(dato, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado la descripción de la transición";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "obtener datos uso de transición", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //public JsonResult ActualizarOrden(decimal IdTransicion, short orden, int opcion, decimal Idwrk, decimal Idst)
        public JsonResult ActualizarOrden(short orden, int opcion, decimal Idwrk, decimal Idst)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    bool opcOK = false;
                    int ord = 0;
                    int contador = 0;
                    int newOrder = 0;
                    int resultado = 0;

                    if (orden == 1)
                        //ord = orden + 1;
                        ord = 1;
                    else
                        ord = orden;

                    BL_WORKF_ACTIONS_MAPPINGS servicio = new BL_WORKF_ACTIONS_MAPPINGS();
                    List<WORKF_ACTIONS_MAPPINGS> filaBajar = new List<WORKF_ACTIONS_MAPPINGS>();
                    List<WORKF_ACTIONS_MAPPINGS> filaSubir = new List<WORKF_ACTIONS_MAPPINGS>();

                    if (opcion == -1)
                        //filaBajar = servicio.ObtenerOrdenBajar(GlobalVars.Global.OWNER, IdTransicion, ord, Idwrk, Idst);
                        filaBajar = servicio.ObtenerOrdenBajar(GlobalVars.Global.OWNER, ord, Idwrk, Idst);
                    if (opcion == 1)
                        //filaSubir = servicio.ObtenerOrdenSubir(GlobalVars.Global.OWNER, IdTransicion, ord, Idwrk, Idst);
                        filaSubir = servicio.ObtenerOrdenSubir(GlobalVars.Global.OWNER, ord, Idwrk, Idst);

                    if (filaBajar.Count() == 2)
                    {
                        foreach (WORKF_ACTIONS_MAPPINGS item in filaBajar)
                        {
                            contador += 1;

                            if (contador == 1)
                                newOrder = Convert.ToInt32(item.WRKF_AORDER) + 1;
                            else
                                newOrder = Convert.ToInt32(item.WRKF_AORDER) - 1;

                            resultado = servicio.ActualizarOrden(GlobalVars.Global.OWNER, item.WRKF_AMID, newOrder, UsuarioActual);
                        }

                        if (resultado == 1)
                        {
                            retorno.result = 1;
                            retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        }
                        else
                        {
                            retorno.result = 0;
                            retorno.message = "No se envío el parámetro esperado [Up or Down]";
                        }
                    }

                    else if (filaSubir.Count() == 2)
                    {
                        foreach (WORKF_ACTIONS_MAPPINGS item in filaSubir)
                        {
                            contador += 1;

                            if (contador == 1)
                                newOrder = Convert.ToInt32(item.WRKF_AORDER) - 1;
                            else
                                newOrder = Convert.ToInt32(item.WRKF_AORDER) + 1;

                            resultado = servicio.ActualizarOrden(GlobalVars.Global.OWNER, item.WRKF_AMID, newOrder, UsuarioActual);
                        }

                        if (resultado == 1)
                        {
                            retorno.result = 1;
                            retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        }
                        else
                        {
                            retorno.result = 0;
                            retorno.message = "No se envío el parámetro esperado [Up or Down]";
                        }
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se envío el parámetro esperado [Up or Down]";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ActualizarOrden", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarAccion(decimal idMappings, string Orden, decimal Wrkf, decimal Estado)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (ActionMappingsTmp != null)
                    {
                        WORKF_ACTIONS_MAPPINGS obj = new WORKF_ACTIONS_MAPPINGS();
                        obj.OWNER = GlobalVars.Global.OWNER;
                        obj.WRKF_AMID = idMappings;
                        obj.LOG_USER_UPDATE = UsuarioActual;
                        var datos = new BL_WORKF_ACTIONS_MAPPINGS().Eliminar(obj);

                        obj.WRKF_AORDER = Orden;
                        obj.WRKF_ID = Wrkf;
                        obj.WRKF_SID = Estado;
                        var lista = new BL_WORKF_ACTIONS_MAPPINGS().ObtenerOrdenActualizar(obj);

                        int NewOrder = 0;
                        foreach (var item in lista)
                        {
                            NewOrder += 1;
                            item.WRKF_AORDERNew = NewOrder.ToString();
                        }

                        var newlista = lista;

                        foreach (var item in newlista)
                        {
                            obj.WRKF_AORDERNew = item.WRKF_AORDERNew;
                            obj.WRKF_AMID = item.WRKF_AMID;
                            var resultado = new BL_WORKF_ACTIONS_MAPPINGS().ActualizarOrdenEliminar(obj);
                        }

                        retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha podido actualizar";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "ActualizarTransicion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerProcMod(decimal wrkfid)
        {
            Resultado retorno = new Resultado();
            try
            {
                var resultado = new BL_WORKF_ACTIONS().ObtenerProcMod(GlobalVars.Global.OWNER, wrkfid);
                if (resultado != string.Empty || resultado != null)
                {
                    retorno.result = 1;
                    retorno.data = Json(resultado, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "No se ha encontrado dato";
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerProcMod", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerDescripcionObjeto(decimal idObj)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    WORKF_ACTIONS_MAPPINGS item = new WORKF_ACTIONS_MAPPINGS();
                    var desc = new BL_WORKF_OBJECTS().ObtenerObjects(GlobalVars.Global.OWNER, idObj);
                    if (desc != null)
                    {
                        item.WRKF_ODESC = desc.WRKF_ODESC.ToUpper();
                        retorno.result = 1;
                        retorno.data = Json(item.WRKF_ODESC, JsonRequestBehavior.AllowGet);
                    }
                    else
                        retorno.result = 0;

                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener Descripción Objeto", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellParametro(decimal id)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    //consulta si existe en bd
                    WORKF_PARAMETERS dato = new WORKF_PARAMETERS();
                    dato = new BL_WORKF_PARAMETERS().ObtenerParameterXId(GlobalVars.Global.OWNER, id);

                    if (dato == null)
                    {
                        objetoParametro = ObjetoParametroTmp;

                        if (objetoParametro != null)
                        {
                            var objDel = objetoParametro.Where(x => x.Id == id).FirstOrDefault();
                            if (objDel != null)
                            {
                                objetoParametro.Remove(objDel);
                                ObjetoParametroTmp = objetoParametro;
                                retorno.result = 1;
                                retorno.message = "OK";
                            }
                        }
                    }
                    else
                    {
                        WORKF_PARAMETERS er = new WORKF_PARAMETERS();
                        er.OWNER = GlobalVars.Global.OWNER;
                        er.WRKF_PID = id;
                        er.LOG_USER_UPDATE = UsuarioActual;
                        var del = new BL_WORKF_PARAMETERS().EliminarParametro(er);
                        objetoParametro = ObjetoParametroTmp;
                        var objDel = objetoParametro.Where(x => x.Id == id).FirstOrDefault();
                        if (objDel != null)
                        {
                            objetoParametro.Remove(objDel);
                            ObjetoParametroTmp = objetoParametro;
                        }

                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "DellAddMetodoPago", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}

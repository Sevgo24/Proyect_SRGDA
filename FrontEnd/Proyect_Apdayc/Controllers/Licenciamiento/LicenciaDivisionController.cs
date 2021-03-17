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


namespace Proyect_Apdayc.Controllers.Licenciamiento
{
    public class LicenciaDivisionController : Base
    {
        //
        // GET: /LicenciaDivision/
        string nomAplicacion = "SGRDA";


        [HttpPost]
        public JsonResult ValidacionDivision(decimal idModalidad, decimal idEstablecimiento)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    decimal idUbigeoEst = new BLLicenciaDivision().ObtenerUbigeoEstablecimiento(idEstablecimiento);
                    //Obtener divisiones con la Modalidad indicada.
                    List<BELicenciaDivision> listaDivisiones = new BLLicenciaDivision().ObtenerDivisionesXModalidad(idModalidad);
                    //agregamos al ddl las divisiones que coinciden con el ubigeo.

                    List<BELicenciaDivision> listaDivFiltrado = new List<BELicenciaDivision>();
                    BELicenciaDivision objDivFiltrado = null;
                    foreach (var division in listaDivisiones)
                    {
                        int validacion = 0;
                        validacion = new BLLicenciaDivision().ValidarDivsionXUbigeo(division.DAD_ID, idUbigeoEst);
                        if (validacion == 1)
                        {
                            if (listaDivFiltrado != null)
                            {
                                //Si no existe la division entes agregamos a la lista.
                                int existe = listaDivFiltrado.Where(x => x.DAD_ID == division.DAD_ID).Count();
                                if (existe == 0)
                                {
                                    objDivFiltrado = new BELicenciaDivision();
                                    objDivFiltrado.DAD_ID = division.DAD_ID;
                                    objDivFiltrado.DAD_NAME = division.DAD_NAME;
                                    listaDivFiltrado.Add(objDivFiltrado);
                                }
                            }
                        }
                    }

                    //string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
                    //string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
                    //string codGrupoMod = new BLModalidad().Obtener(GlobalVars.Global.OWNER, idModalidad).MOG_ID;
                    //var listaDivisionModalidad = new BLAgenteRecaudo().Obtener_Division_Modalidad_Agente(UsuarioActual);
                    var datos = listaDivFiltrado.Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.DAD_ID),
                        Text = Convert.ToString(c.DAD_NAME)
                    });
                    retorno.result = 1;
                    retorno.data = Json(datos.OrderBy(x => x.Text), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ListaDivisionesXOficina", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region DIVISIONES
        [HttpPost]
        public JsonResult AddDivisionAdm(decimal idLicencia, decimal idDivision)
        {
            Resultado retorno = new Resultado();
            try
            {
                int resultadoDivision = 0;
                int estadoValidacionDiv = 1;
                string mensajeValidacion = "";
                //int pasaValidacion = 1;
                if (!isLogout(ref retorno))
                {
                    var ListaDivision = new BLLicenciaDivision().ListarDivisionLicencia(GlobalVars.Global.OWNER, idLicencia);
                    foreach (var div in ListaDivision)
                    {
                        if (div.ENDS == null)
                        {
                            mensajeValidacion = "Existe una división activa. Debe dar de baja a la actual para ingresar una nueva división.";
                            estadoValidacionDiv = 0;
                            break;
                        }

                        if (div.DAD_ID == idDivision)
                        {
                            mensajeValidacion = "La división ya existe o esta inactiva.";
                            estadoValidacionDiv = 0;
                            break;
                        }
                    }

                    if (estadoValidacionDiv == 1)
                        resultadoDivision = new BLLicenciaDivision().Insertar(GlobalVars.Global.OWNER, idLicencia, idDivision, UsuarioActual);

                    if (resultadoDivision > 1)
                        mensajeValidacion = "OK";

                    retorno.message = mensajeValidacion;
                    retorno.result = estadoValidacionDiv;
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
        public JsonResult ActEstadoDivisionAdm(decimal id, decimal idLic, decimal idDiv, decimal estado)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    int valdicaionDiv = 1;

                    if (estado == Constantes.EstadoVigencia.ACTIVO)
                    {
                        List<BELicenciaDivision> ListaDivision = new List<BELicenciaDivision>();
                        ListaDivision = new BLLicenciaDivision().ListarDivisionLicencia(GlobalVars.Global.OWNER, idLic);
                        foreach (var itemLic in ListaDivision.Where(x => x.ENDS == null))
                        {
                            if (itemLic.DAD_ID != idDiv)
                            {
                                retorno.result = 0; valdicaionDiv = 0;
                                retorno.message = "No se logro actualizar la información porque existe una división activa.";
                                break;
                            }
                        }
                    }

                    if (valdicaionDiv == 1)
                    {
                        int resultadoDivision = new BLLicenciaDivision().ActualizarEstado(GlobalVars.Global.OWNER, id, idLic, idDiv, UsuarioActual, estado);
                        if (resultadoDivision > 0)
                        {
                            retorno.result = 1;
                            retorno.message = "OK";
                        }
                        else
                        {
                            retorno.result = 0;
                            retorno.message = "No se logro registrar la división.";
                        }
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

        public JsonResult ListarDivisionAdm(decimal idLicencia)
        {
            List<BELicenciaDivision> ListaDivision = new List<BELicenciaDivision>();
            ListaDivision = new BLLicenciaDivision().ListarDivisionLicencia(GlobalVars.Global.OWNER, idLicencia);

            List<BELicenciaDivisionAgente> ListaDivisionAgente = new List<BELicenciaDivisionAgente>();
            ListaDivisionAgente = new BLLicenciaDivisionAgente().Listar(GlobalVars.Global.OWNER, idLicencia);

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
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Agregar Agente</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Fec. Vigencia</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Fec. Baja</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Estado</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='width:30px'></th></tr></thead>");

                    if (ListaDivision != null)
                    {
                        int contador = 0;
                        foreach (var item in ListaDivision)
                        {
                            contador += 1;
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.AppendFormat("<td style='width: 20px;text-align:center;color: black' class='idLicDiv'>{0}</td>", item.LIC_DIV_ID);
                            shtml.AppendFormat("<td style='width:100px;text-align:center;color: black; display:none' class='idDivAdm'>{0}</td>", item.DAD_ID);
                            shtml.AppendFormat("<td style='width:120px;text-align:center;color: black'>{0}</td>", item.DAD_NAME);
                            shtml.AppendFormat("<td style='width:70px;text-align:center;''> <a href=# onclick='AbrirPoPupAddAgente({0},{1});'> <img src='../Images/botones/invoice_more.png' title='Agregar Agente de Recaudo.' border=0></a>", item.LIC_DIV_ID, item.DAD_ID);

                            shtml.AppendFormat("<td style='width:120px;text-align:center;color: black'>{0}</td>", string.Format("{0:dd/MM/yyyy}", item.LOG_DATE_CREAT));
                            shtml.AppendFormat("<td style='width:120px;text-align:center;color: black'>{0}</td>", item.ENDS == null ? "" : string.Format("{0:dd/MM/yyyy}", item.ENDS));
                            shtml.AppendFormat("<td style='width:120px;text-align:center;color: black'>{0}</td>", item.ENDS == null ? "Activo" : "Inactivo");

                            //if (item.ENDS == null)
                            shtml.AppendFormat("<td style='width:30px;text-align:center;'> <a href=# onclick='ActEstadoDivisionAdm({0},{1},{2},{5});'> <img src='../Images/iconos/{3}' title='{4}' border=0></a>", item.LIC_DIV_ID, item.LIC_ID, item.DAD_ID, item.ENDS == null ? "delete.png" : "activate.png", item.ENDS == null ? "Eliminar División" : "Activar División", (item.ENDS == null) ? Constantes.EstadoVigencia.INACTIVO : Constantes.EstadoVigencia.ACTIVO);
                            //else
                            //shtml.AppendFormat("<td style='width:30px;text-align:center;'> <a href=# onclick='actDivisionAdm({0},{1},{2});'> <img src='../Images/iconos/{3}' title='{4}' border=0></a>", item.LIC_DIV_ID, item.LIC_ID, item.DAD_ID, item.ENDS == null ? "delete.png" : "activate.png", item.ENDS == null ? "Eliminar Cliente" : "Activar Cliente");
                            //shtml.AppendFormat("<td style='width:30px;text-align:center;'> <a href=# onclick='delDivisionAdm({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Cliente" : "Activar Cliente");

                            shtml.Append("</td></tr>");
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.Append("<td colspan='3'></td>");
                            shtml.Append("<td colspan='3' style='vertical-align: text-top'><div id='divLicDivAdm" + item.LIC_DIV_ID.ToString() + "'>");
                            if (ListaDivisionAgente != null && ListaDivisionAgente.Where(p => p.DAD_ID == item.DAD_ID).Count() > 0)
                                shtml.Append(getHtmlListarAgentesXDivision(item.LIC_DIV_ID, ListaDivisionAgente.Where(p => p.DAD_ID == item.DAD_ID).ToList()));
                            shtml.Append("</div></td>");

                            shtml.Append("</div></td>");

                            shtml.Append("</tr>");
                            if (ListaDivision.Count != contador)
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

        public StringBuilder getHtmlListarAgentesXDivision(decimal IdOficinaDiv, List<BELicenciaDivisionAgente> ListaAgente)
        {
            StringBuilder shtml = new StringBuilder();
            shtml.Append("<table id='tblOficinaDiv' border=0 width='100%;' class='k-grid k-widget'><thead><tr>");
            shtml.Append("<th style='display:none' class='ui-state-default ui-th-column ui-th-ltr'>Id</th>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Agencia de Recaudo</th>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Rol</th>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Agente</th>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Estado</th>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='width:30px'></th></tr></thead>");

            if (ListaAgente != null)
            {
                foreach (var item in ListaAgente)
                {
                    shtml.Append("<tr style='background-color:white'>");
                    shtml.AppendFormat("<td style='display:none;text-align:center; color: black'>{0}</td>", item.ID);
                    shtml.AppendFormat("<td style='color: black; width:60p'>{0}</td>", item.OFF_NAME);
                    shtml.AppendFormat("<td style='color: black; width:60p'>{0}</td>", item.ROL_DESC);
                    shtml.AppendFormat("<td style='color: black; width:60p'>{0}</td>", item.AGENTE);
                    shtml.AppendFormat("<td style='color: black; width:60p'>{0}</td>", (item.ENDS == null) ? "Activo" : "Inactivo");
                    //if (item.ENDS == null)
                    //    shtml.AppendFormat("<td style='text-align:center; width:30px'> <a href=# onclick='delAgenteDivision({0},{3});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.ID, (item.ENDS == null) ? "delete.png" : "activate.png", (item.ENDS == null) ? "Eliminar Agente" : "Activar Agente", item.LIC_ID);
                    //else
                    //    shtml.AppendFormat("<td style='text-align:center; width:30px'> <a href=# onclick='actAgenteDivision({0},{3});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.ID, (item.ENDS == null) ? "delete.png" : "activate.png", (item.ENDS == null) ? "Eliminar Agente" : "Activar Agente", item.LIC_ID);

                    shtml.AppendFormat("<td style='text-align:center; width:30px'> <a href=# onclick='actAgenteDivision({0},{3},{4});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.ID, (item.ENDS == null) ? "delete.png" : "activate.png", (item.ENDS == null) ? "Eliminar Agente" : "Activar Agente", item.LIC_ID, (item.ENDS == null) ? Constantes.EstadoVigencia.INACTIVO : Constantes.EstadoVigencia.ACTIVO);
                    shtml.Append("</td></tr>");
                }
            }
            shtml.Append(" </table>");
            return shtml;
        }

        #endregion


        #region AGENTE POR DIVISION
        [HttpPost]
        public JsonResult AddDivisionAgenteRecaudo(decimal idRecaudo, decimal idLicencia)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    int estadoValidacion = 1;
                    string msjValidacion = "";
                    BEAgenteRecaudo agente = new BEAgenteRecaudo();
                    agente.OWNER = GlobalVars.Global.OWNER;
                    agente.COLL_OFF_ID = idRecaudo;
                    agente = new BLAgenteRecaudo().ObtenerAgente(agente);

                    if (agente != null)
                    {
                        //Agente_X_Division                        
                        var obtenerAgentes_x_Division = new BLLicenciaDivisionAgente().Obtener_Agente_X_Division(new BELicenciaDivisionAgente()
                        {
                            OWNER = GlobalVars.Global.OWNER,
                            LIC_ID = idLicencia,
                            DAD_ID = agente.DAD_ID,
                            COLL_OFF_ID = agente.COLL_OFF_ID
                        });

                        //VALIDACION - EL AGENTE NO SE REPITA EN LA DIVISION
                        if (obtenerAgentes_x_Division.Count > 0)
                        {
                            msjValidacion = "El agente ya existe. Vuelva a activarlo.";
                            estadoValidacion = 0;
                        }
                        else
                        {
                            // VALIDACION - QUE EÑ AGENTE SEA DE LA MISMA OFICINA DE RECAUDO Y SI ES DISTINTO QUE ENVIE AVISO.
                            List<BELicenciaDivisionAgente> ListaDivisionAgente = new List<BELicenciaDivisionAgente>();
                            ListaDivisionAgente = new BLLicenciaDivisionAgente().Listar(GlobalVars.Global.OWNER, idLicencia);

                            foreach (var itemAgeXDiv in ListaDivisionAgente.Where(x => x.DAD_ID == agente.DAD_ID && x.ENDS == null))
                            {
                                if (itemAgeXDiv.OFF_ID != agente.OFF_ID)
                                {
                                    msjValidacion = "Existen agentes activos de otra oficina de recaudo para esta misma división.\r\nDebe dar de baja a los agentes existentes para ingresar al nuevo agente.";
                                    estadoValidacion = 0;
                                    break;
                                }
                            }

                            if (estadoValidacion == 1)
                            {
                                //REGISTRAR AGENTE POR DIVISION
                                int registro = new BLLicenciaDivisionAgente().Insertar(new BELicenciaDivisionAgente()
                                {
                                    OWNER = GlobalVars.Global.OWNER,
                                    LIC_ID = idLicencia,
                                    DAD_ID = agente.DAD_ID,
                                    COLL_OFF_ID = agente.COLL_OFF_ID,
                                    OFF_ID = agente.OFF_ID,
                                    LOG_USER_CREAT = UsuarioActual,
                                });
                            }

                        }
                    }

                    retorno.result = estadoValidacion;
                    retorno.message = msjValidacion;
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
        public JsonResult actDivisionAgenteRecaudo(decimal id, decimal idLic, decimal idEstado)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BELicenciaDivisionAgente agente = new BELicenciaDivisionAgente();
                    agente.OWNER = GlobalVars.Global.OWNER;
                    agente.ID = id;
                    agente.LIC_ID = idLic;
                    agente.LOG_USER_UPDAT = UsuarioActual;
                    agente.INDICADOR = idEstado;
                    int resultadoDivision = new BLLicenciaDivisionAgente().ActualizarEstado(agente);

                    if (resultadoDivision > 0)
                    {
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se logro actualizar la información del agente.";
                    }
                }
            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "actDivisionAgenteRecaudo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion



    }
}

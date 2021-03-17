using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyect_Apdayc.Clases;
using SGRDA.BL.WorkFlow;
using SGRDA.Entities.WorkFlow;
using SGRDA.Entities;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases.DTO;
using System.Text;
//using SGRDA.Utility;
using SGRDA.Documento;
using SGRDA.BL;


namespace Proyect_Apdayc.Controllers.WorkFlow
{
    public class ActionController : Base
    {
        private const string K_SESSION_AGENTE = "___DTOAgenteUD";
        private const string K_SESSION_AGENTE_DEL = "___DTOAgenteDELUD";
        private const string K_SESSION_AGENTE_ACT = "___DTOAgenteACTUD";

        //
        // GET: /Action/

        List<DTOAgente> Agente = new List<DTOAgente>();

        private List<DTOAgente> AgenteTmpUPDEstado
        {
            get
            {
                return (List<DTOAgente>)Session[K_SESSION_AGENTE_ACT];
            }
            set
            {
                Session[K_SESSION_AGENTE_ACT] = value;
            }
        }

        private List<DTOAgente> AgenteTmpDelBD
        {
            get
            {
                return (List<DTOAgente>)Session[K_SESSION_AGENTE_DEL];
            }
            set
            {
                Session[K_SESSION_AGENTE_DEL] = value;
            }
        }

        public List<DTOAgente> AgenteTmp
        {
            get
            {
                return (List<DTOAgente>)Session[K_SESSION_AGENTE];
            }
            set
            {
                Session[K_SESSION_AGENTE] = value;
            }
        }

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESSION_AGENTE);
            Session.Remove(K_SESSION_AGENTE_ACT);
            Session.Remove(K_SESSION_AGENTE_DEL);
            return View();
        }

        public JsonResult Listar(int skip, int take, int page, int pageSize, string group,
                                string nombre, string etiqueta, decimal idTipoAccion, decimal idTipoDato,
                                decimal idProceso, string idAuto, int estado)
        {
            Resultado retorno = new Resultado();
            var lista = BLListar(GlobalVars.Global.OWNER, nombre, etiqueta, idTipoAccion,
                                            idTipoDato, idProceso, idAuto, estado, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new WORKF_ACTIONS { ListarAcciones = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new WORKF_ACTIONS { ListarAcciones = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<WORKF_ACTIONS> BLListar(string owner, string nombre, string etiqueta,
                                decimal idTipoAccion, decimal idTipoDato, decimal idProceso, string idAuto,
                               int estado, int pagina, int cantRegxPag)
        {
            return new BL_WORKF_ACTIONS().Listar(owner, nombre, etiqueta,
                                 idTipoAccion, idTipoDato, idProceso, idAuto,
                                estado, pagina, cantRegxPag);
        }

        /// <summary>
        /// EJECUTA LOS CAMBIOS DE ESTADO DE LICENCIAMIENTO
        /// </summary>
        /// <param name="aidWrkf"></param>
        /// <param name="idWrkf"></param>
        /// <param name="sidWrkf"></param>
        /// <param name="ref1Wrkf"></param>
        /// <param name="idProc"></param>
        /// <param name="amidWrkf"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        public JsonResult EjecutarProceso(decimal aidWrkf, decimal idWrkf, decimal sidWrkf, decimal ref1Wrkf, decimal idProc, decimal amidWrkf, decimal oid)
        {
             Resultado retorno = new Resultado();
            retorno.data = Json(new { ObjectType = "DOCNONE" }, JsonRequestBehavior.AllowGet);
            try
            {
                string prefijoMailUsu = GlobalVars.Global.PrefijoMailUsu;
                string prefijoMailTer = GlobalVars.Global.PrefijoMailTer;
                string prefijoDocIn = GlobalVars.Global.PrefijoDocumentoEntrada;

                BL_WORKF_OBJECTS objService = new BL_WORKF_OBJECTS();
                if (!isLogout(ref retorno))
                {
                    ////validar si perfil tiene permisos
                    if (Generica.hasAccess(PerfilUsuarioActual, aidWrkf))
                    {

                        BL_WORKF_PARAMETERS paramService = new BL_WORKF_PARAMETERS();
                        WORKF_OBJECTS objeto = null;
                        bool existeFile = true;

                        // verifica si es un objeto que tiene que mostrar un documento.
                        if (oid != 0)
                        {
                            objeto = objService.ObtenerObjects(GlobalVars.Global.OWNER, oid);
                            if (objeto != null)
                            {
                                //cambiarlo por prefijo. nuevo cambpo objeto.WRKF_OPATH
                                // verificar si existe el arhivo fisico de la platinlla
                                //(objeto.TipoObjeto.WRKF_OPREF != prefijoMailUsu ||objeto.TipoObjeto.WRKF_OPREF != prefijoMailTer)
                                if (objeto.TipoObjeto != null && objeto.TipoObjeto.WRKF_OPREF != prefijoDocIn)
                                {
                                    existeFile = System.IO.File.Exists(string.Format("{0}{1}", GlobalVars.Global.RutaPlantillaLicencia, objeto.WRKF_OPATH));
                                    if (!existeFile)
                                        existeFile = System.IO.File.Exists(string.Format("{0}{1}", GlobalVars.Global.RutaPlantillaLicencia, objeto.WRKF_OPATH_JURIDICO));
                                }
                            }
                        }
                        if (existeFile)
                        {
                            retorno = ejecutarAccion(aidWrkf, idWrkf, sidWrkf, ref1Wrkf, idProc, amidWrkf);
                            retorno.data = Json(new { ObjectType = "DOCIN" }, JsonRequestBehavior.AllowGet);
                            if (objeto != null)
                            {
                                if (objeto.TipoObjeto.WRKF_OPREF == prefijoDocIn)
                                    retorno.data = Json(new { ObjectType = "DOCIN" }, JsonRequestBehavior.AllowGet);
                                else
                                    retorno.data = Json(new { ObjectType = "DOCOUT" }, JsonRequestBehavior.AllowGet);
                            }
                            if (retorno.result == 1)
                            {
                                if (objeto != null)
                                {
                                    //solo si ES CORREO - 
                                    if (objeto.TipoObjeto != null && (objeto.TipoObjeto.WRKF_OPREF == prefijoMailUsu || objeto.TipoObjeto.WRKF_OPREF == prefijoMailTer))
                                    {
                                        var asunto = (objeto.WRKF_OSUBJECT != null ? objeto.WRKF_OSUBJECT : "-Asunto Prueba");
                                        var util = new SGRDA.Utility.Util();
                                        List<string> correos = null;

                                        //si el correo esta dirigido solamente a el usuario
                                        if (objeto.TipoObjeto.WRKF_OPREF == prefijoMailUsu)
                                        {
                                            correos = new BLCorreo().CorreoNotificarUsuLic(GlobalVars.Global.OWNER, ref1Wrkf);
                                        }
                                        //si el correo esta dirigido solamente a los agentes.
                                        if (objeto.TipoObjeto.WRKF_OPREF == prefijoMailTer)
                                        {
                                            correos = new BLCorreo().CorreoNotificarAgente(GlobalVars.Global.OWNER, amidWrkf);
                                        }
                                        if (correos != null && correos.Count > 0)
                                        {
                                            var contenido = getBodyMail(objeto.WRKF_OINTID, objeto.WRKF_OPATH, ref1Wrkf);
                                            if (contenido != "-1")
                                            {
                                                var resutl = util.EnviarCorreo(correos, asunto, contenido, true);
                                                retorno.data = Json(new { ObjectType = "MAIL" }, JsonRequestBehavior.AllowGet);
                                                if (!resutl)
                                                {
                                                    new BL_WORKF_ACTIONS().RollBackStateLic(retorno.Code);
                                                    retorno.result = 0;
                                                    retorno.message = "Error al enviar correo electrónico."; //Constantes.MensajeEjecutarAccion.MSG_ERROR_ENVIO_MAIL
                                                }
                                            }
                                            else
                                            {
                                                new BL_WORKF_ACTIONS().RollBackStateLic(retorno.Code);
                                                retorno.result = 0;
                                                retorno.message = "Error al intentar obtener la plantilla. Comuníquese con el administrador.";//Constantes.MensajeEjecutarAccion.MSG_ERROR_OBTENER_PLANTILLA
                                            }
                                        }
                                        else
                                        {
                                            new BL_WORKF_ACTIONS().RollBackStateLic(retorno.Code);
                                            retorno.result = 0;
                                            retorno.message = "No se encontró ningun correo de destino para la notificación."; //Constantes.MensajeEjecutarAccion.MSG_ERROR_SIN_CORREOS
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            retorno.result = 0;
                            retorno.message = "No existe la plantilla para la acción a ejecutar.";//Constantes.MensajeEjecutarAccion.MSG_ERROR_SIN_PLANTILLA
                        }
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "El usuario con el perfil actual no puede ejecutar la accion. No tiene asignado el permiso respectivo.";//Constantes.MensajeEjecutarAccion.MSG_ERROR_ACCESO_ACCION
                    }

                }
            }
            catch (Exception ex)
            {
                if (retorno.Code > 0)
                {
                    new BL_WORKF_ACTIONS().RollBackStateLic(retorno.Code);
                }
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "EjecutarProceso", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }


        private Resultado ejecutarAccion(decimal aidWrkf, decimal idWrkf, decimal sidWrkf, decimal ref1Wrkf, decimal idProc, decimal amidWrkf)
        {
            Resultado retorno = new Resultado();
            BL_WORKF_TRACES traServ = new BL_WORKF_TRACES();
            WORKF_TRACES entidad = new WORKF_TRACES();
            entidad.OWNER = GlobalVars.Global.OWNER;
            entidad.WRKF_AID = aidWrkf;
            entidad.WRKF_ID = idWrkf;
            entidad.WRKF_SID = sidWrkf;
            entidad.WRKF_REF1 = ref1Wrkf;
            entidad.PROC_MOD = Constantes.Modulo.LICENCIAMIENTO;
            entidad.PROC_ID = idProc;
            entidad.LOG_USER_CREAT = UsuarioActual;
            entidad.WRKF_AMID = amidWrkf;

            decimal idTrace;

            var result = traServ.InsertarTraceLic(entidad, amidWrkf, out idTrace);

            if (result != -999 && idTrace != 0)
            {
                retorno.result = 1;
                retorno.valor = result.ToString();
                retorno.Code = Convert.ToInt32(idTrace);
                retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;

                if (result == -998)
                {
                    retorno.result = 3;
                    retorno.message = "Ya ha sido ejecutado la accion.";//Constantes.MensajeEjecutarAccion.MSG_WARNING_ACCION_EJECUTADA
                    retorno.valor = sidWrkf.ToString();
                }
                if (result == -997)
                {
                    retorno.result = 3;
                    retorno.message = "No se ha configurado parametros para el cambio de estado.";//Constantes.MensajeEjecutarAccion.MSG_WARNING_SIN_PARAMETROS
                    retorno.valor = sidWrkf.ToString();
                }
            }
            else
            {
                retorno.result = 0;
                retorno.message = "No ha cumplido los pre requisitos para cambiar el estado";//Constantes.MensajeEjecutarAccion.MSG_ERROR_SIN_REQUISITOS
            }



            return retorno;

        }

        public ActionResult Obtener(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    WORKF_ACTIONS tipo = new WORKF_ACTIONS();
                    tipo = new BL_WORKF_ACTIONS().Obtener(GlobalVars.Global.OWNER, id);
                    if (tipo != null)
                    {
                        tipo.AgenteAccion = new BLAgenteAccion().AgenteXAccion(GlobalVars.Global.OWNER, id, GlobalVars.Global.PREFIJO);
                    }

                    if (tipo.AgenteAccion != null)
                    {
                        Agente = new List<DTOAgente>();
                        if (tipo.AgenteAccion != null)
                        {
                            tipo.AgenteAccion.ForEach(s =>
                            {
                                Agente.Add(new DTOAgente
                                {
                                    Id = s.WRKF_AGAC_ID,
                                    Codigo = s.WRKF_AID,
                                    IdAgente = s.WRKF_AGID,
                                    NombreAgente = s.Nombre,
                                    FechaCrea = s.LOG_DATE_CREAT,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    EnBD = true,
                                    Activo = s.ENDS.HasValue ? false : true
                                });
                            });

                            AgenteTmp = Agente;

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

        [HttpPost]
        public JsonResult AddAgente(DTOAgente entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    int registroNuevo = 0;
                    int registroModificar = 0;
                    Agente = AgenteTmp;
                    if (Agente != null)
                    {
                        registroNuevo = Agente.Where(p => p.IdAgente == entidad.IdAgente && entidad.Id == 0).Count();
                        registroModificar = Agente.Where(p => p.IdAgente == entidad.IdAgente && p.Id == entidad.Id).Count();
                    }
                    if ((entidad.Id == 0 && registroNuevo == 0) || (entidad.Id != 0 && registroModificar > 0))
                    {
                        if (Agente == null) Agente = new List<DTOAgente>();
                        if (Convert.ToInt32(entidad.Id) <= 0)
                        {
                            decimal nuevoId = 1;
                            if (Agente.Count > 0) nuevoId = Agente.Max(x => x.Id) + 1;
                            entidad.Id = nuevoId;
                            entidad.Activo = true;
                            entidad.EnBD = false;
                            entidad.UsuarioCrea = UsuarioActual;
                            entidad.FechaCrea = DateTime.Now;
                            Agente.Add(entidad);
                        }
                        else
                        {
                            var item = Agente.Where(x => x.Id == entidad.Id).FirstOrDefault();
                            entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                            entidad.Activo = item.Activo;
                            entidad.UsuarioCrea = item.UsuarioCrea;
                            entidad.FechaCrea = item.FechaCrea;
                            if (entidad.EnBD)
                            {
                                entidad.UsuarioModifica = UsuarioActual;
                                entidad.FechaModifica = DateTime.Now;
                            }
                            Agente.Remove(item);
                            Agente.Add(entidad);
                        }
                        AgenteTmp = Agente;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                    else
                    {
                        retorno.result = 2;
                        retorno.message = "El Agente ya existe.";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DellAddAgente(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                Agente = AgenteTmp;
                if (Agente != null)
                {
                    var objDel = Agente.Where(x => x.Id == Id).FirstOrDefault();
                    if (objDel != null)
                    {
                        if (objDel.EnBD)
                        {
                            bool blActivo = !objDel.Activo;

                            if (AgenteTmpUPDEstado == null) AgenteTmpUPDEstado = new List<DTOAgente>();
                            if (AgenteTmpDelBD == null) AgenteTmpDelBD = new List<DTOAgente>();

                            var itemUpd = AgenteTmpUPDEstado.Where(x => x.Id == Id).FirstOrDefault();
                            var itemDel = AgenteTmpDelBD.Where(x => x.Id == Id).FirstOrDefault();

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
                            Agente.Remove(objDel);
                            Agente.Add(objDel);
                        }
                        else
                        {
                            Agente.Remove(objDel);
                        }

                        AgenteTmp = Agente;
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

        public JsonResult ListarAgente()
        {
            Agente = AgenteTmp;
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Agente</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (Agente != null)
                    {
                        foreach (var item in Agente.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.NombreAgente);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td> <a href=# onclick='updAddAgente({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                            shtml.AppendFormat("<a href=# onclick='delAddAgente({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Red Social" : "Activar Agente");
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

        public ActionResult ObtenerAgente(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    WORKF_ACTIONS tipo = new WORKF_ACTIONS();
                    tipo = new BL_WORKF_ACTIONS().Obtener(GlobalVars.Global.OWNER, id);
                    if (tipo != null)
                    {
                        tipo.AgenteAccion = new BLAgenteAccion().AgenteXAccion(GlobalVars.Global.OWNER, id, GlobalVars.Global.PREFIJO);
                    }

                    if (tipo.AgenteAccion.Count > 0)
                    {
                        Agente = new List<DTOAgente>();
                        if (tipo.AgenteAccion != null)
                        {
                            tipo.AgenteAccion.ForEach(s =>
                            {
                                Agente.Add(new DTOAgente
                                {
                                    Id = s.WRKF_AGAC_ID,
                                    Codigo = s.WRKF_AID,
                                    IdAgente = s.WRKF_AGID,
                                    NombreAgente = s.Nombre,
                                    FechaCrea = s.LOG_DATE_CREAT,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    EnBD = true,
                                    Activo = s.ENDS.HasValue ? false : true
                                });
                            });

                            AgenteTmp = Agente;
                            retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                            retorno.message = "OK";
                            retorno.result = 1;
                        }
                    }
                    else
                    {
                        retorno.data = Json(tipo, JsonRequestBehavior.AllowGet);
                        retorno.message = "No se encontró agentes para esta acción.";
                        retorno.result = 2;
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

        public JsonResult VerDetalle()
        {
            Agente = AgenteTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Agente</th>");
                    shtml.Append("<th  class='k-header'></th></tr></thead>");

                    if (Agente.Count > 0)
                    {
                        foreach (var item in Agente.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td >{0}</td>", item.NombreAgente);
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        }
                        shtml.Append("</table>");
                        retorno.message = shtml.ToString();
                        retorno.result = 1;
                    }
                    else
                    {
                        shtml.Append("</table>");
                        retorno.message = "No se encontró agentes para esta acción.";
                        retorno.result = 2;
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

        private List<BEAgenteAccion> obtenerAgente()
        {
            List<BEAgenteAccion> datos = new List<BEAgenteAccion>();

            if (AgenteTmp != null)
            {
                AgenteTmp.ForEach(x =>
                {
                    datos.Add(new BEAgenteAccion
                    {
                        WRKF_AGAC_ID = x.EnBD ? x.Id : decimal.Zero,
                        WRKF_AID = x.Codigo, //x.EnBD ? x.Id : decimal.Zero,
                        WRKF_AGID = x.IdAgente,
                        Nombre = x.NombreAgente,
                        LOG_USER_CREAT = UsuarioActual
                    });
                });
            }
            return datos;
        }

        public JsonResult ObtieneAgenteTmp(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    string owner = GlobalVars.Global.OWNER;
                    var entidad = AgenteTmp.Where(x => x.Id == Id).FirstOrDefault();
                    retorno.data = Json(entidad, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtieneAgenteTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private static void validacion(out bool exito, out string msg_validacion, WORKF_ACTIONS entidad)
        {
            exito = true;
            msg_validacion = string.Empty;

            if (exito && string.IsNullOrEmpty(entidad.WRKF_ANAME))
            {
                msg_validacion = "Ingrese el nombre";
                exito = false;
            }

            if (exito && string.IsNullOrEmpty(entidad.WRKF_ALABEL))
            {
                msg_validacion = "Ingrese la etiqueta";
                exito = false;
            }
        }

        [HttpPost]
        public JsonResult Insertar(WORKF_ACTIONS entidad)
        {
            bool exito = true;
            string msg_validacion = "";
            Resultado retorno = new Resultado();
            try
            {
                DTOAgente agente = new DTOAgente();

                if (!isLogout(ref retorno))
                {
                    validacion(out exito, out msg_validacion, entidad);
                    if (exito)
                    {
                        entidad.OWNER = GlobalVars.Global.OWNER;
                        entidad.LOG_USER_CREAT = UsuarioActual;
                        entidad.AgenteAccion = obtenerAgente();

                        if (entidad.WRKF_AID == 0)
                        {
                            entidad.LOG_USER_CREAT = UsuarioActual;
                            var datos = new BL_WORKF_ACTIONS().Insertar(entidad);
                        }
                        else
                        {
                            //entidad.WRKF_AID = agente.Codigo;
                            entidad.LOG_USER_UPDATE = UsuarioActual;

                            //1.setting Acciones eliminar
                            List<BEAgenteAccion> listaAgenteDel = null;
                            if (AgenteTmpDelBD != null)
                            {
                                listaAgenteDel = new List<BEAgenteAccion>();
                                AgenteTmpDelBD.ForEach(x => { listaAgenteDel.Add(new BEAgenteAccion { WRKF_AGAC_ID = x.Id }); });
                            }
                            //2. setting Acciones activar
                            List<BEAgenteAccion> listaAgenteUpdEst = null;
                            if (AgenteTmpUPDEstado != null)
                            {
                                listaAgenteUpdEst = new List<BEAgenteAccion>();
                                AgenteTmpUPDEstado.ForEach(x => { listaAgenteUpdEst.Add(new BEAgenteAccion { WRKF_AGAC_ID = x.Id }); });
                            }
                            var datos = new BL_WORKF_ACTIONS().Actualizar(entidad, listaAgenteDel, listaAgenteUpdEst);

                        }
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                        retorno.result = 1;
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


        ///// <summary>
        ///// VALIDACION SI LA ACCION A EJECUTARSE ESTA CONFIGURADA
        ///// PARA QUE EL USUARIO LOGEADO PUEDA REALIZAR LA ACCION
        ///// DBS - 20150320
        ///// </summary>
        ///// <param name="idRol"></param>
        ///// <param name="idAcction"></param>
        ///// <returns></returns>
        //public static bool hasAccess(decimal idRol, decimal idAction)
        //{
        //    BL_WORKF_AGENTS servicio = new BL_WORKF_AGENTS();
        //    return servicio.TieneRol(GlobalVars.Global.OWNER, idRol, idAction);

        //}
        /// <summary>
        /// Si retorna -1 hubo error al leer la plantilla
        /// </summary>
        /// <param name="idInterno"></param>
        /// <param name="nombrePlantilla"></param>
        /// <param name="idLic"></param>
        /// <returns></returns>
        string getBodyMail(string idInterno, string nombrePlantilla, decimal idLic)
        {

            string body = "";
            var pathFile = string.Format("{0}{1}", GlobalVars.Global.RutaPlantillaCorreo, nombrePlantilla);

            PlantillaCorreo plantilla = new PlantillaCorreo();
            switch (idInterno)
            {

                case "999":
                    body = plantilla.crearContenidoAceptacion(pathFile, idLic);
                    break;
                default:
                    break;
            }


            return body;



        }
    }
}

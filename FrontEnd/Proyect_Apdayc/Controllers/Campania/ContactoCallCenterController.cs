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

namespace Proyect_Apdayc.Controllers.Campania
{
    public class ContactoCallCenterController : Base
    {
        // GET: /ContactoCallCenter/
        List<DTODocumento> documentos = new List<DTODocumento>();
        private const string K_SESSION_DOCUMENTO = "___DTODocumento";
        private const string K_SESSION_DOCUMENTO_DEL = "___DTODocumentoDEL";
        private const string K_SESSION_DOCUMENTO_ACT = "___DTODocumentoACT";
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

        public const string nomAplicacion = "SGRDA";
        private const string K_SESSION_LOTECLIENTE = "___DTOLoteCliente";
        private const string K_SESSION_LOTECLIENTE_DEL = "___DTOLoteClienteDEL";
        private const string K_SESSION_LOTECLIENTE_ACT = "___DTOLoteClienteACT";
        List<DTOCampaniaLoteCliente> lotecliente = new List<DTOCampaniaLoteCliente>();
        private List<DTOCampaniaLoteCliente> LoteClienteTmpUPDEstado
        {
            get
            {
                return (List<DTOCampaniaLoteCliente>)Session[K_SESSION_LOTECLIENTE_ACT];
            }
            set
            {
                Session[K_SESSION_LOTECLIENTE_ACT] = value;
            }
        }
        private List<DTOCampaniaLoteCliente> LoteClienteTmpDelBD
        {
            get
            {
                return (List<DTOCampaniaLoteCliente>)Session[K_SESSION_LOTECLIENTE_DEL];
            }
            set
            {
                Session[K_SESSION_LOTECLIENTE_DEL] = value;
            }
        }
        public List<DTOCampaniaLoteCliente> LoteClienteTmp
        {
            get
            {
                return (List<DTOCampaniaLoteCliente>)Session[K_SESSION_LOTECLIENTE];
            }
            set
            {
                Session[K_SESSION_LOTECLIENTE] = value;
            }
        }

        private const string K_SESSION_OBSERVACION_CON = "___DTOObservacion_CON";
        private const string K_SESSION_OBSERVACION_CON_LIST = "___DTOObservacion_CON_LIST";
        private const string K_SESSION_OBSERVACION_DEL_CON = "___DTOObservacionDEL_CON";
        private const string K_SESSION_OBSERVACION_ACT_CON = "___DTOObservacionACT_CON";
        DTOObservacion Observacion = new DTOObservacion();
        List<DTOObservacion> observacionList = new List<DTOObservacion>();
        //private DTOObservacion ObservacionesTmpUPDEstado
        //{
        //    get
        //    {
        //        return (DTOObservacion)Session[K_SESSION_OBSERVACION_ACT_CON];
        //    }
        //    set
        //    {
        //        Session[K_SESSION_OBSERVACION_ACT_CON] = value;
        //    }
        //}
        //private DTOObservacion ObservacionesTmpDelBD
        //{
        //    get
        //    {
        //        return (DTOObservacion)Session[K_SESSION_OBSERVACION_DEL_CON];
        //    }
        //    set
        //    {
        //        Session[K_SESSION_OBSERVACION_DEL_CON] = value;
        //    }
        //}

        public DTOObservacion ObservacionesTmp
        {
            get
            {
                return (DTOObservacion)Session[K_SESSION_OBSERVACION_CON];
            }
            set
            {
                Session[K_SESSION_OBSERVACION_CON] = value;
            }
        }
        public List<DTOObservacion> ObservacionesListaTmp
        {
            get
            {
                return (List<DTOObservacion>)Session[K_SESSION_OBSERVACION_CON_LIST];
            }
            set
            {
                Session[K_SESSION_OBSERVACION_CON_LIST] = value;
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
            Session.Remove(K_SESSION_LOTECLIENTE);
            Session.Remove(K_SESSION_LOTECLIENTE_DEL);
            Session.Remove(K_SESSION_LOTECLIENTE_ACT);
            return View();
        }

        public JsonResult ListarLoteCliente(decimal idLote)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var listaLoteTrabajo = new BLCampaniaContactollamada().Obtiene(GlobalVars.Global.OWNER, idLote, Constantes.ENTIDAD.OTROS);

                    if (listaLoteTrabajo.LoteCliente != null)
                    {
                        lotecliente = new List<DTOCampaniaLoteCliente>();
                        listaLoteTrabajo.LoteCliente.ForEach(s =>
                        {
                            lotecliente.Add(new DTOCampaniaLoteCliente
                                {
                                    Id = s.CONC_MID,
                                    IdClienteLote = s.CONC_SID,
                                    IdCliente = s.BPS_ID,
                                    Nombre = s.BPS_NAME,
                                    IdObs = s.OBS_ID,
                                    TipoObservacion = s.OBS_DESC,
                                    IdTipoObservacion = s.OBS_TYPE,
                                    Observacion = s.OBS_VALUE,
                                    ValorExpectativa = s.CONC_MEXPEC,
                                    ValorReal = s.CONC_MREAL,
                                    FechaCrea = s.LOG_DATE_CREATE,
                                    UsuarioCrea = s.LOG_USER_CREAT
                                });
                        });
                        LoteClienteTmp = lotecliente;
                    }

                    if (listaLoteTrabajo.Observaciones != null)
                    {
                        Observacion = new DTOObservacion();
                        listaLoteTrabajo.Observaciones.ForEach(s =>
                            {
                                observacionList.Add(new DTOObservacion
                                    {
                                        Id = s.OBS_ID,
                                        TipoObservacion = s.OBS_TYPE.ToString(),
                                        Entidad = s.ENT_ID,
                                        Observacion = s.OBS_VALUE,
                                        UsuarioCrea = UsuarioActual,
                                        UsuarioModifica = UsuarioActual
                                    });
                            });
                        ObservacionesListaTmp = observacionList;
                    }

                    if (listaLoteTrabajo.Documentos != null)
                    {
                        documentos = new List<DTODocumento>();
                        listaLoteTrabajo.Documentos.ForEach(s =>
                        {
                            var newDTODocumento = new DTODocumento();
                            newDTODocumento.Id = s.DOC_ID;
                            newDTODocumento.IdContactoLlamada = s.CONC_MID;
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

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(listaLoteTrabajo, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarLoteCliente", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarDocumento(decimal IdContactoLlamada)
        {
            documentos = DocumentosTmp.Where(x => x.IdContactoLlamada == IdContactoLlamada).ToList();

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
                            var pathWeb = System.Web.Configuration.WebConfigurationManager.AppSettings["RutaWebImgContactoCallCenter"];
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarDocumento", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarCampaniaLoteCliente()
        {
            var lista = LoteClienteTmp;

            Resultado retorno = new Resultado();
            try
            {
                decimal i = 0;
                string ruta = string.Empty;
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    var clase = "'ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'";
                    shtml.Append("<table class='k-grid k-widget' border=0 width='100%;' id='tblAsignarCampania'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class=" + clase + " style='width:40px; text-align:center;'>Id</th>");
                    shtml.Append("<th class=" + clase + " style='width:40px; text-align:center; display:none;'>IdCliente</th>");
                    shtml.Append("<th class=" + clase + " style='width:100px; text-align:center;'>Cliente</th>");
                    shtml.Append("<th class=" + clase + " style='width:100px; text-align:center;'>Tipo Observación</th>");
                    shtml.Append("<th class=" + clase + " style='width:40px; text-align:center; display:none;'>IdObs</th>");
                    shtml.Append("<th class=" + clase + " style='width:100px; text-align:center;'>Observación</th>");
                    shtml.Append("<th class=" + clase + " style='width:8px;'></th>");

                    shtml.Append("<th class=" + clase + " style='width:50px; text-align:center;'>Valor Expect.</th>");
                    shtml.Append("<th class=" + clase + " style='width:50px; text-align:center;'>Valor Real</th>");
                    shtml.Append("<th class=" + clase + " style='width:80px; text-align:center;'>Usu. Crea</th>");
                    shtml.Append("<th class=" + clase + " style='width:80px; text-align:center;'>Fecha Crea</th>");
                    shtml.Append("<th class=" + clase + " style='width:30px; text-align:center;'>Documento</th>");
                    shtml.Append("<th class=" + clase + " style='width:20px; text-align:center;'></th>");
                    shtml.Append("<th class=" + clase + " style='width:30px;'></th></tr></thead>");

                    if (lista.Count > 0)
                    {
                        foreach (var item in lista.OrderBy(x => x.Id))
                        {
                            if (item.Id == 0)
                                i = item.Id + 1;
                            else
                                i = item.Id;

                            var documento = DocumentosTmp.Where(s => s.IdContactoLlamada == item.Id).ToList();
                            //var documento = new BLDocumentoContactoLlamada().ObtenerDocumento(GlobalVars.Global.OWNER, item.Id);

                            shtml.Append("<tr style='background-color:white'>");
                            shtml.AppendFormat("<td style='cursor:pointer; text-align:center;'>{0}</td>", i);
                            shtml.AppendFormat("<td style='cursor:pointer; text-align:center; display:none;'><input type='text' id='txtBpsid_{0}' value={1} style='width:70px; text-align:center;' /></td>", i, item.IdCliente);
                            shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.Nombre);
                            shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.TipoObservacion);

                            shtml.AppendFormat("<td style='cursor:pointer; text-align:center; display:none;'><input type='text' id='txtObsid_{0}' value={1} style='width:70px; text-align:center;' /></td>", i, item.IdObs == null ? 0 : item.IdObs);
                            shtml.AppendFormat("<td ><label id='lbObservacion_{0}'>{1}</label>  </td>", item.IdCliente, item.Observacion);

                            shtml.AppendFormat("<td style='text-align:right;'> <a href=# onclick='return EditarObservacion({0},{1});'> <img src='../Images/botones/edit24.png' title='Editar' border=0></a></td>", item.IdObs == null ? 0 : item.IdObs, item.Id);
                            shtml.AppendFormat("<td style='cursor:pointer; text-align:center;'><input type='text' id='txtValExpectativa_{0}' value={1} style='width:70px; text-align:center;' /></td>", i, item.ValorExpectativa == null ? 0 : item.ValorExpectativa);
                            shtml.AppendFormat("<td style='cursor:pointer; text-align:center;'><input type='text' id='txtValReal_{0}' value={1} style='width:70px; text-align:center;' /></td>", i, item.ValorReal == null ? 0 : item.ValorReal);
                            shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td style='text-align:center; cursor:pointer;'> <a href=# onclick='addDoc({0});'> <label id='lblAgregar' style='cursor:pointer;' class='addDocumento'>Agregar</label></td>", item.Id);

                            if (documento.Count() > 0)
                                shtml.AppendFormat("<td style='text-align:center;'> <a href='#' onclick='verDocumentos({0});' ><img src='../Images/iconos/file.png' border=0 title='Ver Documento'></a></td>", item.Id);
                            else
                                shtml.AppendFormat("<td style='text-align:center;'> <img src='../Images/iconos/file.png' border=0 style='display:none;'></td>", string.Empty);

                            shtml.AppendFormat("<td style='text-align:center;'> <a href=# onclick='return Grabar({0},{1});' ><img src='../Images/botones/2save.png' border=0 style='width: 16px' title='Grabar' id='btnGrabar_{0}'></a></td>", i, item.Id);
                            shtml.Append("</tr>");
                        }
                    }
                    else
                    {
                        shtml.Append("<tr id='trMensajeLoteCliente' style='background-color:white'><td colspan=14><b><center>No se encontraron resultados de búsqueda.</center></b></td></tr>");
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarCampaniaLoteCliente", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insertar(BECampaniaContactollamada en)
        {
            Resultado retorno = new Resultado();
            try
            {
                BECampaniaContactollamada obj = new BECampaniaContactollamada();
                obj.OWNER = GlobalVars.Global.OWNER;
                obj.CONC_MID = en.CONC_MID;
                obj.CONC_SID = en.CONC_SID;
                obj.CONC_CID = en.CONC_CID;
                obj.OBS_ID = en.OBS_ID;
                obj.BPS_ID = en.BPS_ID;
                obj.CONC_MEXPEC = en.CONC_MEXPEC;
                obj.CONC_MREAL = en.CONC_MREAL;
                obj.LOG_USER_CREAT = UsuarioActual;
                obj.LOG_USER_UPDAT = UsuarioActual;
                obj.Documentos = obtenerDocumentos();
                obj.Observacion = obtenerObservacion(obj.OBS_ID);

                if (obj.CONC_MID == 0)
                {
                    var datos = new BLCampaniaContactollamada().Insertar(obj);

                    if (datos != 0)
                    {
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GRABAR;
                        retorno.result = 0;
                    }
                }
                else
                {
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

                    var datos = new BLCampaniaContactollamada().Actualizar(obj, listaDocDel, listaDocUpdEst);

                    if (datos != 0)
                    {
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GRABAR;
                        retorno.result = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar", ex);
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddDocumento", ex);
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
                        CONC_MID = x.IdContactoLlamada,
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ActualizarNombreDocTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddObservacion(decimal IdTipoObservacion, string Observacion, decimal IdContactoLlamada, decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    //Id id de la observación
                    LoteClienteTmp.Where(w => w.Id == IdContactoLlamada).ToList().ForEach(i => i.IdTipoObservacion = IdTipoObservacion);
                    LoteClienteTmp.Where(w => w.Id == IdContactoLlamada).ToList().ForEach(i => i.IdObs = Id);
                    var TipoObservacionDesc = new BLTipoObservacion().Obtener(GlobalVars.Global.OWNER, Convert.ToInt32(IdTipoObservacion)).OBS_DESC;
                    LoteClienteTmp.Where(w => w.Id == IdContactoLlamada).ToList().ForEach(i => i.Observacion = Observacion.ToUpper());
                    LoteClienteTmp.Where(w => w.Id == IdContactoLlamada).ToList().ForEach(i => i.TipoObservacion = TipoObservacionDesc.ToUpper());

                    ObservacionesTmp = new DTOObservacion();
                    ObservacionesTmp.Id = Id;
                    ObservacionesTmp.Observacion = Observacion.ToUpper();
                    ObservacionesTmp.TipoObservacion = IdTipoObservacion.ToString();
                    ObservacionesTmp.TipoObservacionDesc = TipoObservacionDesc.ToUpper();
                    ObservacionesTmp.UsuarioCrea = UsuarioActual;
                    ObservacionesTmp.UsuarioModifica = UsuarioActual;

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

        public JsonResult ObtieneObservacionTmp(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var observacion = LoteClienteTmp.Where(x => x.IdObs == id).FirstOrDefault();
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

        private BEObservationGral obtenerObservacion(decimal? Id)
        {
            BEObservationGral datos = new BEObservationGral();

            if (ObservacionesTmp != null)
            {
                datos.OBS_ID = ObservacionesTmp.Id;
                datos.OWNER = GlobalVars.Global.OWNER;
                datos.OBS_TYPE = Convert.ToInt32(ObservacionesTmp.TipoObservacion);
                datos.OBS_VALUE = ObservacionesTmp.Observacion;
                datos.ENT_ID = 8;
                datos.LOG_USER_CREAT = UsuarioActual;
                datos.LOG_USER_UPDATE = UsuarioActual;
                datos.OBS_USER = UsuarioActual;
            }
            else
            {
                var list = ObservacionesListaTmp.Where(x => x.Id == Id).ToList();
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        datos.OBS_ID = item.Id;
                        datos.OWNER = GlobalVars.Global.OWNER;
                        datos.OBS_TYPE = Convert.ToInt32(item.TipoObservacion);
                        datos.OBS_VALUE = item.Observacion;
                        datos.ENT_ID = 8;
                        datos.LOG_USER_CREAT = UsuarioActual;
                        datos.LOG_USER_UPDATE = UsuarioActual;
                        datos.OBS_USER = UsuarioActual;
                    }
                }
            }
            return datos;


            //if (ObservacionesTmp != null)
            //{
            //    datos.OBS_ID = ObservacionesTmp.Id;
            //    datos.OWNER = GlobalVars.Global.OWNER;
            //    datos.OBS_TYPE = Convert.ToInt32(ObservacionesTmp.TipoObservacion);
            //    datos.OBS_VALUE = ObservacionesTmp.Observacion;
            //    datos.ENT_ID = 8;
            //    datos.LOG_USER_CREAT = UsuarioActual;
            //    datos.LOG_USER_UPDATE = UsuarioActual;
            //    datos.OBS_USER = UsuarioActual;
            //}
            //else
            //{ 
            //    var listLote = LoteClienteTmp.Where(x => x.Id == Id).ToList();
            //    foreach (var item in listLote)
            //    {                    
            //        var listObs = ObservacionesListaTmp.Where(s => s.Id == item.Id).ToList();
            //        foreach (var x in listObs)
            //        {                       
            //            datos.OBS_ID = ObservacionesTmp.Id;
            //            datos.OWNER = GlobalVars.Global.OWNER;
            //            datos.OBS_TYPE = Convert.ToInt32(ObservacionesTmp.TipoObservacion);
            //            datos.OBS_VALUE = ObservacionesTmp.Observacion;
            //            datos.ENT_ID = 8;
            //            datos.LOG_USER_CREAT = UsuarioActual;
            //            datos.LOG_USER_UPDATE = UsuarioActual;
            //            datos.OBS_USER = UsuarioActual;
            //        }
            //    }
            //}
        }
    }
}

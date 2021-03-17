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
    public class CampaniaCallCenterController : Base
    {
        //
        // GET: /CampaniaCallCenter/
        private const string K_SESSION_DOCUMENTO = "___DTODocumento";
        private const string K_SESSION_DOCUMENTO_DEL = "___DTODocumentoDEL";
        private const string K_SESSION_DOCUMENTO_ACT = "___DTODocumentoACT";

        private const string K_SESSION_ASOCIADO = "___DTOAsociadoUD";
        private const string K_SESSION_ASOCIADO_DEL = "___DTOAsociadoDELUD";
        private const string K_SESSION_ASOCIADO_ACT = "___DTOAsociadoACTUD";

        private const string K_SESSION_LOTETRABAJO = "___DTOLoteTrabajoUD";
        private const string K_SESSION_LOTETRABAJO_DEL = "___DTOLoteTrabajoDELUD";
        private const string K_SESSION_LOTETRABAJO_ACT = "___DTOLoteTrabajoACTUD";

        public const string nomAplicacion = "SGRDA";
        List<DTODocumento> documentos = new List<DTODocumento>();
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

        List<DTOAsociado> asociados = new List<DTOAsociado>();
        private List<DTOAsociado> AsociadosTmpUPDEstado
        {
            get
            {
                return (List<DTOAsociado>)Session[K_SESSION_ASOCIADO_ACT];
            }
            set
            {
                Session[K_SESSION_ASOCIADO_ACT] = value;
            }
        }
        private List<DTOAsociado> AsociadosTmpDelBD
        {
            get
            {
                return (List<DTOAsociado>)Session[K_SESSION_ASOCIADO_DEL];
            }
            set
            {
                Session[K_SESSION_ASOCIADO_DEL] = value;
            }
        }
        public List<DTOAsociado> AsociadosTmp
        {
            get
            {
                return (List<DTOAsociado>)Session[K_SESSION_ASOCIADO];
            }
            set
            {
                Session[K_SESSION_ASOCIADO] = value;
            }
        }

        //consulta para asignar socios a campaña
        private const string K_SESSION_ASIGNAR_SOCIO_CAMP = "___DTOAsignar";
        private const string K_SESSION_ASIGNAR_SOCIO_CAMP_DET = "___DTOAsignarDet";
        private const string K_SESSION_ASIGNAR_SOCIO_CAMP_SUBDET = "___DTOAsignarSubDet";
        public List<DTOCampaniaAsignarSocio> listaAsignarSocioCab = new List<DTOCampaniaAsignarSocio>();
        public List<DTOCampaniaAsignarSocioDetalle> listaAsignarSocioDet = new List<DTOCampaniaAsignarSocioDetalle>();
        public List<DTOCampaniaAsignarSocioSubDetalle> listaAsignarSocioSubDet = new List<DTOCampaniaAsignarSocioSubDetalle>();
        public List<DTOCampaniaAsignarSocio> AsignarSocioCabTmp
        {
            get
            {
                return (List<DTOCampaniaAsignarSocio>)Session[K_SESSION_ASIGNAR_SOCIO_CAMP];
            }
            set
            {
                Session[K_SESSION_ASIGNAR_SOCIO_CAMP] = value;
            }
        }
        public List<DTOCampaniaAsignarSocioDetalle> AsignarSocioDetTmp
        {
            get
            {
                return (List<DTOCampaniaAsignarSocioDetalle>)Session[K_SESSION_ASIGNAR_SOCIO_CAMP_DET];
            }
            set
            {
                Session[K_SESSION_ASIGNAR_SOCIO_CAMP_DET] = value;
            }
        }
        public List<DTOCampaniaAsignarSocioSubDetalle> AsignarSocioSubDetTmp
        {
            get
            {
                return (List<DTOCampaniaAsignarSocioSubDetalle>)Session[K_SESSION_ASIGNAR_SOCIO_CAMP_SUBDET];
            }
            set
            {
                Session[K_SESSION_ASIGNAR_SOCIO_CAMP_SUBDET] = value;
            }
        }

        List<DTOLoteTrabajo> lotetrabajo = new List<DTOLoteTrabajo>();
        private List<DTOLoteTrabajo> LoteTrabajoTmpUPDEstado
        {
            get
            {
                return (List<DTOLoteTrabajo>)Session[K_SESSION_LOTETRABAJO_ACT];
            }
            set
            {
                Session[K_SESSION_LOTETRABAJO_ACT] = value;
            }
        }
        private List<DTOLoteTrabajo> LoteTrabajoTmpDelBD
        {
            get
            {
                return (List<DTOLoteTrabajo>)Session[K_SESSION_LOTETRABAJO_DEL];
            }
            set
            {
                Session[K_SESSION_LOTETRABAJO_DEL] = value;
            }
        }
        public List<DTOLoteTrabajo> LoteTrabajoTmp
        {
            get
            {
                return (List<DTOLoteTrabajo>)Session[K_SESSION_LOTETRABAJO];
            }
            set
            {
                Session[K_SESSION_LOTETRABAJO] = value;
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
            Session.Remove(K_SESSION_DOCUMENTO);
            Session.Remove(K_SESSION_DOCUMENTO_ACT);
            Session.Remove(K_SESSION_DOCUMENTO_DEL);
            Session.Remove(K_SESSION_ASOCIADO);
            Session.Remove(K_SESSION_ASOCIADO_ACT);
            Session.Remove(K_SESSION_ASOCIADO_DEL);
            Session.Remove(K_SESSION_ASIGNAR_SOCIO_CAMP);
            Session.Remove(K_SESSION_ASIGNAR_SOCIO_CAMP_DET);
            Session.Remove(K_SESSION_LOTETRABAJO);
            Session.Remove(K_SESSION_LOTETRABAJO_DEL);
            Session.Remove(K_SESSION_LOTETRABAJO_ACT);
            return View();
        }

        public JsonResult ListaPageCampaniaCallCenter(int skip, int take, int page, int pageSize, decimal contacto, decimal tipoCamp, string estadoCamp, string nombre, decimal perfilCliente, string fechaIni, string fechaFin, int st)
        {
            var lista = listaCampaniaCall(GlobalVars.Global.OWNER, contacto, tipoCamp, estadoCamp, nombre, perfilCliente, Convert.ToDateTime(fechaIni), Convert.ToDateTime(fechaFin), st, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
                return Json(new BECampaniaCallCenter { ListaCampaniaCall = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            else
                return Json(new BECampaniaCallCenter { ListaCampaniaCall = lista, TotalVirtual = Convert.ToInt32(tot[0].ToString()) }, JsonRequestBehavior.AllowGet);
        }

        public List<BECampaniaCallCenter> listaCampaniaCall(string owner, decimal contacto, decimal tipoCamp, string estadoCamp, string nombre, decimal perfilCliente, DateTime fechaIni, DateTime fechaFin, int st, int pagina, int cantRegxPag)
        {
            return new BLCampaniaCallCenter().ListarCampaniaCallCenter(owner, contacto, tipoCamp, estadoCamp, nombre, perfilCliente, fechaIni, fechaFin, st, pagina, cantRegxPag);
        }

        public JsonResult ObtieneDatos(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var data = new BLCampaniaCallCenter().ObtenerDatos(GlobalVars.Global.OWNER, Id);

                    if (data.Documentos != null)
                    {
                        documentos = new List<DTODocumento>();
                        data.Documentos.ForEach(s =>
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

                    if (data.Asociados != null)
                    {
                        asociados = new List<DTOAsociado>();
                        if (data.Asociados != null)
                        {
                            foreach (var s in data.Asociados)
                            {
                                var obj = new DTOAsociado();
                                obj.Id = s.SEQUENCE;
                                obj.RolTipo = Convert.ToInt32(s.ROL_ID);
                                obj.RolTipoDesc = s.ROL_DESC;
                                obj.CodigoAsociado = s.BPS_ID;
                                obj.EnBD = true;
                                obj.UsuarioCrea = s.LOG_USER_CREAT;
                                obj.FechaCrea = s.LOG_DATE_CREAT;
                                obj.UsuarioModifica = s.LOG_USER_UPDATE;
                                obj.FechaModifica = s.LOG_DATE_UPDATE;
                                obj.Activo = s.ENDS.HasValue ? false : true;

                                var usu = new BLSocioNegocio().ObtenerDatos(s.BPS_ID, GlobalVars.Global.OWNER);
                                if (usu != null)
                                {
                                    var nombres = "";
                                    if (Convert.ToString(usu.ENT_TYPE) == "N")
                                    {
                                        nombres = String.Format("{0} {1} {2}", usu.BPS_FIRST_NAME, usu.BPS_FATH_SURNAME, usu.BPS_MOTH_SURNAME);
                                    }
                                    else
                                    {
                                        nombres = String.Format("{0}", usu.BPS_NAME);
                                    }
                                    obj.NombreAsociado = nombres;
                                    obj.NroDocAsociado = usu.TAX_ID;
                                }

                                asociados.Add(obj);
                            }
                            AsociadosTmp = asociados;
                        }
                    }
                    if (data.LoteTrabajo != null)
                    {
                        lotetrabajo = new List<DTOLoteTrabajo>();
                        if (data.LoteTrabajo != null)
                        {
                            foreach (var s in data.LoteTrabajo)
                            {
                                var obj = new DTOLoteTrabajo();
                                obj.Id = s.CONC_SID;
                                obj.IdAgente = s.BPS_ID;
                                obj.NombreAgente = s.BPS_NAME;
                                obj.FechaIni = s.CONC_SDATEINI.ToShortDateString();
                                obj.FechaFin = s.CONC_SDATEND.ToShortDateString();
                                obj.EnBD = true;
                                obj.UsuarioCrea = s.LOG_USER_CREAT;
                                obj.FechaCrea = s.LOG_DATE_CREAT;
                                obj.UsuarioModifica = s.LOG_USER_UPDAT;
                                obj.FechaModifica = s.LOG_DATE_UPDATE;
                                obj.Activo = s.ENDS.HasValue ? false : true;
                                lotetrabajo.Add(obj);
                            }
                            LoteTrabajoTmp = lotetrabajo;
                        }
                    }
                    retorno.data = Json(data, JsonRequestBehavior.AllowGet);
                    retorno.message = "Campaña encontrada";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener datos", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Insertar(BECampaniaCallCenter en)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BECampaniaCallCenter obj = new BECampaniaCallCenter();
                    obj.OWNER = GlobalVars.Global.OWNER;
                    obj.CONC_CID = en.CONC_CID;
                    obj.CONC_CNAME = en.CONC_CNAME;
                    obj.CONC_ID = en.CONC_ID;
                    obj.CONC_CDESC = en.CONC_CDESC;
                    obj.CONC_CTID = en.CONC_CTID;
                    obj.ENT_ID = en.ENT_ID;
                    obj.CONC_CSTATUS = en.CONC_CSTATUS;
                    obj.CONC_CDINI = en.CONC_CDINI;
                    obj.CONC_CDEND = en.CONC_CDEND;
                    obj.LOG_USER_CREAT = UsuarioActual;
                    obj.LOG_USER_UPDATE = UsuarioActual;
                    obj.Documentos = obtenerDocumentos();
                    obj.Asociados = obtenerAsociados();
                    obj.LoteTrabajo = obtenerLoteTrabajo();

                    if (obj.CONC_CID == 0)
                    {
                        var resultado = new BLCampaniaCallCenter().Insertar(obj);
                    }
                    else
                    {
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
                        //7.setting Asociados eliminar
                        List<BEAgenteCampania> listaAsoDel = null;
                        if (AsociadosTmpDelBD != null)
                        {
                            listaAsoDel = new List<BEAgenteCampania>();
                            AsociadosTmpDelBD.ForEach(x => { listaAsoDel.Add(new BEAgenteCampania { SEQUENCE = x.Id }); });
                        }
                        //setting Asociados activar
                        List<BEAgenteCampania> listaAsoUpdEst = null;
                        if (AsociadosTmpUPDEstado != null)
                        {
                            listaAsoUpdEst = new List<BEAgenteCampania>();
                            AsociadosTmpUPDEstado.ForEach(x => { listaAsoUpdEst.Add(new BEAgenteCampania { SEQUENCE = x.Id }); });
                        }


                        //Prueba nueva pestaña lote de trabajo
                        List<BELoteTrabajo> listaLoteDel = null;
                        if (AsociadosTmpDelBD != null)
                        {
                            listaLoteDel = new List<BELoteTrabajo>();
                            LoteTrabajoTmpDelBD.ForEach(x => { listaLoteDel.Add(new BELoteTrabajo { CONC_SID = x.Id }); });
                        }
                        List<BELoteTrabajo> listaLoteUpdEst = null;
                        if (LoteTrabajoTmpUPDEstado != null)
                        {
                            listaLoteUpdEst = new List<BELoteTrabajo>();
                            LoteTrabajoTmpUPDEstado.ForEach(x => { listaLoteUpdEst.Add(new BELoteTrabajo { CONC_SID = x.Id }); });
                        }


                        var resultado = new BLCampaniaCallCenter().Actualizar(obj, listaDocDel, listaDocUpdEst, listaAsoDel, listaAsoUpdEst, listaLoteDel, listaLoteUpdEst);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "insertar Campaña", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                var dato = new BLCampaniaCallCenter().Eliminar(new BECampaniaCallCenter
                {
                    OWNER = GlobalVars.Global.OWNER,
                    CONC_CID = Id,
                    LOG_USER_UPDATE = UsuarioActual
                });

                if (dato > 0)
                {
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_ELIMINAR;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Eliminar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region Documento
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
                            shtml.AppendFormat("<td >{0}</td>", item.FechaRecepcion.Substring(0, 10));
                            shtml.AppendFormat("<td ><a href='#' onclick=verImagen('{0}');>Ver Imagen</a></td>", ruta);
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
                        LOG_USER_UPDATE = UsuarioActual,
                        DOC_VERSION = 1
                    });
                });
            }
            return datos;
        }
        #endregion

        #region Agente
        private List<BEAgenteCampania> obtenerAsociados()
        {
            List<BEAgenteCampania> datos = new List<BEAgenteCampania>();

            if (AsociadosTmp != null)
            {
                AsociadosTmp.ForEach(x =>
                {
                    datos.Add(new BEAgenteCampania
                    {
                        SEQUENCE = x.Id,
                        ROL_ID = Convert.ToString(x.RolTipo),
                        BPS_ID = x.CodigoAsociado,
                        OWNER = GlobalVars.Global.OWNER,
                        LOG_USER_CREAT = UsuarioActual
                    });
                });
            }
            return datos;
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

        public JsonResult AddAsociado(DTOAsociado entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
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
                        entidad.Accion = true;
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
                        entidad.FechaCrea = item.FechaCrea;
                        entidad.Accion = true;
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
                    asociados = AsociadosTmp;
                    if (asociados != null)
                    {
                        var objDel = asociados.Where(x => x.Id == id).FirstOrDefault();
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddAsociado", ex);
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
                    shtml.Append("<thead><tr><th class='k-header' >Id</th>");
                    shtml.Append("<th class='k-header' style='display:none'>Id</th>");
                    shtml.Append("<th class='k-header' style='display:none'>Codigo</th>");
                    shtml.Append("<th class='k-header'>Nombre del Asociado</th>");
                    shtml.Append("<th class='k-header' style='display:none'>Nombre</th>");
                    shtml.Append("<th class='k-header'>Rol</th>");
                    shtml.Append("<th class='k-header' style='display:none'>RolTipo</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th class='k-header'></th></tr></thead>");

                    if (asociados != null)
                    {
                        GeneralController gen = new GeneralController();
                        var items = gen.ListarRolesEntidadesEst();

                        foreach (var item in asociados.OrderBy(x => x.Id))
                        {
                            string option = "<option value='0'>--SELECCIONE--</option>";
                            option += itemRoles(items, Convert.ToString(item.Id));

                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);

                            shtml.AppendFormat("<td style='display:none'><input type='text' id='txtId_{1}' style='width: 40px' value={0}></input></td>", item.Id, item.Id);
                            shtml.AppendFormat("<td style='display:none'><input type='text' id='txtCodigoAsociado_{1}' style='width: 40px' value={0}></input></td>", item.CodigoAsociado, item.Id);

                            shtml.AppendFormat("<td >{0}</td>", item.NombreAsociado);
                            shtml.AppendFormat("<td style='display:none'><input type='text' id='txtNombreAsociado_{1}' style='width: 200px' value={0}></input></td>", item.NombreAsociado, item.Id);

                            shtml.AppendFormat("<td style='text-align:center'><select id='ddlAplicable_{0}' onchange='return UpdRol({0},{1});' style='width: 160px'>{2}</select></td>", item.Id, item.CodigoAsociado, option);

                            shtml.AppendFormat("<td style='display:none'>{0}</td>", item.RolTipo);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td> <a href=# onclick='delAddAsociado({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Asociado" : "Activar Asociado" + "</td>");
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

        private string itemAgenteRecaudo(List<SelectListItem> items, string codeId)
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

        private string itemAgenteRecaudoTmp(List<SelectListItem> items, string codeId)
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

        private string itemRoles(List<SelectListItem> items, string codeId)
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

        private List<SelectListItem> ListarAgenteRecaudadorTmp()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            foreach (var itemss in AsociadosTmp)
            {
                lista.Add(new SelectListItem
                {
                    //Value = itemss.Id.ToString(),
                    Value = itemss.CodigoAsociado.ToString(),
                    Text = itemss.NombreAsociado
                });
            }

            return lista;
        }

        [HttpPost]
        public JsonResult UpdRol(decimal idRol, decimal idAsociado)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLSocioNegocio servicio = new BLSocioNegocio();
                    var datos = servicio.ObtenerDatos(idAsociado, GlobalVars.Global.OWNER);
                    if (datos != null)
                    {
                        var aso = AsociadosTmp;
                        if (aso.Count > 0)
                        {
                            aso.ForEach(c =>
                            {
                                if (c.CodigoAsociado == idAsociado)
                                {
                                    c.RolTipo = idRol;
                                }
                            });
                            AsociadosTmp = aso;
                        }
                        retorno.result = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "UpdRol", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult UpdAgente(decimal id, decimal IdAgente)
        //{
        //    Resultado retorno = new Resultado();
        //    try
        //    {
        //        if (!isLogout(ref retorno))
        //        {
        //            BLSocioNegocio servicio = new BLSocioNegocio();
        //            //var datos = servicio.ObtenerDatos(idAsociado, GlobalVars.Global.OWNER);
        //            //if (datos != null)
        //            //{
        //            var aso = LoteTrabajoTmp;
        //            if (aso.Count > 0)
        //            {
        //                aso.ForEach(c =>
        //                {
        //                    if (c.Id == id)
        //                    {
        //                        c.IdAgente = IdAgente;
        //                    }
        //                });
        //                LoteTrabajoTmp = aso;
        //            }
        //            retorno.result = 1;
        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
        //        retorno.result = 0;
        //        ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "UpdAgente", ex);
        //    }
        //    return Json(retorno, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        public JsonResult ObtieneListaAsignarSocioCampaña(decimal idTipoLic, decimal idMod, string idGrupoMod, decimal idGrupoFac, decimal idTemp, decimal idSerie, decimal idEst,
            decimal idSubtipoEst, decimal idTipoEst, string idTipoPersona, decimal idTipoDoc, string numeroDoc, string socio, decimal idUbigeo, string usuario, string recaudador, string asociado, string grupo, string empleado, string proveedor, int Estado)
        {
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    var listaAsignarSocio = new BLCampaniaCallCenter().ObtenerListas(GlobalVars.Global.OWNER, idTipoLic, idMod, idGrupoMod, idGrupoFac, idTemp, idSerie, idEst,
                        idSubtipoEst, idTipoEst, idTipoPersona, idTipoDoc, numeroDoc, socio, idUbigeo, usuario, recaudador, asociado, grupo, empleado, proveedor, Estado);

                    if (listaAsignarSocio.AsignarSocioCab != null)
                    {
                        listaAsignarSocioCab = new List<DTOCampaniaAsignarSocio>();
                        listaAsignarSocio.AsignarSocioCab.ForEach(s =>
                            {
                                listaAsignarSocioCab.Add(new DTOCampaniaAsignarSocio
                                {
                                    idSocio = s.BPS_ID,
                                    NombreSocio = s.BPS_NAME,
                                    TipoDocumento = s.TAXN_NAME,
                                    NumeroDocumento = s.TAX_ID,
                                    NetoFact = s.INV_NET,
                                    CobradoFact = s.INV_COLLECTN,
                                    PendienteFact = s.INV_BALANCE,
                                    Perfil = s.PERFIL,
                                });
                            });
                        AsignarSocioCabTmp = listaAsignarSocioCab;
                    }

                    if (listaAsignarSocio.AsignarSocioDet != null)
                    {
                        listaAsignarSocioDet = new List<DTOCampaniaAsignarSocioDetalle>();
                        listaAsignarSocio.AsignarSocioDet.ForEach(s =>
                            {
                                listaAsignarSocioDet.Add(new DTOCampaniaAsignarSocioDetalle
                                    {
                                        idLicencia = s.LIC_ID,
                                        idSocio = s.BPS_ID,
                                        Licencia = s.LIC_NAME,
                                        Establecimiento = s.EST_NAME,
                                        NetoFact = s.INV_NET,
                                        CobradoFact = s.INV_COLLECTN,
                                        PendienteFact = s.INV_BALANCE,
                                        Moneda = s.CUR_DESC
                                    });
                            });
                        AsignarSocioDetTmp = listaAsignarSocioDet;
                    }

                    if (listaAsignarSocio.AsignarSocioSubDet != null)
                    {
                        listaAsignarSocioSubDet = new List<DTOCampaniaAsignarSocioSubDetalle>();
                        listaAsignarSocio.AsignarSocioSubDet.ForEach(s =>
                        {
                            listaAsignarSocioSubDet.Add(new DTOCampaniaAsignarSocioSubDetalle
                                {
                                    idPeriodo = s.LIC_PL_ID,
                                    idLicencia = s.LIC_ID,
                                    Periodo = s.LIC_DATE_CAD,
                                    NetoFact = s.INV_NET,
                                    CobradoFact = s.INV_COLLECTN,
                                    PendienteFact = s.INV_BALANCE
                                });
                        });
                        AsignarSocioSubDetTmp = listaAsignarSocioSubDet;
                    }

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(listaAsignarSocio, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneListaAsignarSocioCampaña", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarAsignarSocioCampaniaCab()
        {
            var listaCab = AsignarSocioCabTmp;
            Resultado retorno = new Resultado();
            try
            {
                int i = 0;
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    var clase = "'ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'";
                    shtml.Append("<table class='k-grid k-widget' border=0 width='100%;' id='tblAsignarCampania'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class=" + clase + " style='width:10px'></th>");
                    shtml.Append("<th class=" + clase + " style='width:20px'></th>");
                    shtml.Append("<th class=" + clase + " style='width:40px'>Id</th>");
                    shtml.Append("<th class=" + clase + " style='width:40px; display:none;'>IdAux</th>");
                    shtml.Append("<th class=" + clase + " style='width:40px; display:none;'>IdSocio</th>");
                    shtml.Append("<th class=" + clase + " style='width:250px'>Socio</th>");
                    shtml.Append("<th class=" + clase + " style='width:80px'>Tipo Doc.</th>");
                    shtml.Append("<th class=" + clase + " style='width:80px'>Num Doc.</th>");
                    shtml.Append("<th class=" + clase + " style='width:150px'>Facturado</th>");
                    shtml.Append("<th class=" + clase + " style='width:150px'>Cobrado</th>");
                    shtml.Append("<th class=" + clase + " style='width:150px'>Saldo Pendiente</th>");
                    //shtml.Append("<th class=" + clase + " style='width:120px'>Perfil</th>");

                    if (listaCab.Count > 0)
                    {
                        foreach (var item in listaCab.OrderBy(x => x.idSocio))
                        {
                            i++;
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.AppendFormat("<td style='text-align:center;width:25px'><input type='checkbox' id='{0}' class='chk'/></td>", "chkFact" + i);
                            shtml.AppendFormat("<td style='width:25px; cursor:pointer;'>");
                            shtml.AppendFormat("<a href=# onclick='verDeta({0});'><img id='expand" + item.idSocio + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.idSocio);
                            shtml.Append("</td>");
                            shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.idSocio);
                            //style='display:none'
                            shtml.AppendFormat("<td style='cursor:pointer; display:none;' class='IdClass'>{0}</td>", i);
                            shtml.AppendFormat("<td style='cursor:pointer; display:none;'><input type='text' id='txtId_{1}' value={0} style='width:30px; text-align:center' readonly='true'/></td>", item.idSocio, i);

                            shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.NombreSocio);
                            shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.TipoDocumento);
                            shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.NumeroDocumento);
                            shtml.AppendFormat("<td style='width:120px;text-align:left;  padding-left:20px; cursor:pointer;'>S/. {0}</td>", item.NetoFact.ToString("# ### ###.00"), item.idSocio);
                            shtml.AppendFormat("<td style='width:120px;text-align:left;  padding-left:15px; cursor:pointer;'>S/. {0}</td>", item.CobradoFact.ToString("# ### ###.00"), item.idSocio);
                            shtml.AppendFormat("<td style='width:120px;text-align:left;  padding-left:20px; cursor:pointer;'>S/. {0}</td>", item.PendienteFact.ToString("# ### ###.00"), item.idSocio);
                            //shtml.AppendFormat("<td style='width:120px;text-align:center; padding-right:20px; cursor:pointer;'>{0}</td>", item.Perfil);
                            shtml.Append("</tr>");
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td colspan='10'>");
                            shtml.Append("<div style='display:none;' id='" + "div" + item.idSocio.ToString() + "'  > ");
                            shtml.Append(getHtmlListarAsignarSocioCampaniaDet(item.idSocio));
                            shtml.Append("</div>");
                            shtml.Append("</td>");
                            shtml.Append("</tr>");
                        }
                    }
                    else
                    {
                        shtml.Append("<tr id='trMesanjeCab' style='background-color:white'><td colspan=14><b><center>No se encontraron resultados de búsqueda.</center></b></td></tr>");
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarAsignarSocioCampaniaCab", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public StringBuilder getHtmlListarAsignarSocioCampaniaDet(decimal idSocio)
        {
            var listaDetalle = AsignarSocioDetTmp.Where(p => p.idSocio == idSocio).ToList();
            //int i = 0;

            StringBuilder shtml = new StringBuilder();
            shtml.Append("<table  border=0 width='100%;' id='FiltroTabla'>");
            shtml.Append("<thead>");
            shtml.Append("<tr>");
            shtml.Append("<th class='k-header' style='width:20px'></th>");
            shtml.Append("<th class='k-header' style='width:70px;'>Id</th>");
            shtml.Append("<th class='k-header' style='width:120px; display:none'>IdSocio</th>");
            shtml.Append("<th class='k-header' style='width:350px; cursor:pointer;'>Licencia</th>");
            shtml.Append("<th class='k-header' style='width:300px; cursor:pointer;'>Establecimiento</th>");
            shtml.Append("<th class='k-header' style='width:150px; cursor:pointer;'>Facturado</th>");
            shtml.Append("<th class='k-header' style='width:150px; cursor:pointer;'>Cobrado</th>");
            shtml.Append("<th class='k-header' style='width:150px; cursor:pointer;'>Saldo Pendiente</th>");
            shtml.Append("<th class='k-header' style='width:120px; cursor:pointer;'>Moneda</th>");

            if (listaDetalle != null && listaDetalle.Count > 0)
            {
                foreach (var item in listaDetalle.OrderBy(x => x.idLicencia))
                {
                    //i++;
                    //var aux = i * 2;
                    shtml.Append("<tr style='background-color:white'>");
                    shtml.AppendFormat("<td style='width:25px; cursor:pointer;'>");
                    shtml.AppendFormat("<a href=# onclick='verSubDeta({0});'><img id='expandSub" + item.idLicencia + "'  src='../Images/botones/more.png'  width=20px     title='Ver sub detalle.' alt='ver sub detalle.' border=0></a>", item.idLicencia);
                    shtml.AppendFormat("</td>");
                    shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.idLicencia);
                    shtml.AppendFormat("<td style='display:none'>{0}</td>", item.idSocio);
                    shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.Licencia);
                    shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.Establecimiento);
                    shtml.AppendFormat("<td style='text-align:left;  padding-left:20px; cursor:pointer;'>S/. {0}</td>", item.NetoFact.ToString("# ### ###.00"), item.idSocio);
                    shtml.AppendFormat("<td style='text-align:left;  padding-left:15px; cursor:pointer;'>S/. {0}</td>", item.CobradoFact.ToString("# ### ###.00"), item.idSocio);
                    shtml.AppendFormat("<td style='text-align:left;  padding-left:20px; cursor:pointer;'>S/. {0}</td>", item.PendienteFact.ToString("# ### ###.00"), item.idSocio);
                    shtml.AppendFormat("<td style='text-align:center; cursor:pointer;'>{0}</td>", item.Moneda);
                    shtml.Append("</td>");
                    shtml.Append("</tr>");

                    //shtml.Append("</tr>");
                    shtml.Append("<tr style='background-color:white'>");
                    shtml.Append("<td></td>");
                    shtml.Append("<td></td>");
                    shtml.Append("<td></td>");
                    shtml.Append("<td colspan='10'>");
                    shtml.Append("<div style='display:none;' id='" + "div" + item.idLicencia + "'  > ");
                    shtml.Append(getHtmlListarAsignarSocioCampaniaSubDet(item.idLicencia));
                    shtml.Append("</div>");
                    shtml.Append("</td>");
                    shtml.Append("</tr>");
                }
            }
            else
            {
                shtml.Append("<tr id='trMesanjeDet' style='background-color:white'><td colspan=14><b><center>No se encontraron resultados de búsqueda.</center></b></td></tr>");
            }

            shtml.Append("</table>");
            return shtml;
        }

        private StringBuilder getHtmlListarAsignarSocioCampaniaSubDet(decimal idLic)
        {
            var listaSubDetalle = AsignarSocioSubDetTmp.Where(p => p.idLicencia == idLic).ToList();

            StringBuilder shtml = new StringBuilder();
            shtml.Append("<table  border=0 width='100%;' id='FiltroTablaSub'>");
            shtml.Append("<thead>");
            shtml.Append("<tr>");
            shtml.Append("<th class='k-header' style='width:70px;'>Periodo</th>");
            shtml.Append("<th class='k-header' style='width:150px; cursor:pointer;'>Facturado</th>");
            shtml.Append("<th class='k-header' style='width:150px; cursor:pointer;'>Cobrado</th>");
            shtml.Append("<th class='k-header' style='width:150px; cursor:pointer;'>Saldo Pendiente</th>");

            if (listaSubDetalle != null && listaSubDetalle.Count > 0)
            {
                foreach (var item in listaSubDetalle.OrderBy(x => x.idLicencia))
                {
                    shtml.Append("<tr style='background-color:white'>");
                    shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.Periodo);
                    shtml.AppendFormat("<td style='text-align:left;  padding-left:20px; cursor:pointer;'>S/. {0}</td>", item.NetoFact.ToString("# ### ###.00"), item.idLicencia);
                    shtml.AppendFormat("<td style='text-align:left;  padding-left:15px; cursor:pointer;'>S/. {0}</td>", item.CobradoFact.ToString("# ### ###.00"), item.idLicencia);
                    shtml.AppendFormat("<td style='text-align:left;  padding-left:20px; cursor:pointer;'>S/. {0}</td>", item.PendienteFact.ToString("# ### ###.00"), item.idLicencia);
                    shtml.Append("</td>");
                    shtml.Append("</tr>");
                }
            }
            else
            {
                shtml.Append("<tr id='trMesanjeSubDet' style='background-color:white'><td colspan=14><b><center>No se encontraron resultados de búsqueda.</center></b></td></tr>");
            }

            shtml.Append("</table>");
            return shtml;
        }

        public JsonResult ObtenerSocioAsignarCampania(List<DTOCampaniaAsignarSocio> Asignar)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (Asignar != null || Asignar.Count() > 0)
                {
                    string NombreCampania = string.Empty;
                    List<BEContactoAsignarCampania> listaAsignar = new List<BEContactoAsignarCampania>();
                    List<BEContactoAsignarCampania> listaRepetidos = new List<BEContactoAsignarCampania>();
                    BEContactoAsignarCampania en = null;
                    BEContactoAsignarCampania contacto = null;
                    BEContactoAsignarCampania obtenercontacto = new BEContactoAsignarCampania();
                    BEContactoAsignarCampania contactosRepertidos = new BEContactoAsignarCampania();
                    List<string> aux = new List<string>();

                    foreach (var item in Asignar)
                    {
                        obtenercontacto = new BLCampaniaCallCenter().ObtenerSociosAsignados(GlobalVars.Global.OWNER, item.idCampania, item.idSocio);

                        if (obtenercontacto != null)
                        {
                            contacto = new BEContactoAsignarCampania();
                            contacto.CONC_CID = obtenercontacto.CONC_CID;
                            contacto.BPS_ID = obtenercontacto.BPS_ID;
                            listaRepetidos.Add(contacto);
                        }
                        else
                        {
                            en = new BEContactoAsignarCampania();
                            en.OWNER = GlobalVars.Global.OWNER;
                            en.CONC_CID = item.idCampania;
                            en.BPS_ID = item.idSocio;
                            en.LOG_USER_CREAT = UsuarioActual;
                            listaAsignar.Add(en);
                        }
                    }

                    foreach (var item in listaRepetidos)
                    {
                        var desc = new BLCampaniaCallCenter().obtenerNombreCampania(GlobalVars.Global.OWNER, item.CONC_CID);
                        NombreCampania = desc.CONC_CNAME;

                        var usu = new BLSocioNegocio().ObtenerDatos(item.BPS_ID, GlobalVars.Global.OWNER);
                        if (usu != null)
                        {
                            aux.Add(usu.BPS_NAME);
                        }
                    }

                    var dato = new BLCampaniaCallCenter().InsertarCampaniaAsignarXML(listaAsignar);

                    if (dato)
                    {
                        retorno.result = 1;

                        if (listaRepetidos.Count > 0 && listaRepetidos != null)
                        {
                            var cadena = String.Join(", ", aux);
                            retorno.message = "Se asigno exitosamente." + Environment.NewLine + "Estos socios ya han sido asignados anteriormente" + Environment.NewLine + cadena + Environment.NewLine + "Campaña: " + NombreCampania;
                        }
                        else
                            retorno.message = "Se asigno exitosamente.";
                    }
                    else
                    {
                        if (listaRepetidos.Count > 0 && listaRepetidos != null)
                        {
                            var cadena = String.Join(", ", aux);
                            retorno.message = "Estos socios ya han sido asignados anteriormente" + Environment.NewLine + cadena + Environment.NewLine + "Campaña: " + NombreCampania;
                        }
                        else if (listaRepetidos.Count == 0 || listaRepetidos == null)
                        {
                            retorno.result = 0;
                            retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GRABAR;
                        }
                        else
                        {
                            retorno.result = 0;
                            retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GRABAR;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerSocioAsignarCampania", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region LoteTrabajo
        public JsonResult ObtenerLoteTrabajo(List<DTOLoteTrabajo> LoteTrabajo)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (LoteTrabajo != null || lotetrabajo.Count() > 0)
                {
                    foreach (var x in LoteTrabajo)
                    {
                        foreach (var item in LoteTrabajoTmp.Where(w => w.Id == x.Id))
                        {
                            item.IdAgente = x.IdAgente;
                            item.FechaIni = x.FechaIni;
                            item.FechaFin = x.FechaFin;
                        }
                    }
                    retorno.result = 1;

                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "No se ha podido grabar, no se pudo obtener los datos de Lote de Trabajo";
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerLoteTrabajo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddLoteTrabajo(DTOLoteTrabajo entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (AsociadosTmp.Count > 0 && AsociadosTmp != null)
                    {
                        lotetrabajo = LoteTrabajoTmp;
                        if (lotetrabajo == null) lotetrabajo = new List<DTOLoteTrabajo>();
                        if (Convert.ToInt32(entidad.Id) <= 0)
                        {
                            decimal nuevoId = 1;
                            if (lotetrabajo.Count > 0) nuevoId = lotetrabajo.Max(x => x.Id) + 1;
                            entidad.Id = nuevoId;
                            entidad.Activo = true;
                            entidad.EnBD = false;
                            entidad.UsuarioCrea = UsuarioActual;
                            entidad.FechaCrea = DateTime.Now;
                            lotetrabajo.Add(entidad);
                        }
                        else
                        {
                            var item = lotetrabajo.Where(x => x.Id == entidad.Id).FirstOrDefault();
                            entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                            entidad.Activo = item.Activo;
                            entidad.UsuarioCrea = item.UsuarioCrea;
                            entidad.FechaCrea = item.FechaCrea;
                            if (entidad.EnBD)
                            {
                                entidad.UsuarioModifica = UsuarioActual;
                                entidad.FechaModifica = DateTime.Now;
                            }
                            lotetrabajo.Remove(item);
                            lotetrabajo.Add(entidad);
                        }
                        LoteTrabajoTmp = lotetrabajo;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "Ingrese Agentes. Para poder ingresar lote de trabajo";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddLoteTrabajo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarLoteTrabajo(decimal? idCampania)
        {
            lotetrabajo = LoteTrabajoTmp;
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tbLoteTrabajo' border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th>");
                    shtml.Append("<th class='k-header' style='display:none'>IdAge</th>");
                    shtml.Append("<th class='k-header'>Agente Recaudador</th>");
                    shtml.Append("<th class='k-header'>Fecha Inicial</th>");
                    shtml.Append("<th class='k-header'>Fecha Final</th>");
                    shtml.Append("<th class='k-header'>Usu. Crea</th>");
                    shtml.Append("<th class='k-header'>Fecha Crea</th>");
                    shtml.Append("<th class='k-header'>Usu. Modi</th>");
                    shtml.Append("<th class='k-header'>Fecha Modi</th>");
                    shtml.Append("<th class='k-header'></th></tr></thead>");

                    if (lotetrabajo != null)
                    {
                        string option;
                        GeneralController gen = new GeneralController();
                        List<SelectListItem> items = new List<SelectListItem>();
                        if (idCampania != null)
                            items = gen.ListarAgenteRecaudador(idCampania);

                        foreach (var item in lotetrabajo.OrderBy(x => x.Id))
                        {
                            if (idCampania != null)
                            {
                                option = "<option value='0'>--SELECCIONE--</option>";
                                option += itemAgenteRecaudo(items, Convert.ToString(item.IdAgente));
                            }
                            else
                            {
                                option = "<option value='0'>--SELECCIONE--</option>";
                                List<SelectListItem> itemss = new List<SelectListItem>();
                                itemss = ListarAgenteRecaudadorTmp();
                                option += itemAgenteRecaudoTmp(itemss, "0");
                            }

                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td style='text-align:center; display:none'><input type='text' id='txtId_{0}' value='{1}' /></td>", item.Id, item.Id);
                            //shtml.AppendFormat("<td style='text-align:center'><select id='ddlAgente_{0}' onchange='return UpdAgente({0},{1});' style='width: 160px'>{2}</select></td>", item.Id, item.IdAgente, option);
                            shtml.AppendFormat("<td style='text-align:center'><select id='ddlAgente_{0}' style='width: 160px'>{1}</select></td>", item.Id, option);
                            shtml.AppendFormat("<td style='text-align:center'><input type='text' id='txtFechaIni_{0}' value='{1}' /></td>", item.Id, item.FechaIni);
                            shtml.AppendFormat("<td style='text-align:center'><input type='text' id='txtFechaFin_{0}' value='{1}' /></td>", item.Id, item.FechaFin);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td style='text-align:center'> <a href=# onclick='delAddLoteTrabajo({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Asociado" : "Activar Asociado" + "</td>");
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarLoteTrabajo", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DellAddLoteTrabajo(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    lotetrabajo = LoteTrabajoTmp;
                    if (lotetrabajo != null)
                    {
                        var objDel = lotetrabajo.Where(x => x.Id == id).FirstOrDefault();
                        if (objDel != null)
                        {

                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (LoteTrabajoTmpUPDEstado == null) LoteTrabajoTmpUPDEstado = new List<DTOLoteTrabajo>();
                                if (LoteTrabajoTmpDelBD == null) LoteTrabajoTmpDelBD = new List<DTOLoteTrabajo>();

                                var itemUpd = LoteTrabajoTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                                var itemDel = LoteTrabajoTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) LoteTrabajoTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) LoteTrabajoTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) LoteTrabajoTmpDelBD.Add(objDel);
                                    if (itemUpd != null) LoteTrabajoTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                lotetrabajo.Remove(objDel);
                                lotetrabajo.Add(objDel);
                            }
                            else
                            {
                                lotetrabajo.Remove(objDel);
                            }
                            LoteTrabajoTmp = lotetrabajo;
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

        private List<BELoteTrabajo> obtenerLoteTrabajo()
        {
            List<BELoteTrabajo> datos = new List<BELoteTrabajo>();

            if (LoteTrabajoTmp != null)
            {
                LoteTrabajoTmp.ForEach(x =>
                {
                    datos.Add(new BELoteTrabajo
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        CONC_SID = x.Id,
                        CONC_SDATEINI = Convert.ToDateTime(x.FechaIni),
                        CONC_SDATEND = Convert.ToDateTime(x.FechaFin),
                        BPS_ID = x.IdAgente,
                        CONC_CID = x.IdCampania,
                        LOG_USER_CREAT = UsuarioActual,
                        LOG_USER_UPDAT = UsuarioActual

                    });
                });
            }
            return datos;
        }

        [HttpPost]
        public JsonResult ValidaRangoFechasLoteTrabajoInicioFin(DateTime FechaIni, DateTime FechaFin, DateTime FechaIniCamp, DateTime FechaFinCamp)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    #region ValidacionFechaLote
                    if (DateTime.Compare(FechaIni, FechaFin) > 0)
                    {
                        retorno.result = 0;
                        retorno.message = "Fecha inicial no puede ser mayor a la final";
                    }
                    else if (DateTime.Compare(FechaIni, FechaFin) == 0)
                    {
                        retorno.result = 0;
                        retorno.message = "Fecha inicial no puede ser igual a la final";
                    }
                    #endregion

                    #region ValidacionFechaLoteConCampaña
                    else if (DateTime.Compare(FechaIni, FechaIniCamp) < 0)
                    {
                        retorno.result = 0;
                        retorno.message = "Fecha inicial del lote de trabajo, no puede ser menor a la fecha inicial de la campaña";
                    }
                    else if (DateTime.Compare(FechaIni, FechaFinCamp) > 0)
                    {
                        retorno.result = 0;
                        retorno.message = "Fecha inicial del lote de trabajo, no puede ser mayor a la fecha final de la campaña";
                    }
                    else if (DateTime.Compare(FechaIni, FechaIniCamp) == 0)
                    {
                        retorno.result = 0;
                        retorno.message = "Fecha inicial del lote de trabajo no puede ser igual a la fecha inicial de la campaña";
                    }
                    else if (DateTime.Compare(FechaIni, FechaFinCamp) == 0)
                    {
                        retorno.result = 0;
                        retorno.message = "Fecha inicial del lote de trabajo no puede ser igual a la fecha final de la campaña";
                    }
                    #endregion

                    else
                        retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ValidaRangoFechasLoteTrabajoInicioFin", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ValidaRangoFechasLoteTrabajoFinInicio(DateTime FechaFin, DateTime FechaIni, DateTime FechaFinCamp, DateTime FechaIniCamp)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    #region ValidacionFechaLote
                    if (DateTime.Compare(FechaFin, FechaIni) < 0)
                    {
                        retorno.result = 0;
                        retorno.message = "Fecha final no puede ser menor a la inicial";
                    }
                    else if (DateTime.Compare(FechaFin, FechaIni) == 0)
                    {
                        retorno.result = 0;
                        retorno.message = "Fecha final no puede ser igual a la inicial";
                    }
                    #endregion

                    #region ValidacionFechaLoteConCampaña
                    else if (DateTime.Compare(FechaFin, FechaIniCamp) < 0)
                    {
                        retorno.result = 0;
                        retorno.message = "Fecha final del lote de trabajo, no puede ser menor a la fecha inicial de la campaña";
                    }
                    else if (DateTime.Compare(FechaFin, FechaFinCamp) > 0)
                    {
                        retorno.result = 0;
                        retorno.message = "Fecha final del lote de trabajo, no puede ser mayor a la fecha final de la campaña";
                    }
                    else if (DateTime.Compare(FechaFin, FechaIniCamp) == 0)
                    {
                        retorno.result = 0;
                        retorno.message = "Fecha final del lote de trabajo no puede ser igual a la fecha inicial de la campaña";
                    }
                    else if (DateTime.Compare(FechaFin, FechaFinCamp) == 0)
                    {
                        retorno.result = 0;
                        retorno.message = "Fecha final del lote de trabajo no puede ser igual a la fecha final de la campaña";
                    }
                    #endregion

                    else
                        retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ValidaRangoFechasLoteTrabajoFinInicio", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CampaniaClienteAsignados

        [HttpPost]
        public JsonResult ListarClientesAsignadosCampania(decimal Id)
        {
            var lista = new BLCampaniaCallCenter().ListarClientesAsignadosCampania(GlobalVars.Global.OWNER, Id);

            Resultado retorno = new Resultado();
            try
            {
                int i = 0;
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    var clase = "'ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'";
                    shtml.Append("<table class='k-grid k-widget' border=0 width='100%;' id='tblAsignados'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class=" + clase + " style='width:60px'>Id</th>");
                    shtml.Append("<th class=" + clase + " style='width:500px'>Socio</th>");
                    shtml.Append("<th class=" + clase + " style='width:300px'>Usu. Crea</th>");
                    shtml.Append("<th class=" + clase + " style='width:80px'>Fecha Crea</th>");

                    if (lista.Count > 0)
                    {
                        foreach (var item in lista.OrderBy(x => x.CONC_CID))
                        {
                            i++;
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.BPS_ID);
                            shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.BPS_NAME);
                            shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.LOG_USER_CREAT);
                            shtml.AppendFormat("<td style='cursor:pointer;'>{0}</td>", item.LOG_DATE_CREAT.ToShortDateString());
                            shtml.Append("</tr>");
                        }
                    }
                    else
                    {
                        shtml.Append("<tr id='trMesanjeAsig' style='background-color:white'><td colspan=14><b><center>No se encontraron resultados.</center></b></td></tr>");
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarAsignarSocioCampaniaCab", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        
        #endregion
    }
}

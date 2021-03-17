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


namespace Proyect_Apdayc.Controllers
{
    public class RequerimientoDineroController : Base
    {
        public const string nomAplicacion = "SRGDA";

        private const string K_SESION_DETALLE_GASTO = "___DTODetalleGasto";
        private const string K_SESION_DETALLE_GASTO_DEL = "___DTODetalleGastoDEL";
        private const string K_SESION_DETALLE_GASTO_ACT = "___DTODetalleGastoACT";

        private const string K_SESION_DETALLE_APRO_GASTO = "___DTODetalleAproGasto";
        private const string K_SESION_DETALLE_APRO_GASTO_DEL = "___DTODetalleAproGastoDEL";
        private const string K_SESION_DETALLE_APRO_GASTO_ACT = "___DTODetalleAproGastoACT";

        private const string K_SESION_DETALLE_RENDIR_GASTO = "___DTODetalleRendirGasto";
        private const string K_SESION_DETALLE_RENDIR_GASTO_DEL = "___DTODetalleRendirGastoDEL";
        private const string K_SESION_DETALLE_RENDIR_GASTO_ACT = "___DTODetalleRendirGastoACT";

        //
        // GET: /RequerimientoDinero/
        List<DTODetalleGasto> detalles = new List<DTODetalleGasto>();
        List<DTODetalleGasto> detallesApro = new List<DTODetalleGasto>();
        List<DTODetalleGasto> detallesRendir = new List<DTODetalleGasto>();

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult ConsultaSolicitud()
        {
            Init(false);
            return View();
        }

        public ActionResult ConsultaAprobacion()
        {
            Init(false);
            return View();
        }

        public ActionResult ConsultaRendir()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESION_DETALLE_GASTO);
            Session.Remove(K_SESION_DETALLE_GASTO_DEL);
            Session.Remove(K_SESION_DETALLE_GASTO_ACT);
            return View();
        }

        public ActionResult Aprobacion()
        {
            Init(false);
            Session.Remove(K_SESION_DETALLE_APRO_GASTO);
            Session.Remove(K_SESION_DETALLE_APRO_GASTO_DEL);
            Session.Remove(K_SESION_DETALLE_APRO_GASTO_ACT);
            return View();
        }

        public ActionResult Rendir()
        {
            Init(false);
            Session.Remove(K_SESION_DETALLE_RENDIR_GASTO);
            Session.Remove(K_SESION_DETALLE_RENDIR_GASTO_DEL);
            Session.Remove(K_SESION_DETALLE_RENDIR_GASTO_ACT);
            return View();
        }

        public ActionResult Edit()
        {
            Init(false);
            return View();
        }

        public JsonResult Listar(int skip, int take, int page, int pageSize, string group, decimal id, string tipo, string nro, string nombre, int estado)
        {
            Resultado retorno = new Resultado();
            var lista = BLListar(GlobalVars.Global.OWNER, id, tipo, nro, nombre, estado, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BERequerimientoDinero { RequerimientoDinero = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BERequerimientoDinero { RequerimientoDinero = lista, TotalVirtual = Convert.ToInt32(tot[0]) }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BERequerimientoDinero> BLListar(string owner, decimal id, string tipo, string nro, string nombre, int estado, int pagina, int cantRegxPag)
        {
            return new BLRequerimientoDinero().Listar(owner, id, tipo, nro, nombre, estado, pagina, cantRegxPag);
        }

        #region SolicitarDetalle

        public List<DTODetalleGasto> DetallesTmp
        {
            get
            {
                return (List<DTODetalleGasto>)Session[K_SESION_DETALLE_GASTO];
            }
            set
            {
                Session[K_SESION_DETALLE_GASTO] = value;
            }
        }

        private List<DTODetalleGasto> DetallesTmpUPDEstado
        {
            get
            {
                return (List<DTODetalleGasto>)Session[K_SESION_DETALLE_GASTO_ACT];
            }
            set
            {
                Session[K_SESION_DETALLE_GASTO_ACT] = value;
            }
        }

        private List<DTODetalleGasto> DetallesTmpDelBD
        {
            get
            {
                return (List<DTODetalleGasto>)Session[K_SESION_DETALLE_GASTO_DEL];
            }
            set
            {
                Session[K_SESION_DETALLE_GASTO_DEL] = value;
            }
        }

        [HttpPost]
        public JsonResult AddDetalle(DTODetalleGasto detalle)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    detalles = DetallesTmp;
                    if (detalles == null) detalles = new List<DTODetalleGasto>();

                    // if (Convert.ToInt32(documento.Id) <= 0) documento.Id = Convert.ToString(documentos.Count+1);
                    if (Convert.ToInt32(detalle.Id) <= 0)
                    {
                        decimal nuevoId = 1;
                        if (detalles.Count > 0) nuevoId = detalles.Max(x => x.Id) + 1;
                        detalle.Id = nuevoId;
                        detalle.UsuarioCrea = UsuarioActual;
                        detalle.FechaCrea = DateTime.Now;
                        detalle.Activo = true;
                        detalle.EnBD = false;
                        detalles.Add(detalle);
                    }
                    else
                    {
                        var item = detalles.Where(x => x.Id == detalle.Id).FirstOrDefault();
                        detalle.EnBD = item.EnBD;//indicador que item viene de la BD
                        detalle.Activo = item.Activo;
                        detalle.UsuarioCrea = item.UsuarioCrea;
                        detalle.FechaCrea = item.FechaCrea;
                        if (item.EnBD)
                        {
                            detalle.UsuarioModifica = UsuarioActual;
                            detalle.FechaModifica = DateTime.Now;
                        }
                        detalles.Remove(item);
                        detalles.Add(detalle);
                    }
                    DetallesTmp = detalles;
                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddDetalle", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddDetalle(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    detalles = DetallesTmp;
                    if (detalles != null)
                    {
                        var objDel = detalles.Where(x => x.Id == id).FirstOrDefault();
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (DetallesTmpUPDEstado == null) DetallesTmpUPDEstado = new List<DTODetalleGasto>();
                                if (DetallesTmpDelBD == null) DetallesTmpDelBD = new List<DTODetalleGasto>();

                                var itemUpd = DetallesTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                                var itemDel = DetallesTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) DetallesTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) DetallesTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) DetallesTmpDelBD.Add(objDel);
                                    if (itemUpd != null) DetallesTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                detalles.Remove(objDel);
                                detalles.Add(objDel);
                            }
                            else
                            {
                                detalles.Remove(objDel);
                            }
                            DetallesTmp = detalles;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddDetalle", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BEDetalleGasto> obtenerDetalles()
        {
            List<BEDetalleGasto> datos = new List<BEDetalleGasto>();
            if (DetallesTmp != null)
            {
                DetallesTmp.ForEach(x =>
                {
                    datos.Add(new BEDetalleGasto
                    {
                        MNR_DET_ID = x.Id,
                        MNR_ID = x.ReqGasto_Id,
                        OWNER = GlobalVars.Global.OWNER,
                        EXP_TYPE = x.Tipo_Id,
                        EXPG_ID = x.Grupo_Id,
                        EXP_ID = x.Gasto_Id,
                        EXP_DESC = x.Gasto_Des,
                        EXP_VAL_PRE = x.Monto_Solicitado,
                        EXP_VAL_APR = x.Monto_Aprobado,
                        EXP_VAL_CON = x.Monto_Gastado,
                        LOG_USER_CREAT = x.UsuarioCrea,
                        LOG_USER_UPDAT = x.UsuarioModifica

                    });
                });
            }
            return datos;
        }

        public JsonResult ObtieneDetalleTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var param = DetallesTmp.Where(x => x.Id == idDir).FirstOrDefault();
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

        public JsonResult ListarDetalle()
        {
            detalles = DetallesTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tblUsuario' border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Gastos</th>");
                    shtml.Append("<th class='k-header'>S/. Solicitado</th>");
                    shtml.Append("<th  style='display:none;' class='k-header'>Solicitado</th>");
                    shtml.Append("<th class='k-header'>S/. Aprobado</th>");
                    shtml.Append("<th class='k-header'>S/. Gastado</th>");
                    shtml.Append("<th class='k-header'>Estado Req.</th>");

                    shtml.Append("<th class='k-header'>Usuario Creación</th>");
                    shtml.Append("<th class='k-header'>Fecha Creación</th>");
                    shtml.Append("<th class='k-header'>Usuario Modificación</th>");
                    shtml.Append("<th class='k-header'>Fecha Modificación</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th  class='k-header'></th>");
                    shtml.Append("</tr></thead>");

                    if (detalles != null)
                    {
                        foreach (var item in detalles.OrderBy(x => x.Id))
                        {
                            if (item.Activo)
                            {
                                shtml.Append("<tr class='k-grid-content'>");
                                shtml.AppendFormat("<td >{0}</td>", item.Id);
                                shtml.AppendFormat("<td >{0}</td>", item.Gasto_Des);
                                shtml.AppendFormat("<td >{0:C}</td>", item.Monto_Solicitado);
                                shtml.AppendFormat("<td  style='display:none;'>{0}</td>", item.Monto_Solicitado);
                                shtml.AppendFormat("<td >{0:C}</td>", item.Monto_Aprobado);
                                shtml.AppendFormat("<td >{0:C}</td>", item.Monto_Gastado);
                                //shtml.AppendFormat("<td >{0}</td>", item.Estado);
                                shtml.AppendFormat("<td >{0}</td>", "ABIERTO");
                                shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                                shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                                shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                                shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                                shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                                shtml.AppendFormat("<td style='width:60px'> <a href=# onclick='updAddDetalle({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                                shtml.AppendFormat("                        <a href=# onclick='delAddDetalle({0});'><img src='../Images/iconos/{1}'      title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Gasto" : "Activar Gasto");
                                shtml.Append("</td>");
                                shtml.Append("</tr>");
                            }
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

        //*************************************************************
        [HttpPost]
        public JsonResult Insertar(BERequerimientoDinero req)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    req.OWNER = GlobalVars.Global.OWNER;
                    req.DetalleGasto = obtenerDetalles();
                    if (req.MNR_ID == 0)
                    {
                        req.LOG_USER_CREAT = UsuarioActual;
                        var datos = new BLRequerimientoDinero().Insertar(req);
                    }
                    else
                    {
                        req.LOG_USER_UPDATE = UsuarioActual;
                        //var datos = new BLRequerimientoDinero().Actualizar(req);

                        //.setting  valor eliminar
                        List<BEDetalleGasto> listaDetalleDel = null;
                        if (DetallesTmpDelBD != null)
                        {
                            listaDetalleDel = new List<BEDetalleGasto>();
                            DetallesTmpDelBD.ForEach(x => { listaDetalleDel.Add(new BEDetalleGasto { MNR_DET_ID = x.Id }); });
                        }
                        //setting valor activar
                        List<BEDetalleGasto> listaDetallesUpdEst = null;
                        if (DetallesTmpUPDEstado != null)
                        {
                            listaDetallesUpdEst = new List<BEDetalleGasto>();
                            DetallesTmpUPDEstado.ForEach(x => { listaDetallesUpdEst.Add(new BEDetalleGasto { MNR_DET_ID = x.Id }); });
                        }
                        var datos = new BLRequerimientoDinero().Actualizar(req,
                                                                 listaDetalleDel, listaDetallesUpdEst
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Obtener(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BERequerimientoDinero req = new BERequerimientoDinero();
                    req = new BLRequerimientoDinero().Obtener(GlobalVars.Global.OWNER, id);

                    if (req != null)
                    {
                        if (req.DetalleGasto != null)
                        {
                            detalles = new List<DTODetalleGasto>();
                            if (req.DetalleGasto != null)
                            {
                                req.DetalleGasto.ForEach(s =>
                                {
                                    detalles.Add(new DTODetalleGasto
                                    {
                                        Id = s.MNR_DET_ID,
                                        ReqGasto_Id = s.MNR_ID,
                                        Tipo_Id = s.EXP_TYPE,
                                        Grupo_Id = s.EXPG_ID,
                                        Gasto_Id = s.EXP_ID,
                                        Gasto_Des = s.EXP_DESC,
                                        Monto_Solicitado = s.EXP_VAL_PRE,
                                        Monto_Aprobado = s.EXP_VAL_APR,
                                        Monto_Gastado = s.EXP_VAL_CON,

                                        UsuarioCrea = s.LOG_USER_CREAT,
                                        FechaCrea = s.LOG_DATE_CREAT,
                                        UsuarioModifica = s.LOG_USER_UPDAT,
                                        FechaModifica = s.LOG_DATE_UPDATE,

                                        EnBD = true,
                                        Activo = s.ENDS.HasValue ? false : true
                                    });
                                });
                                DetallesTmp = detalles;
                                ListarDetalle();
                            }
                        }

                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(req, JsonRequestBehavior.AllowGet);
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado la definición de gasto";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Obtener", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region AprobarDetalle

        public List<DTODetalleGasto> DetallesTmpApro
        {
            get
            {
                return (List<DTODetalleGasto>)Session[K_SESION_DETALLE_APRO_GASTO];
            }
            set
            {
                Session[K_SESION_DETALLE_APRO_GASTO] = value;
            }
        }

        private List<DTODetalleGasto> DetallesTmpUPDEstadoApro
        {
            get
            {
                return (List<DTODetalleGasto>)Session[K_SESION_DETALLE_APRO_GASTO_ACT];
            }
            set
            {
                Session[K_SESION_DETALLE_APRO_GASTO_ACT] = value;
            }
        }

        private List<DTODetalleGasto> DetallesTmpDelBDApro
        {
            get
            {
                return (List<DTODetalleGasto>)Session[K_SESION_DETALLE_APRO_GASTO_DEL];
            }
            set
            {
                Session[K_SESION_DETALLE_APRO_GASTO_DEL] = value;
            }
        }

        [HttpPost]
        public JsonResult AddDetalleApro(DTODetalleGasto detalleApro)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    detallesApro = DetallesTmpApro;
                    if (detallesApro == null) detallesApro = new List<DTODetalleGasto>();

                    if (Convert.ToInt32(detalleApro.Id) <= 0)
                    {
                        decimal nuevoId = 1;
                        if (detallesApro.Count > 0) nuevoId = detallesApro.Max(x => x.Id) + 1;
                        detalleApro.Id = nuevoId;
                        detalleApro.UsuarioCrea = UsuarioActual;
                        detalleApro.FechaCrea = DateTime.Now;
                        detalleApro.Activo = true;
                        detalleApro.EnBD = false;
                        detallesApro.Add(detalleApro);
                    }
                    else
                    {
                        var item = detallesApro.Where(x => x.Id == detalleApro.Id).FirstOrDefault();
                        detalleApro.EnBD = item.EnBD;//indicador que item viene de la BD
                        detalleApro.Activo = item.Activo;
                        detalleApro.UsuarioCrea = item.UsuarioCrea;
                        detalleApro.FechaCrea = item.FechaCrea;
                        if (item.EnBD)
                        {
                            detalleApro.UsuarioModifica = UsuarioActual;
                            detalleApro.FechaModifica = DateTime.Now;
                        }
                        detalleApro.Monto_Solicitado = item.Monto_Solicitado;
                        detallesApro.Remove(item);
                        detallesApro.Add(detalleApro);
                    }
                    DetallesTmpApro = detallesApro;
                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddDetalleApro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddDetalleApro(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    detallesApro = DetallesTmpApro;
                    if (detallesApro != null)
                    {
                        var objDel = detallesApro.Where(x => x.Id == id).FirstOrDefault();
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (DetallesTmpUPDEstadoApro == null) DetallesTmpUPDEstadoApro = new List<DTODetalleGasto>();
                                if (DetallesTmpDelBDApro == null) DetallesTmpDelBDApro = new List<DTODetalleGasto>();

                                var itemUpd = DetallesTmpUPDEstadoApro.Where(x => x.Id == id).FirstOrDefault();
                                var itemDel = DetallesTmpDelBDApro.Where(x => x.Id == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) DetallesTmpUPDEstadoApro.Add(objDel);
                                    if (itemDel != null) DetallesTmpDelBDApro.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) DetallesTmpDelBDApro.Add(objDel);
                                    if (itemUpd != null) DetallesTmpUPDEstadoApro.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                detallesApro.Remove(objDel);
                                detallesApro.Add(objDel);
                            }
                            else
                            {
                                detallesApro.Remove(objDel);
                            }
                            DetallesTmpApro = detallesApro;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddDetalleApro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BEDetalleGasto> obtenerDetallesApro()
        {
            List<BEDetalleGasto> datos = new List<BEDetalleGasto>();
            if (DetallesTmpApro != null)
            {
                DetallesTmpApro.ForEach(x =>
                {
                    datos.Add(new BEDetalleGasto
                    {
                        MNR_DET_ID = x.Id,
                        MNR_ID = x.ReqGasto_Id,
                        OWNER = GlobalVars.Global.OWNER,
                        EXP_TYPE = x.Tipo_Id,
                        EXPG_ID = x.Grupo_Id,
                        EXP_ID = x.Gasto_Id,
                        EXP_DESC = x.Gasto_Des,
                        EXP_VAL_PRE = x.Monto_Solicitado,
                        EXP_VAL_APR = x.Monto_Aprobado,
                        EXP_VAL_CON = x.Monto_Gastado,
                        LOG_USER_CREAT = x.UsuarioCrea,
                        LOG_USER_UPDAT = x.UsuarioModifica

                    });
                });
            }
            return datos;
        }

        public JsonResult ObtieneDetalleTmpApro(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var param = DetallesTmpApro.Where(x => x.Id == idDir).FirstOrDefault();
                    retorno.data = Json(param, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneDetalleTmpApro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarDetalleApro(decimal estadoReq)
        {
            string estado;
            detallesApro = DetallesTmpApro;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tblUsuario' border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Gastos</th>");
                    shtml.Append("<th class='k-header'>S/. Solicitado</th>");
                    shtml.Append("<th  style='display:none;' class='k-header'>Solicitado</th>");
                    shtml.Append("<th class='k-header'>S/. Aprobado</th>");
                    shtml.Append("<th  style='display:none;' class='k-header'>Aprobado</th>");
                    shtml.Append("<th class='k-header'>S/. Gastado</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usuario Creación</th>");
                    shtml.Append("<th class='k-header'>Fecha Creación</th>");
                    shtml.Append("<th class='k-header'>Usuario Modificación</th>");
                    shtml.Append("<th class='k-header'>Fecha Modificación</th>");
                    shtml.Append("<th style='display:none;' class='k-header'>Estado</th>");
                    if (Constantes.EstadoReqDinero.ATENDIDO != estadoReq)
                        shtml.Append("<th class='k-header'></th>");

                    shtml.Append("</tr></thead>");

                    if (detallesApro != null)
                    {
                        foreach (var item in detallesApro.OrderBy(x => x.Id))
                        {
                            estado = string.Empty;
                            if (item.Activo)
                            {
                                shtml.Append("<tr class='k-grid-content'>");
                                shtml.AppendFormat("<td >{0}</td>", item.Id);
                                shtml.AppendFormat("<td >{0}</td>", item.Gasto_Des);
                                shtml.AppendFormat("<td >{0:C}</td>", item.Monto_Solicitado);
                                shtml.AppendFormat("<td  style='display:none;'>{0}</td>", item.Monto_Solicitado);
                                shtml.AppendFormat("<td >{0:C}</td>", item.Monto_Aprobado);
                                shtml.AppendFormat("<td  style='display:none;'>{0}</td>", item.Monto_Aprobado);
                                shtml.AppendFormat("<td >{0:C}</td>", item.Monto_Gastado);

                                if (item.Monto_Aprobado == 0)
                                    estado = Constantes.EstadoDetReq.DENEGADO;
                                else if (item.Monto_Aprobado < item.Monto_Solicitado)
                                    estado = Constantes.EstadoDetReq.APROBADO_PARCIAL;
                                else if (item.Monto_Aprobado >= item.Monto_Solicitado)
                                    estado = Constantes.EstadoDetReq.APROBADO;
                                shtml.AppendFormat("<td >{0}</td>", estado);

                                shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                                shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                                shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                                shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                                shtml.AppendFormat("<td  style='display:none;'>{0}</td>", item.Activo ? "Activo" : "Inactivo");
                                if (Constantes.EstadoReqDinero.ATENDIDO != estadoReq)
                                {
                                    shtml.AppendFormat("<td style='width:30px'> <a href=# onclick='updAddDetalleApro({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                                    //shtml.AppendFormat("                        <a href=# onclick='delAddDetalleApro({0});'><img src='../Images/iconos/{1}'      title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Gasto" : "Activar Gasto");
                                    shtml.Append("</td>");
                                }
                                shtml.Append("</tr>");
                            }
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarDetalleApro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //*************************************************************
        [HttpPost]
        public JsonResult InsertarApro(BERequerimientoDinero req)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    req.OWNER = GlobalVars.Global.OWNER;
                    req.DetalleGasto = obtenerDetallesApro();
                    if (req.MNR_ID == 0)
                    {
                        req.LOG_USER_CREAT = UsuarioActual;
                        var datos = new BLRequerimientoDinero().Insertar(req);
                    }
                    else
                    {
                        req.LOG_USER_UPDATE = UsuarioActual;

                        //.setting  valor eliminar
                        List<BEDetalleGasto> listaDetalleDel = null;
                        if (DetallesTmpDelBDApro != null)
                        {
                            listaDetalleDel = new List<BEDetalleGasto>();
                            DetallesTmpDelBDApro.ForEach(x => { listaDetalleDel.Add(new BEDetalleGasto { MNR_DET_ID = x.Id }); });
                        }
                        //setting valor activar
                        List<BEDetalleGasto> listaDetallesUpdEst = null;
                        if (DetallesTmpUPDEstadoApro != null)
                        {
                            listaDetallesUpdEst = new List<BEDetalleGasto>();
                            DetallesTmpUPDEstadoApro.ForEach(x => { listaDetallesUpdEst.Add(new BEDetalleGasto { MNR_DET_ID = x.Id }); });
                        }

                        var datos = new BLRequerimientoDinero().Actualizar_Estado(req,
                                                                 listaDetalleDel, listaDetallesUpdEst
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "InsertarApro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerApro(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BERequerimientoDinero req = new BERequerimientoDinero();
                    req = new BLRequerimientoDinero().Obtener(GlobalVars.Global.OWNER, id);

                    if (req != null)
                    {
                        req.FECHA = req.MNR_DATE.ToShortDateString();
                        if (req.DetalleGasto != null)
                        {
                            detallesApro = new List<DTODetalleGasto>();
                            if (req.DetalleGasto != null)
                            {
                                req.DetalleGasto.ForEach(s =>
                                {
                                    detallesApro.Add(new DTODetalleGasto
                                    {
                                        Id = s.MNR_DET_ID,
                                        ReqGasto_Id = s.MNR_ID,
                                        Tipo_Id = s.EXP_TYPE,
                                        Grupo_Id = s.EXPG_ID,
                                        Gasto_Id = s.EXP_ID,
                                        Gasto_Des = s.EXP_DESC,
                                        Monto_Solicitado = s.EXP_VAL_PRE,
                                        Monto_Aprobado = s.EXP_VAL_APR,
                                        Monto_Gastado = s.EXP_VAL_CON,

                                        UsuarioCrea = s.LOG_USER_CREAT,
                                        FechaCrea = s.LOG_DATE_CREAT,
                                        UsuarioModifica = s.LOG_USER_UPDAT,
                                        FechaModifica = s.LOG_DATE_UPDATE,

                                        EnBD = true,
                                        Activo = s.ENDS.HasValue ? false : true
                                    });
                                });
                                DetallesTmpApro = detallesApro;
                            }
                        }

                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(req, JsonRequestBehavior.AllowGet);
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado la definición de gasto";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerApro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region RendirDetalle

        public List<DTODetalleGasto> DetallesTmpRendir
        {
            get
            {
                return (List<DTODetalleGasto>)Session[K_SESION_DETALLE_RENDIR_GASTO];
            }
            set
            {
                Session[K_SESION_DETALLE_RENDIR_GASTO] = value;
            }
        }

        private List<DTODetalleGasto> DetallesTmpUPDEstadoRendir
        {
            get
            {
                return (List<DTODetalleGasto>)Session[K_SESION_DETALLE_RENDIR_GASTO_ACT];
            }
            set
            {
                Session[K_SESION_DETALLE_RENDIR_GASTO_ACT] = value;
            }
        }

        private List<DTODetalleGasto> DetallesTmpDelBDRendir
        {
            get
            {
                return (List<DTODetalleGasto>)Session[K_SESION_DETALLE_RENDIR_GASTO_DEL];
            }
            set
            {
                Session[K_SESION_DETALLE_RENDIR_GASTO_DEL] = value;
            }
        }

        [HttpPost]
        public JsonResult AddDetalleRendir(DTODetalleGasto detalleRendir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    detallesRendir = DetallesTmpRendir;
                    if (detallesRendir == null) detallesRendir = new List<DTODetalleGasto>();

                    if (Convert.ToInt32(detalleRendir.Id) <= 0)
                    {
                        decimal nuevoId = 1;
                        if (detallesRendir.Count > 0) nuevoId = detallesRendir.Max(x => x.Id) + 1;
                        detalleRendir.Id = nuevoId;
                        detalleRendir.UsuarioCrea = UsuarioActual;
                        detalleRendir.FechaCrea = DateTime.Now;
                        detalleRendir.Activo = true;
                        detalleRendir.EnBD = false;
                        detallesRendir.Add(detalleRendir);
                    }
                    else
                    {
                        var item = detallesRendir.Where(x => x.Id == detalleRendir.Id).FirstOrDefault();
                        detalleRendir.EnBD = item.EnBD;//indicador que item viene de la BD
                        detalleRendir.Activo = item.Activo;
                        detalleRendir.UsuarioCrea = item.UsuarioCrea;
                        detalleRendir.FechaCrea = item.FechaCrea;
                        if (item.EnBD)
                        {
                            detalleRendir.UsuarioModifica = UsuarioActual;
                            detalleRendir.FechaModifica = DateTime.Now;
                        }
                        detalleRendir.Monto_Solicitado = item.Monto_Solicitado;
                        detalleRendir.Monto_Aprobado = item.Monto_Aprobado;
                        detallesRendir.Remove(item);
                        detallesRendir.Add(detalleRendir);
                    }
                    DetallesTmpRendir = detallesRendir;
                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddDetalleRendir", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddDetalleRendir(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    detallesRendir = DetallesTmpRendir;
                    if (detallesRendir != null)
                    {
                        var objDel = detallesRendir.Where(x => x.Id == id).FirstOrDefault();
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (DetallesTmpUPDEstadoRendir == null) DetallesTmpUPDEstadoRendir = new List<DTODetalleGasto>();
                                if (DetallesTmpDelBDRendir == null) DetallesTmpDelBDRendir = new List<DTODetalleGasto>();

                                var itemUpd = DetallesTmpUPDEstadoRendir.Where(x => x.Id == id).FirstOrDefault();
                                var itemDel = DetallesTmpDelBDRendir.Where(x => x.Id == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) DetallesTmpUPDEstadoRendir.Add(objDel);
                                    if (itemDel != null) DetallesTmpDelBDRendir.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) DetallesTmpDelBDRendir.Add(objDel);
                                    if (itemUpd != null) DetallesTmpUPDEstadoRendir.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                detallesRendir.Remove(objDel);
                                detallesRendir.Add(objDel);
                            }
                            else
                            {
                                detallesRendir.Remove(objDel);
                            }
                            DetallesTmpRendir = detallesRendir;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddDetalleRendir", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BEDetalleGasto> obtenerDetallesRendir()
        {
            List<BEDetalleGasto> datos = new List<BEDetalleGasto>();
            if (DetallesTmpRendir != null)
            {
                DetallesTmpRendir.ForEach(x =>
                {
                    datos.Add(new BEDetalleGasto
                    {
                        MNR_DET_ID = x.Id,
                        MNR_ID = x.ReqGasto_Id,
                        OWNER = GlobalVars.Global.OWNER,
                        EXP_TYPE = x.Tipo_Id,
                        EXPG_ID = x.Grupo_Id,
                        EXP_ID = x.Gasto_Id,
                        EXP_DESC = x.Gasto_Des,
                        EXP_VAL_PRE = x.Monto_Solicitado,
                        EXP_VAL_APR = x.Monto_Aprobado,
                        EXP_VAL_CON = x.Monto_Gastado,
                        LOG_USER_CREAT = x.UsuarioCrea,
                        LOG_USER_UPDAT = x.UsuarioModifica

                    });
                });
            }
            return datos;
        }

        public JsonResult ObtieneDetalleTmpRendir(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var param = DetallesTmpRendir.Where(x => x.Id == idDir).FirstOrDefault();
                    retorno.data = Json(param, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneDetalleTmpRendir", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarDetalleRendir(decimal estadoReq)
        {
            string estado;
            detallesRendir = DetallesTmpRendir;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tblUsuario' border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr><th class='k-header' >Id</th><th  class='k-header'>Gastos</th>");
                    shtml.Append("<th class='k-header'>S/. Solicitado</th>");
                    shtml.Append("<th  style='display:none;' class='k-header'>Solicitado</th>");
                    shtml.Append("<th class='k-header'>S/. Aprobado</th>");
                    shtml.Append("<th  style='display:none;' class='k-header'>Aprobado</th>");
                    shtml.Append("<th class='k-header'>S/. Gastado</th>");
                    shtml.Append("<th  style='display:none;' class='k-header'>Gastado</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header'>Usuario Creación</th>");
                    shtml.Append("<th class='k-header'>Fecha Creación</th>");
                    shtml.Append("<th class='k-header'>Usuario Modificación</th>");
                    shtml.Append("<th class='k-header'>Fecha Modificación</th>");
                    shtml.Append("<th style='display:none;' class='k-header'>Estado</th>");
                    //if (Constantes.EstadoReqDinero.ATENDIDO != estadoReq)
                    shtml.Append("<th class='k-header'></th>");

                    shtml.Append("</tr></thead>");

                    if (detallesRendir != null)
                    {
                        foreach (var item in detallesRendir.OrderBy(x => x.Id))
                        {
                            estado = string.Empty;
                            if (item.Activo)
                            {
                                shtml.Append("<tr class='k-grid-content'>");
                                shtml.AppendFormat("<td >{0}</td>", item.Id);
                                shtml.AppendFormat("<td >{0}</td>", item.Gasto_Des);
                                shtml.AppendFormat("<td >{0:C}</td>", item.Monto_Solicitado);
                                shtml.AppendFormat("<td  style='display:none;'>{0}</td>", item.Monto_Solicitado);
                                shtml.AppendFormat("<td >{0:C}</td>", item.Monto_Aprobado);
                                shtml.AppendFormat("<td  style='display:none;'>{0}</td>", item.Monto_Aprobado);
                                shtml.AppendFormat("<td >{0:C}</td>", item.Monto_Gastado);
                                shtml.AppendFormat("<td  style='display:none;'>{0}</td>", item.Monto_Gastado);
                                if (item.Monto_Aprobado == 0)
                                    estado = Constantes.EstadoDetReq.DENEGADO;
                                else if (item.Monto_Aprobado < item.Monto_Solicitado)
                                    estado = Constantes.EstadoDetReq.APROBADO_PARCIAL;
                                else if (item.Monto_Aprobado >= item.Monto_Solicitado)
                                    estado = Constantes.EstadoDetReq.APROBADO;
                                shtml.AppendFormat("<td >{0}</td>", estado);

                                shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                                shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                                shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                                shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                                shtml.AppendFormat("<td  style='display:none;'>{0}</td>", item.Activo ? "Activo" : "Inactivo");
                                //if (Constantes.EstadoReqDinero.ATENDIDO != estadoReq)
                                //{
                                shtml.AppendFormat("<td style='width:30px'> <a href=# onclick='updAddDetalleRendir({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.Id);
                                //shtml.AppendFormat("                        <a href=# onclick='delAddDetalleApro({0});'><img src='../Images/iconos/{1}'      title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Gasto" : "Activar Gasto");
                                shtml.Append("</td>");
                                //}
                                shtml.Append("</tr>");
                            }
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarDetalleRendir", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //*************************************************************
        [HttpPost]
        public JsonResult InsertarRendir(BERequerimientoDinero req)
        {
            Resultado retorno = new Resultado();
            BELegalizacion leg = new BELegalizacion();
            try
            {
                if (!isLogout(ref retorno))
                {
                    req.OWNER = GlobalVars.Global.OWNER;
                    req.DetalleGasto = obtenerDetallesRendir();
                    if (req.MNR_ID == 0)
                    {
                        req.LOG_USER_CREAT = UsuarioActual;
                        var datos = new BLRequerimientoDinero().Insertar(req);
                    }
                    else
                    {
                        req.LOG_USER_UPDATE = UsuarioActual;

                        leg.OWNER = GlobalVars.Global.OWNER;
                        leg.MNR_ID = req.MNR_ID;
                        if (req.MNR_VALUE_CON < req.MNR_VALUE_APR)
                        {
                            leg.LEG_ADJ = Constantes.SaldoLegalizacion.FAVOR;
                            leg.MNR_VALUE_ADJ = req.MNR_VALUE_APR - req.MNR_VALUE_CON;
                        }
                        else if (req.MNR_VALUE_CON > req.MNR_VALUE_APR)
                        {
                            leg.LEG_ADJ = Constantes.SaldoLegalizacion.CONTRA;
                            leg.MNR_VALUE_ADJ = req.MNR_VALUE_CON - req.MNR_VALUE_APR;
                        }
                        else
                        {
                            leg.LEG_ADJ = Constantes.SaldoLegalizacion.IGUAL;
                            leg.MNR_VALUE_ADJ = 0;
                        }


                        leg.LOG_USER_CREAT = UsuarioActual;
                        var datos = new BLRequerimientoDinero().Actualizar_Estado_Rendir(req, leg);
                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "InsertarApro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerRendir(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BERequerimientoDinero req = new BERequerimientoDinero();
                    req = new BLRequerimientoDinero().Obtener(GlobalVars.Global.OWNER, id);

                    if (req != null)
                    {
                        req.FECHA = req.MNR_DATE.ToShortDateString();
                        if (req.DetalleGasto != null)
                        {
                            detallesRendir = new List<DTODetalleGasto>();
                            if (req.DetalleGasto != null)
                            {
                                req.DetalleGasto.ForEach(s =>
                                {
                                    detallesRendir.Add(new DTODetalleGasto
                                    {
                                        Id = s.MNR_DET_ID,
                                        ReqGasto_Id = s.MNR_ID,
                                        Tipo_Id = s.EXP_TYPE,
                                        Grupo_Id = s.EXPG_ID,
                                        Gasto_Id = s.EXP_ID,
                                        Gasto_Des = s.EXP_DESC,
                                        Monto_Solicitado = s.EXP_VAL_PRE,
                                        Monto_Aprobado = s.EXP_VAL_APR,
                                        Monto_Gastado = s.EXP_VAL_CON,

                                        UsuarioCrea = s.LOG_USER_CREAT,
                                        FechaCrea = s.LOG_DATE_CREAT,
                                        UsuarioModifica = s.LOG_USER_UPDAT,
                                        FechaModifica = s.LOG_DATE_UPDATE,

                                        EnBD = true,
                                        Activo = s.ENDS.HasValue ? false : true
                                    });
                                });
                                DetallesTmpRendir = detallesRendir;
                            }
                        }

                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(req, JsonRequestBehavior.AllowGet);
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se ha encontrado la definición de gasto";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerApro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion


    }
}

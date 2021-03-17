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
    public class ImpuestoValorController : Base
    {
        private const string K_SESSION_VALOR_IMP = "___DTOValor_IMP";
        private const string K_SESSION_VALOR_DEL_IMP = "___DTOValorDEL_IMP";
        private const string K_SESSION_VALOR_ACT_IMP = "___DTOValorACT_IMP";
        //
        // GET: /ImpuestoValor/
        List<DTOImpuestoValor> valores = new List<DTOImpuestoValor>();
        
        public const string nomAplicacion = "SRGDA";

        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Init(false);
            Session.Remove(K_SESSION_VALOR_IMP);
            Session.Remove(K_SESSION_VALOR_DEL_IMP);
            Session.Remove(K_SESSION_VALOR_ACT_IMP);
            return View();
        }

        public JsonResult Listar(int skip, int take, int page, int pageSize, string group, string dato, decimal territorio, int estado)
        {
            var lista = ImpuestoListar(GlobalVars.Global.OWNER, dato, territorio, estado, page, pageSize);
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEREC_TAXES { RECTAXES = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEREC_TAXES { RECTAXES = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<BEREC_TAXES> ImpuestoListar(string owner, string descripcion, decimal territorio, int estado, int pagina, int cantRegxPag)
        {
            return new BLREC_TAXES().Listar(owner, descripcion, territorio, estado, pagina, cantRegxPag);
        }

        [HttpPost]
        public JsonResult Eliminar(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var servicio = new BLREC_TAXES();
                    var impuesto = new BEREC_TAXES();

                    impuesto.OWNER = GlobalVars.Global.OWNER;
                    impuesto.TAX_ID = id;
                    impuesto.LOG_USER_UPDATE = UsuarioActual;
                    servicio.Eliminar(impuesto);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
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

        [HttpPost()]
        public JsonResult ObtenerXDescripcion(BEREC_TAXES impuesto)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLREC_TAXES servicio = new BLREC_TAXES();
                    impuesto.OWNER = GlobalVars.Global.OWNER;
                    int resultado = servicio.ObtenerXDescripcion(impuesto);
                    if (resultado >= 1)
                        retorno.result = 1;
                    else
                        retorno.result = 0;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerXDescripcion", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpGet()]
        public JsonResult Obtener(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLREC_TAXES servicio = new BLREC_TAXES();
                    //BEREC_TAXES impuesto = new BEREC_TAXES();
                    var impuesto = servicio.Obtener(GlobalVars.Global.OWNER, id);
                    if (impuesto != null)
                    {

                        if (impuesto.Valores != null)
                        {
                            valores = new List<DTOImpuestoValor>();
                            if (impuesto.Valores != null)
                            {
                                impuesto.Valores.ForEach(s =>
                                {
                                    valores.Add(new DTOImpuestoValor
                                    {
                                        Id = s.TAXV_ID,
                                        IdDivision = s.DIV_ID,
                                        IdImpuesto = s.TAX_ID,
                                        Division = s.DIVISION,
                                        ValorPorcentaje = s.TAXV_VALUEP,
                                        ValorMonto = s.TAXV_VALUEM,
                                        FechaVigencia = s.FechaVigencia,
                                        UsuarioCrea = s.LOG_USER_CREAT,
                                        FechaCrea = s.LOG_DATE_CREAT,
                                        UsuarioModifica = s.LOG_USER_UPDAT,
                                        FechaModifica = s.LOG_DATE_UPDATE,

                                        EnBD = true,
                                        Activo = s.ENDS.HasValue ? false : true
                                    });
                                });
                                ValoresTmp = valores;
                            }
                        }

                    }
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(impuesto, JsonRequestBehavior.AllowGet);
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

        [HttpPost()]
        public JsonResult Insertar(BEREC_TAXES impuesto)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    bool valResultado;
                    string valMsg;
                    validarInsert(out valResultado, out valMsg, impuesto);
                    if (valResultado)
                    {
                        impuesto.OWNER = GlobalVars.Global.OWNER;
                        impuesto.Valores = obtenerValores();

                        if (impuesto.TAX_ID == 0)
                        {
                            impuesto.LOG_USER_CREAT = UsuarioActual;
                            var datos = new BLREC_TAXES().Insertar(impuesto);
                        }
                        else
                        {
                            impuesto.LOG_USER_UPDATE = UsuarioActual;

                            //.setting  valor eliminar
                            List<BEImpuestoValor> listaValDel = null;
                            if (ValoresTmpDelBD != null)
                            {
                                listaValDel = new List<BEImpuestoValor>();
                                ValoresTmpDelBD.ForEach(x => { listaValDel.Add(new BEImpuestoValor { TAXV_ID = x.Id }); });
                            }
                            //setting valor activar
                            List<BEImpuestoValor> listaValUpdEst = null;
                            if (ValoresTmpUPDEstado != null)
                            {
                                listaValUpdEst = new List<BEImpuestoValor>();
                                ValoresTmpUPDEstado.ForEach(x => { listaValUpdEst.Add(new BEImpuestoValor { TAXV_ID = x.Id }); });
                            }

                            var datos = new BLREC_TAXES().Actualizar(impuesto,
                                                                     listaValDel, listaValUpdEst
                                                                    );
                        }
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = valMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "Insertar", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private static void validarInsert(out bool exito, out string msg_validacion, BEREC_TAXES entidad)
        {
            exito = true;
            msg_validacion = string.Empty;
            if (exito && string.IsNullOrEmpty(entidad.DESCRIPTION))
            {
                msg_validacion = "Ingrese descripción.";
                exito = false;
            }
        }


        #region VALOR

        public List<DTOImpuestoValor> ValoresTmp
        {
            get
            {
                return (List<DTOImpuestoValor>)Session[K_SESSION_VALOR_IMP];
            }
            set
            {
                Session[K_SESSION_VALOR_IMP] = value;
            }
        }

        private List<DTOImpuestoValor> ValoresTmpUPDEstado
        {
            get
            {
                return (List<DTOImpuestoValor>)Session[K_SESSION_VALOR_ACT_IMP];
            }
            set
            {
                Session[K_SESSION_VALOR_ACT_IMP] = value;
            }
        }

        private List<DTOImpuestoValor> ValoresTmpDelBD
        {
            get
            {
                return (List<DTOImpuestoValor>)Session[K_SESSION_VALOR_DEL_IMP];
            }
            set
            {
                Session[K_SESSION_VALOR_DEL_IMP] = value;
            }
        }

        [HttpPost]
        public JsonResult AddValor(DTOImpuestoValor entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    //if (valores == null) valores = new List<DTOImpuestoValor>();
                    int registroNuevo = 0;
                    int registroModificar = 0;
                    //caracteristicas = CaracteristicasTmp;
                    valores = ValoresTmp;
                    if (valores != null)
                    {
                        registroNuevo = valores.Where(p => p.IdDivision == entidad.IdDivision && entidad.Id == 0).Count();
                        registroModificar = valores.Where(p => p.IdDivision == entidad.IdDivision && p.Id == entidad.Id).Count();
                    }
                    else
                    {
                        valores = new List<DTOImpuestoValor>();
                    }

                    if ((entidad.Id == 0 && registroNuevo == 0)
                         || (entidad.Id != 0 && registroModificar > 0)
                       )
                    {

                        if (Convert.ToInt32(entidad.Id) <= 0)
                        {
                            decimal nuevoId = 1;
                            if (valores.Count > 0) nuevoId = valores.Max(x => x.Id) + 1;
                            entidad.Id = nuevoId;
                            entidad.UsuarioCrea = UsuarioActual;
                            entidad.FechaCrea = DateTime.Now;
                            entidad.Activo = true;
                            entidad.EnBD = false;
                            valores.Add(entidad);
                        }
                        else
                        {
                            var item = valores.Where(x => x.Id == entidad.Id).FirstOrDefault();
                            entidad.UsuarioCrea = UsuarioActual;
                            entidad.EnBD = item.EnBD;//indicador que item viene de la BD
                            entidad.Activo = item.Activo;
                            entidad.UsuarioCrea = item.UsuarioCrea;
                            entidad.FechaCrea = item.FechaCrea;
                            entidad.UsuarioModifica = UsuarioActual;
                            entidad.FechaModifica = DateTime.Now;
                            valores.Remove(item);
                            valores.Add(entidad);
                        }
                        ValoresTmp = valores;
                        retorno.result = 1;
                        retorno.message = "OK";

                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "La valor ya existe.";
                    }

                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddValor", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddValor(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    valores = ValoresTmp;
                    if (valores != null)
                    {
                        var objDel = valores.Where(x => x.Id == id).FirstOrDefault();
                        if (objDel != null)
                        {
                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (ValoresTmpUPDEstado == null) ValoresTmpUPDEstado = new List<DTOImpuestoValor>();
                                if (ValoresTmpDelBD == null) ValoresTmpDelBD = new List<DTOImpuestoValor>();

                                var itemUpd = ValoresTmpUPDEstado.Where(x => x.Id == id).FirstOrDefault();
                                var itemDel = ValoresTmpDelBD.Where(x => x.Id == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) ValoresTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) ValoresTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) ValoresTmpDelBD.Add(objDel);
                                    if (itemUpd != null) ValoresTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                valores.Remove(objDel);
                                valores.Add(objDel);
                            }
                            else
                            {
                                valores.Remove(objDel);
                            }
                            ValoresTmp = valores;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddValor", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private List<BEImpuestoValor> obtenerValores()
        {
            List<BEImpuestoValor> datos = new List<BEImpuestoValor>();
            if (ValoresTmp != null)
            {
                ValoresTmp.ForEach(x =>
                {
                    datos.Add(new BEImpuestoValor
                    {
                        OWNER = GlobalVars.Global.OWNER,
                        TAXV_ID = x.Id,
                        //TAXD_ID = x.IdDivision,
                        DIV_ID = x.IdDivision,
                        TAX_ID = x.IdImpuesto,
                        DIVISION = x.Division,
                        TAXV_VALUEP = x.ValorPorcentaje,
                        TAXV_VALUEM = x.ValorMonto,
                        START = Convert.ToDateTime(x.FechaVigencia),
                        FechaVigencia = x.FechaVigencia,
                        LOG_USER_CREAT = x.UsuarioCrea,
                        LOG_DATE_CREAT = x.FechaCrea,
                        LOG_USER_UPDAT = x.UsuarioModifica,
                        LOG_DATE_UPDATE = x.FechaModifica
                    });
                });
            }

            return datos;
        }

        public JsonResult ObtieneValorTmp(decimal idDir)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var observacion = ValoresTmp.Where(x => x.Id == idDir).FirstOrDefault();
                    retorno.data = Json(observacion, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneValorTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarValor()
        {
            valores = ValoresTmp;
            Resultado retorno = new Resultado();

            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table border=0 width='100%;' class='k-grid k-widget'>");
                    shtml.Append("<thead><tr>");
                    shtml.Append("<th class='k-header' >Id</th>");
                    shtml.Append("<th class='k-header'>División </th>");
                    shtml.Append("<th class='k-header'>Valor en % </th>");
                    shtml.Append("<th class='k-header'>Valor de Monto </th>");
                    shtml.Append("<th class='k-header'>Fecha Vigencia</th>");
                    shtml.Append("<th class='k-header'>Usuario Creación</th>");
                    shtml.Append("<th class='k-header'>Fecha Creación</th>");
                    shtml.Append("<th class='k-header'>Usuario Modificación</th>");
                    shtml.Append("<th class='k-header'>Fecha Modificación</th>");
                    shtml.Append("<th class='k-header'>Estado</th>");
                    shtml.Append("<th class='k-header' style='width:60px'></th></tr></thead>");

                    if (valores != null)
                    {
                        foreach (var item in valores.OrderBy(x => x.Id))
                        {
                            shtml.Append("<tr class='k-grid-content'>");
                            shtml.AppendFormat("<td >{0}</td>", item.Id);
                            shtml.AppendFormat("<td nowrap>{0}</td>", item.Division);
                            shtml.AppendFormat("<td >{0}</td>", "% " + item.ValorPorcentaje.ToString("#####.00"));
                            shtml.AppendFormat("<td >{0}</td>", item.ValorMonto.ToString("#####.00"));
                            shtml.AppendFormat("<td >{0}</td>", item.FechaVigencia);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaCrea);
                            shtml.AppendFormat("<td >{0}</td>", item.UsuarioModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.FechaModifica);
                            shtml.AppendFormat("<td >{0}</td>", item.Activo ? "Activo" : "Inactivo");
                            shtml.AppendFormat("<td style='width:60px'> <a href=# onclick='updAddValor({0});'><img src='../Images/iconos/edit.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.Id, "Modificar Valor");
                            shtml.AppendFormat("                        <a href=# onclick='delAddvalor({0});'><img src='../Images/iconos/{1}'      title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Valor" : "Activar Valor");
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
    }
}

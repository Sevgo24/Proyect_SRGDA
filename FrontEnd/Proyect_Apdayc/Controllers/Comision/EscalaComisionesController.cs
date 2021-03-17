using System;
using SGRDA.BL.Comision;
using SGRDA.Entities.Comision;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;
using System.Text.RegularExpressions;

namespace Proyect_Apdayc.Controllers.Comision
{
    public class EscalaComisionesController : Base
    {
        public const string nomAplicacion = "SGRDA";


        private const string K_SESSION_ESCALA_RANGO = "___DTOEscalaRango";
        private const string K_SESSION_ESCALA_RANGO_ACT = "___DTOEscalaRangoACT";
        private const string K_SESSION_ESCALA_RANGO_DEL = "___DTOEscalaRangoDEL";
        List<DTOComisionEscala> ListaEscalaComision = new List<DTOComisionEscala>();
        //
        // GET: /EscalaComisiones/

        #region VISTA
        public ActionResult Index()
        {
            Init(false);
            return View();
        }
        public ActionResult Nuevo()
        {
            Session.Remove(K_SESSION_ESCALA_RANGO);
            Session.Remove(K_SESSION_ESCALA_RANGO_ACT);
            Session.Remove(K_SESSION_ESCALA_RANGO_DEL);
            Init(false);
            return View();
        }
        #endregion

        #region AGREGAR ESCALA
        public List<DTOComisionEscala> EscalaRangoTmp
        {
            get
            {
                return (List<DTOComisionEscala>)Session[K_SESSION_ESCALA_RANGO];
            }
            set
            {
                Session[K_SESSION_ESCALA_RANGO] = value;
            }
        }

        public List<DTOComisionEscala> EscalaRangoTmpUPD
        {
            get
            {
                return (List<DTOComisionEscala>)Session[K_SESSION_ESCALA_RANGO_ACT];
            }
            set
            {
                Session[K_SESSION_ESCALA_RANGO_ACT] = value;
            }
        }

        public List<DTOComisionEscala> EscalaRangoTmpDel
        {
            get
            {
                return (List<DTOComisionEscala>)Session[K_SESSION_ESCALA_RANGO_DEL];
            }
            set
            {
                Session[K_SESSION_ESCALA_RANGO_DEL] = value;
            }
        }

        public JsonResult ObtieneUltimoOrden()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    DTOComisionEscala escala = new DTOComisionEscala();
                    if (EscalaRangoTmp == null)
                        escala.Orden = 1;
                    else
                        escala.Orden = EscalaRangoTmp.Max(p => p.Id) + 1;
                    retorno.data = Json(escala, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneUltimoOrden", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddEscalaRango(DTOComisionEscala escala)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    ListaEscalaComision = EscalaRangoTmp;
                    if (ListaEscalaComision == null) ListaEscalaComision = new List<DTOComisionEscala>();

                    bool existe = ListaEscalaComision.Where(x => x.Id == escala.Id).ToList().Count > 0 ? true : false;
                    if (!existe)
                    {
                        decimal nuevoId = 1;
                        if (ListaEscalaComision.Count > 0) nuevoId = ListaEscalaComision.Max(x => x.Id) + 1;
                        escala.Id = nuevoId;
                        escala.TipoDescripcion = escala.TipoId == Constantes.TipoEscalaRango.PORCENTAJE ? "PORCENTAJE" : "VALOR";
                        escala.FechaCrea = DateTime.Now;
                        escala.Activo = true;
                        escala.EnBD = false;
                        ListaEscalaComision.Add(escala);
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                    else
                    {
                        var item = ListaEscalaComision.Where(x => x.Id == escala.Id).FirstOrDefault();
                        escala.TipoDescripcion = escala.TipoId == Constantes.TipoEscalaRango.PORCENTAJE ? "PORCENTAJE" : "VALOR";
                        escala.EnBD = item.EnBD; //indicador que item viene de la BD
                        escala.Activo = item.Activo;
                        ListaEscalaComision.Remove(item);
                        ListaEscalaComision.Add(escala);
                        retorno.result = 1;
                        retorno.message = "Ok";
                    }
                    EscalaRangoTmp = ListaEscalaComision;
                }
            }
            catch (Exception ex)
            {
                ListaEscalaComision.Add(escala);
                EscalaRangoTmp = ListaEscalaComision;

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddEscalaRango", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult delAddEscalaRango(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    //DocCliente = DocClienteTmp;
                    ListaEscalaComision = EscalaRangoTmp;
                    if (ListaEscalaComision != null)
                    {
                        var objDel = ListaEscalaComision.Where(x => x.Id == id).FirstOrDefault();
                        if (objDel != null)
                        {

                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (EscalaRangoTmpUPD == null) EscalaRangoTmpUPD = new List<DTOComisionEscala>();
                                if (EscalaRangoTmpDel == null) EscalaRangoTmpDel = new List<DTOComisionEscala>();

                                var itemUpd = EscalaRangoTmpUPD.Where(x => x.Id == id).FirstOrDefault();
                                var itemDel = EscalaRangoTmpDel.Where(x => x.Id == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) EscalaRangoTmpUPD.Add(objDel);
                                    if (itemDel != null) EscalaRangoTmpDel.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) EscalaRangoTmpDel.Add(objDel);
                                    if (itemUpd != null) EscalaRangoTmpUPD.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                ListaEscalaComision.Remove(objDel);
                                ListaEscalaComision.Add(objDel);
                            }
                            else
                            {
                                ListaEscalaComision.Remove(objDel);
                            }
                            EscalaRangoTmp = ListaEscalaComision;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "delAddEscalaRango", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtieneEscalaRangoTmp(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var observacion = EscalaRangoTmp.Where(x => x.Id == id).FirstOrDefault();
                    retorno.data = Json(observacion, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtieneEscalaRangoTmp", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarEscalaRango(string accion)
        {
            ListaEscalaComision = EscalaRangoTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tblVoucherDetalle' border=0 width='100%;'><thead><tr>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Id</th>");
                    //shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='text-align:left; padding-left:10px;'  >Banco</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Orden </th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Desde</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Hasta</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Tipo</th>");
                    //shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='display:none;'>IdMoneda</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Valor</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='width:30px'></th></tr></thead>");
                    //if (accion == Constantes.AccionVista.Nuevo) shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='width:30px'></th></tr></thead>");

                    if (ListaEscalaComision != null)
                    {
                        foreach (var item in ListaEscalaComision.OrderBy(x => x.Orden))
                        {
                            shtml.Append("<tr style='background-color:white'>");
                            //shtml.appendformat("<td style='width:70px; text-align:center;color: black' class='tmpidvoucher'>{0}</td>", item.id);
                            shtml.AppendFormat("<td style='width:200px;text-align:center;  color: black'>{0}</td>", item.Id);
                            shtml.AppendFormat("<td style='width:200px;text-align:center;  color: black'>{0}</td>", item.Orden);
                            shtml.AppendFormat("<td style='width:200px;text-align:center;  color: black'>{0}</td>", item.Desde);
                            shtml.AppendFormat("<td style='width:200px;text-align:center;  color: black'>{0}</td>", item.Hasta);
                            shtml.AppendFormat("<td style='width:200px;text-align:center;  color: black'>{0}</td>", item.TipoDescripcion);
                            shtml.AppendFormat("<td style='width:200px;text-align:center;  color: black'>{0}</td>", item.Valor);
                            //    shtml.AppendFormat("<td style='width:30px;text-align:center;'> <a href=# onclick='delAddCliente({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Cliente" : "Activar Cliente");
                            shtml.AppendFormat("<td style='width:80px'>");
                            shtml.AppendFormat("                        <a href=# onclick='updAddEscalaRango({0});'><img src='../Images/iconos/edit.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.Id, "Modificar escala.");
                            shtml.AppendFormat("                        <a href=# onclick='delAddEscalaRango({0});'><img src='../Images/iconos/{1}'      title='{2}' border=0></a>", item.Id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar escala." : "Activar escala.");
                            shtml.Append("</td>");

                            shtml.Append("</td></tr>");
                            shtml.Append("</tr>");

                            //shtml.appendformat("<td style='width:120px;text-align:right;padding-right:25px;  color: black'>{0}</td>", item.valoringreso.tostring("# ###,##0.000"));
                            //shtml.appendformat("<td style='display:none;'  class='tmpmontovoucher'>{0}</td>", item.valoringreso);

                            //string color = item.confirmacioningreso == clases.constantes.estadosconfirmacion.confirmacion ? "green" : "red";
                            //shtml.appendformat("<td style='width:130px; text-align:center; color: " + color + " '>{0}</td>", item.confirmacioningresodesc);
                            //// font-weight:bold;

                            //if (accion == constantes.accionvista.nuevo)
                            //    shtml.appendformat("<td style='width:30px;text-align:center;'> <a href=# onclick='deladdvoucherdet({0});'> <img src='../images/iconos/{1}' title='{2}' border=0></a>", item.id, item.activo ? "delete.png" : "activate.png", item.activo ? "eliminar factura" : "activar factura");

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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarEscalaRango", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        /*
        [HttpPost]
        public JsonResult DellAddVoucherDet(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    DocVoucherDet = VoucherDetTmp;
                    if (DocVoucherDet != null)
                    {
                        var objDel = DocVoucherDet.Where(x => x.id == id).FirstOrDefault();
                        if (objDel != null)
                        {

                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (VoucherDetTmpUPDEstado == null) VoucherDetTmpUPDEstado = new List<DTOBecVoucherDetalle>();
                                if (VoucherDetTmpDelBD == null) VoucherDetTmpDelBD = new List<DTOBecVoucherDetalle>();

                                var itemUpd = VoucherDetTmpUPDEstado.Where(x => x.id == id).FirstOrDefault();
                                var itemDel = VoucherDetTmpDelBD.Where(x => x.id == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) VoucherDetTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) VoucherDetTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) VoucherDetTmpDelBD.Add(objDel);
                                    if (itemUpd != null) VoucherDetTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                DocVoucherDet.Remove(objDel);
                                DocVoucherDet.Add(objDel);
                            }
                            else
                            {
                                DocVoucherDet.Remove(objDel);
                            }
                            VoucherDetTmp = DocVoucherDet;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddVoucherDet", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActualizarMonedaComprobante(string IdMoneda, string Moneda)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    DocVoucherDet = VoucherDetTmp;
                    foreach (var item in DocVoucherDet)
                    {
                        item.IdMoneda = IdMoneda;
                        item.Moneda = Moneda;
                    }
                    retorno.result = 1;
                    retorno.message = "OK";
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "No se logro actualizar los comprobantes.";
                }
            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ActualizarMonedaComprobante", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarVoucherDet(string accion)
        {
            DocVoucherDet = VoucherDetTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tblVoucherDetalle' border=0 width='100%;'><thead><tr>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Id</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='text-align:left; padding-left:10px;'  >Banco</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Cuenta</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Tipo Pago</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Fecha Depósito</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Código de Depósito</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='display:none;'>IdMoneda</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Moneda</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Monto de Depósito</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='display:none;'  >Monto</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Estado de Confirmación</th>");
                    if (accion == Constantes.AccionVista.Nuevo) shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='width:30px'></th></tr></thead>");

                    if (DocVoucherDet != null)
                    {
                        foreach (var item in DocVoucherDet.OrderBy(x => x.id))
                        {
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.AppendFormat("<td style='width:70px; text-align:center;color: black' class='tmpIdVoucher'>{0}</td>", item.id);
                            shtml.AppendFormat("<td style='width:230px;text-align:left; color: black'>{0}</td>", item.Banco);
                            shtml.AppendFormat("<td style='width:200px;text-align:center;  color: black'>{0}</td>", item.CuentaBancaria);
                            shtml.AppendFormat("<td style='width:200px;text-align:center;  color: black'>{0}</td>", item.TipoPago);
                            shtml.AppendFormat("<td style='width:100px;text-align:center;  color: black'>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.fechaDeposito));
                            shtml.AppendFormat("<td style='width:160px;text-align:center;  color: black'>{0}</td>", item.Voucher);
                            shtml.AppendFormat("<td style='display:none;' width:160px;text-align:center;  color: black' class='tmpIdMoneda'>{0}</td>", item.IdMoneda);
                            shtml.AppendFormat("<td style='width:140px;text-align:center;  color: black'>{0}</td>", item.Moneda);
                            shtml.AppendFormat("<td style='width:120px;text-align:right;padding-right:25px;  color: black'>{0}</td>", item.valorIngreso.ToString("# ###,##0.000"));
                            shtml.AppendFormat("<td style='display:none;'  class='tmpMontoVoucher'>{0}</td>", item.valorIngreso);

                            string color = item.confirmacionIngreso == Clases.Constantes.EstadosConfirmacion.CONFIRMACION ? "Green" : "Red";
                            shtml.AppendFormat("<td style='width:130px; text-align:center; color: " + color + " '>{0}</td>", item.confirmacionIngresoDesc);
                            // font-weight:bold;

                            if (accion == Constantes.AccionVista.Nuevo)
                                shtml.AppendFormat("<td style='width:30px;text-align:center;'> <a href=# onclick='delAddVoucherDet({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Factura" : "Activar Factura");
                            shtml.Append("</td></tr>");
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarVoucherDet", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        */
        #endregion


        public List<BEComisionEscalaRango> ObtenerRangoDetalle()
        {
            List<BEComisionEscalaRango> ListaRangoDetalle = new List<BEComisionEscalaRango>();
            foreach (var item in EscalaRangoTmp)
            {
                BEComisionEscalaRango detalle = new BEComisionEscalaRango();
                detalle.OWNER = GlobalVars.Global.OWNER;
                detalle.SET_ID = 0;
                detalle.PRG_ORDER = item.Orden;
                detalle.PRG_VALUEI = item.Desde;
                detalle.PRG_VALUEF = item.Hasta;
                if (item.TipoId == Constantes.TipoEscalaRango.PORCENTAJE)
                    detalle.PRG_PERC = item.Valor;
                else
                    detalle.PRG_VALUEC = item.Valor;
                detalle.LOG_USER_CREAT = UsuarioActual;
                ListaRangoDetalle.Add(detalle);
            }
            return ListaRangoDetalle;
        }

        #region REGISTRAR

        [HttpPost]
        public JsonResult Insertar(BEComisionEscala ComisionEscala)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    ComisionEscala.OWNER = GlobalVars.Global.OWNER;
                    
                    List<BEComisionEscalaRango> ListaRangoDetalle = new List<BEComisionEscalaRango>();
                    ListaRangoDetalle = ObtenerRangoDetalle();
                    if (ComisionEscala.SET_ID == 0)
                    {
                        ComisionEscala.LOG_USER_CREAT = UsuarioActual;
                        var dato = new BLComisionEscala().Insertar(ComisionEscala, ListaRangoDetalle);
                    }
                    else
                    {
                        ComisionEscala.LOG_USER_UPDAT = UsuarioActual;
                        var dato = new BLComisionEscala().Actualizar(ComisionEscala, ListaRangoDetalle);
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

        public JsonResult Obtener(decimal Id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var escalaCabecera = new BLComisionEscala().ObtenerComision(GlobalVars.Global.OWNER, Id);
                    if (escalaCabecera.ListaComisionRango.Count > 0)
                    {
                        EscalaRangoTmp = new List<DTOComisionEscala>();
                        DTOComisionEscala detalle = null;
                        foreach (var rango in escalaCabecera.ListaComisionRango)
                        {
                            detalle = new DTOComisionEscala();
                            detalle.Id = rango.RANG_ID;
                            detalle.IdComisionEscala = rango.SET_ID;
                            detalle.Orden = rango.PRG_ORDER;
                            detalle.Desde = rango.PRG_VALUEI;
                            detalle.Hasta = rango.PRG_VALUEF;
                            if (rango.PRG_PERC != null && rango.PRG_PERC != 0)
                            {
                                detalle.TipoId = Constantes.TipoEscalaRango.PORCENTAJE;
                                detalle.TipoDescripcion = "PORCENTAJE";
                                detalle.Valor = rango.PRG_PERC;
                            }
                            else if (rango.PRG_VALUEF != null && rango.PRG_VALUEF != 0)
                            {
                                detalle.TipoId = Constantes.TipoEscalaRango.VALOR;
                                detalle.TipoDescripcion = "VALOR";
                                detalle.Valor = rango.PRG_VALUEF;
                            }
                            detalle.FechaCrea = rango.LOG_DATE_CREAT;
                            detalle.Activo = true;
                            detalle.EnBD = true;
                            EscalaRangoTmp.Add(detalle);
                        }
                    }
                    retorno.data = Json(escalaCabecera, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
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

    }
}

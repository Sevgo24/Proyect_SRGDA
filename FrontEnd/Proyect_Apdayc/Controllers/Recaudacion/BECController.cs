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
using SGRDA.BL.BLAlfresco;
using System.IO;

namespace Proyect_Apdayc.Controllers.Recaudacion
{
    public class BECController : Base
    {
        public const string nomAplicacion = "SRGDA";

        private const string K_SESSION_DOC_VOUCHER_DET = "___DTODocVoucher_Det";
        private const string K_SESSION_DOC_VOUCHER_DET_DEL = "___DTODocVoucherDEL_Det";
        private const string K_SESSION_DOC_VOUCHER_DET_ACT = "___DTODocVoucherACT_Det";
        List<DTOBecVoucherDetalle> DocVoucherDet = new List<DTOBecVoucherDetalle>();
        private static string MREC_ID = "";

        private const string K_SESSION_DOC_CLIENTE = "___DTODocCliente_Det";
        private const string K_SESSION_DOC_CLIENTE_DEL = "___DTODocClienteDEL_Det";
        private const string K_SESSION_DOC_CLIENTE_ACT = "___DTODocClienteACT_Det";
        List<DTOSocio> DocCliente = new List<DTOSocio>();

        private const string K_SESSION_DOC_FACTURA_DET = "___DTODocFactura_Det";
        private const string K_SESSION_DOC_FACTURA_DET_DEL = "___DTODocFacturaDEL_Det";
        private const string K_SESSION_DOC_FACTURA_DET_ACT = "___DTODocFacturaACT_Det";
        List<DTOBecDetalle> DocFacturaDet = new List<DTOBecDetalle>();

        public class Variables
        {
            public const int SI = 1;
            public const int NO = 0;
            public const string MSG_COBRO_NO_PASO_VALIDACION_MONTO = "LA DIFERENCIA DE MONTOS ENTRE LO DEPOSITADO Y LO APLICADO EXCEDE EL VALOR DE 1 SOL , POR FAVOR DE SOLICITAR EL PERMISO POR PARTE DE GENAREC";
            public const string MSG_OCURRIO_UN_ERROR_AL_VALIDAR = "HA OCURRIDO UN ERROR AL VALIDAR EL COBRO | COMUNICARSE CON EL RESPONSABLE DEL MODULO";
        }


        // GET: /BEC/
        #region VISTAS
        public ActionResult Index()
        {
            Init(false);
            return View();
        }

        public ActionResult Nuevo()
        {
            Session.Remove(K_SESSION_DOC_VOUCHER_DET);
            Session.Remove(K_SESSION_DOC_VOUCHER_DET_DEL);
            Session.Remove(K_SESSION_DOC_VOUCHER_DET_ACT);

            Session.Remove(K_SESSION_DOC_CLIENTE);
            Session.Remove(K_SESSION_DOC_CLIENTE_DEL);
            Session.Remove(K_SESSION_DOC_CLIENTE_ACT);

            Session.Remove(K_SESSION_DOC_FACTURA_DET);
            Session.Remove(K_SESSION_DOC_FACTURA_DET_DEL);
            Session.Remove(K_SESSION_DOC_FACTURA_DET_ACT);

            Init(false);
            return View();
        }
        #endregion

        #region CONSULTA
        [HttpPost()]
        public JsonResult Listar(int skip, int take, int page, int pageSize, string group,
                                decimal idSerie, string voucher,
                                decimal idBanco, decimal idCuenta, DateTime? fini, DateTime? ffin,
                                int conFecha, int estado, int estadoConfirmacion, int estadoCobro, decimal numRecibo = 0, decimal idCobro = 0, int bpsId = 0)
        {
            List<BEMultiRecibo> lista = new List<BEMultiRecibo>();
            decimal idOficina = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
            lista = new BLMultiRecibo().Listar(GlobalVars.Global.OWNER,
                                               idSerie, voucher, idBanco, idCuenta, fini, ffin, conFecha, bpsId, estado, idOficina, estadoConfirmacion, estadoCobro, numRecibo, idCobro,
                                               page, pageSize).ToList();
            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
            {
                return Json(new BEMultiRecibo { ListarMultiRecibo = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BEMultiRecibo { ListarMultiRecibo = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region AGREGAR VOUCHER
        public List<DTOBecVoucherDetalle> VoucherDetTmp
        {
            get
            {
                return (List<DTOBecVoucherDetalle>)Session[K_SESSION_DOC_VOUCHER_DET];
            }
            set
            {
                Session[K_SESSION_DOC_VOUCHER_DET] = value;
            }
        }

        public List<DTOBecVoucherDetalle> VoucherDetTmpUPDEstado
        {
            get
            {
                return (List<DTOBecVoucherDetalle>)Session[K_SESSION_DOC_VOUCHER_DET_ACT];
            }
            set
            {
                Session[K_SESSION_DOC_VOUCHER_DET_ACT] = value;
            }
        }

        public List<DTOBecVoucherDetalle> VoucherDetTmpDelBD
        {
            get
            {
                return (List<DTOBecVoucherDetalle>)Session[K_SESSION_DOC_VOUCHER_DET_DEL];
            }
            set
            {
                Session[K_SESSION_DOC_VOUCHER_DET_DEL] = value;
            }
        }

        [HttpPost]
        public JsonResult AddVoucherDet(DTOBecVoucherDetalle entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (DocVoucherDet == null) DocVoucherDet = new List<DTOBecVoucherDetalle>();
                    if (VoucherDetTmp != null) DocVoucherDet = VoucherDetTmp;

                    int depositoCantidadRep = 0;
                    if (entidad.idBanco == Constantes.Bancos.BANCO_SCOTIABANK)//BANCO SCOTIABANK
                        depositoCantidadRep = DocVoucherDet.Where(x => x.idBanco == entidad.idBanco && x.Voucher == entidad.Voucher && x.fechaDeposito == entidad.fechaDeposito).Count();
                    else
                        depositoCantidadRep = DocVoucherDet.Where(x => x.idBanco == entidad.idBanco && x.Voucher == entidad.Voucher).Count();

                    if (depositoCantidadRep == 0)
                    {
                        decimal nuevoId = 1;
                        if (DocVoucherDet.Count > 0) nuevoId = DocVoucherDet.Max(x => x.id) + 1;
                        entidad.CuentaBancaria = string.IsNullOrEmpty(entidad.CuentaBancaria) ? "" : entidad.CuentaBancaria;
                        entidad.id = nuevoId;
                        entidad.UsuarioCrea = UsuarioActual;
                        entidad.FechaCrea = DateTime.Now;
                        entidad.Activo = true;
                        entidad.EnBD = false;
                        DocVoucherDet.Add(entidad);
                        VoucherDetTmp = DocVoucherDet;
                        retorno.result = 1;
                        retorno.message = "OK";
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "El Código de deposito ya se ingreso anteriormente.";

                    }
                }
            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddVoucherDet", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

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

        [HttpPost]
        public JsonResult ObtenerXidVoucher(decimal IdVoucher)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEDetalleMetodoPago voucher = new BEDetalleMetodoPago();
                    voucher = new BLDetalleMetodosPago().ObtenerRecibosVoucherXid(GlobalVars.Global.OWNER, IdVoucher);
                    //retorno.message = "OK";
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.data = Json(voucher, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "No se logro obtener el depósito.";
                }
            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerVoucher", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActualizarVoucherDet(DTOBecVoucherDetalle entidad)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEDetalleMetodoPago voucher = new BEDetalleMetodoPago();
                    voucher.OWNER = GlobalVars.Global.OWNER;
                    voucher.REC_PID = entidad.id;
                    voucher.REC_PWID = entidad.idTipoPago;
                    voucher.REC_PVALUE = entidad.valorIngreso;
                    voucher.REC_DATEDEPOSITE = entidad.fechaDeposito;
                    voucher.BNK_ID = entidad.idBanco;
                    voucher.BRCH_ID = entidad.idSucursal;
                    voucher.BACC_NUMBER = entidad.CuentaBancaria;
                    voucher.REC_REFERENCE = entidad.Voucher;
                    voucher.LOG_USER_UPDATE = UsuarioActual;
                    int resultado = new BLMetodoPago().ActualizarSinConfirmarVoucher(voucher);
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
            var Alfresco = GlobalVars.Global.ActivarAlfresco_Cobros;
            string QueryAlfresco = new BLAlfresco().Query_Alfresco(Convert.ToInt32(3));
            var documentos = new List<BEDocumentoGral>();
            int cantidad = 0;
            if (GlobalVars.Global.ActivarAlfresco_Cobros == "T" && accion != "I")
            {
                List<BEDocumentoGral> documentosB = new BLAlfresco().ListaDocumento(Convert.ToDecimal(MREC_ID), QueryAlfresco);
                documentos = documentosB;
                cantidad = documentosB.Count();
            }
            DocVoucherDet = VoucherDetTmp;
            Resultado retorno = new Resultado();
            int habilitarbanco = 0;
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tblVoucherDetalle' border=0 width='100%;'><thead><tr>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Id</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='text-align:left; padding-left:10px;'  >Banco</th>");
                    shtml.Append("<th style='display:none;' class='ui-state-default ui-th-column ui-th-ltr' >Cuenta</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Tipo Pago</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Fecha Depósito</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Código de Depósito</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='display:none;'>IdMoneda</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Moneda</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Monto de Depósito</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='display:none;'  >Monto</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Estado de Confirmación</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Fecha Confirmación</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Código de Confirmación</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Observación</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Voucher</th>");
                    if (accion != Constantes.AccionVista.Nuevo) shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='width:30px'></th>");
                    if (accion == Constantes.AccionVista.Nuevo) shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='width:30px'></th></tr></thead>");

                    if (DocVoucherDet != null)
                    {
                        foreach (var item in DocVoucherDet.OrderBy(x => x.id))
                        {
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.AppendFormat("<td style='width:70px; text-align:center;color: black' class='tmpIdVoucher'>{0}</td>", item.id);
                            shtml.AppendFormat("<td style='width:230px;text-align:left; color: black'>{0}</td>", item.Banco);
                            shtml.AppendFormat("<td style='width:200px;text-align:center;  color: black ;display:none;'>{0}</td>", item.CuentaBancaria);
                            shtml.AppendFormat("<td style='width:200px;text-align:center;  color: black'>{0}</td>", item.TipoPago);
                            shtml.AppendFormat("<td style='width:100px;text-align:center;  color: black'>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.fechaDeposito));
                            shtml.AppendFormat("<td style='width:160px;text-align:center;  color: black'>{0}</td>", item.Voucher);
                            shtml.AppendFormat("<td style='display:none;' width:160px;text-align:center;  color: black' class='tmpIdMoneda'>{0}</td>", item.IdMoneda);
                            shtml.AppendFormat("<td style='width:140px;text-align:center;  color: black'>{0}</td>", item.Moneda);
                            shtml.AppendFormat("<td style='width:120px;text-align:right;padding-right:25px;  color: black'>{0}</td>", item.valorIngreso.ToString("# ###,##0.000"));
                            shtml.AppendFormat("<td style='display:none;'  class='tmpMontoVoucher'>{0}</td>", item.valorIngreso);

                            //string color = item.confirmacionIngreso == Clases.Constantes.EstadosConfirmacion.CONFIRMACION ? "Green" : "Red";
                            string color = string.Empty;
                            if (item.confirmacionIngreso == Clases.Constantes.EstadosConfirmacion.CONFIRMACION)
                                color = "Green";
                            else if (item.confirmacionIngreso == Clases.Constantes.EstadosConfirmacion.RECHAZADO)
                                color = "Red";
                            else if (item.confirmacionIngreso == Clases.Constantes.EstadosConfirmacion.SIN_CONFIRMACION)
                                color = "Blue";
                            shtml.AppendFormat("<td style='width:200px; text-align:center; color: " + color + " '>{0}</td>", item.confirmacionIngresoDesc);

                            if (item.fechaConfirmacion != null)
                                shtml.AppendFormat("<td style='width:100px;text-align:center;  color: black'>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.fechaConfirmacion));
                            else
                                shtml.AppendFormat("<td style='width:100px;text-align:center;  color: black'> </td>");

                            if (item.codigoConfirmacion != null)
                                shtml.AppendFormat("<td style='width:100px;text-align:center;  color: black'>{0}</td>", item.codigoConfirmacion);
                            else
                                shtml.AppendFormat("<td style='width:100px;text-align:center;  color: black'> </td>");

                            shtml.AppendFormat("<td style='width:300px;text-align:left;  color: black'>{0}</td>", item.Observacion);

                            if (item.DOC_ID > 1 && Alfresco != "T")
                            {
                                shtml.AppendFormat("<td style='width:30px;text-align:center'> <a href=# onclick='ObtenerRuta({0});'><img src='../Images/iconos/file.png' border=0></a>&nbsp;&nbsp;", MREC_ID);
                            }
                            else if (cantidad >= 1 && Alfresco == "T")
                            {
                                shtml.AppendFormat("<td style='width:30px;text-align:center'> <a href=# onclick='ObtenerRutaAlfresco({0});'><img src='../Images/iconos/file.png' border=0></a>&nbsp;&nbsp;", MREC_ID);

                            }
                            else
                                shtml.AppendFormat("<td style='width:30px;text-align:center'>");

                            if (accion != Constantes.AccionVista.Nuevo && (item.confirmacionIngreso == Clases.Constantes.EstadosConfirmacion.SIN_CONFIRMACION || item.confirmacionIngreso == Clases.Constantes.EstadosConfirmacion.RECHAZADO))
                                shtml.AppendFormat("<td style='width:30px;text-align:center'> <a href=# onclick='delUppVoucherDet({0});'><img src='../Images/iconos/edit.png' border=0></a>&nbsp;&nbsp;", item.id);
                            if (accion == Constantes.AccionVista.Nuevo)
                                shtml.AppendFormat("<td style='width:30px;text-align:center;'> <a href=# onclick='delAddVoucherDet({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Factura" : "Activar Factura");
                            shtml.Append("</td></tr>");

                            //if (accion = Constantes.AccionVista.Modificacion && (item.confirmacionIngreso == Clases.Constantes.EstadosConfirmacion.SIN_CONFIRMACION || item.confirmacionIngreso == Clases.Constantes.EstadosConfirmacion.RECHAZADO))
                            if ((item.confirmacionIngreso == Clases.Constantes.EstadosConfirmacion.SIN_CONFIRMACION || item.confirmacionIngreso == Clases.Constantes.EstadosConfirmacion.RECHAZADO))
                                habilitarbanco = 1;
                        }
                    }

                    shtml.Append(" </table>");
                    retorno.message = shtml.ToString();
                    retorno.result = 1;
                    retorno.Code = habilitarbanco;
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

        #endregion

        #region AGREGAR CLIENTE
        public List<DTOSocio> DocClienteTmp
        {
            get
            {
                return (List<DTOSocio>)Session[K_SESSION_DOC_CLIENTE];
            }
            set
            {
                Session[K_SESSION_DOC_CLIENTE] = value;
            }
        }

        public List<DTOSocio> DocClienteTmpUPDEstado
        {
            get
            {
                return (List<DTOSocio>)Session[K_SESSION_DOC_CLIENTE_ACT];
            }
            set
            {
                Session[K_SESSION_DOC_CLIENTE_ACT] = value;
            }
        }

        public List<DTOSocio> DocClienteTmpDelBD
        {
            get
            {
                return (List<DTOSocio>)Session[K_SESSION_DOC_CLIENTE_DEL];
            }
            set
            {
                Session[K_SESSION_DOC_CLIENTE_DEL] = value;
            }
        }

        [HttpPost]
        public JsonResult AddCliente(decimal Id)
        {
            Resultado retorno = new Resultado();
            DTOSocio entidad = null;
            try
            {
                if (!isLogout(ref retorno))
                {
                    DocCliente = DocClienteTmp;
                    if (DocCliente == null) DocCliente = new List<DTOSocio>();
                    bool existe = DocCliente.Where(x => x.Codigo == Id).ToList().Count > 0 ? true : false;
                    if (!existe)
                    {
                        var cliente = new BLRecibo().ObtenerCliente(GlobalVars.Global.OWNER, Id);
                        if (cliente != null)
                        {
                            entidad = new DTOSocio();
                            //decimal nuevoId = 1;
                            //if (DocCliente.Count > 0) nuevoId = DocCliente.Max(x => x.Codigo) + 1;
                            //entidad.Codigo = nuevoId;
                            entidad.Codigo = cliente.BPS_ID;
                            entidad.TipoPersona = cliente.ENT_TYPE_NOMBRE;
                            entidad.NombreDocumento = cliente.TAXN_NAME;
                            entidad.NumDocumento = cliente.TAX_ID;
                            entidad.RazonSocial = cliente.BPS_NAME;

                            entidad.FechaCrea = DateTime.Now;
                            entidad.UsuarioCrea = UsuarioActual;
                            entidad.Activo = true;
                            entidad.EnBD = false;

                            DocCliente.Add(entidad);
                            DocClienteTmp = DocCliente;
                            retorno.result = 1;
                            retorno.message = "OK";
                        }
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "El cliente fue agregada anteriormene.\r\nSeleccione otro cliente.";
                    }
                }
            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddCliente", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult delAddCliente(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    DocCliente = DocClienteTmp;
                    if (DocCliente != null)
                    {
                        var objDel = DocCliente.Where(x => x.Codigo == id).FirstOrDefault();
                        if (objDel != null)
                        {

                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (DocClienteTmpUPDEstado == null) DocClienteTmpUPDEstado = new List<DTOSocio>();
                                if (DocClienteTmpDelBD == null) DocClienteTmpDelBD = new List<DTOSocio>();

                                var itemUpd = DocClienteTmpUPDEstado.Where(x => x.Codigo == id).FirstOrDefault();
                                var itemDel = DocClienteTmpDelBD.Where(x => x.Codigo == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) DocClienteTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) DocClienteTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) DocClienteTmpDelBD.Add(objDel);
                                    if (itemUpd != null) DocClienteTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                DocCliente.Remove(objDel);
                                DocCliente.Add(objDel);
                            }
                            else
                            {
                                DocCliente.Remove(objDel);
                                if (FacturasDetTmp != null && FacturasDetTmp.Where(s => s.idBps == id).Count() > 0)
                                    FacturasDetTmp.RemoveAll(s => s.idBps == id);
                            }
                            DocClienteTmp = DocCliente;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "delAddCliente", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarClienteDoc(string accion)
        {
            DocCliente = DocClienteTmp;
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tblCliente' border=0 width='100%;'  ><thead><tr>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Id</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Tipo Persona</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Tipo Documento</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Número</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Razón Social</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Agregar Factura</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='width:30px'></th></tr></thead>");

                    if (DocCliente != null)
                    {
                        int contador = 0;
                        foreach (var item in DocCliente.OrderBy(x => x.FechaCrea))
                        {
                            contador += 1;
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.AppendFormat("<td style='width:40px;text-align:center;color: black' class='idBps'>{0}</td>", item.Codigo);
                            shtml.AppendFormat("<td style='width:170px;text-align:center;color: black'>{0}</td>", item.TipoPersona);
                            shtml.AppendFormat("<td style='width:170px;text-align:center;color: black'>{0}</td>", item.NombreDocumento);
                            shtml.AppendFormat("<td style='width:170px;text-align:center;color: black'>{0}</td>", item.NumDocumento);
                            shtml.AppendFormat("<td style='text-align:left;color: black' class='RazonSocial'>{0}</td>", item.RazonSocial);
                            shtml.AppendFormat("<td style='width:190px;text-align:center;''> <a href=# onclick='AbrirPoPupAddFactura({0});'> <img src='../Images/botones/invoice_more.png' title='Agregar factura.' border=0></a>", item.Codigo, item.RazonSocial);

                            //if (Id == 0)
                            if (!item.EnBD)
                                shtml.AppendFormat("<td style='width:30px;text-align:center;'> <a href=# onclick='delAddCliente({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.Codigo, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Cliente" : "Activar Cliente");
                            shtml.Append("</td></tr>");

                            shtml.Append("<tr style='background-color:white'>");
                            shtml.Append("<td></td>");
                            shtml.Append("<td colspan='4'>");

                            shtml.Append("<div id='divBps" + item.Codigo.ToString() + "'>");
                            if (FacturasDetTmp != null && FacturasDetTmp.Where(p => p.idBps == item.Codigo).Count() > 0)
                                shtml.Append(getHtmlListarFacturasDet(item.Codigo));
                            shtml.Append("</div>");
                            shtml.Append("</td>");

                            shtml.Append("<td></td>");
                            shtml.Append("<td></td>");
                            shtml.Append("</tr>");

                            if (DocCliente.Count != contador)
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarClienteDoc", ex);

            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region AGREGAR DETALLE FACTURA
        public List<DTOBecDetalle> FacturasDetTmp
        {
            get
            {
                return (List<DTOBecDetalle>)Session[K_SESSION_DOC_FACTURA_DET];
            }
            set
            {
                Session[K_SESSION_DOC_FACTURA_DET] = value;
            }
        }

        public List<DTOBecDetalle> FacturasDetTmpUPDEstado
        {
            get
            {
                return (List<DTOBecDetalle>)Session[K_SESSION_DOC_FACTURA_DET_ACT];
            }
            set
            {
                Session[K_SESSION_DOC_FACTURA_DET_ACT] = value;
            }
        }

        public List<DTOBecDetalle> FacturasDetTmpDelBD
        {
            get
            {
                return (List<DTOBecDetalle>)Session[K_SESSION_DOC_FACTURA_DET_DEL];
            }
            set
            {
                Session[K_SESSION_DOC_FACTURA_DET_DEL] = value;
            }
        }

        [HttpPost]
        public JsonResult AddFacturaDet(decimal Id)
        {
            Resultado retorno = new Resultado();
            DTOBecDetalle entidad = null;
            try
            {
                if (!isLogout(ref retorno))
                {
                    DocFacturaDet = FacturasDetTmp;
                    if (DocFacturaDet == null) DocFacturaDet = new List<DTOBecDetalle>();
                    bool existe = DocFacturaDet.Where(x => x.IdFactura == Id).ToList().Count > 0 ? true : false;
                    if (!existe)
                    {
                        var factura = new BLFactura().ObtenerFacturaBec(GlobalVars.Global.OWNER, Id);
                        decimal pendientAplicar = Math.Round(factura.INV_NET, 3) - factura.PENDIENTE_APLICACION;
                        if (factura != null && pendientAplicar > 0)
                        {
                            entidad = new DTOBecDetalle();
                            decimal nuevoId = 1;
                            if (DocFacturaDet.Count > 0) nuevoId = DocFacturaDet.Max(x => x.id) + 1;
                            entidad.id = nuevoId;
                            entidad.IdFactura = Id;
                            entidad.Factura = factura.NMR_SERIAL + " - " + factura.INV_NUMBER.ToString();
                            entidad.valorBase = Math.Round(factura.INV_BASE, 3);
                            entidad.valorImpuesto = Math.Round(factura.INV_TAXES, 3);
                            entidad.valorRetenciones = Math.Round(factura.INV_TDEDUCTIONS, 3);
                            entidad.valorDescuento = Math.Round(factura.INV_DISCOUNTS, 3);
                            entidad.valorFinal = Math.Round(factura.INV_NET, 3);
                            entidad.pendienteAplicar = factura.PENDIENTE_APLICACION;
                            entidad.montoAplicar = entidad.valorFinal - entidad.pendienteAplicar;
                            entidad.Moneda = factura.MONEDA;
                            entidad.idMoneda = factura.CUR_ALPHA;
                            entidad.UsuarioCrea = UsuarioActual;
                            entidad.idBps = factura.BPS_ID;
                            entidad.socio = factura.SOCIO;
                            entidad.tipoDocumento = factura.TIPO_FACT;

                            entidad.FechaCrea = DateTime.Now;
                            entidad.Activo = true;
                            entidad.EnBD = false;
                            DocFacturaDet.Add(entidad);
                            FacturasDetTmp = DocFacturaDet;
                            retorno.result = 1;
                            retorno.message = "OK";
                        }
                        else
                        {
                            retorno.result = 0;
                            retorno.message = "La factura fue agregada anteriormente en un cobro y se encuentra pendiente de confirmar.\r\nSeleccione otra factura.";
                        }
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "La factura fue agregada anteriormene.\r\nSeleccione otra factura.";
                    }
                }
            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddFacturaDet", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActualizarFacturaDet(DTOBecDetalle detalleFactura)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    DocFacturaDet = FacturasDetTmp;
                    foreach (var item in DocFacturaDet)
                    {
                        if (item.IdFactura == detalleFactura.IdFactura)
                        {
                            item.montoAplicar = detalleFactura.montoAplicarNuevo;
                        }
                    }
                    retorno.result = 1;
                    retorno.message = "OK";
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "No se logro actualizar el monto a aplicar.";
                }
            }
            catch (Exception ex)
            {

                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "AddFacturaDet", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DellAddFacturasDet(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    DocFacturaDet = FacturasDetTmp;
                    if (DocFacturaDet != null)
                    {
                        var objDel = DocFacturaDet.Where(x => x.id == id).FirstOrDefault();
                        if (objDel != null)
                        {

                            if (objDel.EnBD)
                            {
                                bool blActivo = !objDel.Activo;

                                if (FacturasDetTmpUPDEstado == null) FacturasDetTmpUPDEstado = new List<DTOBecDetalle>();
                                if (FacturasDetTmpDelBD == null) FacturasDetTmpDelBD = new List<DTOBecDetalle>();

                                var itemUpd = FacturasDetTmpUPDEstado.Where(x => x.id == id).FirstOrDefault();
                                var itemDel = FacturasDetTmpDelBD.Where(x => x.id == id).FirstOrDefault();

                                if (!(objDel.Activo))
                                {
                                    if (itemUpd == null) FacturasDetTmpUPDEstado.Add(objDel);
                                    if (itemDel != null) FacturasDetTmpDelBD.Remove(itemDel);
                                }
                                else
                                {
                                    if (itemDel == null) FacturasDetTmpDelBD.Add(objDel);
                                    if (itemUpd != null) FacturasDetTmpUPDEstado.Remove(itemUpd);
                                }
                                objDel.Activo = blActivo;
                                DocFacturaDet.Remove(objDel);
                                DocFacturaDet.Add(objDel);
                            }
                            else
                            {
                                DocFacturaDet.Remove(objDel);
                            }
                            FacturasDetTmp = DocFacturaDet;
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "DellAddFacturasDet", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public StringBuilder getHtmlListarFacturasDet(decimal IdBps)
        {
            var detalleFacturas = FacturasDetTmp.Where(p => p.idBps == IdBps).ToList();
            StringBuilder shtml = new StringBuilder();
            //shtml.Append("<table id='tblDetalleFactura' border=0 width='100%;' ><thead><tr>");
            shtml.Append("<table id='tblDetalleFactura' border=0 width='100%;' class='k-grid k-widget'><thead><tr>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Id</th>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Tipo Doc.</th>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' > Nº Documento</th>");

            shtml.Append("<th style='display:none; class='k-header'>IdFactura</th>");
            shtml.Append("<th style='display:none; class='k-header'>Imp. Base</th>");
            shtml.Append("<th style='display:none; class='k-header'>Imp. Impuesto</th>");
            shtml.Append("<th style='display:none; class='k-header'>Imp. Retención</th>");
            shtml.Append("<th style='display:none; class='k-header'>Descuento</th>");
            shtml.Append("<th style='display:none; class='k-header'>Imp. Neto</th>");
            shtml.Append("<th style='display:none; class='k-header'>Imp. Base</th>");
            shtml.Append("<th style='display:none; class='k-header'>Imp. Impuesto</th>");
            shtml.Append("<th style='display:none; class='k-header'>Imp. Retención</th>");
            shtml.Append("<th style='display:none; class='k-header'>Descuento</th>");

            shtml.Append("<th style='display:none; class='k-header'>IdMoneda</th>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Moneda</th>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Saldo Pendiente</th>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Pendiente de Aplicación</th>");
            shtml.Append("<th style='display:none; class='k-header'>Pendiente Aplicacion</th>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Monto a Aplicar</th>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='width:30px'></th></tr></thead>");
            shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='display:none;width:30px'></th></tr></thead>");//

            if (detalleFacturas != null)
            {
                foreach (var item in detalleFacturas.OrderBy(x => x.id))
                {
                    shtml.Append("<tr style='background-color:white'>");
                    shtml.AppendFormat("<td style='text-align:center; color: black'>{0}</td>", item.id);
                    shtml.AppendFormat("<td style='text-align:center; color: black'>{0}</td>", item.tipoDocumento);
                    shtml.AppendFormat("<td style='text-align:center;  color: black'>{0}</td>", item.Factura);

                    shtml.AppendFormat("<td style='display:none;'  class='tmpIdFactura'>{0}</td>", item.IdFactura);
                    shtml.AppendFormat("<td style='display:none;' class='tmpBase'>{0}</td>", item.valorBase);
                    shtml.AppendFormat("<td style='display:none;' class='tmpImpuesto'>{0}</td>", item.valorImpuesto);
                    shtml.AppendFormat("<td style='display:none;' class='tmpRetencion'>{0}</td>", item.valorRetenciones);
                    shtml.AppendFormat("<td style='display:none;' class='tmpDescuento'>{0}</td>", item.valorDescuento);
                    shtml.AppendFormat("<td style='display:none;' class='tmpFinal'>{0}</td>", item.valorFinal);
                    shtml.AppendFormat("<td style='display:none; text-align:right;  padding-right:10px; background-color: rgb(215, 215, 215); color: black' >{0}</td>", item.valorBase.ToString("########,##0.000"));
                    shtml.AppendFormat("<td style='display:none; text-align:right;  padding-right:10px; background-color: rgb(215, 215, 215); color: black' >{0}</td>", item.valorImpuesto.ToString("########,##0.000"));
                    shtml.AppendFormat("<td style='display:none; text-align:right;  padding-right:10px; background-color: rgb(215, 215, 215); color: black' >{0}</td>", item.valorRetenciones.ToString("########,##0.000"));
                    shtml.AppendFormat("<td style='display:none; text-align:right;  padding-right:10px; background-color: rgb(215, 215, 215); color: black' class='tmpDescuento'>{0}</td>", item.valorDescuento.ToString("########,##0.000"));

                    shtml.AppendFormat("<td style='display:none;width:80px;'  class='tmpIdMonedaFact'>{0}</td>", item.idMoneda);
                    shtml.AppendFormat("<td style='text-align:center;  padding-right:30px; color: black'>{0}</td>", item.Moneda);
                    shtml.AppendFormat("<td style='text-align:right;  padding-right:80px; color: black'>{0}</td>", item.valorFinal.ToString("########,##0.000"));
                    shtml.AppendFormat("<td style='text-align:right;  padding-right:80px; color: black'>{0}</td>", item.pendienteAplicar.ToString("########,##0.000"));
                    shtml.AppendFormat("<td style='display:none;width:80px;'       class='tmpPendienteAplicacion'>{0}</td>", item.pendienteAplicar);

                    decimal? montoAplicar = Convert.ToDecimal(item.montoAplicar);
                    if (!item.EnBD)//Nuevo                    
                    {
                        shtml.AppendFormat("<td style='text-align:center; padding-right:10px;  background-color: rgb(215, 215, 215); '>  <input id=txtFactMontoAplicar{0} class='requerido'  onblur='calcularMontos()'   value='{1}' style='width:80px;text-align:right; ' maxlength='18'    /></td>", item.IdFactura, (montoAplicar != null ? montoAplicar.Value.ToString("N4") : ""));
                        shtml.AppendFormat("<td style='text-align:center; width:30px'> <a href=# onclick='delAddFacturaDet({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.id, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Factura" : "Activar Factura");
                    }
                    else
                    {
                        shtml.AppendFormat("<td style='text-align:center; padding-right:10px;  '>  <input id=txtFactMontoAplicar{0} class='requerido'  onblur='calcularMontos()'   value='{1}' style='width:80px;text-align:right; ' maxlength='18'  readonly  /></td>", item.IdFactura, (montoAplicar != null ? montoAplicar.Value.ToString("N4") : ""));
                        shtml.AppendFormat("<td style='text-align:center; width:30px'> </td> ");
                    }
                    shtml.AppendFormat("<td style='display:none;width:80px;'       class='tmpEnDB'>{0}</td>", item.EnBD);
                    shtml.Append("</td></tr>");
                }
            }
            shtml.Append(" </table>");
            return shtml;
        }

        #endregion

        #region REGISTRAR PAGO
        [HttpPost]
        public JsonResult Insertar(decimal idMultiRecibo, string tipo, decimal RserieRecibo, string MRobservacion, decimal idBanco,
                                    decimal idCuenta, decimal tipoCambio, decimal totalDepositos, decimal totalFacturas)
        {
            Resultado retorno = new Resultado();
            int inv_id = 0;
            decimal IdRecID = 0;
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (idMultiRecibo == 0)//Nuevo
                    {
                        // XXX RECIBO XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                        List<BERecibo> ListaCabeceraRecibo = new List<BERecibo>();
                        BERecibo reciboEnt = null; decimal correlativoRecibo = 1;
                        foreach (var itemRecibo in DocClienteTmp)
                        {
                            reciboEnt = new BERecibo();
                            reciboEnt.OWNER = GlobalVars.Global.OWNER;
                            reciboEnt.REC_ID_TMP = correlativoRecibo;
                            reciboEnt.NMR_ID = RserieRecibo;
                            reciboEnt.BPS_ID = itemRecibo.Codigo;
                            reciboEnt.REC_TBASE = 0;
                            reciboEnt.REC_TTAXES = 0;
                            reciboEnt.REC_TDEDUCTIONS = 0;
                            reciboEnt.REC_TTOTAL = 0;
                            reciboEnt.LOG_USER_CREAT = UsuarioActual;
                            reciboEnt.REC_OBSERVATION = MRobservacion;
                            reciboEnt.CUR_ALPHA = Clases.Constantes.TipoMoneda.SOLES;
                            correlativoRecibo += 1;
                            ListaCabeceraRecibo.Add(reciboEnt);
                        }

                        if (FacturasDetTmp != null && FacturasDetTmp.Count > 0)
                        {
                            foreach (var itemRecibo in ListaCabeceraRecibo)
                            {
                                decimal tbase = 0; decimal ttaxe = 0; decimal tdeduction = 0; decimal ttotal = 0;
                                var facturasRecibo = FacturasDetTmp.Where(p => p.idBps == itemRecibo.BPS_ID);
                                if (facturasRecibo != null)
                                {
                                    foreach (var factura in facturasRecibo)
                                    {
                                        tbase += factura.idMoneda == Clases.Constantes.TipoMoneda.DOLARES ? (factura.montoAplicar * tipoCambio) : factura.montoAplicar;
                                        ttaxe += factura.idMoneda == Clases.Constantes.TipoMoneda.DOLARES ? (factura.valorImpuesto * tipoCambio) : factura.valorImpuesto;
                                        tdeduction += factura.idMoneda == Clases.Constantes.TipoMoneda.DOLARES ? (factura.valorRetenciones * tipoCambio) : factura.valorRetenciones;
                                        ttotal += factura.idMoneda == Clases.Constantes.TipoMoneda.DOLARES ? (factura.montoAplicar * tipoCambio) : factura.montoAplicar;
                                    }
                                }
                                itemRecibo.REC_TBASE = tbase;
                                itemRecibo.REC_TTAXES = ttaxe;
                                itemRecibo.REC_TDEDUCTIONS = tdeduction;
                                itemRecibo.REC_TTOTAL = ttotal;
                            }
                        }

                        //XXX RECIBO DETALLE XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                        BEReciboDetalle reciboDetalleEnt;
                        List<BEReciboDetalle> ListaDetalleRecibo = new List<BEReciboDetalle>();
                        if (FacturasDetTmp != null && FacturasDetTmp.Count > 0)
                        {
                            foreach (var item in FacturasDetTmp)
                            {
                                reciboDetalleEnt = new BEReciboDetalle();
                                reciboDetalleEnt.OWNER = GlobalVars.Global.OWNER;
                                reciboDetalleEnt.REC_DID = item.id;
                                reciboDetalleEnt.INV_ID = item.IdFactura;

                                reciboDetalleEnt.REC_BASE = item.montoAplicar;//recDetalle.REC_BASE = item.valorBase;
                                reciboDetalleEnt.REC_TAXES = item.valorImpuesto;
                                reciboDetalleEnt.REC_DEDUCTIONS = item.valorRetenciones;    //recDetalle.REC_TOTAL = item.valorFinal;
                                reciboDetalleEnt.REC_TOTAL = item.montoAplicar;

                                reciboDetalleEnt.LOG_USER_CREAT = UsuarioActual;
                                reciboDetalleEnt.BPS_ID = item.idBps;
                                reciboDetalleEnt.CUR_ALPHA = item.idMoneda;
                                ListaDetalleRecibo.Add(reciboDetalleEnt);
                            }
                        }

                        //XXX DETALLE DE LOS METODOS DE PAGO - RECIBO XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                        List<BEDetalleMetodoPago> ListaDetalleVoucher = new List<BEDetalleMetodoPago>(); BEDetalleMetodoPago detalleVoucher = null;
                        if (VoucherDetTmp != null && VoucherDetTmp.Count > 0)
                        {
                            foreach (var itemDetallePago in VoucherDetTmp)
                            {
                                detalleVoucher = new BEDetalleMetodoPago();
                                detalleVoucher.OWNER = GlobalVars.Global.OWNER;
                                detalleVoucher.REC_DID = itemDetallePago.id;
                                detalleVoucher.REC_PWID = itemDetallePago.idTipoPago;
                                detalleVoucher.REC_PVALUE = itemDetallePago.valorIngreso;
                                detalleVoucher.REC_DATEDEPOSITE = itemDetallePago.fechaDeposito;
                                detalleVoucher.REC_CONFIRMED = itemDetallePago.confirmacionIngreso;
                                detalleVoucher.REC_CODECONFIRMED = itemDetallePago.codigoConfirmacion;
                                detalleVoucher.BNK_ID = itemDetallePago.idBanco;
                                detalleVoucher.BRCH_ID = itemDetallePago.idSucursal;
                                detalleVoucher.BACC_NUMBER = itemDetallePago.CuentaBancaria;
                                detalleVoucher.REC_REFERENCE = itemDetallePago.Voucher;
                                detalleVoucher.LOG_USER_CREAT = UsuarioActual;
                                detalleVoucher.BPS_ID = ListaCabeceraRecibo.FirstOrDefault().BPS_ID;
                                detalleVoucher.CUR_ALPHA = itemDetallePago.IdMoneda;
                                ListaDetalleVoucher.Add(detalleVoucher);
                            }
                        }

                        // CABEERA - RECIBO MULTIPLES
                        BEMultiRecibo MultiRecibo = null;
                        if (ListaDetalleVoucher != null && ListaDetalleVoucher.Count > 0)
                        {
                            decimal idOficina = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                            MultiRecibo = new BEMultiRecibo();
                            MultiRecibo.OWNER = GlobalVars.Global.OWNER;
                            MultiRecibo.MREC_NMR = ListaCabeceraRecibo.Count;
                            MultiRecibo.NMR_ID = RserieRecibo;
                            //MultiRecibo.MREC_DATE = Convert.ToDateTime(fecha);
                            MultiRecibo.MREC_TBASE = ListaCabeceraRecibo.Sum(mr => mr.REC_TBASE); //ListaDetalleVoucher.Sum(mr => mr.REC_PVALUE);
                            MultiRecibo.MREC_TTAXES = 0;// ListaCabeceraRecibo.Sum(mr => mr.REC_TTAXES);
                            MultiRecibo.MREC_TTDEDUCTION = 0;// ListaCabeceraRecibo.Sum(mr => mr.REC_TDEDUCTIONS);
                            MultiRecibo.MREC_TTOTAL = ListaCabeceraRecibo.Sum(mr => mr.REC_TTOTAL);//ListaDetalleVoucher.Sum(mr => mr.REC_PVALUE);
                            MultiRecibo.BPS_ID = 3;
                            MultiRecibo.BNK_ID = idBanco;
                            MultiRecibo.BACC_NUMBER = idCuenta;
                            MultiRecibo.MREC_OBSERVATION = MRobservacion;
                            MultiRecibo.LOG_USER_CREAT = UsuarioActual;
                            MultiRecibo.OFF_ID = idOficina;
                            MultiRecibo.CUR_VALUE = tipoCambio;

                            if (totalDepositos != 0 && totalFacturas == 0)
                                MultiRecibo.ESTADO_MULTIRECIBO = Clases.Constantes.EstadosMultirecibo.Pendiente_Aplicacion;
                            else if (totalDepositos != 0 && totalFacturas != 0 && Math.Round(totalDepositos, 4) == Math.Round(totalFacturas, 4))
                                MultiRecibo.ESTADO_MULTIRECIBO = Clases.Constantes.EstadosMultirecibo.Aplicado;
                            else if (totalDepositos != 0 && totalFacturas != 0 && Math.Round(totalFacturas, 4) < Math.Round(totalDepositos, 4))
                                MultiRecibo.ESTADO_MULTIRECIBO = Clases.Constantes.EstadosMultirecibo.Parcialmente_Aplicacion;
                            else
                                MultiRecibo.ESTADO_MULTIRECIBO = 0;
                        }

                        IdRecID = new BLMultiRecibo().Insertar(tipo, ListaCabeceraRecibo, ListaDetalleRecibo,
                                                              ListaDetalleVoucher, MultiRecibo);
                        if (ListaDetalleRecibo != null && ListaDetalleRecibo.Count > 0)
                        {//david agregado
                            inv_id = Convert.ToInt32(ListaDetalleRecibo[0].INV_ID);
                            IdRecID = 0;
                        }
                    }
                    else
                    {   // ACCION => MODIFICAR
                        // XXX RECIBO XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                        List<BERecibo> ListaCabeceraRecibo = new List<BERecibo>();
                        BERecibo reciboEnt = null; decimal correlativoRecibo = 1;
                        foreach (var itemRecibo in DocClienteTmp)
                        {
                            //if (itemRecibo.EnBD)
                            reciboEnt = new BERecibo();
                            reciboEnt.OWNER = GlobalVars.Global.OWNER;
                            reciboEnt.REC_ID = itemRecibo.IdRecibo;
                            reciboEnt.REC_ID_TMP = correlativoRecibo;
                            reciboEnt.NMR_ID = RserieRecibo;
                            reciboEnt.BPS_ID = itemRecibo.Codigo;
                            //reciboEnt.REC_TBASE = itemRecibo.ReciboBase;  //reciboEnt.REC_TTAXES = itemRecibo.ReciboImpuesto;
                            //reciboEnt.REC_TDEDUCTIONS = itemRecibo.ReciboRetenciaones; //reciboEnt.REC_TTOTAL = itemRecibo.ReciboTotal;
                            reciboEnt.REC_TBASE = 0;
                            reciboEnt.REC_TTAXES = 0;
                            reciboEnt.REC_TDEDUCTIONS = 0;
                            reciboEnt.REC_TTOTAL = 0;
                            reciboEnt.LOG_USER_CREAT = UsuarioActual;
                            reciboEnt.LOG_USER_UPDATE = UsuarioActual;
                            reciboEnt.CUR_ALPHA = Clases.Constantes.TipoMoneda.SOLES;
                            correlativoRecibo += 1;
                            ListaCabeceraRecibo.Add(reciboEnt);
                        }

                        if (FacturasDetTmp != null && FacturasDetTmp.Count > 0)
                        {
                            foreach (var itemRecibo in ListaCabeceraRecibo)
                            {
                                //decimal tbase = itemRecibo.REC_TBASE; decimal ttaxe = itemRecibo.REC_TTAXES;
                                //decimal tdeduction = itemRecibo.REC_TDEDUCTIONS; decimal ttotal = itemRecibo.REC_TTOTAL;
                                decimal tbase = 0; decimal ttaxe = 0;
                                decimal tdeduction = 0; decimal ttotal = 0;
                                //var facturasRecibo = FacturasDetTmp.Where(p => p.idBps == itemRecibo.BPS_ID && p.EnBD == false);//Solo Facturas Agregadas 
                                var facturasRecibo = FacturasDetTmp.Where(p => p.idBps == itemRecibo.BPS_ID);//Solo Facturas Agregadas 
                                if (facturasRecibo != null)
                                {
                                    foreach (var factura in facturasRecibo)
                                    {
                                        tbase += factura.idMoneda == Clases.Constantes.TipoMoneda.DOLARES ? (factura.montoAplicar * tipoCambio) : factura.montoAplicar;
                                        ttaxe += factura.idMoneda == Clases.Constantes.TipoMoneda.DOLARES ? (factura.valorImpuesto * tipoCambio) : factura.valorImpuesto;
                                        tdeduction += factura.idMoneda == Clases.Constantes.TipoMoneda.DOLARES ? (factura.valorRetenciones * tipoCambio) : factura.valorRetenciones;
                                        ttotal += factura.idMoneda == Clases.Constantes.TipoMoneda.DOLARES ? (factura.montoAplicar * tipoCambio) : factura.montoAplicar;
                                    }
                                }
                                itemRecibo.REC_TBASE = tbase;
                                itemRecibo.REC_TTAXES = ttaxe;
                                itemRecibo.REC_TDEDUCTIONS = tdeduction;
                                itemRecibo.REC_TTOTAL = ttotal;
                                //reciboEnt.REC_TBASE = tbase;
                                //reciboEnt.REC_TTAXES = ttaxe;
                                //reciboEnt.REC_TDEDUCTIONS = tdeduction;
                                //reciboEnt.REC_TTOTAL = ttotal;
                            }
                        }

                        //XXX RECIBO DETALLE XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                        BEReciboDetalle reciboDetalleEnt;
                        List<BEReciboDetalle> ListaDetalleRecibo = new List<BEReciboDetalle>();
                        if (FacturasDetTmp != null && FacturasDetTmp.Count > 0)
                        {
                            foreach (var item in FacturasDetTmp)
                            {
                                //if (!item.EnBD)//obteniendo facturas nuevas.
                                //{
                                reciboDetalleEnt = new BEReciboDetalle();
                                reciboDetalleEnt.OWNER = GlobalVars.Global.OWNER;
                                reciboDetalleEnt.REC_DID = item.id;
                                reciboDetalleEnt.INV_ID = item.IdFactura;

                                reciboDetalleEnt.REC_BASE = item.montoAplicar;//recDetalle.REC_BASE = item.valorBase;
                                reciboDetalleEnt.REC_TAXES = item.valorImpuesto;
                                reciboDetalleEnt.REC_DEDUCTIONS = item.valorRetenciones;    //recDetalle.REC_TOTAL = item.valorFinal;
                                reciboDetalleEnt.REC_TOTAL = item.montoAplicar;

                                reciboDetalleEnt.LOG_USER_CREAT = UsuarioActual;
                                reciboDetalleEnt.LOG_USER_UPDATE = UsuarioActual;
                                reciboDetalleEnt.BPS_ID = item.idBps;
                                reciboDetalleEnt.CUR_ALPHA = item.idMoneda;
                                ListaDetalleRecibo.Add(reciboDetalleEnt);
                                //}
                            }
                        }

                        //MULTI-RECIBO
                        BEMultiRecibo MultiRecibo = new BEMultiRecibo();
                        MultiRecibo.OWNER = GlobalVars.Global.OWNER;
                        MultiRecibo.MREC_ID = idMultiRecibo;
                        MultiRecibo.MREC_NMR = ListaCabeceraRecibo.Count;
                        MultiRecibo.MREC_TBASE = ListaCabeceraRecibo.Sum(mr => mr.REC_TBASE); //ListaDetalleVoucher.Sum(mr => mr.REC_PVALUE);
                        MultiRecibo.MREC_TTAXES = 0;// ListaCabeceraRecibo.Sum(mr => mr.REC_TTAXES);
                        MultiRecibo.MREC_TTDEDUCTION = 0;// ListaCabeceraRecibo.Sum(mr => mr.REC_TDEDUCTIONS);
                        MultiRecibo.MREC_TTOTAL = ListaCabeceraRecibo.Sum(mr => mr.REC_TTOTAL);//ListaDetalleVoucher.Sum(mr => mr.REC_PVALUE);                        
                        MultiRecibo.MREC_OBSERVATION = MRobservacion;
                        MultiRecibo.LOG_USER_UPDAT = UsuarioActual;

                        if (totalDepositos != 0 && totalFacturas == 0)
                            MultiRecibo.ESTADO_MULTIRECIBO = Clases.Constantes.EstadosMultirecibo.Pendiente_Aplicacion;
                        else if (totalDepositos != 0 && totalFacturas != 0 && Math.Round(totalDepositos, 4) == Math.Round(totalFacturas, 4))
                            MultiRecibo.ESTADO_MULTIRECIBO = Clases.Constantes.EstadosMultirecibo.Aplicado;
                        else if (totalDepositos != 0 && totalFacturas != 0 && Math.Round(totalFacturas, 4) < Math.Round(totalDepositos, 4))
                            MultiRecibo.ESTADO_MULTIRECIBO = Clases.Constantes.EstadosMultirecibo.Parcialmente_Aplicacion;
                        else
                            MultiRecibo.ESTADO_MULTIRECIBO = 0;

                        int Id = new BLMultiRecibo().Actualizar(ListaCabeceraRecibo, ListaDetalleRecibo, MultiRecibo);

                        List<BEDetalleMetodoPago> ListaDepositoBancario = new List<BEDetalleMetodoPago>();
                        foreach (var deposito in VoucherDetTmp.Where(x => x.confirmacionIngreso == Clases.Constantes.EstadosConfirmacion.CONFIRMACION))
                        {
                            BEDetalleMetodoPago depositoBancario = new BEDetalleMetodoPago();
                            depositoBancario.REC_PID = deposito.id;
                            depositoBancario.LOG_USER_UPDATE = UsuarioActual;
                            ListaDepositoBancario.Add(depositoBancario);
                        }

                        int resultListaCobros = 0;
                        if (ListaDepositoBancario != null && ListaDepositoBancario.Count > 0)
                            resultListaCobros = new BLMetodoPago().AplicarCobrosXListaDepositos(ListaDepositoBancario);

                    }
                    retorno.result = 1;
                    if (idMultiRecibo == 0)//Nuevo
                    {
                        retorno.Code = inv_id;
                        retorno.valor = IdRecID.ToString();
                    }
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
        #endregion

        [HttpPost]
        public JsonResult Obtener(decimal idRecibo, string version)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEMultiRecibo MRecibo = new BEMultiRecibo();
                    MRecibo = new BLMultiRecibo().ObtenerCabecera_PagoXDoc(GlobalVars.Global.OWNER, idRecibo, version);
                    MREC_ID = Convert.ToString(MRecibo.MREC_ID);
                    if (MRecibo != null)
                    {
                        MRecibo.ListaRecibo = new BLRecibo().ObtenerRecibosCliente(GlobalVars.Global.OWNER, idRecibo, version);
                        MRecibo.ListaReciboDetalleVoucher = new BLDetalleMetodosPago().ObtenerRecibosVoucher(GlobalVars.Global.OWNER, idRecibo, version);
                        MRecibo.ListaReciboDetalleFactura = new BLRecibo().ObtenerRecibosDetalle(GlobalVars.Global.OWNER, idRecibo, version);

                        if (MRecibo.ListaRecibo != null)
                        {
                            DocCliente = new List<DTOSocio>();
                            MRecibo.ListaRecibo.ForEach(s =>
                            {
                                DocCliente.Add(new DTOSocio
                                {
                                    IdRecibo = s.REC_ID,
                                    Codigo = s.BPS_ID,
                                    TipoPersona = s.TIPO_PERSONA,
                                    NombreDocumento = s.TIPO_DOC,
                                    NumDocumento = s.NUM_DOC,
                                    RazonSocial = s.SOCIO,
                                    ReciboBase = s.REC_TBASE,
                                    ReciboImpuesto = s.REC_TTAXES,
                                    ReciboRetenciaones = s.REC_TDEDUCTIONS,
                                    ReciboTotal = s.REC_TTOTAL,
                                    FechaCrea = s.LOG_DATE_CREAT,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    EnBD = true,
                                    Activo = true // s.ENDS.HasValue ? false : true
                                });
                            });
                            DocClienteTmp = DocCliente;
                        }

                        if (MRecibo.ListaReciboDetalleVoucher != null)
                        {
                            DocVoucherDet = new List<DTOBecVoucherDetalle>();
                            MRecibo.ListaReciboDetalleVoucher.ForEach(s =>
                            {
                                DocVoucherDet.Add(new DTOBecVoucherDetalle
                                {
                                    id = s.REC_PID,
                                    idBanco = s.BNK_ID,
                                    Banco = s.BNK_NAME,
                                    idCuentaBancaria = s.BPS_ACC_ID.ToString(),
                                    CuentaBancaria = s.BACC_NUMBER,
                                    idTipoPago = s.REC_PWID,
                                    TipoPago = s.REC_PWDESC,
                                    fechaDeposito = s.REC_DATEDEPOSITE,
                                    Voucher = s.REC_REFERENCE,
                                    valorIngreso = s.REC_PVALUE,
                                    confirmacionIngreso = s.REC_CONFIRMED,
                                    confirmacionIngresoDesc = s.ESTADO_DEPOSITO,
                                    FechaCrea = s.LOG_DATE_CREAT,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    IdMoneda = s.CUR_ALPHA,
                                    Moneda = s.MONEDA,
                                    Observacion = s.REC_OBSERVATION,
                                    ObservacionUsuario = s.REC_OBSERVATION_USER,
                                    EnBD = true,
                                    Activo = true, // s.ENDS.HasValue ? false : true
                                    fechaConfirmacion = s.REC_DATECONFIRMED,
                                    codigoConfirmacion = s.REC_CODECONFIRMED,
                                    DOC_ID = s.DOC_ID
                                });
                            });
                            VoucherDetTmp = DocVoucherDet;
                        }

                        if (MRecibo.ListaReciboDetalleFactura != null)
                        {
                            DocFacturaDet = new List<DTOBecDetalle>();
                            MRecibo.ListaReciboDetalleFactura.ForEach(s =>
                            {
                                DocFacturaDet.Add(new DTOBecDetalle
                                {
                                    id = s.REC_DID,
                                    idRecibo = s.REC_ID,
                                    IdFactura = s.INV_ID,
                                    idBps = s.BPS_ID,
                                    tipoDocumento = s.TIPO_DOC,
                                    Factura = s.FACTURA,

                                    valorBase = s.REC_BASE,
                                    valorImpuesto = s.REC_TAXES,
                                    valorRetenciones = s.REC_DISCOUNTS,
                                    valorDescuento = s.REC_DEDUCTIONS,
                                    valorFinal = s.SALDO_PENDIENTE,
                                    montoAplicar = s.REC_TOTAL,

                                    pendienteAplicar = s.REC_BALANCE,

                                    idMoneda = s.CUR_ALPHA,
                                    Moneda = s.MONEDA,
                                    FechaCrea = s.LOG_DATE_CREAT,
                                    UsuarioCrea = s.LOG_USER_CREAT,
                                    EnBD = true,
                                    Activo = true // s.ENDS.HasValue ? false : true
                                });
                            });
                            FacturasDetTmp = DocFacturaDet;
                        }

                        retorno.result = 1;
                        retorno.Code = Convert.ToInt32(MRecibo.ESTADO_MULTIRECIBO);
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                        retorno.data = Json(MRecibo, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                        retorno.data = Json(MRecibo, JsonRequestBehavior.AllowGet);
                    }

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
        public JsonResult validarDuplicadoVoucher(string idBanco, string fechaDeposito, string Voucher, decimal idVoucher)
        {
            Resultado retorno = new Resultado();
            try
            {
                int valCantidadRep = new BLRecibo().VoucherDuplicidad(GlobalVars.Global.OWNER, idBanco, fechaDeposito, Voucher, idVoucher);
                retorno.Code = valCantidadRep;
                retorno.result = 1;
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "validarDuplicadoVoucher", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerTipoCambioActual()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var VUM = new BLREF_CURRENCY_VALUES().ObtenerTipoCambioActual();
                    retorno.data = Json(VUM, JsonRequestBehavior.AllowGet);
                    retorno.message = "OK";
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerTipoCambioActual", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region BANCO_DESTINO
        public JsonResult ActualizarBancoDestino(decimal idCobro, decimal idBanco, decimal idCuenta)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEMultiRecibo multiRecibo = new BEMultiRecibo();
                    multiRecibo.OWNER = GlobalVars.Global.OWNER;
                    multiRecibo.MREC_ID = idCobro;
                    multiRecibo.BNK_ID = idBanco;
                    multiRecibo.BACC_NUMBER = idCuenta;
                    multiRecibo.LOG_USER_UPDAT = UsuarioActual;
                    int result = new BLMultiRecibo().ActualizarBanco(multiRecibo);
                    retorno.result = 1;
                    retorno.message = "OK";
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "No se logro actualizar.";
                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ActualizarBancoDestino", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ELIMINAR_COBRO
        public JsonResult EliminarCobro(decimal idCobro)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEMultiRecibo multiRecibo = new BEMultiRecibo();
                    multiRecibo.OWNER = GlobalVars.Global.OWNER;
                    multiRecibo.MREC_ID = idCobro;
                    multiRecibo.LOG_USER_UPDAT = UsuarioActual;
                    int result = new BLMultiRecibo().EliminarCobro(multiRecibo);
                    retorno.result = 1;
                    retorno.message = "OK";
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "No se logro eliminar el cobro.";
                }
            }
            catch (Exception ex)
            {
                retorno.message = String.Format("{0}...{1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "EliminarCobro", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region AGREGAR VOUCHER
        [HttpPost]
        public JsonResult AddDocumento(int Cod)
        {
            Resultado retorno = new Resultado();
            var alfresco = GlobalVars.Global.ActivarAlfresco_Cobros;
            var docGral = new BEDocumentoGral();
            try
            {
                if (!isLogout(ref retorno))
                {
                    docGral.DOC_ID = 0;
                    docGral.OWNER = GlobalVars.Global.OWNER;
                    docGral.DOC_TYPE = 58;
                    docGral.DOC_PATH = "";
                    docGral.DOC_DATE = DateTime.Now;
                    docGral.ENT_ID = Convert.ToInt32(Constantes.ENTIDAD.OTROS);
                    docGral.DOC_USER = UsuarioActual;
                    docGral.LOG_USER_CREAT = UsuarioActual;
                    if (alfresco == "T")
                    {
                        docGral.DOC_VERSION = 2;
                    }
                    else
                    {
                        docGral.DOC_VERSION = 1;
                    }


                    var codigoGenDoc = new BLDocumentoGral().InsertarBec(docGral, new BEDocumentoLic
                    {
                        LIC_ID = Cod,
                        OWNER = docGral.OWNER,
                        LOG_USER_CREAT = docGral.LOG_USER_CREAT,
                        DOC_ORG = Constantes.OrigenDocumento.EXTERNO
                    });
                    //var result = new BLDocumentoLic().Insertar(new BEDocumentoLic
                    //{
                    //    LIC_ID = codLic,
                    //    DOC_ID = codigoGenDoc,
                    //    OWNER = docGral.OWNER,
                    //    LOG_USER_CREAT = docGral.LOG_USER_CREAT, 
                    //    DOC_ORG=Constantes.OrigenDocumento.EXTERNO
                    //});

                    retorno.result = 1;
                    retorno.Code = Convert.ToInt32(codigoGenDoc);
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO+"- Adjuntar Facturas.";
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "AddDocumento", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Obtener RutaAlfresco
        [HttpPost]
        public JsonResult ObtenerRutaAlfresco(string MREC_ID)
        {
            Resultado retorno = new Resultado();
            //var docGral = new BEDocumentoGral();
            try
            {
                string QueryAlfresco = new BLAlfresco().Query_Alfresco(Convert.ToInt32(3));
                var RutaFisicaLyrics = GlobalVars.Global.RutaFisicaImgBECDoc;
                var RutaWebLyrics = GlobalVars.Global.RutaWebImgBecDoc;
                var documentos = new List<BEDocumentoGral>();
                List<BEDocumentoGral> documentosB = new BLAlfresco().ListaDocumento(Convert.ToDecimal(MREC_ID), QueryAlfresco);
                if (GlobalVars.Global.ActivarAlfresco_Cobros == "T" && documentosB.Count() > 0)
                {

                    documentos = documentosB;
                    var ruta = documentos[0].DOC_ORG;
                    var paht = MREC_ID + "-" + documentos[0].DOC_PATH;
                    var RutaFisicaLyricsArchivo = System.IO.Path.Combine(RutaFisicaLyrics, paht);
                    var RutaWebLyricsArchivo = System.IO.Path.Combine(RutaWebLyrics, paht);

                    var Existe_Copia = System.IO.File.Exists(RutaFisicaLyricsArchivo);
                    if (Existe_Copia == false)
                    {
                        StreamReader objLeerArchivo = new StreamReader(documentosB[0].ArchivoBytes);
                        byte[] data;
                        using (objLeerArchivo)
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                objLeerArchivo.BaseStream.CopyTo(ms);
                                data = ms.ToArray();
                            }
                        }
                        System.IO.File.WriteAllBytes(RutaFisicaLyricsArchivo, data);
                        //System.IO.File.Copy(ruta, RutaFisicaLyricsArchivo, true);
                    }
                    retorno.message = RutaWebLyricsArchivo;
                    retorno.valor = "1";
                }
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "RutaAlfresco", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion
        [HttpPost]
        public JsonResult Obtener_Inv_id(int MREC_ID)
        {
            Resultado retorno = new Resultado();
            try
            {
                BLAlfresco bl = new BLAlfresco();
                decimal Inv_id = bl.Obtener_INV_ID_X_MREC_ID(MREC_ID);
                bl.Desactivar_Imagen_Cobro(MREC_ID);
                retorno.message = Convert.ToString(Inv_id);
                retorno.valor = Convert.ToString(Inv_id);
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.valor = "0"; ;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ActivarAlfrescoCobros()
        {
            Resultado retorno = new Resultado();
            try
            {
                var ActivarAlfresco = GlobalVars.Global.ActivarAlfresco_Cobros;
                retorno.message = ActivarAlfresco;
                retorno.valor = ActivarAlfresco;
            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.valor = "0"; ;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerMontoMinimo()
        {
            Resultado retorno = new Resultado();
            try
            {
                retorno.result = Variables.SI;
                retorno.valor =( new BLMultiRecibo().ObtenerMontoMinimo()).ToString();
                

            }
            catch(Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CobroSaltaValidacionMontoMinimo(decimal CodigoCobro)
        {
            Resultado retorno = new Resultado();
            try
            {

                retorno.result= new BLMultiRecibo().ObtenerValidacionMontoBEC(CodigoCobro);
                retorno.message = Variables.MSG_COBRO_NO_PASO_VALIDACION_MONTO;

            }
            catch (Exception ex)
            {
                retorno.result = Variables.NO;
                retorno.message =Variables.MSG_OCURRIO_UN_ERROR_AL_VALIDAR;
            }

            return Json(retorno,JsonRequestBehavior.AllowGet);
        }

    }
}

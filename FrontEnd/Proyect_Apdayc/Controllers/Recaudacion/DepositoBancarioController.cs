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
using SGRDA.DA.Alfresco;
using SGRDA.BL.BLAlfresco;
using System.IO;

namespace Proyect_Apdayc.Controllers.Recaudacion
{
    public class DepositoBancarioController : Base
    {
        //
        // GET: /DepositoBancario/
        public const string nomAplicacion = "SRGDA";
        private const string K_SESSION_REP_DEPOSITO = "___K_SESSION_REP_DEPOSITO";

        private List<BEDetalleMetodoPago> ListaReporteDepositoTmp
        {
            get
            {
                return (List<BEDetalleMetodoPago>)Session[K_SESSION_REP_DEPOSITO];
            }
            set
            {
                Session[K_SESSION_REP_DEPOSITO] = value;
            }
        }


        #region VISTA
        public ActionResult Index()
        {
            Init(false);
            return View();
        }
        #endregion

        #region BANDEJA
        [HttpPost()]
        public JsonResult Listar(int skip, int take, int page, int pageSize, string group,
                                string idBanco, string idTipoPago,
                                string idMoneda, string idEstadoConfirmacion, string CodigigoDeposito,
                                int conFecha, DateTime FIni, DateTime FFin, decimal IdBps,
                                string idBancoDestino, string idCuentaDestino, string montoDepositado,
                                int conFechaIngreso, DateTime FIniIngreso, DateTime FFinIngreso, decimal IdOficina, decimal IdVoucher, string CodigoConfirmacion, decimal becEspecial, decimal becEspecialAprobacion, decimal idCobro
                                )
        {
            List<BEDetalleMetodoPago> lista = new List<BEDetalleMetodoPago>();
            lista = new BLMetodoPago().ListarDepositosBancarios(GlobalVars.Global.OWNER,
                                        idBanco, idTipoPago,
                                        idMoneda, idEstadoConfirmacion, CodigigoDeposito,
                                        conFecha, FIni, FFin, IdBps, idBancoDestino, idCuentaDestino, montoDepositado,
                                        conFechaIngreso, FIniIngreso, FFinIngreso, IdOficina, IdVoucher, CodigoConfirmacion, becEspecial, becEspecialAprobacion, idCobro,
                                        page, pageSize);

            var tot = lista.Select(s => s.TotalVirtual).Take(1).ToList();
            if (tot.Count == 0)
                return Json(new BEDetalleMetodoPago { ListarDetalleMetodoPago = lista, TotalVirtual = 0 }, JsonRequestBehavior.AllowGet);
            else
                return Json(new BEDetalleMetodoPago { ListarDetalleMetodoPago = lista, TotalVirtual = tot[0] }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region REGISTRO
        [HttpPost]
        public JsonResult ObtenerComprobante(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEDetalleMetodoPago ComprobanteDeposito = new BEDetalleMetodoPago();
                    ComprobanteDeposito = new BLMetodoPago().ObtenerComprobante(GlobalVars.Global.OWNER, id);
                    if (ComprobanteDeposito != null)
                    {
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                    }
                    retorno.data = Json(ComprobanteDeposito, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerComprobante", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActualizarEstadoDeposito(decimal id, string codigoConfirmacion, string estado, string observacion, decimal idCobro)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEDetalleMetodoPago comprobante = new BEDetalleMetodoPago();
                    comprobante.OWNER = GlobalVars.Global.OWNER;
                    comprobante.REC_PID = id;
                    comprobante.REC_CONFIRMED = estado;
                    comprobante.REC_USERCONFIRMED = UsuarioActual;
                    comprobante.REC_CODECONFIRMED = codigoConfirmacion;
                    comprobante.REC_OBSERVATION = observacion;
                    comprobante.LOG_USER_UPDATE = UsuarioActual;
                    comprobante.MREC_ID = idCobro;
                    var result = new BLMetodoPago().ActualizarComprobante(comprobante);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "No se logro actualizar los comprobantes.";
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ActualizarEstadoDeposito", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ObtenerRecibosXIdCobro(decimal idCobro)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    var listaRecibosXCobro = new BLMultiRecibo().ObtenerRecibosXIdCobro(GlobalVars.Global.OWNER, idCobro);
                    if (listaRecibosXCobro != null && listaRecibosXCobro.Count > 0)
                    {
                        retorno.Code = Convert.ToInt32(listaRecibosXCobro.FirstOrDefault().REC_ID);
                        retorno.result = 1;
                        retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    }
                    else
                    {
                        retorno.Code = 0;
                        retorno.result = 0;
                        retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                    }
                    retorno.data = Json(listaRecibosXCobro, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ObtenerComprobante", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //public int AplicarCobroFactura(decimal idComprobante)
        //{
        //    int resultado = 0;
        //    var listaRecibos = new BLMetodoPago().ObtenerRecibos_x_Comprobante(GlobalVars.Global.OWNER, idComprobante);
        //    var listaFactura = new BLMetodoPago().ObtenerFactura_x_Comprobante(GlobalVars.Global.OWNER, idComprobante);
        //    var listafacturaDetalle = new BLMetodoPago().ObtenerFacturaDetalle_x_Comprobante(GlobalVars.Global.OWNER, idComprobante);

        //    if (listaRecibos != null && listaFactura != null && listafacturaDetalle != null)
        //    {
        //        decimal TotalRecibosSoles = listaRecibos.Sum(x => x.REC_TOTAL_PAGAR);
        //        decimal TotalFacturasxCobrarSoles = listaFactura.Sum(x => x.MONTO_POR_COBRAR);
        //        decimal TotalFacturasDetallexCobrarSoles = listafacturaDetalle.Sum(x => x.MONTO_POR_COBRAR);


        //    }

        //    return resultado;
        //}
        #endregion

        [HttpPost()]
        public JsonResult ValidarVoucherRepetidosConfirmados(decimal idVoucher, string nro_confirmacion)
        {
            Resultado retorno = new Resultado();
            try
            {
                int valCantidadRep = new BLRecibo().VoucherRepetidosConfirmados(idVoucher, nro_confirmacion);
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

        public JsonResult ValidarImagen(string MREC_ID)
        {
            Resultado retorno = new Resultado();
            try
            {
                var alfresco = GlobalVars.Global.ActivarAlfresco_Cobros;
                var ruta = GlobalVars.Global.RutaWebImgBecDoc;
                BLAlfresco bl = new BLAlfresco();
                BESelectListItem be = new BESelectListItem();
                be = bl.ValidarImagen_X_MRECID(MREC_ID);
                if (be != null && alfresco != "T")
                {
                    retorno.message = Path.Combine(ruta, be.Value);
                    retorno.valor = "1";
                }
                //var ActivarAlfresco = GlobalVars.Global.EnviarDocumento;

            }
            catch (Exception ex)
            {
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                retorno.valor = "0"; ;
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActualizarBecEspecial(decimal id, decimal estadoAprobacion, string obs)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    BEDetalleMetodoPago comprobante = new BEDetalleMetodoPago();
                    comprobante.REC_PID = id;
                    comprobante.REC_BEC_ESPECIAL_APPROVED = estadoAprobacion;
                    comprobante.REC_BEC_ESPECIAL_OBSERVATION = obs;
                    comprobante.REC_BEC_ESPECIAL_USER_APPROVED = UsuarioActual;
                    var result = new BLMetodoPago().ActualizarDepositoBecEspecial(comprobante);
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "No se logro actualizar los comprobantes.";
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ActualizarBecEspecial", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerOficinaxOficina()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    decimal oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                    int TipoPermiso = 0;
                    var myInClause = new string[] { "CONFIRMACIONES", "BEC_ESPECIALES" };
                    var lista = new BLMetodoPago().ObtenerPermisoXoficina(oficina_id);

                    if (lista.Count == 0)
                    {
                        TipoPermiso = PermisoXoficina.CONSULTA;
                    }
                    else if (lista.Count == 1)
                    {
                        var permiso = lista.Where(x => myInClause.Contains(x.PERMISO)).First().PERMISO;
                        if (permiso == "CONFIRMACIONES")
                            TipoPermiso = PermisoXoficina.CONFIRMACIONES;
                        else if (permiso == "BEC_ESPECIALES")
                            TipoPermiso = PermisoXoficina.BEC_ESPECIALES;
                    }
                    else if (lista.Count == 2)
                    {
                        TipoPermiso = 4;
                    }

                    retorno.result = 1;
                    retorno.Code = TipoPermiso;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GRABAR;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "No se logro obtener permisos.";
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ActualizarBecEspecial", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public class PermisoXoficina
        {
            public const int CONSULTA = 1;
            public const int CONFIRMACIONES = 2;
            public const int BEC_ESPECIALES = 3;
            public const int PERMISO_TOTAL = 4;
        }

        public JsonResult ActualizarBancoDestinoFecDep(decimal idCobro, decimal idBanco, decimal idCuenta, DateTime fecDeposito, string nroOperacion)
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
                    multiRecibo.FECH_DEPO = fecDeposito;
                    multiRecibo.VOUCHER = nroOperacion;
                    int result = new BLMultiRecibo().ActualizarBancoFecDeposito(multiRecibo);
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
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ActualizarBancoDestinoFecDep", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #region REPORTE

        //public JsonResult Listar(int skip, int take, int page, int pageSize, string group,
        //                string idBanco, string idTipoPago,
        //                string idMoneda, string idEstadoConfirmacion, string CodigigoDeposito,
        //                int conFecha, DateTime FIni, DateTime FFin, decimal IdBps,
        //                string idBancoDestino, string idCuentaDestino, string montoDepositado,
        //                int conFechaIngreso, DateTime FIniIngreso, DateTime FFinIngreso, decimal IdOficina, decimal IdVoucher, string CodigoConfirmacion, decimal becEspecial, decimal becEspecialAprobacion, decimal idCobro
        //                )
        //{
        //    List<BEDetalleMetodoPago> lista = new List<BEDetalleMetodoPago>();
        //    lista = new BLMetodoPago().ListarDepositosBancarios(GlobalVars.Global.OWNER,
        //                                idBanco, idTipoPago,
        //                                idMoneda, idEstadoConfirmacion, CodigigoDeposito,
        //                                conFecha, FIni, FFin, IdBps, idBancoDestino, idCuentaDestino, montoDepositado,
        //                                conFechaIngreso, FIniIngreso, FFinIngreso, IdOficina, IdVoucher, CodigoConfirmacion, becEspecial, becEspecialAprobacion, idCobro,
        //                                page, pageSize);


        public ActionResult ConsultaFiltroReporteDeposito(
                        string idBanco, string idTipoPago, string idMoneda, string idEstadoConfirmacion, string CodigigoDeposito,
                        int conFecha, DateTime FIni, DateTime FFin, decimal IdBps, string idBancoDestino,
                        string idCuentaDestino, string montoDepositado, int conFechaIngreso, DateTime FIniIngreso, DateTime FFinIngreso,
                        decimal IdOficina, decimal IdVoucher, string CodigoConfirmacion, decimal becEspecial, decimal becEspecialAprobacion,
                        decimal idCobro, string formato
                        )
        {
            Session.Remove(K_SESSION_REP_DEPOSITO);
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            Resultado retorno = new Resultado();
            string format = formato;

            try
            {
                //cambiar ruta del reporte
                //LocalReport localReport = new LocalReport();
                //localReport.ReportPath = Server.MapPath("~/Reportes/Contable/R_REC_DEPOSITOS.rdlc");
                //List<BEDetalleMetodoPago> listar = new List<BEDetalleMetodoPago>();
                ListaReporteDepositoTmp = new BLMetodoPago().ListarDepositosBancarios_Reporte(GlobalVars.Global.OWNER,
                         idBanco, idTipoPago, idMoneda, idEstadoConfirmacion, CodigigoDeposito,
                         conFecha, Convert.ToDateTime(FIni), Convert.ToDateTime(FFin), IdBps, idBancoDestino,
                         idCuentaDestino, montoDepositado, conFechaIngreso, Convert.ToDateTime(FIniIngreso), Convert.ToDateTime(FFinIngreso),
                         IdOficina, IdVoucher, CodigoConfirmacion, becEspecial, becEspecialAprobacion,
                         idCobro

                    );
                if (ListaReporteDepositoTmp.Count > 0)
                {
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "No se encuentra registros.";
                }

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GenerarReporteDeposito(string formato, string BancoDestino, string cuentaDestino, string estado, string fecIngreso, string fecDeposito)
        {

            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            Resultado retorno = new Resultado();
            string format = formato;

            try
            {
                //cambiar ruta del reporte
                LocalReport localReport = new LocalReport();
                if (formato == "PDF")
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_DEPOSITOS.rdlc");
                else if (formato == "EXCEL")
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_DEPOSITOS_EXCEL.rdlc");

                List<BEDetalleMetodoPago> listar = new List<BEDetalleMetodoPago>();
                //listar = ListaReporteDepositoTmp;
                listar = (from p in ListaReporteDepositoTmp
                          orderby p.ESTADO_DEPOSITO, p.FECHA_INGRESO descending
                          select p).ToList();

                if (listar.Count > 0)
                {
                    //listar estados
                    var resultEstados = listar
                        .GroupBy(x => x.ESTADO_DEPOSITO)
                        .Select(g => new
                        {
                            estado = g.Key.ToLower(),
                            cantidad = g.Count()
                        });
                    string cadenaEstado = "";
                    foreach (var item in resultEstados)
                    {
                        if (item.Equals(resultEstados.Last()))
                            cadenaEstado += item.estado + " (" + item.cantidad.ToString() + ")";
                        else
                            cadenaEstado += item.estado + " (" + item.cantidad.ToString() + ")  |  ";
                    }

                    //sumatoria conertido a soles
                    decimal acumuladoSoles = 0;
                    foreach (var item in listar)
                    {
                        if (item.MONEDA == "SOL")
                            acumuladoSoles += item.REC_PVALUE;
                        else if (item.MONEDA != "SOL")
                            acumuladoSoles += (item.REC_PVALUE * item.CUR_VALUE);
                    }

                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = listar;
                    localReport.DataSources.Add(reportDataSource);

                    ReportParameter parametroBancoDestino = new ReportParameter();
                    parametroBancoDestino = new ReportParameter("BancoDestino", BancoDestino);
                    localReport.SetParameters(parametroBancoDestino);

                    ReportParameter parametroCuenta = new ReportParameter();
                    parametroCuenta = new ReportParameter("Cuenta", cuentaDestino);
                    localReport.SetParameters(parametroCuenta);

                    ReportParameter parametroEstado = new ReportParameter();
                    //parametroEstado = new ReportParameter("Estado", estado);
                    parametroEstado = new ReportParameter("Estado", cadenaEstado);
                    localReport.SetParameters(parametroEstado);

                    ReportParameter parametroFecIngreso = new ReportParameter();
                    parametroFecIngreso = new ReportParameter("FecIngreso", fecIngreso);
                    localReport.SetParameters(parametroFecIngreso);

                    ReportParameter parametroFecConfirmacion = new ReportParameter();
                    parametroFecConfirmacion = new ReportParameter("FecDeposito", fecDeposito);
                    localReport.SetParameters(parametroFecConfirmacion);

                    ReportParameter parametroCantidad = new ReportParameter();
                    parametroCantidad = new ReportParameter("Cantidad", listar.Count.ToString());
                    localReport.SetParameters(parametroCantidad);

                    ReportParameter parametroTotalSoles = new ReportParameter();
                    parametroTotalSoles = new ReportParameter("TotalSoles", acumuladoSoles.ToString());
                    localReport.SetParameters(parametroTotalSoles);

                    ReportParameter parametroUsuario = new ReportParameter();
                    parametroUsuario = new ReportParameter("Usuario", UsuarioActual);
                    localReport.SetParameters(parametroUsuario);


                    //parametros para la fecha inicio y fin
                    //ReportParameter parametroFechaIni = new ReportParameter();
                    //parametroFechaIni = new ReportParameter("FechaInicio", FIni == "" ? "-" : FIni);
                    //localReport.SetParameters(parametroFechaIni);

                    //ReportParameter parametroFechaFin = new ReportParameter();
                    //parametroFechaFin = new ReportParameter("FechaFin", FFin == "" ? "-" : FFin);
                    //localReport.SetParameters(parametroFechaFin);


                    /*
                    ReportParameter parametroFecha = new ReportParameter();
                    parametroFecha = new ReportParameter("FechaImpresion", DateTime.Now.ToShortDateString());
                    localReport.SetParameters(parametroFecha);

                    ReportParameter parametroNomusu = new ReportParameter();
                    parametroFecha = new ReportParameter("NombreUsuario", usuario);
                    localReport.SetParameters(parametroFecha);

                    ReportParameter parametroNomoficina = new ReportParameter();
                    parametroNomoficina = new ReportParameter("NombreOficina", oficina);
                    localReport.SetParameters(parametroNomoficina);
                    */




                    //ReportParameter parametroBanco = new ReportParameter();
                    //parametroBanco = new ReportParameter("BancoDestino", "-");
                    //localReport.SetParameters(parametroBanco);


                    //ReportParameter parametroCuenta = new ReportParameter();
                    //parametroCuenta = new ReportParameter("CtaDestino", "-");
                    //localReport.SetParameters(parametroCuenta);

                    //ReportParameter parametroEstado = new ReportParameter();
                    //parametroEstado = new ReportParameter("Estado", "-");
                    //localReport.SetParameters(parametroEstado);


                    //ReportParameter parametroFechaIniCrea = new ReportParameter();
                    //parametroFechaIniCrea = new ReportParameter("FecIniCrea", "-");
                    //localReport.SetParameters(parametroFechaIniCrea);


                    //ReportParameter parametroFechaFinCrea = new ReportParameter();
                    //parametroFechaFinCrea = new ReportParameter("FecFinCrea", "-");
                    //localReport.SetParameters(parametroFechaFinCrea);

                    //parametroFechaFinCon = new ReportParameter("FechaFinCon", conCon == 1 ? ffinCon : "-");

                    // ************************************************************************************************
                    //ReportParameter parametroFechaIniCon = new ReportParameter();
                    //parametroFechaIniCon = new ReportParameter("FecIniConfirmacion", "-");
                    //localReport.SetParameters(parametroFechaIniCon);

                    //ReportParameter parametroFechaFinCon = new ReportParameter();                    
                    //parametroFechaIniCon = new ReportParameter("FecFinConfirmacion", "-");
                    //localReport.SetParameters(parametroFechaFinCon);

                    //parametroFechaFinCon = new ReportParameter("FechaFinCon", conCon == 1 ? ffinCon : "-");
                    // ************************************************************************************************

                    string reportType = format;
                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    //CODIGO REPETIBLE
                    //The DeviceInfo settings should be changed based on the reportType            
                    //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
                    string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>" + format + "</OutputFormat>" +
                    //  "  <PageWidth>8.5in</PageWidth>" +
                    "  <PageWidth>11in</PageWidth>" +
                    //"  <PageHeight>11in</PageHeight>" +
                    "  <PageHeight>8.3in</PageHeight>" +
                    "  <MarginTop>0.0in</MarginTop>" +
                    "  <MarginLeft>0.3in</MarginLeft>" +
                    "  <MarginRight>0.0in</MarginRight>" +
                    "  <MarginBottom>0.3in</MarginBottom>" +
                    "</DeviceInfo>";


                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;
                    //Render the report            
                    renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                    //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension); 

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    //retorno.data = Json(FacturaMasiva, JsonRequestBehavior.AllowGet);
                    retorno.result = 1;
                    localReport.DisplayName = "Reporte de Consulta de Sociedad";
                    if (format == null)
                    {
                        return File(renderedBytes, "image/jpeg");
                    }

                    else if (format == "PDF")
                    {
                        return File(renderedBytes, mimeType);
                    }
                    else if (format == "EXCEL")
                    {
                        return File(renderedBytes, mimeType);
                    }
                    else
                    {
                        return File(renderedBytes, "image/jpeg");
                    }
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}

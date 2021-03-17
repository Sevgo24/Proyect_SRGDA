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
using System.IO;
using OfficeOpenXml;

namespace Proyect_Apdayc.Controllers.Recaudacion
{
    public class DocumentoNoIdentificadoController : Base
    {
        public const string nomAplicacion = "SRGDA";
        //public const string nomAplicacion = "SRGDA";
        //private const string K_SESSION_DIN = "___DTO_DIN";
        //private const string K_SESSION_DIN_DEL = "___DTO_DIN_Del";
        //private const string K_SESSION_DIN_ACT = "___DTO_DIN_Act";
        List<DTOBecVoucherDetalle> DocVoucherDet = new List<DTOBecVoucherDetalle>();


        // GET: DocumentoNoIdentificado
        #region VISTA
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        //#region TEMPORALES
        //public List<DTOBecVoucherDetalle> VoucherDetTmp
        //{
        //    get
        //    {
        //        return (List<DTOBecVoucherDetalle>)Session[K_SESSION_DIN];
        //    }
        //    set
        //    {
        //        Session[K_SESSION_DIN] = value;
        //    }
        //}

        //public List<DTOBecVoucherDetalle> VoucherDetTmpUPDEstado
        //{
        //    get
        //    {
        //        return (List<DTOBecVoucherDetalle>)Session[K_SESSION_DIN_ACT];
        //    }
        //    set
        //    {
        //        Session[K_SESSION_DIN_ACT] = value;
        //    }
        //}

        //public List<DTOBecVoucherDetalle> VoucherDetTmpDelBD
        //{
        //    get
        //    {
        //        return (List<DTOBecVoucherDetalle>)Session[K_SESSION_DIN_DEL];
        //    }
        //    set
        //    {
        //        Session[K_SESSION_DIN_DEL] = value;
        //    }
        //}
        //#endregion

        [HttpPost]
        public JsonResult RegistrarDNI(DTODocumentoNoIdentificado objDNI)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    if (objDNI != null)
                    {
                        BEDocumentoNoIdentificado obj = new BEDocumentoNoIdentificado();
                        obj.REC_PWID = objDNI.ID_Tipo_Deposito;
                        obj.ID_BANCO = objDNI.Id_Banco;
                        obj.ID_CUENTA = objDNI.Id_Cuenta;
                        obj.FECHA_DEPOSITO = objDNI.Fecha_Deposito;
                        obj.ID_MONEDA = objDNI.Id_Moneda;
                        obj.MONTO = objDNI.Monto_Original;
                        obj.TIPO_CAMBIO = objDNI.Tipo_Cambio;

                        if (obj.ID_MONEDA == "PEN")// SOLES
                            obj.MONTO_SOLES = objDNI.Monto_Original;
                        else
                            obj.MONTO_SOLES = objDNI.Monto_Original * objDNI.Tipo_Cambio;

                        obj.NRO_CONFIRMACION = objDNI.Nro_Confirmacion;
                        obj.Observacion = objDNI.Observacion;

                        obj.ID_SOCIO_NEGOCIO = objDNI.Id_Socio;
                        obj.ID_OFICINA = objDNI.Id_Oficina;
                        obj.USUARIO_CREA = UsuarioActual;
                        obj.FECHA_CREA = objDNI.FechaCreacion;

                        obj.FECHA_CREA = DateTime.Now;
                        obj.ID_OFICINA = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                        var result = new BLDocumentoNoIdentificado().InsertarDNI(obj);
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
        public JsonResult EliminarDNI(decimal id)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    var result = new BLDocumentoNoIdentificado().EliminarDNI(id);
                    retorno.result = 1;
                    retorno.message = "OK";
                }
            }
            catch (Exception ex)
            {
                retorno.message = ex.Message;
                retorno.result = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "EliminarDNI", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarDIN(decimal bnk_id, string fecha_ini, string fecha_fin, int estado)
        {
            //DocVoucherDet = VoucherDetTmp;
            
            var Listadeposito = new BLDocumentoNoIdentificado().ListarDNI(bnk_id, fecha_ini, fecha_fin, estado);

            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    StringBuilder shtml = new StringBuilder();
                    shtml.Append("<table id='tblVoucherDetalle' border=0 width='100%;'><thead><tr>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Id</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Tipo Pago</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='text-align:left; padding-left:10px;'  >Banco Destino</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='text-align:left; padding-left:10px;'  >Cuenta</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Fecha Depósito</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr'>Monto de Depósito</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >N° de Depósito</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Fec. Creación</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Fec. Anulación</th>");

                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Estado</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Fec. Interfaz</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' >Fec. Reversión</th>");
                    shtml.Append("<th class='ui-state-default ui-th-column ui-th-ltr' style='width:30px'></th></tr></thead>");

                    if (Listadeposito != null)
                    {
                        foreach (var item in Listadeposito.OrderByDescending(x => x.REC_PID))
                        {
                            shtml.Append("<tr style='background-color:white'>");
                            shtml.AppendFormat("<td style='width:70px; text-align:center;color: black' class='tmpIdVoucher'>{0}</td>", item.REC_PID);
                            shtml.AppendFormat("<td style='width:150px;text-align:center;  color: black'>{0}</td>", item.TipoPago);
                            shtml.AppendFormat("<td style='width:150px;text-align:left; color: black'>{0}</td>", item.BANCO_DESTINO);
                            shtml.AppendFormat("<td style='width:150px;text-align:left; color: black'>{0}</td>", item.CTA_DESTINO);
                            shtml.AppendFormat("<td style='width:100px;text-align:center;  color: black'>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FECHA_DEPOSITO));
                            shtml.AppendFormat("<td style='width:120px;text-align:right;padding-right:25px;  color: black'>{0}</td>", item.MONTO.ToString("# ###,##0.000"));
                            shtml.AppendFormat("<td style='width:100px;text-align:center;  color: black'>{0}</td>", item.NRO_CONFIRMACION);
                            shtml.AppendFormat("<td style='width:100px;text-align:center;  color: black'>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FEC_CREA));
                            shtml.AppendFormat("<td style='width:100px;text-align:center;  color: black'>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.ENDS));
                            //shtml.AppendFormat("<td style='width:100px;text-align:center;  color: black'>{0}</td>", String.Format("{0:dd/MM/yyyy  h:mm tt}", item.ENDS));

                            if (item.REC_PID == 946453)
                                item.REC_PID = 0;

                            if (item.ESTADO == 0)
                                shtml.AppendFormat("<td style='width:100px;text-align:center;  color: black'>{0}</td>", "PENDIENTE");
                            else if (item.ESTADO == 1)
                                shtml.AppendFormat("<td style='width:100px;text-align:center;  color: green'>{0}</td>", "ENVIADO SAP");
                            else if (item.ESTADO == 2)
                                shtml.AppendFormat("<td style='width:100px;text-align:center;  color: red'>{0}</td>", "ANULADO");
                            else if (item.ESTADO == 3)
                                shtml.AppendFormat("<td style='width:100px;text-align:center;  color: blue'>{0}</td>", "REVERTIDO");
                            else
                                shtml.AppendFormat("<td style='width:100px;text-align:center;  color: green'>{0}</td>", "");

                            shtml.AppendFormat("<td style='width:100px;text-align:center;  color: black'>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FEC_INTERFAZ));
                            shtml.AppendFormat("<td style='width:100px;text-align:center;  color: black'>{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FEC_INTERFAZ_REVERT));

                            if (item.ESTADO == 0)
                                shtml.AppendFormat("<td style='width:30px;text-align:center;'> <a href=# onclick='delAddVoucherDet({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.REC_PID, item.ENDS == null ? "delete.png" : "activate.png", item.ENDS == null ? "Eliminar Factura" : "Activar Factura");
                            //shtml.AppendFormat("<td style='width:30px;text-align:center;'> <a href=# onclick='delAddVoucherDet({0});'> <img src='../Images/iconos/{1}' title='{2}' border=0></a>", item.REC_PID, item.Activo ? "delete.png" : "activate.png", item.Activo ? "Eliminar Factura" : "Activar Factura");
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
                retorno.Code = 0;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarDIN", ex);

            }

            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult Cargar(HttpPostedFileBase archivoTXT)
        {
            //List<BEArchivosPlanosBancos> listatmp = new List<BEArchivosPlanosBancos>();
            Resultado retorno = new Resultado();
            var mensaje = "";
            List<BEDocumentoNoIdentificado> lista = new List<BEDocumentoNoIdentificado>();
            try
            {

                Stream documentConverted = archivoTXT.InputStream;
                var NombreArchivo = archivoTXT.FileName;
                int rowCount = 0;
                if (archivoTXT != null)
                {
                    var allowedExtensions = new[] { ".xlsx", ".xls", ".xlsm", ".xlsb" };
                    var Extension = Path.GetExtension(archivoTXT.FileName);
                    if (allowedExtensions.Contains(Extension))
                    {
                        using (ExcelPackage paquete = new ExcelPackage())
                        {
                            // Creamos un flujo a partir del archivo de Excel, y lo cargamos en el paquete                  
                            paquete.Load(documentConverted);
                            // Obtenemos la primera hoja del documento
                            ExcelWorksheet hoja1 = paquete.Workbook.Worksheets.First();
                            rowCount = hoja1.Dimension.End.Row;
                            //List<BEDocumentoNoIdentificado> lista = new List<BEDocumentoNoIdentificado>();
                            // Empezamos a leer a partir de la segunda fila
                            long number1 = 0;
                            for (int numFila = 2; numFila <= rowCount; numFila++)
                            {
                                BEDocumentoNoIdentificado be = new BEDocumentoNoIdentificado();

                                //if (string.IsNullOrEmpty(Convert.ToString(be.MONTO))) break;
                                // Obtenemos el valor de la celda de la primera columna, como texto
                                be.LineaExcel = numFila;
                                if (string.IsNullOrEmpty(hoja1.Cells[numFila, 1].Text))
                                    mensaje = mensaje + "Linea " + numFila + " : error en columna " + hoja1.Cells[1, 1].Text + Environment.NewLine;
                                else
                                {
                                    if (hoja1.Cells[numFila, 1].Text == "SOL" || hoja1.Cells[numFila, 1].Text == "DOL")
                                        be.ID_MONEDA = hoja1.Cells[numFila, 1].Text;
                                    else
                                        mensaje = mensaje + "Linea " + numFila + " : error en el formato solo se acepta SOL o DOL en la columna " + hoja1.Cells[1, 1].Text + Environment.NewLine;
                                }

                                if (string.IsNullOrEmpty(hoja1.Cells[numFila, 2].Text) || long.TryParse((hoja1.Cells[numFila, 2].Text), out number1) == false)
                                    mensaje = mensaje + "Linea " + numFila + " : error en columna " + hoja1.Cells[1, 2].Text + Environment.NewLine;
                                else
                                    be.ID_BANCO = Convert.ToDecimal(hoja1.Cells[numFila, 2].Text);
                                if (string.IsNullOrEmpty(hoja1.Cells[numFila, 3].Text) || long.TryParse((hoja1.Cells[numFila, 3].Text), out number1) == false)
                                    mensaje = mensaje + "Linea " + numFila + " : error en columna " + hoja1.Cells[1, 3].Text + Environment.NewLine;
                                else
                                    be.ID_CUENTA = Convert.ToDecimal(hoja1.Cells[numFila, 3].Text);
                                if (string.IsNullOrEmpty(hoja1.Cells[numFila, 4].Text))
                                    mensaje = mensaje + "Linea " + numFila + " : error en columna " + hoja1.Cells[1, 4].Text + Environment.NewLine;
                                else
                                    be.Banco = hoja1.Cells[numFila, 4].Text;
                                if (string.IsNullOrEmpty(hoja1.Cells[numFila, 5].Text))
                                    mensaje = mensaje + "Linea " + numFila + " : error en columna " + hoja1.Cells[1, 5].Text + Environment.NewLine;
                                else
                                    be.CuentaBancaria = hoja1.Cells[numFila, 5].Text;
                                if (string.IsNullOrEmpty(hoja1.Cells[numFila, 6].Text))
                                    mensaje = mensaje + "Linea " + numFila + " : error en columna " + hoja1.Cells[1, 6].Text + Environment.NewLine;
                                else
                                    be.FECHA_DEPOSITO = Convert.ToDateTime(hoja1.Cells[numFila, 6].Value);
                                if (string.IsNullOrEmpty(hoja1.Cells[numFila, 7].Text) && long.TryParse((hoja1.Cells[numFila, 7].Text), out number1) == false)
                                    mensaje = mensaje + "Linea " + numFila + " : error en columna " + hoja1.Cells[1, 7].Text + Environment.NewLine;
                                else
                                    be.MONTO = Convert.ToDecimal(hoja1.Cells[numFila, 7].Text);
                                if (string.IsNullOrEmpty(hoja1.Cells[numFila, 8].Text))
                                    mensaje = mensaje + "Linea " + numFila + " : error en columna " + hoja1.Cells[1, 8].Text + Environment.NewLine;
                                else
                                    be.NRO_CONFIRMACION = hoja1.Cells[numFila, 8].Text;
                                // Si la celda está en blanco, finalizamos la lectura
                                lista.Add(be);
                            }
                        }
                    }
                    else
                    {
                        mensaje = "Formato de Archivo Incorrecto.";
                    }
                }else
                {
                    mensaje = "Seleccione Archivo.";
                }

                    if (mensaje != "")
                {
                    retorno.result = 0;
                    retorno.message = mensaje.ToString();
                }
                else
                {
                    // VALIDACIÓN - OK
                    int estadoValidacionDNI = 1;
                    string mendajeValidacionDNI = "";
                    foreach (var Fila in lista)
                    {
                        BEDocumentoNoIdentificado objDNIValidar = new BEDocumentoNoIdentificado();
                        objDNIValidar.OWNER = GlobalVars.Global.OWNER;
                        objDNIValidar.ID_MONEDA = Fila.ID_MONEDA;
                        objDNIValidar.ID_BANCO = Fila.ID_BANCO; ;
                        objDNIValidar.ID_CUENTA = Fila.ID_CUENTA;
                        objDNIValidar.FECHA_DEPOSITO = Fila.FECHA_DEPOSITO;
                        objDNIValidar.NRO_CONFIRMACION = Fila.NRO_CONFIRMACION;
                        objDNIValidar.ID_MONEDA = Fila.ID_MONEDA;
                        objDNIValidar.MONTO = Fila.MONTO;

                        if (Fila.ID_MONEDA == "SOL")
                            objDNIValidar.ID_MONEDA = "PEN";
                        else if (Fila.ID_MONEDA == "DOL")
                            objDNIValidar.ID_MONEDA = "44";

                        var obtenerValidacion = new BLDocumentoNoIdentificado().Validar_DNI(objDNIValidar);
                        if (obtenerValidacion > 0)
                        {
                            estadoValidacionDNI = 0;
                            mendajeValidacionDNI = mendajeValidacionDNI + "El depósito de la Linea " + Fila.LineaExcel + " fue registrado anteriormente como DNI o depósito de la gestora." + Environment.NewLine;
                        }

                    }

                    if (estadoValidacionDNI == 1)
                    {
                        // SI ESTA OK SE REGISTRA MASIVAMENTE
                        foreach (var ObjExcel in lista)
                        {

                            BEDocumentoNoIdentificado obj = new BEDocumentoNoIdentificado();
                            obj.REC_PWID = "TB";
                            //obj.ID_BANCO = objDNI.Id_Banco;
                            obj.ID_BANCO = ObjExcel.ID_BANCO;
                            //obj.ID_CUENTA = objDNI.Id_Cuenta;
                            obj.ID_CUENTA = ObjExcel.ID_CUENTA;
                            //obj.FECHA_DEPOSITO = objDNI.Fecha_Deposito;
                            obj.FECHA_DEPOSITO = ObjExcel.FECHA_DEPOSITO;
                            //obj.ID_MONEDA = objDNI.Id_Moneda;
                            //obj.ID_MONEDA = ObjExcel.ID_MONEDA;
                            if (ObjExcel.ID_MONEDA == "SOL")
                                obj.ID_MONEDA = "PEN";
                            else if (ObjExcel.ID_MONEDA == "DOL")
                                obj.ID_MONEDA = "44";

                            //obj.MONTO = objDNI.Monto_Original;
                            obj.MONTO = ObjExcel.MONTO;
                            //obj.TIPO_CAMBIO = objDNI.Tipo_Cambio;
                            obj.TIPO_CAMBIO = Convert.ToDecimal(3.2);

                            if (ObjExcel.ID_MONEDA == "SOL")
                                obj.MONTO_SOLES = ObjExcel.MONTO;
                            else if (ObjExcel.ID_MONEDA == "DOL")
                                obj.MONTO_SOLES = ObjExcel.MONTO * Convert.ToDecimal(3.2);

                            //obj.NRO_CONFIRMACION = ObjExcel.Nro_Confirmacion;
                            obj.NRO_CONFIRMACION = ObjExcel.NRO_CONFIRMACION;
                            obj.Observacion = "";

                            //obj.ID_SOCIO_NEGOCIO = ObjExcel.Id_Socio;
                            //obj.ID_OFICINA = ObjExcel.Id_Oficina;
                            obj.USUARIO_CREA = UsuarioActual;
                            obj.FECHA_CREA = DateTime.Now;
                            obj.ID_OFICINA = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                            var result = new BLDocumentoNoIdentificado().InsertarDNI(obj);
                        }
                        //vaidacion de repetidos
                        retorno.message = "Se cargo correctamente " + (rowCount - 1).ToString() + " Depositos de tipo DNI.";
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.message = mendajeValidacionDNI + Environment.NewLine + " Primero debe de corregir para volver hacer la carga completa.";
                        retorno.result = 0;
                    }


                }

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = mensaje;
                ViewBag.mensaje = "Se produjo un error : " + ex.Message;
            }


            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReporteDNI(decimal bnk_id, string fecha_ini, string fecha_fin, int estado,string formato)
        {

            Resultado retorno = new Resultado();
            string format = formato;
            int oficina_id = 0;
            int? rubrofiltro = null;

            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            List<BEDocumentoNoIdentificado> Listadeposito = new List<BEDocumentoNoIdentificado>();
            if (format == "PDF")
            {
                 Listadeposito = new BLDocumentoNoIdentificado().ListarDNI(bnk_id, fecha_ini, fecha_fin, estado);
            }
            else
            {
                 Listadeposito = new BLDocumentoNoIdentificado().ListarDNI_EXCEL(bnk_id, fecha_ini, fecha_fin, estado);
            }
          


            try
            {
                LocalReport localReport = new LocalReport();
                //cambiar ruta del reporte
                if (formato == "PDF")
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_DNI.rdlc");
                else
                    localReport.ReportPath = Server.MapPath("~/Reportes/R_REC_REPORTE_DNI.rdlc");




                //listar = new BLReporteFacturaxCobrar().ListarFacturaxCobrar(fini, ffin, idoficina, rubrofiltro);


                if (Listadeposito.Count > 0)
                {
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = Listadeposito;
                    localReport.DataSources.Add(reportDataSource);

                    //ReportParameter parametroFechaIni = new ReportParameter();
                    //parametroFechaIni = new ReportParameter("FechaInicio", fecha_ini);
                    //localReport.SetParameters(parametroFechaIni);

                    //ReportParameter parametroFechaFin = new ReportParameter();
                    //parametroFechaFin = new ReportParameter("FechaFin", fecha_fin);
                    //localReport.SetParameters(parametroFechaFin);

                    //ReportParameter parametroFecha = new ReportParameter();
                    //parametroFecha = new ReportParameter("FechaImpresion", DateTime.Now.ToShortDateString());
                    //localReport.SetParameters(parametroFecha);

                    //ReportParameter parametroNomusu = new ReportParameter();
                    //parametroFecha = new ReportParameter("NombreUsuario", oficina.Replace("Y", "&"));
                    //localReport.SetParameters(parametroFecha);

                    //ReportParameter parametroNomoficina = new ReportParameter();
                    //parametroNomoficina = new ReportParameter("NombreOficina", usuario);
                    //localReport.SetParameters(parametroNomoficina);

                    string reportType = format;
                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    //CODIGO REPETIBLE
                    //The DeviceInfo settings should be changed based on the reportType            
                    //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
                    string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>" + format + "</OutputFormat>" +
                    "  <PageWidth>11in</PageWidth>" +
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

                    if (reportType == "EXCEL")
                        renderedBytes = localReport.Render("EXCELOPENXML", deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                    else
                        renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);


                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.result = 1;
                    localReport.DisplayName = "Reporte de Factura Pendiente";

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
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte DNI ", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}
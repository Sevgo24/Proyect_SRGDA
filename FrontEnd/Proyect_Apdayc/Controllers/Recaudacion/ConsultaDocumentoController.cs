using System;
using SGRDA.BL;
using SGRDA.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases.Factura_Electronica;
using SGRDA.Entities.FacturaElectronica;
using SGRDA.BL.FacturaElectronica;
using Proyect_Apdayc.Clases;
using System.Text;
using System.Text.RegularExpressions;
using SGRDA.BL.Reporte;
using SGRDA.BL.Consulta;
using System.Globalization;
using SGRDA.Utility;
using System.IO;


namespace Proyect_Apdayc.Controllers.Recaudacion
{
    public class ConsultaDocumentoController : Base
    {
        // GET: ConsultaDocumento
        public const string nomAplicacion = "SRGDA";
        string MSG_SUNAT = "";

        private const string K_SESION_FACTURA_REPORTE = "___dTOPFacturaReporte";
        public class TiposDeDocmentos
        {
            public const string FACTURA = "FACTURA";
            public const string BOLETA = "BOLETA";
            public const string NOTA_CREDITO = "NC";
            public const string NOTA_DEBITO = "ND";
            public const int NC = 3;

        }
        private class Variables
        {
            public const int SI = 1;
            public const int NO = 0;
            public const int MODIF_DOC_MANUAL = 3;
            public const int MODIF_DOC_PEND_CANC = 4;
            public const int Cero = 0;
            public const int Uno = 1;

        }
        private DateTime FechaSistema = new BLREC_RATES_GRAL().ObtenerFechaSistema();
        private int MesesQuiebra = new BLREC_RATES_GRAL().ObtenerMesesQuiebra();
        ComprobanteElectronica FE = new ComprobanteElectronica();
        ComprobanteElectronicoContingenciaController FE2 = new ComprobanteElectronicoContingenciaController();

        #region TEMPORALES 
        public List<BEFactura> FacturaTemp
        {
            get
            {
                return (List<BEFactura>)Session[K_SESION_FACTURA_REPORTE];
            }
            set
            {
                Session[K_SESION_FACTURA_REPORTE] = value;
            }
        }
        #endregion
        public ActionResult Index()
        {
            Init(false);
            Session.Remove(K_SESION_FACTURA_REPORTE);
            return View();
        }
        public ActionResult NotaCredito()
        {
            Init(false);
            Session.Remove(K_SESION_FACTURA_REPORTE);
            return View();
        }



        #region ConsultaDocumento_Nueva_Busqueda
        public JsonResult ConsultaDocumento(decimal idSerial, decimal numFact, decimal idFactura,
                                                                    decimal idSocio, decimal idGrupoFacturacion, decimal idGrupoEmpresarial,
                                                                    int conFecha, DateTime Fini, DateTime Ffin, decimal idLicencia,
                                                                    decimal idDivision, decimal idOficina, decimal idAgente,
                                                                    string idMoneda, decimal tipoDoc, decimal estado,
                                                                    decimal idDepartamento, decimal idProvincia, decimal idDistrito, int estadoSunat,int ORDEN)
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    List<BEFactura> ListarConsultaDocumento = new List<BEFactura>();
                    //decimal idOfiTmp = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                    //ListarConsultaDocumento = new BLConsultaDocumento().ListarConsultaDocumento(GlobalVars.Global.OWNER, idSerial, numFact, idFactura,
                    //                                                 idSocio, idGrupoFacturacion, idGrupoEmpresarial,
                    //                                                 conFecha, Fini, Ffin, idLicencia,
                    //                                                 idDivision, idOfiTmp, idAgente,
                    //                                                 idMoneda, tipoDoc, estado,
                    //                                                 idDepartamento, idProvincia, idDistrito);

                    ListarConsultaDocumento = new BLConsultaDocumento().ListarConsultaDocumento(GlobalVars.Global.OWNER, idSerial, numFact, idFactura,
                                                                     idSocio, idGrupoFacturacion, idGrupoEmpresarial,
                                                                     conFecha, Fini, Ffin, idLicencia,
                                                                     idDivision, idOficina, idAgente,
                                                                     idMoneda, tipoDoc, estado,
                                                                     idDepartamento, idProvincia, idDistrito, estadoSunat, ORDEN);

                    FacturaTemp = ListarConsultaDocumento;

                    StringBuilder ObtenerLista = new StringBuilder();
                    ObtenerLista = ObtenerListarConsultaDocumento(ListarConsultaDocumento);

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    //retorno.data = Json(FacturaConsulta, JsonRequestBehavior.AllowGet);
                    //retorno.result = 1;

                    retorno.message = ObtenerLista.ToString();
                    retorno.TotalFacturas = ListarConsultaDocumento.Count;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarConsultaDocumento", ex);
            }
            //return Json(retorno, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public JsonResult ConsultaDocumento2(string idSerial, decimal numFact, decimal idFactura, decimal idOficina,
                                                                   int conFecha, DateTime Fini, DateTime Ffin
                                                                  )
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {

                    List<BEFactura> ListarConsultaDocumento = new List<BEFactura>();
                    //decimal idOfiTmp = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                    //ListarConsultaDocumento = new BLConsultaDocumento().ListarConsultaDocumento(GlobalVars.Global.OWNER, idSerial, numFact, idFactura,
                    //                                                 idSocio, idGrupoFacturacion, idGrupoEmpresarial,
                    //                                                 conFecha, Fini, Ffin, idLicencia,
                    //                                                 idDivision, idOfiTmp, idAgente,
                    //                                                 idMoneda, tipoDoc, estado,
                    //                                                 idDepartamento, idProvincia, idDistrito);

                    ListarConsultaDocumento = new BLConsultaDocumento().ListarConsultaDocumento2(GlobalVars.Global.OWNER, idSerial, numFact, idFactura,  idOficina,
                                                                     conFecha, Fini, Ffin);

                    FacturaTemp = ListarConsultaDocumento;

                    StringBuilder ObtenerLista = new StringBuilder();
                    ObtenerLista = ObtenerListarConsultaDocumento2(ListarConsultaDocumento);

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    //retorno.data = Json(FacturaConsulta, JsonRequestBehavior.AllowGet);
                    //retorno.result = 1;

                    retorno.message = ObtenerLista.ToString();
                    retorno.TotalFacturas = ListarConsultaDocumento.Count;
                    retorno.result = 1;
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarConsultaDocumento", ex);
            }
            //return Json(retorno, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public StringBuilder ObtenerListarConsultaDocumento(List<BEFactura> ListarConsultaDocumento)
        {
            //int MesesFechaQuiebra = 6;
            DateTime fechaMinAnulacion = Convert.ToDateTime(FechaSistema.AddDays(-GlobalVars.Global.DiasFechaAnulacion));
            DateTime fechaMinQuiebra = Convert.ToDateTime(FechaSistema.AddMonths(-MesesQuiebra));

            Sunat su = new Sunat();
            //su.ValidarRUC_Sunat("70037781");


            var usuario= Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoPerdilUsuario]);
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));
            BLFactura blFactura = new BLFactura();
            var lista = new BLConsultaDocumento().UsuariosAprobadosParaAnular();
            bool Anular = lista.Contains(Convert.ToInt32(usuario));
            //listar = ListaConsultaTmp;
            int habNC = 0;
            bool habOficina = false;
            int idOficina = Convert.ToInt32(Session[Constantes.Sesiones.CodigoOficina]);
            if (idOficina == 10081 || idOficina == 10154)// TI - GENAREC
                habOficina = true;
            else
                habOficina = false;

            //Resultado retorno = new Resultado();
            StringBuilder shtml = new StringBuilder();
            try
            {

                shtml.Append("<table class='tblFacturaMasiva' border=0 width='100%;' class='k-grid k-widget' id='tblFacturaMasiva'>");
                shtml.Append("<thead><tr>");

                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='text-align:center;width:25px'>");
                //if (estado == 0)
                //{
                shtml.Append("<input type='checkbox' id='idCheck' name='Check' class='Check' onchange='clickCheck()'>");
                //}
                shtml.Append("</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Id</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >T.E</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Tipo</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Serie</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >#</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Fecha</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Cancelación</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Anulado</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Ident.</th>");

                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >N° Ident.</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Socio de negocio</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Moneda</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Facturado</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Cobrado</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Saldo Pendiente</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Ref N.C</th>");
                //shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Tipo</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Estado</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Estado Sunat</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Ver</th>");

                shtml.Append("<th style'width:80px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                shtml.Append("<th style'width:80px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                //if (opcAdm == 1) { 
                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Quiebra</th>");

                //requerimientos de cambio doc manuales 
                shtml.Append("<th style'width:80px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Req.</th>");
                shtml.Append("<th style'width:80px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                //                 }
                if (ListarConsultaDocumento != null)
                {
                    //foreach (var item in ListarConsultaDocumento.OrderByDescending(x => x.NMR_SERIAL).OrderByDescending(x => x.INV_NUMBER)) //.OrderByDescending(x => x.id))
                    ListarConsultaDocumento.ForEach(item =>
                    {
                        if (item.INVT_DESC != "NC" && habOficina)
                            habNC = 1;
                        else
                            habNC = 0;

                        shtml.Append("<tr style='background-color:white'>");
                        shtml.Append("</td>");
                        shtml.Append("<td style='text-align:center;width:25px'>");
                        if ((item.TIPO_EMI_DOC != "M" || item.TIPO_EMI_DOC == null) && (item.ESTADO_SUNAT != Constantes.Mensaje_Sunat.MSG_ACEPTADO && item.ESTADO_SUNAT != Constantes.Mensaje_Sunat.MSG_RECHAZADO))
                        {
                            shtml.AppendFormat("<input type='checkbox' id='{0}' name='Check' class='Check' />", "chkFact" + item.INV_ID);
                        }
                        shtml.Append("</td>");
                        shtml.AppendFormat("<td style='width:25px'> ");
                        //shtml.AppendFormat("<a href=# onclick='verDetaFactura({0});'><img id='expand" + item.INV_ID + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.INV_ID);
                        shtml.AppendFormat("<a href=# onclick='verDetalleDocumento({0});'><img id='expand" + item.INV_ID + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.INV_ID);
                        shtml.Append("</td>");
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:45px' onclick='return obtenerId({0},{1});' class='IDCell' >{0}</td>", item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3},{4},{5});'>{0}</td>", item.TIPO_EMI_DOC, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); //TIPO EMI M o  A
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3},{4},{5});' class='TipCell'>{0}</td>", item.INVT_DESC, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); //TIPO DOC
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right' onclick='return obtenerId({1},{2},{3},{4},{5});'>{0}</td>", item.NMR_SERIAL, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); // SERIE
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right' onclick='return obtenerId({1},{2},{3},{4},{5});' >{0}</td>", item.INV_NUMBER, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); // NUMERO
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3},{4},{5});' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.INV_DATE), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO);// FECHA EMISION
                        if (item.EST_FACT == 2)// Constantes.EstadoFactura.CANCELADO
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3},{4},{5});' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FECHA_CANCELACION), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO);// FECHA CANCELACION
                        else
                            shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3},{4},{5});' >{0}</td>", "", item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3},{4},{5});' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.INV_NULL), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO);
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' >{0}</td>", item.TAXN_NAME, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO);// TIPO ODC IDENTIFICACION


                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' >{0}</td>", item.TAX_ID, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); // NUMEROC
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' >{0}</td>", item.SOCIO, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); // SOCIO
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' >{0}</td>", item.MONEDA, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' onclick='return obtenerId({1},{2},{3},{4},{5});' >{0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INV_NET), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); // VALOR NETO
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' onclick='return obtenerId({1},{2},{3},{4},{5});' > {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INV_COLLECTN), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); // COBRADO
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' onclick='return obtenerId({1},{2},{3},{4},{5});' > {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INV_BALANCE), item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); // SALDO

                        if (item.INV_CN_REF != 0) //factRefNotCred
                            shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'><font color='red'>{0} </font></td>", item.INV_CN_REF, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO);
                        else
                            shtml.AppendFormat("<td style='cursor:pointer;' style='text-align:right; width:150px; padding-right:10px'> </td>");


                        if (item.INV_TYPE == 1 || item.INV_TYPE == 2)
                        {

                            switch (item.EST_FACT)
                            {
                                case 4: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", Constantes.EstadoFactura.ANULADA, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                case 2: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.CANCELADO, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                case 1: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='green'> {0} </font> </td>", Constantes.EstadoFactura.CANCELADA_PARCIAL, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                case 3: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.PENDIENTE_PAGO, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                //case 5: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.SOLICITUD_Nota_Credito, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                //case 6: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.SOLICITUD_QUIEBRA, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                case 11: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='green'> {0} </font></td>", Constantes.EstadoFactura.COBRANZA_DUDOSA, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                case 12: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.CASTIGO, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                default: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", Constantes.EstadoFactura.ANULADA, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                            }
                        }
                        else
                        {
                            if (item.EST_FACT == 2 && item.INV_IND_NC_TOTAL == 1) // NC - DEVOLUCION
                                shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'> <font color='green'> {0} </font> </td>", Constantes.EstadoFactura.NC_DEVOLUCION, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC);
                            else if (item.EST_FACT == 2 && item.INV_IND_NC_TOTAL == 0)
                            {
                                if (item.EST_FACT == 2 && item.INV_STATUS_NC == Constantes.EstadosFacturaValor.NC_ANULACION) shtml.AppendFormat("<td style='cursor:pointer;'  style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.NC_ANULACION);
                                if (item.EST_FACT == 2 && item.INV_STATUS_NC == Constantes.EstadosFacturaValor.NC_DESCUENTO) shtml.AppendFormat("<td style='cursor:pointer;'  style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.NC_DESCUENTO);
                                if (item.EST_FACT == 2 && item.INV_STATUS_NC == Constantes.EstadosFacturaValor.NC_ANULADO) shtml.AppendFormat("<td style='cursor:pointer;'  style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.NC_ANULADO);
                                if (item.EST_FACT == 2 && item.INV_STATUS_NC == Constantes.EstadosFacturaValor.NC_OTRO) shtml.AppendFormat("<td style='cursor:pointer;'  style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.NC_OTRO);
                            }
                            else
                            {
                                shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", "", item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); 
                            }

                        }

                        ////ESTADO SUNAT 
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3},{4},{5});' width:150px; padding-right:10px'>{0}</td>", item.ESTADO_SUNAT, item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO);
                        shtml.AppendFormat("<td style='text-align:center'>");
                        shtml.AppendFormat("<label onclick='verReporte({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta2.png' border=0 title='{1}'></label>&nbsp;&nbsp;", item.INV_ID, "Ver Comprobante");
                        shtml.AppendFormat("</td>");



                        shtml.AppendFormat("<td style='text-align:center ;style='width:80px'>");
                        //REENVIO DE COMPROBANTE A SUNAT
                        if (item.ESTADO_SUNAT != Constantes.Mensaje_Sunat.MSG_ACEPTADO && item.ESTADO_SUNAT != Constantes.Mensaje_Sunat.ERDTE && item.EST_FACT != 4 && (item.INV_DATE.Value.CompareTo(fechaMinAnulacion) >= 0))
                        {
                            if (item.TIPO_EMI_DOC == "A")
                                shtml.AppendFormat("<label onclick='ReenvioSunat({0},{1});'><img style='cursor:pointer;' src='../Images/iconos/undoMoney.png' border=0 title='{1}'></label>&nbsp;&nbsp;", item.INV_ID, item.INV_TYPE, "Reenvío de Comprobante");

                        }
                        shtml.AppendFormat("</td>");


                        shtml.AppendFormat("<td style='text-align:center ;style='width:80px'>");
                        //if (item.saldoFactura == item.valorFinal)
                        if (item.INV_BALANCE == item.INV_NET || (item.INV_TYPE == TiposDeDocmentos.NC && GlobalVars.Global.PermitirRevertNC == true))
                        {
                            DateTime fechaEmision = item.INV_DATE.Value;
                            DateTime FecAnulacion = new System.DateTime(fechaMinAnulacion.Year, fechaMinAnulacion.Month, fechaMinAnulacion.Day, 0, 0, 0);
                            DateTime FecEmision = new System.DateTime(fechaEmision.Year, fechaEmision.Month, fechaEmision.Day, 0, 0, 0);
                            int validarFechaAnulacion = FecEmision.CompareTo(FecAnulacion);

                            if (item.TIPO_EMI_DOC == "M" && item.INV_DATE.Value.Month == FechaSistema.Month && item.EST_FACT != 4 & item.INV_DATE.Value.Day <=FechaSistema.Day & Anular==true)
                            {
                                if (item.TIPO_EMI_DOC == "M")
                                {

                                }
                                shtml.AppendFormat("<a href=# onclick='eliminarFactura({0},{1},{2});'><img src='../Images/iconos/delete.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.INV_ID, 1, item.INV_TYPE, "Anular Comprobante");
                            }
                            //else if (item.TIPO_EMI_DOC == "A" && item.EST_FACT != 4    && (item.INV_DATE.Value.CompareTo(fechaMinAnulacion) >= 0)
                            else if (item.TIPO_EMI_DOC == "A" && item.EST_FACT != 4 && validarFechaAnulacion == 1 & Anular==true && item.ESTADO_SUNAT == Constantes.Mensaje_Sunat.MSG_ACEPTADO)
                            {
                                shtml.AppendFormat("<a href=# onclick='eliminarFactura({0},{1},{2});'><img src='../Images/iconos/delete.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.INV_ID, 2, item.INV_TYPE, "Anular Comprobante");
                            }
                        }
                        shtml.AppendFormat("</td>");
                        //// MANDAR A QUIEBRA
                        shtml.AppendFormat("<td style='text-align:center'>");
                        DateTime fechaEmision1 = item.INV_DATE.Value;
                        DateTime FecQuiebra = new System.DateTime(fechaMinQuiebra.Year, fechaMinQuiebra.Month, fechaMinQuiebra.Day, 0, 0, 0);
                        DateTime FecEmision1 = new System.DateTime(fechaEmision1.Year, fechaEmision1.Month, fechaEmision1.Day, 0, 0, 0);
                        int validarFechaQuiebra = FecEmision1.CompareTo(FecQuiebra);
                        //if (item.EST_FACT == 3 && opcAdm == 1 && validarFechaQuiebra == -1)
                        //{

                        //    if (blFactura.ValidaQuiebra(item.INV_ID) == 0)
                        //    {
                        //        shtml.AppendFormat("<label onclick='quiebraFactura({0});'><img style='cursor:pointer;' src='../Images/iconos/quiebra2.png' border=0 title='{1}'></label>&nbsp;&nbsp;", item.INV_ID, "Mandar a quiebra");
                        //    }
                        //    //else
                        //    //{
                        //    //    shtml.AppendFormat("<label><font color='blue'>Quiebra</font> </label>");
                        //    //}
                        //}
                        shtml.AppendFormat("</td>");

                        if (item.INV_BALANCE > 0 && (item.TIPO_EMI_DOC == "M" /*|| item.TIPO_EMI_DOC == null*/))
                        {
                            shtml.AppendFormat("<td style='text-align:center'>");
                            shtml.AppendFormat("<label onclick='SolicitarRuerimiento({0},{1});'><img style='cursor:pointer;' src='../Images/iconos/report_deta2.png' border=0 title='{1}'></label>&nbsp;&nbsp;", item.INV_ID ,Variables.MODIF_DOC_MANUAL, "Solicitar Requerimiento.");
                            shtml.AppendFormat("</td>");
                        }
                        else if (item.INV_BALANCE > 0)
                        {
                            shtml.AppendFormat("<td style='text-align:center'>");
                            shtml.AppendFormat("<label onclick='SolicitarRuerimiento({0},{1});'><img style='cursor:pointer;' src='../Images/iconos/report_deta2.png' border=0 title='{2}'></label>&nbsp;&nbsp;", item.INV_ID,Variables.MODIF_DOC_PEND_CANC, "Solicitar Requerimiento.");
                            shtml.AppendFormat("</td>");
                        }

                        //shtml.Append("</div>");
                        //shtml.Append("</td>");
                        //shtml.Append("</tr>");
                        //shtml.Append("</tr>");

                        //shtml.Append("<tr style='background-color:white'>");
                        //shtml.Append("<td></td>");
                        //shtml.Append("<td></td>");
                        //shtml.Append("<td style='width:100%' colspan='20'>");


                        //shtml.Append("<div style='display:none;' id='" + "div" + item.INV_ID.ToString() + "'  > ");
                        ////shtml.Append(getHtmlTableDetaLicenciaBorrador(item.id));

                        //shtml.Append("</div>");
                        //shtml.Append("</td>");
                        //shtml.Append("</tr>");
                        shtml.Append("</tr>");
                        shtml.Append("<tr><td colspan='30' style='background-color:#DBDBDE'></hr></td></tr>");

                    });
                }
                shtml.Append("</table>");
                //retorno.message = shtml.ToString();
                //retorno.Code = listar.Count;
                //retorno.result = 1;
            }
            catch (Exception ex)
            {
                //retorno.message = ex.Message;
                //retorno.result = 0;
                shtml = null;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarFacturaMasivaCabecera", ex);
            }
            return shtml;
            //var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
        }
        public StringBuilder ObtenerListarConsultaDocumento2(List<BEFactura> ListarConsultaDocumento)
        {
            //int MesesFechaQuiebra = 6;
            DateTime fechaMinAnulacion = Convert.ToDateTime(FechaSistema.AddDays(-GlobalVars.Global.DiasFechaAnulacion));
            DateTime fechaMinQuiebra = Convert.ToDateTime(FechaSistema.AddMonths(-MesesQuiebra));

            Sunat su = new Sunat();
            //su.ValidarRUC_Sunat("70037781");


            var usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoPerdilUsuario]);
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));
            BLFactura blFactura = new BLFactura();
            var lista = new BLConsultaDocumento().UsuariosAprobadosParaAnular();
            bool Anular = lista.Contains(Convert.ToInt32(usuario));
            //listar = ListaConsultaTmp;
            int habNC = 0;
            int penddiente = 0;
            bool habOficina = false;
            int idOficina = Convert.ToInt32(Session[Constantes.Sesiones.CodigoOficina]);
            if (idOficina == 10081 || idOficina == 10154)// TI - GENAREC
                habOficina = true;
            else
                habOficina = false;

            //Resultado retorno = new Resultado();
            StringBuilder shtml = new StringBuilder();
            try
            {

                shtml.Append("<table class='tblFacturaMasiva' border=0 width='100%;' class='k-grid k-widget' id='tblFacturaMasiva'>");
                shtml.Append("<thead><tr>");

                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' style='text-align:center;width:25px'>");
                ////if (estado == 0)
                ////{
                //shtml.Append("<input type='checkbox' id='idCheck' name='Check' class='Check' onchange='clickCheck()'>");
                ////}
                //shtml.Append("</th>");
                //shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Id</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >T.E</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Tipo</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Serie</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >#</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Fecha</th>");
                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Cancelación</th>");
                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Anulado</th>");
                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Ident.</th>");

                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >N° Ident.</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Socio de negocio</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Moneda</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Facturado</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Cobrado</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Saldo Pendiente</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Ref N.C</th>");
                //shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Tipo</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Estado</th>");
                shtml.Append("<th style'width:45px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix'>Estado Sunat</th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Ver</th>");

                shtml.Append("<th style'width:80px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                shtml.Append("<th style'width:80px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                //if (opcAdm == 1) { 
                //shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Quiebra</th>");

                //requerimientos de cambio doc manuales 
                shtml.Append("<th style'width:80px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                shtml.Append("<th class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' >Req.</th>");
                shtml.Append("<th style'width:80px' class='ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix' ></th>");
                //                 }
                if (ListarConsultaDocumento != null)
                {
                    //foreach (var item in ListarConsultaDocumento.OrderByDescending(x => x.NMR_SERIAL).OrderByDescending(x => x.INV_NUMBER)) //.OrderByDescending(x => x.id))
                    ListarConsultaDocumento.ForEach(item =>
                    {
                        if (item.INVT_DESC != "NC" && habOficina)
                        {
                            habNC = 1;
                        }
                        else
                        {
                            habNC = 0;
                        }
                        if (item.INV_BALANCE != 0)
                        {
                            penddiente = 1;
                        }
                        else
                        {
                            penddiente = 0;
                        }
                        shtml.Append("<tr style='background-color:white'>");
                        shtml.Append("</td>");
                        //shtml.Append("<td style='text-align:center;width:25px'>");
                        //if ((item.TIPO_EMI_DOC != "M" || item.TIPO_EMI_DOC == null) && (item.ESTADO_SUNAT != Constantes.Mensaje_Sunat.MSG_ACEPTADO && item.ESTADO_SUNAT != Constantes.Mensaje_Sunat.MSG_RECHAZADO))
                        //{
                        //    shtml.AppendFormat("<input type='checkbox' id='{0}' name='Check' class='Check' />", "chkFact" + item.INV_ID);
                        //}
                        //shtml.Append("</td>");
                        //shtml.AppendFormat("<td style='width:25px'> ");
                        ////shtml.AppendFormat("<a href=# onclick='verDetaFactura({0});'><img id='expand" + item.INV_ID + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.INV_ID);
                        //shtml.AppendFormat("<a href=# onclick='verDetalleDocumento({0});'><img id='expand" + item.INV_ID + "'  src='../Images/botones/more.png'  width=20px     title='Ver detalle.' alt='ver detalle.' border=0></a>", item.INV_ID);
                        //shtml.Append("</td>");
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:45px' onclick='return obtenerId2({0},{1});' class='IDCell' >{0}</td>", item.INV_ID, habNC);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId2({1},{2},{3},{4},{5});'>{0}</td>", item.TIPO_EMI_DOC, item.INV_ID, penddiente,habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); //TIPO EMI M o  A
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId2({1},{2},{3},{4},{5});' class='TipCell'>{0}</td>", item.INVT_DESC, item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); //TIPO DOC
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right' onclick='return obtenerId2({1},{2},{3},{4},{5});'>{0}</td>", item.NMR_SERIAL, item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); // SERIE
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right' onclick='return obtenerId2({1},{2},{3},{4},{5});' >{0}</td>", item.INV_NUMBER, item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); // NUMERO
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId2({1},{2},{3},{4},{5});' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.INV_DATE), item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO);// FECHA EMISION
                        //if (item.EST_FACT == 2)// Constantes.EstadoFactura.CANCELADO
                        //    shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3},{4},{5});' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.FECHA_CANCELACION), item.INV_ID, penddiente, item.INV_QUIEBRA, item.INV_NOTA_CREDITO);// FECHA CANCELACION
                        //else
                        //    shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3},{4},{5});' >{0}</td>", "", item.INV_ID, penddiente, item.INV_QUIEBRA, item.INV_NOTA_CREDITO);
                        //shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId({1},{2},{3},{4},{5});' >{0}</td>", String.Format("{0:dd/MM/yyyy}", item.INV_NULL), item.INV_ID, penddiente, item.INV_QUIEBRA, item.INV_NOTA_CREDITO);
                        //shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' >{0}</td>", item.TAXN_NAME, item.INV_ID, penddiente, item.INV_QUIEBRA, item.INV_NOTA_CREDITO);// TIPO ODC IDENTIFICACION


                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId2({1},{2},{3},{4},{5});' >{0}</td>", item.TAX_ID, item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); // NUMEROC
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId2({1},{2},{3},{4},{5});' >{0}</td>", item.SOCIO, item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); // SOCIO
                        shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId2({1},{2},{3},{4},{5});' >{0}</td>", item.MONEDA, item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO);
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' onclick='return obtenerId2({1},{2},{3},{4},{5});' >{0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INV_NET), item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); // VALOR NETO
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' onclick='return obtenerId2({1},{2},{3},{4},{5});' > {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INV_COLLECTN), item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); // COBRADO
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:right; width:100px; padding-right:10px' onclick='return obtenerId2({1},{2},{3},{4},{5});' > {0}</td>", string.Format("{0:# ### ### ##0.##########}", item.INV_BALANCE), item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); // SALDO

                        if (item.INV_CN_REF != 0) //factRefNotCred
                            shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId2({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'><font color='red'>{0} </font></td>", item.INV_CN_REF, item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO);
                        else
                            shtml.AppendFormat("<td style='cursor:pointer;' style='text-align:right; width:150px; padding-right:10px'> </td>");


                        if (item.INV_TYPE == 1 || item.INV_TYPE == 2)
                        {

                            switch (item.EST_FACT)
                            {
                                case 4: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId2({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", Constantes.EstadoFactura.ANULADA, item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                case 2: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId2({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.CANCELADO, item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                case 1: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId2({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='green'> {0} </font> </td>", Constantes.EstadoFactura.CANCELADA_PARCIAL, item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                case 3: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId2({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.PENDIENTE_PAGO, item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                //case 5: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.SOLICITUD_Nota_Credito, item.INV_ID, penddiente, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                //case 6: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.SOLICITUD_QUIEBRA, item.INV_ID, penddiente, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                case 11: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId2({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='green'> {0} </font></td>", Constantes.EstadoFactura.COBRANZA_DUDOSA, item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                case 12: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId2({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='red'> {0} </font></td>", Constantes.EstadoFactura.CASTIGO, item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                                default: shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId2({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", Constantes.EstadoFactura.ANULADA, item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO); break;
                            }
                        }
                        else
                        {
                            if (item.EST_FACT == 2 && item.INV_IND_NC_TOTAL == 1) // NC - DEVOLUCION
                                shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId2({1},{2},{3});' style='text-align:right; width:150px; padding-right:10px'> <font color='green'> {0} </font> </td>", Constantes.EstadoFactura.NC_DEVOLUCION, item.INV_ID, penddiente);
                            else if (item.EST_FACT == 2 && item.INV_IND_NC_TOTAL == 0)
                            {
                                if (item.EST_FACT == 2 && item.INV_STATUS_NC == Constantes.EstadosFacturaValor.NC_ANULACION) shtml.AppendFormat("<td style='cursor:pointer;'  style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.NC_ANULACION);
                                if (item.EST_FACT == 2 && item.INV_STATUS_NC == Constantes.EstadosFacturaValor.NC_DESCUENTO) shtml.AppendFormat("<td style='cursor:pointer;'  style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.NC_DESCUENTO);
                                if (item.EST_FACT == 2 && item.INV_STATUS_NC == Constantes.EstadosFacturaValor.NC_ANULADO) shtml.AppendFormat("<td style='cursor:pointer;'  style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.NC_ANULADO);
                                if (item.EST_FACT == 2 && item.INV_STATUS_NC == Constantes.EstadosFacturaValor.NC_OTRO) shtml.AppendFormat("<td style='cursor:pointer;'  style='text-align:right; width:150px; padding-right:10px'> <font color='blue'> {0} </font> </td>", Constantes.EstadoFactura.NC_OTRO);
                            }
                            else
                            {
                                shtml.AppendFormat("<td style='cursor:pointer;' onclick='return obtenerId({1},{2},{3},{4},{5});' style='text-align:right; width:150px; padding-right:10px'> <font color='black'> {0} </font></td>", "", item.INV_ID, item.INV_NULL == null ? 0 : 1, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO);
                            }
                        }

                        ////ESTADO SUNAT 
                        shtml.AppendFormat("<td style='cursor:pointer;text-align:center;' onclick='return obtenerId2({1},{2},{3},{4},{5});' width:150px; padding-right:10px'>{0}</td>", item.ESTADO_SUNAT, item.INV_ID, penddiente, habNC, item.INV_QUIEBRA, item.INV_NOTA_CREDITO);
                        shtml.AppendFormat("<td style='text-align:center'>");
                        shtml.AppendFormat("<label onclick='verReporte({0});'><img style='cursor:pointer;' src='../Images/iconos/report_deta2.png' border=0 title='{1}'></label>&nbsp;&nbsp;", item.INV_ID, "Ver Comprobante");
                        shtml.AppendFormat("</td>");



                        shtml.AppendFormat("<td style='text-align:center ;style='width:80px'>");
                        //REENVIO DE COMPROBANTE A SUNAT
                        if (item.ESTADO_SUNAT != Constantes.Mensaje_Sunat.MSG_ACEPTADO && item.ESTADO_SUNAT != Constantes.Mensaje_Sunat.ERDTE && item.EST_FACT != 4 && (item.INV_DATE.Value.CompareTo(fechaMinAnulacion) >= 0))
                        {
                            if (item.TIPO_EMI_DOC == "A")
                                shtml.AppendFormat("<label onclick='ReenvioSunat({0},{1});'><img style='cursor:pointer;' src='../Images/iconos/undoMoney.png' border=0 title='{1}'></label>&nbsp;&nbsp;", item.INV_ID, item.INV_TYPE, "Reenvío de Comprobante");

                        }
                        shtml.AppendFormat("</td>");


                        shtml.AppendFormat("<td style='text-align:center ;style='width:80px'>");
                        //if (item.saldoFactura == item.valorFinal)
                        if (item.INV_BALANCE == item.INV_NET || (item.INV_TYPE == TiposDeDocmentos.NC && GlobalVars.Global.PermitirRevertNC == true))
                        {
                            DateTime fechaEmision = item.INV_DATE.Value;
                            DateTime FecAnulacion = new System.DateTime(fechaMinAnulacion.Year, fechaMinAnulacion.Month, fechaMinAnulacion.Day, 0, 0, 0);
                            DateTime FecEmision = new System.DateTime(fechaEmision.Year, fechaEmision.Month, fechaEmision.Day, 0, 0, 0);
                            int validarFechaAnulacion = FecEmision.CompareTo(FecAnulacion);

                            if (item.TIPO_EMI_DOC == "M" && item.INV_DATE.Value.Month == FechaSistema.Month && item.EST_FACT != 4 & item.INV_DATE.Value.Day <= FechaSistema.Day & Anular == true)
                            {
                                if (item.TIPO_EMI_DOC == "M")
                                {

                                }
                                shtml.AppendFormat("<a href=# onclick='eliminarFactura({0},{1},{2});'><img src='../Images/iconos/delete.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.INV_ID, 1, item.INV_TYPE, "Anular Comprobante");
                            }
                            //else if (item.TIPO_EMI_DOC == "A" && item.EST_FACT != 4    && (item.INV_DATE.Value.CompareTo(fechaMinAnulacion) >= 0)
                            else if (item.TIPO_EMI_DOC == "A" && item.EST_FACT != 4 && validarFechaAnulacion == 1 & Anular == true && item.ESTADO_SUNAT == Constantes.Mensaje_Sunat.MSG_ACEPTADO)
                            {
                                shtml.AppendFormat("<a href=# onclick='eliminarFactura({0},{1},{2});'><img src='../Images/iconos/delete.png' border=0 title='{1}'></a>&nbsp;&nbsp;", item.INV_ID, 2, item.INV_TYPE, "Anular Comprobante");
                            }
                        }
                        shtml.AppendFormat("</td>");
                        //// MANDAR A QUIEBRA
                        shtml.AppendFormat("<td style='text-align:center'>");
                        DateTime fechaEmision1 = item.INV_DATE.Value;
                        DateTime FecQuiebra = new System.DateTime(fechaMinQuiebra.Year, fechaMinQuiebra.Month, fechaMinQuiebra.Day, 0, 0, 0);
                        DateTime FecEmision1 = new System.DateTime(fechaEmision1.Year, fechaEmision1.Month, fechaEmision1.Day, 0, 0, 0);
                        int validarFechaQuiebra = FecEmision1.CompareTo(FecQuiebra);
                        //if (item.EST_FACT == 3 && opcAdm == 1 && validarFechaQuiebra == -1)
                        //{

                        //    if (blFactura.ValidaQuiebra(item.INV_ID) == 0)
                        //    {
                        //        shtml.AppendFormat("<label onclick='quiebraFactura({0});'><img style='cursor:pointer;' src='../Images/iconos/quiebra2.png' border=0 title='{1}'></label>&nbsp;&nbsp;", item.INV_ID, "Mandar a quiebra");
                        //    }
                        //    //else
                        //    //{
                        //    //    shtml.AppendFormat("<label><font color='blue'>Quiebra</font> </label>");
                        //    //}
                        //}
                        shtml.AppendFormat("</td>");

                        if (item.INV_BALANCE > 0 && (item.TIPO_EMI_DOC == "M" /*|| item.TIPO_EMI_DOC == null*/))
                        {
                            shtml.AppendFormat("<td style='text-align:center'>");
                            shtml.AppendFormat("<label onclick='SolicitarRuerimiento({0},{1});'><img style='cursor:pointer;' src='../Images/iconos/report_deta2.png' border=0 title='{1}'></label>&nbsp;&nbsp;", item.INV_ID, Variables.MODIF_DOC_MANUAL, "Solicitar Requerimiento.");
                            shtml.AppendFormat("</td>");
                        }
                        else if (item.INV_BALANCE > 0)
                        {
                            shtml.AppendFormat("<td style='text-align:center'>");
                            shtml.AppendFormat("<label onclick='SolicitarRuerimiento({0},{1});'><img style='cursor:pointer;' src='../Images/iconos/report_deta2.png' border=0 title='{2}'></label>&nbsp;&nbsp;", item.INV_ID, Variables.MODIF_DOC_PEND_CANC, "Solicitar Requerimiento.");
                            shtml.AppendFormat("</td>");
                        }

                        //shtml.Append("</div>");
                        //shtml.Append("</td>");
                        //shtml.Append("</tr>");
                        //shtml.Append("</tr>");

                        //shtml.Append("<tr style='background-color:white'>");
                        //shtml.Append("<td></td>");
                        //shtml.Append("<td></td>");
                        //shtml.Append("<td style='width:100%' colspan='20'>");


                        //shtml.Append("<div style='display:none;' id='" + "div" + item.INV_ID.ToString() + "'  > ");
                        ////shtml.Append(getHtmlTableDetaLicenciaBorrador(item.id));

                        //shtml.Append("</div>");
                        //shtml.Append("</td>");
                        //shtml.Append("</tr>");
                        shtml.Append("</tr>");
                        shtml.Append("<tr><td colspan='30' style='background-color:#DBDBDE'></hr></td></tr>");

                    });
                }
                shtml.Append("</table>");
                //retorno.message = shtml.ToString();
                //retorno.Code = listar.Count;
                //retorno.result = 1;
            }
            catch (Exception ex)
            {
                //retorno.message = ex.Message;
                //retorno.result = 0;
                shtml = null;
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "ListarFacturaMasivaCabecera", ex);
            }
            return shtml;
            //var jsonResult = Json(retorno, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
        }

        #endregion


        public JsonResult ObtenerOficinaConsultaDocumeno()
        {
            Resultado retorno = new Resultado();
            try
            {
                if (!isLogout(ref retorno))
                {
                    decimal idOfiTmp = Convert.ToDecimal(Session[Constantes.Sesiones.CodigoOficina]);
                    var datos = new BLOffices().ObtenerNombre(GlobalVars.Global.OWNER, idOfiTmp);
                    if (datos != null)
                    {
                        retorno.valor = datos.OFF_NAME;
                        retorno.Code = Convert.ToInt32(idOfiTmp);
                        retorno.result = 1;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "Descripción de la oficina no encontrada";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "ObtenerNombreOficina", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }



        public JsonResult ReenviarDocumentosSeleccionados(List<BEFactura> ReglaValor)
        {

            Resultado retorno = new Resultado();
            try
            {


                //    if (!isLogout(ref retorno))
                // {

                var MSG_SUNAT = "";
                var cant_anulado = 0;

                //if (GlobalVars.Global.FE == true)
                if (true)
                {
                    #region  MASIVO
                    if (ReglaValor.Count > 0 && ReglaValor.Count > 3 && ReglaValor != null)
                    {
                        var vExtras = new BLExtras().ListarExtras(GlobalVars.Global.OWNER, 0);
                        List<BECabeceraFactura> vCabecera = new BLFactura().ListaCabezeraMasivaSunat(GlobalVars.Global.OWNER, ReglaValor);
                        List<BEDetalleFactura> vDetalle = new BLFactura().ListaDetalleaMasivaSunat(GlobalVars.Global.OWNER, ReglaValor); 
                        List<BEDescuentoRecargo> vDescuento = new BLDescuentoRecargo().ListarDescuentoFactura(GlobalVars.Global.OWNER, 0);

                        foreach (var s in vCabecera)
                        {


                            #region Consulta y vuelve a Actualizar Estado SUNAT

                            var RespuestaConsultaSunat = FE.ConsultaEstado(s.INV_ID);

                            if (RespuestaConsultaSunat.Contains("NBD")) // SI NO EXISTE EN LA SUITE
                            {
                                List<BECabeceraFactura> vCabeceraIndividual = vCabecera.Where(x => x.INV_ID == s.INV_ID).ToList();
                                List<BEDetalleFactura> vDetalleIndividual = vDetalle.Where(x => x.INV_ID == s.INV_ID).ToList();

                                MSG_SUNAT = FE.EnvioComprobanteElectronicoMasivoAct(vExtras, vCabeceraIndividual, vDetalleIndividual, vDescuento);

                                if (MSG_SUNAT == Constantes.Mensaje_Sunat.MSG_DOK_SUNAT)
                                    cant_anulado += 1;

                                //if (MSG_SUNAT == Constantes.Mensaje_Sunat.MSG_EXISTE_SUNAT)
                                //{
                                //    FE.ActualizarEstadoSunat(s.INV_ID, Constantes.Mensaje_Sunat.MSG_SUNAT_DOK, Constantes.Mensaje_Sunat.MSG_ACEPTADO);
                                //    cant_anulado += 1;
                                //}


                                vCabeceraIndividual = new List<BECabeceraFactura>();
                                vDetalleIndividual = new List<BEDetalleFactura>();
                                //una vez enviado a SUNAT SE vuelve a consultar
                                RespuestaConsultaSunat = FE.ConsultaEstado(s.INV_ID);
                            }
                            else
                            {
                                if (RespuestaConsultaSunat.Contains("DOK"))
                                    cant_anulado += Variables.Uno;
                            }


                            EvaluaConsultaSunat(s.INV_ID, RespuestaConsultaSunat);
                        }
                            #endregion

                            //    TotalNoEmitidas += 1;

                        
                    }
                    #endregion
                    #region INDIVIDUAL FA BO NOTAS DE CREDITO
                    else if (ReglaValor.Count > 0 && ReglaValor.Count <= 3 && ReglaValor != null)
                    {

                        foreach (var item in ReglaValor)
                        {
                            if (item.INVT_DESC == TiposDeDocmentos.NOTA_CREDITO)
                            {
                                try
                                {
                                    

                                    #region Consulta y vuelve a Actualizar Estado SUNAT

                                    var RespuestaConsultaSunat = FE.ConsultaEstado(item.INV_ID);

                                    if (RespuestaConsultaSunat.Contains("NBD")) // SI NO EXISTE EN LA SUITE
                                    {
                                        MSG_SUNAT = FE.EnvioNotaCredito(item.INV_ID);

                                        if (MSG_SUNAT == Constantes.Mensaje_Sunat.MSG_DOK_SUNAT)
                                            cant_anulado++;

                                        RespuestaConsultaSunat = FE.ConsultaEstado(item.INV_ID);
                                    }
                                    else
                                    {
                                        if (RespuestaConsultaSunat.Contains("DOK"))
                                            cant_anulado += Variables.Uno;
                                    }


                                    EvaluaConsultaSunat(item.INV_ID, RespuestaConsultaSunat);

                                    #endregion



                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            else
                            {
                                try
                                {
                                    //if()
                                    

                                    #region Consulta y vuelve a Actualizar Estado SUNAT

                                    var RespuestaConsultaSunat = FE.ConsultaEstado(item.INV_ID);

                                    if (RespuestaConsultaSunat.Contains("NBD")) // SI NO EXISTE EN LA SUITE
                                    {
                                        MSG_SUNAT = FE.EnvioComprobanteElectronico(item.INV_ID);

                                        if (MSG_SUNAT == Constantes.Mensaje_Sunat.MSG_DOK_SUNAT)
                                            cant_anulado++;

                                        RespuestaConsultaSunat = FE.ConsultaEstado(item.INV_ID);
                                    }
                                    else
                                    {
                                        if (RespuestaConsultaSunat.Contains("DOK"))
                                            cant_anulado += Variables.Uno;
                                    }


                                    EvaluaConsultaSunat(item.INV_ID,RespuestaConsultaSunat);

                                    #endregion

                                }
                                catch (Exception ex)
                                {

                                }

                            }

                        }

                    }
                    #endregion
                }

                retorno.result = 1;
                retorno.message = (MSG_SUNAT + " SE ENVIARON " + cant_anulado.ToString() + " DE " + ReglaValor.Count().ToString());
                //}

                //retorno.result = 1;

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "EnviarFacturasSeleccionadas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ReenviarAnuladosDocumentosSeleccionados(List<BEFactura> ReglaValor)
        {

            Resultado retorno = new Resultado();
            try
            {


                if (!isLogout(ref retorno))
                {

                    var MSG_SUNAT = "";
                    var cant_anulado = 0;

                    if (GlobalVars.Global.FE == true)
                    {

                        if (ReglaValor.Count > 0 && ReglaValor != null)
                        {
                            List<BECabeceraFactura> vCabecera = new BLFactura().ListaCabezeraMasivaSunat(GlobalVars.Global.OWNER, ReglaValor);

                            foreach (var item in vCabecera)
                            {
                                try
                                {
                                    List<BECabeceraFactura> vCabeceraIndividual = vCabecera.Where(x => x.INV_ID == item.INV_ID).ToList();

                                    MSG_SUNAT = FE.AnularDocumentoMasivo(vCabeceraIndividual, "ERROR DE ENVIO A LA SUITE");

                                    if (MSG_SUNAT == Constantes.Mensaje_Sunat.MSG_ANULACION_EXITOSA)
                                    {
                                        cant_anulado++;
                                        FE.ActualizarEstadoSunat(item.INV_ID, "DOK", "DOCUMENTO ENVIADO A SUNAT");
                                    }

                                    //var Rspt = MSG_SUNAT.Contains("Ya se encuentra registrado"); //like '%amp%' BOOL

                                    //if (Rspt)
                                    //{
                                    //    FE.ActualizarEstadoSunat(item.INV_ID, Constantes.Mensaje_Sunat.MSG_SUNAT_DOK, Constantes.Mensaje_Sunat.MSG_ACEPTADO);
                                    //}

                                }
                                catch (Exception ex)
                                {

                                }

                            }
                        }
                    }

                    retorno.result = 1;
                    retorno.message = (MSG_SUNAT + " SE ENVIARON " + cant_anulado.ToString() + " DE " + ReglaValor.Count().ToString());
                }

                retorno.result = 1;

            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "EnviarFacturasSeleccionadas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReenviarDocumentosMasivosEmision(DateTime fechaInicio, DateTime fechaFin, decimal Oficina)
        {

            Resultado retorno = new Resultado();
            try
            {
                var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));


                if (opcAdm == Variables.NO) // SI NO PASA LA VALIDACION  SELECCIONAR LA OFICINA A LA QUE PERTENECE EL USUARIO
                    Oficina = oficina;


                if (opcAdm == Variables.SI) // SI NO PASA LA VALIDACION  SELECCIONAR LA OFICINA A LA QUE PERTENECE EL USUARIO
                    Oficina = Variables.Cero;


                var MSG_SUNAT = "";
                var cant_anulado = Variables.Cero;
                List<BECabeceraFactura> vCabecera = new List<BECabeceraFactura>();

                //if (GlobalVars.Global.FE == true)
                if (true)
                {
                        var vExtras = new BLExtras().ListarExtras(GlobalVars.Global.OWNER, Variables.Cero);
                        vCabecera = new BLFactura().ListaCabezeraMasivaSunatEmiMensualLocTrans(fechaInicio,fechaFin,Oficina);
                        List<BEDetalleFactura> vDetalle = new BLFactura().ListaDetalleaMasivaSunatEmisionMensualLocTrans(fechaInicio, fechaFin, Oficina);
                        List<BEDescuentoRecargo> vDescuento = new BLDescuentoRecargo().ListarDescuentoFactura(GlobalVars.Global.OWNER, Variables.Cero);

                    if (vCabecera.Count > 0 && vCabecera != null)
                    {
                        foreach (var s in vCabecera)
                        {
                            List<BECabeceraFactura> vCabeceraIndividual = vCabecera.Where(x => x.INV_ID == s.INV_ID).ToList();
                            List<BEDetalleFactura> vDetalleIndividual = vDetalle.Where(x => x.INV_ID == s.INV_ID).ToList();

                            #region Consulta y vuelve a Actualizar Estado SUNAT

                            var RespuestaConsultaSunat = FE.ConsultaEstado(s.INV_ID);

                            if (RespuestaConsultaSunat.Contains("NBD")) // SI NO EXISTE EN LA SUITE
                            {
                                MSG_SUNAT = FE.EnvioComprobanteElectronicoMasivoAct(vExtras, vCabeceraIndividual, vDetalleIndividual, vDescuento);
                                if (MSG_SUNAT == Constantes.Mensaje_Sunat.MSG_DOK_SUNAT)
                                    cant_anulado += Variables.Uno;

                                RespuestaConsultaSunat = FE.ConsultaEstado(s.INV_ID);
                            }
                            else
                            {
                                if (RespuestaConsultaSunat.Contains("DOK"))
                                    cant_anulado += Variables.Uno;
                            }


                            EvaluaConsultaSunat(s.INV_ID, RespuestaConsultaSunat);

                            #endregion

                           
                            vCabeceraIndividual = new List<BECabeceraFactura>();
                            vDetalleIndividual = new List<BEDetalleFactura>();
                        }
                    }

                }

                retorno.result = Variables.Uno;
                retorno.message = (MSG_SUNAT + " SE ENVIARON " + cant_anulado.ToString() + " DE " + vCabecera.Count().ToString());

            }
            catch (Exception ex)
            {
                retorno.result = Variables.Cero;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "EnviarFacturasSeleccionadas", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EnviarDOcumentosManuales(DateTime fechaInicio, DateTime fechaFin, decimal Oficina)
        {

            Resultado retorno = new Resultado();
            try
            {
                var oficina = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
                var opcAdm = new BLREGISTRO_CAJA().ValidarReporteOficinaDL(Convert.ToInt32(oficina));


                if (opcAdm == Variables.NO) // SI NO PASA LA VALIDACION  SELECCIONAR LA OFICINA A LA QUE PERTENECE EL USUARIO
                    Oficina = oficina;


                if (opcAdm == Variables.SI && Oficina==Variables.Cero) // SI NO PASA LA VALIDACION  SELECCIONAR LA OFICINA A LA QUE PERTENECE EL USUARIO
                    Oficina = Variables.Cero;


                var MSG_SUNAT = "";
                var cant_anulado = Variables.Cero;
                List<BECabeceraFactura> vCabecera = new List<BECabeceraFactura>();

                if (GlobalVars.Global.FE == true)
                {
                    var vExtras = new BLExtras().ListarExtras(GlobalVars.Global.OWNER, Variables.Cero);
                    vCabecera = new BLFactura().ListaCabezeraMasivaSunatManuales(fechaInicio, fechaFin, Oficina);
                    List<BEDetalleFactura> vDetalleIndividual = null;
                    //List<BEDescuentoRecargo> vDescuento = new BLDescuentoRecargo().ListarDescuentoFactura(GlobalVars.Global.OWNER, Variables.Cero);

                    if (vCabecera.Count > 0 && vCabecera != null)
                    {
                        foreach (var s in vCabecera)
                        {
                            List<BECabeceraFactura> vCabeceraIndividual = vCabecera.Where(x => x.INV_ID == s.INV_ID).ToList();

                            #region Consulta y vuelve a Actualizar Estado SUNAT

                            var RespuestaConsultaSunat = FE.ConsultaEstado(s.INV_ID);

                            if (RespuestaConsultaSunat.Contains("NBD")) // SI NO EXISTE EN LA SUITE
                            {
                                MSG_SUNAT = FE2.EnvioComprobanteContingencia(s.INV_ID);
                                if (MSG_SUNAT == Constantes.Mensaje_Sunat.MSG_DOK_SUNAT)
                                    cant_anulado += Variables.Uno;

                                vCabeceraIndividual = new List<BECabeceraFactura>();
                                vDetalleIndividual = new List<BEDetalleFactura>();

                                RespuestaConsultaSunat = FE.ConsultaEstado(s.INV_ID);
                            }
                            else
                            {
                                if (RespuestaConsultaSunat.Contains("DOK"))
                                    cant_anulado += Variables.Uno;
                            }


                            EvaluaConsultaSunat(s.INV_ID, RespuestaConsultaSunat);

                            #endregion



                        }
                    }

                }

                retorno.result = Variables.Uno;
                retorno.message = (MSG_SUNAT + " SE ENVIARON " + cant_anulado.ToString() + " DE " + vCabecera.Count().ToString());

            }
            catch (Exception ex)
            {
                retorno.result = Variables.Cero;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "EnviarFacturasManualess", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public void EvaluaConsultaSunat(decimal CodigoDocumento,string RespuestaConsultaSunat)
        {
            if (RespuestaConsultaSunat.Contains("DOK"))
            {
                FE.ActualizarEstadoSunat(CodigoDocumento, "DOK", "PROCESADO POR SUNAT");
            }
            else if (RespuestaConsultaSunat.Contains("RCH"))
            {
                FE.ActualizarEstadoSunat(CodigoDocumento, "RCH", "RECHAZADO POR SUNAT");
            }
            else if (RespuestaConsultaSunat.Contains("FIR"))
            {
                FE.ActualizarEstadoSunat(CodigoDocumento, "FIR", "REVISION POR SUNAT");
            }
            else if (RespuestaConsultaSunat.Contains("PEN"))
            {
                FE.ActualizarEstadoSunat(CodigoDocumento, "PEN", "PENDIENTE DE REVISION POR SUNAT");
            }
            else if (RespuestaConsultaSunat.Contains("ERDTE"))
            {
                FE.ActualizarEstadoSunat(CodigoDocumento, "ERDTE", "DOCUMENTO YA EXISTE EN LA SUITE");
            }
            else if (RespuestaConsultaSunat.Contains("ANU") && RespuestaConsultaSunat.Contains("Anulacion Aceptada"))
            {
                FE.ActualizarEstadoSunat(CodigoDocumento, "DOK", "DOCUMENTO ANULADO EXITOSAMENTE");
            }
            else
                FE.ActualizarEstadoSunat(CodigoDocumento, "", "VOLVER A CONSULTAR");

        }


        public ActionResult ReporteTipo()
        {
            Resultado retorno = new Resultado();

            try
            {

                List<BEFactura> listar = new List<BEFactura>();
                listar = FacturaTemp;


                if (listar != null && listar.Count > 0)
                {
                    retorno.result = 1;
                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                }
                else
                {
                    retorno.result = 0;
                    retorno.message = "REALIZAR BUSQUEDA ANTES DE (VER PDF / REP. EXCEL) |  LA BUSQUEDA NO DEVOLVIO NINGUN RESULTADO";
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte de FACTURA CONSULTA", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ReporteFacturasConsultadas(string formato)
        {
            string format = formato;
            int oficina_id = 0;
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Oficina]);
            string usuario = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.Usuario]);
            oficina_id = Convert.ToInt32(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);

            Resultado retorno = new Resultado();

            try
            {

                List<BEFactura> lstReporte = new List<BEFactura>();
                lstReporte = FacturaTemp;

                if (lstReporte.Count() > 0 && lstReporte != null)
                {
                    LocalReport localReport = new LocalReport();

                    localReport.ReportPath = Server.MapPath("~/Reportes/R_CONSULTA_DOC.rdlc");

                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = lstReporte;
                    localReport.DataSources.Add(reportDataSource);

                    ReportParameter parametroNomusu = new ReportParameter();
                    parametroNomusu = new ReportParameter("NombreUsuario", oficina);
                    localReport.SetParameters(parametroNomusu);

                    ReportParameter parametroNomoficina = new ReportParameter();
                    parametroNomoficina = new ReportParameter("NombreOficina", usuario);
                    localReport.SetParameters(parametroNomoficina);

                    ReportParameter fecha = new ReportParameter();
                    fecha = new ReportParameter("FechaImpresion", DateTime.Now.ToShortDateString());
                    localReport.SetParameters(fecha);

                    string reportType = format;
                    string mimeType;
                    string encoding;

                    //aqui le cambie solo dejar string fileNameExtension en caso de error
                    string fileNameExtension;

                    //CODIGO REPETIBLE
                    //The DeviceInfo settings should be changed based on the reportType            
                    //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
                    string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>" + format + "</OutputFormat>" +
                    //  "  <PageWidth>8.5in</PageWidth>" +
                    "  <PageWidth>9in</PageWidth>" +
                    //"  <PageHeight>11in</PageHeight>" +
                    "  <PageHeight>16.3in</PageHeight>" +
                    "  <MarginTop>0.0in</MarginTop>" +
                    "  <MarginLeft>0.3in</MarginLeft>" +
                    "  <MarginRight>0.0in</MarginRight>" +
                    "  <MarginBottom>0.3in</MarginBottom>" +
                    "</DeviceInfo>";

                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;

                    renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    retorno.message = Constantes.MensajeGenerico.MSG_OK_GENERICO;
                    retorno.result = 1;
                    localReport.DisplayName = "Reporte de consulta de Licencias";

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
                    retorno.message = "REALIZAR LA CONSULTA ANTES DE MOSTRAR EL PDF | EL REPORTE NO TIENE REGISTROS QUE MOSTRAR";
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Reporte de FACTURA CONSULTA", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Validar_Anulacion_X_Modalidad(decimal inv_id)
        {
            Resultado retorno = new Resultado();
            string oficina = Convert.ToString(Session[Proyect_Apdayc.Clases.Constantes.Sesiones.CodigoOficina]);
            try
            {
                if (!isLogout(ref retorno))
                {
                    BLConsultaDocumento bl = new BLConsultaDocumento();
                    int result = bl.VALIDAR_ANULACION_X_MODALIDAD(inv_id,Convert.ToDecimal(oficina));
                    if (result >= 1)
                    {                       
                        retorno.result = 1;
                        retorno.message = "Usted no cuenta con permisos para anular esta factura.";
                    }
                    else
                    {
                        retorno.result = 0;                       
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Validar_Anulacion_X_Modalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarNC2(decimal idFactura, DateTime fechaEmision, int TipoNC, decimal TextoTipoNC,string TipoSunat,string Observacion, string serieNC)
        {
            Resultado retorno = new Resultado();
            
            try
            {
                if (!isLogout(ref retorno))
                {
                    BENotaCredito bENotaCredito = new BENotaCredito();
                    BLConsultaDocumento bl = new BLConsultaDocumento();
                    bENotaCredito.facturaId = idFactura;
                    bENotaCredito.fechaEmision = fechaEmision;
                    bENotaCredito.tipoNC = TipoNC;
                    bENotaCredito.textoTipoNC = TextoTipoNC;
                    bENotaCredito.TipoSunat = TipoSunat;
                    bENotaCredito.Observacion = Observacion;
                    bENotaCredito.UsuarioCreacion = UsuarioActual;

                    int result = bl.GuardarNuevaNotaCredito(bENotaCredito);
                    if (GlobalVars.Global.FE == true)
                    {
                        MSG_SUNAT = FE.EnvioNotaCredito(result);
                    }
                    var vCabecera = new BLCabeceraFactura().ListarCabeceraFacturaNc(GlobalVars.Global.OWNER, result);
                    decimal Id = vCabecera.FirstOrDefault().Id_Ref;
                    var vReferencia = new BLReferencia().ListarRefFactura(GlobalVars.Global.OWNER, Id);
                    string vReferenciaId = vReferencia.FirstOrDefault().FolioRef;
                    if (result >= 1)
                    {
                        retorno.result = 1;
                        retorno.message = "Se guardó la Nota de Crédito satisfactoriamente con Serie Nc "+ serieNC+" y referencial "+ vReferenciaId + " "+ MSG_SUNAT;
                    }
                    else
                    {
                        retorno.result = 0;
                        retorno.message = "No se puedo emtimir la Nota de crédito";

                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = Constantes.MensajeGenerico.MSG_ERROR_GENERICO + ".." + ex.Message;
                ucLogApp.ucLog.GrabarLogError("SGRDA", UsuarioActual, "Validar_Anulacion_X_Modalidad", ex);
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}
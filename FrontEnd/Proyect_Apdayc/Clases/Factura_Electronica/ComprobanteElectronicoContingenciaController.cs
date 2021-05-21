using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proyect_Apdayc.Clases.DTO;
using Proyect_Apdayc.Clases;
using System.Text;
using SGRDA.Entities;
using SGRDA.BL;
using SGRDA.Entities.FacturaElectronica;
using SGRDA.BL.FacturaElectronica;
//using Proyect_Apdayc.SGRDA_Carga;
using Proyect_Apdayc.SGRDA_Carga_Contingencia;
using Proyect_Apdayc.SGRDA_Estado;
using Proyect_Apdayc.SGRDA_Bajas;
using System.Globalization;
using SGRDA.Utility;
using System.Threading;

namespace Proyect_Apdayc.Clases.Factura_Electronica
{
    public class ComprobanteElectronicoContingenciaController : Base
    {
        // GET: ComprobanteElectronicoContingencia
        public const string nomAplicacion = "SRGDA";
        public string EnvioComprobanteContingencia(decimal idfactura)
        {
            string mensaje = "";
            try
            {
                //INVOCAR LA WEB SERVICE

                SGRDA_Carga_Contingencia.CustomerETDLoadASPSoapClient WS = new CustomerETDLoadASPSoapClient();
                //SGRDA_Estado.Service1SoapClient wsEstado = new Service1SoapClient();

                //========================================================================================
                var vExtras = new BLExtras().ListarExtras(GlobalVars.Global.OWNER, idfactura);
                var vCabecera = new BLCabeceraFactura().ListarCabeceraFactura(GlobalVars.Global.OWNER, idfactura);
                var vDetalle = new BLDetalleFactura().ListarDetalleFacturaMasiva(GlobalVars.Global.OWNER, idfactura);
                var vDescuento = new BLDescuentoRecargo().ListarDescuentoFactura(GlobalVars.Global.OWNER, idfactura);

                //===================================CONVERTIR PARA EL RESULTADO A SUNAT=====================================================

                string Ruc_Msg = vCabecera.FirstOrDefault().TipoDTE;
                int Tipo_Doc_Msg = Convert.ToInt32(vCabecera.FirstOrDefault().TipoDTE);
                int Folio_Msg = Convert.ToInt32(vCabecera.FirstOrDefault().Correlativo);
                string Serie_Msg = vCabecera.FirstOrDefault().Serie;

                //==========================================================================================
                Extras obj_Extras = new Extras();
                Encabezado obj_Encabezado = new Encabezado();
                CamposHead obj_CamposHead = new CamposHead();
                CamposDetalle obj_Detalle = new CamposDetalle();
                Referencias obj_Referencia = new Referencias();
                DescuentosRecargosyOtros obj_DescuentosRecargosyOtros = new DescuentosRecargosyOtros();
                Descuentos obj_Descuentos = new Descuentos();
                DatosAdicionales obj_DatosAdicionales = new DatosAdicionales();
                DatosAdicionales obj_DatosAdicionales2 = new DatosAdicionales();
                DatosAdicionales obj_DatosAdicionales3 = new DatosAdicionales();
                DatosAdicionales obj_DatosAdicionales4 = new DatosAdicionales();
                DatosAdicionales obj_DatosAdicionales5 = new DatosAdicionales();
                DatosAdicionales obj_DatosAdicionales6 = new DatosAdicionales();
                DatosAdicionales obj_DatosAdicionales7 = new DatosAdicionales();
                DatosAdicionales obj_DatosAdicionales8 = new DatosAdicionales();
                Pago obj_DatosPagos1 = new Pago(); 

                //=============================CABECERA=========================================================//
                decimal total = 0;
                //obj_CamposHead.TipoDTE = "01";
                obj_CamposHead.TipoDTE = vCabecera.FirstOrDefault().TipoDTE;
                obj_CamposHead.Serie = vCabecera.FirstOrDefault().Serie;
                obj_CamposHead.Correlativo = vCabecera.FirstOrDefault().Correlativo;
                obj_CamposHead.FchEmis = vCabecera.FirstOrDefault().FChEmis;
                obj_CamposHead.TipoMoneda = vCabecera.FirstOrDefault().TipoMoneda;
                //obj_CamposHead.TpoMoneda = vCabecera.FirstOrDefault().TipoMoneda;
                obj_CamposHead.RUTEmis = vCabecera.FirstOrDefault().RUTEmisor;
                obj_CamposHead.TipoRucEmis = vCabecera.FirstOrDefault().TipoRucEmis;
                obj_CamposHead.RznSocEmis = vCabecera.FirstOrDefault().RznSocEmis;
                obj_CamposHead.NomComer = vCabecera.FirstOrDefault().NomComer;
                obj_CamposHead.DirEmis = vCabecera.FirstOrDefault().DirEmis;
                //obj_CamposHead.CodiComu = vCabecera.FirstOrDefault().CodiComu;
                obj_CamposHead.ComuEmis = vCabecera.FirstOrDefault().CodiComu;

                //obj_CamposHead.UrbanizaEmis = "-";
                //obj_CamposHead.ProviEmis = "-";
                //obj_CamposHead.DeparEmis = "-";
                //obj_CamposHead.DistriEmis = "-";
                //obj_CamposHead.PaisEmis = "-";

                obj_CamposHead.RUTRecep = vCabecera.FirstOrDefault().RUTRecep;

                //obj_CamposHead.TipoRUTRecep = vCabecera.FirstOrDefault().TipoRUTRecep;
                obj_CamposHead.TipoRutReceptor = vCabecera.FirstOrDefault().TipoRUTRecep;
                obj_CamposHead.RznSocRecep = vCabecera.FirstOrDefault().RznSocRecep;

                //obj_CamposHead.DirRecepUbiGeo = "-";

                obj_CamposHead.DirRecep = vCabecera.FirstOrDefault().DirRecep;

                obj_CamposHead.MntNeto = Convert.ToString(vCabecera.FirstOrDefault().MntExe).Replace(",", ".").ToString(); 
                obj_CamposHead.MntExe = Convert.ToString(vCabecera.FirstOrDefault().MntNeto).Replace(",", ".").ToString();
                obj_CamposHead.MntExo = Convert.ToString(vCabecera.FirstOrDefault().MntExo).Replace(",", ".").ToString();
                obj_CamposHead.MntTotal = Convert.ToString(vCabecera.FirstOrDefault().MntTotal).Replace(",", ".").ToString();

                ////// Agregar Campos Facturacion UBL 2.1
                obj_CamposHead.HoraEmision = vCabecera.FirstOrDefault().HoraEmision;

                obj_CamposHead.MntRedondeo = "0.00";//Convert.ToString(vCabecera.FirstOrDefault().MntTotal).Replace(",", ".").ToString();
                obj_CamposHead.CodigoLocalAnexo = vCabecera.FirstOrDefault().CodigoLocal;

                //-------------------------------
                obj_CamposHead.TipoOperacion = vCabecera.FirstOrDefault().TipoOper;
                //obj_CamposHead.TipoOperacion = "0101";

                //obj_CamposHead.CantidadItem = "1";  
                total = vCabecera.FirstOrDefault().MntTotal;

                //obj_CamposHead.TipoOper = vCabecera.FirstOrDefault().TipoOper;

                //Campos para Facturacion UBL 2.1
                obj_CamposHead.FechVencFact = vCabecera.FirstOrDefault().FChVen; // Verdadera Fecha Vencimiento

                //Campos nuevos
                if (vCabecera.FirstOrDefault().TipoDTE != "03")
                {
                    //nuevos campos 
                    obj_CamposHead.FormaPago = vCabecera.FirstOrDefault().FormaPago;
                    obj_CamposHead.MontoNetoPendPago = Convert.ToString(vCabecera.FirstOrDefault().MontoNetoPendPago).Replace(",", ".").ToString();
                }

                obj_Encabezado.camposEncabezado = obj_CamposHead;

                //===================================Detalle=================================================//
                Array lst_Detalle = Array.CreateInstance(typeof(SGRDA_Carga_Contingencia.CamposDetalle), vDetalle.Count);
                int count = 0;
                int int_Posicion = 0;
                Detalle[] Listar_Detalle = new Detalle[vDetalle.Count];
                int contadorDetalle = 0;
                foreach (var item in vDetalle)
                {
                    obj_Detalle = new CamposDetalle();
                    obj_Detalle.NroLinDet = Convert.ToString(count += 1).ToString();
                    obj_Detalle.QtyItem = item.QtyItem;
                    obj_Detalle.UnmdItem = "NIU";
                    obj_Detalle.VlrCodigo = item.VlrCodigo;
                    obj_Detalle.NmbItem = item.NmbItem;
                    obj_Detalle.PrcItem = Convert.ToString(item.PrcItem).Replace(",", ".").ToString();
                    //obj_Detalle.DescuentoMonto = Convert.ToString(item.DescuentoMonto).Replace(",", ".").ToString();
                    obj_Detalle.PrcItemSinIgv = Convert.ToString(item.PrcItemSinIgv).Replace(",", ".").ToString();
                    obj_Detalle.MontoItem = Convert.ToString(item.MontoItem).Replace(",", ".").ToString();

                    obj_Detalle.IndExe = "30";
                    obj_Detalle.CodigoTipoIgv = "9998";
                    obj_Detalle.TasaIgv = "18";
                    obj_Detalle.ImpuestoIgv = "0.00";

                    lst_Detalle.SetValue(obj_Detalle, int_Posicion);
                    int_Posicion = int_Posicion + 1;

                    Detalle Det = new Detalle();
                    Det.Detalles = obj_Detalle;
                    Listar_Detalle[contadorDetalle] = Det;
                    contadorDetalle += 1;
                }

                //===================================Descuentos=============================================//

                Array lst_Descuento = Array.CreateInstance(typeof(SGRDA_Carga_Contingencia.Descuentos), vDescuento.Count);
                int int_Posicion2 = 0;
                //foreach (var item2 in vDescuento)
                //{
                obj_Descuentos = new Descuentos();
                obj_Descuentos.NroLinDR = "-";
                obj_Descuentos.TpoMov = "-";
                obj_Descuentos.ValorDR = "-";
                lst_Descuento.SetValue(obj_Descuentos, int_Posicion2);
                int_Posicion2 = int_Posicion2 + 1;
                //}
                Descuentos Desc = new Descuentos();
                Desc = obj_Descuentos;

                //===================================Datos Adicionales=============================================//
                Array lst_Adicional = Array.CreateInstance(typeof(DatosAdicionales), vDetalle.Count);
                //int int_Posicion3 = 0;
                //GRUPO
                //foreach (var item3 in vDetalle)
                //{
                obj_DatosAdicionales = new DatosAdicionales();
                obj_DatosAdicionales.TipoAdicSunat = "01";
                obj_DatosAdicionales.NmrLineasAdicSunat = "01";
                obj_DatosAdicionales.DescripcionAdicsunat = vCabecera.FirstOrDefault().Grupo;
                //}
                //Afecto o Inafecto IGV
                //foreach (var item4 in vDetalle)
                //{
                obj_DatosAdicionales2 = new DatosAdicionales();
                obj_DatosAdicionales2.TipoAdicSunat = "01";
                obj_DatosAdicionales2.NmrLineasAdicSunat = "02";
                obj_DatosAdicionales2.DescripcionAdicsunat = "DERECHO DE AUTOR INAFECTO A I.G.V., SEGÚN D.S. 055-99-EF ART. 2 INC. A";
                //}
                //Fecha Vencimiento
                //foreach (var item5 in vDetalle)
                //{
                obj_DatosAdicionales3 = new DatosAdicionales();
                obj_DatosAdicionales3.TipoAdicSunat = "01";
                obj_DatosAdicionales3.NmrLineasAdicSunat = "03";
                obj_DatosAdicionales3.DescripcionAdicsunat = vCabecera.FirstOrDefault().FChVen;
                //}
                //Codigo de Usuario
                //foreach (var item6 in vDetalle)
                //{
                obj_DatosAdicionales4 = new DatosAdicionales();
                obj_DatosAdicionales4.TipoAdicSunat = "01";
                obj_DatosAdicionales4.NmrLineasAdicSunat = "04";
                obj_DatosAdicionales4.DescripcionAdicsunat = vCabecera.FirstOrDefault().CodiUsuario;
                //}
                //Oficina
                //foreach (var item7 in vDetalle)
                //{
                obj_DatosAdicionales5 = new DatosAdicionales();
                obj_DatosAdicionales5.TipoAdicSunat = "01";
                obj_DatosAdicionales5.NmrLineasAdicSunat = "05";
                obj_DatosAdicionales5.DescripcionAdicsunat = vCabecera.FirstOrDefault().OficinaRecaudo;
                //}

                //NumeroLetras
                //foreach (var item8 in vDetalle)
                //{
                obj_DatosAdicionales6 = new DatosAdicionales();
                obj_DatosAdicionales6.TipoAdicSunat = "01";
                obj_DatosAdicionales6.NmrLineasAdicSunat = "06";
                if (vCabecera.FirstOrDefault().TipoMoneda == "PEN")
                {
                    obj_DatosAdicionales6.DescripcionAdicsunat = Util.NumeroALetrasFE(total.ToString(), "SOLES");
                }
                else
                {
                    obj_DatosAdicionales6.DescripcionAdicsunat = Util.NumeroALetrasFE(total.ToString(), "DOLARES");
                }
                //}
                //Observaciones
                //foreach (var item9 in vDetalle)
                //{
                obj_DatosAdicionales7 = new DatosAdicionales();
                obj_DatosAdicionales7.TipoAdicSunat = "01";
                obj_DatosAdicionales7.NmrLineasAdicSunat = "07";
                obj_DatosAdicionales7.DescripcionAdicsunat = vDetalle.FirstOrDefault().Observacion;
                //}
                //Correo Electrónico
                //foreach (var item10 in vDetalle)
                //{
                obj_DatosAdicionales8 = new DatosAdicionales();
                obj_DatosAdicionales8.TipoAdicSunat = "01";
                obj_DatosAdicionales8.NmrLineasAdicSunat = "08";
                obj_DatosAdicionales8.DescripcionAdicsunat = vCabecera.FirstOrDefault().CorreoUsuario;
                //}
                DatosAdicionales Adicional = new DatosAdicionales();
                DatosAdicionales Adicional2 = new DatosAdicionales();
                DatosAdicionales Adicional3 = new DatosAdicionales();
                DatosAdicionales Adicional4 = new DatosAdicionales();
                DatosAdicionales Adicional5 = new DatosAdicionales();
                DatosAdicionales Adicional6 = new DatosAdicionales();
                DatosAdicionales Adicional7 = new DatosAdicionales();
                DatosAdicionales Adicional8 = new DatosAdicionales();
                Adicional = obj_DatosAdicionales;
                Adicional2 = obj_DatosAdicionales2;
                Adicional3 = obj_DatosAdicionales3;
                Adicional4 = obj_DatosAdicionales4;
                Adicional5 = obj_DatosAdicionales5;
                Adicional6 = obj_DatosAdicionales6;
                Adicional7 = obj_DatosAdicionales7;
                Adicional8 = obj_DatosAdicionales8;

                DatosAdicionales[] Listar_Adicional = { Adicional, Adicional2, Adicional3, Adicional4, Adicional5, Adicional6, Adicional7, Adicional8 };
                obj_DescuentosRecargosyOtros.DatosAdicionales = Listar_Adicional;

                //Datos de Pagos- para cuotas normativa 193
                obj_DatosPagos1 = new Pago();
                obj_DatosPagos1.Cuota = "1";
                obj_DatosPagos1.MontoCuota = Convert.ToString(vCabecera.FirstOrDefault().MntTotal).Replace(",", ".").ToString();
                obj_DatosPagos1.FechaVencCuota = vCabecera.FirstOrDefault().FChVen;

                Pago Pago1 = new Pago();
                Pago1 = obj_DatosPagos1;

                Pago[] Listar_Pagos = { Pago1 };

                obj_DescuentosRecargosyOtros.Pagos = Listar_Pagos;

                //===========================================================================================================================
                //string ruc = vCabecera.FirstOrDefault().RUTEmisor;
                //int tipo_docu = Convert.ToInt32(vCabecera.FirstOrDefault().TipoDTE);
                //int foli_inte = Convert.ToInt32(vCabecera.FirstOrDefault().Correlativo);
                //string serie_inte = vCabecera.FirstOrDefault().Serie;
                //===========================================================================================================================

                //Carga
                var obj_ResultadoMensaje = WS.putCustomerETDLoad(obj_Extras, obj_Encabezado, Listar_Detalle, obj_DescuentosRecargosyOtros);

                //var Estado = wsEstado.ConsultaEstado(ruc, tipo_docu, foli_inte, serie_inte);

                //if (Estado == Constantes.Mensaje_Sunat.MSG_FIR_SUNAT)
                //{
                //    Estado = Constantes.Mensaje_Sunat.MSG_FIR_SUNAT_OK;
                //}
                //else if (obj_ResultadoMensaje.Codigo == "ERROR")
                //{
                //    Estado = Constantes.Mensaje_Sunat.MSG_ERROR;
                //}
                //else
                //{
                //    Estado = "El Comprobante número " + serie_inte + "-" + foli_inte + " ha sido aceptado";
                //}
                if (vCabecera.FirstOrDefault().TipoDTE == "01" && vCabecera.FirstOrDefault().TipoRUTRecep == "1")
                {
                    ActualizarEstadoSunat(idfactura, "RCH", mensaje = Constantes.Mensaje_Sunat.MSG_RCH_SUNAT);
                }
                else
                {
                    if (obj_ResultadoMensaje.Codigo == "DOK")
                    {
                        mensaje = Constantes.Mensaje_Sunat.MSG_DOK_SUNAT;
                    }
                    else if (obj_ResultadoMensaje.Codigo == "ERDTE ")
                    {
                        mensaje = Constantes.Mensaje_Sunat.MSG_EXISTE_SUNAT;
                    }
                    else if (obj_ResultadoMensaje.Codigo == "ERROR")
                    {
                        mensaje = Constantes.Mensaje_Sunat.MSG_ERROR;
                    }
                    else
                    {
                        mensaje = Constantes.Mensaje_Sunat.MSG_RCH_SUNAT;
                    }

                    //ACTUALIZA EL ESTADO Y OBSERVACION DE SUNAT
                    ActualizarEstadoSunat(idfactura, obj_ResultadoMensaje.Codigo, obj_ResultadoMensaje.Mensajes);
                    //---------------------------------------------------------------------
                }
                return mensaje;
            }
            catch (Exception  ex)
            {
                return mensaje = "0";
            }
        }


        public void ActualizarEstadoSunat(decimal id, string estado, string obs)
        {
            Resultado retorno = new Resultado();
            try
            {
                BESunat Fac = new BESunat();
                Fac.OWNER = GlobalVars.Global.OWNER;
                Fac.INV_ID = id;
                Fac.ESTADO_SUNAT = estado;
                Fac.OBSERVACION_SUNAT = obs;
                int n = new BLSunat().ActualizarEstadoSunat(Fac);
            }
            catch (Exception ex)
            {
                retorno.result = 0;
                retorno.message = string.Format("{0} - {1}", Constantes.MensajeGenerico.MSG_ERROR_GENERICO, ex.Message);
                ucLogApp.ucLog.GrabarLogError(nomAplicacion, UsuarioActual, "generarAnulacion", ex);
            }
        }



    }
}
using SGRDA.BL;
using SGRDA.Entities;
using SGRDA.Servicios.Contrato;
using SGRDA.Servicios.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGRDA.Servicios.Implementacion
{
    public class SEFactura :ISEFactura  
    {
        public  Factura ObtenerFactura(decimal idDocumento)
        {
            Factura factura = null;
            string validarHoraEjecucion = Convert.ToString(System.Web.Configuration.WebConfigurationManager.AppSettings["validarHoraEjecucion"]);
            int horai = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["horaIniPrintFact"]);
            int horaf = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["horaFinPrintFact"]);
            int horaa = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
            var flg_exito = true;
            ///CONDICIONAL PARA IR A LA BASE DE DATOS Y TRAER INFORMACION . ABRE Y CIERRA CONEXION.
            if (validarHoraEjecucion == "S" )
            {
                if (!(horaa >= horai && horaa <= horaf))
                {
                    flg_exito = false;
                }
            }
            
            if(flg_exito){
                factura = new Factura();
                decimal acumTotal = decimal.Zero;

                List<BEFactura> listaCab = new List<BEFactura>();
                listaCab = new BLFactura().ListaReporteCabeceraConsulta("APD", idDocumento);
                List<BEFacturaDetalle> listaDet = new List<BEFacturaDetalle>();
                listaDet = new BLFactura().ListaReporteDetalleConsulta("APD", idDocumento);

                if (listaCab != null && listaCab.Count > 0)
                {
                    var entidadFact = listaCab[0];

                    factura.RazonSocial = entidadFact.SOCIO;
                    factura.Direccion = entidadFact.ADDRESS;
                    factura.RUM = entidadFact.RUM;
                    factura.RUC = entidadFact.TAX_ID;
                    factura.Local = "";
                    factura.NumFact = Convert.ToString(entidadFact.INV_NUMBER);
                    factura.TipoFactura = entidadFact.TIPO_FACT;
                    factura.FechaEmision = entidadFact.INV_DATE;

                    List<FacturaDetalle> lista = new List<FacturaDetalle>();
                    string totalLetras = "";
                    foreach (var item in listaDet)
                    {
                        var deta = new FacturaDetalle();
                        deta.Item = Convert.ToString(item.INVL_ID);
                        deta.Descripcion = item.LIC_NAME;
                        deta.Cantidad = "1";
                        deta.SubTotal = item.TOTAL;
                        lista.Add(deta);
                        acumTotal = acumTotal + item.TOTAL;
                        totalLetras = item.TOTAL_LETRA;
                    }
                    factura.Total = acumTotal;
                    factura.TotalLetras = totalLetras;
                    factura.Detalle = lista;
                }
            }


            return factura;

        }
    }
}
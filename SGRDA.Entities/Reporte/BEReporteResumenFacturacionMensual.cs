using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BEReporteResumenFacturacionMensual
    {
        public string FECHA_EMISION { get; set; }
        public string TD { get; set; }
        public string SERIE { get; set; }
        public string NRO { get; set; }
        public string RUC { get; set; }
        public string OFICINA { get; set; }
        public string RUBRO { get; set; }
        public string UBIGEO { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string NOMBRE_COMERCIAL { get; set; }
        public string PERIODO { get; set; }
        public decimal MONTO { get; set; }
        public decimal IGV { get; set; }
        public decimal TOTAL { get; set; }
        public decimal PAGOS { get; set; }
        public decimal SALDO { get; set; }
        public string FEC_CANCELACION { get; set; }
        public string ESTADO    { get; set; }
        public string TIPO      { get; set; }
        public string SUBTIPO   { get; set; }
        public string DIRECCION { get; set; }
        public decimal MONTO_NC { get; set; }
        public decimal RECAUDO  { get; set; }
        public string TIPO_ENVIO { get; set; }
        public int ID_ESTABLECIMIENTO { get; set; }
        public string FECHA_CONFIRMACION { get; set; }
        public string CORRELATIVO { get; set; }
        public int LIC_ID { get; set; }
    }                 
}                     

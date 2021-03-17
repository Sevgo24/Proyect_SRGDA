using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEDocumentoNoIdentificado
    {
        public DateTime FECHA_CREA { get; set; }
        public string USUARIO_CREA { get; set; }
        public decimal ID_OFICINA { get; set; }
        public string ID_MONEDA { get; set; }
        public decimal ID_SOCIO_NEGOCIO { get; set; }
        public decimal MONTO { get; set; }
        public decimal MONTO_SOLES { get; set; }
        //--  FACTURA --
        public decimal INV_ID { get; set; }
        public decimal INVL_ID { get; set; }
        public decimal TIPO_CAMBIO { get; set; }
        //-- COBRO --
        public decimal MREC_ID { get; set; }
        public decimal REC_ID { get; set; }
        public decimal REC_DID { get; set; }
        public string REC_PWID { get; set; }
        public decimal ID_BANCO { get; set; }
        public decimal ID_CUENTA { get; set; }
        public DateTime FECHA_DEPOSITO { get; set; }
        public string NRO_CONFIRMACION { get; set; }
        public decimal REC_PID { get; set; }
        public string Observacion { get; set; }
        //DESCRIPCIONES
        public string TipoPago { get; set; }
        public string Banco { get; set; }
        public string CuentaBancaria { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public Nullable<DateTime> FEC_INTERFAZ { get; set; }

        public Nullable<DateTime> FEC_INTERFAZ_REVERT { get; set; }

        public Nullable<DateTime> FEC_CREA { get; set; }
        public string CTA_DESTINO { get; set; }
        public string BANCO_DESTINO { get; set; }
        public int ESTADO { get; set; }

        public int LineaExcel { get; set; }
        public string OWNER { get; set; }
        
                
        public decimal MREC_ID2 { get; set; }
        public string Factura { get; set; }
       
    }
}

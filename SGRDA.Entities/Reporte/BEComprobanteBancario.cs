using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BEComprobanteBancario
    {
        public string BNK_NAME { get; set; }
        public string NUM_CUENTA { get; set; }
        public string FECHA_DEP { get; set; }
        public string REC_CODECONFIRMED { get; set; }
        public decimal REC_PVALUE { get; set; }

        public string ESTADO_DEPOSITO { get; set; }
        public string MONEDA { get; set; }
        public string BANCO_DESTINO { get; set; }
        public string CUENTA_DESTINO { get; set; }
        public decimal OFF_ID { get; set; }

        public decimal MREC_ID { get; set; }

        public string REC_OBSERVATION { get; set; }

        public string REC_REFERENCE { get; set; }

        public string FECHA_CONFIRMACION { get; set; }

        public string FECHA_INGRESO { get; set; }

        public DateTime FECHA_INGRESO_DATE { get; set; }

        public string OFF_NAME { get; set; }
        public decimal REC_BALANCE { get; set; }
        public decimal RECAUDO { get; set; }
        public decimal ACUMULADO_FACTURAS { get; set; }

        public string USUARIO_MODIF { get; set; }
        public string FECHA_MODIF { get; set; }

        public string SOCIO       {get;set;}
        public decimal COD_LICENCIA{get;set;}
        public string RUBRO_NOMBRE{get;set;}
        public string SERIE       {get;set;}
        public decimal NUMERO      {get;set;}
        public string ESTADO      {get;set;}
        public Int64 Correlativo { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
   public class BEREPORTE_DEPOSITO_BANCO
    {
       public IList<BEREPORTE_DEPOSITO_BANCO> ListarReporteDepositoBanco { get; set; }
       public BEREPORTE_DEPOSITO_BANCO()
       {
           ListarReporteDepositoBanco = new  List<BEREPORTE_DEPOSITO_BANCO>();
       }
       //ATRIBUTOS
       public decimal BEC { get; set; }
       public string fec_deposito { get; set; }
       public string cuenta_corriente { get; set; }
       public string nro_operacion { get; set; }
       public string documento { get; set; }
       public decimal importe { get; set; }
       public string RUBRO { get; set; }
       public string obervacion { get; set; }
       //CAMPO AGREGADOR

       public string DIVISION_EST { get; set; }
       public string PERIODO { get; set; }


       //COBROS
       public string RUBRO_NOMBRE { get; set; }       
       public decimal ID_COBRO { get; set; }
       public string FEC_DEPOSITO { get; set; }
       public string FEC_COBRO_DETALLE { get; set; }
       public string CUENTA { get; set; }
       public decimal IMPORTE_DEPOSITO_SOLES { get; set; }
       public string DOCUMENTO { get; set; }
       public decimal IMPORTE_DETALLE_DEPOSITO { get; set; }
       public decimal NODO_ID { get; set; }
       public string NODO { get; set; }
       public int TERRITORIO { get; set; }
       public string FEC_DEPOSITO_CONFIRMACION { get; set; }

       public decimal DEPOSITO_MONTO { get; set; }
       public decimal DEPOSITO_SALDO { get; set; }
       public decimal DEPOSITO_MONTO_SOLES { get; set; }
       public decimal DEPOSITO_SALDO_SOLES { get; set; }

        public int ANIO_CANCELACION_DETALLE { get; set; }
        public int MES_CANCELACION_DETALLE { get; set; }
        public DateTime FEC_CANCELACION_DETALLE_DATE { get; set; }
        public string FEC_CANCELACION_DETALLE { get; set; }

        public string PERIODO_CONTABLE { get; set; }

        public string TIPOCOBRO { get; set; }

        public string FECHA_INI {get;set;}
        public string FECHA_FIN { get; set; }


    }
}

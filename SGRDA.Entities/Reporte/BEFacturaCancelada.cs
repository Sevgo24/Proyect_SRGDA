using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BEFacturaCancelada
    {
        public IList<BEFacturaCancelada> ListarReporteFacturaCancelada { get; set; }
        public BEFacturaCancelada()
        {
            ListarReporteFacturaCancelada = new List<BEFacturaCancelada>();
        }
        //ATRIBUTOS
        public string ruc { get; set; }
        public string usuario { get; set; }
        public string documento { get; set; }
        public string femi { get; set; }
        public string periodo { get; set; }
        public string fecan { get; set; }
        public decimal importe { get; set; }
        public string RUBRO { get; set; }
        public string DIVISION_EST { get; set; }

        public string MOG_ID { get; set; }
        public string MOG_DESC { get; set; }


        public int ANIO_CANCELACION_DETALLE { get; set; }
        public int MES_CANCELACION_DETALLE { get; set; }
        public DateTime FEC_CANCELACION_DETALLE_DATE { get; set; }
        public string FEC_CANCELACION_DETALLE { get; set; }

        public string LICENCIA { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string PROVINCIA { get; set; }
        public string DISTRITO { get; set; }
        public string NODO { get; set; }
        public string ESTABLECIMIENTO { get; set; }
        public int EST_ID { get; set; }
        public string Direccion {get;set;}
        public string TIPO_EST { get; set; }
        public string SUBTIPO_EST { get; set; }
        public string Fec_Confirmacion { get; set; }
        public int LIC_ID { get; set; }
        public string TIPO { get; set; }
        public string MONEDA {get;set;}
        public decimal MONTO_DOLAR { get; set; }
        public decimal ID_COBRO { get; set; }
    }   
}

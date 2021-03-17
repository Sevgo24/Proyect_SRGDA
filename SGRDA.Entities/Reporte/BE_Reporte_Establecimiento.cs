using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BE_Reporte_Establecimiento
    {
        public decimal ID_SOCIO { get; set; }
        public string SOCIO { get; set; }
        public string TIPO_DOCUMENTO { get; set; }
        public string NRO_DOCUMENTO { get; set; }
        public decimal LIC_ID { get; set; }
        public string GRUPO { get; set; }
        public string MODALIDAD { get; set; }
        public string LICENCIA { get; set; }
        public string FECHA_CREACION_LICENCIA { get; set; }
        public decimal EST_ID { get; set; }
        public string ESTABLECIMIENTO { get; set; }
        public string TIPO { get; set; }
        public string SUB_TIPO { get; set; }
        public string DIRECCION { get; set; }
        public string UBIGEO { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string PROVINCIA { get; set; }
        public string DISTRITO { get; set; }
        public string TD { get; set; }
        public string FEC_EMI_FACTURA { get; set; }
        public string SERIE { get; set; }
        public string MONEDA { get; set; }
        public decimal NUMERO { get; set; }
        public decimal IMPORTE { get; set; }
        public decimal RECAUDADO { get; set; }
        public string FEC_CANCELACION { get; set; }
        public int ANIO_ULTIMA_FACTURA { get; set; }

    }
}

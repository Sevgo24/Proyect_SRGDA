using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BEReporteEstadoCuenta
    {
        public IList<BEReporteEstadoCuenta> ListarReporteEstadoCuenta { get; set; }
        public BEReporteEstadoCuenta()
        {
            ListarReporteEstadoCuenta = new List<BEReporteEstadoCuenta>();
        }
        //
        public string RUBRO { get; set; }
        public string tipo_doc { get; set; }
        public string fec_emi_fact { get; set; }
        public string PERIODO { get; set; }
        public string fec_can_fact { get; set; }
        public decimal importe { get; set; }
        public string ruc { get; set; }
        public string LIC_ID { get; set; }
        public string socio { get; set; }
        public string numero { get; set; }
        public string ESTADO { get; set; }
        public string NOMBRE_LICENCIA { get; set; }
        public string ESTADO_SUNAT { get; set; }
        public string NC { get; set; }
        public string NOMBRE_LOCAL { get; set; }
        public string DISTRITO { get; set; }
    }
}

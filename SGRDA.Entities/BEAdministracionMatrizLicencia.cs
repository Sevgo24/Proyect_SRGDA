using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEAdministracionMatrizLicencia
    {

        public List<BEAdministracionMatrizLicencia> listaMatrizLicencia { get; set; }

        public decimal TotalVirtual { get; set;}
        public decimal LIC_ID { get; set; }
        public string LIC_NAME { get; set; }
        public string EST_NAME { get; set; }
        public string DIRECCION { get; set; }
        public string UBIGEO { get; set; }
        public string MODALIDAD { get; set; }
        public string ULT_PER_FACT { get; set; }
        public decimal MONTO { get; set; }
        public string ESTADO { get; set;}
        public string SOCIO { get; set; }
        public string NUMERO_DOCUMENTO { get; set; }


        public string PER_NO_FACT { get; set; }

    }
}

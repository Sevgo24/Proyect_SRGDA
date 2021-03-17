using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
  public  class BEPreImpresion
    {

        public decimal CodigoImpresion { get; set; }
        public decimal CodigoDocumento { get; set; }
        public decimal CodigoUsuario { get; set; }
        public decimal CodigLocal { get; set; }

        public DateTime FechaSel { get; set; }
        public DateTime? FechaImp { get; set; }

        public string Estado { get; set; }

        public string Host { get; set; }
        public string HostIp { get; set; }



    }
}

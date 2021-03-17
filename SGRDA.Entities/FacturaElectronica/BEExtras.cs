using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.FacturaElectronica
{
    public class BEExtras
    {
        public string MailEnvio { get; set; }
        public string MailCopia { get; set; }
        public string MailCopiaOculta { get; set; }

        public string LineaReferencia { get; set; }
        public string NombreAdjunto { get; set; }
        public string DescripcionAdjunto { get; set; }
    }
}

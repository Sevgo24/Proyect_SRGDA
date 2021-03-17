using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.FacturaElectronica
{
    public class BESunat
    {

        public string OWNER { get; set; }
        public decimal INV_ID { get; set; }
        public string INV_NULLREASON { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        //FACTURACION ELECTRONICA - TIPO NOTA DE CREDITO
        public string CODE_DESCRIPTION { get; set; }
        public string ESTADO_SUNAT { get; set; }
        public string OBSERVACION_SUNAT { get; set; }
    }
}

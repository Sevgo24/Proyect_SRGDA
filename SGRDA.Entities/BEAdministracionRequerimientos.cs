using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEAdministracionRequerimientos
    {
        public decimal ID_REQ { get; set; }
        public decimal ID_REQ_TYPE { get; set; }

        public string REQUERIMENTS_DESC { get; set; }
        public string REQ_DESCRIPCION { get; set; }
        public string REQ_DATE { get; set; }
        public decimal MONTO { get; set; }
        public int ACTIVO { get; set; }
        public int DES_ACTIVO { get; set; }
        public decimal LIC_ID { get; set; }

        public string LIC_NAME { get; set; }
        public decimal EST_ID { get; set; }
        public string EST_NAME { get; set; }
        public decimal INV_ID { get; set; }

        public string SERIE { get; set; }
        public decimal NUMERO { get; set; }

        public decimal INV_NET { get; set; }

        public decimal INV_NETACT { get; set; }

        public string INV_DATE { get; set; }
        public decimal OFICINA { get; set; }

        public string DESC_OFICINA { get; set; }
        public int ESTADO { get; set; }

        public string DESC_ESTADO { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_DATE_CREAT { get; set; }

        public string ENDS { get; set; }
        public decimal BPS_ID { get; set; }

        public string SOCIO { get; set; }

        public string DOCUMENTOSOCIO { get; set; }

        public string REQ_DESCRIPCION_RESP { get; set; }

        public decimal BEC_ID { get; set; }

        public decimal REC_ID { get; set; }
        public int CodigoTipoInactivacion { get; set; }
    }
}


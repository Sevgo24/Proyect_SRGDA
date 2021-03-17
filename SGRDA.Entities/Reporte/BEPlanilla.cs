using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BEPlanilla
    {
        public decimal INV_ID { get; set; }
        public decimal INV_NUMBER { get; set; }
        public string NMR_SERIAL { get; set; }
        public decimal REPORT_NUMBER { get; set; }
        public string LOG_DATE_CREAT { get; set; }
        public decimal INV_NET { get; set; }
        public decimal LIC_ID { get; set; }
        public string RUM { get; set; }
        public decimal LIC_YEAR { get; set; }
        public decimal LIC_MONTH { get; set; }

        public decimal SHOW { get; set; }

        public string ARTISTA_DESC { get; set; }

        public string MODALIDAD { get; set; }

        public string GRUPO_MODALIDAD { get; set; }

        public string  MOG_ID { get; set; }

        public decimal MOD_ID { get; set; }

        public string DOCUMENTO { get; set; }

        public decimal MONTO { get; set; }

        public string FECHA_CANCEL { get; set; }

        public string MES_EVENTO { get; set; }
        
    }
}

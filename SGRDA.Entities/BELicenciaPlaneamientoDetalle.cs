using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BELicenciaPlaneamientoDetalle
    {
        public string OWNER { get; set; }
        public decimal LIC_PL_ID_SQ { get; set; }
        public decimal LIC_PL_ID { get; set; }
        public decimal INV_ID { get; set; }
        public decimal LIC_INVOICE_VAL { get; set; }
        public decimal LIC_INVOICE_LINE { get; set; }
        public DateTime LIC_DATEI { get; set; }
        public bool LIC_PL_PARTIAL { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public decimal SERIE { get; set; }
        public string INV_REPORT_DETAILS { get; set; }
        //FACTURACION MANUAL
        public int NUMERO { get; set; }
        public DateTime FECHA_EMISION { get; set; }
        public bool TIPO_MANUAL { get; set; }
    }
}

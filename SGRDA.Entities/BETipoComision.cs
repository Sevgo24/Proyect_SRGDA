using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETipoComision
    {
        public string OWNER { get; set; }
        public decimal COMT_ID { get; set; }
        public string COM_DESC { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public DateTime? ENDS { get; set; }
        public string ESTADO { get; set; }
        public int TotalVirtual { get; set; }
        public List<BETipoComision> listaComision = null;

        public BETipoComision()
        {
            listaComision = new List<BETipoComision>();
        }
    }
}

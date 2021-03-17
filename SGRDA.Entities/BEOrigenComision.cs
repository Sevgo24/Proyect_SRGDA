using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEOrigenComision
    {
        public string OWNER { get; set; }
        public decimal COM_ORG { get; set; }
        public string COM_DESC { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }
        public List<BEOrigenComision> ListaOrigenComision = null;
        public int TotalVirtual { get; set; }
        public string ESTADO { get; set; }

        public BEOrigenComision()
        {
            ListaOrigenComision = new List<BEOrigenComision>();
        }
    }
}

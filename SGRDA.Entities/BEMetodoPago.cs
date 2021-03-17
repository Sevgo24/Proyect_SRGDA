using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEMetodoPago
    {
        public string OWNER { get; set; }
        public string REC_PWID { get; set; }
        public string REC_PWDESC { get; set; }
        public string REC_PWDESCAux { get; set; }
        public DateTime? ENDS { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public string ESTADO { get; set; }
        public int TotalVirtual { get; set; }
        public int valgraba { get; set; }
        public bool REC_AUT { get; set; }
        public string Confirmed { get; set; }
        public string Automaticamente { get; set; }
        public IList<BEMetodoPago> ListaMetodoPago { get; set; }
        public BEMetodoPago()
        {
            ListaMetodoPago = new List<BEMetodoPago>();
        }
    }
}

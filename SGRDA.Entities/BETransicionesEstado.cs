using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETransicionesEstado
    {
        public string OWNER { get; set; }
        public decimal LICS_ID { get; set; }
        public decimal LICS_IDT { get; set; }
        public decimal auxLICS_ID { get; set; }
        public decimal auxLICS_IDT { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string ESTADO { get; set; }
        public int TotalVirtual { get; set; }
        public string LICS_NAMEori { get; set; }
        public string LICS_NAMEdes { get; set; }
        public List<BETransicionesEstado> listaTranEstado;

        public BETransicionesEstado()
        {
            listaTranEstado = new List<BETransicionesEstado>();
        }
    }
}

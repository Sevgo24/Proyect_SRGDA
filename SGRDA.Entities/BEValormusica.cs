using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEValormusica
    {
        public string OWNER { get; set; }
        public decimal VUM_ID { get; set; }
        public DateTime? START { get; set; }
        public DateTime? ENDS { get; set; }
        public decimal VUM_VAL { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public DateTime? DELETE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public int TotalVirtual { get; set; }
        public string ESTADO { get; set; }

        public List<BEValormusica> listaValorMusica;

        public BEValormusica()
        {
            listaValorMusica = new List<BEValormusica>();
        }
    }
}

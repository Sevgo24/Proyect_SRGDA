using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BECaracteristicaValor:Paginacion
    {
         public IList<BECaracteristicaValor> ListaCaracteristica { get; set; }
         public BECaracteristicaValor()
        {
            ListaCaracteristica = new List<BECaracteristicaValor>();
        }
        public string OWNER { get; set; }
        public decimal CHARVAL_ID { get; set; }
        public string DAD_TYPE { get; set; }
        public decimal DAD_ID { get; set; }
        public decimal DAD_STYPE { get; set; }
        public decimal DADV_ID { get; set; }
        public string DAC_ID { get; set; }
        public string VALUE { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> ENDS { get; set; }

        public string SUBDIVISION { get; set; }
        public string VALOR { get; set; }
        public string CARACTERISTICA { get; set; }
    }
}

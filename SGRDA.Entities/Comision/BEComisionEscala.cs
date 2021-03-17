using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Comision
{
    public class BEComisionEscala
    {
        public string OWNER { get; set; }
        public decimal SET_ID { get; set; }
        public string SET_DESC { get; set; }
        public string SET_ACC { get; set; }
        public decimal SET_NTRANI { get; set; }
        public decimal SET_ITRANF { get; set; }
        public string SET_MOT { get; set; }

        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }

        public Nullable<DateTime> ENDS { get; set; }
        public List<BEComisionEscalaRango> ListaComisionRango { get; set; }
    }
}

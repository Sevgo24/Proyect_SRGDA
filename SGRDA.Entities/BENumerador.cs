using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SGRDA.Entities
{
    public class BENumerador
    {
        public string OWNER { get; set; }
        public decimal NMR_ID { get; set; }
        public decimal TIS_N { get; set; }
        public string NMR_TYPE { get; set; }
        public string NMR_SERIAL { get; set; }
        public string NMR_TYPE_DES { get; set; }
        public string NMR_NAME { get; set; }
        public string W_SERIAL { get; set; }
        public string W_YEAR { get; set; }
        public decimal NMR_FORM { get; set; }
        public decimal NMR_TO { get; set; }
        public decimal NMR_NOW { get; set; }
        public string AJUST { get; set; }
        public decimal POS_SERIAL { get; set; }
        public decimal LON_YEAR { get; set; }
        public decimal POS_YEAR { get; set; }
        public string DIVIDER1 { get; set; }
        public string DIVIDER2 { get; set; }
        public string NMR_MANUAL { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public decimal NMR_ID_NUEVO { get; set; }

    }
}

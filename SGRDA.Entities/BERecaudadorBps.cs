using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SGRDA.Entities
{
    public class BERecaudadorBps : Paginacion
    {

        public List<BERecaudadorBps> ListaAgenteOficina { get; set; }

        public BERecaudadorBps()
        {
            ListaAgenteOficina = new List<BERecaudadorBps>();
        }

        public string OWNER { get; set; }
        public decimal BPS_ID { get; set; }
        public decimal OFF_ID { get; set; }
        public decimal COLL_LEVEL { get; set; }
        public string CUR_ALPHA { get; set; }
        public Nullable<DateTime> AGR_DATE { get; set; }
        public Nullable<DateTime> LAST_SET { get; set; }
        public Nullable<DateTime> LAST_EXP { get; set; }
        public Nullable<DateTime> LAST_SET_TOT { get; set; }
        public string ACC_ID { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }

        public string TAXN_NAME { get; set; }
        public string TAX_ID { get; set; }
        public string BPS_NAME { get; set; }
        public string LEVEL { get; set; }
        public bool COLL_MAIN { get; set; }

    }
}

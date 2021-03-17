using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BENumeradorOficina : Paginacion
    {
        public IList<BENumeradorOficina> ListaNumOficina { get; set; }
        public BENumeradorOficina()
        {
            ListaNumOficina = new List<BENumeradorOficina>();
        }

        public string OWNER { get; set; }
        public decimal OFF_ID { get; set; }
        public decimal NMR_ID { get; set; }
        public decimal NMR_ID_NUEVO { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public decimal NUM_ID { get; set; }
        public decimal ID_COLL_DIV { get; set; }
        public decimal DAD_ID { get; set; }        
        public string TIPO_NUMERADOR { get; set; }
        public string SERIE_NUMERADOR { get; set; }
    }
}

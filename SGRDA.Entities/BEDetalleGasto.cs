using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEDetalleGasto : Paginacion
    {
        public Int32 RowNumber { get; set; }
        public string OWNER { get; set; }
        public decimal MNR_DET_ID { get; set; }
        public string EXP_ID { get; set; }
        public decimal MNR_ID { get; set; }
        public decimal? LEG_ID { get; set; }

        public decimal EXP_VAL_PRE { get; set; }
        public decimal EXP_VAL_APR { get; set; }
        public decimal EXP_VAL_CON { get; set; } 
       
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string ESTADO { get; set; }        

        public string EXP_TYPE { get; set; }
        public string EXPG_ID { get; set; }
        public string EXP_DESC { get; set; }


    }
}

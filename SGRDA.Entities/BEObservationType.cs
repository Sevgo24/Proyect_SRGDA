using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEObservationType : Paginacion
    {
        public string OWNER { get; set; }
        public int OBS_TYPE { get; set; }
        public decimal TIPO { get; set; }
        public int ENT_ID { get; set; }
        public string OBS_GROUP { get; set; }
        public string OBS_DESC { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }
        public int ESTADO { get; set; }
        public string ACTIVO { get; set; }
        public string OBS_OBSERV { get; set; } 
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEObservationGral:Paginacion
    {
        public string OWNER { get; set; }
        public decimal OBS_ID { get; set; }
        public int OBS_TYPE { get; set; }
        public int ENT_ID { get; set; }
        public string OBS_VALUE { get; set; }        
        public DateTime OBS_DATE { get; set; }
        public string OBS_USER { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public string DES_TYPE { get; set; }
        public DateTime? ENDS { get; set; }
    }
}

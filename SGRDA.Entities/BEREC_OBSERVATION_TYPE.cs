using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEREC_OBSERVATION_TYPE : Paginacion
    {
        public string OWNER { get; set; }
        public decimal OBS_TYPE { get; set; }
        public string OBS_GROUP { get; set; }
        public string OBS_DESC { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public string Activo { get; set; }
        public string OBS_OBSERV { get; set; }
    }
}

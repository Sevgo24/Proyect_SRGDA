using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Validation;

namespace SGRDA.Entities
{
    [MetadataType(typeof(REF_DIV_SUBTYPE))]
    public partial class BEREF_DIV_SUBTYPE : Paginacion
    {
        public string OWNER { get; set; }
        public decimal DAD_ID { get; set; }
        public string DAD_CODE { get; set; }
        public decimal DAD_STYPE { get; set; }
        public string DAD_SNAME { get; set; }
        public string DAD_NAME { get; set; }
        public decimal DAD_BELONGS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public string SUBDIVISION { get; set; }
        public string DEPENDENCIA { get; set; }        
    }
}

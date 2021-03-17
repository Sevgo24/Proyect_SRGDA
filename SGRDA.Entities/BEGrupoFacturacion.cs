using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEGrupoFacturacion:Paginacion
    {
        public string OWNER { get; set; }
        public decimal INVG_ID { get; set; }
        public string INVG_DESC { get; set; }
        public decimal BPS_ID { get; set; }
        public string BPS_NAME { get; set; }
        public string MOG_ID { get; set; }
        public string GRUPO { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }

        public string Activo { get; set; }
    }
}

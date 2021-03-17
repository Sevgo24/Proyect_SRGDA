using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEFormatoFacturaxGrupoModalidad : Paginacion
    {
        public string OWNER { get; set; }
        public decimal Id { get; set; }
        public decimal INVF_ID { get; set; }
        public decimal INVF_ID_ANT { get; set; }        
        public string FORMATO { get; set; }
        public string MOG_ID { get; set; }
        public string GRUPO { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }

        public string Activo { get; set; }
    }
}

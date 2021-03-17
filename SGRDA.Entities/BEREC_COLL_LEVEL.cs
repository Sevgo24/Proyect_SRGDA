using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEREC_COLL_LEVEL : Paginacion
    {
        //[DisplayName("Propietario")]
        //[MaxLength(3)]
        public string OWNER { get; set; }

        //[DisplayName("Niv. Agente Comercial")]
        //[Required(ErrorMessage = "Ingrese nivel de agente comercial")]
        //public decimal LEVEL_ID { get; set; }

        //[DisplayName("numerador Asignado")]
        public decimal NMR_ID { get; set; }

        //[DisplayName("Descripción")]
        //[Required(ErrorMessage = "Ingrese descripción")]
        //[MaxLength(40)]
        //public string DESCRIPTION { get; set; }

        //[DisplayName("Niv. S. Agente Comercial")]
        public decimal LEVEL_DEP { get; set; }

        public decimal LEVEL_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public DateTime? ENDS { get; set; }

        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_BPS_QUALY : Paginacion
    {
        [DisplayName("Propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Id")]
        [Required(ErrorMessage = "Ingrese socio del negocio")]
        public decimal BPS_ID { get; set; }

        [DisplayName("Tipo caracteristica")]
        public decimal QUC_ID { get; set; }

        //descripcion QUC_ID DESCRIPCION
        [DisplayName("Tipo caracteristica")]
        public string CARACTERISTICA { get; set; }

        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }
    }
}

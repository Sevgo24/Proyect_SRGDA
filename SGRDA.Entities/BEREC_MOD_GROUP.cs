using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_MOD_GROUP : Paginacion
    {
        [DisplayName("Propietario")]
        public string OWNER { get; set; }

        [DisplayName("Grupo Modalidad")]
        [Required(ErrorMessage = "Ingrese id")]
        [MaxLength(3)]
        public string MOG_ID { get; set; }

        public decimal IdFormato { get; set; }
        public decimal IdFormatoAnt { get; set; }
        public string Formato { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Ingrese descripción")]
        [MaxLength(40)]
        public string MOG_DESC { get; set; }

        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }

        public DateTime? ENDS { get; set; }

        public string Activo { get; set; }

        public List<BEFormatoFacturaxGrupoModalidad> FormatoModalidad { get; set; }
    }
}

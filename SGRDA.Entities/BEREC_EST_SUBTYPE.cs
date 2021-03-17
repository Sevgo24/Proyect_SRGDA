using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_EST_SUBTYPE : Paginacion
    {
        public string DESCRIPTIONTYPE { get; set; }
        public string ECON_DESC { get; set; }

        [DisplayName("Propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Subtipo Estableccimiento")]
        public decimal SUBE_ID { get; set; }

        [DisplayName("Tipo Establecimiento")]
        [Required(ErrorMessage = "Seleccione tipo establecimiento")]
        public decimal ESTT_ID { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Ingrese descripción")]
        [MaxLength(80)]
        public string DESCRIPTION { get; set; }

        [DisplayName("Fecha Creación")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }

        [DisplayName("Fecha Modificación")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

        [DisplayName("Uduario Creación")]
        public string LOG_USER_CREAT { get; set; }

        [DisplayName("Usuario modificación")]
        public string LOG_USER_UPDAT { get; set; }

        public DateTime? ENDS { get; set; }

        public string ACTIVO { get; set; }
        
    }
}

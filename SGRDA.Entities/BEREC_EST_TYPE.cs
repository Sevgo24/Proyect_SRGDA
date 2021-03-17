using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_EST_TYPE : Paginacion
    {
        [DisplayName("Propietario")]
        public string OWNER { get; set; }

        [DisplayName("ID")]
        public decimal ESTT_ID { get; set; }

        [DisplayName("Actividad Económica")]
        [MaxLength(10)]
        public string ECON_ID { get; set; }

        public string ECON_DESC { get; set; }
        
        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Ingrese descripción")]
        [MaxLength(80)]
        public string DESCRIPTION { get; set; }     

        [DisplayName("Fecha creación")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LOG_DATE_CREAT { get; set; }

        [DisplayName("Fecha actualización")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LOG_DATE_UPDATE { get; set; }

        [DisplayName("Usuario creación")]
        public string LOG_USER_CREAT { get; set; }

        [DisplayName("Usuario modificación")]
        public string LOG_USER_UPDATE { get; set; }

        public DateTime? ENDS { get; set; }
        public string ACTIVO { get; set; } 
    }
}

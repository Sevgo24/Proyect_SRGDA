using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class ROLES : Paginacion
    {
        
        public int ROL_ICODIGO_ROL { get;set;}

        [Required(ErrorMessage = "Ingrese Nombre")]
        [MaxLength(50)]
        public string ROL_VNOMBRE_ROL { get; set; }

        [Required(ErrorMessage="Ingrese Descripcion")]
        [MaxLength(50)]
        public string ROL_VDESCRIPCION_ROL { get; set; }
        
        public string ROL_CACTIVO_ROL { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class Roles_Usuarios : Paginacion
    {
        [Required(ErrorMessage = "Seleccione el Usuario")]
        [DisplayName("Módulo")]
        public int USUA_ICODIGO_USUARIO { get; set; }

        public string NOMBRE_COMPLETO { get; set; }

        [Required(ErrorMessage = "Seleccione el Rol")]
        [DisplayName("Rol")]
        public int ROL_ICODIGO_ROL { get; set; }

        public string ROL_VNOMBRE_ROL { get; set; }

        [Required(ErrorMessage = "Seleccione el Estado")]
        [DisplayName("Activo")]
        public string USRO_ACTIVO { get; set; }
        
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}

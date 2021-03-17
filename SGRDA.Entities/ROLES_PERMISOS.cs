using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class ROLES_PERMISOS : Paginacion
    {
        [Required(ErrorMessage = "Seleccione el Modulo")]
        [DisplayName("Módulo")]
        public int MODU_ICODIGO_MODULO { get; set; }

        [Required(ErrorMessage = "Seleccione el Rol")]
        [DisplayName("Rol")]
        public int ROL_ICODIGO_ROL { get; set; }

        [Required(ErrorMessage = "Seleccione el Modulo")]
        [DisplayName("Módulo")]
        public int CABE_ICODIGO_MODULO { get; set; }

        [Required(ErrorMessage = "Seleccione el Nivel")]
        public int MODU_INIVEL_MODULO { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre de Rol")]
        [DisplayName("Rol")]
        public string ROL_VDESCRIPCION_ROL { get; set; }

        [Required(ErrorMessage = "Ingrese Nombre del Modulo")]
        [DisplayName("Nombre")]
        public string MODU_VNOMBRE_MODULO { get; set; }

        [Required(ErrorMessage = "Ingrese la Ruta")]
        [DisplayName("Ruta")]
        public string MODU_VRUTA_PAGINA { get; set; }

        [Required(ErrorMessage = "Ingrese la Descripción")]
        [DisplayName("Descripción")]
        public string MODU_VDESCRIPCION_MODULO { get; set; }

        [Required(ErrorMessage = "Seleccione Activo")]
        [DisplayName("Activo")]
        public string ROMO_CACTIVO { get; set; }

        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public string BNK_ID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class CABECERA_MODULO : Paginacion
    {
        public int CABE_ICODIGO_MODULO { get; set; }

        [Required(ErrorMessage = "Ingrese Nombre")]
        [MaxLength(50)]
        public string CABE_VNOMBRE_MODULO { get; set; }

        public string CABE_CACTIVO_MODULO { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}

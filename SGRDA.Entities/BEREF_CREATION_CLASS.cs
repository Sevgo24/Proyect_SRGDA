using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREF_CREATION_CLASS : Paginacion
    {
        [DisplayName("Clase de Creación")]
        public string CLASS_COD { get; set; }

        [DisplayName("Tipo Documento")]
        [MaxLength(10)]
        public string CLASS_DESC { get; set; }

        [DisplayName("Clase")]
        [Required(ErrorMessage = "Código de la clase")]
        public string COD_PARENT_CLASS { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}

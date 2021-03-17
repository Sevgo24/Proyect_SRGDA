using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_DOCUMENTS_GRAL : Paginacion
    {
        [DisplayName("Propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }

     
        public decimal DOC_ID { get; set; }

        [DisplayName("Tipo Documento")]
        [Required(ErrorMessage = "Ingrese la tipo de documento")]
        public decimal DOC_TYPE { get; set; }

        //descripcion del tipo de documento
        [DisplayName("Tipo Documento")]
        public string DOC_DESC { get; set; }

        public decimal ENT_ID { get; set; }

        [DisplayName("Fecha Inclusión")]
        [Required(ErrorMessage = "Ingrese la fecha de inclusión")]
        public Nullable<DateTime> DOC_DATE { get; set; }

        [DisplayName("Versión")]
        [Required(ErrorMessage = "Ingrese la versión del documento")]
        public decimal DOC_VERSION { get; set; }

        [DisplayName("Usuario Anexa Documento")]
        [MaxLength(30)]
        public string DOC_USER { get; set; }

        [DisplayName("Ruta del Documento")]
        [MaxLength(30)]
        public string DOC_PATH { get; set; }

        public Nullable<DateTime> ENDS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
    }
}

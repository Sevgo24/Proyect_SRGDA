using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_DOCUMENT_TYPE : Paginacion
    {
        [DisplayName("Propietario")]
        public string OWNER { get; set; }

        [DisplayName("Tipo de Documento")]
        public decimal DOC_TYPE { get; set; }

        [Required(ErrorMessage = "Ingrese descripción")]
        [MaxLength(40)]

        [DisplayName("Descripción")]
        public string DOC_DESC { get; set; }

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

        public int ESTADO { get; set; }
        public string ACTIVO { get; set; }
        public string DOC_OBSERV { get; set; }
    }
}

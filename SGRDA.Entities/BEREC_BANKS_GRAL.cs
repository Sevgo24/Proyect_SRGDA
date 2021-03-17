using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_BANKS_GRAL : Paginacion
    {
        [DisplayName("Propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Código")]
        //[MaxLength(10)]
        public decimal BNK_ID { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Ingrese nombre")]
        [MaxLength(40)]
        public string BNK_NAME { get; set; }

        [DisplayName("Long. Cód. Sucursal")]
        [Required(ErrorMessage = "Ingrese long. cód. sucursal")]
        public decimal BNK_C_BRANCH { get; set; }

        [DisplayName("Long. Dígito Control")]
        [Required(ErrorMessage = "Ingrese long. dígito control")]
        public decimal BNK_C_DC { get; set; }

        [DisplayName("Long. Cód. Cuenta")]
        [Required(ErrorMessage = "Ingrese long. cód. cuenta")]
        public decimal BNK_C_ACCOUNT { get; set; }

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

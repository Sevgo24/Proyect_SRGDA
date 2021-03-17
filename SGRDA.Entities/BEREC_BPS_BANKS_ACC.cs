using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_BPS_BANKS_ACC : Paginacion
    {
        [DisplayName("Propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Id")]
        [Required(ErrorMessage = "Ingrese socio del negocio")]
        public decimal BPS_ID { get; set; }

        [DisplayName("Identificador Banco")]
        [Required(ErrorMessage = "Ingrese Identificador banco")]
        public decimal BNK_ID { get; set; }

        public string BNK_NAME { get; set; }

        [DisplayName("Identificador Sucursal")]
        [Required(ErrorMessage = "Ingrese indetificador sucursal")]
        [MaxLength(10)]
        public string BRCH_ID { get; set; }

        [DisplayName("Cuenta Bancaria")]
        [Required(ErrorMessage = "Ingrese cuenta bancaria")]
        [MaxLength(30)]
        public string BACC_NUMBER { get; set; }

        [DisplayName("Digito de Control")]
        [MaxLength(10)]
        public string BACC_DC { get; set; }

        [DisplayName("Tipo Cta. Bancaria")]
        [Required(ErrorMessage = "Ingrese tipo cuenta bancaria")]
        [MaxLength(3)]
        public string BACC_TYPE { get; set; }

        [DisplayName("Cta. Principal")]
        [Required(ErrorMessage = "Ingrese cta. principal")]
        [MaxLength(1)]
        public string BACC_DEF { get; set; }

        [DisplayName("Fecha creación")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }

        [DisplayName("Fecha actualización")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }

        [DisplayName("Usuario creación")]
        public string LOG_USER_CREAT { get; set; }

        [DisplayName("Usuario modificación")]
        public string LOG_USER_UPDATE { get; set; }

        public DateTime? ENDS { get; set; }

        public string ACTIVO { get; set; }

        public string BRCH_NAME { get; set; }

        public decimal BPS_ACC_ID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_TAXES : Paginacion
    {
        [DisplayName("Propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Correlativo")]
        public decimal TAX_ID { get; set; }
        [Required(ErrorMessage = "Ingrese Territorio de impuesto")]

        [DisplayName("Territorio impuesto")]
        public decimal TIS_N { get; set; }

        public string NAME_TER { get; set; }

        [MaxLength(3)]
        [DisplayName("Código impuesto")]
        public string TAX_COD { get; set; }

        [Required(ErrorMessage = "Ingrese Descripción")]
        [MaxLength(30)]
        [DisplayName("Descripción")]
        public string DESCRIPTION { get; set; }

        [DisplayName("Fecha Vigencia")]
        public Nullable<DateTime> START { get; set; }

        [DisplayName("Fecha Baja")]
        public DateTime? ENDS { get; set; }

        [Required(ErrorMessage = "Ingrese cuenta contable")]
        [DisplayName("Cuenta Contable")]
        public decimal TAX_CACC { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public string ESTADO { get; set; }
        public string FECHA_VIGENCIA { get; set; }
        public List<BEImpuestoValor> Valores { get; set; }
        public string Activo { get; set; }
    }
}

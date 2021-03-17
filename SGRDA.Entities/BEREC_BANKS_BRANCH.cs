using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREC_BANKS_BRANCH : Paginacion
    {
        [DisplayName("Propietario")]
        [MaxLength(3)]
        public string OWNER { get; set; }

        [DisplayName("Ident. de Banco")]
        public decimal BNK_ID { get; set; }

        [DisplayName("Ident. de Sucursal")]
        [MaxLength(10)]
        [Required(ErrorMessage = "Ingrese identificador de sucursal")]
        public string BRCH_ID { get; set; }

        [DisplayName("Nombre")]
        [MaxLength(40)]
        [Required(ErrorMessage = "Ingrese nombre de sucursal")]
        public string BRCH_NAME { get; set; }

        public string BNK_NAME { get; set; }        

        [DisplayName("Cod. Dirección")]
        public decimal ADD_ID { get; set; }

        public string ADDRESS { get; set; }

        public string auxBNK_ID { get; set; }

        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public List<BEDireccion> Direccion { get; set; }
        public List<BEREC_BANKS_BPS> Contacto { get; set; }

        public DateTime? ENDS { get; set; }

        public string ACTIVO { get; set; }
        public decimal ID { get; set; }
    }
}

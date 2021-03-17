using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEREF_DIVISIONES : Paginacion
    {
        [DisplayName("Propietario")]
        [MaxLength(3)]  
        public string OWNER { get; set; }

        [DisplayName("Código de Division")]
        [MaxLength(10)]  
        [Required(ErrorMessage = "Ingrese código de división")]
        public decimal DAD_ID { get; set; }

        [DisplayName("Ident. Corta de División")]
        [MaxLength(9)]  
        public string DAD_CODE { get; set; }

        [DisplayName("Ident. Larga de División")]
        [MaxLength(40)]  
        public string DAD_NAME { get; set; }

        [DisplayName("Tipo de División")]
        [MaxLength(3)]  
        public string DAD_TYPE { get; set; }

        [DisplayName("Subtipo de División")]
        [MaxLength(3)]  
        public string DAD_STYPE { get; set; }

        [DisplayName("Dependencia Jerarquica")]
        public decimal DAD_BELONGS { get; set; }
        
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        public Nullable<DateTime> ENDS { get; set; }

        [DisplayName("Tipo de Divisiones")]
        [MaxLength(3)]
        public string DAD_TNAME { get; set; }

        [DisplayName("Subtipos de Divisones")]
        [MaxLength(3)]
        public string DAD_SNAME { get; set; }

        /// <summary>
        /// Territorio
        /// </summary>
        public decimal TIS_N { get; set; }
        public string NAME_TER { get; set; }
        public string ESTADO { get; set; }
        public string DIV_DESCRIPTION { get; set; }
    }
}

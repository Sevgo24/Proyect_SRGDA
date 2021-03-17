using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace SGRDA.Entities
{
    public partial class BECaracteristicaPredefinida : Paginacion
    {
        public Int32 RowNumber { get; set; }
        public string OWNER { get; set; }
        public decimal CHAR_TYPES_ID { get; set; }
        public decimal CHAR_ID { get; set; }
        public decimal EST_ID { get; set; }
        public string TIPO { get; set; }
        public decimal SUBE_ID { get; set; }
        public string SUBTIPO { get; set; }
        public string CHAR_SHORT { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }

        public string ACTIVO { get; set; }

        public List<BECaracteristicaPredefinida> DetalleCaracteristica { get; set; }
    }
}

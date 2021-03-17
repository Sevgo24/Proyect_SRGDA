using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BECaracteristicaLic  
    {
        public string OWNER { get; set; }
         
        public decimal LIC_CAR_ID { get; set; } 
        public decimal LIC_ID { get; set; } 
        public decimal CHAR_ID { get; set; } 
        public decimal? LIC_CHAR_VAL { get; set; } 
        public string CHAR_LONG{ get; set; }
        public string CHAR_SHORT { get; set; } 


        public string LIC_VAL_ORIGEN { get; set; }
        public DateTime? START { get; set; }
        public DateTime? ENDS { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public string LOG_USER_CREAT { get; set; }

        public decimal LIC_PL_ID { get; set; }


        /// <summary>
        /// Indicador que sirve para determinar que el valor de la caractreristica es un valor alterado(No es el dato real)
        /// debido a que se desea llegar a un monto para la factura distinto a lo planificado.
        /// </summary>
        public bool? FLG_MANUAL { get; set; }
        /// <summary>
        /// Descripcion,comentario relacionado al campo FLG_MANUAL
        /// </summary>
        public string COMMENT_FLG { get; set; }
         

 
    }
}

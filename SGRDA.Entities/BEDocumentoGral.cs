using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEDocumentoGral:Paginacion
    {
        public string OWNER { get; set; }
        public decimal DOC_ID { get; set; }
        public int DOC_TYPE { get; set; }
        public int ENT_ID { get; set; }
         
        public DateTime DOC_DATE { get; set; } 
        public decimal DOC_VERSION { get; set; }
        public string DOC_USER { get; set; }
        public string DOC_PATH { get; set; }          
        public DateTime? ENDS { get; set; }

        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }

        /// <summary>
        /// Propiedad sólo para documentos de una licencia
        /// </summary>
        public string DOC_ORG { get; set; }

        //Id para identificar el documento de contacto llamada campaña
        public decimal CONC_MID { get; set; }
        public Stream ArchivoBytes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEEstadoLicencia : Paginacion
    {
        public string OWNER { get; set; }
        public decimal LICS_ID { get; set; }
        public decimal LIC_TYPE { get; set; }
        public string LICS_NAME { get; set; }
        public string LIC_TDESC { get; set; }
        public char LICS_INI { get; set; }
        public char LICS_END { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public DateTime? ENDS { get; set; }

        public string Activo { get; set; }
        public List<BELicenciaEstadoTab> ListaTab { get; set; }
    }
}

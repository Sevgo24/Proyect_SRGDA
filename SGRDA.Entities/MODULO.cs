using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class MODULO
    {
        public int MODU_ICODIGO_MODULO { get; set; }
        public int MODU_INIVEL_MODULO { get; set; }
        public string MODU_VNOMBRE_MODULO { get; set; }
        public string MODU_VRUTA_PAGINA { get; set; }
        public string MODU_VDESCRIPCION_MODULO { get; set; }
        public int MODU_ICODIGO_MODULO_DEPENDIENTE { get; set; }
        public int MODU_IORDEN_MODULO { get; set; }
        public string MODU_CACTIVO_MODULO { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public int CABE_ICODIGO_MODULO { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.extends
{
    public class exREC_EST_SUBTYPE : Paginacion
    {
        public string DESCRIPTIONTYPE { get; set; }
        public string ECON_DESC { get; set; }        
    }

    public class pruebas : Paginacion
    {
        public string DEMO { get; set; }
    }

    public class Paginacion
    {
        public int Pagina { get; set; }
        public int CantregxPag { get; set; }
        public int TotalVirtual { get; set; }
        public string PropOrder { get; set; }
        public string TipoOrder { get; set; }
    }
}

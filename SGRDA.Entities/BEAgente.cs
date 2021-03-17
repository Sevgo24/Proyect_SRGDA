using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEAgente : Paginacion
    {
        public IList<BEAgente> ListaAgente { get; set; }
        public BEAgente()
        {
            ListaAgente = new List<BEAgente>();
        }

        public string OWNER { get; set; }
        public decimal LEVEL_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public string DEPENDENCIA { get; set; }
        public decimal LEVEL_DEP { get; set; }
        public Nullable<DateTime> ENDS { get; set; }
        public Nullable<DateTime> LOG_DATE_CREAT { get; set; }
        public Nullable<DateTime> LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }

        public string ESTADO { get; set; }

    }
}

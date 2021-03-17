using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BERutas : Paginacion
    {
        public string OWNER { get; set; }
        public decimal ROU_ID { get; set; }
        public string ROU_COD { get; set; }
        public string ROU_TSEL { get; set; }
    }
}

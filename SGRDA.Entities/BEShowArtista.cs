using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEShowArtista : BEAuditoria
    {

      public  string OWNER { get; set; }
      public decimal SHOW_ID { get; set; }
      public decimal ARTIST_ID { get; set; }
      public string ARTIST_PPAL { get; set; }
      public string NAME { get; set; }
      public string IP_NAME { get; set; }
      public decimal ARTIST_ID_SGS { get; set; }
      public decimal ESTADO_ID { get; set; }
      public string ESTADO { get; set; }
      public decimal REPORT_ID { get; set; }


    }
}

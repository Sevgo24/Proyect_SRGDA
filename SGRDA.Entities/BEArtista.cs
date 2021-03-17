using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEArtista
    {
        public string OWNER { get; set; }
        public decimal COD_ARTIST_SQ { get; set; }
        public string NAME { get; set; }
        public string IP_NAME { get; set; }
        public string FIRST_NAME { get; set; }
        public decimal ARTIST_ID { get; set; }
        public string ART_COMPLETE { get; set; }
        public string ESTADO { get; set; }
        public string LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public int TotalVirtual { get; set; }
        public List<BEArtista> listaArtista = null;
        public string SHOW_NAME { get; set; }
        public decimal LIC_ID { get; set; }
        public decimal ESTADO_ID { get; set; }
        public decimal SHOW_ID { get; set; }

        public string OBSERVACION { get; set; }
        public BEArtista()
        {
            listaArtista = new List<BEArtista>();
        }

        public string OFF_NAME { get; set; }
        public string MODALIDAD { get; set; }
        public string RUBRO { get; set; }
        public string FEC_CANCELED { get; set; }
        public string FACTURA { get; set; }

    }
}

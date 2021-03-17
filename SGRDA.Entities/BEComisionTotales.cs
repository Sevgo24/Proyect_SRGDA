using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEComisionTotales
    {
        public string OWNER { get; set; }
        public decimal PRG_ID { get; set; }
        public string PRG_DESC { get; set; }
        public DateTime PRG_LASTL { get; set; }
        public decimal RAT_FID { get; set; }
        public DateTime START { get; set; }
        public DateTime ENDS { get; set; }
        public DateTime? LOG_DATE_CREATE { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public int TotalVirtual { get; set; }
        public List<BEComisionTotales> ListaComisionTotales = null;

        public BEComisionTotales()
        {
            ListaComisionTotales = new List<BEComisionTotales>();
        }
        
        public List<BEComisionRepresentantes> Representantes { get; set; }
        public List<BEComisionRecaudadorRango> Rangos { get; set; }

        ///Datos del recaudador
        public string BPS_NAME { get; set; }
        public decimal BPS_ID { get; set; }

        //Datos de la periodicidad
        public string RAT_FDESC { get; set; }
    }
}

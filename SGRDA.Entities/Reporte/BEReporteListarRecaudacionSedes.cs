using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BEReporteListarRecaudacionSedes
    {
        IList<BEReporteListarRecaudacionSedes> ReporteListarRecaudacionSedes { get; set; }
        public BEReporteListarRecaudacionSedes()
        {
            ReporteListarRecaudacionSedes = new List<BEReporteListarRecaudacionSedes>();
        }
        //


        public string RUBRO { get; set; }
        public string DIVISION_EST { get; set; }
        public decimal total { get; set; }
        public string NODO { get; set; }
        public decimal TV { get; set; }
        public decimal CABLE { get; set; }
        public decimal RADIO { get; set; }
        public decimal SINCRONIZACION { get; set; }
        public decimal FONO { get; set; }
        public decimal REDES { get; set; }
        public decimal MUSICA_ESPERA { get; set; }
        public decimal TRANSPORTE { get; set; }
        public decimal LOCALES { get; set; }
        public decimal ESPECTACULOS { get; set; }

        //
        public decimal REDRADIO { get; set; }
        public decimal REDTV { get; set; }
        public decimal REDSINCRO { get; set; }

        public decimal BAILES { get; set; }

        public decimal C_PRI { get; set; }

        public decimal GRAN_DERECHO { get; set; }
        public decimal EMPADRONAMIENTO { get; set; }

        public decimal INTERNACIONAL { get; set; }
        public decimal VUD { get; set; }
        public decimal NC { get; set; }

        public decimal DERECHOS_INTERNACIONALES { get; set; }
    }
}

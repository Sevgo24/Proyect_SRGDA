using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEAdministracionEmisionMensual
    {
        public decimal ID_VALUE { get; set; }

        public string DESC_VALUE { get; set; }

        public int DIA { get; set; }

        public string RANGO_INICIAL { get; set; }

        public string RANGO_FINAL { get; set; }

        public decimal OFF_ID { get; set; }

        public DateTime ENDS { get; set; }

        public decimal OFICINA { get; set; }

        public string FECHA_DE_BAJA{get; set;}

        public string ESTADO { get; set; }



    }
}

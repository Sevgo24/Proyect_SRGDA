using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BE_LICENCIA_X_ARTISTA
    {
        public int CODIGO_LICENCIA { get; set; }
        public string FECHA_EVENTO { get; set; }
        public int CANTIDAD_DE_ARTISTAS { get; set; }
        public string NOMBRE_ESTABLECIMIENTO { get; set; }
        public decimal MONTO_COBRADO { get; set; }
        public string LUGAR { get; set; }

    }
}

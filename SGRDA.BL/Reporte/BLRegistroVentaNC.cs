using SGRDA.DA.Reporte;
using SGRDA.Entities.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.BL.Reporte
{
    public class BLRegistroVentaNC
    {
        public List<BERegistroVentaNC> ReporteRegistroVentaNC(string fini, string ffin, string oficina, int ESTADO)
        {
            return new DARegistroVentaNC().ReporteRegistroVenta_NC(fini, ffin, oficina, ESTADO);
        }
    }
}


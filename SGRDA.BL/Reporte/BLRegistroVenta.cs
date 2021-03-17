using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities.Reporte;
using SGRDA.DA.Reporte;
using SGRDA.Entities;
using SGRDA.Entities.WorkFlow;

namespace SGRDA.BL.Reporte
{
    public class BLRegistroVenta
    {
        public List<BERegistroVenta> ReporteRegistroVenta(string fini, string ffin, string oficina, int? rubro,string parametros)
        {
            return new DARegistroVenta().ReporteRegistroVenta(fini, ffin, oficina, rubro,parametros);
        }
    }
}

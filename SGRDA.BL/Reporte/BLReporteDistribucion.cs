using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using SGRDA.DA.Reporte;
using SGRDA.Entities.Reporte;
using System.Transactions;

namespace SGRDA.BL.Reporte
{
    public class BLReporteDistribucion
    {
        public List<BEReporteDistribucion> ListadDistribucion_Neta(string Fini, string Ffin)
        {
            return new DAReporteDistribucion().ListadDistribucion_Neta(Fini, Ffin);
        }

        public List<BEReporteDistribucion> ListarDistribucion_Resumen(string Fini, string Ffin)
        {
            return new DAReporteDistribucion().ListarDistribucion_Resumen(Fini, Ffin);
        }

        //public List<BEReporteDistribucion> ListarDistribucion_Resumen_Agrupado(string Fini, string Ffin)
        //{
        //    return new DAReporteDistribucion().ListarDistribucion_Resumen_Agrupado(Fini, Ffin);
        //}

        public List<BEReporteDistribucion> ListarDistribucion_Detallado(string Fini, string Ffin)
        {
            return new DAReporteDistribucion().ListarDistribucion_Detallado(Fini, Ffin);
        }

        public List<BEReporteDistribucion> ListaContableDesplegable()
        {
            return new DAReporteDistribucion().ListaContableDesplegable();
        }

    }
}

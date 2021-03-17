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
    public class BLReporteResumenFacturacionMensual
    {
        public List<BEReporteResumenFacturacionMensual> ReporteResumenFacturacionMensual(string Fini, string Ffin, string FiniCan, string FfinCan, string FiniCon, string FfinCon, string oficina, string parametros, int DEPARTAMENTO, int PROVINCIA, int DISTRITO, string estado, string TipoDoc,string tipoenvio,string ModalidadDetalle)
        {
            return new DAReporteResumenFacturacionMensual().ReporteResumenFacturacionMensual(Fini, Ffin, FiniCan, FfinCan, FiniCon, FfinCon, oficina, parametros, DEPARTAMENTO, PROVINCIA, DISTRITO, estado, TipoDoc, tipoenvio,ModalidadDetalle);
        }
    }
}

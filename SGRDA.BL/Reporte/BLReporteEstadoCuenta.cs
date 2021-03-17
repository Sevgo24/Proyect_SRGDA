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
    public class BLReporteEstadoCuenta
    {
        public List<BEReporteEstadoCuenta> ReporteEstadoCuenta(string fini, string ffin, int BPS_ID, int EST_ID, string oficina, string LIC_ID, int ESTADO, string oficina_nombre, string usuario)
        {
            return new DAReporteEstadoCuenta().ListarReporteEstadoCuenta(fini, ffin, BPS_ID, EST_ID, oficina, LIC_ID, ESTADO, oficina_nombre, usuario);
        }
        public List<BEReporteEstadoCuenta> ReporteEstadoCuentaResumen(string fini, string ffin, int BPS_ID, int EST_ID, string oficina, string LIC_ID, int ESTADO, string oficina_nombre, string usuario)
        {
            return new DAReporteEstadoCuenta().ListarReporteEstadoCuentaResumen(fini, ffin, BPS_ID, EST_ID, oficina, LIC_ID, ESTADO, oficina_nombre, usuario);
        }
        public List<BEReporteEstadoCuenta> ReporteEstadoCuentaTransporte(string fini, string ffin, int BPS_ID, int EST_ID, string oficina, string LIC_ID, int ESTADO, string oficina_nombre, string usuario, string Modalidad)
        {
            return new DAReporteEstadoCuenta().ListarReporteEstadoCuentaTransporte(fini, ffin, BPS_ID, EST_ID, oficina, LIC_ID, ESTADO, oficina_nombre, usuario, Modalidad);
        }
    }
}

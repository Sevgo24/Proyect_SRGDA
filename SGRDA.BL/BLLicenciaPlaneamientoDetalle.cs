using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLLicenciaPlaneamientoDetalle
    {
        public List<BELicenciaPlaneamientoDetalle> ObtenerPagosParciales(string owner, int idfactura)
        {
            return new DALicenciaPlaneamientoDetalle().ObtenerPagosParciales(owner, idfactura);
        }

        public bool ActualizarDetallePlanificacion(BELicenciaPlaneamientoDetalle en)
        {
            var exitoPlanificacionDetalle = new DALicenciaPlaneamientoDetalle().ActualizarDetallePlanificacion(en);
            return exitoPlanificacionDetalle;
        }
    }
}

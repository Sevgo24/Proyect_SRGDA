using SGRDA.DA.Reporte;
using SGRDA.Entities.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.BL.Reporte
{
    public class BL_Reporte_Bec_Inactiva
    {
        public List<Be_Bec_Inactivas> Listar_Becs_Inactivas(decimal LIC_ID, decimal BPS_ID, decimal INV_ID, string Serie, decimal nro,
            decimal Bec_id, string Fini_Rechazo, string Ffin_Rechazo, decimal oficina_id)
        {
            return new DA_Reporte_Bec_Inactiva().Listar_Becs_Inactivas(LIC_ID, BPS_ID, INV_ID, Serie, nro, Bec_id, Fini_Rechazo, Ffin_Rechazo, oficina_id);
        }
    }
}

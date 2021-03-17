using SGRDA.DA.Reporte;
using SGRDA.Entities.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.BL.Reporte
{
    public class BL_ReporteEstablecimiento
    {
        public List<BE_Reporte_Establecimiento> ListarDatosEstablecimiento(string MOG_ID,int id_socio, int id_departamento, int id_provincia
            , int id_distrito, int id_est)
        {
            return new DA_ReporteEstablecimiento().ListarDatosEstablecimiento(MOG_ID,id_socio, id_departamento, id_provincia, id_distrito, id_est);
        }
    }
}

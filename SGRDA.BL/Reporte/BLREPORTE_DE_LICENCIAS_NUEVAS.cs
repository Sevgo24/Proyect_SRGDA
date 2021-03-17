using SGRDA.DA.Reporte;
using SGRDA.Entities.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.BL.Reporte
{
    public class BLREPORTE_DE_LICENCIAS_NUEVAS
    {
        public List<BE_REPORTE_DE_LICENCIAS_NUEVAS> ObtenerDatosREPORTE_DE_LICENCIAS_NUEVAS(string finicio, string ffin, int ID_SOCIO, string ID_MODALIDAD, int ID_OFICINA, int Estado)
        {
            return new DAREPORTE_DE_LICENCIAS_NUEVAS().ObtenerDatosREPORTE_DE_LICENCIAS_NUEVAS(finicio, ffin, ID_SOCIO, ID_MODALIDAD, ID_OFICINA, Estado);
        }
    }
}

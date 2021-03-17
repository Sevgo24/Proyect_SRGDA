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
    public class BLReporteListarusuarios
    {
        public List<BEReporteListarUsuarios> ListarUsuarios(string ffni, string ffin, string usuario, string numero)
        {
            return new DAReporteListarUsuario().ListarReporteUsuario(ffni, ffin, usuario, numero);
        }
    }
}

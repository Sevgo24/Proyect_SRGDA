using SGRDA.DA.Reporte;
using SGRDA.Entities.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.BL.Reporte
{
    public class BL_LICENCIA_X_ARTISTA
    {
        public List<BE_LICENCIA_X_ARTISTA> LISTAR_LICENCIA_X_ARTISTA(string artista, string FECHA_INICIO, string FECHA_FIN)
        {
            return new DA_LICENCIA_X_ARTISTA().LISTAR_LICENCIA_X_ARTISTA(artista, FECHA_INICIO, FECHA_FIN);
        }
    }
}

using SGRDA.DA.Reporte;
using SGRDA.Entities.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.BL.Reporte
{
    public class BLReporteArtistaDetallado
    {
        DAReporteArtistaDetallado da = new DAReporteArtistaDetallado();

        public List<BEReporteArtistaDetallado> ListarArtistaDetallado(string femi_ini, string femi_fin, string feve_ini, string feve_fin, string fcan_ini, string fcan_fin, string fcon_ini, string fcon_fin, string artista)
        {
            return da.ListarArtistaDetallado(femi_ini, femi_fin, feve_ini, feve_fin, fcan_ini, fcan_fin, fcon_ini, fcon_fin, artista);
        }
        public List<BEArtistas> ListaArtista(string Artista)
        {
            return da.ListaArtista(Artista);
        }
    }
}

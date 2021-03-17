using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLComisionExclusion
    {
        public int Insertar(BEComisionExclusion en)
        {
            return new DAComisionExclusion().Insertar(en);
        }

        public int Actualizar(BEComisionExclusion en)
        {
            return new DAComisionExclusion().Actualizar(en);
        }

        public int ValidacionInsertar(BEComisionExclusion en)
        {
            return new DAComisionExclusion().ValidacionInsertar(en);
        }

        public BEComisionExclusion Obtener(string owner, decimal id, decimal idDivAdm)
        {
            return new DAComisionExclusion().Obtener(owner, id, idDivAdm);
        }

        public List<BEComisionExclusion> ListarPage(string Owner, string Origen, string Sociedad, string Clases, string Grupo, string Derecho, string Incidencia, string Frecuencia, string Repertorio, decimal? Tarifa, decimal TipoComision, decimal OrigenComision, decimal Division, DateTime FechaIni, DateTime FechaFin, int st, int pagina, int cantRegxPag)
        {
            return new DAComisionExclusion().ListarPage(Owner, Origen, Sociedad, Clases, Grupo, Derecho, Incidencia, Frecuencia, Repertorio, Tarifa, TipoComision, OrigenComision, Division, FechaIni, FechaFin, st, pagina, cantRegxPag);
        }

        public int Eliminar(BEComisionExclusion en)
        {
            return new DAComisionExclusion().Eliminar(en);
        }
    }
}

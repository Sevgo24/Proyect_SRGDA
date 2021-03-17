using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLComisionProducto
    {
        public int Insertar(BEComisionProducto en)
        {
            return new DAComisionProducto().Insertar(en);
        }

        public int Actualizar(BEComisionProducto en)
        {
            return new DAComisionProducto().Actualizar(en);
        }

        public int Eliminar(BEComisionProducto en)
        {
            return new DAComisionProducto().Eliminar(en);
        }

        public int ValidacionInsertar(BEComisionProducto en)
        {
            return new DAComisionProducto().ValidacionInsertar(en);
        }

        public BEComisionProducto Obtener(string owner, decimal id, decimal idNivAgent)
        {
            return new DAComisionProducto().Obtener(owner, id, idNivAgent);
        }

        public List<BEComisionProducto> ListarPage(string Owner, string Origen, string Sociedad, string Clases, string Grupo, string Derecho, string Incidencia, string Frecuencia, string Repertorio, decimal? Tarifa, decimal TipoComision, decimal OrigenComision, decimal NivelAgente, DateTime FechaIni, DateTime FechaFin, int st, int pagina, int cantRegxPag)
        {
            return new DAComisionProducto().ListarPage(Owner, Origen, Sociedad, Clases, Grupo, Derecho, Incidencia, Frecuencia, Repertorio, Tarifa, TipoComision, OrigenComision, NivelAgente, FechaIni, FechaFin, st, pagina, cantRegxPag);
        }
    }
}

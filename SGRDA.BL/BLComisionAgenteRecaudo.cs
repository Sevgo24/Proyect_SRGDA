using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLComisionAgenteRecaudo
    {
        public int Insertar(BEComisionAgenteRecaudo en)
        {
            return new DAComisionAgenteRecaudo().Insertar(en);
        }

        public int Actualizar(BEComisionAgenteRecaudo en)
        {
            return new DAComisionAgenteRecaudo().Actualizar(en);
        }

        public BEComisionAgenteRecaudo ObtenerNivelAgente(string owner, decimal idAgente)
        {
            return new DAComisionAgenteRecaudo().ObtenerNivelAgente(owner, idAgente);
        }

        public BEComisionAgenteRecaudo Obtener(string owner, decimal id, decimal idAgente)
        {
            return new DAComisionAgenteRecaudo().Obtener(owner, id, idAgente);
        }

        public List<BEComisionAgenteRecaudo> ListarPage(string Owner, string Origen, string Sociedad, string Clases,
            string Grupo, string Derecho, string Incidencia, string Frecuencia, string Repertorio, decimal? Tarifa,
            decimal TipoComision, decimal OrigenComision, decimal Agente, DateTime FechaIni,
            DateTime FechaFin, int st, int pagina, int cantRegxPag)
        {
            return new DAComisionAgenteRecaudo().ListarPage(Owner, Origen, Sociedad, Clases, Grupo, Derecho, Incidencia, Frecuencia, Repertorio, Tarifa, TipoComision, OrigenComision, Agente, FechaIni, FechaFin, st, pagina, cantRegxPag);
        }

        public BEComisionAgenteRecaudo ObtenerTarifaPorTemporalidad(string owner, decimal? idModalidad, decimal idTemporalidad)
        {
            return new DAComisionAgenteRecaudo().ObtenerTarifaPorTemporalidad(owner, idModalidad, idTemporalidad);
        }

        public int Eliminar(BEComisionAgenteRecaudo en)
        {
            return new DAComisionAgenteRecaudo().Eliminar(en);
        }

        public List<BEComisionAgenteRecaudo> ListarTemporalidadporModalidad(string owner, decimal idModalidad)
        {
            return new DAComisionAgenteRecaudo().ListarTemporalidadporModalidad(owner, idModalidad);
        }

        public int ValidacionInsertar(BEComisionAgenteRecaudo en)
        {
            return new DAComisionAgenteRecaudo().ValidacionInsertar(en);
        }
    }
}

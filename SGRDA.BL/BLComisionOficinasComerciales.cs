using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLComisionOficinasComerciales
    {
        public int Insertar(BEComisionOficinasComerciales en)
        {
            return new DAComisionOficinasComerciales().Insertar(en);
        }

        public int Actualizar(BEComisionOficinasComerciales en)
        {
            return new DAComisionOficinasComerciales().Actualizar(en);
        }

        public List<BEComisionOficinasComerciales> ListarOficinas(string owner)
        {
            return new DAComisionOficinasComerciales().ListarOficinas(owner);
        }

        public BEComisionOficinasComerciales Obtener(string owner, decimal id, decimal idNivAgent, decimal idOficina)
        {
            return new DAComisionOficinasComerciales().Obtener(owner,id, idNivAgent, idOficina);
        }

        public List<BEComisionOficinasComerciales> ListarPage(string Owner, string Origen, string Sociedad,
            string Clases, string Grupo, string Derecho, string Incidencia, string Frecuencia, string Repertorio,
            decimal TipoComision, decimal OrigenComision, decimal NivelAgente, decimal Oficina, DateTime FechaIni,
            DateTime FechaFin, int st, int pagina, int cantRegxPag)
        {
            return new DAComisionOficinasComerciales().ListarPage(Owner, Origen, Sociedad,
                Clases, Grupo, Derecho, Incidencia, Frecuencia, Repertorio,
                TipoComision, OrigenComision, NivelAgente, Oficina, FechaIni,
                FechaFin, st, pagina, cantRegxPag);
        }

        public int Eliminar(BEComisionOficinasComerciales en)
        {
            return new DAComisionOficinasComerciales().Eliminar(en);
        }

        public int ValidacionInsertar(BEComisionOficinasComerciales en)
        {
            return new DAComisionOficinasComerciales().ValidacionInsertar(en);
        }
    }
}

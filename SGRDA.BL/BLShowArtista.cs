using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLShowArtista
    {

        public List<BEShowArtista> ShowsXArtistas(decimal idShow, string owner)
        {
            return new DAShowArtista().ShowsXArtistas(idShow, owner);
        }
        public int Insertar(BEShowArtista entidad)
        {
            return 0;// new DAShow().Insertar(entidad);
        }
        public BEShowArtista ObtenerShow(string owner, decimal idShow)
        {
            return null;// new DAShow().ObtenerShow(idShow, owner);
        }
        public int Actualizar(BEShowArtista aut)
        {
            return 0;// new DAShow().Actualizar(aut);
        }
        public int Eliminar(decimal id, string owner, string usuDel)
        {
            return new DAShow().Eliminar(id, owner, usuDel);
        }
        public int Activar(decimal id, string owner, string usuDel)
        {
            return new DAShow().Activar(id, owner, usuDel);
        }
    }
}

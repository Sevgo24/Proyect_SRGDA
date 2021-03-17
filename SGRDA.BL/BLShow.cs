using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLShow
    {

        public List<BEShow> ShowsXAutorizaciones(decimal idAutorizacion, string owner)
        {
            return new DAShow().ShowsXAutorizaciones(idAutorizacion, owner);
        }
        public int Insertar(BEShow entidad)
        {
            return new DAShow().Insertar(entidad);
        }
        public BEShow ObtenerShow(string owner, decimal idShow)
        {
            return new DAShow().ObtenerShow(idShow, owner);
        }
        public int Actualizar(BEShow aut)
        {
            return new DAShow().Actualizar(aut);
        }
        public int Eliminar(decimal id, string owner, string usuDel)
        {
            return new DAShow().Eliminar(id, owner, usuDel);
        }
        public int Activar(decimal id, string owner, string usuDel)
        {
            return new DAShow().Activar(id, owner, usuDel);
        }

        public List<BEShow> ListaShowxLicencia(decimal codigolic)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAShow().ListarShowxLicencia(owner, codigolic);
        }
        public int ValidarShowArtistaPlan(decimal ShowId, string ShowStart,decimal LIC_AUT_ID,int Opcion)
        {
            return new DAShow().ValidarShowArtistaPlan(ShowId, Opcion, ShowStart, LIC_AUT_ID);
        }
    }
}

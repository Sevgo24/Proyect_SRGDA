using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLTipoCampania
    {
        public List<BETipoCampania> ListarCampaniaPage(string owner, decimal tipo, string dato, int st, int pagina, int cantRegxPag)
        {
            return new DATipoCampania().ListarCampaniaPage(owner, tipo, dato, st, pagina, cantRegxPag);
        }

        public int Eliminar(BETipoCampania en)
        {
            return new DATipoCampania().Eliminar(en);
        }

        public BETipoCampania Obtener(string owner, decimal Id)
        {
            return new DATipoCampania().Obtener(owner, Id);
        }

        public int Insertar(BETipoCampania en)
        {
            return new DATipoCampania().Insertar(en);
        }

        public int Actualizar(BETipoCampania en)
        {
            return new DATipoCampania().Actualizar(en);
        }

        public int ObtenerXDescripcion(BETipoCampania en)
        {
            return new DATipoCampania().ObtenerXDescripcion(en);
        }

        public List<BETipoCampania> ListaTipoCampania(string owner, decimal Tipo, string Descripcion, int Estado)
        {
            return new DATipoCampania().ListaTipoCampania(owner, Tipo, Descripcion, Estado);
        }

        public List<BETipoCampania> ListaDropTipoCampania(string owner)
        {
            return new DATipoCampania().ListaDropTipoCampania(owner);
        }
    }
}

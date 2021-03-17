using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLCentroContacto
    {
        public List<BECentroContacto> ListarCentroContactoPage(string owner, decimal Idoficina, string dato, int st, int pagina, int cantRegxPag)
        {
            return new DACentroContacto().ListarCentroContactoPage(owner, Idoficina, dato, st, pagina, cantRegxPag);
        }

        public int Eliminar(BECentroContacto en)
        {
            return new DACentroContacto().Eliminar(en);
        }

        public BECentroContacto Obtener(string owner, decimal Id)
        {
            return new DACentroContacto().Obtener(owner, Id);
        }

        public int Insertar(BECentroContacto en)
        {
            return new DACentroContacto().Insertar(en);
        }

        public int Actualizar(BECentroContacto en)
        {
            return new DACentroContacto().Actualizar(en);
        }

        public int ObtenerXDescripcion(BECentroContacto en)
        {
            return new DACentroContacto().ObtenerXDescripcion(en);
        }

        public List<BECentroContacto> ListaCentroContactos(string owner, decimal Idoficina, string Descripcion, int Estado)
        {
            return new DACentroContacto().ListaCentroContactos(owner, Idoficina, Descripcion, Estado);
        }

        public List<BECentroContacto> ListarDropCentroContacto(string owner)
        {
            return new DACentroContacto().ListarDropCentroContacto(owner);
        }
    }
}

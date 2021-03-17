using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLTipoComision
    {
        public List<BETipoComision> ListarPaginacion(string owner, string param, int st, int pagina, int cantRegxPag)
        {
            return new DATipoComision().ListarPaginacion(owner, param, st, pagina, cantRegxPag);
        }

        public int Eliminar(BETipoComision en)
        {
            return new DATipoComision().Eliminar(en);
        }

        public int Insertar(BETipoComision en)
        {
            return new DATipoComision().Insertar(en);
        }

        public BETipoComision Obtener(string owner, decimal id)
        {
            return new DATipoComision().Obtener(owner, id);
        }

        public BETipoComision ObtenerDescripcion(string owner, string desc)
        {
            return new DATipoComision().ObtenerDescripcion(owner, desc);
        }

        public int Actualizar(BETipoComision en)
        {
            return new DATipoComision().Actualizar(en);
        }

        public List<BETipoComision> Listar(string owner)
        {
            return new DATipoComision().Listar(owner);
        }
    }
}

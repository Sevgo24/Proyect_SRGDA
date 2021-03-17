using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLOrigenComision
    {
        public List<BEOrigenComision> ListarPage(string owner, string param, int st, int pagina, int cantRegxPag)
        {
            return new DAOrigenComision().ListarPage(owner, param, st, pagina, cantRegxPag);
        }

        public List<BEOrigenComision> Listar(string owner)
        {
            return new DAOrigenComision().Listar(owner);
        }

        public int Eliminar(BEOrigenComision en)
        {
            return new DAOrigenComision().Eliminar(en);
        }

        public int ValidacionOrigenComision(BEOrigenComision en)
        {
            return new DAOrigenComision().ValidacionOrigenComision(en);
        }

        public int Insertar(BEOrigenComision en)
        {
            return new DAOrigenComision().Insertar(en);
        }

        public int Actualizar(BEOrigenComision en)
        {
            return new DAOrigenComision().Actualizar(en);
        }

        public BEOrigenComision Obtener(string owner, decimal id)
        {
            return new DAOrigenComision().Obtener(owner, id);
        }
    }
}

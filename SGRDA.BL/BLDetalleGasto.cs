using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLDetalleGasto
    {
        public List<BEDetalleGasto> Listar_Page_DefinicionGasto(int id, int pagina, int cantRegxPag)
        {
            return new DADetalleGasto().Listar_Page_DetalleGasto(id, pagina, cantRegxPag);
        }
    }
}

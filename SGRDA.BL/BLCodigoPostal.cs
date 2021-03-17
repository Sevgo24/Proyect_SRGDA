using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLCodigoPostal
    {
        public List<BECodigoPostal> Listar_Page_CodigoPostal(decimal parametro, int st, int pagina, int cantRegxPag)
        {
            return new DACodigoPostal().Listar_Page_Codigo_Postal(parametro, st, pagina, cantRegxPag);
        }

        public List<BECodigoPostal> Obtener(decimal parametro)
        {
            return new DACodigoPostal().Obtener(parametro);
        }

        public int Insertar(BECodigoPostal ins)
        {
            return new DACodigoPostal().Insertar(ins);
        }

        public int Actualizar(BECodigoPostal upd)
        {
            return new DACodigoPostal().Actualizar(upd);
        }

        public int Eliminar(BECodigoPostal del)
        {
            return new DACodigoPostal().Eliminar(del);
        }
    }
}

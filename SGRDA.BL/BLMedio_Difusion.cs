using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLMedio_Difusion
    {
        public List<BEMedioDifusion> Listar_Page_MedioDifusion(string parametro, int st, int pagina, int cantRegxPag)
        {
            return new DAMedioDifusion().Listar_Page_Medio_Difusion(parametro, st, pagina, cantRegxPag);
        }

        public List<BEMedioDifusion> Obtener(string owner, decimal id)
        {
            return new DAMedioDifusion().Obtener(owner, id);
        }

        public int Insertar(BEMedioDifusion ins)
        {
            return new DAMedioDifusion().Insertar(ins);
        }

        public int Actualizar(BEMedioDifusion upd)
        {
            return new DAMedioDifusion().Actualizar(upd);
        }

        public int Eliminar(BEMedioDifusion del)
        {
            return new DAMedioDifusion().Eliminar(del);
        }
    }
}

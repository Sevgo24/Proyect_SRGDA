using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA.WorkFlow;
using SGRDA.Entities.WorkFlow;

namespace SGRDA.BL.WorkFlow
{
    public class BL_WORKF_EVENTS
    {
        public List<WORKF_EVENTS> ListarItems(string owner)
        {
            return new DA_WORKF_EVENTS().ListarItems(owner);
        }

        public List<WORKF_EVENTS> usp_Get_EventoPage(string owner, string param, int st, int pagina, int cantRegxPag)
        {
            return new DA_WORKF_EVENTS().usp_Get_EventoPage(owner, param, st, pagina, cantRegxPag);
        }

        public WORKF_EVENTS Obtener(string owner, decimal IdEvento)
        {
            return new DA_WORKF_EVENTS().Obtener(owner, IdEvento);
        }

        public int Eliminar(WORKF_EVENTS en)
        {
            return new DA_WORKF_EVENTS().Eliminar(en);
        }

        public int Insertar(WORKF_EVENTS en)
        {
            return new DA_WORKF_EVENTS().Insertar(en);
        }

        public int Actualizar(WORKF_EVENTS en)
        {
            return new DA_WORKF_EVENTS().Actualizar(en);
        }

        public int ValidarDescripcion(string owner, string Descripcion)
        {
            return new DA_WORKF_EVENTS().ValidarDescripcion(owner, Descripcion);
        }
    }
}

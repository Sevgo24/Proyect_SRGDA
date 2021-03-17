using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLTipoObservacion
    {
        public List<BEObservationType> Listar_Page_TipoObservacion(string param, int st, int pagina, int cantRegxPag)
        {
            return new DATipoObservacion().Listar_Page_TipoObservacion(param, st, pagina, cantRegxPag);
        }

        public List<BEREC_OBSERVATION_TYPE> usp_ListarTipoObservacion(string owner)
        {
            return new DATipoObservacion().Get_TipoObservacion(owner);
        }

        public BEREC_OBSERVATION_TYPE  Obtener(string owner,int id)
        {
            return new DATipoObservacion().Obtener(owner, id);
        }

        public List<BEObservationType> Obtener_Observacion(string owner, decimal id)
        {
            return new DATipoObservacion().Obtener_Observacion(owner, id);
        }

        public List<BEREC_OBSERVATION_TYPE> ListarTipoObservacion(string owner)
        {
            return new DATipoObservacion().ListarTipoObservacion(owner);
        }

        public bool existeTipoObservacion(string Owner, string nombre)
        {
            return new DATipoObservacion().existeTipoObservacion(Owner, nombre);
        }

        public bool existeTipoObservacion(string Owner, decimal id, string nombre)
        {
            return new DATipoObservacion().existeTipoObservacion(Owner, id, nombre);
        }

        public int Insertar(BEObservationType ins)
        {
            return new DATipoObservacion().Insertar(ins);
        }

        public int Actualizar(BEObservationType upd)
        {
            return new DATipoObservacion().Actualizar(upd);
        }

        public int Eliminar(BEObservationType del)
        {
            return new DATipoObservacion().Eliminar(del);
        }
    }
}

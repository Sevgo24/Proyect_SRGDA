using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.DA.WorkFlow;
using SGRDA.Entities;
using SGRDA.Entities.WorkFlow;

namespace SGRDA.BL.WorkFlow
{
    public class BL_WORKF_TRANSITIONS
    {
        public WORKF_TRANSITIONS ObtenerCicloTransitions(string owner, decimal tidWrkf)
        {
            return new DA_WORKF_TRANSITIONS().ObtenerCicloTransitions(owner, tidWrkf);
        }

        public WORKF_TRANSITIONS ObtenerTransitions(string owner, decimal tidWrkf)
        {
            return new DA_WORKF_TRANSITIONS().ObtenerTransitions(owner, tidWrkf);
        }
        public WORKF_TRANSITIONS ObtenerTransitionsXActionMapping(string owner, decimal amidWrkf)
        {
            return new DA_WORKF_TRANSITIONS().ObtenerTransitionsXActionMapping(owner, amidWrkf);
        }
        public List<WORKF_TRANSITIONS> Listar(string owner, decimal idCiclo, decimal idEvento, decimal idEstadoIni, decimal idEstadoFin, int estado, int pagina, int cantRegxPag)
        {
            return new DA_WORKF_TRANSITIONS().Listar(owner,  idCiclo,  idEvento,  idEstadoIni,  idEstadoFin,  estado,  pagina,  cantRegxPag);
        }
        public decimal Insertar(WORKF_TRANSITIONS entidad)
        {
            return new DA_WORKF_TRANSITIONS().Insertar(entidad);
        }
        public decimal Actualizar(WORKF_TRANSITIONS entidad)
        {
            return new DA_WORKF_TRANSITIONS().Actualizar(entidad);
        }
        public decimal Eliminar(WORKF_TRANSITIONS entidad)
        {
            return new DA_WORKF_TRANSITIONS().Eliminar(entidad);
        }
    }
}

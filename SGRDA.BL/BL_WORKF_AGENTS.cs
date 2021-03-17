using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA.WorkFlow;
using SGRDA.Entities.WorkFlow;

namespace SGRDA.BL.WorkFlow
{
    public class BL_WORKF_AGENTS
    {

        public List<WORKF_AGENTS> Listar(string owner, string nombre, string etiqueta, int estado, int pagina, int cantRegxPag)
        {
            return new DA_WORKF_AGENTS().Listar(owner, nombre, etiqueta, estado, pagina, cantRegxPag);
        }
        public WORKF_AGENTS Obtener(string owner, decimal id)
        {
            return new DA_WORKF_AGENTS().Obtener(owner, id);
        }
        public decimal Eliminar(WORKF_AGENTS entidad)
        {
            return new DA_WORKF_AGENTS().Eliminar(entidad);
        }
        public decimal Actualizar(WORKF_AGENTS entidad)
        {
            return new DA_WORKF_AGENTS().Actualizar(entidad);
        }
        public decimal Insertar(WORKF_AGENTS entidad)
        {
            return new DA_WORKF_AGENTS().Insertar(entidad);
        }
        public List<WORKF_AGENTS> ListarReporte(WORKF_AGENTS agente)
        {
            return new DA_WORKF_AGENTS().ListarReporte(agente);
        }
        public bool  TieneRol(string owner, decimal idRol,decimal idAccion)
        {
            return new DA_WORKF_AGENTS().TieneRol(owner, idRol, idAccion);
        }

    }
}

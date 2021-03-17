using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
using SGRDA.Entities.WorkFlow;
using SGRDA.DA.WorkFlow;

namespace SGRDA.BL.WorkFlow
{
    public class BL_WORKF_ACTIONS_MAPPINGS
    {
        public int Insertar(WORKF_ACTIONS_MAPPINGS en)
        {
            var orden = new DA_WORKF_ACTIONS_MAPPINGS().ObtenerOrden(en);
            en.WRKF_AORDER = orden.ToString();
            return new DA_WORKF_ACTIONS_MAPPINGS().Insertar(en);
        }

        public int ObtenerOrden(WORKF_ACTIONS_MAPPINGS en)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().ObtenerOrden(en);
        }

        public WORKF_ACTIONS_MAPPINGS Listar(string Owner, decimal Idwrk, decimal Idst)
        {
            WORKF_ACTIONS_MAPPINGS objMapping = new WORKF_ACTIONS_MAPPINGS();
            objMapping.Mapping = new DA_WORKF_ACTIONS_MAPPINGS().Listar(Owner, Idwrk, Idst);            
            return objMapping;
        }

        public int ActualizarOrden(string Owner, decimal? IdMappings, int Orden, string user)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().ActualizarOrden(Owner, IdMappings, Orden, user);
        }

        public int ActualizarPrioridad(WORKF_ACTIONS_MAPPINGS en)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().ActualizarPrioridad(en);
        }

        public int ActualizarObligatorio(WORKF_ACTIONS_MAPPINGS en)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().ActualizarObligatorio(en);
        }

        public int ActualizarPrerrequisito(WORKF_ACTIONS_MAPPINGS en)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().ActualizarPrerrequisito(en);
        }

        public int ActualizarObjeto(WORKF_ACTIONS_MAPPINGS en)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().ActualizarObjeto(en);
        }

        public int EliminarObjeto(WORKF_ACTIONS_MAPPINGS en)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().EliminarObjeto(en);
        }

        public int ActualizarAccion(WORKF_ACTIONS_MAPPINGS en)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().ActualizarAccion(en);
        }

        public int ActualizarGrabacion(WORKF_ACTIONS_MAPPINGS en)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().ActualizarGrabacion(en);
        }

        public int ActualizarVisible(WORKF_ACTIONS_MAPPINGS en)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().ActualizarVisible(en);
        }

        public int ActualizarEvento(WORKF_ACTIONS_MAPPINGS en)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().ActualizarEvento(en);
        }

        public int ActualizarTransicion(WORKF_ACTIONS_MAPPINGS en)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().ActualizarTransicion(en);
        }

        public int ActualizarNext(WORKF_ACTIONS_MAPPINGS en)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().ActualizarNext(en);
        }

        public List<WORKF_EVENTS> ListarEvento(string Owner)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().ListarEvento(Owner);
        }

        //public List<WORKF_ACTIONS_MAPPINGS> ObtenerOrdenBajar(string Owner, decimal? IdTransicion, int Orden, decimal workflow, decimal estado)
        public List<WORKF_ACTIONS_MAPPINGS> ObtenerOrdenBajar(string Owner, int Orden, decimal workflow, decimal estado)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().ObtenerOrdenBajar(Owner, Orden, workflow, estado);
        }

        //public List<WORKF_ACTIONS_MAPPINGS> ObtenerOrdenSubir(string Owner, decimal? IdTransicion, int Orden, decimal workflow, decimal estado)
        public List<WORKF_ACTIONS_MAPPINGS> ObtenerOrdenSubir(string Owner, int Orden, decimal workflow, decimal estado)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().ObtenerOrdenSubir(Owner, Orden, workflow, estado);
        }

        public int Eliminar(WORKF_ACTIONS_MAPPINGS en)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().Eliminar(en);
        }

        public List<WORKF_ACTIONS_MAPPINGS> ObtenerOrdenActualizar(WORKF_ACTIONS_MAPPINGS en)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().ObtenerOrdenActualizar(en);
        }

        public int ActualizarOrdenEliminar(WORKF_ACTIONS_MAPPINGS en)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().ActualizarOrdenEliminar(en);
        }

        public List<WORKF_ACTIONS_MAPPINGS> ListarPrerrequisito(string Owner, decimal Idwrk, decimal Idst, decimal? wrkfaId)
        {
            return new DA_WORKF_ACTIONS_MAPPINGS().ListarPrerrequisito(Owner, Idwrk, Idst, wrkfaId);
        }
    }
}

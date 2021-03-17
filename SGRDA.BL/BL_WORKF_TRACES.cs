using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.DA.WorkFlow;
using SGRDA.Entities;
using SGRDA.Entities.WorkFlow;
using System.Transactions;

namespace SGRDA.BL.WorkFlow
{
    public class BL_WORKF_TRACES
    {
        public List<WORKF_TRACES> ListarTraces(string owner, decimal wrkf_tid)
        {
            return new DA_WORKF_TRACES().ListarTraces(owner, wrkf_tid);
        }
        public WORKF_TRACES ObtenerTraces(string owner, decimal wrkf_tid)
        {
            return new DA_WORKF_TRACES().ObtenerTraces(owner, wrkf_tid);
        }
        public decimal ActualizarTrace(WORKF_TRACES entidad)
        {
            return new DA_WORKF_TRACES().ActualizarTrace(entidad);
        }

        /// <summary>
        /// Inserta seguimiento y actualiza el estado de Licencia
        /// RETORNA:  1=OK, -999=No Cumple Prerequisito
        /// </summary>
        /// <param name="entidad"></param>
        /// <param name="amidWrkf"></param>
        /// <returns></returns>
        public decimal InsertarTraceLic(WORKF_TRACES entidad, decimal amidWrkf,out decimal idTrace)
        {
            DA_WORKF_TRACES servicio = new DA_WORKF_TRACES();
           // DALicencias licServ = new DALicencias();
            DA_WORKF_ACTIONS serviceAction = new DA_WORKF_ACTIONS();

            var estado = 0;
            idTrace = 0;
         //   int resultCambioEstado = 0;
           bool cumpleRequisito= serviceAction.CumplePreRequisito(entidad.OWNER, Convert.ToDecimal(entidad.WRKF_REF1), amidWrkf);
           if (cumpleRequisito)
           {
               using (TransactionScope transa = new TransactionScope())
               {
                   idTrace = servicio.InsertarTrace(entidad);
                   estado = serviceAction.CambiarEstado(entidad.PROC_MOD.Value, Convert.ToDecimal(entidad.WRKF_REF1), entidad.OWNER, entidad.WRKF_ID.Value, entidad.WRKF_SID, amidWrkf);
                   if (estado != -1)
                   {
                       transa.Complete();
                   }
                   else {
                       estado = -997;
                   }
  
               }
           }
           else {
               estado = -999;
           }

           //if (estado == entidad.WRKF_SID) { estado = -998; }
            return estado;
        }
        public List<BETracesLog> LogTraces(string owner, decimal wrkf_ref1) {
            return new DA_WORKF_TRACES().LogTraces(owner, wrkf_ref1);
        }
    }
}

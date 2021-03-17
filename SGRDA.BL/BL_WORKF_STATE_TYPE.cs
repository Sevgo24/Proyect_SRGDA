using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA.WorkFlow;
using SGRDA.Entities.WorkFlow;

namespace SGRDA.BL.WorkFlow
{
    public class BL_WORKF_STATE_TYPE
    {
        public List<WORKF_STATE_TYPE> ListarItemTiposEstados(string owner, decimal IdCicloAprob)
        {
            return new DA_WORKF_STATE_TYPE().ListarItemTiposEstados(owner, IdCicloAprob);
        }

        public List<WORKF_STATE_TYPE> ListarTiposEstados(string owner)
        {
            return new DA_WORKF_STATE_TYPE().ListarTiposEstados(owner);
        }
    }
}

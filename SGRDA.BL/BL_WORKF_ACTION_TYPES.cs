using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA.WorkFlow;
using SGRDA.Entities.WorkFlow;

namespace SGRDA.BL.WorkFlow
{
    public class BL_WORKF_ACTION_TYPES
    {
        public List<WORKF_ACTION_TYPES> ListarItem(string owner)
        {
            return new DA_WORKF_ACTION_TYPES().ListarItem(owner);
        }
    }
}

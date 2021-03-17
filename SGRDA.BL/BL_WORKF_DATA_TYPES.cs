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
    public class BL_WORKF_DATA_TYPES
    {
        public List<WORKF_DATA_TYPES> ListarDataTypes(string owner, decimal wrkf_dtid)
        {
            return new DA_WORKF_DATA_TYPES().ListarDataTypes(owner, wrkf_dtid);
        }
        public WORKF_DATA_TYPES ObtenerDataTypes(string owner, decimal wrkf_dtid)
        {
            return new DA_WORKF_DATA_TYPES().ObtenerDataTypes(owner, wrkf_dtid);
        }
        public List<WORKF_DATA_TYPES> ListarItem(string owner)
        {
            return new DA_WORKF_DATA_TYPES().ListarItem(owner);
        }
    }
}

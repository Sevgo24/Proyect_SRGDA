using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA.WorkFlow;
using SGRDA.Entities.WorkFlow;

namespace SGRDA.BL.WorkFlow
{
    public class BL_WORKF_OBJECTS_TYPE
    {
        public List<WORKF_OBJECTS_TYPE> ListarTipoObjeto(string owner)
        {
            return new DA_WORKF_OBJECTS_TYPE().ListarTipoObjeto(owner);
        }

        public WORKF_OBJECTS_TYPE SeleccionarTipo(string owner, decimal? tipo)
        {
            var obj = new DA_WORKF_OBJECTS_TYPE().obtener(owner, tipo);
            return obj;
        }
    }
}

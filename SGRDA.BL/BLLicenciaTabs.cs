using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLLicenciaTabs
    {
        public List<BELicenciaTabs> ListarLicenciaTab(string owner)
        {
            return new DALicenciaTabs().ListarLicenciaTab(owner);
        }

        public BELicenciaTabs ObtenerNombre(string Owner, decimal Id)
        {
            return new DALicenciaTabs().ObtenerNombre(Owner, Id);
        }
    }
}

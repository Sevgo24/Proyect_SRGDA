using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLLicenciaImpuesto
    {
        public List<BEImpuestoValor> ListaImpuesto(string Owner, decimal IdEstablecimiento)
        {
            return new DALicenciaImpuesto().ListaImpuesto(Owner, IdEstablecimiento);
        }
    }
}

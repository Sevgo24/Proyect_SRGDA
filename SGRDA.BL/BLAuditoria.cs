using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLAuditoria
    {
        public List<BEAuditoria> ListaAuditoria(string Owner, decimal IdLicencia)
        {
            return new DAAuditoria().ListaAuditoria(Owner, IdLicencia);
        }
    }
}

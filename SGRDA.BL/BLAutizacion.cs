using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLAutizacion
    {
        public List<BEAutorizacion> ListarXLic(decimal idLic,string owner)
        {
            return new DAAutorizacion().AutorizacionXLicencia(idLic, owner);
        }
    }
}

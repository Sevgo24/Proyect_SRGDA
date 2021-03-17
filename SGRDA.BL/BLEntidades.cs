using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLEntidades
    {
        public List<BEEntidades> ListaDropEntidades(string owner)
        {
            return new DAEntidades().ListaDropEntidades(owner);
        }
    }
}

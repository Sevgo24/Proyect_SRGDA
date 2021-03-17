using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLRutas
    {
        public List<BERutas> Listar_Rutas(string owner, string rou_tsel)
        {
            return new DARutas().Get_RUTAS(owner, rou_tsel);
        }
    }
}

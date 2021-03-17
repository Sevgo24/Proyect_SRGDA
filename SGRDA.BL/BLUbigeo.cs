using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLUbigeo
    {
        public List<BEUbigeo> Listar_Ubigeo(decimal codigo, string value)
        {
            return new DAUbigeo().Listar_Ubigeo(codigo, value);
        }

        public BEUbigeo ObtenerDescripcion(decimal idTerritorio, decimal idUbigeo)
        {
            return new DAUbigeo().ObtenerDescripcion(idTerritorio, idUbigeo);
        }
    }
}

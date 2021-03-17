using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLModuloSistema
    {
        public List<BEModulo> Listar_Page_Modulo_Sistema(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAModuloSistema().Listar_Page_Modulo_Sistema(param, st, pagina, cantRegxPag);
        }

        public int Insertar(BEModulo ins)
        {
            return new DAModuloSistema().Insertar(ins);
        }

        public int Actualizar(BEModulo upd)
        {
            return new DAModuloSistema().Actualizar(upd);
        }

        public int Eliminar(BEModulo del)
        {
            return new DAModuloSistema().Eliminar(del);
        }

        public List<BEModulo> Obtener(string owner, decimal id)
        {
            return new DAModuloSistema().Obtener(owner, id);
        }

        public List<BEModulo> ListarModulo(string owner)
        {
            return new DAModuloSistema().ListarModulo(owner);
        }
    }
}

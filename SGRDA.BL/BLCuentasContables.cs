using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;
namespace SGRDA.BL
{
    public class BLCuentasContables
    {

        public List<BECuentaContable> Listar_Page_Cuentas_Contables(string desc, string cuenta, int st, int pagina, int cantRegxPag)
        {
            return new DACuentaContable().Listar_Page_Cuentas_Contables(desc, cuenta, st, pagina, cantRegxPag);
        }

        public List<BECuentaContable> Obtener(string owner, decimal id)
        {
            return new DACuentaContable().Obtener(owner, id);
        }

        public int Insertar(BECuentaContable ins)
        {
            return new DACuentaContable().Insertar(ins);
        }

        public int Actualizar(BECuentaContable upd)
        {
            return new DACuentaContable().Actualizar(upd);
        }

        public int Eliminar(BECuentaContable del)
        {
            return new DACuentaContable().Eliminar(del);
        }

        public List<BECuentaContable> ListarCombo(string owner)
        {
            return new DACuentaContable().ListarCombo(owner);
        }
    }
}

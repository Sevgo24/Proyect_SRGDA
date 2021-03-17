using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLFormatoFactura
    {
        public List<BEFormatoFactura> Listar_Page_Formato_Factura(string parametro, int st, int pagina, int cantRegxPag)
        {
            return new DAFormatoFactura().Listar_Page_Formato_Factura(parametro, st, pagina, cantRegxPag);
        }

        public List<BEFormatoFactura> Obtener(string owner, decimal id)
        {
            return new DAFormatoFactura().Obtener(owner, id);
        }

        public int Insertar(BEFormatoFactura ins)
        {
            return new DAFormatoFactura().Insertar(ins);
        }

        public int Actualizar(BEFormatoFactura upd)
        {
            return new DAFormatoFactura().Actualizar(upd);
        }

        public int Eliminar(BEFormatoFactura del)
        {
            return new DAFormatoFactura().Eliminar(del);
        }

        public List<BEFormatoFactura> ListarCombo(string owner)
        {
            return new DAFormatoFactura().ListarFormatoFactura(owner);
        }

        public List<BEFormatoFactura> FormatoFacturacion(string owner)
        {
            return new DAFormatoFactura().FormatoFacturacion(owner);
        }
    }
}

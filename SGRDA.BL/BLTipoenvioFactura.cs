using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
using System.Transactions;

namespace SGRDA.BL
{
    public class BLTipoenvioFactura
    {
        public List<BETipoenvioFactura> ListarPaginacion(string owner, string param, int st, int pagina, int cantRegxPag)
        {
            return new DATipoenvioFactura().ListarPaginacion(owner, param, st, pagina, cantRegxPag);
        }

        public int Eliminar(BETipoenvioFactura TipoenvioFactura)
        {
            return new DATipoenvioFactura().Eliminar(TipoenvioFactura);
        }

        public int Insertar(BETipoenvioFactura TipoenvioFactura)
        {
            return new DATipoenvioFactura().Insertar(TipoenvioFactura);
        }

        public BETipoenvioFactura Obtener(string owner, decimal id)
        {
            return new DATipoenvioFactura().Obtener(owner, id);
        }

        public int Actualizar(BETipoenvioFactura TipoenvioFactura)
        {
            return new DATipoenvioFactura().Actualizar(TipoenvioFactura);
        }

        public int ObtenerXDescripcion(BETipoenvioFactura TipoenvioFactura)
        {
            return new DATipoenvioFactura().ObtenerXDescripcion(TipoenvioFactura);
        }
        public  List<BETipoenvioFactura> Listar(string owner)
        {
            return new DATipoenvioFactura().Listar(owner);
        }
    }
}

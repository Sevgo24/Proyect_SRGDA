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
    public class BLModuloCliente
    {
        
        public BEModuloCliente Obtener(string owner, decimal id)
        {
            return new DAModuloCliente().Obtener(owner, id);
        }

        public int Actualizar(BEModuloCliente cliente)
        {
            return new DAModuloCliente().Actualizar(cliente);
        }

        public int Insertar(BEModuloCliente cliente)
        {
            return new DAModuloCliente().Insertar(cliente);
        }

        public int Eliminar(BEModuloCliente cliente)
        {
            return new DAModuloCliente().Eliminar(cliente);
        }

        public List<BEModuloCliente> Listar(string owner, string desc, string label, int estado, int pagina, int cantRegxPag)
        {
            return new DAModuloCliente().Listar( owner,  desc,  label,  estado,  pagina,  cantRegxPag);
        }

        public List<BEModuloCliente> ListarNombre(string owner)
        {
            return new DAModuloCliente().ListarNombre(owner);
        }
    }
}

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
    public class BLAgente
    {

        public List<BEAgente> ListarAgentes(string owner, string param, int st, int pagina, int cantRegxPag)
        {
            return new DAAgente().ListarAgentes(owner, param, st, pagina, cantRegxPag);
        }

        public List<BETreeview> ListarArbol(string owner)
        {
            return new DAAgente().ListarArbol(owner);
        }

        public int Eliminar(BEAgente agente)
        {
            return new DAAgente().Eliminar(agente);
        }

        public List<BEAgente> ListarAgenteDependencia(string owner)
        {
            return new DAAgente().ListarAgenteDependencia(owner);
        }

        public int Insertar(BEAgente agente)
        {
            return new DAAgente().Insertar(agente);
        }

        public List<BEAgente> ObtenerXDescripcion(BEAgente agente)
        {
            return new DAAgente().ObtenerXDescripcion(agente);
        }

        public BEAgente Obtener(string owner, decimal id)
        {
            return new DAAgente().Obtener(owner, id);
        }

        public int Actualizar(BEAgente agente)
        {
            return new DAAgente().Actualizar(agente);
        }

        public List<BEAgente> ListarCombo(string owner)
        {
            return new DAAgente().ListarCombo(owner);
        }

        public List<BEDivisionRecaudador> ObtenerDivXOficina_Deplegable(decimal IdOficina, string owner)
        {
            return new DAOficinaDivision().ObtenerDivXOficina_Deplegable(IdOficina, owner);
        }
    }
}

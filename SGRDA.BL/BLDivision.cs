using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLDivision
    {
        public List<BEDivision> Listar()
        {
            return new DADivisiones().Listar();
        }

        public List<BEDivision> ListarPorCodigo(decimal dcodigo)
        {
            return new DADivisiones().ListarPorCodigo(dcodigo);
        }

        public List<BEDivision> usp_Get_DivisionPage(string param, int pagina, int cantRegxPag)
        {
            return new DADivisiones().usp_Get_DivisionPage(param, pagina, cantRegxPag);
        }

        public List<BEDivision> ListarPorSubtipo(decimal dSubTipoDivision)
        {
            return new DADivisiones().ListarPorSubtipo(dSubTipoDivision);
        }

        public int Insertar(BEDivision en)
        {
            return new DADivisiones().Insertar(en);
        }

        public int Update(BEDivision en)
        {
            return new DADivisiones().Update(en);
        }

        public int Eliminar(decimal dCodigo)
        {
            return new DADivisiones().Eliminar(dCodigo);
        }
        public int ValidarUbigeoXOficina(string owner, int oficina, int ubigeo)
        {
            return new DADivisiones().ValidarUbigeoXOficina(owner, oficina, ubigeo);
        }
        public int ValidarUbigeoXEstablecimiento(string owner, int est_id, int oficina)
        {
            int result = 0;
            int ubigeo = new DADivisiones().ObtenerUbigeoXEstablecimiento(owner, est_id);
            if (ubigeo >= 0)
                result = new DADivisiones().ValidarUbigeoXOficina(GlobalVars.Global.OWNER, oficina, ubigeo);
            return result;
        }

        public List<BEREF_DIV_SUBTYPE> ListarSubTipoDivisiones(decimal idDivision)
        {
            return new DADivisiones().ListarSubTipoDivisiones(idDivision);
        }

        public List<BEDivision> ListarValoresXsubtipo_Division(decimal idDivision, decimal idSubTipo, decimal idBelong)
        {
            return new DADivisiones().ListarValoresXsubtipo_Division(idDivision, idSubTipo, idBelong);
        }

    }
}

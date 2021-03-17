using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREF_DIVISIONES_VALUES
    {
        //prueba
        public List<BEREF_DIVISIONES_VALUES> usp_ListarUbigeo(decimal value, string text)
        {
            return new DAREF_DIVISIONES_VALUES().USP_BUSCAR_UBIGEO(value, text);
        }



        public List<BEREF_DIVISIONES_VALUES> Listar(string owner, decimal id, decimal subId, decimal depId,string nombre, int pagina, int cantRegxPag)
        {
            return new DAREF_DIVISIONES_VALUES().Listar(owner, id,subId, depId,nombre, pagina, cantRegxPag);
        }
         
        public int insertar(BEREF_DIVISIONES_VALUES values)
        {
            return new DAREF_DIVISIONES_VALUES().insertar(values);
        }

        public List<BEREF_DIVISIONES_VALUES> ListarValoresDep(string owner,decimal id,decimal subdivision)
        {
            return new DAREF_DIVISIONES_VALUES().ListarValoresDep(owner, id, subdivision);
        }

        public int Eliminar(BEREF_DIVISIONES_VALUES valores)
        {
            return new DAREF_DIVISIONES_VALUES().Eliminar(valores);
        }

        public int ObtenerXAbrev(BEREF_DIVISIONES_VALUES valores)
        {
            return new DAREF_DIVISIONES_VALUES().ObtenerXAbrev(valores);
        }

        public List<BETreeview> ArbolValoresReporte(string owner, decimal id)
        {
            return new DAREF_DIVISIONES_VALUES().ArbolValoresReporte(owner,id);
        }

        public List<BEREF_DIVISIONES_VALUES> ListarDivisionesValor(decimal id)
        {
            return new DAREF_DIVISIONES_VALUES().ListarDivisionesValor(id);
        }

        public List<BEREF_DIVISIONES_VALUES> ListarValoresXSubdivision(string owner, decimal id, decimal subdivision)
        {
            return new DAREF_DIVISIONES_VALUES().ListarValoresXSubdivision(owner, id, subdivision);
        }

        public BEREF_DIVISIONES_VALUES ObtenerValor(string owner, decimal id)
        {
            return new DAREF_DIVISIONES_VALUES().ObtenerValor(owner, id);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_ECON_ACTIVITIES
    {
        public List<BEREC_ECON_ACTIVITIES> Get_REC_ECON_ACTIVITIES()
        {
            return new DAREC_ECON_ACTIVITIES().Get_REC_ECON_ACTIVITIES();
        }

        public List<BEREC_ECON_ACTIVITIES> REC_ECON_ACTIVITIES_GET_by_ECON_ID(string ECON_ID)
        {
            return new DAREC_ECON_ACTIVITIES().REC_ECON_ACTIVITIES_GET_by_ECON_ID(ECON_ID);
        }

        public List<BEREC_ECON_ACTIVITIES> REC_ECON_ACTIVITIES_Page(string owner, string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_ECON_ACTIVITIES().REC_ECON_ACTIVITIES_Page(owner, param, st, pagina, cantRegxPag);
        }

        public bool REC_ECON_ACTIVITIES_Ins(BEREC_ECON_ACTIVITIES en)
        {
            var lista = new DAREC_ECON_ACTIVITIES().REC_ECON_ACTIVITIES_GET_by_ECON_ID(en.ECON_ID);
            if (lista.Count == 0)
                return new DAREC_ECON_ACTIVITIES().REC_ECON_ACTIVITIES_Ins(en);
            else
                return false;
        }

        public bool REC_ECON_ACTIVITIES_Upd(BEREC_ECON_ACTIVITIES en)
        {
            return new DAREC_ECON_ACTIVITIES().REC_ECON_ACTIVITIES_Upd(en);
        }

        public bool REC_ECON_ACTIVITIES_Del(string ECON_ID)
        {
            return new DAREC_ECON_ACTIVITIES().REC_ECON_ACTIVITIES_Del(ECON_ID);
        }

        public List<BEREC_ECON_ACTIVITIES> ListarActividadEcon(string owner)
        {
            return new DAREC_ECON_ACTIVITIES().ListarActividadEcon(owner);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_EST_TYPE
    {
        public List<BEREC_EST_TYPE> REC_EST_TYPE_GET()
        {
            return new DAREC_EST_TYPE().REC_EST_TYPE_GET();
        }

        public List<BEREC_EST_TYPE> usp_REC_EST_TYPE_Page(string param, string tipo, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_EST_TYPE().usp_REC_EST_TYPE_Page(param, tipo, st, pagina, cantRegxPag);
        }

        public bool REC_EST_TYPE_Ins(BEREC_EST_TYPE en)
        {
            return new DAREC_EST_TYPE().REC_EST_TYPE_Ins(en);
        }

        public bool existeTipoEstablecimiento(string Owner, string id, string desc)
        {
            return new DAREC_EST_TYPE().existeTipoEstablecimiento(Owner, id, desc);
        }

        public bool existeTipoEstablecimiento(string Owner, string idEco, string desc, decimal Id)
        {
            return new DAREC_EST_TYPE().existeTipoEstablecimiento(Owner, idEco, desc, Id);
        }

        public bool REC_EST_TYPE_Upd(BEREC_EST_TYPE en)
        {
            return new DAREC_EST_TYPE().REC_EST_TYPE_Upd(en);
        }

        public List<BEREC_EST_TYPE> REC_EST_TYPE_GET_by_ESTT_ID(decimal ESTT_ID)
        {
            return new DAREC_EST_TYPE().REC_EST_TYPE_GET_by_ESTT_ID(ESTT_ID);
        }

        public bool REC_EST_TYPE_Del(decimal ESTT_ID)
        {
            return new DAREC_EST_TYPE().REC_EST_TYPE_Del(ESTT_ID);
        }
    }
}

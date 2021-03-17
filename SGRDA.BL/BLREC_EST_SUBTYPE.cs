using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_EST_SUBTYPE
    {
        public List<BEREC_EST_SUBTYPE> REC_EST_SUBTYPE_GET()
        {
            return new DAREC_EST_SUBTYPE().REC_EST_SUBTYPE_GET();
        }

        public List<BEREC_EST_SUBTYPE> ListarSubtipoEstablecimientoPorTipo(string Owner, decimal? IdTipo)
        {
            return new DAREC_EST_SUBTYPE().ListarSubtipoEstablecimientoPorTipo(Owner, IdTipo); 
        }

        public List<BEREC_EST_SUBTYPE> usp_REC_EST_SUBTYPE_Page(string owner, string param, decimal TipoEst, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_EST_SUBTYPE().usp_REC_EST_SUBTYPE_Page(owner, param, TipoEst, st, pagina, cantRegxPag);
        }

        public bool existeSubTipoEstablecimiento(string Owner, decimal id, string desc)
        {
            return new DAREC_EST_SUBTYPE().existeSubTipoEstablecimiento(Owner, id, desc);
        }

        public bool existeSubTipoEstablecimiento(string Owner, decimal idTipo, string desc, decimal Id)
        {
            return new DAREC_EST_SUBTYPE().existeSubTipoEstablecimiento(Owner, idTipo, desc, Id);
        }

        public bool REC_EST_SUBTYPE_Ins(BEREC_EST_SUBTYPE en)
        {
            return new DAREC_EST_SUBTYPE().REC_EST_SUBTYPE_Ins(en);
        }

        public bool REC_EST_SUBTYPE_Upd(BEREC_EST_SUBTYPE en)
        {
            return new DAREC_EST_SUBTYPE().REC_EST_SUBTYPE_Upd(en);
        }

        public List<BEREC_EST_SUBTYPE> REC_EST_SUBTYPE_by_SUBE_ID(decimal SUBE_ID)
        {
            return new DAREC_EST_SUBTYPE().REC_EST_SUBTYPE_by_SUBE_ID(SUBE_ID);
        }

        public bool REC_EST_SUBTYPE_Del(decimal SUBE_ID)
        {
            return new DAREC_EST_SUBTYPE().REC_EST_SUBTYPE_Del(SUBE_ID);
        }
    }
}

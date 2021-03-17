using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_TAX_ID
    {
        public List<BEREC_TAX_ID> Get_REC_TAX_ID()
        {
            return new DAREC_TAX_ID().Get_REC_TAX_ID();
        }

        public List<BEREC_TAX_ID> REC_TAX_ID_GET_by_TAXT_ID(decimal TAXT_ID)
        {
            return new DAREC_TAX_ID().REC_TAX_ID_GET_by_TAXT_ID(TAXT_ID);
        }

        public List<BEREC_TAX_ID> REC_TAX_ID_Page(string param, string owner, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_TAX_ID().REC_TAX_ID_Page(param, owner, st, pagina, cantRegxPag);
        }

        public bool REC_TAX_ID_Ins(BEREC_TAX_ID en)
        {
            return new DAREC_TAX_ID().REC_TAX_ID_Ins(en);
        }

        public bool REC_TAX_ID_Upd(BEREC_TAX_ID en)
        {
            return new DAREC_TAX_ID().REC_TAX_ID_Upd(en);
        }

        public bool REC_TAX_ID_Del(decimal TAXT_ID)
        {
            return new DAREC_TAX_ID().REC_TAX_ID_Del(TAXT_ID);
        }

        /// <summary>
        ///   addon dbs 20140727 - OBTIENE UN ITEM DE LA TABLA TIPO DE DOCUMENTO DE IDENTIFICACION (DNI RUC, ENTRE OTROS)
        /// </summary>
        /// <param name="Owner"></param>
        /// <param name="idTipo"></param>
        /// <returns></returns>
        public BEREC_TAX_ID Obtener(string Owner, decimal idTipo)
        {
            return new DAREC_TAX_ID().Obtener(Owner,idTipo);
        }
    }
}

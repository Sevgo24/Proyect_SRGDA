using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_PAYMENT_TYPE
    {
        public List<BEREC_PAYMENT_TYPE> Get_REC_PAYMENT_TYPE()
        {
            return new DAREC_PAYMENT_TYPE().Get_REC_PAYMENT_TYPE();
        }

        public List<BEREC_PAYMENT_TYPE> REC_PAYMENT_TYPE_by_PAY_ID(string PAY_ID)
        {
            return new DAREC_PAYMENT_TYPE().REC_PAYMENT_TYPE_by_PAY_ID(PAY_ID);
        }

        public List<BEREC_PAYMENT_TYPE> REC_PAYMENT_TYPE_Page(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_PAYMENT_TYPE().REC_PAYMENT_TYPE_Page(param, st, pagina, cantRegxPag);
        }

        public int ValidacionFormaPago(string owner, string id, string descripcion)
        {
            return new DAREC_PAYMENT_TYPE().ValidacionFormaPago(owner, id, descripcion);
        }

        public bool REC_PAYMENT_TYPE_Ins(BEREC_PAYMENT_TYPE en)
        {
            return new DAREC_PAYMENT_TYPE().REC_PAYMENT_TYPE_Ins(en);
        }

        public bool REC_PAYMENT_TYPE_Upd(BEREC_PAYMENT_TYPE en)
        {
            return new DAREC_PAYMENT_TYPE().REC_PAYMENT_TYPE_Upd(en);
        }

        public bool REC_PAYMENT_TYPE_Del(string PAY_ID)
        {
            return new DAREC_PAYMENT_TYPE().REC_PAYMENT_TYPE_Del(PAY_ID);
        }

        public List<BEREC_PAYMENT_TYPE> ListarTipo(string owner)
        {
            return new DAREC_PAYMENT_TYPE().ListarTipo(owner);
        }

        public int Eliminar(BEREC_PAYMENT_TYPE del)
        {
            return new DAREC_PAYMENT_TYPE().Eliminar(del);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;


namespace SGRDA.BL
{
    public class BLREF_DIV_STYPE_CHAR
    {
        public List<BEREF_DIV_STYPE_CHAR> usp_Get_REF_DIV_STYPE_CHAR()
        {
            return new DAREF_DIV_STYPE_CHAR().usp_Get_REF_DIV_STYPE_CHAR();
        }

        public List<BEREF_DIV_STYPE_CHAR> usp_REF_DIV_STYPE_CHAR_GET_by_Patametros(string DAC_ID, string DAD_TYPE, string DAD_STYPE)
        {
            return new DAREF_DIV_STYPE_CHAR().usp_REF_DIV_STYPE_CHAR_GET_by_Patametros(DAC_ID, DAD_TYPE, DAD_STYPE);
        }

        public List<BEREF_DIV_STYPE_CHAR> usp_REF_DIV_STYPE_CHAR_Page(string param, int pagina, int cantRegxPag)
        {
            return new DAREF_DIV_STYPE_CHAR().usp_REF_DIV_STYPE_CHAR_Page(param, pagina, cantRegxPag);
        }

        public bool REF_DIV_STYPE_CHAR_Ins(BEREF_DIV_STYPE_CHAR en)
        {
            //validar duplicados
            List<BEREF_DIV_STYPE_CHAR> lista = new List<BEREF_DIV_STYPE_CHAR>();
            lista = new DAREF_DIV_STYPE_CHAR().usp_REF_DIV_STYPE_CHAR_GET_by_Patametros(en.DAC_ID, en.DAD_TYPE, en.DAD_STYPE);

            if (lista.Count > 0) return false;

            return new DAREF_DIV_STYPE_CHAR().REF_DIV_STYPE_CHAR_Ins(en);
        }

        public bool REF_DIV_STYPE_CHAR_Upd(BEREF_DIV_STYPE_CHAR en, string auxDAC_ID, string auxDAD_TYPE, string auxDAD_STYPE)
        {
            return new DAREF_DIV_STYPE_CHAR().REF_DIV_STYPE_CHAR_Upd(en, auxDAC_ID, auxDAD_TYPE, auxDAD_STYPE);
        }

        public bool REF_DIV_STYPE_CHAR_Del(string DAC_ID, string DAD_TYPE, string DAD_STYPE)
        {
            return new DAREF_DIV_STYPE_CHAR().REF_DIV_STYPE_CHAR_Del(DAC_ID, DAD_TYPE, DAD_STYPE);
        }
    }
}

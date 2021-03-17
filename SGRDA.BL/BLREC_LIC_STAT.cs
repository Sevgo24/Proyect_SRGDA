using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_LIC_STAT
    {
        public List<BEREC_LIC_STAT> GET_REC_LIC_STAT()
        {
            return new DAREC_LIC_STAT().GET_REC_LIC_STAT();
        }
        public BEREC_LIC_STAT GET_REC_LIC_STAT_X_COD(decimal LICS_ID)
        {
            return new DAREC_LIC_STAT().GET_REC_LIC_STAT_X_COD(LICS_ID);
        }

        public List<BEREC_LIC_STAT> EstadoIniPorTipo(decimal tipoLic, string empresa)
        {
            return new DAREC_LIC_STAT().EstadoIniPorTipo(tipoLic, empresa);
        }
        public List<BEREC_LIC_STAT> EstadoFinPorTipo(decimal tipoLic, string empresa)
        {
            return new DAREC_LIC_STAT().EstadoFinPorTipo(tipoLic, empresa);
        }
        public List<BEREC_LIC_STAT> EstadoIntPorTipo(decimal tipoLic, string empresa)
        {
            return new DAREC_LIC_STAT().EstadoIntPorTipo(tipoLic, empresa);
        }
    }
}

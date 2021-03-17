using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BETerritorio
    {
        public List<BETerritorio> Division { get; set; }
        public BETerritorio() { }

        public BETerritorio(IDataReader Reader)
        {
            TIS_N = Convert.ToDecimal(Reader["TIS_N"]);
            COD_TIS_ALPHA = Convert.ToString(Reader["COD_TIS_ALPHA"]);
            ISO_LANG = Convert.ToString(Reader["ISO_LANG"]);
            NAME_TER = Convert.ToString(Reader["NAME_TER"]);
            ABBREV_NAME_TER = Convert.ToString(Reader["ABBREV_NAME_TER"]);
            OFFI_NAME_TER = Convert.ToString(Reader["OFFI_NAME_TER"]);
            UNOFFI_NAME_TER = Convert.ToString(Reader["UNOFFI_NAME_TER"]);

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_UPDATE"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_UPDATE"]);
            }


            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDATE"]);
        }

        public BETerritorio(IDataReader Reader, int flag)
        {
            TIS_N = Convert.ToDecimal(Reader["TIS_N"]);
            COD_TIS_ALPHA = Convert.ToString(Reader["COD_TIS_ALPHA"]);
            ISO_LANG = Convert.ToString(Reader["ISO_LANG"]);
            NAME_TER = Convert.ToString(Reader["NAME_TER"]);
            ABBREV_NAME_TER = Convert.ToString(Reader["ABBREV_NAME_TER"]);
            OFFI_NAME_TER = Convert.ToString(Reader["OFFI_NAME_TER"]);
            UNOFFI_NAME_TER = Convert.ToString(Reader["UNOFFI_NAME_TER"]);

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_UPDATE"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_UPDATE"]);
            }


            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDATE"]);

            TotalVirtual = flag;
        }
    }
}

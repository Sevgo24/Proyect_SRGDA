using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_NUMBERING
    {
        public List<BEREC_NUMBERING> RECNUMBERING { get; set; }
        public BEREC_NUMBERING() { }

        public BEREC_NUMBERING(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            NMR_ID = Convert.ToDecimal(Reader["NMR_ID"]);
            TIS_N = Convert.ToDecimal(Reader["TIS_N"]);
            NMR_TYPE = Convert.ToString(Reader["NMR_TYPE"]);
            NMR_SERIAL = Convert.ToString(Reader["NMR_SERIAL"]);
            NMR_NAME = Convert.ToString(Reader["NMR_NAME"]);
            W_SERIAL = Convert.ToString(Reader["W_SERIAL"]);
            W_YEAR = Convert.ToString(Reader["W_YEAR"]);
            NMR_FORM = Convert.ToDecimal(Reader["NMR_FORM"]);
            NMR_TO = Convert.ToDecimal(Reader["NMR_TO"]);
            NMR_NOW = Convert.ToDecimal(Reader["NMR_NOW"]);
            AJUST = Convert.ToString(Reader["AJUST"]);
            POS_SERIAL = Convert.ToDecimal(Reader["POS_SERIAL"]);
            LON_YEAR = Convert.ToDecimal(Reader["LON_YEAR"]);
            POS_YEAR = Convert.ToDecimal(Reader["POS_YEAR"]);
            DIVIDER1 = Convert.ToString(Reader["DIVIDER1"]);
            DIVIDER2 = Convert.ToString(Reader["DIVIDER2"]);
            NMR_MANUAL = Convert.ToString(Reader["NMR_MANUAL"]);
            ENDS = Convert.ToDateTime(Reader["ENDS"]);

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

        public BEREC_NUMBERING(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            NMR_ID = Convert.ToDecimal(Reader["NMR_ID"]);
            TIS_N = Convert.ToDecimal(Reader["TIS_N"]);
            NMR_TYPE = Convert.ToString(Reader["NMR_TYPE"]);
            NMR_SERIAL = Convert.ToString(Reader["NMR_SERIAL"]);
            NMR_NAME = Convert.ToString(Reader["NMR_NAME"]);
            W_SERIAL = Convert.ToString(Reader["W_SERIAL"]);
            W_YEAR = Convert.ToString(Reader["W_YEAR"]);
            NMR_FORM = Convert.ToDecimal(Reader["NMR_FORM"]);
            NMR_TO = Convert.ToDecimal(Reader["NMR_TO"]);
            NMR_NOW = Convert.ToDecimal(Reader["NMR_NOW"]);
            AJUST = Convert.ToString(Reader["AJUST"]);
            POS_SERIAL = Convert.ToDecimal(Reader["POS_SERIAL"]);
            LON_YEAR = Convert.ToDecimal(Reader["LON_YEAR"]);
            POS_YEAR = Convert.ToDecimal(Reader["POS_YEAR"]);
            DIVIDER1 = Convert.ToString(Reader["DIVIDER1"]);
            DIVIDER2 = Convert.ToString(Reader["DIVIDER2"]);
            NMR_MANUAL = Convert.ToString(Reader["NMR_MANUAL"]);

            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
                ACTIVO = "I";
            }
            else
            {
                ACTIVO = "A";
            }

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

            NMR_TDESC = Convert.ToString(Reader["NMR_TDESC"]);
            TotalVirtual = flag;
        }
    }
}

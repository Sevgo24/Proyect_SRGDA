using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREF_CURRENCY
    {
        public List<BEREF_CURRENCY> REFCURRENCY { get; set; }
        public BEREF_CURRENCY() { }

        public BEREF_CURRENCY(IDataReader Reader)
        {
            CUR_ALPHA = Convert.ToString(Reader["CUR_ALPHA"]);
            CUR_DESC = Convert.ToString(Reader["CUR_DESC"]);
            CUR_NUM = Convert.ToDecimal(Reader["CUR_NUM"]);
            UNIT_MAJOR = Convert.ToString(Reader["UNIT_MAJOR"]);
            UNIT_MINOR = Convert.ToString(Reader["UNIT_MINOR"]);
            DECIMALS = Convert.ToDecimal(Reader["DECIMALS"]);
            FORMAT = Convert.ToString(Reader["FORMAT"]);
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

        public BEREF_CURRENCY(IDataReader Reader, int flag)
        {
            CUR_ALPHA = Convert.ToString(Reader["CUR_ALPHA"]);
            CUR_DESC = Convert.ToString(Reader["CUR_DESC"]);
            CUR_NUM = Convert.ToDecimal(Reader["CUR_NUM"]);
            UNIT_MAJOR = Convert.ToString(Reader["UNIT_MAJOR"]);
            UNIT_MINOR = Convert.ToString(Reader["UNIT_MINOR"]);
            DECIMALS = Convert.ToDecimal(Reader["DECIMALS"]);
            FORMAT = Convert.ToString(Reader["FORMAT"]);

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

            TotalVirtual = flag;
        }
    }
}

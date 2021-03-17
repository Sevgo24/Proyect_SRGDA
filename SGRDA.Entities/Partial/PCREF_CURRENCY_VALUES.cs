using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class REF_CURRENCY_VALUES
    {
        public List<REF_CURRENCY_VALUES> CURRENCYVALUES { get; set; }
        public REF_CURRENCY_VALUES() { }

        public REF_CURRENCY_VALUES(IDataReader Reader)
        {
            CUR_ALPHA = Convert.ToString(Reader["CUR_ALPHA"]);
            CUR_DATE = Convert.ToDateTime(Reader["CUR_DATE"]);
            CUR_VALUE = Convert.ToDecimal(Reader["CUR_VALUE"]);

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

        public REF_CURRENCY_VALUES(IDataReader Reader, int flag)
        {
            CUR_ALPHA = Convert.ToString(Reader["CUR_ALPHA"]);
            CUR_DATE = Convert.ToDateTime(Reader["CUR_DATE"]);
            CUR_VALUE = Convert.ToDecimal(Reader["CUR_VALUE"]);

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

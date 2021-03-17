using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_DEBTORS_RANGE
    {
        public List<BEREC_DEBTORS_RANGE> RECDEBTORSRANGE { get; set; }
        public BEREC_DEBTORS_RANGE() { }

        public BEREC_DEBTORS_RANGE(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            RANGE_COD = Convert.ToDecimal(Reader["RANGE_COD"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
            RANGE_FROM = Convert.ToDecimal(Reader["RANGE_FROM"]);
            RANGE_TO = Convert.ToDecimal(Reader["RANGE_TO"]);

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

        public BEREC_DEBTORS_RANGE(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            RANGE_COD = Convert.ToDecimal(Reader["RANGE_COD"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
            RANGE_FROM = Convert.ToDecimal(Reader["RANGE_FROM"]);
            RANGE_TO = Convert.ToDecimal(Reader["RANGE_TO"]);

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

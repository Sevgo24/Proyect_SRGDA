using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_BPS_QUALY
    {
        public List<BEREC_BPS_QUALY> RECBPSQUALY { get; set; }
        public BEREC_BPS_QUALY() { }

        public BEREC_BPS_QUALY(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            BPS_ID = Convert.ToDecimal(Reader["BPS_ID"]);
            QUC_ID = Convert.ToDecimal(Reader["QUC_ID"]);
            CARACTERISTICA = Convert.ToString(Reader["CARACTERISTICA"]);

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_UPDATE"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_UPDATE"]);
            }

            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDAT = Convert.ToString(Reader["LOG_USER_UPDAT"]);
        }

        public BEREC_BPS_QUALY(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            BPS_ID = Convert.ToDecimal(Reader["BPS_ID"]);
            QUC_ID = Convert.ToDecimal(Reader["QUC_ID"]);
            CARACTERISTICA = Convert.ToString(Reader["CARACTERISTICA"]);

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_UPDATE"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_UPDATE"]);
            }

            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDAT = Convert.ToString(Reader["LOG_USER_UPDAT"]);

            TotalVirtual = flag;
        }
    }
}

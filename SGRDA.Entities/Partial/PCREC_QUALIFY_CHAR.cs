using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_QUALIFY_CHAR
    {
        public List<BEREC_QUALIFY_CHAR> RECQUALIFYCHAR { get; set; }
        public BEREC_QUALIFY_CHAR() { }

        public BEREC_QUALIFY_CHAR(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            QUC_ID = Convert.ToDecimal(Reader["QUC_ID"]);
            QUA_ID = Convert.ToDecimal(Reader["QUA_ID"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
            DESCTIPO = Convert.ToString(Reader["DESCTIPO"]);

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

        public BEREC_QUALIFY_CHAR(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            QUC_ID = Convert.ToDecimal(Reader["QUC_ID"]);
            QUA_ID = Convert.ToDecimal(Reader["QUA_ID"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
            DESCTIPO = Convert.ToString(Reader["DESCTIPO"]);

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

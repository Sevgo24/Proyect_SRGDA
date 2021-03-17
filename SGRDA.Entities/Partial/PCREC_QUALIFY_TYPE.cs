using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_QUALIFY_TYPE
    {
        public List<BEREC_QUALIFY_TYPE> RECQUALIFYTYPE { get; set; }
        public BEREC_QUALIFY_TYPE() { }

        public BEREC_QUALIFY_TYPE(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            QUA_ID = Convert.ToDecimal(Reader["QUA_ID"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_UPDATE"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_UPDATE"]);
            }

            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDAT = Convert.ToString(Reader["LOG_USER_UPDATE"]);
        }

        public BEREC_QUALIFY_TYPE(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            QUA_ID = Convert.ToDecimal(Reader["QUA_ID"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_UPDATE"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_UPDATE"]);
            }

            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDAT = Convert.ToString(Reader["LOG_USER_UPDATE"]);

            TotalVirtual = flag;
        }
    }
}

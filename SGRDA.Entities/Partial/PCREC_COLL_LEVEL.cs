using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_COLL_LEVEL
    {
        public List<BEREC_COLL_LEVEL> RECCOLLLEVEL { get; set; }
        public BEREC_COLL_LEVEL() { }

        public BEREC_COLL_LEVEL(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            LEVEL_ID = Convert.ToDecimal(Reader["LEVEL_ID"]);
            NMR_ID = Convert.ToDecimal(Reader["NMR_ID"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
            LEVEL_DEP = Convert.ToDecimal(Reader["LEVEL_DEP"]);

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

        public BEREC_COLL_LEVEL(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            LEVEL_ID = Convert.ToDecimal(Reader["LEVEL_ID"]);
            NMR_ID = Convert.ToDecimal(Reader["NMR_ID"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
            LEVEL_DEP = Convert.ToDecimal(Reader["LEVEL_DEP"]);

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

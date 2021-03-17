using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_BANKS_GRAL
    {
        public List<BEREC_BANKS_GRAL> REC_BA { get; set; }
        public BEREC_BANKS_GRAL() { }

        public BEREC_BANKS_GRAL(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            BNK_ID = Convert.ToDecimal(Reader["BNK_ID"]);
            BNK_NAME = Convert.ToString(Reader["BNK_NAME"]);
            BNK_C_BRANCH = Convert.ToDecimal(Reader["BNK_C_BRANCH"]);
            BNK_C_DC = Convert.ToDecimal(Reader["BNK_C_DC"]);
            BNK_C_ACCOUNT = Convert.ToDecimal(Reader["BNK_C_ACCOUNT"]);
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



        public BEREC_BANKS_GRAL(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            BNK_ID = Convert.ToDecimal(Reader["BNK_ID"]);
            BNK_NAME = Convert.ToString(Reader["BNK_NAME"]);
            BNK_C_BRANCH = Convert.ToDecimal(Reader["BNK_C_BRANCH"]);
            BNK_C_DC = Convert.ToDecimal(Reader["BNK_C_DC"]);
            BNK_C_ACCOUNT = Convert.ToDecimal(Reader["BNK_C_ACCOUNT"]);

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

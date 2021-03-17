using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_BPS_BANKS_ACC
    {
        public List<BEREC_BPS_BANKS_ACC> RECBPSBANKSACC { get; set; }
        public BEREC_BPS_BANKS_ACC() { }

        public BEREC_BPS_BANKS_ACC(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            BPS_ACC_ID = Convert.ToDecimal(Reader["BPS_ACC_ID"]);
            BPS_ID = Convert.ToDecimal(Reader["BPS_ID"]);
            BNK_ID = Convert.ToDecimal(Reader["BNK_ID"]);
            BNK_NAME = Convert.ToString(Reader["BNK_NAME"]);
            BRCH_ID = Convert.ToString(Reader["BRCH_ID"]);
            BACC_NUMBER = Convert.ToString(Reader["BACC_NUMBER"]);
            BACC_DC = Convert.ToString(Reader["BACC_DC"]);
            BACC_TYPE = Convert.ToString(Reader["BACC_TYPE"]);
            BACC_DEF = Convert.ToString(Reader["BACC_DEF"]);
            ENDS = Convert.ToDateTime(Reader["ENDS"]);
            BRCH_NAME = Convert.ToString(Reader["BRCH_NAME"]);

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

        public BEREC_BPS_BANKS_ACC(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            BPS_ACC_ID = Convert.ToDecimal(Reader["BPS_ACC_ID"]);
            BPS_ID = Convert.ToDecimal(Reader["BPS_ID"]);
            BNK_ID = Convert.ToDecimal(Reader["BNK_ID"]);
            BNK_NAME = Convert.ToString(Reader["BNK_NAME"]);
            BRCH_ID = Convert.ToString(Reader["BRCH_ID"]);
            BACC_NUMBER = Convert.ToString(Reader["BACC_NUMBER"]);
            BACC_DC = Convert.ToString(Reader["BACC_DC"]);
            BACC_TYPE = Convert.ToString(Reader["BACC_TYPE"]);
            BACC_DEF = Convert.ToString(Reader["BACC_DEF"]);
            //BRCH_NAME = Convert.ToString(Reader["BRCH_NAME"]);

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

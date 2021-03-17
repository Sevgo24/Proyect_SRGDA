using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_BANKS_BRANCH
    {
        public List<BEREC_BANKS_BRANCH> RECBANKSBRANCH { get; set; }
        public BEREC_BANKS_BRANCH() { }

        public BEREC_BANKS_BRANCH(IDataReader Reader) 
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            BNK_ID = Convert.ToDecimal(Reader["BNK_ID"]);
            BRCH_ID = Convert.ToString(Reader["BRCH_ID"]);
            BRCH_NAME = Convert.ToString(Reader["BRCH_NAME"]);
            ADD_ID = Convert.ToDecimal(Reader["ADD_ID"]);
            ADDRESS = Convert.ToString(Reader["ADDRESS"]);
            BNK_NAME = Convert.ToString(Reader["BNK_NAME"]);
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

        public BEREC_BANKS_BRANCH(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            BNK_ID = Convert.ToDecimal(Reader["BNK_ID"]);
            BRCH_ID = Convert.ToString(Reader["BRCH_ID"]);
            BRCH_NAME = Convert.ToString(Reader["BRCH_NAME"]);
            ADD_ID = Convert.ToDecimal(Reader["ADD_ID"]);
            ADDRESS = Convert.ToString(Reader["ADDRESS"]);

            if (!DBNull.Value.Equals(Reader["BNK_NAME"]))
            {
                BNK_NAME = Convert.ToString(Reader["BNK_NAME"]);
            }

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

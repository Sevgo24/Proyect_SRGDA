using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_BANKS_BPS
    {
        public List<BEREC_BANKS_BPS> RECBANKSBPS { get; set; }
        public BEREC_BANKS_BPS() { }

        public BEREC_BANKS_BPS(IDataReader Reader)
        {
            if (!DBNull.Value.Equals(Reader["Id"]))
            {
                Id = Convert.ToDecimal(Reader["Id"]);
            }
            BNK_ID = Convert.ToDecimal(Reader["BNK_ID"]);
            TAXN_NAME = Convert.ToString(Reader["TAXN_NAME"]);
            TAX_ID = Convert.ToString(Reader["TAX_ID"]);
            BPS_NAME = Convert.ToString(Reader["BPS_NAME"]);
            ROL_DESC = Convert.ToString(Reader["ROL_DESC"]);
            TAXT_ID = Convert.ToDecimal(Reader["TAXT_ID"]);
            BPS_ID = Convert.ToDecimal(Reader["BPS_ID"]);

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_UPDATE"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_UPDATE"]);
            }

            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
            }

            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDATE"]);
        }

        public BEREC_BANKS_BPS(IDataReader Reader, int flag)
        {
            if (!DBNull.Value.Equals(Reader["Id"]))
            {
                Id = Convert.ToDecimal(Reader["Id"]);
            }

            BNK_ID = Convert.ToDecimal(Reader["BNK_ID"]);
            TAXN_NAME = Convert.ToString(Reader["TAXN_NAME"]);
            TAX_ID = Convert.ToString(Reader["TAX_ID"]);
            BPS_NAME = Convert.ToString(Reader["BPS_NAME"]);
            ROL_DESC = Convert.ToString(Reader["ROL_DESC"]);
            TAXT_ID = Convert.ToDecimal(Reader["TAXT_ID"]);
            BPS_ID = Convert.ToDecimal(Reader["BPS_ID"]);

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_UPDATE"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_UPDATE"]);
            }

            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
            }

            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDATE"]);

            TotalVirtual = flag;
        }
    }
}

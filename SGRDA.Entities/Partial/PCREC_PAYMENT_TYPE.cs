using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_PAYMENT_TYPE
    {
        public List<BEREC_PAYMENT_TYPE> RECPAYMENTTYPE { get; set; }
        public BEREC_PAYMENT_TYPE() { }

        public BEREC_PAYMENT_TYPE(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            RowNumber = Convert.ToInt32(Reader["RowNumber"]);
            PAY_ID = Convert.ToString(Reader["PAY_ID"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
            PAY_BANK = Convert.ToBoolean(Reader["PAY_BANK"]);
            PAY_BANK_RECEIPT = Convert.ToBoolean(Reader["PAY_BANK_RECEIPT"]);
            PAY_AGE_RECEIPT = Convert.ToBoolean(Reader["PAY_AGE_RECEIPT"]);
            PAY_TRANSFER = Convert.ToBoolean(Reader["PAY_TRANSFER"]);

            PAY_DATE_FIX = Convert.ToBoolean(Reader["PAY_DATE_FIX"]);
            if (!DBNull.Value.Equals(Reader["PAY_DATE_FIX_DAY"]))
            {
                PAY_DATE_FIX_DAY = Convert.ToDecimal(Reader["PAY_DATE_FIX_DAY"]);
            }
            VTO1 = Convert.ToDecimal(Reader["VTO1"]);
            VTO2 = Convert.ToDecimal(Reader["VTO2"]);
            VTO3 = Convert.ToDecimal(Reader["VTO3"]);
            VTO4 = Convert.ToDecimal(Reader["VTO4"]);
            VTO5 = Convert.ToDecimal(Reader["VTO5"]);
            VTO6 = Convert.ToDecimal(Reader["VTO6"]);

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

        public BEREC_PAYMENT_TYPE(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            RowNumber = Convert.ToInt32(Reader["RowNumber"]);
            PAY_ID = Convert.ToString(Reader["PAY_ID"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
            PAY_BANK = Convert.ToBoolean(Reader["PAY_BANK"]);
            PAY_BANK_RECEIPT = Convert.ToBoolean(Reader["PAY_BANK_RECEIPT"]);
            PAY_AGE_RECEIPT = Convert.ToBoolean(Reader["PAY_AGE_RECEIPT"]);
            PAY_TRANSFER = Convert.ToBoolean(Reader["PAY_TRANSFER"]);

            if (!DBNull.Value.Equals(Reader["VTO1"]))
            {
                VTO1 = Convert.ToDecimal(Reader["VTO1"]);
            }
            if (!DBNull.Value.Equals(Reader["VTO2"]))
            {
                VTO2 = Convert.ToDecimal(Reader["VTO2"]);
            }
            if (!DBNull.Value.Equals(Reader["VTO3"]))
            {
                VTO3 = Convert.ToDecimal(Reader["VTO3"]);
            }
            if (!DBNull.Value.Equals(Reader["VTO4"]))
            {
                VTO4 = Convert.ToDecimal(Reader["VTO4"]);
            }
            if (!DBNull.Value.Equals(Reader["VTO5"]))
            {
                VTO5 = Convert.ToDecimal(Reader["VTO5"]);
            }
            if (!DBNull.Value.Equals(Reader["VTO6"]))
            {
                VTO6 = Convert.ToDecimal(Reader["VTO6"]);
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

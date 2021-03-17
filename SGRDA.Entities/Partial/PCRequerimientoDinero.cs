using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BERequerimientoDinero
    {

        public List<BERequerimientoDinero> RequerimientoDinero { get; set; }
        public BERequerimientoDinero() { }

        public BERequerimientoDinero(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            MNR_ID = Convert.ToDecimal(Reader["MNR_ID"]);
            BPS_ID = Convert.ToDecimal(Reader["BPS_ID"]);
            TAXT_ID = Convert.ToDecimal(Reader["TAXT_ID"]);
            TAX_ID = Convert.ToString(Reader["TAX_ID"]);
            BPS_NAME = Convert.ToString(Reader["BPS_NAME"]);
            STT_ID = Convert.ToDecimal(Reader["STT_ID"]);
            MNR_DESC = Convert.ToString(Reader["MNR_DESC"]);
            MNR_DATE = Convert.ToDateTime(Reader["MNR_DATE"]);
            MNR_VALUE_PRE = Convert.ToDecimal(Reader["MNR_VALUE_PRE"]);
            MNR_VALUE_APR = Convert.ToDecimal(Reader["MNR_VALUE_APR"]);
            MNR_VALUE_CON = Convert.ToDecimal(Reader["MNR_VALUE_CON"]);
            EXP_APR = Convert.ToDecimal(Reader["EXP_APR"]);
            MNR_APP = Convert.ToString(Reader["MNR_APP"]);
            MNR_APP_DATE = Convert.ToDateTime(Reader["MNR_APP_DATE"]);
            MNR_APP_USER = Convert.ToDecimal(Reader["MNR_APP_USER"]);
            MNR_APP_CODE = Convert.ToDecimal(Reader["MNR_APP_CODE"]);
            MNR_COUNT = Convert.ToDateTime(Reader["MNR_COUNT"]);
            MNR_COUNT_N = Convert.ToString(Reader["MNR_COUNT_N"]);
            ESTADO = Convert.ToString(Reader["ESTADO"]);

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_UPDATE"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_UPDATE"]);
            }

            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDAT"]);
        }

        public BERequerimientoDinero(IDataReader Reader, int flag)
        {
            MNR_ID = Convert.ToDecimal(Reader["MNR_ID"]);
            BPS_ID = Convert.ToDecimal(Reader["BPS_ID"]);
            STT_ID = Convert.ToDecimal(Reader["STT_ID"]);
            BPS_NAME = Convert.ToString(Reader["NAME"]);
            MNR_DESC = Convert.ToString(Reader["MNR_DESC"]);
            MNR_DATE = Convert.ToDateTime(Reader["MNR_DATE"]);
            MNR_VALUE_PRE = Convert.ToDecimal(Reader["MNR_VALUE_PRE"]);            
            MNR_VALUE_APR = Convert.ToDecimal(Reader["MNR_VALUE_APR"]);
            MNR_VALUE_CON = Convert.ToDecimal(Reader["MNR_VALUE_CON"]);
            ESTADO = Convert.ToString(Reader["ESTADO"]);
            TIPO_DOC = Convert.ToString(Reader["TAXN_NAME"]);
            NUM_DOC = Convert.ToString(Reader["TAX_ID"]);
            TotalVirtual = flag;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_TAX_ID
    {
        public List<BEREC_TAX_ID> RECTAXID { get; set; }
        public BEREC_TAX_ID() { }

        public BEREC_TAX_ID(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            TAXT_ID = Convert.ToDecimal(Reader["TAXT_ID"]);
            TIS_N = Convert.ToDecimal(Reader["TIS_N"]);
            TAXN_NAME = Convert.ToString(Reader["TAXN_NAME"]);
            TAXN_POS = Convert.ToDecimal(Reader["TAXN_POS"]);
            TEXT_DESCRIPTION = Convert.ToString(Reader["TEXT_DESCRIPTION"]);
            NAME_TER = Convert.ToString(Reader["NAME_TER"]);

            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
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
            LOG_USER_UPDAT = Convert.ToString(Reader["LOG_USER_UPDAT"]);
        }

        public BEREC_TAX_ID(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            TAXT_ID = Convert.ToDecimal(Reader["TAXT_ID"]);
            TIS_N = Convert.ToDecimal(Reader["TIS_N"]);
            TAXN_NAME = Convert.ToString(Reader["TAXN_NAME"]);
            TAXN_POS = Convert.ToDecimal(Reader["TAXN_POS"]);
            TEXT_DESCRIPTION = Convert.ToString(Reader["TEXT_DESCRIPTION"]);
            NAME_TER = Convert.ToString(Reader["NAME_TER"]);

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
            LOG_USER_UPDAT = Convert.ToString(Reader["LOG_USER_UPDAT"]);

            TotalVirtual = flag;
        }
    }
}

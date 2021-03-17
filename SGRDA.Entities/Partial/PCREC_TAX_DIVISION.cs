using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREF_TAX_DIVISION
    {
        public List<BEREF_TAX_DIVISION> RECTAXDIVISION { get; set; }
        public BEREF_TAX_DIVISION() { }

        public BEREF_TAX_DIVISION(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            TAXD_ID = Convert.ToDecimal(Reader["TAXD_ID"]);
            TIS_N = Convert.ToDecimal(Reader["TIS_N"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
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

        public BEREF_TAX_DIVISION(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            TAXD_ID = Convert.ToDecimal(Reader["TAXD_ID"]);
            TIS_N = Convert.ToDecimal(Reader["TIS_N"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);

            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
                Activo = "I";
            }
            else
            {
                Activo = "A";
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

            NAME_TER = Convert.ToString(Reader["NAME_TER"]);

            TotalVirtual = flag;
        }
    }
}

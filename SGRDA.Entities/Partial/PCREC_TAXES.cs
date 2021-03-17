using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BEREC_TAXES
    {
        public List<BEREC_TAXES> RECTAXES { get; set; }
        public BEREC_TAXES() { }

        public BEREC_TAXES(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            TAX_ID = Convert.ToDecimal(Reader["TAX_ID"]);
            TAX_COD = Convert.ToString(Reader["TAX_COD"]);
            TIS_N = Convert.ToDecimal(Reader["TIS_N"]);
            TAX_COD = Convert.ToString(Reader["TAX_COD"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
            NAME_TER = Convert.ToString(Reader["NAME_TER"]);

            if (!DBNull.Value.Equals(Reader["START"]))
            {
                START = Convert.ToDateTime(Reader["START"]);
            }

            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
            }

            TAX_CACC = Convert.ToDecimal(Reader["TAX_CACC"]);

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDATE"]);
        }

        public BEREC_TAXES(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            TAX_ID = Convert.ToDecimal(Reader["TAX_ID"]);
            TAX_COD = Convert.ToString(Reader["TAX_COD"]);
            TIS_N = Convert.ToDecimal(Reader["TIS_N"]);
            TAX_COD = Convert.ToString(Reader["TAX_COD"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
            NAME_TER = Convert.ToString(Reader["NAME_TER"]);

            if (!DBNull.Value.Equals(Reader["START"]))
            {
                START = Convert.ToDateTime(Reader["START"]);
            }

            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
                Activo = "I";
            }
            else
            {
                Activo = "A";
            }

            TAX_CACC = Convert.ToDecimal(Reader["TAX_CACC"]);

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

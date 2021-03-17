using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_CHARACTERISTICS
    {
        public List<BEREC_CHARACTERISTICS> RECCHARACTERISTICS { get; set; }
        public BEREC_CHARACTERISTICS() { }

        public BEREC_CHARACTERISTICS(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            CHAR_ID = Convert.ToDecimal(Reader["CHAR_ID"]);
            CHAR_SHORT = Convert.ToString(Reader["CHAR_SHORT"]);
            CHAR_LONG = Convert.ToString(Reader["CHAR_LONG"]);
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

        public BEREC_CHARACTERISTICS(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            CHAR_ID = Convert.ToDecimal(Reader["CHAR_ID"]);
            CHAR_SHORT = Convert.ToString(Reader["CHAR_SHORT"]);
            CHAR_LONG = Convert.ToString(Reader["CHAR_LONG"]);

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

            TotalVirtual = flag;
        }
    }
}

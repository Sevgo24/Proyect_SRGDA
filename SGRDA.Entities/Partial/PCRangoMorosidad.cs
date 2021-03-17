using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BERangoMorosidad
    {
        public List<BERangoMorosidad> RangoMorosidad { get; set; }
        public BERangoMorosidad() { }

        public BERangoMorosidad(IDataReader Reader)
        {
            RowNumber = Convert.ToInt32(Reader["RowNumber"]);
            OWNER = Convert.ToString(Reader["OWNER"]);
            RANGE_COD = Convert.ToDecimal(Reader["RANGE_COD"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
            RANGE_FROM = Convert.ToDecimal(Reader["RANGE_FROM"]);
            RANGE_TO = Convert.ToDecimal(Reader["RANGE_TO"]);
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

        public BERangoMorosidad(IDataReader Reader, int flag)
        {
            RowNumber = Convert.ToInt32(Reader["RowNumber"]);
            OWNER = Convert.ToString(Reader["OWNER"]);
            RANGE_COD = Convert.ToDecimal(Reader["RANGE_COD"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
            RANGE_FROM = Convert.ToDecimal(Reader["RANGE_FROM"]);
            RANGE_TO = Convert.ToDecimal(Reader["RANGE_TO"]);

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

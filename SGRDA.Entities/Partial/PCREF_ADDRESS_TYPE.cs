using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREF_ADDRESS_TYPE
    {
        public List<BEREF_ADDRESS_TYPE> REFADDRESSTYPE { get; set; }
        public BEREF_ADDRESS_TYPE() { }

        public BEREF_ADDRESS_TYPE(IDataReader Reader)
        {   
            OWNER = Convert.ToString(Reader["OWNER"]);
            ADDT_ID = Convert.ToDecimal(Reader["ADDT_ID"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
            ADDT_OBSERV = Convert.ToString(Reader["ADDT_OBSERV"]);
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

        public BEREF_ADDRESS_TYPE(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            ADDT_ID = Convert.ToDecimal(Reader["ADDT_ID"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]).ToUpper();
            ADDT_OBSERV = Convert.ToString(Reader["ADDT_OBSERV"]);
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

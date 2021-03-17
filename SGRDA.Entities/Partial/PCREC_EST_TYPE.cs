using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_EST_TYPE
    {
        public List<BEREC_EST_TYPE> RECESTTYPE { get; set; }
        public BEREC_EST_TYPE() { }

        public BEREC_EST_TYPE(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            ESTT_ID = Convert.ToDecimal(Reader["ESTT_ID"]);
            ECON_ID = Convert.ToString(Reader["ECON_ID"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
            ECON_DESC = Convert.ToString(Reader["ECON_DESC"]);

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

        public BEREC_EST_TYPE(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            ESTT_ID = Convert.ToDecimal(Reader["ESTT_ID"]);
            ECON_ID = Convert.ToString(Reader["ECON_ID"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]).ToUpper();
            ECON_DESC = Convert.ToString(Reader["ECON_DESC"]).ToUpper();

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
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDAT"]);

            TotalVirtual = flag;
        }
    }
}

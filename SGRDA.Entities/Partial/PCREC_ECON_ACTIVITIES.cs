using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_ECON_ACTIVITIES
    {
        public List<BEREC_ECON_ACTIVITIES> RECECONACTIVITIES { get; set; }
        public BEREC_ECON_ACTIVITIES() { }

        public BEREC_ECON_ACTIVITIES(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            ECON_ID = Convert.ToString(Reader["ECON_ID"]);
            ECON_DESC = Convert.ToString(Reader["ECON_DESC"]);
            ECON_BELONGS = Convert.ToString(Reader["ECON_BELONGS"]);
            ECON_ID_Bellong = Convert.ToString(Reader["ECON_ID_Bellong"]);
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
            LOG_USER_UPDAT = Convert.ToString(Reader["LOG_USER_UPDAT"]);
        }

        public BEREC_ECON_ACTIVITIES(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            ECON_ID = Convert.ToString(Reader["ECON_ID"]);
            ECON_DESC = Convert.ToString(Reader["ECON_DESC"]);
            ECON_BELONGS = Convert.ToString(Reader["ECON_BELONGS"]);
            ECON_ID_Bellong = Convert.ToString(Reader["ECON_ID_Bellong"]);

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

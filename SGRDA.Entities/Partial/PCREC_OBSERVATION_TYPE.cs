using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_OBSERVATION_TYPE
    {
        public List<BEREC_OBSERVATION_TYPE> REC_OBSERVATION_TYPE { get; set; }
        public BEREC_OBSERVATION_TYPE() { }

        public BEREC_OBSERVATION_TYPE(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            OBS_TYPE = Convert.ToDecimal(Reader["OBS_TYPE"]);
            OBS_GROUP = Convert.ToString(Reader["OBS_GROUP"]);
            OBS_DESC = Convert.ToString(Reader["OBS_DESC"]);
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

        public BEREC_OBSERVATION_TYPE(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            OBS_TYPE = Convert.ToDecimal(Reader["OBS_TYPE"]);
            OBS_GROUP = Convert.ToString(Reader["OBS_GROUP"]);
            OBS_DESC = Convert.ToString(Reader["OBS_DESC"]);

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
            TotalVirtual = flag;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BEObservationType
    {
        public List<BEObservationType> TipoObservacion { get; set; }
        public BEObservationType() { }

        public BEObservationType(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            TIPO = Convert.ToDecimal(Reader["OBS_TYPE"]);
            OBS_DESC = Convert.ToString(Reader["OBS_DESC"]);
            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }
            OBS_OBSERV = Convert.ToString(Reader["OBS_OBSERV"]);
            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDATE"]);
        }

        public BEObservationType(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            TIPO = Convert.ToDecimal(Reader["OBS_TYPE"]);
            OBS_DESC = Convert.ToString(Reader["OBS_DESC"]).ToUpper();

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

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            OBS_OBSERV = Convert.ToString(Reader["OBS_OBSERV"]);
            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDATE"]);

            TotalVirtual = flag;
        }
    }
}

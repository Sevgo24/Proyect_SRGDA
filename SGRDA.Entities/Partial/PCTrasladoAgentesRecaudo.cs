using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BETrasladoAgentesRecaudo
    {
        public List<BETrasladoAgentesRecaudo> Traslado { get; set; }
        public BETrasladoAgentesRecaudo() { }

        public BETrasladoAgentesRecaudo(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            OFF_ID = Convert.ToDecimal(Reader["OFF_ID"]);
            BPS_ID = Convert.ToDecimal(Reader["BPS_ID"]);
            LEVEL_ID = Convert.ToDecimal(Reader["LEVEL_ID"]);
            OFF_NAME = Convert.ToString(Reader["OFF_NAME"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
            BPS_NAME = Convert.ToString(Reader["BPS_NAME"]);

            if (!DBNull.Value.Equals(Reader["START"]))
            {
                START = Convert.ToDateTime(Reader["START"]);
            }

            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDAT = Convert.ToString(Reader["LOG_USER_UPDAT"]);
        }

        public BETrasladoAgentesRecaudo(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            OFF_ID = Convert.ToDecimal(Reader["OFF_ID"]);
            BPS_ID = Convert.ToDecimal(Reader["BPS_ID"]);
            LEVEL_ID = Convert.ToDecimal(Reader["LEVEL_ID"]);
            OFF_NAME = Convert.ToString(Reader["OFF_NAME"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
            BPS_NAME = Convert.ToString(Reader["BPS_NAME"]);

            if (!DBNull.Value.Equals(Reader["START"]))
            {
                START = Convert.ToDateTime(Reader["START"]);
            }

            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDAT = Convert.ToString(Reader["LOG_USER_UPDAT"]);

            TotalVirtual = flag;
        }
    }
}

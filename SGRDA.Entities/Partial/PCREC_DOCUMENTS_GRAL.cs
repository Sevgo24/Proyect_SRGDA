using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_DOCUMENTS_GRAL
    {
        public List<BEREC_DOCUMENTS_GRAL> RECDOCUMENTSGRAL { get; set; }
        public BEREC_DOCUMENTS_GRAL() { }

        public BEREC_DOCUMENTS_GRAL(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            DOC_ID = Convert.ToDecimal(Reader["DOC_ID"]);
            DOC_TYPE = Convert.ToDecimal(Reader["DOC_TYPE"]);
            DOC_DESC = Convert.ToString(Reader["DOC_DESC"]);
            ENT_ID = Convert.ToDecimal(Reader["ENT_ID"]);

            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
            }

            if (!DBNull.Value.Equals(Reader["DOC_DATE"]))
            {
                DOC_DATE = Convert.ToDateTime(Reader["DOC_DATE"]);
            }

            DOC_VERSION = Convert.ToDecimal(Reader["DOC_VERSION"]);
            DOC_USER = Convert.ToString(Reader["DOC_USER"]);
            DOC_PATH = Convert.ToString(Reader["DOC_PATH"]);

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

        public BEREC_DOCUMENTS_GRAL(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            DOC_ID = Convert.ToDecimal(Reader["DOC_ID"]);
            DOC_TYPE = Convert.ToDecimal(Reader["DOC_TYPE"]);
            DOC_DESC = Convert.ToString(Reader["DOC_DESC"]);
            ENT_ID = Convert.ToDecimal(Reader["ENT_ID"]);

            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
            }

            if (!DBNull.Value.Equals(Reader["DOC_DATE"]))
            {
                DOC_DATE = Convert.ToDateTime(Reader["DOC_DATE"]);
            }

            DOC_VERSION = Convert.ToDecimal(Reader["DOC_VERSION"]);
            DOC_USER = Convert.ToString(Reader["DOC_USER"]);
            DOC_PATH = Convert.ToString(Reader["DOC_PATH"]);

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

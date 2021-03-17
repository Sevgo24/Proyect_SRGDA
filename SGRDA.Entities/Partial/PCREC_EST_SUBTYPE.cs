using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_EST_SUBTYPE 
    {
        public List<BEREC_EST_SUBTYPE> RECESTSUBTYPE { get; set; }  
        public BEREC_EST_SUBTYPE() { }

        public BEREC_EST_SUBTYPE(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            SUBE_ID = Convert.ToDecimal(Reader["SUBE_ID"]);
            ESTT_ID = Convert.ToDecimal(Reader["ESTT_ID"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);
            DESCRIPTIONTYPE = Convert.ToString(Reader["DESCRIPTIONTYPE"]);
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

        public BEREC_EST_SUBTYPE(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            SUBE_ID = Convert.ToDecimal(Reader["SUBE_ID"]);
            ESTT_ID = Convert.ToDecimal(Reader["ESTT_ID"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);

            DESCRIPTIONTYPE = Convert.ToString(Reader["DESCRIPTIONTYPE"]);

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

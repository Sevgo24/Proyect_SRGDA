using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREF_SOCIETY_TYPE
    {
        public List<BEREF_SOCIETY_TYPE> REFSOCIETYTYPE { get; set; }
        public BEREF_SOCIETY_TYPE() { }

        public BEREF_SOCIETY_TYPE(IDataReader Reader)
        {
            SOC_TYPE = Convert.ToString(Reader["SOC_TYPE"]);
            SOC_DESC = Convert.ToString(Reader["SOC_DESC"]);

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_UPDATE"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_UPDATE"]);
            }

            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDAT = Convert.ToString(Reader["LOG_USER_UPDATE"]);
        }

        public BEREF_SOCIETY_TYPE(IDataReader Reader, int flag)
        {
            SOC_TYPE = Convert.ToString(Reader["SOC_TYPE"]);
            SOC_DESC = Convert.ToString(Reader["SOC_DESC"]);

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_UPDATE"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_UPDATE"]);
            }

            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDAT = Convert.ToString(Reader["LOG_USER_UPDATE"]);

            TotalVirtual = flag;
        }
    }
}

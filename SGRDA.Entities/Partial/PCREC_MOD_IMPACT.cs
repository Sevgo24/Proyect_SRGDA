using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_MOD_IMPACT
    {
        public List<BEREC_MOD_IMPACT> RECUSESTYPE { get; set; }
        public BEREC_MOD_IMPACT() { }

        public BEREC_MOD_IMPACT(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            MOD_INCID = Convert.ToString(Reader["MOD_INCID"]);
            MOD_IDESC = Convert.ToString(Reader["MOD_IDESC"]);
            MOD_IDET = Convert.ToString(Reader["MOD_IDET"]);
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

        public BEREC_MOD_IMPACT(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            MOD_INCID = Convert.ToString(Reader["MOD_INCID"]);
            if (!DBNull.Value.Equals(Reader["MOD_IDESC"]))
                MOD_IDESC = Convert.ToString(Reader["MOD_IDESC"]).ToUpper();
            if (!DBNull.Value.Equals(Reader["MOD_IDET"]))
                MOD_IDET = Convert.ToString(Reader["MOD_IDET"]).ToUpper();

            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
                Activo = "I";
            }
            else
            {
                Activo = "A";
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

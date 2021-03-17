using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_MOV_TYPE
    {
        public List<BEREC_MOV_TYPE> RECMOVTYPE { get; set; }
        public BEREC_MOV_TYPE() { }

        public BEREC_MOV_TYPE(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            MOV_TYPE = Convert.ToString(Reader["MOV_TYPE"]);
            MOV_DESC = Convert.ToString(Reader["MOV_DESC"]);
            MOV_SIGN = Convert.ToString(Reader["MOV_SIGN"]);
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

        public BEREC_MOV_TYPE(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            MOV_TYPE = Convert.ToString(Reader["MOV_TYPE"]);
            MOV_DESC = Convert.ToString(Reader["MOV_DESC"]);
            MOV_SIGN = Convert.ToString(Reader["MOV_SIGN"]);

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

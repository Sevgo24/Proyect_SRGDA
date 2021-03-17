using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BEREF_DIV_TYPE
    {
        public List<BEREF_DIV_TYPE> REFDIVTYPE { get; set; }
        public BEREF_DIV_TYPE() { }

        public BEREF_DIV_TYPE(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            DAD_TYPE = Convert.ToString(Reader["DAD_TYPE"]);
            DAD_TNAME = Convert.ToString(Reader["DAD_TNAME"]);
            TIS_N = Convert.ToDecimal(Reader["TIS_N"]);
            NAME_TER = Convert.ToString(Reader["NAME_TER"]);

            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
                Activo = "I";
            }
            else
            {
                Activo = "A";
            }

            DIVT_OBSERV = Convert.ToString(Reader["DIVT_OBSERV"]);

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

        public BEREF_DIV_TYPE(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            DAD_TYPE = Convert.ToString(Reader["DAD_TYPE"]);
            DAD_TNAME = Convert.ToString(Reader["DAD_TNAME"]);
            TIS_N = Convert.ToDecimal(Reader["TIS_N"]);
            NAME_TER = Convert.ToString(Reader["NAME_TER"]);
            DIVT_OBSERV = Convert.ToString(Reader["DIVT_OBSERV"]);

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
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDATE"]);

            TotalVirtual = flag;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BEREF_DIV_STYPE_CHAR
    {
        public List<BEREF_DIV_STYPE_CHAR> REFDIVSTYPECHAR { get; set; }
        public BEREF_DIV_STYPE_CHAR() { }

        public BEREF_DIV_STYPE_CHAR(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            DAC_ID = Convert.ToString(Reader["DAC_ID"]);
            DAD_TYPE = Convert.ToString(Reader["DAD_TYPE"]);
            DAD_STYPE = Convert.ToString(Reader["DAD_STYPE"]);

            DAD_TNAME = Convert.ToString(Reader["DAD_TNAME"]);
            DAD_SNAME = Convert.ToString(Reader["DAD_SNAME"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);    

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

        public BEREF_DIV_STYPE_CHAR(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            DAC_ID = Convert.ToString(Reader["DAC_ID"]);
            DAD_TYPE = Convert.ToString(Reader["DAD_TYPE"]);
            DAD_STYPE = Convert.ToString(Reader["DAD_STYPE"]);

            DAD_TNAME = Convert.ToString(Reader["DAD_TNAME"]);
            DAD_SNAME = Convert.ToString(Reader["DAD_SNAME"]);
            DESCRIPTION = Convert.ToString(Reader["DESCRIPTION"]);    

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

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREF_DIVISIONES
    {
        public List<BEREF_DIVISIONES> REFDIVISIONES { get; set; }
        public BEREF_DIVISIONES() { }

        public BEREF_DIVISIONES(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            DAD_ID = Convert.ToDecimal(Reader["DAD_ID"]);
            DAD_CODE = Convert.ToString(Reader["DAD_CODE"]);
            DAD_NAME = Convert.ToString(Reader["DAD_NAME"]);
            DAD_TYPE = Convert.ToString(Reader["DAD_TYPE"]); 
            DAD_TNAME = Convert.ToString(Reader["DAD_TNAME"]);

            if (!DBNull.Value.Equals(Reader["DIV_DESCRIPTION"]))
            {
                DIV_DESCRIPTION = Convert.ToString(Reader["DIV_DESCRIPTION"]);
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
        }

        public BEREF_DIVISIONES(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            DAD_ID = Convert.ToDecimal(Reader["DAD_ID"]);
            DAD_CODE = Convert.ToString(Reader["DAD_CODE"]);
            DAD_NAME = Convert.ToString(Reader["DAD_NAME"]);
            DAD_TYPE = Convert.ToString(Reader["DAD_TYPE"]);   
            DAD_TNAME = Convert.ToString(Reader["DAD_TNAME"]);

            if (!DBNull.Value.Equals(Reader["DIV_DESCRIPTION"]))
            {
                DIV_DESCRIPTION = Convert.ToString(Reader["DIV_DESCRIPTION"]);
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

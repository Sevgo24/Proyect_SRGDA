using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SGRDA.Entities
{
    public partial class BEREF_DIV_SUBTYPE
    {
        public List<BEREF_DIV_SUBTYPE> REFDIVSUBTYPE { get; set; }
        public BEREF_DIV_SUBTYPE() { }

        public BEREF_DIV_SUBTYPE(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            DAD_CODE = Convert.ToString(Reader["DAD_CODE"]);
            DAD_STYPE = Convert.ToDecimal(Reader["DAD_STYPE"]);
            DAD_SNAME = Convert.ToString(Reader["DAD_SNAME"]);
            DAD_NAME = Convert.ToString(Reader["DAD_NAME"]);
            DAD_BELONGS = Convert.ToDecimal(Reader["DAD_BELONGS"]);

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

        public BEREF_DIV_SUBTYPE(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            DAD_CODE = Convert.ToString(Reader["DAD_CODE"]);
            DAD_STYPE = Convert.ToDecimal(Reader["DAD_STYPE"]);
            DAD_SNAME = Convert.ToString(Reader["DAD_SNAME"]);
            DAD_NAME = Convert.ToString(Reader["DAD_NAME"]);
            DAD_BELONGS = Convert.ToDecimal(Reader["DAD_BELONGS"]);

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

           TotalVirtual =  flag;
        }
    }
}

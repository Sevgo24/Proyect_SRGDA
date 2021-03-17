using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEDivision
    {
        public List<BEDivision> Div { get; set; }
        public BEDivision() { }

        public BEDivision(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            DADV_ID = Convert.ToDecimal(Reader["DADV_ID"]);
            DAD_ID = Convert.ToDecimal(Reader["DAD_ID"]);
            DAD_STYPE = Convert.ToDecimal(Reader["DAD_STYPE"]);
            DAD_VCODE = Convert.ToString(Reader["DAD_VCODE"]);
            DAD_VNAME = Convert.ToString(Reader["DAD_VNAME"]);
            DAD_BELONGS = Convert.ToString(Reader["DAD_BELONGS"]);

            DAD_CODE = Convert.ToString(Reader["DAD_CODE"]);
            DAD_SNAME = Convert.ToString(Reader["DAD_SNAME"]);
            DAD_NAME = Convert.ToString(Reader["DAD_NAME"]);

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

        public BEDivision(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            DADV_ID = Convert.ToDecimal(Reader["DADV_ID"]);
            DAD_ID = Convert.ToDecimal(Reader["DAD_ID"]);
            DAD_STYPE = Convert.ToDecimal(Reader["DAD_STYPE"]);
            DAD_VCODE = Convert.ToString(Reader["DAD_VCODE"]);
            DAD_VNAME = Convert.ToString(Reader["DAD_VNAME"]);
            DAD_BELONGS = Convert.ToString(Reader["DAD_BELONGS"]);

            DAD_CODE = Convert.ToString(Reader["DAD_CODE"]);
            DAD_SNAME = Convert.ToString(Reader["DAD_SNAME"]);
            DAD_NAME = Convert.ToString(Reader["DAD_NAME"]);

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

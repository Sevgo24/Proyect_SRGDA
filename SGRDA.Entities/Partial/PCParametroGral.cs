using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SGRDA.Entities
{
    public partial class BEParametroGral
    {
        public List<BEParametroGral> Parametro { get; set; }
        public BEParametroGral() { }
        
        public BEParametroGral(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            PAR_ID = Convert.ToDecimal(Reader["PAR_ID"]);
            PAR_TYPE = Convert.ToDecimal(Reader["PAR_TYPE"]);
            ENT_ID = Convert.ToDecimal(Reader["ENT_ID"]);
            PAR_VALUE = Convert.ToString(Reader["PAR_VALUE"]);

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

        public BEParametroGral(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            PAR_ID = Convert.ToDecimal(Reader["PAR_ID"]);
            PAR_TYPE = Convert.ToDecimal(Reader["PAR_TYPE"]);
            ENT_ID = Convert.ToDecimal(Reader["ENT_ID"]);
            PAR_VALUE = Convert.ToString(Reader["PAR_VALUE"]);

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
    }
}

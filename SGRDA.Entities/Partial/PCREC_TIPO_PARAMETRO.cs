using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_TIPO_PARAMETRO
    {
        public List<BEREC_TIPO_PARAMETRO> REC_TIPO_PARAMETRO { get; set; }
        public BEREC_TIPO_PARAMETRO() { }

        public BEREC_TIPO_PARAMETRO(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            PAR_TYPE = Convert.ToDecimal(Reader["PAR_TYPE"]);
            PAR_GROUP = Convert.ToString(Reader["PAR_GROUP"]);
            PAR_DESC = Convert.ToString(Reader["PAR_DESC"]);
            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDATE"]);
        }

        public BEREC_TIPO_PARAMETRO(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            PAR_TYPE = Convert.ToDecimal(Reader["PAR_TYPE"]);
            PAR_GROUP = Convert.ToString(Reader["PAR_GROUP"]);
            PAR_DESC = Convert.ToString(Reader["PAR_DESC"]);
            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            {
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            }

            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDATE"]);
            TotalVirtual = flag;
        }
    }
}

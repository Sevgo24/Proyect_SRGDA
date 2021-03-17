using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREC_MOD_GROUP 
    {
        public List<BEREC_MOD_GROUP> RECMODGROUP { get; set; }
        public BEREC_MOD_GROUP() { }

        public BEREC_MOD_GROUP(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            MOG_ID = Convert.ToString(Reader["MOG_ID"]);
            MOG_DESC = Convert.ToString(Reader["MOG_DESC"]);

            IdFormato = Convert.ToDecimal(Reader["IdFormato"]);
            Formato = Convert.ToString(Reader["Formato"]);

            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
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
        }

        public BEREC_MOD_GROUP(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            MOG_ID = Convert.ToString(Reader["MOG_ID"]);
            MOG_DESC = Convert.ToString(Reader["MOG_DESC"]);

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

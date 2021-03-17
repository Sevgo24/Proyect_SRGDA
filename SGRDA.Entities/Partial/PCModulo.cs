using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BEModulo
    {
        public List<BEModulo> Modulo { get; set; }
        public BEModulo() { }

        public BEModulo(IDataReader Reader)
        {
            //RowNumber = Convert.ToInt32(Reader["RowNumber"]);
            OWNER = Convert.ToString(Reader["OWNER"]);
            PROC_MOD = Convert.ToDecimal(Reader["PROC_MOD"]);
            MOD_DESC = Convert.ToString(Reader["MOD_DESC"]);
            MOD_CLABEL = Convert.ToString(Reader["MOD_CLABEL"]);
            MOD_CAPIKEY = Convert.ToString(Reader["MOD_CAPIKEY"]);
            MOD_CSECRETKEY = Convert.ToString(Reader["MOD_CSECRETKEY"]);
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
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDATE"]);
        }

        public BEModulo(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            PROC_MOD = Convert.ToDecimal(Reader["PROC_MOD"]);
            MOD_DESC = Convert.ToString(Reader["MOD_DESC"]);
            MOD_CLABEL = Convert.ToString(Reader["MOD_CLABEL"]);
            MOD_CAPIKEY = Convert.ToString(Reader["MOD_CAPIKEY"]);
            MOD_CSECRETKEY = Convert.ToString(Reader["MOD_CSECRETKEY"]);

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

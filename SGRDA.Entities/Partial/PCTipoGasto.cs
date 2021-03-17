using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BETipoGasto
    {
        public List<BETipoGasto> TipoGasto { get; set; }
        public BETipoGasto() { }

        public BETipoGasto(IDataReader Reader)
        {
            RowNumber = Convert.ToInt32(Reader["RowNumber"]);
            OWNER = Convert.ToString(Reader["OWNER"]);
            EXP_TYPE = Convert.ToString(Reader["EXP_TYPE"]);
            EXPT_DESC = Convert.ToString(Reader["EXPT_DESC"]);
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
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDAT"]);
        }

        public BETipoGasto(IDataReader Reader, int flag)
        {
            RowNumber = Convert.ToInt32(Reader["RowNumber"]);
            OWNER = Convert.ToString(Reader["OWNER"]);
            EXP_TYPE = Convert.ToString(Reader["EXP_TYPE"]);
            EXPT_DESC = Convert.ToString(Reader["EXPT_DESC"]);

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
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDAT"]);

            TotalVirtual = flag;
        }
    }
}

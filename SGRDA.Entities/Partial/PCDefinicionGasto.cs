using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BEDefinicionGasto
    {
        public List<BEDefinicionGasto> DefinicionGasto { get; set; }
        public BEDefinicionGasto() { }

        public BEDefinicionGasto(IDataReader Reader)
        {
            RowNumber = Convert.ToInt32(Reader["RowNumber"]);
            OWNER = Convert.ToString(Reader["OWNER"]);
            EXP_ID = Convert.ToString(Reader["EXP_ID"]);
            EXP_TYPE = Convert.ToString(Reader["EXP_TYPE"]);
            EXPT_DESC = Convert.ToString(Reader["EXPT_DESC"]);
            EXPG_ID = Convert.ToString(Reader["EXPG_ID"]);
            EXPG_DESC = Convert.ToString(Reader["EXPG_DESC"]);
            EXP_DESC = Convert.ToString(Reader["EXP_DESC"]);
            EXP_ACC = Convert.ToString(Reader["EXP_ACC"]);
            EXP_APR = Convert.ToDecimal(Reader["EXP_APR"]);

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
            LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDAT"]);
        }

        public BEDefinicionGasto(IDataReader Reader, int flag)
        {
            RowNumber = Convert.ToInt32(Reader["RowNumber"]);
            OWNER = Convert.ToString(Reader["OWNER"]);
            EXP_ID = Convert.ToString(Reader["EXP_ID"]);
            EXP_TYPE = Convert.ToString(Reader["EXP_TYPE"]);
            EXPT_DESC = Convert.ToString(Reader["EXPT_DESC"]);
            EXPG_ID = Convert.ToString(Reader["EXPG_ID"]);
            EXPG_DESC = Convert.ToString(Reader["EXPG_DESC"]);
            EXP_DESC = Convert.ToString(Reader["EXP_DESC"]);

            if (!DBNull.Value.Equals(Reader["EXP_ACC"]))
            {
                EXP_ACC = Convert.ToString(Reader["EXP_ACC"]);
            }

            if (!DBNull.Value.Equals(Reader["EXP_APR"]))
            {
                EXP_APR = Convert.ToDecimal(Reader["EXP_APR"]);
            }

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

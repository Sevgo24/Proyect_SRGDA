using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BECodigoPostal
    {
        public List<BECodigoPostal> Codigo_Postal { get; set; }
        public BECodigoPostal() { }

        public BECodigoPostal(IDataReader Reader)
        {
            CPO_ID = Convert.ToDecimal(Reader["CPO_ID"]);
            TIS_N = Convert.ToDecimal(Reader["TIS_N"]);
            POSITIONS = Convert.ToDecimal(Reader["POSITIONS"]);
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

        public BECodigoPostal(IDataReader Reader, int flag)
        {
            CPO_ID = Convert.ToDecimal(Reader["CPO_ID"]);
            TIS_N = Convert.ToDecimal(Reader["TIS_N"]);
            DAD_VNAME = Convert.ToString(Reader["DAD_VNAME"]);
            POSITIONS = Convert.ToDecimal(Reader["POSITIONS"]);

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

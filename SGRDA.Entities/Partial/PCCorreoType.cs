using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BECorreoType
    {
       public List<BECorreoType> Correo { get; set; }
       public BECorreoType() { }

       public BECorreoType(IDataReader Reader)
       {
           OWNER = Convert.ToString(Reader["OWNER"]);
           MAIL_TYPE = Convert.ToDecimal(Reader["MAIL_TYPE"]);
           MAIL_TDESC = Convert.ToString(Reader["MAIL_TDESC"]);
           MAIL_OBSERV = Convert.ToString(Reader["MAIL_OBSERV"]);
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

       public BECorreoType(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            MAIL_TYPE = Convert.ToDecimal(Reader["MAIL_TYPE"]);
            MAIL_TDESC = Convert.ToString(Reader["MAIL_TDESC"]);
            MAIL_OBSERV = Convert.ToString(Reader["MAIL_OBSERV"]);

            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
                ACTIVO = "I";
            }
            else
            {
                ACTIVO = "A";
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

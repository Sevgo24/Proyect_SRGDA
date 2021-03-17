using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BETelefonoType
    {
       public List<BETelefonoType> Telefono { get; set; }
       public BETelefonoType() { }

       public BETelefonoType(IDataReader Reader)
       {
           OWNER = Convert.ToString(Reader["OWNER"]);
           PHONE_TYPE = Convert.ToDecimal(Reader["PHONE_TYPE"]);
           PHONE_TDESC = Convert.ToString(Reader["PHONE_TDESC"]);
           ENDS = Convert.ToDateTime(Reader["ENDS"]);

           if (!DBNull.Value.Equals(Reader["PHONE_TOBSERV"]))
           {
               PHONE_TOBSERV = Convert.ToString(Reader["PHONE_TOBSERV"]);
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

       public BETelefonoType(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            PHONE_TYPE = Convert.ToDecimal(Reader["PHONE_TYPE"]);
            PHONE_TDESC = Convert.ToString(Reader["PHONE_TDESC"]).ToUpper();
            if (!DBNull.Value.Equals(Reader["PHONE_TOBSERV"]))
            {
                PHONE_TOBSERV = Convert.ToString(Reader["PHONE_TOBSERV"]);
            }
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

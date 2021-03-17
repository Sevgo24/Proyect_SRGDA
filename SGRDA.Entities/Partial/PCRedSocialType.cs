using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BERedSocialType
    {
       public List<BERedSocialType> TipoRedSocial { get; set; }
       public BERedSocialType() { }

       public BERedSocialType(IDataReader Reader)
       {
           OWNER = Convert.ToString(Reader["OWNER"]);
           CONT_TYPE = Convert.ToDecimal(Reader["CONT_TYPE"]);
           CONT_TDESC = Convert.ToString(Reader["CONT_TDESC"]);
           CONT_OBSERV = Convert.ToString(Reader["CONT_OBSERV"]);
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

       public BERedSocialType(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            CONT_TYPE = Convert.ToDecimal(Reader["CONT_TYPE"]);
            CONT_TDESC = Convert.ToString(Reader["CONT_TDESC"]);
            CONT_OBSERV = Convert.ToString(Reader["CONT_OBSERV"]);

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

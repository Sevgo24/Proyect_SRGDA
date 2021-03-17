using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEEstablecimiento
    {
        public List<BEEstablecimiento> Establecimiento { get; set; }
        public BEEstablecimiento() { }

        public BEEstablecimiento(IDataReader Reader)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            EST_NAME = Convert.ToString(Reader["EST_NAME"]);
            EST_ID = Convert.ToDecimal(Reader["EST_ID"]);
            if (!DBNull.Value.Equals(Reader["DAD_ID"]))
                DAD_ID = Convert.ToDecimal(Reader["DAD_ID"]);
            ESTT_ID = Convert.ToDecimal(Reader["ESTT_ID"]);
            SUBE_ID = Convert.ToDecimal(Reader["SUBE_ID"]);
            //TAXN_NAME = Convert.ToString(Reader["TAXN_NAME"]);
            //TAX_ID = Convert.ToString(Reader["TAX_ID"]);
            BPS_NAME = Convert.ToString(Reader["BPS_NAME"]);
            EST_TYPE = Convert.ToString(Reader["EST_TYPE"]);
            EST_SUBTYPE = Convert.ToString(Reader["EST_SUBTYPE"]);
            ADDRESS = Convert.ToString(Reader["ADDRESS"]);
            if (!DBNull.Value.Equals(Reader["MAIN_ADD"]))
                MAIN_ADD = Convert.ToString(Reader["MAIN_ADD"]);
            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            if (!DBNull.Value.Equals(Reader["LOG_DATE_UPDATE"]))
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_UPDATE"]);
            if (!DBNull.Value.Equals(Reader["ENDS"]))
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDAT = Convert.ToString(Reader["LOG_USER_UPDAT"]);
        }

        public BEEstablecimiento(IDataReader Reader, int flag)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            EST_NAME = Convert.ToString(Reader["EST_NAME"]);
            EST_ID = Convert.ToDecimal(Reader["EST_ID"]);
            if (!DBNull.Value.Equals(Reader["DAD_ID"]))
                DAD_ID = Convert.ToDecimal(Reader["DAD_ID"]);
            ESTT_ID = Convert.ToDecimal(Reader["ESTT_ID"]);
            SUBE_ID = Convert.ToDecimal(Reader["SUBE_ID"]);
            //TAXN_NAME = Convert.ToString(Reader["TAXN_NAME"]);
            //TAX_ID = Convert.ToString(Reader["TAX_ID"]);
            BPS_NAME = Convert.ToString(Reader["BPS_NAME"]);
            EST_TYPE = Convert.ToString(Reader["EST_TYPE"]);
            EST_SUBTYPE = Convert.ToString(Reader["EST_SUBTYPE"]);
            ADDRESS = Convert.ToString(Reader["ADDRESS"]);
            if (!DBNull.Value.Equals(Reader["MAIN_ADD"]))
                MAIN_ADD = Convert.ToString(Reader["MAIN_ADD"]);
            if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
                LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            if (!DBNull.Value.Equals(Reader["LOG_DATE_UPDATE"]))
                LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_UPDATE"]);
            if (!DBNull.Value.Equals(Reader["ENDS"]))
            {
                ENDS = Convert.ToDateTime(Reader["ENDS"]);
                Activo = "I";
            }
            else
            {
                Activo = "A";
            }
            LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            LOG_USER_UPDAT = Convert.ToString(Reader["LOG_USER_UPDAT"]);
            if (!DBNull.Value.Equals(Reader["UBIGEO"]))
            {
                UBIGEO = Convert.ToString(Reader["UBIGEO"]);
            }
            else
            {
                UBIGEO = "";
            }
            TotalVirtual = flag;
        }

        //Lista Establecimientos

        public BEEstablecimiento(IDataReader Reader, int flag, int id)
        {
            OWNER = Convert.ToString(Reader["OWNER"]);
            EST_NAME = Convert.ToString(Reader["EST_NAME"]);
            EST_ID = Convert.ToDecimal(Reader["EST_ID"]);
            if (!DBNull.Value.Equals(Reader["DAD_ID"]))
                DAD_ID = Convert.ToDecimal(Reader["DAD_ID"]);
            ESTT_ID = Convert.ToDecimal(Reader["ESTT_ID"]);
            SUBE_ID = Convert.ToDecimal(Reader["SUBE_ID"]);
            ESTT_ID = Convert.ToDecimal(Reader["ESTT_ID"]);
            TotalVirtual = flag;
        }
    }
}

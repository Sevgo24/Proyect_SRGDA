using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BEREF_CREATION_CLASS
    {
        public List<BEREF_CREATION_CLASS> REFCREATIONCLASS { get; set; }
        public BEREF_CREATION_CLASS() { }

        public BEREF_CREATION_CLASS(IDataReader Reader)
        {
            CLASS_COD = Convert.ToString(Reader["CLASS_COD"]);
            CLASS_DESC = Convert.ToString(Reader["CLASS_DESC"]);
            COD_PARENT_CLASS = Convert.ToString(Reader["COD_PARENT_CLASS"]);

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

        public BEREF_CREATION_CLASS(IDataReader Reader, int flag)
        {
            CLASS_COD = Convert.ToString(Reader["CLASS_COD"]);
            CLASS_DESC = Convert.ToString(Reader["CLASS_DESC"]);
            COD_PARENT_CLASS = Convert.ToString(Reader["COD_PARENT_CLASS"]);

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

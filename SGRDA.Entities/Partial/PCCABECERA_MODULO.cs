using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class CABECERA_MODULO
    {
        public List<CABECERA_MODULO> CABECERA { get; set; }
        public CABECERA_MODULO() { }

        public CABECERA_MODULO(IDataReader Reader)
        {
            CABE_ICODIGO_MODULO = Convert.ToInt32(Reader["CABE_ICODIGO_MODULO"]);
            CABE_VNOMBRE_MODULO = Convert.ToString(Reader["CABE_VNOMBRE_MODULO"]);
            CABE_CACTIVO_MODULO = Convert.ToString(Reader["CABE_CACTIVO_MODULO"]);

            //if (!DBNull.Value.Equals(Reader["LOG_DATE_CREAT"]))
            //{
            //    LOG_DATE_CREAT = Convert.ToDateTime(Reader["LOG_DATE_CREAT"]);
            //}

            //if (!DBNull.Value.Equals(Reader["LOG_DATE_UPDATE"]))
            //{
            //    LOG_DATE_UPDATE = Convert.ToDateTime(Reader["LOG_DATE_UPDATE"]);
            //}


            //LOG_USER_CREAT = Convert.ToString(Reader["LOG_USER_CREAT"]);
            //LOG_USER_UPDATE = Convert.ToString(Reader["LOG_USER_UPDATE"]);
        }
    }
}

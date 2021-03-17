using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class ROLES_PERMISOS
    {
        public List<ROLES_PERMISOS> ROL_PERMISOS { get; set; }
        public ROLES_PERMISOS() { }

        public ROLES_PERMISOS(IDataReader Reader)
        {
            MODU_ICODIGO_MODULO = Convert.ToInt32(Reader["MODU_ICODIGO_MODULO"]);
            ROL_ICODIGO_ROL = Convert.ToInt32(Reader["ROL_ICODIGO_ROL"]);
            ROL_VDESCRIPCION_ROL = Convert.ToString(Reader["ROL_VDESCRIPCION_ROL"]);
            CABE_ICODIGO_MODULO = Convert.ToInt32(Reader["CABE_ICODIGO_MODULO"]);
            MODU_INIVEL_MODULO = Convert.ToInt32(Reader["MODU_INIVEL_MODULO"]);
            MODU_VNOMBRE_MODULO = Convert.ToString(Reader["MODU_VNOMBRE_MODULO"]);
            MODU_VRUTA_PAGINA = Convert.ToString(Reader["MODU_VRUTA_PAGINA"]);
            MODU_VDESCRIPCION_MODULO = Convert.ToString(Reader["MODU_VDESCRIPCION_MODULO"]);
            ROMO_CACTIVO = Convert.ToString(Reader["ROMO_CACTIVO"]);

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

        public ROLES_PERMISOS(IDataReader Reader, int flag)
        {
            MODU_ICODIGO_MODULO = Convert.ToInt32(Reader["MODU_ICODIGO_MODULO"]);
            ROL_ICODIGO_ROL = Convert.ToInt32(Reader["ROL_ICODIGO_ROL"]);
            ROL_VDESCRIPCION_ROL = Convert.ToString(Reader["ROL_VDESCRIPCION_ROL"]);
            CABE_ICODIGO_MODULO = Convert.ToInt32(Reader["CABE_ICODIGO_MODULO"]);
            MODU_INIVEL_MODULO = Convert.ToInt32(Reader["MODU_INIVEL_MODULO"]);
            MODU_VNOMBRE_MODULO = Convert.ToString(Reader["MODU_VNOMBRE_MODULO"]);
            MODU_VRUTA_PAGINA = Convert.ToString(Reader["MODU_VRUTA_PAGINA"]);
            MODU_VDESCRIPCION_MODULO = Convert.ToString(Reader["MODU_VDESCRIPCION_MODULO"]);
            ROMO_CACTIVO = Convert.ToString(Reader["ROMO_CACTIVO"]);

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

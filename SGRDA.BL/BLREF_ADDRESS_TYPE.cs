using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLREF_ADDRESS_TYPE
    {
        public List<BEREF_ADDRESS_TYPE> ListarDirecciones()
        {
            return new DAREF_ADDRESS_TYPE().ListarDirecciones();
        }

        public BEREF_ADDRESS_TYPE Obtener(string owner, decimal ADDT_ID)
        {
            return new DAREF_ADDRESS_TYPE().Obtener(owner, ADDT_ID);
        }

        public List<BEREF_ADDRESS_TYPE> ListarPage(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREF_ADDRESS_TYPE().ListarPage(param, st, pagina, cantRegxPag);
        }

        public int Insertar(BEREF_ADDRESS_TYPE en)
        {
            BEREF_ADDRESS_TYPE item = new BEREF_ADDRESS_TYPE();
            item = new DAREF_ADDRESS_TYPE().Obtener(en.OWNER, en.ADDT_ID);
            if (item != null) return 0;
            return new DAREF_ADDRESS_TYPE().Insertar(en);
        }

        public bool existeTipoDirecciones(string Owner, string nombre)
        {
            return new DAREF_ADDRESS_TYPE().existeTipoDirecciones(Owner, nombre);
        }

        public bool existeTipoDirecciones(string Owner, decimal id, string nombre)
        {
            return new DAREF_ADDRESS_TYPE().existeTipoDirecciones(Owner, id, nombre);
        }

        public int Actualizar(BEREF_ADDRESS_TYPE en)
        {
            return new DAREF_ADDRESS_TYPE().Actualizar(en);
        }

        public int Eliminar(BEREF_ADDRESS_TYPE en)
        {
            return new DAREF_ADDRESS_TYPE().Eliminar(en);
        }

        public BEREF_ADDRESS_TYPE Obtiene(string owner, decimal idTipoDireccion)
        {
            return new DAREF_ADDRESS_TYPE().Obtiene(owner, idTipoDireccion);
        }
    }
}

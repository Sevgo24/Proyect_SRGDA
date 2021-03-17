using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLDirecciones
    {
        public List<BEDireccion> usp_Get_DireccionPage(decimal param, int pagina, int cantRegxPag)
        {
            return new DADirecciones().usp_Get_DireccionPage(param, pagina, cantRegxPag);
        }

        //public List<BEDireccion> USP_REC_ADDRESS_LISTAR(decimal BPS_ID)
        //{
        //    return new DADirecciones().USP_REC_ADDRESS_LISTAR(BPS_ID);
        //}

        public int usp_Upd_Direccion(BEDireccion direccion)
        {
            return new DADirecciones().Insertar(direccion);
        }

        public int usp_Ins_Direccion(BEDireccion direccion)
        {
            return new DADirecciones().Update(direccion);
        }

        public List<BEDireccion> DireccionXSucursales(string owner, string idSucursal, decimal idBank)
        {
            return new DADirecciones().DireccionXSucursales(owner, idSucursal, idBank);
        }

        public List<BEDireccion> Obtener(string owner, decimal id)
        {
            return new DADirecciones().ObtenerDirBPSAll(owner, id);
        }

        public BEDireccion ObtenerDireccionXId(string owner, decimal Id)
        {
            return new DADirecciones().ObtenerDireccionXId(owner, Id);
        }

        public BEDireccion ObtenerUbigeoSocio(string owner, decimal Id)
        {
            return new DADirecciones().ObtenerUbigeoSocio(owner, Id);
        }
    }
}

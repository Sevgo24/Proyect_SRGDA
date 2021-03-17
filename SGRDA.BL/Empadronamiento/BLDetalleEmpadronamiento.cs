using SGRDA.DA.Empadronamiento;
using SGRDA.Entities.Empadronamiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.BL.Empadronamiento
{
    public class BLDetalleEmpadronamiento
    {
        DADetalleEmpadronamiento DA = new DADetalleEmpadronamiento();
        public List<BEDetalleEmpadronamiento> ObtenerLista_Matriz_Detalle_EMPADRONAMIENTO(decimal LIC_ID)
        {
            return DA.ObtenerLista_Matriz_Detalle_EMPADRONAMIENTO(LIC_ID);
        }

        public string Nombre_x_Licencia(int LIC_ID)
        {
            return  DA.Nombre_x_Licencia(LIC_ID);
        }
    }
}

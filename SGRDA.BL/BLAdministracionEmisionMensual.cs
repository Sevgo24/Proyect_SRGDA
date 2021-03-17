using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLAdministracionEmisionMensual
    {

        public List<BEAdministracionEmisionMensual> lista(string NOMBRE_OFI,int DIA,string RANGO_INI,string RANGO_FIN ,int ESTADO)
        {
            return new DAAdministracionEmisionMensual().ListarOficinasEmisionMensual(NOMBRE_OFI, DIA, RANGO_INI, RANGO_FIN, ESTADO);
        }

        public BEAdministracionEmisionMensual Obtiene (decimal ID)
        {
            return new DAAdministracionEmisionMensual().Obtiene(ID);
        }

        public int InactivaEmisionMensual(decimal ID)
        {
            return new DAAdministracionEmisionMensual().InactivaEmisionMensual(ID);
        }

        public int ModificarEmisionMensual(decimal ID, string NOMBRE_OFI, int DIA, string RANGO_INI, string RANGO_FIN, decimal OFF_ID)
        {
            return new DAAdministracionEmisionMensual().ModificarEmisionMensual(ID,  NOMBRE_OFI,DIA, RANGO_INI,RANGO_FIN, OFF_ID);
        }
        public int ValidaEmisionMensual(decimal ID, int DIA, string FECHA_INI, string FECHA_FIN)
        {
            return new DAAdministracionEmisionMensual().ValidaEmisionMensual(ID,  DIA,  FECHA_INI,  FECHA_FIN);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Empadronamiento
{
    public class BETabla_Comision
    {
        public int ID_COMISION      { get; set; }
        public int ID_RANGO         { get; set; }
		public decimal MONTO_DESDE      { get; set; }
        public decimal MONTO_HASTA      { get; set; }
        public decimal PORCENTAJE       { get; set; }
        public string LOG_USER_CREATE { get; set; }
        public string Fecha_Creacion { get; set; }
        public string Desc_Rango    { get; set; }

    }
}

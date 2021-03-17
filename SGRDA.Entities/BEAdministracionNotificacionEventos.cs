using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEAdministracionNotificacionEventos
    {

        public decimal IdLicencia { get; set;}

        public string Observacion { get; set; }
        public DateTime FechaInicioEvento { get; set;}
        public DateTime FechaFinEvento { get; set; }
        public string DescripcionUbigeo { get; set;}
        public string NombreEstablecimiento { get; set; }
        public string DescripcionDireccion { get; set; }
        public string DescripcionEstado { get; set; }
        public decimal IdEstablecimiento { get; set; }
        public string DocumentosDescripcion { get; set; }


    }
}

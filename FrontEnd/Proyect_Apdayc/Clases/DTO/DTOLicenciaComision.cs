using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOLicenciaComision 
    {
        //
        // GET: /LicenciaComision/

        public decimal idComision { get; set; }
        public string Nombre { get; set; }
        public string TipoComision { get; set; }
        public string Descripcion { get; set; }
        public string Porcentaje { get; set; }
        public decimal Valor { get; set; }
        public string Fecha { get; set; }

        public bool Activo { get; set; }
    }
}

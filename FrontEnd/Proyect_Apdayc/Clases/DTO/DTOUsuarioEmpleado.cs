using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOUsuarioEmpleado
    {

          public decimal Codigo { get; set; }
          public string Cargo { get; set; }
          public DateTime? FechaIngreso { get; set; }
          public DateTime? FechaBaja { get; set; }

          public bool Activo { get; set; }

    }
}
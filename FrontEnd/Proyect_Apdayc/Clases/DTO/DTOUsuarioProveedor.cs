using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOUsuarioProveedor
    {

          public decimal Codigo { get; set; }
          public string  ActividadEcon {get; set; }
          public string LineaArticulo { get; set; }
          public bool Activo { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOUsuarioDerecho
    {

          public decimal Codigo { get; set; }
          public string TipoPago { get; set; }
          public decimal?  GrupoEmpresarial{ get; set; }
          public decimal? DescuentoPer { get; set; }
          public decimal? DescuentoMonto { get; set; }
          public string RazonSocial { get; set; }
          public string Nombres { get; set; }
          public string ApellidoPaterno { get; set; }
          public string ApllidoMaterno { get; set; }
          public string GrupoEmpDescc { get; set; }
          public string DescuentoMotivo { get; set; }
          public string CuentaContable { get; set; }
          public bool Activo { get; set; }

          public string Partida { get; set; }
          public string Zona { get; set; }
          public string Sede { get; set; }
 
         



    }
}
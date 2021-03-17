using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOPlantillaDescuentoMatriz
    {
        public decimal Id { get; set; }
        public decimal IdPlantillaDescuento { get; set; }
        public decimal ValId_1 { get; set; }
        public string Val_Desc1 { get; set; }
        public decimal ValId_2 { get; set; }
        public string Val_Desc2 { get; set; }
        public decimal ValId_3 { get; set; }
        public string Val_Desc3 { get; set; }
        public decimal Formula { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }

        public bool EnBD { get; set; }
        public bool Activo { get; set; }

    }
}
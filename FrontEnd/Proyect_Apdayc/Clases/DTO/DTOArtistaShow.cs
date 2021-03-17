using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOArtistaShow
    {
        public decimal CodigoShow { get; set; }
        public string CodigoArtista { get; set; }
        public string Principal { get; set; }
        public string NombreArtista { get; set; }
        public int Tipo { get; set; }
        public string Observacion { get; set; }
    }
}
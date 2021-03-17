using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOAgente
    {
        public decimal Id { get; set; }
        public decimal Codigo { get; set; }
        public decimal IdAgente { get; set; }
        public string NombreAgente { get; set; }
        public string Descripcion { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }

        public bool EnBD { get; set; }
        public bool Activo { get; set; }
    }
}
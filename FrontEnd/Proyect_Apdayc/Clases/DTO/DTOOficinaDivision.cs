using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOOficinaDivision
    {
        public string owner { get; set; }
        public decimal Id { get; set; }
        public decimal IdOficina{ get; set; }
        public decimal idDivision { get; set; }
        public string Division { get; set; }
        public string Detalle { get; set; }
        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        public bool EnBD { get; set; }
        public bool Activo { get; set; }
    }
}
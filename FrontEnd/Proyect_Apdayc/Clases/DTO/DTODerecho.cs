using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTODerecho
    {
        public decimal Id { get; set; }
        public string IdDerecho { get; set; }
        public string auxIdDerecho { get; set; }
        public string Derecho { get; set; }

        public string IdClase { get; set; }
        public string Clase { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }

        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }
        public bool GetTipo { get; set; }
        public bool IsDuplicate { get; set; }
    }
}
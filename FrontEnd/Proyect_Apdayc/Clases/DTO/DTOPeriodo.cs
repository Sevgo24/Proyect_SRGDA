using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOPeriodo
    {
        public decimal Id { get; set; }
        public decimal NroordenPeriodo { get; set; }
        public decimal NroordenPeriodoAnt { get; set; }    
        public string NombrePeriodo { get; set; }
        public decimal NrodiasPeriodo { get; set; }
        public Nullable<DateTime> Fecha { get; set; }
        public string FechaString { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }

        public decimal Accion { get; set; }
        public decimal Tmp { get; set; }
    }
}
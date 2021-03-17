using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOAjusteComision
    {
        public decimal Id { get; set; }
        public decimal IdAgente { get; set; }
        public string Agente { get; set; }
        public decimal IdLicencia { get; set; }
        public decimal IdOrigenComision { get; set; }
        public decimal ValorComision { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        /// public decimal Correlativo { get; set; }


        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }
    }
}
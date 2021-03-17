using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOTarifaValor
    {
        public decimal Id { get; set; }
        public decimal IdCaracteristica { get; set; }
        public decimal CharId { get; set; }
        public decimal Desde { get; set; }
        public decimal Hasta { get; set; }
        public string  Descripcion { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        public string Tramo { get; set; }
        public string Caracteristica { get; set; }
        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }
        public decimal SequenceChar { get; set; }
    }
}
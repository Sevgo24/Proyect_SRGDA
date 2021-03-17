using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOCampaniaLoteCliente
    {
        public decimal Id { get; set; }
        public decimal IdClienteLote { get; set; }
        public decimal? IdCliente { get; set; }
        public string Nombre { get; set; }
        public decimal IdTipoObservacion { get; set; }
        public string TipoObservacion { get; set; }
        public string Observacion { get; set; }
        public decimal? ValorExpectativa { get; set; }
        public decimal? ValorReal { get; set; }
        public string RutaDoc { get; set; }
        public decimal IdDoc { get; set; }
        public decimal? IdObs { get; set; }

        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public string FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }

        public byte[] Contenido { get; set; }
    }
}
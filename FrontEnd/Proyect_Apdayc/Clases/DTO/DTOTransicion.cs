using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOTransicion
    {
        public decimal Id { get; set; }
        public decimal IdWorkFlow { get; set; }
        public decimal? IdEstadoInicial { get; set; }
        public decimal? IdEstadoFinal { get; set; }
        public decimal? IdEvento { get; set; }
        public string EstadoInicial { get; set; }
        public string EstadoFinal { get; set; }
        public string Evento { get; set; }
        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }

        public bool EnBD { get; set; }
        public bool Activo { get; set; }
    }
}
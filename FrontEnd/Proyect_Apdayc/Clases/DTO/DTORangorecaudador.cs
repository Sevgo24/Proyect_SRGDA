using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTORangorecaudador
    {
        public decimal IdPrograma { get; set; }
        public decimal Orden { get; set; }
        public decimal ValorInicial { get; set; }
        public decimal ValorFinal { get; set; }
        public string FormatoComision { get; set; }
        public decimal? ValorComisionAdicional { get; set; }
        public decimal? PorcentajeAdicional { get; set; }
        public decimal sequence { get; set; }
        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        public decimal IdRepresentante { get; set; }
        public bool EnBD { get; set; }
        public bool Activo { get; set; }
    }
}
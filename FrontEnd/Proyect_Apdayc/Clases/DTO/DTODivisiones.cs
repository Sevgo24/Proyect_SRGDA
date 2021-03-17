using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTODivisiones
    {
        public string owner { get; set; }
        public decimal Id { get; set; }
        public decimal idEstablecimiento { get; set; }
        public string idTipoDivision { get; set; }
        public string TipoDivision { get; set; }
        public decimal idDivision { get; set; }
        public string Division { get; set; }
        public decimal idSubTipoDivision { get; set; }
        public string SubTipoDivision { get; set; }
        public decimal idDivisionValor { get; set; }
        public decimal auxidDivisionValor { get; set; }
        public string DivisionValor { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }

        public bool EnBD { get; set; }
        public bool Activo { get; set; }
    }
}
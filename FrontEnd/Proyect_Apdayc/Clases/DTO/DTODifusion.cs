using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTODifusion
    {
        public string owner { get; set; }
        public decimal Id { get; set; }//sequence
        public decimal idEstablecimiento { get; set; }
        public decimal idDifusion { get; set; }
        public string Difusion { get; set; }
        public decimal NroDifusion { get; set; }
        public bool almacenamiento { get; set; }
        public string almacenamientoDes { get; set; }
        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }

        public bool EnBD { get; set; }
        public bool Activo { get; set; }
    }   
}
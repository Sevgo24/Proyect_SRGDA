using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOTarifaReglaData
    {
        public decimal Id { get; set; }
        public decimal IdPlantilla { get; set; }     
        public decimal IdRegla { get; set; }        
        public decimal IdCaracteristica { get; set; }
        public string OrigenCaracteristica { get; set; }
        public string Letra { get; set; }
        public string DesCaracteristica { get; set; }
        public decimal Tramo { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }
    }
}
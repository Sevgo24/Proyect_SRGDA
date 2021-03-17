using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOCaracteristica
    {
        public decimal Id { get; set; }
        public string Idcaracteristica { get; set; }
        public string IdEstablecimiento { get; set; }
        public string TipoEstablecimiento { get; set; }
        public string IdSubTipoEstablecimiento { get; set; }
        public string SubTipoEstablecimiento { get; set; }
        public string caracteristica { get; set; }
        public string Valor { get; set; }
        public string Fecha { get; set; }
        public string usercreate { get; set; }

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
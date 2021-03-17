using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOCaracteristicaPredefinida
    {
        public decimal Id { get; set; }
        public string TipoEstablecimiento { get; set; }
        public string IdSubTipoEstablecimiento { get; set; }
        public string caracteristica { get; set; }

        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }
        public bool GetTipo { get; set; }
    }
}
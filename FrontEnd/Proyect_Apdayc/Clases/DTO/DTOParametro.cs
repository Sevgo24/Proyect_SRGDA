using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOParametro
    {

        public decimal Id { get; set; }
        public string TipoParametro { get; set; }
        public string TipoParametroDesc { get; set; }
        public string Descripcion { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }

        public decimal IdSubTipoParametro { get; set; }
        public string SubTipoParametroDesc { get; set; }
    }
}


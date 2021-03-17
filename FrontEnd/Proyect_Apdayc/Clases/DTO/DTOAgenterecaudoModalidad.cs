using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOAgenterecaudoModalidad
    {
        public decimal Codigo { get; set; }
        public decimal IDAgenteRecaudo { get; set; }
        public decimal IdModalidad { get; set; }


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
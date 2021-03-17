﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOTelefono
    {
        //public string  Owner { get; set; }
        //public decimal IdBps { get; set; }
        public decimal IdTipo { get; set; }

        public decimal Id { get; set; }

        public string TipoDesc { get; set; }
        public string Observacion { get; set; }
        public string Numero { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
     ///   public decimal BpsId { get; set; }

  

       /// public decimal Correlativo { get; set; }

        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }
    }
}
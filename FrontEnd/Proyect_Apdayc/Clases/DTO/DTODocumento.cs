using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTODocumento
    {

  
        public decimal Id { get; set; }
        public string TipoDocumento { get; set; }
        public string Archivo { get; set; }
        public string TipoDocumentoDesc { get; set; }
        public string FechaRecepcion { get; set; }

        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
         
        public byte[] Contenido { get; set; }

        public decimal IdContactoLlamada { get; set; }

        public string codigo_alfresco { get; set; }
        public Stream ArchivoBytes { get; set; }
    }
}
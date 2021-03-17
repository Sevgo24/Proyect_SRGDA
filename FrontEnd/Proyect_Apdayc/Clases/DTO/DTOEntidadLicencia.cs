using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOEntidadLicencia
    {
        public string owner { get; set; }
        public decimal Id { get; set; }
        public decimal IdBps { get; set; }
        public decimal IdLicencia { get; set; }        
        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }

        public bool EnBD { get; set; }
        public bool Activo { get; set; }

        /// <summary>
        /// Nombre del IdBps (código la entidad socio de negocio)
        /// NroDocumento (nro de documento de la entidad socio de negocio)
        /// TipoDocumento (tipo documento de la entidad socio de negocio)
        /// </summary>
        public string Nombre { get; set; }
        public string NroDocumento { get; set; }
        public decimal TipoDocumento { get; set; }
    }
}
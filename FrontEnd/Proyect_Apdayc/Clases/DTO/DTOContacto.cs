using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOContacto
    {
        public decimal Id { get; set; }
        public string Idsucursal { get; set; }
        public string contacto { get; set; }
        public decimal idRol { get; set; }
        public decimal idDocumento { get; set; }

        public string Documento { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public decimal idBanco { get; set; }
        public string usercreate { get; set; }


        public string IdEstablecimiento { get; set; }

        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }
    }
}
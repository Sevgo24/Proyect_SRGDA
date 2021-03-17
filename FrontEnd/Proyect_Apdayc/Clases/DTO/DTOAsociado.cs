using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOAsociado 
    {
        public decimal Id { get; set; }
        public decimal Codigo { get; set; }
        public decimal CodigoAsociado { get; set; }
        public string NroDocAsociado { get; set; }
        public string NombreAsociado { get; set; }
        public decimal RolTipo { get; set; }
        public string RolTipoDesc { get; set; }

        public string Perfil { get; set; }
        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        public bool Accion { get; set; }
        public bool EnBD { get; set; }
        public bool Activo { get; set; }
        public string EsContacto { get; set; }
        public Boolean EsPrincipal { get; set; }
    }
}

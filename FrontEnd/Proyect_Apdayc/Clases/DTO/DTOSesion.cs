using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOSesion
    {
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string CodigoUsuarioOficina { get; set; }
        public string CodigoOficina { get; set; }
        public string Oficina { get; set; }
        public string CodigoPerfil { get; set; }
        public string Perfil { get; set; }
        public string CodigoPerdilUsuario { get; set; }
        public string DetallePerfil { get; set; }
        public int result { get; set; }
        public string message { get; set; }
        public string TipoAcceso { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEAdministracionSocioMoroso
    {
        public  decimal CodigoSocioMoroso { get; set; }
        public  decimal CodigoSocio { get; set; }
        public  string Descripcion { get; set; }
        public  int Estado { get; set; }
        public string DescripcionEst { get; set; }
        public string DescripcionEstado { get; set; }
        public  string UsuarioCreacion { get; set; }
        public  string FechaCreacion { get; set; }
        public  string RazonSocial { get; set; }
        public  string DatosSocio { get; set; }
        public  string TipoSocio { get; set; }
        public  string DocumentoIdentificacion { get; set; }
        public string Socio { get; set; }
        public string FechaEvento { get; set; }
        public string Local { get; set; }
        public string Direccion { get; set; }
        public string NombreEvento { get; set; }
        public string NombreOficina { get; set; }
        public string Representante { get; set; }
        
    }
}

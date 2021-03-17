using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTODireccion
    {
        public decimal Id { get; set; }

        public decimal Nro { get; set; }
        public string RazonSocial { get; set; }
        public string Territorio { get; set; }
        public string CodigoUbigeo { get; set; }
        public string CodigoPostal { get; set; }
        public string Referencia { get; set; }

        
        public string Urbanizacion { get; set; }
        public string Numero { get; set; }
        public string Manzana { get; set; }
        public string Lote { get; set; }
        public string NroPiso { get; set; }
        public string Avenida { get; set; }
        public string Etapa { get; set; }
        
        
        public string TipoDireccion { get; set; }
        public string TipoAvenida { get; set; }
        public string TipoUrb { get; set; }
        public string TipoDepa { get; set; }
        public string TipoEtapa { get; set; }

        public string TipoPerfil { get; set; }
        public string TipoDireccionDesc { get; set; }
        public string TipoAvenidaDes { get; set; }
        public string TipoUrbDes { get; set; }
        public string TipoDepaDes { get; set; }
        public string TipoEtapaDes { get; set; }

        public string DescripcionUbigeo { get; set; }


        public string EsPrincipal { get; set; }

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
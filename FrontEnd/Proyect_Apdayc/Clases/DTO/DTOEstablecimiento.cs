using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOEstablecimiento
    {
        public decimal Codigo { get; set; }
        public decimal CodigoSocio { get; set; }
        public string SocioNombre { get; set; }
        public decimal CodigoTipoidentificacionfiscal { get; set; }
        public decimal? CodigoDivisionfiscal { get; set; }
        public string NumeroIdentificacionfiscal { get; set; }
        public decimal TipoEstablecimiento { get; set; }
        public decimal SubTipoestablecimiento { get; set; }
        public decimal Codigodivision { get; set; }
        public string Nombre { get; set; }
        public string value { get; set; }
        public bool Nuevo { get; set; }
        public decimal DivisionAdministrativaAsociado { get; set; }
        public decimal DivisionAdministrativaEstablecimiento { get; set; }
        public string Ubigeo { get; set; }

        public string Latitud { get; set; }
        public string Longitud { get; set; }
    }
}
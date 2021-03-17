using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEEmisionMensual
    {
        public decimal CodigoSocio { get; set; }
        public string DescripcionSocio { get; set; }
        public string DescripcionDocumentoSocio { get; set; }
        public string DescripcionTipoMoneda { get; set; }
        public string DescripcionMoneda { get; set; }
        public string DescripcionGrupoFacturacion { get; set; }
        public decimal CodigoGrupoFacturacion { get; set; }
        public string CodigoPermiteFacturacion { get; set; }
        public string DescripcionDireccionSocio { get; set; }
        public decimal MontoBrutoTotalSocioGrupo { get; set; }
        public decimal MontoDesctoTotalSocioGrupo { get; set; }
        public decimal MontoNetoTotalSocioGrupo { get; set; }
        public int CantidadLicenciasSocioGrupo { get; set; }
        public decimal CodigoLicencia { get; set; }
        public string DescripcionEstablecimiento { get; set; }
        public string DescripcionDireccionEstablecimiento { get; set; }
        public string DescripcionUbigeo { get; set; }
        public decimal MontoLicenciaBruto { get; set; }
        public decimal MontoLicenciaDscto { get; set; }
        public decimal MontoLicenciaNeto { get; set; }
        public decimal CodigoPeriodo { get; set; }
        public string DescripcionPeriodo { get; set; }
        public string EstadoPeriodo { get; set; }

        public decimal CodigoOficina { get; set; }


    }
}

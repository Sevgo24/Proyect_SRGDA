using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BELicencia
    {
        public string Owner { get; set; }
        public decimal IdRepresentante { get; set; }
        public decimal IdEstablecimiento { get; set; }
        public decimal? IdLicencia { get; set; }
        public string NombreLicencia { get; set; }
        public string RazonSocial { get; set; }
        public string DocRazonSocial { get; set; }
        public string NumRazonSocial { get; set; }
        public string GiroLocal { get; set; }
        public string NombreLocal { get; set; }
        public string DireccionLocal { get; set; }
        public string Representante { get; set; }
        public string DireccionRepresentante { get; set; }
        public string DocRepresentante { get; set; }
        public string NumRepresentante { get; set; }
        public string DireccionCobranza { get; set; }
        
        public string TelefonoRazonSocial { get; set; }
        public string FaxRazonSocial { get; set; }
        
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }

        //Valor unidad musical 
        public string Mes { get; set; }
        public string Anio { get; set; }
        public double Valor { get; set; }

        //Nivel de incidencia de la obra
        //me trae en una lista los niveles de incidencia musical
        public string NivelIncidencia { get; set; }

        //Obtener departamento, provincia, distrito
        public decimal? Tis_n_Representante { get; set; }
        public decimal? Geo_id_Representante { get; set; }
        public decimal? Tis_n_Establecimiento { get; set; }
        public decimal? Geo_id_Establecimiento { get; set; }

        public decimal? Tis_n_DirCobranza { get; set; }
        public decimal? Geo_id_DirCobranza { get; set; }

        public DateTime FechaCreacionLicencia { get; set; }

        public string DireccionRazonSocial { get; set; }
    }
}

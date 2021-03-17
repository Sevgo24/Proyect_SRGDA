using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOTarifaTest
    {
        public decimal Id { get; set; }        
        public decimal IdRegla { get; set; }
        public decimal IdPlantilla { get; set; }

        public decimal? IdVal_1 { get; set; }
        public decimal? IdCaracteristica1 { get; set; }
        public string Descripcion_1 { get; set; }
        public string Tramo_1 { get; set; }
        public decimal? Desde_1 { get; set; }
        public decimal? Hasta_1 { get; set; }

        public decimal? IdVal_2 { get; set; }
        public decimal? IdCaracteristica2 { get; set; }
        public string Descripcion_2 { get; set; }
        public string Tramo_2 { get; set; }
        public decimal? Desde_2 { get; set; }
        public decimal? Hasta_2 { get; set; }

        public decimal? IdVal_3 { get; set; }
        public decimal? IdCaracteristica3 { get; set; }
        public string Descripcion_3 { get; set; }
        public string Tramo_3 { get; set; }
        public decimal? Desde_3 { get; set; }
        public decimal? Hasta_3 { get; set; }

        public decimal? IdVal_4 { get; set; }
        public decimal? IdCaracteristica4 { get; set; }
        public string Descripcion_4 { get; set; }
        public string Tramo_4 { get; set; }
        public decimal? Desde_4 { get; set; }
        public decimal? Hasta_4 { get; set; }

        public decimal? IdVal_5 { get; set; }
        public decimal? IdCaracteristica5 { get; set; }
        public string Descripcion_5 { get; set; }
        public string Tramo_5 { get; set; }
        public decimal? Desde_5 { get; set; }
        public decimal? Hasta_5 { get; set; }

        public decimal? Tarifa { get; set; }
        public decimal? Minimo { get; set; }
        
        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }

    }
}
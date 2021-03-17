using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOFormatoFactura
    {
        public decimal Id { get; set; }
        public decimal IdFormato { get; set; }
        public decimal IdFormatoAnt { get; set; }
        public string IdGrupo { get; set; }
        public string Grupo { get; set; }
        public string Formato { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDAT { get; set; }


        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }
        public bool GetTipo { get; set; }
    }
}
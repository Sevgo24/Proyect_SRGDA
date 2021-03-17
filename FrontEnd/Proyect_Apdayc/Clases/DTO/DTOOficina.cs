using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOOficina
    {
        public string OWNER { get; set; }
        public decimal OFF_ID { get; set; }
        public string OFF_NAME { get; set; }
        public string HQ_IND { get; set; }
        public decimal SOFF_ID { get; set; }
        public decimal ADD_ID { get; set; }
        public decimal OFF_TYPE { get; set; }
        public string OFF_CC { get; set; }
        public decimal BPS_ID { get; set; }
        public string SOCIO { get; set; }
        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }
        public decimal OFF_ID_PRE { get; set; }
    }
}
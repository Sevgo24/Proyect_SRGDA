using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOInspection
    {
        public decimal Id { get; set; }
        public decimal IdEstablecimiento { get; set; }
        public string IdDocumentoInspeccion { get; set; }
        public string FechaInspeccion { get; set; }
        public string HoraInspeccion { get; set; }
        public string Observacion { get; set; }
        public string Documento { get; set; }
        public decimal Funcionario { get; set; }//BPS_ID

        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }
    }
}
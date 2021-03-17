using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOHistorico 
    {
        public int IdOficina { get; set; }
        public string OficinaDesc { get; set; }
        public decimal IdLevel { get; set; }
        public string LevelDesc { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

    }
}

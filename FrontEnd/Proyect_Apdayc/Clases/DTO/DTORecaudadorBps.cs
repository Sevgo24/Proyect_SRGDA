using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTORecaudadorBps
    {
        public decimal Id { get; set; }
        public string Taxn_name { get; set; }
        public string Tax_id { get; set; }
        public string Bps_name { get; set; }
        public string nivel { get; set; }
        public bool Principal { get; set; }
    }
}
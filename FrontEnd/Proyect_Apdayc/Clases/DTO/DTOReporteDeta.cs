using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOReporteDeta
    {
        
               public decimal CodigoReporteCab{get;set;}
               public decimal  CodigoReporteDeta{get;set;}
                public string Titulo{get;set;}
                 public string Show {get;set;}
                 public string AutorA {get;set;}
                 public string AutorB {get;set;}
                 public   decimal TotalDetalle {get;set;}
                 public   decimal TotalEjecucion {get;set;}
               public string  Fecha {get;set;}
               //public string Fecha2 { get; set; }
                public string CodigoMoneda {get;set;}
                public string Owner { get; set; }
                public bool  Activo { get; set; }

                public decimal? DuracionSeg { get; set; }
                public decimal? DuracionMin { get; set; }
                public decimal? DuracionSegTotal { get; set; }

    }
}
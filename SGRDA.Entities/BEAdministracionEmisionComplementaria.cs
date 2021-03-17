using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEAdministracionEmisionComplementaria
    {
        public string OWNER { get; set; }
        public decimal CodigoEmisionComplementaria { get; set;}
        public string NombreOficina {get;set;}
        public int  Estado{get;set;}
        public string DescripcionEstado { get; set; }
        public int CantidadPeriodos{get;set;}
        public int CantidadLicencias {get;set;}
        public int CantidadDocumentos { get; set; }
        public string  NombreEmisionComplementaria{get;set;}
        public string  RespuestaEmisionComplementaria{get;set;}           
        public string  UsuarioCreacion{get;set;}
        public string  UsuarioActualizacion{get;set;}
        public DateTime Ends  {get;set;}
        public DateTime FechaCreacion {get;set;}
        public DateTime FechaActualizacion {get;set;}
        public string FechaProcesado { get; set; }
        public decimal CodigoOficina { get; set; }

    }
}

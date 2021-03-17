using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEAdministracionEmisionComplementariaDetalle
    {
       public string OWNER { get; set; }
       public decimal CodigoEmisionComplementariaDet {get;set;}
       public decimal CodigoEmisionComplementariaCab{get;set;}
       public decimal CodigoLicencia{get;set;}
       public decimal CodigoPeriodo{get;set;}
       public int EstadoDet { get; set; }
       public string RespuestaEmisionCompleDet {get;set;}
       public string UsuarioCreacionCompDet{get;set;} 
       public string UsuarioActualizacionCompDet{get;set;}
       public int EndsDet{get;set;}
       public DateTime FechaCreacionDet{get;set;} 
       public DateTime FechaActualizacionDet{get;set;} 
       public string Socio { get; set; }
       public string Documento { get; set; }
       public int anioperiodo { get; set; }
        public string periodo { get; set; }
       public string mesperiodo { get; set; }
       public decimal MontoPeriodo { get; set; }
       public int EstadoCab { get; set; }
       public string GrupoFacturacion { get; set; }
       public int EstadoObsDetalle { get; set; }
    }
}

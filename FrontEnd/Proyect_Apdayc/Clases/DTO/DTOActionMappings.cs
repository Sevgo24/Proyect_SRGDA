using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOActionMappings
    {
        public decimal? IdmapaAccion { get; set; }
        public decimal? PrioridadAccion { get; set; }
        public decimal? CodigoAccion { get; set; }
        public decimal? CodigoAccionAux { get; set; }
        public string DescripcionAccion { get; set; }
        public decimal? CodigoTransicion { get; set; }
        public string DescripcionSeguridad { get; set; }
        public decimal? CodigoSeguridad { get; set; }
        public decimal? CodigoObjeto { get; set; }
        public string DescripcionObjeto { get; set; }
        public string IndicadorObligatorio { get; set; }
        public string IndicadorVisibilidad { get; set; }
        public decimal? CodigoAccionPrerequisito { get; set; }
        public string OrdenAccion { get; set; }
        public string OrdenAccionNew { get; set; }
        public string TiempoMaxAccion { get; set; }
        
        public decimal? Etrigger { get; set; }
        public decimal? Amtrigger { get; set; }
        
        public bool EnBD { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public bool Activo { get; set; }

        public decimal? Transicion { get; set; }
        public decimal workFlow { get; set; }
        public decimal estado { get; set; }
        //public bool valgraba { get; set; }
        //public decimal? Id { get; set; }
    }
}
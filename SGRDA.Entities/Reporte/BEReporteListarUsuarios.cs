using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
   public class BEReporteListarUsuarios

    {
         public IList<BEReporteListarUsuarios> ListarReporteUsuarios { get; set; }

         public BEReporteListarUsuarios()
       {
           ListarReporteUsuarios = new List<BEReporteListarUsuarios>();
       }
        //Vamos a cambiar  el modelo a hora sera listado de usuarios x
        //atributos
        public decimal CODIGO { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string LOCAL_EST { get; set; }
        public string TIPO_EST { get; set; }
        public string SUBTIPO_EST { get; set; }
        public string DIRECCION { get; set; }
        public string FECHA_INGRESA { get; set; }
        public string TIPO_DOC { get; set; }
        public string NRO { get; set; }
        public string ESTADO { get; set; }
        public decimal TARIFA { get; set; }
        public string PROVINCIA { get; set; }
        public string DISTRITO { get; set; }

        //public decimal cod_bec { get; set; }
        //public int cob { get; set; }
        //public string UBIGEO_EST { get; set; }
        //public string ESTABLECIMIENTO { get; set; }
    }
}

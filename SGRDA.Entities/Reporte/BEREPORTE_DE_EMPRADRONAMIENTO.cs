using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BEREPORTE_DE_EMPRADRONAMIENTO
    {
        public decimal LIC_ID { get; set; }
        public string FECHA_CREACION { get; set; }
        public string ESTABLECIMIENTO { get; set; }
        public string LIC_NAME { get; set; }
        public string TD { get; set; }
        public string SERIE { get; set; }
        public decimal NRO { get; set; }
        public string PERIODO { get; set; }
        public string FECHA_EMISION { get; set; }
        public string FECHA_CONFIRMACION { get; set; }        
        public decimal LIC_PL_ID { get; set; }
        public string NODO { get; set; }
        public string NORO { get; set; }
        public decimal INVL_NET { get; set; }
        public decimal INVL_COLLECTN { get; set; }
        public string ESTADO { get; set; }
        public string MOG_DESC { get; set; }
        public decimal EST_ID { get; set; }
        public string OFICINA_ADMINISTRATIVA { get; set; }
        public string DISTRITO { get; set; }
        public decimal COMISION { get; set; }
        public string PROVINCIA	    {get;set;}
        public string DEPARTAMENTO  {get;set;}
        public string DIRECCION     {get;set;}
        public string TIPO_EST      {get;set;}
        public string SUBTIPO_EST   {get; set;}    
        public string FLAG_PAG_ADEL {get; set;}
        public int    PAGOS         {get;set;}
        public string DESCRIPCION   {get;set;}
    }
}

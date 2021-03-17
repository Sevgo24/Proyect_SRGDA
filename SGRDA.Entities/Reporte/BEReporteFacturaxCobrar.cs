using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BEReporteFacturaxCobrar
    {
        IList<BEReporteFacturaxCobrar> ListarReporteFacturaxCobrar { get; set; }
        public BEReporteFacturaxCobrar()
        {
            ListarReporteFacturaxCobrar = new List<BEReporteFacturaxCobrar>();
        }
        //atributos
        public string FECHA         { get; set; }
        public string TD            { get; set; }
        public string SERIE         { get; set; }
        public decimal NUMERO       { get; set; }
        public decimal TOTAL        { get; set; }
        public string RUC           { get; set; }
        public string NOMBRE        { get; set; }
        public string PERIODO       { get; set; }
        public string RUBRO         { get; set; }
        public string DEPARTAMENTO  { get; set; }
        public string PROVINCIA     {get;set;}
        public string DISTRITO      {get;set;}
        public int EST_ID           { get; set; }
        public string EST_NAME      { get; set; }
        public string NODO          { get; set; }
        public string Direccion     { get; set; }
        public string TIPO_EST { get; set; }
        public string SUBTIPO_EST { get; set; }
        public string LIC_NAME { get; set; }
        public int LIC_ID { get; set; }
        public string TIPO { get; set; }
        public decimal SALDO { get; set; }
        public string MONEDA { get; set; }
        public decimal IMPORTE_ORIGINAL { get; set; }
        public string  MODALIDAD { get; set; }
        public string RUBRO_MODALIDAD { get; set; }
    }  
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class BEReporteInformeControl
    {
        public string FECHA_DIA    {get;set;}
        public string FECHA_MES    {get;set;}
        public string FECHA_ANIO   {get;set;}
        public int FACTURAS     {get;set;}
        public decimal VALOR        {get;set;}
        public int ANULADAS     {get;set;}
        public string P_ANULADOS   {get;set;}
        public int  EN_FIRME     {get;set;}
        public decimal ACUMULADO                {get;set;}
        public decimal FAC_ACUMULADAS           {get;set;}
        public decimal EN_FIRME_ACUMULADAS { get; set; }
    }
}

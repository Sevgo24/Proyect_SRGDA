using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Alfresco
{
    public class BEMigrarContrato
    {
        public string SOCIO { get; set; }
        public string REPRESENTANTE_LEGAL { get;set;}
        public decimal DNI               {get;set;}
        public string DIRECCION         {get;set;}
        public string FECHA_CONTRATO    {get;set;}
        public string MODALIDAD         {get;set;}
        public decimal RUC               {get;set;}
        public decimal COD_LICENCIA      {get;set;}
        public string DOC_PATH { get; set; }
    }
}

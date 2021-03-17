using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.BancosPagos
{
    public class BEGenerarArchivo
    {
        public List<BEGenerarArchivo> ListaGenerarArchivo { get; set; }
        public BEGenerarArchivo()
        {
            ListaGenerarArchivo = new List<BEGenerarArchivo>();
        }
      public decimal GENAR_ID          {get;set;}
      public string DESC_ARC          {get;set;}
      public decimal BNK_ID            {get;set;}
      public string FECHA_INI         {get;set;}
      public string FECHA_FIN         {get;set;}
      public decimal CANT_ARC          {get;set;}
      public decimal MONTO_TOTAL       {get;set;}
      public string LOG_USER_CREAT    {get;set;}
      public string LOG_USER_UPDATE   {get;set;}
      public string LOG_DATE_CREAT    {get;set;}
      public string LOG_DATE_UPDATE   {get;set;}
      public int TotalVirtual { get; set;}
    }
}

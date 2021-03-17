using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.BancosPagos
{
    public class BEFileBanco
    {
        public List<BEFileBanco> ListaFileBanco { get; set; }
        public BEFileBanco()
        {
            ListaFileBanco = new List<BEFileBanco>();
        }
        public int      FILE_COBRO_ID    {get;set;}
       public string    DESC_FILE        {get;set;}
       public decimal   BNK_ID           {get;set;}
       public decimal   TOTAL_CABECERAS  {get; set;}
        public string TOTAL            {get; set;}
       public string    LOG_USER_CREAT   {get;set;}
       public string    LOG_USER_UPDATE  {get;set;}
       public string LOG_DATE_CREAT   {get;set;}
       public string LOG_DATE_UPDATE   {get; set;}
        public int TotalVirtual { get; set; }
        public string MONTO_CARGADO { get; set;}
        public int TOTAL_CARGADO { get; set; }


    }
}

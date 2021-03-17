using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.BancosPagos
{
    public class BE_CargaArchivoPlano
    {
       public decimal ID_VALUE { get; set; }
       public string VTYPE { get; set; }
       public string VSUB_TYPE { get; set; }
       public string VDESC { get; set; }
       public string VALUE { get; set; }
       public decimal BNK_ID { get; set; }
       public string BNK_NAME { get; set; }
       public decimal BPS_ACC_ID { get; set; }
    }
}

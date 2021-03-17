using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEDescuentosPlantilla
    {
        public List<BEDescuentosPlantilla> ListaDescuentosPlantilla { get; set; }

        public decimal TEMP_ID_DSC { get; set; }//id de la tabla
        public String TEMP_DESC { get; set; }//descripcion
        public DateTime STARTS { get; set; }//fECHA DE INICIO
        public int TotalVirtual { get; set; }
        public decimal CHAR_ID { get; set; }
        public decimal SECC_CHARSEQ { get; set; }
        public decimal IND_TR { get; set; }
    }
}

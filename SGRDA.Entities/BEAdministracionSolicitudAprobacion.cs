using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SGRDA.Entities
{
    public class BEAdministracionSolicitudAprobacion
    {
        public decimal INV_ID { get; set; }

        public string SERIE { get; set; }

        public decimal NUMERO { get; set; }

        public string  OFICINA { get; set; }

        public string USUARIO_SOLICITANTE { get; set; }

        public string FECHA_QUIEBRA { get; set; }

        public int TIPO { get; set; }

        public decimal NETO { get; set; }

        public string ESTADO { get; set; }

        public string DESCRIPCION { get; set; }

        public string TIPO_DESC { get; set; }

        public string FECHA_EMISION { get; set; }

        public string VALUE { get; set; }

        public int INV_ESTADO_APROB { get; set;}

    }
}

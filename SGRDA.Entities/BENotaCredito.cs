using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BENotaCredito
    {
        public decimal facturaId { set; get; }
        public decimal facturaDetId { set; get; }
        public DateTime fechaEmision { set; get; }
        public int tipoNC { set; get; }
        public decimal textoTipoNC { set; get; }
        public string TipoSunat { set; get; }
        public string Observacion { set; get; }
        public string UsuarioCreacion { set; get; }
    }
}

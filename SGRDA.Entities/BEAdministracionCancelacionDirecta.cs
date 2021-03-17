using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEAdministracionCancelacionDirecta
    {
        public decimal ID { get; set;}
        public DateTime FechaCreacion { get; set; }
        public decimal TipoCancelacion { get; set; }
        public string NombreTipoCancelacion { get; set; }
        public decimal CodigoDocumento { get; set; }
        public string DescripcionTipoDocumento { get; set; }
        public string Serie { get; set; }
        public decimal NumeroDoc { get; set; }
        public DateTime FechaEmision { get; set; }
        public string NombreSocio { get; set; }
        public string DescripcionTipoMoneda { get; set; }
        public string NombreOficina { get; set; }
        public decimal CodigoOficinaSeleccionada { get; set; }
        public decimal CodigoOficinaResponsable { get; set; }
        public string NombreOficinaResponsable { get; set; }
        public string Memo { get; set; }
        public string NumMemo { get; set; }
        public string AbrevOfiMemo { get; set; }
        public DateTime MemoDate { get; set; }
        public string Referencia { get; set; }
        public decimal NetoDocumento { get; set; }
        public decimal NetoAplicar { get; set; }
        public decimal ControlId { get; set; }
        public string DescripcionControl { get; set; }
        public string Usuario { get; set; }
        public string Procedencia { get; set; }

    }
}

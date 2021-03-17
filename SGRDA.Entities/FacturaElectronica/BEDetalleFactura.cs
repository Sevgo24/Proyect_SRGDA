using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.FacturaElectronica
{
    public class BEDetalleFactura
    {
        public decimal INV_ID { get; set; }
        public string NroLinDet { get; set; }
        public string QtyItem { get; set; }
        public string UnmdItem { get; set; }
        public string VlrCodigo { get; set; }
        public string NmbItem { get; set; }
        public decimal PrcItem { get; set; }
        public decimal PrcItemSinIgv { get; set; }
        public decimal MontoItem { get; set; }
        public decimal DescuentoMonto { get; set; }
        public string IndExe { get; set; }
        public string CodigoTipoIgv { get; set; }
        public string TasaIgv { get; set; }
        public decimal ImpuestoIgv { get; set; }
        public string CodigoIsc { get; set; }
        public string CodigoTipoIsc { get; set; }
        public string MontoIsc { get; set; }
        public string TasaIsc { get; set; }
        public string Observacion { get; set; }
    }
}

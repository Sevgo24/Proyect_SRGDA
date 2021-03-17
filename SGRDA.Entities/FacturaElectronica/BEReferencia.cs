using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.FacturaElectronica
{
    public class BEReferencia
    {
        public string Owner { get; set; }
        public string NroLinRef { get; set; }
        public string TpoDocRef { get; set; }
        public string SerieRef { get; set; }
        public string FolioRef { get; set; }
        public string TipoRef { get; set; }
        public string RucRef { get; set; }
        public string FechEmisDocRef { get; set; }
        public decimal MntTotalDocRef { get; set; }
        public string MonedaDocRef { get; set; }
        public string FechOperacion { get; set; }
        public string NroOperacion { get; set; }
        public string ImporteOperacion { get; set; }
        public string MonedaOperacion { get; set; }
        public string ImporteMovimiento { get; set; }
        public string MonedaMovimiento { get; set; }
        public string FechaMovimiento { get; set; }
        public string TotalMovimiento { get; set; }
        public string Moneda { get; set; }
        public string MonedaReferencia { get; set; }
        public string MonedaObjetivo { get; set; }
        public string TipoCambio { get; set; }
        public string FechTipoCambio { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.FacturaElectronica
{
    public class BECabeceraFactura
    {
        public string Owner { get; set; }

        public decimal INV_ID { get; set; }
        public string TipoDTE { get; set; }
        public string Serie { get; set; }
        public string Correlativo { get; set; }
        public string FChEmis { get; set; }
        public string FChVen { get; set; }
        public string TipoMoneda { get; set; }
        public string RUTEmisor { get; set; }
        public string TipoRucEmis { get; set; }
        public string RznSocEmis { get; set; }
        public string NomComer { get; set; }
        public string DirEmis { get; set; }
        public string CodiComu { get; set; }
        public string CodiUsuario { get; set; }
        public string TipoRUTRecep { get; set; }
        public string RUTRecep { get; set; }
        public string RznSocRecep { get; set; }
        public string DirRecep { get; set; }
        public string Grupo { get; set; }
        public string CorreoUsuario { get; set; }
        public string Sustento { get; set; }
        public string TipoNotaCredito { get; set; }
        public decimal MntNeto { get; set; }
        public decimal MntExe { get; set; }
        public decimal MntExo { get; set; }
        public decimal MntTotalIgv { get; set; }
        public decimal MntTotal { get; set; }
        public string Total { get; set; }
        public string TipoOper { get; set; }
        public string Rubro { get; set; }
        public decimal Id_Ref { get; set; }
        public string OficinaRecaudo { get; set; }
        public string Observacion { get; set; }
        public int EsManual { get; set; }
        public string HoraEmision { get; set; }
        public string CodigoLocal { get; set; }
    }
}

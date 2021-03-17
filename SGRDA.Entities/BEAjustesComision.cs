using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEAjustesComision
    {
        public string OWNER { get; set; }
        public decimal COM_ID { get; set; }
        public decimal? COMT_ID { get; set; }
        public decimal COMT_ORIGEN { get; set; }
        public decimal BPS_ID { get; set; }
        public decimal LEVEL_ID { get; set; }
        public decimal LIC_ID { get; set; }
        public decimal COM_VALUE { get; set; }
        public decimal? COM_PERC { get; set; }
        public decimal? COM_BASE { get; set; }
        public string COM_PPIND { get; set; }
        public decimal? COM_PRIMARY { get; set; }
        public string COM_EST { get; set; }
        public decimal? PAY_ID { get; set; }
        public decimal? COM_INVOICE { get; set; }
        public DateTime? COM_LDATE { get; set; }
        public DateTime? COM_RDATE { get; set; }
        public string COM_RDESC { get; set; }
        public DateTime? COM_PDATE { get; set; }
        public decimal? COM_PNUM { get; set; }
        public DateTime? LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public int TotalVirtual { get; set; }
        public string COM_DESC { get; set; }
        public decimal SEQUENCE { get; set; }
        public DateTime? FechaIni { get; set; }
        public DateTime? FechaFin { get; set; }
        public List<BEAjustesComision> listaAjustesCom = null;
        public List<BEAjustesComision> listaRetLibComision = null;
        public List<BEAjustesComision> listaPreLiquidacionComision = null;
        public List<BEAjustesComision> listaPago = null;
        public int valgraba { get; set; }
        public BEAjustesComision()
        {
            listaAjustesCom = new List<BEAjustesComision>();
            listaRetLibComision = new List<BEAjustesComision>();
            listaPreLiquidacionComision = new List<BEAjustesComision>();
            listaPago = new List<BEAjustesComision>();
        }

        ////Identificador Retención
        public bool Retencion { get; set; }
        ////Identificador Liquidación
        public bool Liquidacion { get; set; }
        ////Identificador Pago
        public bool Pago { get; set; }
        ////datos agente
        public string BPS_NAME { get; set; }
        /// datos moneda
        public string CUR_ALPHA { get; set; }
        public string CUR_DESC { get; set; }
        /// datos modalidad
        public string MOD_ID { get; set; }
        public string MOD_DEC { get; set; }
        //datos establecimiento
        public decimal EST_ID { get; set; }
        //datos tarifa
        public decimal RATE_ID { get; set; }
        //datos oficina
        public decimal OFF_ID { get; set; }
        //datos división administrativas
        public decimal DAD_ID { get; set; }
    }
}

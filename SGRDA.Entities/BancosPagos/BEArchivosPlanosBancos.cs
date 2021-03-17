using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.BancosPagos
{
    public class BEArchivosPlanosBancos : Paginacion
    {
        public List<BEArchivosPlanosBancos> ListaArchivosPlanos { get; set; }
        public BEArchivosPlanosBancos()
        {
            ListaArchivosPlanos = new List<BEArchivosPlanosBancos>();
        }
        public string TR { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string IDENT_PAGO_ADICIONAL { get; set; }
        public string FECHA_VEN { get; set; }
        public string FECHA_BLOQUEO { get; set; }
        public string PERIODO { get; set; }
        public string IMPORTE_MAX { get; set; }
        public string IMPORTE_MIN { get; set; }
        public string INFO_ADI { get; set; }
        public string COD_SUB_01 { get; set; }
        public string VALOR_SUB_01 { get; set; }
        public string COD_SUB_02 { get; set; }
        public string VALOR_SUB_02 { get; set; }
        public string COD_SUB_03 { get; set; }
        public string VALOR_SUB_03 { get; set; }
        public string COD_SUB_04 { get; set; }
        public string VALOR_SUB_04 { get; set; }
        public string COD_SUB_05 { get; set; }
        public string VALOR_SUB_05 { get; set; }
        public string COD_SUB_06 { get; set; }
        public string VALOR_SUB_06 { get; set; }
        public string COD_SUB_07 { get; set; }
        public string VALOR_SUB_07 { get; set; }
        public string COD_SUB_08 { get; set; }
        public string VALOR_SUB_08 { get; set; }
        public string VACIO { get; set; }
        public string IMPORTE_TOTAL { get; set; }
        public string TXT { get; set; }
        public int TotalVirtual { get; set; }

        public string IMPORTE_ORIGEN { get; set; }
        public string IMPORTE_DEPOSITADO { get; set; }
        public string IMPORTE_MORA { get; set; }
        public string NORO_ID { get; set; }
        public string NRO_MOVIMIENTO { get; set; }
        public string FECHA_PAGO { get; set; }
        public string TIPO_VALOR { get; set; }
        public string CANAL_ENTRADA { get; set; }

        public string RUC { get; set; }
        public string TD { get; set; }
        public string SERIE { get; set; }
        public string NRO { get; set; }
        public string CUENTA_DESTINO { get; set; }
        public int CountCabeceras { get; set; }
        public decimal MontoCabeceras { get; set; }
    }
}
  

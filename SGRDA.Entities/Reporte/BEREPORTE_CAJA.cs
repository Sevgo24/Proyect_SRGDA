using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
   public class BEREPORTE_CAJA
    {
       public IList<BEREPORTE_CAJA> ListarReporteCaja { get; set; }

       public BEREPORTE_CAJA()
       {
           ListarReporteCaja = new List<BEREPORTE_CAJA>();
       }
       //atributos
       //COD_SOCIO
       public string rum { get; set; }
       //public string rum { get; set; }
       public int serie { get; set; }
       public int coidtfac { get; set; }
       public string periodo { get; set; }
       //RUBRO
       public string E { get; set; }
       public decimal importe { get; set; }
       public string femi { get; set; }
       public string fecan { get; set; }
       //COD_BEC
       public int coidtbec { get; set; }
       //TIPO DOCUMENTO
       public string td { get; set; }
       public int territorio { get; set; }

       //COBROS
       public string FEC_CANCELACION_DETALLE { get; set; }
       public decimal ID_COBRO { get; set; }
       public string RUBRO_NOMBRE { get; set; }
       public string RUC { get; set; }
       public string DOCUMENTO { get; set; }
       public string FEC_EMI_FACTURA { get; set; }
       public  decimal IMPORTE_DETALLE_DEPOSITO_SOLES { get; set; }
       public string TIPOCOBRO { get; set; }
       public int ANIO_CANCELACION_DETALLE { get; set; }
       public int MES_CANCELACION_DETALLE { get; set; }
       public  DateTime FEC_CANCELACION_DETALLE_DATE { get; set; }

       public string PERIODO_CONTABLE { get; set; }
       public string FECHA_INI {get;set;}
       public string FECHA_FIN { get; set; }
    }
}

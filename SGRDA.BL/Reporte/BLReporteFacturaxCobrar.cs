using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using SGRDA.DA.Reporte;
using SGRDA.Entities.Reporte;
using System.Transactions;

namespace SGRDA.BL.Reporte
{
    public class BLReporteFacturaxCobrar
    {
        //public List<BEReporteFacturaxCobrar> ListarFacturaxCobrar(string fini, string ffin, string oficina, int? rubro, string parametros)
        //{
        //    return new DAReporteFacturaxCobrar().ListarReporteFacturaxCobrar(fini, ffin, oficina, rubro, parametros);
        //}

        public List<BEReporteFacturaxCobrar> ListarFacturaxCobrar(string fini, string ffin, string oficina, int? rubro, string parametrosRubro )
        {
            return new DAReporteFacturaxCobrar().ListarReporteFacturaxCobrar(fini, ffin, oficina, rubro, parametrosRubro);
        }
        //
        public List<BEReporteFacturaxCobrar> ListarReporteFacturaxCobrar_EXCEL(string fini, string ffin, string oficina, int? rubro,string tipoenvio, string parametrosRubro)
        {
            return new DAReporteFacturaxCobrar().ListarReporteFacturaxCobrar_EXCEL(fini, ffin, oficina, rubro, tipoenvio, parametrosRubro);
        }


    }
}

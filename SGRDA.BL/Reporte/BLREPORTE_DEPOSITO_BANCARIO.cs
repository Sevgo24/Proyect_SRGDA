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
    public class BLREPORTE_DEPOSITO_BANCARIO
    {
        public List<BEREPORTE_DEPOSITO_BANCO> ReporteDepositoBancario(string fini, string ffin, string oficina, int? rubro)
        {
            return new DAREPORTE_DEPOSITO_BANCO().ListarReporteDepositoBancario(fini, ffin, oficina, rubro);
        }

        // REPORTE RECAUDACION
        public List<BEREPORTE_DEPOSITO_BANCO> ReporteDepositoBancario_Cobro(string fini, string ffin, string oficina
                    , int conFechaIngreso, int conFechaConfirmacion, string finiConfirmacion, string ffinConfirmacion
                    , int? rubro,string TipoCobro)
        {
            return new DAREPORTE_DEPOSITO_BANCO().ListarReporteDepositoBancario_Cobro(fini, ffin, oficina
                        ,conFechaIngreso,  conFechaConfirmacion,  finiConfirmacion,  ffinConfirmacion
                        ,rubro, TipoCobro);
        }

        //REPORTE CONTABLE
        public List<BEREPORTE_DEPOSITO_BANCO> ListarReporteContableDepositoBancario(string fini, string ffin, string oficina  ,decimal idContable
                    , int? rubro)
        {
            return new DAREPORTE_DEPOSITO_BANCO().ListarReporteContableDepositoBancario(fini, ffin, oficina  , idContable
                        , rubro);
        }
        public List<BE_TipoCobro> ListarTipoCobro()
        {
            return new DAREPORTE_DEPOSITO_BANCO().ListarTipoCobro();
        }

    }
}
